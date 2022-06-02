using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class VUIParameters : Object
{
	public class BitstreamRestriction : Object
	{
		public bool motionVectorsOverPicBoundariesFlag;

		public int maxBytesPerPicDenom;

		public int maxBitsPerMbDenom;

		public int log2MaxMvLengthHorizontal;

		public int log2MaxMvLengthVertical;

		public int numReorderFrames;

		public int maxDecFrameBuffering;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(11)]
		public BitstreamRestriction()
		{
		}
	}

	public bool aspectRatioInfoPresentFlag;

	public int sarWidth;

	public int sarHeight;

	public bool overscanInfoPresentFlag;

	public bool overscanAppropriateFlag;

	public bool videoSignalTypePresentFlag;

	public int videoFormat;

	public bool videoFullRangeFlag;

	public bool colourDescriptionPresentFlag;

	public int colourPrimaries;

	public int transferCharacteristics;

	public int matrixCoefficients;

	public bool chromaLocInfoPresentFlag;

	public int chromaSampleLocTypeTopField;

	public int chromaSampleLocTypeBottomField;

	public bool timingInfoPresentFlag;

	public int numUnitsInTick;

	public int timeScale;

	public bool fixedFrameRateFlag;

	public bool lowDelayHrdFlag;

	public bool picStructPresentFlag;

	public HRDParameters nalHRDParams;

	public HRDParameters vclHRDParams;

	public BitstreamRestriction bitstreamRestriction;

	public AspectRatio aspectRatio;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(9)]
	public VUIParameters()
	{
	}
}
