using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class PictureHeader : Object, MPEGHeader
{
	public int temporal_reference;

	public int picture_coding_type;

	public int vbv_delay;

	public int full_pel_forward_vector;

	public int forward_f_code;

	public int full_pel_backward_vector;

	public int backward_f_code;

	public QuantMatrixExtension quantMatrixExtension;

	public CopyrightExtension copyrightExtension;

	public PictureDisplayExtension pictureDisplayExtension;

	public PictureCodingExtension pictureCodingExtension;

	public PictureSpatialScalableExtension pictureSpatialScalableExtension;

	public PictureTemporalScalableExtension pictureTemporalScalableExtension;

	private bool _hasExtensions;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 98, 105 })]
	private PictureHeader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 162, 105, 109, 173, 105, 109, 173, 105,
		109, 173, 105, 109, 173, 105, 109, 173, 105, 109,
		143
	})]
	private void writeExtensions(ByteBuffer @out)
	{
		if (quantMatrixExtension != null)
		{
			@out.putInt(181);
			quantMatrixExtension.write(@out);
		}
		if (copyrightExtension != null)
		{
			@out.putInt(181);
			copyrightExtension.write(@out);
		}
		if (pictureCodingExtension != null)
		{
			@out.putInt(181);
			pictureCodingExtension.write(@out);
		}
		if (pictureDisplayExtension != null)
		{
			@out.putInt(181);
			pictureDisplayExtension.write(@out);
		}
		if (pictureSpatialScalableExtension != null)
		{
			@out.putInt(181);
			pictureSpatialScalableExtension.write(@out);
		}
		if (pictureTemporalScalableExtension != null)
		{
			@out.putInt(181);
			pictureTemporalScalableExtension.write(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 103, 104, 104, 104, 104, 105, 105,
		105
	})]
	public static PictureHeader createPictureHeader(int temporal_reference, int picture_coding_type, int vbv_delay, int full_pel_forward_vector, int forward_f_code, int full_pel_backward_vector, int backward_f_code)
	{
		PictureHeader p = new PictureHeader();
		p.temporal_reference = temporal_reference;
		p.picture_coding_type = picture_coding_type;
		p.vbv_delay = vbv_delay;
		p.full_pel_forward_vector = full_pel_forward_vector;
		p.forward_f_code = forward_f_code;
		p.full_pel_backward_vector = full_pel_backward_vector;
		p.backward_f_code = backward_f_code;
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 98, 104, 103, 111, 110, 111, 115, 109,
		142, 106, 109, 142, 106, 171
	})]
	public static PictureHeader read(ByteBuffer bb)
	{
		BitReader _in = BitReader.createBitReader(bb);
		PictureHeader ph = new PictureHeader();
		ph.temporal_reference = _in.readNBit(10);
		ph.picture_coding_type = _in.readNBit(3);
		ph.vbv_delay = _in.readNBit(16);
		if (ph.picture_coding_type == 2 || ph.picture_coding_type == 3)
		{
			ph.full_pel_forward_vector = _in.read1Bit();
			ph.forward_f_code = _in.readNBit(3);
		}
		if (ph.picture_coding_type == 3)
		{
			ph.full_pel_backward_vector = _in.read1Bit();
			ph.backward_f_code = _in.readNBit(3);
		}
		while (_in.read1Bit() == 1)
		{
			_in.readNBit(8);
		}
		return ph;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 104, 104, 105, 159, 39, 109, 134,
		109, 134, 153, 131, 109, 131, 109, 131, 109, 131,
		159, 7
	})]
	public static void readExtension(ByteBuffer bb, PictureHeader ph, SequenceHeader sh)
	{
		ph._hasExtensions = true;
		BitReader _in = BitReader.createBitReader(bb);
		int extType = _in.readNBit(4);
		switch (extType)
		{
		case 3:
			ph.quantMatrixExtension = QuantMatrixExtension.read(_in);
			break;
		case 4:
			ph.copyrightExtension = CopyrightExtension.read(_in);
			break;
		case 7:
			ph.pictureDisplayExtension = PictureDisplayExtension.read(_in, sh.sequenceExtension, ph.pictureCodingExtension);
			break;
		case 8:
			ph.pictureCodingExtension = PictureCodingExtension.read(_in);
			break;
		case 9:
			ph.pictureSpatialScalableExtension = PictureSpatialScalableExtension.read(_in);
			break;
		case 16:
			ph.pictureTemporalScalableExtension = PictureTemporalScalableExtension.read(_in);
			break;
		default:
		{
			string message = new StringBuilder().append("Unsupported extension: ").append(extType).toString();
			
			throw new RuntimeException(message);
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 66, 104, 111, 110, 111, 115, 109, 141,
		106, 109, 142, 104, 135, 106
	})]
	public virtual void write(ByteBuffer os)
	{
		BitWriter @out = new BitWriter(os);
		@out.writeNBit(temporal_reference, 10);
		@out.writeNBit(picture_coding_type, 3);
		@out.writeNBit(vbv_delay, 16);
		if (picture_coding_type == 2 || picture_coding_type == 3)
		{
			@out.write1Bit(full_pel_forward_vector);
			@out.write1Bit(forward_f_code);
		}
		if (picture_coding_type == 3)
		{
			@out.write1Bit(full_pel_backward_vector);
			@out.writeNBit(backward_f_code, 3);
		}
		@out.write1Bit(0);
		@out.flush();
		writeExtensions(os);
	}

	[LineNumberTable(159)]
	public virtual bool hasExtensions()
	{
		return _hasExtensions;
	}
}
