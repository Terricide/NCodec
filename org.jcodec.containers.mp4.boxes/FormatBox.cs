using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class FormatBox : Box
{
	private string fmt;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 118, 104 })]
	public static FormatBox createFormatBox(string fmt)
	{
		
		FormatBox frma = new FormatBox(new Header(fourcc()));
		frma.fmt = fmt;
		return frma;
	}

	[LineNumberTable(24)]
	public static string fourcc()
	{
		return "frma";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 106 })]
	public FormatBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 110 })]
	public override void parse(ByteBuffer input)
	{
		fmt = NIOUtils.readString(input, 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 115 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(JCodecUtil2.asciiString(fmt));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(43)]
	public override int estimateSize()
	{
		return (int)((nint)JCodecUtil2.asciiString(fmt).LongLength + 8);
	}
}
