using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.codecs.raw;

public class V210Decoder : Object
{
	private int width;

	private int height;

	[LineNumberTable(67)]
	private byte to8Bit(int i)
	{
		return (byte)(sbyte)((i + 2 >> 2) - 128);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 105, 104, 104 })]
	public V210Decoder(int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 114, 104, 121, 123, 156, 108, 105,
		117, 122, 151, 105, 119, 116, 154, 105, 116, 122,
		152, 105, 119, 116, 123, 134
	})]
	public virtual Picture decode(byte[] data)
	{
		ByteBuffer littleEndian = ByteBuffer.wrap(data).order(ByteOrder.LITTLE_ENDIAN);
		IntBuffer dat = littleEndian.asIntBuffer();
		ByteBuffer y = ByteBuffer.wrap(new byte[width * height]);
		ByteBuffer cb = ByteBuffer.wrap(new byte[width * height / 2]);
		ByteBuffer cr = ByteBuffer.wrap(new byte[width * height / 2]);
		while (dat.hasRemaining())
		{
			int i = dat.get();
			cr.put((byte)(sbyte)to8Bit(i >> 20));
			y.put((byte)(sbyte)to8Bit((i >> 10) & 0x3FF));
			cb.put((byte)(sbyte)to8Bit(i & 0x3FF));
			i = dat.get();
			y.put((byte)(sbyte)to8Bit(i & 0x3FF));
			y.put((byte)(sbyte)to8Bit(i >> 20));
			cb.put((byte)(sbyte)to8Bit((i >> 10) & 0x3FF));
			i = dat.get();
			cb.put((byte)(sbyte)to8Bit(i >> 20));
			y.put((byte)(sbyte)to8Bit((i >> 10) & 0x3FF));
			cr.put((byte)(sbyte)to8Bit(i & 0x3FF));
			i = dat.get();
			y.put((byte)(sbyte)to8Bit(i & 0x3FF));
			y.put((byte)(sbyte)to8Bit(i >> 20));
			cr.put((byte)(sbyte)to8Bit((i >> 10) & 0x3FF));
		}
		Picture result = Picture.createPicture(width, height, new byte[3][]
		{
			y.array(),
			cb.array(),
			cr.array()
		}, ColorSpace.___003C_003EYUV422);
		
		return result;
	}
}
