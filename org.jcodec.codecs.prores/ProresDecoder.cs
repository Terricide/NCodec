using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.codecs.prores;

public class ProresDecoder : VideoDecoder
{
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] table;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] mask;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 105 })]
	public ProresDecoder()
	{
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 127, 98, 111, 102, 137 })]
	public static int nZeros(int check16Bit)
	{
		int low = table[check16Bit & 0xFF];
		check16Bit >>= 8;
		int high = table[check16Bit];
		return high + (mask[high] & low);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(88)]
	public static int golumbSign(int val)
	{
		return -(val & 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 125, 98, 109, 139, 106, 106, 102, 107, 119,
		106, 154
	})]
	public static int readCodeword(BitReader reader, Codebook codebook)
	{
		int q = nZeros(reader.check16Bits());
		reader.skipFast(q + 1);
		if (q > codebook.switchBits)
		{
			int bits = codebook.golombBits + q;
			if (bits > 16)
			{
				Logger.error("Broken prores slice");
			}
			return ((1 << bits) | reader.readFast16(bits)) - codebook.golombOffset;
		}
		if (codebook.riceOrder > 0)
		{
			return (q << codebook.riceOrder) | reader.readFast16(codebook.riceOrder);
		}
		return q;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(84)]
	public static int golumbToSigned(int val)
	{
		return (val >> 1) ^ golumbSign(val);
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(150)]
	private static int qScale(int[] qMat, int ind, int val)
	{
		return val * qMat[ind] >> 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 109, 101, 162, 104, 146, 105, 108,
		117, 101, 194, 100, 140, 131, 111, 243, 51, 243,
		79
	})]
	public static void readDCCoeffs(BitReader bits, int[] qMat, int[] @out, int blocksPerSlice, int blkSize)
	{
		int c = readCodeword(bits, ProresConsts.firstDCCodebook);
		if (c < 0)
		{
			return;
		}
		int prevDc = golumbToSigned(c);
		@out[0] = 4096 + qScale(qMat, 0, prevDc);
		int code = 5;
		int sign = 0;
		int idx = blkSize;
		int i = 1;
		while (i < blocksPerSlice)
		{
			code = readCodeword(bits, ProresConsts.___003C_003EdcCodebooks[java.lang.Math.min(code, 6)]);
			if (code < 0)
			{
				break;
			}
			sign = ((code != 0) ? (sign ^ golumbSign(code)) : 0);
			prevDc += MathUtil.toSigned(code + 1 >> 1, sign);
			@out[idx] = 4096 + qScale(qMat, 0, prevDc);
			i++;
			idx += blkSize;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Protected | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 112, 66, 99, 131, 101, 104, 138, 100, 118,
		118, 111, 162, 137, 120, 109, 162, 106, 106, 103,
		99, 127, 4, 102
	})]
	protected internal static void readACCoeffs(BitReader bits, int[] qMat, int[] @out, int blocksPerSlice, int[] scan, int max, int log2blkSize)
	{
		int run = 4;
		int level = 2;
		int blockMask = blocksPerSlice - 1;
		int log2BlocksPerSlice = MathUtil.log2(blocksPerSlice);
		int maxCoeffs = 64 << log2BlocksPerSlice;
		int pos = blockMask;
		while (bits.remaining() > 32 || bits.checkAllBits() != 0)
		{
			run = readCodeword(bits, ProresConsts.___003C_003ErunCodebooks[java.lang.Math.min(run, 15)]);
			if (run < 0 || run >= maxCoeffs - pos - 1)
			{
				break;
			}
			pos += run + 1;
			level = readCodeword(bits, ProresConsts.___003C_003ElevCodebooks[java.lang.Math.min(level, 9)]) + 1;
			if (level < 0 || level > 65535)
			{
				break;
			}
			int sign = -bits.read1Bit();
			int ind = pos >> log2BlocksPerSlice;
			if (ind >= max)
			{
				break;
			}
			@out[((pos & blockMask) << log2blkSize) + scan[ind]] = qScale(qMat, ind, MathUtil.toSigned(level, sign));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 66, 136, 110, 142, 101, 144, 155, 177,
		105, 191, 14, 191, 22, 223, 22, 152, 120, 63,
		2
	})]
	public virtual Picture decodeFrameHiBD(ByteBuffer data, byte[][] target, byte[][] lowBits)
	{
		ProresConsts.FrameHeader fh = readFrameHeader(data);
		int codedWidth = (fh.width + 15) & -16;
		int codedHeight = (fh.height + 15) & -16;
		int lumaSize = codedWidth * codedHeight;
		int chromaSize = lumaSize >> 3 - fh.chromaType;
		if (target == null || (nint)target[0].LongLength < lumaSize || (nint)target[1].LongLength < chromaSize || (nint)target[2].LongLength < chromaSize)
		{
			
			throw new RuntimeException("Provided output picture won't fit into provided buffer");
		}
		if (fh.frameType == 0)
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight, codedWidth >> 4, fh.qMatLuma, fh.qMatChroma, fh.scan, 0, fh.chromaType);
		}
		else
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 4, fh.qMatLuma, fh.qMatChroma, fh.scan, fh.topFieldFirst ? 1 : 2, fh.chromaType);
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 4, fh.qMatLuma, fh.qMatChroma, fh.scan, (!fh.topFieldFirst) ? 1 : 2, fh.chromaType);
		}
		ColorSpace color = ((fh.chromaType != 2) ? ColorSpace.___003C_003EYUV444 : ColorSpace.___003C_003EYUV422);
		Picture result = new Picture(codedWidth, codedHeight, target, lowBits, color, (lowBits != null) ? 2 : 0, new Rect(0, 0, fh.width & color.getWidthMask(), fh.height & color.getHeightMask()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 162, 104, 104, 110, 145, 104, 136, 137,
		105, 137, 138, 105, 169, 132, 101, 138, 104, 102,
		164, 106, 106, 106, 106, 106, 138, 144, 106, 138,
		106, 141, 169, 106, 141, 169, 99, 63, 10, 167
	})]
	public static ProresConsts.FrameHeader readFrameHeader(ByteBuffer inp)
	{
		int frameSize = inp.getInt();
		string sig = readSig(inp);
		if (!java.lang.String.instancehelper_equals("icpf", sig))
		{
			
			throw new RuntimeException("Not a prores frame");
		}
		int hdrSize = inp.getShort();
		int version = inp.getShort();
		int res1 = inp.getInt();
		int width = inp.getShort();
		int height = inp.getShort();
		int flags1 = (sbyte)inp.get();
		int frameType = (flags1 >> 2) & 3;
		int chromaType = (flags1 >> 6) & 3;
		int topFieldFirst = 0;
		int[] scan;
		if (frameType == 0)
		{
			scan = ProresConsts.progressive_scan;
		}
		else
		{
			scan = ProresConsts.interlaced_scan;
			if (frameType == 1)
			{
				topFieldFirst = 1;
			}
		}
		int res2 = (sbyte)inp.get();
		int prim = (sbyte)inp.get();
		int transFunc = (sbyte)inp.get();
		int matrix = (sbyte)inp.get();
		int pixFmt = (sbyte)inp.get();
		int res3 = (sbyte)inp.get();
		int flags2 = (sbyte)inp.get() & 0xFF;
		int[] qMatLuma = new int[64];
		int[] qMatChroma = new int[64];
		if (hasQMatLuma(flags2))
		{
			readQMat(inp, qMatLuma, scan);
		}
		else
		{
			Arrays.fill(qMatLuma, 4);
		}
		if (hasQMatChroma(flags2))
		{
			readQMat(inp, qMatChroma, scan);
		}
		else
		{
			Arrays.fill(qMatChroma, 4);
		}
		inp.position(inp.position() + hdrSize - (20 + (hasQMatLuma(flags2) ? 64 : 0) + (hasQMatChroma(flags2) ? 64 : 0)));
		ProresConsts.FrameHeader result = new ProresConsts.FrameHeader(frameSize - hdrSize - 8, width, height, frameType, (byte)topFieldFirst != 0, scan, qMatLuma, qMatChroma, chromaType);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 64, 162, 136, 101, 109, 146, 104, 135, 191,
		17, 101, 102, 109, 99, 229, 52, 236, 79
	})]
	protected internal virtual void decodePicture(ByteBuffer data, byte[][] result, byte[][] lowBits, int width, int height, int mbWidth, int[] qMatLuma, int[] qMatChroma, int[] scan, int pictureType, int chromaType)
	{
		ProresConsts.PictureHeader ph = readPictureHeader(data);
		int mbX = 0;
		int mbY = 0;
		int sliceMbCount = 1 << ph.log2SliceMbWidth;
		for (int i = 0; i < (nint)ph.sliceSizes.LongLength; i++)
		{
			while (mbWidth - mbX < sliceMbCount)
			{
				sliceMbCount >>= 1;
			}
			decodeSlice(NIOUtils.read(data, ph.sliceSizes[i]), qMatLuma, qMatChroma, scan, sliceMbCount, mbX, mbY, ph.sliceSizes[i], result, lowBits, width, pictureType, chromaType);
			mbX += sliceMbCount;
			if (mbX == mbWidth)
			{
				sliceMbCount = 1 << ph.log2SliceMbWidth;
				mbX = 0;
				mbY++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 91, 162, 136, 110, 142, 101, 134, 108, 159,
		2, 177, 159, 23, 115, 39, 162, 101, 103, 191,
		31, 177, 191, 47, 191, 47, 117, 117, 7
	})]
	public virtual Picture[] decodeFieldsHiBD(ByteBuffer data, byte[][][] target, byte[][][] lowBits)
	{
		ProresConsts.FrameHeader fh = readFrameHeader(data);
		int codedWidth = (fh.width + 15) & -16;
		int codedHeight = (fh.height + 15) & -16;
		int lumaSize = codedWidth * codedHeight;
		int chromaSize = lumaSize >> 1;
		if (fh.frameType == 0)
		{
			if (target == null || (nint)target[0][0].LongLength < lumaSize || (nint)target[0][1].LongLength < chromaSize || (nint)target[0][2].LongLength < chromaSize)
			{
				
				throw new RuntimeException("Provided output picture won't fit into provided buffer");
			}
			decodePicture(data, target[0], lowBits[0], fh.width, fh.height, codedWidth >> 4, fh.qMatLuma, fh.qMatChroma, fh.scan, 0, fh.chromaType);
			return new Picture[1] { Picture.createPicture(codedWidth, codedHeight, target[0], ColorSpace.___003C_003EYUV422) };
		}
		lumaSize >>= 1;
		chromaSize >>= 1;
		if (target == null || (nint)target[0][0].LongLength < lumaSize || (nint)target[0][1].LongLength < chromaSize || (nint)target[0][2].LongLength < chromaSize || (nint)target[1][0].LongLength < lumaSize || (nint)target[1][1].LongLength < chromaSize || (nint)target[1][2].LongLength < chromaSize)
		{
			
			throw new RuntimeException("Provided output picture won't fit into provided buffer");
		}
		decodePicture(data, target[(!fh.topFieldFirst) ? 1 : 0], lowBits[(!fh.topFieldFirst) ? 1 : 0], fh.width, fh.height >> 1, codedWidth >> 4, fh.qMatLuma, fh.qMatChroma, fh.scan, 0, fh.chromaType);
		decodePicture(data, target[fh.topFieldFirst ? 1 : 0], lowBits[(!fh.topFieldFirst) ? 1 : 0], fh.width, fh.height >> 1, codedWidth >> 4, fh.qMatLuma, fh.qMatChroma, fh.scan, 0, fh.chromaType);
		return new Picture[2]
		{
			Picture.createPicture(codedWidth, codedHeight >> 1, target[0], ColorSpace.___003C_003EYUV422),
			Picture.createPicture(codedWidth, codedHeight >> 1, target[1], ColorSpace.___003C_003EYUV422)
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 65, 66, 104, 105 })]
	internal static string readSig(ByteBuffer inp)
	{
		byte[] sig = new byte[4];
		inp.get(sig);
		string result = Platform.stringFromBytes(sig);
		
		return result;
	}

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(463)]
	internal static bool hasQMatLuma(int flags2)
	{
		return (((uint)flags2 & 2u) != 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 29, 162, 105, 105, 104, 41, 167 })]
	internal static void readQMat(ByteBuffer inp, int[] qMatLuma, int[] scan)
	{
		byte[] b = new byte[64];
		inp.get(b);
		for (int i = 0; i < 64; i++)
		{
			qMatLuma[i] = b[scan[i]];
		}
	}

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(451)]
	internal static bool hasQMatChroma(int flags2)
	{
		return (((uint)flags2 & (true ? 1u : 0u)) != 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 58, 98, 113, 104, 136, 111, 133, 105, 105,
		44, 169
	})]
	public static ProresConsts.PictureHeader readPictureHeader(ByteBuffer inp)
	{
		int hdrSize = ((sbyte)inp.get() & 0xFF) >> 3;
		inp.getInt();
		int sliceCount = inp.getShort();
		int a = (sbyte)inp.get() & 0xFF;
		int log2SliceMbWidth = a >> 4;
		short[] sliceSizes = new short[sliceCount];
		for (int i = 0; i < sliceCount; i++)
		{
			sliceSizes[i] = inp.getShort();
		}
		ProresConsts.PictureHeader result = new ProresConsts.PictureHeader(log2SliceMbWidth, sliceSizes);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 54, 129, 68, 113, 122, 115, 104, 105, 151,
		108, 127, 3, 109, 108, 127, 2, 108, 159, 2,
		159, 11
	})]
	private void decodeSlice(ByteBuffer data, int[] qMatLuma, int[] qMatChroma, int[] scan, int sliceMbCount, int mbX, int mbY, short sliceSize, byte[][] result, byte[][] lowBits, int lumaStride, int pictureType, int chromaType)
	{
		int hdrSize = ((sbyte)data.get() & 0xFF) >> 3;
		int qScale = clip((sbyte)data.get() & 0xFF, 1, 224);
		qScale = ((qScale <= 128) ? qScale : (qScale - 96 << 2));
		int yDataSize = data.getShort();
		int uDataSize = data.getShort();
		int vDataSize = ((hdrSize <= 7) ? (sliceSize - uDataSize - yDataSize - hdrSize) : data.getShort());
		int[] y = new int[sliceMbCount << 8];
		decodeOnePlane(bitstream(data, yDataSize), sliceMbCount << 2, y, scaleMat(qMatLuma, qScale), scan, mbX, mbY, 0);
		int chromaBlkCount = sliceMbCount << chromaType >> 1;
		int[] u = new int[chromaBlkCount << 6];
		decodeOnePlane(bitstream(data, uDataSize), chromaBlkCount, u, scaleMat(qMatChroma, qScale), scan, mbX, mbY, 1);
		int[] v = new int[chromaBlkCount << 6];
		decodeOnePlane(bitstream(data, vDataSize), chromaBlkCount, v, scaleMat(qMatChroma, qScale), scan, mbX, mbY, 2);
		putSlice(result, lowBits, lumaStride, mbX, mbY, y, u, v, (pictureType != 0) ? 1 : 0, (pictureType == 2) ? 1 : 0, chromaType, sliceMbCount);
	}

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(386)]
	internal static int clip(int val, int min, int max)
	{
		return (val < min) ? min : ((val <= max) ? val : max);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(382)]
	internal static BitReader bitstream(ByteBuffer data, int dataSize)
	{
		BitReader result = BitReader.createBitReader(NIOUtils.read(data, dataSize));
		
		return result;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 49, 130, 105, 104, 41, 167 })]
	public static int[] scaleMat(int[] qMatLuma, int qScale)
	{
		int[] res = new int[(nint)qMatLuma.LongLength];
		for (int i = 0; i < (nint)qMatLuma.LongLength; i++)
		{
			res[i] = qMatLuma[i] * qScale;
		}
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 66, 109, 191, 8, 3, 98, 191, 34,
		103, 42, 167
	})]
	protected internal virtual void decodeOnePlane(BitReader bits, int blocksPerSlice, int[] @out, int[] qMat, int[] scan, int mbX, int mbY, int plane)
	{
		RuntimeException ex2;
		try
		{
			readDCCoeffs(bits, qMat, @out, blocksPerSlice, 64);
			readACCoeffs(bits, qMat, @out, blocksPerSlice, scan, 64, 6);
		}
		catch (System.Exception x)
		{
			RuntimeException ex = ByteCodeHelper.MapException<RuntimeException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
			goto IL_0032;
		}
		goto IL_007b;
		IL_0032:
		RuntimeException e = ex2;
		java.lang.System.err.println(new StringBuilder().append("Suppressing slice error at [").append(mbX).append(", ")
			.append(mbY)
			.append("].")
			.toString());
		goto IL_007b;
		IL_007b:
		for (int i = 0; i < blocksPerSlice; i++)
		{
			SimpleIDCT10Bit.idct10(@out, i << 6);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 45, 162, 133, 159, 11, 102, 159, 11, 191,
		13, 159, 11, 191, 13
	})]
	protected internal virtual void putSlice(byte[][] result, byte[][] lowBits, int lumaStride, int mbX, int mbY, int[] y, int[] u, int[] v, int dist, int shift, int chromaType, int sliceMbCount)
	{
		int chromaStride = lumaStride >> 1;
		putLuma(result[0], (lowBits == null) ? null : lowBits[0], shift * lumaStride, lumaStride << dist, mbX, mbY, y, sliceMbCount, dist, shift);
		if (chromaType == 2)
		{
			putChroma(result[1], (lowBits == null) ? null : lowBits[1], shift * chromaStride, chromaStride << dist, mbX, mbY, u, sliceMbCount, dist, shift);
			putChroma(result[2], (lowBits == null) ? null : lowBits[2], shift * chromaStride, chromaStride << dist, mbX, mbY, v, sliceMbCount, dist, shift);
		}
		else
		{
			putLuma(result[1], (lowBits == null) ? null : lowBits[1], shift * lumaStride, lumaStride << dist, mbX, mbY, u, sliceMbCount, dist, shift);
			putLuma(result[2], (lowBits == null) ? null : lowBits[2], shift * lumaStride, lumaStride << dist, mbX, mbY, v, sliceMbCount, dist, shift);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 40, 130, 113, 107, 117, 122, 127, 1, 127,
		3, 231, 59, 234, 71
	})]
	private void putLuma(byte[] y, byte[] lowBits, int off, int stride, int mbX, int mbY, int[] luma, int mbPerSlice, int dist, int shift)
	{
		off += (mbX << 4) + (mbY << 4) * stride;
		for (int i = 0; i < mbPerSlice; i++)
		{
			putBlock(y, lowBits, off, stride, luma, i << 8, dist, shift);
			putBlock(y, lowBits, off + 8, stride, luma, (i << 8) + 64, dist, shift);
			putBlock(y, lowBits, off + 8 * stride, stride, luma, (i << 8) + 128, dist, shift);
			putBlock(y, lowBits, off + 8 * stride + 8, stride, luma, (i << 8) + 192, dist, shift);
			off += 16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 37, 130, 113, 104, 117, 125, 230, 61, 231,
		69
	})]
	private void putChroma(byte[] y, byte[] lowBits, int off, int stride, int mbX, int mbY, int[] chroma, int mbPerSlice, int dist, int shift)
	{
		off += (mbX << 3) + (mbY << 4) * stride;
		for (int i = 0; i < mbPerSlice; i++)
		{
			putBlock(y, lowBits, off, stride, chroma, i << 7, dist, shift);
			putBlock(y, lowBits, off + 8 * stride, stride, chroma, (i << 7) + 64, dist, shift);
			off += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 34, 66, 108, 103, 120, 15, 39, 243, 71,
		103, 115, 105, 118, 122, 240, 61, 41, 249, 72
	})]
	private void putBlock(byte[] square, byte[] lowBits, int sqOff, int sqStride, int[] flat, int flOff, int dist, int shift)
	{
		int j = 0;
		int dstOff2 = sqOff;
		int srcOff2 = flOff;
		while (j < 8)
		{
			for (int l = 0; l < 8; l++)
			{
				int round2 = MathUtil.clip(flat[l + srcOff2] + 2 >> 2, 1, 255);
				square[l + dstOff2] = (byte)(sbyte)(round2 - 128);
			}
			j++;
			dstOff2 += sqStride;
			srcOff2 += 8;
		}
		if (lowBits == null)
		{
			return;
		}
		int i = 0;
		int dstOff = sqOff;
		int srcOff = flOff;
		while (i < 8)
		{
			for (int k = 0; k < 8; k++)
			{
				int val = MathUtil.clip(flat[k + srcOff], 4, 1019);
				int round = MathUtil.clip(flat[k + srcOff] + 2 >> 2, 1, 255);
				lowBits[k + dstOff] = (byte)(sbyte)(val - (round << 2));
			}
			i++;
			dstOff += sqStride;
			srcOff += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(168)]
	public override Picture decodeFrame(ByteBuffer data, byte[][] target)
	{
		Picture result = decodeFrameHiBD(data, target, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(203)]
	public virtual Picture[] decodeFields(ByteBuffer data, byte[][][] target)
	{
		Picture[] result = decodeFieldsHiBD(data, target, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(467)]
	public virtual bool isProgressive(ByteBuffer data)
	{
		return (((((sbyte)data.get(20) & 0xFF) >> 2) & 3) == 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 24, 66, 127, 18, 100 })]
	public static int probe(ByteBuffer data)
	{
		if ((sbyte)data.get(4) == 105 && (sbyte)data.get(5) == 99 && (sbyte)data.get(6) == 112 && (sbyte)data.get(7) == 102)
		{
			return 100;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 23, 162, 104 })]
	public override VideoCodecMeta getCodecMeta(ByteBuffer data)
	{
		ProresConsts.FrameHeader fh = readFrameHeader(data);
		VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(fh.width, fh.height), (fh.chromaType != 2) ? ColorSpace.___003C_003EYUV444 : ColorSpace.___003C_003EYUV422);
		
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159,
		130,
		98,
		byte.MaxValue,
		166,
		40,
		73
	})]
	static ProresDecoder()
	{
		table = new int[256]
		{
			8, 7, 6, 6, 5, 5, 5, 5, 4, 4,
			4, 4, 4, 4, 4, 4, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0
		};
		mask = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, -1 };
	}
}
