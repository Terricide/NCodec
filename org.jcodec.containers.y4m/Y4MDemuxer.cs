using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.platform;

namespace org.jcodec.containers.y4m;

[Implements(new string[] { "org.jcodec.common.DemuxerTrack", "org.jcodec.common.Demuxer" })]
public class Y4MDemuxer : java.lang.Object, DemuxerTrack, Demuxer, Closeable, AutoCloseable
{
	private SeekableByteChannel @is;

	private int width;

	private int height;

	private string invalidFormat;

	private Rational fps;

	private int bufSize;

	private int frameNum;

	private int totalFrames;

	private int totalDuration;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 105, 104, 114, 143, 112, 108, 130,
		106, 113, 108, 162, 116, 148, 107, 101, 108, 191,
		4, 116, 116, 118, 110, 125, 127, 14
	})]
	public Y4MDemuxer(SeekableByteChannel _is)
	{
		@is = _is;
		ByteBuffer buf = NIOUtils.fetchFromChannel(@is, 2048);
		string[] header = StringUtils.splitC(readLine(buf), ' ');
		if (!java.lang.String.instancehelper_equals("YUV4MPEG2", header[0]))
		{
			invalidFormat = "Not yuv4mpeg stream";
			return;
		}
		string chroma = find(header, 'C');
		if (chroma != null && !java.lang.String.instancehelper_startsWith(chroma, "420"))
		{
			invalidFormat = "Only yuv420p is supported";
			return;
		}
		width = Integer.parseInt(find(header, 'W'));
		height = Integer.parseInt(find(header, 'H'));
		string fpsStr = find(header, 'F');
		if (fpsStr != null)
		{
			string[] numden = StringUtils.splitC(fpsStr, ':');
			Rational.___003Cclinit_003E();
			fps = new Rational(Integer.parseInt(numden[0]), Integer.parseInt(numden[1]));
		}
		@is.setPosition(buf.position());
		bufSize = width * height;
		bufSize += bufSize / 2;
		long fileSize = @is.size();
		long num = bufSize + 7;
		totalFrames = (int)((num != -1) ? (fileSize / num) : (-fileSize));
		int num2 = totalFrames * fps.getDen();
		int num3 = fps.getNum();
		totalDuration = ((num3 != -1) ? (num2 / num3) : (-num2));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 130, 104, 116, 99, 105, 112 })]
	private static string readLine(ByteBuffer y4m)
	{
		ByteBuffer duplicate = y4m.duplicate();
		while (y4m.hasRemaining() && (sbyte)y4m.get() != 10)
		{
		}
		if (y4m.hasRemaining())
		{
			duplicate.limit(y4m.position() - 1);
		}
		string result = Platform.stringFromBytes(NIOUtils.toArray(duplicate));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 97, 67, 104, 101, 107, 235, 61, 231,
		69
	})]
	private static string find(string[] header, char c)
	{
		for (int i = 0; i < (nint)header.LongLength; i++)
		{
			string @string = header[i];
			if (java.lang.String.instancehelper_charAt(@string, 0) == c)
			{
				string result = java.lang.String.instancehelper_substring(@string, 1);
				
				return result;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 111, 162, 103, 105 })]
	public virtual List getTracks()
	{
		ArrayList list = new ArrayList();
		((List)list).add((object)this);
		return list;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 105, 127, 12, 114, 104, 113, 131,
		127, 1, 115, 127, 43, 111
	})]
	public virtual Packet nextFrame()
	{
		if (invalidFormat != null)
		{
			string message = new StringBuilder().append("Invalid input: ").append(invalidFormat).toString();
			
			throw new RuntimeException(message);
		}
		ByteBuffer buf = NIOUtils.fetchFromChannel(@is, 2048);
		string frame = readLine(buf);
		if (frame == null || !java.lang.String.instancehelper_startsWith(frame, "FRAME"))
		{
			return null;
		}
		@is.setPosition(@is.position() - buf.remaining());
		ByteBuffer pix = NIOUtils.fetchFromChannel(@is, bufSize);
		Packet.___003Cclinit_003E();
		Packet packet = new Packet(pix, frameNum * fps.getDen(), fps.getNum(), fps.getDen(), frameNum, Packet.FrameType.___003C_003EKEY, null, frameNum);
		frameNum++;
		return packet;
	}

	[LineNumberTable(111)]
	public virtual Rational getFps()
	{
		return fps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 66, 127, 17, 46 })]
	public virtual DemuxerTrackMeta getMeta()
	{
		DemuxerTrackMeta result = new DemuxerTrackMeta(TrackType.___003C_003EVIDEO, Codec.___003C_003ERAW, totalDuration, null, totalFrames, null, VideoCodecMeta.createSimpleVideoCodecMeta(new Size(width, height), ColorSpace.___003C_003EYUV420), null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 112, 130, 110 })]
	public virtual void close()
	{
		@is.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(134)]
	public virtual List getVideoTracks()
	{
		List tracks = getTracks();
		
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(139)]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		
		return result;
	}

	public void Dispose()
	{
		close();
	}
}
