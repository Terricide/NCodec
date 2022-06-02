using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.model;

public class Point : Object
{
	private int x;

	private int y;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 105, 104, 104 })]
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	[LineNumberTable(20)]
	public virtual int getX()
	{
		return x;
	}

	[LineNumberTable(24)]
	public virtual int getY()
	{
		return y;
	}
}
