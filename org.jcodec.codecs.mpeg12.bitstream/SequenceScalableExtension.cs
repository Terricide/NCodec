using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class SequenceScalableExtension : Object, MPEGHeader
{
	public const int DATA_PARTITIONING = 0;

	public const int SPATIAL_SCALABILITY = 1;

	public const int SNR_SCALABILITY = 2;

	public const int TEMPORAL_SCALABILITY = 3;

	public int scalable_mode;

	public int layer_id;

	public int lower_layer_prediction_horizontal_size;

	public int lower_layer_prediction_vertical_size;

	public int horizontal_subsampling_factor_m;

	public int horizontal_subsampling_factor_n;

	public int vertical_subsampling_factor_m;

	public int vertical_subsampling_factor_n;

	public int picture_mux_enable;

	public int mux_to_progressive_sequence;

	public int picture_mux_order;

	public int picture_mux_factor;

	public const int Sequence_Scalable_Extension = 5;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 103, 110, 142, 106, 111, 104, 111,
		110, 110, 110, 174, 106, 109, 105, 109, 110, 174
	})]
	public static SequenceScalableExtension read(BitReader _in)
	{
		SequenceScalableExtension sse = new SequenceScalableExtension();
		sse.scalable_mode = _in.readNBit(2);
		sse.layer_id = _in.readNBit(4);
		if (sse.scalable_mode == 1)
		{
			sse.lower_layer_prediction_horizontal_size = _in.readNBit(14);
			_in.read1Bit();
			sse.lower_layer_prediction_vertical_size = _in.readNBit(14);
			sse.horizontal_subsampling_factor_m = _in.readNBit(5);
			sse.horizontal_subsampling_factor_n = _in.readNBit(5);
			sse.vertical_subsampling_factor_m = _in.readNBit(5);
			sse.vertical_subsampling_factor_n = _in.readNBit(5);
		}
		if (sse.scalable_mode == 3)
		{
			sse.picture_mux_enable = _in.read1Bit();
			if (sse.picture_mux_enable != 0)
			{
				sse.mux_to_progressive_sequence = _in.read1Bit();
			}
			sse.picture_mux_order = _in.readNBit(3);
			sse.picture_mux_factor = _in.readNBit(3);
		}
		return sse;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 104, 137, 110, 142, 106, 111, 104,
		111, 110, 110, 110, 174, 106, 109, 105, 109, 110,
		142, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(5, 4);
		bw.writeNBit(scalable_mode, 2);
		bw.writeNBit(layer_id, 4);
		if (scalable_mode == 1)
		{
			bw.writeNBit(lower_layer_prediction_horizontal_size, 14);
			bw.write1Bit(1);
			bw.writeNBit(lower_layer_prediction_vertical_size, 14);
			bw.writeNBit(horizontal_subsampling_factor_m, 5);
			bw.writeNBit(horizontal_subsampling_factor_n, 5);
			bw.writeNBit(vertical_subsampling_factor_m, 5);
			bw.writeNBit(vertical_subsampling_factor_n, 5);
		}
		if (scalable_mode == 3)
		{
			bw.write1Bit(picture_mux_enable);
			if (picture_mux_enable != 0)
			{
				bw.write1Bit(mux_to_progressive_sequence);
			}
			bw.writeNBit(picture_mux_order, 3);
			bw.writeNBit(picture_mux_factor, 3);
		}
		bw.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public SequenceScalableExtension()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 66, 109, 103, 109, 189 })]
	public static MPEGConst.MBType[] mbTypeVal(int picture_coding_type, SequenceScalableExtension sse)
	{
		if (sse != null && sse.scalable_mode == 2)
		{
			return MPEGConst.___003C_003EmbTypeValSNR;
		}
		if (sse != null && sse.scalable_mode == 1)
		{
			return picture_coding_type switch
			{
				1 => MPEGConst.___003C_003EmbTypeValISpat, 
				2 => MPEGConst.___003C_003EmbTypeValPSpat, 
				_ => MPEGConst.___003C_003EmbTypeValBSpat, 
			};
		}
		return picture_coding_type switch
		{
			1 => MPEGConst.___003C_003EmbTypeValI, 
			2 => MPEGConst.___003C_003EmbTypeValP, 
			_ => MPEGConst.___003C_003EmbTypeValB, 
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 66, 109, 103, 109, 189 })]
	public static VLC vlcMBType(int picture_coding_type, SequenceScalableExtension sse)
	{
		if (sse != null && sse.scalable_mode == 2)
		{
			return MPEGConst.___003C_003EvlcMBTypeSNR;
		}
		if (sse != null && sse.scalable_mode == 1)
		{
			return picture_coding_type switch
			{
				1 => MPEGConst.___003C_003EvlcMBTypeISpat, 
				2 => MPEGConst.___003C_003EvlcMBTypePSpat, 
				_ => MPEGConst.___003C_003EvlcMBTypeBSpat, 
			};
		}
		return picture_coding_type switch
		{
			1 => MPEGConst.___003C_003EvlcMBTypeI, 
			2 => MPEGConst.___003C_003EvlcMBTypeP, 
			_ => MPEGConst.___003C_003EvlcMBTypeB, 
		};
	}
}
