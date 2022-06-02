using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common;

public class Ints : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public Ints()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 98, 100, 134, 159, 7 })]
	public static int checkedCast(long value)
	{
		int result = (int)value;
		if (result != value)
		{
			string s = new StringBuilder().append("Out of range: ").append(value).toString();
			
			throw new IllegalArgumentException(s);
		}
		return result;
	}
}
