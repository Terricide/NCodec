using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.scale;

public class Yuv422pToRgb : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public Yuv422pToRgb()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 97, 74, 102, 100, 132, 123, 127, 2,
		123, 121, 123, 123
	})]
	public static void YUV444toRGB888(byte y, byte u, byte v, byte[] data, int off)
	{
		int y2 = (sbyte)y;
		int u2 = (sbyte)u;
		int v2 = (sbyte)v;
		int c = y2 + 112;
		int d = u2;
		int e = v2;
		int r = 298 * c + 409 * e + 128 >> 8;
		int g = 298 * c - 100 * d - 208 * e + 128 >> 8;
		int b = 298 * c + 516 * d + 128 >> 8;
		data[off] = (byte)(sbyte)(MathUtil.clip(r, 0, 255) - 128);
		data[off + 1] = (byte)(sbyte)(MathUtil.clip(g, 0, 255) - 128);
		data[off + 2] = (byte)(sbyte)(MathUtil.clip(b, 0, 255) - 128);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 130, 105, 105, 137, 137, 103, 113, 113,
		119, 123, 103, 231, 60, 44, 236, 73
	})]
	public virtual void transform(Picture src, Picture dst)
	{
		byte[] y = src.getPlaneData(0);
		byte[] u = src.getPlaneData(1);
		byte[] v = src.getPlaneData(2);
		byte[] data = dst.getPlaneData(0);
		int offLuma = 0;
		int offChroma = 0;
		for (int i = 0; i < dst.getHeight(); i++)
		{
			for (int j = 0; j < dst.getWidth(); j += 2)
			{
				YUV444toRGB888(y[offLuma], u[offChroma], v[offChroma], data, offLuma * 3);
				YUV444toRGB888(y[offLuma + 1], u[offChroma], v[offChroma], data, (offLuma + 1) * 3);
				offLuma += 2;
				offChroma++;
			}
		}
	}
}
