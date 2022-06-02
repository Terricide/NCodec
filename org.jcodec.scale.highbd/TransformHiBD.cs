using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public interface TransformHiBD
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/scale/highbd/TransformHiBD$Levels;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class Levels : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			STUDIO,
			PC
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Levels ___003C_003ESTUDIO;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Levels ___003C_003EPC;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static Levels[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Levels STUDIO
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESTUDIO;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Levels PC
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPC;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(14)]
		private Levels(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(14)]
		public static Levels[] values()
		{
			return (Levels[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(14)]
		public static Levels valueOf(string name)
		{
			return (Levels)java.lang.Enum.valueOf(ClassLiteral<Levels>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 139, 162, 63, 2 })]
		static Levels()
		{
			___003C_003ESTUDIO = new Levels("STUDIO", 0);
			___003C_003EPC = new Levels("PC", 1);
			_0024VALUES = new Levels[2] { ___003C_003ESTUDIO, ___003C_003EPC };
		}
	}

	void transform(PictureHiBD phbd1, PictureHiBD phbd2);
}
