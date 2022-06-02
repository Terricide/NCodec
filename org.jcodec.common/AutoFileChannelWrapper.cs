using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using org.jcodec.common.io;

namespace org.jcodec.common;

[Implements(new string[] { "org.jcodec.common.io.SeekableByteChannel", "org.jcodec.common.io.AutoResource" })]
public class AutoFileChannelWrapper : java.lang.Object, org.jcodec.common.io.SeekableByteChannel, ByteChannel, ReadableByteChannel, Channel, Closeable, AutoCloseable, WritableByteChannel, AutoResource
{
	private const long THRESHOLD = 5000L;

	private FileChannel ch;

	private File file;

	private long savedPos;

	private long curTime;

	private long accessTime;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 66, 109, 118, 124, 147 })]
	private void ensureOpen()
	{
		accessTime = curTime;
		if (ch == null || !ch.isOpen())
		{			
			ch = new FileInputStream(file).getChannel();
			ch.position(savedPos);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 128, 98, 118, 114, 108, 136 })]
	public virtual void close()
	{
		if (ch != null && ch.isOpen())
		{
			savedPos = ch.position();
			ch.close();
			ch = null;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 134, 66, 105, 104, 108, 108, 105 })]
	public AutoFileChannelWrapper(File file)
	{
		this.file = file;
		curTime = java.lang.System.currentTimeMillis();
		AutoPool.getInstance().add(this);
		ensureOpen();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 130, 98, 103, 110, 114 })]
	public virtual int read(ByteBuffer arg0)
	{
		ensureOpen();
		int r = ch.read(arg0);
		savedPos = ch.position();
		return r;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(66)]
	public virtual bool isOpen()
	{
		return (ch != null && ch.isOpen()) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 103, 110, 114 })]
	public virtual int write(ByteBuffer arg0)
	{
		ensureOpen();
		int w = ch.write(arg0);
		savedPos = ch.position();
		return w;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 123, 162, 103 })]
	public virtual long position()
	{
		ensureOpen();
		long result = ch.position();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 121, 98, 103, 110, 104 })]
	public virtual org.jcodec.common.io.SeekableByteChannel setPosition(long newPosition)
	{
		ensureOpen();
		ch.position(newPosition);
		savedPos = newPosition;
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 119, 98, 103 })]
	public virtual long size()
	{
		ensureOpen();
		long result = ch.size();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 118, 162, 103, 110, 114 })]
	public virtual org.jcodec.common.io.SeekableByteChannel truncate(long size)
	{
		ensureOpen();
		ch.truncate(size);
		savedPos = ch.position();
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 162, 104, 159, 7, 185, 3, 98, 173 })]
	public virtual void setCurTime(long curTime)
	{
		this.curTime = curTime;
		if (ch != null && ch.isOpen() && curTime - accessTime > 5000u)
		{
			IOException ex;
			try
			{
				close();
				return;
			}
			catch (IOException x)
			{
				ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
			}
			IOException e = ex;
			
			throw new RuntimeException(e);
		}
	}

	public void Dispose()
	{
		close();
	}
}
