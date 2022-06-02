using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.containers.mkv.util;
using org.jcodec.platform;

namespace org.jcodec.containers.mkv.boxes;

public abstract class EbmlBase : Object
{
	protected internal EbmlMaster parent;

	public MKVType type;

	public byte[] id;

	public int dataLen;

	public long offset;

	public long dataOffset;

	public int typeSizeLength;

	public abstract ByteBuffer getData();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 233, 58, 232, 71, 104 })]
	public EbmlBase(byte[] id)
	{
		dataLen = 0;
		this.id = id;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public virtual bool equalId(byte[] typeId)
	{
		bool result = Platform.arrayEqualsByte(id, typeId);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	public virtual long size()
	{
		return dataLen + EbmlUtil.ebmlLength(dataLen) + (nint)id.LongLength;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 131, 98, 104 })]
	public virtual long mux(SeekableByteChannel os)
	{
		ByteBuffer bb = getData();
		return os.write(bb);
	}
}
