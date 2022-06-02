using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.prores;

public class ProresToThumb2x2 : ProresDecoder
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105 })]
	public ProresToThumb2x2()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 98, 113, 102, 109, 109, 103, 111, 103,
		111, 103, 144, 104, 230, 54, 234, 76
	})]
	private void _putLuma(byte[] y, byte[] lowBits, int off, int stride, int mbX, int mbY, int[] luma, int mbPerSlice, int dist, int shift)
	{
		off += (mbX << 2) + (mbY << 2) * stride;
		int tstride = stride * 3;
		int i = 0;
		int sOff = 0;
		for (; i < mbPerSlice; i++)
		{
			putGroup(y, lowBits, off, luma, sOff);
			off += stride;
			putGroup(y, lowBits, off, luma, sOff + 2);
			off += stride;
			putGroup(y, lowBits, off, luma, sOff + 8);
			off += stride;
			putGroup(y, lowBits, off, luma, sOff + 10);
			off += 4 - tstride;
			sOff += 16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 162, 112, 141, 109, 118, 152, 110, 240,
		59, 245, 72, 229, 54, 234, 77, 103, 112, 144,
		111, 115, 117, 119, 153, 109, 239, 57, 249, 74,
		229, 52, 236, 79
	})]
	private void _putChroma(byte[] y, byte[] lowBits, int off_, int stride, int mbX, int mbY, int[] chroma, int mbPerSlice, int dist, int shift)
	{
		int off = off_ + (mbX << 1) + (mbY << 2) * stride;
		int j = 0;
		int sOff2 = 0;
		for (; j < mbPerSlice; j++)
		{
			int row2 = 0;
			int rowOff2 = off;
			while (row2 < 4)
			{
				int round = MathUtil.clip(chroma[sOff2] + 2 >> 2, 1, 255);
				int round3 = MathUtil.clip(chroma[sOff2 + 1] + 2 >> 2, 1, 255);
				y[rowOff2] = (byte)(sbyte)(round - 128);
				y[rowOff2 + 1] = (byte)(sbyte)(round3 - 128);
				row2++;
				rowOff2 += stride;
				sOff2 += 2;
			}
			off += 2;
		}
		if (lowBits == null)
		{
			return;
		}
		off = off_ + (mbX << 1) + (mbY << 2) * stride;
		int i = 0;
		int sOff = 0;
		for (; i < mbPerSlice; i++)
		{
			int row = 0;
			int rowOff = off;
			while (row < 4)
			{
				int val0 = MathUtil.clip(chroma[sOff], 4, 1019);
				int val1 = MathUtil.clip(chroma[sOff + 1], 4, 1019);
				int round0 = MathUtil.clip(chroma[sOff] + 2 >> 2, 1, 255);
				int round2 = MathUtil.clip(chroma[sOff + 1] + 2 >> 2, 1, 255);
				lowBits[rowOff] = (byte)(sbyte)(val0 - (round0 << 2));
				lowBits[rowOff + 1] = (byte)(sbyte)(val1 - (round2 << 2));
				row++;
				rowOff += stride;
				sOff += 2;
			}
			off += 2;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 130, 118, 120, 120, 152, 108, 110, 110,
		142, 103, 115, 117, 117, 149, 107, 109, 109, 141
	})]
	private void putGroup(byte[] y, byte[] lowBits, int off, int[] luma, int sOff)
	{
		int round0 = MathUtil.clip(luma[sOff] + 2 >> 2, 1, 255);
		int round1 = MathUtil.clip(luma[sOff + 1] + 2 >> 2, 1, 255);
		int round2 = MathUtil.clip(luma[sOff + 4] + 2 >> 2, 1, 255);
		int round3 = MathUtil.clip(luma[sOff + 5] + 2 >> 2, 1, 255);
		y[off] = (byte)(sbyte)(round0 - 128);
		y[off + 1] = (byte)(sbyte)(round1 - 128);
		y[off + 2] = (byte)(sbyte)(round2 - 128);
		y[off + 3] = (byte)(sbyte)(round3 - 128);
		if (lowBits != null)
		{
			int val0 = MathUtil.clip(luma[sOff], 4, 1019);
			int val1 = MathUtil.clip(luma[sOff + 1], 4, 1019);
			int val2 = MathUtil.clip(luma[sOff + 4], 4, 1019);
			int val3 = MathUtil.clip(luma[sOff + 5], 4, 1019);
			lowBits[off] = (byte)(sbyte)(val0 - (round0 << 2));
			lowBits[off + 1] = (byte)(sbyte)(val1 - (round1 << 2));
			lowBits[off + 2] = (byte)(sbyte)(val2 - (round2 << 2));
			lowBits[off + 3] = (byte)(sbyte)(val3 - (round3 << 2));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 108, 143, 103, 42, 167 })]
	protected internal override void decodeOnePlane(BitReader bits, int blocksPerSlice, int[] @out, int[] qMat, int[] scan, int mbX, int mbY, int plane)
	{
		ProresDecoder.readDCCoeffs(bits, qMat, @out, blocksPerSlice, 4);
		ProresDecoder.readACCoeffs(bits, qMat, @out, blocksPerSlice, scan, 4, 2);
		for (int i = 0; i < blocksPerSlice; i++)
		{
			IDCT2x2.idct(@out, i << 2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 136, 112, 144, 101, 134, 155, 177,
		105, 191, 30, 191, 38, 223, 38, 120, 122, 63,
		4
	})]
	public override Picture decodeFrameHiBD(ByteBuffer data, byte[][] target, byte[][] lowBits)
	{
		ProresConsts.FrameHeader fh = ProresDecoder.readFrameHeader(data);
		int codedWidth = ((fh.width + 15) & -16) >> 2;
		int codedHeight = ((fh.height + 15) & -16) >> 2;
		int lumaSize = codedWidth * codedHeight;
		int chromaSize = lumaSize >> 1;
		if (target == null || (nint)target[0].LongLength < lumaSize || (nint)target[1].LongLength < chromaSize || (nint)target[2].LongLength < chromaSize)
		{
			
			throw new RuntimeException("Provided output picture won't fit into provided buffer");
		}
		if (fh.frameType == 0)
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight, codedWidth >> 2, fh.qMatLuma, fh.qMatChroma, new int[4] { 0, 1, 2, 3 }, 0, fh.chromaType);
		}
		else
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 2, fh.qMatLuma, fh.qMatChroma, new int[4] { 0, 2, 1, 3 }, fh.topFieldFirst ? 1 : 2, fh.chromaType);
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 2, fh.qMatLuma, fh.qMatChroma, new int[4] { 0, 2, 1, 3 }, (!fh.topFieldFirst) ? 1 : 2, fh.chromaType);
		}
		ColorSpace color = ((fh.chromaType != 2) ? ColorSpace.___003C_003EYUV444 : ColorSpace.___003C_003EYUV422);
		Picture result = new Picture(codedWidth, codedHeight, target, lowBits, color, (lowBits != null) ? 2 : 0, new Rect(0, 0, (fh.width >> 2) & color.getWidthMask(), (fh.height >> 2) & color.getHeightMask()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 130, 133, 159, 11, 102, 159, 11, 191,
		13, 159, 11, 191, 13
	})]
	protected internal override void putSlice(byte[][] result, byte[][] lowBits, int lumaStride, int mbX, int mbY, int[] y, int[] u, int[] v, int dist, int shift, int chromaType, int sliceMbCount)
	{
		int chromaStride = lumaStride >> 1;
		_putLuma(result[0], (lowBits != null) ? lowBits[0] : null, shift * lumaStride, lumaStride << dist, mbX, mbY, y, sliceMbCount, dist, shift);
		if (chromaType == 2)
		{
			_putChroma(result[1], (lowBits != null) ? lowBits[1] : null, shift * chromaStride, chromaStride << dist, mbX, mbY, u, sliceMbCount, dist, shift);
			_putChroma(result[2], (lowBits != null) ? lowBits[2] : null, shift * chromaStride, chromaStride << dist, mbX, mbY, v, sliceMbCount, dist, shift);
		}
		else
		{
			_putLuma(result[1], (lowBits != null) ? lowBits[1] : null, shift * lumaStride, lumaStride << dist, mbX, mbY, u, sliceMbCount, dist, shift);
			_putLuma(result[2], (lowBits != null) ? lowBits[2] : null, shift * lumaStride, lumaStride << dist, mbX, mbY, v, sliceMbCount, dist, shift);
		}
	}

	[HideFromJava]
	static ProresToThumb2x2()
	{
		ProresDecoder.___003Cclinit_003E();
	}
}
