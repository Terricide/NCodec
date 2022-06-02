using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv444pToRgb : Object, TransformHiBD
{
	private int downShift;

	private int upShift;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 104, 104 })]
	public Yuv444pToRgb(int downShift, int upShift)
	{
		this.downShift = downShift;
		this.upShift = upShift;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 102, 105, 137, 121, 127, 0, 122,
		107, 110, 110
	})]
	public static void YUV444toRGB888(int y, int u, int v, int[] data, int off)
	{
		int c = y - 16;
		int d = u - 128;
		int e = v - 128;
		int r = 298 * c + 409 * e + 128 >> 8;
		int g = 298 * c - 100 * d - 208 * e + 128 >> 8;
		int b = 298 * c + 516 * d + 128 >> 8;
		data[off] = crop(r);
		data[off + 1] = crop(g);
		data[off + 2] = crop(b);
	}

	[LineNumberTable(50)]
	private static int crop(int val)
	{
		return (val >= 0) ? ((val <= 255) ? val : 255) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 105, 105, 137, 137, 119, 113, 63,
		50, 56, 236, 70
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] y = src.getPlaneData(0);
		int[] u = src.getPlaneData(1);
		int[] v = src.getPlaneData(2);
		int[] data = dst.getPlaneData(0);
		int i = 0;
		int srcOff = 0;
		int dstOff = 0;
		for (; i < dst.getHeight(); i++)
		{
			int j = 0;
			while (j < dst.getWidth())
			{
				YUV444toRGB888(y[srcOff] << upShift >> downShift, u[srcOff] << upShift >> downShift, v[srcOff] << upShift >> downShift, data, dstOff);
				j++;
				srcOff++;
				dstOff += 3;
			}
		}
	}
}
