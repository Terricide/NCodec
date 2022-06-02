using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4BiRenderer : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		129,
		97,
		67,
		136,
		121,
		153,
		115,
		147,
		135,
		100,
		103,
		106,
		114,
		byte.MaxValue,
		3,
		61,
		231,
		71
	})]
	private static void renderBiDir(MPEG4DecodingContext ctx, Picture[] refs, Macroblock mb, bool direct)
	{
		int cbp = mb.cbp;
		MPEG4Renderer.validateVector(mb.mvs, ctx, mb.x, mb.y);
		MPEG4Renderer.validateVector(mb.bmvs, ctx, mb.x, mb.y);
		renderOneDir(ctx, mb, direct, refs[1], mb.mvs, 0);
		renderOneDir(ctx, mb, direct, refs[0], mb.bmvs, 3);
		mergePred(mb);
		if (cbp == 0)
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
	[LineNumberTable(new byte[]
	{
		159, 121, 129, 67, 107, 107, 106, 107, 107, 139,
		108, 100, 127, 10, 39, 171, 127, 11, 39, 134,
		127, 13, 39, 134, 127, 17, 39, 134, 127, 19,
		39, 203, 127, 13, 39, 134, 127, 15, 39, 134,
		127, 19, 39, 134, 127, 21, 39, 230, 69, 100,
		114, 148, 108, 140, 127, 16, 39, 134, 127, 16,
		39, 136
	})]
	private static void renderOneDir(MPEG4DecodingContext ctx, Macroblock mb, bool direct, Picture forward, Macroblock.Vector[] mvs, int pred)
	{
		int mbX = 16 * mb.x;
		int mbY = 16 * mb.y;
		int codedW = ctx.mbWidth << 4;
		int codedH = ctx.mbHeight << 4;
		int codedWcr = ctx.mbWidth << 3;
		int codedHcr = ctx.mbHeight << 3;
		if (ctx.quarterPel)
		{
			if (!direct)
			{
				MPEG4Interpolator.interpolate16x16QP(mb.pred[pred], forward.getPlaneData(0), mbX, mbY, codedW, codedH, mvs[0].x, mvs[0].y, forward.getWidth(), rounding: false);
			}
			else
			{
				MPEG4Interpolator.interpolate8x8QP(mb.pred[pred], 0, forward.getPlaneData(0), mbX, mbY, codedW, codedH, mvs[0].x, mvs[0].y, forward.getWidth(), rounding: false);
				MPEG4Interpolator.interpolate8x8QP(mb.pred[pred], 8, forward.getPlaneData(0), mbX + 8, mbY, codedW, codedH, mvs[1].x, mvs[1].y, forward.getWidth(), rounding: false);
				MPEG4Interpolator.interpolate8x8QP(mb.pred[pred], 128, forward.getPlaneData(0), mbX, mbY + 8, codedW, codedH, mvs[2].x, mvs[2].y, forward.getWidth(), rounding: false);
				MPEG4Interpolator.interpolate8x8QP(mb.pred[pred], 136, forward.getPlaneData(0), mbX + 8, mbY + 8, codedW, codedH, mvs[3].x, mvs[3].y, forward.getWidth(), rounding: false);
			}
		}
		else
		{
			MPEG4Interpolator.interpolate8x8Planar(mb.pred[pred], 0, 16, forward.getPlaneData(0), mbX, mbY, codedW, codedH, mvs[0].x, mvs[0].y, forward.getWidth(), rounding: false);
			MPEG4Interpolator.interpolate8x8Planar(mb.pred[pred], 8, 16, forward.getPlaneData(0), mbX + 8, mbY, codedW, codedH, mvs[1].x, mvs[1].y, forward.getWidth(), rounding: false);
			MPEG4Interpolator.interpolate8x8Planar(mb.pred[pred], 128, 16, forward.getPlaneData(0), mbX, mbY + 8, codedW, codedH, mvs[2].x, mvs[2].y, forward.getWidth(), rounding: false);
			MPEG4Interpolator.interpolate8x8Planar(mb.pred[pred], 136, 16, forward.getPlaneData(0), mbX + 8, mbY + 8, codedW, codedH, mvs[3].x, mvs[3].y, forward.getWidth(), rounding: false);
		}
		int mx_chr;
		int my_chr;
		if (!direct)
		{
			mx_chr = MPEG4Renderer.calcChromaMv(ctx, mvs[0].x);
			my_chr = MPEG4Renderer.calcChromaMv(ctx, mvs[0].y);
		}
		else
		{
			mx_chr = MPEG4Renderer.calcChromaMvAvg(ctx, mvs, x: true);
			my_chr = MPEG4Renderer.calcChromaMvAvg(ctx, mvs, x: false);
		}
		MPEG4Interpolator.interpolate8x8Planar(mb.pred[pred + 1], 0, 8, forward.getPlaneData(1), 8 * mb.x, 8 * mb.y, codedWcr, codedHcr, mx_chr, my_chr, forward.getPlaneWidth(1), rounding: false);
		MPEG4Interpolator.interpolate8x8Planar(mb.pred[pred + 2], 0, 8, forward.getPlaneData(2), 8 * mb.x, 8 * mb.y, codedWcr, codedHcr, mx_chr, my_chr, forward.getPlaneWidth(2), rounding: false);
	}

	[LineNumberTable(new byte[]
	{
		159, 124, 130, 107, 63, 6, 167, 103, 104, 63,
		8, 39, 231, 69
	})]
	private static void mergePred(Macroblock mb)
	{
		for (int j = 0; j < 256; j++)
		{
			mb.pred[0][j] = (byte)(sbyte)(mb.pred[0][j] + mb.pred[3][j] + 1 >> 1);
		}
		for (int pl = 1; pl < 3; pl++)
		{
			for (int i = 0; i < 64; i++)
			{
				mb.pred[pl][i] = (byte)(sbyte)(mb.pred[pl][i] + mb.pred[pl + 3][i] + 1 >> 1);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(27)]
	public MPEG4BiRenderer()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 191, 4, 107, 163, 107, 163, 109,
		163, 109, 163
	})]
	public static void renderBi(MPEG4DecodingContext ctx, Picture[] refs, int fcodeForward, int fcodeBackward, Macroblock mb)
	{
		switch (mb.mode)
		{
		case 0:
		case 4:
			renderBiDir(ctx, refs, mb, direct: true);
			break;
		case 1:
			renderBiDir(ctx, refs, mb, direct: false);
			break;
		case 2:
			MPEG4Renderer.renderInter(ctx, refs, mb, fcodeBackward, 0, bvop: true);
			break;
		case 3:
			MPEG4Renderer.renderInter(ctx, refs, mb, fcodeForward, 1, bvop: true);
			break;
		}
	}
}
