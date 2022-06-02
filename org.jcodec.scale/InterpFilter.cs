using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.scale;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/scale/InterpFilter;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class InterpFilter : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		LANCZOS,
		BICUBIC
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static InterpFilter ___003C_003ELANCZOS;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static InterpFilter ___003C_003EBICUBIC;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static InterpFilter[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static InterpFilter LANCZOS
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELANCZOS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static InterpFilter BICUBIC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBICUBIC;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	private InterpFilter(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public static InterpFilter[] values()
	{
		return (InterpFilter[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public static InterpFilter valueOf(string name)
	{
		return (InterpFilter)java.lang.Enum.valueOf(ClassLiteral<InterpFilter>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 141, 66, 63, 2 })]
	static InterpFilter()
	{
		___003C_003ELANCZOS = new InterpFilter("LANCZOS", 0);
		___003C_003EBICUBIC = new InterpFilter("BICUBIC", 1);
		_0024VALUES = new InterpFilter[2] { ___003C_003ELANCZOS, ___003C_003EBICUBIC };
	}
}
