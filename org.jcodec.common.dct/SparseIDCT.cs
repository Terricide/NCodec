using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common.dct;

public class SparseIDCT : Object
{
	internal static int[][] ___003C_003ECOEFF;

	public const int PRECISION = 13;

	public const int DC_SHIFT = 10;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] COEFF
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECOEFF;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 103, 104, 103, 103, 103, 231, 60,
		231, 70
	})]
	public static void start(int[] block, int dc)
	{
		dc <<= 10;
		for (int i = 0; i < 64; i += 4)
		{
			block[i + 0] = dc;
			block[i + 1] = dc;
			block[i + 2] = dc;
			block[i + 3] = dc;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 107, 119, 123, 123, 251, 60, 234,
		70
	})]
	public static void coeff(int[] block, int ind, int level)
	{
		for (int i = 0; i < 64; i += 4)
		{
			int num = i;
			int[] array = block;
			array[num] += ___003C_003ECOEFF[ind][i] * level;
			num = i + 1;
			array = block;
			array[num] += ___003C_003ECOEFF[ind][i + 1] * level;
			num = i + 2;
			array = block;
			array[num] += ___003C_003ECOEFF[ind][i + 2] * level;
			num = i + 3;
			array = block;
			array[num] += ___003C_003ECOEFF[ind][i + 3] * level;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 124, 98, 104, 108, 112, 112, 240, 60, 231,
		70
	})]
	public static void finish(int[] block)
	{
		for (int i = 0; i < 64; i += 4)
		{
			block[i] = div(block[i]);
			block[i + 1] = div(block[i + 1]);
			block[i + 2] = div(block[i + 2]);
			block[i + 3] = div(block[i + 3]);
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 122, 130, 102, 102 })]
	private static int div(int x)
	{
		int i = x >> 31;
		int j = (int)((uint)x >> 31);
		return (((x ^ i) + j >> 13) ^ i) + j;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public SparseIDCT()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 98, 237, 69, 111, 146, 103, 104, 111,
		107, 238, 61, 231, 69
	})]
	static SparseIDCT()
	{
		___003C_003ECOEFF = new int[64][];
		___003C_003ECOEFF[0] = new int[64];
		Arrays.fill(___003C_003ECOEFF[0], 1024);
		int ac = 8192;
		for (int i = 1; i < 64; i++)
		{
			___003C_003ECOEFF[i] = new int[64];
			___003C_003ECOEFF[i][i] = ac;
			SimpleIDCT10Bit.idct10(___003C_003ECOEFF[i], 0);
		}
	}
}
