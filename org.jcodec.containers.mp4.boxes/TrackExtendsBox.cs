using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class TrackExtendsBox : FullBox
{
	private int trackId;

	private int defaultSampleDescriptionIndex;

	private int defaultSampleDuration;

	private int defaultSampleBytes;

	private int defaultSampleFlags;

	[LineNumberTable(28)]
	public static string fourcc()
	{
		return "trex";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 106 })]
	public TrackExtendsBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 104, 109, 109, 109, 109, 109 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		trackId = input.getInt();
		defaultSampleDescriptionIndex = input.getInt();
		defaultSampleDuration = input.getInt();
		defaultSampleBytes = input.getInt();
		defaultSampleFlags = input.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 104, 110, 110, 110, 110, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(trackId);
		@out.putInt(defaultSampleDescriptionIndex);
		@out.putInt(defaultSampleDuration);
		@out.putInt(defaultSampleBytes);
		@out.putInt(defaultSampleFlags);
	}

	[LineNumberTable(53)]
	public override int estimateSize()
	{
		return 32;
	}

	[LineNumberTable(57)]
	public virtual int getTrackId()
	{
		return trackId;
	}

	[LineNumberTable(new byte[] { 159, 127, 98, 104 })]
	public virtual void setTrackId(int trackId)
	{
		this.trackId = trackId;
	}

	[LineNumberTable(65)]
	public virtual int getDefaultSampleDescriptionIndex()
	{
		return defaultSampleDescriptionIndex;
	}

	[LineNumberTable(new byte[] { 159, 125, 98, 104 })]
	public virtual void setDefaultSampleDescriptionIndex(int defaultSampleDescriptionIndex)
	{
		this.defaultSampleDescriptionIndex = defaultSampleDescriptionIndex;
	}

	[LineNumberTable(73)]
	public virtual int getDefaultSampleDuration()
	{
		return defaultSampleDuration;
	}

	[LineNumberTable(new byte[] { 159, 123, 98, 104 })]
	public virtual void setDefaultSampleDuration(int defaultSampleDuration)
	{
		this.defaultSampleDuration = defaultSampleDuration;
	}

	[LineNumberTable(81)]
	public virtual int getDefaultSampleBytes()
	{
		return defaultSampleBytes;
	}

	[LineNumberTable(new byte[] { 159, 121, 98, 104 })]
	public virtual void setDefaultSampleBytes(int defaultSampleBytes)
	{
		this.defaultSampleBytes = defaultSampleBytes;
	}

	[LineNumberTable(89)]
	public virtual int getDefaultSampleFlags()
	{
		return defaultSampleFlags;
	}

	[LineNumberTable(new byte[] { 159, 119, 98, 104 })]
	public virtual void setDefaultSampleFlags(int defaultSampleFlags)
	{
		this.defaultSampleFlags = defaultSampleFlags;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(97)]
	public static TrackExtendsBox createTrackExtendsBox()
	{
		
		TrackExtendsBox result = new TrackExtendsBox(new Header(fourcc()));
		
		return result;
	}
}
