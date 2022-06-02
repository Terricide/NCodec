using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class Codec : Object
{
	internal static Codec ___003C_003EH264;

	internal static Codec ___003C_003EMPEG2;

	internal static Codec ___003C_003EMPEG4;

	internal static Codec ___003C_003EPRORES;

	internal static Codec ___003C_003EDV;

	internal static Codec ___003C_003EVC1;

	internal static Codec ___003C_003EVC3;

	internal static Codec ___003C_003EV210;

	internal static Codec ___003C_003ESORENSON;

	internal static Codec ___003C_003EFLASH_SCREEN_VIDEO;

	internal static Codec ___003C_003EFLASH_SCREEN_V2;

	internal static Codec ___003C_003EPNG;

	internal static Codec ___003C_003EJPEG;

	internal static Codec ___003C_003EJ2K;

	internal static Codec ___003C_003EVP6;

	internal static Codec ___003C_003EVP8;

	internal static Codec ___003C_003EVP9;

	internal static Codec ___003C_003EVORBIS;

	internal static Codec ___003C_003EAAC;

	internal static Codec ___003C_003EMP3;

	internal static Codec ___003C_003EMP2;

	internal static Codec ___003C_003EMP1;

	internal static Codec ___003C_003EAC3;

	internal static Codec ___003C_003EDTS;

	internal static Codec ___003C_003ETRUEHD;

	internal static Codec ___003C_003EPCM_DVD;

	internal static Codec ___003C_003EPCM;

	internal static Codec ___003C_003EADPCM;

	internal static Codec ___003C_003EALAW;

	internal static Codec ___003C_003ENELLYMOSER;

	internal static Codec ___003C_003EG711;

	internal static Codec ___003C_003ESPEEX;

	internal static Codec ___003C_003ERAW;

	internal static Codec ___003C_003ETIMECODE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/common/Codec;>;")]
	private static Map _values;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private string _name;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private TrackType type;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec H264
	{
		[HideFromJava]
		get
		{
			return ___003C_003EH264;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec MPEG2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec MPEG4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG4;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec PRORES
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRORES;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec DV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec VC1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVC1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec VC3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVC3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec V210
	{
		[HideFromJava]
		get
		{
			return ___003C_003EV210;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec SORENSON
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESORENSON;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec FLASH_SCREEN_VIDEO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFLASH_SCREEN_VIDEO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec FLASH_SCREEN_V2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFLASH_SCREEN_V2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec PNG
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPNG;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec JPEG
	{
		[HideFromJava]
		get
		{
			return ___003C_003EJPEG;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec J2K
	{
		[HideFromJava]
		get
		{
			return ___003C_003EJ2K;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec VP6
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVP6;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec VP8
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVP8;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec VP9
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVP9;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec VORBIS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVORBIS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec AAC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec MP3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMP3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec MP2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMP2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec MP1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMP1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec AC3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAC3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec DTS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDTS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec TRUEHD
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETRUEHD;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec PCM_DVD
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM_DVD;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec PCM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec ADPCM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EADPCM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec ALAW
	{
		[HideFromJava]
		get
		{
			return ___003C_003EALAW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec NELLYMOSER
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENELLYMOSER;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec G711
	{
		[HideFromJava]
		get
		{
			return ___003C_003EG711;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec SPEEX
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESPEEX;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec RAW
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERAW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codec TIMECODE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETIMECODE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(119)]
	public static Codec valueOf(string s)
	{
		return (Codec)_values.get(s);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 98, 105, 104, 104 })]
	public Codec(string name, TrackType type)
	{
		_name = name;
		this.type = type;
	}

	[LineNumberTable(99)]
	public virtual TrackType getType()
	{
		return type;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 117, 162, 110, 103, 123, 103, 127, 28, 104,
		103, 110, 103, 110, 135
	})]
	public static Codec codecByFourcc(string fourcc)
	{
		if (String.instancehelper_equals(fourcc, "avc1"))
		{
			return ___003C_003EH264;
		}
		if (String.instancehelper_equals(fourcc, "m1v1") || String.instancehelper_equals(fourcc, "m2v1"))
		{
			return ___003C_003EMPEG2;
		}
		if (String.instancehelper_equals(fourcc, "apco") || String.instancehelper_equals(fourcc, "apcs") || String.instancehelper_equals(fourcc, "apcn") || String.instancehelper_equals(fourcc, "apch") || String.instancehelper_equals(fourcc, "ap4h"))
		{
			return ___003C_003EPRORES;
		}
		if (String.instancehelper_equals(fourcc, "mp4a"))
		{
			return ___003C_003EAAC;
		}
		if (String.instancehelper_equals(fourcc, "jpeg"))
		{
			return ___003C_003EJPEG;
		}
		return null;
	}

	[LineNumberTable(124)]
	public override string toString()
	{
		return _name;
	}

	[LineNumberTable(128)]
	public virtual string name()
	{
		return _name;
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 98, 117, 117, 117, 117, 117, 117, 117,
		117, 117, 117, 117, 117, 117, 117, 117, 117, 117,
		117, 117, 117, 117, 117, 117, 117, 117, 117, 117,
		117, 117, 117, 117, 117, 113, 149, 139, 118, 118,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118
	})]
	static Codec()
	{
		___003C_003EH264 = new Codec("H264", TrackType.___003C_003EVIDEO);
		___003C_003EMPEG2 = new Codec("MPEG2", TrackType.___003C_003EVIDEO);
		___003C_003EMPEG4 = new Codec("MPEG4", TrackType.___003C_003EVIDEO);
		___003C_003EPRORES = new Codec("PRORES", TrackType.___003C_003EVIDEO);
		___003C_003EDV = new Codec("DV", TrackType.___003C_003EVIDEO);
		___003C_003EVC1 = new Codec("VC1", TrackType.___003C_003EVIDEO);
		___003C_003EVC3 = new Codec("VC3", TrackType.___003C_003EVIDEO);
		___003C_003EV210 = new Codec("V210", TrackType.___003C_003EVIDEO);
		___003C_003ESORENSON = new Codec("SORENSON", TrackType.___003C_003EVIDEO);
		___003C_003EFLASH_SCREEN_VIDEO = new Codec("FLASH_SCREEN_VIDEO", TrackType.___003C_003EVIDEO);
		___003C_003EFLASH_SCREEN_V2 = new Codec("FLASH_SCREEN_V2", TrackType.___003C_003EVIDEO);
		___003C_003EPNG = new Codec("PNG", TrackType.___003C_003EVIDEO);
		___003C_003EJPEG = new Codec("JPEG", TrackType.___003C_003EVIDEO);
		___003C_003EJ2K = new Codec("J2K", TrackType.___003C_003EVIDEO);
		___003C_003EVP6 = new Codec("VP6", TrackType.___003C_003EVIDEO);
		___003C_003EVP8 = new Codec("VP8", TrackType.___003C_003EVIDEO);
		___003C_003EVP9 = new Codec("VP9", TrackType.___003C_003EVIDEO);
		___003C_003EVORBIS = new Codec("VORBIS", TrackType.___003C_003EVIDEO);
		___003C_003EAAC = new Codec("AAC", TrackType.___003C_003EAUDIO);
		___003C_003EMP3 = new Codec("MP3", TrackType.___003C_003EAUDIO);
		___003C_003EMP2 = new Codec("MP2", TrackType.___003C_003EAUDIO);
		___003C_003EMP1 = new Codec("MP1", TrackType.___003C_003EAUDIO);
		___003C_003EAC3 = new Codec("AC3", TrackType.___003C_003EAUDIO);
		___003C_003EDTS = new Codec("DTS", TrackType.___003C_003EAUDIO);
		___003C_003ETRUEHD = new Codec("TRUEHD", TrackType.___003C_003EAUDIO);
		___003C_003EPCM_DVD = new Codec("PCM_DVD", TrackType.___003C_003EAUDIO);
		___003C_003EPCM = new Codec("PCM", TrackType.___003C_003EAUDIO);
		___003C_003EADPCM = new Codec("ADPCM", TrackType.___003C_003EAUDIO);
		___003C_003EALAW = new Codec("ALAW", TrackType.___003C_003EAUDIO);
		___003C_003ENELLYMOSER = new Codec("NELLYMOSER", TrackType.___003C_003EAUDIO);
		___003C_003EG711 = new Codec("G711", TrackType.___003C_003EAUDIO);
		___003C_003ESPEEX = new Codec("SPEEX", TrackType.___003C_003EAUDIO);
		___003C_003ERAW = new Codec("RAW", null);
		___003C_003ETIMECODE = new Codec("TIMECODE", TrackType.___003C_003EOTHER);
		_values = new LinkedHashMap();
		_values.put("H264", ___003C_003EH264);
		_values.put("MPEG2", ___003C_003EMPEG2);
		_values.put("MPEG4", ___003C_003EMPEG4);
		_values.put("PRORES", ___003C_003EPRORES);
		_values.put("DV", ___003C_003EDV);
		_values.put("VC1", ___003C_003EVC1);
		_values.put("VC3", ___003C_003EVC3);
		_values.put("V210", ___003C_003EV210);
		_values.put("SORENSON", ___003C_003ESORENSON);
		_values.put("FLASH_SCREEN_VIDEO", ___003C_003EFLASH_SCREEN_VIDEO);
		_values.put("FLASH_SCREEN_V2", ___003C_003EFLASH_SCREEN_V2);
		_values.put("PNG", ___003C_003EPNG);
		_values.put("JPEG", ___003C_003EJPEG);
		_values.put("J2K", ___003C_003EJ2K);
		_values.put("VP6", ___003C_003EVP6);
		_values.put("VP8", ___003C_003EVP8);
		_values.put("VP9", ___003C_003EVP9);
		_values.put("VORBIS", ___003C_003EVORBIS);
		_values.put("AAC", ___003C_003EAAC);
		_values.put("MP3", ___003C_003EMP3);
		_values.put("MP2", ___003C_003EMP2);
		_values.put("MP1", ___003C_003EMP1);
		_values.put("AC3", ___003C_003EAC3);
		_values.put("DTS", ___003C_003EDTS);
		_values.put("TRUEHD", ___003C_003ETRUEHD);
		_values.put("PCM_DVD", ___003C_003EPCM_DVD);
		_values.put("PCM", ___003C_003EPCM);
		_values.put("ADPCM", ___003C_003EADPCM);
		_values.put("ALAW", ___003C_003EALAW);
		_values.put("NELLYMOSER", ___003C_003ENELLYMOSER);
		_values.put("G711", ___003C_003EG711);
		_values.put("SPEEX", ___003C_003ESPEEX);
		_values.put("RAW", ___003C_003ERAW);
		_values.put("TIMECODE", ___003C_003ETIMECODE);
	}
}
