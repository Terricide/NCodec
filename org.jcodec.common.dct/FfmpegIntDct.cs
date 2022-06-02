using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.common.dct;

public class FfmpegIntDct : Object
{
	private const int DCTSIZE = 8;

	private const int DCTSIZE_6 = 48;

	private const int DCTSIZE_5 = 40;

	private const int DCTSIZE_4 = 32;

	private const int DCTSIZE_3 = 24;

	private const int DCTSIZE_2 = 16;

	private const int DCTSIZE_1 = 8;

	private const int DCTSIZE_7 = 56;

	private const int DCTSIZE_0 = 0;

	private const int PASS1_BITS = 2;

	private const int CONST_BITS = 13;

	private const int D1 = 11;

	private const int D2 = 18;

	private const int ONEHALF_11 = 1024;

	private const int ONEHALF_18 = 131072;

	private const short FIX_0_211164243 = 1730;

	private const short FIX_0_275899380 = 2260;

	private const short FIX_0_298631336 = 2446;

	private const short FIX_0_390180644 = 3196;

	private const short FIX_0_509795579 = 4176;

	private const short FIX_0_541196100 = 4433;

	private const short FIX_0_601344887 = 4926;

	private const short FIX_0_765366865 = 6270;

	private const short FIX_0_785694958 = 6436;

	private const short FIX_0_899976223 = 7373;

	private const short FIX_1_061594337 = 8697;

	private const short FIX_1_111140466 = 9102;

	private const short FIX_1_175875602 = 9633;

	private const short FIX_1_306562965 = 10703;

	private const short FIX_1_387039845 = 11363;

	private const short FIX_1_451774981 = 11893;

	private const short FIX_1_501321110 = 12299;

	private const short FIX_1_662939225 = 13623;

	private const short FIX_1_847759065 = 15137;

	private const short FIX_1_961570560 = 16069;

	private const short FIX_2_053119869 = 16819;

	private const short FIX_2_172734803 = 17799;

	private const short FIX_2_562915447 = 20995;

