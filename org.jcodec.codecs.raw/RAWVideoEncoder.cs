using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.codecs.raw;

public class RAWVideoEncoder : VideoEncoder
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(18)]
	public RAWVideoEncoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 136, 104, 108, 111, 116, 105, 105,
		117, 117, 138, 106, 106, 106, 55, 169, 231, 60,
		233, 55, 239, 81, 109, 108, 105, 105, 105, 105,
		138, 110, 109, 109, 106, 58, 41, 208, 232, 59,
		236, 72, 104
	})]
	public override EncodedFrame encodeFrame(Picture pic, ByteBuffer _out)
	{
		ByteBuffer dup = _out.duplicate();
		ColorSpace color = pic.getColor();
		if (color.planar)
		{
			for (int plane = 0; plane < color.nComp; plane++)
			{
				int width = pic.getWidth() >> color.compWidth[plane];
				int startX2 = pic.getStartX();
				int startY2 = pic.getStartY();
				int cropW2 = pic.getCroppedWidth() >> color.compWidth[plane];
				int cropH2 = pic.getCroppedHeight() >> color.compHeight[plane];
				byte[] planeData2 = pic.getPlaneData(plane);
				int pos2 = width * startY2 + startX2;
				for (int y2 = 0; y2 < cropH2; y2++)
				{
					for (int x2 = 0; x2 < cropW2; x2++)
					{
						dup.put((byte)(sbyte)(planeData2[pos2 + x2] + 128));
					}
					pos2 += width;
				}
			}
		}
		else
		{
			int bytesPerPixel = color.bitsPerPixel + 7 >> 3;
			int stride = pic.getWidth() * bytesPerPixel;
			int startX = pic.getStartX();
			int startY = pic.getStartY();
			int cropW = pic.getCroppedWidth();
			int cropH = pic.getCroppedHeight();
			byte[] planeData = pic.getPlaneData(0);
			int pos = stride * startY + startX * bytesPerPixel;
			for (int y = 0; y < cropH; y++)
			{
				int x = 0;
				int off = 0;
				while (x < cropW)
				{
					for (int b = 0; b < bytesPerPixel; b++)
					{
						dup.put((byte)(sbyte)(planeData[pos + off + b] + 128));
					}
					x++;
					off += bytesPerPixel;
				}
				pos += stride;
			}
		}
		dup.flip();
		EncodedFrame result = new EncodedFrame(dup, keyFrame: true);
		
		return result;
	}

	[LineNumberTable(66)]
	public override ColorSpace[] getSupportedColorSpaces()
	{
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 162, 111, 104, 99, 108, 61, 167 })]
	public override int estimateBufferSize(Picture frame)
	{
		int fullPlaneSize = frame.getWidth() * frame.getCroppedHeight();
		ColorSpace color = frame.getColor();
		int totalSize = 0;
		for (int i = 0; i < color.nComp; i++)
		{
			totalSize += fullPlaneSize >> color.compWidth[i] >> color.compHeight[i];
		}
		return totalSize;
	}
}
