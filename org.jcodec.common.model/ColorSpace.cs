using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.model;

public class ColorSpace : Object
{
	public const int MAX_PLANES = 4;

	public int nComp;

	public int[] compPlane;

	public int[] compWidth;

	public int[] compHeight;

	public bool planar;

	private string _name;

	public int bitsPerPixel;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] _000;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] _011;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] _012;

	internal static ColorSpace ___003C_003EBGR;

	internal static ColorSpace ___003C_003ERGB;

	internal static ColorSpace ___003C_003EYUV420;

	internal static ColorSpace ___003C_003EYUV420J;

	internal static ColorSpace ___003C_003EYUV422;

	internal static ColorSpace ___003C_003EYUV422J;

	internal static ColorSpace ___003C_003EYUV444;

	internal static ColorSpace ___003C_003EYUV444J;

	internal static ColorSpace ___003C_003EYUV422_10;

	internal static ColorSpace ___003C_003EGREY;

	internal static ColorSpace ___003C_003EMONO;

	internal static ColorSpace ___003C_003EYUV444_10;

	internal static ColorSpace ___003C_003EANY;

	internal static ColorSpace ___003C_003EANY_PLANAR;

	internal static ColorSpace ___003C_003EANY_INTERLEAVED;

	internal static ColorSpace ___003C_003ESAME;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace BGR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBGR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace RGB
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERGB;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV420
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV420;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV420J
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV420J;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV422
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV422;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV422J
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV422J;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV444
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV444;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV444J
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV444J;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV422_10
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV422_10;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace GREY
	{
		[HideFromJava]
		get
		{
			return ___003C_003EGREY;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace MONO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMONO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace YUV444_10
	{
		[HideFromJava]
		get
		{
			return ___003C_003EYUV444_10;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace ANY
	{
		[HideFromJava]
		get
		{
			return ___003C_003EANY;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace ANY_PLANAR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EANY_PLANAR;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace ANY_INTERLEAVED
	{
		[HideFromJava]
		get
		{
			return ___003C_003EANY_INTERLEAVED;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ColorSpace SAME
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAME;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(52)]
	public virtual int getWidthMask()
	{
		return ((nComp > 1) ? compWidth[1] : 0) ^ -1;
	}

	[LineNumberTable(56)]
	public virtual int getHeightMask()
	{
		return ((nComp > 1) ? compHeight[1] : 0) ^ -1;
	}

	[LineNumberTable(new byte[] { 159, 125, 66, 101, 99, 113, 99, 159, 16, 99 })]
	public virtual bool matches(ColorSpace inputColor)
	{
		if (inputColor == this)
		{
			return true;
		}
		if (inputColor == ___003C_003EANY || this == ___003C_003EANY)
		{
			return true;
		}
		if ((inputColor == ___003C_003EANY_INTERLEAVED || this == ___003C_003EANY_INTERLEAVED || inputColor == ___003C_003EANY_PLANAR || this == ___003C_003EANY_PLANAR) && inputColor.planar == planar)
		{
			return true;
		}
		return false;
	}

	[LineNumberTable(new byte[] { 159, 131, 66, 99, 103, 51, 167 })]
	private static int calcBitsPerPixel(int nComp, int[] compWidth, int[] compHeight)
	{
		int bitsPerPixel = 0;
		for (int i = 0; i < nComp; i++)
		{
			bitsPerPixel += 8 >> compWidth[i] >> compHeight[i];
		}
		return bitsPerPixel;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 65, 68, 105, 104, 104, 104, 105, 105,
		104, 113
	})]
	private ColorSpace(string name, int nComp, int[] compPlane, int[] compWidth, int[] compHeight, bool planar)
	{
		_name = name;
		this.nComp = nComp;
		this.compPlane = compPlane;
		this.compWidth = compWidth;
		this.compHeight = compHeight;
		this.planar = planar;
		bitsPerPixel = calcBitsPerPixel(nComp, compWidth, compHeight);
	}

	[LineNumberTable(40)]
	public override string toString()
	{
		return _name;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 66, 117, 99 })]
	public virtual Size compSize(Size size, int comp)
	{
		if (compWidth[comp] == 0 && compHeight[comp] == 0)
		{
			return size;
		}
		Size result = new Size(size.getWidth() >> compWidth[comp], size.getHeight() >> compHeight[comp]);
		
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159,
		120,
		98,
		120,
		120,
		120,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		2,
		127,
		17,
		127,
		2,
		byte.MaxValue,
		2,
		69,
		245,
		69,
		245,
		69,
		245,
		69
	})]
	static ColorSpace()
	{
		_000 = new int[3] { 0, 0, 0 };
		_011 = new int[3] { 0, 1, 1 };
		_012 = new int[3] { 0, 1, 2 };
		___003C_003EBGR = new ColorSpace("BGR", 3, _000, _000, _000, planar: false);
		___003C_003ERGB = new ColorSpace("RGB", 3, _000, _000, _000, planar: false);
		___003C_003EYUV420 = new ColorSpace("YUV420", 3, _012, _011, _011, planar: true);
		___003C_003EYUV420J = new ColorSpace("YUV420J", 3, _012, _011, _011, planar: true);
		___003C_003EYUV422 = new ColorSpace("YUV422", 3, _012, _011, _000, planar: true);
		___003C_003EYUV422J = new ColorSpace("YUV422J", 3, _012, _011, _000, planar: true);
		___003C_003EYUV444 = new ColorSpace("YUV444", 3, _012, _000, _000, planar: true);
		___003C_003EYUV444J = new ColorSpace("YUV444J", 3, _012, _000, _000, planar: true);
		___003C_003EYUV422_10 = new ColorSpace("YUV422_10", 3, _012, _011, _000, planar: true);
		___003C_003EGREY = new ColorSpace("GREY", 1, new int[1] { 0 }, new int[1] { 0 }, new int[1] { 0 }, planar: true);
		___003C_003EMONO = new ColorSpace("MONO", 1, _000, _000, _000, planar: true);
		___003C_003EYUV444_10 = new ColorSpace("YUV444_10", 3, _012, _000, _000, planar: true);
		___003C_003EANY = new ColorSpace("ANY", 0, null, null, null, planar: true);
		___003C_003EANY_PLANAR = new ColorSpace("ANY_PLANAR", 0, null, null, null, planar: true);
		___003C_003EANY_INTERLEAVED = new ColorSpace("ANY_INTERLEAVED", 0, null, null, null, planar: false);
		___003C_003ESAME = new ColorSpace("SAME", 0, null, null, null, planar: false);
	}
}
