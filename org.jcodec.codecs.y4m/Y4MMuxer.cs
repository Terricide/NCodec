using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.nio.channels;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.codecs.y4m;

[Implements(new string[] { "org.jcodec.common.Muxer", "org.jcodec.common.MuxerTrack" })]
public class Y4MMuxer : Object, Muxer, MuxerTrack
{
	private WritableByteChannel ch;

	private bool headerWritten;

	private VideoCodecMeta meta;

	internal static byte[] ___003C_003EframeTag;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] frameTag
	{
		[HideFromJava]
		get
		{
			return ___003C_003EframeTag;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 105, 104 })]
	public Y4MMuxer(WritableByteChannel ch)
	{
		this.ch = ch;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 134, 66, 109, 111, 127, 0, 103, 115 })]
	protected internal virtual void writeHeader()
	{
		Size size = meta.getSize();
		byte[] bytes = String.instancehelper_getBytes(String.format("YUV4MPEG2 W%d H%d F25:1 Ip A0:0 C420jpeg XYSCSS=420JPEG\n", Integer.valueOf(size.getWidth()), Integer.valueOf(size.getHeight())));
		ch.write(ByteBuffer.wrap(bytes));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 98, 105, 103, 136, 119, 120 })]
	public virtual void addFrame(Packet outPacket)
	{
		if (!headerWritten)
		{
			writeHeader();
			headerWritten = true;
		}
		ch.write(ByteBuffer.wrap(___003C_003EframeTag));
		ch.write(outPacket.data.duplicate());
	}

	[LineNumberTable(new byte[] { 159, 130, 162, 104 })]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		this.meta = meta;
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		
		throw new RuntimeException("Y4M doesn't support audio");
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(62)]
	public virtual void finish()
	{
	}

	[LineNumberTable(25)]
	static Y4MMuxer()
	{
		___003C_003EframeTag = String.instancehelper_getBytes("FRAME\n");
	}
}
