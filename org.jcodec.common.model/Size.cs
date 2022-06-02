using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.model;

public class Size : Object
{
	private int width;

	private int height;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 105, 104, 104 })]
	public Size(int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	[LineNumberTable(20)]
	public virtual int getWidth()
	{
		return width;
	}

	[LineNumberTable(24)]
	public virtual int getHeight()
	{
		return height;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 101, 99, 100, 99, 111, 99, 104,
		111, 99, 111, 99
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
		Size other = (Size)obj;
		if (height != other.height)
		{
			return false;
		}
		if (width != other.width)
		{
			return false;
		}
		return true;
	}

	[LineNumberTable(new byte[] { 159, 135, 98, 100, 99, 109, 109 })]
	public override int hashCode()
	{
		int prime = 31;
		int result = 1;
		result = 31 * result + height;
		return 31 * result + width;
	}
}
