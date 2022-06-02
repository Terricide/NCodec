using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.io.model;

namespace org.jcodec.codecs.h264.decode;

public class DeblockerInput : Object
{
	public int[][] nCoeff;

	public H264Utils.MvList2D mvs;

	public MBType[] mbTypes;

	public int[][] mbQps;

	public bool[] tr8x8Used;

	public Frame[][][] refsUsed;

	public SliceHeader[] shs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 105, 106, 136, 127, 19, 114, 111,
		111, 127, 17, 111, 111
	})]
	public DeblockerInput(SeqParameterSet activeSps)
	{
		int picWidthInMbs = activeSps.picWidthInMbsMinus1 + 1;
		int picHeightInMbs = SeqParameterSet.getPicHeightInMbs(activeSps);
		int num = picHeightInMbs << 2;
		int num2 = picWidthInMbs << 2;
		int[] array = new int[2];
		int num3 = (array[1] = num2);
		num3 = (array[0] = num);
		nCoeff = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		mvs = new H264Utils.MvList2D(picWidthInMbs << 2, picHeightInMbs << 2);
		mbTypes = new MBType[picHeightInMbs * picWidthInMbs];
		tr8x8Used = new bool[picHeightInMbs * picWidthInMbs];
		int num4 = picHeightInMbs * picWidthInMbs;
		array = new int[2];
		num3 = (array[1] = num4);
		num3 = (array[0] = 3);
		mbQps = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		shs = new SliceHeader[picHeightInMbs * picWidthInMbs];
		refsUsed = new Frame[picHeightInMbs * picWidthInMbs][][];
	}
}
