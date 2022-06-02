using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.codecs.h264.io.model;

public class SliceHeader : Object
{
	public SeqParameterSet sps;

	public PictureParameterSet pps;

	public RefPicMarking refPicMarkingNonIDR;

	public RefPicMarkingIDR refPicMarkingIDR;

	public int[][][] refPicReordering;

	public PredictionWeightTable predWeightTable;

	public int firstMbInSlice;

	public bool fieldPicFlag;

	public SliceType sliceType;

	public bool sliceTypeRestr;

	public int picParameterSetId;

	public int frameNum;

	public bool bottomFieldFlag;

	public int idrPicId;

	public int picOrderCntLsb;

	public int deltaPicOrderCntBottom;

	public int[] deltaPicOrderCnt;

	public int redundantPicCnt;

	public bool directSpatialMvPredFlag;

	public bool numRefIdxActiveOverrideFlag;

	public int[] numRefIdxActiveMinus1;

	public int cabacInitIdc;

	public int sliceQpDelta;

	public bool spForSwitchFlag;

	public int sliceQsDelta;

	public int disableDeblockingFilterIdc;

	public int sliceAlphaC0OffsetDiv2;

	public int sliceBetaOffsetDiv2;

	public int sliceGroupChangeCycle;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 98, 105, 109 })]
	public SliceHeader()
	{
		numRefIdxActiveMinus1 = new int[2];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(103)]
	public override string toString()
	{
		string result = Platform.toJSON(this);
		
		return result;
	}
}
