using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mps;

public class MTSDemuxer : java.lang.Object
{
	public class MTSPacket : java.lang.Object
	{
		public ByteBuffer payload;

		public bool payloadStart;

		public int pid;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 110, 161, 67, 105, 104, 104, 104 })]
		public MTSPacket(int guid, bool payloadStart, ByteBuffer payload)
		{
			pid = guid;
			this.payloadStart = payloadStart;
			this.payload = payload;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	[Implements(new string[] { "java.nio.channels.ReadableByteChannel" })]
	internal class ProgramChannel : java.lang.Object, ReadableByteChannel, Channel, Closeable, AutoCloseable
	{
		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private MTSDemuxer demuxer;

		[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
		private List data;

		private bool closed;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 124, 130, 105, 104, 108 })]
		public ProgramChannel(MTSDemuxer demuxer)
		{
			this.demuxer = demuxer;
			data = new ArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 115, 98, 105, 98, 115 })]
		public virtual void storePacket(MTSPacket pkt)
		{
			if (!closed)
			{
				data.add(pkt.payload);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(81)]
		public virtual bool isOpen()
		{
			return (!closed && access_0024000(demuxer).isOpen()) ? true : false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 121, 130, 104, 110 })]
		public virtual void close()
		{
			closed = true;
			data.clear();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 119, 66, 99, 108, 110, 110, 138, 115, 115,
			111, 105, 110, 101, 102
		})]
		public virtual int read(ByteBuffer dst)
		{
			int bytesRead = 0;
			while (dst.hasRemaining())
			{
				while (data.size() == 0)
				{
					if (!access_0024100(demuxer))
					{
						return (bytesRead <= 0) ? (-1) : bytesRead;
					}
				}
				ByteBuffer first = (ByteBuffer)data.get(0);
				int toRead = java.lang.Math.min(dst.remaining(), first.remaining());
				dst.put(NIOUtils.read(first, toRead));
				if (!first.hasRemaining())
				{
					data.remove(0);
				}
				bytesRead += toRead;
			}
			return bytesRead;
		}

		public void Dispose()
		{
			close();
		}
	}

	private org.jcodec.common.io.SeekableByteChannel channel;

	[Signature("Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mps/MTSDemuxer$ProgramChannel;>;")]
	private Map programs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 105, 104, 108, 127, 8, 121, 99,
		106
	})]
	public MTSDemuxer(org.jcodec.common.io.SeekableByteChannel src)
	{
		channel = src;
		programs = new HashMap();
		Iterator iterator = findPrograms(src).iterator();
		while (iterator.hasNext())
		{
			int pid = ((Integer)iterator.next()).intValue();
			programs.put(Integer.valueOf(pid), new ProgramChannel(this));
		}
		src.setPosition(0L);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(64)]
	public virtual ReadableByteChannel getProgram(int pid)
	{
		return (ReadableByteChannel)programs.get(Integer.valueOf(pid));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/Set<Ljava/lang/Integer;>;")]
	[LineNumberTable(33)]
	public virtual Set getPrograms()
	{
		Set result = programs.keySet();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 113, 66, 109, 100, 99, 125, 100, 136 })]
	private bool readAndDispatchNextTSPacket()
	{
		MTSPacket pkt = readPacket(channel);
		if (pkt == null)
		{
			return false;
		}
		((ProgramChannel)programs.get(Integer.valueOf(pkt.pid)))?.storePacket(pkt);
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 108, 162, 108, 111, 99, 104 })]
	public static MTSPacket readPacket(ReadableByteChannel channel)
	{
		ByteBuffer buffer = ByteBuffer.allocate(188);
		if (NIOUtils.readFromChannel(channel, buffer) != 188)
		{
			return null;
		}
		buffer.flip();
		MTSPacket result = parsePacket(buffer);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/SeekableByteChannel;)Ljava/util/Set<Ljava/lang/Integer;>;")]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 104, 103, 125, 104, 100, 102, 105,
		99, 105, 127, 14, 243, 56, 234, 75, 105
	})]
	public virtual Set findPrograms(org.jcodec.common.io.SeekableByteChannel src)
	{
		long rem = src.position();
		HashSet guids = new HashSet();
		for (int i = 0; ((Set)guids).size() == 0 || i < ((Set)guids).size() * 500; i++)
		{
			MTSPacket pkt = readPacket(src);
			if (pkt == null)
			{
				break;
			}
			if (pkt.payload != null)
			{
				ByteBuffer payload = pkt.payload;
				if (!((Set)guids).contains((object)Integer.valueOf(pkt.pid)) && (payload.duplicate().getInt() & -256) == 256)
				{
					((Set)guids).add((object)Integer.valueOf(pkt.pid));
				}
			}
		}
		src.setPosition(rem);
		return guids;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 111, 107, 104, 105, 104, 112, 104,
		104, 100, 114, 140
	})]
	public static MTSPacket parsePacket(ByteBuffer buffer)
	{
		int marker = (sbyte)buffer.get() & 0xFF;
		Preconditions.checkState(71 == marker);
		int guidFlags = buffer.getShort();
		int guid = guidFlags & 0x1FFF;
		int payloadStart = (guidFlags >> 14) & 1;
		int b0 = (sbyte)buffer.get() & 0xFF;
		int counter = b0 & 0xF;
		if (((uint)b0 & 0x20u) != 0)
		{
			int taken = 0;
			taken = ((sbyte)buffer.get() & 0xFF) + 1;
			NIOUtils.skip(buffer, taken - 1);
		}
		MTSPacket result = new MTSPacket(guid, payloadStart == 1, ((b0 & 0x10) == 0) ? null : buffer);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 98, 104, 167, 109, 127, 8, 134, 105,
		127, 1, 102, 117, 106, 104, 181, 106, 191, 28,
		6, 99, 163, 100, 105, 122, 112, 111, 103, 246,
		60, 236, 71
	})]
	public static int probe(ByteBuffer b_)
	{
		ByteBuffer b = b_.duplicate();
		IntObjectMap streams = new IntObjectMap();
		while (true)
		{
			ByteBuffer sub;
			System.Exception ex;
			try
			{
				sub = NIOUtils.read(b, 188);
				if (sub.remaining() < 188)
				{
					break;
				}
			}
			catch (System.Exception x)
			{
				ex = ByteCodeHelper.MapException<System.Exception>(x, ByteCodeHelper.MapFlags.None);
				goto IL_003e;
			}
			MTSPacket tsPkt;
			System.Exception ex2;
			try
			{
				tsPkt = parsePacket(sub);
				if (tsPkt == null)
				{
					break;
				}
			}
			catch (System.Exception x2)
			{
				ex2 = ByteCodeHelper.MapException<System.Exception>(x2, ByteCodeHelper.MapFlags.None);
				goto IL_006c;
			}
			System.Exception ex3;
			try
			{
				object data = (List)streams.get(tsPkt.pid);
				if ((List)data == null)
				{
					data = new ArrayList();
					streams.put(tsPkt.pid, (ArrayList)data);
				}
				if (tsPkt.payload != null)
				{
					object obj = data;
					object payload = tsPkt.payload;
					((obj == null) ? null : ((obj as List) ?? throw new java.lang.IncompatibleClassChangeError())).add(payload);
				}
			}
			catch (System.Exception x3)
			{
				ex3 = ByteCodeHelper.MapException<System.Exception>(x3, ByteCodeHelper.MapFlags.None);
				goto IL_00f6;
			}
			continue;
			IL_003e:
			System.Exception ex4 = ex;
			goto IL_0100;
			IL_0100:
			System.Exception t = ex4;
			break;
			IL_00f6:
			ex4 = ex3;
			goto IL_0100;
			IL_006c:
			ex4 = ex2;
			goto IL_0100;
		}
		int maxScore = 0;
		int[] keys = streams.keys();
		int[] array = keys;
		int num = array.Length;
		for (int j = 0; j < num; j++)
		{
			int i = array[j];
			List packets = (List)streams.get(i);
			int score = MPSDemuxer.probe(NIOUtils.combineBuffers(packets));
			if (score > maxScore)
			{
				maxScore = score + ((packets.size() > 20) ? 50 : 0);
			}
		}
		return maxScore;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(28)]
	internal static org.jcodec.common.io.SeekableByteChannel access_0024000(MTSDemuxer x0)
	{
		return x0.channel;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(28)]
	internal static bool access_0024100(MTSDemuxer x0)
	{
		bool result = x0.readAndDispatchNextTSPacket();
		
		return result;
	}
}
