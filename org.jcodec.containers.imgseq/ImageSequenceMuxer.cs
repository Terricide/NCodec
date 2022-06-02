using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.containers.imgseq;

[Implements(new string[] { "org.jcodec.common.Muxer", "org.jcodec.common.MuxerTrack" })]
public class ImageSequenceMuxer : Object, Muxer, MuxerTrack
{
	private string fileNamePattern;

	private int frameNo;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 104 })]
	public ImageSequenceMuxer(string fileNamePattern)
	{
		this.fileNamePattern = fileNamePattern;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 162, 127, 32 })]
	public virtual void addFrame(Packet packet)
	{
		ByteBuffer data = packet.getData();
		string format = fileNamePattern;
		object[] array = new object[1];
		int num = frameNo;
		frameNo = num + 1;
		array[0] = Integer.valueOf(num);
		NIOUtils.writeTo(data, MainUtils.tildeExpand(String.format(format, array)));
	}

	[LineNumberTable(36)]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 107 })]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		Logger.warn("Audio is not supported for image sequence muxer.");
		return null;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(48)]
	public virtual void finish()
	{
	}
}
