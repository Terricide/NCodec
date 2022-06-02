using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.common.dct;

public class IDCT4x4 : Object
{
	public const int CN_SHIFT = 12;

	internal static int ___003C_003EC1;

	internal static int ___003C_003EC2;

	internal static int ___003C_003EC3;

	public const int C_SHIFT = 18;

	public const int RN_SHIFT = 15;

	internal static int ___003C_003ER1;

	internal static int ___003C_003ER2;

	internal static int ___003C_003ER3;

	public const int R_SHIFT = 11;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int C1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EC1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int C2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EC2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int C3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EC3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int R1
	{
		[HideFromJava]
		get
		{
			return ___003C_003ER1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int R2
	{
		[HideFromJava]
		get
		{
			return ___003C_003ER2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int R3
	{
		[HideFromJava]
		get
		{
			return ___003C_003ER3;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 103, 44, 199, 103, 42, 167 })]
	public static void idct(int[] blk, int off)
	{
		for (int i = 0; i < 4; i++)
		{
			idct4row(blk, off + (i << 2));
		}
		for (int i = 0; i < 4; i++)
		{
			idct4col_add(blk, off + i);
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 126, 162, 101, 103, 103, 103, 114, 114, 114,
		114, 108, 110, 110, 110
	})]
	private static void idct4row(int[] blk, int off)
	{
		int a0 = blk[off];
		int a1 = blk[off + 1];
		int a2 = blk[off + 2];
		int a3 = blk[off + 3];
		int c0 = (a0 + a2) * ___003C_003ER3 + 1024;
		int c2 = (a0 - a2) * ___003C_003ER3 + 1024;
		int c1 = a1 * ___003C_003ER1 + a3 * ___003C_003ER2;
		int c3 = a1 * ___003C_003ER2 - a3 * ___003C_003ER1;
		blk[off] = c0 + c1 >> 11;
		blk[off + 1] = c2 + c3 >> 11;
		blk[off + 2] = c2 - c3 >> 11;
		blk[off + 3] = c0 - c1 >> 11;
	}

	[LineNumberTable(new byte[]
	{
		159, 133, 130, 101, 103, 103, 104, 114, 114, 114,
		146, 108, 110, 110, 111
	})]
	private static void idct4col_add(int[] blk, int off)
	{
		int a0 = blk[off];
		int a1 = blk[off + 4];
		int a2 = blk[off + 8];
		int a3 = blk[off + 12];
		int c0 = (a0 + a2) * ___003C_003EC3 + 131072;
		int c2 = (a0 - a2) * ___003C_003EC3 + 131072;
		int c1 = a1 * ___003C_003EC1 + a3 * ___003C_003EC2;
		int c3 = a1 * ___003C_003EC2 - a3 * ___003C_003EC1;
		blk[off] = c0 + c1 >> 18;
		blk[off + 4] = c2 + c3 >> 18;
		blk[off + 8] = c2 - c3 >> 18;
		blk[off + 12] = c0 - c1 >> 18;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(27)]
	public static int C_FIX(double x)
	{
		return ByteCodeHelper.d2i(x * 1.414213562 * 4096.0 + 0.5);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(56)]
	public static int R_FIX(double x)
	{
		return ByteCodeHelper.d2i(x * 1.414213562 * 32768.0 + 0.5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public IDCT4x4()
	{
	}

	[LineNumberTable(new byte[] { 159, 135, 130, 116, 116, 244, 91, 116, 116 })]
	static IDCT4x4()
	{
		___003C_003EC1 = C_FIX(0.6532814824);
		___003C_003EC2 = C_FIX(0.2705980501);
		___003C_003EC3 = C_FIX(0.5);
		___003C_003ER1 = R_FIX(0.6532814824);
		___003C_003ER2 = R_FIX(0.2705980501);
		___003C_003ER3 = R_FIX(0.5);
	}
}
