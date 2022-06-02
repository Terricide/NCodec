using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.mjpeg;
using org.jcodec.codecs.mpeg12;
using org.jcodec.codecs.mpeg4;
using org.jcodec.codecs.ppm;
using org.jcodec.codecs.prores;
using org.jcodec.codecs.vpx;
using org.jcodec.codecs.wav;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;
using org.jcodec.containers.imgseq;
using org.jcodec.containers.mkv.demuxer;
using org.jcodec.containers.mp3;
using org.jcodec.containers.mp4.demuxer;
using org.jcodec.containers.mps;
using org.jcodec.containers.webp;
using org.jcodec.containers.y4m;
using org.jcodec.platform;
using org.jcodec.scale;

namespace org.jcodec.common;

public class JCodecUtil : java.lang.Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Map<Lorg/jcodec/common/Codec;Ljava/lang/Class<*>;>;")]
	private static Map decoders;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Map<Lorg/jcodec/common/Format;Ljava/lang/Class<*>;>;")]
	private static Map demuxers;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 99, 99, 127, 6, 121, 102, 109,
		132, 99
	})]
	public static Format detectFormatBuffer(ByteBuffer b)
	{
		int maxScore = 0;
		Format selected = null;
		Iterator iterator = demuxers.entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry vd = (Map.Entry)iterator.next();
			int score = probe(b.duplicate(), (Class)vd.getValue());
			if (score > maxScore)
			{
				selected = (Format)vd.getKey();
				maxScore = score;
			}
		}
		return selected;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(71)]
	public static Format detectFormat(File f)
	{
		Format result = detectFormatBuffer(NIOUtils.fetchFromFileL(f, 204800));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 99, 99, 127, 6, 121, 102, 109,
		132, 99
	})]
	public static Codec detectDecoder(ByteBuffer b)
	{
		int maxScore = 0;
		Codec selected = null;
		Iterator iterator = decoders.entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry vd = (Map.Entry)iterator.next();
			int score = probe(b.duplicate(), (Class)vd.getValue());
			if (score > maxScore)
			{
				selected = (Codec)vd.getKey();
				maxScore = score;
			}
		}
		return selected;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/io/File;Lorg/jcodec/common/TrackType;)Lorg/jcodec/common/Tuple$_2<Ljava/lang/Integer;Lorg/jcodec/common/Demuxer;>;")]
	[LineNumberTable(new byte[]
	{
		159, 89, 66, 104, 104, 104, 105, 107, 131, 99,
		127, 4, 112, 100, 104, 131, 106, 127, 3, 110,
		107, 159, 30, 136, 102
	})]
	public static Tuple._2 createM2TSDemuxer(File input, TrackType targetTrack)
	{
		FileChannelWrapper ch = NIOUtils.readableChannel(input);
		MTSDemuxer mts = new MTSDemuxer(ch);
		Set programs = mts.getPrograms();
		if (programs.size() == 0)
		{
			Logger.error("The MPEG TS stream contains no programs");
			return null;
		}
		Tuple._2 found = null;
		Iterator iterator = programs.iterator();
		while (iterator.hasNext())
		{
			Integer pid = (Integer)iterator.next();
			ReadableByteChannel program = mts.getProgram(pid.intValue());
			if (found != null)
			{
				program.close();
				continue;
			}
			MPSDemuxer demuxer = new MPSDemuxer(program);
			if ((targetTrack == TrackType.___003C_003EAUDIO && demuxer.getAudioTracks().size() > 0) || (targetTrack == TrackType.___003C_003EVIDEO && demuxer.getVideoTracks().size() > 0))
			{
				found = Tuple.pair(pid, demuxer);
				Logger.info(new StringBuilder().append("Using M2TS program: ").append(pid).append(" for ")
					.append(targetTrack)
					.append(" track.")
					.toString());
			}
			else
			{
				program.close();
			}
		}
		return found;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 97, 162, 99, 105, 136, 105, 106, 105, 106,
		105, 106, 105, 116, 105, 106, 105, 106, 105, 111,
		105, 106, 105, 138, 159, 11
	})]
	public static Demuxer createDemuxer(Format format, File input)
	{
		FileChannelWrapper ch = null;
		if (format != Format.___003C_003EIMG)
		{
			ch = NIOUtils.readableChannel(input);
		}
		if (Format.___003C_003EMOV == format)
		{
			MP4Demuxer result = MP4Demuxer.createMP4Demuxer(ch);
			
			return result;
		}
		if (Format.___003C_003EMPEG_PS == format)
		{
			MPSDemuxer result2 = new MPSDemuxer(ch);
			
			return result2;
		}
		if (Format.___003C_003EMKV == format)
		{
			MKVDemuxer result3 = new MKVDemuxer(ch);
			
			return result3;
		}
		if (Format.___003C_003EIMG == format)
		{
			ImageSequenceDemuxer result4 = new ImageSequenceDemuxer(input.getAbsolutePath(), int.MaxValue);
			
			return result4;
		}
		if (Format.___003C_003EY4M == format)
		{
			Y4MDemuxer result5 = new Y4MDemuxer(ch);
			
			return result5;
		}
		if (Format.___003C_003EWEBP == format)
		{
			WebpDemuxer result6 = new WebpDemuxer(ch);
			
			return result6;
		}
		if (Format.___003C_003EH264 == format)
		{
			BufferH264ES result7 = new BufferH264ES(NIOUtils.fetchAllFromChannel(ch));
			
			return result7;
		}
		if (Format.___003C_003EWAV == format)
		{
			WavDemuxer result8 = new WavDemuxer(ch);
			
			return result8;
		}
		if (Format.___003C_003EMPEG_AUDIO == format)
		{
			MPEGAudioDemuxer result9 = new MPEGAudioDemuxer(ch);
			
			return result9;
		}
		Logger.error(new StringBuilder().append("Format ").append(format).append(" is not supported")
			.toString());
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Ljava/lang/Class<*>;)I")]
	[LineNumberTable(new byte[] { 159, 116, 130, 127, 28, 130 })]
	private static int probe(ByteBuffer b, Class vd)
	{
		//Discarded unreachable code: IL_0023
		java.lang.Exception ex2;
		try
		{
			return ((Integer)Platform.invokeStaticMethod(vd, "probe", new object[1] { b })).intValue();
		}
		catch (System.Exception x)
		{
			java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		java.lang.Exception ex3 = ex2;
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(50)]
	public JCodecUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(75)]
	public static Format detectFormatChannel(ReadableByteChannel f)
	{
		Format result = detectFormatBuffer(NIOUtils.fetchFromChannel(f, 204800));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 98, 127, 28, 104, 105, 110, 137 })]
	public static VideoDecoder getVideoDecoder(string fourcc)
	{
		if (java.lang.String.instancehelper_equals("apch", fourcc) || java.lang.String.instancehelper_equals("apcs", fourcc) || java.lang.String.instancehelper_equals("apco", fourcc) || java.lang.String.instancehelper_equals("apcn", fourcc) || java.lang.String.instancehelper_equals("ap4h", fourcc))
		{
			ProresDecoder result = new ProresDecoder();
			
			return result;
		}
		if (java.lang.String.instancehelper_equals("m2v1", fourcc))
		{
			MPEGDecoder result2 = new MPEGDecoder();
			
			return result2;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 112, 162, 114, 120, 105, 116 })]
	public static void savePictureAsPPM(Picture pic, File file)
	{
		Transform transform = ColorUtil.getTransform(pic.getColor(), ColorSpace.___003C_003ERGB);
		Picture rgb = Picture.create(pic.getWidth(), pic.getHeight(), ColorSpace.___003C_003ERGB);
		transform.transform(pic, rgb);
		NIOUtils.writeTo(new PPMEncoder().encodeFrame(rgb), file);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 130, 104, 105, 104, 40, 167 })]
	public static byte[] asciiString(string fourcc)
	{
		char[] ch = java.lang.String.instancehelper_toCharArray(fourcc);
		byte[] result = new byte[(nint)ch.LongLength];
		for (int i = 0; i < (nint)ch.LongLength; i++)
		{
			result[i] = (byte)(sbyte)ch[i];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 162, 115, 115, 114, 109 })]
	public static void writeBER32(ByteBuffer buffer, int value)
	{
		buffer.put((byte)(sbyte)((uint)(value >> 21) | 0x80u));
		buffer.put((byte)(sbyte)((uint)(value >> 14) | 0x80u));
		buffer.put((byte)(sbyte)((uint)(value >> 7) | 0x80u));
		buffer.put((byte)(sbyte)((uint)value & 0x7Fu));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 130, 114, 102, 104, 101, 105, 234, 59,
		231, 71
	})]
	public static void writeBER32Var(ByteBuffer bb, int value)
	{
		int i = 0;
		int bits = MathUtil.log2(value);
		for (; i < 4; i++)
		{
			if (bits <= 0)
			{
				break;
			}
			bits += -7;
			int @out = value >> bits;
			if (bits > 0)
			{
				@out |= 0x80;
			}
			bb.put((byte)(sbyte)@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 66, 99, 103, 105, 106, 108, 227, 60,
		231, 70
	})]
	public static int readBER32(ByteBuffer input)
	{
		int size = 0;
		for (int i = 0; i < 4; i++)
		{
			int b = (sbyte)input.get();
			size = (size << 7) | (b & 0x7F);
			if ((b & 0xFF) >> 7 == 0)
			{
				break;
			}
		}
		return size;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 162, 104, 104, 105, 104, 39, 167 })]
	public static int[] getAsIntArray(ByteBuffer yuv, int size)
	{
		byte[] b = new byte[size];
		int[] result = new int[size];
		yuv.get(b);
		for (int i = 0; i < (nint)b.LongLength; i++)
		{
			result[i] = b[i];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 98, 98, 100, 99 })]
	public static string removeExtension(string name)
	{
		if (name == null)
		{
			return null;
		}
		string result = java.lang.String.instancehelper_replaceAll(name, "\\.[^\\.]+$", "");
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 83, 162, 105, 138, 159, 11 })]
	public static AudioDecoder createAudioDecoder(Codec codec, ByteBuffer decoderSpecific)
	{
		if (Codec.___003C_003EAAC == codec)
		{
			AACDecoder result = new AACDecoder(decoderSpecific);
			
			return result;
		}
		Logger.error(new StringBuilder().append("Codec ").append(codec).append(" is not supported")
			.toString());
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 66, 105, 148, 105, 105, 105, 105, 105,
		137, 159, 11
	})]
	public static VideoDecoder createVideoDecoder(Codec codec, ByteBuffer decoderSpecific)
	{
		if (Codec.___003C_003EH264 == codec)
		{
			H264Decoder result = ((decoderSpecific == null) ? new H264Decoder() : H264Decoder.createH264DecoderFromCodecPrivate(decoderSpecific));
			
			return result;
		}
		if (Codec.___003C_003EMPEG2 == codec)
		{
			MPEGDecoder result2 = new MPEGDecoder();
			
			return result2;
		}
		if (Codec.___003C_003EVP8 == codec)
		{
			VP8Decoder result3 = new VP8Decoder();
			
			return result3;
		}
		if (Codec.___003C_003EJPEG == codec)
		{
			JpegDecoder result4 = new JpegDecoder();
			
			return result4;
		}
		Logger.error(new StringBuilder().append("Codec ").append(codec).append(" is not supported")
			.toString());
		return null;
	}

	[LineNumberTable(new byte[]
	{
		159, 129, 66, 107, 171, 118, 118, 118, 118, 118,
		150, 118, 118, 118, 118, 118
	})]
	static JCodecUtil()
	{
		decoders = new HashMap();
		demuxers = new HashMap();
		decoders.put(Codec.___003C_003EVP8, ClassLiteral<VP8Decoder>.Value);
		decoders.put(Codec.___003C_003EPRORES, ClassLiteral<ProresDecoder>.Value);
		decoders.put(Codec.___003C_003EMPEG2, ClassLiteral<MPEGDecoder>.Value);
		decoders.put(Codec.___003C_003EH264, ClassLiteral<H264Decoder>.Value);
		decoders.put(Codec.___003C_003EAAC, ClassLiteral<AACDecoder>.Value);
		decoders.put(Codec.___003C_003EMPEG4, ClassLiteral<MPEG4Decoder>.Value);
		demuxers.put(Format.___003C_003EMPEG_TS, ClassLiteral<MTSDemuxer>.Value);
		demuxers.put(Format.___003C_003EMPEG_PS, ClassLiteral<MPSDemuxer>.Value);
		demuxers.put(Format.___003C_003EMOV, ClassLiteral<MP4Demuxer>.Value);
		demuxers.put(Format.___003C_003EWEBP, ClassLiteral<WebpDemuxer>.Value);
		demuxers.put(Format.___003C_003EMPEG_AUDIO, ClassLiteral<MPEGAudioDemuxer>.Value);
	}
}
