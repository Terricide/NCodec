using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv422pToYuv420jHiBD : Object, TransformHiBD
{
	public static int COEFF = 9362;

	private int shift;

	private int halfSrc;

	private int halfDst;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 109, 106, 159, 10, 121, 121 })]
	public Yuv422pToYuv420jHiBD(int upshift, int downshift)
	{
		shift = downshift + 13 - upshift;
		if (shift < 0)
		{
			string s = new StringBuilder().append("Maximum upshift allowed: ").append(downshift + 13).toString();
			
			throw new IllegalArgumentException(s);
		}
		halfSrc = 128 << Math.max(downshift - upshift, 0);
		halfDst = 128 << Math.max(upshift - downshift, 0);
	}

	[LineNumberTable(new byte[]
	{
		159, 133, 162, 101, 109, 106, 127, 5, 159, 7,
		237, 60, 242, 70, 229, 57, 234, 73
	})]
	private void copyAvg(int[] src, int[] dst, int width, int height)
	{
		int offSrc = 0;
		int offDst = 0;
		for (int y = 0; y < height / 2; y++)
		{
			int x = 0;
			while (x < width)
			{
				int a = ((src[offSrc] - halfSrc) * COEFF >> shift) + halfDst;
				int b = ((src[offSrc + width] - halfSrc) * COEFF >> shift) + halfDst;
				dst[offDst] = a + b + 1 >> 1;
				x++;
				offDst++;
				offSrc++;
			}
			offSrc += width;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 105, 105, 117, 58, 167, 127, 4,
		127, 6
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] sy = src.getPlaneData(0);
		int[] dy = dst.getPlaneData(0);
		for (int i = 0; i < src.getPlaneWidth(0) * src.getPlaneHeight(0); i++)
		{
			dy[i] = (sy[i] - 16) * COEFF >> shift;
		}
		copyAvg(src.getPlaneData(1), dst.getPlaneData(1), src.getPlaneWidth(1), src.getPlaneHeight(1));
		copyAvg(src.getPlaneData(2), dst.getPlaneData(2), src.getPlaneWidth(2), src.getPlaneHeight(2));
	}
}
