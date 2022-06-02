using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.common.biari;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.io;

public class CABAC : Object
{
	public sealed class BlockType : Object
	{
		internal static BlockType ___003C_003ELUMA_16_DC;

		internal static BlockType ___003C_003ELUMA_15_AC;

		internal static BlockType ___003C_003ELUMA_16;

		internal static BlockType ___003C_003ECHROMA_DC;

		internal static BlockType ___003C_003ECHROMA_AC;

		internal static BlockType ___003C_003ELUMA_64;

		internal static BlockType ___003C_003ECB_16_DC;

		internal static BlockType ___003C_003ECB_15x16_AC;

		internal static BlockType ___003C_003ECB_16;

		internal static BlockType ___003C_003ECB_64;

		internal static BlockType ___003C_003ECR_16_DC;

		internal static BlockType ___003C_003ECR_15x16_AC;

		internal static BlockType ___003C_003ECR_16;

		internal static BlockType ___003C_003ECR_64;

		public int codedBlockCtxOff;

		public int sigCoeffFlagCtxOff;

		public int lastSigCoeffCtxOff;

		public int sigCoeffFlagFldCtxOff;

		public int lastSigCoeffFldCtxOff;

		public int coeffAbsLevelCtxOff;

		public int coeffAbsLevelAdjust;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType LUMA_16_DC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELUMA_16_DC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType LUMA_15_AC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELUMA_15_AC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType LUMA_16
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELUMA_16;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CHROMA_DC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECHROMA_DC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CHROMA_AC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECHROMA_AC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType LUMA_64
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELUMA_64;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CB_16_DC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECB_16_DC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CB_15x16_AC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECB_15x16_AC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CB_16
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECB_16;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CB_64
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECB_64;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CR_16_DC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECR_16_DC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CR_15x16_AC
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECR_15x16_AC;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CR_16
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECR_16;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static BlockType CR_64
		{
			[HideFromJava]
			get
			{
				return ___003C_003ECR_64;
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
			159, 128, 162, 105, 104, 104, 104, 105, 105, 105,
			105
		})]
		private BlockType(int codecBlockCtxOff, int sigCoeffCtxOff, int lastSigCoeffCtxOff, int sigCoeffFlagFldCtxOff, int lastSigCoeffFldCtxOff, int coeffAbsLevelCtxOff, int coeffAbsLevelAdjust)
		{
			codedBlockCtxOff = codecBlockCtxOff;
			sigCoeffFlagCtxOff = sigCoeffCtxOff;
			this.lastSigCoeffCtxOff = lastSigCoeffCtxOff;
			this.sigCoeffFlagFldCtxOff = sigCoeffFlagFldCtxOff;
			this.lastSigCoeffFldCtxOff = sigCoeffFlagFldCtxOff;
			this.coeffAbsLevelCtxOff = coeffAbsLevelCtxOff;
			this.coeffAbsLevelAdjust = coeffAbsLevelAdjust;
		}

