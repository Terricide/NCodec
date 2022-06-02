using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common;
using org.jcodec.containers.mxf.model;

namespace org.jcodec.containers.mxf;

public class MXFCodec : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private UL ul;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private Codec codec;

	internal static MXFCodec ___003C_003EMPEG2_XDCAM;

	internal static MXFCodec ___003C_003EMPEG2_ML;

	internal static MXFCodec ___003C_003EMPEG2_D10_PAL;

	internal static MXFCodec ___003C_003EMPEG2_HL;

	internal static MXFCodec ___003C_003EMPEG2_HL_422_I;

	internal static MXFCodec ___003C_003EMPEG4_XDCAM_PROXY;

	internal static MXFCodec ___003C_003EDV_25_PAL;

	internal static MXFCodec ___003C_003EJPEG2000;

	internal static MXFCodec ___003C_003EVC1;

	internal static MXFCodec ___003C_003ERAW;

	internal static MXFCodec ___003C_003ERAW_422;

	internal static MXFCodec ___003C_003EVC3_DNXHD;

	internal static MXFCodec ___003C_003EVC3_DNXHD_2;

	internal static MXFCodec ___003C_003EVC3_DNXHD_AVID;

	internal static MXFCodec ___003C_003EAVC_INTRA;

	internal static MXFCodec ___003C_003EAVC_SPSPPS;

	internal static MXFCodec ___003C_003EV210;

	internal static MXFCodec ___003C_003EPRORES_AVID;

	internal static MXFCodec ___003C_003EPRORES;

	internal static MXFCodec ___003C_003EPCM_S16LE_1;

	internal static MXFCodec ___003C_003EPCM_S16LE_3;

	internal static MXFCodec ___003C_003EPCM_S16LE_2;

	internal static MXFCodec ___003C_003EPCM_S16BE;

	internal static MXFCodec ___003C_003EPCM_ALAW;

	internal static MXFCodec ___003C_003EAC3;

	internal static MXFCodec ___003C_003EMP2;

	internal static MXFCodec ___003C_003EUNKNOWN;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MPEG2_XDCAM
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG2_XDCAM;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MPEG2_ML
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG2_ML;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MPEG2_D10_PAL
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG2_D10_PAL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MPEG2_HL
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG2_HL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MPEG2_HL_422_I
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG2_HL_422_I;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MPEG4_XDCAM_PROXY
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMPEG4_XDCAM_PROXY;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec DV_25_PAL
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDV_25_PAL;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec JPEG2000
	{
		[HideFromJava]
		get
		{
			return ___003C_003EJPEG2000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec VC1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVC1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec RAW
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERAW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec RAW_422
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERAW_422;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec VC3_DNXHD
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVC3_DNXHD;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec VC3_DNXHD_2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVC3_DNXHD_2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec VC3_DNXHD_AVID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVC3_DNXHD_AVID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec AVC_INTRA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVC_INTRA;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec AVC_SPSPPS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVC_SPSPPS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec V210
	{
		[HideFromJava]
		get
		{
			return ___003C_003EV210;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PRORES_AVID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRORES_AVID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PRORES
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRORES;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PCM_S16LE_1
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM_S16LE_1;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PCM_S16LE_3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM_S16LE_3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PCM_S16LE_2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM_S16LE_2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PCM_S16BE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM_S16BE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec PCM_ALAW
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPCM_ALAW;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec AC3
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAC3;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec MP2
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMP2;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MXFCodec UNKNOWN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EUNKNOWN;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 104 })]
	internal MXFCodec(UL ul, Codec codec)
	{
		this.ul = ul;
		this.codec = codec;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(18)]
	internal static MXFCodec mxfCodec(string ul, Codec codec)
	{
		MXFCodec result = new MXFCodec(UL.newUL(ul), codec);
		
		return result;
	}

	[LineNumberTable(27)]
	public virtual UL getUl()
	{
		return ul;
	}

	[LineNumberTable(31)]
	public virtual Codec getCodec()
	{
		return codec;
	}

	[LineNumberTable(71)]
	public static MXFCodec[] values()
	{
		return new MXFCodec[26]
		{
			___003C_003EMPEG2_XDCAM, ___003C_003EMPEG2_ML, ___003C_003EMPEG2_D10_PAL, ___003C_003EMPEG2_HL, ___003C_003EMPEG2_HL_422_I, ___003C_003EMPEG4_XDCAM_PROXY, ___003C_003EDV_25_PAL, ___003C_003EJPEG2000, ___003C_003EVC1, ___003C_003ERAW,
			___003C_003ERAW_422, ___003C_003EVC3_DNXHD, ___003C_003EVC3_DNXHD_2, ___003C_003EVC3_DNXHD_AVID, ___003C_003EAVC_INTRA, ___003C_003EAVC_SPSPPS, ___003C_003EV210, ___003C_003EPRORES_AVID, ___003C_003EPRORES, ___003C_003EPCM_S16LE_1,
			___003C_003EPCM_S16LE_3, ___003C_003EPCM_S16LE_2, ___003C_003EPCM_S16BE, ___003C_003EPCM_ALAW, ___003C_003EAC3, ___003C_003EMP2
		};
	}

	[LineNumberTable(new byte[]
	{
		159, 134, 130, 117, 117, 117, 117, 117, 117, 117,
		117, 117, 145, 145, 117, 149, 117, 181, 117, 181,
		149, 117, 113, 113, 113, 113, 117, 117, 117
	})]
	static MXFCodec()
	{
		___003C_003EMPEG2_XDCAM = mxfCodec("06.0E.2B.34.04.01.01.03.04.01.02.02.01.04.03", Codec.___003C_003EMPEG2);
		___003C_003EMPEG2_ML = mxfCodec("06.0E.2B.34.04.01.01.03.04.01.02.02.01.01.11", Codec.___003C_003EMPEG2);
		___003C_003EMPEG2_D10_PAL = mxfCodec("06.0E.2B.34.04.01.01.01.04.01.02.02.01.02.01.01", Codec.___003C_003EMPEG2);
		___003C_003EMPEG2_HL = mxfCodec("06.0E.2B.34.04.01.01.03.04.01.02.02.01.03.03", Codec.___003C_003EMPEG2);
		___003C_003EMPEG2_HL_422_I = mxfCodec("06.0E.2B.34.04.01.01.03.04.01.02.02.01.04.02", Codec.___003C_003EMPEG2);
		___003C_003EMPEG4_XDCAM_PROXY = mxfCodec("06.0E.2B.34.04.01.01.03.04.01.02.02.01.20.02.03", Codec.___003C_003EMPEG4);
		___003C_003EDV_25_PAL = mxfCodec("06.0E.2B.34.04.01.01.01.04.01.02.02.02.01.02", Codec.___003C_003EDV);
		___003C_003EJPEG2000 = mxfCodec("06.0E.2B.34.04.01.01.07.04.01.02.02.03.01.01", Codec.___003C_003EJ2K);
		___003C_003EVC1 = mxfCodec("06.0e.2b.34.04.01.01.0A.04.01.02.02.04", Codec.___003C_003EVC1);
		___003C_003ERAW = mxfCodec("06.0E.2B.34.04.01.01.01.04.01.02.01.7F", null);
		___003C_003ERAW_422 = mxfCodec("06.0E.2B.34.04.01.01.0A.04.01.02.01.01.02.01", null);
		___003C_003EVC3_DNXHD = mxfCodec("06.0E.2B.34.04.01.01.01.04.01.02.02.03.02", Codec.___003C_003EVC3);
		___003C_003EVC3_DNXHD_2 = mxfCodec("06.0E.2B.34.04.01.01.01.04.01.02.02.71", Codec.___003C_003EVC3);
		___003C_003EVC3_DNXHD_AVID = mxfCodec("06.0E.2B.34.04.01.01.01.0E.04.02.01.02.04.01", Codec.___003C_003EVC3);
		___003C_003EAVC_INTRA = mxfCodec("06.0E.2B.34.04.01.01.0A.04.01.02.02.01.32", Codec.___003C_003EH264);
		___003C_003EAVC_SPSPPS = mxfCodec("06.0E.2B.34.04.01.01.0A.04.01.02.02.01.31.11.01", Codec.___003C_003EH264);
		___003C_003EV210 = mxfCodec("06.0E.2B.34.04.01.01.0A.04.01.02.01.01.02.02", Codec.___003C_003EV210);
		___003C_003EPRORES_AVID = mxfCodec("06.0E.2B.34.04.01.01.01.0E.04.02.01.02.11", Codec.___003C_003EPRORES);
		___003C_003EPRORES = mxfCodec("06.0E.2B.34.04.01.01.0D.04.01.02.02.03.06", Codec.___003C_003EPRORES);
		___003C_003EPCM_S16LE_1 = mxfCodec("06.0E.2B.34.04.01.01.01.04.02.02.01", null);
		___003C_003EPCM_S16LE_3 = mxfCodec("06.0E.2B.34.04.01.01.01.04.02.02.01.01", null);
		___003C_003EPCM_S16LE_2 = mxfCodec("06.0E.2B.34.04.01.01.01.04.02.02.01.7F", null);
		___003C_003EPCM_S16BE = mxfCodec("06.0E.2B.34.04.01.01.07.04.02.02.01.7E", null);
		___003C_003EPCM_ALAW = mxfCodec("06.0E.2B.34.04.01.01.04.04.02.02.02.03.01.01", Codec.___003C_003EALAW);
		___003C_003EAC3 = mxfCodec("06.0E.2B.34.04.01.01.01.04.02.02.02.03.02.01", Codec.___003C_003EAC3);
		___003C_003EMP2 = mxfCodec("06.0E.2B.34.04.01.01.01.04.02.02.02.03.02.05", Codec.___003C_003EMP3);
		UL.___003Cclinit_003E();
		___003C_003EUNKNOWN = new MXFCodec(new UL(new byte[0]), null);
	}
}
