using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace net.sourceforge.jaad.aac.tools;

[Serializable]
[Signature("Ljava/lang/Enum<Lnet/sourceforge/jaad/aac/tools/MSMask;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class MSMask : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		TYPE_ALL_0,
		TYPE_USED,
		TYPE_ALL_1,
		TYPE_RESERVED
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static MSMask ___003C_003ETYPE_ALL_0;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static MSMask ___003C_003ETYPE_USED;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static MSMask ___003C_003ETYPE_ALL_1;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static MSMask ___003C_003ETYPE_RESERVED;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static MSMask[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static MSMask TYPE_ALL_0
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_ALL_0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static MSMask TYPE_USED
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_USED;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static MSMask TYPE_ALL_1
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_ALL_1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static MSMask TYPE_RESERVED
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_RESERVED;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public static MSMask[] values()
	{
		return (MSMask[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	private MSMask(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public static MSMask valueOf(string name)
	{
		return (MSMask)java.lang.Enum.valueOf(ClassLiteral<MSMask>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 139, 98, 63, 34 })]
	static MSMask()
	{
		___003C_003ETYPE_ALL_0 = new MSMask("TYPE_ALL_0", 0);
		___003C_003ETYPE_USED = new MSMask("TYPE_USED", 1);
		___003C_003ETYPE_ALL_1 = new MSMask("TYPE_ALL_1", 2);
		___003C_003ETYPE_RESERVED = new MSMask("TYPE_RESERVED", 3);
		_0024VALUES = new MSMask[4] { ___003C_003ETYPE_ALL_0, ___003C_003ETYPE_USED, ___003C_003ETYPE_ALL_1, ___003C_003ETYPE_RESERVED };
	}
}
