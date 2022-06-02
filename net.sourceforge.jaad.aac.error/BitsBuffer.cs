using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.error;

public class BitsBuffer : Object
{
	internal int bufa;

	internal int bufb;

	internal int len;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] S;

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	internal static int[] B;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 135, 98, 102, 171, 127, 8, 190, 127, 38 })]
	public virtual int showBits(int bits)
	{
		if (bits == 0)
		{
			return 0;
		}
		if (len <= 32)
		{
			if (len >= bits)
			{
				return (bufa >> len - bits) & (-1 >> 32 - bits);
			}
			return (bufa << bits - len) & (-1 >> 32 - bits);
		}
		if (len - bits < 32)
		{
			return ((bufb & (-1 >> 64 - len)) << bits - len + 32) | (bufa >> len - bits);
		}
		return (bufb >> len - bits - 32) & (-1 >> 32 - bits);
	}

	[LineNumberTable(new byte[] { 159, 132, 162, 175, 106, 104, 133, 99 })]
	public virtual bool flushBits(int bits)
	{
		len -= bits;
		if (len < 0)
		{
			len = 0;
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 162, 104, 102, 101, 176, 127, 15, 127,
		15, 127, 15, 127, 15, 127, 15, 127, 15, 127,
		15, 127, 15, 127, 15, 191, 15, 119, 141
	})]
	internal static int[] rewindReverse64(int hi, int lo, int len)
	{
		int[] i = new int[2];
		if (len <= 32)
		{
			i[0] = 0;
			i[1] = rewindReverse32(lo, len);
		}
		else
		{
			lo = ((lo >> S[0]) & B[0]) | ((lo << S[0]) & (B[0] ^ -1));
			hi = ((hi >> S[0]) & B[0]) | ((hi << S[0]) & (B[0] ^ -1));
			lo = ((lo >> S[1]) & B[1]) | ((lo << S[1]) & (B[1] ^ -1));
			hi = ((hi >> S[1]) & B[1]) | ((hi << S[1]) & (B[1] ^ -1));
			lo = ((lo >> S[2]) & B[2]) | ((lo << S[2]) & (B[2] ^ -1));
			hi = ((hi >> S[2]) & B[2]) | ((hi << S[2]) & (B[2] ^ -1));
			lo = ((lo >> S[3]) & B[3]) | ((lo << S[3]) & (B[3] ^ -1));
			hi = ((hi >> S[3]) & B[3]) | ((hi << S[3]) & (B[3] ^ -1));
			lo = ((lo >> S[4]) & B[4]) | ((lo << S[4]) & (B[4] ^ -1));
			hi = ((hi >> S[4]) & B[4]) | ((hi << S[4]) & (B[4] ^ -1));
			i[1] = (hi >> 64 - len) | (lo << len - 32);
			i[1] = lo >> 64 - len;
		}
		return i;
	}

	[LineNumberTable(new byte[]
	{
		159, 113, 98, 127, 15, 127, 15, 127, 15, 127,
		15, 191, 15, 140
	})]
	internal static int rewindReverse32(int v, int len)
	{
		v = ((v >> S[0]) & B[0]) | ((v << S[0]) & (B[0] ^ -1));
		v = ((v >> S[1]) & B[1]) | ((v << S[1]) & (B[1] ^ -1));
		v = ((v >> S[2]) & B[2]) | ((v << S[2]) & (B[2] ^ -1));
		v = ((v >> S[3]) & B[3]) | ((v << S[3]) & (B[3] ^ -1));
		v = ((v >> S[4]) & B[4]) | ((v << S[4]) & (B[4] ^ -1));
		v >>= 32 - len;
		return v;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104 })]
	public BitsBuffer()
	{
		len = 0;
	}

	[LineNumberTable(25)]
	public virtual int getLength()
	{
		return len;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 105, 108 })]
	public virtual int getBits(int n)
	{
		int i = showBits(n);
		if (!flushBits(n))
		{
			i = -1;
		}
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 105, 108 })]
	public virtual int getBit()
	{
		int i = showBits(1);
		if (!flushBits(1))
		{
			i = -1;
		}
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 162, 106, 121, 106, 106 })]
	public virtual void rewindReverse()
	{
		if (len != 0)
		{
			int[] i = rewindReverse64(bufb, bufa, len);
			bufb = i[0];
			bufa = i[1];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 124, 162, 106, 104, 168, 139, 104, 153, 112,
		165, 118, 99, 124, 205, 106, 138, 116
	})]
	public virtual void concatBits(BitsBuffer a)
	{
		if (a.len != 0)
		{
			int al = a.bufa;
			int ah = a.bufb;
			int bl;
			int bh;
			if (len > 32)
			{
				bl = bufa;
				bh = bufb & ((1 << len - 32) - 1);
				ah = al << len - 32;
				al = 0;
			}
			else
			{
				bl = bufa & ((1 << len) - 1);
				bh = 0;
				ah = (ah << len) | (al >> 32 - len);
				al <<= len;
			}
			bufa = bl | al;
			bufb = bh | ah;
			len += a.len;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 117, 162, 136, 102, 113, 177, 110, 136 })]
	public virtual void readSegment(int segwidth, IBitStream _in)
	{
		len = segwidth;
		if (segwidth > 32)
		{
			bufb = _in.readBits(segwidth - 32);
			bufa = _in.readBits(32);
		}
		else
		{
			bufa = _in.readBits(segwidth);
			bufb = 0;
		}
	}

	[LineNumberTable(new byte[] { 159, 138, 98, 127, 2 })]
	static BitsBuffer()
	{
		S = new int[5] { 1, 2, 4, 8, 16 };
		B = new int[5] { 1431655765, 858993459, 252645135, 16711935, 65535 };
	}
}
