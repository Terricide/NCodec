using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.common;

public class Fourcc : Object
{
	internal static int ___003C_003Eftyp;

	internal static int ___003C_003Efree;

	internal static int ___003C_003Emoov;

	internal static int ___003C_003Emdat;

	internal static int ___003C_003Ewide;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int ftyp
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eftyp;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int free
	{
		[HideFromJava]
		get
		{
			return ___003C_003Efree;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int moov
	{
		[HideFromJava]
		get
		{
			return ___003C_003Emoov;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int mdat
	{
		[HideFromJava]
		get
		{
			return ___003C_003Emdat;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int wide
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ewide;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 140, 65, 77 })]
	public static int makeInt(byte b3, byte b2, byte b1, byte b0)
	{
		int b7 = (sbyte)b3;
		int b6 = (sbyte)b2;
		int b5 = (sbyte)b1;
		int b4 = (sbyte)b0;
		return (b7 << 24) | ((b6 & 0xFF) << 16) | ((b5 & 0xFF) << 8) | (b4 & 0xFF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 104 })]
	public static int intFourcc(string @string)
	{
		byte[] b = Platform.getBytes(@string);
		int result = makeInt(b[0], b[1], b[2], b[3]);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(5)]
	public Fourcc()
	{
	}

	[LineNumberTable(new byte[] { 159, 138, 162, 112, 112, 112, 112 })]
	static Fourcc()
	{
		___003C_003Eftyp = intFourcc("ftyp");
		___003C_003Efree = intFourcc("free");
		___003C_003Emoov = intFourcc("moov");
		___003C_003Emdat = intFourcc("mdat");
		___003C_003Ewide = intFourcc("wide");
	}
}
