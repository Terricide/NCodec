using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.huffman;

public class Huffman : Object, Codebooks
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static bool[] UNSIGNED;

	private const int QUAD_LEN = 4;

	private const int PAIR_LEN = 2;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB1
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB1;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB2
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB2;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB3
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB3;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB4
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB4;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB5
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB5;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB6
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB6;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB7
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB7;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB8
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB8;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB9
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB9;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB10
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB10;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB11
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB11;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] HCB_SF
	{
		[HideFromJava]
		get
		{
			return Codebooks.HCB_SF;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][][] CODEBOOKS
	{
		[HideFromJava]
		get
		{
			return Codebooks.CODEBOOKS;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 99, 103, 137, 105, 101, 105, 103,
		104, 141
	})]
	private static int findOffset(IBitStream _in, int[][] table)
	{
		int off = 0;
		int len = table[off][0];
		int cw = _in.readBits(len);
		while (cw != table[off][1])
		{
			off++;
			int i = table[off][0] - len;
			len = table[off][0];
			cw <<= i;
			cw |= _in.readBits(i);
		}
		return off;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 133, 130, 105, 102, 16, 231, 69 })]
	private static void signValues(IBitStream _in, int[] data, int off, int len)
	{
		for (int i = off; i < off + len; i++)
		{
			if (data[i] != 0 && _in.readBool())
			{
				data[i] = -data[i];
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 131, 130, 138, 99, 105, 135, 144 })]
	private static int getEscape(IBitStream _in, int s)
	{
		int neg = ((s < 0) ? 1 : 0);
		int i = 4;
		while (_in.readBool())
		{
			i++;
		}
		int j = _in.readBits(i) | (1 << i);
		return (neg == 0) ? j : (-j);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105 })]
	private Huffman()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 128, 130, 109 })]
	public static int decodeScaleFactor(IBitStream _in)
	{
		int offset = findOffset(_in, Codebooks.HCB_SF);
		return Codebooks.HCB_SF[offset][2];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 127, 162, 171, 169, 105, 107, 101, 107, 203,
		102, 159, 5, 107, 113, 121, 159, 2, 127, 7
	})]
	public static void decodeSpectralData(IBitStream _in, int cb, int[] data, int off)
	{
		int[][] HCB = Codebooks.CODEBOOKS[cb - 1];
		int offset = findOffset(_in, HCB);
		data[off] = HCB[offset][2];
		data[off + 1] = HCB[offset][3];
		if (cb < 5)
		{
			data[off + 2] = HCB[offset][4];
			data[off + 3] = HCB[offset][5];
		}
		if (cb < 11)
		{
			if (UNSIGNED[cb - 1])
			{
				signValues(_in, data, off, (cb >= 5) ? 2 : 4);
			}
			return;
		}
		if (cb == 11 || cb > 15)
		{
			signValues(_in, data, off, (cb >= 5) ? 2 : 4);
			if (Math.abs(data[off]) == 16)
			{
				data[off] = getEscape(_in, data[off]);
			}
			if (Math.abs(data[off + 1]) == 16)
			{
				data[off + 1] = getEscape(_in, data[off + 1]);
			}
			return;
		}
		string message = new StringBuilder().append("Huffman: unknown spectral codebook: ").append(cb).toString();
		
		throw new AACException(message);
	}

	[LineNumberTable(16)]
	static Huffman()
	{
		UNSIGNED = new bool[11]
		{
			false, false, true, true, false, false, true, true, true, true,
			true
		};
	}
}
