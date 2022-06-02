using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class HRDParameters : Object
{
	public int cpbCntMinus1;

	public int bitRateScale;

	public int cpbSizeScale;

	public int[] bitRateValueMinus1;

	public int[] cpbSizeValueMinus1;

	public bool[] cbrFlag;

	public int initialCpbRemovalDelayLengthMinus1;

	public int cpbRemovalDelayLengthMinus1;

	public int dpbOutputDelayLengthMinus1;

	public int timeOffsetLength;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public HRDParameters()
	{
	}
}
