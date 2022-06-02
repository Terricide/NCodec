using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.scale.highbd;

public class Yuv420jToRgbHiBD : Object, TransformHiBD
{
	private const int SCALEBITS = 10;

	private const int ONE_HALF = 512;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_0_71414;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_1_772;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int _FIX_0_34414;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_1_402;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 105 })]
	public Yuv420jToRgbHiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 103, 106, 106, 111, 119, 143, 104,
		105, 105, 113, 116, 116
	})]
	public static void YUVJtoRGB(int y, int cb, int cr, int[] data, int off)
	{
		y <<= 10;
		cb -= 128;
		cr -= 128;
		int add_r = FIX_1_402 * cr + 512;
		int add_g = _FIX_0_34414 * cb - FIX_0_71414 * cr + 512;
		int add_b = FIX_1_772 * cb + 512;
		int r = y + add_r >> 10;
		int g = y + add_g >> 10;
		int b = y + add_b >> 10;
		data[off] = MathUtil.clip(r, 0, 255);
		data[off + 1] = MathUtil.clip(g, 0, 255);
		data[off + 2] = MathUtil.clip(b, 0, 255);
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(70)]
	private static int FIX(double x)
	{
		return ByteCodeHelper.d2i(x * 1024.0 + 0.5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 162, 105, 105, 105, 137, 103, 105, 115,
		115, 103, 125, 159, 2, 127, 4, 159, 8, 231,
		56, 236, 74, 110, 139, 125, 159, 4, 167, 234,
		44, 236, 86, 110, 115, 103, 125, 159, 2, 231,
		59, 236, 71, 107, 139, 157, 167
	})]
	public void transform(PictureHiBD src, PictureHiBD dst)
	{
		int[] y = src.getPlaneData(0);
		int[] u = src.getPlaneData(1);
		int[] v = src.getPlaneData(2);
		int[] data = dst.getPlaneData(0);
		int offLuma = 0;
		int offChroma = 0;
		int stride = dst.getWidth();
		for (int i = 0; i < dst.getHeight() >> 1; i++)
		{
			for (int k2 = 0; k2 < dst.getWidth() >> 1; k2++)
			{
				int m = k2 << 1;
				YUVJtoRGB(y[offLuma + m], u[offChroma], v[offChroma], data, (offLuma + m) * 3);
				YUVJtoRGB(y[offLuma + m + 1], u[offChroma], v[offChroma], data, (offLuma + m + 1) * 3);
				YUVJtoRGB(y[offLuma + m + stride], u[offChroma], v[offChroma], data, (offLuma + m + stride) * 3);
				YUVJtoRGB(y[offLuma + m + stride + 1], u[offChroma], v[offChroma], data, (offLuma + m + stride + 1) * 3);
				offChroma++;
			}
			if (((uint)dst.getWidth() & (true ? 1u : 0u)) != 0)
			{
				int l = dst.getWidth() - 1;
				YUVJtoRGB(y[offLuma + l], u[offChroma], v[offChroma], data, (offLuma + l) * 3);
				YUVJtoRGB(y[offLuma + l + stride], u[offChroma], v[offChroma], data, (offLuma + l + stride) * 3);
				offChroma++;
			}
			offLuma += 2 * stride;
		}
		if (((uint)dst.getHeight() & (true ? 1u : 0u)) != 0)
		{
			for (int n = 0; n < dst.getWidth() >> 1; n++)
			{
				int k = n << 1;
				YUVJtoRGB(y[offLuma + k], u[offChroma], v[offChroma], data, (offLuma + k) * 3);
				YUVJtoRGB(y[offLuma + k + 1], u[offChroma], v[offChroma], data, (offLuma + k + 1) * 3);
				offChroma++;
			}
			if (((uint)dst.getWidth() & (true ? 1u : 0u)) != 0)
			{
				int j = dst.getWidth() - 1;
				YUVJtoRGB(y[offLuma + j], u[offChroma], v[offChroma], data, (offLuma + j) * 3);
				offChroma++;
			}
		}
	}

	[LineNumberTable(new byte[] { 159, 124, 98, 116, 116, 117 })]
	static Yuv420jToRgbHiBD()
	{
		FIX_0_71414 = FIX(0.71414);
		FIX_1_772 = FIX(1.772);
		_FIX_0_34414 = -FIX(0.34414);
		FIX_1_402 = FIX(1.402);
	}
}
