using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class SoundMediaHeaderBox : FullBox
{
	private short balance;

	[LineNumberTable(18)]
	public static string fourcc()
	{
		return "smhd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 106 })]
	public SoundMediaHeaderBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	public static SoundMediaHeaderBox createSoundMediaHeaderBox()
	{
		
		SoundMediaHeaderBox result = new SoundMediaHeaderBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 104, 109, 104 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		balance = input.getShort();
		input.getShort();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 104, 110, 105 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putShort(balance);
		@out.putShort(0);
	}

	[LineNumberTable(43)]
	public override int estimateSize()
	{
		return 16;
	}

	[LineNumberTable(47)]
	public virtual short getBalance()
	{
		return balance;
	}
}
