using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.codecs.mpeg12;

public class MPEGPredDbl : MPEGPredOct
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 141, 98, 106 })]
	public MPEGPredDbl(MPEGPred other)
		: base(other)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 98, 159, 7 })]
	public override void predictPlane(byte[] @ref, int refX, int refY, int refW, int refH, int refVertStep, int refVertOff, int[] tgt, int tgtY, int tgtW, int tgtH, int tgtVertStep)
	{
		base.predictPlane(@ref, refX << 1, refY << 1, refW, refH, refVertStep, refVertOff, tgt, tgtY, tgtW << 2, tgtH << 2, tgtVertStep);
	}

	[HideFromJava]
	static MPEGPredDbl()
	{
		MPEGPredOct.___003Cclinit_003E();
	}
}
