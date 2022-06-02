using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.error;

public class HCR : Object, SyntaxConstants
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class Codeword : Object
	{
		internal int cb;

		internal int decoded;

		internal int sp_offset;

		internal BitsBuffer bits;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 130, 104, 104, 104, 108 })]
		private void fill(int sp, int cb)
		{
			sp_offset = sp;
			this.cb = cb;
			decoded = 0;
			bits = new BitsBuffer();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(24)]
		private Codeword()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(24)]
		internal static void access_0024000(Codeword x0, int x1, int x2)
		{
			x0.fill(x1, x2);
		}
	}

	private const int NUM_CB = 6;

	private const int NUM_CB_ER = 22;

	private const int MAX_CB = 32;

	private const int VCB11_FIRST = 16;

	private const int VCB11_LAST = 31;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] PRE_SORT_CB_STD;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] PRE_SORT_CB_ER;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] MAX_CW_LEN;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 131, 130, 99, 116, 119, 134 })]
	private static bool isGoodCB(int cb, int sectCB)
	{
		int b = 0;
		if ((sectCB > 0 && sectCB <= 11) || (sectCB >= 16 && sectCB <= 31))
		{
			b = ((cb >= 11) ? ((sectCB == cb) ? 1 : 0) : ((sectCB == cb || sectCB == cb + 1) ? 1 : 0));
		}
		return (byte)b != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	public HCR()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 128, 65, 67, 104, 104, 104, 105, 169, 127,
		15, 127, 15, 105, 127, 15, 191, 15, 105, 134,
		105, 187, 105, 103, 134, 105, 59, 201, 109, 205,
		100, 104, 167, 104, 164, 100, 100, 100, 196, 141,
		136, 108, 127, 2, 108, 112, 159, 0, 139, 143,
		118, 108, 120, 178, 124, 189, 136, 106, 110, 232,
		69, 139, 204, 106, 136, 110, 126, 139, 113, 127,
		9, 108, 123, 174, 127, 6, 108, 151, 150, 101,
		132, 208, 146, 231, 16, 236, 51, 44, 44, 44,
		236, 60, 236, 160, 78, 149, 208, 109, 109, 109,
		116, 174, 143, 122, 159, 7, 237, 54, 44, 236,
		88, 106, 43, 233, 39, 236, 93
	})]
	public static void decodeReorderedSpectralData(ICStream ics, IBitStream _in, short[] spectralData, bool sectionDataResilience)
	{
		ICSInfo info = ics.getInfo();
		int windowGroupCount = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[] swbOffsets = info.getSWBOffsets();
		int swbOffsetMax = info.getSWBOffsetMax();
		int[] array = new int[2];
		int num = (array[1] = 0);
		num = (array[0] = 0);
		int[][] sectStart = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 0);
		num = (array[0] = 0);
		int[][] sectEnd = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int[] numSec = new int[0];
		array = new int[2];
		num = (array[1] = 0);
		num = (array[0] = 0);
		int[][] sectCB = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 0);
		num = (array[0] = 0);
		int[][] sectSFBOffsets = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int spDataLen = ics.getReorderedSpectralDataLength();
		if (spDataLen == 0)
		{
			return;
		}
		int longestLen = ics.getLongestCodewordLength();
		if (longestLen == 0 || longestLen >= spDataLen)
		{
			
			throw new AACException("length of longest HCR codeword out of range");
		}
		int[] spOffsets = new int[8];
		int shortFrameLen = (int)((nint)spectralData.LongLength / 8);
		spOffsets[0] = 0;
		for (int g = 1; g < windowGroupCount; g++)
		{
			spOffsets[g] = spOffsets[g - 1] + shortFrameLen * info.getWindowGroupLength(g - 1);
		}
		Codeword[] codeword = new Codeword[512];
		BitsBuffer[] segment = new BitsBuffer[512];
		int[] preSortCB;
		int lastCB;
		if (sectionDataResilience)
		{
			preSortCB = PRE_SORT_CB_ER;
			lastCB = 22;
		}
		else
		{
			preSortCB = PRE_SORT_CB_STD;
			lastCB = 6;
		}
		int PCWs_done = 0;
		int segmentsCount = 0;
		int numberOfCodewords = 0;
		int bitsread = 0;
		for (int sortloop = 0; sortloop < lastCB; sortloop++)
		{
			int thisCB = preSortCB[sortloop];
			for (int sfb = 0; sfb < maxSFB; sfb++)
			{
				for (int w_idx = 0; 4 * w_idx < Math.min(swbOffsets[sfb + 1], swbOffsetMax) - swbOffsets[sfb]; w_idx++)
				{
					for (int g = 0; g < windowGroupCount; g++)
					{
						for (int j = 0; j < numSec[g]; j++)
						{
							if (sectStart[g][j] > sfb || sectEnd[g][j] <= sfb)
							{
								continue;
							}
							int thisSectCB = sectCB[g][j];
							if (!isGoodCB(thisCB, thisSectCB))
							{
								continue;
							}
							int sect_sfb_size = sectSFBOffsets[g][sfb + 1] - sectSFBOffsets[g][sfb];
							int inc = ((thisSectCB >= 5) ? 2 : 4);
							int num2 = 4 * info.getWindowGroupLength(g);
							int group_cws_count = ((inc != -1) ? (num2 / inc) : (-num2));
							int segwidth = Math.min(MAX_CW_LEN[thisSectCB], longestLen);
							for (int cws = 0; cws < group_cws_count && cws + w_idx * group_cws_count < sect_sfb_size; cws++)
							{
								int sp = spOffsets[g] + sectSFBOffsets[g][sfb] + inc * (cws + w_idx * group_cws_count);
								if (PCWs_done == 0)
								{
									if (bitsread + segwidth <= spDataLen)
									{
										segment[segmentsCount].readSegment(segwidth, _in);
										bitsread += segwidth;
										segment[segmentsCount].rewindReverse();
										segmentsCount++;
									}
									else
									{
										if (bitsread < spDataLen)
										{
											int additional_bits = spDataLen - bitsread;
											segment[segmentsCount].readSegment(additional_bits, _in);
											segment[segmentsCount].len += segment[segmentsCount - 1].len;
											segment[segmentsCount].rewindReverse();
											if (segment[segmentsCount - 1].len > 32)
											{
												segment[segmentsCount - 1].bufb = segment[segmentsCount].bufb + segment[segmentsCount - 1].showBits(segment[segmentsCount - 1].len - 32);
												segment[segmentsCount - 1].bufa = segment[segmentsCount].bufa + segment[segmentsCount - 1].showBits(32);
											}
											else
											{
												segment[segmentsCount - 1].bufa = segment[segmentsCount].bufa + segment[segmentsCount - 1].showBits(segment[segmentsCount - 1].len);
												segment[segmentsCount - 1].bufb = segment[segmentsCount].bufb;
											}
											segment[segmentsCount - 1].len += additional_bits;
										}
										bitsread = spDataLen;
										PCWs_done = 1;
										Codeword.access_0024000(codeword[0], sp, thisSectCB);
									}
								}
								else
								{
									Codeword.access_0024000(codeword[numberOfCodewords - segmentsCount], sp, thisSectCB);
								}
								numberOfCodewords++;
							}
						}
					}
				}
			}
		}
		if (segmentsCount == 0)
		{
			
			throw new AACException("no segments _in HCR");
		}
		int num3 = numberOfCodewords;
		int num4 = segmentsCount;
		int numberOfSets = ((num4 != -1) ? (num3 / num4) : (-num3));
		for (int set = 1; set <= numberOfSets; set++)
		{
			for (int trial = 0; trial < segmentsCount; trial++)
			{
				for (int codewordBase = 0; codewordBase < segmentsCount; codewordBase++)
				{
					int num5 = trial + codewordBase;
					int num6 = segmentsCount;
					int segmentID = ((num6 != -1) ? (num5 % num6) : 0);
					int codewordID = codewordBase + set * segmentsCount - segmentsCount;
					if (codewordID >= numberOfCodewords - segmentsCount)
					{
						break;
					}
					if (codeword[codewordID].decoded == 0 && segment[segmentID].len > 0)
					{
						if (codeword[codewordID].bits.len != 0)
						{
							segment[segmentID].concatBits(codeword[codewordID].bits);
						}
						int len = segment[segmentID].len;
					}
				}
			}
			for (int i = 0; i < segmentsCount; i++)
			{
				segment[i].rewindReverse();
			}
		}
	}

	[LineNumberTable(new byte[] { 159, 132, 98, 127, 7, 127, 101 })]
	static HCR()
	{
		PRE_SORT_CB_STD = new int[6] { 11, 9, 7, 5, 3, 1 };
		PRE_SORT_CB_ER = new int[22]
		{
			11, 31, 30, 29, 28, 27, 26, 25, 24, 23,
			22, 21, 20, 19, 18, 17, 16, 9, 7, 5,
			3, 1
		};
		MAX_CW_LEN = new int[32]
		{
			0, 11, 9, 20, 16, 13, 11, 14, 12, 17,
			14, 49, 0, 0, 0, 0, 14, 17, 21, 21,
			25, 25, 29, 29, 29, 29, 33, 33, 33, 37,
			37, 41
		};
	}
}
