using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode.deblock;

public class DeblockingFilter : Object
{
	public static int[] alphaTab;

	public static int[] betaTab;

	public static int[][] tcs;

	private DeblockerInput di;

	internal static int[] inverse;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 162, 111, 143, 110, 141, 127, 7, 159,
		11, 104, 127, 15, 108, 105, 134, 127, 33, 120,
		127, 14, 234, 61, 231, 60, 236, 76, 108, 108,
		105, 137, 127, 34, 120, 127, 14, 232, 61, 231,
		60, 44, 236, 75
	})]
	private void calcBsH(Picture pic, int mbAddr, int[][] bs)
	{
		SliceHeader sh = di.shs[mbAddr];
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		int mbX = ((mbWidth != -1) ? (mbAddr % mbWidth) : 0);
		int mbY = ((mbWidth != -1) ? (mbAddr / mbWidth) : (-mbAddr));
		int topAvailable = ((mbY > 0 && (sh.disableDeblockingFilterIdc != 2 || di.shs[mbAddr - mbWidth] == sh)) ? 1 : 0);
		int thisIntra = ((di.mbTypes[mbAddr] != null && di.mbTypes[mbAddr].isIntra()) ? 1 : 0);
		if (topAvailable != 0)
		{
			int topIntra = ((di.mbTypes[mbAddr - mbWidth] != null && di.mbTypes[mbAddr - mbWidth].isIntra()) ? 1 : 0);
			for (int blkX2 = 0; blkX2 < 4; blkX2++)
			{
				int thisBlkX2 = (mbX << 2) + blkX2;
				int thisBlkY2 = mbY << 2;
				bs[0][blkX2] = calcBoundaryStrenth(atMbBoundary: true, (byte)topIntra != 0, (byte)thisIntra != 0, di.nCoeff[thisBlkY2][thisBlkX2], di.nCoeff[thisBlkY2 - 1][thisBlkX2], di.mvs.getMv(thisBlkX2, thisBlkY2, 0), di.mvs.getMv(thisBlkX2, thisBlkY2 - 1, 0), di.mvs.getMv(thisBlkX2, thisBlkY2, 1), di.mvs.getMv(thisBlkX2, thisBlkY2 - 1, 1), mbAddr, mbAddr - mbWidth);
			}
		}
		for (int blkY = 1; blkY < 4; blkY++)
		{
			for (int blkX = 0; blkX < 4; blkX++)
			{
				int thisBlkX = (mbX << 2) + blkX;
				int thisBlkY = (mbY << 2) + blkY;
				bs[blkY][blkX] = calcBoundaryStrenth(atMbBoundary: false, (byte)thisIntra != 0, (byte)thisIntra != 0, di.nCoeff[thisBlkY][thisBlkX], di.nCoeff[thisBlkY - 1][thisBlkX], di.mvs.getMv(thisBlkX, thisBlkY, 0), di.mvs.getMv(thisBlkX, thisBlkY - 1, 0), di.mvs.getMv(thisBlkX, thisBlkY, 1), di.mvs.getMv(thisBlkX, thisBlkY - 1, 1), mbAddr, mbAddr);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 82, 66, 111, 143, 110, 141, 127, 7, 159,
		11, 104, 127, 15, 108, 102, 105, 127, 33, 120,
		127, 14, 234, 61, 231, 61, 236, 74, 108, 108,
		105, 105, 127, 34, 120, 127, 14, 232, 61, 231,
		61, 44, 236, 74
	})]
	private void calcBsV(Picture pic, int mbAddr, int[][] bs)
	{
		SliceHeader sh = di.shs[mbAddr];
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		int mbX = ((mbWidth != -1) ? (mbAddr % mbWidth) : 0);
		int mbY = ((mbWidth != -1) ? (mbAddr / mbWidth) : (-mbAddr));
		int leftAvailable = ((mbX > 0 && (sh.disableDeblockingFilterIdc != 2 || di.shs[mbAddr - 1] == sh)) ? 1 : 0);
		int thisIntra = ((di.mbTypes[mbAddr] != null && di.mbTypes[mbAddr].isIntra()) ? 1 : 0);
		if (leftAvailable != 0)
		{
			int leftIntra = ((di.mbTypes[mbAddr - 1] != null && di.mbTypes[mbAddr - 1].isIntra()) ? 1 : 0);
			for (int blkY2 = 0; blkY2 < 4; blkY2++)
			{
				int thisBlkX2 = mbX << 2;
				int thisBlkY2 = (mbY << 2) + blkY2;
				bs[blkY2][0] = calcBoundaryStrenth(atMbBoundary: true, (byte)leftIntra != 0, (byte)thisIntra != 0, di.nCoeff[thisBlkY2][thisBlkX2], di.nCoeff[thisBlkY2][thisBlkX2 - 1], di.mvs.getMv(thisBlkX2, thisBlkY2, 0), di.mvs.getMv(thisBlkX2 - 1, thisBlkY2, 0), di.mvs.getMv(thisBlkX2, thisBlkY2, 1), di.mvs.getMv(thisBlkX2 - 1, thisBlkY2, 1), mbAddr, mbAddr - 1);
			}
		}
		for (int blkX = 1; blkX < 4; blkX++)
		{
			for (int blkY = 0; blkY < 4; blkY++)
			{
				int thisBlkX = (mbX << 2) + blkX;
				int thisBlkY = (mbY << 2) + blkY;
				bs[blkY][blkX] = calcBoundaryStrenth(atMbBoundary: false, (byte)thisIntra != 0, (byte)thisIntra != 0, di.nCoeff[thisBlkY][thisBlkX], di.nCoeff[thisBlkY][thisBlkX - 1], di.mvs.getMv(thisBlkX, thisBlkY, 0), di.mvs.getMv(thisBlkX - 1, thisBlkY, 0), di.mvs.getMv(thisBlkX, thisBlkY, 1), di.mvs.getMv(thisBlkX - 1, thisBlkY, 1), mbAddr, mbAddr);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 74, 162, 111, 143, 106, 138, 111, 142, 127,
		8, 146, 114, 114, 104, 116, 108, 105, 103, 106,
		127, 0, 52, 230, 61, 236, 71, 158, 108, 108,
		102, 108, 106, 106, 127, 0, 53, 230, 61, 236,
		61, 236, 74
	})]
	private void fillVerticalEdge(Picture pic, int comp, int mbAddr, int[][] bs)
	{
		SliceHeader sh = di.shs[mbAddr];
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		int alpha = sh.sliceAlphaC0OffsetDiv2 << 1;
		int beta = sh.sliceBetaOffsetDiv2 << 1;
		int mbX = ((mbWidth != -1) ? (mbAddr % mbWidth) : 0);
		int mbY = ((mbWidth != -1) ? (mbAddr / mbWidth) : (-mbAddr));
		int leftAvailable = ((mbX > 0 && (sh.disableDeblockingFilterIdc != 2 || di.shs[mbAddr - 1] == sh)) ? 1 : 0);
		int curQp = di.mbQps[comp][mbAddr];
		int cW = 2 - pic.getColor().compWidth[comp];
		int cH = 2 - pic.getColor().compHeight[comp];
		if (leftAvailable != 0)
		{
			int leftQp = di.mbQps[comp][mbAddr - 1];
			int avgQpV = leftQp + curQp + 1 >> 1;
			for (int blkY2 = 0; blkY2 < 4; blkY2++)
			{
				int thisBlkX2 = mbX << 2;
				int thisBlkY2 = (mbY << 2) + blkY2;
				filterBlockEdgeVert(pic, comp, thisBlkX2 << cW, thisBlkY2 << cH, getIdxAlpha(alpha, avgQpV), getIdxBeta(beta, avgQpV), bs[blkY2][0], 1 << cH);
			}
		}
		int skip4x4 = (((comp == 0 && di.tr8x8Used[mbAddr]) || cW == 1) ? 1 : 0);
		for (int blkX = 1; blkX < 4; blkX++)
		{
			if (skip4x4 == 0 || (blkX & 1) != 1)
			{
				for (int blkY = 0; blkY < 4; blkY++)
				{
					int thisBlkX = (mbX << 2) + blkX;
					int thisBlkY = (mbY << 2) + blkY;
					filterBlockEdgeVert(pic, comp, thisBlkX << cW, thisBlkY << cH, getIdxAlpha(alpha, curQp), getIdxBeta(beta, curQp), bs[blkY][blkX], 1 << cH);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 66, 111, 143, 106, 138, 111, 142, 127,
		8, 146, 114, 114, 104, 116, 108, 105, 106, 135,
		127, 0, 52, 230, 60, 236, 73, 158, 108, 108,
		134, 108, 106, 138, 127, 0, 53, 230, 60, 236,
		60, 236, 76
	})]
	private void fillHorizontalEdge(Picture pic, int comp, int mbAddr, int[][] bs)
	{
		SliceHeader sh = di.shs[mbAddr];
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		int alpha = sh.sliceAlphaC0OffsetDiv2 << 1;
		int beta = sh.sliceBetaOffsetDiv2 << 1;
		int mbX = ((mbWidth != -1) ? (mbAddr % mbWidth) : 0);
		int mbY = ((mbWidth != -1) ? (mbAddr / mbWidth) : (-mbAddr));
		int topAvailable = ((mbY > 0 && (sh.disableDeblockingFilterIdc != 2 || di.shs[mbAddr - mbWidth] == sh)) ? 1 : 0);
		int curQp = di.mbQps[comp][mbAddr];
		int cW = 2 - pic.getColor().compWidth[comp];
		int cH = 2 - pic.getColor().compHeight[comp];
		if (topAvailable != 0)
		{
			int topQp = di.mbQps[comp][mbAddr - mbWidth];
			int avgQp = topQp + curQp + 1 >> 1;
			for (int blkX2 = 0; blkX2 < 4; blkX2++)
			{
				int thisBlkX2 = (mbX << 2) + blkX2;
				int thisBlkY2 = mbY << 2;
				filterBlockEdgeHoris(pic, comp, thisBlkX2 << cW, thisBlkY2 << cH, getIdxAlpha(alpha, avgQp), getIdxBeta(beta, avgQp), bs[0][blkX2], 1 << cW);
			}
		}
		int skip4x4 = (((comp == 0 && di.tr8x8Used[mbAddr]) || cH == 1) ? 1 : 0);
		for (int blkY = 1; blkY < 4; blkY++)
		{
			if (skip4x4 == 0 || (blkY & 1) != 1)
			{
				for (int blkX = 0; blkX < 4; blkX++)
				{
					int thisBlkX = (mbX << 2) + blkX;
					int thisBlkY = (mbY << 2) + blkY;
					filterBlockEdgeHoris(pic, comp, thisBlkX << cW, thisBlkY << cH, getIdxAlpha(alpha, curQp), getIdxBeta(beta, curQp), bs[blkY][blkX], 1 << cW);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(147)]
	private bool mvThresh(int v0, int v1)
	{
		return (Math.abs(H264Utils.Mv.mvX(v0) - H264Utils.Mv.mvX(v1)) >= 4 || Math.abs(H264Utils.Mv.mvY(v0) - H264Utils.Mv.mvY(v1)) >= 4) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 161, 71, 106, 99, 103, 163, 107, 131,
		127, 0, 159, 1, 102, 131, 127, 9, 159, 9,
		127, 9, 159, 9, 159, 18, 131, 115, 106, 63,
		17, 162, 109, 127, 7, 109, 223, 7
	})]
	private int calcBoundaryStrenth(bool atMbBoundary, bool leftIntra, bool rightIntra, int leftCoeff, int rightCoeff, int mvA0, int mvB0, int mvA1, int mvB1, int mbAddrA, int mbAddrB)
	{
		if (atMbBoundary && (leftIntra || rightIntra))
		{
			return 4;
		}
		if (leftIntra || rightIntra)
		{
			return 3;
		}
		if (leftCoeff > 0 || rightCoeff > 0)
		{
			return 2;
		}
		int nA = ((H264Utils.Mv.mvRef(mvA0) != -1) ? 1 : 0) + ((H264Utils.Mv.mvRef(mvA1) != -1) ? 1 : 0);
		int nB = ((H264Utils.Mv.mvRef(mvB0) != -1) ? 1 : 0) + ((H264Utils.Mv.mvRef(mvB1) != -1) ? 1 : 0);
		if (nA != nB)
		{
			return 1;
		}
		org.jcodec.codecs.h264.io.model.Frame ra0 = ((H264Utils.Mv.mvRef(mvA0) >= 0) ? di.refsUsed[mbAddrA][0][H264Utils.Mv.mvRef(mvA0)] : null);
		org.jcodec.codecs.h264.io.model.Frame ra1 = ((H264Utils.Mv.mvRef(mvA1) >= 0) ? di.refsUsed[mbAddrA][1][H264Utils.Mv.mvRef(mvA1)] : null);
		org.jcodec.codecs.h264.io.model.Frame rb0 = ((H264Utils.Mv.mvRef(mvB0) >= 0) ? di.refsUsed[mbAddrB][0][H264Utils.Mv.mvRef(mvB0)] : null);
		org.jcodec.codecs.h264.io.model.Frame rb1 = ((H264Utils.Mv.mvRef(mvB1) >= 0) ? di.refsUsed[mbAddrB][1][H264Utils.Mv.mvRef(mvB1)] : null);
		if ((ra0 != rb0 && ra0 != rb1) || (ra1 != rb0 && ra1 != rb1) || (rb0 != ra0 && rb0 != ra1) || (rb1 != ra0 && rb1 != ra1))
		{
			return 1;
		}
		if (ra0 == ra1 && ra1 == rb0 && rb0 == rb1)
		{
			return (ra0 != null && (mvThresh(mvA0, mvB0) || mvThresh(mvA1, mvB0) || mvThresh(mvA0, mvB1) || mvThresh(mvA1, mvB1))) ? 1 : 0;
		}
		if (ra0 == rb0 && ra1 == rb1)
		{
			return ((ra0 != null && mvThresh(mvA0, mvB0)) || (ra1 != null && mvThresh(mvA1, mvB1))) ? 1 : 0;
		}
		if (ra0 == rb1 && ra1 == rb0)
		{
			return ((ra0 != null && mvThresh(mvA0, mvB1)) || (ra1 != null && mvThresh(mvA1, mvB0))) ? 1 : 0;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(155)]
	private static int getIdxAlpha(int sliceAlphaC0Offset, int avgQp)
	{
		int result = MathUtil.clip(avgQp + sliceAlphaC0Offset, 0, 51);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(151)]
	private static int getIdxBeta(int sliceBetaOffset, int avgQp)
	{
		int result = MathUtil.clip(avgQp + sliceBetaOffset, 0, 51);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		63,
		66,
		105,
		136,
		107,
		105,
		106,
		104,
		102,
		104,
		138,
		102,
		106,
		138,
		159,
		15,
		136,
		byte.MaxValue,
		13,
		48,
		234,
		84
	})]
	private void filterBlockEdgeHoris(Picture pic, int comp, int x, int y, int indexAlpha, int indexBeta, int bs, int blkW)
	{
		int stride = pic.getPlaneWidth(comp);
		int offset = y * stride + x;
		for (int pixOff = 0; pixOff < blkW; pixOff++)
		{
			int p2Idx = offset - 3 * stride + pixOff;
			int p1Idx = offset - 2 * stride + pixOff;
			int p0Idx = offset - stride + pixOff;
			int q0Idx = offset + pixOff;
			int q1Idx = offset + stride + pixOff;
			int q2Idx = offset + 2 * stride + pixOff;
			if (bs == 4)
			{
				int p3Idx = offset - 4 * stride + pixOff;
				int q3Idx = offset + 3 * stride + pixOff;
				filterBs4(indexAlpha, indexBeta, pic.getPlaneData(comp), pic.getPlaneData(comp), p3Idx, p2Idx, p1Idx, p0Idx, q0Idx, q1Idx, q2Idx, q3Idx, (comp != 0) ? true : false);
			}
			else if (bs > 0)
			{
				filterBs(bs, indexAlpha, indexBeta, pic.getPlaneData(comp), pic.getPlaneData(comp), p2Idx, p1Idx, p0Idx, q0Idx, q1Idx, q2Idx, (comp != 0) ? true : false);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		56,
		66,
		105,
		107,
		106,
		101,
		102,
		102,
		100,
		102,
		134,
		102,
		102,
		102,
		159,
		15,
		104,
		byte.MaxValue,
		13,
		49,
		234,
		83
	})]
	private void filterBlockEdgeVert(Picture pic, int comp, int x, int y, int indexAlpha, int indexBeta, int bs, int blkH)
	{
		int stride = pic.getPlaneWidth(comp);
		for (int i = 0; i < blkH; i++)
		{
			int offsetQ = (y + i) * stride + x;
			int p2Idx = offsetQ - 3;
			int p1Idx = offsetQ - 2;
			int p0Idx = offsetQ - 1;
			int q0Idx = offsetQ;
			int q1Idx = offsetQ + 1;
			int q2Idx = offsetQ + 2;
			if (bs == 4)
			{
				int p3Idx = offsetQ - 4;
				int q3Idx = offsetQ + 3;
				filterBs4(indexAlpha, indexBeta, pic.getPlaneData(comp), pic.getPlaneData(comp), p3Idx, p2Idx, p1Idx, p0Idx, q0Idx, q1Idx, q2Idx, q3Idx, (comp != 0) ? true : false);
			}
			else if (bs > 0)
			{
				filterBs(bs, indexAlpha, indexBeta, pic.getPlaneData(comp), pic.getPlaneData(comp), p2Idx, p1Idx, p0Idx, q0Idx, q1Idx, q2Idx, (comp != 0) ? true : false);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 34, 97, 68, 102, 102, 102, 135, 106, 138,
		159, 13, 101, 194, 100, 100, 137, 110, 142, 125,
		221, 104, 103, 135, 120, 111, 118, 113, 113, 113,
		99, 111, 177, 110, 103, 103, 120, 112, 119, 113,
		113, 113, 99, 111, 145
	})]
	public static void filterBs4(int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p3Idx, int p2Idx, int p1Idx, int p0Idx, int q0Idx, int q1Idx, int q2Idx, int q3Idx, bool isChroma)
	{
		int p0 = pelsP[p0Idx];
		int q0 = pelsQ[q0Idx];
		int p1 = pelsP[p1Idx];
		int q1 = pelsQ[q1Idx];
		int alphaThresh = alphaTab[indexAlpha];
		int betaThresh = betaTab[indexBeta];
		if ((Math.abs(p0 - q0) < alphaThresh && Math.abs(p1 - p0) < betaThresh && Math.abs(q1 - q0) < betaThresh) ? true : false)
		{
			int conditionP;
			int conditionQ;
			if (isChroma)
			{
				conditionP = 0;
				conditionQ = 0;
			}
			else
			{
				int ap = Math.abs(pelsP[p2Idx] - p0);
				int aq = Math.abs(pelsQ[q2Idx] - q0);
				conditionP = ((ap < betaThresh && Math.abs(p0 - q0) < (alphaThresh >> 2) + 2) ? 1 : 0);
				conditionQ = ((aq < betaThresh && Math.abs(p0 - q0) < (alphaThresh >> 2) + 2) ? 1 : 0);
			}
			if (conditionP != 0)
			{
				int p3 = pelsP[p3Idx];
				int p2 = pelsP[p2Idx];
				int p0n2 = p2 + 2 * p1 + 2 * p0 + 2 * q0 + q1 + 4 >> 3;
				int p1n = p2 + p1 + p0 + q0 + 2 >> 2;
				int p2n = 2 * p3 + 3 * p2 + p1 + p0 + q0 + 4 >> 3;
				pelsP[p0Idx] = (byte)(sbyte)MathUtil.clip(p0n2, -128, 127);
				pelsP[p1Idx] = (byte)(sbyte)MathUtil.clip(p1n, -128, 127);
				pelsP[p2Idx] = (byte)(sbyte)MathUtil.clip(p2n, -128, 127);
			}
			else
			{
				int p0n = 2 * p1 + p0 + q1 + 2 >> 2;
				pelsP[p0Idx] = (byte)(sbyte)MathUtil.clip(p0n, -128, 127);
			}
			if (conditionQ != 0 && !isChroma)
			{
				int q2 = pelsQ[q2Idx];
				int q3 = pelsQ[q3Idx];
				int q0n2 = p1 + 2 * p0 + 2 * q0 + 2 * q1 + q2 + 4 >> 3;
				int q1n = p0 + q0 + q1 + q2 + 2 >> 2;
				int q2n = 2 * q3 + 3 * q2 + q1 + q0 + p0 + 4 >> 3;
				pelsQ[q0Idx] = (byte)(sbyte)MathUtil.clip(q0n2, -128, 127);
				pelsQ[q1Idx] = (byte)(sbyte)MathUtil.clip(q1n, -128, 127);
				pelsQ[q2Idx] = (byte)(sbyte)MathUtil.clip(q2n, -128, 127);
			}
			else
			{
				int q0n = 2 * q1 + q0 + p1 + 2 >> 2;
				pelsQ[q0Idx] = (byte)(sbyte)MathUtil.clip(q0n, -128, 127);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 50, 97, 68, 102, 102, 103, 136, 106, 138,
		159, 13, 101, 226, 69, 206, 103, 110, 111, 115,
		109, 109, 99, 103, 100, 164, 113, 155, 103, 111,
		103, 143, 104, 135, 115, 123, 103, 177, 104, 104,
		116, 123, 104, 178, 114, 145
	})]
	public static void filterBs(int bs, int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p2Idx, int p1Idx, int p0Idx, int q0Idx, int q1Idx, int q2Idx, bool isChroma)
	{
		int p1 = pelsP[p1Idx];
		int p0 = pelsP[p0Idx];
		int q0 = pelsQ[q0Idx];
		int q1 = pelsQ[q1Idx];
		int alphaThresh = alphaTab[indexAlpha];
		int betaThresh = betaTab[indexBeta];
		if ((Math.abs(p0 - q0) < alphaThresh && Math.abs(p1 - p0) < betaThresh && Math.abs(q1 - q0) < betaThresh) ? true : false)
		{
			int tC2 = tcs[bs - 1][indexAlpha];
			int tC;
			int conditionP;
			int conditionQ;
			if (!isChroma)
			{
				int ap = Math.abs(pelsP[p2Idx] - p0);
				int aq = Math.abs(pelsQ[q2Idx] - q0);
				tC = tC2 + ((ap < betaThresh) ? 1 : 0) + ((aq < betaThresh) ? 1 : 0);
				conditionP = ((ap < betaThresh) ? 1 : 0);
				conditionQ = ((aq < betaThresh) ? 1 : 0);
			}
			else
			{
				tC = tC2 + 1;
				conditionP = 0;
				conditionQ = 0;
			}
			int sigma = (q0 - p0 << 2) + (p1 - q1) + 4 >> 3;
			sigma = ((sigma < -tC) ? (-tC) : ((sigma <= tC) ? sigma : tC));
			int p0n = p0 + sigma;
			p0n = ((p0n >= -128) ? p0n : (-128));
			int q0n = q0 - sigma;
			q0n = ((q0n >= -128) ? q0n : (-128));
			if (conditionP != 0)
			{
				int p2 = pelsP[p2Idx];
				int diff2 = p2 + (p0 + q0 + 1 >> 1) - (p1 << 1) >> 1;
				diff2 = ((diff2 < -tC2) ? (-tC2) : ((diff2 <= tC2) ? diff2 : tC2));
				int p1n = p1 + diff2;
				pelsP[p1Idx] = (byte)(sbyte)MathUtil.clip(p1n, -128, 127);
			}
			if (conditionQ != 0)
			{
				int q2 = pelsQ[q2Idx];
				int diff = q2 + (p0 + q0 + 1 >> 1) - (q1 << 1) >> 1;
				diff = ((diff < -tC2) ? (-tC2) : ((diff <= tC2) ? diff : tC2));
				int q1n = q1 + diff;
				pelsQ[q1Idx] = (byte)(sbyte)MathUtil.clip(q1n, -128, 127);
			}
			pelsQ[q0Idx] = (byte)(sbyte)MathUtil.clip(q0n, -128, 127);
			pelsP[p0Idx] = (byte)(sbyte)MathUtil.clip(p0n, -128, 127);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 130, 105, 104 })]
	public DeblockingFilter(int bitDepthLuma, int bitDepthChroma, DeblockerInput di)
	{
		this.di = di;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 162, 232, 70, 127, 43, 119, 108, 107,
		110, 109, 14, 233, 61, 236, 77
	})]
	public virtual void deblockFrame(Picture result)
	{
		ColorSpace color = result.getColor();
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = 4);
		int[][] bsV = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 4);
		int[][] bsH = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < (nint)di.shs.LongLength; i++)
		{
			calcBsH(result, i, bsH);
			calcBsV(result, i, bsV);
			for (int c = 0; c < color.nComp; c++)
			{
				fillVerticalEdge(result, c, i, bsV);
				fillHorizontalEdge(result, c, i, bsH);
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		132,
		66,
		191,
		160,
		220,
		191,
		160,
		189,
		byte.MaxValue,
		162,
		208,
		120
	})]
	static DeblockingFilter()
	{
		alphaTab = new int[52]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 4, 4, 5, 6,
			7, 8, 9, 10, 12, 13, 15, 17, 20, 22,
			25, 28, 32, 36, 40, 45, 50, 56, 63, 71,
			80, 90, 101, 113, 127, 144, 162, 182, 203, 226,
			255, 255
		};
		betaTab = new int[52]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 2, 2, 2, 3,
			3, 3, 3, 4, 4, 4, 6, 6, 7, 7,
			8, 8, 9, 9, 10, 10, 11, 11, 12, 12,
			13, 13, 14, 14, 15, 15, 16, 16, 17, 17,
			18, 18
		};
		tcs = new int[3][]
		{
			new int[52]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 2, 2, 2, 2, 3, 3, 3,
				4, 4, 4, 5, 6, 6, 7, 8, 9, 10,
				11, 13
			},
			new int[52]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 2, 2, 2, 2, 3, 3, 3, 4, 4,
				5, 5, 6, 7, 8, 8, 10, 11, 12, 13,
				15, 17
			},
			new int[52]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 2, 2, 2,
				2, 3, 3, 3, 4, 4, 4, 5, 6, 6,
				7, 8, 9, 10, 11, 13, 14, 16, 18, 20,
				23, 25
			}
		};
		inverse = new int[16]
		{
			0, 1, 4, 5, 2, 3, 6, 7, 8, 9,
			12, 13, 10, 11, 14, 15
		};
	}
}
