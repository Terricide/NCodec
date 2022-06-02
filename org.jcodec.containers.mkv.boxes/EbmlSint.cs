using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.containers.mkv.util;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlSint : EbmlBin
{
	internal static long[] ___003C_003EsignedComplement;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static long[] signedComplement
	{
		[HideFromJava]
		get
		{
			return ___003C_003EsignedComplement;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 104, 140 })]
	public static byte[] convertToBytes(long val)
	{
		int num = ebmlSignedLength(val);
		val += ___003C_003EsignedComplement[num];
		byte[] result = EbmlUtil.ebmlEncodeLen(val, num);
		
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 133, 98, 109, 99, 115, 99, 115, 99, 115,
		99, 121, 99, 121, 99, 121, 131
	})]
	public static int ebmlSignedLength(long val)
	{
		if (val <= 64u && val >= -63)
		{
			return 1;
		}
		if (val <= 8192u && val >= -8191)
		{
			return 2;
		}
		if (val <= 1048576u && val >= -1048575)
		{
			return 3;
		}
		if (val <= 134217728u && val >= -134217727)
		{
			return 4;
		}
		if (val <= 17179869184L && val >= -17179869183L)
		{
			return 5;
		}
		if (val <= 2199023255552L && val >= -2199023255551L)
		{
			return 6;
		}
		if (val <= 281474976710656L && val >= -281474976710655L)
		{
			return 7;
		}
		return 8;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public EbmlSint(byte[] id)
		: base(id)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 114 })]
	public virtual void setLong(long value)
	{
		data = ByteBuffer.wrap(convertToBytes(value));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 123, 148, 109, 100, 106, 52, 167 })]
	public virtual long getLong()
	{
		if (data.limit() - data.position() == 8)
		{
			long @long = data.duplicate().getLong();
			
			return @long;
		}
		byte[] b = data.array();
		long j = 0L;
		for (int i = (int)((nint)b.LongLength - 1); i >= 0; i += -1)
		{
			j |= (long)(int)b[i] << (int)((8 * ((nint)b.LongLength - 1 - i)) & 0x3F);
		}
		return j;
	}

	[LineNumberTable(56)]
	static EbmlSint()
	{
		___003C_003EsignedComplement = new long[9] { 0L, 63L, 8191L, 1048575L, 134217727L, 17179869183L, 2199023255551L, 281474976710655L, 36028797018963967L };
	}
}
