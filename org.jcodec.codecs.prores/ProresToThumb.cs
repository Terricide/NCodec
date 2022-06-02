using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.prores;

public class ProresToThumb : ProresDecoder
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 105 })]
	public ProresToThumb()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 112, 109, 117, 140, 120, 143, 134,
		120, 141, 120, 143, 104, 229, 48, 234, 83, 103,
		112, 112, 115, 119, 140, 117, 121, 142, 134, 117,
		121, 140, 117, 121, 142, 104, 231, 44, 236, 87
	})]
	private void _putLuma(byte[] y, byte[] lowBits, int _off, int stride, int mbX, int mbY, int[] luma, int mbPerSlice, int dist, int shift)
	{
		int off = _off + (mbX << 1) + (mbY << 1) * stride;
		int j = 0;
		int sOff2 = 0;
		for (; j < mbPerSlice; j++)
		{
			int round = MathUtil.clip(luma[sOff2] + 2 >> 2, 1, 255);
			y[off] = (byte)(sbyte)(round - 128);
			int round3 = MathUtil.clip(luma[sOff2 + 1] + 2 >> 2, 1, 255);
			y[off + 1] = (byte)(sbyte)(round3 - 128);
			off += stride;
			int round5 = MathUtil.clip(luma[sOff2 + 2] + 2 >> 2, 1, 255);
			y[off] = (byte)(sbyte)(round5 - 128);
			int round7 = MathUtil.clip(luma[sOff2 + 3] + 2 >> 2, 1, 255);
			y[off + 1] = (byte)(sbyte)(round7 - 128);
			off += 2 - stride;
			sOff2 += 4;
		}
		if (lowBits != null)
		{
			off = _off + (mbX << 1) + (mbY << 1) * stride;
			int i = 0;
			int sOff = 0;
			for (; i < mbPerSlice; i++)
			{
				int val0 = MathUtil.clip(luma[sOff], 4, 1019);
				int round0 = MathUtil.clip(luma[sOff] + 2 >> 2, 1, 255);
				lowBits[off] = (byte)(sbyte)(val0 - (round0 << 2));
				int val1 = MathUtil.clip(luma[sOff + 1], 4, 1019);
				int round2 = MathUtil.clip(luma[sOff + 1] + 2 >> 2, 1, 255);
				lowBits[off + 1] = (byte)(sbyte)(val1 - (round2 << 2));
				off += stride;
				int val2 = MathUtil.clip(luma[sOff + 2], 4, 1019);
				int round4 = MathUtil.clip(luma[sOff + 2] + 2 >> 2, 1, 255);
				lowBits[off] = (byte)(sbyte)(val2 - (round4 << 2));
				int val3 = MathUtil.clip(luma[sOff + 3], 4, 1019);
				int round6 = MathUtil.clip(luma[sOff + 3] + 2 >> 2, 1, 255);
				lowBits[off + 1] = (byte)(sbyte)(val3 - (round6 << 2));
				off += 2 - stride;
				sOff += 4;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 98, 110, 109, 117, 140, 134, 120, 141,
		104, 229, 54, 234, 77, 103, 110, 144, 115, 119,
		140, 134, 117, 121, 140, 104, 231, 51, 236, 80
	})]
	private void _putChroma(byte[] y, byte[] lowBits, int _off, int stride, int mbX, int mbY, int[] chroma, int mbPerSlice, int dist, int shift)
	{
		int off = _off + mbX + (mbY << 1) * stride;
		int j = 0;
		int sOff2 = 0;
		for (; j < mbPerSlice; j++)
		{
			int round = MathUtil.clip(chroma[sOff2] + 2 >> 2, 1, 255);
			y[off] = (byte)(sbyte)(round - 128);
			off += stride;
			int round3 = MathUtil.clip(chroma[sOff2 + 1] + 2 >> 2, 1, 255);
			y[off] = (byte)(sbyte)(round3 - 128);
			off += 1 - stride;
			sOff2 += 2;
		}
		if (lowBits != null)
		{
			off = _off + mbX + (mbY << 1) * stride;
			int i = 0;
			int sOff = 0;
			for (; i < mbPerSlice; i++)
			{
				int val0 = MathUtil.clip(chroma[sOff], 4, 1019);
				int round0 = MathUtil.clip(chroma[sOff] + 2 >> 2, 1, 255);
				lowBits[off] = (byte)(sbyte)(val0 - (round0 << 2));
				off += stride;
				int val1 = MathUtil.clip(chroma[sOff + 1], 4, 1019);
				int round2 = MathUtil.clip(chroma[sOff + 1] + 2 >> 2, 1, 255);
				lowBits[off] = (byte)(sbyte)(val1 - (round2 << 2));
				off += 1 - stride;
				sOff += 2;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 191, 4, 3, 98, 191, 34, 103,
		48, 167
	})]
	protected internal override void decodeOnePlane(BitReader bits, int blocksPerSlice, int[] @out, int[] qMat, int[] scan, int mbX, int mbY, int plane)
	{
		RuntimeException ex2;
		try
		{
			ProresDecoder.readDCCoeffs(bits, qMat, @out, blocksPerSlice, 1);
		}
		catch (System.Exception x)
		{
			RuntimeException ex = ByteCodeHelper.MapException<RuntimeException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
			goto IL_0021;
		}
		goto IL_006a;
		IL_0021:
		RuntimeException e = ex2;
		java.lang.System.err.println(new StringBuilder().append("Suppressing slice error at [").append(mbX).append(", ")
			.append(mbY)
			.append("].")
			.toString());
		goto IL_006a;
		IL_006a:
		for (int i = 0; i < blocksPerSlice; i++)
		{
			int num = i;
			@out[num] >>= 3;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 136, 112, 144, 101, 134, 155, 177,
		105, 191, 18, 191, 26, 223, 26, 120, 122, 63,
		4
	})]
	public override Picture decodeFrameHiBD(ByteBuffer data, byte[][] target, byte[][] lowBits)
	{
		ProresConsts.FrameHeader fh = ProresDecoder.readFrameHeader(data);
		int codedWidth = ((fh.width + 15) & -16) >> 3;
		int codedHeight = ((fh.height + 15) & -16) >> 3;
		int lumaSize = codedWidth * codedHeight;
		int chromaSize = lumaSize >> 1;
		if (target == null || (nint)target[0].LongLength < lumaSize || (nint)target[1].LongLength < chromaSize || (nint)target[2].LongLength < chromaSize)
		{
			
			throw new RuntimeException("Provided output picture won't fit into provided buffer");
		}
		if (fh.frameType == 0)
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight, codedWidth >> 1, fh.qMatLuma, fh.qMatChroma, new int[1] { 0 }, 0, fh.chromaType);
		}
		else
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 1, fh.qMatLuma, fh.qMatChroma, new int[1] { 0 }, fh.topFieldFirst ? 1 : 2, fh.chromaType);
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 1, fh.qMatLuma, fh.qMatChroma, new int[1] { 0 }, (!fh.topFieldFirst) ? 1 : 2, fh.chromaType);
		}
		ColorSpace color = ((fh.chromaType != 2) ? ColorSpace.___003C_003EYUV444 : ColorSpace.___003C_003EYUV422);
		Picture result = new Picture(codedWidth, codedHeight, target, lowBits, color, (lowBits != null) ? 2 : 0, new Rect(0, 0, (fh.width >> 3) & color.getWidthMask(), (fh.height >> 3) & color.getHeightMask()));
		
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
		_putLuma(result[0], (lowBits == null) ? null : lowBits[0], shift * lumaStride, lumaStride << dist, mbX, mbY, y, sliceMbCount, dist, shift);
		if (chromaType == 2)
		{
			_putChroma(result[1], (lowBits == null) ? null : lowBits[1], shift * chromaStride, chromaStride << dist, mbX, mbY, u, sliceMbCount, dist, shift);
			_putChroma(result[2], (lowBits == null) ? null : lowBits[2], shift * chromaStride, chromaStride << dist, mbX, mbY, v, sliceMbCount, dist, shift);
		}
		else
		{
			_putLuma(result[1], (lowBits == null) ? null : lowBits[1], shift * lumaStride, lumaStride << dist, mbX, mbY, u, sliceMbCount, dist, shift);
			_putLuma(result[2], (lowBits == null) ? null : lowBits[2], shift * lumaStride, lumaStride << dist, mbX, mbY, v, sliceMbCount, dist, shift);
		}
	}

	[HideFromJava]
	static ProresToThumb()
	{
		ProresDecoder.___003Cclinit_003E();
	}
}
