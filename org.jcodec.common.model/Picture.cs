using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.common.tools;

namespace org.jcodec.common.model;

public class Picture : Object
{
	private ColorSpace color;

	private int width;

	private int height;

	private byte[][] data;

	private byte[][] lowBits;

	private int lowBitsNum;

	private Rect crop;

	[LineNumberTable(135)]
	public virtual ColorSpace getColor()
	{
		return color;
	}

	[LineNumberTable(123)]
	public virtual int getWidth()
	{
		return width;
	}

	[LineNumberTable(127)]
	public virtual int getHeight()
	{
		return height;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(75)]
	public static Picture create(int width, int height, ColorSpace colorSpace)
	{
		Picture result = createCropped(width, height, colorSpace, null);
		
		return result;
	}

	[LineNumberTable(139)]
	public virtual byte[][] getData()
	{
		return data;
	}

	[LineNumberTable(148)]
	public virtual Rect getCrop()
	{
		return crop;
	}

	[LineNumberTable(new byte[] { 159, 85, 162, 104 })]
	public virtual void setCrop(Rect crop)
	{
		this.crop = crop;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(361)]
	public virtual Size getSize()
	{
		Size result = new Size(width, height);
		
		return result;
	}

	[LineNumberTable(131)]
	public virtual byte[] getPlaneData(int plane)
	{
		return data[plane];
	}

	[LineNumberTable(152)]
	public virtual int getPlaneWidth(int plane)
	{
		return width >> color.compWidth[plane];
	}

