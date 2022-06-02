using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class SampleSizesBox : FullBox
{
	private int defaultSize;

	private int count;

	private int[] sizes;

	[LineNumberTable(22)]
	public static string fourcc()
	{
		return "stsz";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 106 })]
	public SampleSizesBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 118, 104, 104 })]
	public static SampleSizesBox createSampleSizesBox(int defaultSize, int count)
	{
		
		SampleSizesBox stsz = new SampleSizesBox(new Header(fourcc()));
		stsz.defaultSize = defaultSize;
		stsz.count = count;
		return stsz;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 118, 104, 105 })]
	public static SampleSizesBox createSampleSizesBox2(int[] sizes)
	{
		
		SampleSizesBox stsz = new SampleSizesBox(new Header(fourcc()));
		stsz.sizes = sizes;
		stsz.count = sizes.Length;
		return stsz;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 104, 109, 141, 105, 114, 108, 47,
		199
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		defaultSize = input.getInt();
		count = input.getInt();
		if (defaultSize == 0)
		{
			sizes = new int[count];
			for (int i = 0; i < count; i++)
			{
				sizes[i] = input.getInt();
			}
		}
	}

	[LineNumberTable(53)]
	public virtual int getDefaultSize()
	{
		return defaultSize;
	}

	[LineNumberTable(57)]
	public virtual int[] getSizes()
	{
		return sizes;
	}

	[LineNumberTable(61)]
	public virtual int getCount()
	{
		return count;
	}

	[LineNumberTable(new byte[] { 159, 126, 98, 104 })]
	public virtual void setCount(int count)
	{
		this.count = count;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 130, 104, 142, 105, 110, 109, 107, 10,
		233, 69, 142
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(defaultSize);
		if (defaultSize == 0)
		{
			@out.putInt(count);
			for (int i = 0; i < (nint)sizes.LongLength; i++)
			{
				long size = sizes[i];
				@out.putInt((int)size);
			}
		}
		else
		{
			@out.putInt(count);
		}
	}

	[LineNumberTable(86)]
	public override int estimateSize()
	{
		return (int)(((defaultSize != 0) ? 0 : ((nint)sizes.LongLength * 4)) + 20);
	}

	[LineNumberTable(new byte[] { 159, 120, 130, 104, 105 })]
	public virtual void setSizes(int[] sizes)
	{
		this.sizes = sizes;
		count = sizes.Length;
	}
}
