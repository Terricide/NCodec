using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;

namespace org.jcodec.common.io;

[Implements(new string[] { "org.jcodec.common.io.SeekableByteChannel" })]
public class ByteBufferSeekableByteChannel : java.lang.Object, SeekableByteChannel, ByteChannel, ReadableByteChannel, Channel, Closeable, AutoCloseable, WritableByteChannel
{
	private ByteBuffer backing;

	private bool open;

	private int contentLength;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104, 104, 104 })]
	public ByteBufferSeekableByteChannel(ByteBuffer backing, int contentLength)
	{
		this.backing = backing;
		this.contentLength = contentLength;
		open = true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(27)]
	public static ByteBufferSeekableByteChannel writeToByteBuffer(ByteBuffer buf)
	{
		ByteBufferSeekableByteChannel result = new ByteBufferSeekableByteChannel(buf, 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(31)]
	public static ByteBufferSeekableByteChannel readFromByteBuffer(ByteBuffer buf)
	{
		ByteBufferSeekableByteChannel result = new ByteBufferSeekableByteChannel(buf, buf.remaining());
		
		return result;
	}

	[LineNumberTable(36)]
	public virtual bool isOpen()
	{
		return open;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 98, 104 })]
	public virtual void close()
	{
		open = false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 131, 130, 119, 131, 120, 110, 116, 125 })]
	public virtual int read(ByteBuffer dst)
	{
		if (!backing.hasRemaining() || contentLength <= 0)
		{
			return -1;
		}
		int toRead = java.lang.Math.min(backing.remaining(), dst.remaining());
		toRead = java.lang.Math.min(toRead, contentLength);
		dst.put(NIOUtils.read(backing, toRead));
		contentLength = java.lang.Math.max(contentLength, backing.position());
		return toRead;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 128, 130, 120, 116, 125 })]
	public virtual int write(ByteBuffer src)
	{
		int toWrite = java.lang.Math.min(backing.remaining(), src.remaining());
		backing.put(NIOUtils.read(src, toWrite));
		contentLength = java.lang.Math.max(contentLength, backing.position());
		return toWrite;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(66)]
	public virtual long position()
	{
		return backing.position();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 111, 125 })]
	public virtual SeekableByteChannel setPosition(long newPosition)
	{
		backing.position((int)newPosition);
		contentLength = java.lang.Math.max(contentLength, backing.position());
		return this;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(78)]
	public virtual long size()
	{
		return contentLength;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 122, 162, 105 })]
	public virtual SeekableByteChannel truncate(long size)
	{
		contentLength = (int)size;
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 66, 109, 105, 110 })]
	public virtual ByteBuffer getContents()
	{
		ByteBuffer contents = backing.duplicate();
		contents.position(0);
		contents.limit(contentLength);
		return contents;
	}

	public void Dispose()
	{
		close();
	}
}
