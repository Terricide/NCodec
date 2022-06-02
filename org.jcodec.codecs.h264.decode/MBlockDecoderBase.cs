using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderBase : Object
{
	protected internal DecoderState s;

	protected internal SliceHeader sh;

	protected internal DeblockerInput di;

	protected internal int poc;

	protected internal BlockInterpolator interpolator;

	protected internal Picture[] mbb;

	protected internal int[][] scalingMatrix;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 66, 127, 14, 104, 191, 45, 106, 125,
		113, 159, 21, 246, 59, 234, 71, 102, 103, 102,
		103, 102, 103, 102, 103, 102, 103, 103, 104, 103,
		104, 103, 104
	})]
	protected internal static int[][] initScalingMatrix(SliceHeader sh2)
	{
		if (sh2.sps.scalingMatrix == null && (sh2.pps.extended == null || sh2.pps.extended.scalingMatrix == null))
		{
			return null;
		}
		int[][] merged = new int[12][]
		{
			H264Const.___003C_003EdefaultScalingList4x4Intra,
			null,
			null,
			H264Const.___003C_003EdefaultScalingList4x4Inter,
			null,
			null,
			H264Const.___003C_003EdefaultScalingList8x8Intra,
			H264Const.___003C_003EdefaultScalingList8x8Inter,
			null,
			null,
			null,
			null
		};
		for (int i = 0; i < 8; i++)
		{
			if (sh2.sps.scalingMatrix != null && sh2.sps.scalingMatrix[i] != null)
			{
				merged[i] = sh2.sps.scalingMatrix[i];
			}
			if (sh2.pps.extended != null && sh2.pps.extended.scalingMatrix != null && sh2.pps.extended.scalingMatrix[i] != null)
			{
				merged[i] = sh2.pps.extended.scalingMatrix[i];
			}
		}
		if (merged[1] == null)
		{
			merged[1] = merged[0];
		}
		if (merged[2] == null)
		{
			merged[2] = merged[0];
		}
		if (merged[4] == null)
		{
			merged[4] = merged[3];
		}
		if (merged[5] == null)
		{
			merged[5] = merged[3];
		}
		if (merged[8] == null)
		{
			merged[8] = merged[6];
		}
		if (merged[10] == null)
		{
			merged[10] = merged[6];
		}
		if (merged[9] == null)
		{
			merged[9] = merged[7];
		}
		if (merged[11] == null)
		{
			merged[11] = merged[7];
		}
		return merged;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 162, 104, 114, 163, 127, 19, 240, 58,
		234, 72
	})]
	private void residualLuma4x4(MBlock mBlock)
	{
		for (int i = 0; i < 16; i++)
		{
			if ((mBlock.cbpLuma() & (1 << (i >> 2))) != 0)
			{
				CoeffTransformer.dequantizeAC(mBlock.ac[0][i], s.qp, getScalingList((!mBlock.curMbType.intra) ? 3 : 0));
				CoeffTransformer.idct4x4(mBlock.ac[0][i]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 98, 103, 112, 163, 127, 19, 240, 58,
		234, 72
	})]
	private void residualLuma8x8CABAC(MBlock mBlock)
	{
		for (int i = 0; i < 4; i++)
		{
			if ((mBlock.cbpLuma() & (1 << i)) != 0)
			{
				CoeffTransformer.dequantizeAC8x8(mBlock.ac[0][i], s.qp, getScalingList((!mBlock.curMbType.intra) ? 7 : 6));
				CoeffTransformer.idct8x8(mBlock.ac[0][i]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 103, 112, 163, 127, 19, 240, 58,
		234, 72
	})]
	private void residualLuma8x8CAVLC(MBlock mBlock)
	{
		for (int i = 0; i < 4; i++)
		{
			if ((mBlock.cbpLuma() & (1 << i)) != 0)
			{
				CoeffTransformer.dequantizeAC8x8(mBlock.ac[0][i], s.qp, getScalingList((!mBlock.curMbType.intra) ? 7 : 6));
				CoeffTransformer.idct8x8(mBlock.ac[0][i]);
			}
		}
	}

	[LineNumberTable(new byte[] { 159, 124, 130, 105, 99 })]
	protected internal virtual int[] getScalingList(int which)
	{
		if (scalingMatrix == null)
		{
			return null;
		}
		return scalingMatrix[which];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(194)]
	internal static int calcQpChroma(int qp, int crQpOffset)
	{
		return H264Const.___003C_003EQP_SCALE_CR[MathUtil.clip(qp + crQpOffset, 0, 51)];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 97, 69, 108, 108, 122, 186, 120, 51,
		134, 120, 51, 168
	})]
	internal virtual void decodeChromaResidual(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY, int crQp1, int crQp2)
	{
		if (mBlock.cbpChroma() != 0)
		{
			if ((mBlock.cbpChroma() & 3) > 0)
			{
				chromaDC(mbX, leftAvailable, topAvailable, mBlock.dc1, 1, crQp1, mBlock.curMbType);
				chromaDC(mbX, leftAvailable, topAvailable, mBlock.dc2, 2, crQp2, mBlock.curMbType);
			}
			chromaAC(leftAvailable, topAvailable, mbX, mbY, mBlock.dc1, 1, crQp1, mBlock.curMbType, (mBlock.cbpChroma() & 2) > 0, mBlock.ac[1]);
			chromaAC(leftAvailable, topAvailable, mbX, mbY, mBlock.dc2, 2, crQp2, mBlock.curMbType, (mBlock.cbpChroma() & 2) > 0, mBlock.ac[2]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 98, 66, 104, 127, 5 })]
	private void chromaDC(int mbX, bool leftAvailable, bool topAvailable, int[] dc, int comp, int crQp, MBType curMbType)
	{
		CoeffTransformer.invDC2x2(dc);
		CoeffTransformer.dequantizeDC2x2(dc, crQp, getScalingList(((!curMbType.intra) ? 7 : 6) + comp * 2));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 97, 129, 68, 105, 134, 100, 159, 0, 104,
		231, 57, 231, 73
	})]
	private void chromaAC(bool leftAvailable, bool topAvailable, int mbX, int mbY, int[] dc, int comp, int crQp, MBType curMbType, bool codedAC, int[][] residualOut)
	{
		for (int i = 0; i < (nint)dc.LongLength; i++)
		{
			int[] ac = residualOut[i];
			if (codedAC)
			{
				CoeffTransformer.dequantizeAC(ac, crQp, getScalingList(((!curMbType.intra) ? 3 : 0) + comp));
			}
			ac[0] = dc[i];
			CoeffTransformer.idct4x4(ac);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 98, 105, 108, 105, 104, 104, 104, 127,
		28, 109
	})]
	public MBlockDecoderBase(SliceHeader sh, DeblockerInput di, int poc, DecoderState decoderState)
	{
		interpolator = new BlockInterpolator();
		s = decoderState;
		this.sh = sh;
		this.di = di;
		this.poc = poc;
		mbb = new Picture[2]
		{
			Picture.create(16, 16, s.chromaFormat),
			Picture.create(16, 16, s.chromaFormat)
		};
		scalingMatrix = initScalingMatrix(sh);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 105, 106, 115, 138, 138 })]
	internal virtual void residualLuma(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY)
	{
		if (!mBlock.transform8x8Used)
		{
			residualLuma4x4(mBlock);
		}
		else if (sh.pps.entropyCodingModeFlag)
		{
			residualLuma8x8CABAC(mBlock);
		}
		else
		{
			residualLuma8x8CAVLC(mBlock);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 108, 129, 71, 115, 111, 113, 162, 118, 150,
		105, 142, 121, 114, 114, 127, 29, 38, 134, 127,
		29, 38, 136
	})]
	public virtual void decodeChroma(MBlock mBlock, int mbX, int mbY, bool leftAvailable, bool topAvailable, Picture mb, int qp)
	{
		if (s.chromaFormat == ColorSpace.___003C_003EMONO)
		{
			Arrays.fill(mb.getPlaneData(1), 0);
			Arrays.fill(mb.getPlaneData(2), 0);
			return;
		}
		int qp2 = calcQpChroma(qp, s.chromaQpOffset[0]);
		int qp3 = calcQpChroma(qp, s.chromaQpOffset[1]);
		if (mBlock.cbpChroma() != 0)
		{
			decodeChromaResidual(mBlock, leftAvailable, topAvailable, mbX, mbY, qp2, qp3);
		}
		int addr = mbY * (sh.sps.picWidthInMbsMinus1 + 1) + mbX;
		di.mbQps[1][addr] = qp2;
		di.mbQps[2][addr] = qp3;
		ChromaPredictionBuilder.predictWithMode(mBlock.ac[1], mBlock.chromaPredictionMode, mbX, leftAvailable, topAvailable, s.leftRow[1], s.topLine[1], s.topLeft[1], mb.getPlaneData(1));
		ChromaPredictionBuilder.predictWithMode(mBlock.ac[2], mBlock.chromaPredictionMode, mbX, leftAvailable, topAvailable, s.leftRow[2], s.topLine[2], s.topLeft[2], mb.getPlaneData(2));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 92, 66, 106, 106, 109, 102, 106, 109, 107,
		142, 104, 136, 113, 146, 119, 127, 9, 12, 230,
		53, 234, 61, 234, 84, 108, 127, 8, 127, 1,
		24, 230, 42, 234, 90
	})]
	public virtual void predictChromaInter(org.jcodec.codecs.h264.io.model.Frame[][] refs, H264Utils.MvList vectors, int x, int y, int comp, Picture mb, H264Const.PartPred[] predType)
	{
		for (int blk8x8 = 0; blk8x8 < 4; blk8x8++)
		{
			for (int list = 0; list < 2; list++)
			{
				if (H264Const.usesList(predType[blk8x8], list))
				{
					for (int blk4x5 = 0; blk4x5 < 4; blk4x5++)
					{
						int i = H264Const.___003C_003EBLK_INV_MAP[(blk8x8 << 2) + blk4x5];
						int mv = vectors.getMv(i, list);
						org.jcodec.codecs.h264.io.model.Frame @ref = refs[list][H264Utils.Mv.mvRef(mv)];
						int blkPox = (i & 3) << 1;
						int blkPoy = i >> 2 << 1;
						int xx = (x + blkPox << 3) + H264Utils.Mv.mvX(mv);
						int yy = (y + blkPoy << 3) + H264Utils.Mv.mvY(mv);
						BlockInterpolator.getBlockChroma(@ref.getPlaneData(comp), @ref.getPlaneWidth(comp), @ref.getPlaneHeight(comp), mbb[list].getPlaneData(comp), blkPoy * mb.getPlaneWidth(comp) + blkPox, mb.getPlaneWidth(comp), xx, yy, 2, 2);
					}
				}
			}
			int blk4x4 = H264Const.___003C_003EBLK8x8_BLOCKS[blk8x8][0];
			PredictionMerger.mergePrediction(sh, vectors.mv0R(blk4x4), vectors.mv1R(blk4x4), predType[blk8x8], comp, mbb[0].getPlaneData(comp), mbb[1].getPlaneData(comp), H264Const.___003C_003EBLK_8x8_MB_OFF_CHROMA[blk8x8], mb.getPlaneWidth(comp), 4, 4, mb.getPlaneData(comp), refs, poc);
		}
	}
}
