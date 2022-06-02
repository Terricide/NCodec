using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common.io;

namespace org.jcodec.codecs.h264.io.model;

public class SeqParameterSetExt : Object
{
	public int seq_parameter_set_id;

	public int aux_format_idc;

	public int bit_depth_aux_minus8;

	public bool alpha_incr_flag;

	public bool additional_extension_flag;

	public int alpha_opaque_value;

	public int alpha_transparent_value;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(23)]
	public SeqParameterSetExt()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 136, 103, 114, 114, 105, 114, 114,
		123, 155, 146
	})]
	public static SeqParameterSetExt read(ByteBuffer @is)
	{
		BitReader _in = BitReader.createBitReader(@is);
		SeqParameterSetExt spse = new SeqParameterSetExt();
		spse.seq_parameter_set_id = CAVLCReader.readUEtrace(_in, "SPSE: seq_parameter_set_id");
		spse.aux_format_idc = CAVLCReader.readUEtrace(_in, "SPSE: aux_format_idc");
		if (spse.aux_format_idc != 0)
		{
			spse.bit_depth_aux_minus8 = CAVLCReader.readUEtrace(_in, "SPSE: bit_depth_aux_minus8");
			spse.alpha_incr_flag = CAVLCReader.readBool(_in, "SPSE: alpha_incr_flag");
			spse.alpha_opaque_value = CAVLCReader.readU(_in, spse.bit_depth_aux_minus8 + 9, "SPSE: alpha_opaque_value");
			spse.alpha_transparent_value = CAVLCReader.readU(_in, spse.bit_depth_aux_minus8 + 9, "SPSE: alpha_transparent_value");
		}
		spse.additional_extension_flag = CAVLCReader.readBool(_in, "SPSE: additional_extension_flag");
		return spse;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 104, 105 })]
	public virtual void write(ByteBuffer @out)
	{
		BitWriter writer = new BitWriter(@out);
		CAVLCWriter.writeTrailingBits(writer);
	}
}
