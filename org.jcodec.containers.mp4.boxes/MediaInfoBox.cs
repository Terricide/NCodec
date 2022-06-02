using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;

namespace org.jcodec.containers.mp4.boxes;

public class MediaInfoBox : NodeBox
{
	[LineNumberTable(15)]
	public static string fourcc()
	{
		return "minf";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 106 })]
	public MediaInfoBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public static MediaInfoBox createMediaInfoBox()
	{
		
		MediaInfoBox result = new MediaInfoBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(27)]
	public virtual DataInfoBox getDinf()
	{
		return (DataInfoBox)NodeBox.findFirst(this, ClassLiteral<DataInfoBox>.Value, "dinf");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(31)]
	public virtual NodeBox getStbl()
	{
		return (NodeBox)NodeBox.findFirst(this, ClassLiteral<NodeBox>.Value, "stbl");
	}
}
