using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.syntax;

internal class SCE_LFE : Element
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private ICStream ics;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 109 })]
	internal SCE_LFE(int frameLength)
	{
		ics = new ICStream(frameLength);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 137, 162, 104, 113 })]
	internal virtual void decode(IBitStream _in, AACDecoderConfig conf)
	{
		readElementInstanceTag(_in);
		ics.decode(_in, commonWindow: false, conf);
	}

	[LineNumberTable(28)]
	public virtual ICStream getICStream()
	{
		return ics;
	}
}
