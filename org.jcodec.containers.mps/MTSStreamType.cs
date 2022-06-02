using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.containers.mps;

public class MTSStreamType : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mps/MTSStreamType;>;")]
	private static List _values;

	internal static MTSStreamType ___003C_003ERESERVED;

	internal static MTSStreamType ___003C_003EVIDEO_MPEG1;

	internal static MTSStreamType ___003C_003EVIDEO_MPEG2;

	internal static MTSStreamType ___003C_003EAUDIO_MPEG1;

	internal static MTSStreamType ___003C_003EAUDIO_MPEG2;

	internal static MTSStreamType ___003C_003EPRIVATE_SECTION;

	internal static MTSStreamType ___003C_003EPRIVATE_DATA;

	internal static MTSStreamType ___003C_003EMHEG;

	internal static MTSStreamType ___003C_003EDSM_CC;

	internal static MTSStreamType ___003C_003EATM_SYNC;

	internal static MTSStreamType ___003C_003EDSM_CC_A;

	internal static MTSStreamType ___003C_003EDSM_CC_B;

	internal static MTSStreamType ___003C_003EDSM_CC_C;

	internal static MTSStreamType ___003C_003EDSM_CC_D;

	internal static MTSStreamType ___003C_003EMPEG_AUX;

	internal static MTSStreamType ___003C_003EAUDIO_AAC_ADTS;

	internal static MTSStreamType ___003C_003EVIDEO_MPEG4;

	internal static MTSStreamType ___003C_003EAUDIO_AAC_LATM;

	internal static MTSStreamType ___003C_003EFLEXMUX_PES;

	internal static MTSStreamType ___003C_003EFLEXMUX_SEC;

	internal static MTSStreamType ___003C_003EDSM_CC_SDP;

	internal static MTSStreamType ___003C_003EMETA_PES;

	internal static MTSStreamType ___003C_003EMETA_SEC;

	internal static MTSStreamType ___003C_003EDSM_CC_DATA_CAROUSEL;

	internal static MTSStreamType ___003C_003EDSM_CC_OBJ_CAROUSEL;

	internal static MTSStreamType ___003C_003EDSM_CC_SDP1;

	internal static MTSStreamType ___003C_003EIPMP;

	internal static MTSStreamType ___003C_003EVIDEO_H264;

	internal static MTSStreamType ___003C_003EAUDIO_AAC_RAW;

	internal static MTSStreamType ___003C_003ESUBS;

	internal static MTSStreamType ___003C_003EAUX_3D;

	internal static MTSStreamType ___003C_003EVIDEO_AVC_SVC;

	internal static MTSStreamType ___003C_003EVIDEO_AVC_MVC;

	internal static MTSStreamType ___003C_003EVIDEO_J2K;

	internal static MTSStreamType ___003C_003EVIDEO_MPEG2_3D;

	internal static MTSStreamType ___003C_003EVIDEO_H264_3D;

	internal static MTSStreamType ___003C_003EVIDEO_CAVS;

	internal static MTSStreamType ___003C_003EIPMP_STREAM;

	internal static MTSStreamType ___003C_003EAUDIO_AC3;

	internal static MTSStreamType ___003C_003EAUDIO_DTS;

	private int tag;

	private bool video;

	private bool audio;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType RESERVED
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERESERVED;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_MPEG1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_MPEG1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_MPEG2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_MPEG2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_MPEG1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_MPEG1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_MPEG2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_MPEG2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType PRIVATE_SECTION
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRIVATE_SECTION;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType PRIVATE_DATA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRIVATE_DATA;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType MHEG
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMHEG;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType ATM_SYNC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EATM_SYNC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_A
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_A;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_B
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_B;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_C
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_C;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_D
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType MPEG_AUX
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG_AUX;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_AAC_ADTS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_AAC_ADTS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_MPEG4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_MPEG4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_AAC_LATM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_AAC_LATM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType FLEXMUX_PES
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFLEXMUX_PES;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType FLEXMUX_SEC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFLEXMUX_SEC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_SDP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_SDP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType META_PES
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMETA_PES;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType META_SEC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMETA_SEC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_DATA_CAROUSEL
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_DATA_CAROUSEL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_OBJ_CAROUSEL
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_OBJ_CAROUSEL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType DSM_CC_SDP1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDSM_CC_SDP1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType IPMP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EIPMP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_H264
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_H264;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_AAC_RAW
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_AAC_RAW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType SUBS
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESUBS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUX_3D
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUX_3D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_AVC_SVC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_AVC_SVC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_AVC_MVC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_AVC_MVC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_J2K
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_J2K;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_MPEG2_3D
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_MPEG2_3D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_H264_3D
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_H264_3D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType VIDEO_CAVS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO_CAVS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType IPMP_STREAM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EIPMP_STREAM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_AC3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_AC3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MTSStreamType AUDIO_DTS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAUDIO_DTS;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 103, 104, 101, 106, 227, 61, 231,
		69
	})]
	public static MTSStreamType fromTag(int streamTypeTag)
	{
		MTSStreamType[] values = MTSStreamType.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			MTSStreamType streamType = values[i];
			if (streamType.tag == streamTypeTag)
			{
				return streamType;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(61)]
	public static MTSStreamType[] values()
	{
		return (MTSStreamType[])_values.toArray(new MTSStreamType[0]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 97, 69, 105, 104, 104, 104, 109 })]
	private MTSStreamType(int tag, bool video, bool audio)
	{
		this.tag = tag;
		this.video = video;
		this.audio = audio;
		_values.add(this);
	}

	[LineNumberTable(75)]
	public virtual int getTag()
	{
		return tag;
	}

	[LineNumberTable(79)]
	public virtual bool isVideo()
	{
		return video;
	}

	[LineNumberTable(83)]
	public virtual bool isAudio()
	{
		return audio;
	}

	[LineNumberTable(new byte[]
	{
		159, 141, 130, 139, 110, 110, 110, 110, 110, 110,
		110, 110, 110, 111, 111, 111, 111, 111, 111, 111,
		111, 111, 111, 111, 111, 111, 111, 111, 111, 111,
		111, 111, 111, 111, 111, 111, 111, 111, 111, 111,
		111, 111, 114
	})]
	static MTSStreamType()
	{
		_values = new ArrayList();
		___003C_003ERESERVED = new MTSStreamType(0, video: false, audio: false);
		___003C_003EVIDEO_MPEG1 = new MTSStreamType(1, video: true, audio: false);
		___003C_003EVIDEO_MPEG2 = new MTSStreamType(2, video: true, audio: false);
		___003C_003EAUDIO_MPEG1 = new MTSStreamType(3, video: false, audio: true);
		___003C_003EAUDIO_MPEG2 = new MTSStreamType(4, video: false, audio: true);
		___003C_003EPRIVATE_SECTION = new MTSStreamType(5, video: false, audio: false);
		___003C_003EPRIVATE_DATA = new MTSStreamType(6, video: false, audio: false);
		___003C_003EMHEG = new MTSStreamType(7, video: false, audio: false);
		___003C_003EDSM_CC = new MTSStreamType(8, video: false, audio: false);
		___003C_003EATM_SYNC = new MTSStreamType(9, video: false, audio: false);
		___003C_003EDSM_CC_A = new MTSStreamType(10, video: false, audio: false);
		___003C_003EDSM_CC_B = new MTSStreamType(11, video: false, audio: false);
		___003C_003EDSM_CC_C = new MTSStreamType(12, video: false, audio: false);
		___003C_003EDSM_CC_D = new MTSStreamType(13, video: false, audio: false);
		___003C_003EMPEG_AUX = new MTSStreamType(14, video: false, audio: false);
		___003C_003EAUDIO_AAC_ADTS = new MTSStreamType(15, video: false, audio: true);
		___003C_003EVIDEO_MPEG4 = new MTSStreamType(16, video: true, audio: false);
		___003C_003EAUDIO_AAC_LATM = new MTSStreamType(17, video: false, audio: true);
		___003C_003EFLEXMUX_PES = new MTSStreamType(18, video: false, audio: false);
		___003C_003EFLEXMUX_SEC = new MTSStreamType(19, video: false, audio: false);
		___003C_003EDSM_CC_SDP = new MTSStreamType(20, video: false, audio: false);
		___003C_003EMETA_PES = new MTSStreamType(21, video: false, audio: false);
		___003C_003EMETA_SEC = new MTSStreamType(22, video: false, audio: false);
		___003C_003EDSM_CC_DATA_CAROUSEL = new MTSStreamType(23, video: false, audio: false);
		___003C_003EDSM_CC_OBJ_CAROUSEL = new MTSStreamType(24, video: false, audio: false);
		___003C_003EDSM_CC_SDP1 = new MTSStreamType(25, video: false, audio: false);
		___003C_003EIPMP = new MTSStreamType(26, video: false, audio: false);
		___003C_003EVIDEO_H264 = new MTSStreamType(27, video: true, audio: false);
		___003C_003EAUDIO_AAC_RAW = new MTSStreamType(28, video: false, audio: true);
		___003C_003ESUBS = new MTSStreamType(29, video: false, audio: false);
		___003C_003EAUX_3D = new MTSStreamType(30, video: false, audio: false);
		___003C_003EVIDEO_AVC_SVC = new MTSStreamType(31, video: true, audio: false);
		___003C_003EVIDEO_AVC_MVC = new MTSStreamType(32, video: true, audio: false);
		___003C_003EVIDEO_J2K = new MTSStreamType(33, video: true, audio: false);
		___003C_003EVIDEO_MPEG2_3D = new MTSStreamType(34, video: true, audio: false);
		___003C_003EVIDEO_H264_3D = new MTSStreamType(35, video: true, audio: false);
		___003C_003EVIDEO_CAVS = new MTSStreamType(66, video: false, audio: true);
		___003C_003EIPMP_STREAM = new MTSStreamType(127, video: false, audio: false);
		___003C_003EAUDIO_AC3 = new MTSStreamType(129, video: false, audio: true);
		___003C_003EAUDIO_DTS = new MTSStreamType(138, video: false, audio: true);
	}
}
