using System.Runtime.CompilerServices;
using IKVM.Attributes;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg12;

public class MPEGPredOct : MPEGPred
{
	private int[] tmp;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] COEFF;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 86, 66, 138, 103, 145, 106, 109, 135, 104,
		142, 109, 109, 63, 45, 244, 69, 102, 230, 57,
		236, 73
	})]
	private void predictFullXSubYSafe(byte[] @ref, int rx, int ry, int iy, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int[] coeff = COEFF[iy];
		int offTgt = tgtW * tgtY;
		int offRef = ((ry << refVertStep) + refVertOff) * refW + rx;
		int singleRefW = refW << refVertStep;
		int dblRefW = refW << 1 + refVertStep;
		int tripleRefW = dblRefW + singleRefW;
		int lfTgt = tgtVertStep * tgtW;
		int lfRef = (refW << refVertStep) - tgtW;
		for (int i = 0; i < tgtH; i++)
		{
			int j = 0;
			while (j < tgtW)
			{
				tgt[offTgt] = (@ref[offRef - dblRefW] * coeff[0] + @ref[offRef - singleRefW] * coeff[1] + @ref[offRef] * coeff[2] + @ref[offRef + singleRefW] * coeff[3] + @ref[offRef + dblRefW] * coeff[4] + @ref[offRef + tripleRefW] * coeff[5] + 64 >> 7) + 128;
				j++;
				offTgt++;
				offRef++;
			}
			offRef += lfRef;
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 90, 66, 138, 103, 135, 107, 111, 106, 63,
		5, 169, 229, 59, 234, 71
	})]
	private void predictFullXSubYUnSafe(byte[] @ref, int rx, int ry, int iy, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int[] coeff = COEFF[iy];
		int tgtOff = tgtW * tgtY;
		int lfTgt = tgtVertStep * tgtW;
		for (int i = 0; i < tgtH; i++)
		{
			int y = (i + ry << refVertStep) + refVertOff;
			for (int j = 0; j < tgtW; j++)
			{
				int num = tgtOff;
				tgtOff++;
				tgt[num] = getPix6Vert(@ref, refW, refH, j + rx, y, refVertStep, refVertOff, coeff) + 64 >> 7;
			}
			tgtOff += lfTgt;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 95, 98, 137, 114, 103, 109, 136, 109, 109,
		63, 46, 208, 101, 230, 58, 236, 72
	})]
	private void predictSubXFullYSafe(byte[] @ref, int rx, int ix, int ry, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int[] coeff = COEFF[ix];
		int offRef = ((ry << refVertStep) + refVertOff) * refW + rx;
		int offTgt = tgtW * tgtY;
		int lfRef = (refW << refVertStep) - tgtW;
		int lfTgt = tgtVertStep * tgtW;
		for (int i = 0; i < tgtH; i++)
		{
			int j = 0;
			while (j < tgtW)
			{
				int num = offTgt;
				offTgt++;
				tgt[num] = (@ref[offRef - 2] * coeff[0] + @ref[offRef - 1] * coeff[1] + @ref[offRef] * coeff[2] + @ref[offRef + 1] * coeff[3] + @ref[offRef + 2] * coeff[4] + @ref[offRef + 3] * coeff[5] + 64 >> 7) + 128;
				j++;
				offRef++;
			}
			offRef += lfRef;
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 98, 137, 103, 135, 107, 112, 106, 63,
		5, 169, 229, 59, 234, 71
	})]
	private void predictSubXFullYUnSafe(byte[] @ref, int rx, int ix, int ry, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int[] coeff = COEFF[ix];
		int tgtOff = tgtW * tgtY;
		int lfTgt = tgtVertStep * tgtW;
		for (int i = 0; i < tgtH; i++)
		{
			int y = (i + ry << refVertStep) + refVertOff;
			for (int j = 0; j < tgtW; j++)
			{
				int num = tgtOff;
				tgtOff++;
				tgt[num] = getPix6(@ref, refW, refH, j + rx, y, refVertStep, refVertOff, coeff) + 64 >> 7;
			}
			tgtOff += lfTgt;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 107, 162, 137, 116, 103, 109, 104, 103, 136,
		114, 109, 63, 36, 214, 229, 59, 236, 72, 106,
		113, 109, 63, 87, 246, 69, 230, 58, 236, 72
	})]
	private void predictSubXSubYSafe(byte[] @ref, int rx, int ix, int ry, int iy, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int[] coeff = COEFF[ix];
		int offRef = ((ry - 2 << refVertStep) + refVertOff) * refW + rx;
		int offTgt = tgtW * tgtY;
		int lfRef = (refW << refVertStep) - tgtW;
		int lfTgt = tgtVertStep * tgtW;
		int dblTgtW = tgtW << 1;
		int tripleTgtW = dblTgtW + tgtW;
		int j = 0;
		int offTmp2 = 0;
		for (; j < tgtH + 5; j++)
		{
			int l = 0;
			while (l < tgtW)
			{
				tmp[offTmp2] = @ref[offRef - 2] * coeff[0] + @ref[offRef - 1] * coeff[1] + @ref[offRef] * coeff[2] + @ref[offRef + 1] * coeff[3] + @ref[offRef + 2] * coeff[4] + @ref[offRef + 3] * coeff[5];
				l++;
				offTmp2++;
				offRef++;
			}
			offRef += lfRef;
		}
		coeff = COEFF[iy];
		int i = 0;
		int offTmp = dblTgtW;
		for (; i < tgtH; i++)
		{
			int k = 0;
			while (k < tgtW)
			{
				tgt[offTgt] = (tmp[offTmp - dblTgtW] * coeff[0] + tmp[offTmp - tgtW] * coeff[1] + tmp[offTmp] * coeff[2] + tmp[offTmp + tgtW] * coeff[3] + tmp[offTmp + dblTgtW] * coeff[4] + tmp[offTmp + tripleTgtW] * coeff[5] + 8192 >> 14) + 128;
				k++;
				offTmp++;
				offTgt++;
			}
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 113, 66, 103, 102, 102, 135, 106, 112, 113,
		106, 63, 2, 15, 236, 71, 107, 112, 109, 63,
		84, 246, 69, 229, 58, 236, 72
	})]
	private void predictSubXSubYUnSafe(byte[] @ref, int rx, int ix, int ry, int iy, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int offTgt = tgtW * tgtY;
		int dblTgtW = tgtW << 1;
		int tripleTgtW = dblTgtW + tgtW;
		int lfTgt = tgtVertStep * tgtW;
		int[] coeff = COEFF[ix];
		int j = -2;
		int offTmp2 = 0;
		for (; j < tgtH + 3; j++)
		{
			int y = (j + ry << refVertStep) + refVertOff;
			int l = 0;
			while (l < tgtW)
			{
				tmp[offTmp2] = getPix6(@ref, refW, refH, l + rx, y, refVertStep, refVertOff, coeff);
				l++;
				offTmp2++;
			}
		}
		coeff = COEFF[iy];
		int i = 0;
		int offTmp = dblTgtW;
		for (; i < tgtH; i++)
		{
			int k = 0;
			while (k < tgtW)
			{
				tgt[offTgt] = tmp[offTmp - dblTgtW] * coeff[0] + tmp[offTmp - tgtW] * coeff[1] + tmp[offTmp] * coeff[2] + tmp[offTmp + tgtW] * coeff[3] + tmp[offTmp + dblTgtW] * coeff[4] + tmp[offTmp + tripleTgtW] * coeff[5] + 8192 >> 14;
				k++;
				offTmp++;
				offTgt++;
			}
			offTgt += lfTgt;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 110, 111, 111, 109, 112, 112, 112,
		143
	})]
	protected internal virtual int getPix6(byte[] @ref, int refW, int refH, int x, int y, int refVertStep, int refVertOff, int[] coeff)
	{
		int lastLine = refH - (1 << refVertStep) + refVertOff;
		int x2 = MathUtil.clip(x - 2, 0, refW - 1);
		int x3 = MathUtil.clip(x - 1, 0, refW - 1);
		int x4 = MathUtil.clip(x, 0, refW - 1);
		int x5 = MathUtil.clip(x + 1, 0, refW - 1);
		int x6 = MathUtil.clip(x + 2, 0, refW - 1);
		int x7 = MathUtil.clip(x + 3, 0, refW - 1);
		int off = MathUtil.clip(y, refVertOff, lastLine) * refW;
		return @ref[off + x2] * coeff[0] + @ref[off + x3] * coeff[1] + @ref[off + x4] * coeff[2] + @ref[off + x5] * coeff[3] + @ref[off + x6] * coeff[4] + @ref[off + x7] * coeff[5] + 16384;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 117, 66, 110, 116, 116, 107, 117, 117, 117,
		142
	})]
	protected internal virtual int getPix6Vert(byte[] @ref, int refW, int refH, int x, int y, int refVertStep, int refVertOff, int[] coeff)
	{
		int lastLine = refH - (1 << refVertStep) + refVertOff;
		int y2 = MathUtil.clip(y - (2 << refVertStep), refVertOff, lastLine);
		int y3 = MathUtil.clip(y - (1 << refVertStep), refVertOff, lastLine);
		int y4 = MathUtil.clip(y, 0, lastLine);
		int y5 = MathUtil.clip(y + (1 << refVertStep), refVertOff, lastLine);
		int y6 = MathUtil.clip(y + (2 << refVertStep), refVertOff, lastLine);
		int y7 = MathUtil.clip(y + (3 << refVertStep), refVertOff, lastLine);
		x = MathUtil.clip(x, 0, refW - 1);
		return @ref[y2 * refW + x] * coeff[0] + @ref[y3 * refW + x] * coeff[1] + @ref[y4 * refW + x] * coeff[2] + @ref[y5 * refW + x] * coeff[3] + @ref[y6 * refW + x] * coeff[4] + @ref[y7 * refW + x] * coeff[5] + 16384;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 123, 113 })]
	public MPEGPredOct(MPEGPred other)
		: base(other.fCode, other.chromaFormat, other.topFieldFirst)
	{
		tmp = new int[336];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 105, 103, 135, 127, 9, 105, 102,
		100, 191, 2, 191, 2, 100, 191, 5, 191, 5,
		102, 100, 191, 5, 191, 2, 100, 191, 5, 191,
		5
	})]
	public override void predictPlane(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		int rx = refX >> 3;
		int ry = refY >> 3;
		tgtW >>= 3;
		tgtH >>= 3;
		int safe = ((rx >= 2 && ry >= 2 && rx + tgtW + 3 < refW && ry + tgtH + 3 << refVertStep < refH) ? 1 : 0);
		if ((refX & 7) == 0)
		{
			if ((refY & 7) == 0)
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
				predictFullXSubYSafe(@ref, rx, ry, refY & 7, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
			else
			{
				predictFullXSubYUnSafe(@ref, rx, ry, refY & 7, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
		}
		else if ((refY & 7) == 0)
		{
			if (safe != 0)
			{
				predictSubXFullYSafe(@ref, rx, refX & 7, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
			else
			{
				predictSubXFullYUnSafe(@ref, rx, refX & 7, ry, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
			}
		}
		else if (safe != 0)
		{
			predictSubXSubYSafe(@ref, rx, refX & 7, ry, refY & 7, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
		}
		else
		{
			predictSubXSubYUnSafe(@ref, rx, refX & 7, ry, refY & 7, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW, tgtH, tgtVertStep);
		}
	}

	[LineNumberTable(19)]
	static MPEGPredOct()
	{
		COEFF = new int[8][]
		{
			new int[6] { 0, 0, 128, 0, 0, 0 },
			new int[6] { 0, -6, 123, 12, -1, 0 },
			new int[6] { 2, -11, 108, 36, -8, 1 },
			new int[6] { 0, -9, 93, 50, -6, 0 },
			new int[6] { 3, -16, 77, 77, -16, 3 },
			new int[6] { 0, -6, 50, 93, -9, 0 },
			new int[6] { 1, -8, 36, 108, -11, 2 },
			new int[6] { 0, -1, 12, 123, -6, 0 }
		};
	}
}
