using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.nio;

namespace org.jcodec.common.dct;

public class IntDCT : DCT
{
	internal static IntDCT ___003C_003EINSTANCE;

	private const int DCTSIZE = 8;

	private const int PASS1_BITS = 2;

	private const int MAXJSAMPLE = 255;

	private const int CENTERJSAMPLE = 128;

	private const int RANGE_MASK = 1023;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static IntBuffer sample_range_limit;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static IntBuffer idct_sample_range_limit;

	private const int CONST_BITS = 13;

	private const int ONE_HALF = 4096;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_0_298631336;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_0_390180644;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_0_541196100;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_0_765366865;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_0_899976223;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_1_175875602;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_1_501321110;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_1_847759065;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_1_961570560;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_2_053119869;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_2_562915447;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int FIX_3_072711026;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static IntDCT INSTANCE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EINSTANCE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 109, 109 })]
	protected internal static IntBuffer doDecode(IntBuffer inptr, IntBuffer workspace, IntBuffer outptr)
	{
		pass1(inptr, workspace.duplicate());
		pass2(outptr, workspace.duplicate());
		return outptr;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 95, 130, 234, 70, 106, 106, 111, 113, 112,
		105, 106, 105, 105, 104, 104, 104, 232, 70, 107,
		107, 107, 106, 103, 103, 103, 104, 145, 111, 111,
		111, 111, 110, 110, 110, 112, 102, 136, 105, 106,
		105, 202, 101, 117, 118, 117, 118, 118, 118, 118,
		118, 105, 233, 2, 234, 160, 64
	})]
	private static void pass1(IntBuffer inptr, IntBuffer wsptr)
	{
		for (int ctr = 8; ctr > 0; ctr += -1)
		{
			int z2 = inptr.get(16);
			int z3 = inptr.get(48);
			int z1 = MULTIPLY(z2 + z3, FIX_0_541196100);
			int tmp6 = z1 + MULTIPLY(z3, -FIX_1_847759065);
			int tmp7 = z1 + MULTIPLY(z2, FIX_0_765366865);
			z2 = inptr.get(0);
			z3 = inptr.get(32);
			int tmp0 = z2 + z3 << 13;
			int tmp1 = z2 - z3 << 13;
			int tmp2 = tmp0 + tmp7;
			int tmp5 = tmp0 - tmp7;
			int tmp3 = tmp1 + tmp6;
			int tmp4 = tmp1 - tmp6;
			tmp0 = inptr.get(56);
			tmp1 = inptr.get(40);
			tmp6 = inptr.get(24);
			tmp7 = inptr.get(8);
			z1 = tmp0 + tmp7;
			z2 = tmp1 + tmp6;
			z3 = tmp0 + tmp6;
			int z4 = tmp1 + tmp7;
			int z5 = MULTIPLY(z3 + z4, FIX_1_175875602);
			tmp0 = MULTIPLY(tmp0, FIX_0_298631336);
			tmp1 = MULTIPLY(tmp1, FIX_2_053119869);
			tmp6 = MULTIPLY(tmp6, FIX_3_072711026);
			tmp7 = MULTIPLY(tmp7, FIX_1_501321110);
			z1 = MULTIPLY(z1, -FIX_0_899976223);
			z2 = MULTIPLY(z2, -FIX_2_562915447);
			z3 = MULTIPLY(z3, -FIX_1_961570560);
			z4 = MULTIPLY(z4, -FIX_0_390180644);
			z3 += z5;
			z4 += z5;
			tmp0 += z1 + z3;
			tmp1 += z2 + z4;
			tmp6 += z2 + z3;
			tmp7 += z1 + z4;
			int D = 11;
			wsptr.put(0, DESCALE(tmp2 + tmp7, D));
			wsptr.put(56, DESCALE(tmp2 - tmp7, D));
			wsptr.put(8, DESCALE(tmp3 + tmp6, D));
			wsptr.put(48, DESCALE(tmp3 - tmp6, D));
			wsptr.put(16, DESCALE(tmp4 + tmp1, D));
			wsptr.put(40, DESCALE(tmp4 - tmp1, D));
			wsptr.put(24, DESCALE(tmp5 + tmp0, D));
			wsptr.put(32, DESCALE(tmp5 - tmp0, D));
			inptr = advance(inptr);
			wsptr = advance(wsptr);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 202, 105, 137, 111, 113, 144, 117,
		149, 104, 104, 104, 232, 71, 106, 106, 106, 138,
		103, 103, 103, 104, 145, 111, 111, 111, 111, 110,
		110, 110, 144, 102, 136, 105, 106, 105, 202, 101,
		127, 0, 127, 0, 127, 0, 127, 0, 127, 0,
		127, 0, 127, 0, 159, 0, 234, 0, 234, 160,
		67
	})]
	private static void pass2(IntBuffer outptr, IntBuffer wsptr)
	{
		for (int ctr = 0; ctr < 8; ctr++)
		{
			int z2 = wsptr.get(2);
			int z3 = wsptr.get(6);
			int z1 = MULTIPLY(z2 + z3, FIX_0_541196100);
			int tmp6 = z1 + MULTIPLY(z3, -FIX_1_847759065);
			int tmp7 = z1 + MULTIPLY(z2, FIX_0_765366865);
			int tmp0 = wsptr.get(0) + wsptr.get(4) << 13;
			int tmp1 = wsptr.get(0) - wsptr.get(4) << 13;
			int tmp2 = tmp0 + tmp7;
			int tmp5 = tmp0 - tmp7;
			int tmp3 = tmp1 + tmp6;
			int tmp4 = tmp1 - tmp6;
			tmp0 = wsptr.get(7);
			tmp1 = wsptr.get(5);
			tmp6 = wsptr.get(3);
			tmp7 = wsptr.get(1);
			z1 = tmp0 + tmp7;
			z2 = tmp1 + tmp6;
			z3 = tmp0 + tmp6;
			int z4 = tmp1 + tmp7;
			int z5 = MULTIPLY(z3 + z4, FIX_1_175875602);
			tmp0 = MULTIPLY(tmp0, FIX_0_298631336);
			tmp1 = MULTIPLY(tmp1, FIX_2_053119869);
			tmp6 = MULTIPLY(tmp6, FIX_3_072711026);
			tmp7 = MULTIPLY(tmp7, FIX_1_501321110);
			z1 = MULTIPLY(z1, -FIX_0_899976223);
			z2 = MULTIPLY(z2, -FIX_2_562915447);
			z3 = MULTIPLY(z3, -FIX_1_961570560);
			z4 = MULTIPLY(z4, -FIX_0_390180644);
			z3 += z5;
			z4 += z5;
			tmp0 += z1 + z3;
			tmp1 += z2 + z4;
			tmp6 += z2 + z3;
			tmp7 += z1 + z4;
			int D = 18;
			outptr.put(range_limit(DESCALE(tmp2 + tmp7, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp3 + tmp6, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp4 + tmp1, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp5 + tmp0, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp5 - tmp0, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp4 - tmp1, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp3 - tmp6, D) & 0x3FF));
			outptr.put(range_limit(DESCALE(tmp2 - tmp7, D) & 0x3FF));
			wsptr = doAdvance(wsptr, 8);
		}
	}

	[LineNumberTable(277)]
	private static int MULTIPLY(int i, int j)
	{
		return i * j;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(269)]
	internal static int DESCALE(int x, int n)
	{
		int result = RIGHT_SHIFT(x + (1 << n - 1), n);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(112)]
	public static int range_limit(int i)
	{
		int result = idct_sample_range_limit.get(i + 256);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 66, 112 })]
	private static IntBuffer doAdvance(IntBuffer ptr, int size)
	{
		ptr.position(ptr.position() + size);
		IntBuffer result = ptr.slice();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(260)]
	private static IntBuffer advance(IntBuffer ptr)
	{
		IntBuffer result = doAdvance(ptr, 1);
		
		return result;
	}

	[LineNumberTable(273)]
	private static int RIGHT_SHIFT(int x, int shft)
	{
		return x >> shft;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public IntDCT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 113, 107, 45, 167, 104, 45, 167,
		107, 45, 167, 107, 45, 167, 109, 46, 201, 114,
		63, 5, 201
	})]
	private static void prepare_range_limit_table()
	{
		sample_range_limit.position(256);
		for (int n = 0; n < 128; n++)
		{
			sample_range_limit.put(n);
		}
		for (int m = -128; m < 0; m++)
		{
			sample_range_limit.put(m);
		}
		for (int l = 0; l < 384; l++)
		{
			sample_range_limit.put(-1);
		}
		for (int k = 0; k < 384; k++)
		{
			sample_range_limit.put(0);
		}
		for (int j = 0; j < 128; j++)
		{
			sample_range_limit.put(j);
		}
		for (int i = 0; i < idct_sample_range_limit.capacity(); i++)
		{
			idct_sample_range_limit.put(sample_range_limit.get(i + 128) & 0xFF);
		}
	}

	[LineNumberTable(283)]
	private static int FIX(double x)
	{
		return ByteCodeHelper.d2i(x * 8192.0 + 0.5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 104, 105, 105, 106 })]
	public override int[] decode(int[] orig)
	{
		IntBuffer inptr = IntBuffer.wrap(orig);
		IntBuffer workspace = IntBuffer.allocate(64);
		IntBuffer outptr = IntBuffer.allocate(64);
		doDecode(inptr, workspace, outptr);
		int[] result = outptr.array();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 162, 125, 123, 123, 139, 139, 106, 106,
		107, 107, 107, 107, 107, 139, 105, 105, 131
	})]
	private static bool shortcut(IntBuffer inptr, IntBuffer wsptr)
	{
		if (inptr.get(8) == 0 && inptr.get(16) == 0 && inptr.get(24) == 0 && inptr.get(32) == 0 && inptr.get(40) == 0 && inptr.get(48) == 0 && inptr.get(56) == 0)
		{
			int dcval = inptr.get(0) << 2;
			wsptr.put(0, dcval);
			wsptr.put(8, dcval);
			wsptr.put(16, dcval);
			wsptr.put(24, dcval);
			wsptr.put(32, dcval);
			wsptr.put(40, dcval);
			wsptr.put(48, dcval);
			wsptr.put(56, dcval);
			inptr = advance(inptr);
			wsptr = advance(wsptr);
			return true;
		}
		return false;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 66, 235, 160, 103, 102, 107, 102, 150,
		230, 160, 166, 116, 116, 116, 116, 116, 116, 116,
		116, 116, 116, 116
	})]
	static IntDCT()
	{
		___003C_003EINSTANCE = new IntDCT();
		sample_range_limit = IntBuffer.allocate(1408);
		idct_sample_range_limit = IntBuffer.allocate(sample_range_limit.capacity() - 128);
		prepare_range_limit_table();
		FIX_0_298631336 = FIX(0.298631336);
		FIX_0_390180644 = FIX(0.390180644);
		FIX_0_541196100 = FIX(0.5411961);
		FIX_0_765366865 = FIX(0.765366865);
		FIX_0_899976223 = FIX(0.899976223);
		FIX_1_175875602 = FIX(1.175875602);
		FIX_1_501321110 = FIX(1.50132111);
		FIX_1_847759065 = FIX(1.847759065);
		FIX_1_961570560 = FIX(1.96157056);
		FIX_2_053119869 = FIX(2.053119869);
		FIX_2_562915447 = FIX(2.562915447);
		FIX_3_072711026 = FIX(3.072711026);
	}
}
