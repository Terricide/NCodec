using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class RgbToYuv422pHiBD : Object, TransformHiBD
{
	private int upShift;

	private int downShift;

	private int downShiftChr;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104, 106 })]
	public RgbToYuv422pHiBD(int upShift, int downShift)
	{
		this.upShift = upShift;
		this.downShift = downShift;
		downShiftChr = downShift + 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 106, 136, 101, 113, 115, 103, 135,
		134, 159, 9, 159, 2, 159, 11, 159, 6, 127,
		0, 127, 0, 229, 48, 44, 236, 84
	})]
	public virtual void transform(PictureHiBD img, PictureHiBD dst)
	{
		int[] y = img.getData()[0];
		int[][] dstData = dst.getData();
		int off = 0;
		int offSrc = 0;
		for (int i = 0; i < img.getHeight(); i++)
		{
			for (int j = 0; j < img.getWidth() >> 1; j++)
			{
				dstData[1][off] = 0;
				dstData[2][off] = 0;
				int offY = off << 1;
				int num = offSrc;
				offSrc++;
				int r = y[num];
				int num2 = offSrc;
				offSrc++;
				int g = y[num2];
				int num3 = offSrc;
				offSrc++;
				RgbToYuv420pHiBD.rgb2yuv(r, g, y[num3], dstData[0], offY, dstData[1], off, dstData[2], off);
				dstData[0][offY] = dstData[0][offY] << upShift >> downShift;
				int num4 = offSrc;
				offSrc++;
				int r2 = y[num4];
				int num5 = offSrc;
				offSrc++;
				int g2 = y[num5];
				int num6 = offSrc;
				offSrc++;
				RgbToYuv420pHiBD.rgb2yuv(r2, g2, y[num6], dstData[0], offY + 1, dstData[1], off, dstData[2], off);
				dstData[0][offY + 1] = dstData[0][offY + 1] << upShift >> downShift;
				dstData[1][off] = dstData[1][off] << upShift >> downShiftChr;
				dstData[2][off] = dstData[2][off] << upShift >> downShiftChr;
				off++;
			}
		}
	}
}
