using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg4.es;

public class DescriptorParser : Object
{
	private const int ES_TAG = 3;

	private const int DC_TAG = 4;

	private const int DS_TAG = 5;

	private const int SL_TAG = 6;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 104, 105, 104 })]
	private static ES parseES(ByteBuffer input)
	{
		int trackId = input.getShort();
		_ = (sbyte)input.get();
		NodeDescriptor node = parseNodeDesc(input);
		ES result = new ES(trackId, node.getChildren());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 130, 118 })]
	private static SL parseSL(ByteBuffer input)
	{
		Preconditions.checkState(2 == ((sbyte)input.get() & 0xFF));
		SL result = new SL();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 66, 111, 105, 127, 0, 104, 136, 105 })]
	private static DecoderConfig parseDecoderConfig(ByteBuffer input)
	{
		int objectType = (sbyte)input.get() & 0xFF;
		_ = (sbyte)input.get();
		int bufSize = (((sbyte)input.get() & 0xFF) << 16) | (input.getShort() & 0xFFFF);
		int maxBitrate = input.getInt();
		int avgBitrate = input.getInt();
		NodeDescriptor node = parseNodeDesc(input);
		DecoderConfig result = new DecoderConfig(objectType, bufSize, maxBitrate, avgBitrate, node.getChildren());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 162, 104 })]
	private static DecoderSpecific parseDecoderSpecific(ByteBuffer input)
	{
		ByteBuffer data = NIOUtils.readBuf(input);
		DecoderSpecific result = new DecoderSpecific(data);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 106, 99, 111, 136, 137, 155, 138,
		138, 138, 138
	})]
	public static Descriptor read(ByteBuffer input)
	{
		if (input.remaining() < 2)
		{
			return null;
		}
		int tag = (sbyte)input.get() & 0xFF;
		int size = JCodecUtil2.readBER32(input);
		ByteBuffer byteBuffer = NIOUtils.read(input, size);
		switch (tag)
		{
		case 3:
		{
			ES result4 = parseES(byteBuffer);
			
			return result4;
		}
		case 6:
		{
			SL result3 = parseSL(byteBuffer);
			
			return result3;
		}
		case 4:
		{
			DecoderConfig result2 = parseDecoderConfig(byteBuffer);
			
			return result2;
		}
		case 5:
		{
			DecoderSpecific result = parseDecoderSpecific(byteBuffer);
			
			return result;
		}
		default:
		{
			string message = new StringBuilder().append("unknown tag ").append(tag).toString();
			
			throw new RuntimeException(message);
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 167, 104, 100, 105, 100 })]
	private static NodeDescriptor parseNodeDesc(ByteBuffer input)
	{
		ArrayList children = new ArrayList();
		Descriptor d;
		do
		{
			d = read(input);
			if (d != null)
			{
				((Collection)children).add((object)d);
			}
		}
		while (d != null);
		NodeDescriptor result = new NodeDescriptor(0, children);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public DescriptorParser()
	{
	}
}
