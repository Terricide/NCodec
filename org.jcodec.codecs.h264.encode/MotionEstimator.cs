using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.encode;

public class MotionEstimator : Object
{
	private int maxSearchRange;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 66, 158, 101, 134, 112, 113, 122, 154,
		102, 135, 103, 104, 191, 4, 105, 145, 113, 127,
		0, 127, 3, 127, 0, 127, 3, 122, 103, 102,
		101, 103, 105, 103, 105, 103, 137, 231, 48, 236,
		84
	})]
	public virtual int[] estimate(Picture @ref, byte[] patch, int mbX, int mbY, int mvpx, int mvpy)
	{
		byte[] searchPatch = new byte[(maxSearchRange * 2 + 16) * (maxSearchRange * 2 + 16)];
		int startX = mbX << 4;
		int startY = mbY << 4;
		int patchTlX = Math.max(startX - maxSearchRange, 0);
		int patchTlY = Math.max(startY - maxSearchRange, 0);
		int patchBrX = Math.min(startX + maxSearchRange + 16, @ref.getPlaneWidth(0));
		int patchBrY = Math.min(startY + maxSearchRange + 16, @ref.getPlaneHeight(0));
		int centerX = startX - patchTlX;
		int centerY = startY - patchTlY;
		int patchW = patchBrX - patchTlX;
		int patchH = patchBrY - patchTlY;
		MBEncoderHelper.takeSafe(@ref.getPlaneData(0), @ref.getPlaneWidth(0), @ref.getPlaneHeight(0), patchTlX, patchTlY, searchPatch, patchW, patchH);
		int bestMvX = centerX;
		int bestMvY = centerY;
		int bestScore = sad(searchPatch, patchW, patch, bestMvX, bestMvY);
		for (int i = 0; i < maxSearchRange; i++)
		{
			int score1 = ((bestMvX <= 0) ? int.MaxValue : sad(searchPatch, patchW, patch, bestMvX - 1, bestMvY));
			int score2 = ((bestMvX >= patchW - 1) ? int.MaxValue : sad(searchPatch, patchW, patch, bestMvX + 1, bestMvY));
			int score3 = ((bestMvY <= 0) ? int.MaxValue : sad(searchPatch, patchW, patch, bestMvX, bestMvY - 1));
			int score4 = ((bestMvY >= patchH - 1) ? int.MaxValue : sad(searchPatch, patchW, patch, bestMvX, bestMvY + 1));
			int min = Math.min(Math.min(Math.min(score1, score2), score3), score4);
			if (min > bestScore)
			{
				break;
			}
			bestScore = min;
			if (score1 == min)
			{
				bestMvX += -1;
			}
			else if (score2 == min)
			{
				bestMvX++;
			}
			else
			{
				bestMvY = ((score3 != min) ? (bestMvY + 1) : (bestMvY + -1));
			}
		}
		return new int[2]
		{
			bestMvX - centerX << 2,
			bestMvY - centerY << 2
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 98, 109, 104, 106, 48, 177, 232, 60,
		231, 70
	})]
	private int sad(byte[] big, int bigStride, byte[] small, int offX, int offY)
	{
		int score = 0;
		int bigOff = offY * bigStride + offX;
		int smallOff = 0;
		for (int i = 0; i < 16; i++)
		{
			int j = 0;
			while (j < 16)
			{
				score += MathUtil.abs(big[bigOff] - small[smallOff]);
				j++;
				bigOff++;
				smallOff++;
			}
			bigOff += bigStride - 16;
		}
		return score;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104 })]
	public MotionEstimator(int maxSearchRange)
	{
		this.maxSearchRange = maxSearchRange;
	}
}
