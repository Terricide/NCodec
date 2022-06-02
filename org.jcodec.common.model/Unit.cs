using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.common.model;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/common/model/Unit;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class Unit : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		FRAME,
		SEC
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Unit ___003C_003EFRAME;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Unit ___003C_003ESEC;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static Unit[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Unit FRAME
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFRAME;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Unit SEC
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESEC;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	private Unit(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static Unit[] values()
	{
		return (Unit[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static Unit valueOf(string name)
	{
		return (Unit)java.lang.Enum.valueOf(ClassLiteral<Unit>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 140, 162, 63, 2 })]
	static Unit()
	{
		___003C_003EFRAME = new Unit("FRAME", 0);
		___003C_003ESEC = new Unit("SEC", 1);
		_0024VALUES = new Unit[2] { ___003C_003EFRAME, ___003C_003ESEC };
	}
}
