using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.encode;

public class MBEncoderI16x16 : Object, SaveRestore
{
	private CAVLC[] cavlc;

	private byte[][] leftRow;

	private byte[][] topLine;

	private static int[] DUMMY;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 130, 101, 101, 127, 9, 159, 9, 107,
		111, 106, 127, 1, 159, 6, 140, 107, 63, 12,
		169
	})]
	private void luma(Picture pic, int mbX, int mbY, BitWriter @out, int qp, Picture outMB, CAVLC cavlc)
	{
		int x = mbX << 4;
		int y = mbY << 4;
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 16);
		int[][] ac = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 16);
		num = (array[0] = 16);
		byte[][] pred = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		lumaDCPred(x, y, pred);
		transform(pic, 0, ac, pred, x, y);
		int[] dc = extractDC(ac);
		writeDC(cavlc, mbX, mbY, @out, qp, mbX << 2, mbY << 2, dc, MBType.___003C_003EI_16x16, MBType.___003C_003EI_16x16);
		writeAC(cavlc, mbX, mbY, @out, mbX << 2, mbY << 2, ac, qp, MBType.___003C_003EI_16x16, MBType.___003C_003EI_16x16, DUMMY);
		restorePlane(dc, ac, qp);
		for (int blk = 0; blk < (nint)ac.LongLength; blk++)
		{
			MBEncoderHelper.putBlk(outMB.getPlaneData(0), ac[blk], pred[blk], 4, H264Const.___003C_003EBLK_X[blk], H264Const.___003C_003EBLK_Y[blk], 4, 4);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 130, 101, 101, 127, 8, 127, 8, 127,
		8, 159, 8, 111, 143, 159, 12, 119, 121
	})]
	private void chroma(Picture pic, int mbX, int mbY, BitWriter @out, int qp, Picture outMB)
	{
		int x = mbX << 3;
		int y = mbY << 3;
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] ac1 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] ac2 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 16);
		num = (array[0] = 4);
		byte[][] pred1 = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 16);
		num = (array[0] = 4);
		byte[][] pred2 = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		predictChroma(pic, ac1, pred1, 1, x, y);
		predictChroma(pic, ac2, pred2, 2, x, y);
		chromaResidual(pic, mbX, mbY, @out, qp, ac1, ac2, cavlc[1], cavlc[2], MBType.___003C_003EI_16x16, MBType.___003C_003EI_16x16);
		putChroma(outMB.getData()[1], 1, x, y, ac1, pred1);
		putChroma(outMB.getData()[2], 2, x, y, ac2, pred2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 90, 66, 112, 112, 112, 144, 191, 11, 191,
		13, 191, 13, 159, 17
	})]
	private void predictChroma(Picture pic, int[][] ac, byte[][] pred, int comp, int x, int y)
	{
		chromaPredBlk0(comp, x, y, pred[0]);
		chromaPredBlk1(comp, x, y, pred[1]);
		chromaPredBlk2(comp, x, y, pred[2]);
		chromaPredBlk3(comp, x, y, pred[3]);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x, y, ac[0], pred[0], 4, 4);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x + 4, y, ac[1], pred[1], 4, 4);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x, y + 4, ac[2], pred[2], 4, 4);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x + 4, y + 4, ac[3], pred[3], 4, 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 98, 104, 136, 105, 137, 120, 152, 126,
		158, 107, 109
	})]
	public static void chromaResidual(Picture pic, int mbX, int mbY, BitWriter @out, int qp, int[][] ac1, int[][] ac2, CAVLC cavlc1, CAVLC cavlc2, MBType leftMBType, MBType topMBType)
	{
		transformChroma(ac1);
		transformChroma(ac2);
		int[] dc1 = extractDC(ac1);
		int[] dc2 = extractDC(ac2);
		writeDC(cavlc1, mbX, mbY, @out, qp, mbX << 1, mbY << 1, dc1, leftMBType, topMBType);
		writeDC(cavlc2, mbX, mbY, @out, qp, mbX << 1, mbY << 1, dc2, leftMBType, topMBType);
		writeAC(cavlc1, mbX, mbY, @out, mbX << 1, mbY << 1, ac1, qp, leftMBType, topMBType, DUMMY);
		writeAC(cavlc2, mbX, mbY, @out, mbX << 1, mbY << 1, ac2, qp, leftMBType, topMBType, DUMMY);
		restorePlane(dc1, ac1, qp);
		restorePlane(dc2, ac2, qp);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 98, 148, 148, 148, 118 })]
	private void putChroma(byte[] mb, int comp, int x, int y, int[][] ac, byte[][] pred)
	{
		MBEncoderHelper.putBlk(mb, ac[0], pred[0], 3, 0, 0, 4, 4);
		MBEncoderHelper.putBlk(mb, ac[1], pred[1], 3, 4, 0, 4, 4);
		MBEncoderHelper.putBlk(mb, ac[2], pred[2], 3, 0, 4, 4, 4);
		MBEncoderHelper.putBlk(mb, ac[3], pred[3], 3, 4, 4, 4, 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 162, 231, 72, 233, 56, 231, 74 })]
	private static void transformChroma(int[][] ac)
	{
		for (int i = 0; i < 4; i++)
		{
			CoeffTransformer.fdct4x4(ac[i]);
		}
	}

	[LineNumberTable(new byte[] { 159, 103, 66, 105, 104, 105, 7, 199 })]
	private static int[] extractDC(int[][] ac)
	{
		int[] dc = new int[(nint)ac.LongLength];
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			dc[i] = ac[i][0];
			ac[i][0] = 0;
		}
		return dc;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 66, 103, 106, 104, 127, 15, 103, 106,
		104, 159, 28, 104, 106, 136, 191, 2
	})]
	private static void writeDC(CAVLC cavlc, int mbX, int mbY, BitWriter @out, int qp, int mbLeftBlk, int mbTopBlk, int[] dc, MBType leftMBType, MBType topMBType)
	{
		if ((nint)dc.LongLength == 4)
		{
			CoeffTransformer.quantizeDC2x2(dc, qp);
			CoeffTransformer.fvdDC2x2(dc);
			cavlc.writeChrDCBlock(@out, dc, H264Const.___003C_003EtotalZeros4, 0, dc.Length, new int[4] { 0, 1, 2, 3 });
		}
		else if ((nint)dc.LongLength == 8)
		{
			CoeffTransformer.quantizeDC4x2(dc, qp);
			CoeffTransformer.fvdDC4x2(dc);
			cavlc.writeChrDCBlock(@out, dc, H264Const.___003C_003EtotalZeros8, 0, dc.Length, new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 });
		}
		else
		{
			CoeffTransformer.reorderDC4x4(dc);
			CoeffTransformer.quantizeDC4x4(dc, qp);
			CoeffTransformer.fvdDC4x4(dc);
			cavlc.writeLumaDCBlock(@out, mbLeftBlk, mbTopBlk, leftMBType, topMBType, dc, H264Const.___003C_003EtotalZeros16, 0, 16, CoeffTransformer.zigzag4x4);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 130, 105, 108, 31, 33, 231, 70 })]
	private static void writeAC(CAVLC cavlc, int mbX, int mbY, BitWriter @out, int mbLeftBlk, int mbTopBlk, int[][] ac, int qp, MBType leftMBType, MBType topMBType, int[] nc)
	{
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			CoeffTransformer.quantizeAC(ac[i], qp);
			nc[H264Const.___003C_003EBLK_INV_MAP[i]] = CAVLC.totalCoeff(cavlc.writeACBlock(@out, mbLeftBlk + H264Const.___003C_003EMB_BLK_OFF_LEFT[i], mbTopBlk + H264Const.___003C_003EMB_BLK_OFF_TOP[i], leftMBType, topMBType, ac[i], H264Const.___003C_003EtotalZeros16, 1, 15, CoeffTransformer.zigzag4x4));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 162, 102, 103, 107, 102, 103, 138, 103,
		105, 135, 104, 107, 105, 233, 61, 231, 71
	})]
	private static void restorePlane(int[] dc, int[][] ac, int qp)
	{
		if ((nint)dc.LongLength == 4)
		{
			CoeffTransformer.invDC2x2(dc);
			CoeffTransformer.dequantizeDC2x2(dc, qp, null);
		}
		else if ((nint)dc.LongLength == 8)
		{
			CoeffTransformer.invDC4x2(dc);
			CoeffTransformer.dequantizeDC4x2(dc, qp);
		}
		else
		{
			CoeffTransformer.invDC4x4(dc);
			CoeffTransformer.dequantizeDC4x4(dc, qp, null);
			CoeffTransformer.reorderDC4x4(dc);
		}
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			CoeffTransformer.dequantizeAC(ac[i], qp, null);
			ac[i][0] = dc[i];
			CoeffTransformer.idct4x4(ac[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 70, 66, 103, 101, 100, 117, 100, 152, 159,
		6, 104, 106, 51, 39, 167
	})]
	private void lumaDCPred(int x, int y, byte[][] pred)
	{
		int dc = ((x != 0 || y != 0) ? ((y == 0) ? (ArrayUtil.sumByte(leftRow[0]) + 8 >> 4) : ((x != 0) ? (ArrayUtil.sumByte(leftRow[0]) + ArrayUtil.sumByte3(topLine[0], x, 16) + 16 >> 5) : (ArrayUtil.sumByte3(topLine[0], x, 16) + 8 >> 4))) : 0);
		for (int i = 0; i < (nint)pred.LongLength; i++)
		{
			for (int j = 0; j < (nint)pred[i].LongLength; j++)
			{
				byte[] obj = pred[i];
				int num = j;
				byte[] array = obj;
				array[num] = (byte)(sbyte)(array[num] + dc);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		67,
		162,
		104,
		101,
		byte.MaxValue,
		23,
		73,
		231,
		53,
		231,
		77
	})]
	private void transform(Picture pic, int comp, int[][] ac, byte[][] pred, int x, int y)
	{
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			int[] coeff = ac[i];
			MBEncoderHelper.takeSubtract(pic.getPlaneData(comp), pic.getPlaneWidth(comp), pic.getPlaneHeight(comp), x + H264Const.___003C_003EBLK_X[i], y + H264Const.___003C_003EBLK_Y[i], coeff, pred[i], 4, 4);
			CoeffTransformer.fdct4x4(coeff);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 162, 101, 103, 124, 100, 115, 100, 147,
		99, 105, 50, 135
	})]
	private void chromaPredBlk0(int comp, int x, int y, byte[] pred)
	{
		int predY = y & 7;
		int dc = ((x != 0 && y != 0) ? chromaPredTwo(leftRow[comp], topLine[comp], predY, x) : ((x != 0) ? chromaPredOne(leftRow[comp], predY) : ((y != 0) ? chromaPredOne(topLine[comp], x) : 0)));
		for (int i = 0; i < (nint)pred.LongLength; i++)
		{
			int num = i;
			pred[num] = (byte)(sbyte)(pred[num] + dc);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 98, 101, 100, 117, 100, 147, 99, 105,
		50, 135
	})]
	private void chromaPredBlk1(int comp, int x, int y, byte[] pred)
	{
		int predY = y & 7;
		int dc = ((y != 0) ? chromaPredOne(topLine[comp], x + 4) : ((x != 0) ? chromaPredOne(leftRow[comp], predY) : 0));
		for (int i = 0; i < (nint)pred.LongLength; i++)
		{
			int num = i;
			pred[num] = (byte)(sbyte)(pred[num] + dc);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 77, 98, 101, 100, 117, 100, 147, 99, 105,
		50, 135
	})]
	private void chromaPredBlk2(int comp, int x, int y, byte[] pred)
	{
		int predY = y & 7;
		int dc = ((x != 0) ? chromaPredOne(leftRow[comp], predY + 4) : ((y != 0) ? chromaPredOne(topLine[comp], x) : 0));
		for (int i = 0; i < (nint)pred.LongLength; i++)
		{
			int num = i;
			pred[num] = (byte)(sbyte)(pred[num] + dc);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 74, 98, 101, 103, 127, 1, 100, 117, 100,
		149, 99, 105, 50, 135
	})]
	private void chromaPredBlk3(int comp, int x, int y, byte[] pred)
	{
		int predY = y & 7;
		int dc = ((x != 0 && y != 0) ? chromaPredTwo(leftRow[comp], topLine[comp], predY + 4, x + 4) : ((x != 0) ? chromaPredOne(leftRow[comp], predY + 4) : ((y != 0) ? chromaPredOne(topLine[comp], x + 4) : 0)));
		for (int i = 0; i < (nint)pred.LongLength; i++)
		{
			int num = i;
			pred[num] = (byte)(sbyte)(pred[num] + dc);
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(231)]
	private int chromaPredTwo(byte[] pix1, byte[] pix2, int x, int y)
	{
		return pix1[x] + pix1[x + 1] + pix1[x + 2] + pix1[x + 3] + pix2[y] + pix2[y + 1] + pix2[y + 2] + pix2[y + 3] + 4 >> 3;
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(227)]
	private int chromaPredOne(byte[] pix, int x)
	{
		return pix[x] + pix[x + 1] + pix[x + 2] + pix[x + 3] + 2 >> 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 105, 104, 104, 104 })]
	public MBEncoderI16x16(CAVLC[] cavlc, byte[][] leftRow, byte[][] topLine)
	{
		this.cavlc = cavlc;
		this.leftRow = leftRow;
		this.topLine = topLine;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 109, 46, 135 })]
	public virtual void save()
	{
		for (int i = 0; i < (nint)cavlc.LongLength; i++)
		{
			cavlc[i].save();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 109, 46, 135 })]
	public virtual void restore()
	{
		for (int i = 0; i < (nint)cavlc.LongLength; i++)
		{
			cavlc[i].restore();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 105, 138, 109, 138, 125, 149, 115 })]
	public virtual void encodeMacroblock(Picture pic, int mbX, int mbY, BitWriter @out, EncodedMB outMB, EncodedMB leftOutMB, EncodedMB topOutMB, int qp, int qpDelta)
	{
		CAVLCWriter.writeUE(@out, 0);
		CAVLCWriter.writeSE(@out, qpDelta);
		outMB.setType(MBType.___003C_003EI_16x16);
		outMB.setQp(qp);
		luma(pic, mbX, mbY, @out, qp, outMB.getPixels(), cavlc[0]);
		chroma(pic, mbX, mbY, @out, qp, outMB.getPixels());
		new MBDeblocker().deblockMBI(outMB, leftOutMB, topOutMB);
	}

	[LineNumberTable(319)]
	public virtual int getPredMode(Picture pic, int mbX, int mbY)
	{
		return 2;
	}

	[LineNumberTable(323)]
	public virtual int getCbpChroma(Picture pic, int mbX, int mbY)
	{
		return 2;
	}

	[LineNumberTable(327)]
	public virtual int getCbpLuma(Picture pic, int mbX, int mbY)
	{
		return 15;
	}

	[LineNumberTable(67)]
	static MBEncoderI16x16()
	{
		DUMMY = new int[16];
	}
}
