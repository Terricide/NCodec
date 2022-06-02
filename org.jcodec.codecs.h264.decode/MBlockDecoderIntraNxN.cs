using System.Runtime.CompilerServices;
using IKVM.Attributes;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderIntraNxN : MBlockDecoderBase
{
	private Mapper mapper;

	private Intra8x8PredictionBuilder prediction8x8Builder;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 111, 104, 108 })]
	public MBlockDecoderIntraNxN(Mapper mapper, SliceHeader sh, DeblockerInput di, int poc, DecoderState decoderState)
		: base(sh, di, poc, decoderState)
	{
		this.mapper = mapper;
		prediction8x8Builder = new Intra8x8PredictionBuilder();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 115, 115, 115, 115, 116, 116, 148,
		115, 159, 14, 155, 141, 108, 109, 105, 136, 107,
		223, 42, 159, 57, 6, 230, 55, 241, 78, 108,
		105, 135, 125, 189, 159, 69, 6, 230, 56, 236,
		78, 153, 116, 148, 142, 110, 127, 0
	})]
	public virtual void decode(MBlock mBlock, Picture mb)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int mbAddr = mapper.getAddress(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		int topLeftAvailable = (mapper.topLeftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topRightAvailable = (mapper.topRightAvailable(mBlock.mbIdx) ? 1 : 0);
		if (mBlock.cbpLuma() > 0 || mBlock.cbpChroma() > 0)
		{
			DecoderState decoderState = s;
			int num = s.qp + mBlock.mbQPDelta + 52;
			decoderState.qp = ((52 != -1) ? (num % 52) : 0);
		}
		di.mbQps[0][mbAddr] = s.qp;
		residualLuma(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		if (!mBlock.transform8x8Used)
		{
			for (int j = 0; j < 16; j++)
			{
				int blkX2 = (j & 3) << 2;
				int blkY2 = j & -4;
				int bi = H264Const.___003C_003EBLK_INV_MAP[j];
				int trAvailable2 = ((((bi == 0 || bi == 1 || bi == 4) && topAvailable != 0) || (bi == 5 && topRightAvailable != 0) || bi == 2 || bi == 6 || bi == 8 || bi == 9 || bi == 10 || bi == 12 || bi == 14) ? 1 : 0);
				Intra4x4PredictionBuilder.predictWithMode(mBlock.lumaModes[bi], mBlock.ac[0][bi], (blkX2 != 0) ? true : ((byte)leftAvailable != 0), (blkY2 != 0) ? true : ((byte)topAvailable != 0), (byte)trAvailable2 != 0, s.leftRow[0], s.topLine[0], s.topLeft[0], mbX << 4, blkX2, blkY2, mb.getPlaneData(0));
			}
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				int blkX = (i & 1) << 1;
				int blkY = i & 2;
				int trAvailable = (((i == 0 && topAvailable != 0) || (i == 1 && topRightAvailable != 0) || i == 2) ? 1 : 0);
				int tlAvailable = i switch
				{
					0 => topLeftAvailable, 
					1 => topAvailable, 
					2 => leftAvailable, 
					_ => 1, 
				};
				prediction8x8Builder.predictWithMode(mBlock.lumaModes[i], mBlock.ac[0][i], (blkX != 0) ? true : ((byte)leftAvailable != 0), (blkY != 0) ? true : ((byte)topAvailable != 0), (byte)tlAvailable != 0, (byte)trAvailable != 0, s.leftRow[0], s.topLine[0], s.topLeft[0], mbX << 4, blkX << 2, blkY << 2, mb.getPlaneData(0));
			}
		}
		decodeChroma(mBlock, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, mb, s.qp);
		di.mbTypes[mbAddr] = mBlock.curMbType;
		di.tr8x8Used[mbAddr] = mBlock.transform8x8Used;
		MBlockDecoderUtils.collectChromaPredictors(s, mb, mbX);
		MBlockDecoderUtils.saveMvsIntra(di, mbX, mbY);
		MBlockDecoderUtils.saveVectIntra(s, mapper.getMbX(mBlock.mbIdx));
	}
}
