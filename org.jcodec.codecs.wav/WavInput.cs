using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using org.jcodec.audio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.codecs.wav;

[Implements(new string[] { "java.io.Closeable" })]
public class WavInput : java.lang.Object, Closeable, AutoCloseable
{
	[Implements(new string[] { "org.jcodec.audio.AudioSource", "java.io.Closeable" })]
	public class Source : java.lang.Object, AudioSource, Closeable, AutoCloseable
	{
		private WavInput src;

		private AudioFormat format;

		private int pos;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 123, 98, 105, 104, 109 })]
		public Source(WavInput src)
		{
			this.src = src;
			format = src.getFormat();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(83)]
		public virtual AudioFormat getFormat()
		{
			AudioFormat result = src.getFormat();
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 121, 162, 110 })]
		public virtual void close()
		{
			src.close();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 119, 66, 139, 115, 110, 104, 111 })]
		public virtual int read(int[] samples, int max)
		{
			max = java.lang.Math.min(max, samples.Length);
			ByteBuffer bb = ByteBuffer.allocate(format.samplesToBytes(max));
			int read = src.read(bb);
			bb.flip();
			AudioUtil.toInt(format, bb, samples);
			int result = format.bytesToFrames(read);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 117, 130, 120, 110, 101, 99, 104, 110, 110,
			143
		})]
		public virtual int readFloat(FloatBuffer samples)
		{
			ByteBuffer bb = ByteBuffer.allocate(format.samplesToBytes(samples.remaining()));
			int i = src.read(bb);
			if (i == -1)
			{
				return -1;
			}
			bb.flip();
			AudioUtil.toFloat(format, bb, samples);
			int read = format.bytesToFrames(i);
			pos += read;
			return read;
		}

		public void Dispose()
		{
			close();
		}
	}

	public class WavFile : WavInput
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 128, 130, 111 })]
		public WavFile(File f)
			: base(NIOUtils.readableChannel(f))
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 127, 162, 103, 110 })]
		public override void close()
		{
			base.close();
			_in.close();
		}
	}

	protected internal WavHeader header;

	protected internal byte[] prevBuf;

	protected internal ReadableByteChannel _in;

	protected internal AudioFormat format;

	[LineNumberTable(49)]
	public virtual AudioFormat getFormat()
	{
		return format;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 98, 110 })]
	public virtual void close()
	{
		_in.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 66, 126 })]
	public virtual int read(ByteBuffer buf)
	{
		int maxRead = format.framesToBytes(format.bytesToFrames(buf.remaining()));
		int result = NIOUtils.readL(_in, buf, maxRead);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 98, 105, 109, 114, 104 })]
	public WavInput(ReadableByteChannel _in)
	{
		header = WavHeader.readChannel(_in);
		format = header.getFormat();
		this._in = _in;
	}

	[LineNumberTable(45)]
	public virtual WavHeader getHeader()
	{
		return header;
	}

	public void Dispose()
	{
		close();
	}
}
