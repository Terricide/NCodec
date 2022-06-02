using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class RgbToYuv422p : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public RgbToYuv422p()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 98, 106, 104, 104, 136, 103, 113, 115,
		135, 127, 6, 138, 127, 6, 140, 115, 115, 231,
		53, 44, 236, 79
	})]
	public virtual void transform(Picture img, Picture dst)
	{
		byte[] y = img.getData()[0];
		byte[] out1 = new byte[3];
		byte[] out2 = new byte[3];
		byte[][] dstData = dst.getData();
		int off = 0;
		int offSrc = 0;
		for (int i = 0; i < img.getHeight(); i++)
		{
			for (int j = 0; j < img.getWidth() >> 1; j++)
			{
				int offY = off << 1;
				int num = offSrc;
				offSrc++;
				byte r = y[num];
				int num2 = offSrc;
				offSrc++;
				byte g = y[num2];
				int num3 = offSrc;
				offSrc++;
				RgbToYuv420p.rgb2yuv(r, g, y[num3], out1);
				dstData[0][offY] = out1[0];
				int num4 = offSrc;
				offSrc++;
				byte r2 = y[num4];
				int num5 = offSrc;
				offSrc++;
				byte g2 = y[num5];
				int num6 = offSrc;
				offSrc++;
				RgbToYuv420p.rgb2yuv(r2, g2, y[num6], out2);
				dstData[0][offY + 1] = out2[0];
				dstData[1][off] = (byte)(sbyte)(out1[1] + out2[1] + 1 >> 1);
				dstData[2][off] = (byte)(sbyte)(out1[2] + out2[2] + 1 >> 1);
				off++;
			}
		}
	}
}
