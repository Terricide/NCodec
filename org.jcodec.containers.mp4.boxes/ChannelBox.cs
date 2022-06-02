using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4.boxes;

public class ChannelBox : FullBox
{
	public class ChannelDescription : Object
	{
		private int channelLabel;

		private int channelFlags;

		private float[] coordinates;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 66, 105, 109, 104, 104, 104 })]
		public ChannelDescription(int channelLabel, int channelFlags, float[] coordinates)
		{
			this.coordinates = new float[3];
			this.channelLabel = channelLabel;
			this.channelFlags = channelFlags;
			this.coordinates = coordinates;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(44)]
		public virtual Label getLabel()
		{
			Label byVal = Label.getByVal(channelLabel);
			
			return byVal;
		}

		[LineNumberTable(32)]
		public virtual int getChannelLabel()
		{
			return channelLabel;
		}

		[LineNumberTable(36)]
		public virtual int getChannelFlags()
		{
			return channelFlags;
		}

		[LineNumberTable(40)]
		public virtual float[] getCoordinates()
		{
			return coordinates;
		}
	}

	private int channelLayout;

	private int channelBitmap;

	private ChannelDescription[] descriptions;

	[LineNumberTable(53)]
	public static string fourcc()
	{
		return "chan";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public static ChannelBox createChannelBox()
	{
		
		ChannelBox result = new ChannelBox(new Header(fourcc()));
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 115, 130, 104 })]
	public virtual void setChannelLayout(int channelLayout)
	{
		this.channelLayout = channelLayout;
	}

	[LineNumberTable(new byte[] { 159, 114, 130, 104 })]
	public virtual void setDescriptions(ChannelDescription[] descriptions)
	{
		this.descriptions = descriptions;
	}

	[LineNumberTable(98)]
	public virtual int getChannelLayout()
	{
		return channelLayout;
	}

	[LineNumberTable(106)]
	public virtual ChannelDescription[] getDescriptions()
	{
		return descriptions;
	}

	[LineNumberTable(102)]
	public virtual int getChannelBitmap()
	{
		return channelBitmap;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 106 })]
	public ChannelBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 136, 109, 109, 136, 109, 103, 125,
		127, 2, 244, 61, 231, 69
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		channelLayout = input.getInt();
		channelBitmap = input.getInt();
		int numDescriptions = input.getInt();
		descriptions = new ChannelDescription[numDescriptions];
		FloatConverter converter = default(FloatConverter);
		for (int i = 0; i < numDescriptions; i++)
		{
			descriptions[i] = new ChannelDescription(input.getInt(), input.getInt(), new float[3]
			{
				FloatConverter.ToFloat(input.getInt(), ref converter),
				FloatConverter.ToFloat(input.getInt(), ref converter),
				FloatConverter.ToFloat(input.getInt(), ref converter)
			});
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 66, 104, 110, 110, 143, 109, 106, 110,
		142, 112, 112, 240, 57, 234, 73
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(channelLayout);
		@out.putInt(channelBitmap);
		@out.putInt(descriptions.Length);
		for (int i = 0; i < (nint)descriptions.LongLength; i++)
		{
			ChannelDescription channelDescription = descriptions[i];
			@out.putInt(channelDescription.getChannelLabel());
			@out.putInt(channelDescription.getChannelFlags());
			@out.putFloat(channelDescription.getCoordinates()[0]);
			@out.putFloat(channelDescription.getCoordinates()[1]);
			@out.putFloat(channelDescription.getCoordinates()[2]);
		}
	}

	[LineNumberTable(94)]
	public override int estimateSize()
	{
		return (int)(24 + (nint)descriptions.LongLength * 20);
	}
}
