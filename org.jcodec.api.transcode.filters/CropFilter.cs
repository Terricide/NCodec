using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode.filters;

public class CropFilter : Object, Filter
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(9)]
	public CropFilter()
	{
	}

	[LineNumberTable(13)]
	public virtual PixelStore.LoanerPicture filter(Picture picture, PixelStore store)
	{
		return null;
	}

	[LineNumberTable(19)]
	public virtual ColorSpace getInputColor()
	{
		return null;
	}

	[LineNumberTable(25)]
	public virtual ColorSpace getOutputColor()
	{
		return null;
	}
}
