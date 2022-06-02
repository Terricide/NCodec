using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class SliceDecoder : Object
{
	private Mapper mapper;

	private MBlockDecoderIntra16x16 decoderIntra16x16;

	private MBlockDecoderIntraNxN decoderIntraNxN;

	private MBlockDecoderInter decoderInter;

	private MBlockDecoderInter8x8 decoderInter8x8;

	private MBlockSkipDecoder skipDecoder;

	private MBlockDecoderBDirect decoderBDirect;

	private RefListManager refListManager;

	private MBlockDecoderIPCM decoderIPCM;

	private SliceReader parser;

	private SeqParameterSet activeSps;

	private org.jcodec.codecs.h264.io.model.Frame frameOut;

	private DecoderState decoderState;

	private DeblockerInput di;

	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	private IntObjectMap lRefs;

	private org.jcodec.codecs.h264.io.model.Frame[] sRefs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 141, 109, 158, 127, 11, 127, 11,
		127, 11, 127, 11, 127, 17, 127, 17, 152, 127,
		0
	})]
	private void initContext()
	{
		SliceHeader sh = parser.getSliceHeader();
		decoderState = new DecoderState(sh);
		mapper = new MapManager(sh.sps, sh.pps).getMapper(sh);
		decoderIntra16x16 = new MBlockDecoderIntra16x16(mapper, sh, di, frameOut.getPOC(), decoderState);
		decoderIntraNxN = new MBlockDecoderIntraNxN(mapper, sh, di, frameOut.getPOC(), decoderState);
		decoderInter = new MBlockDecoderInter(mapper, sh, di, frameOut.getPOC(), decoderState);
		decoderBDirect = new MBlockDecoderBDirect(mapper, sh, di, frameOut.getPOC(), decoderState);
		decoderInter8x8 = new MBlockDecoderInter8x8(mapper, decoderBDirect, sh, di, frameOut.getPOC(), decoderState);
		skipDecoder = new MBlockSkipDecoder(mapper, decoderBDirect, sh, di, frameOut.getPOC(), decoderState);
		decoderIPCM = new MBlockDecoderIPCM(mapper, decoderState);
		refListManager = new RefListManager(sh, sRefs, lRefs, frameOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 162, 118, 143, 114, 114, 122, 115, 111,
		110, 113, 121, 111, 108, 104, 103, 102
	})]
	private void decodeMacroblocks(org.jcodec.codecs.h264.io.model.Frame[][] refList)
	{
		Picture mb = Picture.create(16, 16, activeSps.chromaFormatIdc);
		int mbWidth = activeSps.picWidthInMbsMinus1 + 1;
		MBlock mBlock = new MBlock(activeSps.chromaFormatIdc);
		while (parser.readMacroblock(mBlock))
		{
			decode(mBlock, parser.getSliceHeader().sliceType, mb, refList);
			int mbAddr = mapper.getAddress(mBlock.mbIdx);
			int mbX = ((mbWidth != -1) ? (mbAddr % mbWidth) : 0);
			int mbY = ((mbWidth != -1) ? (mbAddr / mbWidth) : (-mbAddr));
			putMacroblock(frameOut, mb, mbX, mbY);
			di.shs[mbAddr] = parser.getSliceHeader();
			di.refsUsed[mbAddr] = refList;
			fillCoeff(mBlock, mbX, mbY);
			mb.fill(0);
			mBlock.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 98, 105, 115, 105, 107, 105, 141, 141 })]
	public virtual void decode(MBlock mBlock, SliceType sliceType, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] references)
	{
		if (mBlock.skipped)
		{
			skipDecoder.decodeSkip(mBlock, references, mb, sliceType);
		}
		else if (sliceType == SliceType.___003C_003EI)
		{
			decodeMBlockI(mBlock, mb);
		}
		else if (sliceType == SliceType.___003C_003EP)
		{
			decodeMBlockP(mBlock, mb, references);
		}
		else
		{
			decodeMBlockB(mBlock, mb, references);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 95, 66, 105, 137, 105, 105, 138, 100, 103,
		103, 106, 106, 119, 8, 233, 69, 102, 102, 106,
		106, 105, 103, 110, 110, 238, 60, 233, 70
	})]
	private static void putMacroblock(Picture tgt, Picture decoded, int mbX, int mbY)
	{
		byte[] luma = tgt.getPlaneData(0);
		int stride = tgt.getPlaneWidth(0);
		byte[] cb = tgt.getPlaneData(1);
		byte[] cr = tgt.getPlaneData(2);
		int strideChroma = tgt.getPlaneWidth(1);
		int dOff = 0;
		int mbx16 = mbX * 16;
		int mby16 = mbY * 16;
		byte[] decodedY = decoded.getPlaneData(0);
		for (int j = 0; j < 16; j++)
		{
			ByteCodeHelper.arraycopy_primitive_1(decodedY, dOff, luma, (mby16 + j) * stride + mbx16, 16);
			dOff += 16;
		}
		int mbx17 = mbX * 8;
		int mby17 = mbY * 8;
		byte[] decodedCb = decoded.getPlaneData(1);
		byte[] decodedCr = decoded.getPlaneData(2);
		for (int i = 0; i < 8; i++)
		{
			int decodePos = i << 3;
			int chromaPos = (mby17 + i) * strideChroma + mbx17;
			ByteCodeHelper.arraycopy_primitive_1(decodedCb, decodePos, cb, chromaPos, 8);
			ByteCodeHelper.arraycopy_primitive_1(decodedCr, decodePos, cr, chromaPos, 8);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 130, 104, 105, 105, 103, 136, 249, 58,
		231, 72
	})]
	private void fillCoeff(MBlock mBlock, int mbX, int mbY)
	{
		for (int i = 0; i < 16; i++)
		{
			int blkOffLeft = H264Const.___003C_003EMB_BLK_OFF_LEFT[i];
			int blkOffTop = H264Const.___003C_003EMB_BLK_OFF_TOP[i];
			int blkX = (mbX << 2) + blkOffLeft;
			int blkY = (mbY << 2) + blkOffTop;
			di.nCoeff[blkY][blkX] = mBlock.nCoeff[i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 98, 107 })]
	private void decodeMBlockI(MBlock mBlock, Picture mb)
	{
		decodeMBlockIInt(mBlock, mb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 104, 66, 110, 121, 110, 126, 110, 123, 110,
		119, 110, 151, 139
	})]
	private void decodeMBlockP(MBlock mBlock, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] references)
	{
		if (MBType.___003C_003EP_16x16 == mBlock.curMbType)
		{
			decoderInter.decode16x16(mBlock, mb, references, H264Const.PartPred.___003C_003EL0);
		}
		else if (MBType.___003C_003EP_16x8 == mBlock.curMbType)
		{
			decoderInter.decode16x8(mBlock, mb, references, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0);
		}
		else if (MBType.___003C_003EP_8x16 == mBlock.curMbType)
		{
			decoderInter.decode8x16(mBlock, mb, references, H264Const.PartPred.___003C_003EL0, H264Const.PartPred.___003C_003EL0);
		}
		else if (MBType.___003C_003EP_8x8 == mBlock.curMbType)
		{
			decoderInter8x8.decode(mBlock, references, mb, SliceType.___003C_003EP, ref0: false);
		}
		else if (MBType.___003C_003EP_8x8ref0 == mBlock.curMbType)
		{
			decoderInter8x8.decode(mBlock, references, mb, SliceType.___003C_003EP, ref0: true);
		}
		else
		{
			decodeMBlockIInt(mBlock, mb);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 66, 110, 142, 110, 116, 106, 127, 3,
		107, 119, 107, 191, 14, 223, 14
	})]
	private void decodeMBlockB(MBlock mBlock, Picture mb, org.jcodec.codecs.h264.io.model.Frame[][] references)
	{
		if (mBlock.curMbType.isIntra())
		{
			decodeMBlockIInt(mBlock, mb);
		}
		else if (mBlock.curMbType == MBType.___003C_003EB_Direct_16x16)
		{
			decoderBDirect.decode(mBlock, mb, references);
		}
		else if (mBlock.mbType <= 3)
		{
			decoderInter.decode16x16(mBlock, mb, references, H264Const.___003C_003EbPredModes[mBlock.mbType][0]);
		}
		else if (mBlock.mbType == 22)
		{
			decoderInter8x8.decode(mBlock, references, mb, SliceType.___003C_003EB, ref0: false);
		}
		else if ((mBlock.mbType & 1) == 0)
		{
			decoderInter.decode16x8(mBlock, mb, references, H264Const.___003C_003EbPredModes[mBlock.mbType][0], H264Const.___003C_003EbPredModes[mBlock.mbType][1]);
		}
		else
		{
			decoderInter.decode8x16(mBlock, mb, references, H264Const.___003C_003EbPredModes[mBlock.mbType][0], H264Const.___003C_003EbPredModes[mBlock.mbType][1]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 98, 110, 112, 110, 144, 107, 144 })]
	private void decodeMBlockIInt(MBlock mBlock, Picture mb)
	{
		if (mBlock.curMbType == MBType.___003C_003EI_NxN)
		{
			decoderIntraNxN.decode(mBlock, mb);
			return;
		}
		if (mBlock.curMbType == MBType.___003C_003EI_16x16)
		{
			decoderIntra16x16.decode(mBlock, mb);
			return;
		}
		Logger.warn("IPCM macroblock found. Not tested, may cause unpredictable behavior.");
		decoderIPCM.decode(mBlock, mb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 105, 105, 104, 105, 104, 104 })]
	public SliceDecoder(SeqParameterSet activeSps, org.jcodec.codecs.h264.io.model.Frame[] sRefs, IntObjectMap lRefs, DeblockerInput di, org.jcodec.codecs.h264.io.model.Frame result)
	{
		this.di = di;
		this.activeSps = activeSps;
		frameOut = result;
		this.sRefs = sRefs;
		this.lRefs = lRefs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 98, 136, 135, 159, 8, 141, 106 })]
	public virtual void decodeFromReader(SliceReader sliceReader)
	{
		parser = sliceReader;
		initContext();
		MBlockDecoderUtils.debugPrint("============%d============= ", Integer.valueOf(frameOut.getPOC()));
		org.jcodec.codecs.h264.io.model.Frame[][] refList = refListManager.getRefList();
		decodeMacroblocks(refList);
	}
}
