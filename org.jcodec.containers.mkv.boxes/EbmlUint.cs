using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlUint : EbmlBin
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 106 })]
	public EbmlUint(byte[] id)
		: base(id)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 114, 114 })]
	public virtual void setUint(long value)
	{
		data = ByteBuffer.wrap(longToBytes(value));
		dataLen = data.limit();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 109, 106, 51, 167 })]
	public static byte[] longToBytes(long value)
	{
		byte[] b = new byte[calculatePayloadSize(value)];
		for (int i = (int)((nint)b.LongLength - 1); i >= 0; i += -1)
		{
			b[i] = (byte)(sbyte)((ulong)value >> (int)((8 * ((nint)b.LongLength - i - 1)) & 0x3F));
		}
		return b;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 106, 99, 106, 141 })]
	public static int calculatePayloadSize(long value)
	{
		if (value == 0u)
		{
			return 1;
		}
		if (value <= 2147483647u)
		{
			return 4 - (Integer.numberOfLeadingZeros((int)value) >> 3);
		}
		return 8 - (Long.numberOfLeadingZeros(value) >> 3);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 104, 104 })]
	public static EbmlUint createEbmlUint(byte[] id, long value)
	{
		EbmlUint e = new EbmlUint(id);
		e.setUint(value);
		return e;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 100, 100, 113, 127, 2, 109, 229,
		61, 231, 69
	})]
	public virtual long getUint()
	{
		long j = 0L;
		long tmp = 0L;
		for (int i = 0; i < data.limit(); i++)
		{
			tmp = (long)(sbyte)data.get(data.limit() - 1 - i) << 56;
			tmp = (long)((ulong)tmp >> 56 - i * 8);
			j |= tmp;
		}
		return j;
	}
}
