using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac.blocks;

public class BlockFil : Block
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public BlockFil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 105, 102, 109, 101, 111, 113 })]
	public override void parse(BitReader _in)
	{
		int num = _in.readNBit(4);
		if (num == 15)
		{
			num += _in.readNBit(8) - 1;
		}
		if (num > 0 && _in.skip(8 * num) != 8 * num)
		{
			
			throw new RuntimeException("Overread");
		}
	}
}
