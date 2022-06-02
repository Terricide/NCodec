using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.codecs.aac;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/aac/ChannelPosition;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class ChannelPosition : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		AAC_CHANNEL_FRONT,
		AAC_CHANNEL_SIDE,
		AAC_CHANNEL_BACK,
		AAC_CHANNEL_LFE,
		AAC_CHANNEL_CC
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelPosition ___003C_003EAAC_CHANNEL_FRONT;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelPosition ___003C_003EAAC_CHANNEL_SIDE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelPosition ___003C_003EAAC_CHANNEL_BACK;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelPosition ___003C_003EAAC_CHANNEL_LFE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static ChannelPosition ___003C_003EAAC_CHANNEL_CC;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static ChannelPosition[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelPosition AAC_CHANNEL_FRONT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_CHANNEL_FRONT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelPosition AAC_CHANNEL_SIDE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_CHANNEL_SIDE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelPosition AAC_CHANNEL_BACK
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_CHANNEL_BACK;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelPosition AAC_CHANNEL_LFE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_CHANNEL_LFE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static ChannelPosition AAC_CHANNEL_CC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_CHANNEL_CC;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static ChannelPosition[] values()
	{
		return (ChannelPosition[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	private ChannelPosition(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public static ChannelPosition valueOf(string name)
	{
		return (ChannelPosition)java.lang.Enum.valueOf(ClassLiteral<ChannelPosition>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 140, 162, 63, 50 })]
	static ChannelPosition()
	{
		___003C_003EAAC_CHANNEL_FRONT = new ChannelPosition("AAC_CHANNEL_FRONT", 0);
		___003C_003EAAC_CHANNEL_SIDE = new ChannelPosition("AAC_CHANNEL_SIDE", 1);
		___003C_003EAAC_CHANNEL_BACK = new ChannelPosition("AAC_CHANNEL_BACK", 2);
		___003C_003EAAC_CHANNEL_LFE = new ChannelPosition("AAC_CHANNEL_LFE", 3);
		___003C_003EAAC_CHANNEL_CC = new ChannelPosition("AAC_CHANNEL_CC", 4);
		_0024VALUES = new ChannelPosition[5] { ___003C_003EAAC_CHANNEL_FRONT, ___003C_003EAAC_CHANNEL_SIDE, ___003C_003EAAC_CHANNEL_BACK, ___003C_003EAAC_CHANNEL_LFE, ___003C_003EAAC_CHANNEL_CC };
	}
}
