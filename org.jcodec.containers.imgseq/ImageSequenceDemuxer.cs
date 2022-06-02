using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.containers.imgseq;

[Implements(new string[] { "org.jcodec.common.Demuxer", "org.jcodec.common.DemuxerTrack" })]
public class ImageSequenceDemuxer : java.lang.Object, Demuxer, Closeable, AutoCloseable, DemuxerTrack
{
	private const int VIDEO_FPS = 25;

	private string namePattern;

	private int frameNo;

	private Packet curFrame;

	private Codec codec;

	private int maxAvailableFrame;

	private int maxFrames;

	private string prevName;

	private const int MAX_MAX = 5184000;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 105, 104, 104, 104, 141, 104, 110,
		110, 123, 140
	})]
	public ImageSequenceDemuxer(string namePattern, int maxFrames)
	{
		this.namePattern = namePattern;
		this.maxFrames = maxFrames;
		maxAvailableFrame = -1;
		curFrame = loadFrame();
		string lowerCase = java.lang.String.instancehelper_toLowerCase(namePattern);
		if (java.lang.String.instancehelper_endsWith(lowerCase, ".png"))
		{
			codec = Codec.___003C_003EPNG;
		}
		else if (java.lang.String.instancehelper_endsWith(lowerCase, ".jpg") || java.lang.String.instancehelper_endsWith(lowerCase, ".jpeg"))
		{
			codec = Codec.___003C_003EJPEG;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 111, 163, 131, 191, 2, 111, 131,
		104, 104, 114, 99, 111, 141, 108, 131, 127, 17,
		111
	})]
	private Packet loadFrame()
	{
		if (frameNo > maxFrames)
		{
			return null;
		}
		File file = null;
		do
		{
			string name = java.lang.String.format(namePattern, Integer.valueOf(frameNo));
			if (java.lang.String.instancehelper_equals(name, prevName))
			{
				return null;
			}
			prevName = name;
			file = new File(name);
			if (file.exists() || frameNo > 0)
			{
				break;
			}
			frameNo++;
		}
		while (frameNo < 2);
		if (file == null || !file.exists())
		{
			return null;
		}
		Packet.___003Cclinit_003E();
		Packet ret = new Packet(NIOUtils.fetchFromFile(file), frameNo, 25, 1L, frameNo, Packet.FrameType.___003C_003EKEY, null, frameNo);
		frameNo++;
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 127, 66, 103, 105 })]
	public virtual List getTracks()
	{
		ArrayList tracks = new ArrayList();
		tracks.add(this);
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 141, 99, 107, 127, 13, 99, 227,
		61, 231, 70, 99, 105, 127, 15, 5, 231, 69,
		104, 159, 6
	})]
	public virtual int getMaxAvailableFrame()
	{
		if (maxAvailableFrame == -1)
		{
			int firstPoint = 0;
			for (int i = 5184000; i > 0; i /= 2)
			{
				
				if (new File(java.lang.String.format(namePattern, Integer.valueOf(i))).exists())
				{
					firstPoint = i;
					break;
				}
			}
			int pos = firstPoint;
			for (int interv = firstPoint / 2; interv > 1; interv /= 2)
			{
				
				if (new File(java.lang.String.format(namePattern, Integer.valueOf(pos + interv))).exists())
				{
					pos += interv;
				}
			}
			maxAvailableFrame = pos;
			Logger.info(new StringBuilder().append("Max frame found: ").append(maxAvailableFrame).toString());
		}
		int result = java.lang.Math.min(maxAvailableFrame, maxFrames);
		
		return result;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(56)]
	public virtual void close()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(67)]
	public virtual List getVideoTracks()
	{
		List tracks = getTracks();
		
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(72)]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 123, 130, 140, 80, 3 })]
	public virtual Packet nextFrame()
	{
		try
		{
			return curFrame;
		}
		finally
		{
			curFrame = loadFrame();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 130, 104 })]
	public virtual DemuxerTrackMeta getMeta()
	{
		int durationFrames = getMaxAvailableFrame();
		DemuxerTrackMeta result = new DemuxerTrackMeta(TrackType.___003C_003EVIDEO, codec, (durationFrames + 1) * 25, null, durationFrames + 1, null, null, null);
		return result;
	}

	public void Dispose()
	{
		close();
	}
}
