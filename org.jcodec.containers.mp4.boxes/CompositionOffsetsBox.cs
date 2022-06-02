using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class CompositionOffsetsBox : FullBox
{
	public class Entry : Object
	{
		public int count;

		public int offset;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 104 })]
		public Entry(int count, int offset)
		{
			this.count = count;
			this.offset = offset;
		}

		[LineNumberTable(28)]
		public virtual int getCount()
		{
			return count;
		}

		[LineNumberTable(32)]
		public virtual int getOffset()
		{
			return offset;
		}
	}

	public class LongEntry : Object
	{
		public long count;

		public long offset;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 132, 66, 105, 104, 104 })]
		public LongEntry(long count, long offset)
		{
			this.count = count;
			this.offset = offset;
		}

		[LineNumberTable(46)]
		public virtual long getCount()
		{
			return count;
		}

		[LineNumberTable(50)]
		public virtual long getOffset()
		{
			return offset;
		}
	}

	private Entry[] entries;

	[LineNumberTable(59)]
	public static string fourcc()
	{
		return "ctts";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 106 })]
	public CompositionOffsetsBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 162, 118, 104 })]
	public static CompositionOffsetsBox createCompositionOffsetsBox(Entry[] entries)
	{
		
		CompositionOffsetsBox ctts = new CompositionOffsetsBox(new Header(fourcc()));
		ctts.entries = entries;
		return ctts;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 130, 104, 136, 109, 103, 58, 167 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		int num = input.getInt();
		entries = new Entry[num];
		for (int i = 0; i < num; i++)
		{
			entries[i] = new Entry(input.getInt(), input.getInt());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 98, 136, 111, 109, 117, 21, 199 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(entries.Length);
		for (int i = 0; i < (nint)entries.LongLength; i++)
		{
			@out.putInt(entries[i].count);
			@out.putInt(entries[i].offset);
		}
	}

	[LineNumberTable(92)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)entries.LongLength * 8);
	}

	[LineNumberTable(96)]
	public virtual Entry[] getEntries()
	{
		return entries;
	}
}
