using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mjpeg;

public class JpegToThumb2x2 : JpegDecoder
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] mapping2x2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(21)]
	public JpegToThumb2x2()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 131, 105, 105, 104, 101, 101, 101,
		102, 124, 133, 139, 135, 105, 105, 104, 101, 102,
		102, 103, 106, 133, 140
	})]
	internal override void readACValues(BitReader _in, int[] target, VLC table, int[] quantTable)
	{
		int curOff = 1;
		int code;
		do
		{
			code = table.readVLC16(_in);
			if (code == 240)
			{
				curOff += 16;
			}
			else if (code > 0)
			{
				int rle2 = code >> 4;
				curOff += rle2;
				int len2 = code & 0xF;
				target[mapping2x2[curOff]] = JpegDecoder.toValue(_in.readNBit(len2), len2) * quantTable[curOff];
				curOff++;
			}
		}
		while (code != 0 && curOff < 5);
		if (code == 0)
		{
			return;
		}
		do
		{
			code = table.readVLC16(_in);
			if (code == 240)
			{
				curOff += 16;
			}
			else if (code > 0)
			{
				int rle = code >> 4;
				curOff += rle;
				int len = code & 0xF;
				_in.skip(len);
				curOff++;
			}
		}
		while (code != 0 && curOff < 64);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 102, 102, 113, 121, 123, 123, 125 })]
	private static void putBlock2x2(byte[] plane, int stride, int[] patch, int x, int y, int field, int step)
	{
		stride >>= 2;
		int dstride = stride * step;
		int off = field * stride + (y >> 2) * dstride + (x >> 2);
		plane[off] = (byte)(sbyte)(MathUtil.clip(patch[0], 0, 255) - 128);
		plane[off + 1] = (byte)(sbyte)(MathUtil.clip(patch[1], 0, 255) - 128);
		plane[off + dstride] = (byte)(sbyte)(MathUtil.clip(patch[2], 0, 255) - 128);
		plane[off + dstride + 1] = (byte)(sbyte)(MathUtil.clip(patch[3], 0, 255) - 128);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 124, 127, 8, 117, 137, 127, 5 })]
	internal override void decodeBlock(BitReader bits, int[] dcPredictor, int[][] quant, VLC[] huff, Picture result, int[] buf, int blkX, int blkY, int plane, int chroma, int field, int step)
	{
		int num = 0;
		int num2 = 3;
		int[] array = buf;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 2;
		array = buf;
		int num4 = num;
		array[num2] = num;
		buf[1] = num4;
		num = JpegDecoder.readDCValue(bits, huff[chroma]) * quant[chroma][0] + dcPredictor[plane];
		num2 = 0;
		array = buf;
		int num5 = num;
		array[num2] = num;
		dcPredictor[plane] = num5;
		readACValues(bits, buf, huff[chroma + 2], quant[chroma]);
		IDCT2x2.idct(buf, 0);
		putBlock2x2(result.getPlaneData(plane), result.getPlaneWidth(plane), buf, blkX, blkY, field, step);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 130, 141, 127, 8, 60 })]
	public override Picture decodeField(ByteBuffer data, byte[][] data2, int field, int step)
	{
		Picture res = base.decodeField(data, data2, field, step);
		Picture result = new Picture(res.getWidth() >> 2, res.getHeight() >> 2, res.getData(), null, res.getColor(), 0, new Rect(0, 0, res.getCroppedWidth() >> 2, res.getCroppedHeight() >> 2));
		
		return result;
	}

	[LineNumberTable(23)]
	static JpegToThumb2x2()
	{
		mapping2x2 = new int[28]
		{
			0, 1, 2, 4, 3, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4
		};
	}
}
