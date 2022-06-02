using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class TrackFragmentBaseMediaDecodeTimeBox : FullBox
{
	public class Factory : Object
	{
		private TrackFragmentBaseMediaDecodeTimeBox box;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 130, 105, 99, 112, 115, 114 })]
		protected internal Factory(TrackFragmentBaseMediaDecodeTimeBox other)
		{
			box = createTrackFragmentBaseMediaDecodeTimeBox(access_0024000(other));
			box.version = (byte)(sbyte)other.version;
			box.flags = other.flags;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 130, 110 })]
		public virtual Factory baseMediaDecodeTime(long val)
		{
			access_0024002(box, val);
			return this;
		}

		[LineNumberTable(new byte[] { 159, 117, 66, 140, 75, 3 })]
		public virtual TrackFragmentBaseMediaDecodeTimeBox create()
		{
			try
			{
				return box;
			}
			finally
			{
				box = null;
			}
		}
	}

	private long baseMediaDecodeTime;

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(22)]
	internal static long access_0024000(TrackFragmentBaseMediaDecodeTimeBox x0)
	{
		return x0.baseMediaDecodeTime;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 118, 104, 111, 136 })]
	public static TrackFragmentBaseMediaDecodeTimeBox createTrackFragmentBaseMediaDecodeTimeBox(long baseMediaDecodeTime)
	{
		
		TrackFragmentBaseMediaDecodeTimeBox box = new TrackFragmentBaseMediaDecodeTimeBox(new Header(fourcc()));
		box.baseMediaDecodeTime = baseMediaDecodeTime;
		if (box.baseMediaDecodeTime > 2147483647u)
		{
			box.version = 1;
		}
		return box;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(22)]
	internal static long access_0024002(TrackFragmentBaseMediaDecodeTimeBox x0, long x1)
	{
		x0.baseMediaDecodeTime = x1;
		return x1;
	}

	[LineNumberTable(41)]
	public static string fourcc()
	{
		return "tfdt";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 106 })]
	public TrackFragmentBaseMediaDecodeTimeBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 104, 106, 112, 107, 143, 113 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		if ((sbyte)version == 0)
		{
			baseMediaDecodeTime = input.getInt();
			return;
		}
		if ((sbyte)version == 1)
		{
			baseMediaDecodeTime = input.getLong();
			return;
		}
		
		throw new RuntimeException("Unsupported tfdt version");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 98, 104, 106, 113, 107, 144, 113 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		if ((sbyte)version == 0)
		{
			@out.putInt((int)baseMediaDecodeTime);
			return;
		}
		if ((sbyte)version == 1)
		{
			@out.putLong(baseMediaDecodeTime);
			return;
		}
		
		throw new RuntimeException("Unsupported tfdt version");
	}

	[LineNumberTable(68)]
	public override int estimateSize()
	{
		return 20;
	}

	[LineNumberTable(72)]
	public virtual long getBaseMediaDecodeTime()
	{
		return baseMediaDecodeTime;
	}

	[LineNumberTable(new byte[] { 159, 123, 66, 104 })]
	public virtual void setBaseMediaDecodeTime(long baseMediaDecodeTime)
	{
		this.baseMediaDecodeTime = baseMediaDecodeTime;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(80)]
	public static Factory copy(TrackFragmentBaseMediaDecodeTimeBox other)
	{
		Factory result = new Factory(other);
		
		return result;
	}
}
