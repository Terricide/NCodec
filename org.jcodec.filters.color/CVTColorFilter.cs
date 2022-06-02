using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.filters.color;

public class CVTColorFilter : Object
{
	[LineNumberTable(new byte[] { 159, 133, 98, 122, 121 })]
	private static byte blue(int d, int c)
	{
		int blue = 1192 * c + 2064 * d + 512 >> 10;
		blue = ((blue >= 0) ? ((blue <= 1023) ? blue : 1023) : 0);
		return (byte)(sbyte)((uint)(blue >> 2) & 0xFFu);
	}

	[LineNumberTable(new byte[] { 159, 132, 162, 127, 3, 121 })]
	private static byte green(int d, int e, int c)
	{
		int green = 1192 * c - 400 * d - 832 * e + 512 >> 10;
		green = ((green >= 0) ? ((green <= 1023) ? green : 1023) : 0);
		return (byte)(sbyte)((uint)(green >> 2) & 0xFFu);
	}

	[LineNumberTable(new byte[] { 159, 130, 98, 122, 121 })]
	private static byte red(int e, int c)
	{
		int red = 1192 * c + 1636 * e + 512 >> 10;
		red = ((red >= 0) ? ((red <= 1023) ? red : 1023) : 0);
		return (byte)(sbyte)((uint)(red >> 2) & 0xFFu);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public CVTColorFilter()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 66, 110, 110, 142, 108, 110, 111, 108,
		140, 113, 115, 145, 114, 116, 114, 102
	})]
	public virtual void yuv422BitTObgr24(Picture yuv, ByteBuffer rgb32)
	{
		ByteBuffer y = ByteBuffer.wrap(yuv.getPlaneData(0));
		ByteBuffer cb = ByteBuffer.wrap(yuv.getPlaneData(1));
		ByteBuffer cr = ByteBuffer.wrap(yuv.getPlaneData(2));
		while (y.hasRemaining())
		{
			int c1 = (sbyte)y.get() + 112 << 2;
			int c2 = (sbyte)y.get() + 112 << 2;
			int d = (sbyte)cb.get() << 2;
			int e = (sbyte)cr.get() << 2;
			rgb32.put((byte)(sbyte)blue(d, c1));
			rgb32.put((byte)(sbyte)green(d, e, c1));
			rgb32.put((byte)(sbyte)red(e, c1));
			rgb32.put((byte)(sbyte)blue(d, c2));
			rgb32.put((byte)(sbyte)green(d, e, c2));
			rgb32.put((byte)(sbyte)red(e, c2));
		}
	}
}
