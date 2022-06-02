using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac.blocks;

public abstract class Block : Object
{
	private BlockType type;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public Block()
	{
	}

	[LineNumberTable(19)]
	public virtual BlockType getType()
	{
		return type;
	}

	public abstract void parse(BitReader br);
}
