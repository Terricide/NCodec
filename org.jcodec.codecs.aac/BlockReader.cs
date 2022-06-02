using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.aac.blocks;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac;

public class BlockReader : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public BlockReader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 113, 105, 131, 137 })]
	public virtual Block nextBlock(BitReader bits)
	{
		BlockType type = BlockType.values()[bits.readNBit(3)];
		if (type == BlockType.___003C_003ETYPE_END)
		{
			return null;
		}
		int id = bits.readNBit(4);
		return null;
	}
}
