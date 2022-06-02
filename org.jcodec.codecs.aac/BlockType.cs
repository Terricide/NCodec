using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.codecs.aac;

[Serializable]
[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/aac/BlockType;>;")]
[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
public sealed class BlockType : java.lang.Enum
{
	[Serializable]
	[HideFromJava]
	public enum __Enum
	{
		TYPE_SCE,
		TYPE_CPE,
		TYPE_CCE,
		TYPE_LFE,
		TYPE_DSE,
		TYPE_PCE,
		TYPE_FIL,
		TYPE_END
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_SCE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_CPE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_CCE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_LFE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_DSE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_PCE;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_FIL;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	internal static BlockType ___003C_003ETYPE_END;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
	private static BlockType[] _0024VALUES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_SCE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_SCE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_CPE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_CPE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_CCE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_CCE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_LFE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_LFE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_DSE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_DSE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_PCE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_PCE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_FIL
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_FIL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	public static BlockType TYPE_END
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETYPE_END;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public static BlockType[] values()
	{
		return (BlockType[])_0024VALUES.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	private BlockType(string str, int i)
		: base(str, i)
	{
		GC.KeepAlive(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public static BlockType valueOf(string name)
	{
		return (BlockType)java.lang.Enum.valueOf(ClassLiteral<BlockType>.Value, name);
	}

	[LineNumberTable(new byte[] { 159, 139, 66, 63, 98 })]
	static BlockType()
	{
		___003C_003ETYPE_SCE = new BlockType("TYPE_SCE", 0);
		___003C_003ETYPE_CPE = new BlockType("TYPE_CPE", 1);
		___003C_003ETYPE_CCE = new BlockType("TYPE_CCE", 2);
		___003C_003ETYPE_LFE = new BlockType("TYPE_LFE", 3);
		___003C_003ETYPE_DSE = new BlockType("TYPE_DSE", 4);
		___003C_003ETYPE_PCE = new BlockType("TYPE_PCE", 5);
		___003C_003ETYPE_FIL = new BlockType("TYPE_FIL", 6);
		___003C_003ETYPE_END = new BlockType("TYPE_END", 7);
		_0024VALUES = new BlockType[8] { ___003C_003ETYPE_SCE, ___003C_003ETYPE_CPE, ___003C_003ETYPE_CCE, ___003C_003ETYPE_LFE, ___003C_003ETYPE_DSE, ___003C_003ETYPE_PCE, ___003C_003ETYPE_FIL, ___003C_003ETYPE_END };
	}
}
