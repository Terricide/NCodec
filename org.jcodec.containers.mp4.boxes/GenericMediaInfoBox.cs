using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class GenericMediaInfoBox : FullBox
{
	private short graphicsMode;

	private short rOpColor;

	private short gOpColor;

	private short bOpColor;

	private short balance;

	[LineNumberTable(20)]
	public static string fourcc()
	{
		return "gmin";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public GenericMediaInfoBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public static GenericMediaInfoBox createGenericMediaInfoBox()
	{
		
		GenericMediaInfoBox result = new GenericMediaInfoBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 104, 109, 109, 109, 109, 109, 104 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		graphicsMode = input.getShort();
		rOpColor = input.getShort();
		gOpColor = input.getShort();
		bOpColor = input.getShort();
		balance = input.getShort();
		input.getShort();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 130, 104, 110, 110, 110, 110, 110, 105 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putShort(graphicsMode);
		@out.putShort(rOpColor);
		@out.putShort(gOpColor);
		@out.putShort(bOpColor);
		@out.putShort(balance);
		@out.putShort(0);
	}

	[LineNumberTable(53)]
	public override int estimateSize()
	{
		return 24;
	}
}
