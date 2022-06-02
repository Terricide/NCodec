using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class TrackFragmentHeaderBox : FullBox
{
	public class Factory : Object
	{
		private TrackFragmentHeaderBox box;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 130, 105, 104 })]
		public Factory(TrackFragmentHeaderBox box)
		{
			this.box = box;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 124, 162, 116, 112 })]
		public virtual Factory baseDataOffset(long baseDataOffset)
		{
			box.flags |= 1;
			access_0024002(box, (int)baseDataOffset);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 122, 98, 116, 111 })]
		public virtual Factory sampleDescriptionIndex(long sampleDescriptionIndex)
		{
			box.flags |= 2;
			access_0024102(box, (int)sampleDescriptionIndex);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 162, 116, 111 })]
		public virtual Factory defaultSampleDuration(long defaultSampleDuration)
		{
			box.flags |= 8;
			access_0024202(box, (int)defaultSampleDuration);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 98, 117, 111 })]
		public virtual Factory defaultSampleSize(long defaultSampleSize)
		{
			box.flags |= 16;
			access_0024302(box, (int)defaultSampleSize);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 118, 162, 117, 111 })]
		public virtual Factory defaultSampleFlags(long defaultSampleFlags)
		{
			box.flags |= 32;
			access_0024402(box, (int)defaultSampleFlags);
			return this;
		}

		[LineNumberTable(new byte[] { 159, 116, 130, 140, 75, 3 })]
		public virtual TrackFragmentHeaderBox create()
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

	public const int FLAG_BASE_DATA_OFFSET = 1;

	public const int FLAG_SAMPLE_DESCRIPTION_INDEX = 2;

	public const int FLAG_DEFAILT_SAMPLE_DURATION = 8;

	public const int FLAG_DEFAULT_SAMPLE_SIZE = 16;

	public const int FLAG_DEFAILT_SAMPLE_FLAGS = 32;

	private int trackId;

	private long baseDataOffset;

	private int sampleDescriptionIndex;

	private int defaultSampleDuration;

	private int defaultSampleSize;

	private int defaultSampleFlags;

	[LineNumberTable(35)]
	public static string fourcc()
	{
		return "tfhd";
	}

	[LineNumberTable(151)]
	public virtual int getTrackId()
	{
		return trackId;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(14)]
	internal static long access_0024002(TrackFragmentHeaderBox x0, long x1)
	{
		x0.baseDataOffset = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(14)]
	internal static int access_0024102(TrackFragmentHeaderBox x0, int x1)
	{
		x0.sampleDescriptionIndex = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(14)]
	internal static int access_0024202(TrackFragmentHeaderBox x0, int x1)
	{
		x0.defaultSampleDuration = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(14)]
	internal static int access_0024302(TrackFragmentHeaderBox x0, int x1)
	{
		x0.defaultSampleSize = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(14)]
	internal static int access_0024402(TrackFragmentHeaderBox x0, int x1)
	{
		x0.defaultSampleFlags = x1;
		return x1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 106 })]
	public TrackFragmentHeaderBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 118, 104 })]
	public static TrackFragmentHeaderBox createTrackFragmentHeaderBoxWithId(int trackId)
	{
		
		TrackFragmentHeaderBox box = new TrackFragmentHeaderBox(new Header(fourcc()));
		box.trackId = trackId;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 118, 104, 104, 104, 104, 105, 105 })]
	public static TrackFragmentHeaderBox tfhd(int trackId, long baseDataOffset, int sampleDescriptionIndex, int defaultSampleDuration, int defaultSampleSize, int defaultSampleFlags)
	{
		
		TrackFragmentHeaderBox box = new TrackFragmentHeaderBox(new Header(fourcc()));
		box.trackId = trackId;
		box.baseDataOffset = baseDataOffset;
		box.sampleDescriptionIndex = sampleDescriptionIndex;
		box.defaultSampleDuration = defaultSampleDuration;
		box.defaultSampleSize = defaultSampleSize;
		box.defaultSampleFlags = defaultSampleFlags;
		return box;
	}

	[LineNumberTable(175)]
	public virtual bool isBaseDataOffsetAvailable()
	{
		return (((uint)flags & (true ? 1u : 0u)) != 0) ? true : false;
	}

	[LineNumberTable(179)]
	public virtual bool isSampleDescriptionIndexAvailable()
	{
		return (((uint)flags & 2u) != 0) ? true : false;
	}

	[LineNumberTable(183)]
	public virtual bool isDefaultSampleDurationAvailable()
	{
		return (((uint)flags & 8u) != 0) ? true : false;
	}

	[LineNumberTable(187)]
	public virtual bool isDefaultSampleSizeAvailable()
	{
		return (((uint)flags & 0x10u) != 0) ? true : false;
	}

	[LineNumberTable(191)]
	public virtual bool isDefaultSampleFlagsAvailable()
	{
		return (((uint)flags & 0x20u) != 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(50)]
	public static Factory create(int trackId)
	{
		Factory result = new Factory(createTrackFragmentHeaderBoxWithId(trackId));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 130, 127, 12, 109, 110 })]
	public static Factory copy(TrackFragmentHeaderBox other)
	{
		TrackFragmentHeaderBox box = tfhd(other.trackId, other.baseDataOffset, other.sampleDescriptionIndex, other.defaultSampleDuration, other.defaultSampleSize, other.defaultSampleFlags);
		box.setFlags(other.getFlags());
		box.setVersion((byte)(sbyte)other.getVersion());
		Factory result = new Factory(box);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 162, 104, 109, 105, 109, 105, 109, 105,
		109, 105, 109, 105, 109
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		trackId = input.getInt();
		if (isBaseDataOffsetAvailable())
		{
			baseDataOffset = input.getLong();
		}
		if (isSampleDescriptionIndexAvailable())
		{
			sampleDescriptionIndex = input.getInt();
		}
		if (isDefaultSampleDurationAvailable())
		{
			defaultSampleDuration = input.getInt();
		}
		if (isDefaultSampleSizeAvailable())
		{
			defaultSampleSize = input.getInt();
		}
		if (isDefaultSampleFlagsAvailable())
		{
			defaultSampleFlags = input.getInt();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 162, 104, 110, 105, 110, 105, 110, 105,
		110, 105, 110, 105, 110
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(trackId);
		if (isBaseDataOffsetAvailable())
		{
			@out.putLong(baseDataOffset);
		}
		if (isSampleDescriptionIndexAvailable())
		{
			@out.putInt(sampleDescriptionIndex);
		}
		if (isDefaultSampleDurationAvailable())
		{
			@out.putInt(defaultSampleDuration);
		}
		if (isDefaultSampleSizeAvailable())
		{
			@out.putInt(defaultSampleSize);
		}
		if (isDefaultSampleFlagsAvailable())
		{
			@out.putInt(defaultSampleFlags);
		}
	}

	[LineNumberTable(147)]
	public override int estimateSize()
	{
		return 40;
	}

	[LineNumberTable(155)]
	public virtual long getBaseDataOffset()
	{
		return baseDataOffset;
	}

	[LineNumberTable(159)]
	public virtual int getSampleDescriptionIndex()
	{
		return sampleDescriptionIndex;
	}

	[LineNumberTable(163)]
	public virtual int getDefaultSampleDuration()
	{
		return defaultSampleDuration;
	}

	[LineNumberTable(167)]
	public virtual int getDefaultSampleSize()
	{
		return defaultSampleSize;
	}

	[LineNumberTable(171)]
	public virtual int getDefaultSampleFlags()
	{
		return defaultSampleFlags;
	}

	[LineNumberTable(new byte[] { 159, 94, 162, 104 })]
	public virtual void setTrackId(int trackId)
	{
		this.trackId = trackId;
	}

	[LineNumberTable(new byte[] { 159, 93, 162, 104 })]
	public virtual void setDefaultSampleFlags(int defaultSampleFlags)
	{
		this.defaultSampleFlags = defaultSampleFlags;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(203)]
	public static TrackFragmentHeaderBox createTrackFragmentHeaderBox()
	{
		
		TrackFragmentHeaderBox result = new TrackFragmentHeaderBox(new Header(fourcc()));
		
		return result;
	}
}
