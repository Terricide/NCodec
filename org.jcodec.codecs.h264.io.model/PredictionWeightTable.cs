using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class PredictionWeightTable : Object
{
	public int lumaLog2WeightDenom;

	public int chromaLog2WeightDenom;

	public int[][] lumaWeight;

	public int[][][] chromaWeight;

	public int[][] lumaOffset;

	public int[][][] chromaOffset;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 105, 109, 141, 109, 109 })]
	public PredictionWeightTable()
	{
		lumaWeight = new int[2][];
		chromaWeight = new int[2][][];
		lumaOffset = new int[2][];
		chromaOffset = new int[2][][];
	}
}
