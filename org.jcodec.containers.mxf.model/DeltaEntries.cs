using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mxf.model;

public class DeltaEntries : Object
{
	private byte[] posTabIdx;

	private byte[] slice;

	private int[] elementData;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104, 104, 104 })]
	public DeltaEntries(byte[] posTabIdx, byte[] slice, int[] elementDelta)
	{
		this.posTabIdx = posTabIdx;
		this.slice = slice;
		elementData = elementDelta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 109, 104, 104, 104, 104, 105, 105,
		108, 108, 108, 235, 60, 233, 70
	})]
	public static DeltaEntries read(ByteBuffer bb)
	{
		bb.order(ByteOrder.BIG_ENDIAN);
		int j = bb.getInt();
		int len = bb.getInt();
		byte[] posTabIdx = new byte[j];
		byte[] slice = new byte[j];
		int[] elementDelta = new int[j];
		for (int i = 0; i < j; i++)
		{
			posTabIdx[i] = (byte)(sbyte)bb.get();
			slice[i] = (byte)(sbyte)bb.get();
			elementDelta[i] = bb.getInt();
			NIOUtils.skip(bb, len - 6);
		}
		DeltaEntries result = new DeltaEntries(posTabIdx, slice, elementDelta);
		
		return result;
	}
}
