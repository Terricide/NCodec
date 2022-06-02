using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.decode.aso;

public class MBToSliceGroupMap : Object
{
	private int[] groups;

	private int[] indices;

	private int[][] inverse;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104, 104 })]
	public MBToSliceGroupMap(int[] groups, int[] indices, int[][] inverse)
	{
		this.groups = groups;
		this.indices = indices;
		this.inverse = inverse;
	}

	[LineNumberTable(25)]
	public virtual int[] getGroups()
	{
		return groups;
	}

	[LineNumberTable(29)]
	public virtual int[] getIndices()
	{
		return indices;
	}

	[LineNumberTable(33)]
	public virtual int[][] getInverse()
	{
		return inverse;
	}
}
