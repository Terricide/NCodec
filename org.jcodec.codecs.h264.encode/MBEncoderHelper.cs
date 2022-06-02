using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.encode;

public class MBEncoderHelper : Object
{
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 116, 110, 110, 116, 116, 244, 60,
		52, 238, 72
	})]
	public static void takeSubtractSafe(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, byte[] pred, int blkW, int blkH)
	{
		int i = 0;
		int srcOff = y * planeWidth + x;
		int dstOff = 0;
		while (i < blkH)
		{
			int j = 0;
			int srcOff2 = srcOff;
			while (j < blkW)
			{
				coeff[dstOff] = planeData[srcOff2] - pred[dstOff];
				coeff[dstOff + 1] = planeData[srcOff2 + 1] - pred[dstOff + 1];
				coeff[dstOff + 2] = planeData[srcOff2 + 2] - pred[dstOff + 2];
				coeff[dstOff + 3] = planeData[srcOff2 + 3] - pred[dstOff + 3];
				j += 4;
				dstOff += 4;
				srcOff2 += 4;
			}
			i++;
			srcOff += planeWidth;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 119, 130, 163, 117, 141, 112, 45, 175, 101,
		104, 45, 235, 57, 234, 75, 108, 144, 114, 46,
		179, 103, 105, 46, 237, 57, 234, 75
	})]
	public static void takeSubtractUnsafe(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, byte[] pred, int blkW, int blkH)
	{
		int outOff = 0;
		int i;
		for (i = y; i < Math.min(y + blkH, planeHeight); i++)
		{
			int off2 = i * planeWidth + Math.min(x, planeWidth);
			int k = x;
			while (k < Math.min(x + blkW, planeWidth))
			{
				coeff[outOff] = planeData[off2] - pred[outOff];
				k++;
				outOff++;
				off2++;
			}
			off2 += -1;
			while (k < x + blkW)
			{
				coeff[outOff] = planeData[off2] - pred[outOff];
				k++;
				outOff++;
			}
		}
		for (; i < y + blkH; i++)
		{
			int off = planeHeight * planeWidth - planeWidth + Math.min(x, planeWidth);
			int j = x;
			while (j < Math.min(x + blkW, planeWidth))
			{
				coeff[outOff] = planeData[off] - pred[outOff];
				j++;
				outOff++;
				off++;
			}
			off += -1;
			while (j < x + blkW)
			{
				coeff[outOff] = planeData[off] - pred[outOff];
				j++;
				outOff++;
			}
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 131, 162, 113, 107, 41, 49, 235, 69 })]
	public static void takeSafe(byte[] planeData, int planeWidth, int planeHeight, int x, int y, byte[] patch, int blkW, int blkH)
	{
		int i = 0;
		int srcOff = y * planeWidth + x;
		int dstOff = 0;
		while (i < blkH)
		{
			int j = 0;
			int srcOff2 = srcOff;
			while (j < blkW)
			{
				patch[dstOff] = planeData[srcOff2];
				j++;
				dstOff++;
				srcOff2++;
			}
			i++;
			srcOff += planeWidth;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 163, 117, 141, 112, 40, 175, 101,
		104, 40, 235, 57, 234, 75, 108, 144, 114, 41,
		179, 103, 105, 41, 237, 57, 234, 75
	})]
	public static void takeExtendBorder(byte[] planeData, int planeWidth, int planeHeight, int x, int y, byte[] patch, int blkW, int blkH)
	{
		int outOff = 0;
		int i;
		for (i = y; i < Math.min(y + blkH, planeHeight); i++)
		{
			int off2 = i * planeWidth + Math.min(x, planeWidth);
			int k = x;
			while (k < Math.min(x + blkW, planeWidth))
			{
				patch[outOff] = planeData[off2];
				k++;
				outOff++;
				off2++;
			}
			off2 += -1;
			while (k < x + blkW)
			{
				patch[outOff] = planeData[off2];
				k++;
				outOff++;
			}
		}
		for (; i < y + blkH; i++)
		{
			int off = planeHeight * planeWidth - planeWidth + Math.min(x, planeWidth);
			int j = x;
			while (j < Math.min(x + blkW, planeWidth))
			{
				patch[outOff] = planeData[off];
				j++;
				outOff++;
				off++;
			}
			off += -1;
			while (j < x + blkW)
			{
				patch[outOff] = planeData[off];
				j++;
				outOff++;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 105, 66, 105, 99, 104, 103, 39, 143, 231,
		61, 231, 69
	})]
	private static void pubBlkOnePlane(byte[] dest, int destWidth, byte[] src, int srcWidth, int srcHeight, int x, int y)
	{
		int destOff = y * destWidth + x;
		int srcOff = 0;
		for (int i = 0; i < srcHeight; i++)
		{
			int j = 0;
			while (j < srcWidth)
			{
				dest[destOff] = src[srcOff];
				j++;
				destOff++;
				srcOff++;
			}
			destOff += destWidth - srcWidth;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public MBEncoderHelper()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 138, 98, 112, 150, 150 })]
	public static void takeSubtract(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, byte[] pred, int blkW, int blkH)
	{
		if (x + blkW < planeWidth && y + blkH < planeHeight)
		{
			takeSubtractSafe(planeData, planeWidth, planeHeight, x, y, coeff, pred, blkW, blkH);
		}
		else
		{
			takeSubtractUnsafe(planeData, planeWidth, planeHeight, x, y, coeff, pred, blkW, blkH);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 133, 130, 112, 148, 148 })]
	public static void take(byte[] planeData, int planeWidth, int planeHeight, int x, int y, byte[] patch, int blkW, int blkH)
	{
		if (x + blkW < planeWidth && y + blkH < planeHeight)
		{
			takeSafe(planeData, planeWidth, planeHeight, x, y, patch, blkW, blkH);
		}
		else
		{
			takeExtendBorder(planeData, planeWidth, planeHeight, x, y, patch, blkW, blkH);
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 121, 98, 113, 107, 41, 49, 235, 69 })]
	public static void takeSafe2(byte[] planeData, int planeWidth, int planeHeight, int x, int y, int[] coeff, int blkW, int blkH)
	{
		int i = 0;
		int srcOff = y * planeWidth + x;
		int dstOff = 0;
		while (i < blkH)
		{
			int j = 0;
			int srcOff2 = srcOff;
			while (j < blkW)
			{
				coeff[dstOff] = planeData[srcOff2];
				j++;
				dstOff++;
				srcOff2++;
			}
			i++;
			srcOff += planeWidth;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 104, 120, 100, 109, 118, 124, 124,
		124, 101, 231, 58, 236, 72, 229, 54, 234, 76
	})]
	public static void putBlk(byte[] planeData, int[] block, byte[] pred, int log2stride, int blkX, int blkY, int blkW, int blkH)
	{
		int stride = 1 << log2stride;
		int line = 0;
		int srcOff = 0;
		int dstOff = (blkY << log2stride) + blkX;
		for (; line < blkH; line++)
		{
			int dstOff2 = dstOff;
			for (int row = 0; row < blkW; row += 4)
			{
				planeData[dstOff2] = (byte)(sbyte)MathUtil.clip(block[srcOff] + pred[srcOff], -128, 127);
				planeData[dstOff2 + 1] = (byte)(sbyte)MathUtil.clip(block[srcOff + 1] + pred[srcOff + 1], -128, 127);
				planeData[dstOff2 + 2] = (byte)(sbyte)MathUtil.clip(block[srcOff + 2] + pred[srcOff + 2], -128, 127);
				planeData[dstOff2 + 3] = (byte)(sbyte)MathUtil.clip(block[srcOff + 3] + pred[srcOff + 3], -128, 127);
				srcOff += 4;
				dstOff2 += 4;
			}
			dstOff += stride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 108, 162, 111, 113, 113, 127, 0, 63, 11,
		38, 202
	})]
	public static void putBlkPic(Picture dest, Picture src, int x, int y)
	{
		if (dest.getColor() != src.getColor())
		{
			
			throw new RuntimeException("Incompatible color");
		}
		for (int c = 0; c < dest.getColor().nComp; c++)
		{
			pubBlkOnePlane(dest.getPlaneData(c), dest.getPlaneWidth(c), src.getPlaneData(c), src.getPlaneWidth(c), src.getPlaneHeight(c), x >> dest.getColor().compWidth[c], y >> dest.getColor().compHeight[c]);
		}
	}
}
