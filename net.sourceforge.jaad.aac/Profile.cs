using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac;

public class Profile : Object
{
	internal static Profile ___003C_003EUNKNOWN;

	internal static Profile ___003C_003EAAC_MAIN;

	internal static Profile ___003C_003EAAC_LC;

	internal static Profile ___003C_003EAAC_SSR;

	internal static Profile ___003C_003EAAC_LTP;

	internal static Profile ___003C_003EAAC_SBR;

	internal static Profile ___003C_003EAAC_SCALABLE;

	internal static Profile ___003C_003ETWIN_VQ;

	internal static Profile ___003C_003EAAC_LD;

	internal static Profile ___003C_003EER_AAC_LC;

	internal static Profile ___003C_003EER_AAC_SSR;

	internal static Profile ___003C_003EER_AAC_LTP;

	internal static Profile ___003C_003EER_AAC_SCALABLE;

	internal static Profile ___003C_003EER_TWIN_VQ;

	internal static Profile ___003C_003EER_BSAC;

	internal static Profile ___003C_003EER_AAC_LD;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static Profile[] ALL;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int num;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private string descr;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool supported;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile UNKNOWN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EUNKNOWN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_MAIN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_MAIN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_LC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_LC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_SSR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_SSR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_LTP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_LTP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_SBR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_SBR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_SCALABLE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_SCALABLE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile TWIN_VQ
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETWIN_VQ;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile AAC_LD
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_LD;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_AAC_LC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_AAC_LC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_AAC_SSR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_AAC_SSR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_AAC_LTP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_AAC_LTP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_AAC_SCALABLE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_AAC_SCALABLE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_TWIN_VQ
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_TWIN_VQ;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_BSAC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_BSAC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Profile ER_AAC_LD
	{
		[HideFromJava]
		get
		{
			return ___003C_003EER_AAC_LD;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(105)]
	public virtual bool isErrorResilientProfile()
	{
		return num > 16;
	}

	[LineNumberTable(66)]
	public virtual int getIndex()
	{
		return num;
	}

	[LineNumberTable(new byte[] { 159, 131, 130, 118, 107 })]
	public static Profile forInt(int i)
	{
		if (i <= 0 || i > (nint)ALL.LongLength)
		{
			return ___003C_003EUNKNOWN;
		}
		return ALL[i - 1];
	}

	[LineNumberTable(94)]
	public virtual bool isDecodingSupported()
	{
		return supported;
	}

	[LineNumberTable(74)]
	public virtual string getDescription()
	{
		return descr;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 129, 67, 105, 104, 104, 104 })]
	private Profile(int num, string descr, bool supported)
	{
		this.num = num;
		this.descr = descr;
		this.supported = supported;
	}

	[LineNumberTable(84)]
	public override string toString()
	{
		return descr;
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 66, 114, 114, 114, 114, 114, 114, 114,
		114, 115, 115, 115, 115, 115, 115, 115, 147
	})]
	static Profile()
	{
		___003C_003EUNKNOWN = new Profile(-1, "unknown", supported: false);
		___003C_003EAAC_MAIN = new Profile(1, "AAC Main Profile", supported: true);
		___003C_003EAAC_LC = new Profile(2, "AAC Low Complexity", supported: true);
		___003C_003EAAC_SSR = new Profile(3, "AAC Scalable Sample Rate", supported: false);
		___003C_003EAAC_LTP = new Profile(4, "AAC Long Term Prediction", supported: false);
		___003C_003EAAC_SBR = new Profile(5, "AAC SBR", supported: true);
		___003C_003EAAC_SCALABLE = new Profile(6, "Scalable AAC", supported: false);
		___003C_003ETWIN_VQ = new Profile(7, "TwinVQ", supported: false);
		___003C_003EAAC_LD = new Profile(11, "AAC Low Delay", supported: false);
		___003C_003EER_AAC_LC = new Profile(17, "Error Resilient AAC Low Complexity", supported: true);
		___003C_003EER_AAC_SSR = new Profile(18, "Error Resilient AAC SSR", supported: false);
		___003C_003EER_AAC_LTP = new Profile(19, "Error Resilient AAC Long Term Prediction", supported: false);
		___003C_003EER_AAC_SCALABLE = new Profile(20, "Error Resilient Scalable AAC", supported: false);
		___003C_003EER_TWIN_VQ = new Profile(21, "Error Resilient TwinVQ", supported: false);
		___003C_003EER_BSAC = new Profile(22, "Error Resilient BSAC", supported: false);
		___003C_003EER_AAC_LD = new Profile(23, "Error Resilient AAC Low Delay", supported: false);
		ALL = new Profile[23]
		{
			___003C_003EAAC_MAIN, ___003C_003EAAC_LC, ___003C_003EAAC_SSR, ___003C_003EAAC_LTP, ___003C_003EAAC_SBR, ___003C_003EAAC_SCALABLE, ___003C_003ETWIN_VQ, null, null, null,
			___003C_003EAAC_LD, null, null, null, null, null, ___003C_003EER_AAC_LC, ___003C_003EER_AAC_SSR, ___003C_003EER_AAC_LTP, ___003C_003EER_AAC_SCALABLE,
			___003C_003EER_TWIN_VQ, ___003C_003EER_BSAC, ___003C_003EER_AAC_LD
		};
	}
}
