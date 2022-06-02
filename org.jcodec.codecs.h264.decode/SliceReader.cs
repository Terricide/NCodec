using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.common.biari;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class SliceReader : Object
{
	private PictureParameterSet activePps;

	private CABAC cabac;

	private MDecoder mDecoder;

	private CAVLC[] cavlc;

	private BitReader reader;

	private Mapper mapper;

	private SliceHeader sh;

	private NALUnit nalUnit;

	private bool prevMbSkipped;

	private int mbIdx;

	private MBType prevMBType;

	private int mbSkipRun;

	private bool endOfData;

	internal MBType[] topMBType;

	internal MBType leftMBType;

	internal int leftCBPLuma;

	internal int[] topCBPLuma;

	internal int leftCBPChroma;

	internal int[] topCBPChroma;

	internal ColorSpace chromaFormat;

	internal bool transform8x8;

	internal int[] numRef;

	internal bool tf8x8Left;

	internal bool[] tf8x8Top;

	internal int[] i4x4PredTop;

	internal int[] i4x4PredLeft;

	internal H264Const.PartPred[] predModeLeft;

	internal H264Const.PartPred[] predModeTop;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 233, 40, 136, 232, 87, 104, 104,
		105, 104, 105, 105, 105, 137, 112, 109, 109, 109,
		115, 127, 10, 106, 159, 12, 159, 20, 109, 109,
		143, 109, 111
	})]
	public SliceReader(PictureParameterSet activePps, CABAC cabac, CAVLC[] cavlc, MDecoder mDecoder, BitReader reader, Mapper mapper, SliceHeader sh, NALUnit nalUnit)
	{
		prevMbSkipped = false;
		prevMBType = null;
		this.activePps = activePps;
		this.cabac = cabac;
		this.mDecoder = mDecoder;
		this.cavlc = cavlc;
		this.reader = reader;
		this.mapper = mapper;
		this.sh = sh;
		this.nalUnit = nalUnit;
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		topMBType = new MBType[mbWidth];
		topCBPLuma = new int[mbWidth];
		topCBPChroma = new int[mbWidth];
		chromaFormat = sh.sps.chromaFormatIdc;
		transform8x8 = sh.pps.extended != null && sh.pps.extended.transform8x8ModeFlag;
		if (sh.numRefIdxActiveOverrideFlag)
		{
			numRef = new int[2]
			{
				sh.numRefIdxActiveMinus1[0] + 1,
				sh.numRefIdxActiveMinus1[1] + 1
			};
		}
		else
		{
			numRef = new int[2]
			{
				sh.pps.numRefIdxActiveMinus1[0] + 1,
				sh.pps.numRefIdxActiveMinus1[1] + 1
			};
		}
		tf8x8Top = new bool[mbWidth];
		predModeLeft = new H264Const.PartPred[2];
		predModeTop = new H264Const.PartPred[mbWidth << 1];
		i4x4PredLeft = new int[4];
		i4x4PredTop = new int[mbWidth << 2];
	}

	[LineNumberTable(1085)]
	public virtual SliceHeader getSliceHeader()
	{
		return sh;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 66, 116, 116, 124, 131, 109, 141, 159,
		6, 127, 7, 113, 119, 110, 200, 109, 111, 115,
		104, 104, 127, 17, 39, 134, 104, 116, 118, 103,
		127, 53, 111, 131, 200, 116, 112, 111, 159, 9,
		127, 25, 114, 40, 171, 100, 127, 20, 179, 105,
		147, 141, 102, 104, 104, 104, 103, 191, 53, 127,
		17, 145, 143, 159, 14
	})]
	public virtual bool readMacroblock(MBlock mBlock)
	{
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		int mbHeight = sh.sps.picHeightInMapUnitsMinus1 + 1;
		if ((endOfData && mbSkipRun == 0) || mbIdx >= mbWidth * mbHeight)
		{
			return false;
		}
		mBlock.mbIdx = mbIdx;
		mBlock.prevMbType = prevMBType;
		int mbaffFrameFlag = ((sh.sps.mbAdaptiveFrameFieldFlag && !sh.fieldPicFlag) ? 1 : 0);
		H264Const.PartPred __003C_003EL;
		int num2;
		H264Const.PartPred[] array6;
		if (sh.sliceType.isInter() && !activePps.entropyCodingModeFlag)
		{
			if (!prevMbSkipped && mbSkipRun == 0)
			{
				mbSkipRun = CAVLCReader.readUEtrace(reader, "mb_skip_run");
				if (!CAVLCReader.moreRBSPData(reader))
				{
					endOfData = true;
				}
			}
			if (mbSkipRun > 0)
			{
				mbSkipRun--;
				int mbAddr2 = mapper.getAddress(mbIdx);
				prevMbSkipped = true;
				prevMBType = null;
				object[] obj = new object[3] { "---------------------- MB (%d,%d) ---------------------", null, null };
				obj[1] = Integer.valueOf((mbWidth != -1) ? (mbAddr2 % mbWidth) : 0);
				obj[2] = Integer.valueOf((mbWidth != -1) ? (mbAddr2 / mbWidth) : (-mbAddr2));
				MBlockDecoderUtils.debugPrint(obj);
				mBlock.skipped = true;
				int mbX2 = mapper.getMbX(mBlock.mbIdx);
				MBType[] array = topMBType;
				
				leftMBType = null;
				array[mbX2] = null;
				int blk8x8X2 = mbX2 << 1;
				H264Const.PartPred[] array2 = predModeLeft;
				H264Const.PartPred[] array3 = predModeLeft;
				H264Const.PartPred[] array4 = predModeTop;
				H264Const.PartPred[] array5 = predModeTop;
				int num = blk8x8X2 + 1;
				__003C_003EL = H264Const.PartPred.___003C_003EL0;
				num2 = num;
				array6 = array5;
				H264Const.PartPred partPred = __003C_003EL;
				array6[num2] = __003C_003EL;
				__003C_003EL = partPred;
				num2 = blk8x8X2;
				array6 = array4;
				H264Const.PartPred partPred2 = __003C_003EL;
				array6[num2] = __003C_003EL;
				__003C_003EL = partPred2;
				num2 = 1;
				array6 = array3;
				H264Const.PartPred partPred3 = __003C_003EL;
				array6[num2] = __003C_003EL;
				array2[0] = partPred3;
				mbIdx++;
				return true;
			}
			prevMbSkipped = false;
		}
		int mbAddr = mapper.getAddress(mbIdx);
		int mbX = ((mbWidth != -1) ? (mbAddr % mbWidth) : 0);
		int mbY = ((mbWidth != -1) ? (mbAddr / mbWidth) : (-mbAddr));
		MBlockDecoderUtils.debugPrint("---------------------- MB (%d,%d) ---------------------", Integer.valueOf(mbX), Integer.valueOf(mbY));
		int mb_field_decoding_flag;
		if (sh.sliceType.isIntra() || !activePps.entropyCodingModeFlag || !readMBSkipFlag(sh.sliceType, mapper.leftAvailable(mbIdx), mapper.topAvailable(mbIdx), mbX))
		{
			mb_field_decoding_flag = 0;
			if (mbaffFrameFlag != 0)
			{
				int num3 = mbIdx;
				if (2 != -1 && num3 % 2 != 0)
				{
					int num4 = mbIdx;
					if (((2 != -1) ? (num4 % 2) : 0) != 1 || !prevMbSkipped)
					{
						goto IL_0310;
					}
				}
				mb_field_decoding_flag = (CAVLCReader.readBool(reader, "mb_field_decoding_flag") ? 1 : 0);
			}
			goto IL_0310;
		}
		prevMBType = null;
		prevMbSkipped = true;
		mBlock.skipped = true;
		int blk8x8X = mbX << 1;
		H264Const.PartPred[] array7 = predModeLeft;
		H264Const.PartPred[] array8 = predModeLeft;
		H264Const.PartPred[] array9 = predModeTop;
		H264Const.PartPred[] array10 = predModeTop;
		int num5 = blk8x8X + 1;
		__003C_003EL = H264Const.PartPred.___003C_003EL0;
		num2 = num5;
		array6 = array10;
		H264Const.PartPred partPred4 = __003C_003EL;
		array6[num2] = __003C_003EL;
		__003C_003EL = partPred4;
		num2 = blk8x8X;
		array6 = array9;
		H264Const.PartPred partPred5 = __003C_003EL;
		array6[num2] = __003C_003EL;
		__003C_003EL = partPred5;
		num2 = 1;
		array6 = array8;
		H264Const.PartPred partPred6 = __003C_003EL;
		array6[num2] = __003C_003EL;
		array7[0] = partPred6;
		goto IL_03b2;
		IL_03b2:
		endOfData = (((activePps.entropyCodingModeFlag && mDecoder.decodeFinalBin() == 1) || (!activePps.entropyCodingModeFlag && !CAVLCReader.moreRBSPData(reader))) ? true : false);
		mbIdx++;
		MBType[] array11 = topMBType;
		int mbX3 = mapper.getMbX(mBlock.mbIdx);
		MBType curMbType = mBlock.curMbType;
		leftMBType = curMbType;
		array11[mbX3] = curMbType;
		return true;
		IL_0310:
		mBlock.fieldDecoding = (byte)mb_field_decoding_flag != 0;
		readMBlock(mBlock, sh.sliceType);
		prevMBType = mBlock.curMbType;
		goto IL_03b2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 33, 65, 69 })]
	public virtual bool readMBSkipFlag(SliceType slType, bool leftAvailable, bool topAvailable, int mbX)
	{
		bool result = cabac.readMBSkipFlag(mDecoder, slType, leftAvailable, topAvailable, mbX);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 149, 130, 105, 106, 105, 138, 136, 115, 122,
		122, 124
	})]
	public virtual void readMBlock(MBlock mBlock, SliceType sliceType)
	{
		if (sliceType == SliceType.___003C_003EI)
		{
			readMBlockI(mBlock);
		}
		else if (sliceType == SliceType.___003C_003EP)
		{
			readMBlockP(mBlock);
		}
		else
		{
			readMBlockB(mBlock);
		}
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int[] array = topCBPLuma;
		int num = mBlock.cbpLuma();
		int num2 = num;
		leftCBPLuma = num;
		array[mbX] = num2;
		int[] array2 = topCBPChroma;
		num = mBlock.cbpChroma();
		int num3 = num;
		leftCBPChroma = num;
		array2[mbX] = num3;
		bool[] array3 = tf8x8Top;
		num = (mBlock.transform8x8Used ? 1 : 0);
		int num4 = mbX;
		bool[] array4 = array3;
		int num5 = num;
		array4[num4] = (byte)num != 0;
		tf8x8Left = (byte)num5 != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 74, 98, 110, 148 })]
	internal virtual bool prev4x4PredMode()
	{
		if (!activePps.entropyCodingModeFlag)
		{
			bool result = CAVLCReader.readBool(reader, "MBP: prev_intra4x4_pred_mode_flag");
			
			return result;
		}
		bool result2 = cabac.prev4x4PredModeFlag(mDecoder);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 130, 110, 149 })]
	internal virtual int rem4x4PredMode()
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int result = CAVLCReader.readNBit(reader, 3, "MB: rem_intra4x4_pred_mode");
			
			return result;
		}
		int result2 = cabac.rem4x4PredMode(mDecoder);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 129, 69, 110, 148, 191, 5 })]
	internal virtual int readChromaPredMode(int mbX, bool leftAvailable, bool topAvailable)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			return CAVLCReader.readUEtrace(reader, "MBP: intra_chroma_pred_mode");
		}
		return cabac.readIntraChromaPredMode(mDecoder, mbX, leftMBType, topMBType[mbX], leftAvailable, topAvailable);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 66, 110, 148, 148 })]
	internal virtual int readMBQpDelta(MBType prevMbType)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			return CAVLCReader.readSE(reader, "mb_qp_delta");
		}
		return cabac.readMBQpDelta(mDecoder, prevMbType);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 72, 65, 69, 110, 191, 15, 159, 12, 191,
		13
	})]
	internal virtual void read16x16DC(bool leftAvailable, bool topAvailable, int mbX, int[] dc)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			cavlc[0].readLumaDCBlock(reader, dc, mbX, leftAvailable, leftMBType, topAvailable, topMBType[mbX], CoeffTransformer.zigzag4x4);
		}
		else if (cabac.readCodedBlockFlagLumaDC(mDecoder, mbX, leftMBType, topMBType[mbX], leftAvailable, topAvailable, MBType.___003C_003EI_16x16) == 1)
		{
			cabac.readCoeffs(mDecoder, CABAC.BlockType.___003C_003ELUMA_16_DC, dc, 0, 16, CoeffTransformer.zigzag4x4, H264Const.___003C_003EidentityMapping16, H264Const.___003C_003EidentityMapping16);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 68, 161, 69, 113, 223, 64, 159, 37, 191,
		15
	})]
	internal virtual int read16x16AC(bool leftAvailable, bool topAvailable, int mbX, int cbpLuma, int[] ac, int blkOffLeft, int blkOffTop, int blkX, int blkY)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int result = cavlc[0].readACBlock(reader, ac, blkX, blkOffTop, (blkOffLeft != 0 || leftAvailable) ? true : false, (blkOffLeft != 0) ? MBType.___003C_003EI_16x16 : leftMBType, (blkOffTop != 0 || topAvailable) ? true : false, (blkOffTop != 0) ? MBType.___003C_003EI_16x16 : topMBType[mbX], 1, 15, CoeffTransformer.zigzag4x4);
			
			return result;
		}
		if (cabac.readCodedBlockFlagLumaAC(mDecoder, CABAC.BlockType.___003C_003ELUMA_15_AC, blkX, blkOffTop, 0, leftMBType, topMBType[mbX], leftAvailable, topAvailable, leftCBPLuma, topCBPLuma[mbX], cbpLuma, MBType.___003C_003EI_16x16) == 1)
		{
			int result2 = cabac.readCoeffs(mDecoder, CABAC.BlockType.___003C_003ELUMA_15_AC, ac, 1, 15, CoeffTransformer.zigzag4x4, H264Const.___003C_003EidentityMapping16, H264Const.___003C_003EidentityMapping16);
			
			return result2;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 59, 162, 114 })]
	public virtual void setZeroCoeff(int comp, int blkX, int blkOffTop)
	{
		cavlc[comp].setZeroCoeff(blkX, blkOffTop);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 162, 161, 69, 108, 108, 120, 152, 116, 51,
		134, 116, 51, 136, 115, 109, 111, 109, 145
	})]
	public virtual void readChromaResidual(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX)
	{
		if (mBlock.cbpChroma() != 0)
		{
			if ((mBlock.cbpChroma() & 3) > 0)
			{
				readChromaDC(mbX, leftAvailable, topAvailable, mBlock.dc1, 1, mBlock.curMbType);
				readChromaDC(mbX, leftAvailable, topAvailable, mBlock.dc2, 2, mBlock.curMbType);
			}
			_readChromaAC(leftAvailable, topAvailable, mbX, mBlock.dc1, 1, mBlock.curMbType, (mBlock.cbpChroma() & 2) > 0, mBlock.ac[1]);
			_readChromaAC(leftAvailable, topAvailable, mbX, mBlock.dc2, 2, mBlock.curMbType, (mBlock.cbpChroma() & 2) > 0, mBlock.ac[2]);
		}
		else if (!sh.pps.entropyCodingModeFlag)
		{
			setZeroCoeff(1, mbX << 1, 0);
			setZeroCoeff(1, (mbX << 1) + 1, 1);
			setZeroCoeff(2, mbX << 1, 0);
			setZeroCoeff(2, (mbX << 1) + 1, 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 161, 69, 110, 114, 137 })]
	protected internal virtual int readCodedBlockPatternInter(bool leftAvailable, bool topAvailable, int leftCBP, int topCBP, MBType leftMB, MBType topMB)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int code = CAVLCReader.readUEtrace(reader, "coded_block_pattern");
			return H264Const.___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR[code];
		}
		int result = cabac.codedBlockPatternIntra(mDecoder, leftAvailable, topAvailable, leftCBP, topCBP, leftMB, topMB);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 91, 97, 75, 110, 148 })]
	internal virtual bool readTransform8x8Flag(bool leftAvailable, bool topAvailable, MBType leftType, MBType topType, bool is8x8Left, bool is8x8Top)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			bool result = CAVLCReader.readBool(reader, "transform_size_8x8_flag");
			
			return result;
		}
		bool result2 = cabac.readTransform8x8Flag(mDecoder, leftAvailable, topAvailable, leftType, topType, is8x8Left, is8x8Top);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 181, 97, 69, 105, 112, 115, 142, 144 })]
	public virtual void readResidualLuma(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY)
	{
		if (!mBlock.transform8x8Used)
		{
			readLuma(mBlock, leftAvailable, topAvailable, mbX, mbY);
		}
		else if (sh.pps.entropyCodingModeFlag)
		{
			readLuma8x8CABAC(mBlock, mbX, mbY);
		}
		else
		{
			readLuma8x8CAVLC(mBlock, leftAvailable, topAvailable, mbX, mbY);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 65, 69, 110, 154 })]
	internal virtual int readRefIdx(bool leftAvailable, bool topAvailable, MBType leftType, MBType topType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred curPred, int mbX, int partX, int partY, int partW, int partH, int list)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int result = CAVLCReader.readTE(reader, numRef[list] - 1);
			
			return result;
		}
		int result2 = cabac.readRefIdx(mDecoder, leftAvailable, topAvailable, leftType, topType, leftPred, topPred, curPred, mbX, partX, partY, partW, partH, list);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 249, 161, 70, 133, 111, 159, 32, 191, 32 })]
	private void readPredictionInter16x16(MBlock mBlock, int mbX, bool leftAvailable, bool topAvailable, int list, H264Const.PartPred curPred)
	{
		int blk8x8X = mbX << 1;
		if (H264Const.usesList(curPred, list))
		{
			mBlock.___003C_003Epb16x16.mvdX[list] = readMVD(0, leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[blk8x8X], curPred, mbX, 0, 0, 4, 4, list);
			mBlock.___003C_003Epb16x16.mvdY[list] = readMVD(1, leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[blk8x8X], curPred, mbX, 0, 0, 4, 4, list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 246, 129, 69, 191, 35, 104, 113, 223, 14,
		115, 179, 142, 110, 142
	})]
	private void readResidualInter(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY)
	{
		mBlock._cbp = readCodedBlockPatternInter(leftAvailable, topAvailable, leftCBPLuma | (leftCBPChroma << 4), topCBPLuma[mbX] | (topCBPChroma[mbX] << 4), leftMBType, topMBType[mbX]);
		mBlock.transform8x8Used = false;
		if (mBlock.cbpLuma() != 0 && transform8x8)
		{
			mBlock.transform8x8Used = readTransform8x8Flag(leftAvailable, topAvailable, leftMBType, topMBType[mbX], tf8x8Left, tf8x8Top[mbX]);
		}
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			mBlock.mbQPDelta = readMBQpDelta(mBlock.prevMbType);
		}
		readResidualLuma(mBlock, leftAvailable, topAvailable, mbX, mbY);
		if (chromaFormat != ColorSpace.___003C_003EMONO)
		{
			readChromaResidual(mBlock, leftAvailable, topAvailable, mbX);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 129, 69, 110, 148 })]
	internal virtual int readMVD(int comp, bool leftAvailable, bool topAvailable, MBType leftType, MBType topType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred curPred, int mbX, int partX, int partY, int partW, int partH, int list)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int result = CAVLCReader.readSE(reader, "mvd_l0_x");
			
			return result;
		}
		int result2 = cabac.readMVD(mDecoder, comp, leftAvailable, topAvailable, leftType, topType, leftPred, topPred, curPred, mbX, partX, partY, partW, partH, list);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 9, 129, 70, 101, 143, 159, 32, 223, 32,
		108, 159, 23, 191, 23
	})]
	private void readPredictionInter16x8(MBlock mBlock, int mbX, bool leftAvailable, bool topAvailable, H264Const.PartPred p0, H264Const.PartPred p1, int list)
	{
		int blk8x8X = mbX << 1;
		if (H264Const.usesList(p0, list))
		{
			mBlock.___003C_003Epb168x168.mvdX1[list] = readMVD(0, leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[blk8x8X], p0, mbX, 0, 0, 4, 2, list);
			mBlock.___003C_003Epb168x168.mvdY1[list] = readMVD(1, leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[blk8x8X], p0, mbX, 0, 0, 4, 2, list);
		}
		if (H264Const.usesList(p1, list))
		{
			mBlock.___003C_003Epb168x168.mvdX2[list] = readMVD(0, leftAvailable, topAvailable: true, leftMBType, MBType.___003C_003EP_16x8, predModeLeft[1], p0, p1, mbX, 0, 2, 4, 2, list);
			mBlock.___003C_003Epb168x168.mvdY2[list] = readMVD(1, leftAvailable, topAvailable: true, leftMBType, MBType.___003C_003EP_16x8, predModeLeft[1], p0, p1, mbX, 0, 2, 4, 2, list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		14,
		97,
		70,
		133,
		111,
		159,
		32,
		byte.MaxValue,
		32,
		69,
		111,
		159,
		27,
		223,
		27
	})]
	private void readPredInter8x16(MBlock mBlock, int mbX, bool leftAvailable, bool topAvailable, int list, H264Const.PartPred p0, H264Const.PartPred p1)
	{
		int blk8x8X = mbX << 1;
		if (H264Const.usesList(p0, list))
		{
			mBlock.___003C_003Epb168x168.mvdX1[list] = readMVD(0, leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[blk8x8X], p0, mbX, 0, 0, 2, 4, list);
			mBlock.___003C_003Epb168x168.mvdY1[list] = readMVD(1, leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[blk8x8X], p0, mbX, 0, 0, 2, 4, list);
		}
		if (H264Const.usesList(p1, list))
		{
			mBlock.___003C_003Epb168x168.mvdX2[list] = readMVD(0, leftAvailable: true, topAvailable, MBType.___003C_003EP_8x16, topMBType[mbX], p0, predModeTop[blk8x8X + 1], p1, mbX, 2, 0, 2, 4, list);
			mBlock.___003C_003Epb168x168.mvdY2[list] = readMVD(1, leftAvailable: true, topAvailable, MBType.___003C_003EP_8x16, topMBType[mbX], p0, predModeTop[blk8x8X + 1], p1, mbX, 2, 0, 2, 4, list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 232, 129, 70, 103, 52, 199, 127, 0, 159,
		28, 159, 27, 159, 25, 191, 24, 159, 31, 159,
		30, 159, 28, 159, 27, 133, 127, 51
	})]
	private void readPrediction8x8P(MBlock mBlock, int mbX, bool leftAvailable, bool topAvailable)
	{
		for (int i = 0; i < 4; i++)
		{
			mBlock.___003C_003Epb8x8.subMbTypes[i] = readSubMBTypeP();
		}
		if (numRef[0] > 1 && mBlock.curMbType != MBType.___003C_003EP_8x8ref0)
		{
			mBlock.___003C_003Epb8x8.refIdx[0][0] = readRefIdx(leftAvailable, topAvailable, leftMBType, topMBType[mbX], H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, mbX, 0, 0, 2, 2, 0);
			mBlock.___003C_003Epb8x8.refIdx[0][1] = readRefIdx(leftAvailable: true, topAvailable, MBType.___003C_003EP_8x8, topMBType[mbX], H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, mbX, 2, 0, 2, 2, 0);
			mBlock.___003C_003Epb8x8.refIdx[0][2] = readRefIdx(leftAvailable, topAvailable: true, leftMBType, MBType.___003C_003EP_8x8, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, mbX, 0, 2, 2, 2, 0);
			mBlock.___003C_003Epb8x8.refIdx[0][3] = readRefIdx(leftAvailable: true, topAvailable: true, MBType.___003C_003EP_8x8, MBType.___003C_003EP_8x8, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, mbX, 2, 2, 2, 2, 0);
		}
		readSubMb8x8(mBlock, 0, mBlock.___003C_003Epb8x8.subMbTypes[0], topAvailable, leftAvailable, 0, 0, mbX, leftMBType, topMBType[mbX], MBType.___003C_003EP_8x8, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, 0);
		readSubMb8x8(mBlock, 1, mBlock.___003C_003Epb8x8.subMbTypes[1], topAvailable, lAvb: true, 2, 0, mbX, MBType.___003C_003EP_8x8, topMBType[mbX], MBType.___003C_003EP_8x8, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, 0);
		readSubMb8x8(mBlock, 2, mBlock.___003C_003Epb8x8.subMbTypes[2], tAvb: true, leftAvailable, 0, 2, mbX, leftMBType, MBType.___003C_003EP_8x8, MBType.___003C_003EP_8x8, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, 0);
		readSubMb8x8(mBlock, 3, mBlock.___003C_003Epb8x8.subMbTypes[3], tAvb: true, lAvb: true, 2, 2, mbX, MBType.___003C_003EP_8x8, MBType.___003C_003EP_8x8, MBType.___003C_003EP_8x8, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, 0);
		int blk8x8X = mbX << 1;
		H264Const.PartPred[] array = predModeLeft;
		H264Const.PartPred[] array2 = predModeLeft;
		H264Const.PartPred[] array3 = predModeTop;
		H264Const.PartPred[] array4 = predModeTop;
		int num = blk8x8X + 1;
		H264Const.PartPred __003C_003EL = H264Const.PartPred.___003C_003EL0;
		int num2 = num;
		H264Const.PartPred[] array5 = array4;
		H264Const.PartPred partPred = __003C_003EL;
		array5[num2] = __003C_003EL;
		__003C_003EL = partPred;
		num2 = blk8x8X;
		array5 = array3;
		H264Const.PartPred partPred2 = __003C_003EL;
		array5[num2] = __003C_003EL;
		__003C_003EL = partPred2;
		num2 = 1;
		array5 = array2;
		H264Const.PartPred partPred3 = __003C_003EL;
		array5[num2] = __003C_003EL;
		array[0] = partPred3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		225,
		129,
		70,
		104,
		103,
		116,
		23,
		231,
		69,
		108,
		109,
		102,
		109,
		159,
		36,
		109,
		159,
		32,
		109,
		159,
		26,
		109,
		byte.MaxValue,
		20,
		51,
		236,
		81,
		159,
		66,
		102,
		108,
		109,
		191,
		43,
		109,
		223,
		39,
		109,
		223,
		34,
		109,
		byte.MaxValue,
		28,
		48,
		236,
		85,
		108,
		109,
		127,
		6
	})]
	private void readPrediction8x8B(MBlock mBlock, int mbX, bool leftAvailable, bool topAvailable)
	{
		H264Const.PartPred[] p = new H264Const.PartPred[4];
		for (int i = 0; i < 4; i++)
		{
			mBlock.___003C_003Epb8x8.subMbTypes[i] = readSubMBTypeB();
			p[i] = H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[i]];
		}
		for (int list2 = 0; list2 < 2; list2++)
		{
			if (numRef[list2] > 1)
			{
				if (H264Const.usesList(p[0], list2))
				{
					mBlock.___003C_003Epb8x8.refIdx[list2][0] = readRefIdx(leftAvailable, topAvailable, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[mbX << 1], p[0], mbX, 0, 0, 2, 2, list2);
				}
				if (H264Const.usesList(p[1], list2))
				{
					mBlock.___003C_003Epb8x8.refIdx[list2][1] = readRefIdx(leftAvailable: true, topAvailable, MBType.___003C_003EB_8x8, topMBType[mbX], p[0], predModeTop[(mbX << 1) + 1], p[1], mbX, 2, 0, 2, 2, list2);
				}
				if (H264Const.usesList(p[2], list2))
				{
					mBlock.___003C_003Epb8x8.refIdx[list2][2] = readRefIdx(leftAvailable, topAvailable: true, leftMBType, MBType.___003C_003EB_8x8, predModeLeft[1], p[0], p[2], mbX, 0, 2, 2, 2, list2);
				}
				if (H264Const.usesList(p[3], list2))
				{
					mBlock.___003C_003Epb8x8.refIdx[list2][3] = readRefIdx(leftAvailable: true, topAvailable: true, MBType.___003C_003EB_8x8, MBType.___003C_003EB_8x8, p[2], p[1], p[3], mbX, 2, 2, 2, 2, list2);
				}
			}
		}
		MBlockDecoderUtils.debugPrint(new StringBuilder().append("Pred: ").append(p[0]).append(", ")
			.append(p[1])
			.append(", ")
			.append(p[2])
			.append(", ")
			.append(p[3])
			.toString());
		int blk8x8X = mbX << 1;
		for (int list = 0; list < 2; list++)
		{
			if (H264Const.usesList(p[0], list))
			{
				readSubMb8x8(mBlock, 0, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[0]], topAvailable, leftAvailable, 0, 0, mbX, leftMBType, topMBType[mbX], MBType.___003C_003EB_8x8, predModeLeft[0], predModeTop[blk8x8X], p[0], list);
			}
			if (H264Const.usesList(p[1], list))
			{
				readSubMb8x8(mBlock, 1, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[1]], topAvailable, lAvb: true, 2, 0, mbX, MBType.___003C_003EB_8x8, topMBType[mbX], MBType.___003C_003EB_8x8, p[0], predModeTop[blk8x8X + 1], p[1], list);
			}
			if (H264Const.usesList(p[2], list))
			{
				readSubMb8x8(mBlock, 2, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[2]], tAvb: true, leftAvailable, 0, 2, mbX, leftMBType, MBType.___003C_003EB_8x8, MBType.___003C_003EB_8x8, predModeLeft[1], p[0], p[2], list);
			}
			if (H264Const.usesList(p[3], list))
			{
				readSubMb8x8(mBlock, 3, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[3]], tAvb: true, lAvb: true, 2, 2, mbX, MBType.___003C_003EB_8x8, MBType.___003C_003EB_8x8, MBType.___003C_003EB_8x8, p[2], p[1], p[3], list);
			}
		}
		predModeLeft[0] = p[1];
		predModeTop[blk8x8X] = p[2];
		H264Const.PartPred[] array = predModeLeft;
		H264Const.PartPred[] array2 = predModeTop;
		int num = blk8x8X + 1;
		H264Const.PartPred partPred = p[3];
		int num2 = num;
		H264Const.PartPred[] array3 = array2;
		array3[num2] = partPred;
		array[1] = partPred;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 50, 66, 110, 148 })]
	public virtual int readSubMBTypeP()
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int result = CAVLCReader.readUEtrace(reader, "SUB: sub_mb_type");
			
			return result;
		}
		int result2 = cabac.readSubMbTypeP(mDecoder);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 211, 97, 71, 156, 159, 0, 131, 159, 0,
		131, 159, 0, 131, 191, 0
	})]
	private void readSubMb8x8(MBlock mBlock, int partNo, int subMbType, bool tAvb, bool lAvb, int blk8x8X, int blk8x8Y, int mbX, MBType leftMBType, MBType topMBType, MBType curMBType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred partPred, int list)
	{
		switch (subMbType)
		{
		case 3:
			readSub4x4(mBlock, partNo, tAvb, lAvb, blk8x8X, blk8x8Y, mbX, leftMBType, topMBType, curMBType, leftPred, topPred, partPred, list);
			break;
		case 2:
			readSub4x8(mBlock, partNo, tAvb, lAvb, blk8x8X, blk8x8Y, mbX, leftMBType, topMBType, curMBType, leftPred, topPred, partPred, list);
			break;
		case 1:
			readSub8x4(mBlock, partNo, tAvb, lAvb, blk8x8X, blk8x8Y, mbX, leftMBType, topMBType, curMBType, leftPred, topPred, partPred, list);
			break;
		case 0:
			readSub8x8(mBlock, partNo, tAvb, lAvb, blk8x8X, blk8x8Y, mbX, leftMBType, topMBType, leftPred, topPred, partPred, list);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 49, 162, 110, 148 })]
	public virtual int readSubMBTypeB()
	{
		if (!activePps.entropyCodingModeFlag)
		{
			int result = CAVLCReader.readUEtrace(reader, "SUB: sub_mb_type");
			
			return result;
		}
		int result2 = cabac.readSubMbTypeB(mDecoder);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 197, 161, 70, 159, 15, 159, 15, 159, 17,
		159, 17, 159, 17, 159, 17, 159, 19, 159, 19
	})]
	private void readSub4x4(MBlock mBlock, int partNo, bool tAvb, bool lAvb, int blk8x8X, int blk8x8Y, int mbX, MBType leftMBType, MBType topMBType, MBType curMBType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred partPred, int list)
	{
		mBlock.___003C_003Epb8x8.mvdX1[list][partNo] = readMVD(0, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdY1[list][partNo] = readMVD(1, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdX2[list][partNo] = readMVD(0, leftAvailable: true, tAvb, curMBType, topMBType, partPred, topPred, partPred, mbX, blk8x8X + 1, blk8x8Y, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdY2[list][partNo] = readMVD(1, leftAvailable: true, tAvb, curMBType, topMBType, partPred, topPred, partPred, mbX, blk8x8X + 1, blk8x8Y, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdX3[list][partNo] = readMVD(0, lAvb, topAvailable: true, leftMBType, curMBType, leftPred, partPred, partPred, mbX, blk8x8X, blk8x8Y + 1, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdY3[list][partNo] = readMVD(1, lAvb, topAvailable: true, leftMBType, curMBType, leftPred, partPred, partPred, mbX, blk8x8X, blk8x8Y + 1, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdX4[list][partNo] = readMVD(0, leftAvailable: true, topAvailable: true, curMBType, curMBType, partPred, partPred, partPred, mbX, blk8x8X + 1, blk8x8Y + 1, 1, 1, list);
		mBlock.___003C_003Epb8x8.mvdY4[list][partNo] = readMVD(1, leftAvailable: true, topAvailable: true, curMBType, curMBType, partPred, partPred, partPred, mbX, blk8x8X + 1, blk8x8Y + 1, 1, 1, list);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 200, 129, 70, 159, 15, 159, 15, 159, 17,
		159, 17
	})]
	private void readSub4x8(MBlock mBlock, int partNo, bool tAvb, bool lAvb, int blk8x8X, int blk8x8Y, int mbX, MBType leftMBType, MBType topMBType, MBType curMBType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred partPred, int list)
	{
		mBlock.___003C_003Epb8x8.mvdX1[list][partNo] = readMVD(0, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 1, 2, list);
		mBlock.___003C_003Epb8x8.mvdY1[list][partNo] = readMVD(1, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 1, 2, list);
		mBlock.___003C_003Epb8x8.mvdX2[list][partNo] = readMVD(0, leftAvailable: true, tAvb, curMBType, topMBType, partPred, topPred, partPred, mbX, blk8x8X + 1, blk8x8Y, 1, 2, list);
		mBlock.___003C_003Epb8x8.mvdY2[list][partNo] = readMVD(1, leftAvailable: true, tAvb, curMBType, topMBType, partPred, topPred, partPred, mbX, blk8x8X + 1, blk8x8Y, 1, 2, list);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 203, 65, 70, 159, 15, 191, 15, 159, 17,
		159, 17
	})]
	private void readSub8x4(MBlock mBlock, int partNo, bool tAvb, bool lAvb, int blk8x8X, int blk8x8Y, int mbX, MBType leftMBType, MBType topMBType, MBType curMBType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred partPred, int list)
	{
		mBlock.___003C_003Epb8x8.mvdX1[list][partNo] = readMVD(0, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 2, 1, list);
		mBlock.___003C_003Epb8x8.mvdY1[list][partNo] = readMVD(1, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 2, 1, list);
		mBlock.___003C_003Epb8x8.mvdX2[list][partNo] = readMVD(0, lAvb, topAvailable: true, leftMBType, curMBType, leftPred, partPred, partPred, mbX, blk8x8X, blk8x8Y + 1, 2, 1, list);
		mBlock.___003C_003Epb8x8.mvdY2[list][partNo] = readMVD(1, lAvb, topAvailable: true, leftMBType, curMBType, leftPred, partPred, partPred, mbX, blk8x8X, blk8x8Y + 1, 2, 1, list);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 206, 129, 70, 159, 15, 159, 15, 127, 39 })]
	private void readSub8x8(MBlock mBlock, int partNo, bool tAvb, bool lAvb, int blk8x8X, int blk8x8Y, int mbX, MBType leftMBType, MBType topMBType, H264Const.PartPred leftPred, H264Const.PartPred topPred, H264Const.PartPred partPred, int list)
	{
		mBlock.___003C_003Epb8x8.mvdX1[list][partNo] = readMVD(0, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 2, 2, list);
		mBlock.___003C_003Epb8x8.mvdY1[list][partNo] = readMVD(1, lAvb, tAvb, leftMBType, topMBType, leftPred, topPred, partPred, mbX, blk8x8X, blk8x8Y, 2, 2, list);
		MBlockDecoderUtils.debugPrint("mvd: (%d, %d)", Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX1[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY1[list][partNo]));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 161, 69, 99, 113, 127, 2, 124, 138,
		105, 105, 142, 127, 8
	})]
	internal virtual int readPredictionI4x4Block(bool leftAvailable, bool topAvailable, MBType leftMBType, MBType topMBType, int blkX, int blkY, int mbX)
	{
		int mode = 2;
		if ((leftAvailable || blkX > 0) && (topAvailable || blkY > 0))
		{
			int predModeB = ((topMBType != MBType.___003C_003EI_NxN && blkY <= 0) ? 2 : i4x4PredTop[(mbX << 2) + blkX]);
			int predModeA = ((leftMBType != MBType.___003C_003EI_NxN && blkX <= 0) ? 2 : i4x4PredLeft[blkY]);
			mode = Math.min(predModeB, predModeA);
		}
		if (!prev4x4PredMode())
		{
			int rem_intra4x4_pred_mode = rem4x4PredMode();
			mode = rem_intra4x4_pred_mode + ((rem_intra4x4_pred_mode >= mode) ? 1 : 0);
		}
		int[] array = i4x4PredTop;
		int num = (mbX << 2) + blkX;
		int[] array2 = i4x4PredLeft;
		int num2 = mode;
		int[] array3 = array2;
		array3[blkY] = num2;
		array[num] = num2;
		return mode;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 161, 69, 110, 152 })]
	protected internal virtual int readCodedBlockPatternIntra(bool leftAvailable, bool topAvailable, int leftCBP, int topCBP, MBType leftMB, MBType topMB)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			return H264Const.___003C_003ECODED_BLOCK_PATTERN_INTRA_COLOR[CAVLCReader.readUEtrace(reader, "coded_block_pattern")];
		}
		int result = cabac.codedBlockPatternIntra(mDecoder, leftAvailable, topAvailable, leftCBP, topCBP, leftMB, topMB);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		179,
		161,
		69,
		107,
		105,
		106,
		105,
		138,
		114,
		115,
		206,
		byte.MaxValue,
		17,
		52,
		234,
		80,
		111
	})]
	private void readLuma(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY)
	{
		for (int i = 0; i < 16; i++)
		{
			int blkOffLeft = H264Const.___003C_003EMB_BLK_OFF_LEFT[i];
			int blkOffTop = H264Const.___003C_003EMB_BLK_OFF_TOP[i];
			int blkX = (mbX << 2) + blkOffLeft;
			int blkY = (mbY << 2) + blkOffTop;
			if ((mBlock.cbpLuma() & (1 << (i >> 2))) == 0)
			{
				if (!sh.pps.entropyCodingModeFlag)
				{
					setZeroCoeff(0, blkX, blkOffTop);
				}
			}
			else
			{
				mBlock.nCoeff[i] = readResidualAC(leftAvailable, topAvailable, mbX, mBlock.curMbType, mBlock.cbpLuma(), blkOffLeft, blkOffTop, blkX, blkY, mBlock.ac[0][i]);
			}
		}
		savePrevCBP(mBlock._cbp);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		174,
		162,
		106,
		103,
		101,
		103,
		136,
		112,
		166,
		150,
		102,
		byte.MaxValue,
		56,
		51,
		234,
		79,
		111
	})]
	private void readLuma8x8CABAC(MBlock mBlock, int mbX, int mbY)
	{
		for (int i = 0; i < 4; i++)
		{
			int blkOffLeft = (i & 1) << 1;
			int blkOffTop = i & 2;
			int blkX = (mbX << 2) + blkOffLeft;
			int blkY = (mbY << 2) + blkOffTop;
			if ((mBlock.cbpLuma() & (1 << i)) != 0)
			{
				int nCoeff = readLumaAC8x8(blkX, blkY, mBlock.ac[0][i]);
				int blk4x4Offset = i << 2;
				int[] nCoeff2 = mBlock.nCoeff;
				int[] nCoeff3 = mBlock.nCoeff;
				int num = blk4x4Offset + 1;
				int[] nCoeff4 = mBlock.nCoeff;
				int num2 = blk4x4Offset + 2;
				int[] nCoeff5 = mBlock.nCoeff;
				int num3 = blk4x4Offset + 3;
				int num4 = nCoeff;
				int num5 = num3;
				int[] array = nCoeff5;
				int num6 = num4;
				array[num5] = num4;
				num4 = num6;
				num5 = num2;
				array = nCoeff4;
				int num7 = num4;
				array[num5] = num4;
				num4 = num7;
				num5 = num;
				array = nCoeff3;
				int num8 = num4;
				array[num5] = num4;
				nCoeff2[blk4x4Offset] = num8;
			}
		}
		savePrevCBP(mBlock._cbp);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		169,
		129,
		69,
		106,
		103,
		102,
		105,
		138,
		112,
		108,
		110,
		110,
		112,
		134,
		100,
		108,
		106,
		105,
		106,
		159,
		1,
		106,
		62,
		233,
		58,
		236,
		73,
		102,
		byte.MaxValue,
		56,
		40,
		234,
		90
	})]
	private void readLuma8x8CAVLC(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY)
	{
		for (int i = 0; i < 4; i++)
		{
			int blk8x8OffLeft = (i & 1) << 1;
			int blk8x8OffTop = i & 2;
			int blkX = (mbX << 2) + blk8x8OffLeft;
			int blkY = (mbY << 2) + blk8x8OffTop;
			if ((mBlock.cbpLuma() & (1 << i)) == 0)
			{
				setZeroCoeff(0, blkX, blk8x8OffTop);
				setZeroCoeff(0, blkX + 1, blk8x8OffTop);
				setZeroCoeff(0, blkX, blk8x8OffTop + 1);
				setZeroCoeff(0, blkX + 1, blk8x8OffTop + 1);
				continue;
			}
			int coeffs = 0;
			for (int j = 0; j < 4; j++)
			{
				int[] ac16 = new int[16];
				int blkOffLeft = blk8x8OffLeft + (j & 1);
				int blkOffTop = blk8x8OffTop + (j >> 1);
				coeffs += readLumaAC(leftAvailable, topAvailable, mbX, mBlock.curMbType, blkX, j, ac16, blkOffLeft, blkOffTop);
				for (int k = 0; k < 16; k++)
				{
					mBlock.ac[0][i][CoeffTransformer.zigzag8x8[(k << 2) + j]] = ac16[k];
				}
			}
			int blk4x4Offset = i << 2;
			int[] nCoeff = mBlock.nCoeff;
			int[] nCoeff2 = mBlock.nCoeff;
			int num = blk4x4Offset + 1;
			int[] nCoeff3 = mBlock.nCoeff;
			int num2 = blk4x4Offset + 2;
			int[] nCoeff4 = mBlock.nCoeff;
			int num3 = blk4x4Offset + 3;
			int num4 = coeffs;
			int num5 = num3;
			int[] array = nCoeff4;
			int num6 = num4;
			array[num5] = num4;
			num4 = num6;
			num5 = num2;
			array = nCoeff3;
			int num7 = num4;
			array[num5] = num4;
			num4 = num7;
			num5 = num;
			array = nCoeff2;
			int num8 = num4;
			array[num5] = num4;
			nCoeff[blk4x4Offset] = num8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 63, 161, 69, 113, 111, 99, 223, 58, 159,
		34, 191, 15
	})]
	internal virtual int readResidualAC(bool leftAvailable, bool topAvailable, int mbX, MBType curMbType, int cbpLuma, int blkOffLeft, int blkOffTop, int blkX, int blkY, int[] ac)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			if (reader.remaining() <= 0)
			{
				return 0;
			}
			int result = cavlc[0].readACBlock(reader, ac, blkX, blkOffTop, (blkOffLeft != 0 || leftAvailable) ? true : false, (blkOffLeft != 0) ? curMbType : leftMBType, (blkOffTop != 0 || topAvailable) ? true : false, (blkOffTop != 0) ? curMbType : topMBType[mbX], 0, 16, CoeffTransformer.zigzag4x4);
			
			return result;
		}
		if (cabac.readCodedBlockFlagLumaAC(mDecoder, CABAC.BlockType.___003C_003ELUMA_16, blkX, blkOffTop, 0, leftMBType, topMBType[mbX], leftAvailable, topAvailable, leftCBPLuma, topCBPLuma[mbX], cbpLuma, curMbType) == 1)
		{
			int result2 = cabac.readCoeffs(mDecoder, CABAC.BlockType.___003C_003ELUMA_16, ac, 0, 16, CoeffTransformer.zigzag4x4, H264Const.___003C_003EidentityMapping16, H264Const.___003C_003EidentityMapping16);
			
			return result2;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 58, 162, 110, 111 })]
	public virtual void savePrevCBP(int codedBlockPattern)
	{
		if (activePps.entropyCodingModeFlag)
		{
			cabac.setPrevCBP(codedBlockPattern);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 53, 98, 159, 12, 110, 112, 112, 146 })]
	public virtual int readLumaAC8x8(int blkX, int blkY, int[] ac)
	{
		int readCoeffs = cabac.readCoeffs(mDecoder, CABAC.BlockType.___003C_003ELUMA_64, ac, 0, 64, CoeffTransformer.zigzag8x8, H264Const.___003C_003Esig_coeff_map_8x8, H264Const.___003C_003Elast_sig_coeff_map_8x8);
		cabac.setCodedBlock(blkX, blkY);
		cabac.setCodedBlock(blkX + 1, blkY);
		cabac.setCodedBlock(blkX, blkY + 1);
		cabac.setCodedBlock(blkX + 1, blkY + 1);
		return readCoeffs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 56, 97, 69 })]
	public virtual int readLumaAC(bool leftAvailable, bool topAvailable, int mbX, MBType curMbType, int blkX, int j, int[] ac16, int blkOffLeft, int blkOffTop)
	{
		int result = cavlc[0].readACBlock(reader, ac16, blkX + (j & 1), blkOffTop, (blkOffLeft != 0 || leftAvailable) ? true : false, (blkOffLeft != 0) ? curMbType : leftMBType, (blkOffTop != 0 || topAvailable) ? true : false, (blkOffTop != 0) ? curMbType : topMBType[mbX], 0, 16, H264Const.___003C_003EidentityMapping16);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 47, 129, 69, 110, 155, 159, 25, 191, 12 })]
	public virtual void readChromaDC(int mbX, bool leftAvailable, bool topAvailable, int[] dc, int comp, MBType curMbType)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			cavlc[comp].readChromaDCBlock(reader, dc, leftAvailable, topAvailable);
		}
		else if (cabac.readCodedBlockFlagChromaDC(mDecoder, mbX, comp, leftMBType, topMBType[mbX], leftAvailable, topAvailable, leftCBPChroma, topCBPChroma[mbX], curMbType) == 1)
		{
			cabac.readCoeffs(mDecoder, CABAC.BlockType.___003C_003ECHROMA_DC, dc, 0, 4, H264Const.___003C_003EidentityMapping16, H264Const.___003C_003EidentityMapping16, H264Const.___003C_003EidentityMapping16);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 157, 129, 72, 108, 103, 106, 138, 137, 100,
		152, 115, 237, 53, 234, 78
	})]
	private void _readChromaAC(bool leftAvailable, bool topAvailable, int mbX, int[] dc, int comp, MBType curMbType, bool codedAC, int[][] residualOut)
	{
		for (int i = 0; i < (nint)dc.LongLength; i++)
		{
			int[] ac = residualOut[i];
			int blkOffLeft = H264Const.___003C_003EMB_BLK_OFF_LEFT[i];
			int blkOffTop = H264Const.___003C_003EMB_BLK_OFF_TOP[i];
			int blkX = (mbX << 1) + blkOffLeft;
			if (codedAC)
			{
				readChromaAC(leftAvailable, topAvailable, mbX, comp, curMbType, ac, blkOffLeft, blkOffTop, blkX);
			}
			else if (!sh.pps.entropyCodingModeFlag)
			{
				setZeroCoeff(comp, blkX, blkOffTop);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 44, 129, 69, 113, 111, 98, 223, 59, 159,
		28, 191, 13
	})]
	public virtual void readChromaAC(bool leftAvailable, bool topAvailable, int mbX, int comp, MBType curMbType, int[] ac, int blkOffLeft, int blkOffTop, int blkX)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			if (reader.remaining() > 0)
			{
				cavlc[comp].readACBlock(reader, ac, blkX, blkOffTop, (blkOffLeft != 0 || leftAvailable) ? true : false, (blkOffLeft != 0) ? curMbType : leftMBType, (blkOffTop != 0 || topAvailable) ? true : false, (blkOffTop != 0) ? curMbType : topMBType[mbX], 1, 15, CoeffTransformer.zigzag4x4);
			}
		}
		else if (cabac.readCodedBlockFlagChromaAC(mDecoder, blkX, blkOffTop, comp, leftMBType, topMBType[mbX], leftAvailable, topAvailable, leftCBPChroma, topCBPChroma[mbX], curMbType) == 1)
		{
			cabac.readCoeffs(mDecoder, CABAC.BlockType.___003C_003ECHROMA_AC, ac, 1, 15, CoeffTransformer.zigzag4x4, H264Const.___003C_003EidentityMapping16, H264Const.___003C_003EidentityMapping16);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 145, 98, 127, 7, 63, 5, 171, 112 })]
	private void readMBlockI(MBlock mBlock)
	{
		mBlock.mbType = decodeMBTypeI(mBlock.mbIdx, mapper.leftAvailable(mBlock.mbIdx), mapper.topAvailable(mBlock.mbIdx), leftMBType, topMBType[mapper.getMbX(mBlock.mbIdx)]);
		readMBlockIInt(mBlock, mBlock.mbType);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 140, 130, 141, 159, 6, 108, 109, 134, 108,
		114, 131, 108, 114, 131, 108, 104, 131, 108, 104,
		131, 146
	})]
	private void readMBlockP(MBlock mBlock)
	{
		mBlock.mbType = readMBTypeP();
		switch (mBlock.mbType)
		{
		case 0:
			mBlock.curMbType = MBType.___003C_003EP_16x16;
			readInter16x16(H264Const.PartPred.___003C_003EL0, mBlock);
			break;
		case 1:
			mBlock.curMbType = MBType.___003C_003EP_16x8;
			readInter16x8(H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, mBlock);
			break;
		case 2:
			mBlock.curMbType = MBType.___003C_003EP_8x16;
			readIntra8x16(H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0, mBlock);
			break;
		case 3:
			mBlock.curMbType = MBType.___003C_003EP_8x8;
			readMBlock8x8(mBlock);
			break;
		case 4:
			mBlock.curMbType = MBType.___003C_003EP_8x8ref0;
			readMBlock8x8(mBlock);
			break;
		default:
			readMBlockIInt(mBlock, mBlock.mbType - 5);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 133, 162, 127, 7, 63, 5, 139, 107, 150,
		147, 105, 109, 106, 120, 107, 106, 107, 159, 7,
		191, 7
	})]
	private void readMBlockB(MBlock mBlock)
	{
		mBlock.mbType = readMBTypeB(mBlock.mbIdx, mapper.leftAvailable(mBlock.mbIdx), mapper.topAvailable(mBlock.mbIdx), leftMBType, topMBType[mapper.getMbX(mBlock.mbIdx)]);
		if (mBlock.mbType >= 23)
		{
			readMBlockIInt(mBlock, mBlock.mbType - 23);
			return;
		}
		mBlock.curMbType = H264Const.___003C_003EbMbTypes[mBlock.mbType];
		if (mBlock.mbType == 0)
		{
			readMBlockBDirect(mBlock);
		}
		else if (mBlock.mbType <= 3)
		{
			readInter16x16(H264Const.___003C_003EbPredModes[mBlock.mbType][0], mBlock);
		}
		else if (mBlock.mbType == 22)
		{
			readMBlock8x8(mBlock);
		}
		else if ((mBlock.mbType & 1) == 0)
		{
			readInter16x8(H264Const.___003C_003EbPredModes[mBlock.mbType][0], H264Const.___003C_003EbPredModes[mBlock.mbType][1], mBlock);
		}
		else
		{
			readIntra8x16(H264Const.___003C_003EbPredModes[mBlock.mbType][0], H264Const.___003C_003EbPredModes[mBlock.mbType][1], mBlock);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 40, 129, 69, 110, 148, 121 })]
	public virtual int decodeMBTypeI(int mbIdx, bool leftAvailable, bool topAvailable, MBType leftMBType, MBType topMBType)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			return CAVLCReader.readUEtrace(reader, "MB: mb_type");
		}
		return cabac.readMBTypeI(mDecoder, leftMBType, topMBType, leftAvailable, topAvailable);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 143, 66, 100, 108, 106, 106, 108, 141, 107,
		108, 138
	})]
	private void readMBlockIInt(MBlock mBlock, int mbType)
	{
		switch (mbType)
		{
		case 0:
			mBlock.curMbType = MBType.___003C_003EI_NxN;
			readIntraNxN(mBlock);
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 21:
		case 22:
		case 23:
		case 24:
			mBlock.curMbType = MBType.___003C_003EI_16x16;
			readIntra16x16(mbType - 1, mBlock);
			break;
		default:
			Logger.warn("IPCM macroblock found. Not tested, may cause unpredictable behavior.");
			mBlock.curMbType = MBType.___003C_003EI_PCM;
			readIPCM(mBlock);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		192,
		130,
		115,
		115,
		115,
		147,
		104,
		105,
		223,
		12,
		105,
		106,
		107,
		107,
		byte.MaxValue,
		6,
		61,
		238,
		71,
		108,
		105,
		103,
		159,
		6,
		117,
		253,
		58,
		236,
		73,
		144,
		191,
		32,
		115,
		147,
		108,
		110,
		141
	})]
	public virtual void readIntraNxN(MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		mBlock.transform8x8Used = false;
		if (transform8x8)
		{
			mBlock.transform8x8Used = readTransform8x8Flag((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], tf8x8Left, tf8x8Top[mbX]);
		}
		if (!mBlock.transform8x8Used)
		{
			for (int j = 0; j < 16; j++)
			{
				int blkX2 = H264Const.___003C_003EMB_BLK_OFF_LEFT[j];
				int blkY2 = H264Const.___003C_003EMB_BLK_OFF_TOP[j];
				mBlock.lumaModes[j] = readPredictionI4x4Block((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], blkX2, blkY2, mbX);
			}
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				int blkX = (i & 1) << 1;
				int blkY = i & 2;
				mBlock.lumaModes[i] = readPredictionI4x4Block((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], blkX, blkY, mbX);
				i4x4PredLeft[blkY + 1] = i4x4PredLeft[blkY];
				i4x4PredTop[(mbX << 2) + blkX + 1] = i4x4PredTop[(mbX << 2) + blkX];
			}
		}
		mBlock.chromaPredictionMode = readChromaPredMode(mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0);
		mBlock._cbp = readCodedBlockPatternIntra((byte)leftAvailable != 0, (byte)topAvailable != 0, leftCBPLuma | (leftCBPChroma << 4), topCBPLuma[mbX] | (topCBPChroma[mbX] << 4), leftMBType, topMBType[mbX]);
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			mBlock.mbQPDelta = readMBQpDelta(mBlock.prevMbType);
		}
		readResidualLuma(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		if (chromaFormat != ColorSpace.___003C_003EMONO)
		{
			readChromaResidual(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 32, 66, 115, 115, 115, 115, 124, 115, 112,
		115, 112, 109, 107, 107, 105, 137, 115, 191, 15,
		115, 236, 53, 236, 79, 110, 141
	})]
	public virtual void readIntra16x16(int mbType, MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int cbpLuma = mbType / 12 * 15;
		int num = mbType / 4;
		mBlock.cbp(cbpLuma, (3 != -1) ? (num % 3) : 0);
		mBlock.luma16x16Mode = ((4 != -1) ? (mbType % 4) : 0);
		mBlock.chromaPredictionMode = readChromaPredMode(mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0);
		mBlock.mbQPDelta = readMBQpDelta(mBlock.prevMbType);
		read16x16DC((byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mBlock.dc);
		for (int i = 0; i < 16; i++)
		{
			int blkOffLeft = H264Const.___003C_003EMB_BLK_OFF_LEFT[i];
			int blkOffTop = H264Const.___003C_003EMB_BLK_OFF_TOP[i];
			int blkX = (mbX << 2) + blkOffLeft;
			int blkY = (mbY << 2) + blkOffTop;
			if ((mBlock.cbpLuma() & (1 << (i >> 2))) != 0)
			{
				mBlock.nCoeff[i] = read16x16AC((byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mBlock.cbpLuma(), mBlock.ac[0][i], blkOffLeft, blkOffTop, blkX, blkY);
			}
			else if (!sh.pps.entropyCodingModeFlag)
			{
				setZeroCoeff(0, blkX, blkOffTop);
			}
		}
		if (chromaFormat != ColorSpace.___003C_003EMONO)
		{
			readChromaResidual(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 153, 162, 141, 107, 58, 167, 117, 149, 107,
		58, 167
	})]
	private void readIPCM(MBlock mBlock)
	{
		reader.align();
		for (int j = 0; j < 256; j++)
		{
			mBlock.___003C_003Eipcm.samplesLuma[j] = reader.readNBit(8);
		}
		int MbWidthC = 16 >> chromaFormat.compWidth[1];
		int MbHeightC = 16 >> chromaFormat.compHeight[1];
		for (int i = 0; i < 2 * MbWidthC * MbHeightC; i++)
		{
			mBlock.___003C_003Eipcm.samplesChroma[i] = reader.readNBit(8);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 38, 162, 110, 148, 115 })]
	public virtual int readMBTypeP()
	{
		if (!activePps.entropyCodingModeFlag)
		{
			return CAVLCReader.readUEtrace(reader, "MB: mb_type");
		}
		return cabac.readMBTypeP(mDecoder);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 19, 98, 115, 115, 115, 147, 108, 119, 31,
		32, 236, 69, 105, 46, 169, 140, 127, 51
	})]
	public virtual void readInter16x16(H264Const.PartPred p0, MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		for (int list2 = 0; list2 < 2; list2++)
		{
			if (H264Const.usesList(p0, list2) && numRef[list2] > 1)
			{
				mBlock.___003C_003Epb16x16.refIdx[list2] = readRefIdx((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[mbX << 1], p0, mbX, 0, 0, 4, 4, list2);
			}
		}
		for (int list = 0; list < 2; list++)
		{
			readPredictionInter16x16(mBlock, mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0, list, p0);
		}
		readResidualInter(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		H264Const.PartPred[] array = predModeLeft;
		H264Const.PartPred[] array2 = predModeLeft;
		H264Const.PartPred[] array3 = predModeTop;
		int num = mbX << 1;
		H264Const.PartPred[] array4 = predModeTop;
		int num2 = (mbX << 1) + 1;
		H264Const.PartPred partPred = p0;
		int num3 = num2;
		H264Const.PartPred[] array5 = array4;
		H264Const.PartPred partPred2 = partPred;
		array5[num3] = partPred;
		partPred = partPred2;
		num3 = num;
		array5 = array3;
		H264Const.PartPred partPred3 = partPred;
		array5[num3] = partPred;
		partPred = partPred3;
		num3 = 1;
		array5 = array2;
		H264Const.PartPred partPred4 = partPred;
		array5[num3] = partPred;
		array[0] = partPred4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		4,
		66,
		115,
		115,
		115,
		147,
		108,
		119,
		159,
		32,
		119,
		byte.MaxValue,
		21,
		59,
		236,
		73,
		105,
		47,
		169,
		140,
		106,
		127,
		29
	})]
	public virtual void readInter16x8(H264Const.PartPred p0, H264Const.PartPred p1, MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		for (int list2 = 0; list2 < 2; list2++)
		{
			if (H264Const.usesList(p0, list2) && numRef[list2] > 1)
			{
				mBlock.___003C_003Epb168x168.refIdx1[list2] = readRefIdx((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[mbX << 1], p0, mbX, 0, 0, 4, 2, list2);
			}
			if (H264Const.usesList(p1, list2) && numRef[list2] > 1)
			{
				mBlock.___003C_003Epb168x168.refIdx2[list2] = readRefIdx((byte)leftAvailable != 0, topAvailable: true, leftMBType, mBlock.curMbType, predModeLeft[1], p0, p1, mbX, 0, 2, 4, 2, list2);
			}
		}
		for (int list = 0; list < 2; list++)
		{
			readPredictionInter16x8(mBlock, mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0, p0, p1, list);
		}
		readResidualInter(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		predModeLeft[0] = p0;
		H264Const.PartPred[] array = predModeLeft;
		H264Const.PartPred[] array2 = predModeTop;
		int num = mbX << 1;
		H264Const.PartPred[] array3 = predModeTop;
		int num2 = (mbX << 1) + 1;
		H264Const.PartPred partPred = p1;
		int num3 = num2;
		H264Const.PartPred[] array4 = array3;
		H264Const.PartPred partPred2 = partPred;
		array4[num3] = partPred;
		partPred = partPred2;
		num3 = num;
		array4 = array2;
		H264Const.PartPred partPred3 = partPred;
		array4[num3] = partPred;
		array[1] = partPred3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		254,
		66,
		115,
		115,
		115,
		115,
		108,
		119,
		159,
		32,
		119,
		byte.MaxValue,
		27,
		59,
		236,
		73,
		105,
		47,
		169,
		108,
		108,
		127,
		27
	})]
	public virtual void readIntra8x16(H264Const.PartPred p0, H264Const.PartPred p1, MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		for (int list2 = 0; list2 < 2; list2++)
		{
			if (H264Const.usesList(p0, list2) && numRef[list2] > 1)
			{
				mBlock.___003C_003Epb168x168.refIdx1[list2] = readRefIdx((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], predModeLeft[0], predModeTop[mbX << 1], p0, mbX, 0, 0, 2, 4, list2);
			}
			if (H264Const.usesList(p1, list2) && numRef[list2] > 1)
			{
				mBlock.___003C_003Epb168x168.refIdx2[list2] = readRefIdx(leftAvailable: true, (byte)topAvailable != 0, mBlock.curMbType, topMBType[mbX], p0, predModeTop[(mbX << 1) + 1], p1, mbX, 2, 0, 2, 4, list2);
			}
		}
		for (int list = 0; list < 2; list++)
		{
			readPredInter8x16(mBlock, mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0, list, p0, p1);
		}
		readResidualInter(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		predModeTop[mbX << 1] = p0;
		H264Const.PartPred[] array = predModeTop;
		int num = (mbX << 1) + 1;
		H264Const.PartPred[] array2 = predModeLeft;
		H264Const.PartPred[] array3 = predModeLeft;
		H264Const.PartPred partPred = p1;
		int num2 = 1;
		H264Const.PartPred[] array4 = array3;
		H264Const.PartPred partPred2 = partPred;
		array4[num2] = partPred;
		partPred = partPred2;
		num2 = 0;
		array4 = array2;
		H264Const.PartPred partPred3 = partPred;
		array4[num2] = partPred;
		array[num] = partPred3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		241,
		162,
		115,
		115,
		115,
		179,
		123,
		139,
		191,
		41,
		107,
		byte.MaxValue,
		60,
		69,
		191,
		32,
		104,
		117,
		223,
		12,
		115,
		147,
		108,
		109
	})]
	public virtual void readMBlock8x8(MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int noSubMBLessThen8x8;
		if (mBlock.curMbType == MBType.___003C_003EP_8x8 || mBlock.curMbType == MBType.___003C_003EP_8x8ref0)
		{
			readPrediction8x8P(mBlock, mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0);
			noSubMBLessThen8x8 = ((mBlock.___003C_003Epb8x8.subMbTypes[0] == 0 && mBlock.___003C_003Epb8x8.subMbTypes[1] == 0 && mBlock.___003C_003Epb8x8.subMbTypes[2] == 0 && mBlock.___003C_003Epb8x8.subMbTypes[3] == 0) ? 1 : 0);
		}
		else
		{
			readPrediction8x8B(mBlock, mbX, (byte)leftAvailable != 0, (byte)topAvailable != 0);
			noSubMBLessThen8x8 = ((H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[0]] == 0 && H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[1]] == 0 && H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[2]] == 0 && H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[3]] == 0) ? 1 : 0);
		}
		mBlock._cbp = readCodedBlockPatternInter((byte)leftAvailable != 0, (byte)topAvailable != 0, leftCBPLuma | (leftCBPChroma << 4), topCBPLuma[mbX] | (topCBPChroma[mbX] << 4), leftMBType, topMBType[mbX]);
		mBlock.transform8x8Used = false;
		if (transform8x8 && mBlock.cbpLuma() != 0 && noSubMBLessThen8x8 != 0)
		{
			mBlock.transform8x8Used = readTransform8x8Flag((byte)leftAvailable != 0, (byte)topAvailable != 0, leftMBType, topMBType[mbX], tf8x8Left, tf8x8Top[mbX]);
		}
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			mBlock.mbQPDelta = readMBQpDelta(mBlock.prevMbType);
		}
		readResidualLuma(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		readChromaResidual(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 35, 65, 69, 110, 148, 121 })]
	public virtual int readMBTypeB(int mbIdx, bool leftAvailable, bool topAvailable, MBType leftMBType, MBType topMBType)
	{
		if (!activePps.entropyCodingModeFlag)
		{
			return CAVLCReader.readUEtrace(reader, "MB: mb_type");
		}
		return cabac.readMBTypeB(mDecoder, leftMBType, topMBType, leftAvailable, topAvailable);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 25, 130, 115, 115, 115, 115, 191, 32, 104,
		127, 4, 223, 12, 115, 147, 108, 139, 127, 55
	})]
	public virtual void readMBlockBDirect(MBlock mBlock)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int lAvb = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int tAvb = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		mBlock._cbp = readCodedBlockPatternInter((byte)lAvb != 0, (byte)tAvb != 0, leftCBPLuma | (leftCBPChroma << 4), topCBPLuma[mbX] | (topCBPChroma[mbX] << 4), leftMBType, topMBType[mbX]);
		mBlock.transform8x8Used = false;
		if (transform8x8 && mBlock.cbpLuma() != 0 && sh.sps.direct8x8InferenceFlag)
		{
			mBlock.transform8x8Used = readTransform8x8Flag((byte)lAvb != 0, (byte)tAvb != 0, leftMBType, topMBType[mbX], tf8x8Left, tf8x8Top[mbX]);
		}
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			mBlock.mbQPDelta = readMBQpDelta(mBlock.prevMbType);
		}
		readResidualLuma(mBlock, (byte)lAvb != 0, (byte)tAvb != 0, mbX, mbY);
		readChromaResidual(mBlock, (byte)lAvb != 0, (byte)tAvb != 0, mbX);
		H264Const.PartPred[] array = predModeTop;
		int num = mbX << 1;
		H264Const.PartPred[] array2 = predModeTop;
		int num2 = (mbX << 1) + 1;
		H264Const.PartPred[] array3 = predModeLeft;
		H264Const.PartPred[] array4 = predModeLeft;
		H264Const.PartPred __003C_003EDirect = H264Const.PartPred.___003C_003EDirect;
		int num3 = 1;
		H264Const.PartPred[] array5 = array4;
		H264Const.PartPred partPred = __003C_003EDirect;
		array5[num3] = __003C_003EDirect;
		__003C_003EDirect = partPred;
		num3 = 0;
		array5 = array3;
		H264Const.PartPred partPred2 = __003C_003EDirect;
		array5[num3] = __003C_003EDirect;
		__003C_003EDirect = partPred2;
		num3 = num2;
		array5 = array2;
		H264Const.PartPred partPred3 = __003C_003EDirect;
		array5[num3] = __003C_003EDirect;
		array[num] = partPred3;
	}

	[LineNumberTable(1089)]
	public virtual NALUnit getNALUnit()
	{
		return nalUnit;
	}
}
