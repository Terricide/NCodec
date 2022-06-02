using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg4;

internal class MPEG4Bitstream : Object
{
	public const int I_VOP = 0;

	public const int P_VOP = 1;

	public const int B_VOP = 2;

	public const int S_VOP = 3;

	public const int N_VOP = 4;

	private const int REVERSE_EVENT_LEN = 0;

	private const int REVERSE_EVENT_LAST = 1;

	private const int REVERSE_EVENT_RUN = 2;

	private const int REVERSE_EVENT_LEVEL = 3;

	private const int VLC_TABLE_VLC_CODE = 0;

	private const int VLC_TABLE_VLC_LEN = 1;

	private const int VLC_TABLE_EVENT_LAST = 2;

	private const int VLC_TABLE_EVENT_RUN = 3;

	private const int VLC_TABLE_EVENT_LEVEL = 4;

	private const int VLC_CODE = 0;

	private const int VLC_LEN = 1;

	private const int ESCAPE = 3;

	private const int NUMBITS_VP_RESYNC_MARKER = 17;

	private const int RESYNC_MARKER = 1;

	private const int VIDOBJLAY_SHAPE_RECTANGULAR = 0;

	private const int VIDOBJLAY_SHAPE_BINARY_ONLY = 2;

	private const int SPRITE_STATIC = 1;

