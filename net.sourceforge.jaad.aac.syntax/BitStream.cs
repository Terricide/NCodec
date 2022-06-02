using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace net.sourceforge.jaad.aac.syntax;

public class BitStream : Object, IBitStream
{
	private const int WORD_BITS = 32;

	private const int WORD_BYTES = 4;

	private const int BYTE_MASK = 255;

	private byte[] buffer;

	private int pos;

	private int cache;

	protected internal int bitsCached;

	protected internal int position;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 103, 104 })]
	public static BitStream createBitStream(byte[] data)
	{
		BitStream bs = new BitStream();
		bs.setData(data);
		return bs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 105 })]
	public BitStream()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 140, 127, 0, 113, 105 })]
	public void setData(byte[] data)
	{
		int size = (int)(4 * (((nint)data.LongLength + 4 - 1) / 4));
		if (buffer == null || (nint)buffer.LongLength != size)
		{
			buffer = new byte[size];
		}
		ByteCodeHelper.arraycopy_primitive_1(data, 0, buffer, 0, data.Length);
		reset();
	}

	[LineNumberTable(new byte[] { 159, 125, 130, 104, 104, 104, 104 })]
	public void reset()
	{
		pos = 0;
		bitsCached = 0;
		cache = 0;
		position = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 93, 98, 111, 106, 177, 107, 102, 103, 139,
		101, 110, 173, 104, 168
	})]
	public virtual void skipBits(int n)
	{
		position += n;
		if (n <= bitsCached)
		{
			bitsCached -= n;
			return;
		}
		n -= bitsCached;
		while (n >= 32)
		{
			n += -32;
			readCache(peek: false);
		}
		if (n > 0)
		{
			cache = readCache(peek: false);
			bitsCached = 32 - n;
		}
		else
		{
			cache = 0;
			bitsCached = 0;
		}
	}

	[LineNumberTable(new byte[] { 159, 83, 162, 106, 106 })]
	public virtual int maskBits(int n)
	{
		if (n == 32)
		{
			return -1;
		}
		return (1 << n) - 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 118, 129, 67, 125, 223, 40, 114 })]
	protected internal virtual int readCache(bool peek)
	{
		if (pos > (nint)buffer.LongLength - 4)
		{
			throw Throwable.___003Cunmap_003E(AACException.endOfStream());
		}
		int i = (buffer[pos] << 24) | (buffer[pos + 1] << 16) | (buffer[pos + 2] << 8) | buffer[pos + 3];
		if (!peek)
		{
			pos += 4;
		}
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 109, 162, 106, 111, 116, 177, 110, 105, 111,
		148
	})]
	public virtual int readBit()
	{
		int i;
		if (bitsCached > 0)
		{
			bitsCached--;
			i = (cache >> bitsCached) & 1;
			position++;
		}
		else
		{
			cache = readCache(peek: false);
			bitsCached = 31;
			position++;
			i = (cache >> bitsCached) & 1;
		}
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 103, 104 })]
	public virtual void destroy()
	{
		reset();
		buffer = null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 127, 98, 106, 110 })]
	public virtual void byteAlign()
	{
		int toFlush = bitsCached & 7;
		if (toFlush > 0)
		{
			skipBits(toFlush);
		}
	}

	[LineNumberTable(81)]
	public virtual int getPosition()
	{
		return position;
	}

	[LineNumberTable(89)]
	public virtual int getBitsLeft()
	{
		return (int)((buffer == null) ? 0 : (8 * ((nint)buffer.LongLength - pos) + bitsCached));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 114, 98, 106, 111, 122, 180, 111, 117, 106,
		110, 107, 159, 2
	})]
	public virtual int readBits(int n)
	{
		int result;
		if (bitsCached >= n)
		{
			bitsCached -= n;
			result = (cache >> bitsCached) & maskBits(n);
			position += n;
		}
		else
		{
			position += n;
			int c = cache & maskBits(bitsCached);
			int left = n - bitsCached;
			cache = readCache(peek: false);
			bitsCached = 32 - left;
			result = ((cache >> bitsCached) & maskBits(left)) | (c << left);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(154)]
	public virtual bool readBool()
	{
		return (((uint)readBit() & (true ? 1u : 0u)) != 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 102, 162, 106, 222, 117, 139, 159, 1 })]
	public virtual int peekBits(int n)
	{
		if (bitsCached >= n)
		{
			return (cache >> bitsCached - n) & maskBits(n);
		}
		int c = cache & maskBits(bitsCached);
		n -= bitsCached;
		return ((readCache(peek: true) >> 32 - n) & maskBits(n)) | (c << n);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 97, 130, 106, 184, 105, 136 })]
	public virtual int peekBit()
	{
		if (bitsCached > 0)
		{
			return (cache >> bitsCached - 1) & 1;
		}
		int word = readCache(peek: true);
		return (word >> 31) & 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 87, 162, 111, 106, 177, 110, 137 })]
	public virtual void skipBit()
	{
		position++;
		if (bitsCached > 0)
		{
			bitsCached--;
			return;
		}
		cache = readCache(peek: false);
		bitsCached = 31;
	}
}
