using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.containers.mp4.boxes;

public class MovieExtendsBox : NodeBox
{
	[LineNumberTable(16)]
	public static string fourcc()
	{
		return "mvex";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 66, 106 })]
	public MovieExtendsBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(20)]
	public static MovieExtendsBox createMovieExtendsBox()
	{
		
		MovieExtendsBox result = new MovieExtendsBox(new Header(fourcc()));
		
		return result;
	}
}
