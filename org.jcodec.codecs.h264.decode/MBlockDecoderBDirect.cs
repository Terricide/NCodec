using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderBDirect : MBlockDecoderBase
{
	private Mapper mapper;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 65, 77, 110, 152, 120 })]
	public virtual void predictBDirect(org.jcodec.codecs.h264.io.model.Frame[][] refs, int mbX, int mbY, bool lAvb, bool tAvb, bool tlAvb, bool trAvb, H264Utils.MvList x, H264Const.PartPred[] pp, Picture mb, int[] blocks)
	{
		if (sh.directSpatialMvPredFlag)
		{
			predictBSpatialDirect(refs, mbX, mbY, lAvb, tAvb, tlAvb, trAvb, x, pp, mb, blocks);
		}
		else
		{
			predictBTemporalDirect(refs, mbX, mbY, lAvb, tAvb, tlAvb, trAvb, x, pp, mb, blocks);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		91,
		97,
		77,
		127,
		10,
		127,
		14,
		127,
		18,
		117,
		149,
		118,
		150,
		113,
		110,
		104,
		107,
		107,
		104,
		12,
		201,
		139,
		105,
		105,
		159,
		16,
		159,
		16,
		127,
		27,
		45,
		134,
		byte.MaxValue,
		15,
		47,
		236,
		83,
		130,
		119,
		119,
		119,
		151,
		104,
		127,
		6,
		110,
		104,
		141,
		118,
		107,
		110,
		104,
		159,
		0,
		103,
		135,
		127,
		42,
		63,
		42,
		166,
		107,
		139,
		102,
		127,
		4,
		53,
		134,
		102,
		127,
		4,
		53,
		230,
		47,
		236,
		84,
		102,
		109,
		127,
		0,
		141,
		103,
		135,
		127,
		28,
		63,
		35,
		166,
		107,
		139,
		102,
		127,
		4,
		53,
		134,
		102,
		127,
		4,
		53,
		166,
		127,
		33,
		127,
		4,
		13,
		230,
		17,
		236,
		115
	})]
	private void predictBSpatialDirect(org.jcodec.codecs.h264.io.model.Frame[][] refs, int mbX, int mbY, bool lAvb, bool tAvb, bool tlAvb, bool trAvb, H264Utils.MvList x, H264Const.PartPred[] pp, Picture mb, int[] blocks8x8)
	{
		int a0 = s.mvLeft.getMv(0, 0);
		int a1 = s.mvLeft.getMv(0, 1);
		int b0 = s.mvTop.getMv(mbX << 2, 0);
		int b1 = s.mvTop.getMv(mbX << 2, 1);
		int c0 = s.mvTop.getMv((mbX << 2) + 4, 0);
		int c1 = s.mvTop.getMv((mbX << 2) + 4, 1);
		int d0 = s.mvTopLeft.getMv(0, 0);
		int d1 = s.mvTopLeft.getMv(0, 1);
		int refIdxL0 = calcRef(a0, b0, c0, d0, lAvb, tAvb, tlAvb, trAvb, mbX);
		int refIdxL1 = calcRef(a1, b1, c1, d1, lAvb, tAvb, tlAvb, trAvb, mbX);
		if (refIdxL0 < 0 && refIdxL1 < 0)
		{
			for (int j = 0; j < (nint)blocks8x8.LongLength; j++)
			{
				int blk8x9 = blocks8x8[j];
				int[] js2 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x9];
				for (int l = 0; l < (nint)js2.LongLength; l++)
				{
					int blk4x5 = js2[l];
					x.setPair(blk4x5, 0, 0);
				}
				pp[blk8x9] = H264Const.PartPred.___003C_003EBi;
				int blkOffX = (blk8x9 & 1) << 5;
				int blkOffY = blk8x9 >> 1 << 5;
				interpolator.getBlockLuma(refs[0][0], mbb[0], H264Const.___003C_003EBLK_8x8_MB_OFF_LUMA[blk8x9], (mbX << 6) + blkOffX, (mbY << 6) + blkOffY, 8, 8);
				interpolator.getBlockLuma(refs[1][0], mbb[1], H264Const.___003C_003EBLK_8x8_MB_OFF_LUMA[blk8x9], (mbX << 6) + blkOffX, (mbY << 6) + blkOffY, 8, 8);
				PredictionMerger.mergePrediction(sh, 0, 0, H264Const.PartPred.___003C_003EBi, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), H264Const.___003C_003EBLK_8x8_MB_OFF_LUMA[blk8x9], 16, 8, 8, mb.getPlaneData(0), refs, poc);
				MBlockDecoderUtils.debugPrint("DIRECT_8x8 [%d, %d]: (0,0,0), (0,0,0)", Integer.valueOf(blk8x9 & 2), Integer.valueOf((blk8x9 << 1) & 2));
			}
			return;
		}
		int mvX0 = MBlockDecoderUtils.calcMVPredictionMedian(a0, b0, c0, d0, lAvb, tAvb, trAvb, tlAvb, refIdxL0, 0);
		int mvY0 = MBlockDecoderUtils.calcMVPredictionMedian(a0, b0, c0, d0, lAvb, tAvb, trAvb, tlAvb, refIdxL0, 1);
		int mvX1 = MBlockDecoderUtils.calcMVPredictionMedian(a1, b1, c1, d1, lAvb, tAvb, trAvb, tlAvb, refIdxL1, 0);
		int mvY1 = MBlockDecoderUtils.calcMVPredictionMedian(a1, b1, c1, d1, lAvb, tAvb, trAvb, tlAvb, refIdxL1, 1);
		org.jcodec.codecs.h264.io.model.Frame col = refs[1][0];
		H264Const.PartPred partPred = ((refIdxL0 >= 0 && refIdxL1 >= 0) ? H264Const.PartPred.___003C_003EBi : ((refIdxL0 < 0) ? H264Const.PartPred.___003C_003EL1 : H264Const.PartPred.___003C_003EL0));
		for (int i = 0; i < (nint)blocks8x8.LongLength; i++)
		{
			int blk8x8 = blocks8x8[i];
			int blk4x4_0 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][0];
			if (!sh.sps.direct8x8InferenceFlag)
			{
				int[] js = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8];
				for (int k = 0; k < (nint)js.LongLength; k++)
				{
					int blk4x4 = js[k];
					pred4x4(mbX, mbY, x, pp, refIdxL0, refIdxL1, mvX0, mvY0, mvX1, mvY1, col, partPred, blk4x4);
					int blkIndX2 = blk4x4 & 3;
					int blkIndY2 = blk4x4 >> 2;
					MBlockDecoderUtils.debugPrint(new StringBuilder().append("DIRECT_4x4 [%d, %d]: (%d,%d,%d), (%d,%d,").append(refIdxL1).append(")")
						.toString(), Integer.valueOf(blkIndY2), Integer.valueOf(blkIndX2), Integer.valueOf(x.mv0X(blk4x4)), Integer.valueOf(x.mv0Y(blk4x4)), Integer.valueOf(refIdxL0), Integer.valueOf(x.mv1X(blk4x4)), Integer.valueOf(x.mv1Y(blk4x4)));
					int blkPredX2 = (mbX << 6) + (blkIndX2 << 4);
					int blkPredY2 = (mbY << 6) + (blkIndY2 << 4);
					if (refIdxL0 >= 0)
					{
						interpolator.getBlockLuma(refs[0][refIdxL0], mbb[0], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4], blkPredX2 + x.mv0X(blk4x4), blkPredY2 + x.mv0Y(blk4x4), 4, 4);
					}
					if (refIdxL1 >= 0)
					{
						interpolator.getBlockLuma(refs[1][refIdxL1], mbb[1], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4], blkPredX2 + x.mv1X(blk4x4), blkPredY2 + x.mv1Y(blk4x4), 4, 4);
					}
				}
			}
			else
			{
				int blk4x4Pred = H264Const.___003C_003EBLK_INV_MAP[blk8x8 * 5];
				pred4x4(mbX, mbY, x, pp, refIdxL0, refIdxL1, mvX0, mvY0, mvX1, mvY1, col, partPred, blk4x4Pred);
				propagatePred(x, blk8x8, blk4x4Pred);
				int blkIndX = blk4x4_0 & 3;
				int blkIndY = blk4x4_0 >> 2;
				MBlockDecoderUtils.debugPrint("DIRECT_8x8 [%d, %d]: (%d,%d,%d), (%d,%d,%d)", Integer.valueOf(blkIndY), Integer.valueOf(blkIndX), Integer.valueOf(x.mv0X(blk4x4_0)), Integer.valueOf(x.mv0Y(blk4x4_0)), Integer.valueOf(refIdxL0), Integer.valueOf(x.mv1X(blk4x4_0)), Integer.valueOf(x.mv1Y(blk4x4_0)), Integer.valueOf(refIdxL1));
				int blkPredX = (mbX << 6) + (blkIndX << 4);
				int blkPredY = (mbY << 6) + (blkIndY << 4);
				if (refIdxL0 >= 0)
				{
					interpolator.getBlockLuma(refs[0][refIdxL0], mbb[0], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4_0], blkPredX + x.mv0X(blk4x4_0), blkPredY + x.mv0Y(blk4x4_0), 8, 8);
				}
				if (refIdxL1 >= 0)
				{
					interpolator.getBlockLuma(refs[1][refIdxL1], mbb[1], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4_0], blkPredX + x.mv1X(blk4x4_0), blkPredY + x.mv1Y(blk4x4_0), 8, 8);
				}
			}
			PredictionMerger.mergePrediction(sh, x.mv0R(blk4x4_0), x.mv1R(blk4x4_0), (refIdxL0 < 0) ? H264Const.PartPred.___003C_003EL1 : ((refIdxL1 < 0) ? H264Const.PartPred.___003C_003EL0 : H264Const.PartPred.___003C_003EBi), 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4_0], 16, 8, 8, mb.getPlaneData(0), refs, poc);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 98, 108, 102, 107, 138, 118, 105, 109,
		103, 142, 103, 135, 127, 28, 63, 49, 166, 107,
		139, 127, 11, 53, 134, 127, 3, 53, 230, 49,
		236, 82, 102, 108, 110, 140, 102, 134, 127, 26,
		63, 45, 166, 107, 139, 127, 8, 52, 134, 127,
		1, 52, 166, 127, 40, 45, 230, 20, 234, 111
	})]
	private void predictBTemporalDirect(org.jcodec.codecs.h264.io.model.Frame[][] refs, int mbX, int mbY, bool lAvb, bool tAvb, bool tlAvb, bool trAvb, H264Utils.MvList x, H264Const.PartPred[] pp, Picture mb, int[] blocks8x8)
	{
		for (int i = 0; i < (nint)blocks8x8.LongLength; i++)
		{
			int blk8x8 = blocks8x8[i];
			int blk4x4_0 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][0];
			pp[blk8x8] = H264Const.PartPred.___003C_003EBi;
			if (!sh.sps.direct8x8InferenceFlag)
			{
				int[] js = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8];
				for (int j = 0; j < (nint)js.LongLength; j++)
				{
					int blk4x4 = js[j];
					predTemp4x4(refs, mbX, mbY, x, blk4x4);
					int blkIndX2 = blk4x4 & 3;
					int blkIndY2 = blk4x4 >> 2;
					MBlockDecoderUtils.debugPrint("DIRECT_4x4 [%d, %d]: (%d,%d,%d), (%d,%d,%d)", Integer.valueOf(blkIndY2), Integer.valueOf(blkIndX2), Integer.valueOf(x.mv0X(blk4x4)), Integer.valueOf(x.mv0Y(blk4x4)), Integer.valueOf(x.mv0R(blk4x4)), Integer.valueOf(x.mv1X(blk4x4)), Integer.valueOf(x.mv1Y(blk4x4)), Integer.valueOf(x.mv1R(blk4x4)));
					int blkPredX2 = (mbX << 6) + (blkIndX2 << 4);
					int blkPredY2 = (mbY << 6) + (blkIndY2 << 4);
					interpolator.getBlockLuma(refs[0][x.mv0R(blk4x4)], mbb[0], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4], blkPredX2 + x.mv0X(blk4x4), blkPredY2 + x.mv0Y(blk4x4), 4, 4);
					interpolator.getBlockLuma(refs[1][0], mbb[1], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4], blkPredX2 + x.mv1X(blk4x4), blkPredY2 + x.mv1Y(blk4x4), 4, 4);
				}
			}
			else
			{
				int blk4x4Pred = H264Const.___003C_003EBLK_INV_MAP[blk8x8 * 5];
				predTemp4x4(refs, mbX, mbY, x, blk4x4Pred);
				propagatePred(x, blk8x8, blk4x4Pred);
				int blkIndX = blk4x4_0 & 3;
				int blkIndY = blk4x4_0 >> 2;
				MBlockDecoderUtils.debugPrint("DIRECT_8x8 [%d, %d]: (%d,%d,%d), (%d,%d)", Integer.valueOf(blkIndY), Integer.valueOf(blkIndX), Integer.valueOf(x.mv0X(blk4x4_0)), Integer.valueOf(x.mv0Y(blk4x4_0)), Integer.valueOf(x.mv0R(blk4x4_0)), Integer.valueOf(x.mv1X(blk4x4_0)), Integer.valueOf(x.mv1Y(blk4x4_0)), Integer.valueOf(x.mv1R(blk4x4_0)));
				int blkPredX = (mbX << 6) + (blkIndX << 4);
				int blkPredY = (mbY << 6) + (blkIndY << 4);
				interpolator.getBlockLuma(refs[0][x.mv0R(blk4x4_0)], mbb[0], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4_0], blkPredX + x.mv0X(blk4x4_0), blkPredY + x.mv0Y(blk4x4_0), 8, 8);
				interpolator.getBlockLuma(refs[1][0], mbb[1], H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4_0], blkPredX + x.mv1X(blk4x4_0), blkPredY + x.mv1Y(blk4x4_0), 8, 8);
			}
			PredictionMerger.mergePrediction(sh, x.mv0R(blk4x4_0), x.mv1R(blk4x4_0), H264Const.PartPred.___003C_003EBi, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), H264Const.___003C_003EBLK_4x4_MB_OFF_LUMA[blk4x4_0], 16, 8, 8, mb.getPlaneData(0), refs, poc);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 104, 162, 148, 103, 102, 134, 104, 136, 179,
		107, 115, 107, 100, 138, 121, 176, 121, 174, 122,
		110, 159, 6, 122, 125, 156, 127, 21, 63, 6,
		168
	})]
	private void predTemp4x4(org.jcodec.codecs.h264.io.model.Frame[][] refs, int mbX, int mbY, H264Utils.MvList x, int blk4x4)
	{
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		org.jcodec.codecs.h264.io.model.Frame picCol = refs[1][0];
		int blkIndX = blk4x4 & 3;
		int blkIndY = blk4x4 >> 2;
		int blkPosX = (mbX << 2) + blkIndX;
		int blkPosY = (mbY << 2) + blkIndY;
		int mvCol = picCol.getMvs().getMv(blkPosX, blkPosY, 0);
		int refIdxL0;
		org.jcodec.codecs.h264.io.model.Frame refL0;
		if (H264Utils.Mv.mvRef(mvCol) == -1)
		{
			mvCol = picCol.getMvs().getMv(blkPosX, blkPosY, 1);
			if (H264Utils.Mv.mvRef(mvCol) == -1)
			{
				refIdxL0 = 0;
				refL0 = refs[0][0];
			}
			else
			{
				refL0 = picCol.getRefsUsed()[mbY * mbWidth + mbX][1][H264Utils.Mv.mvRef(mvCol)];
				refIdxL0 = findPic(refs[0], refL0);
			}
		}
		else
		{
			refL0 = picCol.getRefsUsed()[mbY * mbWidth + mbX][0][H264Utils.Mv.mvRef(mvCol)];
			refIdxL0 = findPic(refs[0], refL0);
		}
		int td = MathUtil.clip(picCol.getPOC() - refL0.getPOC(), -128, 127);
		if (!refL0.isShortTerm() || td == 0)
		{
			x.setPair(blk4x4, H264Utils.Mv.packMv(H264Utils.Mv.mvX(mvCol), H264Utils.Mv.mvY(mvCol), refIdxL0), 0);
			return;
		}
		int tb = MathUtil.clip(poc - refL0.getPOC(), -128, 127);
		int num = 16384 + Math.abs(td / 2);
		int tx = ((td != -1) ? (num / td) : (-num));
		int dsf = MathUtil.clip(tb * tx + 32 >> 6, -1024, 1023);
		x.setPair(blk4x4, H264Utils.Mv.packMv(dsf * H264Utils.Mv.mvX(mvCol) + 128 >> 8, dsf * H264Utils.Mv.mvY(mvCol) + 128 >> 8, refIdxL0), H264Utils.Mv.packMv(x.mv0X(blk4x4) - H264Utils.Mv.mvX(mvCol), x.mv0Y(blk4x4) - H264Utils.Mv.mvY(mvCol), 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 67, 130, 107, 107, 107, 107, 106, 106, 106,
		108
	})]
	private void propagatePred(H264Utils.MvList x, int blk8x8, int blk4x4Pred)
	{
		int b0 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][0];
		int b1 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][1];
		int b2 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][2];
		int b3 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][3];
		x.copyPair(b0, x, blk4x4Pred);
		x.copyPair(b1, x, blk4x4Pred);
		x.copyPair(b2, x, blk4x4Pred);
		x.copyPair(b3, x, blk4x4Pred);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 162, 104, 103, 3, 167, 107 })]
	private int findPic(org.jcodec.codecs.h264.io.model.Frame[] frames, org.jcodec.codecs.h264.io.model.Frame refL0)
	{
		for (int i = 0; i < (nint)frames.LongLength; i++)
		{
			if (frames[i] == refL0)
			{
				return i;
			}
		}
		Logger.error("RefPicList0 shall contain refPicCol");
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 68, 129, 77 })]
	private int calcRef(int a0, int b0, int c0, int d0, bool lAvb, bool tAvb, bool tlAvb, bool trAvb, int mbX)
	{
		int result = minPos(minPos((!lAvb) ? (-1) : H264Utils.Mv.mvRef(a0), (!tAvb) ? (-1) : H264Utils.Mv.mvRef(b0)), trAvb ? H264Utils.Mv.mvRef(c0) : ((!tlAvb) ? (-1) : H264Utils.Mv.mvRef(d0)));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 64, 130, 102, 134, 103, 135, 114, 107, 146,
		127, 6, 149, 119, 106, 142, 106, 142, 141, 110
	})]
	private void pred4x4(int mbX, int mbY, H264Utils.MvList x, H264Const.PartPred[] pp, int refL0, int refL1, int mvX0, int mvY0, int mvX1, int mvY1, org.jcodec.codecs.h264.io.model.Frame col, H264Const.PartPred partPred, int blk4x4)
	{
		int blkIndX = blk4x4 & 3;
		int blkIndY = blk4x4 >> 2;
		int blkPosX = (mbX << 2) + blkIndX;
		int blkPosY = (mbY << 2) + blkIndY;
		int mvCol = col.getMvs().getMv(blkPosX, blkPosY, 0);
		if (H264Utils.Mv.mvRef(mvCol) == -1)
		{
			mvCol = col.getMvs().getMv(blkPosX, blkPosY, 1);
		}
		int colZero = ((col.isShortTerm() && H264Utils.Mv.mvRef(mvCol) == 0 && MathUtil.abs(H264Utils.Mv.mvX(mvCol)) >> 1 == 0 && MathUtil.abs(H264Utils.Mv.mvY(mvCol)) >> 1 == 0) ? 1 : 0);
		int x2 = H264Utils.Mv.packMv(0, 0, refL0);
		int x3 = H264Utils.Mv.packMv(0, 0, refL1);
		if (refL0 > 0 || colZero == 0)
		{
			x2 = H264Utils.Mv.packMv(mvX0, mvY0, refL0);
		}
		if (refL1 > 0 || colZero == 0)
		{
			x3 = H264Utils.Mv.packMv(mvX1, mvY1, refL1);
		}
		x.setPair(blk4x4, x2, x3);
		pp[H264Const.___003C_003EBLK_8x8_IND[blk4x4]] = partPred;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(340)]
	private int minPos(int a, int b)
	{
		int result = ((a < 0 || b < 0) ? Math.max(a, b) : Math.min(a, b));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 111, 104 })]
	public MBlockDecoderBDirect(Mapper mapper, SliceHeader sh, DeblockerInput di, int poc, DecoderState decoderState)
		: base(sh, di, poc, decoderState)
	{
		this.mapper = mapper;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 115, 115, 115, 115, 116, 116, 148,
		159, 3, 124, 156, 115, 159, 14, 156, 140, 115,
		148, 127, 1, 159, 1, 144, 115, 147, 191, 22,
		142, 117, 117
	})]
	public virtual void decode(MBlock mBlock, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] references)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int lAvb = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int tAvb = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int mbAddr = mapper.getAddress(mBlock.mbIdx);
		int tlAvb = (mapper.topLeftAvailable(mBlock.mbIdx) ? 1 : 0);
		int trAvb = (mapper.topRightAvailable(mBlock.mbIdx) ? 1 : 0);
		predictBDirect(references, mbX, mbY, (byte)lAvb != 0, (byte)tAvb != 0, (byte)tlAvb != 0, (byte)trAvb != 0, mBlock.x, mBlock.partPreds, mb, H264Const.___003C_003EidentityMapping4);
		predictChromaInter(references, mBlock.x, mbX << 3, mbY << 3, 1, mb, mBlock.partPreds);
		predictChromaInter(references, mBlock.x, mbX << 3, mbY << 3, 2, mb, mBlock.partPreds);
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			DecoderState decoderState = s;
			int num = s.qp + mBlock.mbQPDelta + 52;
			decoderState.qp = ((52 != -1) ? (num % 52) : 0);
		}
		di.mbQps[0][mbAddr] = s.qp;
		residualLuma(mBlock, (byte)lAvb != 0, (byte)tAvb != 0, mbX, mbY);
		MBlockDecoderUtils.savePrediction8x8(s, mbX, mBlock.x);
		MBlockDecoderUtils.saveMvs(di, mBlock.x, mbX, mbY);
		int qp1 = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[0]);
		int qp2 = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[1]);
		decodeChromaResidual(mBlock, (byte)lAvb != 0, (byte)tAvb != 0, mbX, mbY, qp1, qp2);
		di.mbQps[1][mbAddr] = qp1;
		di.mbQps[2][mbAddr] = qp2;
		MBlockDecoderUtils.mergeResidual(mb, mBlock.ac, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_BLOCK_4x4_LUT : H264Const.___003C_003ECOMP_BLOCK_8x8_LUT, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_POS_4x4_LUT : H264Const.___003C_003ECOMP_POS_8x8_LUT);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		di.mbTypes[mbAddr] = mBlock.curMbType;
		di.tr8x8Used[mbAddr] = mBlock.transform8x8Used;
	}
}
