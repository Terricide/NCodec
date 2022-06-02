using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac.sbr;

internal class DCT : Object
{
	private const int n = 32;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] w_array_real;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] w_array_imag;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] dct4_64_tab;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] bit_rev_tab;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 52, 130, 139, 101, 101, 111, 116, 244, 58,
		234, 74, 232, 69, 139, 106, 103, 135, 117, 121,
		249, 56, 234, 75, 119, 119, 140, 106, 103, 103,
		117, 121, 249, 57, 234, 74
	})]
	public static void dct4_kernel(float[] in_real, float[] in_imag, float[] out_real, float[] out_imag)
	{
		for (int i = 0; i < 32; i++)
		{
			float x_re3 = in_real[i];
			float x_im3 = in_imag[i];
			float tmp3 = (x_re3 + x_im3) * dct4_64_tab[i];
			in_real[i] = x_im3 * dct4_64_tab[i + 64] + tmp3;
			in_imag[i] = x_re3 * dct4_64_tab[i + 32] + tmp3;
		}
		fft_dif(in_real, in_imag);
		for (int i = 0; i < 16; i++)
		{
			int i_rev2 = bit_rev_tab[i];
			float x_re2 = in_real[i_rev2];
			float x_im2 = in_imag[i_rev2];
			float tmp2 = (x_re2 + x_im2) * dct4_64_tab[i + 96];
			out_real[i] = x_im2 * dct4_64_tab[i + 160] + tmp2;
			out_imag[i] = x_re2 * dct4_64_tab[i + 128] + tmp2;
		}
		out_imag[16] = (in_imag[1] - in_real[1]) * dct4_64_tab[112];
		out_real[16] = (in_real[1] + in_imag[1]) * dct4_64_tab[112];
		for (int i = 17; i < 32; i++)
		{
			int i_rev = bit_rev_tab[i];
			float x_re = in_real[i_rev];
			float x_im = in_imag[i_rev];
			float tmp = (x_re + x_im) * dct4_64_tab[i + 96];
			out_real[i] = x_im * dct4_64_tab[i + 160] + tmp;
			out_imag[i] = x_re * dct4_64_tab[i + 128] + tmp;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 105, 162, 107, 101, 101, 102, 102, 134, 106,
		170, 103, 167, 117, 181, 112, 240, 44, 234, 87,
		111, 107, 139, 100, 102, 102, 102, 103, 167, 105,
		169, 117, 181, 115, 147, 103, 102, 102, 102, 103,
		167, 105, 169, 117, 181, 115, 243, 24, 242, 110,
		107, 102, 102, 134, 103, 167, 117, 181, 107, 235,
		50, 234, 80, 138, 107, 102, 102, 134, 103, 167,
		105, 169, 117, 181, 111, 239, 46, 234, 84, 107,
		102, 102, 134, 103, 167, 117, 181, 107, 235, 50,
		234, 80, 139, 107, 102, 102, 134, 103, 167, 105,
		169, 117, 181, 111, 239, 46, 234, 87, 107, 102,
		102, 134, 103, 167, 117, 181, 107, 235, 50, 234,
		80, 107, 102, 102, 134, 103, 167, 117, 181, 107,
		235, 50, 234, 83, 107, 102, 102, 134, 103, 167,
		117, 181, 107, 235, 50, 234, 82
	})]
	private static void fft_dif(float[] Real, float[] Imag)
	{
		for (int i = 0; i < 16; i++)
		{
			float point1_real9 = Real[i];
			float point1_imag9 = Imag[i];
			int i10 = i + 16;
			float point2_real9 = Real[i10];
			float point2_imag9 = Imag[i10];
			float w_real3 = w_array_real[i];
			float w_imag2 = w_array_imag[i];
			point1_real9 -= point2_real9;
			point1_imag9 -= point2_imag9;
			int num = i;
			float[] array = Real;
			array[num] += point2_real9;
			num = i;
			array = Imag;
			array[num] += point2_imag9;
			Real[i10] = point1_real9 * w_real3 - point1_imag9 * w_imag2;
			Imag[i10] = point1_real9 * w_imag2 + point1_imag9 * w_real3;
		}
		int j = 0;
		int w_index = 0;
		while (j < 8)
		{
			float w_real2 = w_array_real[w_index];
			float w_imag = w_array_imag[w_index];
			int i = j;
			float point1_real8 = Real[i];
			float point1_imag8 = Imag[i];
			int i9 = i + 8;
			float point2_real8 = Real[i9];
			float point2_imag8 = Imag[i9];
			point1_real8 -= point2_real8;
			point1_imag8 -= point2_imag8;
			int num = i;
			float[] array = Real;
			array[num] += point2_real8;
			num = i;
			array = Imag;
			array[num] += point2_imag8;
			Real[i9] = point1_real8 * w_real2 - point1_imag8 * w_imag;
			Imag[i9] = point1_real8 * w_imag + point1_imag8 * w_real2;
			i = j + 16;
			point1_real8 = Real[i];
			point1_imag8 = Imag[i];
			i9 = i + 8;
			point2_real8 = Real[i9];
			point2_imag8 = Imag[i9];
			point1_real8 -= point2_real8;
			point1_imag8 -= point2_imag8;
			num = i;
			array = Real;
			array[num] += point2_real8;
			num = i;
			array = Imag;
			array[num] += point2_imag8;
			Real[i9] = point1_real8 * w_real2 - point1_imag8 * w_imag;
			Imag[i9] = point1_real8 * w_imag + point1_imag8 * w_real2;
			j++;
			w_index += 2;
		}
		for (int i = 0; i < 32; i += 8)
		{
			int i8 = i + 4;
			float point1_real7 = Real[i];
			float point1_imag7 = Imag[i];
			float point2_real7 = Real[i8];
			float point2_imag7 = Imag[i8];
			int num = i;
			float[] array = Real;
			array[num] += point2_real7;
			num = i;
			array = Imag;
			array[num] += point2_imag7;
			Real[i8] = point1_real7 - point2_real7;
			Imag[i8] = point1_imag7 - point2_imag7;
		}
		float w_real = w_array_real[4];
		for (int i = 1; i < 32; i += 8)
		{
			int i7 = i + 4;
			float point1_real6 = Real[i];
			float point1_imag6 = Imag[i];
			float point2_real6 = Real[i7];
			float point2_imag6 = Imag[i7];
			point1_real6 -= point2_real6;
			point1_imag6 -= point2_imag6;
			int num = i;
			float[] array = Real;
			array[num] += point2_real6;
			num = i;
			array = Imag;
			array[num] += point2_imag6;
			Real[i7] = (point1_real6 + point1_imag6) * w_real;
			Imag[i7] = (point1_imag6 - point1_real6) * w_real;
		}
		for (int i = 2; i < 32; i += 8)
		{
			int i6 = i + 4;
			float point1_real5 = Real[i];
			float point1_imag5 = Imag[i];
			float point2_real5 = Real[i6];
			float point2_imag5 = Imag[i6];
			int num = i;
			float[] array = Real;
			array[num] += point2_real5;
			num = i;
			array = Imag;
			array[num] += point2_imag5;
			Real[i6] = point1_imag5 - point2_imag5;
			Imag[i6] = point2_real5 - point1_real5;
		}
		w_real = w_array_real[12];
		for (int i = 3; i < 32; i += 8)
		{
			int i5 = i + 4;
			float point1_real4 = Real[i];
			float point1_imag4 = Imag[i];
			float point2_real4 = Real[i5];
			float point2_imag4 = Imag[i5];
			point1_real4 -= point2_real4;
			point1_imag4 -= point2_imag4;
			int num = i;
			float[] array = Real;
			array[num] += point2_real4;
			num = i;
			array = Imag;
			array[num] += point2_imag4;
			Real[i5] = (point1_real4 - point1_imag4) * w_real;
			Imag[i5] = (point1_real4 + point1_imag4) * w_real;
		}
		for (int i = 0; i < 32; i += 4)
		{
			int i4 = i + 2;
			float point1_real3 = Real[i];
			float point1_imag3 = Imag[i];
			float point2_real3 = Real[i4];
			float point2_imag3 = Imag[i4];
			int num = i;
			float[] array = Real;
			array[num] += point2_real3;
			num = i;
			array = Imag;
			array[num] += point2_imag3;
			Real[i4] = point1_real3 - point2_real3;
			Imag[i4] = point1_imag3 - point2_imag3;
		}
		for (int i = 1; i < 32; i += 4)
		{
			int i3 = i + 2;
			float point1_real2 = Real[i];
			float point1_imag2 = Imag[i];
			float point2_real2 = Real[i3];
			float point2_imag2 = Imag[i3];
			int num = i;
			float[] array = Real;
			array[num] += point2_real2;
			num = i;
			array = Imag;
			array[num] += point2_imag2;
			Real[i3] = point1_imag2 - point2_imag2;
			Imag[i3] = point2_real2 - point1_real2;
		}
		for (int i = 0; i < 32; i += 2)
		{
			int i2 = i + 1;
			float point1_real = Real[i];
			float point1_imag = Imag[i];
			float point2_real = Real[i2];
			float point2_imag = Imag[i2];
			int num = i;
			float[] array = Real;
			array[num] += point2_real;
			num = i;
			array = Imag;
			array[num] += point2_imag;
			Real[i2] = point1_real - point2_real;
			Imag[i2] = point1_imag - point2_imag;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	internal DCT()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		162,
		byte.MaxValue,
		117,
		76,
		byte.MaxValue,
		117,
		75,
		byte.MaxValue,
		167,
		40,
		160,
		99
	})]
	static DCT()
	{
		w_array_real = new float[16]
		{
			1f, 0.980785251f, 0.9238795f, 0.8314696f, 0.707106769f, 0.5555702f, 0.3826834f, 0.195090279f, 0f, -0.195090368f,
			-0.3826835f, -0.5555703f, -0.7071068f, -0.831469655f, -0.923879564f, -0.9807853f
		};
		w_array_imag = new float[16]
		{
			0f, -0.195090324f, -0.382683456f, -0.555570245f, -0.707106769f, -0.831469655f, -0.923879564f, -0.9807853f, -1f, -0.980785251f,
			-0.9238795f, -0.8314696f, -0.7071067f, -0.5555702f, -0.382683367f, -0.195090234f
		};
		dct4_64_tab = new float[192]
		{
			0.9999247f, 0.9981181f, 0.993907f, 0.9873014f, 0.9783174f, 0.966976464f, 0.953306f, 0.937339f, 0.9191139f, 0.8986745f,
			0.8760701f, 0.8513552f, 0.824589252f, 0.7958369f, 0.765167236f, 0.7326543f, 0.698376238f, 0.662415743f, 0.624859452f, 0.585797846f,
			0.545325f, 0.5035384f, 0.460538715f, 0.416429549f, 0.371317148f, 0.32531023f, 0.2785196f, 0.231058136f, 0.183039889f, 0.134580687f,
			0.08579727f, 0.0368071645f, -1.0121963f, -1.05943882f, -1.1041292f, -1.14615953f, -1.18542874f, -1.22184217f, -1.255312f, -1.28575766f,
			-1.313106f, -1.33729076f, -1.35825384f, -1.37594485f, -1.390321f, -1.40134788f, -1.40899873f, -1.41325521f, -1.41410708f, -1.41155219f,
			-1.40559673f, -1.396255f, -1.38354969f, -1.36751127f, -1.34817839f, -1.32559752f, -1.29982328f, -1.27091765f, -1.23895013f, -1.20399809f,
			-1.16614532f, -1.12548339f, -1.08210993f, -1.03612959f, -0.9876532f, -0.9367974f, -0.883684754f, -0.8284433f, -0.771206f, -0.712110758f,
			-0.6513001f, -0.588920355f, -0.5251218f, -0.460058242f, -0.393886328f, -0.326765478f, -0.258857429f, -0.190325916f, -0.121335685f, -0.0520532727f,
			0.0173546076f, 0.0867206454f, 0.155877829f, 0.224659324f, 0.292899728f, 0.3604344f, 0.427100927f, 0.492738456f, 0.5571889f, 0.620297134f,
			0.681911f, 0.741881847f, 0.8000656f, 0.856322f, 0.910515368f, 0.962515235f, 1f, 0.99879545f, 0.9951847f, 0.9891765f,
			0.980785251f, 0.970031261f, 0.956940353f, 0.941544056f, 0.9238795f, 0.9039893f, 0.881921232f, 0.8577286f, 0.8314696f, 0.8032075f,
			0.773010433f, 0.7409511f, 0.707106769f, 0.6715589f, 0.6343933f, 0.5956993f, 0.5555702f, 0.5141027f, 0.471396655f, 0.4275551f,
			0.382683426f, 0.336889833f, 0.290284634f, 0.242980123f, 0.195090234f, 0.1467305f, 0.0980171338f, 0.04906765f, -1f, -1.04786313f,
			-1.09320188f, -1.13590693f, -1.17587554f, -1.2130115f, -1.247225f, -1.27843392f, -1.3065629f, -1.3315444f, -1.353318f, -1.37183142f,
			-1.3870399f, -1.39890683f, -1.40740371f, -1.41251016f, 0f, -1.41251016f, -1.40740371f, -1.39890683f, -1.3870399f, -1.37183142f,
			-1.353318f, -1.3315444f, -1.3065629f, -1.27843392f, -1.247225f, -1.21301138f, -1.17587554f, -1.135907f, -1.09320188f, -1.04786313f,
			-1f, -0.9497278f, -0.897167563f, -0.842446f, -0.785694957f, -0.7270511f, -0.66665566f, -0.6046542f, -0.541196048f, -0.476434231f,
			-0.4105245f, -0.343625844f, -0.275899351f, -0.2075082f, -0.1386171f, -0.0693921447f, 0f, 0.0693922639f, 0.138617158f, 0.2075082f,
			0.27589947f, 0.343625963f, 0.410524637f, 0.4764342f, 0.5411961f, 0.6046542f, 0.6666557f, 0.727051139f, 0.7856951f, 0.842446f,
			0.897167563f, 0.9497278f
		};
		bit_rev_tab = new int[32]
		{
			0, 16, 8, 24, 4, 20, 12, 28, 2, 18,
			10, 26, 6, 22, 14, 30, 1, 17, 9, 25,
			5, 21, 13, 29, 3, 19, 11, 27, 7, 23,
			15, 31
		};
	}
}