	private const short FIX_3_072711026 = 25172;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 136, 234, 80, 105, 105, 106, 106,
		106, 106, 106, 138, 150, 132, 102, 105, 45, 233,
		71, 105, 230, 69, 104, 135, 113, 115, 145, 106,
		138, 104, 104, 104, 173, 112, 143, 106, 138, 104,
		104, 104, 173, 135, 110, 142, 106, 138, 104, 104,
		104, 170, 109, 237, 73, 104, 104, 104, 136, 104,
		104, 104, 104, 146, 111, 111, 111, 111, 112, 112,
		112, 144, 104, 136, 107, 107, 107, 176, 104, 104,
		146, 111, 111, 111, 112, 112, 112, 144, 104, 136,
		107, 107, 107, 173, 136, 104, 104, 146, 111, 111,
		111, 112, 112, 112, 144, 104, 136, 107, 107, 104,
		176, 112, 112, 112, 112, 112, 112, 146, 104, 136,
		104, 104, 104, 205, 104, 136, 104, 104, 146, 111,
		111, 111, 112, 112, 112, 144, 104, 136, 107, 104,
		107, 176, 136, 112, 112, 111, 112, 111, 144, 104,
		104, 104, 173, 136, 104, 143, 111, 112, 112, 112,
		143, 104, 104, 104, 173, 112, 111, 112, 244, 69,
		104, 104, 136, 104, 104, 146, 111, 111, 111, 112,
		112, 112, 144, 104, 136, 104, 107, 107, 176, 136,
		111, 111, 112, 112, 111, 144, 104, 104, 104, 173,
		136, 136, 111, 112, 111, 112, 112, 143, 104, 104,
		104, 173, 111, 111, 112, 212, 104, 136, 104, 111,
		112, 111, 112, 111, 143, 104, 104, 104, 173, 112,
		112, 112, 177, 133, 111, 111, 111, 177, 237, 71,
		115, 115, 115, 115, 115, 115, 115, 147, 233, 158,
		151, 234, 161, 108
	})]
	private static void pass1(ShortBuffer data)
	{
		ShortBuffer dataptr = data.duplicate();
		for (int rowctr = 7; rowctr >= 0; rowctr += -1)
		{
			int d0 = dataptr.get(0);
			int d2 = dataptr.get(1);
			int d4 = dataptr.get(2);
			int d6 = dataptr.get(3);
			int d1 = dataptr.get(4);
			int d3 = dataptr.get(5);
			int d5 = dataptr.get(6);
			int d7 = dataptr.get(7);
			if ((d1 | d2 | d3 | d4 | d5 | d6 | d7) == 0)
			{
				if (d0 != 0)
				{
					int dcval = d0 << 2;
					for (int i = 0; i < 8; i++)
					{
						dataptr.put(i, (short)dcval);
					}
				}
				dataptr = advance(dataptr, 8);
				continue;
			}
			int tmp22;
			int tmp25;
			int tmp23;
			int tmp24;
			if (d6 != 0)
			{
				if (d2 != 0)
				{
					int z11 = MULTIPLY(d2 + d6, 4433);
					int tmp36 = z11 + MULTIPLY(-d6, 15137);
					int tmp46 = z11 + MULTIPLY(d2, 6270);
					int tmp10 = d0 + d4 << 13;
					int tmp21 = d0 - d4 << 13;
					tmp22 = tmp10 + tmp46;
					tmp25 = tmp10 - tmp46;
					tmp23 = tmp21 + tmp36;
					tmp24 = tmp21 - tmp36;
				}
				else
				{
					int tmp35 = MULTIPLY(-d6, 10703);
					int tmp45 = MULTIPLY(d6, 4433);
					int tmp9 = d0 + d4 << 13;
					int tmp20 = d0 - d4 << 13;
					tmp22 = tmp9 + tmp45;
					tmp25 = tmp9 - tmp45;
					tmp23 = tmp20 + tmp35;
					tmp24 = tmp20 - tmp35;
				}
			}
			else if (d2 != 0)
			{
				int tmp34 = MULTIPLY(d2, 4433);
				int tmp44 = MULTIPLY(d2, 10703);
				int tmp8 = d0 + d4 << 13;
				int tmp19 = d0 - d4 << 13;
				tmp22 = tmp8 + tmp44;
				tmp25 = tmp8 - tmp44;
				tmp23 = tmp19 + tmp34;
				tmp24 = tmp19 - tmp34;
			}
			else
			{
				tmp22 = (tmp25 = d0 + d4 << 13);
				tmp23 = (tmp24 = d0 - d4 << 13);
			}
			int tmp6;
			int tmp17;
			int tmp32;
			int tmp43;
			if (d7 != 0)
			{
				if (d5 != 0)
				{
					if (d3 != 0)
					{
						if (d1 != 0)
						{
							int z10 = d7 + d1;
							int z21 = d5 + d3;
							int z30 = d7 + d3;
							int z40 = d5 + d1;
							int z51 = MULTIPLY(z30 + z40, 9633);
							int tmp7 = MULTIPLY(d7, 2446);
							int tmp18 = MULTIPLY(d5, 16819);
							int tmp33 = MULTIPLY(d3, 25172);
							int num = MULTIPLY(d1, 12299);
							z10 = MULTIPLY(-z10, 7373);
							z21 = MULTIPLY(-z21, 20995);
							z30 = MULTIPLY(-z30, 16069);
							z40 = MULTIPLY(-z40, 3196);
							z30 += z51;
							z40 += z51;
							tmp6 = tmp7 + (z10 + z30);
							tmp17 = tmp18 + (z21 + z40);
							tmp32 = tmp33 + (z21 + z30);
							tmp43 = num + (z10 + z40);
						}
						else
						{
							int z20 = d5 + d3;
							int z29 = d7 + d3;
							int z50 = MULTIPLY(z29 + d5, 9633);
							int tmp5 = MULTIPLY(d7, 2446);
							int tmp16 = MULTIPLY(d5, 16819);
							int tmp31 = MULTIPLY(d3, 25172);
							int z9 = MULTIPLY(-d7, 7373);
							z20 = MULTIPLY(-z20, 20995);
							z29 = MULTIPLY(-z29, 16069);
							int z39 = MULTIPLY(-d5, 3196);
							z29 += z50;
							z39 += z50;
							tmp6 = tmp5 + (z9 + z29);
							tmp17 = tmp16 + (z20 + z39);
							tmp32 = tmp31 + (z20 + z29);
							tmp43 = z9 + z39;
						}
					}
					else if (d1 != 0)
					{
						int z8 = d7 + d1;
						int z38 = d5 + d1;
						int z49 = MULTIPLY(d7 + z38, 9633);
						int tmp4 = MULTIPLY(d7, 2446);
						int tmp15 = MULTIPLY(d5, 16819);
						int tmp42 = MULTIPLY(d1, 12299);
						z8 = MULTIPLY(-z8, 7373);
						int z19 = MULTIPLY(-d5, 20995);
						int z28 = MULTIPLY(-d7, 16069);
						z38 = MULTIPLY(-z38, 3196);
						z28 += z49;
						z38 += z49;
						tmp6 = tmp4 + (z8 + z28);
						tmp17 = tmp15 + (z19 + z38);
						tmp32 = z19 + z28;
						tmp43 = tmp42 + (z8 + z38);
					}
					else
					{
						int tmp3 = MULTIPLY(-d7, 4926);
						int z7 = MULTIPLY(-d7, 7373);
						int z27 = MULTIPLY(-d7, 16069);
						int tmp14 = MULTIPLY(-d5, 4176);
						int z18 = MULTIPLY(-d5, 20995);
						int z37 = MULTIPLY(-d5, 3196);
						int z48 = MULTIPLY(d5 + d7, 9633);
						z27 += z48;
						z37 += z48;
						tmp6 = tmp3 + z27;
						tmp17 = tmp14 + z37;
						tmp32 = z18 + z27;
						tmp43 = z7 + z37;
					}
				}
				else if (d3 != 0)
				{
					if (d1 != 0)
					{
						int z6 = d7 + d1;
						int z26 = d7 + d3;
						int z47 = MULTIPLY(z26 + d1, 9633);
						int tmp2 = MULTIPLY(d7, 2446);
						int tmp30 = MULTIPLY(d3, 25172);
						int tmp41 = MULTIPLY(d1, 12299);
						z6 = MULTIPLY(-z6, 7373);
						int z17 = MULTIPLY(-d3, 20995);
						z26 = MULTIPLY(-z26, 16069);
						int z36 = MULTIPLY(-d1, 3196);
						z26 += z47;
						z36 += z47;
						tmp6 = tmp2 + (z6 + z26);
						tmp17 = z17 + z36;
						tmp32 = tmp30 + (z17 + z26);
						tmp43 = tmp41 + (z6 + z36);
					}
					else
					{
						int z25 = d7 + d3;
						int tmp = MULTIPLY(-d7, 4926);
						int z5 = MULTIPLY(-d7, 7373);
						int tmp29 = MULTIPLY(d3, 4176);
						int z16 = MULTIPLY(-d3, 20995);
						int z46 = MULTIPLY(z25, 9633);
						z25 = MULTIPLY(-z25, 6436);
						tmp6 = tmp + z25;
						tmp17 = z16 + z46;
						tmp32 = tmp29 + z25;
						tmp43 = z5 + z46;
					}
				}
				else if (d1 != 0)
				{
					int z4 = d7 + d1;
					int z45 = MULTIPLY(z4, 9633);
					z4 = MULTIPLY(z4, 2260);
					int z24 = MULTIPLY(-d7, 16069);
					int tmp0 = MULTIPLY(-d7, 13623);
					int z35 = MULTIPLY(-d1, 3196);
					int tmp40 = MULTIPLY(d1, 9102);
					tmp6 = tmp0 + z4;
					tmp17 = z35 + z45;
					tmp32 = z24 + z45;
					tmp43 = tmp40 + z4;
				}
				else
				{
					tmp6 = MULTIPLY(-d7, 11363);
					tmp17 = MULTIPLY(d7, 9633);
					tmp32 = MULTIPLY(-d7, 6436);
					tmp43 = MULTIPLY(d7, 2260);
				}
			}
			else if (d5 != 0)
			{
				if (d3 != 0)
				{
					if (d1 != 0)
					{
						int z15 = d5 + d3;
						int z34 = d5 + d1;
						int z44 = MULTIPLY(d3 + z34, 9633);
						int tmp13 = MULTIPLY(d5, 16819);
						int tmp28 = MULTIPLY(d3, 25172);
						int tmp39 = MULTIPLY(d1, 12299);
						int z3 = MULTIPLY(-d1, 7373);
						z15 = MULTIPLY(-z15, 20995);
						int z23 = MULTIPLY(-d3, 16069);
						z34 = MULTIPLY(-z34, 3196);
						z23 += z44;
						z34 += z44;
						tmp6 = z3 + z23;
						tmp17 = tmp13 + (z15 + z34);
						tmp32 = tmp28 + (z15 + z23);
						tmp43 = tmp39 + (z3 + z34);
					}
					else
					{
						int z14 = d5 + d3;
						int z43 = MULTIPLY(z14, 9633);
						int tmp12 = MULTIPLY(d5, 13623);
						int z33 = MULTIPLY(-d5, 3196);
						z14 = MULTIPLY(-z14, 11363);
						int tmp27 = MULTIPLY(d3, 9102);
						int z22 = MULTIPLY(-d3, 16069);
						tmp6 = z22 + z43;
						tmp17 = tmp12 + z14;
						tmp32 = tmp27 + z14;
						tmp43 = z33 + z43;
					}
				}
				else if (d1 != 0)
				{
					int z32 = d5 + d1;
					int z42 = MULTIPLY(z32, 9633);
					int z2 = MULTIPLY(-d1, 7373);
					int tmp38 = MULTIPLY(d1, 4926);
					int tmp11 = MULTIPLY(-d5, 4176);
					int z13 = MULTIPLY(-d5, 20995);
					z32 = MULTIPLY(z32, 6436);
					tmp6 = z2 + z42;
					tmp17 = tmp11 + z32;
					tmp32 = z13 + z42;
					tmp43 = tmp38 + z32;
				}
				else
				{
					tmp6 = MULTIPLY(d5, 9633);
					tmp17 = MULTIPLY(d5, 2260);
					tmp32 = MULTIPLY(-d5, 11363);
					tmp43 = MULTIPLY(d5, 6436);
				}
			}
			else if (d3 != 0)
			{
				if (d1 != 0)
				{
					int z41 = d1 + d3;
					int tmp37 = MULTIPLY(d1, 1730);
					int tmp26 = MULTIPLY(-d3, 11893);
					int z1 = MULTIPLY(d1, 8697);
					int z12 = MULTIPLY(-d3, 17799);
					int z31 = MULTIPLY(z41, 6436);
					z41 = MULTIPLY(z41, 9633);
					tmp6 = z1 - z31;
					tmp17 = z12 + z31;
					tmp32 = tmp26 + z41;
					tmp43 = tmp37 + z41;
				}
				else
				{
					tmp6 = MULTIPLY(-d3, 6436);
					tmp17 = MULTIPLY(-d3, 11363);
					tmp32 = MULTIPLY(-d3, 2260);
					tmp43 = MULTIPLY(d3, 9633);
				}
			}
			else if (d1 != 0)
			{
				tmp6 = MULTIPLY(d1, 2260);
				tmp17 = MULTIPLY(d1, 6436);
				tmp32 = MULTIPLY(d1, 9633);
				tmp43 = MULTIPLY(d1, 11363);
			}
			else
			{
				tmp6 = (tmp17 = (tmp32 = (tmp43 = 0)));
			}
			dataptr.put(0, DESCALE11(tmp22 + tmp43));
			dataptr.put(7, DESCALE11(tmp22 - tmp43));
			dataptr.put(1, DESCALE11(tmp23 + tmp32));
			dataptr.put(6, DESCALE11(tmp23 - tmp32));
			dataptr.put(2, DESCALE11(tmp24 + tmp17));
			dataptr.put(5, DESCALE11(tmp24 - tmp17));
			dataptr.put(3, DESCALE11(tmp25 + tmp6));
			dataptr.put(4, DESCALE11(tmp25 - tmp6));
			dataptr = advance(dataptr, 8);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 25, 66, 232, 70, 234, 74, 105, 105, 107,
		107, 107, 107, 107, 203, 104, 136, 114, 115, 146,
		106, 138, 104, 104, 104, 173, 112, 143, 106, 138,
		104, 104, 104, 173, 136, 111, 143, 106, 138, 104,
		104, 104, 170, 109, 237, 72, 104, 104, 104, 135,
		103, 104, 104, 103, 146, 111, 111, 111, 110, 112,
		112, 112, 144, 104, 136, 107, 107, 107, 176, 101,
		104, 104, 146, 111, 111, 111, 112, 112, 112, 144,
		104, 136, 107, 107, 107, 173, 135, 103, 101, 101,
		103, 146, 111, 111, 110, 112, 112, 112, 144, 104,
		136, 107, 107, 104, 176, 112, 112, 112, 112, 112,
		112, 146, 104, 136, 104, 104, 104, 205, 104, 135,
		103, 104, 145, 111, 111, 110, 112, 112, 112, 143,
		104, 136, 107, 104, 107, 176, 136, 112, 112, 111,
		112, 111, 144, 104, 104, 104, 173, 135, 103, 143,
		111, 112, 112, 111, 142, 104, 104, 104, 173, 112,
		111, 112, 244, 69, 104, 104, 135, 104, 103, 146,
		111, 111, 110, 111, 112, 112, 144, 104, 136, 104,
		107, 107, 176, 136, 111, 111, 112, 112, 111, 144,
		104, 104, 104, 173, 135, 135, 111, 111, 110, 112,
		112, 143, 104, 104, 104, 173, 111, 111, 112, 212,
		104, 135, 103, 110, 112, 110, 112, 111, 143, 104,
		104, 104, 173, 112, 112, 112, 177, 132, 110, 110,
		110, 176, 237, 72, 115, 116, 115, 116, 116, 116,
		116, 148, 233, 158, 170, 234, 161, 88
	})]
	private static void pass2(ShortBuffer data)
	{
		ShortBuffer dataptr = data.duplicate();
		for (int rowctr = 7; rowctr >= 0; rowctr += -1)
		{
			int d0 = dataptr.get(0);
			int d1 = dataptr.get(8);
			int d2 = dataptr.get(16);
			int d3 = dataptr.get(24);
			int d4 = dataptr.get(32);
			int d5 = dataptr.get(40);
			int d6 = dataptr.get(48);
			int d7 = dataptr.get(56);
			int tmp22;
			int tmp25;
			int tmp23;
			int tmp24;
			if (d6 != 0)
			{
				if (d2 != 0)
				{
					int z11 = MULTIPLY(d2 + d6, 4433);
					int tmp36 = z11 + MULTIPLY(-d6, 15137);
					int tmp46 = z11 + MULTIPLY(d2, 6270);
					int tmp10 = d0 + d4 << 13;
					int tmp21 = d0 - d4 << 13;
					tmp22 = tmp10 + tmp46;
					tmp25 = tmp10 - tmp46;
					tmp23 = tmp21 + tmp36;
					tmp24 = tmp21 - tmp36;
				}
				else
				{
					int tmp35 = MULTIPLY(-d6, 10703);
					int tmp45 = MULTIPLY(d6, 4433);
					int tmp9 = d0 + d4 << 13;
					int tmp20 = d0 - d4 << 13;
					tmp22 = tmp9 + tmp45;
					tmp25 = tmp9 - tmp45;
					tmp23 = tmp20 + tmp35;
					tmp24 = tmp20 - tmp35;
				}
			}
			else if (d2 != 0)
			{
				int tmp34 = MULTIPLY(d2, 4433);
				int tmp44 = MULTIPLY(d2, 10703);
				int tmp8 = d0 + d4 << 13;
				int tmp19 = d0 - d4 << 13;
				tmp22 = tmp8 + tmp44;
				tmp25 = tmp8 - tmp44;
				tmp23 = tmp19 + tmp34;
				tmp24 = tmp19 - tmp34;
			}
			else
			{
				tmp22 = (tmp25 = d0 + d4 << 13);
				tmp23 = (tmp24 = d0 - d4 << 13);
			}
			int tmp6;
			int tmp17;
			int tmp32;
			int tmp43;
			if (d7 != 0)
			{
				if (d5 != 0)
				{
					if (d3 != 0)
					{
						if (d1 != 0)
						{
							int z10 = d7 + d1;
							int z21 = d5 + d3;
							int z30 = d7 + d3;
							int z40 = d5 + d1;
							int z51 = MULTIPLY(z30 + z40, 9633);
							int tmp7 = MULTIPLY(d7, 2446);
							int tmp18 = MULTIPLY(d5, 16819);
							int tmp33 = MULTIPLY(d3, 25172);
							int num = MULTIPLY(d1, 12299);
							z10 = MULTIPLY(-z10, 7373);
							z21 = MULTIPLY(-z21, 20995);
							z30 = MULTIPLY(-z30, 16069);
							z40 = MULTIPLY(-z40, 3196);
							z30 += z51;
							z40 += z51;
							tmp6 = tmp7 + (z10 + z30);
							tmp17 = tmp18 + (z21 + z40);
							tmp32 = tmp33 + (z21 + z30);
							tmp43 = num + (z10 + z40);
						}
						else
						{
							int z9 = d7;
							int z20 = d5 + d3;
							int z29 = d7 + d3;
							int z50 = MULTIPLY(z29 + d5, 9633);
							int tmp5 = MULTIPLY(d7, 2446);
							int tmp16 = MULTIPLY(d5, 16819);
							int tmp31 = MULTIPLY(d3, 25172);
							z9 = MULTIPLY(-d7, 7373);
							z20 = MULTIPLY(-z20, 20995);
							z29 = MULTIPLY(-z29, 16069);
							int z39 = MULTIPLY(-d5, 3196);
							z29 += z50;
							z39 += z50;
							tmp6 = tmp5 + (z9 + z29);
							tmp17 = tmp16 + (z20 + z39);
							tmp32 = tmp31 + (z20 + z29);
							tmp43 = z9 + z39;
						}
					}
					else if (d1 != 0)
					{
						int z8 = d7 + d1;
						int z19 = d5;
						int z28 = d7;
						int z38 = d5 + d1;
						int z49 = MULTIPLY(z28 + z38, 9633);
						int tmp4 = MULTIPLY(d7, 2446);
						int tmp15 = MULTIPLY(d5, 16819);
						int tmp42 = MULTIPLY(d1, 12299);
						z8 = MULTIPLY(-z8, 7373);
						z19 = MULTIPLY(-d5, 20995);
						z28 = MULTIPLY(-d7, 16069);
						z38 = MULTIPLY(-z38, 3196);
						z28 += z49;
						z38 += z49;
						tmp6 = tmp4 + (z8 + z28);
						tmp17 = tmp15 + (z19 + z38);
						tmp32 = z19 + z28;
						tmp43 = tmp42 + (z8 + z38);
					}
					else
					{
						int tmp3 = MULTIPLY(-d7, 4926);
						int z7 = MULTIPLY(-d7, 7373);
						int z27 = MULTIPLY(-d7, 16069);
						int tmp14 = MULTIPLY(-d5, 4176);
						int z18 = MULTIPLY(-d5, 20995);
						int z37 = MULTIPLY(-d5, 3196);
						int z48 = MULTIPLY(d5 + d7, 9633);
						z27 += z48;
						z37 += z48;
						tmp6 = tmp3 + z27;
						tmp17 = tmp14 + z37;
						tmp32 = z18 + z27;
						tmp43 = z7 + z37;
					}
				}
				else if (d3 != 0)
				{
					if (d1 != 0)
					{
						int z6 = d7 + d1;
						int z26 = d7 + d3;
						int z47 = MULTIPLY(z26 + d1, 9633);
						int tmp2 = MULTIPLY(d7, 2446);
						int tmp30 = MULTIPLY(d3, 25172);
						int tmp41 = MULTIPLY(d1, 12299);
						z6 = MULTIPLY(-z6, 7373);
						int z17 = MULTIPLY(-d3, 20995);
						z26 = MULTIPLY(-z26, 16069);
						int z36 = MULTIPLY(-d1, 3196);
						z26 += z47;
						z36 += z47;
						tmp6 = tmp2 + (z6 + z26);
						tmp17 = z17 + z36;
						tmp32 = tmp30 + (z17 + z26);
						tmp43 = tmp41 + (z6 + z36);
					}
					else
					{
						int z25 = d7 + d3;
						int tmp = MULTIPLY(-d7, 4926);
						int z5 = MULTIPLY(-d7, 7373);
						int tmp29 = MULTIPLY(d3, 4176);
						int z16 = MULTIPLY(-d3, 20995);
						int z46 = MULTIPLY(z25, 9633);
						z25 = MULTIPLY(-z25, 6436);
						tmp6 = tmp + z25;
						tmp17 = z16 + z46;
						tmp32 = tmp29 + z25;
						tmp43 = z5 + z46;
					}
				}
				else if (d1 != 0)
				{
					int z4 = d7 + d1;
					int z45 = MULTIPLY(z4, 9633);
					z4 = MULTIPLY(z4, 2260);
					int z24 = MULTIPLY(-d7, 16069);
					int tmp0 = MULTIPLY(-d7, 13623);
					int z35 = MULTIPLY(-d1, 3196);
					int tmp40 = MULTIPLY(d1, 9102);
					tmp6 = tmp0 + z4;
					tmp17 = z35 + z45;
					tmp32 = z24 + z45;
					tmp43 = tmp40 + z4;
				}
				else
				{
					tmp6 = MULTIPLY(-d7, 11363);
					tmp17 = MULTIPLY(d7, 9633);
					tmp32 = MULTIPLY(-d7, 6436);
					tmp43 = MULTIPLY(d7, 2260);
				}
			}
			else if (d5 != 0)
			{
				if (d3 != 0)
				{
					if (d1 != 0)
					{
						int z15 = d5 + d3;
						int z34 = d5 + d1;
						int z44 = MULTIPLY(d3 + z34, 9633);
						int tmp13 = MULTIPLY(d5, 16819);
						int tmp28 = MULTIPLY(d3, 25172);
						int tmp39 = MULTIPLY(d1, 12299);
						int z3 = MULTIPLY(-d1, 7373);
						z15 = MULTIPLY(-z15, 20995);
						int z23 = MULTIPLY(-d3, 16069);
						z34 = MULTIPLY(-z34, 3196);
						z23 += z44;
						z34 += z44;
						tmp6 = z3 + z23;
						tmp17 = tmp13 + (z15 + z34);
						tmp32 = tmp28 + (z15 + z23);
						tmp43 = tmp39 + (z3 + z34);
					}
					else
					{
						int z14 = d5 + d3;
						int z43 = MULTIPLY(z14, 9633);
						int tmp12 = MULTIPLY(d5, 13623);
						int z33 = MULTIPLY(-d5, 3196);
						z14 = MULTIPLY(-z14, 11363);
						int tmp27 = MULTIPLY(d3, 9102);
						int z22 = MULTIPLY(-d3, 16069);
						tmp6 = z22 + z43;
						tmp17 = tmp12 + z14;
						tmp32 = tmp27 + z14;
						tmp43 = z33 + z43;
					}
				}
				else if (d1 != 0)
				{
					int z32 = d5 + d1;
					int z42 = MULTIPLY(z32, 9633);
					int z2 = MULTIPLY(-d1, 7373);
					int tmp38 = MULTIPLY(d1, 4926);
					int tmp11 = MULTIPLY(-d5, 4176);
					int z13 = MULTIPLY(-d5, 20995);
					z32 = MULTIPLY(z32, 6436);
					tmp6 = z2 + z42;
					tmp17 = tmp11 + z32;
					tmp32 = z13 + z42;
					tmp43 = tmp38 + z32;
				}
				else
				{
					tmp6 = MULTIPLY(d5, 9633);
					tmp17 = MULTIPLY(d5, 2260);
					tmp32 = MULTIPLY(-d5, 11363);
					tmp43 = MULTIPLY(d5, 6436);
				}
			}
			else if (d3 != 0)
			{
				if (d1 != 0)
				{
					int z41 = d1 + d3;
					int tmp37 = MULTIPLY(d1, 1730);
					int tmp26 = MULTIPLY(-d3, 11893);
					int z1 = MULTIPLY(d1, 8697);
					int z12 = MULTIPLY(-d3, 17799);
					int z31 = MULTIPLY(z41, 6436);
					z41 = MULTIPLY(z41, 9633);
					tmp6 = z1 - z31;
					tmp17 = z12 + z31;
					tmp32 = tmp26 + z41;
					tmp43 = tmp37 + z41;
				}
				else
				{
					tmp6 = MULTIPLY(-d3, 6436);
					tmp17 = MULTIPLY(-d3, 11363);
					tmp32 = MULTIPLY(-d3, 2260);
					tmp43 = MULTIPLY(d3, 9633);
				}
			}
			else if (d1 != 0)
			{
				tmp6 = MULTIPLY(d1, 2260);
				tmp17 = MULTIPLY(d1, 6436);
				tmp32 = MULTIPLY(d1, 9633);
				tmp43 = MULTIPLY(d1, 11363);
			}
			else
			{
				tmp6 = (tmp17 = (tmp32 = (tmp43 = 0)));
			}
			dataptr.put(0, DESCALE18(tmp22 + tmp43));
			dataptr.put(56, DESCALE18(tmp22 - tmp43));
			dataptr.put(8, DESCALE18(tmp23 + tmp32));
			dataptr.put(48, DESCALE18(tmp23 - tmp32));
			dataptr.put(16, DESCALE18(tmp24 + tmp17));
			dataptr.put(40, DESCALE18(tmp24 - tmp17));
			dataptr.put(24, DESCALE18(tmp25 + tmp6));
			dataptr.put(32, DESCALE18(tmp25 - tmp6));
			dataptr = advance(dataptr, 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 112 })]
	private static ShortBuffer advance(ShortBuffer dataptr, int size)
	{
		dataptr.position(dataptr.position() + size);
		ShortBuffer result = dataptr.slice();
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 28, 97, 67 })]
	private static int MULTIPLY(int x, short y)
	{
		return y * (short)x;
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(825)]
	private static short DESCALE11(int x)
	{
		return (short)(x + 1024 >> 11);
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(829)]
	private static short DESCALE18(int x)
	{
		return (short)(x + 131072 >> 18);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public FfmpegIntDct()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 104, 103, 103 })]
	public virtual short[] decode(short[] orig)
	{
		ShortBuffer data = ShortBuffer.wrap(orig);
		pass1(data);
		pass2(data);
		return orig;
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(821)]
	private static int DESCALE(int x, int n)
	{
		return x + (1 << n - 1) >> n;
	}
}
