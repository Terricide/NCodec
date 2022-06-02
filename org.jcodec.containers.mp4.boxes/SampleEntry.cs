using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class SampleEntry : NodeBox
{
	protected internal short drefInd;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 106 })]
	public SampleEntry(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 104, 136, 109 })]
	public override void parse(ByteBuffer input)
	{
		input.getInt();
		input.getShort();
		drefInd = input.getShort();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 106 })]
	protected internal virtual void parseExtensions(ByteBuffer input)
	{
		base.parse(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 127, 7, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(new byte[6] { 0, 0, 0, 0, 0, 0 });
		@out.putShort(drefInd);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 106 })]
	protected internal virtual void writeExtensions(ByteBuffer @out)
	{
		base.doWrite(@out);
	}

	[LineNumberTable(43)]
	public virtual short getDrefInd()
	{
		return drefInd;
	}

	[LineNumberTable(new byte[] { 159, 131, 161, 67, 104 })]
	public virtual void setDrefInd(short ind)
	{
		int ind2 = (drefInd = ind);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 109 })]
	public virtual void setMediaType(string mediaType)
	{
		header = new Header(mediaType);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(56)]
	public override int estimateSize()
	{
		return 8 + base.estimateSize();
	}
}
