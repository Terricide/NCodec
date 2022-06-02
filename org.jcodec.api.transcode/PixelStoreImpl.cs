using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public class PixelStoreImpl : Object, PixelStore
{
	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Picture;>;")]
	private List buffers;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 66, 105, 108 })]
	public PixelStoreImpl()
	{
		buffers = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 127, 2, 116, 105, 110, 139, 99 })]
	public virtual PixelStore.LoanerPicture getPicture(int width, int height, ColorSpace color)
	{
		Iterator iterator = buffers.iterator();
		while (iterator.hasNext())
		{
			Picture picture = (Picture)iterator.next();
			if (picture.getWidth() == width && picture.getHeight() == height && picture.getColor() == color)
			{
				buffers.remove(picture);
				PixelStore.LoanerPicture result = new PixelStore.LoanerPicture(picture, 1);
				
				return result;
			}
		}
		PixelStore.LoanerPicture result2 = new PixelStore.LoanerPicture(Picture.create(width, height, color), 1);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 103, 105, 104, 104, 142 })]
	public virtual void putBack(PixelStore.LoanerPicture frame)
	{
		frame.decRefCnt();
		if (frame.unused())
		{
			Picture pixels = frame.getPicture();
			pixels.setCrop(null);
			buffers.add(pixels);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 105 })]
	public virtual void retake(PixelStore.LoanerPicture frame)
	{
		frame.incRefCnt();
	}
}
