using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class TimeToSampleBox : FullBox
{
	public class TimeToSampleEntry : Object
	{
		internal int sampleCount;

		internal int sampleDuration;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 66, 105, 104, 104 })]
		public TimeToSampleEntry(int sampleCount, int sampleDuration)
		{
			this.sampleCount = sampleCount;
			this.sampleDuration = sampleDuration;
		}

		[LineNumberTable(30)]
		public virtual int getSampleCount()
		{
			return sampleCount;
		}

		[LineNumberTable(34)]
		public virtual int getSampleDuration()
		{
			return sampleDuration;
		}

		[LineNumberTable(new byte[] { 159, 133, 130, 104 })]
		public virtual void setSampleDuration(int sampleDuration)
		{
			this.sampleDuration = sampleDuration;
		}

		[LineNumberTable(new byte[] { 159, 132, 130, 104 })]
		public virtual void setSampleCount(int sampleCount)
		{
			this.sampleCount = sampleCount;
		}

		[LineNumberTable(46)]
		public virtual long getSegmentDuration()
		{
			return sampleCount * sampleDuration;
		}
	}

	private TimeToSampleEntry[] entries;

	[LineNumberTable(51)]
	public static string fourcc()
	{
		return "stts";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public TimeToSampleBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 118, 104 })]
	public static TimeToSampleBox createTimeToSampleBox(TimeToSampleEntry[] timeToSamples)
	{
		
		TimeToSampleBox box = new TimeToSampleBox(new Header(fourcc()));
		box.entries = timeToSamples;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 162, 104, 104, 109, 103, 58, 167 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		int foo = input.getInt();
		entries = new TimeToSampleEntry[foo];
		for (int i = 0; i < foo; i++)
		{
			entries[i] = new TimeToSampleEntry(input.getInt(), input.getInt());
		}
	}

	[LineNumberTable(72)]
	public virtual TimeToSampleEntry[] getEntries()
	{
		return entries;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 98, 104, 111, 109, 106, 110, 238, 61,
		231, 69
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(entries.Length);
		for (int i = 0; i < (nint)entries.LongLength; i++)
		{
			TimeToSampleEntry timeToSampleEntry = entries[i];
			@out.putInt(timeToSampleEntry.getSampleCount());
			@out.putInt(timeToSampleEntry.getSampleDuration());
		}
	}

	[LineNumberTable(88)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)entries.LongLength * 8);
	}

	[LineNumberTable(new byte[] { 159, 119, 66, 104 })]
	public virtual void setEntries(TimeToSampleEntry[] entries)
	{
		this.entries = entries;
	}
}
