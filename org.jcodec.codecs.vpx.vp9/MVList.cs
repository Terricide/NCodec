using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class MVList : Object
{
	private static long LO_MASK;

	private static long HI_MASK;

	private static long HI_MASK_NEG;

	private static long LO_MASK_NEG;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 130, 130, 100, 138 })]
	public static int get(long list, int n)
	{
		if (n == 0)
		{
			return (int)(list & LO_MASK);
		}
		return (int)((list >> 31) & LO_MASK);
	}

	[LineNumberTable(20)]
	public static long create(int mv0, int mv1)
	{
		return long.MinValue | ((long)mv1 << 31) | (mv0 & LO_MASK);
	}

	[LineNumberTable(68)]
	public static int size(long list)
	{
		return (int)((list >> 62) & 3u);
	}

	[LineNumberTable(new byte[]
	{
		159, 136, 66, 105, 102, 99, 102, 156, 106, 101,
		159, 0
	})]
	public static long addUniq(long list, int mv)
	{
		switch ((list >> 62) & 3u)
		{
		case 2L:
			return list;
		case 0L:
			return 0x4000000000000000L | (list & LO_MASK_NEG) | (mv & LO_MASK);
		default:
		{
			int first = (int)(list & LO_MASK);
			if (first != mv)
			{
				return long.MinValue | (list & HI_MASK_NEG) | (((long)mv << 31) & HI_MASK);
			}
			return list;
		}
		}
	}

	[LineNumberTable(new byte[] { 159, 133, 162, 105, 102, 99, 102, 156 })]
	public static long add(long list, int mv)
	{
		return ((list >> 62) & 3u) switch
		{
			2L => list, 
			0L => 0x4000000000000000L | (list & LO_MASK_NEG) | (mv & LO_MASK), 
			_ => long.MinValue | (list & HI_MASK_NEG) | (((long)mv << 31) & HI_MASK), 
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public MVList()
	{
	}

	[LineNumberTable(new byte[] { 159, 128, 98, 105, 102, 106, 100, 151 })]
	public static long set(long list, int n, int mv)
	{
		long cnt = (list >> 62) & 3u;
		long newc = n + 1;
		cnt = ((newc <= cnt) ? cnt : newc);
		if (n == 0)
		{
			return (cnt << 62) | (list & LO_MASK_NEG) | (mv & LO_MASK);
		}
		return (cnt << 62) | (list & HI_MASK_NEG) | (((long)mv << 31) & HI_MASK);
	}

	[LineNumberTable(new byte[] { 159, 139, 130, 108, 110, 120 })]
	static MVList()
	{
		LO_MASK = 2147483647L;
		HI_MASK = LO_MASK << 31;
		HI_MASK_NEG = (HI_MASK | -4611686018427387904L) ^ -1;
		LO_MASK_NEG = (LO_MASK | -4611686018427387904L) ^ -1;
	}
}
