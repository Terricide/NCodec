using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using org.jcodec.api.transcode;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.scale;

namespace org.jcodec.api;

public class SequenceEncoder : Object
{
	private Transform transform;

	private int frameNo;

	private int timestamp;

	private Rational fps;

	private Sink sink;

	private PixelStore pixelStore;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 98, 105, 136, 114, 140, 110, 156, 108 })]
	public SequenceEncoder(SeekableByteChannel @out, Rational fps, Format outputFormat, Codec outputVideoCodec, Codec outputAudioCodec)
	{
		this.fps = fps;
		sink = SinkImpl.createWithStream(@out, outputFormat, outputVideoCodec, outputAudioCodec);
		sink.init();
		if (sink.getInputColor() != null)
		{
			transform = ColorUtil.getTransform(ColorSpace.___003C_003ERGB, sink.getInputColor());
		}
		pixelStore = new PixelStoreImpl();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(45)]
	public static SequenceEncoder createSequenceEncoder(File @out, int fps)
	{
		SequenceEncoder result = new SequenceEncoder(NIOUtils.writableChannel(@out), Rational.R(fps, 1), Format.___003C_003EMOV, Codec.___003C_003EH264, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(49)]
	public static SequenceEncoder create25Fps(File @out)
	{
		SequenceEncoder result = new SequenceEncoder(NIOUtils.writableChannel(@out), Rational.R(25, 1), Format.___003C_003EMOV, Codec.___003C_003EH264, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(53)]
	public static SequenceEncoder create30Fps(File @out)
	{
		SequenceEncoder result = new SequenceEncoder(NIOUtils.writableChannel(@out), Rational.R(30, 1), Format.___003C_003EMOV, Codec.___003C_003EH264, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(57)]
	public static SequenceEncoder create2997Fps(File @out)
	{
		SequenceEncoder result = new SequenceEncoder(NIOUtils.writableChannel(@out), Rational.R(30000, 1001), Format.___003C_003EMOV, Codec.___003C_003EH264, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(61)]
	public static SequenceEncoder create24Fps(File @out)
	{
		SequenceEncoder result = new SequenceEncoder(NIOUtils.writableChannel(@out), Rational.R(24, 1), Format.___003C_003EMOV, Codec.___003C_003EH264, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(65)]
	public static SequenceEncoder createWithFps(SeekableByteChannel @out, Rational fps)
	{
		SequenceEncoder result = new SequenceEncoder(@out, fps, Format.___003C_003EMOV, Codec.___003C_003EH264, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 110, 145, 141, 100, 122, 149, 169,
		127, 20, 147, 100, 141, 121, 111
	})]
	public virtual void encodeNativeFrame(Picture pic)
	{
		if (pic.getColor() != ColorSpace.___003C_003ERGB)
		{
			
			throw new IllegalArgumentException("The input images is expected in RGB color.");
		}
		ColorSpace sinkColor = sink.getInputColor();
		PixelStore.LoanerPicture toEncode;
		if (sinkColor != null)
		{
			toEncode = pixelStore.getPicture(pic.getWidth(), pic.getHeight(), sinkColor);
			transform.transform(pic, toEncode.getPicture());
		}
		else
		{
			toEncode = new PixelStore.LoanerPicture(pic, 0);
		}
		Packet pkt = Packet.createPacket(null, timestamp, fps.getNum(), fps.getDen(), frameNo, Packet.FrameType.___003C_003EKEY, null);
		sink.outputVideoFrame(new VideoFrameWithPacket(pkt, toEncode));
		if (sinkColor != null)
		{
			pixelStore.putBack(toEncode);
		}
		timestamp += fps.getDen();
		frameNo++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 115, 162, 110 })]
	public virtual void finish()
	{
		sink.finish();
	}
}
