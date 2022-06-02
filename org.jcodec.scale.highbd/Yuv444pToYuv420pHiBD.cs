using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class Yuv444pToYuv420pHiBD : Object, TransformHiBD
{
	private int shiftUp;

	private int shiftDown;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 105, 104, 104 })]
	public Yuv444pToYuv420pHiBD(int shiftUp, int shiftDown)
	{
		this.shiftUp = shiftUp;
		this.shiftDown = shiftDown;
	}

	[LineNumberTable(new byte[]
	{
		159, 129, 66, 101, 109, 103, 63, 0, 175, 229,
		60, 234, 70
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
				dst[offDst] = src[offSrc] + src[offSrc + 1] + src[offSrc + width] + src[offSrc + width + 1] + 2 >> 2;
				x += 2;
				offDst++;
				offSrc += 2;
			}
			offSrc += width;
		}
	}

	[LineNumberTable(new byte[] { 159, 131, 130, 104, 48, 167 })]
	private void up(int[] dst, int up)
	{
		for (int i = 0; i < (nint)dst.LongLength; i++)
		{
			int num = i;
			dst[num] <<= up;
		}
	}

	[LineNumberTable(new byte[] { 159, 132, 66, 104, 48, 167 })]
	private void down(int[] dst, int down)
	{
		for (int i = 0; i < (nint)dst.LongLength; i++)
		{
			int num = i;
			dst[num] >>= down;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 111, 119, 127, 4, 159, 4, 111,
		123, 123, 125, 111, 123, 123, 157
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		int lumaSize = src.getWidth() * src.getHeight();
		ByteCodeHelper.arraycopy_primitive_4(src.getPlaneData(0), 0, dst.getPlaneData(0), 0, lumaSize);
		copyAvg(src.getPlaneData(1), dst.getPlaneData(1), src.getPlaneWidth(1), src.getPlaneHeight(1));
		copyAvg(src.getPlaneData(2), dst.getPlaneData(2), src.getPlaneWidth(2), src.getPlaneHeight(2));
		if (shiftUp > shiftDown)
		{
			up(dst.getPlaneData(0), shiftUp - shiftDown);
			up(dst.getPlaneData(1), shiftUp - shiftDown);
			up(dst.getPlaneData(2), shiftUp - shiftDown);
		}
		else if (shiftDown > shiftUp)
		{
			down(dst.getPlaneData(0), shiftDown - shiftUp);
			down(dst.getPlaneData(1), shiftDown - shiftUp);
			down(dst.getPlaneData(2), shiftDown - shiftUp);
		}
	}
}
