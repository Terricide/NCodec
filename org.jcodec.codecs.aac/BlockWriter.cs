using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.aac.blocks;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac;

public class BlockWriter : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public BlockWriter()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 147, 110, 130 })]
	public virtual void nextBlock(BitWriter bits, Block block)
	{
		bits.writeNBit(block.getType().ordinal(), 3);
		if (block.getType() != BlockType.___003C_003ETYPE_END)
		{
		}
	}
}
