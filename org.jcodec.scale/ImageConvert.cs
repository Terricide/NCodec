using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;

namespace org.jcodec.scale;

public class ImageConvert : Object
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

	private const int CROP = 1024;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static byte[] cropTable;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] intCropTable;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static byte[] _y_ccir_to_jpeg;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static byte[] _y_jpeg_to_ccir;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(76)]
	public static int icrop(int i)
	{
		return intCropTable[i + 1024];
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(80)]
	public static byte crop(int i)
	{
		return cropTable[i + 1024];
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(16)]
	private static int FIX(double x)
	{
		return ByteCodeHelper.d2i(x * 1024.0 + 0.5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(48)]
	internal static int Y_CCIR_TO_JPEG(int y)
	{
		return y * FIX(1.1643835616438356) + (512 - 16 * FIX(1.1643835616438356)) >> 10;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(44)]
	internal static int Y_JPEG_TO_CCIR(int y)
	{
		return y * FIX(73.0 / 85.0) + 16896 >> 10;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public ImageConvert()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 136, 98, 103, 106, 106, 111, 119, 143, 104,
		105, 105, 105, 107, 107
	})]
	public static int ycbcr_to_rgb24(int y, int cb, int cr)
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
		r = (sbyte)crop(r);
		g = (sbyte)crop(g);
		b = (sbyte)crop(b);
		return ((r & 0xFF) << 16) | ((g & 0xFF) << 8) | (b & 0xFF);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 121, 65, 68 })]
	public static byte y_ccir_to_jpeg(byte y)
	{
		int y2 = (sbyte)y;
		return _y_ccir_to_jpeg[y2 & 0xFF];
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 120, 65, 68 })]
	public static byte y_jpeg_to_ccir(byte y)
	{
		int y2 = (sbyte)y;
		return _y_jpeg_to_ccir[y2 & 0xFF];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 98, 102, 105, 137, 121, 127, 0, 122,
		111, 112, 112
	})]
	public static void YUV444toRGB888(int y, int u, int v, ByteBuffer rgb)
	{
		int c = y - 16;
		int d = u - 128;
		int e = v - 128;
		int r = 298 * c + 409 * e + 128 >> 8;
		int g = 298 * c - 100 * d - 208 * e + 128 >> 8;
		int b = 298 * c + 516 * d + 128 >> 8;
		rgb.put((byte)(sbyte)crop(r));
		rgb.put((byte)(sbyte)crop(g));
		rgb.put((byte)(sbyte)crop(b));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 162, 111, 111, 111, 115, 113, 113, 107,
		109, 109, 114, 118, 118
	})]
	public static void RGB888toYUV444(ByteBuffer rgb, ByteBuffer Y, ByteBuffer U, ByteBuffer V)
	{
		int r = (sbyte)rgb.get() & 0xFF;
		int g = (sbyte)rgb.get() & 0xFF;
		int b = (sbyte)rgb.get() & 0xFF;
		int y = 66 * r + 129 * g + 25 * b;
		int u = -38 * r - 74 * g + 112 * b;
		int v = 112 * r - 94 * g - 18 * b;
		y = y + 128 >> 8;
		u = u + 128 >> 8;
		v = v + 128 >> 8;
		Y.put((byte)(sbyte)crop(y + 16));
		U.put((byte)(sbyte)crop(u + 128));
		V.put((byte)(sbyte)crop(v + 128));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 130, 115, 107 })]
	public static byte RGB888toY4(int r, int g, int b)
	{
		int y = 66 * r + 129 * g + 25 * b;
		y = y + 128 >> 8;
		sbyte result = (sbyte)crop(y + 16);
		
		return (byte)result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 66, 112, 107 })]
	public static byte RGB888toU4(int r, int g, int b)
	{
		int u = -38 * r - 74 * g + 112 * b;
		u = u + 128 >> 8;
		sbyte result = (sbyte)crop(u + 128);
		
		return (byte)result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 130, 112, 107 })]
	public static byte RGB888toV4(int r, int g, int b)
	{
		int v = 112 * r - 94 * g - 18 * b;
		v = v + 128 >> 8;
		sbyte result = (sbyte)crop(v + 128);
		
		return (byte)result;
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 162, 116, 116, 117, 244, 93, 112, 112,
		112, 176, 107, 111, 15, 199, 107, 112, 15, 199,
		111, 111, 19, 199, 107, 116, 20, 199
	})]
	static ImageConvert()
	{
		FIX_0_71414 = FIX(0.71414);
		FIX_1_772 = FIX(1.772);
		_FIX_0_34414 = -FIX(0.34414);
		FIX_1_402 = FIX(1.402);
		cropTable = new byte[2304];
		intCropTable = new int[2304];
		_y_ccir_to_jpeg = new byte[256];
		_y_jpeg_to_ccir = new byte[256];
		for (int l = -1024; l < 0; l++)
		{
			cropTable[l + 1024] = 0;
			intCropTable[l + 1024] = 0;
		}
		for (int k = 0; k < 256; k++)
		{
			cropTable[k + 1024] = (byte)(sbyte)k;
			intCropTable[k + 1024] = k;
		}
		for (int j = 256; j < 1024; j++)
		{
			cropTable[j + 1024] = byte.MaxValue;
			intCropTable[j + 1024] = 255;
		}
		for (int i = 0; i < 256; i++)
		{
			_y_ccir_to_jpeg[i] = (byte)(sbyte)crop(Y_CCIR_TO_JPEG(i));
			_y_jpeg_to_ccir[i] = (byte)(sbyte)crop(Y_JPEG_TO_CCIR(i));
		}
	}
}
