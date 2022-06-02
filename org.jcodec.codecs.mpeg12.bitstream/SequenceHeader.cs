using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class SequenceHeader : Object, MPEGHeader
{
	private static bool _hasExtensions;

	public int horizontal_size;

	public int vertical_size;

	public int aspect_ratio_information;

	public int frame_rate_code;

	public int bit_rate;

	public int vbv_buffer_size_value;

	public int constrained_parameters_flag;

	public int[] intra_quantiser_matrix;

	public int[] non_intra_quantiser_matrix;

	public SequenceExtension sequenceExtension;

	public SequenceScalableExtension sequenceScalableExtension;

	public SequenceDisplayExtension sequenceDisplayExtension;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 105 })]
	private SequenceHeader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 130, 105, 109, 173, 105, 109, 173, 105,
		109, 143
	})]
	private void writeExtensions(ByteBuffer @out)
	{
		if (sequenceExtension != null)
		{
			@out.putInt(181);
			sequenceExtension.write(@out);
		}
		if (sequenceScalableExtension != null)
		{
			@out.putInt(181);
			sequenceScalableExtension.write(@out);
		}
		if (sequenceDisplayExtension != null)
		{
			@out.putInt(181);
			sequenceDisplayExtension.write(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 103, 104, 104, 104, 104, 105, 105,
		105, 105, 105
	})]
	public static SequenceHeader createSequenceHeader(int horizontal_size, int vertical_size, int aspect_ratio_information, int frame_rate_code, int bit_rate, int vbv_buffer_size_value, int constrained_parameters_flag, int[] intra_quantiser_matrix, int[] non_intra_quantiser_matrix)
	{
		SequenceHeader sh = new SequenceHeader();
		sh.horizontal_size = horizontal_size;
		sh.vertical_size = vertical_size;
		sh.aspect_ratio_information = aspect_ratio_information;
		sh.frame_rate_code = frame_rate_code;
		sh.bit_rate = bit_rate;
		sh.vbv_buffer_size_value = vbv_buffer_size_value;
		sh.constrained_parameters_flag = constrained_parameters_flag;
		sh.intra_quantiser_matrix = intra_quantiser_matrix;
		sh.non_intra_quantiser_matrix = non_intra_quantiser_matrix;
		return sh;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 104, 103, 111, 111, 110, 110, 111,
		104, 111, 109, 105, 110, 104, 48, 199, 105, 110,
		104, 48, 231, 69
	})]
	public static SequenceHeader read(ByteBuffer bb)
	{
		BitReader _in = BitReader.createBitReader(bb);
		SequenceHeader sh = new SequenceHeader();
		sh.horizontal_size = _in.readNBit(12);
		sh.vertical_size = _in.readNBit(12);
		sh.aspect_ratio_information = _in.readNBit(4);
		sh.frame_rate_code = _in.readNBit(4);
		sh.bit_rate = _in.readNBit(18);
		_in.read1Bit();
		sh.vbv_buffer_size_value = _in.readNBit(10);
		sh.constrained_parameters_flag = _in.read1Bit();
		if (_in.read1Bit() != 0)
		{
			sh.intra_quantiser_matrix = new int[64];
			for (int j = 0; j < 64; j++)
			{
				sh.intra_quantiser_matrix[j] = _in.readNBit(8);
			}
		}
		if (_in.read1Bit() != 0)
		{
			sh.non_intra_quantiser_matrix = new int[64];
			for (int i = 0; i < 64; i++)
			{
				sh.non_intra_quantiser_matrix[i] = _in.readNBit(8);
			}
		}
		return sh;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 135, 104, 105, 159, 0, 109, 131,
		109, 131, 109, 131, 159, 7
	})]
	public static void readExtension(ByteBuffer bb, SequenceHeader sh)
	{
		_hasExtensions = true;
		BitReader _in = BitReader.createBitReader(bb);
		int extType = _in.readNBit(4);
		switch (extType)
		{
		case 1:
			sh.sequenceExtension = SequenceExtension.read(_in);
			break;
		case 5:
			sh.sequenceScalableExtension = SequenceScalableExtension.read(_in);
			break;
		case 2:
			sh.sequenceDisplayExtension = SequenceDisplayExtension.read(_in);
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
		159, 117, 130, 104, 111, 111, 110, 110, 111, 104,
		111, 109, 115, 105, 104, 48, 199, 115, 105, 104,
		48, 231, 69, 135, 106
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(horizontal_size, 12);
		bw.writeNBit(vertical_size, 12);
		bw.writeNBit(aspect_ratio_information, 4);
		bw.writeNBit(frame_rate_code, 4);
		bw.writeNBit(bit_rate, 18);
		bw.write1Bit(1);
		bw.writeNBit(vbv_buffer_size_value, 10);
		bw.write1Bit(constrained_parameters_flag);
		bw.write1Bit((intra_quantiser_matrix != null) ? 1 : 0);
		if (intra_quantiser_matrix != null)
		{
			for (int j = 0; j < 64; j++)
			{
				bw.writeNBit(intra_quantiser_matrix[j], 8);
			}
		}
		bw.write1Bit((non_intra_quantiser_matrix != null) ? 1 : 0);
		if (non_intra_quantiser_matrix != null)
		{
			for (int i = 0; i < 64; i++)
			{
				bw.writeNBit(non_intra_quantiser_matrix[i], 8);
			}
		}
		bw.flush();
		writeExtensions(bb);
	}

	[LineNumberTable(147)]
	public virtual bool hasExtensions()
	{
		return _hasExtensions;
	}

	[LineNumberTable(new byte[] { 159, 105, 162, 109, 109, 109 })]
	public virtual void copyExtensions(SequenceHeader sh)
	{
		sequenceExtension = sh.sequenceExtension;
		sequenceScalableExtension = sh.sequenceScalableExtension;
		sequenceDisplayExtension = sh.sequenceDisplayExtension;
	}
}
