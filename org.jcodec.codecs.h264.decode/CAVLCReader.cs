using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.io;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class CAVLCReader : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 99, 110, 135, 99, 101, 138, 174 })]
	public static int readUE(BitReader bits)
	{
		int cnt = 0;
		while (bits.read1Bit() == 0 && cnt < 32)
		{
			cnt++;
		}
		int res = 0;
		if (cnt > 0)
		{
			long val = bits.readNBit(cnt);
			res = (int)((1 << cnt) - 1 + val);
		}
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 137, 153 })]
	public static int readNBit(BitReader bits, int n, string message)
	{
		int val = bits.readNBit(n);
		Debug.trace(message, Integer.valueOf(val));
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 136, 153 })]
	public static int readUEtrace(BitReader bits, string message)
	{
		int res = readUE(bits);
		Debug.trace(message, Integer.valueOf(res));
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 137 })]
	private CAVLCReader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 130, 136, 136, 153 })]
	public static int readSE(BitReader bits, string message)
	{
		int val = readUE(bits);
		val = H264Utils2.golomb2Signed(val);
		Debug.trace(message, Integer.valueOf(val));
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 98, 142, 159, 0 })]
	public static bool readBool(BitReader bits, string message)
	{
		int res = ((bits.read1Bit() != 0) ? 1 : 0);
		Debug.trace(message, Integer.valueOf((res != 0) ? 1 : 0));
		return (byte)res != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(73)]
	public static int readU(BitReader bits, int i, string @string)
	{
		int result = readNBit(bits, i, @string);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 98, 101, 106 })]
	public static int readTE(BitReader bits, int max)
	{
		if (max > 1)
		{
			int result = readUE(bits);
			
			return result;
		}
		return (bits.read1Bit() ^ -1) & 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(83)]
	public static int readME(BitReader bits, string @string)
	{
		int result = readUEtrace(bits, @string);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 162, 99, 110, 135, 104, 153 })]
	public static int readZeroBitCount(BitReader bits, string message)
	{
		int count = 0;
		while (bits.read1Bit() == 0 && count < 32)
		{
			count++;
		}
		if (Debug.debug)
		{
			Debug.trace(message, String.valueOf(count));
		}
		return count;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(98)]
	public static bool moreRBSPData(BitReader bits)
	{
		return (bits.remaining() >= 32 || bits.checkNBit(1) != 1 || bits.checkNBit(24) << 9 != 0) ? true : false;
	}
}
