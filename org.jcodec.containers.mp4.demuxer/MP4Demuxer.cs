using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.demuxer;

[Implements(new string[] { "org.jcodec.common.Demuxer" })]
public class MP4Demuxer : java.lang.Object, Demuxer, Closeable, AutoCloseable
{
	[SpecialName]
	[EnclosingMethod(null, "createRawMP4Demuxer", "(Lorg.jcodec.common.io.SeekableByteChannel;)Lorg.jcodec.containers.mp4.demuxer.MP4Demuxer;")]
	internal sealed class _1 : MP4Demuxer
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(55)]
		internal _1(SeekableByteChannel input)
			: base(input)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(58)]
		protected internal override AbstractMP4DemuxerTrack newTrack(TrakBox trak)
		{
			MP4DemuxerTrack result = new MP4DemuxerTrack(movie, trak, input);
			return result;
		}
	}

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/demuxer/AbstractMP4DemuxerTrack;>;")]
	private List tracks;

	private TimecodeMP4DemuxerTrack timecodeTrack;

	internal MovieBox movie;

	protected internal SeekableByteChannel input;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(50)]
	public static MP4Demuxer createMP4Demuxer(SeekableByteChannel input)
	{
		MP4Demuxer result = new MP4Demuxer(input);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 130, 127, 2, 110, 99, 99 })]
	public virtual DemuxerTrack getVideoTrack()
	{
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4DemuxerTrack demuxerTrack = (AbstractMP4DemuxerTrack)iterator.next();
			if (demuxerTrack.box.isVideo())
			{
				return demuxerTrack;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 124, 130, 105, 104, 108, 106 })]
	internal MP4Demuxer(SeekableByteChannel input)
	{
		this.input = input;
		tracks = new LinkedList();
		findMovieBox(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(71)]
	protected internal virtual AbstractMP4DemuxerTrack newTrack(TrakBox trak)
	{
		CodecMP4DemuxerTrack result = new CodecMP4DemuxerTrack(movie, trak, input);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 122, 98, 104, 108, 113, 141, 111 })]
	private void findMovieBox(SeekableByteChannel input)
	{
		MP4Util.Movie mv = MP4Util.parseFullMovieChannel(input);
		if (mv == null || mv.getMoov() == null)
		{
			throw new IOException("Could not find movie meta information box");
		}
		movie = mv.getMoov();
		processHeader(movie);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 130, 99, 119, 107, 101, 127, 30, 120,
		133, 244, 58, 234, 73, 100, 105, 101, 153
	})]
	private void processHeader(NodeBox moov)
	{
		TrakBox tt = null;
		TrakBox[] trakBoxs = (TrakBox[])NodeBox.findAll(moov, ClassLiteral<TrakBox>.Value, "trak");
		for (int i = 0; i < (nint)trakBoxs.LongLength; i++)
		{
			TrakBox trak = trakBoxs[i];
			SampleEntry se = (SampleEntry)NodeBox.findFirstPath(trak, ClassLiteral<SampleEntry>.Value, new string[5] { "mdia", "minf", "stbl", "stsd", null });
			if (se != null && java.lang.String.instancehelper_equals("tmcd", se.getFourcc()))
			{
				tt = trak;
			}
			else
			{
				tracks.add(fromTrakBox(trak));
			}
		}
		if (tt != null)
		{
			DemuxerTrack video = getVideoTrack();
			if (video != null)
			{
				timecodeTrack = new TimecodeMP4DemuxerTrack(movie, tt, input);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 66, 124, 105, 107 })]
	private AbstractMP4DemuxerTrack fromTrakBox(TrakBox trak)
	{
		SampleSizesBox stsz = (SampleSizesBox)NodeBox.findFirstPath(trak, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
		if (stsz.getDefaultSize() == 0)
		{
			AbstractMP4DemuxerTrack result = newTrack(trak);
			return result;
		}
		PCMMP4DemuxerTrack result2 = new PCMMP4DemuxerTrack(movie, trak, input);
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(55)]
	public static MP4Demuxer createRawMP4Demuxer(SeekableByteChannel input)
	{
		_1 result = new _1(input);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 98, 124 })]
	public static MP4TrackType getTrackType(TrakBox trak)
	{
		HandlerBox handler = (HandlerBox)NodeBox.findFirstPath(trak, ClassLiteral<HandlerBox>.Value, Box.path("mdia.hdlr"));
		MP4TrackType result = MP4TrackType.fromHandler(handler.getComponentSubType());
		return result;
	}

	[LineNumberTable(122)]
	public virtual MovieBox getMovie()
	{
		return movie;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 130, 127, 2, 106, 99, 99 })]
	public virtual AbstractMP4DemuxerTrack getTrack(int no)
	{
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4DemuxerTrack track = (AbstractMP4DemuxerTrack)iterator.next();
			if (track.getNo() == no)
			{
				return track;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/demuxer/AbstractMP4DemuxerTrack;>;")]
	[LineNumberTable(135)]
	public virtual List getTracks()
	{
		ArrayList result = new ArrayList(tracks);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 107, 66, 103, 127, 2, 110, 105, 99 })]
	public virtual List getVideoTracks()
	{
		ArrayList result = new ArrayList();
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4DemuxerTrack demuxerTrack = (AbstractMP4DemuxerTrack)iterator.next();
			if (demuxerTrack.box.isVideo())
			{
				result.add(demuxerTrack);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 105, 130, 103, 127, 2, 110, 105, 99 })]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4DemuxerTrack demuxerTrack = (AbstractMP4DemuxerTrack)iterator.next();
			if (demuxerTrack.box.isAudio())
			{
				result.add(demuxerTrack);
			}
		}
		return result;
	}

	[LineNumberTable(159)]
	public virtual TimecodeMP4DemuxerTrack getTimecodeTrack()
	{
		return timecodeTrack;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 66, 104, 99, 99, 109, 109, 105, 100,
		102, 104, 103, 102, 102, 159, 30, 101, 101, 106,
		99, 110, 134
	})]
	public static int probe(ByteBuffer b)
	{
		ByteBuffer fork = b.duplicate();
		int success = 0;
		int total = 0;
		while (fork.remaining() >= 8)
		{
			long len = Platform.unsignedInt(fork.getInt());
			int fcc = fork.getInt();
			int hdrLen = 8;
			if (len == 1u)
			{
				len = fork.getLong();
				hdrLen = 16;
			}
			else if (len < 8u)
			{
				break;
			}
			if ((fcc == Fourcc.___003C_003Eftyp && len < 64u) || (fcc == Fourcc.___003C_003Emoov && len < 104857600u) || fcc == Fourcc.___003C_003Efree || fcc == Fourcc.___003C_003Emdat || fcc == Fourcc.___003C_003Ewide)
			{
				success++;
			}
			total++;
			if (len >= 2147483647u)
			{
				break;
			}
			NIOUtils.skip(fork, (int)(len - hdrLen));
		}
		int result;
		if (total == 0)
		{
			result = 0;
		}
		else
		{
			int num = success * 100;
			int num2 = total;
			result = ((num2 != -1) ? (num / num2) : (-num));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 95, 130, 110 })]
	public virtual void close()
	{
		input.close();
	}

    public void Dispose()
    {
		close();
    }
}
