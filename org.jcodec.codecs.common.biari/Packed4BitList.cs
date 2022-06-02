using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.common.biari;

public class Packed4BitList : Object
{
	private static int[] CLEAR_MASK;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(21)]
	public static int _7(int val0, int val1, int val2, int val3, int val4, int val5, int val6)
	{
		return 0x70000000 | ((val0 & 0xF) << 24) | ((val1 & 0xF) << 20) | ((val2 & 0xF) << 16) | ((val3 & 0xF) << 12) | ((val4 & 0xF) << 8) | ((val5 & 0xF) << 4) | (val6 & 0xF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public Packed4BitList()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public static int _3(int val0, int val1, int val2)
	{
		int result = _7(val0, val1, val2, 0, 0, 0, 0);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 133, 130, 105, 101, 106 })]
	public static int set(int list, int val, int n)
	{
		int cnt = (list >> 28) & 0xF;
		int newc = n + 1;
		cnt = ((newc <= cnt) ? cnt : newc);
		return (list & CLEAR_MASK[n]) | ((val & 0xFF) << (n << 2)) | (cnt << 28);
	}

	[LineNumberTable(new byte[] { 159, 131, 98, 101, 99 })]
	public static int get(int list, int n)
	{
		if (n > 6)
		{
			return 0;
		}
		return (list >> (n << 2)) & 0xFF;
	}

	[LineNumberTable(12)]
	static Packed4BitList()
	{
		CLEAR_MASK = new int[7] { 268435440, -16, -16, -16, -16, -16, -16 };
	}
}
