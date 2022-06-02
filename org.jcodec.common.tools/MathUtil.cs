using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.tools;

public class MathUtil : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] logTab;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] reverseTab;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 132, 66, 99, 106, 103, 134, 106, 102, 133,
		139
	})]
	public static int log2(int v)
	{
		int i = 0;
		if (((uint)v & 0xFFFF0000u) != 0)
		{
			v >>= 16;
			i += 16;
		}
		if (((uint)v & 0xFF00u) != 0)
		{
			v >>= 8;
			i += 8;
		}
		return i + logTab[v];
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(97)]
	public static int clip(int val, int from, int to)
	{
		return (val < from) ? from : ((val <= to) ? val : to);
	}

	[LineNumberTable(106)]
	public static int cubeRoot(int n)
	{
		return 0;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 111, 98, 102 })]
	public static int abs(int val)
	{
		int sign = val >> 31;
		return (val ^ sign) - sign;
	}

	[LineNumberTable(144)]
	public static int wrap(int picNo, int maxFrames)
	{
		return (picNo < 0) ? (picNo + maxFrames) : ((picNo < maxFrames) ? picNo : (picNo - maxFrames));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(148)]
	public static int max3(int a, int b, int c)
	{
		int result = Math.max(Math.max(a, b), c);
		
		return result;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(136)]
	public static int toSigned(int val, int sign)
	{
		return (val ^ sign) - sign;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(140)]
	public static int sign(int val)
	{
		return -(val >> 31);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 110, 130, 100, 99 })]
	public static int golomb(int signedLevel)
	{
		if (signedLevel == 0)
		{
			return 0;
		}
		return (abs(signedLevel) << 1) - (int)((uint)(signedLevel ^ -1) >> 31);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(110)]
	public static int reverse(int b)
	{
		return reverseTab[b & 0xFF];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 162, 100, 150 })]
	public static int gcd(int a, int b)
	{
		if (b != 0)
		{
			int result = gcd(b, (b != -1) ? (a % b) : 0);
			
			return result;
		}
		return a;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 130, 106, 152 })]
	public static long gcdLong(long a, long b)
	{
		if (b != 0u)
		{
			long result = gcdLong(b, (b != -1) ? (a % b) : 0);
			
			return result;
		}
		return a;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MathUtil()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 129, 162, 99, 112, 103, 134, 109, 103, 134,
		109, 102, 133, 140
	})]
	public static int log2l(long v)
	{
		int i = 0;
		if ((v & -4294967296L) != 0u)
		{
			v >>= 32;
			i += 32;
		}
		if ((v & 0xFFFF0000u) != 0u)
		{
			v >>= 16;
			i += 16;
		}
		if ((v & 0xFF00u) != 0u)
		{
			v >>= 8;
			i += 8;
		}
		return i + logTab[(int)v];
	}

	[LineNumberTable(new byte[] { 159, 124, 130, 99, 106, 102, 135 })]
	public static int log2Slow(int val)
	{
		int i = 0;
		while ((val & int.MinValue) == 0)
		{
			val <<= 1;
			i++;
		}
		return 31 - i;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(101)]
	public static int clipMax(int val, int max)
	{
		return (val >= max) ? max : val;
	}

	[LineNumberTable(new byte[] { 159, 114, 130, 102, 104, 104, 104, 104, 105, 102 })]
	public static int nextPowerOfTwo(int n)
	{
		n--;
		n |= n >> 1;
		n |= n >> 2;
		n |= n >> 4;
		n |= n >> 8;
		n |= n >> 16;
		n++;
		return n;
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		130,
		byte.MaxValue,
		166,
		40,
		73
	})]
	static MathUtil()
	{
		logTab = new int[256]
		{
			0, 0, 1, 1, 2, 2, 2, 2, 3, 3,
			3, 3, 3, 3, 3, 3, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7
		};
		reverseTab = new int[256]
		{
			0, 128, 64, 192, 32, 160, 96, 224, 16, 144,
			80, 208, 48, 176, 112, 240, 8, 136, 72, 200,
			40, 168, 104, 232, 24, 152, 88, 216, 56, 184,
			120, 248, 4, 132, 68, 196, 36, 164, 100, 228,
			20, 148, 84, 212, 52, 180, 116, 244, 12, 140,
			76, 204, 44, 172, 108, 236, 28, 156, 92, 220,
			60, 188, 124, 252, 2, 130, 66, 194, 34, 162,
			98, 226, 18, 146, 82, 210, 50, 178, 114, 242,
			10, 138, 74, 202, 42, 170, 106, 234, 26, 154,
			90, 218, 58, 186, 122, 250, 6, 134, 70, 198,
			38, 166, 102, 230, 22, 150, 86, 214, 54, 182,
			118, 246, 14, 142, 78, 206, 46, 174, 110, 238,
			30, 158, 94, 222, 62, 190, 126, 254, 1, 129,
			65, 193, 33, 161, 97, 225, 17, 145, 81, 209,
			49, 177, 113, 241, 9, 137, 73, 201, 41, 169,
			105, 233, 25, 153, 89, 217, 57, 185, 121, 249,
			5, 133, 69, 197, 37, 165, 101, 229, 21, 149,
			85, 213, 53, 181, 117, 245, 13, 141, 77, 205,
			45, 173, 109, 237, 29, 157, 93, 221, 61, 189,
			125, 253, 3, 131, 67, 195, 35, 163, 99, 227,
			19, 147, 83, 211, 51, 179, 115, 243, 11, 139,
			75, 203, 43, 171, 107, 235, 27, 155, 91, 219,
			59, 187, 123, 251, 7, 135, 71, 199, 39, 167,
			103, 231, 23, 151, 87, 215, 55, 183, 119, 247,
			15, 143, 79, 207, 47, 175, 111, 239, 31, 159,
			95, 223, 63, 191, 127, 255
		};
	}
}
