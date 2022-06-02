using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.decode.aso;

public class PrebuiltMBlockMapper : Object, Mapper
{
	private MBToSliceGroupMap map;

	private int firstMBInSlice;

	private int groupId;

	private int picWidthInMbs;

	private int indexOfFirstMb;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 104, 111, 104, 111 })]
	public PrebuiltMBlockMapper(MBToSliceGroupMap map, int firstMBInSlice, int picWidthInMbs)
	{
		this.map = map;
		this.firstMBInSlice = firstMBInSlice;
		groupId = map.getGroups()[firstMBInSlice];
		this.picWidthInMbs = picWidthInMbs;
		indexOfFirstMb = map.getIndices()[firstMBInSlice];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(30)]
	public virtual int getAddress(int mbIndex)
	{
		return map.getInverse()[groupId][mbIndex + indexOfFirstMb];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 125, 133 })]
	public virtual bool leftAvailable(int mbIndex)
	{
		int mbAddr = map.getInverse()[groupId][mbIndex + indexOfFirstMb];
		int leftMBAddr = mbAddr - 1;
		int result;
		if (leftMBAddr >= firstMBInSlice)
		{
			int num = picWidthInMbs;
			if (num != -1 && mbAddr % num != 0 && map.getGroups()[leftMBAddr] == groupId)
			{
				result = 1;
				goto IL_0059;
			}
		}
		result = 0;
		goto IL_0059;
		IL_0059:
		return (byte)result != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 125, 138 })]
	public virtual bool topAvailable(int mbIndex)
	{
		int mbAddr = map.getInverse()[groupId][mbIndex + indexOfFirstMb];
		int topMBAddr = mbAddr - picWidthInMbs;
		return (topMBAddr >= firstMBInSlice && map.getGroups()[topMBAddr] == groupId) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(48)]
	public virtual int getMbX(int index)
	{
		int address = getAddress(index);
		int num = picWidthInMbs;
		return (num != -1) ? (address % num) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(52)]
	public virtual int getMbY(int index)
	{
		int address = getAddress(index);
		int num = picWidthInMbs;
		return (num != -1) ? (address / num) : (-address);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 66, 125, 140 })]
	public virtual bool topRightAvailable(int mbIndex)
	{
		int mbAddr = map.getInverse()[groupId][mbIndex + indexOfFirstMb];
		int topRMBAddr = mbAddr - picWidthInMbs + 1;
		int result;
		if (topRMBAddr >= firstMBInSlice)
		{
			int num = mbAddr + 1;
			int num2 = picWidthInMbs;
			if (num2 != -1 && num % num2 != 0 && map.getGroups()[topRMBAddr] == groupId)
			{
				result = 1;
				goto IL_0062;
			}
		}
		result = 0;
		goto IL_0062;
		IL_0062:
		return (byte)result != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 162, 125, 140 })]
	public virtual bool topLeftAvailable(int mbIndex)
	{
		int mbAddr = map.getInverse()[groupId][mbIndex + indexOfFirstMb];
		int topLMBAddr = mbAddr - picWidthInMbs - 1;
		int result;
		if (topLMBAddr >= firstMBInSlice)
		{
			int num = picWidthInMbs;
			if (num != -1 && mbAddr % num != 0 && map.getGroups()[topLMBAddr] == groupId)
			{
				result = 1;
				goto IL_0060;
			}
		}
		result = 0;
		goto IL_0060;
		IL_0060:
		return (byte)result != 0;
	}
}
