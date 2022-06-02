using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv420pToYuv422pHiBD : Object, TransformHiBD
{
	private int shiftUp;

	private int shiftDown;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 105, 104, 104 })]
	public Yuv420pToYuv422pHiBD(int shiftUp, int shiftDown)
	{
		this.shiftUp = shiftUp;
		this.shiftDown = shiftDown;
	}

	[LineNumberTable(new byte[]
	{
		159, 127, 162, 110, 101, 106, 105, 55, 169, 105,
		45, 233, 60, 234, 71, 104, 106, 105, 47, 41,
		233, 69
	})]
	private static void copy(int[] src, int[] dest, int srcWidth, int dstWidth, int dstHeight, int shiftUp, int shiftDown)
	{
		nint num = (nint)src.LongLength;
		int height = (int)((srcWidth != -1) ? (num / srcWidth) : (-num));
		int dstOff = 0;
		int srcOff = 0;
		for (int j = 0; j < height; j++)
		{
			for (int m = 0; m < srcWidth; m++)
			{
				int num2 = dstOff;
				dstOff++;
				int num3 = srcOff;
				srcOff++;
				dest[num2] = (src[num3] & 0xFF) << 2;
			}
			for (int l = srcWidth; l < dstWidth; l++)
			{
				int num4 = dstOff;
				dstOff++;
				dest[num4] = dest[srcWidth - 1];
			}
		}
		int lastLine = (height - 1) * dstWidth;
		for (int i = height; i < dstHeight; i++)
		{
			for (int k = 0; k < dstWidth; k++)
			{
				int num5 = dstOff;
				dstOff++;
				dest[num5] = dest[lastLine + k];
			}
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 106, 107, 104, 115, 6, 199, 103,
		110, 104, 6, 202, 235, 54, 234, 76, 106, 110,
		106, 107, 6, 202, 235, 59, 234, 71
	})]
	private static void _copy(int[] src, int[] dest, int offX, int offY, int stepX, int stepY, int strideSrc, int strideDest, int heightSrc, int heightDst, int upShift, int downShift)
	{
		int offD = offX + offY * strideDest;
		int srcOff = 0;
		for (int j = 0; j < heightSrc; j++)
		{
			for (int m = 0; m < strideSrc; m++)
			{
				int num = offD;
				int num2 = srcOff;
				srcOff++;
				dest[num] = (src[num2] & 0xFF) << 2;
				offD += stepX;
			}
			int lastOff = offD - stepX;
			for (int l = strideSrc * stepX; l < strideDest; l += stepX)
			{
				dest[offD] = dest[lastOff];
				offD += stepX;
			}
			offD += (stepY - 1) * strideDest;
		}
		int lastLine = offD - stepY * strideDest;
		for (int i = heightSrc * stepY; i < heightDst; i += stepY)
		{
			for (int k = 0; k < strideDest; k += stepX)
			{
				dest[offD] = dest[lastLine + k];
				offD += stepX;
			}
			offD += (stepY - 1) * strideDest;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 191, 19, 127, 5, 58, 134, 127,
		5, 58, 134, 127, 5, 58, 134, 127, 5, 58,
		136
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		copy(src.getPlaneData(0), dst.getPlaneData(0), src.getWidth(), dst.getWidth(), dst.getHeight(), shiftUp, shiftDown);
		_copy(src.getPlaneData(1), dst.getPlaneData(1), 0, 0, 1, 2, src.getWidth() >> 1, dst.getWidth() >> 1, src.getHeight() >> 1, dst.getHeight(), shiftUp, shiftDown);
		_copy(src.getPlaneData(1), dst.getPlaneData(1), 0, 1, 1, 2, src.getWidth() >> 1, dst.getWidth() >> 1, src.getHeight() >> 1, dst.getHeight(), shiftUp, shiftDown);
		_copy(src.getPlaneData(2), dst.getPlaneData(2), 0, 0, 1, 2, src.getWidth() >> 1, dst.getWidth() >> 1, src.getHeight() >> 1, dst.getHeight(), shiftUp, shiftDown);
		_copy(src.getPlaneData(2), dst.getPlaneData(2), 0, 1, 1, 2, src.getWidth() >> 1, dst.getWidth() >> 1, src.getHeight() >> 1, dst.getHeight(), shiftUp, shiftDown);
	}
}
