using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mps;

public class PESPacket : Object
{
	public ByteBuffer data;

	public long pts;

	public int streamId;

	public int length;

	public long pos;

	public long dts;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 98, 105, 104, 104, 104, 105, 105, 105 })]
	public PESPacket(ByteBuffer data, long pts, int streamId, int length, long pos, long dts)
	{
		this.data = data;
		this.pts = pts;
		this.streamId = streamId;
		this.length = length;
		this.pos = pos;
		this.dts = dts;
	}
}
