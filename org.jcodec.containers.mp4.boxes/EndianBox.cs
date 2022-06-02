using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class EndianBox : Box
{
	private ByteOrder endian;

	[LineNumberTable(18)]
	public static string fourcc()
	{
		return "enda";
	}

	[LineNumberTable(50)]
	public virtual ByteOrder getEndian()
	{
		return endian;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 118, 104 })]
	public static EndianBox createEndianBox(ByteOrder endian)
	{
		
		EndianBox endianBox = new EndianBox(new Header(fourcc()));
		endianBox.endian = endian;
		return endianBox;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public EndianBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 105, 102, 142, 140 })]
	public override void parse(ByteBuffer input)
	{
		long end = input.getShort();
		if (end == 1u)
		{
			endian = ByteOrder.LITTLE_ENDIAN;
		}
		else
		{
			endian = ByteOrder.BIG_ENDIAN;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 118 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putShort((endian == ByteOrder.LITTLE_ENDIAN) ? ((short)1) : ((short)0));
	}

	[LineNumberTable(46)]
	public override int estimateSize()
	{
		return 10;
	}

	[LineNumberTable(54)]
	protected internal virtual int calcSize()
	{
		return 2;
	}
}
