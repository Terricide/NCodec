using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;

namespace org.jcodec.common.io;

[Implements(new string[] { "java.io.Closeable" })]
public class DataReader : java.lang.Object, Closeable, AutoCloseable
{
	private const int DEFAULT_BUFFER_SIZE = 1048576;

	private SeekableByteChannel channel;

	private ByteBuffer buffer;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 105, 104, 109, 110, 110 })]
	public DataReader(SeekableByteChannel channel, ByteOrder order, int bufferSize)
	{
		this.channel = channel;
		buffer = ByteBuffer.allocate(bufferSize);
		buffer.limit(0);
		buffer.order(order);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 113, 66, 111, 108, 115, 141 })]
	private void fetchIfNeeded(int length)
	{
		if (buffer.remaining() < length)
		{
			moveRemainderToTheStart(buffer);
			channel.read(buffer);
			buffer.flip();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(96)]
	public virtual long position()
	{
		return channel.position() - buffer.limit() + buffer.position();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 117, 66, 125, 115, 144, 110, 142 })]
	public virtual long setPosition(long newPos)
	{
		int relative = (int)(newPos - (channel.position() - buffer.limit()));
		if (relative >= 0 && relative < buffer.limit())
		{
			buffer.position(relative);
		}
		else
		{
			buffer.limit(0);
			channel.setPosition(newPos);
		}
		long result = position();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 66, 104, 103, 48, 167, 104, 105 })]
	private static void moveRemainderToTheStart(ByteBuffer readBuf)
	{
		int rem = readBuf.remaining();
		for (int i = 0; i < rem; i++)
		{
			readBuf.put(i, (byte)(sbyte)readBuf.get());
		}
		readBuf.clear();
		readBuf.position(rem);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 99, 101, 136, 110, 131, 115, 112,
		102, 102, 99
	})]
	public virtual int readFully3(byte[] b, int off, int len)
	{
		int initOff = off;
		while (len > 0)
		{
			fetchIfNeeded(len);
			if (buffer.remaining() == 0)
			{
				break;
			}
			int toRead = java.lang.Math.min(buffer.remaining(), len);
			buffer.get(b, off, toRead);
			off += toRead;
			len -= toRead;
		}
		return off - initOff;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public static DataReader createDataReader(SeekableByteChannel channel, ByteOrder order)
	{
		DataReader result = new DataReader(channel, order, 1048576);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 130, 162, 104, 111, 156, 140 })]
	public virtual int skipBytes(int n)
	{
		long oldPosition = position();
		if (n < buffer.remaining())
		{
			buffer.position(buffer.position() + n);
		}
		else
		{
			setPosition(oldPosition + n);
		}
		return (int)(position() - oldPosition);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 127, 98, 104 })]
	public virtual byte readByte()
	{
		fetchIfNeeded(1);
		sbyte result = (sbyte)buffer.get();
		
		return (byte)result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 126, 130, 104 })]
	public virtual short readShort()
	{
		fetchIfNeeded(2);
		short @short = buffer.getShort();
		
		return @short;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 104 })]
	public virtual char readChar()
	{
		fetchIfNeeded(2);
		char @char = buffer.getChar();
		
		return @char;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 123, 66, 104 })]
	public virtual int readInt()
	{
		fetchIfNeeded(4);
		int @int = buffer.getInt();
		
		return @int;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 122, 98, 104 })]
	public virtual long readLong()
	{
		fetchIfNeeded(8);
		long @long = buffer.getLong();
		
		return @long;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 121, 130, 104 })]
	public virtual float readFloat()
	{
		fetchIfNeeded(4);
		float @float = buffer.getFloat();
		
		return @float;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 120, 162, 104 })]
	public virtual double readDouble()
	{
		fetchIfNeeded(8);
		double @double = buffer.getDouble();
		
		return @double;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 114, 66, 110 })]
	public virtual void close()
	{
		channel.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(133)]
	public virtual long size()
	{
		long result = channel.size();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(137)]
	public virtual int readFully(byte[] b)
	{
		int result = readFully3(b, 0, b.Length);
		
		return result;
	}

	public void Dispose()
	{
		close();
	}
}
