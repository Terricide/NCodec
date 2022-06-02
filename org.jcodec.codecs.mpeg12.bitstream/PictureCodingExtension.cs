using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class PictureCodingExtension : Object, MPEGHeader
{
	public class CompositeDisplay : Object
	{
		public int v_axis;

		public int field_sequence;

		public int sub_carrier;

		public int burst_amplitude;

		public int sub_carrier_phase;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(39)]
		public CompositeDisplay()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 131, 162, 103, 109, 110, 109, 110, 110 })]
		public static CompositeDisplay read(BitReader _in)
		{
			CompositeDisplay cd = new CompositeDisplay();
			cd.v_axis = _in.read1Bit();
			cd.field_sequence = _in.readNBit(3);
			cd.sub_carrier = _in.read1Bit();
			cd.burst_amplitude = _in.readNBit(7);
			cd.sub_carrier_phase = _in.readNBit(8);
			return cd;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 128, 98, 109, 110, 109, 110, 112 })]
		public virtual void write(BitWriter @out)
		{
			@out.write1Bit(v_axis);
			@out.writeNBit(field_sequence, 3);
			@out.write1Bit(sub_carrier);
			@out.writeNBit(burst_amplitude, 7);
			@out.writeNBit(sub_carrier_phase, 8);
		}
	}

	public const int Top_Field = 1;

	public const int Bottom_Field = 2;

	public const int Frame = 3;

	public int[][] f_code;

	public int intra_dc_precision;

	public int picture_structure;

	public int top_field_first;

	public int frame_pred_frame_dct;

	public int concealment_motion_vectors;

	public int q_scale_type;

	public int intra_vlc_format;

	public int alternate_scan;

	public int repeat_first_field;

	public int chroma_420_type;

	public int progressive_frame;

	public CompositeDisplay compositeDisplay;

	public const int Picture_Coding_Extension = 8;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 105, 127, 11 })]
	public PictureCodingExtension()
	{
		int[] array = new int[2];
		int num = (array[1] = 2);
		num = (array[0] = 2);
		f_code = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 103, 114, 114, 114, 114, 110, 110,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 105,
		173
	})]
	public static PictureCodingExtension read(BitReader _in)
	{
		PictureCodingExtension pce = new PictureCodingExtension();
		pce.f_code[0][0] = _in.readNBit(4);
		pce.f_code[0][1] = _in.readNBit(4);
		pce.f_code[1][0] = _in.readNBit(4);
		pce.f_code[1][1] = _in.readNBit(4);
		pce.intra_dc_precision = _in.readNBit(2);
		pce.picture_structure = _in.readNBit(2);
		pce.top_field_first = _in.read1Bit();
		pce.frame_pred_frame_dct = _in.read1Bit();
		pce.concealment_motion_vectors = _in.read1Bit();
		pce.q_scale_type = _in.read1Bit();
		pce.intra_vlc_format = _in.read1Bit();
		pce.alternate_scan = _in.read1Bit();
		pce.repeat_first_field = _in.read1Bit();
		pce.chroma_420_type = _in.read1Bit();
		pce.progressive_frame = _in.read1Bit();
		if (_in.read1Bit() != 0)
		{
			pce.compositeDisplay = CompositeDisplay.read(_in);
		}
		return pce;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 162, 104, 105, 114, 114, 114, 114, 110,
		110, 109, 109, 109, 109, 109, 109, 109, 109, 109,
		115, 105, 109, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(8, 4);
		bw.writeNBit(f_code[0][0], 4);
		bw.writeNBit(f_code[0][1], 4);
		bw.writeNBit(f_code[1][0], 4);
		bw.writeNBit(f_code[1][1], 4);
		bw.writeNBit(intra_dc_precision, 2);
		bw.writeNBit(picture_structure, 2);
		bw.write1Bit(top_field_first);
		bw.write1Bit(frame_pred_frame_dct);
		bw.write1Bit(concealment_motion_vectors);
		bw.write1Bit(q_scale_type);
		bw.write1Bit(intra_vlc_format);
		bw.write1Bit(alternate_scan);
		bw.write1Bit(repeat_first_field);
		bw.write1Bit(chroma_420_type);
		bw.write1Bit(progressive_frame);
		bw.write1Bit((compositeDisplay != null) ? 1 : 0);
		if (compositeDisplay != null)
		{
			compositeDisplay.write(bw);
		}
		bw.flush();
	}
}
