using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.audio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.codecs.wav;

[Implements(new string[] { "java.io.Closeable" })]
public class WavOutput : java.lang.Object, Closeable, AutoCloseable
{
	[Implements(new string[] { "org.jcodec.audio.AudioSink", "java.io.Closeable" })]
	public class Sink : java.lang.Object, AudioSink, Closeable, AutoCloseable
	{
		private WavOutput @out;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 66, 105, 104 })]
		public Sink(WavOutput @out)
		{
			this.@out = @out;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 124, 98, 125, 115, 104, 111 })]
		public virtual void writeFloat(FloatBuffer data)
		{
			ByteBuffer buf = ByteBuffer.allocate(@out.format.samplesToBytes(data.remaining()));
			AudioUtil.fromFloat(data, @out.format, buf);
			buf.flip();
			@out.write(buf);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 122, 98, 139, 120, 117, 104, 111 })]
		public virtual void write(int[] data, int len)
		{
			len = java.lang.Math.min(data.Length, len);
			ByteBuffer buf = ByteBuffer.allocate(@out.format.samplesToBytes(len));
			AudioUtil.fromInt(data, len, @out.format, buf);
			buf.flip();
			@out.write(buf);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 120, 130, 110 })]
		public virtual void close()
		{
			@out.close();
		}

		public void Dispose()
		{
			close();
		}
	}

	public class WavOutFile : WavOutput
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 129, 66, 112 })]
		public WavOutFile(File f, AudioFormat format)
			: base(NIOUtils.writableChannel(f), format)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 128, 98, 103, 110 })]
		public override void close()
		{
			base.close();
			NIOUtils.closeQuietly(@out);
		}
	}

	protected internal SeekableByteChannel @out;

	protected internal WavHeader header;

	protected internal int written;

	protected internal AudioFormat format;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 98, 122 })]
	public virtual void write(ByteBuffer samples)
	{
		written += @out.write(samples);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 98, 111, 127, 9, 110 })]
	public virtual void close()
	{
		@out.setPosition(0L);
		WavHeader.createWavHeader(format, format.bytesToFrames(written)).write(@out);
		NIOUtils.closeQuietly(@out);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 98, 105, 104, 104, 110, 111 })]
	public WavOutput(SeekableByteChannel @out, AudioFormat format)
	{
		this.@out = @out;
		this.format = format;
		header = WavHeader.createWavHeader(format, 0);
		header.write(@out);
	}

	public void Dispose()
	{
		close();
	}
}
