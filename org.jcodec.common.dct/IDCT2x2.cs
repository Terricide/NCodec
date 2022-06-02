using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.dct;

public class IDCT2x2 : Object
{
	[LineNumberTable(new byte[]
	{
		159, 139, 98, 151, 102, 134, 102, 134, 107, 109,
		109, 109
	})]
	public static void idct(int[] blk, int off)
	{
		int x0 = blk[off];
		int x1 = blk[off + 1];
		int x2 = blk[off + 2];
		int x3 = blk[off + 3];
		int t0 = x0 + x2;
		int t2 = x0 - x2;
		int t1 = x1 + x3;
		int t3 = x1 - x3;
		blk[off] = t0 + t1 >> 3;
		blk[off + 1] = t0 - t1 >> 3;
		blk[off + 2] = t2 + t3 >> 3;
		blk[off + 3] = t2 - t3 >> 3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public IDCT2x2()
	{
	}
}
