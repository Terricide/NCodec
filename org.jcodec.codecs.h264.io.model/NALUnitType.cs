using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class NALUnitType : Object
{
	internal static NALUnitType ___003C_003ENON_IDR_SLICE;

	internal static NALUnitType ___003C_003ESLICE_PART_A;

	internal static NALUnitType ___003C_003ESLICE_PART_B;

	internal static NALUnitType ___003C_003ESLICE_PART_C;

	internal static NALUnitType ___003C_003EIDR_SLICE;

	internal static NALUnitType ___003C_003ESEI;

	internal static NALUnitType ___003C_003ESPS;

	internal static NALUnitType ___003C_003EPPS;

	internal static NALUnitType ___003C_003EACC_UNIT_DELIM;

	internal static NALUnitType ___003C_003EEND_OF_SEQ;

	internal static NALUnitType ___003C_003EEND_OF_STREAM;

	internal static NALUnitType ___003C_003EFILLER_DATA;

	internal static NALUnitType ___003C_003ESEQ_PAR_SET_EXT;

	internal static NALUnitType ___003C_003EAUX_SLICE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static NALUnitType[] lut;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static NALUnitType[] _values;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int value;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private string displayName;

	private string _name;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType NON_IDR_SLICE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENON_IDR_SLICE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType SLICE_PART_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESLICE_PART_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType SLICE_PART_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESLICE_PART_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType SLICE_PART_C
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESLICE_PART_C;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType IDR_SLICE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EIDR_SLICE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType SEI
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESEI;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType SPS
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESPS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType PPS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPPS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType ACC_UNIT_DELIM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EACC_UNIT_DELIM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType END_OF_SEQ
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEND_OF_SEQ;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType END_OF_STREAM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEND_OF_STREAM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType FILLER_DATA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFILLER_DATA;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType SEQ_PAR_SET_EXT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESEQ_PAR_SET_EXT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static NALUnitType AUX_SLICE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUX_SLICE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(60)]
	public static NALUnitType fromValue(int value)
	{
		return (value >= (nint)lut.LongLength) ? null : lut[value];
	}

	[LineNumberTable(56)]
	public virtual int getValue()
	{
		return value;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 105, 104, 104, 104 })]
	private NALUnitType(int value, string name, string displayName)
	{
		this.value = value;
		_name = name;
		this.displayName = displayName;
	}

	[LineNumberTable(52)]
	public virtual string getName()
	{
		return displayName;
	}

	[LineNumberTable(65)]
	public override string toString()
	{
		return _name;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 98, 118, 118, 118, 118, 118, 118, 118,
		118, 119, 119, 119, 119, 119, 247, 70, 159, 99,
		112, 108, 105, 14, 199
	})]
	static NALUnitType()
	{
		___003C_003ENON_IDR_SLICE = new NALUnitType(1, "NON_IDR_SLICE", "non IDR slice");
		___003C_003ESLICE_PART_A = new NALUnitType(2, "SLICE_PART_A", "slice part a");
		___003C_003ESLICE_PART_B = new NALUnitType(3, "SLICE_PART_B", "slice part b");
		___003C_003ESLICE_PART_C = new NALUnitType(4, "SLICE_PART_C", "slice part c");
		___003C_003EIDR_SLICE = new NALUnitType(5, "IDR_SLICE", "idr slice");
		___003C_003ESEI = new NALUnitType(6, "SEI", "sei");
		___003C_003ESPS = new NALUnitType(7, "SPS", "sequence parameter set");
		___003C_003EPPS = new NALUnitType(8, "PPS", "picture parameter set");
		___003C_003EACC_UNIT_DELIM = new NALUnitType(9, "ACC_UNIT_DELIM", "access unit delimiter");
		___003C_003EEND_OF_SEQ = new NALUnitType(10, "END_OF_SEQ", "end of sequence");
		___003C_003EEND_OF_STREAM = new NALUnitType(11, "END_OF_STREAM", "end of stream");
		___003C_003EFILLER_DATA = new NALUnitType(12, "FILLER_DATA", "filler data");
		___003C_003ESEQ_PAR_SET_EXT = new NALUnitType(13, "SEQ_PAR_SET_EXT", "sequence parameter set extension");
		___003C_003EAUX_SLICE = new NALUnitType(19, "AUX_SLICE", "auxilary slice");
		_values = new NALUnitType[14]
		{
			___003C_003ENON_IDR_SLICE, ___003C_003ESLICE_PART_A, ___003C_003ESLICE_PART_B, ___003C_003ESLICE_PART_C, ___003C_003EIDR_SLICE, ___003C_003ESEI, ___003C_003ESPS, ___003C_003EPPS, ___003C_003EACC_UNIT_DELIM, ___003C_003EEND_OF_SEQ,
			___003C_003EEND_OF_STREAM, ___003C_003EFILLER_DATA, ___003C_003ESEQ_PAR_SET_EXT, ___003C_003EAUX_SLICE
		};
		lut = new NALUnitType[256];
		for (int i = 0; i < (nint)_values.LongLength; i++)
		{
			NALUnitType nalUnitType = _values[i];
			lut[nalUnitType.value] = nalUnitType;
		}
	}
}
