using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.scale;

namespace org.jcodec.api.transcode.filters;

public class ScaleFilter : Object, Filter
{
	private BaseResampler resampler;

	private ColorSpace currentColor;

	private Size currentSize;

	private Size targetSize;

	private int width;

	private int height;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 105, 104, 104 })]
	public ScaleFilter(int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public virtual Size getTarget()
	{
		Size result = new Size(width, height);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 104, 127, 6, 109, 109, 127, 17,
		184, 159, 5, 147
	})]
	public virtual PixelStore.LoanerPicture filter(Picture picture, PixelStore store)
	{
		Size pictureSize = picture.getSize();
		if (resampler == null || currentColor != picture.getColor() || !pictureSize.equals(currentSize))
		{
			currentColor = picture.getColor();
			currentSize = picture.getSize();
			targetSize = new Size(width & currentColor.getWidthMask(), height & currentColor.getHeightMask());
			resampler = new LanczosResampler(currentSize, targetSize);
		}
		PixelStore.LoanerPicture dest = store.getPicture(targetSize.getWidth(), targetSize.getHeight(), currentColor);
		resampler.resample(picture, dest.getPicture());
		return dest;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public virtual ColorSpace getInputColor()
	{
		return ColorSpace.___003C_003EANY_PLANAR;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(62)]
	public virtual ColorSpace getOutputColor()
	{
		return ColorSpace.___003C_003ESAME;
	}
}
