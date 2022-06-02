using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.codecs.raw;

public class RAWVideoDecoder : VideoDecoder
{
	private int width;

	private int height;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 104 })]
	public RAWVideoDecoder(int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 111, 56, 167 })]
	internal virtual void copy(ByteBuffer b, byte[] ii, int size)
	{
		int i = 0;
		while (b.hasRemaining() && i < size)
		{
			ii[i] = (byte)(sbyte)(((sbyte)b.get() & 0xFF) - 128);
			i++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 153, 104, 124, 126, 158 })]
	public override Picture decodeFrame(ByteBuffer data, byte[][] buffer)
	{
		Picture create = Picture.createPicture(width, height, buffer, ColorSpace.___003C_003EYUV420);
		ByteBuffer pix = data.duplicate();
		copy(pix, create.getPlaneData(0), width * height);
		copy(pix, create.getPlaneData(1), width * height / 4);
		copy(pix, create.getPlaneData(2), width * height / 4);
		return create;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(47)]
	public override VideoCodecMeta getCodecMeta(ByteBuffer data)
	{
		VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(width, height), ColorSpace.___003C_003EYUV420);
		
		return result;
	}
}
