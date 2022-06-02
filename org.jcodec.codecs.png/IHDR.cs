using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.codecs.png;

internal class IHDR : Object
{
	internal const int PNG_COLOR_MASK_ALPHA = 4;

	internal const int PNG_COLOR_MASK_COLOR = 2;

	internal const int PNG_COLOR_MASK_PALETTE = 1;

	internal int width;

	internal int height;

	internal byte bitDepth;

	internal byte colorType;

	private byte compressionType;

	private byte filterType;

	internal byte interlaceType;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(55)]
	internal virtual int getBitsPerPixel()
	{
		return (sbyte)bitDepth * getNBChannels();
	}

	[LineNumberTable(new byte[] { 159, 131, 130, 99, 109, 99, 108, 101 })]
	private int getNBChannels()
	{
		int channels = 1;
		if (((sbyte)colorType & 3) == 2)
		{
			channels = 3;
		}
		if (((uint)(sbyte)colorType & 4u) != 0)
		{
			channels++;
		}
		return channels;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(7)]
	internal IHDR()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 110, 110, 111, 111, 111, 111, 111 })]
	internal virtual void write(ByteBuffer data)
	{
		data.putInt(width);
		data.putInt(height);
		data.put((byte)(sbyte)bitDepth);
		data.put((byte)(sbyte)colorType);
		data.put((byte)(sbyte)compressionType);
		data.put((byte)(sbyte)filterType);
		data.put((byte)(sbyte)interlaceType);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 109, 109, 110, 110, 110, 110, 110,
		104
	})]
	internal virtual void parse(ByteBuffer data)
	{
		width = data.getInt();
		height = data.getInt();
		bitDepth = (byte)(sbyte)data.get();
		colorType = (byte)(sbyte)data.get();
		compressionType = (byte)(sbyte)data.get();
		filterType = (byte)(sbyte)data.get();
		interlaceType = (byte)(sbyte)data.get();
		data.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	internal virtual int rowSize()
	{
		return width * getBitsPerPixel() + 7 >> 3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(59)]
	internal virtual ColorSpace colorSpace()
	{
		return ColorSpace.___003C_003ERGB;
	}
}
