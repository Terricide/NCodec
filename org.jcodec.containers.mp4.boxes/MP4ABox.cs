using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class MP4ABox : Box
{
	private int val;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 106 })]
	public MP4ABox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(val);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 109 })]
	public override void parse(ByteBuffer input)
	{
		val = input.getInt();
	}

	[LineNumberTable(30)]
	public override int estimateSize()
	{
		return 12;
	}
}
