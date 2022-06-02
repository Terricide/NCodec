using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.security;

namespace org.jcodec.common.tools;

public class MD5 : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 190, 3, 98, 141 })]
	public static MessageDigest getDigest()
	{
		NoSuchAlgorithmException ex;
		try
		{
			return MessageDigest.getInstance("MD5");
		}
		catch (NoSuchAlgorithmException x)
		{
			ex = ByteCodeHelper.MapException<NoSuchAlgorithmException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		NoSuchAlgorithmException e = ex;
		
		throw new RuntimeException(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 103, 104, 101, 105, 102, 109, 238,
		59, 231, 72
	})]
	private static string digestToString(byte[] digest)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < (nint)digest.LongLength; i++)
		{
			int item = digest[i];
			int b = item & 0xFF;
			if (b < 16)
			{
				sb.append("0");
			}
			sb.append(Integer.toHexString(b));
		}
		string result = sb.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public MD5()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 103, 104 })]
	public static string md5sumBytes(byte[] bytes)
	{
		MessageDigest md5 = getDigest();
		md5.update(bytes);
		string result = digestToString(md5.digest());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 103, 104, 104 })]
	public static string md5sum(ByteBuffer bytes)
	{
		MessageDigest md5 = getDigest();
		md5.update(bytes);
		byte[] digest = md5.digest();
		string result = digestToString(digest);
		
		return result;
	}
}
