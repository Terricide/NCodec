using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.lang.reflect;
using java.util;
using java.util.regex;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.common.tools;

public class MainUtils : java.lang.Object
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/common/tools/MainUtils$ANSIColor;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class ANSIColor : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			BLACK,
			RED,
			GREEN,
			BROWN,
			BLUE,
			MAGENTA,
			CYAN,
			GREY
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003EBLACK;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003ERED;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003EGREEN;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003EBROWN;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003EBLUE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003EMAGENTA;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003ECYAN;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static ANSIColor ___003C_003EGREY;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static ANSIColor[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor BLACK
		{
			[HideFromJava]
			get
			{
				return ___003C_003EBLACK;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor RED
		{
			[HideFromJava]
			get
			{
				return ___003C_003ERED;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor GREEN
		{
			[HideFromJava]
			get
			{
				return ___003C_003EGREEN;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor BROWN
		{
			[HideFromJava]
			get
			{
				return ___003C_003EBROWN;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor BLUE
		{
			[HideFromJava]
			get
			{
				return ___003C_003EBLUE;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor MAGENTA
		{
			[HideFromJava]
			get
			{
				return ___003C_003EMAGENTA;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor CYAN
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECYAN;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static ANSIColor GREY
		{
			[HideFromJava]
			get
			{
				return ___003C_003EGREY;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(421)]
		private ANSIColor(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(421)]
		public static ANSIColor[] values()
		{
			return (ANSIColor[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(421)]
		public static ANSIColor valueOf(string name)
		{
			return (ANSIColor)java.lang.Enum.valueOf(ClassLiteral<ANSIColor>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 37, 130, 63, 98 })]
		static ANSIColor()
		{
			___003C_003EBLACK = new ANSIColor("BLACK", 0);
			___003C_003ERED = new ANSIColor("RED", 1);
			___003C_003EGREEN = new ANSIColor("GREEN", 2);
			___003C_003EBROWN = new ANSIColor("BROWN", 3);
			___003C_003EBLUE = new ANSIColor("BLUE", 4);
			___003C_003EMAGENTA = new ANSIColor("MAGENTA", 5);
			___003C_003ECYAN = new ANSIColor("CYAN", 6);
			___003C_003EGREY = new ANSIColor("GREY", 7);
			_0024VALUES = new ANSIColor[8] { ___003C_003EBLACK, ___003C_003ERED, ___003C_003EGREEN, ___003C_003EBROWN, ___003C_003EBLUE, ___003C_003EMAGENTA, ___003C_003ECYAN, ___003C_003EGREY };
		}
	}

	public class Cmd : java.lang.Object
	{
		[Signature("Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;")]
		public Map longFlags;

		[Signature("Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;")]
		public Map shortFlags;

		public string[] args;

		[Signature("[Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;")]
		private Map[] longArgFlags;

		[Signature("[Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;")]
		private Map[] shortArgFlags;

		[LineNumberTable(281)]
		public virtual int argsLength()
		{
			return args.Length;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(209)]
		public virtual java.lang.Boolean getBooleanFlagI(int arg, Flag flagName)
		{
			java.lang.Boolean booleanFlagInternal = getBooleanFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, java.lang.Boolean.valueOf(b: false));
			
			return booleanFlagInternal;
		}

		[LineNumberTable(277)]
		public virtual string getArg(int i)
		{
			return (i >= (nint)args.LongLength) ? null : args[i];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(241)]
		public virtual string getStringFlagI(int arg, Flag flagName)
		{
			string stringFlagInternal = getStringFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, null);
			
			return stringFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(189)]
		public virtual Integer getIntegerFlagID(int arg, Flag flagName, Integer defaultValue)
		{
			Integer integerFlagInternal = getIntegerFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue);
			
			return integerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(205)]
		public virtual java.lang.Boolean getBooleanFlagID(int arg, Flag flagName, java.lang.Boolean defaultValue)
		{
			java.lang.Boolean booleanFlagInternal = getBooleanFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue);
			
			return booleanFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(229)]
		public virtual string getStringFlagD(Flag flagName, string defaultValue)
		{
			string stringFlagInternal = getStringFlagInternal(longFlags, shortFlags, flagName, defaultValue);
			
			return stringFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 98, 105, 104, 104, 104, 105, 105 })]
		public Cmd(Map longFlags, Map shortFlags, string[] args, Map[] longArgFlags, Map[] shortArgFlags)
		{
			this.args = args;
			this.longFlags = longFlags;
			this.shortFlags = shortFlags;
			this.longArgFlags = longArgFlags;
			this.shortArgFlags = shortArgFlags;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/Long;)Ljava/lang/Long;")]
		[LineNumberTable(new byte[] { 159, 119, 162, 127, 15, 63, 13 })]
		private Long getLongFlagInternal(Map longFlags, Map shortFlags, Flag flag, Long defaultValue)
		{
			Long result;
			if (longFlags.containsKey(flag.getLongName()))
			{
				
				result = new Long((string)longFlags.get(flag.getLongName()));
			}
			else if (shortFlags.containsKey(flag.getShortName()))
			{
				
				result = new Long((string)shortFlags.get(flag.getShortName()));
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/Integer;)Ljava/lang/Integer;")]
		[LineNumberTable(new byte[] { 159, 117, 130, 127, 15, 63, 13 })]
		private Integer getIntegerFlagInternal(Map longFlags, Map shortFlags, Flag flag, Integer defaultValue)
		{
			Integer result;
			if (longFlags.containsKey(flag.getLongName()))
			{
				result = new Integer((string)longFlags.get(flag.getLongName()));
			}
			else if (shortFlags.containsKey(flag.getShortName()))
			{
				result = new Integer((string)shortFlags.get(flag.getShortName()));
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/Boolean;)Ljava/lang/Boolean;")]
		[LineNumberTable(new byte[]
		{
			159,
			115,
			98,
			118,
			127,
			0,
			116,
			byte.MaxValue,
			5,
			61
		})]
		private java.lang.Boolean getBooleanFlagInternal(Map longFlags, Map shortFlags, Flag flag, java.lang.Boolean defaultValue)
		{
			java.lang.Boolean result = java.lang.Boolean.valueOf(longFlags.containsKey(flag.getLongName()) ? ((!java.lang.String.instancehelper_equalsIgnoreCase("false", (string)longFlags.get(flag.getLongName()))) ? true : false) : ((!shortFlags.containsKey(flag.getShortName())) ? defaultValue.booleanValue() : ((!java.lang.String.instancehelper_equalsIgnoreCase("false", (string)shortFlags.get(flag.getShortName()))) ? true : false)));
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/Double;)Ljava/lang/Double;")]
		[LineNumberTable(new byte[] { 159, 113, 98, 127, 15, 63, 13 })]
		private java.lang.Double getDoubleFlagInternal(Map longFlags, Map shortFlags, Flag flag, java.lang.Double defaultValue)
		{
			java.lang.Double result;
			if (longFlags.containsKey(flag.getLongName()))
			{
				result = new java.lang.Double((string)longFlags.get(flag.getLongName()));
			}
			else if (shortFlags.containsKey(flag.getShortName()))
			{
				result = new java.lang.Double((string)shortFlags.get(flag.getShortName()));
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/String;)Ljava/lang/String;")]
		[LineNumberTable(new byte[] { 159, 111, 66, 127, 5, 63, 3 })]
		private string getStringFlagInternal(Map longFlags, Map shortFlags, Flag flag, string defaultValue)
		{
			return longFlags.containsKey(flag.getLongName()) ? ((string)longFlags.get(flag.getLongName())) : ((!shortFlags.containsKey(flag.getShortName())) ? defaultValue : ((string)shortFlags.get(flag.getShortName())));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;[I)[I")]
		[LineNumberTable(new byte[]
		{
			159, 109, 66, 111, 117, 111, 149, 100, 109, 105,
			104, 44, 135
		})]
		private int[] getMultiIntegerFlagInternal(Map longFlags, Map shortFlags, Flag flag, int[] defaultValue)
		{
			string flagValue;
			if (longFlags.containsKey(flag.getLongName()))
			{
				flagValue = (string)longFlags.get(flag.getLongName());
			}
			else
			{
				if (!shortFlags.containsKey(flag.getShortName()))
				{
					return defaultValue;
				}
				flagValue = (string)shortFlags.get(flag.getShortName());
			}
			string[] split = StringUtils.splitS(flagValue, ",");
			int[] result = new int[(nint)split.LongLength];
			for (int i = 0; i < (nint)split.LongLength; i++)
			{
				result[i] = Integer.parseInt(split[i]);
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("<T:Ljava/lang/Enum<TT;>;>(Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;Lorg/jcodec/common/tools/MainUtils$Flag;TT;Ljava/lang/Class<TT;>;)TT;")]
		[LineNumberTable(new byte[]
		{
			159, 105, 66, 111, 117, 111, 149, 132, 104, 105,
			125, 117, 100, 99
		})]
		private java.lang.Enum getEnumFlagInternal(Map longFlags, Map shortFlags, Flag flag, java.lang.Enum defaultValue, Class class1)
		{
			string flagValue;
			if (longFlags.containsKey(flag.getLongName()))
			{
				flagValue = (string)longFlags.get(flag.getLongName());
			}
			else
			{
				if (!shortFlags.containsKey(flag.getShortName()))
				{
					return defaultValue;
				}
				flagValue = (string)shortFlags.get(flag.getShortName());
			}
			string strVal = java.lang.String.instancehelper_toLowerCase(flagValue);
			EnumSet allOf = EnumSet.allOf(class1);
			Iterator iterator = allOf.iterator();
			while (iterator.hasNext())
			{
				java.lang.Enum val = (java.lang.Enum)iterator.next();
				if (java.lang.String.instancehelper_equals(java.lang.String.instancehelper_toLowerCase(val.name()), strVal))
				{
					return val;
				}
			}
			return null;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(165)]
		public virtual Long getLongFlagD(Flag flagName, Long defaultValue)
		{
			Long longFlagInternal = getLongFlagInternal(longFlags, shortFlags, flagName, defaultValue);
			
			return longFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(169)]
		public virtual Long getLongFlag(Flag flagName)
		{
			Long longFlagInternal = getLongFlagInternal(longFlags, shortFlags, flagName, null);
			
			return longFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(173)]
		public virtual Long getLongFlagID(int arg, Flag flagName, Long defaultValue)
		{
			Long longFlagInternal = getLongFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue);
			
			return longFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(177)]
		public virtual Long getLongFlagI(int arg, Flag flagName)
		{
			Long longFlagInternal = getLongFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, null);
			
			return longFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(181)]
		public virtual Integer getIntegerFlagD(Flag flagName, Integer defaultValue)
		{
			Integer integerFlagInternal = getIntegerFlagInternal(longFlags, shortFlags, flagName, defaultValue);
			
			return integerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(185)]
		public virtual Integer getIntegerFlag(Flag flagName)
		{
			Integer integerFlagInternal = getIntegerFlagInternal(longFlags, shortFlags, flagName, null);
			
			return integerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(193)]
		public virtual Integer getIntegerFlagI(int arg, Flag flagName)
		{
			Integer integerFlagInternal = getIntegerFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, null);
			
			return integerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(197)]
		public virtual java.lang.Boolean getBooleanFlagD(Flag flagName, java.lang.Boolean defaultValue)
		{
			java.lang.Boolean booleanFlagInternal = getBooleanFlagInternal(longFlags, shortFlags, flagName, defaultValue);
			
			return booleanFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(201)]
		public virtual java.lang.Boolean getBooleanFlag(Flag flagName)
		{
			java.lang.Boolean booleanFlagInternal = getBooleanFlagInternal(longFlags, shortFlags, flagName, java.lang.Boolean.valueOf(b: false));
			
			return booleanFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(213)]
		public virtual java.lang.Double getDoubleFlagD(Flag flagName, java.lang.Double defaultValue)
		{
			java.lang.Double doubleFlagInternal = getDoubleFlagInternal(longFlags, shortFlags, flagName, defaultValue);
			
			return doubleFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(217)]
		public virtual java.lang.Double getDoubleFlag(Flag flagName)
		{
			java.lang.Double doubleFlagInternal = getDoubleFlagInternal(longFlags, shortFlags, flagName, null);
			
			return doubleFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(221)]
		public virtual java.lang.Double getDoubleFlagID(int arg, Flag flagName, java.lang.Double defaultValue)
		{
			java.lang.Double doubleFlagInternal = getDoubleFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue);
			
			return doubleFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(225)]
		public virtual java.lang.Double getDoubleFlagI(int arg, Flag flagName)
		{
			java.lang.Double doubleFlagInternal = getDoubleFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, null);
			
			return doubleFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(233)]
		public virtual string getStringFlag(Flag flagName)
		{
			string stringFlagInternal = getStringFlagInternal(longFlags, shortFlags, flagName, null);
			
			return stringFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(237)]
		public virtual string getStringFlagID(int arg, Flag flagName, string defaultValue)
		{
			string stringFlagInternal = getStringFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue);
			
			return stringFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(245)]
		public virtual int[] getMultiIntegerFlagD(Flag flagName, int[] defaultValue)
		{
			int[] multiIntegerFlagInternal = getMultiIntegerFlagInternal(longFlags, shortFlags, flagName, defaultValue);
			
			return multiIntegerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(249)]
		public virtual int[] getMultiIntegerFlag(Flag flagName)
		{
			int[] multiIntegerFlagInternal = getMultiIntegerFlagInternal(longFlags, shortFlags, flagName, new int[0]);
			
			return multiIntegerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(253)]
		public virtual int[] getMultiIntegerFlagID(int arg, Flag flagName, int[] defaultValue)
		{
			int[] multiIntegerFlagInternal = getMultiIntegerFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue);
			
			return multiIntegerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(257)]
		public virtual int[] getMultiIntegerFlagI(int arg, Flag flagName)
		{
			int[] multiIntegerFlagInternal = getMultiIntegerFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, new int[0]);
			
			return multiIntegerFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("<T:Ljava/lang/Enum<TT;>;>(Lorg/jcodec/common/tools/MainUtils$Flag;TT;Ljava/lang/Class<TT;>;)TT;")]
		[LineNumberTable(261)]
		public virtual java.lang.Enum getEnumFlagD(Flag flagName, java.lang.Enum defaultValue, Class class1)
		{
			java.lang.Enum enumFlagInternal = getEnumFlagInternal(longFlags, shortFlags, flagName, defaultValue, class1);
			
			return enumFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("<T:Ljava/lang/Enum<TT;>;>(Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/Class<TT;>;)TT;")]
		[LineNumberTable(265)]
		public virtual java.lang.Enum getEnumFlag(Flag flagName, Class class1)
		{
			java.lang.Enum enumFlagInternal = getEnumFlagInternal(longFlags, shortFlags, flagName, null, class1);
			
			return enumFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("<T:Ljava/lang/Enum<TT;>;>(ILorg/jcodec/common/tools/MainUtils$Flag;TT;Ljava/lang/Class<TT;>;)TT;")]
		[LineNumberTable(269)]
		public virtual java.lang.Enum getEnumFlagID(int arg, Flag flagName, java.lang.Enum defaultValue, Class class1)
		{
			java.lang.Enum enumFlagInternal = getEnumFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, defaultValue, class1);
			
			return enumFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("<T:Ljava/lang/Enum<TT;>;>(ILorg/jcodec/common/tools/MainUtils$Flag;Ljava/lang/Class<TT;>;)TT;")]
		[LineNumberTable(273)]
		public virtual java.lang.Enum getEnumFlagI(int arg, Flag flagName, Class class1)
		{
			java.lang.Enum enumFlagInternal = getEnumFlagInternal(longArgFlags[arg], shortArgFlags[arg], flagName, null, class1);
			
			return enumFlagInternal;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 71, 98, 159, 0 })]
		public virtual void popArg()
		{
			args = (string[])Platform.copyOfRangeO(args, 1, args.Length);
		}
	}

	public class Flag : java.lang.Object
	{
		private string longName;

		private string shortName;

		private string description;

		private FlagType type;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 130, 98, 105, 104, 104, 104, 105 })]
		public Flag(string longName, string shortName, string description, FlagType type)
		{
			this.longName = longName;
			this.shortName = shortName;
			this.description = description;
			this.type = type;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(57)]
		public static Flag flag(string longName, string shortName, string description)
		{
			Flag result = new Flag(longName, shortName, description, FlagType.___003C_003EANY);
			
			return result;
		}

		[LineNumberTable(69)]
		public virtual string getShortName()
		{
			return shortName;
		}

		[LineNumberTable(73)]
		public virtual FlagType getType()
		{
			return type;
		}

		[LineNumberTable(61)]
		public virtual string getLongName()
		{
			return longName;
		}

		[LineNumberTable(65)]
		public virtual string getDescription()
		{
			return description;
		}
	}

	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/common/tools/MainUtils$FlagType;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class FlagType : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			VOID,
			STRING,
			INT,
			LONG,
			DOUBLE,
			MULT,
			ENUM,
			ANY
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003EVOID;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003ESTRING;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003EINT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003ELONG;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003EDOUBLE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003EMULT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003EENUM;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FlagType ___003C_003EANY;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static FlagType[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType VOID
		{
			[HideFromJava]
			get
			{
				return ___003C_003EVOID;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType STRING
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESTRING;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType INT
		{
			[HideFromJava]
			get
			{
				return ___003C_003EINT;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType LONG
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELONG;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType DOUBLE
		{
			[HideFromJava]
			get
			{
				return ___003C_003EDOUBLE;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType MULT
		{
			[HideFromJava]
			get
			{
				return ___003C_003EMULT;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType ENUM
		{
			[HideFromJava]
			get
			{
				return ___003C_003EENUM;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FlagType ANY
		{
			[HideFromJava]
			get
			{
				return ___003C_003EANY;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(39)]
		private FlagType(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(39)]
		public static FlagType[] values()
		{
			return (FlagType[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(39)]
		public static FlagType valueOf(string name)
		{
			return (FlagType)java.lang.Enum.valueOf(ClassLiteral<FlagType>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 132, 66, 63, 98 })]
		static FlagType()
		{
			___003C_003EVOID = new FlagType("VOID", 0);
			___003C_003ESTRING = new FlagType("STRING", 1);
			___003C_003EINT = new FlagType("INT", 2);
			___003C_003ELONG = new FlagType("LONG", 3);
			___003C_003EDOUBLE = new FlagType("DOUBLE", 4);
			___003C_003EMULT = new FlagType("MULT", 5);
			___003C_003EENUM = new FlagType("ENUM", 6);
			___003C_003EANY = new FlagType("ANY", 7);
			_0024VALUES = new FlagType[8] { ___003C_003EVOID, ___003C_003ESTRING, ___003C_003EINT, ___003C_003ELONG, ___003C_003EDOUBLE, ___003C_003EMULT, ___003C_003EENUM, ___003C_003EANY };
		}
	}

	private const string KEY_GIT_REVISION = "git.commit.id.abbrev";

	private const string JCODEC_LOG_SINK_COLOR = "jcodec.colorPrint";

	private const string GIT_PROPERTIES = "git.properties";

	public static bool isColorSupported;

	private static Pattern flagPattern;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 69, 98, 103, 103, 103, 103, 104, 104, 104,
		100, 106, 116, 127, 4, 106, 154, 151, 121, 109,
		100, 124, 113, 100, 111, 150, 239, 58, 236, 73,
		101, 103, 99, 104, 104, 109, 106, 106, 103, 231,
		35, 236, 97, 120, 127, 4, 31, 8
	})]
	public static Cmd parseArguments(string[] args, Flag[] flags)
	{
		HashMap longFlags = new HashMap();
		HashMap shortFlags = new HashMap();
		HashMap allLongFlags = new HashMap();
		HashMap allShortFlags = new HashMap();
		ArrayList outArgs = new ArrayList();
		ArrayList argLongFlags = new ArrayList();
		ArrayList argShortFlags = new ArrayList();
		CharSequence input = default(CharSequence);
		for (int arg = 0; arg < (nint)args.LongLength; arg++)
		{
			if (java.lang.String.instancehelper_startsWith(args[arg], "--"))
			{
				Pattern pattern = flagPattern;
				Matcher matcher = pattern.matcher(input);
				if (matcher.matches())
				{
					((Map)longFlags).put((object)matcher.group(1), (object)matcher.group(2));
				}
				else
				{
					((Map)longFlags).put((object)java.lang.String.instancehelper_substring(args[arg], 2), (object)"true");
				}
			}
			else if (java.lang.String.instancehelper_startsWith(args[arg], "-"))
			{
				string shortName = java.lang.String.instancehelper_substring(args[arg], 1);
				int found = 0;
				int num = flags.Length;
				for (int i = 0; i < num; i++)
				{
					Flag flag = flags[i];
					if (java.lang.String.instancehelper_equals(shortName, flag.getShortName()))
					{
						found = 1;
						if (flag.getType() != FlagType.___003C_003EVOID)
						{
							HashMap hashMap = shortFlags;
							arg++;
							((Map)hashMap).put((object)shortName, (object)args[arg]);
						}
						else
						{
							((Map)shortFlags).put((object)shortName, (object)"true");
						}
					}
				}
				if (found == 0)
				{
					arg++;
				}
			}
			else
			{
				((Map)allLongFlags).putAll((Map)longFlags);
				((Map)allShortFlags).putAll((Map)shortFlags);
				((List)outArgs).add((object)args[arg]);
				((List)argLongFlags).add((object)longFlags);
				((List)argShortFlags).add((object)shortFlags);
				longFlags = new HashMap();
				shortFlags = new HashMap();
			}
		}
		Cmd result = new Cmd(allLongFlags, allShortFlags, (string[])((List)outArgs).toArray((object[])new string[0]), (Map[])((List)argLongFlags).toArray((object[])(Map[])java.lang.reflect.Array.newInstance(java.lang.Object.instancehelper_getClass(longFlags), 0)), (Map[])((List)argShortFlags).toArray((object[])(Map[])java.lang.reflect.Array.newInstance(java.lang.Object.instancehelper_getClass(shortFlags), 0)));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 57, 66, 121 })]
	public static void printHelpArgs(Flag[] flags, string[] args)
	{
		printHelpOut(java.lang.System.@out, "", flags, Arrays.asList(args));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 55, 66, 126 })]
	public static void printHelpNoFlags(params string[] arguments)
	{
		printHelpOut(java.lang.System.@out, "", new Flag[0], Arrays.asList(arguments));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("([Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/util/List<Ljava/lang/String;>;)V")]
	[LineNumberTable(new byte[] { 159, 56, 66, 116 })]
	public static void printHelp(Flag[] flags, List @params)
	{
		printHelpOut(java.lang.System.@out, "", flags, @params);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(430)]
	public static string colorString(string str, string placeholder)
	{
		return (!isColorSupported) ? str : new StringBuilder().append("\u001b[").append(placeholder).append("m")
			.append(str)
			.append("\u001b[0m")
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(426)]
	public static string bold(string str)
	{
		return (!isColorSupported) ? str : new StringBuilder().append("\u001b[1m").append(str).append("\u001b[0m")
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/io/PrintStream;Ljava/lang/String;[Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/util/List<Ljava/lang/String;>;)V")]
	[LineNumberTable(new byte[]
	{
		159,
		48,
		66,
		113,
		103,
		108,
		104,
		106,
		159,
		94,
		135,
		127,
		7,
		103,
		103,
		124,
		109,
		109,
		109,
		127,
		34,
		159,
		24,
		109,
		106,
		109,
		141,
		127,
		34,
		127,
		24,
		106,
		109,
		173,
		109,
		byte.MaxValue,
		19,
		44,
		236,
		86,
		127,
		4,
		109,
		159,
		21,
		127,
		25,
		102,
		104,
		113,
		106
	})]
	public static void printHelpOut(PrintStream @out, string command, Flag[] flags, List @params)
	{
		string version = ClassLiteral<MainUtils>.Value.getPackage().getImplementationVersion();
		string gitRevision = getGitRevision();
		if (command == null || java.lang.String.instancehelper_isEmpty(command))
		{
			command = "jcodec";
		}
		if (gitRevision != null || version != null)
		{
			@out.println(new StringBuilder().append(command).append(bold(new StringBuilder().append((version == null) ? "" : new StringBuilder().append(" v.").append(version).toString()).append((gitRevision == null) ? "" : new StringBuilder().append(" rev. ").append(gitRevision).toString()).toString())).toString());
			@out.println();
		}
		@out.print(bold(new StringBuilder().append("Syntax: ").append(command).toString()));
		StringBuilder sample = new StringBuilder();
		StringBuilder detail = new StringBuilder();
		int num = flags.Length;
		for (int i = 0; i < num; i++)
		{
			Flag flag = flags[i];
			sample.append(" [");
			detail.append("\t");
			if (flag.getLongName() != null)
			{
				sample.append(bold(color(new StringBuilder().append("--").append(flag.getLongName()).append("=<value>")
					.toString(), ANSIColor.___003C_003EMAGENTA)));
				detail.append(bold(color(new StringBuilder().append("--").append(flag.getLongName()).toString(), ANSIColor.___003C_003EMAGENTA)));
			}
			if (flag.getShortName() != null)
			{
				if (flag.getLongName() != null)
				{
					sample.append(" (");
					detail.append(" (");
				}
				sample.append(bold(color(new StringBuilder().append("-").append(flag.getShortName()).append(" <value>")
					.toString(), ANSIColor.___003C_003EMAGENTA)));
				detail.append(bold(color(new StringBuilder().append("-").append(flag.getShortName()).toString(), ANSIColor.___003C_003EMAGENTA)));
				if (flag.getLongName() != null)
				{
					sample.append(")");
					detail.append(")");
				}
			}
			sample.append("]");
			detail.append(new StringBuilder().append("\t\t").append(flag.getDescription()).append("\n")
				.toString());
		}
		Iterator iterator = @params.iterator();
		while (iterator.hasNext())
		{
			string param = (string)iterator.next();
			if (java.lang.String.instancehelper_charAt(param, 0) != '?')
			{
				sample.append(bold(new StringBuilder().append(" <").append(param).append(">")
					.toString()));
			}
			else
			{
				sample.append(bold(new StringBuilder().append(" [").append(java.lang.String.instancehelper_substring(param, 1)).append("]")
					.toString()));
			}
		}
		@out.println(sample);
		@out.println(bold("Where:"));
		@out.println(detail);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 52, 66, 131, 118, 100, 242, 70, 82, 231,
		58, 99, 103, 104, 191, 1, 78, 231, 61, 99,
		131, 103, 99
	})]
	private static string getGitRevision()
	{
		InputStream @is = null;
		IOException ex;
		try
		{
			try
			{
				@is = Thread.currentThread().getContextClassLoader().getResourceAsStream("git.properties");
				if (@is == null)
				{
					
					goto IL_0042;
				}
			}
			catch (IOException x)
			{
				ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
				goto IL_003c;
			}
		}
		catch
		{
			//try-fault
			IOUtils.closeQuietly(@is);
			throw;
		}
		string result;
		IOException ex2;
		try
		{
			try
			{
				Properties properties = new Properties();
				properties.load(@is);
				result = (string)properties.get("git.commit.id.abbrev");
			}
			catch (IOException x2)
			{
				ex2 = ByteCodeHelper.MapException<IOException>(x2, ByteCodeHelper.MapFlags.NoRemapping);
				goto IL_0085;
			}
		}
		catch
		{
			//try-fault
			IOUtils.closeQuietly(@is);
			throw;
		}
		IOUtils.closeQuietly(@is);
		return result;
		IL_003c:
		IOException ex3 = ex;
		goto IL_0093;
		IL_0042:
		IOUtils.closeQuietly(@is);
		return null;
		IL_0085:
		ex3 = ex2;
		goto IL_0093;
		IL_0093:
		IOException ex4 = ex3;
		IOUtils.closeQuietly(@is);
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(434)]
	public static string color(string str, ANSIColor fg)
	{
		return (!isColorSupported) ? str : new StringBuilder().append("\u001b[").append(30 + (fg.ordinal() & 7)).append("m")
			.append(str)
			.append("\u001b[0m")
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(30)]
	public MainUtils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 66, 117 })]
	public static void printHelpCmdVa(string command, Flag[] flags, string args)
	{
		printHelpOut(java.lang.System.@out, command, flags, Collections.singletonList(args));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/String;[Lorg/jcodec/common/tools/MainUtils$Flag;Ljava/util/List<Ljava/lang/String;>;)V")]
	[LineNumberTable(new byte[] { 159, 53, 66, 112 })]
	public static void printHelpCmd(string command, Flag[] flags, List @params)
	{
		printHelpOut(java.lang.System.@out, command, flags, @params);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 33, 129, 67 })]
	public static string colorBright(string str, ANSIColor fg, bool bright)
	{
		return (!isColorSupported) ? str : new StringBuilder().append("\u001b[").append(30 + (fg.ordinal() & 7)).append(";")
			.append(bright ? 1 : 2)
			.append("m")
			.append(str)
			.append("\u001b[0m")
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 32, 162, 122, 63, 43 })]
	public static string color3(string str, ANSIColor fg, ANSIColor bg)
	{
		return (!isColorSupported) ? str : new StringBuilder().append("\u001b[").append(30 + (fg.ordinal() & 7)).append(";")
			.append(40 + (bg.ordinal() & 7))
			.append(";1m")
			.append(str)
			.append("\u001b[0m")
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 30, 97, 67 })]
	public static string color4(string str, ANSIColor fg, ANSIColor bg, bool bright)
	{
		return (!isColorSupported) ? str : new StringBuilder().append("\u001b[").append(30 + (fg.ordinal() & 7)).append(";")
			.append(40 + (bg.ordinal() & 7))
			.append(";")
			.append(bright ? 1 : 2)
			.append("m")
			.append(str)
			.append("\u001b[0m")
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 29, 130, 110, 152 })]
	public static File tildeExpand(string path)
	{
		if (java.lang.String.instancehelper_startsWith(path, "~"))
		{
			path = java.lang.String.instancehelper_replaceFirst(path, "~", java.lang.System.getProperty("user.home"));
		}
		File result = new File(path);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 133, 66, 109, 246, 160, 253 })]
	static MainUtils()
	{
		isColorSupported = ((java.lang.System.console() != null || java.lang.Boolean.parseBoolean(java.lang.System.getProperty("jcodec.colorPrint"))) ? true : false);
		flagPattern = Pattern.compile("^--([^=]+)=(.*)$");
	}
}
