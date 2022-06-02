using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class Intra16x16PredictionBuilder : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 99, 104, 104, 63, 7, 43, 199 })]
	public static void predictVertical(int[][] residual, bool topAvailable, byte[] topLine, int x, byte[] pixOut)
	{
		int off = 0;
		for (int j = 0; j < 16; j++)
		{
			int i = 0;
			while (i < 16)
			{
				pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ELUMA_4x4_BLOCK_LUT[off]][H264Const.___003C_003ELUMA_4x4_POS_LUT[off]] + topLine[x + i], -128, 127);
				i++;
				off++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 99, 104, 104, 63, 5, 43, 199 })]
	public static void predictHorizontal(int[][] residual, bool leftAvailable, byte[] leftRow, int x, byte[] pixOut)
	{
		int off = 0;
		for (int j = 0; j < 16; j++)
		{
			int i = 0;
			while (i < 16)
			{
				pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ELUMA_4x4_BLOCK_LUT[off]][H264Const.___003C_003ELUMA_4x4_POS_LUT[off]] + leftRow[j], -128, 127);
				i++;
				off++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 65, 69, 109, 99, 104, 39, 135, 106,
		44, 169, 110, 100, 100, 106, 42, 137, 107, 100,
		100, 106, 46, 137, 139, 164, 109, 63, 7, 137
	})]
	public static void predictDC(int[][] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, int x, byte[] pixOut)
	{
		int s2;
		if (leftAvailable && topAvailable)
		{
			int num = 0;
			for (int m = 0; m < 16; m++)
			{
				num += leftRow[m];
			}
			for (int l = 0; l < 16; l++)
			{
				num += topLine[x + l];
			}
			s2 = num + 16 >> 5;
		}
		else if (leftAvailable)
		{
			int s = 0;
			for (int k = 0; k < 16; k++)
			{
				s += leftRow[k];
			}
			s2 = s + 8 >> 4;
		}
		else if (topAvailable)
		{
			int s0 = 0;
			for (int j = 0; j < 16; j++)
			{
				s0 += topLine[x + j];
			}
			s2 = s0 + 8 >> 4;
		}
		else
		{
			s2 = 0;
		}
		for (int i = 0; i < 256; i++)
		{
			pixOut[i] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ELUMA_4x4_BLOCK_LUT[i]][H264Const.___003C_003ELUMA_4x4_POS_LUT[i]] + s2, -128, 127);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 66, 131, 103, 59, 167, 147, 99, 103,
		51, 167, 143, 107, 107, 147, 100, 109, 106, 127,
		4, 31, 7, 47, 236, 70
	})]
	public static void predictPlane(int[][] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int x, byte[] pixOut)
	{
		int H = 0;
		for (int j = 0; j < 7; j++)
		{
			H += (j + 1) * (topLine[x + 8 + j] - topLine[x + 6 - j]);
		}
		H += 8 * (topLine[x + 15] - topLeft[0]);
		int V = 0;
		for (int l = 0; l < 7; l++)
		{
			V += (l + 1) * (leftRow[8 + l] - leftRow[6 - l]);
		}
		V += 8 * (leftRow[15] - topLeft[0]);
		int c = 5 * V + 32 >> 6;
		int b = 5 * H + 32 >> 6;
		int a = 16 * (leftRow[15] + topLine[x + 15]);
		int off = 0;
		for (int k = 0; k < 16; k++)
		{
			int i = 0;
			while (i < 16)
			{
				int val = MathUtil.clip(a + b * (i - 7) + c * (k - 7) + 16 >> 5, -128, 127);
				pixOut[off] = (byte)(sbyte)MathUtil.clip(residual[H264Const.___003C_003ELUMA_4x4_BLOCK_LUT[off]][H264Const.___003C_003ELUMA_4x4_POS_LUT[off]] + val, -128, 127);
				i++;
				off++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public Intra16x16PredictionBuilder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 65, 69, 153, 110, 131, 110, 131, 113,
		131, 213
	})]
	public static void predictWithMode(int predMode, int[][] residual, bool leftAvailable, bool topAvailable, byte[] leftRow, byte[] topLine, byte[] topLeft, int x, byte[] pixOut)
	{
		switch (predMode)
		{
		case 0:
			predictVertical(residual, topAvailable, topLine, x, pixOut);
			break;
		case 1:
			predictHorizontal(residual, leftAvailable, leftRow, x, pixOut);
			break;
		case 2:
			predictDC(residual, leftAvailable, topAvailable, leftRow, topLine, x, pixOut);
			break;
		case 3:
			predictPlane(residual, leftAvailable, topAvailable, leftRow, topLine, topLeft, x, pixOut);
			break;
		}
	}
}
