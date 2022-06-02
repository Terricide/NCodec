using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class CopyrightExtension : Object, MPEGHeader
{
	public int copyright_flag;

	public int copyright_identifier;

	public int original_or_copy;

	public int copyright_number_1;

	public int copyright_number_2;

	public int copyright_number_3;

	public const int Copyright_Extension = 4;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public CopyrightExtension()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 66, 103, 109, 110, 109, 105, 104, 111,
		104, 111, 104, 111
	})]
	public static CopyrightExtension read(BitReader _in)
	{
		CopyrightExtension ce = new CopyrightExtension();
		ce.copyright_flag = _in.read1Bit();
		ce.copyright_identifier = _in.readNBit(8);
		ce.original_or_copy = _in.read1Bit();
		_in.skip(7);
		_in.read1Bit();
		ce.copyright_number_1 = _in.readNBit(20);
		_in.read1Bit();
		ce.copyright_number_2 = _in.readNBit(22);
		_in.read1Bit();
		ce.copyright_number_3 = _in.readNBit(22);
		return ce;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 104, 137, 109, 110, 109, 105, 104,
		111, 104, 111, 104, 143, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(4, 4);
		bw.write1Bit(copyright_flag);
		bw.writeNBit(copyright_identifier, 8);
		bw.write1Bit(original_or_copy);
		bw.writeNBit(0, 7);
		bw.write1Bit(1);
		bw.writeNBit(copyright_number_1, 20);
		bw.write1Bit(1);
		bw.writeNBit(copyright_number_2, 22);
		bw.write1Bit(1);
		bw.writeNBit(copyright_number_3, 22);
		bw.flush();
	}
}