	private const int SPRITE_GMC = 2;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] DQUANT_TABLE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static byte[][][] vlcTab;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 79, 65, 69, 100, 131, 137, 110, 146, 106,
		101, 173, 102, 134, 139, 188, 137, 100, 106, 106,
		140, 110, 159, 2, 175, 138, 105, 145, 147, 107,
		101, 173, 103, 135, 140, 102, 150, 149, 188, 105,
		105, 106, 105, 114, 137
	})]
	internal static int readCoeffs(BitReader br, bool intra, bool shortVideoHeader)
	{
		int intra2 = (intra ? 1 : 0);
		if (shortVideoHeader)
		{
			intra2 = 0;
		}
		int intraIndex = ((intra2 != 0) ? 1 : 0);
		if (br.checkNBit(7) != 3)
		{
			byte[] reverse2 = vlcTab[intraIndex][br.checkNBit(12)];
			int level4;
			if ((level4 = (short)reverse2[3]) == 0)
			{
				int run6 = 64;
				int result = packCoeff(0, run6, 1);
				
				return result;
			}
			int last4 = reverse2[1];
			int run5 = reverse2[2];
			br.skip(reverse2[0]);
			int result2 = packCoeff((!br.readBool()) ? level4 : (-level4), run5, last4);
			
			return result2;
		}
		br.skip(7);
		if (shortVideoHeader)
		{
			int last3 = br.readNBit(1);
			int run4 = br.readNBit(6);
			int level3 = (sbyte)br.readNBit(8);
			if (level3 == 0 || level3 == 128)
			{
				Logger.error(new StringBuilder().append("Illegal LEVEL for ESCAPE mode 4: ").append(level3).toString());
			}
			int result3 = packCoeff(level3, run4, last3);
			
			return result3;
		}
		int mode = br.checkNBit(2);
		if (mode < 3)
		{
			br.skip((mode != 2) ? 1 : 2);
			byte[] reverse = vlcTab[intraIndex][br.checkNBit(12)];
			int level2;
			if ((level2 = (short)reverse[3]) == 0)
			{
				int run3 = 64;
				int result4 = packCoeff(0, run3, 1);
				
				return result4;
			}
			int last2 = reverse[1];
			int run2 = reverse[2];
			br.skip(reverse[0]);
			if (mode < 2)
			{
				level2 = (short)(level2 + MPEG4Consts.MAX_LEVEL[intraIndex][last2][run2]);
			}
			else
			{
				run2 += MPEG4Consts.MAX_RUN[intraIndex][last2][level2] + 1;
			}
			int result5 = packCoeff((!br.readBool()) ? level2 : (-level2), run2, last2);
			
			return result5;
		}
		br.skip(2);
		int last = br.read1Bit();
		int run = br.readNBit(6);
		br.skip(1);
		int level = (short)(br.readNBit(12) << 20 >> 20);
		br.skip(1);
		int result6 = packCoeff(level, run, last);
		
		return result6;
	}

	[LineNumberTable(243)]
	internal static int level(int coeff)
	{
		return (short)coeff;
	}

	[LineNumberTable(239)]
	internal static int run(int coeff)
	{
		return (coeff >> 16) & 0xFF;
	}

	[LineNumberTable(235)]
	internal static int last(int coeff)
	{
		return coeff >> 24;
	}

	[LineNumberTable(231)]
	internal static int packCoeff(int level, int run, int last)
	{
		return ((last & 0xFF) << 24) | ((run & 0xFF) << 16) | (level & 0xFFFF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 62, 130, 105, 227, 69, 138, 105, 111, 105,
		144, 101, 163, 171, 139
	})]
	internal static int readMVData(BitReader br)
	{
		if (br.readBool())
		{
			return 0;
		}
		int index = br.checkNBit(12);
		int[] tab;
		if (index >= 512)
		{
			tab = MPEG4Consts.TMNMV_TAB_0[(index >> 8) - 2];
		}
		else if (index >= 128)
		{
			tab = MPEG4Consts.TMNMV_TAB_1[(index >> 2) - 32];
		}
		else
		{
			if (index < 4)
			{
				return 0;
			}
			tab = MPEG4Consts.TMNMV_TAB_2[index - 4];
		}
		br.skip(tab[1]);
		return tab[0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 14, 98, 104, 102, 139, 108, 139, 106, 169 })]
	internal static bool checkResyncMarker(BitReader br, int addBits)
	{
		int nbits = br.bitsToAlign();
		int nbitResyncMarker = 17 + addBits;
		int code = br.checkNBitDontCare(nbitResyncMarker + nbits);
		int MASK1 = (1 << nbits - 1) - 1;
		int MASK2 = (1 << nbitResyncMarker) - 1;
		if (code >> nbitResyncMarker == MASK1)
		{
			return (code & MASK2) == 1;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 36, 97, 73, 102, 153, 132, 104, 137, 105,
		105, 110, 106, 105, 106, 105, 106, 105, 106, 201,
		139, 106, 179, 105, 137, 136, 164, 148, 105, 105,
		111, 137, 138, 105, 105, 101, 169, 109, 100, 180,
		152, 154, 169, 104, 174, 105, 238, 69, 201, 120,
		105, 152, 169
	})]
	internal static int readVideoPacketHeader(BitReader br, MPEG4DecodingContext ctx, int addBits, bool fcodeForwardEnabled, bool fcodeBackwardEnabled, bool intraDCThresholdEnabled)
	{
		int startcodeBits = 17 + addBits;
		int mbNumBits = MathUtil.log2(ctx.mbWidth * ctx.mbHeight - 1) + 1;
		int hec = 0;
		br.align();
		br.skip(startcodeBits);
		if (ctx.shape != 0)
		{
			hec = (br.readBool() ? 1 : 0);
			if (hec != 0 && ctx.spriteEnable != 1)
			{
				br.skip(13);
				br.skip(1);
				br.skip(13);
				br.skip(1);
				br.skip(13);
				br.skip(1);
				br.skip(13);
				br.skip(1);
			}
		}
		int mbnum = br.readNBit(mbNumBits);
		if (ctx.shape != 2)
		{
			ctx.quant = br.readNBit(ctx.quantBits);
		}
		if (ctx.shape == 0)
		{
			hec = (br.readBool() ? 1 : 0);
		}
		if (hec != 0)
		{
			int timeIncrement = 0;
			int timeBase = 0;
			while (br.readBool())
			{
				timeBase++;
			}
			br.skip(1);
			if (ctx.timeIncrementBits != 0)
			{
				timeIncrement = br.readNBit(ctx.timeIncrementBits);
			}
			br.skip(1);
			int codingType = br.readNBit(2);
			if (ctx.shape != 0)
			{
				br.skip(1);
				if (codingType != 0)
				{
					br.skip(1);
				}
			}
			if (ctx.shape != 2)
			{
				if (intraDCThresholdEnabled)
				{
					ctx.intraDCThreshold = MPEG4Consts.INTRA_DC_THRESHOLD_TABLE[br.readNBit(3)];
				}
				if (ctx.spriteEnable != 2 || codingType != 3 || ctx.spriteWarpingPoints > 0)
				{
				}
				if (ctx.reducedResolutionEnable && ctx.shape == 0 && (codingType == 1 || codingType == 0))
				{
					br.skip(1);
				}
				if (codingType != 0 && fcodeForwardEnabled)
				{
					ctx.fcodeForward = br.readNBit(3);
				}
				if (codingType == 2 && fcodeBackwardEnabled)
				{
					ctx.fcodeBackward = br.readNBit(3);
				}
			}
		}
		if (ctx.newPredEnable)
		{
			int vopId = br.readNBit(Math.min(ctx.timeIncrementBits + 3, 15));
			if (br.readBool())
			{
				int num = br.readNBit(Math.min(ctx.timeIncrementBits + 3, 15));
			}
			br.skip(1);
		}
		return mbnum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 98, 106, 101, 145 })]
	internal static int readMcbpcIntra(BitReader br)
	{
		int index = br.checkNBit(9);
		index >>= 3;
		br.skip(MPEG4Consts.MCBPC_INTRA_TABLE[index][1]);
		return MPEG4Consts.MCBPC_INTRA_TABLE[index][0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 65, 67, 137, 113, 139, 100, 134 })]
	internal static int readCbpy(BitReader br, bool intra)
	{
		int index = br.checkNBit(6);
		br.skip(MPEG4Consts.CBPY_TABLE[index][1]);
		int cbpy = MPEG4Consts.CBPY_TABLE[index][0];
		if (!intra)
		{
			cbpy = 15 - cbpy;
		}
		return cbpy;
	}

	[LineNumberTable(new byte[]
	{
		158, 162, 65, 67, 101, 131, 105, 136, 102, 133,
		102, 133, 100, 136
	})]
	private static int getDCScaler(int quant, bool lum)
	{
		if (quant < 5)
		{
			return 8;
		}
		if (quant < 25 && !lum)
		{
			return (quant + 13) / 2;
		}
		if (quant < 9)
		{
			return 2 * quant;
		}
		if (quant < 25)
		{
			return quant + 8;
		}
		if (lum)
		{
			return 2 * quant - 16;
		}
		return quant - 6;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 157, 98, 108, 138, 101, 133, 104, 104, 136,
		127, 0, 107, 170, 127, 2, 107, 170, 159, 7,
		171, 140, 159, 5, 100, 140, 100, 140, 103, 241,
		69, 101, 133, 103, 108, 241, 69, 100, 108, 172,
		101, 133, 166, 108, 133, 108, 133, 133, 166, 100,
		140, 100, 140, 100, 238, 69, 100, 140, 100, 140,
		100, 236, 69, 127, 3, 107, 159, 24, 108, 107,
		31, 29, 241, 72, 107, 159, 24, 108, 109, 31,
		31, 236, 72
	})]
	private static void predictAcdc(MPEG4DecodingContext ctx, int x, int y, int block, int currentQuant, int iDcScaler, short[] predictors, int bound, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveLeftMb)
	{
		int mbpos = y * ctx.mbWidth + x;
		short[] leftPred = null;
		short[] topPred = null;
		short[] diag = null;
		short[] current = null;
		int leftQuant = currentQuant;
		int topQuant = currentQuant;
		short[] pLeft = MPEG4Consts.DEFAULT_ACDC_VALUES;
		short[] pTop = MPEG4Consts.DEFAULT_ACDC_VALUES;
		short[] pDiag = MPEG4Consts.DEFAULT_ACDC_VALUES;
		if (x != 0 && mbpos >= bound + 1 && (leftMb.mode == 3 || leftMb.mode == 4))
		{
			leftPred = leftMb.predValues[0];
			leftQuant = leftMb.quant;
		}
		if (mbpos >= bound + ctx.mbWidth && (aboveMb.mode == 3 || aboveMb.mode == 4))
		{
			topPred = aboveMb.predValues[0];
			topQuant = aboveMb.quant;
		}
		if (x != 0 && mbpos >= bound + ctx.mbWidth + 1 && (aboveLeftMb.mode == 3 || aboveLeftMb.mode == 4))
		{
			diag = aboveLeftMb.predValues[0];
		}
		current = mb.predValues[0];
		switch (block)
		{
		case 0:
			if (leftPred != null)
			{
				pLeft = leftMb.predValues[1];
			}
			if (topPred != null)
			{
				pTop = aboveMb.predValues[2];
			}
			if (diag != null)
			{
				pDiag = aboveLeftMb.predValues[3];
			}
			break;
		case 1:
			pLeft = current;
			leftQuant = currentQuant;
			if (topPred != null)
			{
				pTop = aboveMb.predValues[3];
				pDiag = aboveMb.predValues[2];
			}
			break;
		case 2:
			if (leftPred != null)
			{
				pLeft = leftMb.predValues[3];
				pDiag = leftMb.predValues[1];
			}
			pTop = current;
			topQuant = currentQuant;
			break;
		case 3:
			pLeft = mb.predValues[2];
			leftQuant = currentQuant;
			pTop = mb.predValues[1];
			topQuant = currentQuant;
			pDiag = current;
			break;
		case 4:
			if (leftPred != null)
			{
				pLeft = leftMb.predValues[4];
			}
			if (topPred != null)
			{
				pTop = aboveMb.predValues[4];
			}
			if (diag != null)
			{
				pDiag = aboveLeftMb.predValues[4];
			}
			break;
		case 5:
			if (leftPred != null)
			{
				pLeft = leftMb.predValues[5];
			}
			if (topPred != null)
			{
				pTop = aboveMb.predValues[5];
			}
			if (diag != null)
			{
				pDiag = aboveLeftMb.predValues[5];
			}
			break;
		}
		if (MathUtil.abs(pLeft[0] - pDiag[0]) < MathUtil.abs(pDiag[0] - pTop[0]))
		{
			mb.acpredDirections[block] = 1;
			int num2;
			if (pTop[0] > 0)
			{
				int num = (int)pTop[0] + (int)((uint)iDcScaler >> 1);
				num2 = ((iDcScaler != -1) ? (num / iDcScaler) : (-num));
			}
			else
			{
				int num3 = (int)pTop[0] - (int)((uint)iDcScaler >> 1);
				num2 = ((iDcScaler != -1) ? (num3 / iDcScaler) : (-num3));
			}
			predictors[0] = (short)num2;
			for (int j = 1; j < 8; j++)
			{
				int a2 = pTop[j] * topQuant;
				int num4 = j;
				int num8;
				if (pTop[j] != 0)
				{
					int num6;
					if (a2 > 0)
					{
						int num5 = a2 + (int)((uint)currentQuant >> 1);
						num6 = ((currentQuant != -1) ? (num5 / currentQuant) : (-num5));
					}
					else
					{
						int num7 = a2 - (int)((uint)currentQuant >> 1);
						num6 = ((currentQuant != -1) ? (num7 / currentQuant) : (-num7));
					}
					num8 = (short)num6;
				}
				else
				{
					num8 = 0;
				}
				predictors[num4] = (short)num8;
			}
			return;
		}
		mb.acpredDirections[block] = 2;
		int num10;
		if (pLeft[0] > 0)
		{
			int num9 = (int)pLeft[0] + (int)((uint)iDcScaler >> 1);
			num10 = ((iDcScaler != -1) ? (num9 / iDcScaler) : (-num9));
		}
		else
		{
			int num11 = (int)pLeft[0] - (int)((uint)iDcScaler >> 1);
			num10 = ((iDcScaler != -1) ? (num11 / iDcScaler) : (-num11));
		}
		predictors[0] = (short)num10;
		for (int i = 1; i < 8; i++)
		{
			int a = pLeft[i + 7] * leftQuant;
			int num12 = i;
			int num16;
			if (pLeft[i + 7] != 0)
			{
				int num14;
				if (a > 0)
				{
					int num13 = a + (int)((uint)currentQuant >> 1);
					num14 = ((currentQuant != -1) ? (num13 / currentQuant) : (-num13));
				}
				else
				{
					int num15 = a - (int)((uint)currentQuant >> 1);
					num14 = ((currentQuant != -1) ? (num15 / currentQuant) : (-num15));
				}
				num16 = (short)num14;
			}
			else
			{
				num16 = 0;
			}
			predictors[num12] = (short)num16;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 46, 66, 138, 104, 101, 105, 133, 229, 59,
		231, 72, 145
	})]
	internal static int readDCSizeLum(BitReader br)
	{
		int code = br.checkNBit(11);
		for (int i = 11; i > 3; i += -1)
		{
			if (code == 1)
			{
				br.skip(i);
				return i + 1;
			}
			code >>= 1;
		}
		br.skip(MPEG4Consts.DC_LUM_TAB[code][1]);
		return MPEG4Consts.DC_LUM_TAB[code][0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 42, 66, 138, 104, 101, 105, 131, 229, 59,
		231, 72
	})]
	internal static int readDCSizeChrom(BitReader br)
	{
		int code = br.checkNBit(12);
		for (int i = 12; i > 2; i += -1)
		{
			if (code == 1)
			{
				br.skip(i);
				return i;
			}
			code >>= 1;
		}
		return 3 - br.readNBit(2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 39, 130, 105, 138, 100, 143 })]
	internal static short readDCDif(BitReader br, int dcSize)
	{
		int code = br.readNBit(dcSize);
		if (code >> dcSize - 1 == 0)
		{
			return (short)(-1 * (code ^ ((1 << dcSize) - 1)));
		}
		return (short)code;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 169, 106, 104, 107, 103, 107, 163,
		136, 113, 191, 1, 102, 108
	})]
	internal static void readIntraBlock(BitReader br, short[] block, int direction, int coeff)
	{
		short[] scan = MPEG4Consts.SCAN_TABLES[direction];
		int c;
		do
		{
			c = readCoeffs(br, intra: true, shortVideoHeader: false);
			int level = MPEG4Bitstream.level(c);
			coeff += run(c);
			if (((uint)coeff & 0xFFFFFFC0u) != 0)
			{
				Logger.error("invalid run or index");
				break;
			}
			block[scan[coeff]] = (short)level;
			if (level < -2047 || level > 2047)
			{
				Logger.error(new StringBuilder().append("intra_overflow: ").append(level).toString());
			}
			coeff++;
		}
		while (last(c) == 0);
	}

	[LineNumberTable(new byte[]
	{
		158, 246, 98, 106, 107, 138, 120, 138, 105, 191,
		10, 101, 105, 146, 103, 103, 237, 59, 238, 71,
		101, 105, 148, 105, 105, 233, 59, 235, 72, 105,
		105, 13, 233, 69
	})]
	internal static void addAcdc(Macroblock mb, int bsVersion, int block, int iDcScaler)
	{
		short[] coeffs = mb.block[block];
		int acpredDirection = (sbyte)mb.acpredDirections[block];
		short[] current = mb.predValues[block];
		int num = 0;
		short[] array = coeffs;
		array[num] += mb.predictors[0];
		current[0] = (short)(coeffs[0] * iDcScaler);
		if (bsVersion == 0 || bsVersion > 34)
		{
			current[0] = (short)((current[0] < -2048) ? (-2048) : ((current[0] <= 2047) ? current[0] : 2047));
		}
		switch (acpredDirection)
		{
		case 1:
		{
			for (int k = 1; k < 8; k++)
			{
				current[k] = (coeffs[k] += mb.predictors[k]);
				current[k + 7] = coeffs[k * 8];
			}
			break;
		}
		case 2:
		{
			for (int j = 1; j < 8; j++)
			{
				current[j + 7] = (coeffs[j * 8] += mb.predictors[j]);
				current[j] = coeffs[j];
			}
			break;
		}
		default:
		{
			for (int i = 1; i < 8; i++)
			{
				current[i] = coeffs[i];
				current[i + 7] = coeffs[i * 8];
			}
			break;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 215, 130, 101, 141, 153, 107, 133, 100, 106,
		101, 104, 152, 103, 245, 54, 234, 77
	})]
	private static void dequantH263Intra(MPEG4DecodingContext ctx, short[] block, int quant, int dcscalar)
	{
		int quantM2 = quant << 1;
		int quantAdd = (((quant & 1) == 0) ? (quant - 1) : quant);
		block[0] = (short)MathUtil.clip(block[0] * dcscalar, -2048, 2047);
		for (int i = 1; i < 64; i++)
		{
			int acLevel = block[i];
			if (acLevel == 0)
			{
				block[i] = 0;
			}
			else if (acLevel < 0)
			{
				acLevel = quantM2 * -acLevel + quantAdd;
				block[i] = (short)((acLevel > 2048) ? (-2048) : (-acLevel));
			}
			else
			{
				acLevel = quantM2 * acLevel + quantAdd;
				block[i] = (short)((acLevel > 2047) ? 2047 : acLevel);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 210, 162, 153, 107, 102, 106, 103, 147, 119,
		99, 146, 245, 54, 234, 77
	})]
	private static void dequantMpegIntra(MPEG4DecodingContext ctx, short[] block, int quant, int dcscalar)
	{
		block[0] = (short)MathUtil.clip(block[0] * dcscalar, -2048, 2047);
		for (int i = 1; i < 64; i++)
		{
			if (block[i] == 0)
			{
				block[i] = 0;
			}
			else if (block[i] < 0)
			{
				int level2 = -block[i] * ctx.intraMpegQuantMatrix[i] * quant >> 3;
				block[i] = (short)((level2 > 2048) ? (-2048) : (-(short)level2));
			}
			else
			{
				int level = block[i] * ctx.intraMpegQuantMatrix[i] * quant >> 3;
				block[i] = (short)((level > 2047) ? 2047 : level);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 98, 116, 145 })]
	internal static int readMcbpcInter(BitReader br)
	{
		int index = Math.min(br.checkNBit(9), 256);
		br.skip(MPEG4Consts.MCBPC_INTER_TABLE[index][1]);
		return MPEG4Consts.MCBPC_INTER_TABLE[index][0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 76, 66, 104, 142, 106, 106, 104, 114, 105,
		146, 234, 57, 234, 75
	})]
	internal static void readInterCoeffs(BitReader br, MPEG4DecodingContext ctx, Macroblock mb)
	{
		int iQuant = mb.quant;
		int direction = (ctx.alternateVerticalScan ? 2 : 0);
		for (int i = 0; i < 6; i++)
		{
			short[] block = mb.block[i];
			Arrays.fill(block, 0);
			if ((mb.cbp & (1 << 5 - i)) != 0)
			{
				if (ctx.quantType)
				{
					readInterBlockMPEG(br, block, direction, iQuant, ctx.interMpegQuantMatrix);
				}
				else
				{
					readInterBlockH263(br, block, direction, iQuant);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 197, 130, 107, 104, 102, 134, 115, 104, 104,
		136, 109, 112, 144, 118, 150, 107, 114, 107, 176,
		107, 114, 107, 176, 159, 55, 112, 144, 118, 155,
		107, 114, 107, 176, 107, 114, 107, 176, 112, 144,
		118, 155, 107, 114, 107, 176, 107, 114, 107, 176,
		108, 108, 127, 9, 159, 9, 127, 1, 118, 127,
		1, 150
	})]
	private static void readMVInterlaced(BitReader br, MPEG4DecodingContext ctx, int x, int y, int k, Macroblock pMB, int fcode, int bound, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveRightMb)
	{
		int scaleFac = 1 << fcode - 1;
		int high = 32 * scaleFac - 1;
		int low = -32 * scaleFac;
		int range = 64 * scaleFac;
		Macroblock.Vector pmv = getPMV2Interlaced(ctx, bound, mb, aboveMb, leftMb, aboveRightMb);
		Macroblock.Vector mv = Macroblock.vec();
		Macroblock.Vector mvf1 = Macroblock.vec();
		Macroblock.Vector mvf2 = Macroblock.vec();
		int num;
		Macroblock.Vector vector;
		if (!pMB.fieldPred)
		{
			mv.x = readMVComponent(br, fcode);
			mv.y = readMVComponent(br, fcode);
			mv.x += pmv.x;
			mv.y += pmv.y;
			if (mv.x < low)
			{
				mv.x += range;
			}
			else if (mv.x > high)
			{
				mv.x -= range;
			}
			if (mv.y < low)
			{
				mv.y += range;
			}
			else if (mv.y > high)
			{
				mv.y -= range;
			}
			Macroblock.Vector[] mvs = pMB.mvs;
			Macroblock.Vector[] mvs2 = pMB.mvs;
			Macroblock.Vector[] mvs3 = pMB.mvs;
			Macroblock.Vector[] mvs4 = pMB.mvs;
			vector = mv;
			num = 3;
			Macroblock.Vector[] array = mvs4;
			Macroblock.Vector vector2 = vector;
			array[num] = vector;
			vector = vector2;
			num = 2;
			array = mvs3;
			Macroblock.Vector vector3 = vector;
			array[num] = vector;
			vector = vector3;
			num = 1;
			array = mvs2;
			Macroblock.Vector vector4 = vector;
			array[num] = vector;
			mvs[0] = vector4;
			return;
		}
		mvf1.x = readMVComponent(br, fcode);
		mvf1.y = readMVComponent(br, fcode);
		mvf1.x += pmv.x;
		mvf1.y = 2 * (mvf1.y + pmv.y / 2);
		if (mvf1.x < low)
		{
			mvf1.x += range;
		}
		else if (mvf1.x > high)
		{
			mvf1.x -= range;
		}
		if (mvf1.y < low)
		{
			mvf1.y += range;
		}
		else if (mvf1.y > high)
		{
			mvf1.y -= range;
		}
		mvf2.x = readMVComponent(br, fcode);
		mvf2.y = readMVComponent(br, fcode);
		mvf2.x += pmv.x;
		mvf2.y = 2 * (mvf2.y + pmv.y / 2);
		if (mvf2.x < low)
		{
			mvf2.x += range;
		}
		else if (mvf2.x > high)
		{
			mvf2.x -= range;
		}
		if (mvf2.y < low)
		{
			mvf2.y += range;
		}
		else if (mvf2.y > high)
		{
			mvf2.y -= range;
		}
		pMB.mvs[0] = mvf1;
		pMB.mvs[1] = mvf2;
		Macroblock.Vector obj = pMB.mvs[2];
		Macroblock.Vector obj2 = pMB.mvs[3];
		num = 0;
		vector = obj2;
		int x2 = num;
		vector.x = num;
		obj.x = x2;
		Macroblock.Vector obj3 = pMB.mvs[2];
		Macroblock.Vector obj4 = pMB.mvs[3];
		num = 0;
		vector = obj4;
		int y2 = num;
		vector.y = num;
		obj3.y = y2;
		int i = pMB.mvs[0].x + pMB.mvs[1].x;
		pMB.mvsAvg.x = (i >> 1) | (i & 1);
		i = pMB.mvs[0].y + pMB.mvs[1].y;
		pMB.mvsAvg.y = (i >> 1) | (i & 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 205, 130, 107, 104, 102, 134, 119, 136, 112,
		144, 118, 150, 107, 114, 107, 176, 107, 114, 107,
		176, 111, 111
	})]
	private static void readMV(BitReader br, MPEG4DecodingContext ctx, int x, int y, int k, Macroblock.Vector retMV, int fcode, int bound, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveRightMb)
	{
		int scaleFac = 1 << fcode - 1;
		int high = 32 * scaleFac - 1;
		int low = -32 * scaleFac;
		int range = 64 * scaleFac;
		Macroblock.Vector pmv = getPMV2(ctx, bound, x, y, k, mb, aboveMb, leftMb, aboveRightMb);
		Macroblock.Vector mv = Macroblock.vec();
		mv.x = readMVComponent(br, fcode);
		mv.y = readMVComponent(br, fcode);
		mv.x += pmv.x;
		mv.y += pmv.y;
		if (mv.x < low)
		{
			mv.x += range;
		}
		else if (mv.x > high)
		{
			mv.x -= range;
		}
		if (mv.y < low)
		{
			mv.y += range;
		}
		else if (mv.y > high)
		{
			mv.y -= range;
		}
		retMV.x = mv.x;
		retMV.y = mv.y;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 1, 130, 106, 111, 177, 191, 14, 105, 170,
		111, 116, 143, 101, 169, 108, 100, 99, 164, 114,
		150, 179, 143, 105, 152, 246, 27, 234, 104
	})]
	internal static void readCoeffIntra(BitReader br, MPEG4DecodingContext ctx, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveLeftMb)
	{
		for (int i = 0; i < 6; i++)
		{
			Arrays.fill(mb.block[i], 0);
			int iDcScaler = getDCScaler(mb.quant, i < 4);
			predictAcdc(ctx, mb.x, mb.y, i, mb.quant, iDcScaler, mb.predictors, mb.bound, mb, aboveMb, leftMb, aboveLeftMb);
			if (!mb.acpredFlag)
			{
				mb.acpredDirections[i] = 0;
			}
			int startCoeff;
			if (mb.quant < ctx.intraDCThreshold)
			{
				int dcSize = ((i >= 4) ? readDCSizeChrom(br) : readDCSizeLum(br));
				int dcDif = ((dcSize != 0) ? readDCDif(br, dcSize) : 0);
				if (dcSize > 8)
				{
					br.skip(1);
				}
				mb.block[i][0] = (short)dcDif;
				startCoeff = 1;
			}
			else
			{
				startCoeff = 0;
			}
			if ((mb.cbp & (1 << 5 - i)) != 0)
			{
				int direction = ((!ctx.alternateVerticalScan) ? mb.acpredDirections[i] : 2);
				readIntraBlock(br, mb.block[i], direction, startCoeff);
			}
			addAcdc(mb, ctx.bsVersion, i, iDcScaler);
			if (!ctx.quantType)
			{
				dequantH263Intra(ctx, mb.block[i], mb.quant, iDcScaler);
			}
			else
			{
				dequantMpegIntra(ctx, mb.block[i], mb.quant, iDcScaler);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 126, 98, 133, 104, 103, 41, 199, 150, 100,
		100, 100, 131, 101, 100, 100, 100, 131, 101, 101,
		100, 100, 100, 131, 101, 101, 101, 100, 100, 164,
		101, 101, 144, 169, 101, 101, 99, 144, 169, 101,
		101, 99, 144, 169, 101, 127, 7, 127, 7, 165
	})]
	private static Macroblock.Vector getPMV2(MPEG4DecodingContext ctx, int bound, int x, int y, int block, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveRightMb)
	{
		int num_cand = 0;
		int last_cand = 1;
		Macroblock.Vector[] pmv = new Macroblock.Vector[4];
		for (int i = 0; i < 4; i++)
		{
			pmv[i] = Macroblock.vec();
		}
		int lz;
		int tz;
		int rz;
		switch (block)
		{
		case 0:
			lz = 1;
			tz = 2;
			rz = 2;
			break;
		case 1:
			leftMb = mb;
			lz = 0;
			tz = 3;
			rz = 2;
			break;
		case 2:
			aboveMb = mb;
			aboveRightMb = mb;
			lz = 3;
			tz = 0;
			rz = 1;
			break;
		default:
			leftMb = mb;
			aboveMb = mb;
			aboveRightMb = mb;
			lz = 2;
			tz = 0;
			rz = 1;
			break;
		}
		if (leftMb != null)
		{
			num_cand++;
			pmv[1] = leftMb.mvs[lz];
		}
		else
		{
			pmv[1] = MPEG4Consts.ZERO_MV;
		}
		if (aboveMb != null)
		{
			num_cand++;
			last_cand = 2;
			pmv[2] = aboveMb.mvs[tz];
		}
		else
		{
			pmv[2] = MPEG4Consts.ZERO_MV;
		}
		if (aboveRightMb != null)
		{
			num_cand++;
			last_cand = 3;
			pmv[3] = aboveRightMb.mvs[rz];
		}
		else
		{
			pmv[3] = MPEG4Consts.ZERO_MV;
		}
		if (num_cand > 1)
		{
			pmv[0].x = selectCand(pmv[1].x, pmv[2].x, pmv[3].x);
			pmv[0].y = selectCand(pmv[1].y, pmv[2].y, pmv[3].y);
			return pmv[0];
		}
		return pmv[last_cand];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 55, 66, 138, 136, 104, 163, 139, 144 })]
	internal static int readMVComponent(BitReader br, int fcode)
	{
		int scaleFac = 1 << fcode - 1;
		int data = readMVData(br);
		if (scaleFac == 1 || data == 0)
		{
			return data;
		}
		int res = br.readNBit(fcode - 1);
		int mv = (Math.abs(data) - 1) * scaleFac + res + 1;
		return (data >= 0) ? mv : (-mv);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 177, 162, 133, 104, 103, 41, 199, 100, 100,
		132, 101, 133, 106, 141, 176, 169, 100, 101, 99,
		105, 140, 175, 169, 101, 101, 99, 106, 141, 176,
		169, 104, 127, 51, 127, 51, 165
	})]
	private static Macroblock.Vector getPMV2Interlaced(MPEG4DecodingContext ctx, int bound, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveRightMb)
	{
		int num_cand = 0;
		int last_cand = 1;
		Macroblock.Vector[] pmv = new Macroblock.Vector[4];
		for (int i = 0; i < 4; i++)
		{
			pmv[i] = Macroblock.vec();
		}
		int lz = 1;
		int tz = 2;
		int rz = 2;
		if (leftMb != null)
		{
			num_cand++;
			if (leftMb.fieldPred)
			{
				pmv[1] = leftMb.mvsAvg;
			}
			else
			{
				pmv[1] = leftMb.mvs[lz];
			}
		}
		else
		{
			pmv[1] = MPEG4Consts.ZERO_MV;
		}
		if (aboveMb != null)
		{
			num_cand++;
			last_cand = 2;
			if (aboveMb.fieldPred)
			{
				pmv[2] = aboveMb.mvsAvg;
			}
			else
			{
				pmv[2] = aboveMb.mvs[tz];
			}
		}
		else
		{
			pmv[2] = MPEG4Consts.ZERO_MV;
		}
		if (aboveRightMb != null)
		{
			num_cand++;
			last_cand = 3;
			if (aboveRightMb.fieldPred)
			{
				pmv[3] = aboveRightMb.mvsAvg;
			}
			else
			{
				pmv[3] = aboveRightMb.mvs[rz];
			}
		}
		else
		{
			pmv[3] = MPEG4Consts.ZERO_MV;
		}
		if (num_cand > 1)
		{
			pmv[0].x = Math.min(Math.max(pmv[1].x, pmv[2].x), Math.min(Math.max(pmv[2].x, pmv[3].x), Math.max(pmv[1].x, pmv[3].x)));
			pmv[0].y = Math.min(Math.max(pmv[1].y, pmv[2].y), Math.min(Math.max(pmv[2].y, pmv[3].y), Math.max(pmv[1].y, pmv[3].y)));
			return pmv[0];
		}
		return pmv[last_cand];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 109, 98, 106, 106, 106, 110, 113 })]
	private static int selectCand(int p1x, int p2x, int p3x)
	{
		int neg12x = neg(p1x - p2x);
		int neg13x = neg(p1x - p3x);
		int neg23x = neg(p2x - p3x);
		int neg1x = neg(p1x - p2x + neg23x - neg13x);
		int neg2x = neg(p2x - p1x + neg1x + neg12x - neg23x);
		return p1x - neg12x + neg2x;
	}

	[LineNumberTable(1166)]
	internal static int neg(int v)
	{
		return (v < 0) ? v : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 51, 66, 103, 105, 3, 231, 70 })]
	internal static int readMBType(BitReader br)
	{
		for (int mbType = 0; mbType <= 3; mbType++)
		{
			if (br.readBool())
			{
				return mbType;
			}
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 49, 130, 105, 99, 105, 164 })]
	internal static int readDBQuant(BitReader br)
	{
		if (!br.readBool())
		{
			return 0;
		}
		if (!br.readBool())
		{
			return -2;
		}
		return 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 83, 130, 106, 104, 102, 134, 106, 138, 108,
		140, 102, 105, 102, 135, 102, 105, 102, 135, 105,
		105
	})]
	private static void getBMotionVector(BitReader br, Macroblock.Vector mv, int fcode, Macroblock.Vector pmv, int x, int y)
	{
		int scaleFac = 1 << fcode - 1;
		int high = 32 * scaleFac - 1;
		int low = -32 * scaleFac;
		int range = 64 * scaleFac;
		int mvX = readMVComponent(br, fcode);
		int mvY = readMVComponent(br, fcode);
		mvX += pmv.x;
		mvY += pmv.y;
		if (mvX < low)
		{
			mvX += range;
		}
		else if (mvX > high)
		{
			mvX -= range;
		}
		if (mvY < low)
		{
			mvY += range;
		}
		else if (mvY > high)
		{
			mvY -= range;
		}
		mv.x = mvX;
		mv.y = mvY;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 130, 105, 99, 195, 106, 105, 106, 103,
		107, 166, 102, 117, 156, 116, 185, 137, 101, 140,
		102, 149
	})]
	internal static void readInterBlockMPEG(BitReader br, short[] block, int direction, int quant, short[] matrix)
	{
		short[] scan = MPEG4Consts.SCAN_TABLES[direction];
		int p = 0;
		int sum = 0;
		int coeff;
		do
		{
			coeff = readCoeffs(br, intra: false, shortVideoHeader: false);
			int level = MPEG4Bitstream.level(coeff);
			p += run(coeff);
			if (((uint)p & 0xFFFFFFC0u) != 0)
			{
				Logger.error("invalid run or index");
				break;
			}
			if (level < 0)
			{
				level = (2 * -level + 1) * matrix[scan[p]] * quant >> 4;
				block[scan[p]] = (short)((level > 2048) ? (-2048) : (-level));
			}
			else
			{
				level = (2 * level + 1) * matrix[scan[p]] * quant >> 4;
				block[scan[p]] = (short)((level > 2047) ? 2047 : level);
			}
			sum ^= block[scan[p]];
			p++;
		}
		while (last(coeff) == 0);
		if ((sum & 1) == 0)
		{
			int num = 63;
			block[num] ^= 1;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 130, 105, 101, 109, 131, 163, 107, 106,
		107, 103, 107, 166, 102, 105, 155, 105, 153, 101,
		109
	})]
	internal static void readInterBlockH263(BitReader br, short[] block, int direction, int quant)
	{
		short[] scan = MPEG4Consts.SCAN_TABLES[direction];
		int quant_m_2 = quant << 1;
		int quant_add = (((quant & 1) == 0) ? (quant - 1) : quant);
		int p = 0;
		p = 0;
		int coeff;
		do
		{
			coeff = readCoeffs(br, intra: false, shortVideoHeader: false);
			int level = MPEG4Bitstream.level(coeff);
			p += run(coeff);
			if (((uint)p & 0xFFFFFFC0u) != 0)
			{
				Logger.error("invalid run or index");
				break;
			}
			if (level < 0)
			{
				level = level * quant_m_2 - quant_add;
				block[scan[p]] = (short)((level < -2048) ? (-2048) : level);
			}
			else
			{
				level = level * quant_m_2 + quant_add;
				block[scan[p]] = (short)((level > 2047) ? 2047 : level);
			}
			p++;
		}
		while (last(coeff) == 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 103, 107, 45, 39, 199, 106, 107,
		110, 110, 110, 110, 110, 136, 114, 110, 112, 112,
		112, 240, 59, 236, 56, 42, 234, 82
	})]
	private static void initVLCTables()
	{
		for (int intra2 = 0; intra2 < 2; intra2++)
		{
			for (int j = 0; j < 4096; j++)
			{
				vlcTab[intra2][j][3] = 0;
			}
		}
		for (int intra = 0; intra < 2; intra++)
		{
			for (int i = 0; i < 102; i++)
			{
				int len = MPEG4Consts.COEFF_TAB[intra][i][1];
				int last = MPEG4Consts.COEFF_TAB[intra][i][2];
				int run = MPEG4Consts.COEFF_TAB[intra][i][3];
				int level = MPEG4Consts.COEFF_TAB[intra][i][4];
				int code = MPEG4Consts.COEFF_TAB[intra][i][0];
				int lowBits = 12 - len;
				for (int k = 0; k < 1 << lowBits; k++)
				{
					int entry = (code << lowBits) | k;
					vlcTab[intra][entry][0] = (byte)(sbyte)len;
					vlcTab[intra][entry][1] = (byte)(sbyte)last;
					vlcTab[intra][entry][2] = (byte)(sbyte)run;
					vlcTab[intra][entry][3] = (byte)(sbyte)level;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(46)]
	internal MPEG4Bitstream()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 66, 99, 163, 99, 105, 114, 137, 133,
		102, 148
	})]
	internal static void readMatrix(BitReader br, short[] matrix)
	{
		int value = 0;
		int i = 0;
		int last;
		do
		{
			last = value;
			value = br.readNBit(8);
			short[] obj = MPEG4Consts.SCAN_TABLES[0];
			int num = i;
			i++;
			matrix[obj[num]] = (short)value;
		}
		while (value != 0 && i < 64);
		i += -1;
		while (i < 64)
		{
			short[] obj2 = MPEG4Consts.SCAN_TABLES[0];
			int num2 = i;
			i++;
			matrix[obj2[num2]] = (short)last;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 10, 66, 108, 140, 106, 146, 125, 188, 104,
		106, 133, 141, 105, 140, 106, 123, 107, 107, 106,
		200, 109, 159, 124, 105, 141
	})]
	internal static void readIntraMode(BitReader br, MPEG4DecodingContext ctx, Macroblock mb)
	{
		while (br.checkNBit(9) == 1)
		{
			br.skip(9);
		}
		if (checkResyncMarker(br, 0))
		{
			mb.bound = readVideoPacketHeader(br, ctx, 0, fcodeForwardEnabled: false, fcodeBackwardEnabled: false, intraDCThresholdEnabled: true);
			int bound = mb.bound;
			int mbWidth = ctx.mbWidth;
			mb.x = ((mbWidth != -1) ? (bound % mbWidth) : 0);
			int bound2 = mb.bound;
			int mbWidth2 = ctx.mbWidth;
			mb.y = ((mbWidth2 != -1) ? (bound2 / mbWidth2) : (-bound2));
		}
		int mcbpc = readMcbpcIntra(br);
		mb.mode = mcbpc & 7;
		int cbpc = (int)((uint)mcbpc >> 4);
		mb.acpredFlag = br.readBool();
		int cbpy = readCbpy(br, intra: true);
		mb.cbp = (cbpy << 2) | cbpc;
		if (mb.mode == 4)
		{
			ctx.quant += DQUANT_TABLE[br.readNBit(2)];
			if (ctx.quant > 31)
			{
				ctx.quant = 31;
			}
			else if (ctx.quant < 1)
			{
				ctx.quant = 1;
			}
		}
		mb.quant = ctx.quant;
		Macroblock.Vector obj = mb.mvs[0];
		Macroblock.Vector obj2 = mb.mvs[0];
		Macroblock.Vector obj3 = mb.mvs[1];
		Macroblock.Vector obj4 = mb.mvs[1];
		Macroblock.Vector obj5 = mb.mvs[2];
		Macroblock.Vector obj6 = mb.mvs[2];
		Macroblock.Vector obj7 = mb.mvs[3];
		Macroblock.Vector obj8 = mb.mvs[3];
		int num = 0;
		Macroblock.Vector vector = obj8;
		int num2 = num;
		vector.y = num;
		num = num2;
		vector = obj7;
		int num3 = num;
		vector.x = num;
		num = num3;
		vector = obj6;
		int num4 = num;
		vector.y = num;
		num = num4;
		vector = obj5;
		int num5 = num;
		vector.x = num;
		num = num5;
		vector = obj4;
		int num6 = num;
		vector.y = num;
		num = num6;
		vector = obj3;
		int num7 = num;
		vector.x = num;
		num = num7;
		vector = obj2;
		int x = num;
		vector.y = num;
		obj.x = x;
		if (ctx.interlacing)
		{
			mb.fieldDCT = br.readBool();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 237, 130, 108, 136, 99, 131, 104, 106, 133,
		152, 100, 141, 136, 138, 141, 115, 112, 112, 107,
		107, 106, 168, 141, 105, 108, 173, 117, 141, 105,
		109, 237, 69, 100, 110, 126, 117, 113, 191, 10,
		127, 12, 127, 42, 159, 47, 109, 127, 12, 127,
		12, 127, 12, 159, 12, 105, 142, 174, 127, 53,
		127, 53, 143, 102, 105, 141, 127, 53, 127, 53,
		136, 139
	})]
	internal static void readInterModeCoeffs(BitReader br, MPEG4DecodingContext ctx, int fcode, Macroblock mb, Macroblock aboveMb, Macroblock leftMb, Macroblock aboveLeftMb, Macroblock aboveRightMb)
	{
		if (!br.readBool())
		{
			mb.coded = true;
			int intra = 0;
			int mcsel = 0;
			int mcbpc = readMcbpcInter(br);
			mb.mode = mcbpc & 7;
			int cbpc = (int)((uint)mcbpc >> 4);
			intra = ((mb.mode == 3 || mb.mode == 4) ? 1 : 0);
			if (intra != 0)
			{
				mb.acpredFlag = br.readBool();
			}
			mb.mcsel = (byte)mcsel != 0;
			int cbpy = readCbpy(br, (byte)intra != 0);
			mb.cbp = (cbpy << 2) | cbpc;
			if (mb.mode == 1 || mb.mode == 4)
			{
				int dquant = DQUANT_TABLE[br.readNBit(2)];
				ctx.quant += dquant;
				if (ctx.quant > 31)
				{
					ctx.quant = 31;
				}
				else if (ctx.quant < 1)
				{
					ctx.quant = 1;
				}
			}
			mb.quant = ctx.quant;
			if (ctx.interlacing)
			{
				if (mb.cbp != 0 || intra != 0)
				{
					mb.fieldDCT = br.readBool();
				}
				if ((mb.mode == 0 || mb.mode == 1) && mcsel == 0)
				{
					mb.fieldPred = br.readBool();
					if (mb.fieldPred)
					{
						mb.fieldForTop = br.readBool();
						mb.fieldForBottom = br.readBool();
					}
				}
			}
			if (mcsel != 0)
			{
				readInterCoeffs(br, ctx, mb);
			}
			else if (mb.mode == 0 || mb.mode == 1 || mb.mode == 2)
			{
				if (mb.mode == 0 || mb.mode == 1)
				{
					if (ctx.interlacing && mb.fieldPred)
					{
						readMVInterlaced(br, ctx, mb.x, mb.y, 0, mb, fcode, mb.bound, mb, aboveMb, leftMb, aboveRightMb);
					}
					else
					{
						readMV(br, ctx, mb.x, mb.y, 0, mb.mvs[0], fcode, mb.bound, mb, aboveMb, leftMb, aboveRightMb);
						Macroblock.Vector obj = mb.mvs[1];
						Macroblock.Vector obj2 = mb.mvs[2];
						Macroblock.Vector obj3 = mb.mvs[3];
						int x = mb.mvs[0].x;
						Macroblock.Vector vector = obj3;
						int num = x;
						vector.x = x;
						x = num;
						vector = obj2;
						int x2 = x;
						vector.x = x;
						obj.x = x2;
						Macroblock.Vector obj4 = mb.mvs[1];
						Macroblock.Vector obj5 = mb.mvs[2];
						Macroblock.Vector obj6 = mb.mvs[3];
						x = mb.mvs[0].y;
						vector = obj6;
						int num2 = x;
						vector.y = x;
						x = num2;
						vector = obj5;
						int y = x;
						vector.y = x;
						obj4.y = y;
					}
				}
				else if (mb.mode == 2)
				{
					readMV(br, ctx, mb.x, mb.y, 0, mb.mvs[0], fcode, mb.bound, mb, aboveMb, leftMb, aboveRightMb);
					readMV(br, ctx, mb.x, mb.y, 1, mb.mvs[1], fcode, mb.bound, mb, aboveMb, leftMb, aboveRightMb);
					readMV(br, ctx, mb.x, mb.y, 2, mb.mvs[2], fcode, mb.bound, mb, aboveMb, leftMb, aboveRightMb);
					readMV(br, ctx, mb.x, mb.y, 3, mb.mvs[3], fcode, mb.bound, mb, aboveMb, leftMb, aboveRightMb);
				}
				if (!mb.fieldPred)
				{
					readInterCoeffs(br, ctx, mb);
				}
				else
				{
					readInterCoeffs(br, ctx, mb);
				}
			}
			else
			{
				Macroblock.Vector obj7 = mb.mvs[0];
				Macroblock.Vector obj8 = mb.mvs[1];
				Macroblock.Vector obj9 = mb.mvs[2];
				Macroblock.Vector obj10 = mb.mvs[3];
				int x = 0;
				Macroblock.Vector vector = obj10;
				int num3 = x;
				vector.x = x;
				x = num3;
				vector = obj9;
				int num4 = x;
				vector.x = x;
				x = num4;
				vector = obj8;
				int x3 = x;
				vector.x = x;
				obj7.x = x3;
				Macroblock.Vector obj11 = mb.mvs[0];
				Macroblock.Vector obj12 = mb.mvs[1];
				Macroblock.Vector obj13 = mb.mvs[2];
				Macroblock.Vector obj14 = mb.mvs[3];
				x = 0;
				vector = obj14;
				int num5 = x;
				vector.y = x;
				x = num5;
				vector = obj13;
				int num6 = x;
				vector.y = x;
				x = num6;
				vector = obj12;
				int y2 = x;
				vector.y = x;
				obj11.y = y2;
				readCoeffIntra(br, ctx, mb, aboveMb, leftMb, aboveLeftMb);
			}
		}
		else
		{
			mb.mode = 16;
			mb.quant = ctx.quant;
			Macroblock.Vector obj15 = mb.mvs[0];
			Macroblock.Vector obj16 = mb.mvs[1];
			Macroblock.Vector obj17 = mb.mvs[2];
			Macroblock.Vector obj18 = mb.mvs[3];
			int x = 0;
			Macroblock.Vector vector = obj18;
			int num7 = x;
			vector.x = x;
			x = num7;
			vector = obj17;
			int num8 = x;
			vector.x = x;
			x = num8;
			vector = obj16;
			int x4 = x;
			vector.x = x;
			obj15.x = x4;
			Macroblock.Vector obj19 = mb.mvs[0];
			Macroblock.Vector obj20 = mb.mvs[1];
			Macroblock.Vector obj21 = mb.mvs[2];
			Macroblock.Vector obj22 = mb.mvs[3];
			x = 0;
			vector = obj22;
			int num9 = x;
			vector.y = x;
			x = num9;
			vector = obj21;
			int num10 = x;
			vector.y = x;
			x = num10;
			vector = obj20;
			int y3 = x;
			vector.y = x;
			obj19.y = y3;
			mb.cbp = 0;
			readInterCoeffs(br, ctx, mb);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		106,
		162,
		108,
		136,
		142,
		100,
		145,
		169,
		115,
		117,
		108,
		108,
		107,
		169,
		105,
		106,
		174,
		106,
		142,
		106,
		110,
		238,
		69,
		99,
		105,
		169,
		103,
		159,
		7,
		188,
		106,
		127,
		27,
		159,
		27,
		159,
		66,
		byte.MaxValue,
		66,
		58,
		234,
		74,
		106,
		166,
		127,
		2,
		127,
		54,
		159,
		54,
		127,
		2,
		127,
		54,
		159,
		54,
		106,
		166,
		127,
		2,
		127,
		54,
		159,
		54,
		106,
		166,
		127,
		2,
		127,
		54,
		159,
		54,
		106,
		195
	})]
	internal static void readBi(BitReader br, MPEG4DecodingContext ctx, int fcodeForward, int fcodeBackward, Macroblock mb, Macroblock lastMB, Macroblock.Vector pFMV, Macroblock.Vector pBMV)
	{
		if (!br.readBool())
		{
			int modb2 = (br.readBool() ? 1 : 0);
			mb.mode = readMBType(br);
			if (modb2 == 0)
			{
				mb.cbp = br.readNBit(6);
			}
			else
			{
				mb.cbp = 0;
			}
			if (mb.mode != 0 && mb.cbp != 0)
			{
				mb.quant += readDBQuant(br);
				if (mb.quant > 31)
				{
					mb.quant = 31;
				}
				else if (mb.quant < 1)
				{
					mb.quant = 1;
				}
			}
			if (ctx.interlacing)
			{
				if (mb.cbp != 0)
				{
					mb.fieldDCT = br.readBool();
				}
				if (mb.mode != 0)
				{
					mb.fieldPred = br.readBool();
					if (mb.fieldPred)
					{
						mb.fieldForTop = br.readBool();
						mb.fieldForBottom = br.readBool();
					}
				}
			}
		}
		else
		{
			mb.mode = 4;
			mb.cbp = 0;
		}
		Macroblock.Vector mv = Macroblock.vec();
		switch (mb.mode)
		{
		case 0:
			getBMotionVector(br, mv, 1, MPEG4Consts.ZERO_MV, mb.x, mb.y);
			goto case 4;
		case 4:
		{
			for (int i = 0; i < 4; i++)
			{
				Macroblock.Vector obj25 = mb.mvs[i];
				int num17 = lastMB.mvs[i].x * ctx.bframeTs;
				int pframeTs = ctx.pframeTs;
				obj25.x = ((pframeTs != -1) ? (num17 / pframeTs) : (-num17)) + mv.x;
				Macroblock.Vector obj26 = mb.mvs[i];
				int num18 = lastMB.mvs[i].y * ctx.bframeTs;
				int pframeTs2 = ctx.pframeTs;
				obj26.y = ((pframeTs2 != -1) ? (num18 / pframeTs2) : (-num18)) + mv.y;
				Macroblock.Vector obj27 = mb.bmvs[i];
				int x6;
				if (mv.x != 0)
				{
					x6 = mb.mvs[i].x - lastMB.mvs[i].x;
				}
				else
				{
					int num19 = lastMB.mvs[i].x * (ctx.bframeTs - ctx.pframeTs);
					int pframeTs3 = ctx.pframeTs;
					x6 = ((pframeTs3 != -1) ? (num19 / pframeTs3) : (-num19));
				}
				obj27.x = x6;
				Macroblock.Vector obj28 = mb.bmvs[i];
				int y5;
				if (mv.y != 0)
				{
					y5 = mb.mvs[i].y - lastMB.mvs[i].y;
				}
				else
				{
					int num20 = lastMB.mvs[i].y * (ctx.bframeTs - ctx.pframeTs);
					int pframeTs4 = ctx.pframeTs;
					y5 = ((pframeTs4 != -1) ? (num20 / pframeTs4) : (-num20));
				}
				obj28.y = y5;
			}
			readInterCoeffs(br, ctx, mb);
			break;
		}
		case 1:
		{
			getBMotionVector(br, mb.mvs[0], fcodeForward, pFMV, mb.x, mb.y);
			Macroblock.Vector obj13 = mb.mvs[1];
			Macroblock.Vector obj14 = mb.mvs[2];
			Macroblock.Vector obj15 = mb.mvs[3];
			int x = mb.mvs[0].x;
			Macroblock.Vector vector = obj15;
			int num9 = x;
			vector.x = x;
			x = num9;
			vector = obj14;
			int num10 = x;
			vector.x = x;
			x = num10;
			vector = obj13;
			int x4 = x;
			vector.x = x;
			pFMV.x = x4;
			Macroblock.Vector obj16 = mb.mvs[1];
			Macroblock.Vector obj17 = mb.mvs[2];
			Macroblock.Vector obj18 = mb.mvs[3];
			x = mb.mvs[0].y;
			vector = obj18;
			int num11 = x;
			vector.y = x;
			x = num11;
			vector = obj17;
			int num12 = x;
			vector.y = x;
			x = num12;
			vector = obj16;
			int y3 = x;
			vector.y = x;
			pFMV.y = y3;
			getBMotionVector(br, mb.bmvs[0], fcodeBackward, pBMV, mb.x, mb.y);
			Macroblock.Vector obj19 = mb.bmvs[1];
			Macroblock.Vector obj20 = mb.bmvs[2];
			Macroblock.Vector obj21 = mb.bmvs[3];
			x = mb.bmvs[0].x;
			vector = obj21;
			int num13 = x;
			vector.x = x;
			x = num13;
			vector = obj20;
			int num14 = x;
			vector.x = x;
			x = num14;
			vector = obj19;
			int x5 = x;
			vector.x = x;
			pBMV.x = x5;
			Macroblock.Vector obj22 = mb.bmvs[1];
			Macroblock.Vector obj23 = mb.bmvs[2];
			Macroblock.Vector obj24 = mb.bmvs[3];
			x = mb.bmvs[0].y;
			vector = obj24;
			int num15 = x;
			vector.y = x;
			x = num15;
			vector = obj23;
			int num16 = x;
			vector.y = x;
			x = num16;
			vector = obj22;
			int y4 = x;
			vector.y = x;
			pBMV.y = y4;
			readInterCoeffs(br, ctx, mb);
			break;
		}
		case 2:
		{
			getBMotionVector(br, mb.mvs[0], fcodeBackward, pBMV, mb.x, mb.y);
			Macroblock.Vector obj7 = mb.mvs[1];
			Macroblock.Vector obj8 = mb.mvs[2];
			Macroblock.Vector obj9 = mb.mvs[3];
			int x = mb.mvs[0].x;
			Macroblock.Vector vector = obj9;
			int num5 = x;
			vector.x = x;
			x = num5;
			vector = obj8;
			int num6 = x;
			vector.x = x;
			x = num6;
			vector = obj7;
			int x3 = x;
			vector.x = x;
			pBMV.x = x3;
			Macroblock.Vector obj10 = mb.mvs[1];
			Macroblock.Vector obj11 = mb.mvs[2];
			Macroblock.Vector obj12 = mb.mvs[3];
			x = mb.mvs[0].y;
			vector = obj12;
			int num7 = x;
			vector.y = x;
			x = num7;
			vector = obj11;
			int num8 = x;
			vector.y = x;
			x = num8;
			vector = obj10;
			int y2 = x;
			vector.y = x;
			pBMV.y = y2;
			readInterCoeffs(br, ctx, mb);
			break;
		}
		case 3:
		{
			getBMotionVector(br, mb.mvs[0], fcodeForward, pFMV, mb.x, mb.y);
			Macroblock.Vector obj = mb.mvs[1];
			Macroblock.Vector obj2 = mb.mvs[2];
			Macroblock.Vector obj3 = mb.mvs[3];
			int x = mb.mvs[0].x;
			Macroblock.Vector vector = obj3;
			int num = x;
			vector.x = x;
			x = num;
			vector = obj2;
			int num2 = x;
			vector.x = x;
			x = num2;
			vector = obj;
			int x2 = x;
			vector.x = x;
			pFMV.x = x2;
			Macroblock.Vector obj4 = mb.mvs[1];
			Macroblock.Vector obj5 = mb.mvs[2];
			Macroblock.Vector obj6 = mb.mvs[3];
			x = mb.mvs[0].y;
			vector = obj6;
			int num3 = x;
			vector.y = x;
			x = num3;
			vector = obj5;
			int num4 = x;
			vector.y = x;
			x = num4;
			vector = obj4;
			int y = x;
			vector.y = x;
			pFMV.y = y;
			readInterCoeffs(br, ctx, mb);
			break;
		}
		}
	}

	[LineNumberTable(new byte[] { 159, 125, 130, 157, 191, 20, 104 })]
	static MPEG4Bitstream()
	{
		DQUANT_TABLE = new int[4] { -1, -2, 1, 2 };
		int[] array = new int[3];
		int num = (array[2] = 4);
		num = (array[1] = 4096);
		num = (array[0] = 2);
		vlcTab = (byte[][][])ByteCodeHelper.multianewarray(typeof(byte[][][]).TypeHandle, array);
		initVLCTables();
	}
}
