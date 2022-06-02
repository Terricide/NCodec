using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class Brand : Object
{
	internal static Brand ___003C_003EMOV;

	internal static Brand ___003C_003EMP4;

	private FileTypeBox ftyp;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Brand MOV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMOV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Brand MP4
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMP4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 116 })]
	private Brand(string majorBrand, int version, string[] compatible)
	{
		ftyp = FileTypeBox.createFileTypeBox(majorBrand, version, Arrays.asList(compatible));
	}

	[LineNumberTable(24)]
	public virtual FileTypeBox getFileTypeBox()
	{
		return ftyp;
	}

	[LineNumberTable(new byte[] { 159, 139, 130, 127, 4 })]
	static Brand()
	{
		___003C_003EMOV = new Brand("qt  ", 512, new string[1] { "qt  " });
		___003C_003EMP4 = new Brand("isom", 512, new string[4] { "isom", "iso2", "avc1", "mp41" });
	}
}
