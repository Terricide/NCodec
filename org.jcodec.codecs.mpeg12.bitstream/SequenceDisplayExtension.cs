using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class SequenceDisplayExtension : Object, MPEGHeader
{
	public class ColorDescription : Object
	{
		internal int colour_primaries;

		internal int transfer_characteristics;

		internal int matrix_coefficients;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(21)]
		public ColorDescription()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 162, 103, 110, 110, 110 })]
		public static ColorDescription read(BitReader _in)
		{
			ColorDescription cd = new ColorDescription();
			cd.colour_primaries = _in.readNBit(8);
			cd.transfer_characteristics = _in.readNBit(8);
			cd.matrix_coefficients = _in.readNBit(8);
			return cd;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 134, 162, 110, 110, 112 })]
		public virtual void write(BitWriter @out)
		{
			@out.writeNBit(colour_primaries, 8);
			@out.writeNBit(transfer_characteristics, 8);
			@out.writeNBit(matrix_coefficients, 8);
		}
	}

	public int video_format;

	public int display_horizontal_size;

	public int display_vertical_size;

	public ColorDescription colorDescription;

	public const int Sequence_Display_Extension = 2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public SequenceDisplayExtension()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 130, 103, 110, 106, 141, 111, 104, 143 })]
	public static SequenceDisplayExtension read(BitReader _in)
	{
		SequenceDisplayExtension sde = new SequenceDisplayExtension();
		sde.video_format = _in.readNBit(3);
		if (_in.read1Bit() == 1)
		{
			sde.colorDescription = ColorDescription.read(_in);
		}
		sde.display_horizontal_size = _in.readNBit(14);
		_in.read1Bit();
		sde.display_vertical_size = _in.readNBit(14);
		return sde;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 104, 137, 110, 115, 105, 109, 111,
		104, 111, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(2, 4);
		bw.writeNBit(video_format, 3);
		bw.write1Bit((colorDescription != null) ? 1 : 0);
		if (colorDescription != null)
		{
			colorDescription.write(bw);
		}
		bw.writeNBit(display_horizontal_size, 14);
		bw.write1Bit(1);
		bw.writeNBit(display_vertical_size, 14);
		bw.flush();
	}
}
