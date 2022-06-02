using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class NameBox : Box
{
	private string name;

	[LineNumberTable(19)]
	public static string fourcc()
	{
		return "name";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 106 })]
	public NameBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 118, 104 })]
	public static NameBox createNameBox(string name)
	{
		
		NameBox box = new NameBox(new Header(fourcc()));
		box.name = name;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 109 })]
	public override void parse(ByteBuffer input)
	{
		name = NIOUtils.readNullTermString(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 115, 105 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(JCodecUtil2.asciiString(name));
		@out.putInt(0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(43)]
	public override int estimateSize()
	{
		return (int)(12 + (nint)JCodecUtil2.asciiString(name).LongLength);
	}

	[LineNumberTable(47)]
	public virtual string getName()
	{
		return name;
	}
}
