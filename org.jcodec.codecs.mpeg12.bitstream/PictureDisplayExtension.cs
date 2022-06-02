using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class PictureDisplayExtension : Object, MPEGHeader
{
	public Point[] frame_centre_offsets;

	public const int Picture_Display_Extension = 7;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public PictureDisplayExtension()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 103, 145, 106, 106, 106, 131, 131,
		163, 106, 131, 106, 131
	})]
	private static int numberOfFrameCentreOffsets(SequenceExtension se, PictureCodingExtension pce)
	{
		if (se == null || pce == null)
		{
			
			throw new IllegalArgumentException("PictureDisplayExtension requires SequenceExtension and PictureCodingExtension to be present");
		}
		if (se.progressive_sequence == 1)
		{
			if (pce.repeat_first_field == 1)
			{
				if (pce.top_field_first == 1)
				{
					return 3;
				}
				return 2;
			}
			return 1;
		}
		if (pce.picture_structure != 3)
		{
			return 1;
		}
		if (pce.repeat_first_field == 1)
		{
			return 3;
		}
		return 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 103, 115, 109, 106, 104, 106, 104,
		240, 59, 231, 71
	})]
	public static PictureDisplayExtension read(BitReader bits, SequenceExtension se, PictureCodingExtension pce)
	{
		PictureDisplayExtension pde = new PictureDisplayExtension();
		pde.frame_centre_offsets = new Point[numberOfFrameCentreOffsets(se, pce)];
		for (int i = 0; i < (nint)pde.frame_centre_offsets.LongLength; i++)
		{
			int frame_centre_horizontal_offset = bits.readNBit(16);
			bits.read1Bit();
			int frame_centre_vertical_offset = bits.readNBit(16);
			bits.read1Bit();
			pde.frame_centre_offsets[i] = new Point(frame_centre_horizontal_offset, frame_centre_vertical_offset);
		}
		return pde;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 66, 104, 137, 109, 106, 111, 239, 61,
		231, 69, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(7, 4);
		for (int i = 0; i < (nint)frame_centre_offsets.LongLength; i++)
		{
			Point point = frame_centre_offsets[i];
			bw.writeNBit(point.getX(), 16);
			bw.writeNBit(point.getY(), 16);
		}
		bw.flush();
	}
}
