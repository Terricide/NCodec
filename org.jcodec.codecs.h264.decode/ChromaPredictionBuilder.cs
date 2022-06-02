using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class ChromaPredictionBuilder : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 65, 69, 114, 114, 114, 116 })]
	public static void predictDC(int[][] planeData, int mbX, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] pixOut)
	{
		predictDCInside(planeData, 0, 0, mbX, leftAvailable, topAvailable, leftRow, topLine, pixOut);
		predictDCTopBorder(planeData, 1, 0, mbX, leftAvailable, topAvailable, leftRow, topLine, pixOut);
		predictDCLeftBorder(planeData, 0, 1, mbX, leftAvailable, topAvailable, leftRow, topLine, pixOut);
		predictDCInside(planeData, 1, 1, mbX, leftAvailable, topAvailable, leftRow, topLine, pixOut);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 105, 103, 63, 5, 43, 199 })]
	public static void predictHorizontal(int[][] residual, int mbX, bool leftAvailable, byte[] leftRow, byte[] pixOut)
	{
		int off = 0;
		for (int j = 0; j < 8; j++)
		{
			int i = 0;
			while (i < 8)
			{
				pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off]][H264Const.___003C_003ECHROMA_POS_LUT[off]] + leftRow[j], -128, 127);
				i++;
				off++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 105, 103, 63, 9, 43, 231, 69 })]
	public static void predictVertical(int[][] residual, int mbX, bool topAvailable, byte[] topLine, byte[] pixOut)
	{
		int off = 0;
		for (int j = 0; j < 8; j++)
		{
			int i = 0;
			while (i < 8)
			{
				pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off]][H264Const.___003C_003ECHROMA_POS_LUT[off]] + topLine[(mbX << 3) + i], -128, 127);
				i++;
				off++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 162, 135, 103, 57, 167, 145, 99, 105,
		56, 169, 143, 108, 108, 145, 111, 105, 122, 31,
		16, 47, 236, 70
	})]
	public static void predictPlane(int[][] residual, int mbX, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, byte[] pixOut)
	{
		int H = 0;
		int blkOffX = mbX << 3;
		for (int j = 0; j < 3; j++)
		{
			H += (j + 1) * (topLine[blkOffX + 4 + j] - topLine[blkOffX + 2 - j]);
		}
		H += 4 * (topLine[blkOffX + 7] - topLeft[0]);
		int V = 0;
		for (int l = 0; l < 3; l++)
		{
			V += (l + 1) * (leftRow[4 + l] - leftRow[2 - l]);
		}
		V += 4 * (leftRow[7] - topLeft[0]);
		int c = 34 * V + 32 >> 6;
		int b = 34 * H + 32 >> 6;
		int a = 16 * (leftRow[7] + topLine[blkOffX + 7]);
		int off = 0;
		for (int k = 0; k < 8; k++)
		{
			int i = 0;
			while (i < 8)
			{
				int val = a + b * (i - 3) + c * (k - 3) + 16 >> 5;
				pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off]][H264Const.___003C_003ECHROMA_POS_LUT[off]] + MathUtil.clip(val, -128, 127), -128, 127);
				i++;
				off++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		126,
		65,
		71,
		141,
		109,
		100,
		105,
		45,
		137,
		105,
		45,
		169,
		110,
		100,
		100,
		105,
		45,
		137,
		107,
		100,
		100,
		105,
		45,
		137,
		139,
		164,
		117,
		127,
		7,
		127,
		13,
		127,
		13,
		byte.MaxValue,
		13,
		60,
		242,
		70
	})]
	public static void predictDCInside(int[][] residual, int blkX, int blkY, int mbX, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] pixOut)
	{
		int blkOffX = (blkX << 2) + (mbX << 3);
		int blkOffY = blkY << 2;
		int s2;
		if (leftAvailable && topAvailable)
		{
			int num = 0;
			for (int l = 0; l < 4; l++)
			{
				num += leftRow[l + blkOffY];
			}
			for (int k = 0; k < 4; k++)
			{
				num += topLine[blkOffX + k];
			}
			s2 = num + 4 >> 3;
		}
		else if (leftAvailable)
		{
			int s = 0;
			for (int j = 0; j < 4; j++)
			{
				s += leftRow[blkOffY + j];
			}
			s2 = s + 2 >> 2;
		}
		else if (topAvailable)
		{
			int s0 = 0;
			for (int i = 0; i < 4; i++)
			{
				s0 += topLine[blkOffX + i];
			}
			s2 = s0 + 2 >> 2;
		}
		else
		{
			s2 = 0;
		}
		int off = (blkY << 5) + (blkX << 2);
		int m = 0;
		while (m < 4)
		{
			pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off]][H264Const.___003C_003ECHROMA_POS_LUT[off]] + s2, -128, 127);
			pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 1]][H264Const.___003C_003ECHROMA_POS_LUT[off + 1]] + s2, -128, 127);
			pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 2]][H264Const.___003C_003ECHROMA_POS_LUT[off + 2]] + s2, -128, 127);
			pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 3]][H264Const.___003C_003ECHROMA_POS_LUT[off + 3]] + s2, -128, 127);
			m++;
			off += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		118,
		161,
		71,
		109,
		100,
		100,
		105,
		45,
		169,
		107,
		100,
		100,
		105,
		45,
		137,
		139,
		164,
		117,
		127,
		7,
		127,
		13,
		127,
		13,
		byte.MaxValue,
		13,
		60,
		242,
		70
	})]
	public static void predictDCTopBorder(int[][] residual, int blkX, int blkY, int mbX, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] pixOut)
	{
		int blkOffX = (blkX << 2) + (mbX << 3);
		int blkOffY = blkY << 2;
		int s2;
		if (topAvailable)
		{
			int num = 0;
			for (int j = 0; j < 4; j++)
			{
				num += topLine[blkOffX + j];
			}
			s2 = num + 2 >> 2;
		}
		else if (leftAvailable)
		{
			int s1 = 0;
			for (int i = 0; i < 4; i++)
			{
				s1 += leftRow[blkOffY + i];
			}
			s2 = s1 + 2 >> 2;
		}
		else
		{
			s2 = 0;
		}
		int off = (blkY << 5) + (blkX << 2);
		int k = 0;
		while (k < 4)
		{
			pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off]][H264Const.___003C_003ECHROMA_POS_LUT[off]] + s2, -128, 127);
			pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 1]][H264Const.___003C_003ECHROMA_POS_LUT[off + 1]] + s2, -128, 127);
			pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 2]][H264Const.___003C_003ECHROMA_POS_LUT[off + 2]] + s2, -128, 127);
			pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 3]][H264Const.___003C_003ECHROMA_POS_LUT[off + 3]] + s2, -128, 127);
			k++;
			off += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		111,
		129,
		71,
		109,
		100,
		100,
		105,
		45,
		137,
		107,
		100,
		100,
		105,
		45,
		137,
		139,
		164,
		117,
		127,
		7,
		127,
		13,
		127,
		13,
		byte.MaxValue,
		13,
		60,
		242,
		70
	})]
	public static void predictDCLeftBorder(int[][] residual, int blkX, int blkY, int mbX, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] pixOut)
	{
		int blkOffX = (blkX << 2) + (mbX << 3);
		int blkOffY = blkY << 2;
		int s3;
		if (leftAvailable)
		{
			int num = 0;
			for (int j = 0; j < 4; j++)
			{
				num += leftRow[blkOffY + j];
			}
			s3 = num + 2 >> 2;
		}
		else if (topAvailable)
		{
			int s2 = 0;
			for (int i = 0; i < 4; i++)
			{
				s2 += topLine[blkOffX + i];
			}
			s3 = s2 + 2 >> 2;
		}
		else
		{
			s3 = 0;
		}
		int off = (blkY << 5) + (blkX << 2);
		int k = 0;
		while (k < 4)
		{
			pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off]][H264Const.___003C_003ECHROMA_POS_LUT[off]] + s3, -128, 127);
			pixOut[off + 1] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 1]][H264Const.___003C_003ECHROMA_POS_LUT[off + 1]] + s3, -128, 127);
			pixOut[off + 2] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 2]][H264Const.___003C_003ECHROMA_POS_LUT[off + 2]] + s3, -128, 127);
			pixOut[off + 3] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ECHROMA_BLOCK_LUT[off + 3]][H264Const.___003C_003ECHROMA_POS_LUT[off + 3]] + s3, -128, 127);
			k++;
			off += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public ChromaPredictionBuilder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 97, 70, 153, 112, 131, 109, 131, 109,
		131, 212
	})]
	public static void predictWithMode(int[][] residual, int chromaMode, int mbX, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, byte[] pixOut)
	{
		switch (chromaMode)
		{
		case 0:
			predictDC(residual, mbX, leftAvailable, topAvailable, leftRow, topLine, pixOut);
			break;
		case 1:
			predictHorizontal(residual, mbX, leftAvailable, leftRow, pixOut);
			break;
		case 2:
			predictVertical(residual, mbX, topAvailable, topLine, pixOut);
			break;
		case 3:
			predictPlane(residual, mbX, leftAvailable, topAvailable, leftRow, topLine, topLeft, pixOut);
			break;
		}
	}
}
