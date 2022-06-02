using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.prores;

public class ProresToThumb4x4 : ProresDecoder
{
	public static int[] progressive_scan_4x4;

	public static int[] interlaced_scan_4x4;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] srcIncLuma;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105 })]
	public ProresToThumb4x4()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 130, 112, 109, 99, 108, 105, 121, 16,
		201, 105, 124, 16, 233, 69, 108, 230, 53, 12,
		238, 81, 103, 112, 112, 100, 108, 105, 118, 122,
		240, 61, 233, 69, 105, 121, 122, 240, 61, 233,
		70, 110, 232, 51, 12, 240, 83
	})]
	private void _putLuma(byte[] y, byte[] lowBits, int fieldOffset, int stride, int mbX, int mbY, int[] luma, int mbPerSlice, int dist, int shift)
	{
		int mbTopLeftOff = fieldOffset + (mbX << 3) + (mbY << 3) * stride;
		int mb2 = 0;
		int sOff2 = 0;
		while (mb2 < mbPerSlice)
		{
			int lineOff2 = mbTopLeftOff;
			for (int line2 = 0; line2 < 8; line2++)
			{
				for (int col4 = 0; col4 < 4; col4++)
				{
					int round4 = MathUtil.clip(luma[sOff2 + col4] + 2 >> 2, 1, 255);
					y[lineOff2 + col4] = (byte)(sbyte)(round4 - 128);
				}
				for (int col3 = 4; col3 < 8; col3++)
				{
					int round3 = MathUtil.clip(luma[sOff2 + col3 + 12] + 2 >> 2, 1, 255);
					y[lineOff2 + col3] = (byte)(sbyte)(round3 - 128);
				}
				sOff2 += srcIncLuma[line2];
				lineOff2 += stride;
			}
			mb2++;
			mbTopLeftOff += 8;
		}
		if (lowBits == null)
		{
			return;
		}
		mbTopLeftOff = fieldOffset + (mbX << 3) + (mbY << 3) * stride;
		int mb = 0;
		int sOff = 0;
		while (mb < mbPerSlice)
		{
			int lineOff = mbTopLeftOff;
			for (int line = 0; line < 4; line++)
			{
				for (int col2 = 0; col2 < 4; col2++)
				{
					int val2 = MathUtil.clip(luma[sOff + col2], 4, 1019);
					int round2 = MathUtil.clip(luma[sOff + col2] + 2 >> 2, 1, 255);
					lowBits[lineOff + col2] = (byte)(sbyte)(val2 - (round2 << 2));
				}
				for (int col = 4; col < 8; col++)
				{
					int val = MathUtil.clip(luma[sOff + col + 12], 4, 1019);
					int round = MathUtil.clip(luma[sOff + col] + 2 >> 2, 1, 255);
					lowBits[lineOff + col] = (byte)(sbyte)(val - (round << 2));
				}
				sOff += srcIncLuma[line];
				lineOff += stride;
			}
			mb++;
			mbTopLeftOff += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 98, 112, 109, 99, 108, 105, 121, 16,
		233, 69, 101, 230, 57, 12, 238, 77, 103, 112,
		112, 100, 108, 105, 118, 122, 240, 61, 233, 70,
		103, 232, 56, 12, 240, 78
	})]
	private void _putChroma(byte[] y, byte[] lowBits, int fieldOff, int stride, int mbX, int mbY, int[] chroma, int mbPerSlice, int dist, int shift)
	{
		int mbTopLeftOff = fieldOff + (mbX << 2) + (mbY << 3) * stride;
		int j = 0;
		int sOff2 = 0;
		while (j < mbPerSlice)
		{
			int lineOff2 = mbTopLeftOff;
			for (int line2 = 0; line2 < 8; line2++)
			{
				for (int col2 = 0; col2 < 4; col2++)
				{
					int round2 = MathUtil.clip(chroma[sOff2 + col2] + 2 >> 2, 1, 255);
					y[lineOff2 + col2] = (byte)(sbyte)(round2 - 128);
				}
				sOff2 += 4;
				lineOff2 += stride;
			}
			j++;
			mbTopLeftOff += 4;
		}
		if (lowBits == null)
		{
			return;
		}
		mbTopLeftOff = fieldOff + (mbX << 2) + (mbY << 3) * stride;
		int i = 0;
		int sOff = 0;
		while (i < mbPerSlice)
		{
			int lineOff = mbTopLeftOff;
			for (int line = 0; line < 8; line++)
			{
				for (int col = 0; col < 4; col++)
				{
					int val = MathUtil.clip(chroma[sOff + col], 4, 1019);
					int round = MathUtil.clip(chroma[sOff + col] + 2 >> 2, 1, 255);
					lowBits[lineOff + col] = (byte)(sbyte)(val - (round << 2));
				}
				sOff += 4;
				lineOff += stride;
			}
			i++;
			mbTopLeftOff += 4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 109, 144, 103, 42, 167 })]
	protected internal override void decodeOnePlane(BitReader bits, int blocksPerSlice, int[] @out, int[] qMat, int[] scan, int mbX, int mbY, int plane)
	{
		ProresDecoder.readDCCoeffs(bits, qMat, @out, blocksPerSlice, 16);
		ProresDecoder.readACCoeffs(bits, qMat, @out, blocksPerSlice, scan, 16, 4);
		for (int i = 0; i < blocksPerSlice; i++)
		{
			IDCT4x4.idct(@out, i << 4);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 136, 112, 144, 101, 134, 155, 177,
		105, 191, 13, 191, 21, 223, 21, 120, 122, 63,
		4
	})]
	public override Picture decodeFrameHiBD(ByteBuffer data, byte[][] target, byte[][] lowBits)
	{
		ProresConsts.FrameHeader fh = ProresDecoder.readFrameHeader(data);
		int codedWidth = ((fh.width + 15) & -16) >> 1;
		int codedHeight = ((fh.height + 15) & -16) >> 1;
		int lumaSize = codedWidth * codedHeight;
		int chromaSize = lumaSize >> 1;
		if (target == null || (nint)target[0].LongLength < lumaSize || (nint)target[1].LongLength < chromaSize || (nint)target[2].LongLength < chromaSize)
		{
			
			throw new RuntimeException("Provided output picture won't fit into provided buffer");
		}
		if (fh.frameType == 0)
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight, codedWidth >> 3, fh.qMatLuma, fh.qMatChroma, progressive_scan_4x4, 0, fh.chromaType);
		}
		else
		{
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 3, fh.qMatLuma, fh.qMatChroma, interlaced_scan_4x4, fh.topFieldFirst ? 1 : 2, fh.chromaType);
			decodePicture(data, target, lowBits, codedWidth, codedHeight >> 1, codedWidth >> 3, fh.qMatLuma, fh.qMatChroma, interlaced_scan_4x4, (!fh.topFieldFirst) ? 1 : 2, fh.chromaType);
		}
		ColorSpace color = ((fh.chromaType != 2) ? ColorSpace.___003C_003EYUV444 : ColorSpace.___003C_003EYUV422);
		Picture result = new Picture(codedWidth, codedHeight, target, lowBits, color, (lowBits != null) ? 2 : 0, new Rect(0, 0, (fh.width >> 1) & color.getWidthMask(), (fh.height >> 1) & color.getHeightMask()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 98, 133, 159, 11, 102, 159, 11, 191,
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

	[LineNumberTable(new byte[]
	{
		159,
		133,
		167,
		159,
		60,
		byte.MaxValue,
		60,
		117
	})]
	static ProresToThumb4x4()
	{
		ProresDecoder.___003Cclinit_003E();
		progressive_scan_4x4 = new int[16]
		{
			0, 1, 4, 5, 2, 3, 6, 7, 8, 9,
			12, 13, 11, 12, 14, 15
		};
		interlaced_scan_4x4 = new int[16]
		{
			0, 4, 1, 5, 8, 12, 9, 13, 2, 6,
			3, 7, 10, 14, 11, 15
		};
		srcIncLuma = new int[8] { 4, 4, 4, 20, 4, 4, 4, 20 };
	}
}
