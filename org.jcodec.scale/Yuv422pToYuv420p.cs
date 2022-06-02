using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class Yuv422pToYuv420p : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public Yuv422pToYuv420p()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 136, 98, 101, 106, 103, 50, 175, 229, 60,
		231, 70
	})]
	private void copyAvg(byte[] src, byte[] dst, int width, int height)
	{
		int offSrc = 0;
		int offDst = 0;
		for (int y = 0; y < height / 2; y++)
		{
			int x = 0;
			while (x < width)
			{
				dst[offDst] = (byte)(sbyte)(src[offSrc] + src[offSrc + width] + 1 >> 1);
				x++;
				offDst++;
				offSrc++;
			}
			offSrc += width;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 111, 119, 127, 4, 127, 6 })]
	public virtual void transform(Picture src, Picture dst)
	{
		int lumaSize = src.getWidth() * src.getHeight();
		ByteCodeHelper.arraycopy_primitive_1(src.getPlaneData(0), 0, dst.getPlaneData(0), 0, lumaSize);
		copyAvg(src.getPlaneData(1), dst.getPlaneData(1), src.getPlaneWidth(1), src.getPlaneHeight(1));
		copyAvg(src.getPlaneData(2), dst.getPlaneData(2), src.getPlaneWidth(2), src.getPlaneHeight(2));
	}
}
