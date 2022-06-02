using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.codecs.aac;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/aac/Profile;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class Profile : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		MAIN,
		LC,
		OTHER
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Profile ___003C_003EMAIN;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Profile ___003C_003ELC;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Profile ___003C_003EOTHER;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static Profile[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Profile MAIN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMAIN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Profile LC
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Profile OTHER
	{
		[HideFromJava]
		get
		{
			return ___003C_003EOTHER;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	private Profile(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static Profile[] values()
	{
		return (Profile[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static Profile valueOf(string name)
	{
		return (Profile)java.lang.Enum.valueOf(ClassLiteral<Profile>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 140, 162, 63, 18 })]
	static Profile()
	{
		___003C_003EMAIN = new Profile("MAIN", 0);
		___003C_003ELC = new Profile("LC", 1);
		___003C_003EOTHER = new Profile("OTHER", 2);
		_0024VALUES = new Profile[3] { ___003C_003EMAIN, ___003C_003ELC, ___003C_003EOTHER };
	}
}
