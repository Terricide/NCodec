using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderInter8x8 : MBlockDecoderBase
{
	private Mapper mapper;

	private MBlockDecoderBDirect bDirectDecoder;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 65, 77, 127, 29, 127, 34, 31, 25,
		230, 69, 127, 16, 127, 39, 31, 21, 230, 69,
		127, 11, 127, 33, 31, 10, 230, 69, 127, 5,
		63, 54, 198, 137, 109, 127, 7, 38, 230, 61,
		233, 71, 112, 111
	})]
	private void predict8x8P(MBlock mBlock, Picture[] references, Picture mb, bool ref0, int mbX, int mbY, bool leftAvailable, bool topAvailable, bool tlAvailable, bool topRightAvailable, H264Utils.MvList x, H264Const.PartPred[] pp)
	{
		decodeSubMb8x8(mBlock, 0, mBlock.___003C_003Epb8x8.subMbTypes[0], references, mbX << 6, mbY << 6, s.mvTopLeft.getMv(0, 0), s.mvTop.getMv(mbX << 2, 0), s.mvTop.getMv((mbX << 2) + 1, 0), s.mvTop.getMv((mbX << 2) + 2, 0), s.mvLeft.getMv(0, 0), s.mvLeft.getMv(1, 0), tlAvailable, topAvailable, topAvailable, leftAvailable, mBlock.x, 0, 1, 4, 5, mBlock.___003C_003Epb8x8.refIdx[0][0], mb, 0, 0);
		decodeSubMb8x8(mBlock, 1, mBlock.___003C_003Epb8x8.subMbTypes[1], references, (mbX << 6) + 32, mbY << 6, s.mvTop.getMv((mbX << 2) + 1, 0), s.mvTop.getMv((mbX << 2) + 2, 0), s.mvTop.getMv((mbX << 2) + 3, 0), s.mvTop.getMv((mbX << 2) + 4, 0), x.getMv(1, 0), x.getMv(5, 0), topAvailable, topAvailable, topRightAvailable, lAvb: true, x, 2, 3, 6, 7, mBlock.___003C_003Epb8x8.refIdx[0][1], mb, 8, 0);
		decodeSubMb8x8(mBlock, 2, mBlock.___003C_003Epb8x8.subMbTypes[2], references, mbX << 6, (mbY << 6) + 32, s.mvLeft.getMv(1, 0), x.getMv(4, 0), x.getMv(5, 0), x.getMv(6, 0), s.mvLeft.getMv(2, 0), s.mvLeft.getMv(3, 0), leftAvailable, tAvb: true, trAvb: true, leftAvailable, x, 8, 9, 12, 13, mBlock.___003C_003Epb8x8.refIdx[0][2], mb, 128, 0);
		decodeSubMb8x8(mBlock, 3, mBlock.___003C_003Epb8x8.subMbTypes[3], references, (mbX << 6) + 32, (mbY << 6) + 32, x.getMv(5, 0), x.getMv(6, 0), x.getMv(7, 0), MBlockDecoderUtils.___003C_003ENULL_VECTOR, x.getMv(9, 0), x.getMv(13, 0), tlAvb: true, tAvb: true, trAvb: false, lAvb: true, x, 10, 11, 14, 15, mBlock.___003C_003Epb8x8.refIdx[0][3], mb, 136, 0);
		for (int i = 0; i < 4; i++)
		{
			int blk4x4 = H264Const.___003C_003EBLK8x8_BLOCKS[i][0];
			PredictionMerger.weightPrediction(sh, x.mv0R(blk4x4), 0, mb.getPlaneData(0), H264Const.___003C_003EBLK_8x8_MB_OFF_LUMA[i], 16, 8, 8, mb.getPlaneData(0));
		}
		MBlockDecoderUtils.savePrediction8x8(s, mbX, x);
		Arrays.fill(pp, H264Const.PartPred.___003C_003EL0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		108,
		129,
		77,
		105,
		58,
		201,
		105,
		109,
		31,
		3,
		233,
		70,
		108,
		127,
		1,
		127,
		18,
		127,
		40,
		31,
		51,
		230,
		70,
		127,
		1,
		127,
		26,
		127,
		18,
		127,
		14,
		byte.MaxValue,
		13,
		61,
		230,
		71,
		127,
		1,
		127,
		21,
		127,
		19,
		31,
		39,
		230,
		70,
		127,
		1,
		127,
		15,
		127,
		6,
		31,
		32,
		230,
		40,
		236,
		95,
		108,
		109,
		127,
		24,
		127,
		3,
		13,
		6,
		236,
		71,
		114
	})]
	private void predict8x8B(MBlock mBlock, org.jcodec.codecs.h264.io.model.Frame[][] refs, Picture mb, bool ref0, int mbX, int mbY, bool leftAvailable, bool topAvailable, bool tlAvailable, bool topRightAvailable, H264Utils.MvList x, H264Const.PartPred[] p)
	{
		for (int k = 0; k < 4; k++)
		{
			p[k] = H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[k]];
		}
		for (int j = 0; j < 4; j++)
		{
			if (p[j] == H264Const.PartPred.___003C_003EDirect)
			{
				bDirectDecoder.predictBDirect(refs, mbX, mbY, leftAvailable, topAvailable, tlAvailable, topRightAvailable, x, p, mb, H264Const.___003C_003EARRAY[j]);
			}
		}
		for (int list = 0; list < 2; list++)
		{
			if (H264Const.usesList(H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[0]], list))
			{
				decodeSubMb8x8(mBlock, 0, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[0]], refs[list], mbX << 6, mbY << 6, s.mvTopLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 1, list), s.mvTop.getMv((mbX << 2) + 2, list), s.mvLeft.getMv(0, list), s.mvLeft.getMv(1, list), tlAvailable, topAvailable, topAvailable, leftAvailable, x, 0, 1, 4, 5, mBlock.___003C_003Epb8x8.refIdx[list][0], mbb[list], 0, list);
			}
			if (H264Const.usesList(H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[1]], list))
			{
				decodeSubMb8x8(mBlock, 1, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[1]], refs[list], (mbX << 6) + 32, mbY << 6, s.mvTop.getMv((mbX << 2) + 1, list), s.mvTop.getMv((mbX << 2) + 2, list), s.mvTop.getMv((mbX << 2) + 3, list), s.mvTop.getMv((mbX << 2) + 4, list), x.getMv(1, list), x.getMv(5, list), topAvailable, topAvailable, topRightAvailable, lAvb: true, x, 2, 3, 6, 7, mBlock.___003C_003Epb8x8.refIdx[list][1], mbb[list], 8, list);
			}
			if (H264Const.usesList(H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[2]], list))
			{
				decodeSubMb8x8(mBlock, 2, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[2]], refs[list], mbX << 6, (mbY << 6) + 32, s.mvLeft.getMv(1, list), x.getMv(4, list), x.getMv(5, list), x.getMv(6, list), s.mvLeft.getMv(2, list), s.mvLeft.getMv(3, list), leftAvailable, tAvb: true, trAvb: true, leftAvailable, x, 8, 9, 12, 13, mBlock.___003C_003Epb8x8.refIdx[list][2], mbb[list], 128, list);
			}
			if (H264Const.usesList(H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[3]], list))
			{
				decodeSubMb8x8(mBlock, 3, H264Const.___003C_003EbSubMbTypes[mBlock.___003C_003Epb8x8.subMbTypes[3]], refs[list], (mbX << 6) + 32, (mbY << 6) + 32, x.getMv(5, list), x.getMv(6, list), x.getMv(7, list), MBlockDecoderUtils.___003C_003ENULL_VECTOR, x.getMv(9, list), x.getMv(13, list), tlAvb: true, tAvb: true, trAvb: false, lAvb: true, x, 10, 11, 14, 15, mBlock.___003C_003Epb8x8.refIdx[list][3], mbb[list], 136, list);
			}
		}
		for (int i = 0; i < 4; i++)
		{
			int blk4x4 = H264Const.___003C_003EBLK8x8_BLOCKS[i][0];
			PredictionMerger.mergePrediction(sh, x.mv0R(blk4x4), x.mv1R(blk4x4), H264Const.___003C_003EbPartPredModes[mBlock.___003C_003Epb8x8.subMbTypes[i]], 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), H264Const.___003C_003EBLK_8x8_MB_OFF_LUMA[i], 16, 8, 8, mb.getPlaneData(0), refs, poc);
		}
		MBlockDecoderUtils.savePrediction8x8(s, mbX, x);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 97, 77, 156, 159, 18, 134, 159, 16,
		131, 159, 16, 131, 191, 16
	})]
	private void decodeSubMb8x8(MBlock mBlock, int partNo, int subMbType, Picture[] references, int offX, int offY, int tl, int t0, int t1, int tr, int l0, int l1, bool tlAvb, bool tAvb, bool trAvb, bool lAvb, H264Utils.MvList x, int i00, int i01, int i10, int i11, int refIdx, Picture mb, int off, int list)
	{
		switch (subMbType)
		{
		case 3:
			decodeSub4x4(mBlock, partNo, references, offX, offY, tl, t0, t1, tr, l0, l1, tlAvb, tAvb, trAvb, lAvb, x, i00, i01, i10, i11, refIdx, mb, off, list);
			break;
		case 2:
			decodeSub4x8(mBlock, partNo, references, offX, offY, tl, t0, t1, tr, l0, tlAvb, tAvb, trAvb, lAvb, x, i00, i01, i10, i11, refIdx, mb, off, list);
			break;
		case 1:
			decodeSub8x4(mBlock, partNo, references, offX, offY, tl, t0, tr, l0, l1, tlAvb, tAvb, trAvb, lAvb, x, i00, i01, i10, i11, refIdx, mb, off, list);
			break;
		case 0:
			decodeSub8x8(mBlock, partNo, references, offX, offY, tl, t0, tr, l0, tlAvb, tAvb, trAvb, lAvb, x, i00, i01, i10, i11, refIdx, mb, off, list);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 69, 161, 77, 119, 151, 127, 17, 110, 127,
		46, 63, 16, 166, 119, 151, 127, 17, 142, 127,
		46, 63, 16, 166, 119, 151, 127, 17, 142, 127,
		46, 63, 16, 166, 122, 154, 127, 17, 142, 127,
		46, 63, 16, 166, 127, 11, 127, 16, 159, 24,
		127, 13, 44, 136
	})]
	private void decodeSub4x4(MBlock mBlock, int partNo, Picture[] references, int offX, int offY, int tl, int t0, int t1, int tr, int l0, int l1, bool tlAvb, bool tAvb, bool trAvb, bool lAvb, H264Utils.MvList x, int i00, int i01, int i10, int i11, int refIdx, Picture mb, int off, int list)
	{
		int mvpX1 = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, t1, tl, lAvb, tAvb, tAvb, tlAvb, refIdx, 0);
		int mvpY1 = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, t1, tl, lAvb, tAvb, tAvb, tlAvb, refIdx, 1);
		int mv1 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX1[list][partNo] + mvpX1, mBlock.___003C_003Epb8x8.mvdY1[list][partNo] + mvpY1, refIdx);
		x.setMv(i00, list, mv1);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX1), Integer.valueOf(mvpY1), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX1[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY1[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv1)), Integer.valueOf(H264Utils.Mv.mvY(mv1)), Integer.valueOf(refIdx));
		int mvpX2 = MBlockDecoderUtils.calcMVPredictionMedian(mv1, t1, tr, t0, aAvb: true, tAvb, trAvb, tAvb, refIdx, 0);
		int mvpY2 = MBlockDecoderUtils.calcMVPredictionMedian(mv1, t1, tr, t0, aAvb: true, tAvb, trAvb, tAvb, refIdx, 1);
		int mv2 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX2[list][partNo] + mvpX2, mBlock.___003C_003Epb8x8.mvdY2[list][partNo] + mvpY2, refIdx);
		x.setMv(i01, list, mv2);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX2), Integer.valueOf(mvpY2), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX2[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY2[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv2)), Integer.valueOf(H264Utils.Mv.mvY(mv2)), Integer.valueOf(refIdx));
		int mvpX3 = MBlockDecoderUtils.calcMVPredictionMedian(l1, mv1, mv2, l0, lAvb, bAvb: true, cAvb: true, lAvb, refIdx, 0);
		int mvpY3 = MBlockDecoderUtils.calcMVPredictionMedian(l1, mv1, mv2, l0, lAvb, bAvb: true, cAvb: true, lAvb, refIdx, 1);
		int mv3 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX3[list][partNo] + mvpX3, mBlock.___003C_003Epb8x8.mvdY3[list][partNo] + mvpY3, refIdx);
		x.setMv(i10, list, mv3);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX3), Integer.valueOf(mvpY3), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX3[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY3[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv3)), Integer.valueOf(H264Utils.Mv.mvY(mv3)), Integer.valueOf(refIdx));
		int mvpX4 = MBlockDecoderUtils.calcMVPredictionMedian(mv3, mv2, MBlockDecoderUtils.___003C_003ENULL_VECTOR, mv1, aAvb: true, bAvb: true, cAvb: false, dAvb: true, refIdx, 0);
		int mvpY4 = MBlockDecoderUtils.calcMVPredictionMedian(mv3, mv2, MBlockDecoderUtils.___003C_003ENULL_VECTOR, mv1, aAvb: true, bAvb: true, cAvb: false, dAvb: true, refIdx, 1);
		int mv4 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX4[list][partNo] + mvpX4, mBlock.___003C_003Epb8x8.mvdY4[list][partNo] + mvpY4, refIdx);
		x.setMv(i11, list, mv4);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX4), Integer.valueOf(mvpY4), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX4[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY4[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv4)), Integer.valueOf(H264Utils.Mv.mvY(mv4)), Integer.valueOf(refIdx));
		interpolator.getBlockLuma(references[refIdx], mb, off, offX + H264Utils.Mv.mvX(mv1), offY + H264Utils.Mv.mvY(mv1), 4, 4);
		interpolator.getBlockLuma(references[refIdx], mb, off + 4, offX + H264Utils.Mv.mvX(mv2) + 16, offY + H264Utils.Mv.mvY(mv2), 4, 4);
		interpolator.getBlockLuma(references[refIdx], mb, off + mb.getWidth() * 4, offX + H264Utils.Mv.mvX(mv3), offY + H264Utils.Mv.mvY(mv3) + 16, 4, 4);
		interpolator.getBlockLuma(references[refIdx], mb, off + mb.getWidth() * 4 + 4, offX + H264Utils.Mv.mvX(mv4) + 16, offY + H264Utils.Mv.mvY(mv4) + 16, 4, 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 76, 129, 77, 119, 151, 127, 17, 110, 142,
		127, 46, 63, 16, 166, 119, 151, 159, 17, 110,
		142, 127, 46, 63, 16, 166, 127, 11, 127, 18
	})]
	private void decodeSub4x8(MBlock mBlock, int partNo, Picture[] references, int offX, int offY, int tl, int t0, int t1, int tr, int l0, bool tlAvb, bool tAvb, bool trAvb, bool lAvb, H264Utils.MvList x, int i00, int i01, int i10, int i11, int refIdx, Picture mb, int off, int list)
	{
		int mvpX1 = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, t1, tl, lAvb, tAvb, tAvb, tlAvb, refIdx, 0);
		int mvpY1 = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, t1, tl, lAvb, tAvb, tAvb, tlAvb, refIdx, 1);
		int mv1 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX1[list][partNo] + mvpX1, mBlock.___003C_003Epb8x8.mvdY1[list][partNo] + mvpY1, refIdx);
		x.setMv(i00, list, mv1);
		x.setMv(i10, list, mv1);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX1), Integer.valueOf(mvpY1), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX1[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY1[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv1)), Integer.valueOf(H264Utils.Mv.mvY(mv1)), Integer.valueOf(refIdx));
		int mvpX2 = MBlockDecoderUtils.calcMVPredictionMedian(mv1, t1, tr, t0, aAvb: true, tAvb, trAvb, tAvb, refIdx, 0);
		int mvpY2 = MBlockDecoderUtils.calcMVPredictionMedian(mv1, t1, tr, t0, aAvb: true, tAvb, trAvb, tAvb, refIdx, 1);
		int mv2 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX2[list][partNo] + mvpX2, mBlock.___003C_003Epb8x8.mvdY2[list][partNo] + mvpY2, refIdx);
		x.setMv(i01, list, mv2);
		x.setMv(i11, list, mv2);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX2), Integer.valueOf(mvpY2), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX2[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY2[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv2)), Integer.valueOf(H264Utils.Mv.mvY(mv2)), Integer.valueOf(refIdx));
		interpolator.getBlockLuma(references[refIdx], mb, off, offX + H264Utils.Mv.mvX(mv1), offY + H264Utils.Mv.mvY(mv1), 4, 8);
		interpolator.getBlockLuma(references[refIdx], mb, off + 4, offX + H264Utils.Mv.mvX(mv2) + 16, offY + H264Utils.Mv.mvY(mv2), 4, 8);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 65, 77, 119, 183, 127, 17, 110, 142,
		127, 46, 63, 16, 166, 122, 154, 127, 17, 110,
		142, 127, 46, 63, 16, 166, 127, 11, 127, 8,
		44, 136
	})]
	private void decodeSub8x4(MBlock mBlock, int partNo, Picture[] references, int offX, int offY, int tl, int t0, int tr, int l0, int l1, bool tlAvb, bool tAvb, bool trAvb, bool lAvb, H264Utils.MvList x, int i00, int i01, int i10, int i11, int refIdx, Picture mb, int off, int list)
	{
		int mvpX1 = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, tr, tl, lAvb, tAvb, trAvb, tlAvb, refIdx, 0);
		int mvpY1 = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, tr, tl, lAvb, tAvb, trAvb, tlAvb, refIdx, 1);
		int mv1 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX1[list][partNo] + mvpX1, mBlock.___003C_003Epb8x8.mvdY1[list][partNo] + mvpY1, refIdx);
		x.setMv(i00, list, mv1);
		x.setMv(i01, list, mv1);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX1), Integer.valueOf(mvpY1), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX1[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY1[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv1)), Integer.valueOf(H264Utils.Mv.mvY(mv1)), Integer.valueOf(refIdx));
		int mvpX2 = MBlockDecoderUtils.calcMVPredictionMedian(l1, mv1, MBlockDecoderUtils.___003C_003ENULL_VECTOR, l0, lAvb, bAvb: true, cAvb: false, lAvb, refIdx, 0);
		int mvpY2 = MBlockDecoderUtils.calcMVPredictionMedian(l1, mv1, MBlockDecoderUtils.___003C_003ENULL_VECTOR, l0, lAvb, bAvb: true, cAvb: false, lAvb, refIdx, 1);
		int mv2 = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX2[list][partNo] + mvpX2, mBlock.___003C_003Epb8x8.mvdY2[list][partNo] + mvpY2, refIdx);
		x.setMv(i10, list, mv2);
		x.setMv(i11, list, mv2);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX2), Integer.valueOf(mvpY2), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX2[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY2[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv2)), Integer.valueOf(H264Utils.Mv.mvY(mv2)), Integer.valueOf(refIdx));
		interpolator.getBlockLuma(references[refIdx], mb, off, offX + H264Utils.Mv.mvX(mv1), offY + H264Utils.Mv.mvY(mv1), 8, 4);
		interpolator.getBlockLuma(references[refIdx], mb, off + mb.getWidth() * 4, offX + H264Utils.Mv.mvX(mv2), offY + H264Utils.Mv.mvY(mv2) + 16, 8, 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 65, 77, 119, 151, 159, 17, 110, 110,
		110, 142, 127, 46, 63, 16, 166, 127, 13
	})]
	private void decodeSub8x8(MBlock mBlock, int partNo, Picture[] references, int offX, int offY, int tl, int t0, int tr, int l0, bool tlAvb, bool tAvb, bool trAvb, bool lAvb, H264Utils.MvList x, int i00, int i01, int i10, int i11, int refIdx, Picture mb, int off, int list)
	{
		int mvpX = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, tr, tl, lAvb, tAvb, trAvb, tlAvb, refIdx, 0);
		int mvpY = MBlockDecoderUtils.calcMVPredictionMedian(l0, t0, tr, tl, lAvb, tAvb, trAvb, tlAvb, refIdx, 1);
		int mv = H264Utils.Mv.packMv(mBlock.___003C_003Epb8x8.mvdX1[list][partNo] + mvpX, mBlock.___003C_003Epb8x8.mvdY1[list][partNo] + mvpY, refIdx);
		x.setMv(i00, list, mv);
		x.setMv(i01, list, mv);
		x.setMv(i10, list, mv);
		x.setMv(i11, list, mv);
		MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX), Integer.valueOf(mvpY), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdX1[list][partNo]), Integer.valueOf(mBlock.___003C_003Epb8x8.mvdY1[list][partNo]), Integer.valueOf(H264Utils.Mv.mvX(mv)), Integer.valueOf(H264Utils.Mv.mvY(mv)), Integer.valueOf(refIdx));
		interpolator.getBlockLuma(references[refIdx], mb, off, offX + H264Utils.Mv.mvX(mv), offY + H264Utils.Mv.mvY(mv), 8, 8);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 112, 104, 104 })]
	public MBlockDecoderInter8x8(Mapper mapper, MBlockDecoderBDirect bDirectDecoder, SliceHeader sh, DeblockerInput di, int poc, DecoderState decoderState)
		: base(sh, di, poc, decoderState)
	{
		this.mapper = mapper;
		this.bDirectDecoder = bDirectDecoder;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 161, 68, 115, 115, 115, 116, 116, 116,
		148, 106, 191, 5, 223, 1, 124, 156, 115, 159,
		14, 156, 141, 148, 127, 1, 159, 1, 145, 115,
		147, 191, 22, 142, 117, 117
	})]
	public virtual void decode(MBlock mBlock, org.jcodec.codecs.h264.io.model.Frame[][] references, Picture mb, SliceType sliceType, bool ref0)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int mbAddr = mapper.getAddress(mBlock.mbIdx);
		int topLeftAvailable = (mapper.topLeftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topRightAvailable = (mapper.topRightAvailable(mBlock.mbIdx) ? 1 : 0);
		if (sliceType == SliceType.___003C_003EP)
		{
			predict8x8P(mBlock, references[0], mb, ref0, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, (byte)topLeftAvailable != 0, (byte)topRightAvailable != 0, mBlock.x, mBlock.partPreds);
		}
		else
		{
			predict8x8B(mBlock, references, mb, ref0, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, (byte)topLeftAvailable != 0, (byte)topRightAvailable != 0, mBlock.x, mBlock.partPreds);
		}
		predictChromaInter(references, mBlock.x, mbX << 3, mbY << 3, 1, mb, mBlock.partPreds);
		predictChromaInter(references, mBlock.x, mbX << 3, mbY << 3, 2, mb, mBlock.partPreds);
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			DecoderState decoderState = s;
			int num = s.qp + mBlock.mbQPDelta + 52;
			decoderState.qp = ((52 != -1) ? (num % 52) : 0);
		}
		di.mbQps[0][mbAddr] = s.qp;
		residualLuma(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		MBlockDecoderUtils.saveMvs(di, mBlock.x, mbX, mbY);
		int qp1 = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[0]);
		int qp2 = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[1]);
		decodeChromaResidual(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY, qp1, qp2);
		di.mbQps[1][mbAddr] = qp1;
		di.mbQps[2][mbAddr] = qp2;
		MBlockDecoderUtils.mergeResidual(mb, mBlock.ac, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_BLOCK_4x4_LUT : H264Const.___003C_003ECOMP_BLOCK_8x8_LUT, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_POS_4x4_LUT : H264Const.___003C_003ECOMP_POS_8x8_LUT);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		di.mbTypes[mbAddr] = mBlock.curMbType;
		di.tr8x8Used[mbAddr] = mBlock.transform8x8Used;
	}
}
