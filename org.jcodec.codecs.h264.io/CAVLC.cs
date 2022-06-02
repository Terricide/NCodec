using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.io;

public class CAVLC : Object, SaveRestore
{
	private ColorSpace color;

	private VLC chromaDCVLC;

	private int[] tokensLeft;

	private int[] tokensTop;

	private int[] tokensLeftSaved;

	private int[] tokensTopSaved;

	private int mbWidth;

	private int mbMask;

	public static int[] NO_ZIGZAG;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 98, 105, 109, 109, 143, 144, 109, 119,
		109, 119
	})]
	public CAVLC(SeqParameterSet sps, PictureParameterSet pps, int mbW, int mbH)
	{
		color = sps.chromaFormatIdc;
		chromaDCVLC = codeTableChromaDC();
		mbWidth = sps.picWidthInMbsMinus1 + 1;
		mbMask = (1 << mbH) - 1;
		tokensLeft = new int[4];
		tokensTop = new int[mbWidth << mbW];
		tokensLeftSaved = new int[4];
		tokensTopSaved = new int[mbWidth << mbW];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 59, 129, 71, 191, 1, 117 })]
	public virtual void readLumaDCBlock(BitReader reader, int[] coeff, int mbX, bool leftAvailable, MBType leftMbType, bool topAvailable, MBType topMbType, int[] zigzag4x4)
	{
		VLC coeffTokenTab = getCoeffTokenVLCForLuma(leftAvailable, leftMbType, tokensLeft[0], topAvailable, topMbType, tokensTop[mbX << 2]);
		readCoeffs(reader, coeffTokenTab, H264Const.___003C_003EtotalZeros16, coeff, 0, 16, zigzag4x4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 57, 129, 71, 191, 7, 118, 159, 9 })]
	public virtual int readACBlock(BitReader reader, int[] coeff, int blkIndX, int blkIndY, bool leftAvailable, MBType leftMbType, bool topAvailable, MBType topMbType, int firstCoeff, int nCoeff, int[] zigzag4x4)
	{
		VLC coeffTokenTab = getCoeffTokenVLCForLuma(leftAvailable, leftMbType, tokensLeft[blkIndY & mbMask], topAvailable, topMbType, tokensTop[blkIndX]);
		int readCoeffs = this.readCoeffs(reader, coeffTokenTab, H264Const.___003C_003EtotalZeros16, coeff, firstCoeff, nCoeff, zigzag4x4);
		int[] array = tokensLeft;
		int num = blkIndY & mbMask;
		int[] array2 = tokensTop;
		int num2 = readCoeffs;
		int[] array3 = array2;
		array3[blkIndX] = num2;
		array[num] = num2;
		int result = totalCoeff(readCoeffs);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 54, 66, 127, 1 })]
	public virtual void setZeroCoeff(int blkIndX, int blkIndY)
	{
		int[] array = tokensLeft;
		int num = blkIndY & mbMask;
		int[] array2 = tokensTop;
		int num2 = 0;
		int[] array3 = array2;
		array3[blkIndX] = num2;
		array[num] = num2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 61, 98, 136, 191, 18 })]
	public virtual void readChromaDCBlock(BitReader reader, int[] coeff, bool leftAvailable, bool topAvailable)
	{
		VLC coeffTokenTab = getCoeffTokenVLCForChromaDC();
		readCoeffs(reader, coeffTokenTab, ((nint)coeff.LongLength == 16) ? H264Const.___003C_003EtotalZeros16 : (((nint)coeff.LongLength != 8) ? H264Const.___003C_003EtotalZeros4 : H264Const.___003C_003EtotalZeros8), coeff, 0, coeff.Length, NO_ZIGZAG);
	}

	[LineNumberTable(new byte[] { 159, 128, 66, 123, 125 })]
	public virtual void save()
	{
		ByteCodeHelper.arraycopy_primitive_4(tokensLeft, 0, tokensLeftSaved, 0, tokensLeft.Length);
		ByteCodeHelper.arraycopy_primitive_4(tokensTop, 0, tokensTopSaved, 0, tokensTop.Length);
	}

	[LineNumberTable(new byte[] { 159, 127, 162, 104, 109, 168, 104, 109, 136 })]
	public virtual void restore()
	{
		int[] tmp2 = tokensLeft;
		tokensLeft = tokensLeftSaved;
		tokensLeftSaved = tmp2;
		int[] tmp = tokensTop;
		tokensTop = tokensTopSaved;
		tokensTopSaved = tmp;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 66, 191, 18, 148, 113, 138 })]
	public virtual int writeACBlock(BitWriter @out, int blkIndX, int blkIndY, MBType leftMBType, MBType topMBType, int[] coeff, VLC[] totalZerosTab, int firstCoeff, int maxCoeff, int[] scan)
	{
		VLC coeffTokenTab = getCoeffTokenVLCForLuma((blkIndX != 0) ? true : false, leftMBType, tokensLeft[blkIndY & mbMask], (blkIndY != 0) ? true : false, topMBType, tokensTop[blkIndX]);
		int coeffToken = writeBlockGen(@out, coeff, totalZerosTab, firstCoeff, maxCoeff, scan, coeffTokenTab);
		tokensLeft[blkIndY & mbMask] = coeffToken;
		tokensTop[blkIndX] = coeffToken;
		return coeffToken;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(315)]
	public static int totalCoeff(int coeffToken)
	{
		return coeffToken >> 4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 98, 119 })]
	public virtual void writeChrDCBlock(BitWriter @out, int[] coeff, VLC[] totalZerosTab, int firstCoeff, int maxCoeff, int[] scan)
	{
		writeBlockGen(@out, coeff, totalZerosTab, firstCoeff, maxCoeff, scan, getCoeffTokenVLCForChromaDC());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 130, 191, 18, 116 })]
	public virtual void writeLumaDCBlock(BitWriter @out, int blkIndX, int blkIndY, MBType leftMBType, MBType topMBType, int[] coeff, VLC[] totalZerosTab, int firstCoeff, int maxCoeff, int[] scan)
	{
		VLC coeffTokenTab = getCoeffTokenVLCForLuma((blkIndX != 0) ? true : false, leftMBType, tokensLeft[blkIndY & mbMask], (blkIndY != 0) ? true : false, topMBType, tokensTop[blkIndX]);
		writeBlockGen(@out, coeff, totalZerosTab, firstCoeff, maxCoeff, scan, coeffTokenTab);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 88, 66, 110, 103, 110, 103, 110, 137 })]
	protected internal virtual VLC codeTableChromaDC()
	{
		if (color == ColorSpace.___003C_003EYUV420J)
		{
			return H264Const.___003C_003EcoeffTokenChromaDCY420;
		}
		if (color == ColorSpace.___003C_003EYUV422)
		{
			return H264Const.___003C_003EcoeffTokenChromaDCY422;
		}
		if (color == ColorSpace.___003C_003EYUV444)
		{
			return H264Const.___003C_003ECoeffToken[0];
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 129, 70, 144 })]
	public virtual VLC getCoeffTokenVLCForLuma(bool leftAvailable, MBType leftMBType, int leftToken, bool topAvailable, MBType topMBType, int topToken)
	{
		int nc = codeTableLuma(leftAvailable, leftMBType, leftToken, topAvailable, topMBType, topToken);
		return H264Const.___003C_003ECoeffToken[Math.min(nc, 8)];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 117, 130, 103, 105, 106, 106, 109, 101, 115,
		135, 235, 58, 233, 73, 102, 135, 99, 191, 0,
		138, 139, 101, 108, 140, 102, 109, 171
	})]
	private int writeBlockGen(BitWriter @out, int[] coeff, VLC[] totalZerosTab, int firstCoeff, int maxCoeff, int[] scan, VLC coeffTokenTab)
	{
		int trailingOnes = 0;
		int totalCoeff = 0;
		int totalZeros = 0;
		int[] runBefore = new int[maxCoeff];
		int[] levels = new int[maxCoeff];
		for (int i = 0; i < maxCoeff; i++)
		{
			int c = coeff[scan[i + firstCoeff]];
			if (c == 0)
			{
				int num = totalCoeff;
				int[] array = runBefore;
				array[num]++;
				totalZeros++;
			}
			else
			{
				int num2 = totalCoeff;
				totalCoeff++;
				levels[num2] = c;
			}
		}
		if (totalCoeff < maxCoeff)
		{
			totalZeros -= runBefore[totalCoeff];
		}
		for (trailingOnes = 0; trailingOnes < totalCoeff && trailingOnes < 3 && Math.abs(levels[totalCoeff - trailingOnes - 1]) == 1; trailingOnes++)
		{
		}
		int coeffToken = H264Const.coeffToken(totalCoeff, trailingOnes);
		coeffTokenTab.writeVLC(@out, coeffToken);
		if (totalCoeff > 0)
		{
			writeTrailingOnes(@out, levels, totalCoeff, trailingOnes);
			writeLevels(@out, levels, totalCoeff, trailingOnes);
			if (totalCoeff < maxCoeff)
			{
				totalZerosTab[totalCoeff - 1].writeVLC(@out, totalZeros);
				writeRuns(@out, runBefore, totalCoeff, totalZeros);
			}
		}
		return coeffToken;
	}

	[LineNumberTable(196)]
	public virtual VLC getCoeffTokenVLCForChromaDC()
	{
		return chromaDCVLC;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 130, 108, 45, 135 })]
	private void writeTrailingOnes(BitWriter @out, int[] levels, int totalCoeff, int trailingOne)
	{
		for (int i = totalCoeff - 1; i >= totalCoeff - trailingOne; i += -1)
		{
			@out.write1Bit((int)((uint)levels[i] >> 31));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 66, 112, 111, 107, 111, 134, 104, 114,
		107, 110, 105, 106, 145, 100, 134, 159, 19, 108,
		139, 100, 99, 119, 229, 40, 234, 90
	})]
	private void writeLevels(BitWriter @out, int[] levels, int totalCoeff, int trailingOnes)
	{
		int suffixLen = ((totalCoeff > 10 && trailingOnes < 3) ? 1 : 0);
		for (int i = totalCoeff - trailingOnes - 1; i >= 0; i += -1)
		{
			int absLev = unsigned(levels[i]);
			if (i == totalCoeff - trailingOnes - 1 && trailingOnes < 3)
			{
				absLev += -2;
			}
			int prefix = absLev >> suffixLen;
			if ((suffixLen == 0 && prefix < 14) || (suffixLen > 0 && prefix < 15))
			{
				@out.writeNBit(1, prefix + 1);
				@out.writeNBit(absLev, suffixLen);
			}
			else if (suffixLen == 0 && absLev < 30)
			{
				@out.writeNBit(1, 15);
				@out.writeNBit(absLev - 14, 4);
			}
			else
			{
				if (suffixLen == 0)
				{
					absLev += -15;
				}
				int len = 12;
				int code;
				while ((code = absLev - (len + 3 << suffixLen) - (1 << len) + 4096) >= 1 << len)
				{
					len++;
				}
				@out.writeNBit(1, len + 4);
				@out.writeNBit(code, len);
			}
			if (suffixLen == 0)
			{
				suffixLen = 1;
			}
			if (MathUtil.abs(levels[i]) > 3 << suffixLen - 1 && suffixLen < 6)
			{
				suffixLen++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 98, 110, 122, 9, 199 })]
	private void writeRuns(BitWriter @out, int[] run, int totalCoeff, int totalZeros)
	{
		for (int i = totalCoeff - 1; i > 0; i += -1)
		{
			if (totalZeros <= 0)
			{
				break;
			}
			H264Const.___003C_003Erun[Math.min(6, totalZeros - 1)].writeVLC(@out, run[i]);
			totalZeros -= run[i];
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 99, 130, 102, 134 })]
	private int unsigned(int signed)
	{
		int sign = (int)((uint)signed >> 31);
		int s = signed >> 31;
		return ((signed ^ s) - s << 1) + sign - 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 92, 129, 70, 110, 144, 103, 105, 100, 99,
		100, 131
	})]
	protected internal virtual int codeTableLuma(bool leftAvailable, MBType leftMBType, int leftToken, bool topAvailable, MBType topMBType, int topToken)
	{
		int nA = ((leftMBType != null) ? totalCoeff(leftToken) : 0);
		int nB = ((topMBType != null) ? totalCoeff(topToken) : 0);
		if (leftAvailable && topAvailable)
		{
			return nA + nB + 1 >> 1;
		}
		if (leftAvailable)
		{
			return nA;
		}
		if (topAvailable)
		{
			return nB;
		}
		return 0;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(319)]
	public static int trailingOnes(int coeffToken)
	{
		return coeffToken & 0xF;
	}

	[LineNumberTable(307)]
	private static int Min(int i, int level_prefix)
	{
		return (i >= level_prefix) ? level_prefix : i;
	}

	[LineNumberTable(311)]
	private static int Abs(int i)
	{
		return (i >= 0) ? i : (-i);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 85, 66, 105, 104, 136, 104, 143, 137, 105,
		48, 169, 105, 110, 100, 106, 100, 103, 135, 113,
		102, 112, 136, 106, 104, 103, 117, 106, 135, 112,
		142, 141, 100, 99, 121, 229, 36, 236, 96, 102,
		103, 116, 103, 148, 180, 132, 137, 112, 121, 104,
		232, 61, 233, 69, 136, 116, 107, 17, 239, 75
	})]
	public virtual int readCoeffs(BitReader _in, VLC coeffTokenTab, VLC[] totalZerosTab, int[] coeffLevel, int firstCoeff, int nCoeff, int[] zigzag)
	{
		int coeffToken = coeffTokenTab.readVLC(_in);
		int totalCoeff = CAVLC.totalCoeff(coeffToken);
		int trailingOnes = CAVLC.trailingOnes(coeffToken);
		if (totalCoeff > 0)
		{
			int suffixLength = ((totalCoeff > 10 && trailingOnes < 3) ? 1 : 0);
			int[] level = new int[totalCoeff];
			int i;
			for (i = 0; i < trailingOnes; i++)
			{
				level[i] = 1 - 2 * _in.read1Bit();
			}
			for (; i < totalCoeff; i++)
			{
				int level_prefix = CAVLCReader.readZeroBitCount(_in, "");
				int levelSuffixSize = suffixLength;
				if (level_prefix == 14 && suffixLength == 0)
				{
					levelSuffixSize = 4;
				}
				if (level_prefix >= 15)
				{
					levelSuffixSize = level_prefix - 3;
				}
				int levelCode = Min(15, level_prefix) << suffixLength;
				if (levelSuffixSize > 0)
				{
					int level_suffix = CAVLCReader.readU(_in, levelSuffixSize, "RB: level_suffix");
					levelCode += level_suffix;
				}
				if (level_prefix >= 15 && suffixLength == 0)
				{
					levelCode += 15;
				}
				if (level_prefix >= 16)
				{
					levelCode += (1 << level_prefix - 3) - 4096;
				}
				if (i == trailingOnes && trailingOnes < 3)
				{
					levelCode += 2;
				}
				int num = levelCode;
				if (2 == -1 || num % 2 == 0)
				{
					level[i] = levelCode + 2 >> 1;
				}
				else
				{
					level[i] = -levelCode - 1 >> 1;
				}
				if (suffixLength == 0)
				{
					suffixLength = 1;
				}
				if (Abs(level[i]) > 3 << suffixLength - 1 && suffixLength < 6)
				{
					suffixLength++;
				}
			}
			int zerosLeft = ((totalCoeff < nCoeff) ? (((nint)coeffLevel.LongLength == 4) ? H264Const.___003C_003EtotalZeros4[totalCoeff - 1].readVLC(_in) : (((nint)coeffLevel.LongLength != 8) ? H264Const.___003C_003EtotalZeros16[totalCoeff - 1].readVLC(_in) : H264Const.___003C_003EtotalZeros8[totalCoeff - 1].readVLC(_in))) : 0);
			int[] runs = new int[totalCoeff];
			int r;
			for (r = 0; r < totalCoeff - 1; r++)
			{
				if (zerosLeft <= 0)
				{
					break;
				}
				int run = H264Const.___003C_003Erun[Math.min(6, zerosLeft - 1)].readVLC(_in);
				zerosLeft -= run;
				runs[r] = run;
			}
			runs[r] = zerosLeft;
			int j = totalCoeff - 1;
			int cn = 0;
			while (j >= 0 && cn < nCoeff)
			{
				cn += runs[j];
				coeffLevel[zigzag[cn + firstCoeff]] = level[j];
				j += -1;
				cn++;
			}
		}
		return coeffToken;
	}

	[LineNumberTable(322)]
	static CAVLC()
	{
		NO_ZIGZAG = new int[16]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15
		};
	}
}
