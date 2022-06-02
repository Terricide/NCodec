using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class Format : Object
{
	internal static Format ___003C_003EMOV;

	internal static Format ___003C_003EMPEG_PS;

	internal static Format ___003C_003EMPEG_TS;

	internal static Format ___003C_003EMKV;

	internal static Format ___003C_003EH264;

	internal static Format ___003C_003ERAW;

	internal static Format ___003C_003EFLV;

	internal static Format ___003C_003EAVI;

	internal static Format ___003C_003EIMG;

	internal static Format ___003C_003EIVF;

	internal static Format ___003C_003EMJPEG;

	internal static Format ___003C_003EY4M;

	internal static Format ___003C_003EWAV;

	internal static Format ___003C_003EWEBP;

	internal static Format ___003C_003EMPEG_AUDIO;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/common/Format;>;")]
	private static Map _values;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool video;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool audio;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private string name;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format MOV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMOV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format MPEG_PS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG_PS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format MPEG_TS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG_TS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format MKV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMKV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format H264
	{
		[HideFromJava]
		get
		{
			return ___003C_003EH264;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format RAW
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERAW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format FLV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFLV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format AVI
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVI;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format IMG
	{
		[HideFromJava]
		get
		{
			return ___003C_003EIMG;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format IVF
	{
		[HideFromJava]
		get
		{
			return ___003C_003EIVF;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format MJPEG
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMJPEG;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format Y4M
	{
		[HideFromJava]
		get
		{
			return ___003C_003EY4M;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format WAV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EWAV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format WEBP
	{
		[HideFromJava]
		get
		{
			return ___003C_003EWEBP;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Format MPEG_AUDIO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG_AUDIO;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(66)]
	public virtual bool isVideo()
	{
		return video;
	}

	[LineNumberTable(62)]
	public virtual bool isAudio()
	{
		return audio;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(70)]
	public static Format valueOf(string s)
	{
		return (Format)_values.get(s);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 161, 69, 105, 104, 104, 104 })]
	private Format(string name, bool video, bool audio)
	{
		this.name = name;
		this.video = video;
		this.audio = audio;
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 66, 114, 114, 114, 114, 114, 114, 114,
		114, 114, 114, 114, 114, 114, 114, 146, 139, 118,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118, 118, 118
	})]
	static Format()
	{
		___003C_003EMOV = new Format("MOV", video: true, audio: true);
		___003C_003EMPEG_PS = new Format("MPEG_PS", video: true, audio: true);
		___003C_003EMPEG_TS = new Format("MPEG_TS", video: true, audio: true);
		___003C_003EMKV = new Format("MKV", video: true, audio: true);
		___003C_003EH264 = new Format("H264", video: true, audio: false);
		___003C_003ERAW = new Format("RAW", video: true, audio: true);
		___003C_003EFLV = new Format("FLV", video: true, audio: true);
		___003C_003EAVI = new Format("AVI", video: true, audio: true);
		___003C_003EIMG = new Format("IMG", video: true, audio: false);
		___003C_003EIVF = new Format("IVF", video: true, audio: false);
		___003C_003EMJPEG = new Format("MJPEG", video: true, audio: false);
		___003C_003EY4M = new Format("Y4M", video: true, audio: false);
		___003C_003EWAV = new Format("WAV", video: false, audio: true);
		___003C_003EWEBP = new Format("WEBP", video: true, audio: false);
		___003C_003EMPEG_AUDIO = new Format("MPEG_AUDIO", video: false, audio: true);
		_values = new LinkedHashMap();
		_values.put("MOV", ___003C_003EMOV);
		_values.put("MPEG_PS", ___003C_003EMPEG_PS);
		_values.put("MPEG_TS", ___003C_003EMPEG_TS);
		_values.put("MKV", ___003C_003EMKV);
		_values.put("H264", ___003C_003EH264);
		_values.put("RAW", ___003C_003ERAW);
		_values.put("FLV", ___003C_003EFLV);
		_values.put("AVI", ___003C_003EAVI);
		_values.put("IMG", ___003C_003EIMG);
		_values.put("IVF", ___003C_003EIVF);
		_values.put("MJPEG", ___003C_003EMJPEG);
		_values.put("Y4M", ___003C_003EY4M);
		_values.put("WAV", ___003C_003EWAV);
		_values.put("WEBP", ___003C_003EWEBP);
		_values.put("MPEG_AUDIO", ___003C_003EMPEG_AUDIO);
	}
}
