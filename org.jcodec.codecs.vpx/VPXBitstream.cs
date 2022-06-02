using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.vpx;

public class VPXBitstream : Object
{
	internal static int[] ___003C_003EcoeffBandMapping;

	private int[][][][] tokenBinProbs;

	private int whtNzLeft;

	private int[] whtNzTop;

	private int[][] dctNzLeft;

	private int[][] dctNzTop;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] coeffBandMapping
	{
		[HideFromJava]
		get
		{
			return ___003C_003EcoeffBandMapping;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 233, 59, 232, 70, 127, 9, 136,
		109, 127, 15
	})]
	public VPXBitstream(int[][][][] tokenBinProbs, int mbWidth)
	{
		whtNzLeft = 0;
		dctNzLeft = new int[3][]
		{
			new int[4],
			new int[2],
			new int[2]
		};
		this.tokenBinProbs = tokenBinProbs;
		whtNzTop = new int[mbWidth];
		dctNzTop = new int[3][]
		{
			new int[mbWidth << 2],
			new int[mbWidth << 1],
			new int[mbWidth << 1]
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 107, 104, 127, 16, 117, 116 })]
	public virtual void encodeCoeffsDCT15(VPXBooleanEncoder bc, int[] coeffs, int mbX, int blkX, int blkY)
	{
		int nCoeff = countCoeff(coeffs, 16);
		int blkAbsX = (mbX << 2) + blkX;
		encodeCoeffs(bc, coeffs, 1, nCoeff, 0, ((blkAbsX != 0 && dctNzLeft[0][blkY] > 0) ? 1 : 0) + ((dctNzTop[0][blkAbsX] > 0) ? 1 : 0));
		dctNzLeft[0][blkY] = Math.max(nCoeff - 1, 0);
		dctNzTop[0][blkAbsX] = Math.max(nCoeff - 1, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 127, 9, 104, 106 })]
	public virtual void encodeCoeffsWHT(VPXBooleanEncoder bc, int[] coeffs, int mbX)
	{
		int nCoeff = fastCountCoeffWHT(coeffs);
		encodeCoeffs(bc, coeffs, 0, nCoeff, 1, ((mbX != 0 && whtNzLeft > 0) ? 1 : 0) + ((whtNzTop[mbX] > 0) ? 1 : 0));
		whtNzLeft = nCoeff;
		whtNzTop[mbX] = nCoeff;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 98, 107, 105, 159, 16, 109, 108 })]
	public virtual void encodeCoeffsDCTUV(VPXBooleanEncoder bc, int[] coeffs, int comp, int mbX, int blkX, int blkY)
	{
		int nCoeff = countCoeff(coeffs, 16);
		int blkAbsX = (mbX << 1) + blkX;
		encodeCoeffs(bc, coeffs, 0, nCoeff, 2, ((blkAbsX != 0 && dctNzLeft[comp][blkY] > 0) ? 1 : 0) + ((dctNzTop[comp][blkAbsX] > 0) ? 1 : 0));
		dctNzLeft[comp][blkY] = nCoeff;
		dctNzTop[comp][blkAbsX] = nCoeff;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 162, 103, 132 })]
	private int fastCountCoeffWHT(int[] coeffs)
	{
		if (coeffs[15] != 0)
		{
			return 16;
		}
		int result = countCoeff(coeffs, 15);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 98, 163, 107, 150, 106, 100, 107, 100,
		107, 137, 107, 101, 107, 137, 100, 107, 101, 107,
		101, 144, 107, 178, 107, 105, 107, 101, 107, 148,
		107, 102, 112, 112, 134, 107, 102, 107, 102, 108,
		141, 108, 170, 107, 102, 108, 145, 108, 239, 70,
		180, 233, 159, 191, 234, 160, 67, 103, 119, 142
	})]
	public virtual void encodeCoeffs(VPXBooleanEncoder bc, int[] coeffs, int firstCoeff, int nCoeff, int blkType, int ctx)
	{
		int prevZero = 0;
		int i;
		for (i = firstCoeff; i < nCoeff; i++)
		{
			int[] probs2 = tokenBinProbs[blkType][___003C_003EcoeffBandMapping[i]][ctx];
			int coeffAbs = MathUtil.abs(coeffs[i]);
			if (prevZero == 0)
			{
				bc.writeBit(probs2[0], 1);
			}
			if (coeffAbs == 0)
			{
				bc.writeBit(probs2[1], 0);
				ctx = 0;
			}
			else
			{
				bc.writeBit(probs2[1], 1);
				if (coeffAbs == 1)
				{
					bc.writeBit(probs2[2], 0);
					ctx = 1;
				}
				else
				{
					ctx = 2;
					bc.writeBit(probs2[2], 1);
					if (coeffAbs <= 4)
					{
						bc.writeBit(probs2[3], 0);
						if (coeffAbs == 2)
						{
							bc.writeBit(probs2[4], 0);
						}
						else
						{
							bc.writeBit(probs2[4], 1);
							bc.writeBit(probs2[5], coeffAbs - 3);
						}
					}
					else
					{
						bc.writeBit(probs2[3], 1);
						if (coeffAbs <= 10)
						{
							bc.writeBit(probs2[6], 0);
							if (coeffAbs <= 6)
							{
								bc.writeBit(probs2[7], 0);
								bc.writeBit(159, coeffAbs - 5);
							}
							else
							{
								bc.writeBit(probs2[7], 1);
								int d = coeffAbs - 7;
								bc.writeBit(165, d >> 1);
								bc.writeBit(145, d & 1);
							}
						}
						else
						{
							bc.writeBit(probs2[6], 1);
							if (coeffAbs <= 34)
							{
								bc.writeBit(probs2[8], 0);
								if (coeffAbs <= 18)
								{
									bc.writeBit(probs2[9], 0);
									writeCat3Ext(bc, coeffAbs);
								}
								else
								{
									bc.writeBit(probs2[9], 1);
									writeCat4Ext(bc, coeffAbs);
								}
							}
							else
							{
								bc.writeBit(probs2[8], 1);
								if (coeffAbs <= 66)
								{
									bc.writeBit(probs2[10], 0);
									writeCatExt(bc, coeffAbs, 35, VPXConst.___003C_003EprobCoeffExtCat5);
								}
								else
								{
									bc.writeBit(probs2[10], 1);
									writeCatExt(bc, coeffAbs, 67, VPXConst.___003C_003EprobCoeffExtCat6);
								}
							}
						}
					}
				}
				bc.writeBit(128, MathUtil.sign(coeffs[i]));
			}
			prevZero = ((coeffAbs == 0) ? 1 : 0);
		}
		if (nCoeff < 16)
		{
			int[] probs = tokenBinProbs[blkType][___003C_003EcoeffBandMapping[i]][ctx];
			bc.writeBit(probs[0], 0);
		}
	}

	[LineNumberTable(new byte[] { 159, 95, 98, 101, 102, 102, 133 })]
	private int countCoeff(int[] coeffs, int nCoeff)
	{
		while (nCoeff > 0)
		{
			nCoeff += -1;
			if (coeffs[nCoeff] != 0)
			{
				return nCoeff + 1;
			}
		}
		return nCoeff;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 130, 102, 111, 113, 113 })]
	private static void writeCat3Ext(VPXBooleanEncoder bc, int coeff)
	{
		int d = coeff - 11;
		bc.writeBit(173, d >> 2);
		bc.writeBit(148, (d >> 1) & 1);
		bc.writeBit(140, d & 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 98, 102, 111, 113, 113, 113 })]
	private static void writeCat4Ext(VPXBooleanEncoder bc, int coeff)
	{
		int d = coeff - 19;
		bc.writeBit(176, d >> 3);
		bc.writeBit(155, (d >> 2) & 1);
		bc.writeBit(140, (d >> 1) & 1);
		bc.writeBit(135, d & 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 102, 98, 101, 108, 54, 167 })]
	private static void writeCatExt(VPXBooleanEncoder bc, int coeff, int catOff, int[] cat)
	{
		int d = coeff - catOff;
		int b = (int)((nint)cat.LongLength - 1);
		int i = 0;
		for (; b >= 0; b += -1)
		{
			int num = i;
			i++;
			bc.writeBit(cat[num], (d >> b) & 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 107, 104, 127, 16, 109, 108 })]
	public virtual void encodeCoeffsDCT16(VPXBooleanEncoder bc, int[] coeffs, int mbX, int blkX, int blkY)
	{
		int nCoeff = countCoeff(coeffs, 16);
		int blkAbsX = (mbX << 2) + blkX;
		encodeCoeffs(bc, coeffs, 0, nCoeff, 3, ((blkAbsX != 0 && dctNzLeft[0][blkY] > 0) ? 1 : 0) + ((dctNzTop[0][blkAbsX] > 0) ? 1 : 0));
		dctNzLeft[0][blkY] = nCoeff;
		dctNzTop[0][blkAbsX] = nCoeff;
	}

	[LineNumberTable(13)]
	static VPXBitstream()
	{
		___003C_003EcoeffBandMapping = new int[16]
		{
			0, 1, 2, 3, 6, 4, 5, 6, 6, 6,
			6, 6, 6, 6, 6, 7
		};
	}
}
