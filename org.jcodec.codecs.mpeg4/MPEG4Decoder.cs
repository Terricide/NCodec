using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4Decoder : VideoDecoder
{
	private Picture[] refs;

	private Macroblock[] prevMBs;

	private Macroblock[] mbs;

	private MPEG4DecodingContext ctx;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 105, 109 })]
	public MPEG4Decoder()
	{
		refs = new Picture[2];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 98, 159, 24, 131, 111, 111, 116, 139,
		106, 109, 100, 100, 100, 108, 103, 110, 102, 108,
		102, 108, 106, 108, 144, 105, 105, 137, 137, 235,
		38, 42, 234, 95
	})]
	private Picture decodeIFrame(BitReader br, MPEG4DecodingContext ctx, byte[][] buffer)
	{
		Picture p = new Picture(ctx.mbWidth << 4, ctx.mbHeight << 4, buffer, null, ColorSpace.___003C_003EYUV420, 0, new Rect(0, 0, ctx.width, ctx.height));
		int bound = 0;
		for (int y = 0; y < ctx.mbHeight; y++)
		{
			int x;
			for (x = 0; x < ctx.mbWidth; x++)
			{
				Macroblock mb = mbs[y * ctx.mbWidth + x];
				mb.reset(x, y, bound);
				MPEG4Bitstream.readIntraMode(br, ctx, mb);
				int index = x + y * ctx.mbWidth;
				Macroblock aboveMb = null;
				Macroblock aboveLeftMb = null;
				Macroblock leftMb = null;
				int apos = index - ctx.mbWidth;
				int lpos = index - 1;
				int alpos = index - 1 - ctx.mbWidth;
				if (apos >= bound)
				{
					aboveMb = mbs[apos];
				}
				if (alpos >= bound)
				{
					aboveLeftMb = mbs[alpos];
				}
				if (x > 0 && lpos >= bound)
				{
					leftMb = mbs[lpos];
				}
				MPEG4Bitstream.readCoeffIntra(br, ctx, mb, aboveMb, leftMb, aboveLeftMb);
				x = mb.x;
				y = mb.y;
				bound = mb.bound;
				MPEG4Renderer.renderIntra(mb, ctx);
				putPix(p, mb, x, y);
			}
		}
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 66, 99, 104, 136, 191, 24, 108, 140,
		108, 140, 109, 112, 111, 174, 111, 100, 100, 100,
		100, 108, 103, 110, 110, 102, 108, 102, 108, 107,
		108, 114, 140, 150, 141, 148, 147, 237, 27, 44,
		236, 106
	})]
	internal virtual Picture decodePFrame(BitReader br, MPEG4DecodingContext ctx, byte[][] buffers, int fcode)
	{
		int bound = 0;
		int mbWidth = ctx.mbWidth;
		int mbHeight = ctx.mbHeight;
		Picture p = new Picture(ctx.mbWidth << 4, ctx.mbHeight << 4, buffers, null, ColorSpace.___003C_003EYUV420, 0, new Rect(0, 0, ctx.width, ctx.height));
		for (int y = 0; y < mbHeight; y++)
		{
			for (int x = 0; x < mbWidth; x++)
			{
				while (br.checkNBit(10) == 1)
				{
					br.skip(10);
				}
				if (MPEG4Bitstream.checkResyncMarker(br, fcode - 1))
				{
					bound = MPEG4Bitstream.readVideoPacketHeader(br, ctx, fcode - 1, fcodeForwardEnabled: true, fcodeBackwardEnabled: false, intraDCThresholdEnabled: true);
					int num = bound;
					x = ((mbWidth != -1) ? (num % mbWidth) : 0);
					int num2 = bound;
					y = ((mbWidth != -1) ? (num2 / mbWidth) : (-num2));
				}
				int index = x + y * ctx.mbWidth;
				Macroblock aboveMb = null;
				Macroblock aboveLeftMb = null;
				Macroblock leftMb = null;
				Macroblock aboveRightMb = null;
				int apos = index - ctx.mbWidth;
				int lpos = index - 1;
				int alpos = index - 1 - ctx.mbWidth;
				int arpos = index + 1 - ctx.mbWidth;
				if (apos >= bound)
				{
					aboveMb = mbs[apos];
				}
				if (alpos >= bound)
				{
					aboveLeftMb = mbs[alpos];
				}
				if (x > 0 && lpos >= bound)
				{
					leftMb = mbs[lpos];
				}
				if (arpos >= bound && x < ctx.mbWidth - 1)
				{
					aboveRightMb = mbs[arpos];
				}
				Macroblock mb = mbs[y * ctx.mbWidth + x];
				mb.reset(x, y, bound);
				MPEG4Bitstream.readInterModeCoeffs(br, ctx, fcode, mb, aboveMb, leftMb, aboveLeftMb, aboveRightMb);
				MPEG4Renderer.renderInter(ctx, refs, mb, fcode, 0, bvop: false);
				putPix(p, mb, x, y);
			}
		}
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 162, 191, 24, 141, 110, 113, 159, 25,
		113, 118, 150, 111, 159, 10, 117, 116, 159, 25,
		106, 138, 138, 108, 105, 105, 106, 115, 109, 163,
		114, 147, 237, 36, 236, 61, 236, 99
	})]
	private Picture decodeBFrame(BitReader br, MPEG4DecodingContext ctx, byte[][] buffers, int quant, int fcodeForward, int fcodeBackward)
	{
		Picture p = new Picture(ctx.mbWidth << 4, ctx.mbHeight << 4, buffers, null, ColorSpace.___003C_003EYUV420, 0, new Rect(0, 0, ctx.width, ctx.height));
		Macroblock.Vector pFMV = Macroblock.vec();
		Macroblock.Vector pBMV = Macroblock.vec();
		int fcodeMax = ((fcodeForward <= fcodeBackward) ? fcodeBackward : fcodeForward);
		for (int y = 0; y < ctx.mbHeight; y++)
		{
			int num = 0;
			Macroblock.Vector vector = pFMV;
			int num2 = num;
			vector.y = num;
			num = num2;
			vector = pFMV;
			int num3 = num;
			vector.x = num;
			num = num3;
			vector = pBMV;
			int x2 = num;
			vector.y = num;
			pBMV.x = x2;
			for (int x = 0; x < ctx.mbWidth; x++)
			{
				Macroblock mb = mbs[y * ctx.mbWidth + x];
				Macroblock lastMB = prevMBs[y * ctx.mbWidth + x];
				if (MPEG4Bitstream.checkResyncMarker(br, fcodeMax - 1))
				{
					int bound = MPEG4Bitstream.readVideoPacketHeader(br, ctx, fcodeMax - 1, (fcodeForward != 0) ? true : false, (fcodeBackward != 0) ? true : false, (ctx.intraDCThreshold != 0) ? true : false);
					int mbWidth = ctx.mbWidth;
					x = ((mbWidth != -1) ? (bound % mbWidth) : 0);
					int mbWidth2 = ctx.mbWidth;
					y = ((mbWidth2 != -1) ? (bound / mbWidth2) : (-bound));
					num = 0;
					vector = pFMV;
					int num4 = num;
					vector.y = num;
					num = num4;
					vector = pFMV;
					int num5 = num;
					vector.x = num;
					num = num5;
					vector = pBMV;
					int x3 = num;
					vector.y = num;
					pBMV.x = x3;
				}
				mb.x = x;
				mb.y = y;
				mb.quant = quant;
				if (lastMB.mode == 16)
				{
					mb.cbp = 0;
					mb.mode = 3;
					MPEG4Bitstream.readInterCoeffs(br, ctx, mb);
					MPEG4Renderer.renderInter(ctx, refs, lastMB, fcodeForward, 1, bvop: true);
					putPix(p, lastMB, x, y);
				}
				else
				{
					MPEG4Bitstream.readBi(br, ctx, fcodeForward, fcodeBackward, mb, lastMB, pFMV, pBMV);
					MPEG4BiRenderer.renderBi(ctx, refs, fcodeForward, fcodeBackward, mb);
					putPix(p, mb, x, y);
				}
			}
		}
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 66, 105, 112, 106, 106, 49, 45, 240,
		69, 108, 107, 115, 108, 105, 53, 47, 246, 61,
		236, 73
	})]
	public static void putPix(Picture p, Macroblock mb, int x, int y)
	{
		byte[] plane2 = p.getPlaneData(0);
		int dsto2 = (y << 4) * p.getWidth() + (x << 4);
		int row2 = 0;
		int srco2 = 0;
		while (row2 < 16)
		{
			int col2 = 0;
			while (col2 < 16)
			{
				plane2[dsto2 + col2] = mb.pred[0][srco2];
				col2++;
				srco2++;
			}
			row2++;
			dsto2 += p.getWidth();
		}
		for (int pl = 1; pl < 3; pl++)
		{
			byte[] plane = p.getPlaneData(pl);
			int dsto = (y << 3) * p.getPlaneWidth(pl) + (x << 3);
			int row = 0;
			int srco = 0;
			while (row < 8)
			{
				int col = 0;
				while (col < 8)
				{
					plane[dsto + col] = mb.pred[pl][srco];
					col++;
					srco++;
				}
				row++;
				dsto += p.getPlaneWidth(pl);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 105, 108, 111, 131, 159, 14, 136,
		111, 131, 127, 4, 109, 46, 199, 100, 114, 159,
		8, 113, 131, 124, 131, 145, 163, 113, 139, 143,
		191, 19, 135
	})]
	public override Picture decodeFrame(ByteBuffer data, byte[][] buffer)
	{
		if (ctx == null)
		{
			ctx = new MPEG4DecodingContext();
		}
		if (!ctx.readHeaders(data))
		{
			return null;
		}
		MPEG4DecodingContext mPEG4DecodingContext = ctx;
		MPEG4DecodingContext mPEG4DecodingContext2 = ctx;
		MPEG4DecodingContext mPEG4DecodingContext3 = ctx;
		int num = 0;
		MPEG4DecodingContext mPEG4DecodingContext4 = mPEG4DecodingContext3;
		int num2 = num;
		mPEG4DecodingContext4.intraDCThreshold = num;
		num = num2;
		mPEG4DecodingContext4 = mPEG4DecodingContext2;
		int fcodeForward = num;
		mPEG4DecodingContext4.fcodeBackward = num;
		mPEG4DecodingContext.fcodeForward = fcodeForward;
		BitReader br = BitReader.createBitReader(data);
		if (!ctx.readVOPHeader(br))
		{
			return null;
		}
		mbs = new Macroblock[ctx.mbWidth * ctx.mbHeight];
		for (int i = 0; i < (nint)mbs.LongLength; i++)
		{
			mbs[i] = new Macroblock();
		}
		Picture decoded = null;
		if (ctx.codingType != 2)
		{
			switch (ctx.codingType)
			{
			case 0:
				decoded = decodeIFrame(br, ctx, buffer);
				break;
			case 1:
				decoded = decodePFrame(br, ctx, buffer, ctx.fcodeForward);
				break;
			case 3:
				
				throw new RuntimeException("GMC not supported.");
			case 4:
				return null;
			}
			refs[1] = refs[0];
			refs[0] = decoded;
			prevMBs = mbs;
		}
		else
		{
			decoded = decodeBFrame(br, ctx, buffer, ctx.quant, ctx.fcodeForward, ctx.fcodeBackward);
		}
		br.terminate();
		return decoded;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 66, 109, 100, 131 })]
	public override VideoCodecMeta getCodecMeta(ByteBuffer data)
	{
		MPEG4DecodingContext ctx = MPEG4DecodingContext.readFromHeaders(data.duplicate());
		if (ctx == null)
		{
			return null;
		}
		VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(ctx.width, ctx.height), ColorSpace.___003C_003EYUV420J);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 77, 98, 109, 100, 99 })]
	public static int probe(ByteBuffer data)
	{
		MPEG4DecodingContext ctx = MPEG4DecodingContext.readFromHeaders(data.duplicate());
		if (ctx == null)
		{
			return 0;
		}
		int result = Math.min((ctx.width <= 320) ? 50 : ((ctx.width >= 1280) ? 50 : 100), (ctx.height <= 240) ? 50 : ((ctx.height >= 720) ? 50 : 100));
		
		return result;
	}
}
