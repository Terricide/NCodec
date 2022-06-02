using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class UdtaMetaBox : MetaBox
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 106 })]
	public UdtaMetaBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(20)]
	public static UdtaMetaBox createUdtaMetaBox()
	{
		UdtaMetaBox result = new UdtaMetaBox(Header.createHeader(MetaBox.fourcc(), 0L));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 104, 106 })]
	public override void parse(ByteBuffer input)
	{
		input.getInt();
		base.parse(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 105, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(0);
		base.doWrite(@out);
	}
}
