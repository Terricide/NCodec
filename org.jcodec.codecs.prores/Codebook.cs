using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.prores;

public class Codebook : Object
{
	internal int riceOrder;

	internal int expOrder;

	internal int switchBits;

	internal int golombOffset;

	internal int golombBits;

	internal int riceMask;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104, 104, 136, 118, 108, 111 })]
	public Codebook(int riceOrder, int expOrder, int switchBits)
	{
		this.riceOrder = riceOrder;
		this.expOrder = expOrder;
		this.switchBits = switchBits;
		golombOffset = (1 << expOrder) - (switchBits + 1 << riceOrder);
		golombBits = expOrder - switchBits - 1;
		riceMask = (1 << riceOrder) - 1;
	}
}
