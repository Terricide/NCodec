using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class MV : Object
{
	[LineNumberTable(13)]
	public static int x(int mv)
	{
		return mv << 18 >> 18;
	}

	[LineNumberTable(17)]
	public static int y(int mv)
	{
		return mv << 4 >> 18;
	}

	[LineNumberTable(21)]
	public static int @ref(int mv)
	{
		return (mv >> 28) & 3;
	}

	[LineNumberTable(9)]
	public static int create(int x, int y, int @ref)
	{
		return (@ref << 28) | ((y & 0x3FFF) << 14) | (x & 0x3FFF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(6)]
	public MV()
	{
	}
}
