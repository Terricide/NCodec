using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.common.tools;

public class Debug : Object
{
	public static bool debug;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 130, 109, 106, 103, 159, 8 })]
	public static void trace(params object[] arguments)
	{
		if (debug && (nint)arguments.LongLength > 0)
		{
			string format = (string)arguments[0];
			ArrayUtil.shiftLeft1(arguments);
			java.lang.System.@out.printf(new StringBuilder().append(format).append(": %d\n").toString(), arguments);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public Debug()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 138, 66, 99, 103, 103, 127, 3, 5, 199,
		235, 59, 231, 71
	})]
	public static void print8x8i(int[] output)
	{
		int i = 0;
		for (int x = 0; x < 8; x++)
		{
			for (int y = 0; y < 8; y++)
			{
				java.lang.System.@out.printf("%3d, ", Integer.valueOf(output[i]));
				i++;
			}
			java.lang.System.@out.println();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 99, 103, 103, 127, 3, 5, 199,
		235, 59, 231, 71
	})]
	public static void print8x8s(short[] output)
	{
		int i = 0;
		for (int x = 0; x < 8; x++)
		{
			for (int y = 0; y < 8; y++)
			{
				java.lang.System.@out.printf("%3d, ", Short.valueOf(output[i]));
				i++;
			}
			java.lang.System.@out.println();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 103, 103, 63, 6, 167, 235, 60,
		231, 70
	})]
	public static void print8x8sb(ShortBuffer output)
	{
		for (int x = 0; x < 8; x++)
		{
			for (int y = 0; y < 8; y++)
			{
				java.lang.System.@out.printf("%3d, ", Short.valueOf(output.get()));
			}
			java.lang.System.@out.println();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 99, 103, 103, 127, 3, 5, 199,
		235, 59, 231, 71
	})]
	public static void prints(short[] table)
	{
		int i = 0;
		for (int x = 0; x < 8; x++)
		{
			for (int y = 0; y < 8; y++)
			{
				java.lang.System.@out.printf("%3d, ", Short.valueOf(table[i]));
				i++;
			}
			java.lang.System.@out.println();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 66, 104, 110 })]
	public static void printInt(int i)
	{
		if (debug)
		{
			java.lang.System.@out.print(i);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 98, 104, 110 })]
	public static void print(string @string)
	{
		if (debug)
		{
			java.lang.System.@out.print(@string);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 130, 104, 110 })]
	public static void println(string @string)
	{
		if (debug)
		{
			java.lang.System.@out.println(@string);
		}
	}
}
