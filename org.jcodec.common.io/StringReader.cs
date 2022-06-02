using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.common.io;

public abstract class StringReader : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 137, 98, 104, 109, 99 })]
	public static byte[] _sureRead(InputStream input, int len)
	{
		byte[] res = new byte[len];
		if (sureRead(input, res, res.Length) == len)
		{
			return res;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 66, 99, 101, 109, 101, 99, 101, 99 })]
	public static int sureRead(InputStream input, byte[] buf, int len)
	{
		int read;
		int tmp;
		for (read = 0; read < len; read += tmp)
		{
			tmp = input.read(buf, read, len - read);
			if (tmp == -1)
			{
				break;
			}
		}
		return read;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public StringReader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 138, 66, 105 })]
	public static string readString(InputStream input, int len)
	{
		byte[] bs = _sureRead(input, len);
		string result = ((bs != null) ? Platform.stringFromBytes(bs) : null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 162, 102, 110 })]
	public static void sureSkip(InputStream @is, long l)
	{
		while (l > 0u)
		{
			l -= @is.skip(l);
		}
	}
}
