using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.mp4;

public class MP4TrackType : Object
{
	internal static MP4TrackType ___003C_003EVIDEO;

	internal static MP4TrackType ___003C_003ESOUND;

	internal static MP4TrackType ___003C_003ETIMECODE;

	internal static MP4TrackType ___003C_003EHINT;

	internal static MP4TrackType ___003C_003ETEXT;

	internal static MP4TrackType ___003C_003EHYPER_TEXT;

	internal static MP4TrackType ___003C_003ECC;

	internal static MP4TrackType ___003C_003ESUB;

	internal static MP4TrackType ___003C_003EMUSIC;

	internal static MP4TrackType ___003C_003EMPEG1;

	internal static MP4TrackType ___003C_003ESPRITE;

	internal static MP4TrackType ___003C_003ETWEEN;

	internal static MP4TrackType ___003C_003ECHAPTERS;

	internal static MP4TrackType ___003C_003ETHREE_D;

	internal static MP4TrackType ___003C_003ESTREAMING;

	internal static MP4TrackType ___003C_003EOBJECTS;

	internal static MP4TrackType ___003C_003EDATA;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MP4TrackType[] _values;

	private string handler;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType VIDEO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVIDEO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType SOUND
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESOUND;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType TIMECODE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETIMECODE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType HINT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHINT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType TEXT
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETEXT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType HYPER_TEXT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHYPER_TEXT;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType CC
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType SUB
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESUB;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType MUSIC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMUSIC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType MPEG1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType SPRITE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESPRITE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType TWEEN
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETWEEN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType CHAPTERS
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHAPTERS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType THREE_D
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETHREE_D;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType STREAMING
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESTREAMING;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType OBJECTS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EOBJECTS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MP4TrackType DATA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDATA;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 108, 105, 111, 227, 61, 231, 69 })]
	public static MP4TrackType fromHandler(string handler)
	{
		for (int i = 0; i < (nint)_values.LongLength; i++)
		{
			MP4TrackType val = _values[i];
			if (String.instancehelper_equals(val.getHandler(), handler))
			{
				return val;
			}
		}
		return null;
	}

	[LineNumberTable(42)]
	public virtual string getHandler()
	{
		return handler;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 105, 104 })]
	private MP4TrackType(string handler)
	{
		this.handler = handler;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 130, 112, 112, 112, 112, 112, 112, 112,
		112, 112, 112, 112, 112, 112, 112, 112, 112, 144
	})]
	static MP4TrackType()
	{
		___003C_003EVIDEO = new MP4TrackType("vide");
		___003C_003ESOUND = new MP4TrackType("soun");
		___003C_003ETIMECODE = new MP4TrackType("tmcd");
		___003C_003EHINT = new MP4TrackType("hint");
		___003C_003ETEXT = new MP4TrackType("text");
		___003C_003EHYPER_TEXT = new MP4TrackType("wtxt");
		___003C_003ECC = new MP4TrackType("clcp");
		___003C_003ESUB = new MP4TrackType("sbtl");
		___003C_003EMUSIC = new MP4TrackType("musi");
		___003C_003EMPEG1 = new MP4TrackType("MPEG");
		___003C_003ESPRITE = new MP4TrackType("sprt");
		___003C_003ETWEEN = new MP4TrackType("twen");
		___003C_003ECHAPTERS = new MP4TrackType("chap");
		___003C_003ETHREE_D = new MP4TrackType("qd3d");
		___003C_003ESTREAMING = new MP4TrackType("strm");
		___003C_003EOBJECTS = new MP4TrackType("obje");
		___003C_003EDATA = new MP4TrackType("url ");
		_values = new MP4TrackType[17]
		{
			___003C_003EVIDEO, ___003C_003ESOUND, ___003C_003ETIMECODE, ___003C_003EHINT, ___003C_003ETEXT, ___003C_003EHYPER_TEXT, ___003C_003ECC, ___003C_003ESUB, ___003C_003EMUSIC, ___003C_003EMPEG1,
			___003C_003ESPRITE, ___003C_003ETWEEN, ___003C_003ECHAPTERS, ___003C_003ETHREE_D, ___003C_003ESTREAMING, ___003C_003EOBJECTS, ___003C_003EDATA
		};
	}
}
