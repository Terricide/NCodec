using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;

namespace org.jcodec.containers.mp4.boxes;

public class MediaBox : NodeBox
{
	[LineNumberTable(13)]
	public static string fourcc()
	{
		return "mdia";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 106 })]
	public MediaBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public static MediaBox createMediaBox()
	{
		
		MediaBox result = new MediaBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(25)]
	public virtual MediaInfoBox getMinf()
	{
		return (MediaInfoBox)NodeBox.findFirst(this, ClassLiteral<MediaInfoBox>.Value, "minf");
	}
}
