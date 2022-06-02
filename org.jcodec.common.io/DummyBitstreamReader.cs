using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace org.jcodec.common.io;

public class DummyBitstreamReader : Object
{
	private InputStream @is;

	private int curByte;

	private int nextByte;

	private int secondByte;

	internal int nBit;

	protected internal static int bitsRead;

	internal int cnt;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 131, 66, 106, 103, 106, 163, 118, 175, 141 })]
	public virtual int read1BitInt()
	{
		if (nBit == 8)
		{
			advance();
			if (curByte == -1)
			{
				return -1;
			}
		}
		int res = (curByte >> 7 - nBit) & 1;
		nBit++;
		bitsRead++;
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 121, 98, 103, 136 })]
	private void advance()
	{
		advance1();
		nBit = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 123, 162, 109, 109, 114 })]
	private void advance1()
	{
		curByte = nextByte;
		nextByte = secondByte;
		secondByte = @is.read();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 102, 145, 131, 103, 101, 10, 231,
		69
	})]
	public virtual int readNBit(int n)
	{
		if (n > 32)
		{
			
			throw new IllegalArgumentException("Can not read more then 32 bit");
		}
		int val = 0;
		for (int i = 0; i < n; i++)
		{
			val <<= 1;
			val |= read1BitInt();
		}
		return val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 91, 98, 111, 104, 115, 103, 145 })]
	public int skip(int bits)
	{
		nBit += bits;
		int was = nBit;
		while (nBit >= 8 && curByte != -1)
		{
			advance1();
			nBit -= 8;
		}
		return was - nBit;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 104, 130, 101, 113, 106, 103, 106, 163, 144,
		99, 108, 55, 199, 103, 55, 199, 100, 105, 103,
		10, 233, 69
	})]
	public virtual int peakNextBits(int n)
	{
		if (n > 8)
		{
			
			throw new IllegalArgumentException("N should be less then 8");
		}
		if (nBit == 8)
		{
			advance();
			if (curByte == -1)
			{
				return -1;
			}
		}
		int[] bits = new int[16 - nBit];
		int cnt = 0;
		for (int k = nBit; k < 8; k++)
		{
			int num = cnt;
			cnt++;
			bits[num] = (curByte >> 7 - k) & 1;
		}
		for (int j = 0; j < 8; j++)
		{
			int num2 = cnt;
			cnt++;
			bits[num2] = (nextByte >> 7 - j) & 1;
		}
		int result = 0;
		for (int i = 0; i < n; i++)
		{
			result <<= 1;
			result |= bits[i];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 233, 72, 232, 57, 104, 109, 109,
		141
	})]
	public DummyBitstreamReader(InputStream @is)
	{
		cnt = 0;
		this.@is = @is;
		curByte = @is.read();
		nextByte = @is.read();
		secondByte = @is.read();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(39)]
	public virtual int read1Bit()
	{
		int result = read1BitInt();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 118, 66, 106, 167, 136, 135 })]
	public virtual int readByte()
	{
		if (nBit > 0)
		{
			advance();
		}
		int res = curByte;
		advance();
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 114, 98, 106, 135, 113, 103, 145 })]
	public virtual bool moreRBSPData()
	{
		if (nBit == 8)
		{
			advance();
		}
		int tail = 1 << 8 - nBit - 1;
		int mask = (tail << 1) - 1;
		int hasTail = (((curByte & mask) == tail) ? 1 : 0);
		return (curByte != -1 && (nextByte != -1 || hasTail == 0)) ? true : false;
	}

	[LineNumberTable(124)]
	public virtual long getBitPosition()
	{
		int num = bitsRead * 8;
		int num2 = nBit;
		return num + ((8 != -1) ? (num2 % 8) : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 110, 66, 106, 103, 106, 99, 123, 113, 144 })]
	public virtual bool moreData()
	{
		if (nBit == 8)
		{
			advance();
		}
		if (curByte == -1)
		{
			return false;
		}
		if (nextByte == -1 || (nextByte == 0 && secondByte == -1))
		{
			int mask = (1 << 8 - nBit) - 1;
			return ((curByte & mask) != 0) ? true : false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(145)]
	public virtual long readRemainingByte()
	{
		return readNBit(8 - nBit);
	}

	[LineNumberTable(188)]
	public virtual bool isByteAligned()
	{
		int num = nBit;
		return (8 == -1 || num % 8 == 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 93, 98, 110 })]
	public virtual void close()
	{
		@is.close();
	}

	[LineNumberTable(201)]
	public virtual int getCurBit()
	{
		return nBit;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 89, 162, 108, 114 })]
	public virtual int align()
	{
		int i = (8 - nBit) & 7;
		skip((8 - nBit) & 7);
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(221)]
	public virtual int checkNBit(int n)
	{
		int result = peakNextBits(n);
		
		return result;
	}

	[LineNumberTable(225)]
	public virtual int curBit()
	{
		return nBit;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(229)]
	public virtual bool lastByte()
	{
		return (nextByte == -1 && secondByte == -1) ? true : false;
	}
}
