using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlDate : EbmlSint
{
	private const int NANOSECONDS_IN_A_SECOND = 1000000000;

	private const int MILISECONDS_IN_A_SECOND = 1000;

	private const int NANOSECONDS_IN_A_MILISECOND = 1000000;

	public static long MILISECONDS_SINCE_UNIX_EPOCH_START;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 119 })]
	private void setMiliseconds(long milliseconds)
	{
		setLong((milliseconds - MILISECONDS_SINCE_UNIX_EPOCH_START) * 1000000u);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 109, 110, 109 })]
	public override void setLong(long value)
	{
		data = ByteBuffer.allocate(8);
		data.putLong(value);
		data.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 106 })]
	public EbmlDate(byte[] id)
		: base(id)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 111 })]
	public virtual void setDate(Date value)
	{
		setMiliseconds(value.getTime());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 104, 112 })]
	public virtual Date getDate()
	{
		long val = getLong();
		val = val / 1000000L + MILISECONDS_SINCE_UNIX_EPOCH_START;
		Date result = new Date(val);
		
		return result;
	}

	[LineNumberTable(17)]
	static EbmlDate()
	{
		EbmlSint.___003Cclinit_003E();
		MILISECONDS_SINCE_UNIX_EPOCH_START = 978307200L;
	}
}
