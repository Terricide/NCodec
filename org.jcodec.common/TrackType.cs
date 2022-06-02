using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.common;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/common/TrackType;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class TrackType : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		VIDEO,
		AUDIO,
		TEXT,
		OTHER
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static TrackType ___003C_003EVIDEO;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static TrackType ___003C_003EAUDIO;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static TrackType ___003C_003ETEXT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static TrackType ___003C_003EOTHER;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static TrackType[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static TrackType VIDEO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static TrackType AUDIO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static TrackType TEXT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETEXT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static TrackType OTHER
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
	private TrackType(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static TrackType[] values()
	{
		return (TrackType[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static TrackType valueOf(string name)
	{
		return (TrackType)java.lang.Enum.valueOf(ClassLiteral<TrackType>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 140, 162, 63, 34 })]
	static TrackType()
	{
		___003C_003EVIDEO = new TrackType("VIDEO", 0);
		___003C_003EAUDIO = new TrackType("AUDIO", 1);
		___003C_003ETEXT = new TrackType("TEXT", 2);
		___003C_003EOTHER = new TrackType("OTHER", 3);
		_0024VALUES = new TrackType[4] { ___003C_003EVIDEO, ___003C_003EAUDIO, ___003C_003ETEXT, ___003C_003EOTHER };
	}
}