	[LineNumberTable(156)]
	public virtual int getPlaneHeight(int plane)
	{
		return height >> color.compHeight[plane];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 65, 130, 109, 48, 167 })]
	public virtual void fill(int val)
	{
		for (int i = 0; i < (nint)data.LongLength; i++)
		{
			Arrays.fill(data[i], (byte)(sbyte)val);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(33)]
	public static Picture createPicture(int width, int height, byte[][] data, ColorSpace color)
	{
		Picture result = new Picture(width, height, data, null, color, 0, new Rect(0, 0, width, height));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(235)]
	public virtual int getCroppedWidth()
	{
		int result = ((crop != null) ? crop.getWidth() : width);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(239)]
	public virtual int getCroppedHeight()
	{
		int result = ((crop != null) ? crop.getHeight() : height);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		132,
		98,
		105,
		104,
		104,
		104,
		105,
		105,
		105,
		137,
		104,
		112,
		118,
		102,
		159,
		53,
		112,
		159,
		53,
		118,
		102,
		159,
		53,
		112,
		byte.MaxValue,
		53,
		51,
		234,
		81
	})]
	public Picture(int width, int height, byte[][] data, byte[][] lowBits, ColorSpace color, int lowBitsNum, Rect crop)
	{
		this.width = width;
		this.height = height;
		this.data = data;
		this.lowBits = lowBits;
		this.color = color;
		this.lowBitsNum = lowBitsNum;
		this.crop = crop;
		if (color == null)
		{
			return;
		}
		for (int i = 0; i < color.nComp; i++)
		{
			int mask = 255 >> 8 - color.compWidth[i];
			if ((width & mask) != 0)
			{
				string s = new StringBuilder().append("Component ").append(i).append(" width should be a multiple of ")
					.append(1 << color.compWidth[i])
					.append(" for colorspace: ")
					.append(color)
					.toString();
				
				throw new IllegalArgumentException(s);
			}
			if (crop != null && (crop.getWidth() & mask) != 0)
			{
				string s2 = new StringBuilder().append("Component ").append(i).append(" cropped width should be a multiple of ")
					.append(1 << color.compWidth[i])
					.append(" for colorspace: ")
					.append(color)
					.toString();
				
				throw new IllegalArgumentException(s2);
			}
			mask = 255 >> 8 - color.compHeight[i];
			if ((height & mask) != 0)
			{
				string s3 = new StringBuilder().append("Component ").append(i).append(" height should be a multiple of ")
					.append(1 << color.compHeight[i])
					.append(" for colorspace: ")
					.append(color)
					.toString();
				
				throw new IllegalArgumentException(s3);
			}
			if (crop != null && (crop.getHeight() & mask) != 0)
			{
				string s4 = new StringBuilder().append("Component ").append(i).append(" cropped height should be a multiple of ")
					.append(1 << color.compHeight[i])
					.append(" for colorspace: ")
					.append(color)
					.toString();
				
				throw new IllegalArgumentException(s4);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 104, 108, 63, 15, 199, 100, 105,
		48, 169, 106, 108, 103, 21, 233, 69
	})]
	public static Picture createCropped(int width, int height, ColorSpace colorSpace, Rect crop)
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
		byte[][] data = new byte[nPlanes][];
		int i = 0;
		int plane = 0;
		for (; i < 4; i++)
		{
			if (planeSizes[i] != 0)
			{
				int num2 = plane;
				plane++;
				data[num2] = new byte[planeSizes[i]];
			}
		}
		Picture result = new Picture(width, height, data, null, colorSpace, 0, crop);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 113, 162, 104 })]
	private void setLowBits(byte[][] lowBits)
	{
		this.lowBits = lowBits;
	}

	[LineNumberTable(new byte[] { 159, 114, 162, 104 })]
	private void setLowBitsNum(int lowBitsNum)
	{
		this.lowBitsNum = lowBitsNum;
	}

	[LineNumberTable(160)]
	public virtual bool compatible(Picture src)
	{
		return (src.color == color && src.width == width && src.height == height) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 162, 111, 63, 32 })]
	protected internal virtual bool cropNeeded()
	{
		return (crop != null && (crop.getX() != 0 || crop.getY() != 0 || crop.getWidth() != width || crop.getHeight() != height)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 130, 105, 99, 159, 4, 113, 112, 107,
		102, 127, 41, 127, 49, 31, 0, 230, 61, 236,
		72, 127, 18, 63, 24, 198
	})]
	public virtual Picture cropped()
	{
		if (!cropNeeded())
		{
			return this;
		}
		Picture result = create(crop.getWidth(), crop.getHeight(), color);
		if (color.planar)
		{
			for (int plane = 0; plane < (nint)data.LongLength; plane++)
			{
				if (data[plane] != null)
				{
					cropSub(data[plane], crop.getX() >> color.compWidth[plane], crop.getY() >> color.compHeight[plane], crop.getWidth() >> color.compWidth[plane], crop.getHeight() >> color.compHeight[plane], width >> color.compWidth[plane], crop.getWidth() >> color.compWidth[plane], result.data[plane]);
				}
			}
		}
		else
		{
			cropSub(data[0], crop.getX(), crop.getY(), crop.getWidth(), crop.getHeight(), width * color.nComp, crop.getWidth() * color.nComp, result.data[0]);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(164)]
	public virtual Picture createCompatible()
	{
		Picture result = create(width, height, color);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		100,
		66,
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
	public virtual void copyFrom(Picture src)
	{
		if (!compatible(src))
		{
			
			throw new IllegalArgumentException("Can not copy to incompatible picture");
		}
		for (int plane = 0; plane < color.nComp; plane++)
		{
			if (data[plane] != null)
			{
				ByteCodeHelper.arraycopy_primitive_1(src.data[plane], 0, data[plane], 0, (width >> color.compWidth[plane]) * (height >> color.compHeight[plane]));
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 87, 66, 106, 104, 104, 44, 167, 102, 230,
		59, 231, 71
	})]
	private void cropSub(byte[] src, int x, int y, int w, int h, int srcStride, int dstStride, byte[] tgt)
	{
		int srcOff = y * srcStride + x;
		int dstOff = 0;
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < dstStride; j++)
			{
				tgt[dstOff + j] = src[srcOff + j];
			}
			srcOff += srcStride;
			dstOff += dstStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 130, 108, 101, 99, 104, 132, 104, 108,
		53, 169, 104, 136
	})]
	public static Picture createCroppedHiBD(int width, int height, int lowBitsNum, ColorSpace colorSpace, Rect crop)
	{
		Picture result = createCropped(width, height, colorSpace, crop);
		if (lowBitsNum <= 0)
		{
			return result;
		}
		byte[][] data = result.getData();
		int nPlanes = data.Length;
		byte[][] lowBits = new byte[nPlanes][];
		int i = 0;
		int plane = 0;
		for (; i < nPlanes; i++)
		{
			int num = plane;
			plane++;
			lowBits[num] = new byte[(nint)data[i].LongLength];
		}
		result.setLowBits(lowBits);
		result.setLowBitsNum(lowBitsNum);
		return result;
	}

	[LineNumberTable(143)]
	public virtual byte[][] getLowBits()
	{
		return lowBits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 70, 66, 136, 109, 113, 103, 63, 1, 7,
		234, 71, 108, 114, 116, 106, 63, 3, 9, 236,
		72
	})]
	private PictureHiBD toPictureHiBDInternal(PictureHiBD pic)
	{
		int[][] dstData = pic.getData();
		for (int j = 0; j < (nint)data.LongLength; j++)
		{
			int planeSize2 = getPlaneWidth(j) * getPlaneHeight(j);
			for (int l = 0; l < planeSize2; l++)
			{
				dstData[j][l] = data[j][l] + 128 << lowBitsNum;
			}
		}
		if (lowBits != null)
		{
			for (int i = 0; i < (nint)lowBits.LongLength; i++)
			{
				int planeSize = getPlaneWidth(i) * getPlaneHeight(i);
				for (int k = 0; k < planeSize; k++)
				{
					int[] obj = dstData[i];
					int num = k;
					int[] array = obj;
					array[num] += lowBits[i][k];
				}
			}
		}
		return pic;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		59,
		66,
		111,
		111,
		127,
		14,
		110,
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
	private bool planeEquals(Picture other, int plane)
	{
		int cw = color.compWidth[plane];
		int ch = color.compHeight[plane];
		int offA = ((other.getCrop() != null) ? ((other.getCrop().getX() >> cw) + (other.getCrop().getY() >> ch) * (other.getWidth() >> cw)) : 0);
		int offB = ((crop != null) ? ((crop.getX() >> cw) + (crop.getY() >> ch) * (width >> cw)) : 0);
		byte[] planeData = other.getPlaneData(plane);
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

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(38)]
	public static Picture createPictureHiBD(int width, int height, byte[][] data, byte[][] lowBits, ColorSpace color, int lowBitsNum)
	{
		Picture result = new Picture(width, height, data, lowBits, color, lowBitsNum, new Rect(0, 0, width, height));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(71)]
	public static Picture copyPicture(Picture other)
	{
		Picture result = new Picture(other.width, other.height, other.data, other.lowBits, other.color, 0, other.crop);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 66, 105, 138, 104, 104 })]
	public virtual Picture cloneCropped()
	{
		if (cropNeeded())
		{
			Picture result = cropped();
			
			return result;
		}
		Picture clone = createCompatible();
		clone.copyFrom(this);
		return clone;
	}

	[LineNumberTable(243)]
	public virtual int getLowBitsNum()
	{
		return lowBitsNum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 162, 106, 138, 117, 38, 167, 124, 127,
		0, 110, 119, 245, 61, 44, 234, 72, 105, 104,
		126, 127, 2, 111, 119, 241, 61, 44, 236, 73
	})]
	public static Picture fromPictureHiBD(PictureHiBD pic)
	{
		int lowBitsNum = pic.getBitDepth() - 8;
		int lowBitsRound = 1 << lowBitsNum >> 1;
		Picture result = createCroppedHiBD(pic.getWidth(), pic.getHeight(), lowBitsNum, pic.getColor(), pic.getCrop());
		for (int j = 0; j < Math.min(pic.getData().Length, result.getData().Length); j++)
		{
			for (int l = 0; l < Math.min(pic.getData()[j].Length, result.getData()[j].Length); l++)
			{
				int val2 = pic.getData()[j][l];
				int round2 = MathUtil.clip(val2 + lowBitsRound >> lowBitsNum, 0, 255);
				result.getData()[j][l] = (byte)(sbyte)(round2 - 128);
			}
		}
		byte[][] lowBits = result.getLowBits();
		if (lowBits != null)
		{
			for (int i = 0; i < Math.min(pic.getData().Length, result.getData().Length); i++)
			{
				for (int k = 0; k < Math.min(pic.getData()[i].Length, result.getData()[i].Length); k++)
				{
					int val = pic.getData()[i][k];
					int round = MathUtil.clip(val + lowBitsRound >> lowBitsNum, 0, 255);
					lowBits[i][k] = (byte)(sbyte)(val - (round << 2));
				}
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 73, 66, 159, 8 })]
	public virtual PictureHiBD toPictureHiBD()
	{
		PictureHiBD create = PictureHiBD.doCreate(width, height, color, lowBitsNum + 8, crop);
		PictureHiBD result = toPictureHiBDInternal(create);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 130, 159, 9 })]
	public virtual PictureHiBD toPictureHiBDWithBuffer(int[][] buffer)
	{
		PictureHiBD create = new PictureHiBD(width, height, buffer, color, lowBitsNum + 8, crop);
		PictureHiBD result = toPictureHiBDInternal(create);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 63, 98, 108, 99, 136, 126, 110, 131, 109,
		107, 3, 167
	})]
	public override bool equals(object obj)
	{
		if (obj == null || !(obj is Picture))
		{
			return false;
		}
		Picture other = (Picture)obj;
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

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(349)]
	public virtual int getStartX()
	{
		int result = ((crop != null) ? crop.getX() : 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(353)]
	public virtual int getStartY()
	{
		int result = ((crop != null) ? crop.getY() : 0);
		
		return result;
	}

	[LineNumberTable(357)]
	public virtual bool isHiBD()
	{
		return lowBits != null;
	}
}
