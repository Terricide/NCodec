using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.decode.deblock;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.encode;

public class MBDeblocker : Object
{
	internal static int[][] LOOKUP_IDX_P_V;

	internal static int[][] LOOKUP_IDX_Q_V;

	internal static int[][] LOOKUP_IDX_P_H;

	internal static int[][] LOOKUP_IDX_Q_H;

	private static int[][] BS_I;

	private static int[][] P_POS_V;

	private static int[][] Q_POS_V;

	private static int[][] P_POS_H;

	private static int[][] Q_POS_H;

	private static int[][] P_POS_V_CHR;

	private static int[][] Q_POS_V_CHR;

	private static int[][] P_POS_H_CHR;

	private static int[][] Q_POS_H_CHR;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 161, 68, 114, 108, 107, 114, 63, 57,
		183, 107, 111, 63, 41, 242, 58, 236, 76
	})]
	private void deblockBorder(int[] boundary, int qp, byte[] p, int pi, byte[] q, int qi, int[][] pTab, int[][] qTab, bool horiz)
	{
		int inc1 = ((!horiz) ? 1 : 16);
		int inc2 = inc1 * 2;
		int inc3 = inc1 * 3;
		for (int b = 0; b < 4; b++)
		{
			if (boundary[b] == 4)
			{
				int j = 0;
				int ii2 = b << 2;
				while (j < 4)
				{
					filterBs4(qp, qp, p, q, pTab[pi][ii2] - inc3, pTab[pi][ii2] - inc2, pTab[pi][ii2] - inc1, pTab[pi][ii2], qTab[qi][ii2], qTab[qi][ii2] + inc1, qTab[qi][ii2] + inc2, qTab[qi][ii2] + inc3);
					j++;
					ii2++;
				}
			}
			else if (boundary[b] > 0)
			{
				int i = 0;
				int ii = b << 2;
				while (i < 4)
				{
					filterBs(boundary[b], qp, qp, p, q, pTab[pi][ii] - inc2, pTab[pi][ii] - inc1, pTab[pi][ii], qTab[qi][ii], qTab[qi][ii] + inc1, qTab[qi][ii] + inc2);
					i++;
					ii++;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 71, 129, 68, 105, 106, 103, 108, 63, 17,
		146, 103, 110, 63, 20, 239, 59, 234, 74
	})]
	private void deblockBorderChroma(int[] boundary, int qp, byte[] p, int pi, byte[] q, int qi, int[][] pTab, int[][] qTab, bool horiz)
	{
		int inc1 = ((!horiz) ? 1 : 8);
		for (int b = 0; b < 4; b++)
		{
			if (boundary[b] == 4)
			{
				int j = 0;
				int ii2 = b << 1;
				while (j < 2)
				{
					filterBs4Chr(qp, qp, p, q, pTab[pi][ii2] - inc1, pTab[pi][ii2], qTab[qi][ii2], qTab[qi][ii2] + inc1);
					j++;
					ii2++;
				}
			}
			else if (boundary[b] > 0)
			{
				int i = 0;
				int ii = b << 1;
				while (i < 2)
				{
					filterBsChr(boundary[b], qp, qp, p, q, pTab[pi][ii] - inc1, pTab[pi][ii], qTab[qi][ii], qTab[qi][ii] + inc1);
					i++;
					ii++;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		128,
		130,
		104,
		103,
		104,
		123,
		159,
		8,
		159,
		8,
		191,
		8,
		106,
		159,
		17,
		159,
		17,
		byte.MaxValue,
		17,
		59,
		234,
		73,
		103,
		105,
		124,
		159,
		10,
		159,
		10,
		191,
		10,
		108,
		159,
		20,
		159,
		20,
		byte.MaxValue,
		20,
		59,
		236,
		72
	})]
	public virtual void deblockMBGeneric(EncodedMB curMB, EncodedMB leftMB, EncodedMB topMB, int[][] vertStrength, int[][] horizStrength)
	{
		Picture curPix = curMB.getPixels();
		if (leftMB != null)
		{
			Picture leftPix = leftMB.getPixels();
			int avgQp2 = MathUtil.clip(leftMB.getQp() + curMB.getQp() + 1 >> 1, 0, 51);
			deblockBorder(vertStrength[0], avgQp2, leftPix.getPlaneData(0), 3, curPix.getPlaneData(0), 0, P_POS_V, Q_POS_V, horiz: false);
			deblockBorderChroma(vertStrength[0], avgQp2, leftPix.getPlaneData(1), 3, curPix.getPlaneData(1), 0, P_POS_V_CHR, Q_POS_V_CHR, horiz: false);
			deblockBorderChroma(vertStrength[0], avgQp2, leftPix.getPlaneData(2), 3, curPix.getPlaneData(2), 0, P_POS_V_CHR, Q_POS_V_CHR, horiz: false);
		}
		for (int j = 0; j < 3; j++)
		{
			deblockBorder(vertStrength[j + 1], curMB.getQp(), curPix.getPlaneData(0), j, curPix.getPlaneData(0), j + 1, P_POS_V, Q_POS_V, horiz: false);
			deblockBorderChroma(vertStrength[j + 1], curMB.getQp(), curPix.getPlaneData(1), j, curPix.getPlaneData(1), j + 1, P_POS_V_CHR, Q_POS_V_CHR, horiz: false);
			deblockBorderChroma(vertStrength[j + 1], curMB.getQp(), curPix.getPlaneData(2), j, curPix.getPlaneData(2), j + 1, P_POS_V_CHR, Q_POS_V_CHR, horiz: false);
		}
		if (topMB != null)
		{
			Picture topPix = topMB.getPixels();
			int avgQp = MathUtil.clip(topMB.getQp() + curMB.getQp() + 1 >> 1, 0, 51);
			deblockBorder(horizStrength[0], avgQp, topPix.getPlaneData(0), 3, curPix.getPlaneData(0), 0, P_POS_H, Q_POS_H, horiz: true);
			deblockBorderChroma(horizStrength[0], avgQp, topPix.getPlaneData(1), 3, curPix.getPlaneData(1), 0, P_POS_H_CHR, Q_POS_H_CHR, horiz: true);
			deblockBorderChroma(horizStrength[0], avgQp, topPix.getPlaneData(2), 3, curPix.getPlaneData(2), 0, P_POS_H_CHR, Q_POS_H_CHR, horiz: true);
		}
		for (int i = 0; i < 3; i++)
		{
			deblockBorder(horizStrength[i + 1], curMB.getQp(), curPix.getPlaneData(0), i, curPix.getPlaneData(0), i + 1, P_POS_H, Q_POS_H, horiz: true);
			deblockBorderChroma(horizStrength[i + 1], curMB.getQp(), curPix.getPlaneData(1), i, curPix.getPlaneData(1), i + 1, P_POS_H_CHR, Q_POS_H_CHR, horiz: true);
			deblockBorderChroma(horizStrength[i + 1], curMB.getQp(), curPix.getPlaneData(2), i, curPix.getPlaneData(2), i + 1, P_POS_H_CHR, Q_POS_H_CHR, horiz: true);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 47, 98, 103, 106, 118, 127, 0, 127, 0,
		254, 61, 39, 234, 72, 106, 106, 102, 127, 0,
		127, 0, 254, 61, 39, 42, 234, 73
	})]
	internal static void calcStrengthForBlocks(EncodedMB cur, EncodedMB other, int[][] outStrength, int[][] LOOKUP_IDX_P, int[][] LOOKUP_IDX_Q)
	{
		if (other != null)
		{
			for (int j = 0; j < 4; j++)
			{
				outStrength[0][j] = ((!other.getType().isIntra()) ? MathUtil.max3(strengthMv(other.getMx()[LOOKUP_IDX_P[0][j]], cur.getMx()[LOOKUP_IDX_Q[0][j]]), strengthMv(other.getMy()[LOOKUP_IDX_P[0][j]], cur.getMy()[LOOKUP_IDX_Q[0][j]]), strengthNc(other.getNc()[LOOKUP_IDX_P[0][j]], cur.getNc()[LOOKUP_IDX_Q[0][j]])) : 4);
			}
		}
		for (int i = 1; i < 4; i++)
		{
			for (int k = 0; k < 4; k++)
			{
				outStrength[i][k] = MathUtil.max3(strengthMv(cur.getMx()[LOOKUP_IDX_P[i][k]], cur.getMx()[LOOKUP_IDX_Q[i][k]]), strengthMv(cur.getMy()[LOOKUP_IDX_P[i][k]], cur.getMy()[LOOKUP_IDX_Q[i][k]]), strengthNc(cur.getNc()[LOOKUP_IDX_P[i][k]], cur.getNc()[LOOKUP_IDX_Q[i][k]]));
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 162, 159, 0 })]
	protected internal virtual void filterBs4(int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p3Idx, int p2Idx, int p1Idx, int p0Idx, int q0Idx, int q1Idx, int q2Idx, int q3Idx)
	{
		_filterBs4(indexAlpha, indexBeta, pelsP, pelsQ, p3Idx, p2Idx, p1Idx, p0Idx, q0Idx, q1Idx, q2Idx, q3Idx, isChroma: false);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 98, 157 })]
	protected internal virtual void filterBs(int bs, int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p2Idx, int p1Idx, int p0Idx, int q0Idx, int q1Idx, int q2Idx)
	{
		_filterBs(bs, indexAlpha, indexBeta, pelsP, pelsQ, p2Idx, p1Idx, p0Idx, q0Idx, q1Idx, q2Idx, isChroma: false);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 161, 68, 102, 103, 102, 136, 106, 138,
		159, 13, 101, 194, 100, 100, 137, 110, 143, 125,
		221, 104, 103, 135, 120, 111, 118, 113, 113, 113,
		99, 111, 177, 110, 104, 104, 120, 112, 119, 114,
		114, 114, 99, 111, 146
	})]
	protected internal virtual void _filterBs4(int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p3Idx, int p2Idx, int p1Idx, int p0Idx, int q0Idx, int q1Idx, int q2Idx, int q3Idx, bool isChroma)
	{
		int p0 = pelsP[p0Idx];
		int q0 = pelsQ[q0Idx];
		int p1 = pelsP[p1Idx];
		int q1 = pelsQ[q1Idx];
		int alphaThresh = DeblockingFilter.alphaTab[indexAlpha];
		int betaThresh = DeblockingFilter.betaTab[indexBeta];
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
		159, 86, 129, 68, 103, 103, 103, 136, 106, 138,
		159, 13, 101, 130, 206, 103, 111, 111, 115, 109,
		109, 99, 103, 100, 164, 113, 155, 103, 111, 103,
		143, 104, 136, 115, 123, 103, 178, 104, 104, 116,
		123, 104, 178, 114, 114
	})]
	protected internal virtual void _filterBs(int bs, int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p2Idx, int p1Idx, int p0Idx, int q0Idx, int q1Idx, int q2Idx, bool isChroma)
	{
		int p1 = pelsP[p1Idx];
		int p0 = pelsP[p0Idx];
		int q0 = pelsQ[q0Idx];
		int q1 = pelsQ[q1Idx];
		int alphaThresh = DeblockingFilter.alphaTab[indexAlpha];
		int betaThresh = DeblockingFilter.betaTab[indexBeta];
		if ((Math.abs(p0 - q0) < alphaThresh && Math.abs(p1 - p0) < betaThresh && Math.abs(q1 - q0) < betaThresh) ? true : false)
		{
			int tC2 = DeblockingFilter.tcs[bs - 1][indexAlpha];
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
	[LineNumberTable(new byte[] { 159, 106, 66, 155 })]
	protected internal virtual void filterBs4Chr(int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p1Idx, int p0Idx, int q0Idx, int q1Idx)
	{
		_filterBs4(indexAlpha, indexBeta, pelsP, pelsQ, -1, -1, p1Idx, p0Idx, q0Idx, q1Idx, -1, -1, isChroma: true);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 130, 123 })]
	protected internal virtual void filterBsChr(int bs, int indexAlpha, int indexBeta, byte[] pelsP, byte[] pelsQ, int p1Idx, int p0Idx, int q0Idx, int q1Idx)
	{
		_filterBs(bs, indexAlpha, indexBeta, pelsP, pelsQ, -1, p1Idx, p0Idx, q0Idx, q1Idx, -1, isChroma: true);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(406)]
	private static int strengthMv(int v0, int v1)
	{
		return (Math.abs(v0 - v1) >= 4) ? 1 : 0;
	}

	[LineNumberTable(402)]
	private static int strengthNc(int ncA, int ncB)
	{
		return (ncA > 0 || ncB > 0) ? 2 : 0;
	}

	[LineNumberTable(new byte[]
	{
		159, 62, 66, 127, 7, 103, 106, 49, 41, 231,
		69
	})]
	private static int[][] buildPPosV()
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] qPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				qPos[i][j] = (j << 4) + (i << 2) + 3;
			}
		}
		return qPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 60, 130, 127, 7, 103, 106, 47, 41, 231,
		69
	})]
	private static int[][] buildQPosV()
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] pPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				pPos[i][j] = (j << 4) + (i << 2);
			}
		}
		return pPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 67, 66, 127, 7, 103, 106, 48, 41, 231,
		69
	})]
	private static int[][] buildPPosH()
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] qPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				qPos[i][j] = j + (i << 6) + 48;
			}
		}
		return qPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 65, 130, 127, 7, 103, 106, 45, 41, 231,
		69
	})]
	private static int[][] buildQPosH()
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] pPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				pPos[i][j] = j + (i << 6);
			}
		}
		return pPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 52, 66, 127, 6, 103, 105, 49, 41, 231,
		69
	})]
	private static int[][] buildPPosVChr()
	{
		int[] array = new int[2];
		int num = (array[1] = 8);
		num = (array[0] = 4);
		int[][] qPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				qPos[i][j] = (j << 3) + (i << 1) + 1;
			}
		}
		return qPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 50, 130, 127, 6, 103, 105, 47, 41, 231,
		69
	})]
	private static int[][] buildQPosVChr()
	{
		int[] array = new int[2];
		int num = (array[1] = 8);
		num = (array[0] = 4);
		int[][] pPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				pPos[i][j] = (j << 3) + (i << 1);
			}
		}
		return pPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 57, 66, 127, 6, 103, 105, 47, 41, 231,
		69
	})]
	private static int[][] buildPPosHChr()
	{
		int[] array = new int[2];
		int num = (array[1] = 8);
		num = (array[0] = 4);
		int[][] qPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				qPos[i][j] = j + (i << 4) + 8;
			}
		}
		return qPos;
	}

	[LineNumberTable(new byte[]
	{
		159, 55, 130, 127, 6, 103, 105, 45, 41, 231,
		69
	})]
	private static int[][] buildQPosHChr()
	{
		int[] array = new int[2];
		int num = (array[1] = 8);
		num = (array[0] = 4);
		int[][] pPos = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				pPos[i][j] = j + (i << 4);
			}
		}
		return pPos;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public MBDeblocker()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 162, 118 })]
	public virtual void deblockMBI(EncodedMB outMB, EncodedMB leftOutMB, EncodedMB topOutMB)
	{
		deblockMBGeneric(outMB, leftOutMB, topOutMB, BS_I, BS_I);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 66, 127, 6, 159, 6, 115, 147, 110 })]
	public virtual void deblockMBP(EncodedMB cur, EncodedMB left, EncodedMB top)
	{
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = 4);
		int[][] vertStrength = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 4);
		int[][] horizStrength = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		calcStrengthForBlocks(cur, left, vertStrength, LOOKUP_IDX_P_V, LOOKUP_IDX_Q_V);
		calcStrengthForBlocks(cur, top, horizStrength, LOOKUP_IDX_P_H, LOOKUP_IDX_Q_H);
		deblockMBGeneric(cur, left, top, vertStrength, horizStrength);
	}

	[LineNumberTable(new byte[]
	{
		159,
		137,
		98,
		127,
		88,
		127,
		88,
		127,
		88,
		byte.MaxValue,
		88,
		71,
		byte.MaxValue,
		81,
		161,
		122,
		107,
		107,
		107,
		139,
		107,
		139,
		107
	})]
	static MBDeblocker()
	{
		LOOKUP_IDX_P_V = new int[4][]
		{
			new int[4] { 3, 7, 11, 15 },
			new int[4] { 0, 4, 8, 12 },
			new int[4] { 1, 5, 9, 13 },
			new int[4] { 2, 6, 10, 14 }
		};
		LOOKUP_IDX_Q_V = new int[4][]
		{
			new int[4] { 0, 4, 8, 12 },
			new int[4] { 1, 5, 9, 13 },
			new int[4] { 2, 6, 10, 14 },
			new int[4] { 3, 7, 11, 15 }
		};
		LOOKUP_IDX_P_H = new int[4][]
		{
			new int[4] { 12, 13, 14, 15 },
			new int[4] { 0, 1, 2, 3 },
			new int[4] { 4, 5, 6, 7 },
			new int[4] { 8, 9, 10, 11 }
		};
		LOOKUP_IDX_Q_H = new int[4][]
		{
			new int[4] { 0, 1, 2, 3 },
			new int[4] { 4, 5, 6, 7 },
			new int[4] { 8, 9, 10, 11 },
			new int[4] { 12, 13, 14, 15 }
		};
		BS_I = new int[4][]
		{
			new int[4] { 4, 4, 4, 4 },
			new int[4] { 3, 3, 3, 3 },
			new int[4] { 3, 3, 3, 3 },
			new int[4] { 3, 3, 3, 3 }
		};
		P_POS_V = buildPPosV();
		Q_POS_V = buildQPosV();
		P_POS_H = buildPPosH();
		Q_POS_H = buildQPosH();
		P_POS_V_CHR = buildPPosVChr();
		Q_POS_V_CHR = buildQPosVChr();
		P_POS_H_CHR = buildPPosHChr();
		Q_POS_H_CHR = buildQPosHChr();
	}
}
