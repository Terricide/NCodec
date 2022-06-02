using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.mpeg12;

public class MPEGUtil : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 125, 98, 115 })]
	public static ByteBuffer nextSegment(ByteBuffer buf)
	{
		gotoMarker(buf, 0, 256, 511);
		ByteBuffer result = gotoMarker(buf, 1, 256, 511);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(24)]
	public static ByteBuffer gotoNextMarker(ByteBuffer buf)
	{
		ByteBuffer result = gotoMarker(buf, 0, 256, 511);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 105, 131, 104, 104, 141, 99, 105,
		115, 105, 100, 112, 112, 131, 168
	})]
	public static ByteBuffer gotoMarker(ByteBuffer buf, int n, int mmin, int mmax)
	{
		if (!buf.hasRemaining())
		{
			return null;
		}
		int from = buf.position();
		ByteBuffer result = buf.slice();
		result.order(ByteOrder.BIG_ENDIAN);
		int val = -1;
		while (buf.hasRemaining())
		{
			val = (val << 8) | ((sbyte)buf.get() & 0xFF);
			if (val >= mmin && val <= mmax)
			{
				if (n == 0)
				{
					buf.position(buf.position() - 4);
					result.limit(buf.position() - from);
					break;
				}
				n += -1;
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(12)]
	public MPEGUtil()
	{
	}
}
