using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg12;

public class MPEGPred : Object
{
	protected internal int[][][] mvPred;

	protected internal int chromaFormat;

	protected internal int[][] fCode;

	protected internal bool topFieldFirst;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 97, 67, 105, 127, 17, 104, 104, 104 })]
	public MPEGPred(int[][] fCode, int chromaFormat, bool topFieldFirst)
	{
		int[] array = new int[3];
		int num = (array[2] = 2);
		num = (array[1] = 2);
		num = (array[0] = 2);
		mvPred = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		this.fCode = fCode;
		this.chromaFormat = chromaFormat;
		this.topFieldFirst = topFieldFirst;
	}

	[LineNumberTable(new byte[] { 159, 22, 66, 127, 116 })]
	public virtual void reset()
	{
		int[] obj = mvPred[0][0];
		int[] obj2 = mvPred[0][0];
		int[] obj3 = mvPred[0][1];
		int[] obj4 = mvPred[0][1];
		int[] obj5 = mvPred[1][0];
		int[] obj6 = mvPred[1][0];
		int[] obj7 = mvPred[1][1];
		int[] obj8 = mvPred[1][1];
		int num = 0;
		int num2 = 1;
		int[] array = obj8;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 0;
		array = obj7;
		int num4 = num;
		array[num2] = num;
		num = num4;
		num2 = 1;
		array = obj6;
		int num5 = num;
		array[num2] = num;
		num = num5;
		num2 = 0;
		array = obj5;
		int num6 = num;
		array[num2] = num;
		num = num6;
		num2 = 1;
		array = obj4;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 0;
		array = obj3;
		int num8 = num;
		array[num2] = num;
		num = num8;
		num2 = 1;
		array = obj2;
		int num9 = num;
		array[num2] = num;
		obj[0] = num9;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 85, 66, 112, 152, 114, 131, 112, 131, 178 })]
	public virtual void predictInFrame(Picture reference, int x, int y, int[][] mbPix, BitReader _in, int motionType, int backward, int spatial_temporal_weight_code)
	{
		Picture[] refs = new Picture[2] { reference, reference };
		switch (motionType)
		{
		case 1:
			predictFieldInFrame(reference, x, y, mbPix, _in, backward, spatial_temporal_weight_code);
			break;
		case 2:
			predict16x16Frame(reference, x, y, _in, backward, mbPix);
			break;
		case 3:
			predict16x16DualPrimeFrame(refs, x, y, _in, backward, mbPix);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 98, 152, 112, 131, 114, 114, 131, 146 })]
	public virtual void predictInField(Picture[] reference, int x, int y, int[][] mbPix, BitReader bits, int motionType, int backward, int fieldNo)
	{
		switch (motionType)
		{
		case 1:
			predict16x16Field(reference, x, y, bits, backward, mbPix);
			break;
		case 2:
			predict16x8MC(reference, x, y, bits, backward, mbPix, 0, 0);
			predict16x8MC(reference, x, y, bits, backward, mbPix, 8, 1);
			break;
		case 3:
			predict16x16DualPrimeField(reference, x, y, bits, mbPix, fieldNo);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 25, 162, 102, 191, 21, 159, 24 })]
	public virtual void predict16x16NoMV(Picture picture, int x, int y, int pictureStructure, int backward, int[][] mbPix)
	{
		if (pictureStructure == 3)
		{
			predictMB(picture, x << 1, mvPred[0][backward][0], y << 1, mvPred[0][backward][1], 16, 16, 0, 0, mbPix, 0, 0);
		}
		else
		{
			predictMB(picture, x << 1, mvPred[0][backward][0], y << 1, mvPred[0][backward][1], 16, 16, 1, pictureStructure - 1, mbPix, 0, 0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 162, 110, 151 })]
	protected internal int getPix1(byte[] @ref, int refW, int refH, int x, int y, int refVertStep, int refVertOff)
	{
		x = MathUtil.clip(x, 0, refW - 1);
		y = MathUtil.clip(y, 0, refH - (1 << refVertStep) + refVertOff);
		return @ref[y * refW + x] + 128;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 162, 110, 110, 108, 110, 140 })]
	protected internal int getPix2(byte[] @ref, int refW, int refH, int x1, int y1, int x2, int y2, int refVertStep, int refVertOff)
	{
		x1 = MathUtil.clip(x1, 0, refW - 1);
		int lastLine = refH - (1 << refVertStep) + refVertOff;
		y1 = MathUtil.clip(y1, 0, lastLine);
		x2 = MathUtil.clip(x2, 0, refW - 1);
		y2 = MathUtil.clip(y2, 0, lastLine);
		return (@ref[y1 * refW + x1] + @ref[y2 * refW + x2] + 1 >> 1) + 128;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 130, 110, 110, 108, 110, 108, 110, 108,
		110, 140
	})]
	protected internal int getPix4(byte[] @ref, int refW, int refH, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int refVertStep, int refVertOff)
	{
		int lastLine = refH - (1 << refVertStep) + refVertOff;
		x1 = MathUtil.clip(x1, 0, refW - 1);
		y1 = MathUtil.clip(y1, 0, lastLine);
		x2 = MathUtil.clip(x2, 0, refW - 1);
		y2 = MathUtil.clip(y2, 0, lastLine);
		x3 = MathUtil.clip(x3, 0, refW - 1);
		y3 = MathUtil.clip(y3, 0, lastLine);
		x4 = MathUtil.clip(x4, 0, refW - 1);
		y4 = MathUtil.clip(y4, 0, lastLine);
		return (@ref[y1 * refW + x1] + @ref[y2 * refW + x2] + @ref[y3 * refW + x3] + @ref[y4 * refW + x4] + 3 >> 2) + 128;
	}

	[LineNumberTable(new byte[]
	{
		159, 134, 130, 127, 4, 135, 106, 106, 54, 137,
		101, 229, 60, 233, 70
	})]
	public virtual void predictFullXFullYSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int offRef = ((refY << refVertStep) + refVertOff) * refW + refX;
		int offTgt = tgtW * tgtY;
		int lfRef = (refW << refVertStep) - tgtW;
		int lfTgt = tgtVertStep * tgtW;
		for (int i = 0; i < tgtH; i++)
		{
			for (int j = 0; j < tgtW; j++)
			{
				int num = offTgt;
				offTgt++;
				int num2 = offRef;
				offRef++;
				tgt[num] = @ref[num2] + 128;
			}
			offRef += lfRef;
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 109, 107, 110, 106, 61, 169, 229,
		59, 234, 71
	})]
	public virtual void predictFullXFullYUnSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int tgtOff = tgtW * tgtY;
		int jump = tgtVertStep * tgtW;
		for (int j = 0; j < tgtH; j++)
		{
			int y = (j + refY << refVertStep) + refVertOff;
			for (int i = 0; i < tgtW; i++)
			{
				int num = tgtOff;
				tgtOff++;
				tgt[num] = getPix1(@ref, refW, refH, i + refX, y, refVertStep, refVertOff);
			}
			tgtOff += jump;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 127, 130, 127, 4, 145, 109, 106, 125, 5,
		201, 101, 229, 58, 236, 72
	})]
	public virtual void predictOddEvenSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int offRef = ((refY << refVertStep) + refVertOff) * refW + refX;
		int offTgt = tgtW * tgtY;
		int lfRef = (refW << refVertStep) - tgtW;
		int lfTgt = tgtVertStep * tgtW;
		int stride = refW << refVertStep;
		for (int i = 0; i < tgtH; i++)
		{
			for (int j = 0; j < tgtW; j++)
			{
				int num = offTgt;
				offTgt++;
				tgt[num] = (@ref[offRef] + @ref[offRef + stride] + 1 >> 1) + 128;
				offRef++;
			}
			offRef += lfRef;
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 98, 109, 107, 110, 113, 106, 63, 4,
		169, 229, 58, 234, 72
	})]
	public virtual void predictOddEvenUnSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int tgtOff = tgtW * tgtY;
		int jump = tgtVertStep * tgtW;
		for (int j = 0; j < tgtH; j++)
		{
			int y1 = (j + refY << refVertStep) + refVertOff;
			int y2 = (j + refY + 1 << refVertStep) + refVertOff;
			for (int i = 0; i < tgtW; i++)
			{
				int num = tgtOff;
				tgtOff++;
				tgt[num] = getPix2(@ref, refW, refH, i + refX, y1, i + refX, y2, refVertStep, refVertOff);
			}
			tgtOff += jump;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 131, 162, 127, 4, 135, 109, 106, 124, 5,
		201, 101, 229, 58, 236, 72
	})]
	public virtual void predictEvenOddSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int offRef = ((refY << refVertStep) + refVertOff) * refW + refX;
		int offTgt = tgtW * tgtY;
		int lfRef = (refW << refVertStep) - tgtW;
		int lfTgt = tgtVertStep * tgtW;
		for (int i = 0; i < tgtH; i++)
		{
			for (int j = 0; j < tgtW; j++)
			{
				int num = offTgt;
				offTgt++;
				tgt[num] = (@ref[offRef] + @ref[offRef + 1] + 1 >> 1) + 128;
				offRef++;
			}
			offRef += lfRef;
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 108, 98, 109, 107, 110, 106, 63, 5, 169,
		229, 59, 234, 71
	})]
	public virtual void predictEvenOddUnSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int tgtOff = tgtW * tgtY;
		int jump = tgtVertStep * tgtW;
		for (int j = 0; j < tgtH; j++)
		{
			int y = (j + refY << refVertStep) + refVertOff;
			for (int i = 0; i < tgtW; i++)
			{
				int num = tgtOff;
				tgtOff++;
				tgt[num] = getPix2(@ref, refW, refH, i + refX, y, i + refX + 1, y, refVertStep, refVertOff);
			}
			tgtOff += jump;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 123, 98, 127, 4, 145, 109, 106, 127, 13,
		5, 201, 101, 229, 58, 236, 72
	})]
	public virtual void predictOddOddSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int offRef = ((refY << refVertStep) + refVertOff) * refW + refX;
		int offTgt = tgtW * tgtY;
		int lfRef = (refW << refVertStep) - tgtW;
		int lfTgt = tgtVertStep * tgtW;
		int stride = refW << refVertStep;
		for (int i = 0; i < tgtH; i++)
		{
			for (int j = 0; j < tgtW; j++)
			{
				int num = offTgt;
				offTgt++;
				tgt[num] = (@ref[offRef] + @ref[offRef + 1] + @ref[offRef + stride] + @ref[offRef + stride + 1] + 3 >> 2) + 128;
				offRef++;
			}
			offRef += lfRef;
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 109, 107, 110, 113, 106, 103, 31,
		11, 233, 69, 229, 56, 234, 74
	})]
	public virtual void predictOddOddUnSafe(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int tgtOff = tgtW * tgtY;
		int jump = tgtVertStep * tgtW;
		for (int j = 0; j < tgtH; j++)
		{
			int y1 = (j + refY << refVertStep) + refVertOff;
			int y2 = (j + refY + 1 << refVertStep) + refVertOff;
			for (int i = 0; i < tgtW; i++)
			{
				int ptX = i + refX;
				int num = tgtOff;
				tgtOff++;
				tgt[num] = getPix4(@ref, refW, refH, ptX, y1, ptX + 1, y1, ptX, y2, ptX + 1, y2, refVertStep, refVertOff);
			}
			tgtOff += jump;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 65, 98, 137, 156, 123, 123 })]
	protected internal virtual void predict16x16Field(Picture[] reference, int x, int y, BitReader bits, int backward, int[][] mbPix)
	{
		int field = bits.read1Bit();
		predictGeneric(reference[field], x, y, bits, backward, mbPix, 0, 16, 16, 1, field, 0, 0, 0);
		mvPred[1][backward][0] = mvPred[0][backward][0];
		mvPred[1][backward][1] = mvPred[0][backward][1];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 67, 162, 137, 127, 3 })]
	private void predict16x8MC(Picture[] reference, int x, int y, BitReader bits, int backward, int[][] mbPix, int vertPos, int vectIdx)
	{
		int field = bits.read1Bit();
		predictGeneric(reference[field], x, y + vertPos, bits, backward, mbPix, vertPos, 16, 8, 1, field, 0, vectIdx, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 66, 127, 1, 144, 127, 1, 144, 111,
		143, 112, 112, 112, 144, 159, 68, 104, 104, 121,
		153, 127, 0, 51, 134, 127, 0, 63, 0, 134,
		127, 0, 63, 0, 166, 105, 105, 122, 122, 103,
		127, 0, 51, 134, 127, 0, 63, 0, 134, 127,
		0, 63, 0, 166, 105, 110, 62, 41, 233, 69,
		127, 9, 127, 9
	})]
	private void predict16x16DualPrimeField(Picture[] reference, int x, int y, BitReader bits, int[][] mbPix, int fieldNo)
	{
		int vect1X = mvectDecode(bits, fCode[0][0], mvPred[0][0][0]);
		int dmX = MPEGConst.___003C_003EvlcDualPrime.readVLC(bits) - 1;
		int vect1Y = mvectDecode(bits, fCode[0][1], mvPred[0][0][1]);
		int dmY = MPEGConst.___003C_003EvlcDualPrime.readVLC(bits) - 1;
		int vect2X = dpXField(vect1X, dmX, 1 - fieldNo);
		int vect2Y = dpYField(vect1Y, dmY, 1 - fieldNo);
		int ch = ((chromaFormat == 1) ? 1 : 0);
		int cw = ((chromaFormat != 3) ? 1 : 0);
		int sh = ((chromaFormat != 1) ? 1 : 2);
		int sw = ((chromaFormat == 3) ? 1 : 2);
		int[] array = new int[2];
		int num = (array[1] = 256);
		num = (array[0] = 3);
		int[][] mbPix2 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 256);
		num = (array[0] = 3);
		int[][] mbPix3 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int refX1 = (x << 1) + vect1X;
		int refY1 = (y << 1) + vect1Y;
		int refX1Chr = (x << 1 >> cw) + ((sw != -1) ? (vect1X / sw) : (-vect1X));
		int refY1Chr = (y << 1 >> ch) + ((sh != -1) ? (vect1Y / sh) : (-vect1Y));
		predictPlane(reference[fieldNo].getPlaneData(0), refX1, refY1, reference[fieldNo].getPlaneWidth(0), reference[fieldNo].getPlaneHeight(0), 1, fieldNo, mbPix2[0], 0, 16, 16, 0);
		predictPlane(reference[fieldNo].getPlaneData(1), refX1Chr, refY1Chr, reference[fieldNo].getPlaneWidth(1), reference[fieldNo].getPlaneHeight(1), 1, fieldNo, mbPix2[1], 0, 16 >> cw, 16 >> ch, 0);
		predictPlane(reference[fieldNo].getPlaneData(2), refX1Chr, refY1Chr, reference[fieldNo].getPlaneWidth(2), reference[fieldNo].getPlaneHeight(2), 1, fieldNo, mbPix2[2], 0, 16 >> cw, 16 >> ch, 0);
		int refX2 = (x << 1) + vect2X;
		int refY2 = (y << 1) + vect2Y;
		int refX2Chr = (x << 1 >> cw) + ((sw != -1) ? (vect2X / sw) : (-vect2X));
		int refY2Chr = (y << 1 >> ch) + ((sh != -1) ? (vect2Y / sh) : (-vect2Y));
		int opposite = 1 - fieldNo;
		predictPlane(reference[opposite].getPlaneData(0), refX2, refY2, reference[opposite].getPlaneWidth(0), reference[opposite].getPlaneHeight(0), 1, opposite, mbPix3[0], 0, 16, 16, 0);
		predictPlane(reference[opposite].getPlaneData(1), refX2Chr, refY2Chr, reference[opposite].getPlaneWidth(1), reference[opposite].getPlaneHeight(1), 1, opposite, mbPix3[1], 0, 16 >> cw, 16 >> ch, 0);
		predictPlane(reference[opposite].getPlaneData(2), refX2Chr, refY2Chr, reference[opposite].getPlaneWidth(2), reference[opposite].getPlaneHeight(2), 1, opposite, mbPix3[2], 0, 16 >> cw, 16 >> ch, 0);
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < (nint)mbPix[i].LongLength; j++)
			{
				mbPix[i][j] = mbPix2[i][j] + mbPix3[i][j] + 1 >> 1;
			}
		}
		int[] obj = mvPred[1][0];
		int[] obj2 = mvPred[0][0];
		num = vect1X;
		int num2 = 0;
		array = obj2;
		int num3 = num;
		array[num2] = num;
		obj[0] = num3;
		int[] obj3 = mvPred[1][0];
		int[] obj4 = mvPred[0][0];
		num = vect1Y;
		num2 = 1;
		array = obj4;
		int num4 = num;
		array[num2] = num;
		obj3[1] = num4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 32, 98, 102, 105, 121, 106, 105, 155, 123,
		123, 159, 18
	})]
	private void predictFieldInFrame(Picture reference, int x, int y, int[][] mbPix, BitReader bits, int backward, int spatial_temporal_weight_code)
	{
		y >>= 1;
		int field = bits.read1Bit();
		predictGeneric(reference, x, y, bits, backward, mbPix, 0, 16, 8, 1, field, 1, 0, 1);
		if (spatial_temporal_weight_code == 0 || spatial_temporal_weight_code == 1)
		{
			field = bits.read1Bit();
			predictGeneric(reference, x, y, bits, backward, mbPix, 1, 16, 8, 1, field, 1, 1, 1);
		}
		else
		{
			mvPred[1][backward][0] = mvPred[0][backward][0];
			mvPred[1][backward][1] = mvPred[0][backward][1];
			predictMB(reference, mvPred[1][backward][0], 0, mvPred[1][backward][1], 0, 16, 8, 1, 1 - field, mbPix, 1, 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 45, 162, 154, 123, 123 })]
	protected internal virtual void predict16x16Frame(Picture reference, int x, int y, BitReader bits, int backward, int[][] mbPix)
	{
		predictGeneric(reference, x, y, bits, backward, mbPix, 0, 16, 16, 0, 0, 0, 0, 0);
		mvPred[1][backward][0] = mvPred[0][backward][0];
		mvPred[1][backward][1] = mvPred[0][backward][1];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 63, 162, 127, 1, 144, 127, 3, 144, 143,
		112, 114, 103, 112, 146, 112, 112, 112, 144, 159,
		68, 104, 102, 121, 151, 124, 49, 134, 124, 61,
		134, 124, 61, 166, 124, 49, 134, 124, 61, 134,
		124, 61, 166, 105, 103, 122, 120, 124, 49, 134,
		124, 61, 134, 124, 61, 166, 105, 103, 122, 120,
		124, 49, 134, 124, 61, 134, 124, 61, 166, 105,
		110, 62, 41, 233, 69, 127, 9, 127, 11
	})]
	private void predict16x16DualPrimeFrame(Picture[] reference, int x, int y, BitReader bits, int backward, int[][] mbPix)
	{
		int vect1X = mvectDecode(bits, fCode[0][0], mvPred[0][0][0]);
		int dmX = MPEGConst.___003C_003EvlcDualPrime.readVLC(bits) - 1;
		int vect1Y = mvectDecode(bits, fCode[0][1], mvPred[0][0][1] >> 1);
		int dmY = MPEGConst.___003C_003EvlcDualPrime.readVLC(bits) - 1;
		int k = (topFieldFirst ? 1 : 3);
		int vect2X = (vect1X * k + ((vect1X > 0) ? 1 : 0) >> 1) + dmX;
		int vect2Y = (vect1Y * k + ((vect1Y > 0) ? 1 : 0) >> 1) + dmY - 1;
		k = 4 - k;
		int vect3X = (vect1X * k + ((vect1X > 0) ? 1 : 0) >> 1) + dmX;
		int vect3Y = (vect1Y * k + ((vect1Y > 0) ? 1 : 0) >> 1) + dmY + 1;
		int ch = ((chromaFormat == 1) ? 1 : 0);
		int cw = ((chromaFormat != 3) ? 1 : 0);
		int sh = ((chromaFormat != 1) ? 1 : 2);
		int sw = ((chromaFormat == 3) ? 1 : 2);
		int[] array = new int[2];
		int num = (array[1] = 256);
		num = (array[0] = 3);
		int[][] mbPix2 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 256);
		num = (array[0] = 3);
		int[][] mbPix3 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int refX1 = (x << 1) + vect1X;
		int refY1 = y + vect1Y;
		int refX1Chr = (x << 1 >> cw) + ((sw != -1) ? (vect1X / sw) : (-vect1X));
		int refY1Chr = (y >> ch) + ((sh != -1) ? (vect1Y / sh) : (-vect1Y));
		predictPlane(reference[0].getPlaneData(0), refX1, refY1, reference[0].getPlaneWidth(0), reference[0].getPlaneHeight(0), 1, 0, mbPix2[0], 0, 16, 8, 1);
		predictPlane(reference[0].getPlaneData(1), refX1Chr, refY1Chr, reference[0].getPlaneWidth(1), reference[0].getPlaneHeight(1), 1, 0, mbPix2[1], 0, 16 >> cw, 8 >> ch, 1);
		predictPlane(reference[0].getPlaneData(2), refX1Chr, refY1Chr, reference[0].getPlaneWidth(2), reference[0].getPlaneHeight(2), 1, 0, mbPix2[2], 0, 16 >> cw, 8 >> ch, 1);
		predictPlane(reference[1].getPlaneData(0), refX1, refY1, reference[1].getPlaneWidth(0), reference[1].getPlaneHeight(0), 1, 1, mbPix2[0], 1, 16, 8, 1);
		predictPlane(reference[1].getPlaneData(1), refX1Chr, refY1Chr, reference[1].getPlaneWidth(1), reference[1].getPlaneHeight(1), 1, 1, mbPix2[1], 1, 16 >> cw, 8 >> ch, 1);
		predictPlane(reference[1].getPlaneData(2), refX1Chr, refY1Chr, reference[1].getPlaneWidth(2), reference[1].getPlaneHeight(2), 1, 1, mbPix2[2], 1, 16 >> cw, 8 >> ch, 1);
		int refX2 = (x << 1) + vect2X;
		int refY2 = y + vect2Y;
		int refX2Chr = (x << 1 >> cw) + ((sw != -1) ? (vect2X / sw) : (-vect2X));
		int refY2Chr = (y >> ch) + ((sh != -1) ? (vect2Y / sh) : (-vect2Y));
		predictPlane(reference[1].getPlaneData(0), refX2, refY2, reference[1].getPlaneWidth(0), reference[1].getPlaneHeight(0), 1, 1, mbPix3[0], 0, 16, 8, 1);
		predictPlane(reference[1].getPlaneData(1), refX2Chr, refY2Chr, reference[1].getPlaneWidth(1), reference[1].getPlaneHeight(1), 1, 1, mbPix3[1], 0, 16 >> cw, 8 >> ch, 1);
		predictPlane(reference[1].getPlaneData(2), refX2Chr, refY2Chr, reference[1].getPlaneWidth(2), reference[1].getPlaneHeight(2), 1, 1, mbPix3[2], 0, 16 >> cw, 8 >> ch, 1);
		int refX3 = (x << 1) + vect3X;
		int refY3 = y + vect3Y;
		int refX3Chr = (x << 1 >> cw) + ((sw != -1) ? (vect3X / sw) : (-vect3X));
		int refY3Chr = (y >> ch) + ((sh != -1) ? (vect3Y / sh) : (-vect3Y));
		predictPlane(reference[0].getPlaneData(0), refX3, refY3, reference[0].getPlaneWidth(0), reference[0].getPlaneHeight(0), 1, 0, mbPix3[0], 1, 16, 8, 1);
		predictPlane(reference[0].getPlaneData(1), refX3Chr, refY3Chr, reference[0].getPlaneWidth(1), reference[0].getPlaneHeight(1), 1, 0, mbPix3[1], 1, 16 >> cw, 8 >> ch, 1);
		predictPlane(reference[0].getPlaneData(2), refX3Chr, refY3Chr, reference[0].getPlaneWidth(2), reference[0].getPlaneHeight(2), 1, 0, mbPix3[2], 1, 16 >> cw, 8 >> ch, 1);
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < (nint)mbPix[i].LongLength; j++)
			{
				mbPix[i][j] = mbPix2[i][j] + mbPix3[i][j] + 1 >> 1;
			}
		}
		int[] obj = mvPred[1][0];
		int[] obj2 = mvPred[0][0];
		num = vect1X;
		int num2 = 0;
		array = obj2;
		int num3 = num;
		array[num2] = num;
		obj[0] = num3;
		int[] obj3 = mvPred[1][0];
		int[] obj4 = mvPred[0][0];
		num = vect1Y << 1;
		num2 = 1;
		array = obj4;
		int num4 = num;
		array[num2] = num;
		obj3[1] = num4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 43, 130, 109, 100, 131, 101, 199, 104, 101,
		99, 101, 106, 107, 133, 100, 100, 133
	})]
	private int mvectDecode(BitReader bits, int fcode, int pred)
	{
		int code = MPEGConst.___003C_003EvlcMotionCode.readVLC(bits);
		if (code == 0)
		{
			return pred;
		}
		if (code < 0)
		{
			return 65535;
		}
		int sign = bits.read1Bit();
		int shift = fcode - 1;
		int val = code;
		if (shift > 0)
		{
			val = val - 1 << shift;
			val |= bits.readNBit(shift);
			val++;
		}
		if (sign != 0)
		{
			val = -val;
		}
		val += pred;
		int result = sign_extend(val, 5 + shift);
		
		return result;
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(298)]
	private int dpXField(int vect1x, int dmX, int topField)
	{
		return (vect1x + ((vect1x > 0) ? 1 : 0) >> 1) + dmX;
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(294)]
	private int dpYField(int vect1y, int dmY, int topField)
	{
		return (vect1y + ((vect1y > 0) ? 1 : 0) >> 1) + (1 - (topField << 1)) + dmY;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 98, 137, 127, 5, 105, 102, 100, 191,
		2, 191, 2, 100, 191, 2, 191, 2, 102, 100,
		158, 190, 100, 158, 190
	})]
	public virtual void predictPlane(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int rx = refX >> 1;
		int ry = refY >> 1;
		int safe = ((rx >= 0 && ry >= 0 && rx + tgtW < refW && ry + tgtH << refVertStep < refH) ? 1 : 0);
		if ((refX & 1) == 0)
		{
			if ((refY & 1) == 0)
			{
				if (safe != 0)
				{
					predictFullXFullYSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
				}
				else
				{
					predictFullXFullYUnSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
				}
			}
			else if (safe != 0)
			{
				predictOddEvenSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
			else
			{
				predictOddEvenUnSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
		}
		else if ((refY & 1) == 0)
		{
			if (safe != 0)
			{
				predictEvenOddSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
			else
			{
				predictEvenOddUnSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
		}
		else if (safe != 0)
		{
			predictOddOddSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
		}
		else
		{
			predictOddOddUnSafe(@ref, rx, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 35, 98, 127, 4, 159, 10, 190, 112, 118 })]
	protected internal virtual void predictGeneric(Picture reference, int x, int y, BitReader bits, int backward, int[][] mbPix, int tgtY, int blkW, int blkH, int isSrcField, int srcField, int isDstField, int vectIdx, int predScale)
	{
		int vectX = mvectDecode(bits, fCode[backward][0], mvPred[vectIdx][backward][0]);
		int vectY = mvectDecode(bits, fCode[backward][1], mvPred[vectIdx][backward][1] >> predScale);
		predictMB(reference, x << 1, vectX, y << 1, vectY, blkW, blkH, isSrcField, srcField, mbPix, tgtY, isDstField);
		mvPred[vectIdx][backward][0] = vectX;
		mvPred[vectIdx][backward][1] = vectY << predScale;
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 37, 162, 102 })]
	private int sign_extend(int val, int bits)
	{
		int shift = 32 - bits;
		return val << shift >> shift;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 28, 66, 111, 143, 111, 143, 159, 21, 127,
		25, 63, 1, 134, 127, 25, 63, 1, 136
	})]
	public virtual void predictMB(Picture @ref, int refX, int vectX, int refY, int vectY, int blkW, int blkH, int refVertStep, int refVertOff, int[][] tgt, int tgtY, int tgtVertStep)
	{
		int ch = ((chromaFormat == 1) ? 1 : 0);
		int cw = ((chromaFormat != 3) ? 1 : 0);
		int sh = ((chromaFormat != 1) ? 1 : 2);
		int sw = ((chromaFormat == 3) ? 1 : 2);
		predictPlane(@ref.getPlaneData(0), refX + vectX, refY + vectY, @ref.getPlaneWidth(0), @ref.getPlaneHeight(0), refVertStep, refVertOff, tgt[0], tgtY, blkW, blkH, tgtVertStep);
		predictPlane(@ref.getPlaneData(1), (refX >> cw) + ((sw != -1) ? (vectX / sw) : (-vectX)), (refY >> ch) + ((sh != -1) ? (vectY / sh) : (-vectY)), @ref.getPlaneWidth(1), @ref.getPlaneHeight(1), refVertStep, refVertOff, tgt[1], tgtY, blkW >> cw, blkH >> ch, tgtVertStep);
		predictPlane(@ref.getPlaneData(2), (refX >> cw) + ((sw != -1) ? (vectX / sw) : (-vectX)), (refY >> ch) + ((sh != -1) ? (vectY / sh) : (-vectY)), @ref.getPlaneWidth(2), @ref.getPlaneHeight(2), refVertStep, refVertOff, tgt[2], tgtY, blkW >> cw, blkH >> ch, tgtVertStep);
	}
}
