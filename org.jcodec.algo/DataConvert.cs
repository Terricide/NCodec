using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.api;

namespace org.jcodec.algo;

public class DataConvert : Object
{
	[LineNumberTable(new byte[] { 159, 136, 162, 107, 99, 104, 63, 1, 199 })]
	public static int[] from24BE(byte[] b)
	{
		int[] result = new int[(nint)b.LongLength / 3];
		int off = 0;
		for (int i = 0; i < (nint)result.LongLength; i++)
		{
			int num = i;
			int num2 = off;
			off++;
			int num3 = b[num2] << 16;
			int num4 = off;
			off++;
			int num5 = num3 | (b[num4] << 8);
			int num6 = off;
			off++;
			result[num] = num5 | b[num6];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 131, 162, 107, 99, 104, 63, 1, 199 })]
	public static int[] from24LE(byte[] b)
	{
		int[] result = new int[(nint)b.LongLength / 3];
		int off = 0;
		for (int i = 0; i < (nint)result.LongLength; i++)
		{
			int num = i;
			int num2 = off;
			off++;
			byte num3 = b[num2];
			int num4 = off;
			off++;
			int num5 = num3 | (b[num4] << 8);
			int num6 = off;
			off++;
			result[num] = num5 | (b[num6] << 16);
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 138, 98, 107, 99, 104, 53, 199 })]
	public static int[] from16BE(byte[] b)
	{
		int[] result = new int[(nint)b.LongLength >> 1];
		int off = 0;
		for (int i = 0; i < (nint)result.LongLength; i++)
		{
			int num = i;
			int num2 = off;
			off++;
			int num3 = b[num2] << 8;
			int num4 = off;
			off++;
			result[num] = num3 | b[num4];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 133, 98, 107, 99, 104, 53, 199 })]
	public static int[] from16LE(byte[] b)
	{
		int[] result = new int[(nint)b.LongLength >> 1];
		int off = 0;
		for (int i = 0; i < (nint)result.LongLength; i++)
		{
			int num = i;
			int num2 = off;
			off++;
			byte num3 = b[num2];
			int num4 = off;
			off++;
			result[num] = num3 | (b[num4] << 8);
		}
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 125, 66, 107, 99, 104, 117, 116, 242, 61,
		231, 70
	})]
	public static byte[] to24BE(int[] ia)
	{
		byte[] result = new byte[(nint)ia.LongLength * 3];
		int off = 0;
		for (int i = 0; i < (nint)ia.LongLength; i++)
		{
			int num = off;
			off++;
			result[num] = (byte)(sbyte)((uint)(ia[i] >> 16) & 0xFFu);
			int num2 = off;
			off++;
			result[num2] = (byte)(sbyte)((uint)(ia[i] >> 8) & 0xFFu);
			int num3 = off;
			off++;
			result[num3] = (byte)(sbyte)((uint)ia[i] & 0xFFu);
		}
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 120, 162, 107, 99, 104, 114, 116, 245, 61,
		231, 70
	})]
	public static byte[] to24LE(int[] ia)
	{
		byte[] result = new byte[(nint)ia.LongLength * 3];
		int off = 0;
		for (int i = 0; i < (nint)ia.LongLength; i++)
		{
			int num = off;
			off++;
			result[num] = (byte)(sbyte)((uint)ia[i] & 0xFFu);
			int num2 = off;
			off++;
			result[num2] = (byte)(sbyte)((uint)(ia[i] >> 8) & 0xFFu);
			int num3 = off;
			off++;
			result[num3] = (byte)(sbyte)((uint)(ia[i] >> 16) & 0xFFu);
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 128, 98, 107, 99, 104, 116, 18, 231, 69 })]
	public static byte[] to16BE(int[] ia)
	{
		byte[] result = new byte[(nint)ia.LongLength << 1];
		int off = 0;
		for (int i = 0; i < (nint)ia.LongLength; i++)
		{
			int num = off;
			off++;
			result[num] = (byte)(sbyte)((uint)(ia[i] >> 8) & 0xFFu);
			int num2 = off;
			off++;
			result[num2] = (byte)(sbyte)((uint)ia[i] & 0xFFu);
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 122, 66, 107, 99, 104, 114, 20, 231, 69 })]
	public static byte[] to16LE(int[] ia)
	{
		byte[] result = new byte[(nint)ia.LongLength << 1];
		int off = 0;
		for (int i = 0; i < (nint)ia.LongLength; i++)
		{
			int num = off;
			off++;
			result[num] = (byte)(sbyte)((uint)ia[i] & 0xFFu);
			int num2 = off;
			off++;
			result[num2] = (byte)(sbyte)((uint)(ia[i] >> 8) & 0xFFu);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public DataConvert()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 113, 65, 67, 102, 100, 138, 106, 102, 100,
		138, 106
	})]
	public static int[] fromByte(byte[] b, int depth, bool isBe)
	{
		switch (depth)
		{
		case 24:
		{
			if (isBe)
			{
				int[] result3 = from24BE(b);
				
				return result3;
			}
			int[] result4 = from24LE(b);
			
			return result4;
		}
		case 16:
		{
			if (isBe)
			{
				int[] result = from16BE(b);
				
				return result;
			}
			int[] result2 = from16LE(b);
			
			return result2;
		}
		default:
		{
			string msg = new StringBuilder().append("Conversion from ").append(depth).append("bit ")
				.append((!isBe) ? "little endian" : "big endian")
				.append(" is not supported.")
				.toString();
			
			throw new NotSupportedException(msg);
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 65, 67, 102, 100, 138, 106, 102, 100,
		138, 106
	})]
	public static byte[] toByte(int[] ia, int depth, bool isBe)
	{
		switch (depth)
		{
		case 24:
		{
			if (isBe)
			{
				byte[] result3 = to24BE(ia);
				
				return result3;
			}
			byte[] result4 = to24LE(ia);
			
			return result4;
		}
		case 16:
		{
			if (isBe)
			{
				byte[] result = to16BE(ia);
				
				return result;
			}
			byte[] result2 = to16LE(ia);
			
			return result2;
		}
		default:
		{
			string msg = new StringBuilder().append("Conversion to ").append(depth).append("bit ")
				.append((!isBe) ? "little endian" : "big endian")
				.append(" is not supported.")
				.toString();
			
			throw new NotSupportedException(msg);
		}
		}
	}
}
