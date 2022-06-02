using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.muxer;

public class MP4Muxer : Object, Muxer
{
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/muxer/AbstractMP4MuxerTrack;>;")]
	private List tracks;

	protected internal long mdatOffset;

	private int nextTrackId;

	protected internal SeekableByteChannel @out;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(49)]
	public static MP4Muxer createMP4MuxerToChannel(SeekableByteChannel output)
	{
		MP4Muxer result = new MP4Muxer(output, Brand.___003C_003EMP4.getFileTypeBox());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 233, 53, 232, 76, 108, 136, 108,
		104, 115, 115, 110, 106, 104, 105
	})]
	public MP4Muxer(SeekableByteChannel output, FileTypeBox ftyp)
	{
		nextTrackId = 1;
		tracks = new ArrayList();
		@out = output;
		ByteBuffer buf = ByteBuffer.allocate(1024);
		ftyp.write(buf);
		Header.createHeader("wide", 8L).write(buf);
		Header.createHeader("mdat", 1L).write(buf);
		mdatOffset = buf.position();
		buf.putLong(0L);
		buf.flip();
		output.write(buf);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/muxer/AbstractMP4MuxerTrack;>(TT;)TT;")]
	[LineNumberTable(new byte[] { 159, 120, 162, 109, 104, 114, 121, 121, 117 })]
	public virtual AbstractMP4MuxerTrack addTrack(AbstractMP4MuxerTrack track)
	{
		Preconditions.checkNotNull((object)track, (object)"track can not be null");
		int trackId = track.getTrackId();
		Preconditions.checkArgument(trackId <= nextTrackId);
		Preconditions.checkArgument((!hasTrackId(trackId)) ? true : false, "track with id %s already exists", trackId);
		tracks.add(track.setOut(@out));
		nextTrackId = Math.max(trackId + 1, nextTrackId);
		return track;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 98, 127, 2, 106, 131, 99 })]
	public virtual bool hasTrackId(int trackId)
	{
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4MuxerTrack t = (AbstractMP4MuxerTrack)iterator.next();
			if (t.getTrackId() == trackId)
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 110, 130, 103, 104, 136, 127, 2, 106, 101,
		105, 99
	})]
	public virtual MovieBox finalizeHeader()
	{
		MovieBox movie = MovieBox.createMovieBox();
		MovieHeaderBox mvhd = movieHeader();
		movie.addFirst(mvhd);
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4MuxerTrack track = (AbstractMP4MuxerTrack)iterator.next();
			Box trak = track.finish(mvhd);
			if (trak != null)
			{
				movie.add(trak);
			}
		}
		return movie;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 112, 130, 119, 141, 115, 111 })]
	public virtual void storeHeader(MovieBox movie)
	{
		long mdatSize = @out.position() - mdatOffset + 8u;
		MP4Util.writeMovie(@out, movie);
		@out.setPosition(mdatOffset);
		NIOUtils.writeLong(@out, mdatSize);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 162, 120, 120, 104, 100, 104, 168, 124,
		63, 36
	})]
	private MovieHeaderBox movieHeader()
	{
		int timescale = ((AbstractMP4MuxerTrack)tracks.get(0)).getTimescale();
		long duration = ((AbstractMP4MuxerTrack)tracks.get(0)).getTrackTotalDuration();
		AbstractMP4MuxerTrack videoTrack = getVideoTrack();
		if (videoTrack != null)
		{
			timescale = videoTrack.getTimescale();
			duration = videoTrack.getTrackTotalDuration();
		}
		MovieHeaderBox result = MovieHeaderBox.createMovieHeaderBox(timescale, duration, 1f, 1f, new Date().getTime(), new Date().getTime(), new int[9] { 65536, 0, 0, 0, 65536, 0, 0, 0, 1073741824 }, nextTrackId);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 162, 127, 2, 105, 131, 99 })]
	public virtual AbstractMP4MuxerTrack getVideoTrack()
	{
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4MuxerTrack frameMuxer = (AbstractMP4MuxerTrack)iterator.next();
			if (frameMuxer.isVideo())
			{
				return frameMuxer;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(87)]
	private CodecMP4MuxerTrack doAddTrack(MP4TrackType type, Codec codec)
	{
		CodecMP4MuxerTrack.___003Cclinit_003E();
		int num = nextTrackId;
		nextTrackId = num + 1;
		return (CodecMP4MuxerTrack)addTrack(new CodecMP4MuxerTrack(num, type, codec));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(184)]
	public virtual PCMMP4MuxerTrack addPCMAudioTrack(AudioFormat format)
	{
		int num = nextTrackId;
		nextTrackId = num + 1;
		return (PCMMP4MuxerTrack)addTrack(new PCMMP4MuxerTrack(num, format));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 66, 110, 136 })]
	public virtual CodecMP4MuxerTrack addCompressedAudioTrack(Codec codec, AudioFormat format)
	{
		CodecMP4MuxerTrack track = doAddTrack(MP4TrackType.___003C_003ESOUND, codec);
		track.addAudioSampleEntry(format);
		return track;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(53)]
	public static MP4Muxer createMP4Muxer(SeekableByteChannel output, Brand brand)
	{
		MP4Muxer result = new MP4Muxer(output, brand.getFileTypeBox());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(71)]
	public virtual TimecodeMP4MuxerTrack addTimecodeTrack()
	{
		TimecodeMP4MuxerTrack.___003Cclinit_003E();
		int num = nextTrackId;
		nextTrackId = num + 1;
		return (TimecodeMP4MuxerTrack)addTrack(new TimecodeMP4MuxerTrack(num));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 162, 121, 106, 110, 117 })]
	public virtual CodecMP4MuxerTrack addTrackWithId(MP4TrackType type, Codec codec, int trackId)
	{
		Preconditions.checkArgument((!hasTrackId(trackId)) ? true : false, "track with id %s already exists", trackId);
		CodecMP4MuxerTrack track = new CodecMP4MuxerTrack(trackId, type, codec);
		tracks.add(track);
		nextTrackId = Math.max(nextTrackId, trackId + 1);
		return track;
	}

	[LineNumberTable(83)]
	public virtual int getNextTrackId()
	{
		return nextTrackId;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/muxer/AbstractMP4MuxerTrack;>;")]
	[LineNumberTable(110)]
	public virtual List getTracks()
	{
		List result = Collections.unmodifiableList(tracks);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 114, 162, 124, 136, 106 })]
	public virtual void finish()
	{
		Preconditions.checkState((tracks.size() != 0) ? true : false, (object)"Can not save header with 0 tracks.");
		MovieBox movie = finalizeHeader();
		storeHeader(movie);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 66, 127, 2, 105, 131, 99 })]
	public virtual AbstractMP4MuxerTrack getTimecodeTrack()
	{
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4MuxerTrack frameMuxer = (AbstractMP4MuxerTrack)iterator.next();
			if (frameMuxer.isTimecode())
			{
				return frameMuxer;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/muxer/AbstractMP4MuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 102, 98, 103, 127, 2, 105, 137, 99 })]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			AbstractMP4MuxerTrack frameMuxer = (AbstractMP4MuxerTrack)iterator.next();
			if (frameMuxer.isAudio())
			{
				result.add(frameMuxer);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 93, 66, 110, 154, 104 })]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		CodecMP4MuxerTrack track = doAddTrack(MP4TrackType.___003C_003EVIDEO, codec);
		Preconditions.checkArgument((meta != null || codec == Codec.___003C_003EH264) ? true : false, (object)"VideoCodecMeta is required upfront for all codecs but H.264");
		track.addVideoSampleEntry(meta);
		return track;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 91, 98, 104, 105, 139 })]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		AudioFormat format = meta.getFormat();
		if (codec == Codec.___003C_003EPCM)
		{
			PCMMP4MuxerTrack result = addPCMAudioTrack(format);
			
			return result;
		}
		CodecMP4MuxerTrack result2 = addCompressedAudioTrack(codec, format);
		
		return result2;
	}
}
