using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlVoid : EbmlBase
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public EbmlVoid(byte[] id)
		: base(id)
	{
	}

	[LineNumberTable(22)]
	public override ByteBuffer getData()
	{
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 136, 130, 118 })]
	public virtual void skip(SeekableByteChannel @is)
	{
		@is.setPosition(dataOffset + dataLen);
	}
}
