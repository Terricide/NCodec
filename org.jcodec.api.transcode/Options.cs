using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.api.transcode;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/api/transcode/Options;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class Options : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		PROFILE,
		INTERLACED,
		DOWNSCALE
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Options ___003C_003EPROFILE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Options ___003C_003EINTERLACED;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static Options ___003C_003EDOWNSCALE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static Options[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Options PROFILE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPROFILE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Options INTERLACED
	{
		[HideFromJava]
		get
		{
			return ___003C_003EINTERLACED;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static Options DOWNSCALE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDOWNSCALE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	private Options(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public static Options[] values()
	{
		return (Options[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public static Options valueOf(string name)
	{
		return (Options)java.lang.Enum.valueOf(ClassLiteral<Options>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 141, 66, 63, 18 })]
	static Options()
	{
		___003C_003EPROFILE = new Options("PROFILE", 0);
		___003C_003EINTERLACED = new Options("INTERLACED", 1);
		___003C_003EDOWNSCALE = new Options("DOWNSCALE", 2);
		_0024VALUES = new Options[3] { ___003C_003EPROFILE, ___003C_003EINTERLACED, ___003C_003EDOWNSCALE };
	}
}
