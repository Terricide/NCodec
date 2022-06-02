using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class MovieFragmentHeaderBox : FullBox
{
	private int sequenceNumber;

	[LineNumberTable(24)]
	public static string fourcc()
	{
		return "mfhd";
	}

	[LineNumberTable(45)]
	public virtual int getSequenceNumber()
	{
		return sequenceNumber;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 106 })]
	public MovieFragmentHeaderBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 104, 109 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		sequenceNumber = input.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 104, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(sequenceNumber);
	}

	[LineNumberTable(41)]
	public override int estimateSize()
	{
		return 16;
	}

	[LineNumberTable(new byte[] { 159, 130, 98, 104 })]
	public virtual void setSequenceNumber(int sequenceNumber)
	{
		this.sequenceNumber = sequenceNumber;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(53)]
	public static MovieFragmentHeaderBox createMovieFragmentHeaderBox()
	{
		
		MovieFragmentHeaderBox result = new MovieFragmentHeaderBox(new Header(fourcc()));
		
		return result;
	}
}
