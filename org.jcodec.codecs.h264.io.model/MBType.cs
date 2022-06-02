using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class MBType : Object
{
	internal static MBType ___003C_003EI_NxN;

	internal static MBType ___003C_003EI_16x16;

	internal static MBType ___003C_003EI_PCM;

	internal static MBType ___003C_003EP_16x16;

	internal static MBType ___003C_003EP_16x8;

	internal static MBType ___003C_003EP_8x16;

	internal static MBType ___003C_003EP_8x8;

	internal static MBType ___003C_003EP_8x8ref0;

	internal static MBType ___003C_003EB_Direct_16x16;

	internal static MBType ___003C_003EB_L0_16x16;

	internal static MBType ___003C_003EB_L1_16x16;

	internal static MBType ___003C_003EB_Bi_16x16;

	internal static MBType ___003C_003EB_L0_L0_16x8;

	internal static MBType ___003C_003EB_L0_L0_8x16;

	internal static MBType ___003C_003EB_L1_L1_16x8;

	internal static MBType ___003C_003EB_L1_L1_8x16;

	internal static MBType ___003C_003EB_L0_L1_16x8;

	internal static MBType ___003C_003EB_L0_L1_8x16;

	internal static MBType ___003C_003EB_L1_L0_16x8;

	internal static MBType ___003C_003EB_L1_L0_8x16;

	internal static MBType ___003C_003EB_L0_Bi_16x8;

	internal static MBType ___003C_003EB_L0_Bi_8x16;

	internal static MBType ___003C_003EB_L1_Bi_16x8;

	internal static MBType ___003C_003EB_L1_Bi_8x16;

	internal static MBType ___003C_003EB_Bi_L0_16x8;

	internal static MBType ___003C_003EB_Bi_L0_8x16;

	internal static MBType ___003C_003EB_Bi_L1_16x8;

	internal static MBType ___003C_003EB_Bi_L1_8x16;

	internal static MBType ___003C_003EB_Bi_Bi_16x8;

	internal static MBType ___003C_003EB_Bi_Bi_8x16;

	internal static MBType ___003C_003EB_8x8;

	public bool intra;

	public int _code;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType I_NxN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EI_NxN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType I_16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EI_16x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType I_PCM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EI_PCM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType P_16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EP_16x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType P_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EP_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType P_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EP_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType P_8x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EP_8x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType P_8x8ref0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EP_8x8ref0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Direct_16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Direct_16x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_16x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_16x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_16x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_L0_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_L0_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_L0_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_L0_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_L1_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_L1_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_L1_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_L1_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_L1_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_L1_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_L1_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_L1_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_L0_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_L0_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_L0_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_L0_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_Bi_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_Bi_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L0_Bi_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L0_Bi_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_Bi_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_Bi_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_L1_Bi_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_L1_Bi_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_L0_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_L0_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_L0_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_L0_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_L1_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_L1_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_L1_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_L1_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_Bi_16x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_Bi_16x8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_Bi_Bi_8x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_Bi_Bi_8x16;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MBType B_8x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB_8x8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(53)]
	public virtual bool isIntra()
	{
		return intra;
	}

	[LineNumberTable(57)]
	public virtual int code()
	{
		return _code;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 161, 67, 105, 104, 104 })]
	private MBType(bool intra, int code)
	{
		this.intra = intra;
		_code = code;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 66, 109, 109, 110, 109, 109, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 109,
		110, 110, 110, 110, 110, 110, 110, 110, 110, 110,
		110, 110, 110
	})]
	static MBType()
	{
		___003C_003EI_NxN = new MBType(intra: true, 0);
		___003C_003EI_16x16 = new MBType(intra: true, 1);
		___003C_003EI_PCM = new MBType(intra: true, 25);
		___003C_003EP_16x16 = new MBType(intra: false, 0);
		___003C_003EP_16x8 = new MBType(intra: false, 1);
		___003C_003EP_8x16 = new MBType(intra: false, 2);
		___003C_003EP_8x8 = new MBType(intra: false, 3);
		___003C_003EP_8x8ref0 = new MBType(intra: false, 4);
		___003C_003EB_Direct_16x16 = new MBType(intra: false, 0);
		___003C_003EB_L0_16x16 = new MBType(intra: false, 1);
		___003C_003EB_L1_16x16 = new MBType(intra: false, 2);
		___003C_003EB_Bi_16x16 = new MBType(intra: false, 3);
		___003C_003EB_L0_L0_16x8 = new MBType(intra: false, 4);
		___003C_003EB_L0_L0_8x16 = new MBType(intra: false, 5);
		___003C_003EB_L1_L1_16x8 = new MBType(intra: false, 6);
		___003C_003EB_L1_L1_8x16 = new MBType(intra: false, 7);
		___003C_003EB_L0_L1_16x8 = new MBType(intra: false, 8);
		___003C_003EB_L0_L1_8x16 = new MBType(intra: false, 9);
		___003C_003EB_L1_L0_16x8 = new MBType(intra: false, 10);
		___003C_003EB_L1_L0_8x16 = new MBType(intra: false, 11);
		___003C_003EB_L0_Bi_16x8 = new MBType(intra: false, 12);
		___003C_003EB_L0_Bi_8x16 = new MBType(intra: false, 13);
		___003C_003EB_L1_Bi_16x8 = new MBType(intra: false, 14);
		___003C_003EB_L1_Bi_8x16 = new MBType(intra: false, 15);
		___003C_003EB_Bi_L0_16x8 = new MBType(intra: false, 16);
		___003C_003EB_Bi_L0_8x16 = new MBType(intra: false, 17);
		___003C_003EB_Bi_L1_16x8 = new MBType(intra: false, 18);
		___003C_003EB_Bi_L1_8x16 = new MBType(intra: false, 19);
		___003C_003EB_Bi_Bi_16x8 = new MBType(intra: false, 20);
		___003C_003EB_Bi_Bi_8x16 = new MBType(intra: false, 21);
		___003C_003EB_8x8 = new MBType(intra: false, 22);
	}
}
