using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.util;
using org.jcodec.api.transcode.filters;
using org.jcodec.common;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.api.transcode;

public class TranscodeMain : java.lang.Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_INPUT;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_MAP_VIDEO;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_MAP_AUDIO;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_SEEK_FRAMES;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_MAX_FRAMES;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_AUDIO_CODEC;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_VIDEO_CODEC;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_FORMAT;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_PROFILE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_INTERLACED;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_DUMPMV;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_DUMPMVJS;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_DOWNSCALE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_VIDEO_FILTER;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag[] ALL_FLAGS;

	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/common/Format;>;")]
	private static Map extensionToF;

	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/common/Codec;>;")]
	private static Map extensionToC;

	[Signature("Ljava/util/Map<Lorg/jcodec/common/Format;Lorg/jcodec/common/Codec;>;")]
	private static Map videoCodecsForF;

	[Signature("Ljava/util/Map<Lorg/jcodec/common/Format;Lorg/jcodec/common/Codec;>;")]
	private static Map audioCodecsForF;

	[Signature("Ljava/util/Set<Lorg/jcodec/common/Codec;>;")]
	private static Set supportedDecoders;

	[Signature("Ljava/util/Map<Ljava/lang/String;Ljava/lang/Class<+Lorg/jcodec/api/transcode/Filter;>;>;")]
	private static Map knownFilters;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 31, 130, 114 })]
	private static Format getFormatFromExtension(string output)
	{
		string extension = java.lang.String.instancehelper_replaceFirst(output, ".*\\.([^\\.]+$)", "$1");
		return (Format)extensionToF.get(extension);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 30, 162, 114 })]
	private static Codec getCodecFromExtension(string output)
	{
		string extension = java.lang.String.instancehelper_replaceFirst(output, ".*\\.([^\\.]+$)", "$1");
		return (Codec)extensionToC.get(extension);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/lang/String;Lorg/jcodec/common/Format;Lorg/jcodec/common/TrackType;)Lorg/jcodec/common/Tuple$_3<Ljava/lang/Integer;Ljava/lang/Integer;Lorg/jcodec/common/Codec;>;")]
	[LineNumberTable(new byte[]
	{
		159, 36, 98, 105, 144, 153, 108, 99, 99, 127,
		7, 103, 125, 106, 111, 151, 101, 99
	})]
	private static org.jcodec.common.Tuple._3 selectSuitableTrack(string input, Format format, TrackType targetType)
	{
		org.jcodec.common.Tuple._2 demuxerPid = ((format != Format.___003C_003EMPEG_TS) ? org.jcodec.common.Tuple.pair(Integer.valueOf(0), JCodecUtil.createDemuxer(format, new File(input))) : JCodecUtil.createM2TSDemuxer(new File(input), targetType));
		if (demuxerPid == null || demuxerPid.___003C_003Ev1 == null)
		{
			return null;
		}
		int trackNo = 0;
		List tracks = ((targetType != TrackType.___003C_003EVIDEO) ? ((Demuxer)demuxerPid.___003C_003Ev1).getAudioTracks() : ((Demuxer)demuxerPid.___003C_003Ev1).getVideoTracks());
		Iterator iterator = tracks.iterator();
		while (iterator.hasNext())
		{
			DemuxerTrack demuxerTrack = (DemuxerTrack)iterator.next();
			Codec codec = detectVideoDecoder(demuxerTrack);
			if (supportedDecoders.contains(codec))
			{
				org.jcodec.common.Tuple._3 result = org.jcodec.common.Tuple.triple(demuxerPid.___003C_003Ev0, Integer.valueOf(trackNo), codec);
				
				return result;
			}
			trackNo++;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(405)]
	private static Codec getFirstVideoCodecForFormat(Format inputFormat)
	{
		return (Codec)videoCodecsForF.get(inputFormat);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(401)]
	private static Codec getFirstAudioCodecForFormat(Format inputFormat)
	{
		return (Codec)audioCodecsForF.get(inputFormat);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		50,
		162,
		100,
		98,
		125,
		110,
		103,
		116,
		101,
		127,
		2,
		159,
		8,
		106,
		103,
		111,
		107,
		107,
		53,
		201,
		113,
		byte.MaxValue,
		5,
		69,
		227,
		60,
		99,
		127,
		27,
		104,
		238,
		43,
		234,
		89
	})]
	private static void addVideoFilters(string vf, Transcoder.TranscoderBuilder builder, int sinkIndex)
	{
		if (vf == null)
		{
			return;
		}
		string[] array = java.lang.String.instancehelper_split(vf, ",");
		int num = array.Length;
		for (int j = 0; j < num; j++)
		{
			string filter = array[j];
			string[] parts = java.lang.String.instancehelper_split(filter, "=");
			string filterName = parts[0];
			Class filterClass = (Class)knownFilters.get(filterName);
			if (filterClass == null)
			{
				Logger.error(new StringBuilder().append("Unknown filter: ").append(filterName).toString());
				string message2 = new StringBuilder().append("Unknown filter: ").append(filterName).toString();
				
				throw new RuntimeException(message2);
			}
			if ((nint)parts.LongLength <= 1)
			{
				continue;
			}
			string filterArgs = parts[1];
			string[] split = java.lang.String.instancehelper_split(filterArgs, ":");
			Integer[] @params = new Integer[(nint)split.LongLength];
			for (int i = 0; i < (nint)split.LongLength; i++)
			{
				@params[i] = Integer.valueOf(Integer.parseInt(split[i]));
			}
			java.lang.Exception ex2;
			try
			{
				Filter f = (Filter)Platform.newInstance(filterClass, @params);
				builder.addFilter(sinkIndex, f);
			}
			catch (System.Exception x)
			{
				java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
				if (ex == null)
				{
					throw;
				}
				ex2 = ex;
				goto IL_011f;
			}
			continue;
			IL_011f:
			java.lang.Exception e = ex2;
			string message = new StringBuilder().append("The filter ").append(filterName).append(" doesn't take ")
				.append(split.Length)
				.append(" arguments.")
				.toString();
			Logger.error(message);
			
			throw new RuntimeException(message);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 40, 98, 104, 100, 104, 100, 131, 104, 100,
		131
	})]
	private static Codec detectVideoDecoder(DemuxerTrack track)
	{
		DemuxerTrackMeta meta = track.getMeta();
		if (meta != null)
		{
			Codec codec = meta.getCodec();
			if (codec != null)
			{
				return codec;
			}
		}
		Packet packet = track.nextFrame();
		if (packet == null)
		{
			return null;
		}
		Codec result = JCodecUtil.detectDecoder(packet.getData());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(48)]
	public TranscodeMain()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159,
		97,
		66,
		159,
		5,
		141,
		135,
		103,
		103,
		136,
		113,
		117,
		102,
		103,
		139,
		144,
		101,
		106,
		106,
		111,
		101,
		133,
		131,
		143,
		101,
		109,
		130,
		191,
		11,
		100,
		112,
		101,
		106,
		125,
		106,
		179,
		191,
		1,
		101,
		106,
		159,
		28,
		223,
		6,
		112,
		101,
		106,
		179,
		191,
		1,
		101,
		106,
		191,
		28,
		223,
		6,
		112,
		118,
		127,
		0,
		191,
		17,
		143,
		122,
		127,
		1,
		106,
		106,
		107,
		106,
		127,
		9,
		byte.MaxValue,
		11,
		159,
		177,
		236,
		160,
		82,
		105,
		127,
		4,
		194,
		104,
		113,
		117,
		102,
		107,
		144,
		101,
		140,
		143,
		112,
		100,
		100,
		101,
		106,
		101,
		140,
		111,
		102,
		111,
		134,
		207,
		112,
		100,
		100,
		101,
		106,
		140,
		111,
		102,
		111,
		134,
		207,
		123,
		107,
		127,
		3,
		58,
		166,
		123,
		107,
		127,
		3,
		58,
		166,
		101,
		112,
		150,
		101,
		113,
		182,
		112,
		107,
		106,
		117,
		181,
		117,
		121,
		117,
		151,
		112,
		101,
		242,
		159,
		179,
		236,
		160,
		81,
		114,
		127,
		4,
		162,
		137,
		106
	})]
	public static void main(string[] args)
	{
		OutLogSink.___003Cclinit_003E();
		Logger.addSink(new OutLogSink(java.lang.System.@out, new OutLogSink.SimpleFormat("#message"), LogLevel.___003C_003EINFO));
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, ALL_FLAGS);
		Transcoder.TranscoderBuilder builder = Transcoder.newTranscoder();
		ArrayList sources = new ArrayList();
		ArrayList inputCodecsVideo = new ArrayList();
		ArrayList inputCodecsAudio = new ArrayList();
		for (int index2 = 0; index2 < cmd.argsLength(); index2++)
		{
			if (!cmd.getBooleanFlagI(index2, FLAG_INPUT).booleanValue())
			{
				continue;
			}
			org.jcodec.common.Tuple._3 inputCodecVideo2 = null;
			org.jcodec.common.Tuple._3 inputCodecAudio2 = null;
			string input = cmd.getArg(index2);
			string inputFormatRaw = cmd.getStringFlagI(index2, FLAG_FORMAT);
			Format inputFormat;
			if (inputFormatRaw == null)
			{
				inputFormat = getFormatFromExtension(input);
				if (inputFormat != Format.___003C_003EIMG)
				{
					Format detectFormat = JCodecUtil.detectFormat(new File(input));
					if (detectFormat != null)
					{
						inputFormat = detectFormat;
					}
				}
			}
			else
			{
				inputFormat = Format.valueOf(java.lang.String.instancehelper_toUpperCase(inputFormatRaw));
			}
			if (inputFormat == null)
			{
				Logger.error("Input format could not be detected");
				return;
			}
			Logger.info(java.lang.String.format("Input stream %d: %s", Integer.valueOf(index2), java.lang.String.valueOf(inputFormat)));
			int videoTrackNo = -1;
			string inputCodecVideoRaw = cmd.getStringFlagI(index2, FLAG_VIDEO_CODEC);
			if (inputCodecVideoRaw == null)
			{
				if (inputFormat == Format.___003C_003EIMG)
				{
					inputCodecVideo2 = org.jcodec.common.Tuple.triple(Integer.valueOf(0), Integer.valueOf(0), getCodecFromExtension(input));
				}
				else if (inputFormat.isVideo())
				{
					inputCodecVideo2 = selectSuitableTrack(input, inputFormat, TrackType.___003C_003EVIDEO);
				}
			}
			else
			{
				inputCodecVideo2 = org.jcodec.common.Tuple.triple(Integer.valueOf(0), Integer.valueOf(0), Codec.valueOf(java.lang.String.instancehelper_toUpperCase(inputCodecVideoRaw)));
			}
			if (inputCodecVideo2 != null)
			{
				if (inputFormat == Format.___003C_003EMPEG_TS)
				{
					Logger.info(java.lang.String.format("Video codec: %s[pid=%d,stream=%d]", java.lang.String.valueOf(inputCodecVideo2.___003C_003Ev2), inputCodecVideo2.___003C_003Ev0, inputCodecVideo2.___003C_003Ev1));
				}
				else
				{
					Logger.info(java.lang.String.format("Video codec: %s", java.lang.String.valueOf(inputCodecVideo2.___003C_003Ev2)));
				}
			}
			string inputCodecAudioRaw = cmd.getStringFlagI(index2, FLAG_AUDIO_CODEC);
			if (inputCodecAudioRaw == null)
			{
				if (inputFormat.isAudio())
				{
					inputCodecAudio2 = selectSuitableTrack(input, inputFormat, TrackType.___003C_003EAUDIO);
				}
			}
			else
			{
				inputCodecAudio2 = org.jcodec.common.Tuple.triple(Integer.valueOf(0), Integer.valueOf(0), Codec.valueOf(java.lang.String.instancehelper_toUpperCase(inputCodecAudioRaw)));
			}
			if (inputCodecAudio2 != null)
			{
				if (inputFormat == Format.___003C_003EMPEG_TS)
				{
					Logger.info(java.lang.String.format("Audio codec: %s[pid=%d,stream=%d]", java.lang.String.valueOf(inputCodecAudio2.___003C_003Ev2), inputCodecAudio2.___003C_003Ev0, inputCodecAudio2.___003C_003Ev1));
				}
				else
				{
					Logger.info(java.lang.String.format("Audio codec: %s", java.lang.String.valueOf(inputCodecAudio2.___003C_003Ev2)));
				}
			}
			SourceImpl source = new SourceImpl(input, inputFormat, inputCodecVideo2, inputCodecAudio2);
			Integer downscale = cmd.getIntegerFlagID(index2, FLAG_DOWNSCALE, Integer.valueOf(1));
			if (downscale != null && 1 << MathUtil.log2(downscale.intValue()) != downscale.intValue())
			{
				Logger.error(new StringBuilder().append("Only values [2, 4, 8] are supported for ").append(FLAG_DOWNSCALE).append(", the option will have no effect.")
					.toString());
			}
			else
			{
				((Source)source).setOption(Options.___003C_003EDOWNSCALE, (object)downscale);
			}
			((Source)source).setOption(Options.___003C_003EPROFILE, (object)cmd.getStringFlagI(index2, FLAG_PROFILE));
			((Source)source).setOption(Options.___003C_003EINTERLACED, (object)cmd.getBooleanFlagID(index2, FLAG_INTERLACED, java.lang.Boolean.valueOf(b: false)));
			((List)sources).add((object)source);
			((List)inputCodecsVideo).add((object)inputCodecVideo2);
			((List)inputCodecsAudio).add((object)inputCodecAudio2);
			builder.addSource(source);
			builder.setSeekFrames(((List)sources).size() - 1, cmd.getIntegerFlagID(index2, FLAG_SEEK_FRAMES, Integer.valueOf(0)).intValue()).setMaxFrames(((List)sources).size() - 1, cmd.getIntegerFlagID(index2, FLAG_MAX_FRAMES, Integer.valueOf(int.MaxValue)).intValue());
		}
		if (((List)sources).isEmpty())
		{
			MainUtils.printHelpArgs(ALL_FLAGS, new string[2] { "input", "output" });
			return;
		}
		ArrayList sinks = new ArrayList();
		for (int index = 0; index < cmd.argsLength(); index++)
		{
			if (cmd.getBooleanFlagI(index, FLAG_INPUT).booleanValue())
			{
				continue;
			}
			string output = cmd.getArg(index);
			string outputFormatRaw = cmd.getStringFlagI(index, FLAG_FORMAT);
			Format outputFormat = ((outputFormatRaw != null) ? Format.valueOf(java.lang.String.instancehelper_toUpperCase(outputFormatRaw)) : getFormatFromExtension(output));
			string outputCodecVideoRaw = cmd.getStringFlagI(index, FLAG_VIDEO_CODEC);
			Codec outputCodecVideo = null;
			int videoCopy = 0;
			if (outputCodecVideoRaw == null)
			{
				outputCodecVideo = getCodecFromExtension(output);
				if (outputCodecVideo == null)
				{
					outputCodecVideo = getFirstVideoCodecForFormat(outputFormat);
				}
			}
			else if (!java.lang.String.instancehelper_equalsIgnoreCase("copy", outputCodecVideoRaw))
			{
				outputCodecVideo = ((!java.lang.String.instancehelper_equalsIgnoreCase("none", outputCodecVideoRaw)) ? Codec.valueOf(java.lang.String.instancehelper_toUpperCase(outputCodecVideoRaw)) : null);
			}
			else
			{
				videoCopy = 1;
			}
			string outputCodecAudioRaw = cmd.getStringFlagI(index, FLAG_AUDIO_CODEC);
			Codec outputCodecAudio = null;
			int audioCopy = 0;
			if (outputCodecAudioRaw == null)
			{
				if (outputFormat.isAudio())
				{
					outputCodecAudio = getFirstAudioCodecForFormat(outputFormat);
				}
			}
			else if (!java.lang.String.instancehelper_equalsIgnoreCase("copy", outputCodecAudioRaw))
			{
				outputCodecAudio = ((!java.lang.String.instancehelper_equalsIgnoreCase("none", outputCodecVideoRaw)) ? Codec.valueOf(java.lang.String.instancehelper_toUpperCase(outputCodecAudioRaw)) : null);
			}
			else
			{
				audioCopy = 1;
			}
			int audioMap = cmd.getIntegerFlagID(index, FLAG_MAP_AUDIO, Integer.valueOf(0)).intValue();
			if (audioMap > ((List)sources).size())
			{
				Logger.error(new StringBuilder().append("Can not map audio from source ").append(audioMap).append(", ")
					.append(((List)sources).size())
					.append(" sources specified.")
					.toString());
			}
			int videoMap = cmd.getIntegerFlagID(index, FLAG_MAP_VIDEO, Integer.valueOf(0)).intValue();
			if (videoMap > ((List)sources).size())
			{
				Logger.error(new StringBuilder().append("Can not map video from source ").append(videoMap).append(", ")
					.append(((List)sources).size())
					.append(" sources specified.")
					.toString());
			}
			if (videoCopy != 0)
			{
				org.jcodec.common.Tuple._3 inputCodecVideo = (org.jcodec.common.Tuple._3)((List)inputCodecsVideo).get(videoMap);
				outputCodecVideo = ((inputCodecVideo == null) ? null : ((Codec)inputCodecVideo.___003C_003Ev2));
			}
			if (audioCopy != 0)
			{
				org.jcodec.common.Tuple._3 inputCodecAudio = (org.jcodec.common.Tuple._3)((List)inputCodecsAudio).get(audioMap);
				outputCodecAudio = ((inputCodecAudio == null) ? null : ((Codec)inputCodecAudio.___003C_003Ev2));
			}
			SinkImpl sink = new SinkImpl(output, outputFormat, outputCodecVideo, outputCodecAudio);
			((List)sinks).add((object)sink);
			builder.addSink(sink);
			builder.setAudioMapping(audioMap, ((List)sinks).size() - 1, (byte)audioCopy != 0);
			builder.setVideoMapping(videoMap, ((List)sinks).size() - 1, (byte)videoCopy != 0);
			if (cmd.getBooleanFlagI(index, FLAG_DUMPMV).booleanValue())
			{
				builder.addFilter(((List)sinks).size() - 1, new DumpMvFilter(js: false));
			}
			else if (cmd.getBooleanFlagI(index, FLAG_DUMPMVJS).booleanValue())
			{
				builder.addFilter(((List)sinks).size() - 1, new DumpMvFilter(js: true));
			}
			string vf = cmd.getStringFlagI(index, FLAG_VIDEO_FILTER);
			if (vf != null)
			{
				addVideoFilters(vf, builder, ((List)sinks).size() - 1);
			}
		}
		if (((List)sources).isEmpty() || ((List)sinks).isEmpty())
		{
			MainUtils.printHelpArgs(ALL_FLAGS, new string[2] { "input", "output" });
		}
		else
		{
			Transcoder transcoder = builder.create();
			transcoder.transcode();
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 130, 98, 127, 0, 154, 154, 118, 154, 122,
		122, 154, 118, 150, 118, 150, 150, 186, 223, 99,
		107, 107, 107, 107, 107, 171, 118, 118, 118, 118,
		118, 118, 118, 118, 118, 118, 150, 118, 150, 118,
		118, 118, 118, 150, 118, 150, 118, 118, 118, 118,
		118, 118, 118, 118, 118, 150, 150, 118, 118, 150,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 150,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118, 118, 118, 150, 113, 113, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 145, 118
	})]
	static TranscodeMain()
	{
		FLAG_INPUT = new MainUtils.Flag("input", "i", "Designates an input argument", MainUtils.FlagType.___003C_003EVOID);
		FLAG_MAP_VIDEO = MainUtils.Flag.flag("map:v", "mv", "Map a video from a specified input into this output");
		FLAG_MAP_AUDIO = MainUtils.Flag.flag("map:a", "ma", "Map a audio from a specified input into this output");
		FLAG_SEEK_FRAMES = MainUtils.Flag.flag("seek-frames", null, "Seek frames");
		FLAG_MAX_FRAMES = MainUtils.Flag.flag("max-frames", "limit", "Max frames");
		FLAG_AUDIO_CODEC = MainUtils.Flag.flag("codec:audio", "acodec", "Audio codec [default=auto].");
		FLAG_VIDEO_CODEC = MainUtils.Flag.flag("codec:video", "vcodec", "Video codec [default=auto].");
		FLAG_FORMAT = MainUtils.Flag.flag("format", "f", "Format [default=auto].");
		FLAG_PROFILE = MainUtils.Flag.flag("profile", null, "Profile to use (supported by some encoders).");
		FLAG_INTERLACED = MainUtils.Flag.flag("interlaced", null, "Encode output as interlaced (supported by Prores encoder).");
		FLAG_DUMPMV = MainUtils.Flag.flag("dumpMv", null, "Dump motion vectors (supported by h.264 decoder).");
		FLAG_DUMPMVJS = MainUtils.Flag.flag("dumpMvJs", null, "Dump motion vectors in form of JASON file (supported by h.264 decoder).");
		FLAG_DOWNSCALE = MainUtils.Flag.flag("downscale", null, "Decode frames in downscale (supported by MPEG, Prores and Jpeg decoders).");
		FLAG_VIDEO_FILTER = MainUtils.Flag.flag("videoFilter", "vf", "Contains a comma separated list of video filters with arguments.");
		ALL_FLAGS = new MainUtils.Flag[14]
		{
			FLAG_INPUT, FLAG_FORMAT, FLAG_VIDEO_CODEC, FLAG_AUDIO_CODEC, FLAG_SEEK_FRAMES, FLAG_MAX_FRAMES, FLAG_PROFILE, FLAG_INTERLACED, FLAG_DUMPMV, FLAG_DUMPMVJS,
			FLAG_DOWNSCALE, FLAG_MAP_VIDEO, FLAG_MAP_AUDIO, FLAG_VIDEO_FILTER
		};
		extensionToF = new HashMap();
		extensionToC = new HashMap();
		videoCodecsForF = new HashMap();
		audioCodecsForF = new HashMap();
		supportedDecoders = new HashSet();
		knownFilters = new HashMap();
		extensionToF.put("mp3", Format.___003C_003EMPEG_AUDIO);
		extensionToF.put("mp2", Format.___003C_003EMPEG_AUDIO);
		extensionToF.put("mp1", Format.___003C_003EMPEG_AUDIO);
		extensionToF.put("mpg", Format.___003C_003EMPEG_PS);
		extensionToF.put("mpeg", Format.___003C_003EMPEG_PS);
		extensionToF.put("m2p", Format.___003C_003EMPEG_PS);
		extensionToF.put("ps", Format.___003C_003EMPEG_PS);
		extensionToF.put("vob", Format.___003C_003EMPEG_PS);
		extensionToF.put("evo", Format.___003C_003EMPEG_PS);
		extensionToF.put("mod", Format.___003C_003EMPEG_PS);
		extensionToF.put("tod", Format.___003C_003EMPEG_PS);
		extensionToF.put("ts", Format.___003C_003EMPEG_TS);
		extensionToF.put("m2t", Format.___003C_003EMPEG_TS);
		extensionToF.put("mp4", Format.___003C_003EMOV);
		extensionToF.put("m4a", Format.___003C_003EMOV);
		extensionToF.put("m4v", Format.___003C_003EMOV);
		extensionToF.put("mov", Format.___003C_003EMOV);
		extensionToF.put("3gp", Format.___003C_003EMOV);
		extensionToF.put("mkv", Format.___003C_003EMKV);
		extensionToF.put("webm", Format.___003C_003EMKV);
		extensionToF.put("264", Format.___003C_003EH264);
		extensionToF.put("jsv", Format.___003C_003EH264);
		extensionToF.put("h264", Format.___003C_003EH264);
		extensionToF.put("raw", Format.___003C_003ERAW);
		extensionToF.put("", Format.___003C_003ERAW);
		extensionToF.put("flv", Format.___003C_003EFLV);
		extensionToF.put("avi", Format.___003C_003EAVI);
		extensionToF.put("jpg", Format.___003C_003EIMG);
		extensionToF.put("jpeg", Format.___003C_003EIMG);
		extensionToF.put("png", Format.___003C_003EIMG);
		extensionToF.put("mjp", Format.___003C_003EMJPEG);
		extensionToF.put("ivf", Format.___003C_003EIVF);
		extensionToF.put("y4m", Format.___003C_003EY4M);
		extensionToF.put("wav", Format.___003C_003EWAV);
		extensionToC.put("mpg", Codec.___003C_003EMPEG2);
		extensionToC.put("mpeg", Codec.___003C_003EMPEG2);
		extensionToC.put("m2p", Codec.___003C_003EMPEG2);
		extensionToC.put("ps", Codec.___003C_003EMPEG2);
		extensionToC.put("vob", Codec.___003C_003EMPEG2);
		extensionToC.put("evo", Codec.___003C_003EMPEG2);
		extensionToC.put("mod", Codec.___003C_003EMPEG2);
		extensionToC.put("tod", Codec.___003C_003EMPEG2);
		extensionToC.put("ts", Codec.___003C_003EMPEG2);
		extensionToC.put("m2t", Codec.___003C_003EMPEG2);
		extensionToC.put("m4a", Codec.___003C_003EAAC);
		extensionToC.put("mkv", Codec.___003C_003EH264);
		extensionToC.put("webm", Codec.___003C_003EVP8);
		extensionToC.put("264", Codec.___003C_003EH264);
		extensionToC.put("raw", Codec.___003C_003ERAW);
		extensionToC.put("jpg", Codec.___003C_003EJPEG);
		extensionToC.put("jpeg", Codec.___003C_003EJPEG);
		extensionToC.put("png", Codec.___003C_003EPNG);
		extensionToC.put("mjp", Codec.___003C_003EJPEG);
		extensionToC.put("y4m", Codec.___003C_003ERAW);
		videoCodecsForF.put(Format.___003C_003EMPEG_PS, Codec.___003C_003EMPEG2);
		audioCodecsForF.put(Format.___003C_003EMPEG_PS, Codec.___003C_003EMP2);
		videoCodecsForF.put(Format.___003C_003EMOV, Codec.___003C_003EH264);
		audioCodecsForF.put(Format.___003C_003EMOV, Codec.___003C_003EAAC);
		videoCodecsForF.put(Format.___003C_003EMKV, Codec.___003C_003EVP8);
		audioCodecsForF.put(Format.___003C_003EMKV, Codec.___003C_003EVORBIS);
		audioCodecsForF.put(Format.___003C_003EWAV, Codec.___003C_003EPCM);
		videoCodecsForF.put(Format.___003C_003EH264, Codec.___003C_003EH264);
		videoCodecsForF.put(Format.___003C_003ERAW, Codec.___003C_003ERAW);
		videoCodecsForF.put(Format.___003C_003EFLV, Codec.___003C_003EH264);
		videoCodecsForF.put(Format.___003C_003EAVI, Codec.___003C_003EMPEG4);
		videoCodecsForF.put(Format.___003C_003EIMG, Codec.___003C_003EPNG);
		videoCodecsForF.put(Format.___003C_003EMJPEG, Codec.___003C_003EJPEG);
		videoCodecsForF.put(Format.___003C_003EIVF, Codec.___003C_003EVP8);
		videoCodecsForF.put(Format.___003C_003EY4M, Codec.___003C_003ERAW);
		supportedDecoders.add(Codec.___003C_003EAAC);
		supportedDecoders.add(Codec.___003C_003EH264);
		supportedDecoders.add(Codec.___003C_003EJPEG);
		supportedDecoders.add(Codec.___003C_003EMPEG2);
		supportedDecoders.add(Codec.___003C_003EPCM);
		supportedDecoders.add(Codec.___003C_003EPNG);
		supportedDecoders.add(Codec.___003C_003EMPEG4);
		supportedDecoders.add(Codec.___003C_003EPRORES);
		supportedDecoders.add(Codec.___003C_003ERAW);
		supportedDecoders.add(Codec.___003C_003EVP8);
		supportedDecoders.add(Codec.___003C_003EMP3);
		supportedDecoders.add(Codec.___003C_003EMP2);
		supportedDecoders.add(Codec.___003C_003EMP1);
		knownFilters.put("scale", ClassLiteral<ScaleFilter>.Value);
	}
}
