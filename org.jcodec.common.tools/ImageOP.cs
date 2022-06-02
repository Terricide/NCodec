using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.common.tools;

public class ImageOP : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 104, 104, 104, 136, 109, 127, 5,
		122, 127, 8, 243, 61, 38, 236, 70
	})]
	public static void subImageWithFillPic8(Picture _in, Picture @out, Rect rect)
	{
		int width = _in.getWidth();
		int height = _in.getHeight();
		ColorSpace color = _in.getColor();
		byte[][] data = _in.getData();
		for (int i = 0; i < (nint)data.LongLength; i++)
		{
			subImageWithFill(data[i], width >> color.compWidth[i], height >> color.compHeight[i], @out.getPlaneData(i), rect.getWidth() >> color.compWidth[i], rect.getHeight() >> color.compHeight[i], rect.getX() >> color.compWidth[i], rect.getY() >> color.compHeight[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 98, 109, 109, 131, 140, 140, 105, 46,
		169, 105, 103, 41, 137, 103, 230, 55, 236, 75,
		103, 103, 109, 6, 201
	})]
	public static void subImageWithFill(byte[] src, int width, int height, byte[] dst, int dstW, int dstH, int offX, int offY)
	{
		int srcHeight = Math.min(height - offY, dstH);
		int srcWidth = Math.min(width - offX, dstW);
		int dstOff = 0;
		int srcOff = offY * width + offX;
		int i;
		for (i = 0; i < srcHeight; i++)
		{
			int j;
			for (j = 0; j < srcWidth; j++)
			{
				dst[dstOff + j] = src[srcOff + j];
			}
			int lastPix = dst[j - 1];
			for (; j < dstW; j++)
			{
				dst[dstOff + j] = (byte)lastPix;
			}
			srcOff += width;
			dstOff += dstW;
		}
		int lastLine = dstOff - dstW;
		for (; i < dstH; i++)
		{
			ByteCodeHelper.arraycopy_primitive_1(dst, lastLine, dst, dstOff, dstW);
			dstOff += dstW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public ImageOP()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 109, 109, 131, 140, 140, 105, 46,
		169, 105, 103, 41, 137, 103, 230, 55, 236, 75,
		103, 103, 109, 6, 201
	})]
	public static void subImageWithFillInt(int[] src, int width, int height, int[] dst, int dstW, int dstH, int offX, int offY)
	{
		int srcHeight = Math.min(height - offY, dstH);
		int srcWidth = Math.min(width - offX, dstW);
		int dstOff = 0;
		int srcOff = offY * width + offX;
		int i;
		for (i = 0; i < srcHeight; i++)
		{
			int j;
			for (j = 0; j < srcWidth; j++)
			{
				dst[dstOff + j] = src[srcOff + j];
			}
			int lastPix = dst[j - 1];
			for (; j < dstW; j++)
			{
				dst[dstOff + j] = lastPix;
			}
			srcOff += width;
			dstOff += dstW;
		}
		int lastLine = dstOff - dstW;
		for (; i < dstH; i++)
		{
			ByteCodeHelper.arraycopy_primitive_4(dst, lastLine, dst, dstOff, dstW);
			dstOff += dstW;
		}
	}
}
