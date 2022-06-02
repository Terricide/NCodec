using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.mjpeg.tools;

public class Asserts : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public Asserts()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 66, 101, 191, 23 })]
	public static void assertEquals(int expected, int actual)
	{
		if (expected != actual)
		{
			string @string = new StringBuilder().append("assert failed: ").append(expected).append(" != ")
				.append(actual)
				.toString();
			
			throw new AssertionException(@string);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 141 })]
	public static void assertInRange(string message, int low, int up, int val)
	{
		if (val < low || val > up)
		{
			
			throw new AssertionException(message);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		136,
		98,
		103,
		113,
		107,
		101,
		101,
		108,
		byte.MaxValue,
		55,
		60,
		234,
		72
	})]
	public static void assertEpsilonEqualsInt(int[] expected, int[] actual, int eps)
	{
		if ((nint)expected.LongLength != (nint)actual.LongLength)
		{
			
			throw new AssertionException("arrays of different size");
		}
		for (int i = 0; i < (nint)expected.LongLength; i++)
		{
			int e = expected[i];
			int a = actual[i];
			if (Math.abs(e - a) > eps)
			{
				string @string = new StringBuilder().append("array element ").append(i).append(" ")
					.append(e)
					.append(" != ")
					.append(a)
					.append(" out of expected diff range ")
					.append(eps)
					.toString();
				
				throw new AssertionException(@string);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 162, 103, 113, 104, 101, 101, 108, 147,
		251, 58, 231, 73
	})]
	public static void assertEpsilonEquals(byte[] expected, byte[] actual, int eps)
	{
		if ((nint)expected.LongLength != (nint)actual.LongLength)
		{
			
			throw new AssertionException("arrays of different size");
		}
		for (int i = 0; i < (nint)expected.LongLength; i++)
		{
			int e = expected[i];
			int a = actual[i];
			if (Math.abs(e - a) > eps)
			{
				string @string = new StringBuilder().append("array element out of expected diff range: ").append(Math.abs(e - a)).toString();
				
				throw new AssertionException(@string);
			}
		}
	}
}
