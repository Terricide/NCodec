using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class Yuv444jToYuv420j : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public Yuv444jToYuv420j()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 139, 162, 111, 151, 106, 105, 105, 106, 119,
		110, 63, 8, 53, 243, 60, 234, 75
	})]
	public virtual void transform(Picture src, Picture dst)
	{
		int size = src.getWidth() * src.getHeight();
		ByteCodeHelper.arraycopy_primitive_1(src.getPlaneData(0), 0, dst.getPlaneData(0), 0, size);
		for (int plane = 1; plane < 3; plane++)
		{
			byte[] srcPl = src.getPlaneData(plane);
			byte[] dstPl = dst.getPlaneData(plane);
			int srcStride = src.getPlaneWidth(plane);
			int y = 0;
			int srcOff = 0;
			int dstOff = 0;
			while (y < src.getHeight())
			{
				int x = 0;
				while (x < src.getWidth())
				{
					dstPl[dstOff] = (byte)(sbyte)(srcPl[srcOff] + srcPl[srcOff + 1] + srcPl[srcOff + srcStride] + srcPl[srcOff + srcStride + 1] + 2 >> 2);
					x += 2;
					srcOff += 2;
					dstOff++;
				}
				y += 2;
				srcOff += srcStride;
			}
		}
	}
}
