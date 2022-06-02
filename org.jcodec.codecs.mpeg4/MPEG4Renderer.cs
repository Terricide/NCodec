using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4Renderer : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 65, 68, 108, 105, 113, 123, 105, 142,
		177, 170, 142
	})]
	public static void renderInter(MPEG4DecodingContext ctx, Picture[] refs, Macroblock mb, int fcode, int @ref, bool bvop)
	{
		if (mb.coded)
		{
			if (mb.mcsel)
			{
				
				throw new RuntimeException("GMC");
			}
			if (mb.mode == 0 || mb.mode == 1 || mb.mode == 2)
			{
				if (mb.fieldPred)
				{
					
					throw new RuntimeException("interlaced");
				}
				renderMBInter(ctx, refs, mb, @ref, bvop);
			}
			else
			{
				renderIntra(mb, ctx);
			}
		}
		else
		{
			renderMBInter(ctx, refs, mb, @ref, bvop);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 112, 111, 107, 111, 140, 110, 110,
		110, 112
	})]
	internal static void validateVector(Macroblock.Vector[] mvs, MPEG4DecodingContext ctx, int xPos, int yPos)
	{
		int shift = 5 + (ctx.quarterPel ? 1 : 0);
		int xHigh = ctx.mbWidth - xPos << shift;
		int xLow = -xPos - 1 << shift;
		int yHigh = ctx.mbHeight - yPos << shift;
		int yLow = -yPos - 1 << shift;
		checkMV(mvs[0], xHigh, xLow, yHigh, yLow);
		checkMV(mvs[1], xHigh, xLow, yHigh, yLow);
		checkMV(mvs[2], xHigh, xLow, yHigh, yLow);
		checkMV(mvs[3], xHigh, xLow, yHigh, yLow);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 98, 105, 106, 140, 198 })]
	internal static int calcChromaMv(MPEG4DecodingContext ctx, int ret)
	{
		if (ctx.quarterPel)
		{
			ret = ((ctx.bsVersion > 1) ? (ret / 2) : ((ret >> 1) | (ret & 1)));
		}
		return (ret >> 1) + MPEG4Consts.ROUNDTAB_79[ret & 3];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 129, 67, 108, 109, 131, 103, 100, 155,
		249, 60, 236, 71, 100, 159, 19, 191, 16, 100,
		159, 8, 191, 6, 112
	})]
	internal static int calcChromaMvAvg(MPEG4DecodingContext ctx, Macroblock.Vector[] mv, bool x)
	{
		int ret;
		if (!ctx.quarterPel)
		{
			ret = ((!x) ? (mv[0].y + mv[1].y + mv[2].y + mv[3].y) : (mv[0].x + mv[1].x + mv[2].x + mv[3].x));
		}
		else if (ctx.bsVersion > 1)
		{
			ret = ((!x) ? (mv[0].y / 2 + mv[1].y / 2 + mv[2].y / 2 + mv[3].y / 2) : (mv[0].x / 2 + mv[1].x / 2 + mv[2].x / 2 + mv[3].x / 2));
		}
		else
		{
			ret = 0;
			for (int z = 0; z < 4; z++)
			{
				ret = ((!x) ? (ret + ((mv[z].y >> 1) | (mv[z].y & 1))) : (ret + ((mv[z].x >> 1) | (mv[z].x & 1))));
			}
		}
		return (ret >> 3) + MPEG4Consts.ROUNDTAB_76[ret & 0xF];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 127, 9 })]
	public static void renderIntra(Macroblock mb, MPEG4DecodingContext ctx)
	{
		MPEG4DCT.idctPut(mb.pred, mb.block, (ctx.interlacing && mb.fieldDCT) ? true : false);
	}

	[LineNumberTable(new byte[]
	{
		159, 135, 66, 106, 106, 106, 168, 106, 106, 107,
		137
	})]
	private static void checkMV(Macroblock.Vector mv, int xHigh, int xLow, int yHigh, int yLow)
	{
		if (mv.x > xHigh)
		{
			mv.x = xHigh;
		}
		else if (mv.x < xLow)
		{
			mv.x = xLow;
		}
		if (mv.y > yHigh)
		{
			mv.y = yHigh;
		}
		else if (mv.y < yLow)
		{
			mv.y = yLow;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		123,
		161,
		68,
		136,
		103,
		63,
		4,
		199,
		148,
		106,
		107,
		107,
		107,
		107,
		139,
		112,
		134,
		113,
		145,
		105,
		127,
		11,
		44,
		168,
		127,
		11,
		44,
		166,
		102,
		107,
		139,
		134,
		107,
		106,
		108,
		159,
		17,
		159,
		19,
		159,
		23,
		191,
		30,
		159,
		19,
		159,
		21,
		159,
		25,
		byte.MaxValue,
		27,
		70,
		127,
		17,
		44,
		134,
		127,
		17,
		44,
		166,
		105,
		105,
		108,
		115,
		byte.MaxValue,
		5,
		61,
		233,
		71
	})]
	internal static void renderMBInter(MPEG4DecodingContext ctx, Picture[] refs, Macroblock mb, int @ref, bool bvop)
	{
		Macroblock.Vector[] mv = new Macroblock.Vector[4];
		for (int j = 0; j < 4; j++)
		{
			mv[j] = new Macroblock.Vector(mb.mvs[j].x, mb.mvs[j].y);
		}
		validateVector(mv, ctx, mb.x, mb.y);
		int mbX = mb.x << 4;
		int mbY = mb.y << 4;
		int codedW = ctx.mbWidth << 4;
		int codedH = ctx.mbHeight << 4;
		int codedWcr = ctx.mbWidth << 3;
		int codedHcr = ctx.mbHeight << 3;
		int uv_dx;
		int uv_dy;
		if (mb.mode != 2 || bvop)
		{
			Picture backward = refs[@ref];
			uv_dx = calcChromaMv(ctx, mv[0].x);
			uv_dy = calcChromaMv(ctx, mv[0].y);
			if (ctx.quarterPel)
			{
				MPEG4Interpolator.interpolate16x16QP(mb.pred[0], backward.getPlaneData(0), mbX, mbY, codedW, codedH, mv[0].x, mv[0].y, backward.getWidth(), ctx.rounding);
			}
			else
			{
				MPEG4Interpolator.interpolate16x16Planar(mb.pred[0], backward.getPlaneData(0), mbX, mbY, codedW, codedH, mv[0].x, mv[0].y, backward.getWidth(), ctx.rounding);
			}
		}
		else
		{
			uv_dx = calcChromaMvAvg(ctx, mv, x: true);
			uv_dy = calcChromaMvAvg(ctx, mv, x: false);
			Picture backward2 = refs[0];
			byte[] lumaPlane = backward2.getPlaneData(0);
			int lumaStride = backward2.getWidth();
			if (ctx.quarterPel)
			{
				MPEG4Interpolator.interpolate8x8QP(mb.pred[0], 0, lumaPlane, mbX, mbY, codedW, codedH, mv[0].x, mv[0].y, lumaStride, ctx.rounding);
				MPEG4Interpolator.interpolate8x8QP(mb.pred[0], 8, lumaPlane, mbX + 8, mbY, codedW, codedH, mv[1].x, mv[1].y, lumaStride, ctx.rounding);
				MPEG4Interpolator.interpolate8x8QP(mb.pred[0], 128, lumaPlane, mbX, mbY + 8, codedW, codedH, mv[2].x, mv[2].y, lumaStride, ctx.rounding);
				MPEG4Interpolator.interpolate8x8QP(mb.pred[0], 136, lumaPlane, mbX + 8, mbY + 8, codedW, codedH, mv[3].x, mv[3].y, lumaStride, ctx.rounding);
			}
			else
			{
				MPEG4Interpolator.interpolate8x8Planar(mb.pred[0], 0, 16, lumaPlane, mbX, mbY, codedW, codedH, mv[0].x, mv[0].y, lumaStride, ctx.rounding);
				MPEG4Interpolator.interpolate8x8Planar(mb.pred[0], 8, 16, lumaPlane, mbX + 8, mbY, codedW, codedH, mv[1].x, mv[1].y, lumaStride, ctx.rounding);
				MPEG4Interpolator.interpolate8x8Planar(mb.pred[0], 128, 16, lumaPlane, mbX, mbY + 8, codedW, codedH, mv[2].x, mv[2].y, lumaStride, ctx.rounding);
				MPEG4Interpolator.interpolate8x8Planar(mb.pred[0], 136, 16, lumaPlane, mbX + 8, mbY + 8, codedW, codedH, mv[3].x, mv[3].y, lumaStride, ctx.rounding);
			}
		}
		MPEG4Interpolator.interpolate8x8Planar(mb.pred[1], 0, 8, refs[@ref].getPlaneData(1), 8 * mb.x, 8 * mb.y, codedWcr, codedHcr, uv_dx, uv_dy, refs[@ref].getPlaneWidth(1), ctx.rounding);
		MPEG4Interpolator.interpolate8x8Planar(mb.pred[2], 0, 8, refs[@ref].getPlaneData(2), 8 * mb.x, 8 * mb.y, codedWcr, codedHcr, uv_dx, uv_dy, refs[@ref].getPlaneWidth(2), ctx.rounding);
		if (mb.cbp == 0)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			short[] block = mb.block[i];
			if ((mb.cbp & (1 << 5 - i)) != 0)
			{
				MPEG4DCT.idctAdd(mb.pred, block, i, (ctx.interlacing && mb.fieldDCT) ? true : false);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public MPEG4Renderer()
	{
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 94, 130, 138, 102, 100, 101, 133 })]
	public static int sanitize(int value, bool quarterPel, int fcode)
	{
		int length = 1 << fcode + 4;
		if (value < -length)
		{
			return -length;
		}
		if (value >= length)
		{
			return length - 1;
		}
		return value;
	}
}
