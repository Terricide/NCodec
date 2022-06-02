using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.codecs.mpeg12;

public class MPEGPredQuad : MPEGPredOct
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public MPEGPredQuad(MPEGPred other)
		: base(other)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 159, 3 })]
	public override void predictPlane(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		base.predictPlane(@ref, refX, refY, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW << 1, tgtH << 1, tgtVertStep);
	}

	[HideFromJava]
	static MPEGPredQuad()
	{
		MPEGPredOct.___003Cclinit_003E();
	}
}
