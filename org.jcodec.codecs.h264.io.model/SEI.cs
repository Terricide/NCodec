using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common.io;

namespace org.jcodec.codecs.h264.io.model;

public class SEI : Object
{
	public class SEIMessage : Object
	{
		public int payloadType;

		public int payloadSize;

		public byte[] payload;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 66, 105, 104, 104, 104 })]
		public SEIMessage(int payloadType2, int payloadSize2, byte[] payload2)
		{
			payload = payload2;
			payloadType = payloadType2;
			payloadSize = payloadSize2;
		}
	}

	public SEIMessage[] messages;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 99, 99, 127, 0, 139, 105, 99,
		101, 99, 127, 0, 139, 105, 99, 101, 106, 102,
		131
	})]
	private static SEIMessage sei_message(ByteBuffer @is)
	{
		int payloadType = 0;
		int b = 0;
		while (@is.hasRemaining() && (b = (sbyte)@is.get() & 0xFF) == 255)
		{
			payloadType += 255;
		}
		if (!@is.hasRemaining())
		{
			return null;
		}
		payloadType += b;
		int payloadSize = 0;
		while (@is.hasRemaining() && (b = (sbyte)@is.get() & 0xFF) == 255)
		{
			payloadSize += 255;
		}
		if (!@is.hasRemaining())
		{
			return null;
		}
		payloadSize += b;
		byte[] payload = sei_payload(payloadType, payloadSize, @is);
		if ((nint)payload.LongLength != payloadSize)
		{
			return null;
		}
		SEIMessage result = new SEIMessage(payloadType, payloadSize, payload);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 105, 104 })]
	public SEI(SEIMessage[] messages)
	{
		this.messages = messages;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 66, 104, 105 })]
	private static byte[] sei_payload(int payloadType, int payloadSize, ByteBuffer @is)
	{
		byte[] res = new byte[payloadSize];
		@is.get(res);
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 167, 104, 100, 105, 132 })]
	public static SEI read(ByteBuffer @is)
	{
		ArrayList messages = new ArrayList();
		SEIMessage msg;
		do
		{
			msg = sei_message(@is);
			if (msg != null)
			{
				((List)messages).add((object)msg);
			}
		}
		while (msg != null);
		SEI result = new SEI((SEIMessage[])((List)messages).toArray((object[])new SEIMessage[0]));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 130, 168, 105 })]
	public virtual void write(ByteBuffer @out)
	{
		BitWriter writer = new BitWriter(@out);
		CAVLCWriter.writeTrailingBits(writer);
	}
}
