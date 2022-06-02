using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class TimecodeMediaInfoBox : FullBox
{
	private short font;

	private short face;

	private short size;

	private short[] color;

	private short[] bgcolor;

	private string name;

	[LineNumberTable(24)]
	public static string fourcc()
	{
		return "tcmi";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 106, 109, 109 })]
	public TimecodeMediaInfoBox(Header atom)
		: base(atom)
	{
		color = new short[3];
		bgcolor = new short[3];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 97, 71, 118, 104, 104, 104, 104, 105,
		105
	})]
	public static TimecodeMediaInfoBox createTimecodeMediaInfoBox(short font, short face, short size, short[] color, short[] bgcolor, string name)
	{
		
		TimecodeMediaInfoBox box = new TimecodeMediaInfoBox(new Header(fourcc()));
		box.font = font;
		box.face = face;
		box.size = size;
		box.color = color;
		box.bgcolor = bgcolor;
		box.name = name;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 104, 109, 109, 109, 104, 111, 111,
		111, 111, 111, 111, 109
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		font = input.getShort();
		face = input.getShort();
		size = input.getShort();
		input.getShort();
		color[0] = input.getShort();
		color[1] = input.getShort();
		color[2] = input.getShort();
		bgcolor[0] = input.getShort();
		bgcolor[1] = input.getShort();
		bgcolor[2] = input.getShort();
		name = NIOUtils.readPascalString(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 162, 104, 110, 110, 110, 105, 112, 112,
		112, 112, 112, 112, 111
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putShort(font);
		@out.putShort(face);
		@out.putShort(size);
		@out.putShort(0);
		@out.putShort(color[0]);
		@out.putShort(color[1]);
		@out.putShort(color[2]);
		@out.putShort(bgcolor[0]);
		@out.putShort(bgcolor[1]);
		@out.putShort(bgcolor[2]);
		NIOUtils.writePascalString(@out, name);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(79)]
	public override int estimateSize()
	{
		return (int)(33 + (nint)NIOUtils.asciiString(name).LongLength);
	}
}
