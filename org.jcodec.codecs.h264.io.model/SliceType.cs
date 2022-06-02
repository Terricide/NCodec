using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class SliceType : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static SliceType[] _values;

	internal static SliceType ___003C_003EP;

	internal static SliceType ___003C_003EB;

	internal static SliceType ___003C_003EI;

	internal static SliceType ___003C_003ESP;

	internal static SliceType ___003C_003ESI;

	private string _name;

	private int _ordinal;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SliceType P
	{
		[HideFromJava]
		get
		{
			return ___003C_003EP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SliceType B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EB;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SliceType I
	{
		[HideFromJava]
		get
		{
			return ___003C_003EI;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SliceType SP
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SliceType SI
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESI;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(52)]
	public static SliceType fromValue(int j)
	{
		return values()[j];
	}

	[LineNumberTable(31)]
	public virtual bool isInter()
	{
		return (this != ___003C_003EI && this != ___003C_003ESI) ? true : false;
	}

	[LineNumberTable(27)]
	public virtual bool isIntra()
	{
		return (this == ___003C_003EI || this == ___003C_003ESI) ? true : false;
	}

	[LineNumberTable(35)]
	public static SliceType[] values()
	{
		return _values;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104, 104, 105 })]
	private SliceType(string name, int ordinal)
	{
		_name = name;
		_ordinal = ordinal;
		_values[ordinal] = this;
	}

	[LineNumberTable(39)]
	public virtual int ordinal()
	{
		return _ordinal;
	}

	[LineNumberTable(44)]
	public override string toString()
	{
		return _name;
	}

	[LineNumberTable(48)]
	public virtual string name()
	{
		return _name;
	}

	[LineNumberTable(new byte[] { 159, 140, 162, 108, 113, 113, 113, 113 })]
	static SliceType()
	{
		_values = new SliceType[5];
		___003C_003EP = new SliceType("P", 0);
		___003C_003EB = new SliceType("B", 1);
		___003C_003EI = new SliceType("I", 2);
		___003C_003ESP = new SliceType("SP", 3);
		___003C_003ESI = new SliceType("SI", 4);
	}
}
