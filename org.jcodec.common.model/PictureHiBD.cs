using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.common.model;

public class PictureHiBD : Object
{
	private ColorSpace color;

	private int width;

	private int height;

	private int[][] data;

	private Rect crop;

	private int bitDepth;

	[LineNumberTable(181)]
	public virtual int getBitDepth()
	{
		return bitDepth;
	}

	[LineNumberTable(86)]
	public virtual int getWidth()
	{
		return width;
	}

	[LineNumberTable(90)]
	public virtual int getHeight()
	{
		return height;
	}

	[LineNumberTable(98)]
	public virtual ColorSpace getColor()
	{
		return color;
	}

	[LineNumberTable(106)]
	public virtual Rect getCrop()
	{
		return crop;
	}

	[LineNumberTable(102)]
	public virtual int[][] getData()
	{
		return data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 104, 108, 63, 15, 199, 100, 105,
		48, 169, 106, 108, 103, 21, 233, 70
	})]
	public static PictureHiBD doCreate(int width, int height, ColorSpace colorSpace, int bitDepth, Rect crop)
	{
		int[] planeSizes = new int[4];
		for (int k = 0; k < colorSpace.nComp; k++)
		{
			int num = colorSpace.compPlane[k];
			int[] array = planeSizes;
			array[num] += (width >> colorSpace.compWidth[k]) * (height >> colorSpace.compHeight[k]);
		}
		int nPlanes = 0;
		for (int j = 0; j < 4; j++)
		{
			nPlanes += ((planeSizes[j] != 0) ? 1 : 0);
		}
		int[][] data = new int[nPlanes][];
		int i = 0;
		int plane = 0;
		for (; i < 4; i++)
		{
			if (planeSizes[i] != 0)
			{
				int num2 = plane;
				plane++;
				data[num2] = new int[planeSizes[i]];
			}
		}
		PictureHiBD result = new PictureHiBD(width, height, data, colorSpace, 8, crop);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 105, 104, 104, 104, 105, 105, 105 })]
	public PictureHiBD(int width, int height, int[][] data, ColorSpace color, int bitDepth, Rect crop)
	{
		this.width = width;
		this.height = height;
		this.data = data;
		this.color = color;
		this.crop = crop;
		this.bitDepth = bitDepth;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(53)]
	public static PictureHiBD create(int width, int height, ColorSpace colorSpace)
	{
		PictureHiBD result = doCreate(width, height, colorSpace, 8, null);
		
		return result;
	}

	[LineNumberTable(118)]
	public virtual bool compatible(PictureHiBD src)
	{
		return (src.color == color && src.width == width && src.height == height) ? true : false;
	}

	[LineNumberTable(new byte[]
	{
		159, 104, 130, 106, 104, 104, 44, 167, 102, 230,
		59, 231, 71
	})]
	private void cropSub(int[] src, int x, int y, int w, int h, int srcStride, int[] tgt)
	{
		int srcOff = y * srcStride + x;
		int dstOff = 0;
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				tgt[dstOff + j] = src[srcOff + j];
			}
			srcOff += srcStride;
			dstOff += w;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(169)]
	public virtual int getCroppedWidth()
	{
		int result = ((crop != null) ? crop.getWidth() : width);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(173)]
	public virtual int getCroppedHeight()
	{
		int result = ((crop != null) ? crop.getHeight() : height);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		92,
		98,
		111,
		111,
		109,
		127,
		15,
		159,
		27,
		106,
		118,
		115,
		119,
		3,
		41,
		byte.MaxValue,
		9,
		70
	})]
	private bool planeEquals(PictureHiBD other, int plane)
	{
		int cw = color.compWidth[plane];
		int ch = color.compHeight[plane];
		int offA = ((other.getCrop() != null) ? ((other.getCrop().getX() >> cw) + (other.getCrop().getY() >> ch) * (other.getWidth() >> cw)) : 0);
		int offB = ((crop != null) ? ((crop.getX() >> cw) + (crop.getY() >> ch) * (width >> cw)) : 0);
		int[] planeData = other.getPlaneData(plane);
		int i = 0;
		while (i < getCroppedHeight() >> ch)
		{
			for (int j = 0; j < getCroppedWidth() >> cw; j++)
			{
				if (planeData[offA + j] != data[plane][offB + j])
				{
					return false;
				}
			}
			i++;
			offA += other.getWidth() >> cw;
			offB += width >> cw;
		}
		return true;
	}

	[LineNumberTable(94)]
	public virtual int[] getPlaneData(int plane)
	{
		return data[plane];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(28)]
	public static PictureHiBD createPicture(int width, int height, int[][] data, ColorSpace color)
	{
		PictureHiBD result = new PictureHiBD(width, height, data, color, 8, new Rect(0, 0, width, height));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public static PictureHiBD createPictureWithDepth(int width, int height, int[][] data, ColorSpace color, int bitDepth)
	{
		PictureHiBD result = new PictureHiBD(width, height, data, color, bitDepth, new Rect(0, 0, width, height));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(36)]
	public static PictureHiBD createPictureCropped(int width, int height, int[][] data, ColorSpace color, Rect crop)
	{
		PictureHiBD result = new PictureHiBD(width, height, data, color, 8, crop);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(49)]
	public static PictureHiBD clonePicture(PictureHiBD other)
	{
		PictureHiBD result = new PictureHiBD(other.width, other.height, other.data, other.color, other.bitDepth, other.crop);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public static PictureHiBD createWithDepth(int width, int height, ColorSpace colorSpace, int bitDepth)
	{
		PictureHiBD result = doCreate(width, height, colorSpace, bitDepth, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(61)]
	public static PictureHiBD createCropped(int width, int height, ColorSpace colorSpace, Rect crop)
	{
		PictureHiBD result = doCreate(width, height, colorSpace, 8, crop);
		
		return result;
	}

	[LineNumberTable(110)]
	public virtual int getPlaneWidth(int plane)
	{
		return width >> color.compWidth[plane];
	}

	[LineNumberTable(114)]
	public virtual int getPlaneHeight(int plane)
	{
		return height >> color.compHeight[plane];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(122)]
	public virtual PictureHiBD createCompatible()
	{
		PictureHiBD result = create(width, height, color);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		111,
		130,
		106,
		113,
		113,
		107,
		99,
		byte.MaxValue,
		40,
		61,
		234,
		70
	})]
	public virtual void copyFrom(PictureHiBD src)
	{
		if (!compatible(src))
		{
			
			throw new IllegalArgumentException("Can not copy to incompatible picture");
		}
		for (int plane = 0; plane < color.nComp; plane++)
		{
			if (data[plane] != null)
			{
				ByteCodeHelper.arraycopy_primitive_4(src.data[plane], 0, data[plane], 0, (width >> color.compWidth[plane]) * (height >> color.compHeight[plane]));
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 108, 98, 111, 127, 28, 99, 159, 4, 116,
		107, 102, 127, 41, 63, 51, 230, 61, 234, 72
	})]
	public virtual PictureHiBD cropped()
	{
		if (crop == null || (crop.getX() == 0 && crop.getY() == 0 && crop.getWidth() == width && crop.getHeight() == height))
		{
			return this;
		}
		PictureHiBD result = create(crop.getWidth(), crop.getHeight(), color);
		for (int plane = 0; plane < color.nComp; plane++)
		{
			if (data[plane] != null)
			{
				cropSub(data[plane], crop.getX() >> color.compWidth[plane], crop.getY() >> color.compHeight[plane], crop.getWidth() >> color.compWidth[plane], crop.getHeight() >> color.compHeight[plane], width >> color.compWidth[plane], result.data[plane]);
			}
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 101, 98, 104 })]
	public virtual void setCrop(Rect crop)
	{
		this.crop = crop;
	}

	[LineNumberTable(new byte[] { 159, 98, 98, 104 })]
	public virtual void setBitDepth(int bitDepth)
	{
		this.bitDepth = bitDepth;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 130, 108, 99, 136, 126, 110, 131, 109,
		107, 3, 167
	})]
	public override bool equals(object obj)
	{
		if (obj == null || !(obj is PictureHiBD))
		{
			return false;
		}
		PictureHiBD other = (PictureHiBD)obj;
		if (other.getCroppedWidth() != getCroppedWidth() || other.getCroppedHeight() != getCroppedHeight() || other.getColor() != color)
		{
			return false;
		}
		for (int i = 0; i < (nint)getData().LongLength; i++)
		{
			if (!planeEquals(other, i))
			{
				return false;
			}
		}
		return true;
	}
}
