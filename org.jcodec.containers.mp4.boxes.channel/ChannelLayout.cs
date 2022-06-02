using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4.boxes.channel;

public class ChannelLayout : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/channel/ChannelLayout;>;")]
	private static List _values;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_UseChannelDescriptions;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_UseChannelBitmap;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Mono;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Stereo;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_StereoHeadphones;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MatrixStereo;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MidSide;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_XY;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Binaural;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Ambisonic_B_Format;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Quadraphonic;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Pentagonal;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Hexagonal;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Octagonal;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Cube;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_3_0_A;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_3_0_B;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_4_0_A;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_4_0_B;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_A;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_B;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_C;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_D;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_A;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_B;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_C;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_D;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_6_1_A;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_7_1_A;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_7_1_B;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_MPEG_7_1_C;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_Emagic_Default_7_1;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_SMPTE_DTV;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_ITU_2_1;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_ITU_2_2;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_DVD_4;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_DVD_5;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_DVD_6;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_DVD_10;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_DVD_11;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_DVD_18;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_AudioUnit_6_0;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_AudioUnit_7_0;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_AAC_6_0;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_AAC_6_1;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_AAC_7_0;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_AAC_Octagonal;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_TMH_10_2_std;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_TMH_10_2_full;

	internal static ChannelLayout ___003C_003EkCAFChannelLayoutTag_RESERVED_DO_NOT_USE;

	private int code;

	private Label[] labels;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_UseChannelDescriptions
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_UseChannelDescriptions;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_UseChannelBitmap
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_UseChannelBitmap;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Mono
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Mono;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Stereo
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Stereo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_StereoHeadphones
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_StereoHeadphones;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MatrixStereo
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MatrixStereo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MidSide
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MidSide;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_XY
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_XY;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Binaural
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Binaural;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Ambisonic_B_Format
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Ambisonic_B_Format;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Quadraphonic
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Quadraphonic;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Pentagonal
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Pentagonal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Hexagonal
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Hexagonal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Octagonal
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Octagonal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Cube
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Cube;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_3_0_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_3_0_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_3_0_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_3_0_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_4_0_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_4_0_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_4_0_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_4_0_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_0_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_0_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_0_C
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_C;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_0_D
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_0_D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_1_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_1_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_1_C
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_C;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_5_1_D
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_5_1_D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_6_1_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_6_1_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_7_1_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_7_1_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_7_1_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_7_1_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_MPEG_7_1_C
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_MPEG_7_1_C;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_Emagic_Default_7_1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_Emagic_Default_7_1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_SMPTE_DTV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_SMPTE_DTV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_ITU_2_1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_ITU_2_1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_ITU_2_2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_ITU_2_2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_DVD_4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_DVD_4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_DVD_5
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_DVD_5;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_DVD_6
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_DVD_6;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_DVD_10
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_DVD_10;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_DVD_11
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_DVD_11;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_DVD_18
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_DVD_18;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_AudioUnit_6_0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_AudioUnit_6_0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_AudioUnit_7_0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_AudioUnit_7_0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_AAC_6_0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_AAC_6_0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_AAC_6_1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_AAC_6_1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_AAC_7_0
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_AAC_7_0;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_AAC_Octagonal
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_AAC_Octagonal;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_TMH_10_2_std
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_TMH_10_2_std;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_TMH_10_2_full
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_TMH_10_2_full;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelLayout kCAFChannelLayoutTag_RESERVED_DO_NOT_USE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkCAFChannelLayoutTag_RESERVED_DO_NOT_USE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(161)]
	public virtual int getCode()
	{
		return code;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(169)]
	public static ChannelLayout[] values()
	{
		return (ChannelLayout[])_values.toArray(new ChannelLayout[0]);
	}

	[LineNumberTable(165)]
	public virtual Label[] getLabels()
	{
		return labels;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 130, 105, 104, 104, 109 })]
	private ChannelLayout(int code, Label[] labels)
	{
		this.code = code;
		this.labels = labels;
		_values.add(this);
	}

	[LineNumberTable(new byte[]
	{
		159, 131, 130, 139, 146, 150, 158, 159, 7, 159,
		7, 159, 7, 159, 7, 159, 7, 159, 7, 159,
		23, 159, 23, 159, 31, 159, 39, 159, 55, 159,
		55, 159, 15, 159, 15, 159, 23, 159, 23, 159,
		31, 159, 31, 159, 31, 159, 31, 159, 39, 159,
		39, 159, 39, 159, 39, 159, 47, 159, 55, 159,
		55, 159, 55, 159, 55, 159, 55, 159, 15, 159,
		23, 159, 15, 159, 23, 159, 31, 159, 23, 159,
		31, 159, 31, 159, 39, 159, 47, 159, 39, 159,
		47, 159, 47, 191, 55, 191, 127, 159, 31
	})]
	static ChannelLayout()
	{
		_values = new ArrayList();
		___003C_003EkCAFChannelLayoutTag_UseChannelDescriptions = new ChannelLayout(0, new Label[0]);
		___003C_003EkCAFChannelLayoutTag_UseChannelBitmap = new ChannelLayout(65536, new Label[0]);
		___003C_003EkCAFChannelLayoutTag_Mono = new ChannelLayout(6553601, new Label[1] { Label.___003C_003EMono });
		___003C_003EkCAFChannelLayoutTag_Stereo = new ChannelLayout(6619138, new Label[2]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight
		});
		___003C_003EkCAFChannelLayoutTag_StereoHeadphones = new ChannelLayout(6684674, new Label[2]
		{
			Label.___003C_003EHeadphonesLeft,
			Label.___003C_003EHeadphonesRight
		});
		___003C_003EkCAFChannelLayoutTag_MatrixStereo = new ChannelLayout(6750210, new Label[2]
		{
			Label.___003C_003ELeftTotal,
			Label.___003C_003ERightTotal
		});
		___003C_003EkCAFChannelLayoutTag_MidSide = new ChannelLayout(6815746, new Label[2]
		{
			Label.___003C_003EMS_Mid,
			Label.___003C_003EMS_Side
		});
		___003C_003EkCAFChannelLayoutTag_XY = new ChannelLayout(6881282, new Label[2]
		{
			Label.___003C_003EXY_X,
			Label.___003C_003EXY_Y
		});
		___003C_003EkCAFChannelLayoutTag_Binaural = new ChannelLayout(6946818, new Label[2]
		{
			Label.___003C_003EHeadphonesLeft,
			Label.___003C_003EHeadphonesRight
		});
		___003C_003EkCAFChannelLayoutTag_Ambisonic_B_Format = new ChannelLayout(7012356, new Label[4]
		{
			Label.___003C_003EAmbisonic_W,
			Label.___003C_003EAmbisonic_X,
			Label.___003C_003EAmbisonic_Y,
			Label.___003C_003EAmbisonic_Z
		});
		___003C_003EkCAFChannelLayoutTag_Quadraphonic = new ChannelLayout(7077892, new Label[4]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_Pentagonal = new ChannelLayout(7143429, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter
		});
		___003C_003EkCAFChannelLayoutTag_Hexagonal = new ChannelLayout(7208966, new Label[6]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_Octagonal = new ChannelLayout(7274504, new Label[8]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter,
			Label.___003C_003ECenterSurround,
			Label.___003C_003ELeftCenter,
			Label.___003C_003ERightCenter
		});
		___003C_003EkCAFChannelLayoutTag_Cube = new ChannelLayout(7340040, new Label[8]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ETopBackLeft,
			Label.___003C_003ETopBackRight,
			Label.___003C_003ETopBackCenter,
			Label.___003C_003ETopCenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_3_0_A = new ChannelLayout(7405571, new Label[3]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_3_0_B = new ChannelLayout(7471107, new Label[3]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_4_0_A = new ChannelLayout(7536644, new Label[4]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_4_0_B = new ChannelLayout(7602180, new Label[4]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_0_A = new ChannelLayout(7667717, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_0_B = new ChannelLayout(7733253, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_0_C = new ChannelLayout(7798789, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ECenter,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_0_D = new ChannelLayout(7864325, new Label[5]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_1_A = new ChannelLayout(7929862, new Label[6]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_1_B = new ChannelLayout(7995398, new Label[6]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_1_C = new ChannelLayout(8060934, new Label[6]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ECenter,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_5_1_D = new ChannelLayout(8126470, new Label[6]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_6_1_A = new ChannelLayout(8192007, new Label[7]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ERight
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_7_1_A = new ChannelLayout(8257544, new Label[8]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ELeftCenter,
			Label.___003C_003ERightCenter
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_7_1_B = new ChannelLayout(8323080, new Label[8]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeftCenter,
			Label.___003C_003ERightCenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_MPEG_7_1_C = new ChannelLayout(8388616, new Label[8]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ERearSurroundLeft,
			Label.___003C_003ERearSurroundRight
		});
		___003C_003EkCAFChannelLayoutTag_Emagic_Default_7_1 = new ChannelLayout(8454152, new Label[8]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftCenter,
			Label.___003C_003ERightCenter
		});
		___003C_003EkCAFChannelLayoutTag_SMPTE_DTV = new ChannelLayout(8519688, new Label[8]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ELeftTotal,
			Label.___003C_003ERightTotal
		});
		___003C_003EkCAFChannelLayoutTag_ITU_2_1 = new ChannelLayout(8585219, new Label[3]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_ITU_2_2 = new ChannelLayout(8650756, new Label[4]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_DVD_4 = new ChannelLayout(8716291, new Label[3]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_DVD_5 = new ChannelLayout(8781828, new Label[4]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_DVD_6 = new ChannelLayout(8847365, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround
		});
		___003C_003EkCAFChannelLayoutTag_DVD_10 = new ChannelLayout(8912900, new Label[4]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_DVD_11 = new ChannelLayout(8978437, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_DVD_18 = new ChannelLayout(9043973, new Label[5]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_AudioUnit_6_0 = new ChannelLayout(9109510, new Label[6]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_AudioUnit_7_0 = new ChannelLayout(9175047, new Label[7]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenter,
			Label.___003C_003ERearSurroundLeft,
			Label.___003C_003ERearSurroundRight
		});
		___003C_003EkCAFChannelLayoutTag_AAC_6_0 = new ChannelLayout(9240582, new Label[6]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_AAC_6_1 = new ChannelLayout(9306119, new Label[7]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ECenterSurround,
			Label.___003C_003ELFEScreen
		});
		___003C_003EkCAFChannelLayoutTag_AAC_7_0 = new ChannelLayout(9371655, new Label[7]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ERearSurroundLeft,
			Label.___003C_003ERearSurroundRight
		});
		___003C_003EkCAFChannelLayoutTag_AAC_Octagonal = new ChannelLayout(9437192, new Label[8]
		{
			Label.___003C_003ECenter,
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003ERearSurroundLeft,
			Label.___003C_003ERearSurroundRight,
			Label.___003C_003ECenterSurround
		});
		___003C_003EkCAFChannelLayoutTag_TMH_10_2_std = new ChannelLayout(9502736, new Label[16]
		{
			Label.___003C_003ELeft,
			Label.___003C_003ERight,
			Label.___003C_003ECenter,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003ELeftSurround,
			Label.___003C_003ERightSurround,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003ECenterSurround,
			Label.___003C_003ELFEScreen,
			Label.___003C_003ELFE2
		});
		___003C_003EkCAFChannelLayoutTag_TMH_10_2_full = new ChannelLayout(9568277, new Label[5]
		{
			Label.___003C_003ELeftCenter,
			Label.___003C_003ERightCenter,
			Label.___003C_003EMono,
			Label.___003C_003EMono,
			Label.___003C_003EMono
		});
		___003C_003EkCAFChannelLayoutTag_RESERVED_DO_NOT_USE = new ChannelLayout(9633792, new Label[0]);
	}
}
