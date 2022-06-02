using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using net.sourceforge.jaad.aac;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.codecs.aac;

public class AACDecoder : Object, AudioDecoder
{
	private Decoder decoder;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 135, 162, 105, 106, 104, 100, 137, 139, 114 })]
	public AACDecoder(ByteBuffer decoderSpecific)
	{
		if (decoderSpecific.remaining() >= 7)
		{
			ADTSParser.Header header = ADTSParser.read(decoderSpecific);
			if (header != null)
			{
				decoderSpecific = ADTSParser.adtsToStreamInfo(header);
			}
			Logger.info("Creating AAC decoder from ADTS header.");
		}
		decoder = new Decoder(NIOUtils.toArray(decoderSpecific));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 98, 115, 52 })]
	private AudioFormat toAudioFormat(SampleBuffer sampleBuffer)
	{
		AudioFormat.___003Cclinit_003E();
		AudioFormat result = new AudioFormat(sampleBuffer.getSampleRate(), sampleBuffer.getBitsPerSample(), sampleBuffer.getChannels(), signed: true, sampleBuffer.isBigEndian());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 131, 98, 104, 103, 115, 137, 168 })]
	public virtual AudioBuffer decodeFrame(ByteBuffer frame, ByteBuffer dst)
	{
		ADTSParser.read(frame);
		SampleBuffer sampleBuffer = new SampleBuffer();
		decoder.decodeFrame(NIOUtils.toArray(frame), sampleBuffer);
		if (sampleBuffer.isBigEndian())
		{
			sampleBuffer.setBigEndian(bigEndian: false);
		}
		AudioBuffer result = new AudioBuffer(ByteBuffer.wrap(sampleBuffer.getData()), toAudioFormat(sampleBuffer), 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 127, 162, 103, 115, 136 })]
	public virtual AudioCodecMeta getCodecMeta(ByteBuffer data)
	{
		SampleBuffer sampleBuffer = new SampleBuffer();
		decoder.decodeFrame(NIOUtils.toArray(data), sampleBuffer);
		sampleBuffer.setBigEndian(bigEndian: false);
		AudioCodecMeta result = AudioCodecMeta.fromAudioFormat(toAudioFormat(sampleBuffer));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 66, 106, 99, 104, 100, 100 })]
	public static int probe(ByteBuffer data)
	{
		if (data.remaining() < 7)
		{
			return 0;
		}
		ADTSParser.Header header = ADTSParser.read(data);
		if (header != null)
		{
			return 100;
		}
		return 0;
	}
}
