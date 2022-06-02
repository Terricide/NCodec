using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.common.io;

public class BitReader : Object
{
	private int deficit;

	private int curInt;

	private ByteBuffer bb;

	private int initPos;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 104, 109, 104 })]
	public static BitReader createBitReader(ByteBuffer bb)
	{
		BitReader r = new BitReader(bb);
		r.curInt = r.readInt();
		r.deficit = 0;
		return r;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(154)]
	public virtual int align()
	{
		return ((deficit & 7) > 0) ? skip(8 - (deficit & 7)) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(233)]
	public virtual int position()
	{
		return (bb.position() - initPos - 4 << 3) + deficit;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(115)]
	public virtual int remaining()
	{
		return (bb.remaining() << 3) + 32 - deficit;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 130, 102, 145, 131, 99, 109, 116, 110,
		104, 105, 173, 100, 114, 114, 239, 71
	})]
	public virtual int readNBit(int n)
	{
		if (n > 32)
		{
			
			throw new IllegalArgumentException("Can not read more then 32 bit");
		}
		int nn = n;
		int ret = 0;
		if (n + deficit > 31)
		{
			ret |= (int)((uint)curInt >> deficit);
			n -= 32 - deficit;
			ret <<= n;
			deficit = 32;
			curInt = readInt();
		}
		if (n != 0)
		{
			ret |= (int)((uint)curInt >> 32 - n);
			curInt <<= n;
			deficit += n;
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 98, 107, 111, 111, 107, 205 })]
	public virtual int read1Bit()
	{
		int ret = (int)((uint)curInt >> 31);
		curInt <<= 1;
		deficit++;
		if (deficit == 32)
		{
			curInt = readInt();
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 162, 102, 177 })]
	public virtual int checkNBit(int n)
	{
		if (n > 24)
		{
			
			throw new IllegalArgumentException("Can not check more then 24 bit");
		}
		int result = checkNBitDontCare(n);
		
		return result;
	}

	[LineNumberTable(220)]
	public virtual int curBit()
	{
		return deficit & 7;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 131, 112, 109, 105, 102, 117, 122,
		135, 173, 111, 146
	})]
	public virtual int skip(int bits)
	{
		int left = bits;
		if (left + deficit > 31)
		{
			left -= 32 - deficit;
			deficit = 32;
			if (left > 31)
			{
				int skip = Math.min(left >> 3, bb.remaining());
				bb.position(bb.position() + skip);
				left -= skip << 3;
			}
			curInt = readInt();
		}
		deficit += left;
		curInt <<= left;
		return bits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 98, 127, 5 })]
	public virtual void stop()
	{
		bb.position(bb.position() - (32 - deficit >> 3));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 130, 122 })]
	public virtual bool moreData()
	{
		int remaining = bb.remaining() + 4 - (deficit + 7 >> 3);
		return (remaining > 1 || (remaining == 1 && curInt != 0)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 85, 66, 109, 122 })]
	public virtual void terminate()
	{
		int putBack = 32 - deficit >> 3;
		bb.position(bb.position() - putBack);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(249)]
	public virtual bool readBool()
	{
		return read1Bit() == 1;
	}

	[LineNumberTable(150)]
	public virtual int bitsToAlign()
	{
		return ((deficit & 7) > 0) ? (8 - (deficit & 7)) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 162, 109, 111, 159, 1, 112 })]
	public virtual int checkNBitDontCare(int n)
	{
		while (deficit + n > 32)
		{
			deficit -= 8;
			curInt |= nextIgnore() << deficit;
		}
		return (int)((uint)curInt >> 32 - n);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 66, 107, 112, 158 })]
	public virtual int check16Bits()
	{
		if (deficit > 16)
		{
			deficit -= 16;
			curInt |= nextIgnore16() << deficit;
		}
		return (int)((uint)curInt >> 16);
	}

	[LineNumberTable(new byte[] { 159, 107, 162, 111, 146 })]
	public virtual int skipFast(int bits)
	{
		deficit += bits;
		curInt <<= bits;
		return bits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 97, 66, 100, 99, 107, 112, 190, 112, 111,
		146
	})]
	public virtual int readFast16(int n)
	{
		if (n == 0)
		{
			return 0;
		}
		if (deficit > 16)
		{
			deficit -= 16;
			curInt |= nextIgnore16() << deficit;
		}
		int ret = (int)((uint)curInt >> 32 - n);
		deficit += n;
		curInt <<= n;
		return ret;
	}

	[LineNumberTable(245)]
	public virtual int checkAllBits()
	{
		return curInt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 98, 105 })]
	public virtual int readNBitSigned(int n)
	{
		int v = readNBit(n);
		return (read1Bit() != 0) ? (-v) : v;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 233, 59, 104, 232, 69, 104, 109 })]
	private BitReader(ByteBuffer bb)
	{
		deficit = -1;
		curInt = -1;
		this.bb = bb;
		initPos = bb.position();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 111, 112, 159, 54 })]
	public int readInt()
	{
		if (bb.remaining() >= 4)
		{
			deficit -= 32;
			return (((sbyte)bb.get() & 0xFF) << 24) | (((sbyte)bb.get() & 0xFF) << 16) | (((sbyte)bb.get() & 0xFF) << 8) | ((sbyte)bb.get() & 0xFF);
		}
		int result = readIntSafe();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 123, 99, 110, 118, 101, 110, 118,
		101, 110, 118, 101, 110, 118
	})]
	private int readIntSafe()
	{
		deficit -= bb.remaining() << 3;
		int res = 0;
		if (bb.hasRemaining())
		{
			res |= (sbyte)bb.get() & 0xFF;
		}
		res <<= 8;
		if (bb.hasRemaining())
		{
			res |= (sbyte)bb.get() & 0xFF;
		}
		res <<= 8;
		if (bb.hasRemaining())
		{
			res |= (sbyte)bb.get() & 0xFF;
		}
		res <<= 8;
		if (bb.hasRemaining())
		{
			res |= (sbyte)bb.get() & 0xFF;
		}
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(212)]
	private int nextIgnore16()
	{
		return (bb.remaining() > 1) ? (bb.getShort() & 0xFFFF) : (bb.hasRemaining() ? (((sbyte)bb.get() & 0xFF) << 8) : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(216)]
	private int nextIgnore()
	{
		return bb.hasRemaining() ? ((sbyte)bb.get() & 0xFF) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 114, 104, 109, 109 })]
	public virtual BitReader fork()
	{
		BitReader fork = new BitReader(bb.duplicate());
		fork.initPos = 0;
		fork.curInt = curInt;
		fork.deficit = deficit;
		return fork;
	}

	[LineNumberTable(119)]
	public bool isByteAligned()
	{
		return ((deficit & 7) == 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 130, 107, 112, 190, 106, 111, 190 })]
	public virtual int check24Bits()
	{
		if (deficit > 16)
		{
			deficit -= 16;
			curInt |= nextIgnore16() << deficit;
		}
		if (deficit > 8)
		{
			deficit -= 8;
			curInt |= nextIgnore() << deficit;
		}
		return (int)((uint)curInt >> 8);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(224)]
	public virtual bool lastByte()
	{
		return bb.remaining() + 4 - (deficit >> 3) <= 1;
	}
}
