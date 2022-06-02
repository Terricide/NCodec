using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;

namespace org.jcodec.containers.mp4.boxes;

public class DataInfoBox : NodeBox
{
	[LineNumberTable(15)]
	public static string fourcc()
	{
		return "dinf";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 106 })]
	public DataInfoBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public static DataInfoBox createDataInfoBox()
	{
		
		DataInfoBox result = new DataInfoBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(27)]
	public virtual DataRefBox getDref()
	{
		return (DataRefBox)NodeBox.findFirst(this, ClassLiteral<DataRefBox>.Value, "dref");
	}
}
