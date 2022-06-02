using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.flv;

public class FLVWriter : Object
{
	private const int WRITE_BUFFER_SIZE = 1048576;

	private int startOfLastPacket;

	private SeekableByteChannel @out;

	private ByteBuffer writeBuf;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 162, 112, 109, 127, 0, 109, 112, 145 })]
	public virtual void addPacket(FLVTag pkt)
	{
		if (!writePacket(writeBuf, pkt))
		{
			writeBuf.flip();
			startOfLastPacket -= @out.write(writeBuf);
			writeBuf.clear();
			if (!writePacket(writeBuf, pkt))
			{
				
				throw new RuntimeException("Unexpected");
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 233, 60, 233, 69, 104, 113, 110 })]
	public FLVWriter(SeekableByteChannel @out)
	{
		startOfLastPacket = 9;
		this.@out = @out;
		writeBuf = ByteBuffer.allocate(1048576);
		writeHeader(writeBuf);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 129, 130, 109, 115 })]
	public virtual void finish()
	{
		writeBuf.flip();
		@out.write(writeBuf);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 98, 106, 106, 106, 105, 105, 106 })]
	private static void writeHeader(ByteBuffer writeBuf)
	{
		writeBuf.put(70);
		writeBuf.put(76);
		writeBuf.put(86);
		writeBuf.put(1);
		writeBuf.put(5);
		writeBuf.putInt(9);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 127, 6, 141, 109, 131, 117, 141,
		106, 108, 144, 119, 117, 152, 105, 137, 146
	})]
	private bool writePacket(ByteBuffer writeBuf, FLVTag pkt)
	{
		int pktType = ((pkt.getType() == FLVTag.Type.___003C_003EVIDEO) ? 9 : ((pkt.getType() != FLVTag.Type.___003C_003ESCRIPT) ? 8 : 18));
		int dataLen = pkt.getData().remaining();
		if (writeBuf.remaining() < 15 + dataLen)
		{
			return false;
		}
		writeBuf.putInt(writeBuf.position() - startOfLastPacket);
		startOfLastPacket = writeBuf.position();
		writeBuf.put((byte)(sbyte)pktType);
		writeBuf.putShort((short)(dataLen >> 8));
		writeBuf.put((byte)(sbyte)((uint)dataLen & 0xFFu));
		writeBuf.putShort((short)((pkt.getPts() >> 8) & 0xFFFF));
		writeBuf.put((byte)(sbyte)((uint)pkt.getPts() & 0xFFu));
		writeBuf.put((byte)(sbyte)((uint)(pkt.getPts() >> 24) & 0xFFu));
		writeBuf.putShort(0);
		writeBuf.put(0);
		NIOUtils.write(writeBuf, pkt.getData().duplicate());
		return true;
	}
}
