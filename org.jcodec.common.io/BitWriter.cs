using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.common.io;

public class BitWriter : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private ByteBuffer buf;

	private int curInt;

	private int _curBit;

	private int initPos;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 109 })]
	public BitWriter(ByteBuffer buf)
	{
		this.buf = buf;
		initPos = buf.position();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 102, 113, 100, 98, 110, 109, 126,
		111, 107, 109, 104, 170, 109, 116, 109, 112, 136
	})]
	public void writeNBit(int value, int n)
	{
		if (n > 32)
		{
			
			throw new IllegalArgumentException("Max 32 bit to write");
		}
		if (n == 0)
		{
			return;
		}
		value &= (int)(uint.MaxValue >> 32 - n);
		if (32 - _curBit >= n)
		{
			curInt |= value << 32 - _curBit - n;
			_curBit += n;
			if (_curBit == 32)
			{
				putInt(curInt);
				_curBit = 0;
				curInt = 0;
			}
		}
		else
		{
			int secPart = n - (32 - _curBit);
			curInt |= (int)((uint)value >> secPart);
			putInt(curInt);
			curInt = value << 32 - secPart;
			_curBit = secPart;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 108, 103, 119, 15, 199 })]
	public virtual void flush()
	{
		int toWrite = _curBit + 7 >> 3;
		for (int i = 0; i < toWrite; i++)
		{
			buf.put((byte)(sbyte)((uint)curInt >> 24));
			curInt <<= 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 98, 126, 111, 107, 109, 104, 136 })]
	public virtual void write1Bit(int bit)
	{
		curInt |= bit << 32 - _curBit - 1;
		_curBit++;
		if (_curBit == 32)
		{
			putInt(curInt);
			_curBit = 0;
			curInt = 0;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 114, 109, 109, 109 })]
	public virtual BitWriter fork()
	{
		BitWriter fork = new BitWriter(buf.duplicate());
		fork._curBit = _curBit;
		fork.curInt = curInt;
		fork.initPos = initPos;
		return fork;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(87)]
	public virtual int position()
	{
		return (buf.position() - initPos << 3) + _curBit;
	}

	[LineNumberTable(91)]
	public virtual ByteBuffer getBuffer()
	{
		return buf;
	}

	[LineNumberTable(83)]
	public virtual int curBit()
	{
		return _curBit & 7;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 132, 162, 114, 114, 113, 111 })]
	private void putInt(int i)
	{
		buf.put((byte)(sbyte)((uint)i >> 24));
		buf.put((byte)(sbyte)(i >> 16));
		buf.put((byte)(sbyte)(i >> 8));
		buf.put((byte)(sbyte)i);
	}
}
