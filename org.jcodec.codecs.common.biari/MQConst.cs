using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.common.biari;

public class MQConst : Object
{
	internal static int[] ___003C_003EpLps;

	internal static int[] ___003C_003EmpsSwitch;

	internal static int[] ___003C_003EtransitLPS;

	internal static int[] ___003C_003EtransitMPS;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] pLps
	{
		[HideFromJava]
		get
		{
			return ___003C_003EpLps;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] mpsSwitch
	{
		[HideFromJava]
		get
		{
			return ___003C_003EmpsSwitch;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] transitLPS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EtransitLPS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] transitMPS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EtransitMPS;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MQConst()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		130,
		byte.MaxValue,
		161,
		56,
		69,
		191,
		160,
		144,
		223,
		160,
		188
	})]
	static MQConst()
	{
		___003C_003EpLps = new int[47]
		{
			22017, 13313, 6145, 2753, 1313, 545, 22017, 21505, 18433, 14337,
			12289, 9217, 7169, 5633, 22017, 21505, 20737, 18433, 14337, 13313,
			12289, 10241, 9217, 8705, 7169, 6145, 5633, 5121, 4609, 4353,
			2753, 2497, 2209, 1313, 1089, 673, 545, 321, 273, 133,
			73, 37, 21, 9, 5, 1, 22017
		};
		___003C_003EmpsSwitch = new int[47]
		{
			1, 0, 0, 0, 0, 0, 1, 0, 0, 0,
			0, 0, 0, 0, 1, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0
		};
		___003C_003EtransitLPS = new int[47]
		{
			1, 6, 9, 12, 29, 33, 6, 14, 14, 14,
			17, 18, 20, 21, 14, 14, 15, 16, 17, 18,
			19, 19, 20, 21, 22, 23, 24, 25, 26, 27,
			28, 29, 30, 31, 32, 33, 34, 35, 36, 37,
			38, 39, 40, 41, 42, 43, 46
		};
		___003C_003EtransitMPS = new int[47]
		{
			1, 2, 3, 4, 5, 38, 7, 8, 9, 10,
			11, 12, 13, 29, 15, 16, 17, 18, 19, 20,
			21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
			31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
			41, 42, 43, 44, 45, 45, 46
		};
	}
}
