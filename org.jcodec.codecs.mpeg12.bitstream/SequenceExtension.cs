using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class SequenceExtension : Object, MPEGHeader
{
	public const int Chroma420 = 1;

	public const int Chroma422 = 2;

	public const int Chroma444 = 3;

	public int profile_and_level;

	public int progressive_sequence;

	public int chroma_format;

	public int horizontal_size_extension;

	public int vertical_size_extension;

	public int bit_rate_extension;

	public int vbv_buffer_size_extension;

	public int low_delay;

	public int frame_rate_extension_n;

	public int frame_rate_extension_d;

	public const int Sequence_Extension = 1;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 105 })]
	private SequenceExtension()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 103, 104, 104, 104, 104, 105, 105,
		105, 105, 105, 105
	})]
	public static SequenceExtension createSequenceExtension(int profile_and_level, int progressive_sequence, int chroma_format, int horizontal_size_extension, int vertical_size_extension, int bit_rate_extension, int vbv_buffer_size_extension, int low_delay, int frame_rate_extension_n, int frame_rate_extension_d)
	{
		SequenceExtension se = new SequenceExtension();
		se.profile_and_level = profile_and_level;
		se.progressive_sequence = progressive_sequence;
		se.chroma_format = chroma_format;
		se.horizontal_size_extension = horizontal_size_extension;
		se.vertical_size_extension = vertical_size_extension;
		se.bit_rate_extension = bit_rate_extension;
		se.vbv_buffer_size_extension = vbv_buffer_size_extension;
		se.low_delay = low_delay;
		se.frame_rate_extension_n = frame_rate_extension_n;
		se.frame_rate_extension_d = frame_rate_extension_d;
		return se;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 98, 103, 110, 109, 110, 110, 110, 111,
		110, 109, 110, 142
	})]
	public static SequenceExtension read(BitReader _in)
	{
		SequenceExtension se = new SequenceExtension();
		se.profile_and_level = _in.readNBit(8);
		se.progressive_sequence = _in.read1Bit();
		se.chroma_format = _in.readNBit(2);
		se.horizontal_size_extension = _in.readNBit(2);
		se.vertical_size_extension = _in.readNBit(2);
		se.bit_rate_extension = _in.readNBit(12);
		se.vbv_buffer_size_extension = _in.readNBit(8);
		se.low_delay = _in.read1Bit();
		se.frame_rate_extension_n = _in.readNBit(2);
		se.frame_rate_extension_d = _in.readNBit(5);
		return se;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 162, 104, 105, 110, 109, 110, 110, 110,
		111, 104, 110, 109, 110, 142, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(1, 4);
		bw.writeNBit(profile_and_level, 8);
		bw.write1Bit(progressive_sequence);
		bw.writeNBit(chroma_format, 2);
		bw.writeNBit(horizontal_size_extension, 2);
		bw.writeNBit(vertical_size_extension, 2);
		bw.writeNBit(bit_rate_extension, 12);
		bw.write1Bit(1);
		bw.writeNBit(vbv_buffer_size_extension, 8);
		bw.write1Bit(low_delay);
		bw.writeNBit(frame_rate_extension_n, 2);
		bw.writeNBit(frame_rate_extension_d, 5);
		bw.flush();
	}
}
