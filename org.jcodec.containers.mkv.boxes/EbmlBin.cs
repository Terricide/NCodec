using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.containers.mkv.util;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlBin : EbmlBase
{
	public ByteBuffer data;

	protected internal bool dataRead;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 109, 115, 104 })]
	public virtual void read(ByteBuffer source)
	{
		data = source.slice();
		data.limit(dataLen);
		dataRead = true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 234, 61, 200 })]
	public EbmlBin(byte[] id)
		: base(id)
	{
		dataRead = false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 136, 130, 109, 105, 104, 106 })]
	public virtual void readChannel(SeekableByteChannel @is)
	{
		ByteBuffer bb = ByteBuffer.allocate(dataLen);
		@is.read(bb);
		bb.flip();
		read(bb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 105, 119, 136 })]
	public virtual void skip(ByteBuffer source)
	{
		if (!dataRead)
		{
			source.position((int)(dataOffset + dataLen));
			dataRead = true;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 118, 138, 110, 118, 108 })]
	public override long size()
	{
		if (data == null || data.limit() == 0)
		{
			long result = base.size();
			
			return result;
		}
		long totalSize = data.limit();
		totalSize += EbmlUtil.ebmlLength(data.limit());
		return totalSize + id.LongLength;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 66, 109, 114 })]
	public virtual void setBuf(ByteBuffer data)
	{
		this.data = data.slice();
		dataLen = this.data.limit();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 115, 148, 124, 110, 105, 142, 104,
		141
	})]
	public override ByteBuffer getData()
	{
		int sizeSize = EbmlUtil.ebmlLength(data.limit());
		byte[] size = EbmlUtil.ebmlEncodeLen(data.limit(), sizeSize);
		ByteBuffer bb = ByteBuffer.allocate((int)((nint)id.LongLength + sizeSize + data.limit()));
		bb.put(id);
		bb.put(size);
		bb.put(data);
		bb.flip();
		data.flip();
		return bb;
	}
}
