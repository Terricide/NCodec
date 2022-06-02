using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv444jToRgbHiBD : Object, TransformHiBD
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105 })]
	public Yuv444jToRgbHiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 105, 105, 137, 137, 116, 110, 53,
		53, 233, 69
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
				Yuv420jToRgbHiBD.YUVJtoRGB(y[srcOff], u[srcOff], v[srcOff], data, dstOff);
				j++;
				srcOff++;
				dstOff += 3;
			}
		}
	}
}