		[LineNumberTable(new byte[]
		{
			159, 134, 162, 127, 5, 127, 5, 127, 8, 127,
			8, 127, 8, 127, 11, 127, 11, 127, 11, 127,
			11, 127, 11, 127, 11, 127, 11, 127, 11
		})]
		static BlockType()
		{
			___003C_003ELUMA_16_DC = new BlockType(85, 105, 166, 277, 338, 227, 0);
			___003C_003ELUMA_15_AC = new BlockType(89, 120, 181, 292, 353, 237, 0);
			___003C_003ELUMA_16 = new BlockType(93, 134, 195, 306, 367, 247, 0);
			___003C_003ECHROMA_DC = new BlockType(97, 149, 210, 321, 382, 257, 1);
			___003C_003ECHROMA_AC = new BlockType(101, 152, 213, 324, 385, 266, 0);
			___003C_003ELUMA_64 = new BlockType(1012, 402, 417, 436, 451, 426, 0);
			___003C_003ECB_16_DC = new BlockType(460, 484, 572, 776, 864, 952, 0);
			___003C_003ECB_15x16_AC = new BlockType(464, 499, 587, 791, 879, 962, 0);
			___003C_003ECB_16 = new BlockType(468, 513, 601, 805, 893, 972, 0);
			___003C_003ECB_64 = new BlockType(1016, 660, 690, 675, 699, 708, 0);
			___003C_003ECR_16_DC = new BlockType(472, 528, 616, 820, 908, 982, 0);
			___003C_003ECR_15x16_AC = new BlockType(476, 543, 631, 835, 923, 992, 0);
			___003C_003ECR_16 = new BlockType(480, 557, 645, 849, 937, 1002, 0);
			___003C_003ECR_64 = new BlockType(1020, 718, 748, 733, 757, 766, 0);
		}
	}

	private int chromaPredModeLeft;

	private int[] chromaPredModeTop;

	private int prevMbQpDelta;

	private int prevCBP;

	private int[][] codedBlkLeft;

	private int[][] codedBlkTop;

	private int[] codedBlkDCLeft;

	private int[][] codedBlkDCTop;

	private int[][] refIdxLeft;

	private int[][] refIdxTop;

	private bool skipFlagLeft;

	private bool[] skipFlagsTop;

	private int[][][] mvdTop;

	private int[][][] mvdLeft;

	public int[] tmp;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 105, 110, 104, 109, 127, 9, 159,
		15, 109, 159, 11, 127, 11, 159, 13, 141, 127,
		19, 127, 17
	})]
	public CABAC(int mbWidth)
	{
		tmp = new int[16];
		chromaPredModeLeft = 0;
		chromaPredModeTop = new int[mbWidth];
		codedBlkLeft = new int[3][]
		{
			new int[4],
			new int[2],
			new int[2]
		};
		codedBlkTop = new int[3][]
		{
			new int[mbWidth << 2],
			new int[mbWidth << 1],
			new int[mbWidth << 1]
		};
		codedBlkDCLeft = new int[3];
		int[] array = new int[2];
		int num = (array[1] = mbWidth);
		num = (array[0] = 3);
		codedBlkDCTop = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 2);
		refIdxLeft = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int num2 = mbWidth << 2;
		array = new int[2];
		num = (array[1] = num2);
		num = (array[0] = 2);
		refIdxTop = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		skipFlagsTop = new bool[mbWidth];
		int num3 = mbWidth << 2;
		array = new int[3];
		num = (array[2] = num3);
		num = (array[1] = 2);
		num = (array[0] = 2);
		mvdTop = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 4);
		num = (array[1] = 2);
		num = (array[0] = 2);
		mvdLeft = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 130, 152, 184, 110, 126, 102, 106, 137,
		106, 231, 57, 234, 74
	})]
	public virtual void initModels(int[][] cm, SliceType sliceType, int cabacIdc, int sliceQp)
	{
		int[] tabA = ((!sliceType.isIntra()) ? CABACContst.cabac_context_init_PB_A[cabacIdc] : CABACContst.cabac_context_init_I_A);
		int[] tabB = ((!sliceType.isIntra()) ? CABACContst.cabac_context_init_PB_B[cabacIdc] : CABACContst.cabac_context_init_I_B);
		for (int i = 0; i < 1024; i++)
		{
			int preCtxState = MathUtil.clip((tabA[i] * MathUtil.clip(sliceQp, 0, 51) >> 4) + tabB[i], 1, 126);
			if (preCtxState <= 63)
			{
				cm[0][i] = 63 - preCtxState;
				cm[1][i] = 0;
			}
			else
			{
				cm[0][i] = preCtxState - 64;
				cm[1][i] = 1;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 48, 162, 100, 191, 12, 99, 107, 101, 108,
		101, 108, 167, 141
	})]
	public virtual int readMBQpDelta(MDecoder decoder, MBType prevMbType)
	{
		int ctx = 60;
		ctx += ((prevMbType != null && prevMbType != MBType.___003C_003EI_PCM && (prevMbType == MBType.___003C_003EI_16x16 || prevCBP != 0) && prevMbQpDelta != 0) ? 1 : 0);
		int val = 0;
		if (decoder.decodeBin(ctx) == 1)
		{
			val++;
			if (decoder.decodeBin(62) == 1)
			{
				val++;
				while (decoder.decodeBin(63) == 1)
				{
					val++;
				}
			}
		}
		prevMbQpDelta = H264Utils2.golomb2Signed(val);
		return prevMbQpDelta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 38, 161, 71, 100, 126, 159, 3, 106, 101,
		107, 101, 107, 133, 99, 158
	})]
	public virtual int readIntraChromaPredMode(MDecoder decoder, int mbX, MBType left, MBType top, bool leftAvailable, bool topAvailable)
	{
		int ctx = 64;
		ctx += ((leftAvailable && left != null && left.isIntra() && chromaPredModeLeft != 0) ? 1 : 0);
		ctx += ((topAvailable && top != null && top.isIntra() && chromaPredModeTop[mbX] != 0) ? 1 : 0);
		int mode = ((decoder.decodeBin(ctx) != 0) ? ((decoder.decodeBin(67) == 0) ? 1 : ((decoder.decodeBin(67) != 0) ? 3 : 2)) : 0);
		int[] array = chromaPredModeTop;
		int num = mode;
		int[] array2 = array;
		array2[mbX] = num;
		chromaPredModeLeft = num;
		return mode;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 213, 129, 75, 159, 7 })]
	public virtual bool readTransform8x8Flag(MDecoder mDecoder, bool leftAvailable, bool topAvailable, MBType leftType, MBType topType, bool is8x8Left, bool is8x8Top)
	{
		int ctx = 399 + ((leftAvailable && leftType != null && is8x8Left) ? 1 : 0) + ((topAvailable && topType != null && is8x8Top) ? 1 : 0);
		return mDecoder.decodeBin(ctx) == 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 253, 65, 69, 127, 0, 40, 135, 127, 1,
		127, 2, 150, 123, 40, 136, 127, 0, 40, 171
	})]
	public virtual int codedBlockPatternIntra(MDecoder mDecoder, bool leftAvailable, bool topAvailable, int cbpLeft, int cbpTop, MBType mbLeft, MBType mbTop)
	{
		int cbp0 = mDecoder.decodeBin(73 + _condTerm(leftAvailable, mbLeft, (cbpLeft >> 1) & 1) + 2 * _condTerm(topAvailable, mbTop, (cbpTop >> 2) & 1));
		int cbp1 = mDecoder.decodeBin(73 + (1 - cbp0) + 2 * _condTerm(topAvailable, mbTop, (cbpTop >> 3) & 1));
		int cbp2 = mDecoder.decodeBin(73 + _condTerm(leftAvailable, mbLeft, (cbpLeft >> 3) & 1) + 2 * (1 - cbp0));
		int cbp3 = mDecoder.decodeBin(73 + (1 - cbp2) + 2 * (1 - cbp1));
		int cr0 = mDecoder.decodeBin(77 + condTermCr0(leftAvailable, mbLeft, cbpLeft >> 4) + 2 * condTermCr0(topAvailable, mbTop, cbpTop >> 4));
		int cr1 = ((cr0 != 0) ? mDecoder.decodeBin(81 + condTermCr1(leftAvailable, mbLeft, cbpLeft >> 4) + 2 * condTermCr1(topAvailable, mbTop, cbpTop >> 4)) : 0);
		return cbp0 | (cbp1 << 1) | (cbp2 << 2) | (cbp3 << 3) | (cr0 << 4) | (cr1 << 5);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 232, 161, 69, 137, 127, 11, 109, 127, 11,
		142, 159, 9, 159, 9, 147, 101, 134, 107, 101,
		134, 247, 69, 106, 49, 201, 106, 50, 201
	})]
	public virtual int readRefIdx(MDecoder mDecoder, bool leftAvailable, bool topAvailable, MBType leftType, MBType topType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred curPred, int mbX, int partX, int partY, int partW, int partH, int list)
	{
		int partAbsX = (mbX << 2) + partX;
		int predEqA = ((leftPred != null && leftPred != H264Const.PartPred.___003C_003EDirect && (leftPred == H264Const.PartPred.___003C_003EBi || leftPred == curPred || (curPred == H264Const.PartPred.___003C_003EBi && H264Const.usesList(leftPred, list)))) ? 1 : 0);
		int predEqB = ((topPred != null && topPred != H264Const.PartPred.___003C_003EDirect && (topPred == H264Const.PartPred.___003C_003EBi || topPred == curPred || (curPred == H264Const.PartPred.___003C_003EBi && H264Const.usesList(topPred, list)))) ? 1 : 0);
		int ctA = ((leftAvailable && leftType != null && !leftType.isIntra() && predEqA != 0 && refIdxLeft[list][partY] != 0) ? 1 : 0);
		int ctB = ((topAvailable && topType != null && !topType.isIntra() && predEqB != 0 && refIdxTop[list][partAbsX] != 0) ? 1 : 0);
		int val;
		if (mDecoder.decodeBin(54 + ctA + 2 * ctB) == 0)
		{
			val = 0;
		}
		else if (mDecoder.decodeBin(58) == 0)
		{
			val = 1;
		}
		else
		{
			val = 2;
			while (mDecoder.decodeBin(59) == 1)
			{
				val++;
			}
		}
		for (int j = 0; j < partW; j++)
		{
			refIdxTop[list][partAbsX + j] = val;
		}
		for (int i = 0; i < partH; i++)
		{
			refIdxLeft[list][partY + i] = val;
		}
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 245, 97, 70, 139, 137, 127, 11, 110, 127,
		11, 174, 127, 7, 104, 127, 8, 137, 125, 109,
		55, 137, 136, 104, 106, 141, 104, 103, 105, 106,
		133, 135, 102, 50, 169, 171, 177, 106, 51, 169,
		106, 52, 201
	})]
	public virtual int readMVD(MDecoder decoder, int comp, bool leftAvailable, bool topAvailable, MBType leftType, MBType topType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred curPred, int mbX, int partX, int partY, int partW, int partH, int list)
	{
		int ctx = ((comp != 0) ? 47 : 40);
		int partAbsX = (mbX << 2) + partX;
		int predEqA = ((leftPred != null && leftPred != H264Const.PartPred.___003C_003EDirect && (leftPred == H264Const.PartPred.___003C_003EBi || leftPred == curPred || (curPred == H264Const.PartPred.___003C_003EBi && H264Const.usesList(leftPred, list)))) ? 1 : 0);
		int predEqB = ((topPred != null && topPred != H264Const.PartPred.___003C_003EDirect && (topPred == H264Const.PartPred.___003C_003EBi || topPred == curPred || (curPred == H264Const.PartPred.___003C_003EBi && H264Const.usesList(topPred, list)))) ? 1 : 0);
		int absMvdComp = ((leftAvailable && leftType != null && !leftType.isIntra() && predEqA != 0) ? Math.abs(mvdLeft[list][comp][partY]) : 0);
		absMvdComp += ((topAvailable && topType != null && !topType.isIntra() && predEqB != 0) ? Math.abs(mvdTop[list][comp][partAbsX]) : 0);
		int b = decoder.decodeBin(ctx + ((absMvdComp >= 3) ? ((absMvdComp <= 32) ? 1 : 2) : 0));
		int val = 0;
		while (b != 0 && val < 8)
		{
			b = decoder.decodeBin(Math.min(ctx + val + 3, ctx + 6));
			val++;
		}
		val += b;
		if (val != 0)
		{
			if (val == 9)
			{
				int log = 2;
				int add = 0;
				int sum = 0;
				int leftover = 0;
				do
				{
					sum += leftover;
					log++;
					b = decoder.decodeBinBypass();
					leftover = 1 << log;
				}
				while (b != 0);
				for (log += -1; log >= 0; log += -1)
				{
					add |= decoder.decodeBinBypass() << log;
				}
				val += add + sum;
			}
			val = MathUtil.toSigned(val, -decoder.decodeBinBypass());
		}
		for (int j = 0; j < partW; j++)
		{
			mvdTop[list][comp][partAbsX + j] = val;
		}
		for (int i = 0; i < partH; i++)
		{
			mvdLeft[list][comp][partY + i] = val;
		}
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(575)]
	public virtual int rem4x4PredMode(MDecoder decoder)
	{
		return decoder.decodeBin(69) | (decoder.decodeBin(69) << 1) | (decoder.decodeBin(69) << 2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(571)]
	public virtual bool prev4x4PredModeFlag(MDecoder decoder)
	{
		return decoder.decodeBin(68) == 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 28, 161, 71, 124, 159, 1, 153, 107, 141 })]
	public virtual int readCodedBlockFlagLumaDC(MDecoder decoder, int mbX, MBType left, MBType top, bool leftAvailable, bool topAvailable, MBType cur)
	{
		int tLeft = condTerm(cur, leftAvailable, left, left == MBType.___003C_003EI_16x16, codedBlkDCLeft[0]);
		int tTop = condTerm(cur, topAvailable, top, top == MBType.___003C_003EI_16x16, codedBlkDCTop[0][mbX]);
		int decoded = decoder.decodeBin(BlockType.___003C_003ELUMA_16_DC.codedBlockCtxOff + tLeft + 2 * tTop);
		codedBlkDCLeft[0] = decoded;
		codedBlkDCTop[0][mbX] = decoded;
		return decoded;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 98, 137, 106, 120, 122, 227, 61, 231,
		69, 137, 101, 110, 103, 99, 109, 101, 135, 101,
		251, 56, 236, 79
	})]
	public virtual int readCoeffs(MDecoder decoder, BlockType blockType, int[] @out, int first, int num, int[] reorder, int[] scMapping, int[] lscMapping)
	{
		bool[] sigCoeff = new bool[num];
		int numCoeff;
		for (numCoeff = 0; numCoeff < num - 1; numCoeff++)
		{
			sigCoeff[numCoeff] = decoder.decodeBin(blockType.sigCoeffFlagCtxOff + scMapping[numCoeff]) == 1;
			if (sigCoeff[numCoeff] && decoder.decodeBin(blockType.lastSigCoeffCtxOff + lscMapping[numCoeff]) == 1)
			{
				break;
			}
		}
		int num2 = numCoeff;
		numCoeff++;
		sigCoeff[num2] = true;
		int numGt1 = 0;
		int numEq1 = 0;
		for (int i = numCoeff - 1; i >= 0; i += -1)
		{
			if (sigCoeff[i])
			{
				int absLev = readCoeffAbsLevel(decoder, blockType, numGt1, numEq1);
				if (absLev == 0)
				{
					numEq1++;
				}
				else
				{
					numGt1++;
				}
				@out[reorder[i + first]] = MathUtil.toSigned(absLev + 1, -decoder.decodeBinBypass());
			}
		}
		return numGt1 + numEq1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 21, 129, 71, 170, 100, 191, 25, 191, 6,
		100, 191, 25, 159, 6, 151, 110, 142
	})]
	public virtual int readCodedBlockFlagLumaAC(MDecoder decoder, BlockType blkType, int blkX, int blkY, int comp, MBType left, MBType top, bool leftAvailable, bool topAvailable, int leftCBPLuma, int topCBPLuma, int curCBPLuma, MBType cur)
	{
		int blkOffLeft = blkX & 3;
		int blkOffTop = blkY & 3;
		int tLeft = ((blkOffLeft != 0) ? condTerm(cur, nAvb: true, cur, cbp(curCBPLuma, blkOffLeft - 1, blkOffTop), codedBlkLeft[comp][blkOffTop]) : condTerm(cur, leftAvailable, left, (left != null && left != MBType.___003C_003EI_PCM && cbp(leftCBPLuma, 3, blkOffTop)) ? true : false, codedBlkLeft[comp][blkOffTop]));
		int tTop = ((blkOffTop != 0) ? condTerm(cur, nAvb: true, cur, cbp(curCBPLuma, blkOffLeft, blkOffTop - 1), codedBlkTop[comp][blkX]) : condTerm(cur, topAvailable, top, (top != null && top != MBType.___003C_003EI_PCM && cbp(topCBPLuma, blkOffLeft, 3)) ? true : false, codedBlkTop[comp][blkX]));
		int decoded = decoder.decodeBin(blkType.codedBlockCtxOff + tLeft + 2 * tTop);
		codedBlkLeft[comp][blkOffTop] = decoded;
		codedBlkTop[comp][blkX] = decoded;
		return decoded;
	}

	[LineNumberTable(new byte[] { 158, 247, 162, 104 })]
	public virtual void setPrevCBP(int prevCBP)
	{
		this.prevCBP = prevCBP;
	}

	[LineNumberTable(new byte[] { 158, 211, 66, 127, 0 })]
	public virtual void setCodedBlock(int blkX, int blkY)
	{
		int[] obj = codedBlkLeft[0];
		int num = blkY & 3;
		int[] obj2 = codedBlkTop[0];
		int num2 = 1;
		int[] array = obj2;
		array[blkX] = num2;
		obj[num] = num2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 219, 66, 108, 99, 107, 99, 108, 131 })]
	public virtual int readSubMbTypeP(MDecoder mDecoder)
	{
		if (mDecoder.decodeBin(21) == 1)
		{
			return 0;
		}
		if (mDecoder.decodeBin(22) == 0)
		{
			return 1;
		}
		if (mDecoder.decodeBin(23) == 1)
		{
			return 2;
		}
		return 3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 217, 162, 107, 99, 107, 108, 107, 151, 107,
		151
	})]
	public virtual int readSubMbTypeB(MDecoder mDecoder)
	{
		if (mDecoder.decodeBin(36) == 0)
		{
			return 0;
		}
		if (mDecoder.decodeBin(37) == 0)
		{
			return 1 + mDecoder.decodeBin(39);
		}
		if (mDecoder.decodeBin(38) == 0)
		{
			return 3 + (mDecoder.decodeBin(39) << 1) + mDecoder.decodeBin(39);
		}
		if (mDecoder.decodeBin(39) == 0)
		{
			return 7 + (mDecoder.decodeBin(39) << 1) + mDecoder.decodeBin(39);
		}
		return 11 + mDecoder.decodeBin(39);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 24, 65, 71, 127, 2, 159, 4, 153, 107,
		141
	})]
	public virtual int readCodedBlockFlagChromaDC(MDecoder decoder, int mbX, int comp, MBType left, MBType top, bool leftAvailable, bool topAvailable, int leftCBPChroma, int topCBPChroma, MBType cur)
	{
		int tLeft = condTerm(cur, leftAvailable, left, (left != null && leftCBPChroma != 0) ? true : false, codedBlkDCLeft[comp]);
		int tTop = condTerm(cur, topAvailable, top, (top != null && topCBPChroma != 0) ? true : false, codedBlkDCTop[comp][mbX]);
		int decoded = decoder.decodeBin(BlockType.___003C_003ECHROMA_DC.codedBlockCtxOff + tLeft + 2 * tTop);
		codedBlkDCLeft[comp] = decoded;
		codedBlkDCTop[comp][mbX] = decoded;
		return decoded;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 6, 161, 71, 169, 100, 191, 19, 154, 100,
		191, 19, 154, 155, 110, 142
	})]
	public virtual int readCodedBlockFlagChromaAC(MDecoder decoder, int blkX, int blkY, int comp, MBType left, MBType top, bool leftAvailable, bool topAvailable, int leftCBPChroma, int topCBPChroma, MBType cur)
	{
		int blkOffLeft = blkX & 1;
		int blkOffTop = blkY & 1;
		int tLeft = ((blkOffLeft != 0) ? condTerm(cur, nAvb: true, cur, nBlkAvb: true, codedBlkLeft[comp][blkOffTop]) : condTerm(cur, leftAvailable, left, (left != null && left != MBType.___003C_003EI_PCM && ((uint)leftCBPChroma & 2u) != 0) ? true : false, codedBlkLeft[comp][blkOffTop]));
		int tTop = ((blkOffTop != 0) ? condTerm(cur, nAvb: true, cur, nBlkAvb: true, codedBlkTop[comp][blkX]) : condTerm(cur, topAvailable, top, (top != null && top != MBType.___003C_003EI_PCM && ((uint)topCBPChroma & 2u) != 0) ? true : false, codedBlkTop[comp][blkX]));
		int decoded = decoder.decodeBin(BlockType.___003C_003ECHROMA_AC.codedBlockCtxOff + tLeft + 2 * tTop);
		codedBlkLeft[comp][blkOffTop] = decoded;
		codedBlkTop[comp][blkX] = decoded;
		return decoded;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 65, 71, 99, 115, 147, 106, 131 })]
	public virtual int readMBTypeI(MDecoder decoder, MBType left, MBType top, bool leftAvailable, bool topAvailable)
	{
		int ctx = 3;
		ctx += ((leftAvailable && left != MBType.___003C_003EI_NxN) ? 1 : 0);
		ctx += ((topAvailable && top != MBType.___003C_003EI_NxN) ? 1 : 0);
		if (decoder.decodeBin(ctx) == 0)
		{
			return 0;
		}
		return (decoder.decodeFinalBin() != 1) ? (1 + readMBType16x16(decoder)) : 25;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 74, 130, 108, 141, 107, 144 })]
	public virtual int readMBTypeP(MDecoder decoder)
	{
		if (decoder.decodeBin(14) == 1)
		{
			return 5 + readIntraP(decoder, 17);
		}
		if (decoder.decodeBin(15) == 0)
		{
			return (decoder.decodeBin(16) != 0) ? 3 : 0;
		}
		return (decoder.decodeBin(17) != 0) ? 1 : 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 66, 161, 71, 100, 118, 150, 106, 99, 107,
		140, 106, 100, 159, 3, 107, 159, 4, 159, 12,
		141, 142, 132, 228, 69
	})]
	public virtual int readMBTypeB(MDecoder mDecoder, MBType left, MBType top, bool leftAvailable, bool topAvailable)
	{
		int ctx = 27;
		ctx += ((leftAvailable && left != null && left != MBType.___003C_003EB_Direct_16x16) ? 1 : 0);
		ctx += ((topAvailable && top != null && top != MBType.___003C_003EB_Direct_16x16) ? 1 : 0);
		if (mDecoder.decodeBin(ctx) == 0)
		{
			return 0;
		}
		if (mDecoder.decodeBin(30) == 0)
		{
			return 1 + mDecoder.decodeBin(32);
		}
		if (mDecoder.decodeBin(31) == 0)
		{
			return 3 + ((mDecoder.decodeBin(32) << 2) | (mDecoder.decodeBin(32) << 1) | mDecoder.decodeBin(32));
		}
		if (mDecoder.decodeBin(32) == 0)
		{
			return 12 + ((mDecoder.decodeBin(32) << 2) | (mDecoder.decodeBin(32) << 1) | mDecoder.decodeBin(32));
		}
		return ((mDecoder.decodeBin(32) << 1) + mDecoder.decodeBin(32)) switch
		{
			0 => 20 + mDecoder.decodeBin(32), 
			1 => 23 + readIntraP(mDecoder, 32), 
			2 => 11, 
			3 => 22, 
			_ => 0, 
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 222, 97, 70, 144, 191, 20, 159, 0 })]
	public virtual bool readMBSkipFlag(MDecoder mDecoder, SliceType slType, bool leftAvailable, bool topAvailable, int mbX)
	{
		int @base = ((slType != SliceType.___003C_003EP) ? 24 : 11);
		int ret = ((mDecoder.decodeBin(@base + ((leftAvailable && !skipFlagLeft) ? 1 : 0) + ((topAvailable && !skipFlagsTop[mbX]) ? 1 : 0)) == 1) ? 1 : 0);
		bool[] array = skipFlagsTop;
		int num = ret;
		bool[] array2 = array;
		array2[mbX] = (byte)num != 0;
		skipFlagLeft = (byte)num != 0;
		return (byte)ret != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 162, 114, 146, 112, 107, 48, 135, 133,
		105, 139, 103, 104, 132, 102, 114, 13, 233, 69,
		169
	})]
	private int readCoeffAbsLevel(MDecoder decoder, BlockType blockType, int numDecodAbsLevelGt1, int numDecodAbsLevelEq1)
	{
		int incB0 = ((numDecodAbsLevelGt1 == 0) ? Math.min(4, 1 + numDecodAbsLevelEq1) : 0);
		int incBN = 5 + Math.min(4 - blockType.coeffAbsLevelAdjust, numDecodAbsLevelGt1);
		int b = decoder.decodeBin(blockType.coeffAbsLevelCtxOff + incB0);
		int val = 0;
		while (b != 0 && val < 13)
		{
			b = decoder.decodeBin(blockType.coeffAbsLevelCtxOff + incBN);
			val++;
		}
		val += b;
		if (val == 14)
		{
			int log = -2;
			int add = 0;
			int sum = 0;
			do
			{
				log++;
			}
			while (decoder.decodeBinBypass() != 0);
			for (; log >= 0; log += -1)
			{
				add |= decoder.decodeBinBypass() << log;
				sum += 1 << log;
			}
			val += add + sum;
		}
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 91, 66, 114, 146, 101, 149, 112, 103, 104,
		48, 135, 149, 104, 48, 135, 136, 109, 104, 8,
		210, 104, 108, 49, 201
	})]
	private void writeCoeffAbsLevel(MEncoder encoder, BlockType blockType, int numDecodAbsLevelGt1, int numDecodAbsLevelEq1, int absLev)
	{
		int incB0 = ((numDecodAbsLevelGt1 == 0) ? Math.min(4, 1 + numDecodAbsLevelEq1) : 0);
		int incBN = 5 + Math.min(4 - blockType.coeffAbsLevelAdjust, numDecodAbsLevelGt1);
		if (absLev == 0)
		{
			encoder.encodeBin(blockType.coeffAbsLevelCtxOff + incB0, 0);
			return;
		}
		encoder.encodeBin(blockType.coeffAbsLevelCtxOff + incB0, 1);
		if (absLev < 14)
		{
			for (int j = 1; j < absLev; j++)
			{
				encoder.encodeBin(blockType.coeffAbsLevelCtxOff + incBN, 1);
			}
			encoder.encodeBin(blockType.coeffAbsLevelCtxOff + incBN, 0);
			return;
		}
		for (int i = 1; i < 14; i++)
		{
			encoder.encodeBin(blockType.coeffAbsLevelCtxOff + incBN, 1);
		}
		absLev += -14;
		int sufLen = 0;
		int pow = 1;
		while (absLev >= pow)
		{
			encoder.encodeBinBypass(1);
			absLev -= pow;
			sufLen++;
			pow = 1 << sufLen;
		}
		encoder.encodeBinBypass(0);
		for (sufLen += -1; sufLen >= 0; sufLen += -1)
		{
			encoder.encodeBinBypass((absLev >> sufLen) & 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 66, 108, 106, 151 })]
	private int readMBType16x16(MDecoder decoder)
	{
		int type = decoder.decodeBin(6) * 12;
		if (decoder.decodeBin(7) == 0)
		{
			return type + (decoder.decodeBin(9) << 1) + decoder.decodeBin(10);
		}
		return type + (decoder.decodeBin(8) << 2) + (decoder.decodeBin(9) << 1) + decoder.decodeBin(10) + 4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 71, 130, 106, 131 })]
	private int readIntraP(MDecoder decoder, int ctxOff)
	{
		if (decoder.decodeBin(ctxOff) == 0)
		{
			return 0;
		}
		return (decoder.decodeFinalBin() != 1) ? (1 + readMBType16x16P(decoder, ctxOff)) : 25;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 69, 130, 102, 108, 102, 106, 102, 149, 124,
		41
	})]
	private int readMBType16x16P(MDecoder decoder, int ctxOff)
	{
		ctxOff++;
		int type = decoder.decodeBin(ctxOff) * 12;
		ctxOff++;
		if (decoder.decodeBin(ctxOff) == 0)
		{
			ctxOff++;
			return type + (decoder.decodeBin(ctxOff) << 1) + decoder.decodeBin(ctxOff);
		}
		return type + (decoder.decodeBin(ctxOff) << 2) + (decoder.decodeBin(ctxOff + 1) << 1) + decoder.decodeBin(ctxOff + 1) + 4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 53, 162, 102, 139, 105, 135, 101, 105, 108,
		142, 103, 105, 107, 110, 142
	})]
	private void writeMBType16x16(MEncoder encoder, int mbType)
	{
		if (mbType < 12)
		{
			encoder.encodeBin(6, 0);
		}
		else
		{
			encoder.encodeBin(6, 1);
			mbType += -12;
		}
		if (mbType < 4)
		{
			encoder.encodeBin(7, 0);
			encoder.encodeBin(9, mbType >> 1);
			encoder.encodeBin(10, mbType & 1);
		}
		else
		{
			mbType += -4;
			encoder.encodeBin(7, 1);
			encoder.encodeBin(8, mbType >> 2);
			encoder.encodeBin(9, (mbType >> 1) & 1);
			encoder.encodeBin(10, mbType & 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 30, 65, 70, 100, 110, 105, 99, 100, 99 })]
	public virtual int condTerm(MBType mbCur, bool nAvb, MBType mbN, bool nBlkAvb, int cbpN)
	{
		if (!nAvb)
		{
			return mbCur.isIntra() ? 1 : 0;
		}
		if (mbN == MBType.___003C_003EI_PCM)
		{
			return 1;
		}
		if (!nBlkAvb)
		{
			return 0;
		}
		return cbpN;
	}

	[LineNumberTable(new byte[] { 159, 7, 66, 137 })]
	private bool cbp(int cbpLuma, int blkX, int blkY)
	{
		int x8x8 = (blkY & 2) + (blkX >> 1);
		return ((cbpLuma >> x8x8) & 1) == 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 248, 161, 67 })]
	private int _condTerm(bool avb, MBType mbt, int cbp)
	{
		return (avb && mbt != MBType.___003C_003EI_PCM && (mbt == null || cbp != 1)) ? 1 : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 250, 161, 67 })]
	private int condTermCr0(bool avb, MBType mbt, int cbpChroma)
	{
		return (avb && (mbt == MBType.___003C_003EI_PCM || (mbt != null && cbpChroma != 0))) ? 1 : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 249, 161, 67 })]
	private int condTermCr1(bool avb, MBType mbt, int cbpChroma)
	{
		return (avb && (mbt == MBType.___003C_003EI_PCM || (mbt != null && ((uint)cbpChroma & 2u) != 0))) ? 1 : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 162, 104, 50, 167, 99, 104, 107, 5,
		199, 112, 107, 112, 151, 240, 59, 231, 73, 103,
		110, 108, 99, 115, 111, 101, 137, 103, 245, 55,
		236, 75
	})]
	public virtual void writeCoeffs(MEncoder encoder, BlockType blockType, int[] _out, int first, int num, int[] reorder)
	{
		for (int k = 0; k < num; k++)
		{
			tmp[k] = _out[reorder[first + k]];
		}
		int numCoeff = 0;
		for (int j = 0; j < num; j++)
		{
			if (tmp[j] != 0)
			{
				numCoeff = j + 1;
			}
		}
		for (int i = 0; i < Math.min(numCoeff, num - 1); i++)
		{
			if (tmp[i] != 0)
			{
				encoder.encodeBin(blockType.sigCoeffFlagCtxOff + i, 1);
				encoder.encodeBin(blockType.lastSigCoeffCtxOff + i, (i == numCoeff - 1) ? 1 : 0);
			}
			else
			{
				encoder.encodeBin(blockType.sigCoeffFlagCtxOff + i, 0);
			}
		}
		int numGt1 = 0;
		int numEq1 = 0;
		for (int l = numCoeff - 1; l >= 0; l += -1)
		{
			if (tmp[l] != 0)
			{
				int absLev = MathUtil.abs(tmp[l]) - 1;
				writeCoeffAbsLevel(encoder, blockType, numGt1, numEq1, absLev);
				if (absLev == 0)
				{
					numEq1++;
				}
				else
				{
					numGt1++;
				}
				encoder.encodeBinBypass(MathUtil.sign(tmp[l]));
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 57, 97, 71, 99, 115, 147, 101, 139, 105,
		103, 138, 104, 174
	})]
	public virtual void writeMBTypeI(MEncoder encoder, MBType left, MBType top, bool leftAvailable, bool topAvailable, int mbType)
	{
		int ctx = 3;
		ctx += ((leftAvailable && left != MBType.___003C_003EI_NxN) ? 1 : 0);
		ctx += ((topAvailable && top != MBType.___003C_003EI_NxN) ? 1 : 0);
		if (mbType == 0)
		{
			encoder.encodeBin(ctx, 0);
			return;
		}
		encoder.encodeBin(ctx, 1);
		if (mbType == 25)
		{
			encoder.encodeBinFinal(1);
			return;
		}
		encoder.encodeBinFinal(0);
		writeMBType16x16(encoder, mbType - 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 43, 130, 100, 191, 12, 104, 105, 139, 105,
		105, 140, 106, 108, 172
	})]
	public virtual void writeMBQpDelta(MEncoder encoder, MBType prevMbType, int mbQpDelta)
	{
		int ctx = 60;
		ctx += ((prevMbType != null && prevMbType != MBType.___003C_003EI_PCM && (prevMbType == MBType.___003C_003EI_16x16 || prevCBP != 0) && prevMbQpDelta != 0) ? 1 : 0);
		prevMbQpDelta = mbQpDelta;
		int num = mbQpDelta;
		mbQpDelta += -1;
		if (num == 0)
		{
			encoder.encodeBin(ctx, 0);
			return;
		}
		encoder.encodeBin(ctx, 1);
		int num2 = mbQpDelta;
		mbQpDelta += -1;
		if (num2 == 0)
		{
			encoder.encodeBin(62, 0);
			return;
		}
		while (true)
		{
			int num3 = mbQpDelta;
			mbQpDelta += -1;
			if (num3 <= 0)
			{
				break;
			}
			encoder.encodeBin(63, 1);
		}
		encoder.encodeBin(63, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 33, 129, 71, 100, 123, 126, 118, 108, 55,
		135, 127, 0
	})]
	public virtual void writeIntraChromaPredMode(MEncoder encoder, int mbX, MBType left, MBType top, bool leftAvailable, bool topAvailable, int mode)
	{
		int ctx = 64;
		ctx += ((leftAvailable && left.isIntra() && chromaPredModeLeft != 0) ? 1 : 0);
		ctx += ((topAvailable && top.isIntra() && chromaPredModeTop[mbX] != 0) ? 1 : 0);
		int model = ctx;
		int num = mode;
		mode += -1;
		encoder.encodeBin(model, (num != 0) ? 1 : 0);
		int i = 0;
		while (mode >= 0 && i < 2)
		{
			int num2 = mode;
			mode += -1;
			encoder.encodeBin(67, (num2 != 0) ? 1 : 0);
			i++;
		}
		int[] array = chromaPredModeTop;
		int num3 = mode;
		int[] array2 = array;
		array2[mbX] = num3;
		chromaPredModeLeft = num3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 14, 129, 77, 171, 101, 125, 56, 202, 191,
		9, 101, 125, 55, 170, 159, 8, 155, 111, 142
	})]
	public virtual int readCodedBlockFlagLuma64(MDecoder decoder, int blkX, int blkY, int comp, MBType left, MBType top, bool leftAvailable, bool topAvailable, int leftCBPLuma, int topCBPLuma, int curCBPLuma, MBType cur, bool is8x8Left, bool is8x8Top)
	{
		int blkOffLeft = blkX & 3;
		int blkOffTop = blkY & 3;
		int tLeft = ((blkOffLeft != 0) ? condTerm(cur, nAvb: true, cur, cbp(curCBPLuma, blkOffLeft - 1, blkOffTop), codedBlkLeft[comp][blkOffTop]) : condTerm(cur, leftAvailable, left, (left != null && left != MBType.___003C_003EI_PCM && is8x8Left && cbp(leftCBPLuma, 3, blkOffTop)) ? true : false, codedBlkLeft[comp][blkOffTop]));
		int tTop = ((blkOffTop != 0) ? condTerm(cur, nAvb: true, cur, cbp(curCBPLuma, blkOffLeft, blkOffTop - 1), codedBlkTop[comp][blkX]) : condTerm(cur, topAvailable, top, (top != null && top != MBType.___003C_003EI_PCM && is8x8Top && cbp(topCBPLuma, blkOffLeft, 3)) ? true : false, codedBlkTop[comp][blkX]));
		int decoded = decoder.decodeBin(BlockType.___003C_003ELUMA_64.codedBlockCtxOff + tLeft + 2 * tTop);
		codedBlkLeft[comp][blkOffTop] = decoded;
		codedBlkTop[comp][blkX] = decoded;
		return decoded;
	}
}
