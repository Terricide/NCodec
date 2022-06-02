using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mkv.boxes;
using org.jcodec.containers.mkv.util;

namespace org.jcodec.containers.mkv;

public class MKVParser : java.lang.Object
{
	private SeekableByteChannel channel;

	[Signature("Ljava/util/LinkedList<Lorg/jcodec/containers/mkv/boxes/EbmlMaster;>;")]
	private LinkedList trace;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 105, 104, 140 })]
	public MKVParser(SeekableByteChannel channel)
	{
		this.channel = channel;
		trace = new LinkedList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mkv/boxes/EbmlMaster;>;")]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 103, 131, 110, 111, 159, 37, 122,
		154, 136, 105, 119, 108, 104, 114, 127, 0, 191,
		1, 191, 7, 6, 99, 159, 83, 119, 110, 151,
		241, 69, 110, 154
	})]
	public virtual List parse()
	{
		ArrayList tree = new ArrayList();
		EbmlBase e = null;
		while ((e = nextElement()) != null)
		{
			if (!isKnownType(e.id))
			{
				java.lang.System.err.println(new StringBuilder().append("Unspecified header: ").append(EbmlUtil.toHexString(e.id)).append(" at ")
					.append(e.offset)
					.toString());
			}
			while (!possibleChild((EbmlMaster)trace.peekFirst(), e))
			{
				closeElem((EbmlMaster)trace.removeFirst(), tree);
			}
			openElem(e);
			EbmlBin bin;
			OutOfMemoryError outOfMemoryError2;
			if (e is EbmlMaster)
			{
				trace.push((EbmlMaster)e);
			}
			else if (e is EbmlBin)
			{
				bin = (EbmlBin)e;
				EbmlMaster traceTop = (EbmlMaster)trace.peekFirst();
				if (traceTop.dataOffset + traceTop.dataLen < e.dataOffset + e.dataLen)
				{
					channel.setPosition(traceTop.dataOffset + traceTop.dataLen);
				}
				else
				{
					try
					{
						bin.readChannel(channel);
					}
					catch (System.Exception x)
					{
						OutOfMemoryError outOfMemoryError = ByteCodeHelper.MapException<OutOfMemoryError>(x, ByteCodeHelper.MapFlags.None);
						if (outOfMemoryError == null)
						{
							throw;
						}
						outOfMemoryError2 = outOfMemoryError;
						goto IL_014f;
					}
				}
				((EbmlMaster)trace.peekFirst()).add(e);
			}
			else
			{
				if (!(e is EbmlVoid))
				{
					
					throw new RuntimeException("Currently there are no elements that are neither Master nor Binary, should never actually get here");
				}
				((EbmlVoid)e).skip(channel);
			}
			continue;
			IL_014f:
			OutOfMemoryError oome = outOfMemoryError2;
			string message = new StringBuilder().append(e.type).append(" 0x").append(EbmlUtil.toHexString(bin.id))
				.append(" size: ")
				.append(Long.toHexString(bin.dataLen))
				.append(" offset: 0x")
				.append(Long.toHexString(e.offset))
				.toString();
			
			throw new RuntimeException(message, oome);
		}
		while (trace.peekFirst() != null)
		{
			closeElem((EbmlMaster)trace.removeFirst(), tree);
		}
		return tree;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 114, 66, 109, 111, 131, 141, 123, 102, 110,
		175, 141, 105, 104, 117, 146, 137
	})]
	private EbmlBase nextElement()
	{
		long offset = channel.position();
		if (offset >= channel.size())
		{
			return null;
		}
		byte[] typeId = readEbmlId(channel);
		while (typeId == null && !isKnownType(typeId) && offset < channel.size())
		{
			offset++;
			channel.setPosition(offset);
			typeId = readEbmlId(channel);
		}
		long dataLen = readEbmlInt(channel);
		EbmlBase elem = MKVType.createById(typeId, offset);
		elem.offset = offset;
		elem.typeSizeLength = (int)(channel.position() - offset);
		elem.dataOffset = channel.position();
		elem.dataLen = (int)dataLen;
		return elem;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 98, 127, 16, 131 })]
	public virtual bool isKnownType(byte[] b)
	{
		if (!trace.isEmpty() && java.lang.Object.instancehelper_equals(MKVType.___003C_003ECluster, ((EbmlMaster)trace.peekFirst()).type))
		{
			return true;
		}
		bool result = MKVType.isSpecifiedHeader(b);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 66, 127, 92, 127, 31, 131 })]
	private bool possibleChild(EbmlMaster parent, EbmlBase child)
	{
		if (parent != null && java.lang.Object.instancehelper_equals(MKVType.___003C_003ECluster, parent.type) && child != null && !java.lang.Object.instancehelper_equals(MKVType.___003C_003ECluster, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003EInfo, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003ESeekHead, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003ETracks, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003ECues, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003EAttachments, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003ETags, child.type) && !java.lang.Object.instancehelper_equals(MKVType.___003C_003EChapters, child.type))
		{
			return true;
		}
		bool result = MKVType.possibleChild(parent, child);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/containers/mkv/boxes/EbmlMaster;Ljava/util/List<Lorg/jcodec/containers/mkv/boxes/EbmlMaster;>;)V")]
	[LineNumberTable(new byte[] { 159, 116, 66, 110, 139, 153 })]
	private void closeElem(EbmlMaster e, List tree)
	{
		if (trace.peekFirst() == null)
		{
			tree.add(e);
		}
		else
		{
			((EbmlMaster)trace.peekFirst()).add(e);
		}
	}

	[LineNumberTable(101)]
	private void openElem(EbmlBase e)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 105, 162, 111, 131, 104, 105, 105, 136, 105,
		136, 100, 131, 101, 105, 169, 104, 109, 105
	})]
	public static byte[] readEbmlId(SeekableByteChannel source)
	{
		if (source.position() == source.size())
		{
			return null;
		}
		ByteBuffer buffer = ByteBuffer.allocate(8);
		buffer.limit(1);
		source.read(buffer);
		buffer.flip();
		int firstByte = (sbyte)buffer.get();
		int numBytes = EbmlUtil.computeLength((byte)firstByte);
		if (numBytes == 0)
		{
			return null;
		}
		if (numBytes > 1)
		{
			buffer.limit(numBytes);
			source.read(buffer);
		}
		buffer.flip();
		ByteBuffer val = ByteBuffer.allocate(buffer.remaining());
		val.put(buffer);
		byte[] result = val.array();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 98, 130, 104, 137, 105, 168, 105, 136, 100,
		177, 105, 105, 169, 111, 165, 101, 116, 167
	})]
	public static long readEbmlInt(SeekableByteChannel source)
	{
		ByteBuffer buffer = ByteBuffer.allocate(8);
		buffer.limit(1);
		source.read(buffer);
		buffer.flip();
		int firstByte = (sbyte)buffer.get();
		int length = EbmlUtil.computeLength((byte)firstByte);
		if (length == 0)
		{
			
			throw new RuntimeException("Invalid ebml integer size.");
		}
		buffer.limit(length);
		source.read(buffer);
		buffer.position(1);
		long value = firstByte & (int)(255u >> length);
		for (length += -1; length > 0; length += -1)
		{
			value = (value << 8) | ((sbyte)buffer.get() & 0xFF);
		}
		return value;
	}
}
