using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class MovieExtendsHeaderBox : FullBox
{
	private int fragmentDuration;

	[LineNumberTable(22)]
	public static string fourcc()
	{
		return "mehd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 106 })]
	public MovieExtendsHeaderBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 104, 109 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		fragmentDuration = input.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 104, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(fragmentDuration);
	}

	[LineNumberTable(39)]
	public override int estimateSize()
	{
		return 16;
	}

	[LineNumberTable(43)]
	public virtual int getFragmentDuration()
	{
		return fragmentDuration;
	}

	[LineNumberTable(new byte[] { 159, 131, 162, 104 })]
	public virtual void setFragmentDuration(int fragmentDuration)
	{
		this.fragmentDuration = fragmentDuration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(51)]
	public static MovieExtendsHeaderBox createMovieExtendsHeaderBox()
	{
		
		MovieExtendsHeaderBox result = new MovieExtendsHeaderBox(new Header(fourcc()));
		
		return result;
	}
}
