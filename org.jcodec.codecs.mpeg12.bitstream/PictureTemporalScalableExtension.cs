using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class PictureTemporalScalableExtension : Object, MPEGHeader
{
	public int reference_select_code;

	public int forward_temporal_reference;

	public int backward_temporal_reference;

	public const int Picture_Temporal_Scalable_Extension = 16;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 103, 110, 111, 104, 143 })]
	public static PictureTemporalScalableExtension read(BitReader _in)
	{
		PictureTemporalScalableExtension ptse = new PictureTemporalScalableExtension();
		ptse.reference_select_code = _in.readNBit(2);
		ptse.forward_temporal_reference = _in.readNBit(10);
		_in.read1Bit();
		ptse.backward_temporal_reference = _in.readNBit(10);
		return ptse;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 104, 138, 110, 111, 104, 111, 105 })]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(16, 4);
		bw.writeNBit(reference_select_code, 2);
		bw.writeNBit(forward_temporal_reference, 10);
		bw.write1Bit(1);
		bw.writeNBit(backward_temporal_reference, 10);
		bw.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public PictureTemporalScalableExtension()
	{
	}
}
