using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class TrunBox : FullBox
{
	public class Factory : Object
	{
		private TrunBox box;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 117, 66, 105, 104 })]
		protected internal Factory(TrunBox box)
		{
			this.box = box;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 116, 98, 116, 111 })]
		public virtual Factory dataOffset(long dataOffset)
		{
			box.flags |= 1;
			access_0024002(box, (int)dataOffset);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 115, 162, 110, 113, 116, 110 })]
		public virtual Factory firstSampleFlags(int firstSampleFlags)
		{
			if (box.isSampleFlagsAvailable())
			{
				
				throw new IllegalStateException("Sample flags already set on this object");
			}
			box.flags |= 4;
			access_0024102(box, firstSampleFlags);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 113, 162, 112, 113, 120, 110 })]
		public virtual Factory sampleDuration(int[] sampleDuration)
		{
			if ((nint)sampleDuration.LongLength != access_0024200(box))
			{
				
				throw new IllegalArgumentException("Argument array length not equal to sampleCount");
			}
			box.flags |= 256;
			access_0024302(box, sampleDuration);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 111, 162, 112, 113, 120, 110 })]
		public virtual Factory sampleSize(int[] sampleSize)
		{
			if ((nint)sampleSize.LongLength != access_0024200(box))
			{
				
				throw new IllegalArgumentException("Argument array length not equal to sampleCount");
			}
			box.flags |= 512;
			access_0024402(box, sampleSize);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 109, 162, 112, 113, 110, 113, 120, 110 })]
		public virtual Factory sampleFlags(int[] sampleFlags)
		{
			if ((nint)sampleFlags.LongLength != access_0024200(box))
			{
				
				throw new IllegalArgumentException("Argument array length not equal to sampleCount");
			}
			if (box.isFirstSampleFlagsAvailable())
			{
				
				throw new IllegalStateException("First sample flags already set on this object");
			}
			box.flags |= 1024;
			access_0024502(box, sampleFlags);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 106, 98, 112, 113, 120, 110 })]
		public virtual Factory sampleCompositionOffset(int[] sampleCompositionOffset)
		{
			if ((nint)sampleCompositionOffset.LongLength != access_0024200(box))
			{
				
				throw new IllegalArgumentException("Argument array length not equal to sampleCount");
			}
			box.flags |= 2048;
			access_0024602(box, sampleCompositionOffset);
			return this;
		}

		[LineNumberTable(new byte[] { 159, 104, 130, 140, 75, 3 })]
		public virtual TrunBox create()
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

	private const int DATA_OFFSET_AVAILABLE = 1;

	private const int FIRST_SAMPLE_FLAGS_AVAILABLE = 4;

	private const int SAMPLE_DURATION_AVAILABLE = 256;

	private const int SAMPLE_SIZE_AVAILABLE = 512;

	private const int SAMPLE_FLAGS_AVAILABLE = 1024;

	private const int SAMPLE_COMPOSITION_OFFSET_AVAILABLE = 2048;

	private int sampleCount;

	private int dataOffset;

	private int firstSampleFlags;

	private int[] sampleDuration;

	private int[] sampleSize;

	private int[] sampleFlags;

	private int[] sampleCompositionOffset;

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int access_0024002(TrunBox x0, int x1)
	{
		x0.dataOffset = x1;
		return x1;
	}

	[LineNumberTable(214)]
	public virtual bool isSampleFlagsAvailable()
	{
		return (((uint)flags & 0x400u) != 0) ? true : false;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int access_0024102(TrunBox x0, int x1)
	{
		x0.firstSampleFlags = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int access_0024200(TrunBox x0)
	{
		return x0.sampleCount;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int[] access_0024302(TrunBox x0, int[] x1)
	{
		x0.sampleDuration = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int[] access_0024402(TrunBox x0, int[] x1)
	{
		x0.sampleSize = x1;
		return x1;
	}

	[LineNumberTable(226)]
	public virtual bool isFirstSampleFlagsAvailable()
	{
		return (((uint)flags & 4u) != 0) ? true : false;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int[] access_0024502(TrunBox x0, int[] x1)
	{
		x0.sampleFlags = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(35)]
	internal static int[] access_0024602(TrunBox x0, int[] x1)
	{
		x0.sampleCompositionOffset = x1;
		return x1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 130, 118, 104 })]
	public static TrunBox createTrunBox1(int sampleCount)
	{
		
		TrunBox trun = new TrunBox(new Header(fourcc()));
		trun.sampleCount = sampleCount;
		return trun;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 118, 104, 104, 104, 104, 105, 105,
		105
	})]
	public static TrunBox createTrunBox2(int sampleCount, int dataOffset, int firstSampleFlags, int[] sampleDuration, int[] sampleSize, int[] sampleFlags, int[] sampleCompositionOffset)
	{
		
		TrunBox trun = new TrunBox(new Header(fourcc()));
		trun.sampleCount = sampleCount;
		trun.dataOffset = dataOffset;
		trun.firstSampleFlags = firstSampleFlags;
		trun.sampleDuration = sampleDuration;
		trun.sampleSize = sampleSize;
		trun.sampleFlags = sampleFlags;
		trun.sampleCompositionOffset = sampleCompositionOffset;
		return trun;
	}

	[LineNumberTable(54)]
	public static string fourcc()
	{
		return "trun";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 130, 106 })]
	public TrunBox(Header header)
		: base(header)
	{
	}

	[LineNumberTable(206)]
	public virtual bool isDataOffsetAvailable()
	{
		return (((uint)flags & (true ? 1u : 0u)) != 0) ? true : false;
	}

	[LineNumberTable(222)]
	public virtual bool isSampleDurationAvailable()
	{
		return (((uint)flags & 0x100u) != 0) ? true : false;
	}

	[LineNumberTable(218)]
	public virtual bool isSampleSizeAvailable()
	{
		return (((uint)flags & 0x200u) != 0) ? true : false;
	}

	[LineNumberTable(210)]
	public virtual bool isSampleCompositionOffsetAvailable()
	{
		return (((uint)flags & 0x800u) != 0) ? true : false;
	}

	[LineNumberTable(new byte[] { 159, 128, 130, 104 })]
	public virtual void setDataOffset(int dataOffset)
	{
		this.dataOffset = dataOffset;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(62)]
	public static Factory create(int sampleCount)
	{
		Factory result = new Factory(createTrunBox1(sampleCount));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 130, 127, 12, 103, 109, 110 })]
	public static Factory copy(TrunBox other)
	{
		TrunBox box = createTrunBox2(other.sampleCount, other.dataOffset, other.firstSampleFlags, other.sampleDuration, other.sampleSize, other.sampleFlags, other.sampleCompositionOffset);
		box.setFlags(other.getFlags());
		box.setVersion((byte)(sbyte)other.getVersion());
		Factory result = new Factory(box);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(162)]
	public virtual long getSampleCount()
	{
		long result = Platform.unsignedInt(sampleCount);
		
		return result;
	}

	[LineNumberTable(166)]
	public virtual int getDataOffset()
	{
		return dataOffset;
	}

	[LineNumberTable(170)]
	public virtual int getFirstSampleFlags()
	{
		return firstSampleFlags;
	}

	[LineNumberTable(174)]
	public virtual int[] getSampleDurations()
	{
		return sampleDuration;
	}

	[LineNumberTable(178)]
	public virtual int[] getSampleSizes()
	{
		return sampleSize;
	}

	[LineNumberTable(182)]
	public virtual int[] getSamplesFlags()
	{
		return sampleFlags;
	}

	[LineNumberTable(186)]
	public virtual int[] getSampleCompositionOffsets()
	{
		return sampleCompositionOffset;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(190)]
	public virtual long getSampleDuration(int i)
	{
		long result = Platform.unsignedInt(sampleDuration[i]);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(194)]
	public virtual long getSampleSize(int i)
	{
		long result = Platform.unsignedInt(sampleSize[i]);
		
		return result;
	}

	[LineNumberTable(198)]
	public virtual int getSampleFlags(int i)
	{
		return sampleFlags[i];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(202)]
	public virtual long getSampleCompositionOffset(int i)
	{
		long result = Platform.unsignedInt(sampleCompositionOffset[i]);
		
		return result;
	}

	[LineNumberTable(230)]
	public static int flagsGetSampleDependsOn(int flags)
	{
		return (flags >> 6) & 3;
	}

	[LineNumberTable(234)]
	public static int flagsGetSampleIsDependedOn(int flags)
	{
		return (flags >> 8) & 3;
	}

	[LineNumberTable(238)]
	public static int flagsGetSampleHasRedundancy(int flags)
	{
		return (flags >> 10) & 3;
	}

	[LineNumberTable(242)]
	public static int flagsGetSamplePaddingValue(int flags)
	{
		return (flags >> 12) & 7;
	}

	[LineNumberTable(246)]
	public static int flagsGetSampleIsDifferentSample(int flags)
	{
		return (flags >> 15) & 1;
	}

	[LineNumberTable(250)]
	public static int flagsGetSampleDegradationPriority(int flags)
	{
		return (flags >> 16) & 0xFFFF;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(254)]
	public static TrunBox createTrunBox()
	{
		
		TrunBox result = new TrunBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 78, 162, 136, 113, 145, 109, 105, 109, 105,
		109, 105, 114, 105, 114, 105, 114, 105, 146, 111,
		105, 111, 105, 111, 105, 111, 105, 239, 56, 234,
		74
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		if (isSampleFlagsAvailable() && isFirstSampleFlagsAvailable())
		{
			
			throw new RuntimeException("Broken stream");
		}
		sampleCount = input.getInt();
		if (isDataOffsetAvailable())
		{
			dataOffset = input.getInt();
		}
		if (isFirstSampleFlagsAvailable())
		{
			firstSampleFlags = input.getInt();
		}
		if (isSampleDurationAvailable())
		{
			sampleDuration = new int[sampleCount];
		}
		if (isSampleSizeAvailable())
		{
			sampleSize = new int[sampleCount];
		}
		if (isSampleFlagsAvailable())
		{
			sampleFlags = new int[sampleCount];
		}
		if (isSampleCompositionOffsetAvailable())
		{
			sampleCompositionOffset = new int[sampleCount];
		}
		for (int i = 0; i < sampleCount; i++)
		{
			if (isSampleDurationAvailable())
			{
				sampleDuration[i] = input.getInt();
			}
			if (isSampleSizeAvailable())
			{
				sampleSize[i] = input.getInt();
			}
			if (isSampleFlagsAvailable())
			{
				sampleFlags[i] = input.getInt();
			}
			if (isSampleCompositionOffsetAvailable())
			{
				sampleCompositionOffset[i] = input.getInt();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 69, 66, 104, 110, 105, 110, 105, 142, 111,
		105, 112, 105, 112, 105, 112, 105, 240, 56, 234,
		74
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(sampleCount);
		if (isDataOffsetAvailable())
		{
			@out.putInt(dataOffset);
		}
		if (isFirstSampleFlagsAvailable())
		{
			@out.putInt(firstSampleFlags);
		}
		for (int i = 0; i < sampleCount; i++)
		{
			if (isSampleDurationAvailable())
			{
				@out.putInt(sampleDuration[i]);
			}
			if (isSampleSizeAvailable())
			{
				@out.putInt(sampleSize[i]);
			}
			if (isSampleFlagsAvailable())
			{
				@out.putInt(sampleFlags[i]);
			}
			if (isSampleCompositionOffsetAvailable())
			{
				@out.putInt(sampleCompositionOffset[i]);
			}
		}
	}

	[LineNumberTable(313)]
	public override int estimateSize()
	{
		return 24 + sampleCount * 16;
	}
}
