using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.codecs.mpa;

public class Mp3Mdct : Object
{
	private const float factor36pt0 = 0.347296357f;

	private const float factor36pt1 = 1.53208888f;

	private const float factor36pt2 = 1.87938523f;

	private const float factor36pt3 = 1.73205078f;

	private const float factor36pt4 = 1.96961546f;

	private const float factor36pt5 = 1.28557527f;

	private const float factor36pt6 = 0.6840403f;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] factor36;

	private const float cos075 = 0.9914449f;

	private const float cos225 = 0.9238795f;

	private const float cos300 = 0.8660254f;

	private const float cos375 = 0.7933533f;

	private const float cos450 = 0.707106769f;

	private const float cos525 = 0.6087614f;

	private const float cos600 = 0.5f;

	private const float cos675 = 0.382683426f;

	private const float cos825 = 0.130526185f;

	private const float factor12pt0 = 1.93185163f;

	private const float factor12pt1 = 0.5176381f;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] factor12;

	private static float[] tmp;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 98, 140, 105, 42, 171 })]
	internal static void threeShort(float[] src, float[] dst)
	{
		Arrays.fill(dst, 0f);
		int i = 0;
		int outOff = 0;
		while (i < 3)
		{
			imdct12(src, dst, outOff, i);
			i++;
			outOff += 6;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		132,
		162,
		104,
		50,
		167,
		104,
		50,
		168,
		111,
		109,
		110,
		144,
		127,
		28,
		127,
		25,
		127,
		28,
		159,
		28,
		127,
		29,
		127,
		11,
		127,
		29,
		byte.MaxValue,
		29,
		51,
		242,
		80,
		118,
		107,
		107,
		122,
		117,
		127,
		6,
		byte.MaxValue,
		2,
		58,
		254,
		73,
		105,
		124,
		123,
		111,
		239,
		60,
		233,
		71,
		108,
		127,
		0,
		126,
		111,
		239,
		60,
		236,
		71,
		124,
		127,
		5,
		106,
		105,
		121
	})]
	internal static void oneLong(float[] src, float[] dst)
	{
		int num;
		float[] array;
		for (int n = 17; n > 0; n += -1)
		{
			num = n;
			array = src;
			array[num] += src[n - 1];
		}
		for (int m = 17; m > 2; m += -2)
		{
			num = m;
			array = src;
			array[num] += src[m - 2];
		}
		int l = 0;
		int k3 = 0;
		while (l < 2)
		{
			float tmp = src[l] + src[l];
			float tmp3 = tmp + src[12 + l];
			float tmp4 = src[6 + l] * 1.73205078f;
			Mp3Mdct.tmp[k3 + 0] = tmp3 + src[4 + l] * 1.87938523f + src[8 + l] * 1.53208888f + src[16 + l] * 0.347296357f;
			Mp3Mdct.tmp[k3 + 1] = tmp + src[4 + l] - src[8 + l] - src[12 + l] - src[12 + l] - src[16 + l];
			Mp3Mdct.tmp[k3 + 2] = tmp3 - src[4 + l] * 0.347296357f - src[8 + l] * 1.87938523f + src[16 + l] * 1.53208888f;
			Mp3Mdct.tmp[k3 + 3] = tmp3 - src[4 + l] * 1.53208888f + src[8 + l] * 0.347296357f - src[16 + l] * 1.87938523f;
			Mp3Mdct.tmp[k3 + 4] = src[2 + l] * 1.96961546f + tmp4 + src[10 + l] * 1.28557527f + src[14 + l] * 0.6840403f;
			Mp3Mdct.tmp[k3 + 5] = (src[2 + l] - src[10 + l] - src[14 + l]) * 1.73205078f;
			Mp3Mdct.tmp[k3 + 6] = src[2 + l] * 1.28557527f - tmp4 - src[10 + l] * 0.6840403f + src[14 + l] * 1.96961546f;
			Mp3Mdct.tmp[k3 + 7] = src[2 + l] * 0.6840403f - tmp4 + src[10 + l] * 1.96961546f - src[14 + l] * 1.28557527f;
			l++;
			k3 += 8;
		}
		int k = 0;
		int j2 = 4;
		int k2 = 8;
		int l2 = 12;
		while (k < 4)
		{
			float q1 = Mp3Mdct.tmp[k];
			float q2 = Mp3Mdct.tmp[k2];
			float[] array2 = Mp3Mdct.tmp;
			num = k;
			array = array2;
			array[num] += Mp3Mdct.tmp[j2];
			Mp3Mdct.tmp[j2] = q1 - Mp3Mdct.tmp[j2];
			Mp3Mdct.tmp[k2] = (Mp3Mdct.tmp[k2] + Mp3Mdct.tmp[l2]) * factor36[k];
			Mp3Mdct.tmp[l2] = (q2 - Mp3Mdct.tmp[l2]) * factor36[7 - k];
			k++;
			j2++;
			k2++;
			l2++;
		}
		for (int j = 0; j < 4; j++)
		{
			dst[26 - j] = Mp3Mdct.tmp[j] + Mp3Mdct.tmp[8 + j];
			dst[8 - j] = Mp3Mdct.tmp[8 + j] - Mp3Mdct.tmp[j];
			dst[27 + j] = dst[26 - j];
			dst[9 + j] = 0f - dst[8 - j];
		}
		for (int i = 0; i < 4; i++)
		{
			dst[21 - i] = Mp3Mdct.tmp[7 - i] + Mp3Mdct.tmp[15 - i];
			dst[3 - i] = Mp3Mdct.tmp[15 - i] - Mp3Mdct.tmp[7 - i];
			dst[32 + i] = dst[21 - i];
			dst[14 + i] = 0f - dst[3 - i];
		}
		float tmp0 = src[0] - src[4] + src[8] - src[12] + src[16];
		float tmp2 = (src[1] - src[5] + src[9] - src[13] + src[17]) * 0.707106769f;
		dst[4] = tmp2 - tmp0;
		dst[13] = 0f - dst[4];
		float num2 = tmp0 + tmp2;
		num = 22;
		array = dst;
		array[num] = num2;
		dst[31] = num2;
	}

	[LineNumberTable(new byte[]
	{
		159, 116, 162, 113, 48, 173, 118, 149, 112, 111,
		108, 115, 110, 142, 112, 112, 108, 117, 110, 142,
		118, 118, 150, 106, 120, 147, 106, 120, 147, 106,
		120, 147, 105, 58, 169, 119, 120, 119, 120, 119,
		152, 111, 118, 150, 119, 119, 151, 150, 111, 54,
		175
	})]
	private static void imdct12(float[] src, float[] dst, int outOff, int wndIdx)
	{
		int l = 15 + wndIdx;
		int m = 12 + wndIdx;
		int num;
		float[] array;
		while (l >= 3 + wndIdx)
		{
			num = l;
			array = src;
			array[num] += src[m];
			l += -3;
			m += -3;
		}
		num = 15 + wndIdx;
		array = src;
		array[num] += src[9 + wndIdx];
		num = 9 + wndIdx;
		array = src;
		array[num] += src[3 + wndIdx];
		float pp2 = src[12 + wndIdx] * 0.5f;
		float pp1 = src[6 + wndIdx] * 0.8660254f;
		float sum = src[0 + wndIdx] + pp2;
		tmp[1] = src[wndIdx] - src[12 + wndIdx];
		tmp[0] = sum + pp1;
		tmp[2] = sum - pp1;
		pp2 = src[15 + wndIdx] * 0.5f;
		pp1 = src[9 + wndIdx] * 0.8660254f;
		sum = src[3 + wndIdx] + pp2;
		tmp[4] = src[3 + wndIdx] - src[15 + wndIdx];
		tmp[5] = sum + pp1;
		tmp[3] = sum - pp1;
		float[] array2 = tmp;
		num = 3;
		array = array2;
		array[num] *= 1.93185163f;
		float[] array3 = tmp;
		num = 4;
		array = array3;
		array[num] *= 0.707106769f;
		float[] array4 = tmp;
		num = 5;
		array = array4;
		array[num] *= 0.5176381f;
		float t = tmp[0];
		float[] array5 = tmp;
		num = 0;
		array = array5;
		array[num] += tmp[5];
		tmp[5] = t - tmp[5];
		t = tmp[1];
		float[] array6 = tmp;
		num = 1;
		array = array6;
		array[num] += tmp[4];
		tmp[4] = t - tmp[4];
		t = tmp[2];
		float[] array7 = tmp;
		num = 2;
		array = array7;
		array[num] += tmp[3];
		tmp[3] = t - tmp[3];
		for (int k = 0; k < 6; k++)
		{
			float[] array8 = tmp;
			num = k;
			array = array8;
			array[num] *= factor12[k];
		}
		tmp[8] = (0f - tmp[0]) * 0.7933533f;
		tmp[9] = (0f - tmp[0]) * 0.6087614f;
		tmp[7] = (0f - tmp[1]) * 0.9238795f;
		tmp[10] = (0f - tmp[1]) * 0.382683426f;
		tmp[6] = (0f - tmp[2]) * 0.9914449f;
		tmp[11] = (0f - tmp[2]) * 0.130526185f;
		tmp[0] = tmp[3];
		tmp[1] = tmp[4] * 0.382683426f;
		tmp[2] = tmp[5] * 0.6087614f;
		tmp[3] = (0f - tmp[5]) * 0.7933533f;
		tmp[4] = (0f - tmp[4]) * 0.9238795f;
		tmp[5] = (0f - tmp[0]) * 0.9914449f;
		float[] array9 = tmp;
		num = 0;
		array = array9;
		array[num] *= 0.130526185f;
		int i = 0;
		int j = outOff + 6;
		while (i < 12)
		{
			num = j;
			array = dst;
			array[num] += tmp[i];
			i++;
			j++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public Mp3Mdct()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		137,
		98,
		byte.MaxValue,
		45,
		79,
		191,
		29
	})]
	static Mp3Mdct()
	{
		factor36 = new float[8] { 0.5019099f, 0.5176381f, 0.551688969f, 0.610387266f, 0.8717234f, 1.18310082f, 1.93185163f, 5.73685646f };
		factor12 = new float[6] { 0.5043145f, 0.5411961f, 0.6302362f, 0.8213398f, 1.306563f, 3.830649f };
		tmp = new float[16];
	}
}
