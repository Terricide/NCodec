using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class ClipRegionBox : Box
{
	private short rgnSize;

	private short y;

	private short x;

	private short height;

	private short width;

	[LineNumberTable(21)]
	public static string fourcc()
	{
		return "crgn";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 106 })]
	public ClipRegionBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 97, 73, 119, 106, 105, 105, 105, 105 })]
	public static ClipRegionBox createClipRegionBox(short x, short y, short width, short height)
	{
		
		ClipRegionBox b = new ClipRegionBox(new Header(fourcc()));
		b.rgnSize = 10;
		b.x = x;
		b.y = y;
		b.width = width;
		b.height = height;
		return b;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 109, 109, 109, 109, 109 })]
	public override void parse(ByteBuffer input)
	{
		rgnSize = input.getShort();
		y = input.getShort();
		x = input.getShort();
		height = input.getShort();
		width = input.getShort();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 110, 110, 110, 110, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putShort(rgnSize);
		@out.putShort(y);
		@out.putShort(x);
		@out.putShort(height);
		@out.putShort(width);
	}

	[LineNumberTable(56)]
	public override int estimateSize()
	{
		return 18;
	}

	[LineNumberTable(60)]
	public virtual short getRgnSize()
	{
		return rgnSize;
	}

	[LineNumberTable(64)]
	public virtual short getY()
	{
		return y;
	}

	[LineNumberTable(68)]
	public virtual short getX()
	{
		return x;
	}

	[LineNumberTable(72)]
	public virtual short getHeight()
	{
		return height;
	}

	[LineNumberTable(76)]
	public virtual short getWidth()
	{
		return width;
	}
}
