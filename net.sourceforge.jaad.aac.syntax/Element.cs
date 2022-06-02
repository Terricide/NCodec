using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.sbr;

namespace net.sourceforge.jaad.aac.syntax;

public abstract class Element : Object, SyntaxConstants
{
	private int elementInstanceTag;

	private SBR sbr;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public Element()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 137, 66, 110 })]
	protected internal virtual void readElementInstanceTag(IBitStream _in)
	{
		elementInstanceTag = _in.readBits(4);
	}

	[LineNumberTable(24)]
	public virtual int getElementInstanceTag()
	{
		return elementInstanceTag;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 135, 65, 71, 127, 1, 111 })]
	internal virtual void decodeSBR(IBitStream _in, SampleFrequency sf, int count, bool stereo, bool crc, bool downSampled, bool smallFrames)
	{
		if (sbr == null)
		{
			sbr = new SBR(smallFrames, elementInstanceTag == 1, sf, downSampled);
		}
		sbr.decode(_in, count);
	}

	[LineNumberTable(33)]
	internal virtual bool isSBRPresent()
	{
		return sbr != null;
	}

	[LineNumberTable(37)]
	internal virtual SBR getSBR()
	{
		return sbr;
	}
}
