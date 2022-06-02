using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.common.model;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/common/model/ChannelLabel;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class ChannelLabel : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		MONO,
		STEREO_LEFT,
		STEREO_RIGHT,
		LEFT_TOTAL,
		RIGHT_TOTAL,
		FRONT_LEFT,
		FRONT_RIGHT,
		CENTER,
		LFE,
		REAR_LEFT,
		REAR_RIGHT,
		FRONT_CENTER_LEFT,
		FRONT_CENTER_RIGHT,
		REAR_CENTER,
		SIDE_LEFT,
		SIDE_RIGHT
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EMONO;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ESTEREO_LEFT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ESTEREO_RIGHT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ELEFT_TOTAL;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ERIGHT_TOTAL;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EFRONT_LEFT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EFRONT_RIGHT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ECENTER;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ELFE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EREAR_LEFT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EREAR_RIGHT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EFRONT_CENTER_LEFT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EFRONT_CENTER_RIGHT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003EREAR_CENTER;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ESIDE_LEFT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelLabel ___003C_003ESIDE_RIGHT;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static ChannelLabel[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel MONO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMONO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel STEREO_LEFT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESTEREO_LEFT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel STEREO_RIGHT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESTEREO_RIGHT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel LEFT_TOTAL
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELEFT_TOTAL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel RIGHT_TOTAL
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERIGHT_TOTAL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel FRONT_LEFT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFRONT_LEFT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel FRONT_RIGHT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFRONT_RIGHT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel CENTER
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECENTER;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel LFE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELFE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel REAR_LEFT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EREAR_LEFT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel REAR_RIGHT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EREAR_RIGHT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel FRONT_CENTER_LEFT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFRONT_CENTER_LEFT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel FRONT_CENTER_RIGHT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFRONT_CENTER_RIGHT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel REAR_CENTER
	{
		[HideFromJava]
		get
		{
			return ___003C_003EREAR_CENTER;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel SIDE_LEFT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESIDE_LEFT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelLabel SIDE_RIGHT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESIDE_RIGHT;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	private ChannelLabel(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static ChannelLabel[] values()
	{
		return (ChannelLabel[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static ChannelLabel valueOf(string name)
	{
		return (ChannelLabel)java.lang.Enum.valueOf(ClassLiteral<ChannelLabel>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 140, 162, 63, 160, 169 })]
	static ChannelLabel()
	{
		___003C_003EMONO = new ChannelLabel("MONO", 0);
		___003C_003ESTEREO_LEFT = new ChannelLabel("STEREO_LEFT", 1);
		___003C_003ESTEREO_RIGHT = new ChannelLabel("STEREO_RIGHT", 2);
		___003C_003ELEFT_TOTAL = new ChannelLabel("LEFT_TOTAL", 3);
		___003C_003ERIGHT_TOTAL = new ChannelLabel("RIGHT_TOTAL", 4);
		___003C_003EFRONT_LEFT = new ChannelLabel("FRONT_LEFT", 5);
		___003C_003EFRONT_RIGHT = new ChannelLabel("FRONT_RIGHT", 6);
		___003C_003ECENTER = new ChannelLabel("CENTER", 7);
		___003C_003ELFE = new ChannelLabel("LFE", 8);
		___003C_003EREAR_LEFT = new ChannelLabel("REAR_LEFT", 9);
		___003C_003EREAR_RIGHT = new ChannelLabel("REAR_RIGHT", 10);
		___003C_003EFRONT_CENTER_LEFT = new ChannelLabel("FRONT_CENTER_LEFT", 11);
		___003C_003EFRONT_CENTER_RIGHT = new ChannelLabel("FRONT_CENTER_RIGHT", 12);
		___003C_003EREAR_CENTER = new ChannelLabel("REAR_CENTER", 13);
		___003C_003ESIDE_LEFT = new ChannelLabel("SIDE_LEFT", 14);
		___003C_003ESIDE_RIGHT = new ChannelLabel("SIDE_RIGHT", 15);
		_0024VALUES = new ChannelLabel[16]
		{
			___003C_003EMONO, ___003C_003ESTEREO_LEFT, ___003C_003ESTEREO_RIGHT, ___003C_003ELEFT_TOTAL, ___003C_003ERIGHT_TOTAL, ___003C_003EFRONT_LEFT, ___003C_003EFRONT_RIGHT, ___003C_003ECENTER, ___003C_003ELFE, ___003C_003EREAR_LEFT,
			___003C_003EREAR_RIGHT, ___003C_003EFRONT_CENTER_LEFT, ___003C_003EFRONT_CENTER_RIGHT, ___003C_003EREAR_CENTER, ___003C_003ESIDE_LEFT, ___003C_003ESIDE_RIGHT
		};
	}
}
