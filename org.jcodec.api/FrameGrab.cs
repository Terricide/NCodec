using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.api.specific;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.demuxer;

namespace org.jcodec.api;

public class FrameGrab : Object
{
	private SeekableDemuxerTrack videoTrack;

	private ContainerAdaptor decoder;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[Signature("Ljava/lang/ThreadLocal<[[B>;")]
	private ThreadLocal buffers;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 89, 66, 104, 110, 138 })]
	private static ContainerAdaptor detectDecoder(SeekableDemuxerTrack videoTrack)
	{
		DemuxerTrackMeta meta = videoTrack.getMeta();
		if (Codec.___003C_003EH264 == meta.getCodec())
		{
			AVCMP4Adaptor result = new AVCMP4Adaptor(meta);
			
			return result;
		}
		
		throw new UnsupportedFormatException("Codec is not supported");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 130, 105, 104, 104, 108 })]
	public FrameGrab(SeekableDemuxerTrack videoTrack, ContainerAdaptor decoder)
	{
		this.videoTrack = videoTrack;
		this.decoder = decoder;
		buffers = new ThreadLocal();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[]
	{
		159, 99, 66, 136, 105, 105, 138, 104, 105, 141,
		107, 116, 138, 106
	})]
	private void decodeLeadingFrames()
	{
		SeekableDemuxerTrack sdt = this.sdt();
		int curFrame = (int)sdt.getCurFrame();
		int keyFrame = detectKeyFrame(curFrame);
		sdt.gotoFrame(keyFrame);
		Packet frame = sdt.nextFrame();
		if (decoder == null)
		{
			decoder = detectDecoder(sdt);
		}
		while (frame.getFrameNo() < curFrame)
		{
			decoder.decodeFrame(frame, getBuffer());
			frame = sdt.nextFrame();
		}
		sdt.gotoFrame(curFrame);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 122, 98, 110, 145 })]
	private SeekableDemuxerTrack sdt()
	{
		if (!(videoTrack is SeekableDemuxerTrack))
		{
			
			throw new JCodecException("Not a seekable track");
		}
		return videoTrack;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 100, 66, 127, 1 })]
	private void goToPrevKeyframe()
	{
		sdt().gotoFrame(detectKeyFrame((int)sdt().getCurFrame()));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 93, 162, 114, 100, 99, 101, 104, 103, 99,
		229, 61, 231, 69
	})]
	private int detectKeyFrame(int start)
	{
		int[] seekFrames = videoTrack.getMeta().getSeekFrames();
		if (seekFrames == null)
		{
			return start;
		}
		int prev = seekFrames[0];
		for (int i = 1; i < (nint)seekFrames.LongLength && seekFrames[i] <= start; i++)
		{
			prev = seekFrames[i];
		}
		return prev;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 130, 114, 100, 109, 141 })]
	private byte[][] getBuffer()
	{
		byte[][] buf = (byte[][])buffers.get();
		if (buf == null)
		{
			buf = decoder.allocatePicture();
			buffers.set(buf);
		}
		return buf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 108, 105, 104, 104, 100, 209, 105,
		104, 109, 107, 113, 105, 145, 145, 111, 104
	})]
	public static FrameGrab createFrameGrab(SeekableByteChannel _in)
	{
		ByteBuffer header = ByteBuffer.allocate(65536);
		_in.read(header);
		header.flip();
		Format detectFormat = JCodecUtil.detectFormatBuffer(header);
		if (detectFormat == null)
		{
			
			throw new UnsupportedFormatException("Could not detect the format of the input video.");
		}
		if (Format.___003C_003EMOV == detectFormat)
		{
			MP4Demuxer d1 = MP4Demuxer.createMP4Demuxer(_in);
			SeekableDemuxerTrack videoTrack_ = (SeekableDemuxerTrack)d1.getVideoTrack();
			FrameGrab fg = new FrameGrab(videoTrack_, detectDecoder(videoTrack_));
			fg.decodeLeadingFrames();
			return fg;
		}
		if (Format.___003C_003EMPEG_PS == detectFormat)
		{
			
			throw new UnsupportedFormatException("MPEG PS is temporarily unsupported.");
		}
		if (Format.___003C_003EMPEG_TS == detectFormat)
		{
			
			throw new UnsupportedFormatException("MPEG TS is temporarily unsupported.");
		}
		
		throw new UnsupportedFormatException("Container format is not supported by JCodec");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 117, 130, 110, 103 })]
	public virtual FrameGrab seekToSecondPrecise(double second)
	{
		sdt().seek(second);
		decodeLeadingFrames();
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 82, 130, 109, 100, 131 })]
	public virtual Picture getNativeFrame()
	{
		Packet frame = videoTrack.nextFrame();
		if (frame == null)
		{
			return null;
		}
		Picture result = decoder.decodeFrame(frame, getBuffer());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 112, 130, 111, 103 })]
	public virtual FrameGrab seekToFramePrecise(int frameNumber)
	{
		sdt().gotoFrame(frameNumber);
		decodeLeadingFrames();
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 102, 130, 111, 103 })]
	public virtual FrameGrab seekToFrameSloppy(int frameNumber)
	{
		sdt().gotoFrame(frameNumber);
		goToPrevKeyframe();
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 107, 130, 110, 103 })]
	public virtual FrameGrab seekToSecondSloppy(double second)
	{
		sdt().seek(second);
		goToPrevKeyframe();
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 86, 162, 109, 100, 131, 116 })]
	public virtual PictureWithMetadata getNativeFrameWithMetadata()
	{
		Packet frame = videoTrack.nextFrame();
		if (frame == null)
		{
			return null;
		}
		Picture picture = decoder.decodeFrame(frame, getBuffer());
		PictureWithMetadata result = new PictureWithMetadata(picture, frame.getPtsD(), frame.getDurationD(), videoTrack.getMeta().getOrientation());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 78, 162, 131, 104, 152, 74, 3 })]
	public static Picture getFrameAtSec(File file, double second)
	{
		FileChannelWrapper ch = null;
		try
		{
			ch = NIOUtils.readableChannel(file);
			return createFrameGrab(ch).seekToSecondPrecise(second).getNativeFrame();
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "org.jcodec.api.JCodecException", "java.io.IOException" })]
	[LineNumberTable(279)]
	public static Picture getFrameFromChannelAtSec(SeekableByteChannel file, double second)
	{
		Picture nativeFrame = createFrameGrab(file).seekToSecondPrecise(second).getNativeFrame();
		
		return nativeFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(new byte[] { 159, 69, 66, 131, 104, 151, 74, 3 })]
	public static Picture getFrameFromFile(File file, int frameNumber)
	{
		FileChannelWrapper ch = null;
		try
		{
			ch = NIOUtils.readableChannel(file);
			return createFrameGrab(ch).seekToFramePrecise(frameNumber).getNativeFrame();
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "org.jcodec.api.JCodecException", "java.io.IOException" })]
	[LineNumberTable(312)]
	public static Picture getFrameFromChannel(SeekableByteChannel file, int frameNumber)
	{
		Picture nativeFrame = createFrameGrab(file).seekToFramePrecise(frameNumber).getNativeFrame();
		
		return nativeFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(327)]
	public static Picture getNativeFrameAtFrame(SeekableDemuxerTrack vt, ContainerAdaptor decoder, int frameNumber)
	{
		Picture nativeFrame = new FrameGrab(vt, decoder).seekToFramePrecise(frameNumber).getNativeFrame();
		
		return nativeFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(342)]
	public static Picture getNativeFrameAtSec(SeekableDemuxerTrack vt, ContainerAdaptor decoder, double second)
	{
		Picture nativeFrame = new FrameGrab(vt, decoder).seekToSecondPrecise(second).getNativeFrame();
		
		return nativeFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(358)]
	public static Picture getNativeFrameSloppy(SeekableDemuxerTrack vt, ContainerAdaptor decoder, int frameNumber)
	{
		Picture nativeFrame = new FrameGrab(vt, decoder).seekToFrameSloppy(frameNumber).getNativeFrame();
		
		return nativeFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "org.jcodec.api.JCodecException" })]
	[LineNumberTable(374)]
	public static Picture getNativeFrameAtSecSloppy(SeekableDemuxerTrack vt, ContainerAdaptor decoder, double second)
	{
		Picture nativeFrame = new FrameGrab(vt, decoder).seekToSecondSloppy(second).getNativeFrame();
		
		return nativeFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(383)]
	public virtual MediaInfo getMediaInfo()
	{
		MediaInfo mediaInfo = decoder.getMediaInfo();
		
		return mediaInfo;
	}

	[LineNumberTable(390)]
	public virtual SeekableDemuxerTrack getVideoTrack()
	{
		return videoTrack;
	}

	[LineNumberTable(397)]
	public virtual ContainerAdaptor getDecoder()
	{
		return decoder;
	}
}
