using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.png;

public class PNGConsts : Object
{
	internal const long PNGSIG = -8552249625308161526L;

	internal const int PNGSIGhi = -1991225785;

	internal const int MNGSIGhi = -1974645177;

	internal const int PNGSIGlo = 218765834;

	internal const int MNGSIGlo = 218765834;

	internal const int TAG_IHDR = 1229472850;

	internal const int TAG_IDAT = 1229209940;

	internal const int TAG_PLTE = 1347179589;

	internal const int TAG_tRNS = 1951551059;

	internal const int TAG_IEND = 1229278788;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public PNGConsts()
	{
	}
}
