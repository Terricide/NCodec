using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.common;

public abstract class VideoEncoder : Object
{
	public class EncodedFrame : Object
	{
		private ByteBuffer data;

		private bool keyFrame;

		[LineNumberTable(32)]
		public virtual ByteBuffer getData()
		{
			return data;
		}

		[LineNumberTable(35)]
		public virtual bool isKeyFrame()
		{
			return keyFrame;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 129, 67, 105, 104, 104 })]
		public EncodedFrame(ByteBuffer data, bool keyFrame)
		{
			this.data = data;
			this.keyFrame = keyFrame;
		}
	}

	public abstract EncodedFrame encodeFrame(Picture p, ByteBuffer bb);

	public abstract int estimateBufferSize(Picture p);

	public abstract ColorSpace[] getSupportedColorSpaces();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public VideoEncoder()
	{
	}
}
