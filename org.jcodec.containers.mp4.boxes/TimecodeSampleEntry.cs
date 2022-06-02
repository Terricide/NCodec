using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class TimecodeSampleEntry : SampleEntry
{
	private const string TMCD = "tmcd";

	public const int FLAG_DROPFRAME = 1;

	public const int FLAG_24HOURMAX = 2;

	public const int FLAG_NEGATIVETIMEOK = 4;

	public const int FLAG_COUNTER = 8;

	private int flags;

	private int timescale;

	private int frameDuration;

	private byte numFrames;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 106 })]
	public TimecodeSampleEntry(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 113, 104, 104, 104, 105 })]
	public static TimecodeSampleEntry createTimecodeSampleEntry(int flags, int timescale, int frameDuration, int numFrames)
	{
		TimecodeSampleEntry tmcd = new TimecodeSampleEntry(new Header("tmcd"));
		tmcd.flags = flags;
		tmcd.timescale = timescale;
		tmcd.frameDuration = frameDuration;
		tmcd.numFrames = (byte)(sbyte)numFrames;
		return tmcd;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 136, 105, 109, 109, 109, 110, 105 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		NIOUtils.skip(input, 4);
		flags = input.getInt();
		timescale = input.getInt();
		frameDuration = input.getInt();
		numFrames = (byte)(sbyte)input.get();
		NIOUtils.skip(input, 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 130, 104, 105, 110, 110, 110, 111, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(0);
		@out.putInt(flags);
		@out.putInt(timescale);
		@out.putInt(frameDuration);
		@out.put((byte)(sbyte)numFrames);
		@out.put(207);
	}

	[LineNumberTable(68)]
	public virtual int getFlags()
	{
		return flags;
	}

	[LineNumberTable(72)]
	public virtual int getTimescale()
	{
		return timescale;
	}

	[LineNumberTable(76)]
	public virtual int getFrameDuration()
	{
		return frameDuration;
	}

	[LineNumberTable(80)]
	public virtual byte getNumFrames()
	{
		return (byte)(sbyte)numFrames;
	}

	[LineNumberTable(84)]
	public virtual bool isDropFrame()
	{
		return (((uint)flags & (true ? 1u : 0u)) != 0) ? true : false;
	}
}
