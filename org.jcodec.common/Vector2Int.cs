using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common;

public class Vector2Int : Object
{
	[LineNumberTable(14)]
	public static int el16_0(int packed)
	{
		return packed << 16 >> 16;
	}

	[LineNumberTable(18)]
	public static int el16_1(int packed)
	{
		return packed >> 16;
	}

	[LineNumberTable(48)]
	public static int pack16(int el0, int el1)
	{
		return ((el1 & 0xFFFF) << 16) | (el0 & 0xFFFF);
	}

	[LineNumberTable(31)]
	public static int set16_0(int packed, int el)
	{
		return (packed & -65536) | (el & 0xFFFF);
	}

	[LineNumberTable(35)]
	public static int set16_1(int packed, int el)
	{
		return (packed & 0xFFFF) | ((el & 0xFFFF) << 16);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public Vector2Int()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 140, 138 })]
	public static int el16(int packed, int n)
	{
		if (n == 0)
		{
			int result = el16_0(packed);
			
			return result;
		}
		int result2 = el16_1(packed);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 140, 139 })]
	public static int set16(int packed, int el, int n)
	{
		if (n == 0)
		{
			int result = set16_0(packed, el);
			
			return result;
		}
		int result2 = set16_1(packed, el);
		
		return result2;
	}
}
