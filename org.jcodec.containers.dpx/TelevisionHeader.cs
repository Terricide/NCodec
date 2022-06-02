using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.dpx;

public class TelevisionHeader : Object
{
	public int timecode;

	public int userBits;

	public byte interlace;

	public byte filedNumber;

	public byte videoSignalStarted;

	public byte zero;

	public int horSamplingRateHz;

	public int vertSampleRateHz;

	public int frameRate;

	public int timeOffset;

	public int gamma;

	public int blackLevel;

	public int blackGain;

	public int breakpoint;

	public int referenceWhiteLevel;

	public int integrationTime;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public TelevisionHeader()
	{
	}
}
