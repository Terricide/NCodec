using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.common.logging;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/common/logging/LogLevel;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class LogLevel : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		DEBUG,
		INFO,
		WARN,
		ERROR
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static LogLevel ___003C_003EDEBUG;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static LogLevel ___003C_003EINFO;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static LogLevel ___003C_003EWARN;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static LogLevel ___003C_003EERROR;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static LogLevel[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static LogLevel DEBUG
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDEBUG;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static LogLevel INFO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EINFO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static LogLevel WARN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EWARN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static LogLevel ERROR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EERROR;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	private LogLevel(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public static LogLevel[] values()
	{
		return (LogLevel[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public static LogLevel valueOf(string name)
	{
		return (LogLevel)java.lang.Enum.valueOf(ClassLiteral<LogLevel>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 141, 66, 63, 34 })]
	static LogLevel()
	{
		___003C_003EDEBUG = new LogLevel("DEBUG", 0);
		___003C_003EINFO = new LogLevel("INFO", 1);
		___003C_003EWARN = new LogLevel("WARN", 2);
		___003C_003EERROR = new LogLevel("ERROR", 3);
		_0024VALUES = new LogLevel[4] { ___003C_003EDEBUG, ___003C_003EINFO, ___003C_003EWARN, ___003C_003EERROR };
	}
}
