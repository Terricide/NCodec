using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.api;

public class MediaInfo : Object
{
	private Size dim;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 105, 104 })]
	public MediaInfo(Size dim)
	{
		this.dim = dim;
	}

	[LineNumberTable(20)]
	public virtual Size getDim()
	{
		return dim;
	}

	[LineNumberTable(new byte[] { 159, 136, 66, 104 })]
	public virtual void setDim(Size dim)
	{
		this.dim = dim;
	}
}
