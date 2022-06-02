using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.gain;

internal interface GCConstants
{
	const int BANDS = 4;

	const int MAX_CHANNELS = 5;

	const int NPQFTAPS = 96;

	const int NPEPARTS = 64;

	const int ID_GAIN = 16;

	static readonly int[] LN_GAIN;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(17)]
	static GCConstants()
	{
		LN_GAIN = new int[16]
		{
			-4, -3, -2, -1, 0, 1, 2, 3, 4, 5,
			6, 7, 8, 9, 10, 11
		};
	}
}
