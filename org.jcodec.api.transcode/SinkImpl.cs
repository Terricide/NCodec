using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.png;
using org.jcodec.codecs.prores;
using org.jcodec.codecs.raw;
using org.jcodec.codecs.vpx;
using org.jcodec.codecs.wav;
using org.jcodec.codecs.y4m;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.containers.imgseq;
using org.jcodec.containers.mkv.muxer;
using org.jcodec.containers.mp4.muxer;
using org.jcodec.containers.raw;

namespace org.jcodec.api.transcode;

[Implements(new string[] { "org.jcodec.api.transcode.Sink", "org.jcodec.api.transcode.PacketSink" })]
public class SinkImpl : Object, Sink, PacketSink
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : Object
	{
		_1()
		{
			throw null;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class RawAudioEncoder : Object, AudioEncoder
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Synthetic)]
		[LineNumberTable(174)]
		internal RawAudioEncoder(_1 x0)
			: this()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(174)]
		private RawAudioEncoder()
		{
		}

		[LineNumberTable(177)]
		public virtual ByteBuffer encode(ByteBuffer audioPkt, ByteBuffer buf)
		{
			return audioPkt;
		}
	}

	private string destName;

	private SeekableByteChannel destStream;

	private Muxer muxer;

	private MuxerTrack videoOutputTrack;

	private MuxerTrack audioOutputTrack;

	private bool framesOutput;

	private Codec outputVideoCodec;

	private Codec outputAudioCodec;

	private Format outputFormat;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[Signature("Ljava/lang/ThreadLocal<Ljava/nio/ByteBuffer;>;")]
	private ThreadLocal bufferStore;

	private AudioEncoder audioEncoder;

	private VideoEncoder videoEncoder;

	private string profile;

	private bool interlaced;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 162, 107, 104 })]
	public static SinkImpl createWithStream(SeekableByteChannel destStream, Format outputFormat, Codec outputVideoCodec, Codec outputAudioCodec)
	{
		SinkImpl result = new SinkImpl(null, outputFormat, outputVideoCodec, outputAudioCodec);
		result.destStream = destStream;
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 105, 108, 113, 104, 104, 104, 105,
		104, 108
	})]
	public SinkImpl(string destName, Format outputFormat, Codec outputVideoCodec, Codec outputAudioCodec)
	{
		if (destName == null && outputFormat == Format.___003C_003EIMG)
		{
			
			throw new IllegalArgumentException("A destination file should be specified for the image muxer.");
		}
		this.destName = destName;
		this.outputFormat = outputFormat;
		this.outputVideoCodec = outputVideoCodec;
		this.outputAudioCodec = outputAudioCodec;
		this.outputFormat = outputFormat;
		bufferStore = new ThreadLocal();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 162, 118, 114, 110, 124, 110, 119, 110,
		119, 110, 119, 110, 119, 110, 121, 110, 148, 159,
		22
	})]
	public virtual void initMuxer()
	{
		if (destStream == null && outputFormat != Format.___003C_003EIMG)
		{
			destStream = NIOUtils.writableFileChannel(destName);
		}
		if (Format.___003C_003EMKV == outputFormat)
		{
			MKVMuxer.___003Cclinit_003E();
			muxer = new MKVMuxer(destStream);
			return;
		}
		if (Format.___003C_003EMOV == outputFormat)
		{
			muxer = MP4Muxer.createMP4MuxerToChannel(destStream);
			return;
		}
		if (Format.___003C_003EIVF == outputFormat)
		{
			muxer = new IVFMuxer(destStream);
			return;
		}
		if (Format.___003C_003EIMG == outputFormat)
		{
			muxer = new ImageSequenceMuxer(destName);
			return;
		}
		if (Format.___003C_003EWAV == outputFormat)
		{
			muxer = new WavMuxer(destStream);
			return;
		}
		if (Format.___003C_003EY4M == outputFormat)
		{
			Y4MMuxer.___003Cclinit_003E();
			muxer = new Y4MMuxer(destStream);
			return;
		}
		if (Format.___003C_003ERAW == outputFormat)
		{
			muxer = new RawMuxer(destStream);
			return;
		}
		string message = new StringBuilder().append("The output format ").append(outputFormat).append(" is not supported.")
			.toString();
		
		throw new RuntimeException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 100, 66, 105, 145 })]
	private AudioEncoder createAudioEncoder(Codec codec, AudioFormat format)
	{
		if (codec != Codec.___003C_003EPCM)
		{
			
			throw new RuntimeException("Only PCM audio encoding (RAW audio) is supported.");
		}
		RawAudioEncoder result = new RawAudioEncoder(null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 98, 110, 131 })]
	protected internal virtual VideoEncoder.EncodedFrame encodeVideo(Picture frame, ByteBuffer _out)
	{
		if (!outputFormat.isVideo())
		{
			return null;
		}
		VideoEncoder.EncodedFrame result = videoEncoder.encodeFrame(frame, _out);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 130, 110, 98, 105, 153, 109, 104 })]
	public virtual void outputVideoPacket(Packet packet, VideoCodecMeta codecMeta)
	{
		if (outputFormat.isVideo())
		{
			if (videoOutputTrack == null)
			{
				videoOutputTrack = muxer.addVideoTrack(outputVideoCodec, codecMeta);
			}
			videoOutputTrack.addFrame(packet);
			framesOutput = true;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 130, 105, 104, 180 })]
	protected internal virtual ByteBuffer encodeAudio(AudioBuffer audioBuffer)
	{
		if (audioEncoder == null)
		{
			AudioFormat format = audioBuffer.getFormat();
			audioEncoder = createAudioEncoder(outputAudioCodec, format);
		}
		ByteBuffer result = audioEncoder.encode(audioBuffer.getData(), null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 122, 98, 110, 98, 105, 153, 109, 104 })]
	public virtual void outputAudioPacket(Packet audioPkt, AudioCodecMeta audioCodecMeta)
	{
		if (outputFormat.isAudio())
		{
			if (audioOutputTrack == null)
			{
				audioOutputTrack = muxer.addAudioTrack(outputAudioCodec, audioCodecMeta);
			}
			audioOutputTrack.addFrame(audioPkt);
			framesOutput = true;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 114, 98, 105, 142, 139, 105, 142 })]
	public virtual void finish()
	{
		if (framesOutput)
		{
			muxer.finish();
		}
		else
		{
			Logger.warn("No frames output.");
		}
		if (destStream != null)
		{
			IOUtils.closeQuietly(destStream);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 107, 130, 103, 124, 110, 125, 110, 113, 110,
		112, 110, 110, 110, 142, 191, 12
	})]
	public virtual void init()
	{
		initMuxer();
		if (!outputFormat.isVideo() || outputVideoCodec == null)
		{
			return;
		}
		if (Codec.___003C_003EPRORES == outputVideoCodec)
		{
			videoEncoder = ProresEncoder.createProresEncoder(profile, interlaced);
			return;
		}
		if (Codec.___003C_003EH264 == outputVideoCodec)
		{
			videoEncoder = H264Encoder.createH264Encoder();
			return;
		}
		if (Codec.___003C_003EVP8 == outputVideoCodec)
		{
			videoEncoder = VP8Encoder.createVP8Encoder(10);
			return;
		}
		if (Codec.___003C_003EPNG == outputVideoCodec)
		{
			videoEncoder = new PNGEncoder();
			return;
		}
		if (Codec.___003C_003ERAW == outputVideoCodec)
		{
			videoEncoder = new RAWVideoEncoder();
			return;
		}
		string message = new StringBuilder().append("Could not find encoder for the codec: ").append(outputVideoCodec).toString();
		
		throw new RuntimeException(message);
	}

	[LineNumberTable(new byte[] { 159, 95, 162, 104 })]
	public virtual void setProfile(string profile)
	{
		this.profile = profile;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 162, 109 })]
	public virtual void setInterlaced(Boolean interlaced)
	{
		this.interlaced = interlaced.booleanValue();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 92, 66, 118, 130, 114, 120, 109, 104, 141,
		104, 109, 106, 121, 124, 101, 60, 136
	})]
	public virtual void outputVideoFrame(VideoFrameWithPacket videoFrame)
	{
		if (outputFormat.isVideo() && outputVideoCodec != null)
		{
			ByteBuffer buffer = (ByteBuffer)bufferStore.get();
			int bufferSize = videoEncoder.estimateBufferSize(videoFrame.getFrame().getPicture());
			if (buffer == null || bufferSize < buffer.capacity())
			{
				buffer = ByteBuffer.allocate(bufferSize);
				bufferStore.set(buffer);
			}
			buffer.clear();
			Picture frame = videoFrame.getFrame().getPicture();
			VideoEncoder.EncodedFrame enc = encodeVideo(frame, buffer);
			Packet outputVideoPacket = Packet.createPacketWithData(videoFrame.getPacket(), NIOUtils.clone(enc.getData()));
			outputVideoPacket.setFrameType((!enc.isKeyFrame()) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY);
			this.outputVideoPacket(outputVideoPacket, VideoCodecMeta.createSimpleVideoCodecMeta(new Size(frame.getWidth(), frame.getHeight()), frame.getColor()));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 87, 66, 118, 98, 122, 48, 136 })]
	public virtual void outputAudioFrame(AudioFrameWithPacket audioFrame)
	{
		if (outputFormat.isAudio() && outputAudioCodec != null)
		{
			outputAudioPacket(Packet.createPacketWithData(audioFrame.getPacket(), encodeAudio(audioFrame.getAudio())), AudioCodecMeta.fromAudioFormat(audioFrame.getAudio().getFormat()));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 85, 66, 105, 113, 109 })]
	public virtual ColorSpace getInputColor()
	{
		if (videoEncoder == null)
		{
			
			throw new IllegalStateException("Video encoder has not been initialized, init() must be called before using this class.");
		}
		ColorSpace[] colorSpaces = videoEncoder.getSupportedColorSpaces();
		return (colorSpaces != null) ? colorSpaces[0] : null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 83, 66, 105, 111, 105, 114 })]
	public virtual void setOption(Options option, object value)
	{
		if (option == Options.___003C_003EPROFILE)
		{
			profile = (string)value;
		}
		else if (option == Options.___003C_003EINTERLACED)
		{
			interlaced = ((Boolean)value).booleanValue();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(244)]
	public virtual bool isVideo()
	{
		bool result = outputFormat.isVideo();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(249)]
	public virtual bool isAudio()
	{
		bool result = outputFormat.isAudio();
		
		return result;
	}
}
