using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mjpeg;

public class JpegToThumb4x4 : JpegDecoder
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] mapping4x4;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(21)]
	public JpegToThumb4x4()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 131, 105, 105, 104, 101, 101, 101,
		102, 124, 133, 140, 135, 105, 105, 104, 101, 102,
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
				target[mapping4x4[curOff]] = JpegDecoder.toValue(_in.readNBit(len2), len2) * quantTable[curOff];
				curOff++;
			}
		}
		while (code != 0 && curOff < 19);
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
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 102, 102, 113, 107, 121, 125, 125,
		125, 229, 59, 234, 71
	})]
	private static void putBlock4x4(byte[] plane, int stride, int[] patch, int x, int y, int field, int step)
	{
		stride >>= 1;
		int dstride = step * stride;
		int off = field * stride + (y >> 1) * dstride + (x >> 1);
		for (int i = 0; i < 16; i += 4)
		{
			plane[off] = (byte)(sbyte)(MathUtil.clip(patch[i], 0, 255) - 128);
			plane[off + 1] = (byte)(sbyte)(MathUtil.clip(patch[i + 1], 0, 255) - 128);
			plane[off + 2] = (byte)(sbyte)(MathUtil.clip(patch[i + 2], 0, 255) - 128);
			plane[off + 3] = (byte)(sbyte)(MathUtil.clip(patch[i + 3], 0, 255) - 128);
			off += dstride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 159, 160, 72, 127, 8, 117, 137,
		127, 5
	})]
	internal override void decodeBlock(BitReader bits, int[] dcPredictor, int[][] quant, VLC[] huff, Picture result, int[] buf, int blkX, int blkY, int plane, int chroma, int field, int step)
	{
		int num = 0;
		int num2 = 15;
		int[] array = buf;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 14;
		array = buf;
		int num4 = num;
		array[num2] = num;
		num = num4;
		num2 = 13;
		array = buf;
		int num5 = num;
		array[num2] = num;
		num = num5;
		num2 = 12;
		array = buf;
		int num6 = num;
		array[num2] = num;
		num = num6;
		num2 = 11;
		array = buf;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 10;
		array = buf;
		int num8 = num;
		array[num2] = num;
		num = num8;
		num2 = 9;
		array = buf;
		int num9 = num;
		array[num2] = num;
		num = num9;
		num2 = 8;
		array = buf;
		int num10 = num;
		array[num2] = num;
		num = num10;
		num2 = 7;
		array = buf;
		int num11 = num;
		array[num2] = num;
		num = num11;
		num2 = 6;
		array = buf;
		int num12 = num;
		array[num2] = num;
		num = num12;
		num2 = 5;
		array = buf;
		int num13 = num;
		array[num2] = num;
		num = num13;
		num2 = 4;
		array = buf;
		int num14 = num;
		array[num2] = num;
		num = num14;
		num2 = 3;
		array = buf;
		int num15 = num;
		array[num2] = num;
		num = num15;
		num2 = 2;
		array = buf;
		int num16 = num;
		array[num2] = num;
		buf[1] = num16;
		num = JpegDecoder.readDCValue(bits, huff[chroma]) * quant[chroma][0] + dcPredictor[plane];
		num2 = 0;
		array = buf;
		int num17 = num;
		array[num2] = num;
		dcPredictor[plane] = num17;
		readACValues(bits, buf, huff[chroma + 2], quant[chroma]);
		IDCT4x4.idct(buf, 0);
		putBlock4x4(result.getPlaneData(plane), result.getPlaneWidth(plane), buf, blkX, blkY, field, step);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 162, 141, 127, 8, 60 })]
	public override Picture decodeField(ByteBuffer data, byte[][] data2, int field, int step)
	{
		Picture res = base.decodeField(data, data2, field, step);
		Picture result = new Picture(res.getWidth() >> 1, res.getHeight() >> 1, res.getData(), null, res.getColor(), 0, new Rect(0, 0, res.getCroppedWidth() >> 1, res.getCroppedHeight() >> 1));
		
		return result;
	}

	[LineNumberTable(23)]
	static JpegToThumb4x4()
	{
		mapping4x4 = new int[64]
		{
			0, 1, 4, 8, 5, 2, 3, 6, 9, 12,
			16, 13, 10, 7, 16, 16, 16, 11, 14, 16,
			16, 16, 16, 16, 15, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 16, 16
		};
	}
}
