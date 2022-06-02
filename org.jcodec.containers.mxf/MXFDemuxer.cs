using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.api;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.containers.mxf.model;
using org.jcodec.platform;

namespace org.jcodec.containers.mxf;

public class MXFDemuxer : java.lang.Object
{
	public class Fast : MXFDemuxer
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 4, 98, 106 })]
		public Fast(SeekableByteChannel ch)
			: base(ch)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 3, 130, 108, 140, 109, 121, 147, 120, 104,
			100, 111, 159, 2, 148
		})]
		public override void parseHeader(SeekableByteChannel ff)
		{
			partitions = new ArrayList();
			metadata = new ArrayList();
			header = readHeaderPartition(ff);
			metadata.addAll(readPartitionMeta(ff, header));
			partitions.add(header);
			ff.setPosition(header.getPack().getFooterPartition());
			KLV kl = KLV.readKL(ff);
			if (kl != null)
			{
				ByteBuffer fetchFrom = NIOUtils.fetchFromChannel(ff, (int)kl.___003C_003Elen);
				MXFPartition footer = MXFPartition.read(kl.___003C_003Ekey, fetchFrom, ff.position() - kl.___003C_003Eoffset, ff.size());
				metadata.addAll(readPartitionMeta(ff, footer));
			}
		}
	}

	[Implements(new string[] { "org.jcodec.common.SeekableDemuxerTrack" })]
	public class MXFDemuxerTrack : java.lang.Object, SeekableDemuxerTrack, DemuxerTrack
	{
		private UL essenceUL;

		private int dataLen;

		private int indexSegmentIdx;

		private int indexSegmentSubIdx;

		private int frameNo;

		private long pts;

		private int partIdx;

		private long partEssenceOffset;

		private GenericDescriptor descriptor;

		private TimelineTrack track;

		private bool video;

		private bool audio;

		private MXFCodec codec;

		private int audioFrameDuration;

		private int audioTimescale;

		private MXFDemuxer demuxer;

		[LineNumberTable(481)]
		public virtual MXFCodec getCodec()
		{
			return codec;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 66, 98, 105, 104, 104, 104, 137, 106, 106,
			106, 104, 141, 117, 159, 27, 114, 105, 109, 127,
			6, 183
		})]
		public MXFDemuxerTrack(MXFDemuxer demuxer, UL essenceUL, TimelineTrack track, GenericDescriptor descriptor)
		{
			this.demuxer = demuxer;
			this.essenceUL = essenceUL;
			this.track = track;
			this.descriptor = descriptor;
			if (descriptor is GenericPictureEssenceDescriptor)
			{
				video = true;
			}
			else if (descriptor is GenericSoundEssenceDescriptor)
			{
				audio = true;
			}
			codec = resolveCodec();
			if (codec != null || descriptor is WaveAudioDescriptor)
			{
				Logger.warn(new StringBuilder().append("Track type: ").append(video).append(", ")
					.append(audio)
					.toString());
				if (audio && descriptor is WaveAudioDescriptor)
				{
					WaveAudioDescriptor wave = (WaveAudioDescriptor)descriptor;
					cacheAudioFrameSizes(demuxer.ch);
					int num = dataLen;
					int num2 = (wave.getQuantizationBits() >> 3) * wave.getChannelCount();
					audioFrameDuration = ((num2 != -1) ? (num / num2) : (-num));
					audioTimescale = ByteCodeHelper.f2i(wave.getAudioSamplingRate().scalar());
				}
			}
		}

		[LineNumberTable(334)]
		public virtual bool isVideo()
		{
			return video;
		}

		[LineNumberTable(330)]
		public virtual bool isAudio()
		{
			return audio;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(505)]
		public virtual int getTrackId()
		{
			int trackId = track.getTrackId();
			
			return trackId;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(522)]
		public virtual Rational getEditRate()
		{
			Rational editRate = track.getEditRate();
			
			return editRate;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 21, 130, 105, 116, 105, 148, 131, 103, 104,
			101, 111, 227, 61, 231, 69, 159, 1
		})]
		private MXFCodec resolveCodec()
		{
			UL codecUL;
			if (video)
			{
				codecUL = ((GenericPictureEssenceDescriptor)descriptor).getPictureEssenceCoding();
			}
			else
			{
				if (!audio)
				{
					return null;
				}
				codecUL = ((GenericSoundEssenceDescriptor)descriptor).getSoundEssenceCompression();
			}
			MXFCodec[] values = MXFCodec.values();
			for (int i = 0; i < (nint)values.LongLength; i++)
			{
				MXFCodec codec = values[i];
				if (codec.getUl().equals(codecUL))
				{
					return codec;
				}
			}
			Logger.warn(new StringBuilder().append("Unknown codec: ").append(codecUL).toString());
			return null;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 55, 130, 127, 10, 110, 174, 104, 100, 99,
			117, 148, 119, 110, 163, 102
		})]
		private void cacheAudioFrameSizes(SeekableByteChannel ch)
		{
			Iterator iterator = demuxer.partitions.iterator();
			while (iterator.hasNext())
			{
				MXFPartition mxfPartition = (MXFPartition)iterator.next();
				if (mxfPartition.getEssenceLength() <= 0u)
				{
					continue;
				}
				ch.setPosition(mxfPartition.getEssenceFilePos());
				KLV kl;
				do
				{
					kl = KLV.readKL(ch);
					if (kl == null)
					{
						break;
					}
					ch.setPosition(ch.position() + kl.___003C_003Elen);
				}
				while (!essenceUL.equals(kl.___003C_003Ekey));
				if (kl != null && essenceUL.equals(kl.___003C_003Ekey))
				{
					dataLen = (int)kl.___003C_003Elen;
					break;
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 37, 65, 68, 109, 105, 137, 104, 119, 117,
			170, 127, 5, 191, 18, 236, 60
		})]
		public virtual MXFPacket readPacket(long off, int len, long pts, int timescale, int duration, int frameNo, bool kf)
		{
			//Discarded unreachable code: IL_00b0
			SeekableByteChannel ch = demuxer.ch;
			lock (ch)
			{
				ch.setPosition(off);
				KLV kl = KLV.readKL(ch);
				while (kl != null && !essenceUL.equals(kl.___003C_003Ekey))
				{
					ch.setPosition(ch.position() + kl.___003C_003Elen);
					kl = KLV.readKL(ch);
				}
				object result;
				if (kl != null && essenceUL.equals(kl.___003C_003Ekey))
				{
					MXFPacket.___003Cclinit_003E();
					result = new MXFPacket(NIOUtils.fetchFromChannel(ch, (int)kl.___003C_003Elen), pts, timescale, duration, frameNo, (!kf) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY, null, off, len);
				}
				else
				{
					result = null;
				}
				return (MXFPacket)result;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 33, 162, 107, 99, 105, 104, 50, 127, 1,
			107, 104, 63, 17, 180, 121, 49, 171
		})]
		public virtual bool gotoFrame(long frameNo)
		{
			if (frameNo == this.frameNo)
			{
				return true;
			}
			indexSegmentSubIdx = (int)frameNo;
			indexSegmentIdx = 0;
			while (indexSegmentIdx < demuxer.indexSegments.size() && indexSegmentSubIdx >= ((IndexSegment)demuxer.indexSegments.get(indexSegmentIdx)).getIndexDuration())
			{
				indexSegmentSubIdx = (int)(indexSegmentSubIdx - ((IndexSegment)demuxer.indexSegments.get(indexSegmentIdx)).getIndexDuration());
				indexSegmentIdx++;
			}
			indexSegmentSubIdx = java.lang.Math.min(indexSegmentSubIdx, (int)((IndexSegment)demuxer.indexSegments.get(indexSegmentIdx)).getIndexDuration());
			return true;
		}

		[LineNumberTable(338)]
		public virtual double getDuration()
		{
			return demuxer.duration;
		}

		[LineNumberTable(342)]
		public virtual int getNumFrames()
		{
			return demuxer.totalFrames;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(346)]
		public virtual string getName()
		{
			string name = track.getName();
			
			return name;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 50, 162, 121, 131, 157, 109, 104, 136, 139,
			117, 155, 127, 30, 106, 127, 15, 180, 123, 179,
			105, 127, 26, 146, 127, 30, 181, 143, 110, 111,
			136, 127, 2, 126, 223, 3
		})]
		public virtual Packet nextFrame()
		{
			if (indexSegmentIdx >= demuxer.indexSegments.size())
			{
				return null;
			}
			IndexSegment seg = (IndexSegment)demuxer.indexSegments.get(indexSegmentIdx);
			long[] off = seg.getIe().getFileOff();
			int erDen = seg.getIndexEditRateNum();
			int erNum = seg.getIndexEditRateDen();
			long frameEssenceOffset = off[indexSegmentSubIdx];
			int toff = seg.getIe().getDisplayOff()[indexSegmentSubIdx];
			int kf = ((seg.getIe().getKeyFrameOff()[indexSegmentSubIdx] == 0) ? 1 : 0);
			while (frameEssenceOffset >= partEssenceOffset + ((MXFPartition)demuxer.partitions.get(partIdx)).getEssenceLength() && partIdx < demuxer.partitions.size() - 1)
			{
				partEssenceOffset += ((MXFPartition)demuxer.partitions.get(partIdx)).getEssenceLength();
				partIdx++;
			}
			long frameFileOffset = frameEssenceOffset - partEssenceOffset + ((MXFPartition)demuxer.partitions.get(partIdx)).getEssenceFilePos();
			MXFPacket result;
			if (!audio)
			{
				int len = dataLen;
				long num = pts + erNum * toff;
				int num2 = frameNo;
				MXFDemuxerTrack mXFDemuxerTrack = this;
				int num3 = num2;
				mXFDemuxerTrack.frameNo = num2 + 1;
				result = readPacket(frameFileOffset, len, num, erDen, erNum, num3, (byte)kf != 0);
				pts += erNum;
			}
			else
			{
				int len2 = dataLen;
				long num4 = pts;
				int timescale = audioTimescale;
				int duration = audioFrameDuration;
				int num2 = frameNo;
				MXFDemuxerTrack mXFDemuxerTrack = this;
				int num5 = num2;
				mXFDemuxerTrack.frameNo = num2 + 1;
				result = readPacket(frameFileOffset, len2, num4, timescale, duration, num5, (byte)kf != 0);
				pts += audioFrameDuration;
			}
			indexSegmentSubIdx++;
			if (indexSegmentSubIdx >= (nint)off.LongLength)
			{
				indexSegmentIdx++;
				indexSegmentSubIdx = 0;
				if (dataLen == 0 && indexSegmentIdx < demuxer.indexSegments.size())
				{
					IndexSegment nseg = (IndexSegment)demuxer.indexSegments.get(indexSegmentIdx);
					long num6 = pts * nseg.getIndexEditRateNum();
					long num7 = erDen;
					pts = ((num7 != -1) ? (num6 / num7) : (-num6));
				}
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 29, 162, 106, 99, 125, 116 })]
		public virtual bool gotoSyncFrame(long frameNo)
		{
			if (!gotoFrame(frameNo))
			{
				return false;
			}
			IndexSegment seg = (IndexSegment)demuxer.indexSegments.get(indexSegmentIdx);
			int kfOff = seg.getIe().getKeyFrameOff()[indexSegmentSubIdx];
			bool result = gotoFrame(frameNo + kfOff);
			
			return result;
		}

		[LineNumberTable(464)]
		public virtual long getCurFrame()
		{
			return frameNo;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(469)]
		public virtual void seek(double second)
		{
			
			throw new org.jcodec.api.NotSupportedException("");
		}

		[LineNumberTable(473)]
		public virtual UL getEssenceUL()
		{
			return essenceUL;
		}

		[LineNumberTable(477)]
		public virtual GenericDescriptor getDescriptor()
		{
			return descriptor;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 15, 130, 99, 105, 109, 179, 127, 6, 127,
			12, 46
		})]
		public virtual DemuxerTrackMeta getMeta()
		{
			Size size = null;
			if (video)
			{
				GenericPictureEssenceDescriptor pd = (GenericPictureEssenceDescriptor)descriptor;
				size = new Size(pd.getStoredWidth(), pd.getStoredHeight());
			}
			TrackType t = (video ? TrackType.___003C_003EVIDEO : ((!audio) ? TrackType.___003C_003EOTHER : TrackType.___003C_003EAUDIO));
			DemuxerTrackMeta result = new DemuxerTrackMeta(t, getCodec().getCodec(), demuxer.duration, null, demuxer.totalFrames, null, VideoCodecMeta.createSimpleVideoCodecMeta(size, ColorSpace.___003C_003EYUV420), null);
			
			return result;
		}
	}

	public class MXFPacket : Packet
	{
		private long offset;

		private int len;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public new static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 9, 66, 117, 105, 105 })]
		public MXFPacket(ByteBuffer data, long pts, int timescale, long duration, long frameNo, FrameType frameType, TapeTimecode tapeTimecode, long offset, int len)
			: base(data, pts, timescale, duration, frameNo, frameType, tapeTimecode, 0)
		{
			this.offset = offset;
			this.len = len;
		}

		[LineNumberTable(538)]
		public virtual long getOffset()
		{
			return offset;
		}

		[LineNumberTable(542)]
		public virtual int getLen()
		{
			return len;
		}

		[HideFromJava]
		static MXFPacket()
		{
			Packet.___003Cclinit_003E();
		}
	}

	public sealed class OP : java.lang.Object
	{
		internal static OP ___003C_003EOP1a;

		internal static OP ___003C_003EOP1b;

		internal static OP ___003C_003EOP1c;

		internal static OP ___003C_003EOP2a;

		internal static OP ___003C_003EOP2b;

		internal static OP ___003C_003EOP2c;

		internal static OP ___003C_003EOP3a;

		internal static OP ___003C_003EOP3b;

		internal static OP ___003C_003EOP3c;

		internal static OP ___003C_003EOPAtom;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static OP[] _values;

		public int major;

		public int minor;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP1a
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP1a;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP1b
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP1b;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP1c
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP1c;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP2a
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP2a;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP2b
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP2b;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP2c
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP2c;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP3a
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP3a;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP3b
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP3b;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OP3c
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOP3c;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static OP OPAtom
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOPAtom;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(97)]
		public static OP[] values()
		{
			return _values;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 120, 162, 105, 104, 104 })]
		private OP(int major, int minor)
		{
			this.major = major;
			this.minor = minor;
		}

		[LineNumberTable(new byte[]
		{
			159, 123, 66, 109, 109, 109, 109, 109, 109, 109,
			109, 109, 142
		})]
		static OP()
		{
			___003C_003EOP1a = new OP(1, 1);
			___003C_003EOP1b = new OP(1, 2);
			___003C_003EOP1c = new OP(1, 3);
			___003C_003EOP2a = new OP(2, 1);
			___003C_003EOP2b = new OP(2, 2);
			___003C_003EOP2c = new OP(2, 3);
			___003C_003EOP3a = new OP(3, 1);
			___003C_003EOP3b = new OP(3, 2);
			___003C_003EOP3c = new OP(3, 3);
			___003C_003EOPAtom = new OP(16, 0);
			_values = new OP[10] { ___003C_003EOP1a, ___003C_003EOP1b, ___003C_003EOP1c, ___003C_003EOP2a, ___003C_003EOP2b, ___003C_003EOP2c, ___003C_003EOP3a, ___003C_003EOP3b, ___003C_003EOP3c, ___003C_003EOPAtom };
		}
	}

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;")]
	protected internal List metadata;

	protected internal MXFPartition header;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFPartition;>;")]
	protected internal List partitions;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mxf/model/IndexSegment;>;")]
	protected internal List indexSegments;

	protected internal SeekableByteChannel ch;

	protected internal MXFDemuxerTrack[] tracks;

	protected internal int totalFrames;

	protected internal double duration;

	protected internal TimecodeComponent timecode;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 97, 98, 141, 140, 108, 104, 152, 104, 104,
		111, 159, 2, 116, 147, 154, 120, 99, 120
	})]
	public virtual void parseHeader(SeekableByteChannel ff)
	{
		header = readHeaderPartition(ff);
		metadata = new ArrayList();
		partitions = new ArrayList();
		long nextPartition = ff.size();
		ff.setPosition(header.getPack().getFooterPartition());
		do
		{
			long thisPartition = ff.position();
			KLV kl = KLV.readKL(ff);
			ByteBuffer fetchFrom = NIOUtils.fetchFromChannel(ff, (int)kl.___003C_003Elen);
			header = MXFPartition.read(kl.___003C_003Ekey, fetchFrom, ff.position() - kl.___003C_003Eoffset, nextPartition);
			if (header.getPack().getNbEssenceContainers() > 0)
			{
				partitions.add(0, header);
			}
			metadata.addAll(0, readPartitionMeta(ff, header));
			ff.setPosition(header.getPack().getPrevPartition());
			nextPartition = thisPartition;
		}
		while (header.getPack().getThisPartition() != 0u);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 79, 98, 108, 127, 5, 105, 104, 110, 118,
		159, 7, 102
	})]
	private void findIndex()
	{
		indexSegments = new ArrayList();
		Iterator iterator = metadata.iterator();
		while (iterator.hasNext())
		{
			MXFMetadata meta = (MXFMetadata)iterator.next();
			if (meta is IndexSegment)
			{
				IndexSegment @is = (IndexSegment)meta;
				indexSegments.add(@is);
				totalFrames = (int)(totalFrames + @is.getIndexDuration());
				duration += (double)@is.getIndexEditRateDen() * (double)@is.getIndexDuration() / (double)@is.getIndexEditRateNum();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 114, 130, 114, 114, 103, 191, 1, 115, 107,
		131, 106, 112, 127, 2, 166, 112, 101, 127, 7,
		125, 175, 101, 127, 7, 134, 106, 191, 95, 111,
		115, 145, 134
	})]
	public virtual MXFDemuxerTrack[] findTracks()
	{
		List _tracks = MXFUtil.findAllMeta(metadata, ClassLiteral<TimelineTrack>.Value);
		List descriptors = MXFUtil.findAllMeta(metadata, ClassLiteral<org.jcodec.containers.mxf.model.FileDescriptor>.Value);
		LinkedHashMap tracks = new LinkedHashMap();
		Iterator iterator = _tracks.iterator();
		while (iterator.hasNext())
		{
			TimelineTrack track = (TimelineTrack)iterator.next();
			if (track.getTrackId() == 0 || track.getTrackNumber() == 0)
			{
				Logger.warn("trackId == 0 || trackNumber == 0");
				continue;
			}
			int trackId = track.getTrackId();
			if (((Map)tracks).containsKey((object)Integer.valueOf(trackId)))
			{
				Logger.warn(new StringBuilder().append("duplicate trackId ").append(trackId).toString());
				continue;
			}
			org.jcodec.containers.mxf.model.FileDescriptor descriptor = findDescriptor(descriptors, track.getTrackId());
			if (descriptor == null)
			{
				Logger.warn(new StringBuilder().append("No generic descriptor for track: ").append(track.getTrackId()).toString());
				if (descriptors.size() == 1 && ((org.jcodec.containers.mxf.model.FileDescriptor)descriptors.get(0)).getLinkedTrackId() == 0)
				{
					descriptor = (org.jcodec.containers.mxf.model.FileDescriptor)descriptors.get(0);
				}
			}
			if (descriptor == null)
			{
				Logger.warn(new StringBuilder().append("Track without descriptor: ").append(track.getTrackId()).toString());
				continue;
			}
			int trackNumber = track.getTrackNumber();
			UL ul = UL.newULFromInts(new int[16]
			{
				6,
				14,
				43,
				52,
				1,
				2,
				1,
				1,
				13,
				1,
				3,
				1,
				(int)(((uint)trackNumber >> 24) & 0xFF),
				(int)(((uint)trackNumber >> 16) & 0xFF),
				(int)(((uint)trackNumber >> 8) & 0xFF),
				trackNumber & 0xFF
			});
			MXFDemuxerTrack dt = createTrack(ul, track, descriptor);
			if (dt.getCodec() != null || descriptor is WaveAudioDescriptor)
			{
				((Map)tracks).put((object)Integer.valueOf(trackId), (object)dt);
			}
		}
		return (MXFDemuxerTrack[])((Map)tracks).values().toArray(new MXFDemuxerTrack[((Map)tracks).size()]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mxf/model/FileDescriptor;>;I)Lorg/jcodec/containers/mxf/model/FileDescriptor;")]
	[LineNumberTable(new byte[] { 159, 104, 162, 124, 106, 131, 99 })]
	public static org.jcodec.containers.mxf.model.FileDescriptor findDescriptor(List descriptors, int trackId)
	{
		Iterator iterator = descriptors.iterator();
		while (iterator.hasNext())
		{
			org.jcodec.containers.mxf.model.FileDescriptor descriptor = (org.jcodec.containers.mxf.model.FileDescriptor)iterator.next();
			if (descriptor.getLinkedTrackId() == trackId)
			{
				return descriptor;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(164)]
	protected internal virtual MXFDemuxerTrack createTrack(UL ul, TimelineTrack track, GenericDescriptor descriptor)
	{
		MXFDemuxerTrack result = new MXFDemuxerTrack(this, ul, track, descriptor);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 87, 162, 99, 107, 115, 111, 125, 131, 186 })]
	public static MXFPartition readHeaderPartition(SeekableByteChannel ff)
	{
		MXFPartition header = null;
		KLV kl;
		while ((kl = KLV.readKL(ff)) != null)
		{
			if (MXFConst.___003C_003EHEADER_PARTITION_KLV.equals(kl.___003C_003Ekey))
			{
				ByteBuffer data = NIOUtils.fetchFromChannel(ff, (int)kl.___003C_003Elen);
				header = MXFPartition.read(kl.___003C_003Ekey, data, ff.position() - kl.___003C_003Eoffset, 0L);
				break;
			}
			ff.setPosition(ff.position() + kl.___003C_003Elen);
		}
		return header;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/SeekableByteChannel;Lorg/jcodec/containers/mxf/model/MXFPartition;)Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;")]
	[LineNumberTable(new byte[]
	{
		159, 91, 130, 104, 103, 120, 116, 112, 123, 101,
		106, 198
	})]
	public static List readPartitionMeta(SeekableByteChannel ff, MXFPartition header)
	{
		long basePos = ff.position();
		ArrayList local = new ArrayList();
		ByteBuffer metaBuffer = NIOUtils.fetchFromChannel(ff, (int)java.lang.Math.max(0L, header.getEssenceFilePos() - basePos));
		KLV kl;
		while (metaBuffer.hasRemaining() && (kl = KLV.readKLFromBuffer(metaBuffer, basePos)) != null && metaBuffer.remaining() >= kl.___003C_003Elen)
		{
			MXFMetadata meta = parseMeta(kl.___003C_003Ekey, NIOUtils.read(metaBuffer, (int)kl.___003C_003Elen));
			if (meta != null)
			{
				((List)local).add((object)meta);
			}
		}
		return local;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 98, 114, 100, 127, 1, 163, 119, 104,
		125, 131, 127, 1
	})]
	private static MXFMetadata parseMeta(UL ul, ByteBuffer _bb)
	{
		//Discarded unreachable code: IL_005d
		Class class1 = (Class)MXFConst.klMetadata.get(ul);
		if (class1 == null)
		{
			Logger.warn(new StringBuilder().append("Unknown metadata piece: ").append(ul).toString());
			return null;
		}
		java.lang.Exception ex2;
		try
		{
			MXFMetadata meta = (MXFMetadata)Platform.newInstance(class1, new object[1] { ul });
			meta.readBuf(_bb);
			return meta;
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
		Logger.warn(new StringBuilder().append("Unknown metadata piece: ").append(ul).toString());
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 75, 98, 117, 105, 3, 199 })]
	public virtual MXFDemuxerTrack getVideoTrack()
	{
		MXFDemuxerTrack[] array = tracks;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MXFDemuxerTrack track = array[i];
			if (track.isVideo())
			{
				return track;
			}
		}
		return null;
	}

	[LineNumberTable(176)]
	public virtual TimecodeComponent getTimecode()
	{
		return timecode;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(I)Ljava/util/List<Lorg/jcodec/containers/mxf/model/SourceClip;>;")]
	[LineNumberTable(new byte[]
	{
		158, 254, 98, 99, 103, 127, 5, 105, 105, 106,
		135, 108, 105, 107, 170, 102
	})]
	public virtual List getSourceClips(int trackId)
	{
		int trackFound = 1;
		ArrayList clips = new ArrayList();
		Iterator iterator = metadata.iterator();
		while (iterator.hasNext())
		{
			MXFMetadata i = (MXFMetadata)iterator.next();
			if (i is TimelineTrack)
			{
				TimelineTrack tt = (TimelineTrack)i;
				int trackId2 = tt.getTrackId();
				trackFound = ((trackId2 == trackId) ? 1 : 0);
			}
			if (trackFound != 0 && i is SourceClip)
			{
				SourceClip clip = (SourceClip)i;
				if (clip.getSourceTrackId() == trackId)
				{
					((List)clips).add((object)clip);
				}
			}
		}
		return clips;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 126, 130, 105, 104, 106, 104, 103, 109, 124 })]
	public MXFDemuxer(SeekableByteChannel ch)
	{
		this.ch = ch;
		ch.setPosition(0L);
		parseHeader(ch);
		findIndex();
		tracks = findTracks();
		timecode = (TimecodeComponent)MXFUtil.findMeta(metadata, ClassLiteral<TimecodeComponent>.Value);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 117, 130, 146, 103, 104, 101, 127, 2, 227,
		61, 231, 69
	})]
	public virtual OP getOp()
	{
		UL op = header.getPack().getOp();
		OP[] values = OP.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			OP op2 = values[i];
			if (op.get(12) == op2.major && op.get(13) == op2.minor)
			{
				return op2;
			}
		}
		return OP.___003C_003EOPAtom;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mxf/model/IndexSegment;>;")]
	[LineNumberTable(168)]
	public virtual List getIndexes()
	{
		return indexSegments;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFPartition;>;")]
	[LineNumberTable(172)]
	public virtual List getEssencePartitions()
	{
		return partitions;
	}

	[LineNumberTable(265)]
	public virtual MXFDemuxerTrack[] getTracks()
	{
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 73, 98, 103, 118, 106, 10, 199 })]
	public virtual MXFDemuxerTrack[] getAudioTracks()
	{
		ArrayList audio = new ArrayList();
		MXFDemuxerTrack[] array = tracks;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MXFDemuxerTrack track = array[i];
			if (track.isAudio())
			{
				((List)audio).add((object)track);
			}
		}
		return (MXFDemuxerTrack[])((List)audio).toArray((object[])new MXFDemuxerTrack[0]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		158, 249, 66, 136, 104, 104, 104, 111, 101, 100,
		100, 100, 105, 111, 137, 127, 2, 109, 131, 101,
		223, 4, 76, 231, 60, 132, 135, 74, 3
	})]
	public static TapeTimecode readTapeTimecode(File mxf)
	{
		FileChannelWrapper read = NIOUtils.readableChannel(mxf);
		TapeTimecode result;
		try
		{
			Fast fast = new Fast(read);
			MXFDemuxerTrack track = fast.getVideoTrack();
			TimecodeComponent timecode = fast.getTimecode();
			List sourceClips = fast.getSourceClips(track.getTrackId());
			long tc = 0L;
			int dropFrame = 0;
			Rational editRate = null;
			if (timecode != null)
			{
				editRate = track.getEditRate();
				dropFrame = ((timecode.getDropFrame() != 0) ? 1 : 0);
				tc = timecode.getStart();
			}
			Iterator iterator = sourceClips.iterator();
			while (iterator.hasNext())
			{
				SourceClip sourceClip = (SourceClip)iterator.next();
				tc += sourceClip.getStartPosition();
			}
			if (editRate != null)
			{
				result = TapeTimecode.tapeTimecode(tc, (byte)dropFrame != 0, ByteCodeHelper.d2i(java.lang.Math.ceil(editRate.toDouble())));
				goto IL_00c7;
			}
		}
		catch
		{
			//try-fault
			read.close();
			throw;
		}
		try
		{
			
		}
		finally
		{
			read.close();
		}
		return null;
		IL_00c7:
		read.close();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;")]
	[LineNumberTable(624)]
	public virtual List getMetadata()
	{
		List result = Collections.unmodifiableList(metadata);
		
		return result;
	}
}
