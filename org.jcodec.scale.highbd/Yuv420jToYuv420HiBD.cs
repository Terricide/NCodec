using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv420jToYuv420HiBD : Object, TransformHiBD
{
	public static int Y_COEFF = 7168;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public Yuv420jToYuv420HiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 162, 105, 105, 117, 51, 167, 105, 106,
		119, 63, 0, 169, 106, 106, 119, 63, 1, 137
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] sy = src.getPlaneData(0);
		int[] dy = dst.getPlaneData(0);
		for (int k = 0; k < src.getPlaneWidth(0) * src.getPlaneHeight(0); k++)
		{
			dy[k] = (sy[k] * Y_COEFF >> 13) + 16;
		}
		int[] su = src.getPlaneData(1);
		int[] du = dst.getPlaneData(1);
		for (int j = 0; j < src.getPlaneWidth(1) * src.getPlaneHeight(1); j++)
		{
			du[j] = ((su[j] - 128) * Y_COEFF >> 13) + 128;
		}
		int[] sv = src.getPlaneData(2);
		int[] dv = dst.getPlaneData(2);
		for (int i = 0; i < src.getPlaneWidth(2) * src.getPlaneHeight(2); i++)
		{
			dv[i] = ((sv[i] - 128) * Y_COEFF >> 13) + 128;
		}
	}
}
