using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;

namespace org.jcodec.common.io;

[Implements(new string[] { "org.jcodec.common.io.SeekableByteChannel" })]
public class FileChannelWrapper : java.lang.Object, SeekableByteChannel, ByteChannel, ReadableByteChannel, Channel, Closeable, AutoCloseable, WritableByteChannel
{
	private FileChannel ch;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(40)]
	public virtual int write(ByteBuffer arg0)
	{
		int result = ch.write(arg0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104 })]
	public FileChannelWrapper(FileChannel ch)
	{
		this.ch = ch;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(25)]
	public virtual int read(ByteBuffer arg0)
	{
		int result = ch.read(arg0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 130, 110 })]
	public virtual void close()
	{
		ch.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public virtual bool isOpen()
	{
		bool result = ch.isOpen();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(45)]
	public virtual long position()
	{
		long result = ch.position();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 130, 130, 110 })]
	public virtual SeekableByteChannel setPosition(long newPosition)
	{
		ch.position(newPosition);
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(56)]
	public virtual long size()
	{
		long result = ch.size();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 127, 98, 110 })]
	public virtual SeekableByteChannel truncate(long size)
	{
		ch.truncate(size);
		return this;
	}

	public void Dispose()
	{
		close();
	}
}
