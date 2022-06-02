using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlUlong : EbmlBin
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 106, 109 })]
	public EbmlUlong(byte[] id)
		: base(id)
	{
		data = ByteBuffer.allocate(8);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 110, 109 })]
	public virtual void setUlong(long value)
	{
		data.putLong(value);
		data.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 104, 104 })]
	public static EbmlUlong createEbmlUlong(byte[] id, long value)
	{
		EbmlUlong e = new EbmlUlong(id);
		e.setUlong(value);
		return e;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public virtual long getUlong()
	{
		long @long = data.duplicate().getLong();
		
		return @long;
	}
}
