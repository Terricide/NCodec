using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv422pToRgbHiBD : Object, TransformHiBD
{
	private int downShift;

	private int upShift;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 105, 104, 104 })]
	public Yuv422pToRgbHiBD(int downShift, int upShift)
	{
		this.downShift = downShift;
		this.upShift = upShift;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 102, 105, 137, 121, 127, 0, 122,
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

	[LineNumberTable(57)]
	private static int crop(int val)
	{
		return (val >= 0) ? ((val <= 255) ? val : 255) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 105, 105, 137, 137, 103, 113, 113,
		159, 52, 159, 56, 103, 231, 58, 44, 236, 75
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] y = src.getPlaneData(0);
		int[] u = src.getPlaneData(1);
		int[] v = src.getPlaneData(2);
		int[] data = dst.getPlaneData(0);
		int offLuma = 0;
		int offChroma = 0;
		for (int i = 0; i < dst.getHeight(); i++)
		{
			for (int j = 0; j < dst.getWidth(); j += 2)
			{
				YUV444toRGB888(y[offLuma] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, offLuma * 3);
				YUV444toRGB888(y[offLuma + 1] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + 1) * 3);
				offLuma += 2;
				offChroma++;
			}
		}
	}
}
