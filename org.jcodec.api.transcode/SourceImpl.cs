using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.mjpeg;
using org.jcodec.codecs.mpeg12;
using org.jcodec.codecs.mpeg4;
using org.jcodec.codecs.png;
using org.jcodec.codecs.prores;
using org.jcodec.codecs.raw;
using org.jcodec.codecs.vpx;
using org.jcodec.codecs.wav;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.containers.imgseq;
using org.jcodec.containers.mkv.demuxer;
using org.jcodec.containers.mp3;
using org.jcodec.containers.mp4.demuxer;
using org.jcodec.containers.mps;
using org.jcodec.containers.webp;
using org.jcodec.containers.y4m;

namespace org.jcodec.api.transcode;

[Implements(new string[] { "org.jcodec.api.transcode.Source", "org.jcodec.api.transcode.PacketSource" })]
public class SourceImpl : Object, Source, PacketSource
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class RawAudioDecoder : Object, AudioDecoder
	{
		private AudioFormat format;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 52, 66, 105, 104 })]
		public RawAudioDecoder(AudioFormat format)
		{
			this.format = format;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(366)]
		public virtual AudioBuffer decodeFrame(ByteBuffer frame, ByteBuffer dst)
		{
			AudioFormat audioFormat = format;
			int num = frame.remaining();
			short frameSize = format.getFrameSize();
			AudioBuffer result = new AudioBuffer(frame, audioFormat, (frameSize != -1) ? (num / frameSize) : (-num));
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(371)]
		public virtual AudioCodecMeta getCodecMeta(ByteBuffer data)
		{
			AudioCodecMeta result = AudioCodecMeta.fromAudioFormat(format);
			return result;
		}
	}

	private string sourceName;

	private SeekableByteChannel sourceStream;

	private Demuxer demuxVideo;

	private Demuxer demuxAudio;

	private Format inputFormat;

	private DemuxerTrack videoInputTrack;

	private DemuxerTrack audioInputTrack;

	[Signature("Lorg/jcodec/common/Tuple$_3<Ljava/lang/Integer;Ljava/lang/Integer;Lorg/jcodec/common/Codec;>;")]
	private Tuple._3 inputVideoCodec;

	[Signature("Lorg/jcodec/common/Tuple$_3<Ljava/lang/Integer;Ljava/lang/Integer;Lorg/jcodec/common/Codec;>;")]
	private Tuple._3 inputAudioCodec;

	[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/VideoFrameWithPacket;>;")]
	private List frameReorderBuffer;

	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Packet;>;")]
	private List videoPacketReorderBuffer;

	private PixelStore pixelStore;

	private VideoCodecMeta videoCodecMeta;

	private AudioCodecMeta audioCodecMeta;

	private AudioDecoder audioDecoder;

	private VideoDecoder videoDecoder;

	private int downscale;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 91, 162, 99, 127, 2, 106, 131, 103, 101,
		99
	})]
	private MPEGDemuxer.MPEGDemuxerTrack openTSTrack(MPSDemuxer demuxerVideo, Integer selectedTrack)
	{
		int trackNo = 0;
		Iterator iterator = demuxerVideo.getTracks().iterator();
		while (iterator.hasNext())
		{
			MPEGDemuxer.MPEGDemuxerTrack track = (MPEGDemuxer.MPEGDemuxerTrack)iterator.next();
			if (trackNo == selectedTrack.intValue())
			{
				return track;
			}
			track.ignore();
			trackNo++;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 82, 130, 105, 99, 237, 69, 105, 127, 11,
		105, 216
	})]
	private Packet getNextVideoPacket()
	{
		if (videoInputTrack == null)
		{
			return null;
		}
		Packet nextFrame = videoInputTrack.nextFrame();
		if (videoDecoder == null)
		{
			videoDecoder = createVideoDecoder((Codec)inputVideoCodec.___003C_003Ev2, downscale, nextFrame.getData(), null);
			if (videoDecoder != null)
			{
				videoCodecMeta = videoDecoder.getCodecMeta(nextFrame.getData());
			}
		}
		return nextFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 62, 162, 105, 106, 105, 105, 105, 106, 105,
		106, 105, 105, 105, 106, 105, 105, 105, 105, 149
	})]
	private VideoDecoder createVideoDecoder(Codec codec, int downscale, ByteBuffer codecPrivate, VideoCodecMeta videoCodecMeta)
	{
		if (Codec.___003C_003EH264 == codec)
		{
			H264Decoder result = H264Decoder.createH264DecoderFromCodecPrivate(codecPrivate);
			return result;
		}
		if (Codec.___003C_003EPNG == codec)
		{
			PNGDecoder result2 = new PNGDecoder();
			return result2;
		}
		if (Codec.___003C_003EMPEG2 == codec)
		{
			MPEGDecoder result3 = createMpegDecoder(downscale);
			return result3;
		}
		if (Codec.___003C_003EPRORES == codec)
		{
			ProresDecoder result4 = createProresDecoder(downscale);
			return result4;
		}
		if (Codec.___003C_003EVP8 == codec)
		{
			VP8Decoder result5 = new VP8Decoder();
			return result5;
		}
		if (Codec.___003C_003EJPEG == codec)
		{
			JpegDecoder result6 = createJpegDecoder(downscale);
			return result6;
		}
		if (Codec.___003C_003EMPEG4 == codec)
		{
			MPEG4Decoder result7 = new MPEG4Decoder();
			return result7;
		}
		if (Codec.___003C_003ERAW == codec)
		{
			Size dim = videoCodecMeta.getSize();
			RAWVideoDecoder result8 = new RAWVideoDecoder(dim.getWidth(), dim.getHeight());
			return result8;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 64, 98, 115, 106, 115, 153 })]
	private AudioDecoder createAudioDecoder(ByteBuffer codecPrivate)
	{
		if (Codec.___003C_003EAAC == inputAudioCodec.___003C_003Ev2)
		{
			AACDecoder result = new AACDecoder(codecPrivate);
			return result;
		}
		if (Codec.___003C_003EPCM == inputAudioCodec.___003C_003Ev2)
		{
			RawAudioDecoder result2 = new RawAudioDecoder(getAudioMeta().getAudioCodecMeta().getFormat());
			return result2;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException", "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 110, 146, 110, 127, 3, 110, 127,
		8, 110, 124, 110, 119, 110, 127, 3, 110, 109,
		118, 104, 115, 124, 110, 119, 110, 124, 113, 110,
		100, 105, 127, 5, 127, 0, 137, 105, 127, 2,
		159, 5, 127, 0, 137, 127, 15, 127, 29, 104,
		127, 2, 143, 102, 99, 191, 22, 113, 110, 107,
		191, 9, 113, 110, 107, 191, 9
	})]
	public virtual void initDemuxer()
	{
		if (inputFormat != Format.___003C_003EIMG)
		{
			sourceStream = NIOUtils.readableFileChannel(sourceName);
		}
		if (Format.___003C_003EMOV == inputFormat)
		{
			MP4Demuxer mP4Demuxer = MP4Demuxer.createMP4Demuxer(sourceStream);
			demuxAudio = mP4Demuxer;
			demuxVideo = mP4Demuxer;
		}
		else if (Format.___003C_003EMKV == inputFormat)
		{
			MKVDemuxer.___003Cclinit_003E();
			MKVDemuxer mKVDemuxer = new MKVDemuxer(sourceStream);
			demuxAudio = mKVDemuxer;
			demuxVideo = mKVDemuxer;
		}
		else if (Format.___003C_003EIMG == inputFormat)
		{
			demuxVideo = new ImageSequenceDemuxer(sourceName, int.MaxValue);
		}
		else if (Format.___003C_003EWEBP == inputFormat)
		{
			demuxVideo = new WebpDemuxer(sourceStream);
		}
		else if (Format.___003C_003EMPEG_PS == inputFormat)
		{
			MPSDemuxer mPSDemuxer = new MPSDemuxer(sourceStream);
			demuxAudio = mPSDemuxer;
			demuxVideo = mPSDemuxer;
		}
		else if (Format.___003C_003EY4M == inputFormat)
		{
			Y4MDemuxer y4mDemuxer = new Y4MDemuxer(sourceStream);
			Y4MDemuxer y4MDemuxer = y4mDemuxer;
			demuxAudio = y4MDemuxer;
			demuxVideo = y4MDemuxer;
			videoInputTrack = y4mDemuxer;
		}
		else if (Format.___003C_003EH264 == inputFormat)
		{
			demuxVideo = new BufferH264ES(NIOUtils.fetchAllFromChannel(sourceStream));
		}
		else if (Format.___003C_003EWAV == inputFormat)
		{
			demuxAudio = new WavDemuxer(sourceStream);
		}
		else if (Format.___003C_003EMPEG_AUDIO == inputFormat)
		{
			MPEGAudioDemuxer.___003Cclinit_003E();
			demuxAudio = new MPEGAudioDemuxer(sourceStream);
		}
		else
		{
			if (Format.___003C_003EMPEG_TS != inputFormat)
			{
				string message = new StringBuilder().append("Input format: ").append(inputFormat).append(" is not supported.")
					.toString();
				throw new RuntimeException(message);
			}
			MTSDemuxer mtsDemuxer = new MTSDemuxer(sourceStream);
			MPSDemuxer mpsDemuxer = null;
			if (inputVideoCodec != null)
			{
				mpsDemuxer = new MPSDemuxer(mtsDemuxer.getProgram(((Integer)inputVideoCodec.___003C_003Ev0).intValue()));
				videoInputTrack = openTSTrack(mpsDemuxer, (Integer)inputVideoCodec.___003C_003Ev1);
				demuxVideo = mpsDemuxer;
			}
			if (inputAudioCodec != null)
			{
				if (inputVideoCodec == null || inputVideoCodec.___003C_003Ev0 != inputAudioCodec.___003C_003Ev0)
				{
					mpsDemuxer = new MPSDemuxer(mtsDemuxer.getProgram(((Integer)inputAudioCodec.___003C_003Ev0).intValue()));
				}
				audioInputTrack = openTSTrack(mpsDemuxer, (Integer)inputAudioCodec.___003C_003Ev1);
				demuxAudio = mpsDemuxer;
			}
			Iterator iterator = mtsDemuxer.getPrograms().iterator();
			while (iterator.hasNext())
			{
				int pid = ((Integer)iterator.next()).intValue();
				if ((inputVideoCodec == null || pid != ((Integer)inputVideoCodec.___003C_003Ev0).intValue()) && (inputAudioCodec == null || pid != ((Integer)inputAudioCodec.___003C_003Ev0).intValue()))
				{
					Logger.info(new StringBuilder().append("Unused program: ").append(pid).toString());
					mtsDemuxer.getProgram(pid).close();
				}
			}
		}
		if (demuxVideo != null && inputVideoCodec != null)
		{
			List videoTracks = demuxVideo.getVideoTracks();
			if (videoTracks.size() > 0)
			{
				videoInputTrack = (DemuxerTrack)videoTracks.get(((Integer)inputVideoCodec.___003C_003Ev1).intValue());
			}
		}
		if (demuxAudio != null && inputAudioCodec != null)
		{
			List audioTracks = demuxAudio.getAudioTracks();
			if (audioTracks.size() > 0)
			{
				audioInputTrack = (DemuxerTrack)audioTracks.get(((Integer)inputAudioCodec.___003C_003Ev1).intValue());
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 66, 105, 99 })]
	public virtual DemuxerTrackMeta getAudioMeta()
	{
		if (audioInputTrack == null)
		{
			return null;
		}
		DemuxerTrackMeta meta = audioInputTrack.getMeta();
		return meta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 130, 101, 105, 101, 137 })]
	public static MPEGDecoder createMpegDecoder(int downscale)
	{
		switch (downscale)
		{
			case 2:
				{
					Mpeg2Thumb4x4 result3 = new Mpeg2Thumb4x4();
					return result3;
				}
			case 4:
				{
					Mpeg2Thumb2x2 result2 = new Mpeg2Thumb2x2();
					return result2;
				}
			default:
				{
					MPEGDecoder result = new MPEGDecoder();
					return result;
				}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 162, 101, 105, 101, 105, 101, 137 })]
	public static ProresDecoder createProresDecoder(int downscale)
	{
		if (2 == downscale)
		{
			ProresToThumb4x4 result = new ProresToThumb4x4();
			return result;
		}
		if (4 == downscale)
		{
			ProresToThumb2x2 result2 = new ProresToThumb2x2();
			return result2;
		}
		if (8 == downscale)
		{
			ProresToThumb result3 = new ProresToThumb();
			return result3;
		}
		ProresDecoder result4 = new ProresDecoder();
		return result4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 8, 66, 101, 105, 101, 137 })]
	public static JpegDecoder createJpegDecoder(int downscale)
	{
		switch (downscale)
		{
		case 2:
		{
			JpegToThumb4x4 result3 = new JpegToThumb4x4();
			return result3;
		}
		case 4:
		{
			JpegToThumb2x2 result2 = new JpegToThumb2x2();
			return result2;
		}
		default:
		{
			JpegDecoder result = new JpegDecoder();
			return result;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 93, 66, 110, 109, 106, 137, 127, 16 })]
	protected internal virtual int seekToKeyFrame(int frame)
	{
		if (videoInputTrack is SeekableDemuxerTrack)
		{
			SeekableDemuxerTrack seekable = (SeekableDemuxerTrack)videoInputTrack;
			seekable.gotoSyncFrame(frame);
			return (int)seekable.getCurFrame();
		}
		Logger.warn(new StringBuilder().append("Can not seek in ").append(videoInputTrack).append(" container.")
			.toString());
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 37, 130, 104, 104, 127, 1, 38 })]
	protected internal virtual PixelStore.LoanerPicture getPixelBuffer(ByteBuffer firstFrame)
	{
		VideoCodecMeta videoMeta = getVideoCodecMeta();
		Size size = videoMeta.getSize();
		PixelStore.LoanerPicture picture = pixelStore.getPicture((size.getWidth() + 15) & -16, (size.getHeight() + 15) & -16, videoMeta.getColor());
		return picture;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(345)]
	public virtual Picture decodeVideo(ByteBuffer data, Picture target1)
	{
		Picture result = videoDecoder.decodeFrame(data, target1.getData());
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 35, 130, 105, 104, 104, 108, 141 })]
	public virtual VideoCodecMeta getVideoCodecMeta()
	{
		if (videoCodecMeta != null)
		{
			return videoCodecMeta;
		}
		DemuxerTrackMeta meta = getTrackVideoMeta();
		if (meta != null && meta.getVideoCodecMeta() != null)
		{
			videoCodecMeta = meta.getVideoCodecMeta();
		}
		return videoCodecMeta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 74, 130, 105, 99 })]
	public virtual DemuxerTrackMeta getTrackVideoMeta()
	{
		if (videoInputTrack == null)
		{
			return null;
		}
		DemuxerTrackMeta meta = videoInputTrack.getMeta();
		return meta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 41, 130, 115, 130, 99, 57, 136 })]
	private void detectFrameType(Packet inVideoPacket)
	{
		if (inputVideoCodec.___003C_003Ev2 == Codec.___003C_003EH264)
		{
			inVideoPacket.setFrameType((!H264Utils.isByteBufferIDRSlice(inVideoPacket.getData())) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/api/transcode/VideoFrameWithPacket;>;)Lorg/jcodec/api/transcode/VideoFrameWithPacket;")]
	[LineNumberTable(new byte[] { 159, 25, 130, 103, 110, 137, 110, 159, 4 })]
	private VideoFrameWithPacket removeFirstFixDuration(List reorderBuffer)
	{
		Collections.sort(reorderBuffer);
		VideoFrameWithPacket frame = (VideoFrameWithPacket)reorderBuffer.remove(0);
		if (!reorderBuffer.isEmpty())
		{
			VideoFrameWithPacket nextFrame = (VideoFrameWithPacket)reorderBuffer.get(0);
			frame.getPacket().setDuration(nextFrame.getPacket().getPts() - frame.getPacket().getPts());
		}
		return frame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 77, 98, 105, 99, 109, 108, 115, 105, 184 })]
	public virtual Packet inputAudioPacket()
	{
		if (audioInputTrack == null)
		{
			return null;
		}
		Packet audioPkt = audioInputTrack.nextFrame();
		if (audioDecoder == null && audioPkt != null)
		{
			audioDecoder = createAudioDecoder(audioPkt.getData());
			if (audioDecoder != null)
			{
				audioCodecMeta = audioDecoder.getCodecMeta(audioPkt.getData());
			}
		}
		return audioPkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 87, 98, 104, 100, 110, 117, 110, 99, 115,
		103, 127, 3, 114, 107, 100, 99, 105, 105, 131
	})]
	public virtual Packet inputVideoPacket()
	{
		Packet packet;
		do
		{
			packet = getNextVideoPacket();
			if (packet != null)
			{
				videoPacketReorderBuffer.add(packet);
			}
		}
		while (packet != null && videoPacketReorderBuffer.size() <= 7);
		if (videoPacketReorderBuffer.size() == 0)
		{
			return null;
		}
		Packet @out = (Packet)videoPacketReorderBuffer.remove(0);
		int duration = int.MaxValue;
		Iterator iterator = videoPacketReorderBuffer.iterator();
		while (iterator.hasNext())
		{
			Packet packet2 = (Packet)iterator.next();
			int cand = (int)(packet2.getPts() - @out.getPts());
			if (cand > 0 && cand < duration)
			{
				duration = cand;
			}
		}
		if (duration != int.MaxValue)
		{
			@out.setDuration(duration);
		}
		return @out;
	}

	[LineNumberTable(286)]
	public virtual bool haveAudio()
	{
		return audioInputTrack != null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 70, 162, 105, 110 })]
	public virtual void finish()
	{
		if (sourceStream != null)
		{
			IOUtils.closeQuietly(sourceStream);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 68, 66, 233, 159, 59, 232, 160, 198, 104,
		104, 104, 105, 108, 108
	})]
	public SourceImpl(string sourceName, Format inputFormat, Tuple._3 inputVideoCodec, Tuple._3 inputAudioCodec)
	{
		downscale = 1;
		this.sourceName = sourceName;
		this.inputFormat = inputFormat;
		this.inputVideoCodec = inputVideoCodec;
		this.inputAudioCodec = inputAudioCodec;
		frameReorderBuffer = new ArrayList();
		videoPacketReorderBuffer = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 66, 162, 136, 105 })]
	public virtual void init(PixelStore pixelStore)
	{
		this.pixelStore = pixelStore;
		initDemuxer();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 55, 98, 115, 131, 111 })]
	protected internal virtual ByteBuffer decodeAudio(ByteBuffer audioPkt)
	{
		if (inputAudioCodec.___003C_003Ev2 == Codec.___003C_003EPCM)
		{
			return audioPkt;
		}
		AudioBuffer decodeFrame = audioDecoder.decodeFrame(audioPkt, null);
		ByteBuffer data = decodeFrame.getData();
		return data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 48, 98, 100, 194, 235, 69, 117, 110, 116,
		100, 109, 131, 122, 111, 108, 116, 101, 106, 147,
		102
	})]
	public virtual void seekFrames(int seekFrames)
	{
		if (seekFrames == 0)
		{
			return;
		}
		int skipFrames = seekFrames - seekToKeyFrame(seekFrames);
		Packet inVideoPacket;
		while (skipFrames > 0 && (inVideoPacket = getNextVideoPacket()) != null)
		{
			PixelStore.LoanerPicture loanerBuffer = getPixelBuffer(inVideoPacket.getData());
			Picture decodedFrame = decodeVideo(inVideoPacket.getData(), loanerBuffer.getPicture());
			if (decodedFrame == null)
			{
				pixelStore.putBack(loanerBuffer);
				continue;
			}
			frameReorderBuffer.add(new VideoFrameWithPacket(inVideoPacket, new PixelStore.LoanerPicture(decodedFrame, 1)));
			if (frameReorderBuffer.size() > 7)
			{
				Collections.sort(frameReorderBuffer);
				VideoFrameWithPacket removed = (VideoFrameWithPacket)frameReorderBuffer.remove(0);
				skipFrames += -1;
				if (removed.getFrame() != null)
				{
					pixelStore.putBack(removed.getFrame());
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 32, 130, 110, 110, 136, 99, 110, 116, 100,
		109, 134, 122, 111, 144, 166, 111, 240, 69
	})]
	public virtual VideoFrameWithPacket getNextVideoFrame()
	{
		Packet inVideoPacket;
		while ((inVideoPacket = getNextVideoPacket()) != null)
		{
			if (inVideoPacket.getFrameType() == Packet.FrameType.___003C_003EUNKNOWN)
			{
				detectFrameType(inVideoPacket);
			}
			Picture decodedFrame = null;
			PixelStore.LoanerPicture pixelBuffer = getPixelBuffer(inVideoPacket.getData());
			decodedFrame = decodeVideo(inVideoPacket.getData(), pixelBuffer.getPicture());
			if (decodedFrame == null)
			{
				pixelStore.putBack(pixelBuffer);
				continue;
			}
			frameReorderBuffer.add(new VideoFrameWithPacket(inVideoPacket, new PixelStore.LoanerPicture(decodedFrame, 1)));
			if (frameReorderBuffer.size() <= 7)
			{
				continue;
			}
			VideoFrameWithPacket result = removeFirstFixDuration(frameReorderBuffer);
			return result;
		}
		if (frameReorderBuffer.size() > 0)
		{
			VideoFrameWithPacket result2 = removeFirstFixDuration(frameReorderBuffer);
			return result2;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 22, 130, 104, 100, 131, 115, 104, 115, 108,
		99, 148
	})]
	public virtual AudioFrameWithPacket getNextAudioFrame()
	{
		Packet audioPkt = inputAudioPacket();
		if (audioPkt == null)
		{
			return null;
		}
		AudioBuffer audioBuffer;
		if (inputAudioCodec.___003C_003Ev2 == Codec.___003C_003EPCM)
		{
			DemuxerTrackMeta audioMeta = getAudioMeta();
			audioBuffer = new AudioBuffer(audioPkt.getData(), audioMeta.getAudioCodecMeta().getFormat(), audioMeta.getTotalFrames());
		}
		else
		{
			audioBuffer = audioDecoder.decodeFrame(audioPkt.getData(), null);
		}
		AudioFrameWithPacket result = new AudioFrameWithPacket(audioBuffer, audioPkt);
		return result;
	}

	[Signature("()Lorg/jcodec/common/Tuple$_3<Ljava/lang/Integer;Ljava/lang/Integer;Lorg/jcodec/common/Codec;>;")]
	[LineNumberTable(497)]
	public virtual Tuple._3 getIntputVideoCodec()
	{
		return inputVideoCodec;
	}

	[Signature("()Lorg/jcodec/common/Tuple$_3<Ljava/lang/Integer;Ljava/lang/Integer;Lorg/jcodec/common/Codec;>;")]
	[LineNumberTable(501)]
	public virtual Tuple._3 getInputAudioCode()
	{
		return inputAudioCodec;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 16, 130, 105, 114 })]
	public virtual void setOption(Options option, object value)
	{
		if (option == Options.___003C_003EDOWNSCALE)
		{
			downscale = ((Integer)value).intValue();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 14, 66, 124, 109, 148 })]
	public virtual AudioCodecMeta getAudioCodecMeta()
	{
		if (audioInputTrack != null && audioInputTrack.getMeta() != null && audioInputTrack.getMeta().getAudioCodecMeta() != null)
		{
			AudioCodecMeta result = audioInputTrack.getMeta().getAudioCodecMeta();
			return result;
		}
		return audioCodecMeta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 12, 98, 110, 99, 109 })]
	public virtual bool isVideo()
	{
		if (!inputFormat.isVideo())
		{
			return false;
		}
		List tracks = demuxVideo.getVideoTracks();
		return (tracks != null && tracks.size() > 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 10, 98, 110, 99, 109 })]
	public virtual bool isAudio()
	{
		if (!inputFormat.isAudio())
		{
			return false;
		}
		List tracks = demuxAudio.getAudioTracks();
		return (tracks != null && tracks.size() > 0) ? true : false;
	}
}
