using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264;

public class H264Utils2 : Object
{
	[LineNumberTable(new byte[] { 159, 141, 130, 105, 108 })]
	public static int golomb2Signed(int val)
	{
		int sign = ((val & 1) << 1) - 1;
		val = ((val >> 1) + (val & 1)) * sign;
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public H264Utils2()
	{
	}
}
