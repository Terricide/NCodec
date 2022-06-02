using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class PictureSpatialScalableExtension : Object, MPEGHeader
{
	public int lower_layer_temporal_reference;

	public int lower_layer_horizontal_offset;

	public int lower_layer_vertical_offset;

	public int spatial_temporal_weight_code_table_index;

	public int lower_layer_progressive_frame;

	public int lower_layer_deinterlaced_field_select;

	public const int Picture_Spatial_Scalable_Extension = 9;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 66, 135, 111, 104, 111, 104, 111, 110,
		109, 141
	})]
	public static PictureSpatialScalableExtension read(BitReader _in)
	{
		PictureSpatialScalableExtension psse = new PictureSpatialScalableExtension();
		psse.lower_layer_temporal_reference = _in.readNBit(10);
		_in.read1Bit();
		psse.lower_layer_horizontal_offset = _in.readNBit(15);
		_in.read1Bit();
		psse.lower_layer_vertical_offset = _in.readNBit(15);
		psse.spatial_temporal_weight_code_table_index = _in.readNBit(2);
		psse.lower_layer_progressive_frame = _in.read1Bit();
		psse.lower_layer_deinterlaced_field_select = _in.read1Bit();
		return psse;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 104, 138, 111, 104, 111, 104, 111,
		110, 109, 141, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(9, 4);
		bw.writeNBit(lower_layer_temporal_reference, 10);
		bw.write1Bit(1);
		bw.writeNBit(lower_layer_horizontal_offset, 15);
		bw.write1Bit(1);
		bw.writeNBit(lower_layer_vertical_offset, 15);
		bw.writeNBit(spatial_temporal_weight_code_table_index, 2);
		bw.write1Bit(lower_layer_progressive_frame);
		bw.write1Bit(lower_layer_deinterlaced_field_select);
		bw.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public PictureSpatialScalableExtension()
	{
	}
}
