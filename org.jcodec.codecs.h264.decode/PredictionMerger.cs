using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class PredictionMerger : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 104, 110, 155, 159, 13, 122, 114,
		104, 159, 9, 159, 9, 159, 10, 159, 10, 159,
		14, 102, 122, 127, 4, 105, 127, 6, 125, 158,
		112, 101, 200, 190
	})]
	public static void mergePrediction(SliceHeader sh, int refIdxL0, int refIdxL1, H264Const.PartPred predType, int comp, byte[] pred0, byte[] pred1, int off, int stride, int blkW, int blkH, byte[] @out, Frame[][] refs, int thisPoc)
	{
		PictureParameterSet pps = sh.pps;
		if (sh.sliceType == SliceType.___003C_003EP)
		{
			weightPrediction(sh, refIdxL0, comp, pred0, off, stride, blkW, blkH, @out);
			return;
		}
		if (!pps.weightedPredFlag || sh.pps.weightedBipredIdc == 0 || (sh.pps.weightedBipredIdc == 2 && predType != H264Const.PartPred.___003C_003EBi))
		{
			mergeAvg(pred0, pred1, stride, predType, off, blkW, blkH, @out);
			return;
		}
		if (sh.pps.weightedBipredIdc == 1)
		{
			PredictionWeightTable w = sh.predWeightTable;
			int w3 = ((refIdxL0 != -1) ? ((comp != 0) ? w.chromaWeight[0][comp - 1][refIdxL0] : w.lumaWeight[0][refIdxL0]) : 0);
			int w5 = ((refIdxL1 != -1) ? ((comp != 0) ? w.chromaWeight[1][comp - 1][refIdxL1] : w.lumaWeight[1][refIdxL1]) : 0);
			int o0 = ((refIdxL0 != -1) ? ((comp != 0) ? w.chromaOffset[0][comp - 1][refIdxL0] : w.lumaOffset[0][refIdxL0]) : 0);
			int o1 = ((refIdxL1 != -1) ? ((comp != 0) ? w.chromaOffset[1][comp - 1][refIdxL1] : w.lumaOffset[1][refIdxL1]) : 0);
			mergeWeight(pred0, pred1, stride, predType, off, blkW, blkH, (comp != 0) ? w.chromaLog2WeightDenom : w.lumaLog2WeightDenom, w3, w5, o0, o1, @out);
			return;
		}
		int tb = MathUtil.clip(thisPoc - refs[0][refIdxL0].getPOC(), -128, 127);
		int td = MathUtil.clip(refs[1][refIdxL1].getPOC() - refs[0][refIdxL0].getPOC(), -128, 127);
		int w2 = 32;
		int w4 = 32;
		if (td != 0 && refs[0][refIdxL0].isShortTerm() && refs[1][refIdxL1].isShortTerm())
		{
			int num = 16384 + Math.abs(td / 2);
			int tx = ((td != -1) ? (num / td) : (-num));
			int dsf = MathUtil.clip(tb * tx + 32 >> 6, -1024, 1023) >> 2;
			if (dsf >= -64 && dsf <= 128)
			{
				w4 = dsf;
				w2 = 64 - dsf;
			}
		}
		mergeWeight(pred0, pred1, stride, predType, off, blkW, blkH, 5, w2, w4, 0, 0, @out);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 130, 104, 119, 104, 223, 61, 99, 147 })]
	public static void weightPrediction(SliceHeader sh, int refIdxL0, int comp, byte[] pred0, int off, int stride, int blkW, int blkH, byte[] @out)
	{
		PictureParameterSet pps = sh.pps;
		if (pps.weightedPredFlag && sh.predWeightTable != null)
		{
			PredictionWeightTable w = sh.predWeightTable;
			weight(pred0, stride, off, blkW, blkH, (comp != 0) ? w.chromaLog2WeightDenom : w.lumaLog2WeightDenom, (comp != 0) ? w.chromaWeight[0][comp - 1][refIdxL0] : w.lumaWeight[0][refIdxL0], (comp != 0) ? w.chromaOffset[0][comp - 1][refIdxL0] : w.lumaOffset[0][refIdxL0], @out);
		}
		else
		{
			copyPrediction(pred0, stride, off, blkW, blkH, @out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 66, 105, 116, 105, 114, 105, 146 })]
	private static void mergeAvg(byte[] blk0, byte[] blk1, int stride, H264Const.PartPred p0, int off, int blkW, int blkH, byte[] @out)
	{
		if (p0 == H264Const.PartPred.___003C_003EBi)
		{
			_mergePrediction(blk0, blk1, stride, p0, off, blkW, blkH, @out);
		}
		else if (p0 == H264Const.PartPred.___003C_003EL0)
		{
			copyPrediction(blk0, stride, off, blkW, blkH, @out);
		}
		else if (p0 == H264Const.PartPred.___003C_003EL1)
		{
			copyPrediction(blk1, stride, off, blkW, blkH, @out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 162, 105, 120, 105, 120, 105, 157 })]
	private static void mergeWeight(byte[] blk0, byte[] blk1, int stride, H264Const.PartPred partPred, int off, int blkW, int blkH, int logWD, int w0, int w1, int o0, int o1, byte[] @out)
	{
		if (partPred == H264Const.PartPred.___003C_003EL0)
		{
			weight(blk0, stride, off, blkW, blkH, logWD, w0, o0, @out);
		}
		else if (partPred == H264Const.PartPred.___003C_003EL1)
		{
			weight(blk1, stride, off, blkW, blkH, logWD, w1, o1, @out);
		}
		else if (partPred == H264Const.PartPred.___003C_003EBi)
		{
			_weightPrediction(blk0, blk1, stride, off, blkW, blkH, logWD, w0, w1, o0, o1, @out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 66, 139, 201, 104, 104, 104, 103, 63,
		1, 44, 240, 69, 112, 104, 105, 56, 46, 206
	})]
	private static void weight(byte[] blk0, int stride, int off, int blkW, int blkH, int logWD, int w, int o, byte[] @out)
	{
		int round = 1 << logWD - 1;
		if (logWD >= 1)
		{
			o += -128;
			round += w << 7;
			int j = 0;
			while (j < blkH)
			{
				int l = 0;
				while (l < blkW)
				{
					@out[off] = (byte)(sbyte)MathUtil.clip((blk0[off] * w + round >> logWD) + o, -128, 127);
					l++;
					off++;
				}
				j++;
				off += stride - blkW;
			}
			return;
		}
		o += (w << 7) - 128;
		int i = 0;
		while (i < blkH)
		{
			int k = 0;
			while (k < blkW)
			{
				@out[off] = (byte)(sbyte)MathUtil.clip(blk0[off] * w + o, -128, 127);
				k++;
				off++;
			}
			i++;
			off += stride - blkW;
		}
	}

	[LineNumberTable(new byte[] { 159, 116, 130, 104, 103, 40, 44, 174 })]
	private static void copyPrediction(byte[] _in, int stride, int off, int blkW, int blkH, byte[] @out)
	{
		int i = 0;
		while (i < blkH)
		{
			int j = 0;
			while (j < blkW)
			{
				@out[off] = _in[off];
				j++;
				off++;
			}
			i++;
			off += stride - blkW;
		}
	}

	[LineNumberTable(new byte[] { 159, 114, 130, 104, 104, 52, 45, 176 })]
	private static void _mergePrediction(byte[] blk0, byte[] blk1, int stride, H264Const.PartPred p0, int off, int blkW, int blkH, byte[] @out)
	{
		int i = 0;
		while (i < blkH)
		{
			int j = 0;
			while (j < blkW)
			{
				@out[off] = (byte)(sbyte)(blk0[off] + blk1[off] + 1 >> 1);
				j++;
				off++;
			}
			i++;
			off += stride - blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 130, 113, 113, 102, 104, 106, 63, 6,
		46, 207
	})]
	private static void _weightPrediction(byte[] blk0, byte[] blk1, int stride, int off, int blkW, int blkH, int logWD, int w0, int w1, int o0, int o1, byte[] @out)
	{
		int round = (1 << logWD) + (w0 + w1 << 7);
		int sum = (o0 + o1 + 1 >> 1) - 128;
		int logWDCP1 = logWD + 1;
		int i = 0;
		while (i < blkH)
		{
			int j = 0;
			while (j < blkW)
			{
				@out[off] = (byte)(sbyte)MathUtil.clip((blk0[off] * w0 + blk1[off] * w1 + round >> logWDCP1) + sum, -128, 127);
				j++;
				off++;
			}
			i++;
			off += stride - blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(25)]
	public PredictionMerger()
	{
	}
}
