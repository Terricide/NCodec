using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.model;

public class Rect : Object
{
	private int x;

	private int y;

	private int width;

	private int height;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 104, 104, 104, 105 })]
	public Rect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	[LineNumberTable(32)]
	public virtual int getWidth()
	{
		return width;
	}

	[LineNumberTable(36)]
	public virtual int getHeight()
	{
		return height;
	}

	[LineNumberTable(24)]
	public virtual int getX()
	{
		return x;
	}

	[LineNumberTable(28)]
	public virtual int getY()
	{
		return y;
	}

	[LineNumberTable(new byte[] { 159, 132, 98, 100, 99, 109, 109, 109, 109 })]
	public override int hashCode()
	{
		int prime = 31;
		int result = 1;
		result = 31 * result + height;
		result = 31 * result + width;
		result = 31 * result + x;
		return 31 * result + y;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 101, 99, 100, 99, 111, 99, 104,
		111, 99, 111, 99, 111, 99, 111, 99
	})]
	public override bool equals(object obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (obj == null)
		{
			return false;
		}
		if ((object)((object)this).GetType() != obj.GetType())
		{
			return false;
		}
		Rect other = (Rect)obj;
		if (height != other.height)
		{
			return false;
		}
		if (width != other.width)
		{
			return false;
		}
		if (x != other.x)
		{
			return false;
		}
		if (y != other.y)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(72)]
	public override string toString()
	{
		string result = new StringBuilder().append("Rect [x=").append(x).append(", y=")
			.append(y)
			.append(", width=")
			.append(width)
			.append(", height=")
			.append(height)
			.append("]")
			.toString();
		
		return result;
	}
}
