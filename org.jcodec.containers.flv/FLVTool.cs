using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.h264.mp4;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.containers.flv;

public class FLVTool : Object
{
	public class ClipPacketProcessor : Object, PacketProcessor
	{
		public class Factory : Object, PacketProcessorFactory
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(143)]
			public Factory()
			{
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(146)]
			public virtual PacketProcessor newPacketProcessor(MainUtils.Cmd flags)
			{
				___003Cclinit_003E();
				ClipPacketProcessor result = new ClipPacketProcessor(flags.getDoubleFlag(access_0024000()), flags.getDoubleFlag(access_0024100()));
				
				return result;
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(151)]
			public virtual MainUtils.Flag[] getFlags()
			{
				return new MainUtils.Flag[2]
				{
					access_0024000(),
					access_0024100()
				};
			}
		}

		private static FLVTag h264Config;

		private bool copying;

		private Double from;

		private Double to;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static MainUtils.Flag FLAG_FROM;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static MainUtils.Flag FLAG_TO;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(134)]
		internal static MainUtils.Flag access_0024000()
		{
			return FLAG_FROM;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(134)]
		internal static MainUtils.Flag access_0024100()
		{
			return FLAG_TO;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 104, 162, 233, 45, 232, 84, 104, 104 })]
		public ClipPacketProcessor(Double from, Double to)
		{
			copying = false;
			this.from = from;
			this.to = to;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 102, 98, 127, 1, 116, 103, 208, 159, 36,
			127, 11, 104, 113, 172, 124, 127, 11, 131, 105,
			104
		})]
		public virtual bool processPacket(FLVTag pkt, FLVWriter writer)
		{
			if (pkt.getType() == FLVTag.Type.___003C_003EVIDEO && pkt.getTagHeader().getCodec() == Codec.___003C_003EH264 && (sbyte)((FLVTag.AvcVideoTagHeader)pkt.getTagHeader()).getAvcPacketType() == 0)
			{
				h264Config = pkt;
				java.lang.System.@out.println("GOT AVCC");
			}
			if (!copying && (from == null || pkt.getPtsD() > from.doubleValue()) && pkt.getType() == FLVTag.Type.___003C_003EVIDEO && pkt.isKeyFrame() && h264Config != null)
			{
				java.lang.System.@out.println(new StringBuilder().append("Starting at packet: ").append(Platform.toJSON(pkt)).toString());
				copying = true;
				h264Config.setPts(pkt.getPts());
				writer.addPacket(h264Config);
			}
			if (to != null && pkt.getPtsD() >= to.doubleValue())
			{
				java.lang.System.@out.println(new StringBuilder().append("Stopping at packet: ").append(Platform.toJSON(pkt)).toString());
				return false;
			}
			if (copying)
			{
				writer.addPacket(pkt);
			}
			return true;
		}

		[LineNumberTable(187)]
		public virtual void finish(FLVWriter muxer)
		{
		}

		[LineNumberTable(191)]
		public virtual bool hasOutput()
		{
			return true;
		}

		[LineNumberTable(new byte[] { 159, 107, 66, 118 })]
		static ClipPacketProcessor()
		{
			FLAG_FROM = MainUtils.Flag.flag("from", null, "From timestamp (in seconds, i.e 67.49)");
			FLAG_TO = MainUtils.Flag.flag("to", null, "To timestamp");
		}
	}

	public class FixPtsProcessor : Object, PacketProcessor
	{
		public class Factory : Object, PacketProcessorFactory
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(207)]
			public Factory()
			{
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(210)]
			public virtual PacketProcessor newPacketProcessor(MainUtils.Cmd flags)
			{
				FixPtsProcessor result = new FixPtsProcessor();
				
				return result;
			}

			[LineNumberTable(215)]
			public virtual MainUtils.Flag[] getFlags()
			{
				return new MainUtils.Flag[0];
			}
		}

		private double lastPtsAudio;

		private double lastPtsVideo;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/flv/FLVTag;>;")]
		private List tags;

		private int audioTagsInQueue;

		private int videoTagsInQueue;

		private const double CORRECTION_PACE = 0.33;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 88, 162, 233, 45, 109, 237, 83, 108 })]
		public FixPtsProcessor()
		{
			lastPtsAudio = 0.0;
			lastPtsVideo = 0.0;
			tags = new ArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 83, 162, 147, 110, 125, 127, 1, 116, 113,
			127, 3, 125, 127, 18, 58, 146, 111, 127, 22,
			99, 189, 106
		})]
		private void processOneTag(FLVWriter writer)
		{
			FLVTag tag = (FLVTag)tags.remove(0);
			if (tag.getType() == FLVTag.Type.___003C_003EAUDIO)
			{
				tag.setPts((int)Math.round(lastPtsAudio * 1000.0));
				lastPtsAudio += audioFrameDuration((FLVTag.AudioTagHeader)tag.getTagHeader());
				audioTagsInQueue--;
			}
			else if (tag.getType() == FLVTag.Type.___003C_003EVIDEO)
			{
				double duration = 1024.0 * (double)audioTagsInQueue / (double)(48000 * videoTagsInQueue);
				tag.setPts((int)Math.round(lastPtsVideo * 1000.0));
				lastPtsVideo += Math.min(1.33 * duration, Math.max(0.66999999999999993 * duration, duration + Math.min(1.0, Math.abs(lastPtsAudio - lastPtsVideo)) * (lastPtsAudio - lastPtsVideo)));
				videoTagsInQueue--;
				java.lang.System.@out.println(new StringBuilder().append(lastPtsVideo).append(" - ").append(lastPtsAudio)
					.toString());
			}
			else
			{
				tag.setPts((int)Math.round(lastPtsVideo * 1000.0));
			}
			writer.addPacket(tag);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 77, 66, 110, 120, 110, 152 })]
		private double audioFrameDuration(FLVTag.AudioTagHeader audioTagHeader)
		{
			if (Codec.___003C_003EAAC == audioTagHeader.getCodec())
			{
				return 1024.0 / (double)audioTagHeader.getAudioFormat().getSampleRate();
			}
			if (Codec.___003C_003EMP3 == audioTagHeader.getCodec())
			{
				return 1152.0 / (double)audioTagHeader.getAudioFormat().getSampleRate();
			}
			string message = new StringBuilder().append("Audio codec:").append(audioTagHeader.getCodec()).append(" is not supported.")
				.toString();
			
			throw new RuntimeException(message);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 86, 66, 110, 110, 113, 110, 175, 115, 131,
			104
		})]
		public virtual bool processPacket(FLVTag pkt, FLVWriter writer)
		{
			tags.add(pkt);
			if (pkt.getType() == FLVTag.Type.___003C_003EAUDIO)
			{
				audioTagsInQueue++;
			}
			else if (pkt.getType() == FLVTag.Type.___003C_003EVIDEO)
			{
				videoTagsInQueue++;
			}
			if (tags.size() < 600)
			{
				return true;
			}
			processOneTag(writer);
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 75, 130, 111, 138 })]
		public virtual void finish(FLVWriter muxer)
		{
			while (tags.size() > 0)
			{
				processOneTag(muxer);
			}
		}

		[LineNumberTable(277)]
		public virtual bool hasOutput()
		{
			return true;
		}
	}

	public class InfoPacketProcessor : Object, PacketProcessor
	{
		public class Factory : Object, PacketProcessorFactory
		{
			[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
			private static MainUtils.Flag FLAG_CHECK;

			[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
			private static MainUtils.Flag FLAG_STREAM;

			[MethodImpl(MethodImplOptions.NoInlining)]
			[SpecialName]
			public static void ___003Cclinit_003E()
			{
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(289)]
			public Factory()
			{
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(new byte[] { 159, 69, 162, 127, 4, 50 })]
			public virtual PacketProcessor newPacketProcessor(MainUtils.Cmd flags)
			{
				InfoPacketProcessor result = new InfoPacketProcessor(flags.getBooleanFlagD(FLAG_CHECK, Boolean.valueOf(b: false)).booleanValue(), (FLVTag.Type)flags.getEnumFlagD(FLAG_STREAM, null, ClassLiteral<FLVTag.Type>.Value));
				
				return result;
			}

			[LineNumberTable(301)]
			public virtual MainUtils.Flag[] getFlags()
			{
				return new MainUtils.Flag[2] { FLAG_CHECK, FLAG_STREAM };
			}

			[LineNumberTable(new byte[] { 159, 70, 130, 118 })]
			static Factory()
			{
				FLAG_CHECK = MainUtils.Flag.flag("check", null, "Check sanity and report errors only, no packet dump will be generated.");
				FLAG_STREAM = MainUtils.Flag.flag("stream", null, "Stream selector, can be one of: ['video', 'audio', 'script'].");
			}
		}

		private FLVTag prevVideoTag;

		private FLVTag prevAudioTag;

		private bool checkOnly;

		private FLVTag.Type streamType;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 65, 65, 67, 105, 104, 104 })]
		public InfoPacketProcessor(bool checkOnly, FLVTag.Type streamType)
		{
			this.checkOnly = checkOnly;
			this.streamType = streamType;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 58, 98, 127, 55, 63, 20, 134, 113, 109,
			127, 32, 108, 104, 127, 33, 109, 109, 104, 104,
			127, 11, 107, 127, 34, 99, 127, 11, 107, 127,
			34, 163, 118, 110, 106, 127, 36, 63, 9, 134,
			112, 115, 101, 107, 191, 12, 109
		})]
		private void dumpOnePacket(FLVTag pkt, int duration)
		{
			java.lang.System.@out.print(new StringBuilder().append("T=").append(typeString(pkt.getType())).append("|PTS=")
				.append(pkt.getPts())
				.append("|DUR=")
				.append(duration)
				.append("|")
				.append((!pkt.isKeyFrame()) ? " " : "K")
				.append("|POS=")
				.append(pkt.getPosition())
				.toString());
			if (pkt.getTagHeader() is FLVTag.VideoTagHeader)
			{
				FLVTag.VideoTagHeader vt = (FLVTag.VideoTagHeader)pkt.getTagHeader();
				java.lang.System.@out.print(new StringBuilder().append("|C=").append(vt.getCodec()).append("|FT=")
					.append(vt.getFrameType())
					.toString());
				if (vt is FLVTag.AvcVideoTagHeader)
				{
					FLVTag.AvcVideoTagHeader avct = (FLVTag.AvcVideoTagHeader)vt;
					java.lang.System.@out.print(new StringBuilder().append("|PKT_TYPE=").append((sbyte)avct.getAvcPacketType()).append("|COMP_OFF=")
						.append(avct.getCompOffset())
						.toString());
					if ((sbyte)avct.getAvcPacketType() == 0)
					{
						ByteBuffer frameData = pkt.getData().duplicate();
						FLVReader.parseVideoTagHeader(frameData);
						AvcCBox avcc = H264Utils.parseAVCCFromBuffer(frameData);
						Iterator iterator = H264Utils.readSPSFromBufferList(avcc.getSpsList()).iterator();
						while (iterator.hasNext())
						{
							SeqParameterSet sps = (SeqParameterSet)iterator.next();
							java.lang.System.@out.println();
							java.lang.System.@out.print(new StringBuilder().append("  SPS[").append(sps.getSeqParameterSetId()).append("]:")
								.append(Platform.toJSON(sps))
								.toString());
						}
						Iterator iterator2 = H264Utils.readPPSFromBufferList(avcc.getPpsList()).iterator();
						while (iterator2.hasNext())
						{
							PictureParameterSet pps = (PictureParameterSet)iterator2.next();
							java.lang.System.@out.println();
							java.lang.System.@out.print(new StringBuilder().append("  PPS[").append(pps.getPicParameterSetId()).append("]:")
								.append(Platform.toJSON(pps))
								.toString());
						}
					}
				}
			}
			else if (pkt.getTagHeader() is FLVTag.AudioTagHeader)
			{
				FLVTag.AudioTagHeader at = (FLVTag.AudioTagHeader)pkt.getTagHeader();
				AudioFormat format = at.getAudioFormat();
				java.lang.System.@out.print(new StringBuilder().append("|C=").append(at.getCodec()).append("|SR=")
					.append(format.getSampleRate())
					.append("|SS=")
					.append(format.getSampleSizeInBits() >> 3)
					.append("|CH=")
					.append(format.getChannels())
					.toString());
			}
			else if (pkt.getType() == FLVTag.Type.___003C_003ESCRIPT)
			{
				FLVMetadata metadata = FLVReader.parseMetadata(pkt.getData().duplicate());
				if (metadata != null)
				{
					java.lang.System.@out.println();
					java.lang.System.@out.print(new StringBuilder().append("  Metadata:").append(Platform.toJSON(metadata)).toString());
				}
			}
			java.lang.System.@out.println();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(375)]
		private string typeString(FLVTag.Type type)
		{
			string result = String.instancehelper_substring(type.toString(), 0, 1);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 64, 162, 105, 99, 110, 121, 105, 127, 0,
			138, 110, 118, 105, 127, 0, 170, 169
		})]
		public virtual bool processPacket(FLVTag pkt, FLVWriter writer)
		{
			if (checkOnly)
			{
				return true;
			}
			if (pkt.getType() == FLVTag.Type.___003C_003EVIDEO)
			{
				if (streamType == FLVTag.Type.___003C_003EVIDEO || streamType == null)
				{
					if (prevVideoTag != null)
					{
						dumpOnePacket(prevVideoTag, pkt.getPts() - prevVideoTag.getPts());
					}
					prevVideoTag = pkt;
				}
			}
			else if (pkt.getType() == FLVTag.Type.___003C_003EAUDIO)
			{
				if (streamType == FLVTag.Type.___003C_003EAUDIO || streamType == null)
				{
					if (prevAudioTag != null)
					{
						dumpOnePacket(prevAudioTag, pkt.getPts() - prevAudioTag.getPts());
					}
					prevAudioTag = pkt;
				}
			}
			else
			{
				dumpOnePacket(pkt, 0);
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 47, 66, 105, 110, 105, 112 })]
		public virtual void finish(FLVWriter muxer)
		{
			if (prevVideoTag != null)
			{
				dumpOnePacket(prevVideoTag, 0);
			}
			if (prevAudioTag != null)
			{
				dumpOnePacket(prevAudioTag, 0);
			}
		}

		[LineNumberTable(388)]
		public virtual bool hasOutput()
		{
			return false;
		}
	}

	public interface PacketProcessor
	{
		[Throws(new string[] { "java.io.IOException" })]
		bool processPacket(FLVTag flvt, FLVWriter flvw);

		bool hasOutput();

		[Throws(new string[] { "java.io.IOException" })]
		void finish(FLVWriter flvw);
	}

	public interface PacketProcessorFactory
	{
		PacketProcessor newPacketProcessor(MainUtils.Cmd muc);

		MainUtils.Flag[] getFlags();
	}

	public class ShiftPtsProcessor : Object, PacketProcessor
	{
		public class Factory : Object, PacketProcessorFactory
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(405)]
			public Factory()
			{
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(new byte[] { 159, 40, 66, 127, 9, 61 })]
			public virtual PacketProcessor newPacketProcessor(MainUtils.Cmd flags)
			{
				___003Cclinit_003E();
				ShiftPtsProcessor result = new ShiftPtsProcessor(flags.getIntegerFlagD(access_0024200(), Integer.valueOf(0)).intValue(), flags.getIntegerFlag(access_0024300()), flags.getBooleanFlagD(access_0024400(), Boolean.valueOf(b: false)).booleanValue());
				
				return result;
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(414)]
			public virtual MainUtils.Flag[] getFlags()
			{
				return new MainUtils.Flag[3]
				{
					access_0024200(),
					access_0024300(),
					access_0024400()
				};
			}
		}

		private const long WRAP_AROUND_VALUE = 2147483648L;

		private const int HALF_WRAP_AROUND_VALUE = 1073741824;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static MainUtils.Flag FLAG_TO;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static MainUtils.Flag FLAG_BY;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static MainUtils.Flag FLAG_WRAP_AROUND;

		private int shiftTo;

		private Integer shiftBy;

		private long ptsDelta;

		private bool firstPtsSeen;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/flv/FLVTag;>;")]
		private List savedTags;

		private bool expectWrapAround;

		private int prevPts;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(396)]
		internal static MainUtils.Flag access_0024200()
		{
			return FLAG_TO;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(396)]
		internal static MainUtils.Flag access_0024300()
		{
			return FLAG_BY;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(396)]
		internal static MainUtils.Flag access_0024400()
		{
			return FLAG_WRAP_AROUND;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 36, 130, 105, 108, 104, 104, 104 })]
		public ShiftPtsProcessor(int shiftTo, Integer shiftBy, bool expectWrapAround)
		{
			savedTags = new LinkedList();
			this.shiftTo = shiftTo;
			this.shiftBy = shiftBy;
			this.expectWrapAround = true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 23, 130, 112, 102, 127, 6, 105, 134, 107,
			106, 127, 6, 106, 144, 105, 106
		})]
		private void writePacket(FLVTag pkt, FLVWriter writer)
		{
			long newPts = pkt.getPts() + ptsDelta;
			if (newPts < 0u)
			{
				Logger.warn(new StringBuilder().append("Preventing negative pts for tag @").append(pkt.getPosition()).toString());
				newPts = ((shiftBy == null) ? ((long)shiftTo) : 0L);
			}
			else if (newPts >= 2147483648u)
			{
				Logger.warn(new StringBuilder().append("PTS wrap around @").append(pkt.getPosition()).toString());
				newPts -= 2147483648u;
				ptsDelta = newPts - pkt.getPts();
			}
			pkt.setPts((int)newPts);
			writer.addPacket(pkt);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 19, 162, 111, 155 })]
		private void emptySavedTags(FLVWriter muxer)
		{
			while (savedTags.size() > 0)
			{
				writePacket((FLVTag)savedTags.remove(0), muxer);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 34, 130, 111, 120, 120, 111, 120, 151, 121,
			127, 12, 115, 159, 27, 110, 121, 106, 180, 100,
			141, 105, 142, 100, 144, 105, 115, 115, 145, 149,
			104, 104, 201
		})]
		public virtual bool processPacket(FLVTag pkt, FLVWriter writer)
		{
			int avcPrivatePacket = ((pkt.getType() == FLVTag.Type.___003C_003EVIDEO && ((FLVTag.VideoTagHeader)pkt.getTagHeader()).getCodec() == Codec.___003C_003EH264 && (sbyte)((FLVTag.AvcVideoTagHeader)pkt.getTagHeader()).getAvcPacketType() == 0) ? 1 : 0);
			int aacPrivatePacket = ((pkt.getType() == FLVTag.Type.___003C_003EAUDIO && ((FLVTag.AudioTagHeader)pkt.getTagHeader()).getCodec() == Codec.___003C_003EAAC && ((FLVTag.AacAudioTagHeader)pkt.getTagHeader()).getPacketType() == 0) ? 1 : 0);
			int validPkt = ((pkt.getType() != FLVTag.Type.___003C_003ESCRIPT && avcPrivatePacket == 0 && aacPrivatePacket == 0) ? 1 : 0);
			if (expectWrapAround && validPkt != 0 && pkt.getPts() < prevPts && (long)prevPts - (long)pkt.getPts() > 1073741824u)
			{
				Logger.warn(new StringBuilder().append("Wrap around detected: ").append(prevPts).append(" -> ")
					.append(pkt.getPts())
					.toString());
				if (pkt.getPts() < -1073741824)
				{
					ptsDelta += 4294967296L;
				}
				else if (pkt.getPts() >= 0)
				{
					ptsDelta += 2147483648L;
				}
			}
			if (validPkt != 0)
			{
				prevPts = pkt.getPts();
			}
			if (firstPtsSeen)
			{
				writePacket(pkt, writer);
			}
			else if (validPkt == 0)
			{
				savedTags.add(pkt);
			}
			else
			{
				if (shiftBy != null)
				{
					ptsDelta = shiftBy.intValue();
					if (ptsDelta + pkt.getPts() < 0u)
					{
						ptsDelta = -pkt.getPts();
					}
				}
				else
				{
					ptsDelta = shiftTo - pkt.getPts();
				}
				firstPtsSeen = true;
				emptySavedTags(writer);
				writePacket(pkt, writer);
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 17, 130, 106 })]
		public virtual void finish(FLVWriter muxer)
		{
			emptySavedTags(muxer);
		}

		[LineNumberTable(507)]
		public virtual bool hasOutput()
		{
			return true;
		}

		[LineNumberTable(new byte[] { 159, 42, 98, 118, 118 })]
		static ShiftPtsProcessor()
		{
			FLAG_TO = MainUtils.Flag.flag("to", null, "Shift first pts to this value, and all subsequent pts accordingly.");
			FLAG_BY = MainUtils.Flag.flag("by", null, "Shift all pts by this value.");
			FLAG_WRAP_AROUND = MainUtils.Flag.flag("wrap-around", null, "Expect wrap around of timestamps.");
		}
	}

	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/flv/FLVTool$PacketProcessorFactory;>;")]
	private static Map processors;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_MAX_PACKETS;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 98, 122, 63, 16, 136 })]
	private static void printGenericHelp()
	{
		java.lang.System.err.println(new StringBuilder().append("Syntax: <command> [flags] <file in> [file out]\nWhere command is: [").append(StringUtils.joinS(processors.keySet().toArray(new string[0]), ", ")).append("].")
			.toString());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(50)]
	public FLVTool()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 127, 130, 102, 102, 130, 101, 114, 100, 127,
		6, 102, 162, 123, 107, 127, 9, 130, 105, 157,
		100, 132, 121, 105, 121, 106, 106, 100, 118, 109,
		3, 201, 105, 105, 140, 104, 43, 131, 99
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 1)
		{
			printGenericHelp();
			return;
		}
		string command = args[0];
		PacketProcessorFactory processorFactory = (PacketProcessorFactory)processors.get(command);
		if (processorFactory == null)
		{
			java.lang.System.err.println(new StringBuilder().append("Unknown command: ").append(command).toString());
			printGenericHelp();
			return;
		}
		MainUtils.Cmd cmd = MainUtils.parseArguments((string[])Platform.copyOfRangeO(args, 1, args.Length), processorFactory.getFlags());
		if ((nint)cmd.args.LongLength < 1)
		{
			MainUtils.printHelpCmd(command, processorFactory.getFlags(), Arrays.asList("file in", "?file out"));
			return;
		}
		PacketProcessor processor = processorFactory.newPacketProcessor(cmd);
		int maxPackets = cmd.getIntegerFlagD(FLAG_MAX_PACKETS, Integer.valueOf(int.MaxValue)).intValue();
		FileChannelWrapper _in = null;
		FileChannelWrapper @out = null;
		try
		{
			
			_in = NIOUtils.readableChannel(new File(cmd.getArg(0)));
			if (processor.hasOutput())
			{
				
				@out = NIOUtils.writableChannel(new File(cmd.getArg(1)));
			}
			FLVReader demuxer = new FLVReader(_in);
			FLVWriter muxer = new FLVWriter(@out);
			FLVTag pkt = null;
			for (int i = 0; i < maxPackets; i++)
			{
				if ((pkt = demuxer.readNextPacket()) == null)
				{
					break;
				}
				if (!processor.processPacket(pkt, muxer))
				{
					break;
				}
			}
			processor.finish(muxer);
			if (processor.hasOutput())
			{
				muxer.finish();
			}
		}
		finally
		{
			IOUtils.closeQuietly(_in);
			IOUtils.closeQuietly(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 130, 114, 100, 99 })]
	private static PacketProcessor getProcessor(string command, MainUtils.Cmd cmd)
	{
		PacketProcessorFactory factory = (PacketProcessorFactory)processors.get(command);
		if (factory == null)
		{
			return null;
		}
		PacketProcessor result = factory.newPacketProcessor(cmd);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 130, 162, 139, 118, 118, 118, 182 })]
	static FLVTool()
	{
		processors = new HashMap();
		processors.put("clip", new ClipPacketProcessor.Factory());
		processors.put("fix_pts", new FixPtsProcessor.Factory());
		processors.put("info", new InfoPacketProcessor.Factory());
		processors.put("shift_pts", new ShiftPtsProcessor.Factory());
		FLAG_MAX_PACKETS = MainUtils.Flag.flag("max-packets", "m", "Maximum number of packets to process");
	}
}
