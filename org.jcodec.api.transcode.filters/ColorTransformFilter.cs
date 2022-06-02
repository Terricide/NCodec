using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.scale;

namespace org.jcodec.api.transcode.filters;

public class ColorTransformFilter : Object, Filter
{
	private Transform transform;

	private ColorSpace outputColor;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 105, 104 })]
	public ColorTransformFilter(ColorSpace outputColor)
	{
		this.outputColor = outputColor;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 120, 159, 6, 122, 114, 115 })]
	public virtual PixelStore.LoanerPicture filter(Picture picture, PixelStore store)
	{
		if (transform == null)
		{
			transform = ColorUtil.getTransform(picture.getColor(), outputColor);
			Logger.debug(new StringBuilder().append("Creating transform: ").append(transform).toString());
		}
		PixelStore.LoanerPicture outFrame = store.getPicture(picture.getWidth(), picture.getHeight(), outputColor);
		outFrame.getPicture().setCrop(picture.getCrop());
		transform.transform(picture, outFrame.getPicture());
		return outFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(42)]
	public virtual ColorSpace getInputColor()
	{
		return ColorSpace.___003C_003EANY_PLANAR;
	}

	[LineNumberTable(47)]
	public virtual ColorSpace getOutputColor()
	{
		return outputColor;
	}
}
