using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class ClearApertureBox : FullBox
{
	public const string CLEF = "clef";

	protected internal float width;

	protected internal float height;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 106 })]
	public ClearApertureBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 113, 105, 105 })]
	public static ClearApertureBox createClearApertureBox(int width, int height)
	{
		ClearApertureBox clef = new ClearApertureBox(new Header("clef"));
		clef.width = width;
		clef.height = height;
		return clef;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 104, 117, 117 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		width = (float)input.getInt() / 65536f;
		height = (float)input.getInt() / 65536f;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 104, 122, 122 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(ByteCodeHelper.f2i(width * 65536f));
		@out.putInt(ByteCodeHelper.f2i(height * 65536f));
	}

	[LineNumberTable(42)]
	public override int estimateSize()
	{
		return 20;
	}

	[LineNumberTable(46)]
	public virtual float getWidth()
	{
		return width;
	}

	[LineNumberTable(50)]
	public virtual float getHeight()
	{
		return height;
	}
}
