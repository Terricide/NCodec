using System.ComponentModel;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.common.biari;

namespace org.jcodec.codecs.vpx.vp9;

public class InterModeInfo : ModeInfo
{
	private long mvl0;

	private long mvl1;

	private long mvl2;

	private long mvl3;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 137 })]
	internal InterModeInfo()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 130, 99, 106, 108, 106, 120, 203, 99,
		108, 143, 113, 108, 143, 153, 100, 148
	})]
	public new virtual InterModeInfo read(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int segmentId = 0;
		if (c.isSegmentationEnabled())
		{
			segmentId = predicSegmentId(miCol, miRow, blSz, c);
			if (c.isUpdateSegmentMap() && (!c.isSegmentMapConditionalUpdate() || !readSegIdPredicted(miCol, miRow, blSz, decoder, c)))
			{
				segmentId = ModeInfo.readSegmentId(decoder, c);
			}
		}
		int skip = 1;
		if (!c.isSegmentFeatureActive(segmentId, 3))
		{
			skip = (readSkipFlag(miCol, miRow, blSz, decoder, c) ? 1 : 0);
		}
		int isInter = ((c.getSegmentFeature(segmentId, 2) != 0) ? 1 : 0);
		if (!c.isSegmentFeatureActive(segmentId, 2))
		{
			isInter = (readIsInter(miCol, miRow, blSz, decoder, c) ? 1 : 0);
		}
		int txSize = readTxSize(miCol, miRow, blSz, (skip == 0 || isInter == 0) ? true : false, decoder, c);
		if (isInter == 0)
		{
			InterModeInfo result = readInterIntraMode(miCol, miRow, blSz, decoder, c, segmentId, (byte)skip != 0, txSize);
			
			return result;
		}
		InterModeInfo result2 = readInterInterMode(miCol, miRow, blSz, decoder, c, segmentId, (byte)skip != 0, txSize);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 112, 130, 118, 118, 104, 99, 105, 105, 51,
		41, 169
	})]
	private static int predicSegmentId(int miCol, int miRow, int blSz, DecodingContext c)
	{
		int blWcl = Math.min(c.getMiTileWidth() - miCol, Consts.___003C_003EblW[blSz]);
		int blHcl = Math.min(c.getMiTileHeight() - miRow, Consts.___003C_003EblH[blSz]);
		int[][] prevSegmentIds = c.getPrevSegmentIds();
		int seg = 7;
		for (int y = 0; y < blHcl; y++)
		{
			for (int x = 0; x < blWcl; x++)
			{
				seg = Math.min(seg, prevSegmentIds[miRow + y][miCol + x]);
			}
		}
		return seg;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 116, 98, 105, 137, 117, 137, 147, 111, 41,
		137, 111, 41, 169
	})]
	private static bool readSegIdPredicted(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		bool[] aboveSegIdPredicted = c.getAboveSegIdPredicted();
		bool[] leftSegIdPredicted = c.getLeftSegIdPredicted();
		int ctx = (aboveSegIdPredicted[miRow] ? 1 : 0) + (leftSegIdPredicted[miCol] ? 1 : 0);
		int[] prob = c.getSegmentationPredProbs();
		int ret = ((decoder.readBit(prob[ctx]) == 1) ? 1 : 0);
		for (int j = 0; j < Consts.___003C_003EblH[blSz]; j++)
		{
			aboveSegIdPredicted[miCol + j] = (byte)ret != 0;
		}
		for (int i = 0; i < Consts.___003C_003EblW[blSz]; i++)
		{
			leftSegIdPredicted[miRow + i] = (byte)ret != 0;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 121, 98, 106, 112, 113, 147, 113, 145, 100,
		103, 124, 103, 154, 106
	})]
	protected internal virtual bool readIsInter(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		int aboveRefFrame0 = getRef(c.getAboveRefs()[miCol], 0);
		int leftRefFrame0 = getRef(c.getLeftRefs()[miRow & 7], 0);
		int leftIntra = ((availLeft == 0 || leftRefFrame0 <= 0) ? 1 : 0);
		int aboveIntra = ((availAbove == 0 || aboveRefFrame0 <= 0) ? 1 : 0);
		int ctx = 0;
		if (availAbove != 0 && availLeft != 0)
		{
			ctx = ((leftIntra != 0 && aboveIntra != 0) ? 3 : ((leftIntra != 0 || aboveIntra != 0) ? 1 : 0));
		}
		else if (availAbove != 0 || availLeft != 0)
		{
			ctx = 2 * ((availAbove != 0) ? ((aboveIntra != 0) ? 1 : 0) : ((leftIntra != 0) ? 1 : 0));
		}
		int[] probs = c.getIsInterProbs();
		return decoder.readBit(probs[ctx]) == 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 130, 161, 68, 99, 101, 145, 143, 137, 141 })]
	private InterModeInfo readInterIntraMode(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, int segmentId, bool skip, int txSize)
	{
		int subModes = 0;
		int yMode;
		if (blSz >= 3)
		{
			yMode = readInterIntraMode(miCol, miRow, blSz, decoder, c);
		}
		else
		{
			subModes = readInterIntraModeSub(miCol, miRow, blSz, decoder, c);
			yMode = subModes & 0xFF;
		}
		int uvMode = readKfUvMode(yMode, decoder, c);
		InterModeInfo result = new InterModeInfo(segmentId, skip, txSize, yMode, subModes, uvMode, 0L, 0L, 0L, 0L);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 129, 68, 145, 100, 109, 101, 175, 105,
		101, 175, 104, 100, 113, 191, 2, 113, 189, 114
	})]
	private InterModeInfo readInterInterMode(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, int segmentId, bool skip, int txSize)
	{
		int packedRefFrames = readRefFrames(miCol, miRow, blSz, segmentId, decoder, c);
		int lumaMode = 12;
		if (!c.isSegmentFeatureActive(segmentId, 3) && blSz >= 3)
		{
			lumaMode = readInterMode(miCol, miRow, blSz, decoder, c);
		}
		int interpFilter = c.getInterpFilter();
		if (interpFilter == 3)
		{
			interpFilter = readInterpFilter(miCol, miRow, blSz, decoder, c);
		}
		if (blSz < 3)
		{
			if (blSz == 0)
			{
				long[] mv4x4 = readMV4x4(miCol, miRow, blSz, decoder, c, packedRefFrames);
				InterModeInfo result = new InterModeInfo(segmentId, skip, txSize, -1, 0, -1, mv4x4[0], mv4x4[1], mv4x4[2], mv4x4[3]);
				
				return result;
			}
			long[] mv12 = readMvSub8x8(miCol, miRow, blSz, decoder, c, packedRefFrames);
			InterModeInfo result2 = new InterModeInfo(segmentId, skip, txSize, 0, 0, 0, mv12[0], mv12[1], 0L, 0L);
			
			return result2;
		}
		long mvl = readMV8x8AndAbove(miCol, miRow, blSz, decoder, c, packedRefFrames, lumaMode);
		InterModeInfo result3 = new InterModeInfo(segmentId, skip, txSize, lumaMode, 0, lumaMode, mvl, 0L, 0L, 0L);
		
		return result3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 70, 66, 110, 99, 112, 105, 102, 101, 110,
		100, 112, 106, 108, 100, 134, 100, 132, 99, 174,
		142
	})]
	private int readRefFrames(int miCol, int miRow, int blSz, int segmentId, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int ref0 = c.getSegmentFeature(segmentId, 2);
		int ref1 = 0;
		int compoundPred = 0;
		if (!c.isSegmentFeatureActive(segmentId, 2))
		{
			int refMode = c.getRefMode();
			compoundPred = ((refMode == 1) ? 1 : 0);
			if (refMode == 2)
			{
				compoundPred = (readRefMode(miCol, miRow, decoder, c) ? 1 : 0);
			}
			if (compoundPred != 0)
			{
				int compRef = readCompRef(miCol, miRow, blSz, decoder, c);
				int fixedRef = c.getCompFixedRef();
				if (c.refFrameSignBias(fixedRef) == 0)
				{
					ref0 = fixedRef;
					ref1 = compRef;
				}
				else
				{
					ref0 = compRef;
					ref1 = fixedRef;
				}
			}
			else
			{
				ref0 = readSingleRef(miCol, miRow, decoder, c);
			}
		}
		updateRefFrameLineBuffers(miCol, miRow, blSz, c, ref0, ref1, (byte)compoundPred != 0);
		int result = Packed4BitList._3(ref0, ref1, (compoundPred != 0) ? 1 : 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 238, 66, 107, 107, 105, 137, 111, 143, 109,
		109, 105, 103, 105, 103, 137, 137, 103, 109, 105,
		103, 105, 103, 137, 137, 103, 109, 102, 103, 102,
		103, 134, 166, 141, 127, 6, 138, 151, 111, 41,
		137, 111, 52, 169
	})]
	public virtual int readInterMode(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int ind0 = Consts.___003C_003Emv_ref_blocks_sm[blSz][0];
		int ind1 = Consts.___003C_003Emv_ref_blocks_sm[blSz][1];
		int[] leftModes = c.getLeftModes();
		int[] aboveModes = c.getAboveModes();
		int mode0 = getMode(leftModes, aboveModes, ind0, miRow, miCol, c);
		int mode1 = getMode(leftModes, aboveModes, ind1, miRow, miCol, c);
		int ctx;
		switch (mode0)
		{
		case 10:
		case 11:
			switch (mode1)
			{
			case 10:
			case 11:
				ctx = 2;
				break;
			case 13:
				ctx = 3;
				break;
			case 12:
				ctx = 1;
				break;
			default:
				ctx = 5;
				break;
			}
			break;
		case 12:
			switch (mode1)
			{
			case 10:
			case 11:
				ctx = 1;
				break;
			case 13:
				ctx = 3;
				break;
			case 12:
				ctx = 0;
				break;
			default:
				ctx = 5;
				break;
			}
			break;
		case 13:
			switch (mode1)
			{
			case 10:
			case 11:
				ctx = 3;
				break;
			case 13:
				ctx = 4;
				break;
			case 12:
				ctx = 3;
				break;
			default:
				ctx = 3;
				break;
			}
			break;
		default:
			ctx = ((mode1 < 10) ? 6 : 5);
			break;
		}
		java.lang.System.@out.println(String.format("inter_mode_ctx: %d\n", Integer.valueOf(ctx)));
		int[][] probs = c.getInterModeProbs();
		int ret = 10 + decoder.readTree(Consts.___003C_003ETREE_INTER_MODE, probs[ctx]);
		for (int j = 0; j < Consts.___003C_003EblW[blSz]; j++)
		{
			aboveModes[miCol + j] = ret;
		}
		for (int i = 0; i < Consts.___003C_003EblH[blSz]; i++)
		{
			int num = miRow + i;
			leftModes[(8 != -1) ? (num % 8) : 0] = ret;
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 247, 162, 106, 112, 105, 105, 108, 110, 106,
		138, 116, 114, 103, 103, 107, 103, 107, 135, 132,
		138, 148, 111, 42, 137, 111, 44, 169
	})]
	protected internal virtual int readInterpFilter(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		int[] aboveRefs = c.getAboveRefs();
		int[] leftRefs = c.getLeftRefs();
		int aboveRefFrame0 = getRef(aboveRefs[miCol], 0);
		int leftRefFrame0 = getRef(leftRefs[miRow & 7], 0);
		int[] leftInterpFilters = c.getLeftInterpFilters();
		int[] aboveInterpFilters = c.getAboveInterpFilters();
		int leftInterp = ((availLeft == 0 || leftRefFrame0 <= 0) ? 3 : leftInterpFilters[miRow & 7]);
		int aboveInterp = ((availAbove == 0 || aboveRefFrame0 <= 0) ? 3 : aboveInterpFilters[miCol]);
		int ctx = ((leftInterp == aboveInterp) ? leftInterp : ((leftInterp == 3 && aboveInterp != 3) ? aboveInterp : ((leftInterp == 3 || aboveInterp != 3) ? 3 : leftInterp)));
		int[][] probs = c.getInterpFilterProbs();
		int ret = decoder.readTree(Consts.___003C_003ETREE_INTERP_FILTER, probs[ctx]);
		for (int j = 0; j < Consts.___003C_003EblW[blSz]; j++)
		{
			aboveInterpFilters[miCol + j] = ret;
		}
		for (int i = 0; i < Consts.___003C_003EblH[blSz]; i++)
		{
			leftInterpFilters[(miRow + i) & 7] = ret;
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 162, 111, 145, 111, 147, 112, 149, 112,
		151, 109, 174
	})]
	protected internal virtual long[] readMV4x4(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, int packedRefFrames)
	{
		int subMode0 = readInterMode(miCol, miRow, blSz, decoder, c);
		long mvl0 = readSub0(miCol, miRow, blSz, decoder, c, subMode0, packedRefFrames);
		int subMode1 = readInterMode(miCol, miRow, blSz, decoder, c);
		long mvl1 = readSub12(miCol, miRow, blSz, decoder, c, mvl0, subMode1, 1, packedRefFrames);
		int subMode2 = readInterMode(miCol, miRow, blSz, decoder, c);
		long mvl2 = readSub12(miCol, miRow, blSz, decoder, c, mvl0, subMode2, 2, packedRefFrames);
		int subMode3 = readInterMode(miCol, miRow, blSz, decoder, c);
		long mvl3 = readMvSub3(miCol, miRow, blSz, decoder, c, mvl0, mvl1, mvl2, subMode3, packedRefFrames);
		updateMVLineBuffers(miCol, miRow, blSz, c, mvl3);
		updateMVLineBuffers4x4(miCol, miRow, blSz, c, mvl1, mvl2);
		return new long[4] { mvl0, mvl1, mvl2, mvl3 };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 97, 67, 114, 105, 105, 105, 105 })]
	public InterModeInfo(int segmentId, bool skip, int txSize, int yMode, int subModes, int uvMode, long mvl0, long mvl1, long mvl2, long mvl3)
		: base(segmentId, skip, txSize, yMode, subModes, uvMode)
	{
		this.mvl0 = mvl0;
		this.mvl1 = mvl1;
		this.mvl2 = mvl2;
		this.mvl3 = mvl3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 162, 111, 145, 111, 106, 148, 101, 144,
		142, 141
	})]
	protected internal virtual long[] readMvSub8x8(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, int packedRefFrames)
	{
		int subMode0 = readInterMode(miCol, miRow, blSz, decoder, c);
		long mvl0 = readSub0(miCol, miRow, blSz, decoder, c, subMode0, packedRefFrames);
		int subMode1 = readInterMode(miCol, miRow, blSz, decoder, c);
		int blk = ((blSz == 1) ? 1 : 2);
		long mvl1 = readSub12(miCol, miRow, blSz, decoder, c, mvl0, subMode1, blk, packedRefFrames);
		if (blSz == 1)
		{
			updateMVLineBuffers4x4(miCol, miRow, blSz, c, mvl1, mvl0);
		}
		else
		{
			updateMVLineBuffers4x4(miCol, miRow, blSz, c, mvl0, mvl1);
		}
		updateMVLineBuffers(miCol, miRow, blSz, c, mvl1);
		return new long[2] { mvl0, mvl1 };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 98, 146, 108, 109 })]
	protected internal virtual long readMV8x8AndAbove(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, int packedRefFrames, int lumaMode)
	{
		long mvl = readSub0(miCol, miRow, blSz, decoder, c, lumaMode, packedRefFrames);
		updateMVLineBuffers(miCol, miRow, blSz, c, mvl);
		updateMVLineBuffers4x4(miCol, miRow, blSz, c, mvl, mvl);
		return mvl;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 90, 66, 106, 106, 145, 111, 101, 100, 112,
		103, 103, 108, 100, 111, 103, 110, 175
	})]
	private static long readSub0(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, int lumaMode, int packedRefFrames)
	{
		int ref0 = Packed4BitList.get(packedRefFrames, 0);
		int ref1 = Packed4BitList.get(packedRefFrames, 1);
		int compoundPred = ((Packed4BitList.get(packedRefFrames, 2) == 1) ? 1 : 0);
		long nearestNearMv0 = findBestMv(miCol, miRow, blSz, ref0, 0, c, clearHp: true);
		long nearestNearMv = 0L;
		if (compoundPred != 0)
		{
			nearestNearMv = findBestMv(miCol, miRow, blSz, ref1, 0, c, clearHp: true);
		}
		int mv0 = 0;
		int mv1 = 0;
		switch (lumaMode)
		{
		case 13:
			mv0 = readDiffMv(decoder, c, nearestNearMv0);
			if (compoundPred != 0)
			{
				mv1 = readDiffMv(decoder, c, nearestNearMv);
			}
			break;
		default:
			mv0 = MVList.get(nearestNearMv0, lumaMode - 10);
			mv1 = MVList.get(nearestNearMv, lumaMode - 10);
			break;
		case 12:
			break;
		}
		long result = MVList.create(mv0, mv1);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 64, 162, 104, 104, 136, 115, 107, 107, 237,
		61, 231, 70, 117, 114, 109, 109, 238, 60, 236,
		71, 120, 111, 103, 109, 109, 233, 60, 41, 236,
		73, 120, 111, 114, 109, 109, 233, 60, 41, 236,
		72
	})]
	private static void updateMVLineBuffers(int miCol, int miRow, int blSz, DecodingContext c, long mv)
	{
		long[][] leftMVs = c.getLeftMVs();
		long[][] aboveMVs = c.getAboveMVs();
		long[][] aboveLeftMVs = c.getAboveLeftMVs();
		for (int l = 0; l < Math.max(3, Consts.___003C_003EblW[blSz]); l++)
		{
			aboveLeftMVs[2][l] = aboveLeftMVs[1][l];
			aboveLeftMVs[1][l] = aboveLeftMVs[0][l];
			aboveLeftMVs[0][l] = aboveMVs[l][miCol + l];
		}
		for (int k = 0; k < Math.max(3, Consts.___003C_003EblH[blSz]); k++)
		{
			int num = miRow + k;
			int offTop2 = ((8 != -1) ? (num % 8) : 0);
			aboveLeftMVs[k][2] = aboveLeftMVs[k][1];
			aboveLeftMVs[k][1] = aboveLeftMVs[k][0];
			aboveLeftMVs[k][0] = leftMVs[k][offTop2];
		}
		for (int n = 0; n < Math.max(3, Consts.___003C_003EblH[blSz]); n++)
		{
			for (int j = 0; j < Consts.___003C_003EblW[blSz]; j++)
			{
				int offLeft = miCol + j;
				aboveMVs[2][offLeft] = aboveMVs[1][offLeft];
				aboveMVs[1][offLeft] = aboveMVs[0][offLeft];
				aboveMVs[0][offLeft] = mv;
			}
		}
		for (int m = 0; m < Math.max(3, Consts.___003C_003EblW[blSz]); m++)
		{
			for (int i = 0; i < Consts.___003C_003EblH[blSz]; i++)
			{
				int num2 = miRow + i;
				int offTop = ((8 != -1) ? (num2 % 8) : 0);
				leftMVs[2][offTop] = leftMVs[1][offTop];
				leftMVs[1][offTop] = leftMVs[0][offTop];
				leftMVs[0][offTop] = mv;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 98, 104, 136, 102, 113 })]
	private static void updateMVLineBuffers4x4(int miCol, int miRow, int blSz, DecodingContext c, long mvLeft, long mvAbove)
	{
		long[] leftMVs = c.getLeft4x4MVs();
		long[] aboveMVs = c.getAbove4x4MVs();
		aboveMVs[miCol] = mvAbove;
		leftMVs[(8 != -1) ? (miRow % 8) : 0] = mvLeft;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 85, 162, 106, 106, 113, 102, 112, 101, 100,
		112, 103, 108, 103, 114, 106, 114, 38, 136, 101,
		100, 114, 38, 136, 110, 175
	})]
	private static long readSub12(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, long mvl0, int subMode1, int blk, int packedRefFrames)
	{
		int ref0 = Packed4BitList.get(packedRefFrames, 0);
		int ref1 = Packed4BitList.get(packedRefFrames, 1);
		int compoundPred = ((Packed4BitList.get(packedRefFrames, 2) == 1) ? 1 : 0);
		int mv10 = 0;
		int mv11 = 0;
		long nearestNearMv0 = findBestMv(miCol, miRow, blSz, ref0, 0, c, clearHp: true);
		long nearestNearMv = 0L;
		if (compoundPred != 0)
		{
			nearestNearMv = findBestMv(miCol, miRow, blSz, ref1, 0, c, clearHp: true);
		}
		switch (subMode1)
		{
		case 13:
			mv10 = readDiffMv(decoder, c, nearestNearMv0);
			if (compoundPred != 0)
			{
				mv11 = readDiffMv(decoder, c, nearestNearMv);
			}
			break;
		default:
		{
			long nearestNearMv2 = prepandSubMvBlk12(findBestMv(miCol, miRow, blSz, ref0, blk, c, clearHp: false), MVList.get(mvl0, 0));
			long nearestNearMv3 = 0L;
			if (compoundPred != 0)
			{
				nearestNearMv3 = prepandSubMvBlk12(findBestMv(miCol, miRow, blSz, ref1, blk, c, clearHp: false), MVList.get(mvl0, 1));
			}
			mv10 = MVList.get(nearestNearMv2, subMode1 - 10);
			mv11 = MVList.get(nearestNearMv3, subMode1 - 10);
			break;
		}
		case 12:
			break;
		}
		long result = MVList.create(mv10, mv11);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 78, 162, 106, 106, 145, 111, 101, 100, 112,
		103, 103, 108, 103, 114, 106, 113, 54, 136, 101,
		100, 113, 54, 136, 111, 175
	})]
	private static long readMvSub3(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, long mvl0, long mvl1, long mvl2, int subMode3, int packedRefFrames)
	{
		int ref0 = Packed4BitList.get(packedRefFrames, 0);
		int ref1 = Packed4BitList.get(packedRefFrames, 1);
		int compoundPred = ((Packed4BitList.get(packedRefFrames, 2) == 1) ? 1 : 0);
		long nearestNearMv0 = findBestMv(miCol, miRow, blSz, ref0, 0, c, clearHp: true);
		long nearestNearMv = 0L;
		if (compoundPred != 0)
		{
			nearestNearMv = findBestMv(miCol, miRow, blSz, ref1, 0, c, clearHp: true);
		}
		int mv30 = 0;
		int mv31 = 0;
		switch (subMode3)
		{
		case 13:
			mv30 = readDiffMv(decoder, c, nearestNearMv0);
			if (compoundPred != 0)
			{
				mv31 = readDiffMv(decoder, c, nearestNearMv);
			}
			break;
		default:
		{
			long nearestNearMv2 = prepandSubMvBlk3(findBestMv(miCol, miRow, blSz, ref0, 3, c, clearHp: false), MVList.get(mvl0, 0), MVList.get(mvl1, 0), MVList.get(mvl2, 0));
			long nearestNearMv3 = 0L;
			if (compoundPred != 0)
			{
				nearestNearMv3 = prepandSubMvBlk3(findBestMv(miCol, miRow, blSz, ref1, 3, c, clearHp: false), MVList.get(mvl0, 1), MVList.get(mvl1, 1), MVList.get(mvl2, 1));
			}
			mv30 = MVList.get(nearestNearMv2, subMode3 - 10);
			mv31 = MVList.get(nearestNearMv3, subMode3 - 10);
			break;
		}
		case 12:
			break;
		}
		long result = MVList.create(mv30, mv31);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 33, 97, 68, 105, 105, 105, 106, 106, 101,
		228, 69, 108, 108, 113, 209, 102, 126, 102, 177,
		150, 109, 205, 122, 121, 111, 237, 61, 233, 72,
		116, 106, 105, 237, 69, 111, 122, 121, 15, 233,
		72, 116, 106, 105, 207, 111, 100, 172
	})]
	public static long findBestMv(int miCol, int miRow, int blSz, int @ref, int blk, DecodingContext c, bool clearHp)
	{
		long[][] leftMVs = c.getLeftMVs();
		long[][] aboveMVs = c.getAboveMVs();
		long[][] aboveLeftMVs = c.getAboveLeftMVs();
		long[] left4x4MVs = c.getLeft4x4MVs();
		long[] above4x4MVs = c.getAbove4x4MVs();
		long list = 0L;
		int checkDifferentRef = 0;
		int pt0 = Consts.___003C_003Emv_ref_blocks[blSz][0];
		int pt1 = Consts.___003C_003Emv_ref_blocks[blSz][1];
		long mvp2 = getMV(leftMVs, aboveMVs, aboveLeftMVs, pt0, miRow, miCol, c);
		long mvp3 = getMV(leftMVs, aboveMVs, aboveLeftMVs, pt1, miRow, miCol, c);
		switch (blk)
		{
		case 1:
		{
			long num;
			if (mvp2 == -1)
			{
				num = -1L;
			}
			else
			{
				num = left4x4MVs[(8 != -1) ? (miRow % 8) : 0];
			}
			mvp2 = num;
			break;
		}
		case 2:
			mvp3 = ((mvp3 != -1) ? above4x4MVs[miCol] : (-1));
			break;
		}
		checkDifferentRef = ((mvp2 != 0u || mvp3 != 0u) ? 1 : 0);
		list = processCandidate(@ref, list, mvp2);
		list = processCandidate(@ref, list, mvp3);
		for (int j = 2; j < (nint)Consts.___003C_003Emv_ref_blocks[blSz].LongLength; j++)
		{
			if (MVList.size(list) >= 2)
			{
				break;
			}
			long mvi = getMV(leftMVs, aboveMVs, aboveLeftMVs, Consts.___003C_003Emv_ref_blocks[blSz][j], miRow, miCol, c);
			checkDifferentRef |= ((mvi != 0u) ? 1 : 0);
			list = processCandidate(@ref, list, mvi);
		}
		if (MVList.size(list) < 2 && c.isUsePrevFrameMvs())
		{
			long[][] prevFrameMv2 = c.getPrevFrameMv();
			long prevMv2 = prevFrameMv2[miCol][miRow];
			list = processCandidate(@ref, list, prevMv2);
		}
		if (MVList.size(list) < 2 && checkDifferentRef != 0)
		{
			for (int i = 0; i < (nint)Consts.___003C_003Emv_ref_blocks[blSz].LongLength; i++)
			{
				if (MVList.size(list) >= 2)
				{
					break;
				}
				long mvp = getMV(leftMVs, aboveMVs, aboveLeftMVs, Consts.___003C_003Emv_ref_blocks[blSz][i], miRow, miCol, c);
				list = processNECandidate(@ref, c, list, mvp);
			}
		}
		if (MVList.size(list) < 2 && c.isUsePrevFrameMvs())
		{
			long[][] prevFrameMv = c.getPrevFrameMv();
			long prevMv = prevFrameMv[miCol][miRow];
			list = processNECandidate(@ref, c, list, prevMv);
		}
		list = clampMvs(miCol, miRow, blSz, c, list);
		if (clearHp)
		{
			list = InterModeInfo.clearHp(c, list);
		}
		return list;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 45, 98, 105, 118, 147, 102, 105, 107, 105,
		140
	})]
	private static int readDiffMv(VPXBooleanDecoder decoder, DecodingContext c, long nearNearest)
	{
		int bestMv = MVList.get(nearNearest, 0);
		int useHp = ((c.isAllowHpMv() && !largeMv(bestMv)) ? 1 : 0);
		int joint = decoder.readTree(Consts.___003C_003ETREE_MV_JOINT, c.getMvJointProbs());
		int diffMv0 = 0;
		int diffMv1 = 0;
		if (joint == 2 || joint == 3)
		{
			diffMv0 = readMvComponent(decoder, c, 0, (byte)useHp != 0);
		}
		if (joint == 1 || joint == 3)
		{
			diffMv1 = readMvComponent(decoder, c, 1, (byte)useHp != 0);
		}
		int result = MV.create(MV.x(bestMv) + diffMv0, MV.y(bestMv) + diffMv1, MV.@ref(bestMv));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 0, 130, 132, 105, 111, 111 })]
	private static long prepandSubMvBlk12(long list, int blkMv)
	{
		long nlist = 0L;
		nlist = MVList.add(nlist, blkMv);
		nlist = MVList.addUniq(nlist, MVList.get(list, 0));
		return MVList.addUniq(nlist, MVList.get(list, 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 254, 162, 132, 105, 105, 105, 111, 143 })]
	private static long prepandSubMvBlk3(long list, int blk0Mv, int blk1Mv, int blk2Mv)
	{
		long nlist = 0L;
		nlist = MVList.add(nlist, blk2Mv);
		nlist = MVList.addUniq(nlist, blk1Mv);
		nlist = MVList.addUniq(nlist, blk0Mv);
		nlist = MVList.addUniq(nlist, MVList.get(list, 0));
		return MVList.addUniq(nlist, MVList.get(list, 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 140, 66, 106, 112, 105, 105, 114, 116, 106,
		108, 119, 108, 172, 109, 105, 123, 101, 120, 101,
		152, 105, 100, 101, 143, 102, 100, 101, 143, 134,
		132, 138
	})]
	protected internal virtual bool readRefMode(int miCol, int miRow, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		bool[] aboveCompound = c.getAboveCompound();
		bool[] leftCompound = c.getLeftCompound();
		int aboveRefFrame0 = getRef(c.getAboveRefs()[miCol], 0);
		int leftRefFrame0 = getRef(c.getLeftRefs()[miRow & 7], 0);
		int compFixedRef = c.getCompFixedRef();
		int aboveSingle = ((!aboveCompound[miCol]) ? 1 : 0);
		int leftSingle = ((!leftCompound[(8 != -1) ? (miRow % 8) : 0]) ? 1 : 0);
		int aboveIntra = ((aboveRefFrame0 <= 0) ? 1 : 0);
		int leftIntra = ((leftRefFrame0 <= 0) ? 1 : 0);
		int ctx = ((availAbove != 0 && availLeft != 0) ? ((aboveSingle != 0 && leftSingle != 0) ? (((aboveRefFrame0 == compFixedRef) ^ (leftRefFrame0 == compFixedRef)) ? 1 : 0) : ((aboveSingle != 0) ? (2 + ((aboveRefFrame0 == compFixedRef || aboveIntra != 0) ? 1 : 0)) : ((leftSingle == 0) ? 4 : (2 + ((leftRefFrame0 == compFixedRef || leftIntra != 0) ? 1 : 0))))) : ((availAbove != 0) ? ((aboveSingle == 0) ? 3 : ((aboveRefFrame0 == compFixedRef) ? 1 : 0)) : ((availLeft == 0) ? 1 : ((leftSingle == 0) ? 3 : ((leftRefFrame0 == compFixedRef) ? 1 : 0)))));
		int[] probs = c.getCompModeProb();
		return decoder.readBit(probs[ctx]) == 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 201, 130, 105, 106, 106, 112, 106, 106, 114,
		116, 114, 116, 108, 108, 109, 152, 100, 101, 135,
		101, 165, 107, 171, 109, 105, 105, 101, 101, 149,
		117, 101, 101, 149, 149, 109, 109, 109, 105, 105,
		119, 105, 103, 137, 105, 108, 109, 109, 109, 102,
		109, 134, 100, 105, 134, 132, 102, 100, 101, 137,
		101, 144, 144, 100, 101, 134, 101, 144, 176, 164,
		138
	})]
	protected internal virtual int readCompRef(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int compFixedRef = c.getCompFixedRef();
		int fixRefIdx = c.refFrameSignBias(compFixedRef);
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		bool[] aboveCompound = c.getAboveCompound();
		bool[] leftCompound = c.getLeftCompound();
		int aboveRefFrame0 = getRef(c.getAboveRefs()[miCol], 0);
		int leftRefFrame0 = getRef(c.getLeftRefs()[miRow & 7], 0);
		int aboveRefFrame1 = getRef(c.getAboveRefs()[miCol], 1);
		int leftRefFrame1 = getRef(c.getLeftRefs()[miRow & 7], 1);
		int aboveIntra = ((aboveRefFrame0 <= 0) ? 1 : 0);
		int leftIntra = ((leftRefFrame0 <= 0) ? 1 : 0);
		int aboveSingle = ((!aboveCompound[miCol]) ? 1 : 0);
		int leftSingle = ((!leftCompound[(8 != -1) ? (miRow % 8) : 0]) ? 1 : 0);
		int aboveVarRefFrame;
		int leftVarRefFrame;
		if (fixRefIdx == 0)
		{
			aboveVarRefFrame = aboveRefFrame1;
			leftVarRefFrame = leftRefFrame1;
		}
		else
		{
			aboveVarRefFrame = aboveRefFrame0;
			leftVarRefFrame = leftRefFrame0;
		}
		int compVarRef0 = c.getCompVarRef(0);
		int compVarRef1 = c.getCompVarRef(1);
		int ctx;
		if (availAbove == 0 || availLeft == 0)
		{
			ctx = ((availAbove != 0) ? ((aboveIntra != 0) ? 2 : ((aboveSingle == 0) ? (4 * ((aboveVarRefFrame != compVarRef1) ? 1 : 0)) : (3 * ((aboveRefFrame0 != compVarRef1) ? 1 : 0)))) : ((availLeft == 0) ? 2 : ((leftIntra != 0) ? 2 : ((leftSingle == 0) ? (4 * ((leftVarRefFrame != compVarRef1) ? 1 : 0)) : (3 * ((leftRefFrame0 != compVarRef1) ? 1 : 0))))));
		}
		else if (aboveIntra != 0 && leftIntra != 0)
		{
			ctx = 2;
		}
		else if (leftIntra != 0)
		{
			ctx = ((aboveSingle == 0) ? (1 + 2 * ((aboveVarRefFrame != compVarRef1) ? 1 : 0)) : (1 + 2 * ((aboveRefFrame0 != compVarRef1) ? 1 : 0)));
		}
		else if (aboveIntra != 0)
		{
			ctx = ((leftSingle == 0) ? (1 + 2 * ((leftVarRefFrame != compVarRef1) ? 1 : 0)) : (1 + 2 * ((leftRefFrame0 != compVarRef1) ? 1 : 0)));
		}
		else
		{
			int vrfa = ((aboveSingle == 0) ? aboveVarRefFrame : aboveRefFrame0);
			int vrfl = ((leftSingle == 0) ? leftVarRefFrame : leftRefFrame0);
			if (vrfa == vrfl && compVarRef1 == vrfa)
			{
				ctx = 0;
			}
			else if (leftSingle != 0 && aboveSingle != 0)
			{
				ctx = (((vrfa != compFixedRef || vrfl != compVarRef0) && (vrfl != compFixedRef || vrfa != compVarRef0)) ? ((vrfa != vrfl) ? 1 : 3) : 4);
			}
			else if (leftSingle != 0 || aboveSingle != 0)
			{
				int vrfc = ((leftSingle == 0) ? vrfl : vrfa);
				int rfs = ((aboveSingle == 0) ? vrfl : vrfa);
				ctx = ((vrfc == compVarRef1 && rfs != compVarRef1) ? 1 : ((rfs != compVarRef1 || vrfc == compVarRef1) ? 4 : 2));
			}
			else
			{
				ctx = ((vrfa != vrfl) ? 2 : 4);
			}
		}
		int[] probs = c.getCompRefProbs();
		int result = decoder.readBit(probs[ctx]);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 177, 98, 110, 100, 110, 137 })]
	protected internal virtual int readSingleRef(int miCol, int miRow, VPXBooleanDecoder decoder, DecodingContext c)
	{
		if (readSingRefBin(0, miCol, miRow, decoder, c))
		{
			return (!readSingRefBin(2, miCol, miRow, decoder, c)) ? 3 : 2;
		}
		return 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 49, 97, 68, 104, 104, 109, 39, 135, 111,
		42, 169, 111, 51, 169, 111, 53, 169
	})]
	private static void updateRefFrameLineBuffers(int miCol, int miRow, int blSz, DecodingContext c, int ref0, int ref1, bool compoundPred)
	{
		bool[] aboveCompound = c.getAboveCompound();
		bool[] leftCompound = c.getLeftCompound();
		for (int k = 0; k < Consts.___003C_003EblW[blSz]; k++)
		{
			aboveCompound[k + miCol] = compoundPred;
		}
		for (int j = 0; j < Consts.___003C_003EblH[blSz]; j++)
		{
			leftCompound[(miRow + j) & 7] = compoundPred;
		}
		for (int l = 0; l < Consts.___003C_003EblW[blSz]; l++)
		{
			c.getAboveRefs()[l] = @ref(ref0, ref1);
		}
		for (int i = 0; i < Consts.___003C_003EblH[blSz]; i++)
		{
			c.getLeftRefs()[i & 7] = @ref(ref0, ref1);
		}
	}

	[LineNumberTable(361)]
	public static int @ref(int ref0, int ref1)
	{
		return ((ref0 & 3) << 2) | (ref1 & 3);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(429)]
	private static bool largeMv(int mv)
	{
		return ((MV.x(mv) >= 64 || MV.x(mv) <= -64) && (MV.y(mv) >= 64 || MV.y(mv) <= -64)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 42, 161, 67, 111, 149, 100, 112, 120, 119,
		112, 102, 100, 105, 116, 14, 201, 107, 118, 119,
		148
	})]
	private static int readMvComponent(VPXBooleanDecoder decoder, DecodingContext c, int comp, bool useHp)
	{
		int sign = ((decoder.readBitEq() == 1) ? 1 : 0);
		int mvClass = decoder.readTree(Consts.___003C_003EMV_CLASS_TREE, c.getMvClassProbs()[comp]);
		int mag;
		if (mvClass == 0)
		{
			int mvClass0Bit = decoder.readBit(c.getMvClass0BitProbs()[comp]);
			int mvClass0Fr = decoder.readTree(Consts.___003C_003EMV_FR_TREE, c.getMvClass0FrProbs()[comp][mvClass0Bit]);
			int mvClass0Hp = ((!useHp) ? 1 : decoder.readBit(c.getMvClass0HpProbs()[comp]));
			mag = ((mvClass0Bit << 3) | (mvClass0Fr << 1) | mvClass0Hp) + 1;
		}
		else
		{
			int d = 0;
			for (int i = 0; i < mvClass; i++)
			{
				int mvBit = decoder.readBit(c.getMvBitsProb()[comp][i]);
				d |= mvBit << i;
			}
			mag = 2 << mvClass + 2;
			int mvFr = decoder.readTree(Consts.___003C_003EMV_FR_TREE, c.getMvFrProbs()[comp]);
			int mvHp = ((!useHp) ? 1 : decoder.readBit(c.getMvHpProbs()[comp]));
			mag += ((d << 3) | (mvFr << 1) | mvHp) + 1;
		}
		return (sign == 0) ? mag : (-mag);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 218, 130, 105, 105, 191, 61, 191, 3, 177,
		191, 12, 186, 191, 12, 186, 191, 12, 186, 191,
		12, 186, 191, 12, 187, 191, 5, 177, 191, 5,
		177, 189, 187, 189, 157
	})]
	private static long getMV(long[][] leftMV, long[][] aboveMV, long[][] aboveLeftMV, int ind0, int miRow, int miCol, DecodingContext c)
	{
		int th = c.getMiTileHeight();
		int tw = c.getMiTileWidth();
		switch (ind0)
		{
		case 0:
		{
			long result3;
			if (miCol >= c.getMiTileStartCol())
			{
				result3 = leftMV[0][(8 != -1) ? (miRow % 8) : 0];
			}
			else
			{
				result3 = 0L;
			}
			return result3;
		}
		case 1:
			return (miRow <= 0) ? 0u : aboveMV[0][miCol];
		case 2:
		{
			long result5;
			if (miCol >= c.getMiTileStartCol() && miRow < th - 1)
			{
				result5 = leftMV[0][((8 != -1) ? (miRow % 8) : 0) + 1];
			}
			else
			{
				result5 = 0L;
			}
			return result5;
		}
		case 3:
			return (miRow <= 0 || miCol >= tw - 1) ? 0u : aboveMV[0][miCol + 1];
		case 4:
		{
			long result2;
			if (miCol >= c.getMiTileStartCol() && miRow < th - 3)
			{
				result2 = leftMV[0][((8 != -1) ? (miRow % 8) : 0) + 3];
			}
			else
			{
				result2 = 0L;
			}
			return result2;
		}
		case 5:
			return (miRow <= 0 || miCol >= tw - 3) ? 0u : aboveMV[0][miCol + 3];
		case 6:
		{
			long result;
			if (miCol >= c.getMiTileStartCol() && miRow < th - 2)
			{
				result = leftMV[0][((8 != -1) ? (miRow % 8) : 0) + 2];
			}
			else
			{
				result = 0L;
			}
			return result;
		}
		case 7:
			return (miRow <= 0 || miCol >= tw - 2) ? 0u : aboveMV[0][miCol + 2];
		case 8:
		{
			long result6;
			if (miCol >= c.getMiTileStartCol() && miRow < th - 4)
			{
				result6 = leftMV[0][((8 != -1) ? (miRow % 8) : 0) + 4];
			}
			else
			{
				result6 = 0L;
			}
			return result6;
		}
		case 9:
			return (miRow <= 0 || miCol >= tw - 4) ? 0u : aboveMV[0][miCol + 4];
		case 10:
		{
			long result8;
			if (miCol >= c.getMiTileStartCol() && miRow < th - 6)
			{
				result8 = leftMV[0][((8 != -1) ? (miRow % 8) : 0) + 6];
			}
			else
			{
				result8 = 0L;
			}
			return result8;
		}
		case 11:
			return (miCol < c.getMiTileStartCol() || miRow <= 0) ? 0u : aboveLeftMV[0][0];
		case 12:
		{
			long result7;
			if (miCol >= c.getMiTileStartCol() + 1)
			{
				result7 = leftMV[1][(8 != -1) ? (miRow % 8) : 0];
			}
			else
			{
				result7 = 0L;
			}
			return result7;
		}
		case 13:
			return (miRow <= 1) ? 0u : aboveMV[1][miCol];
		case 14:
		{
			long result4;
			if (miCol >= c.getMiTileStartCol() + 2)
			{
				result4 = leftMV[2][(8 != -1) ? (miRow % 8) : 0];
			}
			else
			{
				result4 = 0L;
			}
			return result4;
		}
		case 15:
			return (miRow <= 2) ? 0u : aboveMV[2][miCol];
		case 16:
			return (miCol < c.getMiTileStartCol() + 1 || miRow <= 0) ? 0u : aboveLeftMV[0][1];
		case 17:
			return (miCol < c.getMiTileStartCol() || miRow <= 1) ? 0u : aboveLeftMV[1][0];
		case 18:
			return (miCol < c.getMiTileStartCol() + 1 || miRow <= 1) ? 0u : aboveLeftMV[1][1];
		case 19:
			return (miCol < c.getMiTileStartCol() + 2 || miRow <= 2) ? 0u : aboveLeftMV[2][2];
		default:
			return 0L;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 3, 162, 105, 105, 106, 108, 106, 138 })]
	private static long processCandidate(int @ref, long list, long mvp0)
	{
		int mv0 = MVList.get(mvp0, 0);
		int mv = MVList.get(mvp0, 1);
		if (MV.@ref(mv0) == @ref)
		{
			list = MVList.addUniq(list, mv0);
		}
		else if (MV.@ref(mv) == @ref)
		{
			list = MVList.addUniq(list, mv);
		}
		return list;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 8, 130, 105, 105, 127, 3, 108, 100, 108 })]
	private static long processNECandidate(int @ref, DecodingContext c, long list, long mvp)
	{
		int mv0 = MVList.get(mvp, 0);
		int mv1 = MVList.get(mvp, 1);
		int matchMv = ((MV.x(mv0) == MV.x(mv1) && MV.y(mv0) == MV.y(mv1)) ? 1 : 0);
		list = processNEComponent(@ref, c, list, mv0);
		if (matchMv == 0)
		{
			list = processNEComponent(@ref, c, list, mv1);
		}
		return list;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 11, 162, 106, 138, 112, 112, 113, 113 })]
	private static long clampMvs(int miCol, int miRow, int blSz, DecodingContext c, long list)
	{
		int mv0 = MVList.get(list, 0);
		int mv1 = MVList.get(list, 1);
		int mv0xCl = clampMvCol(miCol, blSz, c, MV.x(mv0));
		int mv0yCl = clampMvRow(miRow, blSz, c, MV.y(mv0));
		int mv1xCl = clampMvCol(miCol, blSz, c, MV.x(mv1));
		int mv1yCl = clampMvRow(miRow, blSz, c, MV.y(mv1));
		long result = MVList.create(MV.create(mv0xCl, mv0yCl, MV.@ref(mv0)), MV.create(mv1xCl, mv1yCl, MV.@ref(mv1)));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 14, 98, 105, 113, 159, 0, 105, 113, 191,
		0, 106
	})]
	private static long clearHp(DecodingContext c, long list)
	{
		int mv0 = MVList.get(list, 0);
		if (!c.isAllowHpMv() || largeMv(mv0))
		{
			mv0 = MV.create(MV.x(mv0) & -2, MV.y(mv0) & -2, MV.@ref(mv0));
		}
		int mv1 = MVList.get(list, 1);
		if (!c.isAllowHpMv() || largeMv(mv1))
		{
			mv1 = MV.create(MV.x(mv1) & -2, MV.y(mv1) & -2, MV.@ref(mv1));
		}
		list = MVList.create(mv0, mv1);
		return list;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 248, 98, 102, 116 })]
	private static int clampMvCol(int miCol, int blSz, DecodingContext c, int mv)
	{
		int mbToLeftEdge = -(miCol << 6);
		int mbToRightEdge = c.getMiFrameWidth() - Consts.___003C_003EblW[blSz] - miCol << 6;
		int result = clip3(mbToLeftEdge - 128, mbToRightEdge + 128, mv);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 251, 162, 102, 116 })]
	private static int clampMvRow(int miRow, int blSz, DecodingContext c, int mv)
	{
		int mbToTopEdge = -(miRow << 6);
		int mbToBottomEdge = c.getMiFrameHeight() - Consts.___003C_003EblH[blSz] - miRow << 6;
		int result = clip3(mbToTopEdge - 128, mbToBottomEdge + 128, mv);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 5, 66, 104, 136, 113, 118, 138 })]
	private static long processNEComponent(int @ref, DecodingContext c, long list, int mv0)
	{
		int ref2 = MV.@ref(mv0);
		if (ref2 != 0 && ref2 != @ref)
		{
			int q = c.refFrameSignBias(ref2) * c.refFrameSignBias(@ref);
			int mv = MV.create(MV.x(mv0) * q, MV.y(mv0), @ref);
			list = MVList.addUniq(list, mv);
		}
		return list;
	}

	[LineNumberTable(597)]
	private static int clip3(int from, int to, int v)
	{
		return (v < from) ? from : ((v <= to) ? v : to);
	}

	[LineNumberTable(new byte[] { 159, 51, 98, 100, 133 })]
	public static int getRef(int packed, int n)
	{
		if (n == 0)
		{
			return packed & 3;
		}
		return (packed >> 2) & 3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 225, 162, 191, 5, 191, 0, 174, 223, 14,
		189, 223, 14, 157
	})]
	private static int getMode(int[] leftModes, int[] aboveModes, int ind0, int miRow, int miCol, DecodingContext c)
	{
		switch (ind0)
		{
		case 0:
		{
			int result;
			if (miCol > c.getMiTileStartCol())
			{
				result = leftModes[(8 != -1) ? (miRow % 8) : 0];
			}
			else
			{
				result = 10;
			}
			return result;
		}
		case 1:
			return (miRow <= 0) ? 10 : aboveModes[miCol];
		case 2:
		{
			int result2;
			if (miCol > c.getMiTileStartCol() && miRow < c.getMiFrameHeight() - 1)
			{
				result2 = leftModes[((8 != -1) ? (miRow % 8) : 0) + 1];
			}
			else
			{
				result2 = 10;
			}
			return result2;
		}
		case 3:
			return (miCol >= c.getMiTileWidth() - 1 || miRow <= 0) ? 10 : aboveModes[miCol + 1];
		case 4:
		{
			int result3;
			if (miCol > c.getMiTileStartCol() && miRow < c.getMiFrameHeight() - 3)
			{
				result3 = leftModes[((8 != -1) ? (miRow % 8) : 0) + 3];
			}
			else
			{
				result3 = 10;
			}
			return result3;
		}
		case 5:
			return (miCol >= c.getMiTileWidth() - 3 || miRow <= 0) ? 10 : aboveModes[miCol + 3];
		default:
			return 10;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 175, 162, 106, 112, 105, 105, 114, 116, 114,
		116, 108, 108, 108, 183, 109, 105, 105, 104, 101,
		100, 143, 102, 137, 175, 100, 152, 154, 104, 101,
		100, 143, 102, 137, 175, 100, 152, 186, 111, 100,
		151, 107, 105, 102, 111, 102, 143, 215, 111, 100,
		191, 3, 109, 152, 169, 109, 109, 109, 100, 102,
		152, 150, 102, 117, 102, 147, 149, 134, 103, 114,
		169, 101, 100, 143, 143, 100, 152, 184, 135, 114,
		137, 101, 100, 140, 140, 100, 149, 213, 132, 138
	})]
	private bool readSingRefBin(int bin, int miCol, int miRow, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		bool[] aboveCompound = c.getAboveCompound();
		bool[] leftCompound = c.getLeftCompound();
		int aboveRefFrame0 = getRef(c.getAboveRefs()[miCol], 0);
		int leftRefFrame0 = getRef(c.getLeftRefs()[miRow & 7], 0);
		int aboveRefFrame1 = getRef(c.getAboveRefs()[miCol], 1);
		int leftRefFrame1 = getRef(c.getLeftRefs()[miRow & 7], 1);
		int aboveIntra = ((aboveRefFrame0 <= 0) ? 1 : 0);
		int leftIntra = ((leftRefFrame0 <= 0) ? 1 : 0);
		int aboveSingle = ((!aboveCompound[miCol]) ? 1 : 0);
		int leftSingle = ((!leftCompound[(8 != -1) ? (miRow % 8) : 0]) ? 1 : 0);
		int ctx;
		if (availAbove == 0 || availLeft == 0)
		{
			ctx = ((availAbove != 0) ? ((aboveIntra == 0 && (bin != 1 || aboveRefFrame0 != 1 || aboveSingle == 0)) ? ((aboveSingle != 0) ? ((bin != 0) ? (4 * ((aboveRefFrame0 == 3) ? 1 : 0)) : (4 * ((aboveRefFrame0 == 1) ? 1 : 0))) : ((bin != 0) ? (3 * ((aboveRefFrame0 == 3 || aboveRefFrame1 == 3) ? 1 : 0)) : (1 + ((aboveRefFrame0 == 1 || aboveRefFrame1 == 1) ? 1 : 0)))) : 2) : ((availLeft == 0) ? 2 : ((leftIntra == 0 && (bin != 1 || leftRefFrame0 != 1 || leftSingle == 0)) ? ((leftSingle != 0) ? ((bin != 0) ? (4 * ((leftRefFrame0 == 3) ? 1 : 0)) : (4 * ((leftRefFrame0 == 1) ? 1 : 0))) : ((bin != 0) ? (3 * ((leftRefFrame0 == 3 || leftRefFrame1 == 3) ? 1 : 0)) : (1 + ((leftRefFrame0 == 1 || leftRefFrame1 == 1) ? 1 : 0)))) : 2)));
		}
		else if (aboveIntra != 0 && leftIntra != 0)
		{
			ctx = 2;
		}
		else if (leftIntra != 0)
		{
			ctx = ((aboveSingle != 0) ? ((bin == 0) ? (4 * ((aboveRefFrame0 == 1) ? 1 : 0)) : ((aboveRefFrame0 != 1) ? (4 * ((aboveRefFrame0 == 3) ? 1 : 0)) : 3)) : ((bin != 0) ? (1 + 2 * ((aboveRefFrame0 == 3 || aboveRefFrame1 == 3) ? 1 : 0)) : (1 + ((aboveRefFrame0 == 1 || aboveRefFrame1 == 1) ? 1 : 0))));
		}
		else if (aboveIntra != 0)
		{
			ctx = ((leftSingle != 0) ? ((bin == 0) ? (4 * ((leftRefFrame0 == 1) ? 1 : 0)) : ((leftRefFrame0 != 1) ? (4 * ((leftRefFrame0 == 3) ? 1 : 0)) : 3)) : ((bin != 0) ? (1 + 2 * ((leftRefFrame0 == 3 || leftRefFrame1 == 3) ? 1 : 0)) : (1 + ((leftRefFrame0 == 1 || leftRefFrame1 == 1) ? 1 : 0))));
		}
		else if (aboveSingle != 0 && leftSingle != 0)
		{
			ctx = ((bin == 0) ? (2 * ((aboveRefFrame0 == 1) ? 1 : 0) + 2 * ((leftRefFrame0 == 1) ? 1 : 0)) : ((aboveRefFrame0 == 1 && leftRefFrame0 == 1) ? 3 : ((aboveRefFrame0 == 1) ? (4 * ((leftRefFrame0 == 3) ? 1 : 0)) : ((leftRefFrame0 != 1) ? (2 * ((aboveRefFrame0 == 3) ? 1 : 0) + 2 * ((leftRefFrame0 == 3) ? 1 : 0)) : (4 * ((aboveRefFrame0 == 3) ? 1 : 0))))));
		}
		else if (aboveSingle == 0 && leftSingle == 0)
		{
			ctx = ((bin == 0) ? (1 + ((aboveRefFrame0 == 1 || aboveRefFrame1 == 1 || leftRefFrame0 == 1 || leftRefFrame1 == 1) ? 1 : 0)) : ((aboveRefFrame0 != leftRefFrame0 || aboveRefFrame1 != leftRefFrame1) ? 2 : (3 * ((aboveRefFrame0 == 3 || aboveRefFrame1 == 3) ? 1 : 0))));
		}
		else
		{
			int rfs = ((aboveSingle == 0) ? leftRefFrame0 : aboveRefFrame0);
			int crf1 = ((aboveSingle == 0) ? aboveRefFrame0 : leftRefFrame0);
			int crf2 = ((aboveSingle == 0) ? aboveRefFrame1 : leftRefFrame1);
			ctx = ((bin == 0) ? ((rfs != 1) ? ((crf1 == 1 || crf2 == 1) ? 1 : 0) : (3 + ((crf1 == 1 || crf2 == 1) ? 1 : 0))) : (rfs switch
			{
				3 => 3 + ((crf1 == 3 || crf2 == 3) ? 1 : 0), 
				2 => (crf1 == 3 || crf2 == 3) ? 1 : 0, 
				_ => 1 + 2 * ((crf1 == 3 || crf2 == 3) ? 1 : 0), 
			}));
		}
		int[][] probs = c.getSingleRefProbs();
		return decoder.readBit(probs[ctx][bin]) == 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 126, 98, 105 })]
	protected internal virtual int readInterIntraMode(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int[][] probs = c.getYModeProbs();
		int result = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[Consts.___003C_003Esize_group_lookup[blSz]]);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 125, 130, 105, 113, 113, 113, 146 })]
	protected internal virtual int readInterIntraModeSub(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int[][] probs = c.getYModeProbs();
		int mode0 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[0]);
		int mode1 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[0]);
		int mode2 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[0]);
		int mode3 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[0]);
		int result = ModeInfo.vect4(mode0, mode1, mode2, mode3);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 122, 66, 104 })]
	public virtual int readKfUvMode(int yMode, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int[][] probs = c.getUvModeProbs();
		int result = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[yMode]);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 65, 67, 114 })]
	public InterModeInfo(int segmentId, bool skip, int txSize, int yMode, int subModes, int uvMode)
		: base(segmentId, skip, txSize, yMode, subModes, uvMode)
	{
	}

	[LineNumberTable(78)]
	public override bool isInter()
	{
		return true;
	}

	[LineNumberTable(82)]
	public virtual long getMvl0()
	{
		return mvl0;
	}

	[LineNumberTable(86)]
	public virtual long getMvl1()
	{
		return mvl1;
	}

	[LineNumberTable(90)]
	public virtual long getMvl2()
	{
		return mvl2;
	}

	[LineNumberTable(94)]
	public virtual long getMvl3()
	{
		return mvl3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LineNumberTable(52)]
	public virtual ModeInfo _003Cbridge_003Eread(int i1, int i2, int i3, VPXBooleanDecoder vpxbd, DecodingContext dc)
	{
		InterModeInfo result = read(i1, i2, i3, vpxbd, dc);
		
		return result;
	}
}
