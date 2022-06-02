using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class DataRefBox : NodeBox
{
	[LineNumberTable(15)]
	public static string fourcc()
	{
		return "dref";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 106 })]
	public DataRefBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public static DataRefBox createDataRefBox()
	{
		
		DataRefBox result = new DataRefBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 104, 104, 106 })]
	public override void parse(ByteBuffer input)
	{
		input.getInt();
		input.getInt();
		base.parse(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 105, 115, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(0);
		@out.putInt(boxes.size());
		base.doWrite(@out);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(42)]
	public override int estimateSize()
	{
		return 8 + base.estimateSize();
	}
}
