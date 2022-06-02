using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.common;

public abstract class VideoDecoder : Object
{
	private byte[][] byteBuffer;

	public abstract Picture decodeFrame(ByteBuffer bb, byte[][] barr);

	public abstract VideoCodecMeta getCodecMeta(ByteBuffer bb);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public VideoDecoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 127, 4, 114 })]
	protected internal virtual byte[][] getSameSizeBuffer(int[][] buffer)
	{
		if (byteBuffer == null || (nint)byteBuffer.LongLength != (nint)buffer.LongLength || (nint)byteBuffer[0].LongLength != (nint)buffer[0].LongLength)
		{
			byteBuffer = ArrayUtil.create2D(buffer[0].Length, buffer.Length);
		}
		return byteBuffer;
	}

	[LineNumberTable(new byte[] { 159, 132, 130, 101, 99 })]
	public virtual VideoDecoder downscaled(int ratio)
	{
		if (ratio == 1)
		{
			return this;
		}
		return null;
	}
}
