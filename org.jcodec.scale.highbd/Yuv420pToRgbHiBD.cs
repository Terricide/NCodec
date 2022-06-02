using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv420pToRgbHiBD : Object, TransformHiBD
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int downShift;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int upShift;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104, 104 })]
	public Yuv420pToRgbHiBD(int upShift, int downShift)
	{
		this.upShift = upShift;
		this.downShift = downShift;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 98, 105, 105, 105, 137, 103, 105, 115,
		115, 103, 159, 58, 191, 62, 191, 64, 223, 68,
		231, 50, 236, 80, 110, 139, 159, 58, 223, 64,
		167, 234, 35, 236, 95, 110, 115, 103, 159, 58,
		191, 62, 231, 57, 236, 73, 110, 139, 191, 58,
		167
	})]
	public void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] y = src.getPlaneData(0);
		int[] u = src.getPlaneData(1);
		int[] v = src.getPlaneData(2);
		int[] data = dst.getPlaneData(0);
		int offLuma = 0;
		int offChroma = 0;
		int stride = dst.getWidth();
		for (int i = 0; i < dst.getHeight() >> 1; i++)
		{
			for (int k2 = 0; k2 < dst.getWidth() >> 1; k2++)
			{
				int m = k2 << 1;
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + m] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + m) * 3);
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + m + 1] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + m + 1) * 3);
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + m + stride] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + m + stride) * 3);
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + m + stride + 1] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + m + stride + 1) * 3);
				offChroma++;
			}
			if (((uint)dst.getWidth() & (true ? 1u : 0u)) != 0)
			{
				int l = dst.getWidth() - 1;
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + l] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + l) * 3);
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + l + stride] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + l + stride) * 3);
				offChroma++;
			}
			offLuma += 2 * stride;
		}
		if (((uint)dst.getHeight() & (true ? 1u : 0u)) != 0)
		{
			for (int n = 0; n < dst.getWidth() >> 1; n++)
			{
				int k = n << 1;
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + k] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + k) * 3);
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + k + 1] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + k + 1) * 3);
				offChroma++;
			}
			if (((uint)dst.getWidth() & (true ? 1u : 0u)) != 0)
			{
				int j = dst.getWidth() - 1;
				Yuv422pToRgbHiBD.YUV444toRGB888(y[offLuma + j] << upShift >> downShift, u[offChroma] << upShift >> downShift, v[offChroma] << upShift >> downShift, data, (offLuma + j) * 3);
				offChroma++;
			}
		}
	}
}
