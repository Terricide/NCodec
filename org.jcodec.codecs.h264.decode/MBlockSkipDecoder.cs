using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.util;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlockSkipDecoder : MBlockDecoderBase
{
	private Mapper mapper;

	private MBlockDecoderBDirect bDirectDecoder;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		125,
		65,
		77,
		103,
		109,
		119,
		149,
		111,
		159,
		27,
		byte.MaxValue,
		27,
		69,
		102,
		127,
		2,
		127,
		3,
		126,
		125,
		153,
		106,
		53,
		169,
		159,
		5,
		159,
		22
	})]
	public virtual void predictPSkip(org.jcodec.codecs.h264.io.model.Frame[][] refs, int mbX, int mbY, bool lAvb, bool tAvb, bool tlAvb, bool trAvb, H264Utils.MvList x, Picture mb)
	{
		int mvX = 0;
		int mvY = 0;
		if (lAvb && tAvb)
		{
			int b = s.mvTop.getMv(mbX << 2, 0);
			int a = s.mvLeft.getMv(0, 0);
			if (a != 0 && b != 0)
			{
				mvX = MBlockDecoderUtils.calcMVPredictionMedian(a, b, s.mvTop.getMv((mbX << 2) + 4, 0), s.mvTopLeft.getMv(0, 0), lAvb, tAvb, trAvb, tlAvb, 0, 0);
				mvY = MBlockDecoderUtils.calcMVPredictionMedian(a, b, s.mvTop.getMv((mbX << 2) + 4, 0), s.mvTopLeft.getMv(0, 0), lAvb, tAvb, trAvb, tlAvb, 0, 1);
			}
		}
		int xx = mbX << 2;
		s.mvTopLeft.copyPair(0, s.mvTop, xx + 3);
		MBlockDecoderUtils.saveVect(s.mvTop, 0, xx, xx + 4, H264Utils.Mv.packMv(mvX, mvY, 0));
		MBlockDecoderUtils.saveVect(s.mvLeft, 0, 0, 4, H264Utils.Mv.packMv(mvX, mvY, 0));
		MBlockDecoderUtils.saveVect(s.mvTop, 1, xx, xx + 4, MBlockDecoderUtils.___003C_003ENULL_VECTOR);
		MBlockDecoderUtils.saveVect(s.mvLeft, 1, 0, 4, MBlockDecoderUtils.___003C_003ENULL_VECTOR);
		for (int i = 0; i < 16; i++)
		{
			x.setMv(i, 0, H264Utils.Mv.packMv(mvX, mvY, 0));
		}
		interpolator.getBlockLuma(refs[0][0], mb, 0, (mbX << 6) + mvX, (mbY << 6) + mvY, 16, 16);
		PredictionMerger.mergePrediction(sh, 0, 0, H264Const.PartPred.___003C_003EL0, 0, mb.getPlaneData(0), null, 0, 16, 16, 16, mb.getPlaneData(0), refs, poc);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 130, 117, 119 })]
	public virtual void decodeChromaSkip(org.jcodec.codecs.h264.io.model.Frame[][] reference, H264Utils.MvList vectors, H264Const.PartPred[] pp, int mbX, int mbY, Picture mb)
	{
		predictChromaInter(reference, vectors, mbX << 3, mbY << 3, 1, mb, pp);
		predictChromaInter(reference, vectors, mbX << 3, mbY << 3, 2, mb, pp);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 112, 104, 104 })]
	public MBlockSkipDecoder(Mapper mapper, MBlockDecoderBDirect bDirectDecoder, SliceHeader sh, DeblockerInput di, int poc, DecoderState sharedState)
		: base(sh, di, poc, sharedState)
	{
		this.mapper = mapper;
		this.bDirectDecoder = bDirectDecoder;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 115, 115, 147, 106, 127, 20, 62,
		134, 150, 127, 8, 127, 4, 24, 166, 179, 151,
		142, 116, 116, 123, 127, 14, 127, 14
	})]
	public virtual void decodeSkip(MBlock mBlock, org.jcodec.codecs.h264.io.model.Frame[][] refs, Picture mb, SliceType sliceType)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int mbAddr = mapper.getAddress(mBlock.mbIdx);
		if (sliceType == SliceType.___003C_003EP)
		{
			predictPSkip(refs, mbX, mbY, mapper.leftAvailable(mBlock.mbIdx), mapper.topAvailable(mBlock.mbIdx), mapper.topLeftAvailable(mBlock.mbIdx), mapper.topRightAvailable(mBlock.mbIdx), mBlock.x, mb);
			Arrays.fill(mBlock.partPreds, H264Const.PartPred.___003C_003EL0);
		}
		else
		{
			bDirectDecoder.predictBDirect(refs, mbX, mbY, mapper.leftAvailable(mBlock.mbIdx), mapper.topAvailable(mBlock.mbIdx), mapper.topLeftAvailable(mBlock.mbIdx), mapper.topRightAvailable(mBlock.mbIdx), mBlock.x, mBlock.partPreds, mb, H264Const.___003C_003EidentityMapping4);
			MBlockDecoderUtils.savePrediction8x8(s, mbX, mBlock.x);
		}
		decodeChromaSkip(refs, mBlock.x, mBlock.partPreds, mbX, mbY, mb);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		MBlockDecoderUtils.saveMvs(di, mBlock.x, mbX, mbY);
		di.mbTypes[mbAddr] = mBlock.curMbType;
		di.mbQps[0][mbAddr] = s.qp;
		di.mbQps[1][mbAddr] = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[0]);
		di.mbQps[2][mbAddr] = MBlockDecoderBase.calcQpChroma(s.qp, s.chromaQpOffset[1]);
	}
}
