using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common;

public class Vector4Int : Object
{
	[LineNumberTable(14)]
	public static int el8_0(int packed)
	{
		return packed << 24 >> 24;
	}

	[LineNumberTable(18)]
	public static int el8_1(int packed)
	{
		return packed << 16 >> 24;
	}

	[LineNumberTable(22)]
	public static int el8_2(int packed)
	{
		return packed << 8 >> 24;
	}

	[LineNumberTable(26)]
	public static int el8_3(int packed)
	{
		return packed >> 24;
	}

	[LineNumberTable(72)]
	public static int pack8(int el0, int el1, int el2, int el3)
	{
		return ((el3 & 0xFF) << 24) | ((el2 & 0xFF) << 16) | ((el1 & 0xFF) << 8) | (el0 & 0xFF);
	}

	[LineNumberTable(43)]
	public static int set8_0(int packed, int el)
	{
		return (packed & -256) | (el & 0xFF);
	}

	[LineNumberTable(47)]
	public static int set8_1(int packed, int el)
	{
		return (packed & -65281) | ((el & 0xFF) << 8);
	}

	[LineNumberTable(51)]
	public static int set8_2(int packed, int el)
	{
		return (packed & -16711681) | ((el & 0xFF) << 16);
	}

	[LineNumberTable(55)]
	public static int set8_3(int packed, int el)
	{
		return (packed & -16711681) | ((el & 0xFF) << 24);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public Vector4Int()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 149, 138, 138, 138 })]
	public static int el8(int packed, int n)
	{
		switch (n)
		{
		case 0:
		{
			int result4 = el8_0(packed);
			
			return result4;
		}
		case 1:
		{
			int result3 = el8_1(packed);
			
			return result3;
		}
		case 2:
		{
			int result2 = el8_2(packed);
			
			return result2;
		}
		default:
		{
			int result = el8_3(packed);
			
			return result;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 149, 139, 139, 139 })]
	public static int set8(int packed, int el, int n)
	{
		switch (n)
		{
		case 0:
		{
			int result4 = set8_0(packed, el);
			
			return result4;
		}
		case 1:
		{
			int result3 = set8_1(packed, el);
			
			return result3;
		}
		case 2:
		{
			int result2 = set8_2(packed, el);
			
			return result2;
		}
		default:
		{
			int result = set8_3(packed, el);
			
			return result;
		}
		}
	}
}
