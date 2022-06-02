using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv422jToRgbHiBD : Object, TransformHiBD
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105 })]
	public Yuv422jToRgbHiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 105, 105, 137, 137, 103, 113, 113,
		119, 123, 103, 231, 60, 44, 236, 73
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
				Yuv420jToRgbHiBD.YUVJtoRGB(y[offLuma], u[offChroma], v[offChroma], data, offLuma * 3);
				Yuv420jToRgbHiBD.YUVJtoRGB(y[offLuma + 1], u[offChroma], v[offChroma], data, (offLuma + 1) * 3);
				offLuma += 2;
				offChroma++;
			}
		}
	}
}
