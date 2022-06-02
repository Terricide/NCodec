using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.vpx;

public class VP8Encoder : VideoEncoder
{
	private VPXBitstream bitstream;

	private byte[][] leftRow;

	private byte[][] topLine;

	private VPXQuantizer quantizer;

	private int[] tmp;

	private RateControl rc;

	private ByteBuffer headerBuffer;

	private ByteBuffer dataBuffer;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public static VP8Encoder createVP8Encoder(int qp)
	{
		VP8Encoder result = new VP8Encoder(new NopRateControl(qp));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 105, 104, 110 })]
	public VP8Encoder(RateControl rc)
	{
		this.rc = rc;
		tmp = new int[16];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 98, 104, 139, 119, 143, 141, 119, 143,
		109
	})]
	private void prepareBuffers(int mbWidth, int mbHeight)
	{
		int dataBufSize = mbHeight * mbHeight << 10;
		int headerBufSize = 256 + mbWidth * mbHeight;
		if (headerBuffer == null || headerBuffer.capacity() < headerBufSize)
		{
			headerBuffer = ByteBuffer.allocate(headerBufSize);
		}
		else
		{
			headerBuffer.clear();
		}
		if (dataBuffer == null || dataBuffer.capacity() < dataBufSize)
		{
			dataBuffer = ByteBuffer.allocate(dataBufSize);
		}
		else
		{
			dataBuffer.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 161, 68, 106, 106, 108 })]
	private void initValue(byte[][] leftRow2, byte val)
	{
		int val2 = (sbyte)val;
		Arrays.fill(leftRow2[0], (byte)val2);
		Arrays.fill(leftRow2[1], (byte)val2);
		Arrays.fill(leftRow2[2], (byte)val2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 77, 162, 101, 101, 110, 105, 110, 142, 107,
		124
	})]
	private void luma(Picture pic, int mbX, int mbY, VPXBooleanEncoder @out, int qp, Picture outMB)
	{
		int x = mbX << 4;
		int y = mbY << 4;
		int[][] ac = transform(pic, 0, qp, x, y);
		int[] dc = extractDC(ac);
		writeLumaDC(mbX, mbY, @out, qp, dc);
		writeLumaAC(mbX, mbY, @out, ac, qp);
		restorePlaneLuma(dc, ac, qp);
		putLuma(outMB.getPlaneData(0), (sbyte)lumaDCPred(x, y), ac, 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 67, 98, 101, 101, 108, 140, 114, 146, 112,
		144, 107, 118, 107, 120
	})]
	private void chroma(Picture pic, int mbX, int mbY, VPXBooleanEncoder boolEnc, int qp, Picture outMB)
	{
		int x = mbX << 3;
		int y = mbY << 3;
		int chromaPred1 = (sbyte)chromaPredBlk(1, x, y);
		int chromaPred2 = (sbyte)chromaPredBlk(2, x, y);
		int[][] ac1 = transformChroma(pic, 1, qp, x, y, outMB, chromaPred1);
		int[][] ac2 = transformChroma(pic, 2, qp, x, y, outMB, chromaPred2);
		writeChroma(1, mbX, mbY, boolEnc, ac1, qp);
		writeChroma(2, mbX, mbY, boolEnc, ac2, qp);
		restorePlaneChroma(ac1, qp);
		putChroma(outMB.getData()[1], 1, x, y, ac1, chromaPred1);
		restorePlaneChroma(ac2, qp);
		putChroma(outMB.getData()[2], 2, x, y, ac2, chromaPred2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 81, 130, 127, 0, 123, 155, 122, 120, 122 })]
	private void collectPredictors(Picture outMB, int mbX)
	{
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(0), 240, topLine[0], mbX << 4, 16);
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(1), 56, topLine[1], mbX << 3, 8);
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(2), 56, topLine[2], mbX << 3, 8);
		copyCol(outMB.getPlaneData(0), 15, 16, leftRow[0]);
		copyCol(outMB.getPlaneData(1), 7, 8, leftRow[1]);
		copyCol(outMB.getPlaneData(2), 7, 8, leftRow[2]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 130, 104, 107, 154, 109, 109, 131, 109,
		131, 237, 54, 234, 77, 105, 63, 2, 169
	})]
	private int[] calcSegmentProbs(int[] segmentMap)
	{
		int[] result = new int[3];
		for (int j = 0; j < (nint)segmentMap.LongLength; j++)
		{
			switch (segmentMap[j])
			{
			case 0:
			{
				int num = 0;
				int[] array = result;
				array[num]++;
				num = 1;
				array = result;
				array[num]++;
				break;
			}
			case 1:
			{
				int num = 0;
				int[] array = result;
				array[num]++;
				break;
			}
			case 2:
			{
				int num = 2;
				int[] array = result;
				array[num]++;
				break;
			}
			}
		}
		for (int i = 0; i < 3; i++)
		{
			int num2 = i;
			int num3 = result[i] << 8;
			nint num4 = (nint)segmentMap.LongLength;
			result[num2] = MathUtil.clip((int)((num4 != -1) ? (num3 / num4) : (-num3)), 1, 255);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 98, 109, 109, 141, 109, 141, 141, 104,
		109, 108, 237, 61, 231, 69, 104, 45, 167, 109,
		109, 109, 141, 103, 109, 12, 231, 69, 109, 106,
		106, 109, 106, 108, 109, 109, 109, 109, 109, 141,
		103, 109, 112, 112, 115, 53, 41, 41, 44, 236,
		73, 111
	})]
	private void writeHeader2(VPXBooleanEncoder boolEnc, int[] segmentQps, int[] probs)
	{
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 1);
		boolEnc.writeBit(128, 1);
		boolEnc.writeBit(128, 1);
		boolEnc.writeBit(128, 1);
		for (int l = 0; l < (nint)segmentQps.LongLength; l++)
		{
			boolEnc.writeBit(128, 1);
			writeInt(boolEnc, segmentQps[l], 7);
			boolEnc.writeBit(128, 0);
		}
		for (int k = segmentQps.Length; k < 4; k++)
		{
			boolEnc.writeBit(128, 0);
		}
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		for (int j = 0; j < 3; j++)
		{
			boolEnc.writeBit(128, 1);
			writeInt(boolEnc, probs[j], 8);
		}
		boolEnc.writeBit(128, 0);
		writeInt(boolEnc, 1, 6);
		writeInt(boolEnc, 0, 3);
		boolEnc.writeBit(128, 0);
		writeInt(boolEnc, 0, 2);
		writeInt(boolEnc, segmentQps[0], 7);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		boolEnc.writeBit(128, 0);
		int[][][][] probFlags = VPXConst.tokenProbUpdateFlagProbs;
		for (int i = 0; i < (nint)probFlags.LongLength; i++)
		{
			for (int m = 0; m < (nint)probFlags[i].LongLength; m++)
			{
				for (int n = 0; n < (nint)probFlags[i][m].LongLength; n++)
				{
					for (int l2 = 0; l2 < (nint)probFlags[i][m][n].LongLength; l2++)
					{
						boolEnc.writeBit(probFlags[i][m][n][l2], 0);
					}
				}
			}
		}
		boolEnc.writeBit(128, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 66, 103, 107, 113 })]
	private void writeSegmetId(VPXBooleanEncoder boolEnc, int id, int[] probs)
	{
		int bit1 = (id >> 1) & 1;
		boolEnc.writeBit(probs[0], bit1);
		boolEnc.writeBit(probs[1 + bit1], id & 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 85, 130, 103, 144, 112, 114, 147, 106, 105,
		138, 106, 106
	})]
	private void writeHeader(ByteBuffer @out, int width, int height, int firstPart)
	{
		int version = 0;
		int type = 0;
		int showFrame = 1;
		int header = (firstPart << 5) | (showFrame << 4) | (version << 1) | type;
		@out.put((byte)(sbyte)((uint)header & 0xFFu));
		@out.put((byte)(sbyte)((uint)(header >> 8) & 0xFFu));
		@out.put((byte)(sbyte)((uint)(header >> 16) & 0xFFu));
		@out.put(157);
		@out.put(1);
		@out.put(42);
		@out.putShort((short)width);
		@out.putShort((short)height);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 86, 98, 105, 52, 135 })]
	internal virtual void writeInt(VPXBooleanEncoder boolEnc, int data, int bits)
	{
		for (int bit = bits - 1; bit >= 0; bit += -1)
		{
			boolEnc.writeBit(128, 1 & (data >> bit));
		}
	}

	[LineNumberTable(new byte[] { 159, 78, 66, 105, 104, 6, 199 })]
	private void copyCol(byte[] planeData, int off, int stride, byte[] @out)
	{
		for (int i = 0; i < (nint)@out.LongLength; i++)
		{
			@out[i] = planeData[off];
			off += stride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 38, 130, 141, 127, 8, 106, 103, 105, 104,
		159, 10, 232, 58, 233, 72
	})]
	private int[][] transform(Picture pic, int comp, int qp, int x, int y)
	{
		int dcc = (sbyte)lumaDCPred(x, y);
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 16);
		int[][] ac = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			int[] coeff = ac[i];
			int blkOffX = (i & 3) << 2;
			int blkOffY = i & -4;
			takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x + blkOffX, y + blkOffY, coeff, dcc);
			VPXDCT.fdct4x4(coeff);
		}
		return ac;
	}

	[LineNumberTable(new byte[] { 159, 43, 130, 105, 104, 41, 167 })]
	private int[] extractDC(int[][] ac)
	{
		int[] dc = new int[(nint)ac.LongLength];
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			dc[i] = ac[i][0];
		}
		return dc;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 130, 104, 112, 126 })]
	private void writeLumaDC(int mbX, int mbY, VPXBooleanEncoder @out, int qp, int[] dc)
	{
		VPXDCT.walsh4x4(dc);
		quantizer.quantizeY2(dc, qp);
		bitstream.encodeCoeffsWHT(@out, zigzag(dc, tmp), mbX);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 74, 162, 104, 114, 31, 5, 199 })]
	private void writeLumaAC(int mbX, int mbY, VPXBooleanEncoder @out, int[][] ac, int qp)
	{
		for (int i = 0; i < 16; i++)
		{
			quantizer.quantizeY(ac[i], qp);
			bitstream.encodeCoeffsDCT15(@out, zigzag(ac[i], tmp), mbX, i & 3, i >> 2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 45, 66, 110, 103, 104, 112, 105, 233, 61,
		231, 69
	})]
	private void restorePlaneLuma(int[] dc, int[][] ac, int qp)
	{
		quantizer.dequantizeY2(dc, qp);
		VPXDCT.iwalsh4x4(dc);
		for (int i = 0; i < 16; i++)
		{
			quantizer.dequantizeY(ac[i], qp);
			ac[i][0] = dc[i];
			VPXDCT.idct4x4(ac[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 41, 130, 103, 131, 100, 116, 100, 151 })]
	private byte lumaDCPred(int x, int y)
	{
		if (x == 0 && y == 0)
		{
			return 0;
		}
		if (y == 0)
		{
			return (byte)(sbyte)(ArrayUtil.sumByte(leftRow[0]) + 8 >> 4);
		}
		if (x == 0)
		{
			return (byte)(sbyte)(ArrayUtil.sumByte3(topLine[0], x, 16) + 8 >> 4);
		}
		return (byte)(sbyte)(ArrayUtil.sumByte(leftRow[0]) + ArrayUtil.sumByte3(topLine[0], x, 16) + 16 >> 5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 52, 98, 104, 103, 102, 240, 61, 231, 69 })]
	private void putLuma(byte[] planeData, int pred, int[][] ac, int log2stride)
	{
		for (int blk = 0; blk < (nint)ac.LongLength; blk++)
		{
			int blkOffX = (blk & 3) << 2;
			int blkOffY = blk & -4;
			putBlk(planeData, pred, ac[blk], log2stride, blkOffX, blkOffY);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 69, 162, 104, 45, 135 })]
	private int[] zigzag(int[] zz, int[] tmp2)
	{
		for (int i = 0; i < 16; i++)
		{
			tmp2[i] = zz[VPXConst.___003C_003Ezigzag[i]];
		}
		return tmp2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 55, 98, 101, 103, 125, 100, 116, 100, 148 })]
	private byte chromaPredBlk(int comp, int x, int y)
	{
		int predY = y & 7;
		if (x != 0 && y != 0)
		{
			sbyte result = (sbyte)chromaPredTwo(leftRow[comp], topLine[comp], predY, x);
			
			return (byte)result;
		}
		if (x != 0)
		{
			sbyte result2 = (sbyte)chromaPredOne(leftRow[comp], predY);
			
			return (byte)result2;
		}
		if (y != 0)
		{
			sbyte result3 = (sbyte)chromaPredOne(topLine[comp], x);
			
			return (byte)result3;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 63, 162, 159, 7, 136, 104, 104, 159, 12,
		233, 58, 231, 73
	})]
	private int[][] transformChroma(Picture pic, int comp, int qp, int x, int y, Picture outMB, int chromaPred)
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] ac = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int blk = 0; blk < (nint)ac.LongLength; blk++)
		{
			int blkOffX = (blk & 1) << 2;
			int blkOffY = blk >> 1 << 2;
			takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x + blkOffX, y + blkOffY, ac[blk], chromaPred);
			VPXDCT.fdct4x4(ac[blk]);
		}
		return ac;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 70, 66, 103, 114, 31, 7, 199 })]
	private void writeChroma(int comp, int mbX, int mbY, VPXBooleanEncoder boolEnc, int[][] ac, int qp)
	{
		for (int i = 0; i < 4; i++)
		{
			quantizer.quantizeUV(ac[i], qp);
			bitstream.encodeCoeffsDCTUV(boolEnc, zigzag(ac[i], tmp), comp, mbX, i & 1, i >> 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 47, 98, 103, 112, 9, 199 })]
	private void restorePlaneChroma(int[][] ac, int qp)
	{
		for (int i = 0; i < 4; i++)
		{
			quantizer.dequantizeUV(ac[i], qp);
			VPXDCT.idct4x4(ac[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 59, 130, 103, 57, 135 })]
	private void putChroma(byte[] mb, int comp, int x, int y, int[][] ac, int chromaPred)
	{
		for (int blk = 0; blk < 4; blk++)
		{
			putBlk(mb, chromaPred, ac[blk], 3, (blk & 1) << 2, blk >> 1 << 2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 34, 98, 111, 148, 148 })]
	private void takeSubtract(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, int dc)
	{
		if (x + 4 < planeWidth && y + 4 < planeHeight)
		{
			takeSubtractSafe(planeData, planeWidth, planeHeight, x, y, coeff, dc);
		}
		else
		{
			takeSubtractUnsafe(planeData, planeWidth, planeHeight, x, y, coeff, dc);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 50, 98, 105, 120, 115, 119, 119, 119, 101,
		229, 58, 234, 72
	})]
	private void putBlk(byte[] planeData, int pred, int[] block, int log2stride, int blkX, int blkY)
	{
		int stride = 1 << log2stride;
		int line = 0;
		int srcOff = 0;
		int dstOff = (blkY << log2stride) + blkX;
		for (; line < 4; line++)
		{
			planeData[dstOff] = (byte)(sbyte)MathUtil.clip(block[srcOff] + pred, -128, 127);
			planeData[dstOff + 1] = (byte)(sbyte)MathUtil.clip(block[srcOff + 1] + pred, -128, 127);
			planeData[dstOff + 2] = (byte)(sbyte)MathUtil.clip(block[srcOff + 2] + pred, -128, 127);
			planeData[dstOff + 3] = (byte)(sbyte)MathUtil.clip(block[srcOff + 3] + pred, -128, 127);
			srcOff += 4;
			dstOff += stride;
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(343)]
	private byte chromaPredTwo(byte[] pix1, byte[] pix2, int x, int y)
	{
		return (byte)(sbyte)(pix1[x] + pix1[x + 1] + pix1[x + 2] + pix1[x + 3] + pix1[x + 4] + pix1[x + 5] + pix1[x + 6] + pix1[x + 7] + pix2[y] + pix2[y + 1] + pix2[y + 2] + pix2[y + 3] + pix2[y + 4] + pix2[y + 5] + pix2[y + 6] + pix2[y + 7] + 8 >> 4);
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(339)]
	private byte chromaPredOne(byte[] pix, int x)
	{
		return (byte)(sbyte)(pix[x] + pix[x + 1] + pix[x + 2] + pix[x + 3] + pix[x + 4] + pix[x + 5] + pix[x + 6] + pix[x + 7] + 4 >> 3);
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 32, 130, 113, 107, 111, 111, 239, 60, 239,
		70
	})]
	private void takeSubtractSafe(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, int dc)
	{
		int i = 0;
		int srcOff = y * planeWidth + x;
		int dstOff = 0;
		while (i < 4)
		{
			coeff[dstOff] = planeData[srcOff] - dc;
			coeff[dstOff + 1] = planeData[srcOff + 1] - dc;
			coeff[dstOff + 2] = planeData[srcOff + 2] - dc;
			coeff[dstOff + 3] = planeData[srcOff + 3] - dc;
			i++;
			srcOff += planeWidth;
			dstOff += 4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 29, 66, 163, 116, 142, 113, 51, 135, 101,
		104, 47, 231, 58, 234, 73, 107, 145, 115, 54,
		137, 103, 105, 48, 233, 58, 234, 73
	})]
	private void takeSubtractUnsafe(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, int dc)
	{
		int outOff = 0;
		int i;
		for (i = y; i < Math.min(y + 4, planeHeight); i++)
		{
			int off2 = i * planeWidth + Math.min(x, planeWidth);
			int k;
			for (k = x; k < Math.min(x + 4, planeWidth); k++)
			{
				int num = outOff;
				outOff++;
				int num2 = off2;
				off2++;
				coeff[num] = planeData[num2] - dc;
			}
			off2 += -1;
			for (; k < x + 4; k++)
			{
				int num3 = outOff;
				outOff++;
				coeff[num3] = planeData[off2] - dc;
			}
		}
		for (; i < y + 4; i++)
		{
			int off = planeHeight * planeWidth - planeWidth + Math.min(x, planeWidth);
			int j;
			for (j = x; j < Math.min(x + 4, planeWidth); j++)
			{
				int num4 = outOff;
				outOff++;
				int num5 = off;
				off++;
				coeff[num4] = planeData[num5] - dc;
			}
			off += -1;
			for (; j < x + 4; j++)
			{
				int num6 = outOff;
				outOff++;
				coeff[num6] = planeData[off] - dc;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 104, 109, 141, 137, 119, 127, 10,
		127, 15, 110, 142, 140, 144, 142, 142, 171, 111,
		142, 140, 106, 110, 136, 116, 148, 150, 234, 53,
		242, 61, 236, 81, 104, 141, 110, 139, 173, 111,
		105, 176, 110, 110, 174, 238, 55, 47, 236, 77,
		104, 141, 109, 127, 0, 110, 142, 136
	})]
	public override EncodedFrame encodeFrame(Picture pic, ByteBuffer _buf)
	{
		ByteBuffer @out = _buf.duplicate();
		int mbWidth = pic.getWidth() + 15 >> 4;
		int mbHeight = pic.getHeight() + 15 >> 4;
		prepareBuffers(mbWidth, mbHeight);
		VPXBitstream.___003Cclinit_003E();
		bitstream = new VPXBitstream(VPXConst.___003C_003EtokenDefaultBinProbs, mbWidth);
		leftRow = new byte[3][]
		{
			new byte[16],
			new byte[8],
			new byte[8]
		};
		topLine = new byte[3][]
		{
			new byte[mbWidth << 4],
			new byte[mbWidth << 3],
			new byte[mbWidth << 3]
		};
		initValue(leftRow, 1);
		initValue(topLine, byte.MaxValue);
		quantizer = new VPXQuantizer();
		Picture outMB = Picture.create(16, 16, ColorSpace.___003C_003EYUV420);
		int[] segmentQps = rc.getSegmentQps();
		VPXBooleanEncoder boolEnc = new VPXBooleanEncoder(dataBuffer);
		int[] segmentMap = new int[mbWidth * mbHeight];
		int mbY2 = 0;
		int mbAddr2 = 0;
		for (; mbY2 < mbHeight; mbY2++)
		{
			initValue(leftRow, 1);
			int mbX2 = 0;
			while (mbX2 < mbWidth)
			{
				int before = boolEnc.position();
				int segment = (segmentMap[mbAddr2] = rc.getSegment());
				luma(pic, mbX2, mbY2, boolEnc, segmentQps[segment], outMB);
				chroma(pic, mbX2, mbY2, boolEnc, segmentQps[segment], outMB);
				rc.report(boolEnc.position() - before);
				collectPredictors(outMB, mbX2);
				mbX2++;
				mbAddr2++;
			}
		}
		boolEnc.stop();
		dataBuffer.flip();
		boolEnc = new VPXBooleanEncoder(headerBuffer);
		int[] probs = calcSegmentProbs(segmentMap);
		writeHeader2(boolEnc, segmentQps, probs);
		int mbY = 0;
		int mbAddr = 0;
		for (; mbY < mbHeight; mbY++)
		{
			int mbX = 0;
			while (mbX < mbWidth)
			{
				writeSegmetId(boolEnc, segmentMap[mbAddr], probs);
				boolEnc.writeBit(145, 1);
				boolEnc.writeBit(156, 0);
				boolEnc.writeBit(163, 0);
				boolEnc.writeBit(142, 0);
				mbX++;
				mbAddr++;
			}
		}
		boolEnc.stop();
		headerBuffer.flip();
		@out.order(ByteOrder.LITTLE_ENDIAN);
		writeHeader(@out, pic.getWidth(), pic.getHeight(), headerBuffer.remaining());
		@out.put(headerBuffer);
		@out.put(dataBuffer);
		@out.flip();
		EncodedFrame result = new EncodedFrame(@out, keyFrame: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(477)]
	public override ColorSpace[] getSupportedColorSpaces()
	{
		return new ColorSpace[1] { ColorSpace.___003C_003EYUV420J };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(482)]
	public override int estimateBufferSize(Picture frame)
	{
		return frame.getWidth() * frame.getHeight() / 2;
	}
}
