using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class HandlerBox : FullBox
{
	private string componentType;

	private string componentSubType;

	private string componentManufacturer;

	private int componentFlags;

	private int componentFlagsMask;

	private string componentName;

	[LineNumberTable(31)]
	public static string fourcc()
	{
		return "hdlr";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 106 })]
	public HandlerBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 118, 104, 104, 104, 104, 105, 108 })]
	public static HandlerBox createHandlerBox(string componentType, string componentSubType, string componentManufacturer, int componentFlags, int componentFlagsMask)
	{
		
		HandlerBox hdlr = new HandlerBox(new Header(fourcc()));
		hdlr.componentType = componentType;
		hdlr.componentSubType = componentSubType;
		hdlr.componentManufacturer = componentManufacturer;
		hdlr.componentFlags = componentFlags;
		hdlr.componentFlagsMask = componentFlagsMask;
		hdlr.componentName = "";
		return hdlr;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 136, 110, 110, 142, 109, 109, 115 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		componentType = NIOUtils.readString(input, 4);
		componentSubType = NIOUtils.readString(input, 4);
		componentManufacturer = NIOUtils.readString(input, 4);
		componentFlags = input.getInt();
		componentFlagsMask = input.getInt();
		componentName = NIOUtils.readString(input, input.remaining());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 136, 115, 115, 147, 110, 110, 105,
		147
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.put(JCodecUtil2.asciiString(componentType));
		@out.put(JCodecUtil2.asciiString(componentSubType));
		@out.put(JCodecUtil2.asciiString(componentManufacturer));
		@out.putInt(componentFlags);
		@out.putInt(componentFlagsMask);
		if (componentName != null)
		{
			@out.put(JCodecUtil2.asciiString(componentName));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 130, 127, 4, 43 })]
	public override int estimateSize()
	{
		return (int)(12 + (nint)JCodecUtil2.asciiString(componentType).LongLength + (nint)JCodecUtil2.asciiString(componentSubType).LongLength + (nint)JCodecUtil2.asciiString(componentManufacturer).LongLength + 9);
	}

	[LineNumberTable(79)]
	public virtual string getComponentType()
	{
		return componentType;
	}

	[LineNumberTable(83)]
	public virtual string getComponentSubType()
	{
		return componentSubType;
	}

	[LineNumberTable(87)]
	public virtual string getComponentManufacturer()
	{
		return componentManufacturer;
	}

	[LineNumberTable(91)]
	public virtual int getComponentFlags()
	{
		return componentFlags;
	}

	[LineNumberTable(95)]
	public virtual int getComponentFlagsMask()
	{
		return componentFlagsMask;
	}
}
