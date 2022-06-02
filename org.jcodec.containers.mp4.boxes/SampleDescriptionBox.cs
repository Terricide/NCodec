using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class SampleDescriptionBox : NodeBox
{
	[LineNumberTable(15)]
	public static string fourcc()
	{
		return "stsd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public SampleDescriptionBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 118, 104, 101, 14, 199 })]
	public static SampleDescriptionBox createSampleDescriptionBox(SampleEntry[] entries)
	{
		
		SampleDescriptionBox box = new SampleDescriptionBox(new Header(fourcc()));
		for (int i = 0; i < (nint)entries.LongLength; i++)
		{
			SampleEntry e = entries[i];
			box.boxes.add(e);
		}
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 104, 104, 106 })]
	public override void parse(ByteBuffer input)
	{
		input.getInt();
		input.getInt();
		base.parse(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 137, 121, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(0);
		@out.putInt(Math.max(1, boxes.size()));
		base.doWrite(@out);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(48)]
	public override int estimateSize()
	{
		return 8 + base.estimateSize();
	}
}
