using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderUtils : Object
{
	private static bool debug;

	internal static int ___003C_003ENULL_VECTOR;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int NULL_VECTOR
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENULL_VECTOR;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 162, 120, 111, 111, 112, 112, 114, 116,
		116, 118
	})]
	internal static void savePrediction8x8(DecoderState sharedState, int mbX, H264Utils.MvList x)
	{
		sharedState.mvTopLeft.copyPair(0, sharedState.mvTop, (mbX << 2) + 3);
		sharedState.mvLeft.copyPair(0, x, 3);
		sharedState.mvLeft.copyPair(1, x, 7);
		sharedState.mvLeft.copyPair(2, x, 11);
		sharedState.mvLeft.copyPair(3, x, 15);
		sharedState.mvTop.copyPair(mbX << 2, x, 12);
		sharedState.mvTop.copyPair((mbX << 2) + 1, x, 13);
		sharedState.mvTop.copyPair((mbX << 2) + 2, x, 14);
		sharedState.mvTop.copyPair((mbX << 2) + 3, x, 15);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 130, 112, 108, 120, 24, 49, 238, 70 })]
	internal static void saveMvs(DeblockerInput di, H264Utils.MvList x, int mbX, int mbY)
	{
		int j = 0;
		int blkOffY = mbY << 2;
		int blkInd = 0;
		while (j < 4)
		{
			int i = 0;
			int blkOffX = mbX << 2;
			while (i < 4)
			{
				di.mvs.setMv(blkOffX, blkOffY, 0, x.getMv(blkInd, 0));
				di.mvs.setMv(blkOffX, blkOffY, 1, x.getMv(blkInd, 1));
				i++;
				blkOffX++;
				blkInd++;
			}
			j++;
			blkOffY++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 162, 106, 105, 104, 63, 2, 7, 234,
		70
	})]
	internal static void mergeResidual(Picture mb, int[][][] residual, int[][] blockLUT, int[][] posLUT)
	{
		for (int comp = 0; comp < 3; comp++)
		{
			byte[] to = mb.getPlaneData(comp);
			for (int i = 0; i < (nint)to.LongLength; i++)
			{
				to[i] = (byte)(sbyte)MathUtil.clip(to[i] + residual[comp][blockLUT[comp][i]][posLUT[comp][i]], -128, 127);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 122, 117, 117, 120, 127, 0, 155,
		107
	})]
	internal static void collectPredictors(DecoderState sharedState, Picture outMB, int mbX)
	{
		sharedState.topLeft[0][0] = sharedState.topLine[0][(mbX << 4) + 15];
		sharedState.topLeft[0][1] = outMB.getPlaneData(0)[63];
		sharedState.topLeft[0][2] = outMB.getPlaneData(0)[127];
		sharedState.topLeft[0][3] = outMB.getPlaneData(0)[191];
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(0), 240, sharedState.topLine[0], mbX << 4, 16);
		copyCol(outMB.getPlaneData(0), 16, 15, 16, sharedState.leftRow[0]);
		collectChromaPredictors(sharedState, outMB, mbX);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 109, 102, 159, 5, 106, 103, 175 })]
	public static void debugPrint(params object[] arguments)
	{
		if (debug && (nint)arguments.LongLength > 0)
		{
			if ((nint)arguments.LongLength == 1)
			{
				Logger.debug(new StringBuilder().append("").append(arguments[0]).toString());
				return;
			}
			string fmt = (string)arguments[0];
			ArrayUtil.shiftLeft1(arguments);
			Logger.debug(String.format(fmt, arguments));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 65, 77, 100, 100, 163, 106, 103, 165,
		110, 110, 142, 127, 0, 108, 127, 0, 108, 127,
		0, 140, 127, 29, 60
	})]
	public static int calcMVPredictionMedian(int a, int b, int c, int d, bool aAvb, bool bAvb, bool cAvb, bool dAvb, int @ref, int comp)
	{
		int cAvb2 = (cAvb ? 1 : 0);
		int bAvb2 = (bAvb ? 1 : 0);
		if (cAvb2 == 0)
		{
			c = d;
			cAvb2 = (dAvb ? 1 : 0);
		}
		if (aAvb && bAvb2 == 0 && cAvb2 == 0)
		{
			b = (c = a);
			bAvb2 = (cAvb2 = (aAvb ? 1 : 0));
		}
		a = ((!aAvb) ? ___003C_003ENULL_VECTOR : a);
		b = ((bAvb2 == 0) ? ___003C_003ENULL_VECTOR : b);
		c = ((cAvb2 == 0) ? ___003C_003ENULL_VECTOR : c);
		if (H264Utils.Mv.mvRef(a) == @ref && H264Utils.Mv.mvRef(b) != @ref && H264Utils.Mv.mvRef(c) != @ref)
		{
			int result = H264Utils.Mv.mvC(a, comp);
			
			return result;
		}
		if (H264Utils.Mv.mvRef(b) == @ref && H264Utils.Mv.mvRef(a) != @ref && H264Utils.Mv.mvRef(c) != @ref)
		{
			int result2 = H264Utils.Mv.mvC(b, comp);
			
			return result2;
		}
		if (H264Utils.Mv.mvRef(c) == @ref && H264Utils.Mv.mvRef(a) != @ref && H264Utils.Mv.mvRef(b) != @ref)
		{
			int result3 = H264Utils.Mv.mvC(c, comp);
			
			return result3;
		}
		return H264Utils.Mv.mvC(a, comp) + H264Utils.Mv.mvC(b, comp) + H264Utils.Mv.mvC(c, comp) - min(H264Utils.Mv.mvC(a, comp), H264Utils.Mv.mvC(b, comp), H264Utils.Mv.mvC(c, comp)) - max(H264Utils.Mv.mvC(a, comp), H264Utils.Mv.mvC(b, comp), H264Utils.Mv.mvC(c, comp));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 66, 103, 43, 167 })]
	internal static void saveVect(H264Utils.MvList mv, int list, int from, int to, int vect)
	{
		for (int i = from; i < to; i++)
		{
			mv.setMv(i, list, vect);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 130, 112, 108, 117, 21, 49, 238, 70 })]
	internal static void saveMvsIntra(DeblockerInput di, int mbX, int mbY)
	{
		int j = 0;
		int blkOffY = mbY << 2;
		int blkInd = 0;
		while (j < 4)
		{
			int i = 0;
			int blkOffX = mbX << 2;
			while (i < 4)
			{
				di.mvs.setMv(blkOffX, blkOffY, 0, ___003C_003ENULL_VECTOR);
				di.mvs.setMv(blkOffX, blkOffY, 1, ___003C_003ENULL_VECTOR);
				i++;
				blkOffX++;
				blkInd++;
			}
			j++;
			blkOffY++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 162, 133, 150, 118, 116, 118, 118 })]
	public static void saveVectIntra(DecoderState sharedState, int mbX)
	{
		int xx = mbX << 2;
		sharedState.mvTopLeft.copyPair(0, sharedState.mvTop, xx + 3);
		saveVect(sharedState.mvTop, 0, xx, xx + 4, ___003C_003ENULL_VECTOR);
		saveVect(sharedState.mvLeft, 0, 0, 4, ___003C_003ENULL_VECTOR);
		saveVect(sharedState.mvTop, 1, xx, xx + 4, ___003C_003ENULL_VECTOR);
		saveVect(sharedState.mvLeft, 1, 0, 4, ___003C_003ENULL_VECTOR);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 121, 153, 123, 155, 120, 122 })]
	internal static void collectChromaPredictors(DecoderState sharedState, Picture outMB, int mbX)
	{
		sharedState.topLeft[1][0] = sharedState.topLine[1][(mbX << 3) + 7];
		sharedState.topLeft[2][0] = sharedState.topLine[2][(mbX << 3) + 7];
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(1), 56, sharedState.topLine[1], mbX << 3, 8);
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(2), 56, sharedState.topLine[2], mbX << 3, 8);
		copyCol(outMB.getPlaneData(1), 8, 7, 8, sharedState.leftRow[1]);
		copyCol(outMB.getPlaneData(2), 8, 7, 8, sharedState.leftRow[2]);
	}

	[LineNumberTable(new byte[] { 159, 129, 66, 103, 40, 172 })]
	private static void copyCol(byte[] planeData, int n, int off, int stride, byte[] @out)
	{
		int i = 0;
		while (i < n)
		{
			@out[i] = planeData[off];
			i++;
			off += stride;
		}
	}

	[LineNumberTable(118)]
	public static int min(int x, int x2, int x3)
	{
		return (x < x2) ? ((x >= x3) ? x3 : x) : ((x2 >= x3) ? x3 : x2);
	}

	[LineNumberTable(114)]
	public static int max(int x, int x2, int x3)
	{
		return (x > x2) ? ((x <= x3) ? x3 : x) : ((x2 <= x3) ? x3 : x2);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public MBlockDecoderUtils()
	{
	}

	[LineNumberTable(15)]
	static MBlockDecoderUtils()
	{
		___003C_003ENULL_VECTOR = H264Utils.Mv.packMv(0, 0, -1);
	}
}
