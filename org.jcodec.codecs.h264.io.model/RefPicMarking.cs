using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.codecs.h264.io.model;

public class RefPicMarking : java.lang.Object
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/h264/io/model/RefPicMarking$InstrType;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class InstrType : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			REMOVE_SHORT,
			REMOVE_LONG,
			CONVERT_INTO_LONG,
			TRUNK_LONG,
			CLEAR,
			MARK_LONG
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static InstrType ___003C_003EREMOVE_SHORT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static InstrType ___003C_003EREMOVE_LONG;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static InstrType ___003C_003ECONVERT_INTO_LONG;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static InstrType ___003C_003ETRUNK_LONG;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static InstrType ___003C_003ECLEAR;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static InstrType ___003C_003EMARK_LONG;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static InstrType[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static InstrType REMOVE_SHORT
		{
			[HideFromJava]
			get
			{
				return ___003C_003EREMOVE_SHORT;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static InstrType REMOVE_LONG
		{
			[HideFromJava]
			get
			{
				return ___003C_003EREMOVE_LONG;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static InstrType CONVERT_INTO_LONG
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECONVERT_INTO_LONG;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static InstrType TRUNK_LONG
		{
			[HideFromJava]
			get
			{
				return ___003C_003ETRUNK_LONG;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static InstrType CLEAR
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECLEAR;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static InstrType MARK_LONG
		{
			[HideFromJava]
			get
			{
				return ___003C_003EMARK_LONG;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(17)]
		public static InstrType[] values()
		{
			return (InstrType[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(17)]
		private InstrType(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(17)]
		public static InstrType valueOf(string name)
		{
			return (InstrType)java.lang.Enum.valueOf(ClassLiteral<InstrType>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 138, 130, 63, 66 })]
		static InstrType()
		{
			___003C_003EREMOVE_SHORT = new InstrType("REMOVE_SHORT", 0);
			___003C_003EREMOVE_LONG = new InstrType("REMOVE_LONG", 1);
			___003C_003ECONVERT_INTO_LONG = new InstrType("CONVERT_INTO_LONG", 2);
			___003C_003ETRUNK_LONG = new InstrType("TRUNK_LONG", 3);
			___003C_003ECLEAR = new InstrType("CLEAR", 4);
			___003C_003EMARK_LONG = new InstrType("MARK_LONG", 5);
			_0024VALUES = new InstrType[6] { ___003C_003EREMOVE_SHORT, ___003C_003EREMOVE_LONG, ___003C_003ECONVERT_INTO_LONG, ___003C_003ETRUNK_LONG, ___003C_003ECLEAR, ___003C_003EMARK_LONG };
		}
	}

	public class Instruction : java.lang.Object
	{
		private InstrType type;

		private int arg1;

		private int arg2;

		[LineNumberTable(33)]
		public virtual InstrType getType()
		{
			return type;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 130, 105, 104, 104, 104 })]
		public Instruction(InstrType type, int arg1, int arg2)
		{
			this.type = type;
			this.arg1 = arg1;
			this.arg2 = arg2;
		}

		[LineNumberTable(37)]
		public virtual int getArg1()
		{
			return arg1;
		}

		[LineNumberTable(41)]
		public virtual int getArg2()
		{
			return arg2;
		}
	}

	private Instruction[] instructions;

	[LineNumberTable(52)]
	public virtual Instruction[] getInstructions()
	{
		return instructions;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 105, 104 })]
	public RefPicMarking(Instruction[] instructions)
	{
		this.instructions = instructions;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public override string toString()
	{
		string result = Platform.toJSON(this);
		return result;
	}
}
