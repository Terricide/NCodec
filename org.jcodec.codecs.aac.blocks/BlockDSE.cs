using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac.blocks;

public class BlockDSE : Block
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public BlockDSE()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 98, 105, 104, 105, 105, 107, 100, 136,
		111, 145
	})]
	public override void parse(BitReader _in)
	{
		int elemType = _in.readNBit(4);
		int byte_align = _in.read1Bit();
		int count = _in.readNBit(8);
		if (count == 255)
		{
			count += _in.readNBit(8);
		}
		if (byte_align != 0)
		{
			_in.align();
		}
		if (_in.skip(8 * count) != 8 * count)
		{
			
			throw new RuntimeException("Overread");
		}
	}
}
