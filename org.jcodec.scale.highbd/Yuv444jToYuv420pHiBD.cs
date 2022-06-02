using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv444jToYuv420pHiBD : Object, TransformHiBD
{
	public static int Y_COEFF = 7168;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 105 })]
	public Yuv444jToYuv420pHiBD()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 135, 98, 101, 109, 138, 123, 125, 125, 159,
		0, 243, 57, 242, 73, 229, 54, 234, 76
	})]
	private void copyAvg(int[] src, int[] dst, int width, int height)
	{
		int offSrc = 0;
		int offDst = 0;
		for (int y = 0; y < height >> 1; y++)
		{
			int x = 0;
			while (x < width)
			{
				int a = ((src[offSrc] - 128) * Y_COEFF >> 13) + 128;
				int b = ((src[offSrc + 1] - 128) * Y_COEFF >> 13) + 128;
				int c = ((src[offSrc + width] - 128) * Y_COEFF >> 13) + 128;
				int d = ((src[offSrc + width + 1] - 128) * Y_COEFF >> 13) + 128;
				dst[offDst] = a + b + c + d + 2 >> 2;
				x += 2;
				offDst++;
				offSrc += 2;
			}
			offSrc += width;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 162, 105, 105, 117, 51, 167, 127, 4,
		127, 6
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] sy = src.getPlaneData(0);
		int[] dy = dst.getPlaneData(0);
		for (int i = 0; i < src.getPlaneWidth(0) * src.getPlaneHeight(0); i++)
		{
			dy[i] = (sy[i] * Y_COEFF >> 13) + 16;
		}
		copyAvg(src.getPlaneData(1), dst.getPlaneData(1), src.getPlaneWidth(1), src.getPlaneHeight(1));
		copyAvg(src.getPlaneData(2), dst.getPlaneData(2), src.getPlaneWidth(2), src.getPlaneHeight(2));
	}
}
