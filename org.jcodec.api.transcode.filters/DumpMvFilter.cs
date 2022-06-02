using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode.filters;

public class DumpMvFilter : Object, Filter
{
	private bool js;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		134,
		130,
		112,
		110,
		98,
		104,
		138,
		159,
		20,
		111,
		103,
		104,
		104,
		104,
		109,
		110,
		110,
		110,
		113,
		109,
		127,
		17,
		127,
		17,
		byte.MaxValue,
		17,
		60,
		236,
		70,
		113,
		114,
		114,
		242,
		46,
		234,
		84,
		110,
		227,
		39,
		234,
		91
	})]
	private void dumpMvTxt(org.jcodec.codecs.h264.io.model.Frame dec)
	{
		java.lang.System.err.println("FRAME ================================================================");
		if (dec.getFrameType() == SliceType.___003C_003EI)
		{
			return;
		}
		H264Utils.MvList2D mvs = dec.getMvs();
		for (int i = 0; i < 2; i++)
		{
			java.lang.System.err.println(new StringBuilder().append((i != 0) ? "FWD" : "BCK").append(" ===========================================================================").toString());
			for (int blkY = 0; blkY < mvs.getHeight(); blkY++)
			{
				StringBuilder line0 = new StringBuilder();
				StringBuilder line1 = new StringBuilder();
				StringBuilder line2 = new StringBuilder();
				StringBuilder line3 = new StringBuilder();
				line0.append("+");
				line1.append("|");
				line2.append("|");
				line3.append("|");
				for (int blkX = 0; blkX < mvs.getWidth(); blkX++)
				{
					line0.append("------+");
					line1.append(String.format("%6d|", Integer.valueOf(H264Utils.Mv.mvX(mvs.getMv(blkX, blkY, i)))));
					line2.append(String.format("%6d|", Integer.valueOf(H264Utils.Mv.mvY(mvs.getMv(blkX, blkY, i)))));
					line3.append(String.format("    %2d|", Integer.valueOf(H264Utils.Mv.mvRef(mvs.getMv(blkX, blkY, i)))));
				}
				java.lang.System.err.println(line0.toString());
				java.lang.System.err.println(line1.toString());
				java.lang.System.err.println(line2.toString());
				java.lang.System.err.println(line3.toString());
			}
			if (dec.getFrameType() != SliceType.___003C_003EB)
			{
				break;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 112, 110, 98, 104, 138, 127, 20,
		111, 111, 127, 55, 63, 29, 38, 42, 234, 71,
		112, 110, 227, 52, 234, 78, 114
	})]
	private void dumpMvJs(org.jcodec.codecs.h264.io.model.Frame dec)
	{
		java.lang.System.err.println("{");
		if (dec.getFrameType() == SliceType.___003C_003EI)
		{
			return;
		}
		H264Utils.MvList2D mvs = dec.getMvs();
		for (int i = 0; i < 2; i++)
		{
			java.lang.System.err.println(new StringBuilder().append((i != 0) ? "forwardRef" : "backRef").append(": [").toString());
			for (int blkY = 0; blkY < mvs.getHeight(); blkY++)
			{
				for (int blkX = 0; blkX < mvs.getWidth(); blkX++)
				{
					java.lang.System.err.println(new StringBuilder().append("{x: ").append(blkX).append(", y: ")
						.append(blkY)
						.append(", mx: ")
						.append(H264Utils.Mv.mvX(mvs.getMv(blkX, blkY, i)))
						.append(", my: ")
						.append(H264Utils.Mv.mvY(mvs.getMv(blkX, blkY, i)))
						.append(", ridx:")
						.append(H264Utils.Mv.mvRef(mvs.getMv(blkX, blkY, i)))
						.append("},")
						.toString());
				}
			}
			java.lang.System.err.println("],");
			if (dec.getFrameType() != SliceType.___003C_003EB)
			{
				break;
			}
		}
		java.lang.System.err.println("}");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 161, 67, 105, 104 })]
	public DumpMvFilter(bool js)
	{
		this.js = js;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 104, 105, 138, 104 })]
	public virtual PixelStore.LoanerPicture filter(Picture picture, PixelStore pixelStore)
	{
		org.jcodec.codecs.h264.io.model.Frame dec = (org.jcodec.codecs.h264.io.model.Frame)picture;
		if (!js)
		{
			dumpMvTxt(dec);
		}
		else
		{
			dumpMvJs(dec);
		}
		return null;
	}

	[LineNumberTable(92)]
	public virtual ColorSpace getInputColor()
	{
		return null;
	}

	[LineNumberTable(98)]
	public virtual ColorSpace getOutputColor()
	{
		return null;
	}
}
