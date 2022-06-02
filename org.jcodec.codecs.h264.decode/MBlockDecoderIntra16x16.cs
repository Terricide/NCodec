using System.Runtime.CompilerServices;
using IKVM.Attributes;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderIntra16x16 : MBlockDecoderBase
{
	private Mapper mapper;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 108, 105, 120, 140, 104, 114, 156,
		117, 240, 59, 231, 71
	})]
	private void residualLumaI16x16(MBlock mBlock, bool leftAvailable, bool topAvailable, int mbX, int mbY)
	{
		CoeffTransformer.invDC4x4(mBlock.dc);
		int[] scalingList = getScalingList(0);
		CoeffTransformer.dequantizeDC4x4(mBlock.dc, s.qp, scalingList);
		CoeffTransformer.reorderDC4x4(mBlock.dc);
		for (int i = 0; i < 16; i++)
		{
			if ((mBlock.cbpLuma() & (1 << (i >> 2))) != 0)
			{
				CoeffTransformer.dequantizeAC(mBlock.ac[0][i], s.qp, scalingList);
			}
			mBlock.ac[0][i][0] = mBlock.dc[i];
			CoeffTransformer.idct4x4(mBlock.ac[0][i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 111, 104 })]
	public MBlockDecoderIntra16x16(Mapper mapper, SliceHeader sh, DeblockerInput di, int poc, DecoderState decoderState)
		: base(sh, di, poc, decoderState)
	{
		this.mapper = mapper;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 115, 115, 115, 115, 116, 127, 14,
		155, 141, 127, 31, 38, 166, 121, 148, 110, 110,
		127, 0
	})]
	public virtual void decode(MBlock mBlock, Picture mb)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		int mbY = mapper.getMbY(mBlock.mbIdx);
		int address = mapper.getAddress(mBlock.mbIdx);
		int leftAvailable = (mapper.leftAvailable(mBlock.mbIdx) ? 1 : 0);
		int topAvailable = (mapper.topAvailable(mBlock.mbIdx) ? 1 : 0);
		DecoderState decoderState = s;
		int num = s.qp + mBlock.mbQPDelta + 52;
		decoderState.qp = ((52 != -1) ? (num % 52) : 0);
		di.mbQps[0][address] = s.qp;
		residualLumaI16x16(mBlock, (byte)leftAvailable != 0, (byte)topAvailable != 0, mbX, mbY);
		Intra16x16PredictionBuilder.predictWithMode(mBlock.luma16x16Mode, mBlock.ac[0], (byte)leftAvailable != 0, (byte)topAvailable != 0, s.leftRow[0], s.topLine[0], s.topLeft[0], mbX << 4, mb.getPlaneData(0));
		decodeChroma(mBlock, mbX, mbY, (byte)leftAvailable != 0, (byte)topAvailable != 0, mb, s.qp);
		di.mbTypes[address] = mBlock.curMbType;
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		MBlockDecoderUtils.saveMvsIntra(di, mbX, mbY);
		MBlockDecoderUtils.saveVectIntra(s, mapper.getMbX(mBlock.mbIdx));
	}
}
