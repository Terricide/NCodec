using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.model;

public class Plane : Object
{
	internal int[] data;

	internal Size size;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 105, 104, 104 })]
	public Plane(int[] data, Size size)
	{
		this.data = data;
		this.size = size;
	}

	[LineNumberTable(21)]
	public virtual int[] getData()
	{
		return data;
	}

	[LineNumberTable(25)]
	public virtual Size getSize()
	{
		return size;
	}
}
