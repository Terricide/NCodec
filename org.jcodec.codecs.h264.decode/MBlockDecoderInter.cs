using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderInter : MBlockDecoderBase
{
	private Mapper mapper;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 74, 161, 77, 106, 111, 127, 30, 63, 13,
		168, 127, 30, 63, 13, 200, 116, 148, 127, 42,
		63, 18, 134, 145, 191, 8, 110, 127, 11, 123,
		151, 106, 46, 169
	})]
	internal virtual void predictInter16x16(MBlock mBlock, Picture mb, Picture[][] references, int mbX, int mbY, bool leftAvailable, bool topAvailable, bool tlAvailable, bool trAvailable, H264Utils.MvList x, int xx, int list, H264Const.PartPred curPred)
	{
		int mvX = 0;
		int mvY = 0;
		int r = -1;
		if (H264Const.usesList(curPred, list))
		{
			int mvpX = MBlockDecoderUtils.calcMVPredictionMedian(s.mvLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 4, list), s.mvTopLeft.getMv(0, list), leftAvailable, topAvailable, trAvailable, tlAvailable, mBlock.___003C_003Epb16x16.refIdx[list], 0);
			int mvpY = MBlockDecoderUtils.calcMVPredictionMedian(s.mvLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 4, list), s.mvTopLeft.getMv(0, list), leftAvailable, topAvailable, trAvailable, tlAvailable, mBlock.___003C_003Epb16x16.refIdx[list], 1);
			mvX = mBlock.___003C_003Epb16x16.mvdX[list] + mvpX;
			mvY = mBlock.___003C_003Epb16x16.mvdY[list] + mvpY;
			MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX), Integer.valueOf(mvpY), Integer.valueOf(mBlock.___003C_003Epb16x16.mvdX[list]), Integer.valueOf(mBlock.___003C_003Epb16x16.mvdY[list]), Integer.valueOf(mvX), Integer.valueOf(mvY), Integer.valueOf(mBlock.___003C_003Epb16x16.refIdx[list]));
			r = mBlock.___003C_003Epb16x16.refIdx[list];
			interpolator.getBlockLuma(references[list][r], mb, 0, (mbX << 6) + mvX, (mbY << 6) + mvY, 16, 16);
		}
		int v = H264Utils.Mv.packMv(mvX, mvY, r);
		s.mvTopLeft.setMv(0, list, s.mvTop.getMv(xx + 3, list));
		MBlockDecoderUtils.saveVect(s.mvTop, list, xx, xx + 4, v);
		MBlockDecoderUtils.saveVect(s.mvLeft, list, 0, 4, v);
		for (int i = 0; i < 16; i++)
		{
			x.setMv(i, list, v);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 66, 161, 70, 115, 159, 14, 156, 142, 118,
		127, 0, 159, 0, 144, 114, 178, 117
	})]
	private void residualInter(MBlock mBlock, org.jcodec.codecs.h264.io.model.Frame[][] refs, bool leftAvailable, bool topAvailable, int mbX, int mbY, int mbAddr)
	{
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			DecoderState decoderState = s;
			int num = s.qp + mBlock.mbQPDelta + 52;
			decoderState.qp = ((52 != -1) ? (num % 52) : 0);
		}
		di.mbQps[0][mbAddr] = s.qp;
		residualLuma(mBlock, leftAvailable, topAvailable, mbX, mbY);
		if (s.chromaFormat != ColorSpace.___003C_003EMONO)
		{
			int qp1 = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[0]);
			int qp2 = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[1]);
			decodeChromaResidual(mBlock, leftAvailable, topAvailable, mbX, mbY, qp1, qp2);
			di.mbQps[1][mbAddr] = qp1;
			di.mbQps[2][mbAddr] = qp2;
		}
		di.tr8x8Used[mbAddr] = mBlock.transform8x8Used;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 56, 161, 77, 110, 140 })]
	public virtual int calcMVPrediction8x16Left(int a, int b, int c, int d, bool aAvb, bool bAvb, bool cAvb, bool dAvb, int refIdx, int comp)
	{
		if (aAvb && H264Utils.Mv.mvRef(a) == refIdx)
		{
			int result = H264Utils.Mv.mvC(a, comp);
			
			return result;
		}
		int result2 = MBlockDecoderUtils.calcMVPredictionMedian(a, b, c, d, aAvb, bAvb, cAvb, dAvb, refIdx, comp);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 161, 77, 149, 108, 141 })]
	public virtual int calcMVPrediction8x16Right(int a, int b, int c, int d, bool aAvb, bool bAvb, bool cAvb, bool dAvb, int refIdx, int comp)
	{
		int lc = (cAvb ? c : ((!dAvb) ? MBlockDecoderUtils.___003C_003ENULL_VECTOR : d));
		if (H264Utils.Mv.mvRef(lc) == refIdx)
		{
			int result = H264Utils.Mv.mvC(lc, comp);
			
			return result;
		}
		int result2 = MBlockDecoderUtils.calcMVPredictionMedian(a, b, c, d, aAvb, bAvb, cAvb, dAvb, refIdx, comp);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 60, 97, 77, 110, 140 })]
	public virtual int calcMVPrediction16x8Top(int a, int b, int c, int d, bool aAvb, bool bAvb, bool cAvb, bool dAvb, int refIdx, int comp)
	{
		if (bAvb && H264Utils.Mv.mvRef(b) == refIdx)
		{
			int result = H264Utils.Mv.mvC(b, comp);
			
			return result;
		}
		int result2 = MBlockDecoderUtils.calcMVPredictionMedian(a, b, c, d, aAvb, bAvb, cAvb, dAvb, refIdx, comp);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 58, 129, 77, 110, 140 })]
	public virtual int calcMVPrediction16x8Bottom(int a, int b, int c, int d, bool aAvb, bool bAvb, bool cAvb, bool dAvb, int refIdx, int comp)
	{
		if (aAvb && H264Utils.Mv.mvRef(a) == refIdx)
		{
			int result = H264Utils.Mv.mvC(a, comp);
			
			return result;
		}
		int result2 = MBlockDecoderUtils.calcMVPredictionMedian(a, b, c, d, aAvb, bAvb, cAvb, dAvb, refIdx, comp);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 161, 77, 115, 143, 127, 31, 63, 13,
		168, 127, 31, 63, 13, 200, 116, 148, 127, 42,
		63, 18, 166, 159, 19, 145, 142, 111, 159, 42,
		191, 42, 116, 148, 127, 42, 63, 18, 166, 159,
		26, 145, 142, 127, 11, 119, 119, 155, 105, 46,
		169, 106, 46, 169
	})]
	private void predictInter16x8(MBlock mBlock, Picture mb, Picture[][] references, int mbX, int mbY, bool leftAvailable, bool topAvailable, bool tlAvailable, bool trAvailable, int xx, H264Utils.MvList x, H264Const.PartPred p0, H264Const.PartPred p1, int list)
	{
		int mvX1 = 0;
		int mvY1 = 0;
		int mvX2 = 0;
		int mvY2 = 0;
		int r1 = -1;
		int r2 = -1;
		if (H264Const.usesList(p0, list))
		{
			int mvpX1 = calcMVPrediction16x8Top(s.mvLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 4, list), s.mvTopLeft.getMv(0, list), leftAvailable, topAvailable, trAvailable, tlAvailable, mBlock.___003C_003Epb168x168.refIdx1[list], 0);
			int mvpY1 = calcMVPrediction16x8Top(s.mvLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 4, list), s.mvTopLeft.getMv(0, list), leftAvailable, topAvailable, trAvailable, tlAvailable, mBlock.___003C_003Epb168x168.refIdx1[list], 1);
			mvX1 = mBlock.___003C_003Epb168x168.mvdX1[list] + mvpX1;
			mvY1 = mBlock.___003C_003Epb168x168.mvdY1[list] + mvpY1;
			MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX1), Integer.valueOf(mvpY1), Integer.valueOf(mBlock.___003C_003Epb168x168.mvdX1[list]), Integer.valueOf(mBlock.___003C_003Epb168x168.mvdY1[list]), Integer.valueOf(mvX1), Integer.valueOf(mvY1), Integer.valueOf(mBlock.___003C_003Epb168x168.refIdx1[list]));
			interpolator.getBlockLuma(references[list][mBlock.___003C_003Epb168x168.refIdx1[list]], mb, 0, (mbX << 6) + mvX1, (mbY << 6) + mvY1, 16, 8);
			r1 = mBlock.___003C_003Epb168x168.refIdx1[list];
		}
		int v1 = H264Utils.Mv.packMv(mvX1, mvY1, r1);
		if (H264Const.usesList(p1, list))
		{
			int mvpX2 = calcMVPrediction16x8Bottom(s.mvLeft.getMv(2, list), v1, MBlockDecoderUtils.___003C_003ENULL_VECTOR, s.mvLeft.getMv(1, list), leftAvailable, bAvb: true, cAvb: false, leftAvailable, mBlock.___003C_003Epb168x168.refIdx2[list], 0);
			int mvpY2 = calcMVPrediction16x8Bottom(s.mvLeft.getMv(2, list), v1, MBlockDecoderUtils.___003C_003ENULL_VECTOR, s.mvLeft.getMv(1, list), leftAvailable, bAvb: true, cAvb: false, leftAvailable, mBlock.___003C_003Epb168x168.refIdx2[list], 1);
			mvX2 = mBlock.___003C_003Epb168x168.mvdX2[list] + mvpX2;
			mvY2 = mBlock.___003C_003Epb168x168.mvdY2[list] + mvpY2;
			MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX2), Integer.valueOf(mvpY2), Integer.valueOf(mBlock.___003C_003Epb168x168.mvdX2[list]), Integer.valueOf(mBlock.___003C_003Epb168x168.mvdY2[list]), Integer.valueOf(mvX2), Integer.valueOf(mvY2), Integer.valueOf(mBlock.___003C_003Epb168x168.refIdx2[list]));
			interpolator.getBlockLuma(references[list][mBlock.___003C_003Epb168x168.refIdx2[list]], mb, 128, (mbX << 6) + mvX2, (mbY << 6) + 32 + mvY2, 16, 8);
			r2 = mBlock.___003C_003Epb168x168.refIdx2[list];
		}
		int v2 = H264Utils.Mv.packMv(mvX2, mvY2, r2);
		s.mvTopLeft.setMv(0, list, s.mvTop.getMv(xx + 3, list));
		MBlockDecoderUtils.saveVect(s.mvLeft, list, 0, 2, v1);
		MBlockDecoderUtils.saveVect(s.mvLeft, list, 2, 4, v2);
		MBlockDecoderUtils.saveVect(s.mvTop, list, xx, xx + 4, v2);
		for (int j = 0; j < 8; j++)
		{
			x.setMv(j, list, v1);
		}
		for (int i = 8; i < 16; i++)
		{
			x.setMv(i, list, v2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 65, 77, 135, 115, 111, 127, 31, 63,
		13, 168, 127, 31, 63, 13, 200, 116, 148, 127,
		42, 63, 18, 166, 159, 19, 241, 69, 110, 111,
		127, 16, 63, 18, 168, 127, 16, 63, 18, 200,
		116, 148, 223, 160, 95, 159, 22, 145, 142, 127,
		11, 123, 125, 151, 106, 110, 112, 112, 240, 60,
		233, 70
	})]
	private void predictInter8x16(MBlock mBlock, Picture mb, Picture[][] references, int mbX, int mbY, bool leftAvailable, bool topAvailable, bool tlAvailable, bool trAvailable, H264Utils.MvList x, int list, H264Const.PartPred p0, H264Const.PartPred p1)
	{
		int xx = mbX << 2;
		int mvX1 = 0;
		int mvY1 = 0;
		int r1 = -1;
		int mvX2 = 0;
		int mvY2 = 0;
		int r2 = -1;
		if (H264Const.usesList(p0, list))
		{
			int mvpX1 = calcMVPrediction8x16Left(s.mvLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 2, list), s.mvTopLeft.getMv(0, list), leftAvailable, topAvailable, topAvailable, tlAvailable, mBlock.___003C_003Epb168x168.refIdx1[list], 0);
			int mvpY1 = calcMVPrediction8x16Left(s.mvLeft.getMv(0, list), s.mvTop.getMv(mbX << 2, list), s.mvTop.getMv((mbX << 2) + 2, list), s.mvTopLeft.getMv(0, list), leftAvailable, topAvailable, topAvailable, tlAvailable, mBlock.___003C_003Epb168x168.refIdx1[list], 1);
			mvX1 = mBlock.___003C_003Epb168x168.mvdX1[list] + mvpX1;
			mvY1 = mBlock.___003C_003Epb168x168.mvdY1[list] + mvpY1;
			MBlockDecoderUtils.debugPrint("MVP: (%d, %d), MVD: (%d, %d), MV: (%d,%d,%d)", Integer.valueOf(mvpX1), Integer.valueOf(mvpY1), Integer.valueOf(mBlock.___003C_003Epb168x168.mvdX1[list]), Integer.valueOf(mBlock.___003C_003Epb168x168.mvdY1[list]), Integer.valueOf(mvX1), Integer.valueOf(mvY1), Integer.valueOf(mBlock.___003C_003Epb168x168.refIdx1[list]));
			interpolator.getBlockLuma(references[list][mBlock.___003C_003Epb168x168.refIdx1[list]], mb, 0, (mbX << 6) + mvX1, (mbY << 6) + mvY1, 8, 16);
			r1 = mBlock.___003C_003Epb168x168.refIdx1[list];
		}
		int v1 = H264Utils.Mv.packMv(mvX1, mvY1, r1);
		if (H264Const.usesList(p1, list))
		{
			int mvpX2 = calcMVPrediction8x16Right(v1, s.mvTop.getMv((mbX << 2) + 2, list), s.mvTop.getMv((mbX << 2) + 4, list), s.mvTop.getMv((mbX << 2) + 1, list), aAvb: true, topAvailable, trAvailable, topAvailable, mBlock.___003C_003Epb168x168.refIdx2[list], 0);
			int mvpY2 = calcMVPrediction8x16Right(v1, s.mvTop.getMv((mbX << 2) + 2, list), s.mvTop.getMv((mbX << 2) + 4, list), s.mvTop.getMv((mbX << 2) + 1, list), aAvb: true, topAvailable, trAvailable, topAvailable, mBlock.___003C_003Epb168x168.refIdx2[list], 1);
			mvX2 = mBlock.___003C_003Epb168x168.mvdX2[list] + mvpX2;
			mvY2 = mBlock.___003C_003Epb168x168.mvdY2[list] + mvpY2;
			MBlockDecoderUtils.debugPrint(new StringBuilder().append("MVP: (").append(mvpX2).append(", ")
				.append(mvpY2)
				.append("), MVD: (")
				.append(mBlock.___003C_003Epb168x168.mvdX2[list])
				.append(", ")
				.append(mBlock.___003C_003Epb168x168.mvdY2[list])
				.append("), MV: (")
				.append(mvX2)
				.append(",")
				.append(mvY2)
				.append(",")
				.append(mBlock.___003C_003Epb168x168.refIdx2[list])
				.append(")")
				.toString());
			interpolator.getBlockLuma(references[list][mBlock.___003C_003Epb168x168.refIdx2[list]], mb, 8, (mbX << 6) + 32 + mvX2, (mbY << 6) + mvY2, 8, 16);
			r2 = mBlock.___003C_003Epb168x168.refIdx2[list];
		}
		int v2 = H264Utils.Mv.packMv(mvX2, mvY2, r2);
		s.mvTopLeft.setMv(0, list, s.mvTop.getMv(xx + 3, list));
		MBlockDecoderUtils.saveVect(s.mvTop, list, xx, xx + 2, v1);
		MBlockDecoderUtils.saveVect(s.mvTop, list, xx + 2, xx + 4, v2);
		MBlockDecoderUtils.saveVect(s.mvLeft, list, 0, 4, v2);
		for (int i = 0; i < 16; i += 4)
		{
			x.setMv(i, list, v1);
			x.setMv(i + 1, list, v1);
			x.setMv(i + 2, list, v2);
			x.setMv(i + 3, list, v2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 111, 104 })]
	public MBlockDecoderInter(Mapper mapper, SliceHeader sh, DeblockerInput di, int poc, DecoderState decoderState)
		: base(sh, di, poc, decoderState)
	{
		this.mapper = mapper;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 115, 115, 115, 147, 116, 116, 116,
		134, 105, 63, 7, 233, 69, 127, 26, 59, 166,
		127, 46, 124, 156, 158, 148, 191, 22, 142, 117
	})]
	public virtual void decode16x16(MBlock mBlock, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] refs, H264Const.PartPred p0)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int topLeftAvailable = (mapper.topLeftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topRightAvailable = (mapper.topRightAvailable(mBlock.mbIdx) ? 1 : 0);
		int address = mapper.getAddress(mBlock.mbIdx);
		int xx = mbX << 2;
		for (int list = 0; list < 2; list++)
		{
			predictInter16x16(mBlock, mbb[list], refs, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, (byte)topLeftAvailable != 0, (byte)topRightAvailable != 0, mBlock.x, xx, list, p0);
		}
		PredictionMerger.mergePrediction(sh, mBlock.x.mv0R(0), mBlock.x.mv1R(0), p0, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), 0, 16, 16, 16, mb.getPlaneData(0), refs, poc);
		H264Const.PartPred[] partPreds = mBlock.partPreds;
		H264Const.PartPred[] partPreds2 = mBlock.partPreds;
		H264Const.PartPred[] partPreds3 = mBlock.partPreds;
		H264Const.PartPred[] partPreds4 = mBlock.partPreds;
		H264Const.PartPred partPred = p0;
		int num = 3;
		H264Const.PartPred[] array = partPreds4;
		H264Const.PartPred partPred2 = partPred;
		array[num] = partPred;
		partPred = partPred2;
		num = 2;
		array = partPreds3;
		H264Const.PartPred partPred3 = partPred;
		array[num] = partPred;
		partPred = partPred3;
		num = 1;
		array = partPreds2;
		H264Const.PartPred partPred4 = partPred;
		array[num] = partPred;
		partPreds[0] = partPred4;
		predictChromaInter(refs, mBlock.x, mbX << 3, mbY << 3, 1, mb, mBlock.partPreds);
		predictChromaInter(refs, mBlock.x, mbX << 3, mbY << 3, 2, mb, mBlock.partPreds);
		residualInter(mBlock, refs, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY, mapper.getAddress(mBlock.mbIdx));
		MBlockDecoderUtils.saveMvs(di, mBlock.x, mbX, mbY);
		MBlockDecoderUtils.mergeResidual(mb, mBlock.ac, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_BLOCK_4x4_LUT : H264Const.___003C_003ECOMP_BLOCK_8x8_LUT, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_POS_4x4_LUT : H264Const.___003C_003ECOMP_POS_8x8_LUT);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		di.mbTypes[address] = mBlock.curMbType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 130, 115, 115, 115, 115, 116, 116, 116,
		134, 105, 63, 9, 233, 69, 127, 39, 45, 134,
		127, 43, 45, 166, 127, 2, 127, 2, 124, 156,
		158, 148, 191, 22, 142, 117
	})]
	public virtual void decode16x8(MBlock mBlock, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] refs, H264Const.PartPred p0, H264Const.PartPred p1)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int topLeftAvailable = (mapper.topLeftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topRightAvailable = (mapper.topRightAvailable(mBlock.mbIdx) ? 1 : 0);
		int address = mapper.getAddress(mBlock.mbIdx);
		int xx = mbX << 2;
		for (int list = 0; list < 2; list++)
		{
			predictInter16x8(mBlock, mbb[list], refs, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, (byte)topLeftAvailable != 0, (byte)topRightAvailable != 0, xx, mBlock.x, p0, p1, list);
		}
		PredictionMerger.mergePrediction(sh, mBlock.x.mv0R(0), mBlock.x.mv1R(0), p0, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), 0, 16, 16, 8, mb.getPlaneData(0), refs, poc);
		PredictionMerger.mergePrediction(sh, mBlock.x.mv0R(8), mBlock.x.mv1R(8), p1, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), 128, 16, 16, 8, mb.getPlaneData(0), refs, poc);
		H264Const.PartPred[] partPreds = mBlock.partPreds;
		H264Const.PartPred[] partPreds2 = mBlock.partPreds;
		H264Const.PartPred partPred = p0;
		int num = 1;
		H264Const.PartPred[] array = partPreds2;
		H264Const.PartPred partPred2 = partPred;
		array[num] = partPred;
		partPreds[0] = partPred2;
		H264Const.PartPred[] partPreds3 = mBlock.partPreds;
		H264Const.PartPred[] partPreds4 = mBlock.partPreds;
		partPred = p1;
		num = 3;
		array = partPreds4;
		H264Const.PartPred partPred3 = partPred;
		array[num] = partPred;
		partPreds3[2] = partPred3;
		predictChromaInter(refs, mBlock.x, mbX << 3, mbY << 3, 1, mb, mBlock.partPreds);
		predictChromaInter(refs, mBlock.x, mbX << 3, mbY << 3, 2, mb, mBlock.partPreds);
		residualInter(mBlock, refs, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY, mapper.getAddress(mBlock.mbIdx));
		MBlockDecoderUtils.saveMvs(di, mBlock.x, mbX, mbY);
		MBlockDecoderUtils.mergeResidual(mb, mBlock.ac, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_BLOCK_4x4_LUT : H264Const.___003C_003ECOMP_BLOCK_8x8_LUT, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_POS_4x4_LUT : H264Const.___003C_003ECOMP_POS_8x8_LUT);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		di.mbTypes[address] = mBlock.curMbType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 162, 115, 115, 115, 115, 116, 116, 148,
		105, 63, 7, 233, 69, 127, 39, 45, 134, 127,
		39, 45, 166, 127, 2, 159, 2, 124, 156, 158,
		148, 191, 22, 142, 117
	})]
	public virtual void decode8x16(MBlock mBlock, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] refs, H264Const.PartPred p0, H264Const.PartPred p1)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int topLeftAvailable = (mapper.topLeftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topRightAvailable = (mapper.topRightAvailable(mBlock.mbIdx) ? 1 : 0);
		int address = mapper.getAddress(mBlock.mbIdx);
		for (int list = 0; list < 2; list++)
		{
			predictInter8x16(mBlock, mbb[list], refs, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, (byte)topLeftAvailable != 0, (byte)topRightAvailable != 0, mBlock.x, list, p0, p1);
		}
		PredictionMerger.mergePrediction(sh, mBlock.x.mv0R(0), mBlock.x.mv1R(0), p0, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), 0, 16, 8, 16, mb.getPlaneData(0), refs, poc);
		PredictionMerger.mergePrediction(sh, mBlock.x.mv0R(2), mBlock.x.mv1R(2), p1, 0, mbb[0].getPlaneData(0), mbb[1].getPlaneData(0), 8, 16, 8, 16, mb.getPlaneData(0), refs, poc);
		H264Const.PartPred[] partPreds = mBlock.partPreds;
		H264Const.PartPred[] partPreds2 = mBlock.partPreds;
		H264Const.PartPred partPred = p0;
		int num = 2;
		H264Const.PartPred[] array = partPreds2;
		H264Const.PartPred partPred2 = partPred;
		array[num] = partPred;
		partPreds[0] = partPred2;
		H264Const.PartPred[] partPreds3 = mBlock.partPreds;
		H264Const.PartPred[] partPreds4 = mBlock.partPreds;
		partPred = p1;
		num = 3;
		array = partPreds4;
		H264Const.PartPred partPred3 = partPred;
		array[num] = partPred;
		partPreds3[1] = partPred3;
		predictChromaInter(refs, mBlock.x, mbX << 3, mbY << 3, 1, mb, mBlock.partPreds);
		predictChromaInter(refs, mBlock.x, mbX << 3, mbY << 3, 2, mb, mBlock.partPreds);
		residualInter(mBlock, refs, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY, mapper.getAddress(mBlock.mbIdx));
		MBlockDecoderUtils.saveMvs(di, mBlock.x, mbX, mbY);
		MBlockDecoderUtils.mergeResidual(mb, mBlock.ac, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_BLOCK_4x4_LUT : H264Const.___003C_003ECOMP_BLOCK_8x8_LUT, (!mBlock.transform8x8Used) ? H264Const.___003C_003ECOMP_POS_4x4_LUT : H264Const.___003C_003ECOMP_POS_8x8_LUT);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		di.mbTypes[address] = mBlock.curMbType;
	}
}
