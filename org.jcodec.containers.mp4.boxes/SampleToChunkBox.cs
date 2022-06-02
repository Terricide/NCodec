using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class SampleToChunkBox : FullBox
{
	public class SampleToChunkEntry : Object
	{
		private long first;

		private int count;

		private int entry;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 98, 105, 104, 104, 104 })]
		public SampleToChunkEntry(long first, int count, int entry)
		{
			this.first = first;
			this.count = count;
			this.entry = entry;
		}

		[LineNumberTable(32)]
		public virtual long getFirst()
		{
			return first;
		}

		[LineNumberTable(new byte[] { 159, 133, 66, 104 })]
		public virtual void setFirst(long first)
		{
			this.first = first;
		}

		[LineNumberTable(40)]
		public virtual int getCount()
		{
			return count;
		}

		[LineNumberTable(44)]
		public virtual int getEntry()
		{
			return entry;
		}

		[LineNumberTable(new byte[] { 159, 130, 66, 104 })]
		public virtual void setEntry(int entry)
		{
			this.entry = entry;
		}

		[LineNumberTable(new byte[] { 159, 129, 66, 104 })]
		public virtual void setCount(int count)
		{
			this.count = count;
		}
	}

	private SampleToChunkEntry[] sampleToChunk;

	[LineNumberTable(57)]
	public static string fourcc()
	{
		return "stsc";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public SampleToChunkBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 118, 104 })]
	public static SampleToChunkBox createSampleToChunkBox(SampleToChunkEntry[] sampleToChunk)
	{
		
		SampleToChunkBox box = new SampleToChunkBox(new Header(fourcc()));
		box.sampleToChunk = sampleToChunk;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 66, 104, 136, 109, 103, 118, 12, 199 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		int size = input.getInt();
		sampleToChunk = new SampleToChunkEntry[size];
		for (int i = 0; i < size; i++)
		{
			sampleToChunk[i] = new SampleToChunkEntry(input.getInt(), input.getInt(), input.getInt());
		}
	}

	[LineNumberTable(79)]
	public virtual SampleToChunkEntry[] getSampleToChunk()
	{
		return sampleToChunk;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 66, 104, 143, 109, 106, 111, 110, 238,
		60, 231, 70
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(sampleToChunk.Length);
		for (int i = 0; i < (nint)sampleToChunk.LongLength; i++)
		{
			SampleToChunkEntry stc = sampleToChunk[i];
			@out.putInt((int)stc.getFirst());
			@out.putInt(stc.getCount());
			@out.putInt(stc.getEntry());
		}
	}

	[LineNumberTable(97)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)sampleToChunk.LongLength * 12);
	}

	[LineNumberTable(new byte[] { 159, 117, 98, 104 })]
	public virtual void setSampleToChunk(SampleToChunkEntry[] sampleToChunk)
	{
		this.sampleToChunk = sampleToChunk;
	}
}
