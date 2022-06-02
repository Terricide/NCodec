using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.decode.aso;

public class FlatMBlockMapper : Object, Mapper
{
	private int frameWidthInMbs;

	private int firstMBAddr;

	[LineNumberTable(33)]
	public virtual int getAddress(int index)
	{
		return firstMBAddr + index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 104, 104 })]
	public FlatMBlockMapper(int frameWidthInMbs, int firstMBAddr)
	{
		this.frameWidthInMbs = frameWidthInMbs;
		this.firstMBAddr = firstMBAddr;
	}

	[LineNumberTable(new byte[] { 159, 137, 130, 106, 121 })]
	public virtual bool leftAvailable(int index)
	{
		int mbAddr = index + firstMBAddr;
		int num = frameWidthInMbs;
		return (num != -1 && mbAddr % num != 0 && 0 == 0 && mbAddr > firstMBAddr) ? true : false;
	}

	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public virtual bool topAvailable(int index)
	{
		int mbAddr = index + firstMBAddr;
		return mbAddr - frameWidthInMbs >= firstMBAddr;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(37)]
	public virtual int getMbX(int index)
	{
		int address = getAddress(index);
		int num = frameWidthInMbs;
		return (num != -1) ? (address % num) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	public virtual int getMbY(int index)
	{
		int address = getAddress(index);
		int num = frameWidthInMbs;
		return (num != -1) ? (address / num) : (-address);
	}

	[LineNumberTable(new byte[] { 159, 131, 98, 106, 123 })]
	public virtual bool topRightAvailable(int index)
	{
		int mbAddr = index + firstMBAddr;
		int num = mbAddr + 1;
		int num2 = frameWidthInMbs;
		return (num2 != -1 && num % num2 != 0 && 0 == 0 && mbAddr - frameWidthInMbs + 1 >= firstMBAddr) ? true : false;
	}

	[LineNumberTable(new byte[] { 159, 130, 162, 106, 121 })]
	public virtual bool topLeftAvailable(int index)
	{
		int mbAddr = index + firstMBAddr;
		int num = frameWidthInMbs;
		return (num != -1 && mbAddr % num != 0 && 0 == 0 && mbAddr - frameWidthInMbs - 1 >= firstMBAddr) ? true : false;
	}
}
