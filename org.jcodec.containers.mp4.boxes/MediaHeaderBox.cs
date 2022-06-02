using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class MediaHeaderBox : FullBox
{
	private long created;

	private long modified;

	private int timescale;

	private long duration;

	private int language;

	private int quality;

	[LineNumberTable(30)]
	public static string fourcc()
	{
		return "mdhd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 106 })]
	public MediaHeaderBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 118, 104, 104, 104, 104, 105, 105 })]
	public static MediaHeaderBox createMediaHeaderBox(int timescale, long duration, int language, long created, long modified, int quality)
	{
		
		MediaHeaderBox mdhd = new MediaHeaderBox(new Header(fourcc()));
		mdhd.timescale = timescale;
		mdhd.duration = duration;
		mdhd.language = language;
		mdhd.created = created;
		mdhd.modified = modified;
		mdhd.quality = quality;
		return mdhd;
	}

	[LineNumberTable(46)]
	public virtual int getTimescale()
	{
		return timescale;
	}

	[LineNumberTable(50)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(54)]
	public virtual long getCreated()
	{
		return created;
	}

	[LineNumberTable(58)]
	public virtual long getModified()
	{
		return modified;
	}

	[LineNumberTable(62)]
	public virtual int getLanguage()
	{
		return language;
	}

	[LineNumberTable(66)]
	public virtual int getQuality()
	{
		return quality;
	}

	[LineNumberTable(new byte[] { 159, 125, 130, 104 })]
	public virtual void setDuration(long duration)
	{
		this.duration = duration;
	}

	[LineNumberTable(new byte[] { 159, 124, 130, 104 })]
	public virtual void setTimescale(int timescale)
	{
		this.timescale = timescale;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 104, 106, 114, 114, 109, 112, 107,
		115, 115, 109, 143, 145
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		if ((sbyte)version == 0)
		{
			created = TimeUtil.fromMovTime(input.getInt());
			modified = TimeUtil.fromMovTime(input.getInt());
			timescale = input.getInt();
			duration = input.getInt();
			return;
		}
		if ((sbyte)version == 1)
		{
			created = TimeUtil.fromMovTime((int)input.getLong());
			modified = TimeUtil.fromMovTime((int)input.getLong());
			timescale = input.getInt();
			duration = input.getLong();
			return;
		}
		
		throw new RuntimeException("Unsupported version");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 162, 104, 115, 115, 110, 111, 111, 111 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(TimeUtil.toMovTime(created));
		@out.putInt(TimeUtil.toMovTime(modified));
		@out.putInt(timescale);
		@out.putInt((int)duration);
		@out.putShort((short)language);
		@out.putShort((short)quality);
	}

	[LineNumberTable(106)]
	public override int estimateSize()
	{
		return 32;
	}
}
