using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using java.util.regex;

namespace org.jcodec.common.model;

public class Label : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Label;>;")]
	private static List _values;

	internal static Label ___003C_003EUnknown;

	internal static Label ___003C_003EUnused;

	internal static Label ___003C_003EUseCoordinates;

	internal static Label ___003C_003ELeft;

	internal static Label ___003C_003ERight;

	internal static Label ___003C_003ECenter;

	internal static Label ___003C_003ELFEScreen;

	internal static Label ___003C_003ELeftSurround;

	internal static Label ___003C_003ERightSurround;

	internal static Label ___003C_003ELeftCenter;

	internal static Label ___003C_003ERightCenter;

	internal static Label ___003C_003ECenterSurround;

	internal static Label ___003C_003ELeftSurroundDirect;

	internal static Label ___003C_003ERightSurroundDirect;

	internal static Label ___003C_003ETopCenterSurround;

	internal static Label ___003C_003EVerticalHeightLeft;

	internal static Label ___003C_003EVerticalHeightCenter;

	internal static Label ___003C_003EVerticalHeightRight;

	internal static Label ___003C_003ETopBackLeft;

	internal static Label ___003C_003ETopBackCenter;

	internal static Label ___003C_003ETopBackRight;

	internal static Label ___003C_003ERearSurroundLeft;

	internal static Label ___003C_003ERearSurroundRight;

	internal static Label ___003C_003ELeftWide;

	internal static Label ___003C_003ERightWide;

	internal static Label ___003C_003ELFE2;

	internal static Label ___003C_003ELeftTotal;

	internal static Label ___003C_003ERightTotal;

	internal static Label ___003C_003EHearingImpaired;

	internal static Label ___003C_003ENarration;

	internal static Label ___003C_003EMono;

	internal static Label ___003C_003EDialogCentricMix;

	internal static Label ___003C_003ECenterSurroundDirect;

	internal static Label ___003C_003EAmbisonic_W;

	internal static Label ___003C_003EAmbisonic_X;

	internal static Label ___003C_003EAmbisonic_Y;

	internal static Label ___003C_003EAmbisonic_Z;

	internal static Label ___003C_003EMS_Mid;

	internal static Label ___003C_003EMS_Side;

	internal static Label ___003C_003EXY_X;

	internal static Label ___003C_003EXY_Y;

	internal static Label ___003C_003EHeadphonesLeft;

	internal static Label ___003C_003EHeadphonesRight;

	internal static Label ___003C_003EClickTrack;

	internal static Label ___003C_003EForeignLanguage;

	internal static Label ___003C_003EDiscrete;

	internal static Label ___003C_003EDiscrete_0;

	internal static Label ___003C_003EDiscrete_1;

	internal static Label ___003C_003EDiscrete_2;

	internal static Label ___003C_003EDiscrete_3;

	internal static Label ___003C_003EDiscrete_4;

	internal static Label ___003C_003EDiscrete_5;

	internal static Label ___003C_003EDiscrete_6;

	internal static Label ___003C_003EDiscrete_7;

	internal static Label ___003C_003EDiscrete_8;

	internal static Label ___003C_003EDiscrete_9;

	internal static Label ___003C_003EDiscrete_10;

	internal static Label ___003C_003EDiscrete_11;

	internal static Label ___003C_003EDiscrete_12;

	internal static Label ___003C_003EDiscrete_13;

	internal static Label ___003C_003EDiscrete_14;

	internal static Label ___003C_003EDiscrete_15;

	internal static Label ___003C_003EDiscrete_65535;

	[Modifiers(Modifiers.Final)]
	internal int labelVal;

	internal long ___003C_003EbitmapVal;

	internal static Pattern ___003C_003EchannelMappingRegex;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Unknown
	{
		[HideFromJava]
		get
		{
			return ___003C_003EUnknown;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Unused
	{
		[HideFromJava]
		get
		{
			return ___003C_003EUnused;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label UseCoordinates
	{
		[HideFromJava]
		get
		{
			return ___003C_003EUseCoordinates;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Left
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELeft;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Right
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Center
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECenter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LFEScreen
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELFEScreen;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LeftSurround
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELeftSurround;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RightSurround
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERightSurround;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LeftCenter
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELeftCenter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RightCenter
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERightCenter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label CenterSurround
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECenterSurround;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LeftSurroundDirect
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELeftSurroundDirect;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RightSurroundDirect
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERightSurroundDirect;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label TopCenterSurround
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETopCenterSurround;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label VerticalHeightLeft
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVerticalHeightLeft;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label VerticalHeightCenter
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVerticalHeightCenter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label VerticalHeightRight
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVerticalHeightRight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label TopBackLeft
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETopBackLeft;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label TopBackCenter
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETopBackCenter;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label TopBackRight
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETopBackRight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RearSurroundLeft
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERearSurroundLeft;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RearSurroundRight
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERearSurroundRight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LeftWide
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELeftWide;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RightWide
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERightWide;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LFE2
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELFE2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label LeftTotal
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELeftTotal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label RightTotal
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERightTotal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label HearingImpaired
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHearingImpaired;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Narration
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENarration;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Mono
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMono;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label DialogCentricMix
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDialogCentricMix;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label CenterSurroundDirect
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECenterSurroundDirect;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Ambisonic_W
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAmbisonic_W;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Ambisonic_X
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAmbisonic_X;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Ambisonic_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAmbisonic_Y;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Ambisonic_Z
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAmbisonic_Z;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label MS_Mid
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMS_Mid;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label MS_Side
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMS_Side;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label XY_X
	{
		[HideFromJava]
		get
		{
			return ___003C_003EXY_X;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label XY_Y
	{
		[HideFromJava]
		get
		{
			return ___003C_003EXY_Y;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label HeadphonesLeft
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHeadphonesLeft;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label HeadphonesRight
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHeadphonesRight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label ClickTrack
	{
		[HideFromJava]
		get
		{
			return ___003C_003EClickTrack;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label ForeignLanguage
	{
		[HideFromJava]
		get
		{
			return ___003C_003EForeignLanguage;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_5
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_5;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_6
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_6;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_7
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_7;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_9
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_9;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_10
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_10;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_11
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_11;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_12
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_12;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_13
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_13;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_14
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_14;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_15
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_15;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label Discrete_65535
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDiscrete_65535;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public long bitmapVal
	{
		[HideFromJava]
		get
		{
			return ___003C_003EbitmapVal;
		}
		[HideFromJava]
		private set
		{
			___003C_003EbitmapVal = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Pattern channelMappingRegex
	{
		[HideFromJava]
		get
		{
			return ___003C_003EchannelMappingRegex;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(146)]
	public static Label[] values()
	{
		return (Label[])_values.toArray(new Label[0]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 162, 105, 104, 127, 13, 109 })]
	private Label(int val)
	{
		labelVal = val;
		___003C_003EbitmapVal = ((labelVal <= 18 && labelVal >= 1) ? ((long)(1 << labelVal - 1)) : 0L);
		_values.add(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 130, 103, 104, 101, 106, 227, 61, 231,
		69
	})]
	public static Label getByVal(int val)
	{
		Label[] values = Label.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			Label label = values[i];
			if (label.labelVal == val)
			{
				return label;
			}
		}
		return ___003C_003EMono;
	}

	[LineNumberTable(160)]
	public virtual int getVal()
	{
		return labelVal;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 162, 171, 172, 172, 141, 108, 108, 108,
		172, 172, 108, 108, 172, 173, 173, 109, 173, 173,
		173, 109, 109, 109, 141, 109, 109, 109, 109, 173,
		173, 109, 109, 109, 109, 173, 109, 112, 112, 112,
		176, 112, 176, 112, 144, 112, 112, 112, 176, 176,
		144, 144, 144, 144, 144, 144, 144, 144, 144, 144,
		144, 144, 144, 144, 144, 144, 208
	})]
	static Label()
	{
		_values = new ArrayList();
		___003C_003EUnknown = new Label(-1);
		___003C_003EUnused = new Label(0);
		___003C_003EUseCoordinates = new Label(100);
		___003C_003ELeft = new Label(1);
		___003C_003ERight = new Label(2);
		___003C_003ECenter = new Label(3);
		___003C_003ELFEScreen = new Label(4);
		___003C_003ELeftSurround = new Label(5);
		___003C_003ERightSurround = new Label(6);
		___003C_003ELeftCenter = new Label(7);
		___003C_003ERightCenter = new Label(8);
		___003C_003ECenterSurround = new Label(9);
		___003C_003ELeftSurroundDirect = new Label(10);
		___003C_003ERightSurroundDirect = new Label(11);
		___003C_003ETopCenterSurround = new Label(12);
		___003C_003EVerticalHeightLeft = new Label(13);
		___003C_003EVerticalHeightCenter = new Label(14);
		___003C_003EVerticalHeightRight = new Label(15);
		___003C_003ETopBackLeft = new Label(16);
		___003C_003ETopBackCenter = new Label(17);
		___003C_003ETopBackRight = new Label(18);
		___003C_003ERearSurroundLeft = new Label(33);
		___003C_003ERearSurroundRight = new Label(34);
		___003C_003ELeftWide = new Label(35);
		___003C_003ERightWide = new Label(36);
		___003C_003ELFE2 = new Label(37);
		___003C_003ELeftTotal = new Label(38);
		___003C_003ERightTotal = new Label(39);
		___003C_003EHearingImpaired = new Label(40);
		___003C_003ENarration = new Label(41);
		___003C_003EMono = new Label(42);
		___003C_003EDialogCentricMix = new Label(43);
		___003C_003ECenterSurroundDirect = new Label(44);
		___003C_003EAmbisonic_W = new Label(200);
		___003C_003EAmbisonic_X = new Label(201);
		___003C_003EAmbisonic_Y = new Label(202);
		___003C_003EAmbisonic_Z = new Label(203);
		___003C_003EMS_Mid = new Label(204);
		___003C_003EMS_Side = new Label(205);
		___003C_003EXY_X = new Label(206);
		___003C_003EXY_Y = new Label(207);
		___003C_003EHeadphonesLeft = new Label(301);
		___003C_003EHeadphonesRight = new Label(302);
		___003C_003EClickTrack = new Label(304);
		___003C_003EForeignLanguage = new Label(305);
		___003C_003EDiscrete = new Label(400);
		___003C_003EDiscrete_0 = new Label(65536);
		___003C_003EDiscrete_1 = new Label(65537);
		___003C_003EDiscrete_2 = new Label(65538);
		___003C_003EDiscrete_3 = new Label(65539);
		___003C_003EDiscrete_4 = new Label(65540);
		___003C_003EDiscrete_5 = new Label(65541);
		___003C_003EDiscrete_6 = new Label(65542);
		___003C_003EDiscrete_7 = new Label(65543);
		___003C_003EDiscrete_8 = new Label(65544);
		___003C_003EDiscrete_9 = new Label(65545);
		___003C_003EDiscrete_10 = new Label(65546);
		___003C_003EDiscrete_11 = new Label(65547);
		___003C_003EDiscrete_12 = new Label(65548);
		___003C_003EDiscrete_13 = new Label(65549);
		___003C_003EDiscrete_14 = new Label(65550);
		___003C_003EDiscrete_15 = new Label(65551);
		___003C_003EDiscrete_65535 = new Label(131071);
		___003C_003EchannelMappingRegex = Pattern.compile("[_\\ \\.][a-zA-Z]+$");
	}
}
