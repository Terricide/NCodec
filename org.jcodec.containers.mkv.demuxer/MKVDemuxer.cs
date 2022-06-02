using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.mp4;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mkv.boxes;

namespace org.jcodec.containers.mkv.demuxer;

[Implements(new string[] { "org.jcodec.common.Demuxer" })]
public class MKVDemuxer : java.lang.Object, Demuxer, Closeable, AutoCloseable
{
	public class AudioTrack : MkvTrack
	{
		public double samplingFrequency;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 20, 66, 107 })]
		public AudioTrack(int trackNo, MKVDemuxer demuxer)
			: base(trackNo, demuxer)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 19, 98, 104, 134 })]
		public override Packet nextFrame()
		{
			MkvBlockData b = ((MkvTrack)this).nextBlock();
			if (b == null)
			{
				return null;
			}
			Packet result = Packet.createPacket(b.data, b.block.absoluteTimecode, (int)java.lang.Math.round(samplingFrequency), 1L, 0L, Packet.FrameType.___003C_003EKEY, TapeTimecode.___003C_003EZERO_TAPE_TIMECODE);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 16, 162, 105, 134 })]
		public override Packet getFrames(int count)
		{
			MkvBlockData frameBlock = getFrameBlock(count);
			if (frameBlock == null)
			{
				return null;
			}
			Packet result = Packet.createPacket(frameBlock.data, frameBlock.block.absoluteTimecode, (int)java.lang.Math.round(samplingFrequency), frameBlock.count, 0L, Packet.FrameType.___003C_003EKEY, TapeTimecode.___003C_003EZERO_TAPE_TIMECODE);
			return result;
		}

		[LineNumberTable(516)]
		public override DemuxerTrackMeta getMeta()
		{
			return null;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(521)]
		public override bool gotoSyncFrame(long frame)
		{
			bool result = gotoFrame(frame);
			return result;
		}
	}

	public class IndexedBlock : java.lang.Object
	{
		public int firstFrameNo;

		public MkvBlock block;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 69, 98, 103, 104, 104 })]
		public static IndexedBlock make(int no, MkvBlock b)
		{
			IndexedBlock ib = new IndexedBlock();
			ib.firstFrameNo = no;
			ib.block = b;
			return ib;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(288)]
		public IndexedBlock()
		{
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	public class MkvBlockData : java.lang.Object
	{
		[Modifiers(Modifiers.Final)]
		internal MkvBlock block;

		[Modifiers(Modifiers.Final)]
		internal ByteBuffer data;

		[Modifiers(Modifiers.Final)]
		internal int count;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 65, 162, 105, 104, 104, 104 })]
		internal MkvBlockData(MkvBlock block, ByteBuffer data, int count)
		{
			this.block = block;
			this.data = data;
			this.count = count;
		}
	}

	[Implements(new string[] { "org.jcodec.common.SeekableDemuxerTrack" })]
	public class MkvTrack : java.lang.Object, SeekableDemuxerTrack, DemuxerTrack
	{
		internal int ___003C_003EtrackNo;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/demuxer/MKVDemuxer$IndexedBlock;>;")]
		internal List blocks;

		internal int framesCount;

		private int frameIdx;

		private int blockIdx;

		private int frameInBlockIdx;

		private MKVDemuxer demuxer;

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public int trackNo
		{
			[HideFromJava]
			get
			{
				return ___003C_003EtrackNo;
			}
			[HideFromJava]
			private set
			{
				___003C_003EtrackNo = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 57, 162, 127, 8, 131, 125, 100, 145, 242,
			70, 120, 109, 115, 136, 116, 111, 111, 112, 111,
			168
		})]
		protected internal virtual MkvBlockData nextBlock()
		{
			if (frameIdx >= blocks.size() || blockIdx >= blocks.size())
			{
				return null;
			}
			MkvBlock b = ((IndexedBlock)blocks.get(blockIdx)).block;
			if (b == null)
			{
				throw new RuntimeException("Something somewhere went wrong.");
			}
			if (b.frames == null || b.frames.Length == 0)
			{
				access_0024000(demuxer).setPosition(b.dataOffset);
				ByteBuffer data2 = ByteBuffer.allocate(b.dataLen);
				access_0024000(demuxer).read(data2);
				b.readFrames(data2);
			}
			ByteBuffer data = b.frames[frameInBlockIdx].duplicate();
			frameInBlockIdx++;
			frameIdx++;
			if (frameInBlockIdx >= (nint)b.frames.LongLength)
			{
				blockIdx++;
				frameInBlockIdx = 0;
			}
			MkvBlockData result = new MkvBlockData(b, data, 1);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159,
			44,
			66,
			113,
			127,
			2,
			131,
			byte.MaxValue,
			3,
			60,
			231,
			71
		})]
		private int findBlockIndex(long i)
		{
			for (int blockIndex = 0; blockIndex < blocks.size(); blockIndex++)
			{
				if (i < ((IndexedBlock)blocks.get(blockIndex)).block.frameSizes.LongLength)
				{
					return blockIndex;
				}
				i -= ((IndexedBlock)blocks.get(blockIndex)).block.frameSizes.LongLength;
			}
			return -1;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 35, 98, 113, 99, 103, 125, 104, 125, 245,
			71, 120, 109, 115, 220, 227, 61, 99, 118, 191,
			13, 122, 111, 111, 112, 104, 143, 102, 134, 100,
			127, 1, 143, 106, 127, 1, 141
		})]
		internal virtual MkvBlockData getFrameBlock(int count)
		{
			if (count + frameIdx >= framesCount)
			{
				return null;
			}
			ArrayList packetFrames = new ArrayList();
			MkvBlock firstBlockInAPacket = ((IndexedBlock)blocks.get(blockIdx)).block;
			while (count > 0)
			{
				MkvBlock b = ((IndexedBlock)blocks.get(blockIdx)).block;
				IOException ex;
				if (b.frames == null || b.frames.Length == 0)
				{
					try
					{
						access_0024000(demuxer).setPosition(b.dataOffset);
						ByteBuffer data2 = ByteBuffer.allocate(b.dataLen);
						access_0024000(demuxer).read(data2);
						b.readFrames(data2);
					}
					catch (IOException x)
					{
						ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
						goto IL_00c3;
					}
				}
				((List)packetFrames).add((object)b.frames[frameInBlockIdx].duplicate());
				frameIdx++;
				frameInBlockIdx++;
				if (frameInBlockIdx >= (nint)b.frames.LongLength)
				{
					frameInBlockIdx = 0;
					blockIdx++;
				}
				count += -1;
				continue;
				IL_00c3:
				IOException ioe = ex;
				string message = new StringBuilder().append("while reading frames of a Block at offset 0x").append(java.lang.String.instancehelper_toUpperCase(Long.toHexString(b.dataOffset))).append(")")
					.toString();
				throw new RuntimeException(message, ioe);
			}
			int size = 0;
			Iterator iterator = ((List)packetFrames).iterator();
			while (iterator.hasNext())
			{
				ByteBuffer aFrame2 = (ByteBuffer)iterator.next();
				size += aFrame2.limit();
			}
			ByteBuffer data = ByteBuffer.allocate(size);
			Iterator iterator2 = ((List)packetFrames).iterator();
			while (iterator2.hasNext())
			{
				ByteBuffer aFrame = (ByteBuffer)iterator2.next();
				data.put(aFrame);
			}
			return new MkvBlockData(firstBlockInAPacket, data, ((List)packetFrames).size());
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 49, 162, 106, 99, 107, 131, 105, 101, 131,
			105, 104, 159, 6
		})]
		public virtual bool gotoFrame(long i)
		{
			if (i > 2147483647u)
			{
				return false;
			}
			if (i > framesCount)
			{
				return false;
			}
			int frameBlockIdx = findBlockIndex(i);
			if (frameBlockIdx == -1)
			{
				return false;
			}
			frameIdx = (int)i;
			blockIdx = frameBlockIdx;
			frameInBlockIdx = (int)i - ((IndexedBlock)blocks.get(blockIdx)).firstFrameNo;
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 61, 162, 233, 58, 104, 104, 104, 200, 140,
			104, 104
		})]
		public MkvTrack(int trackNo, MKVDemuxer demuxer)
		{
			framesCount = 0;
			frameIdx = 0;
			blockIdx = 0;
			frameInBlockIdx = 0;
			blocks = new ArrayList();
			___003C_003EtrackNo = trackNo;
			this.demuxer = demuxer;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 58, 66, 104, 102 })]
		public virtual Packet nextFrame()
		{
			MkvBlockData bd = this.nextBlock();
			if (bd == null)
			{
				return null;
			}
			Packet result = Packet.createPacket(bd.data, bd.block.absoluteTimecode, demuxer.timescale, 1L, frameIdx - 1, Packet.FrameType.___003C_003EKEY, TapeTimecode.___003C_003EZERO_TAPE_TIMECODE);
			return result;
		}

		[LineNumberTable(404)]
		public virtual long getCurFrame()
		{
			return frameIdx;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(409)]
		public virtual void seek(double second)
		{
			throw new RuntimeException("Not implemented yet");
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 37, 66, 105, 166 })]
		public virtual Packet getFrames(int count)
		{
			MkvBlockData frameBlock = getFrameBlock(count);
			if (frameBlock == null)
			{
				return null;
			}
			Packet result = Packet.createPacket(frameBlock.data, frameBlock.block.absoluteTimecode, demuxer.timescale, frameBlock.count, 0L, Packet.FrameType.___003C_003EKEY, TapeTimecode.___003C_003EZERO_TAPE_TIMECODE);
			return result;
		}

		[LineNumberTable(475)]
		public virtual DemuxerTrackMeta getMeta()
		{
			return null;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(480)]
		public virtual bool gotoSyncFrame(long frame)
		{
			bool result = gotoFrame(frame);
			return result;
		}

		//[HideFromJava]
		//[NameSig("nextBlock", "()Lorg.jcodec.containers.mkv.demuxer.MKVDemuxer$MkvBlockData;")]
		//protected internal object nextBlock()
		//{
		//	return this.nextBlock();
		//}

		//[HideFromJava]
		//[NameSig("nextBlock", "()Lorg.jcodec.containers.mkv.demuxer.MKVDemuxer$MkvBlockData;")]
		//protected internal object _003Cnonvirtual_003E0()
		//{
		//	return this.nextBlock();
		//}
	}

	public class SubtitlesTrack : MkvTrack
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 67, 130, 107 })]
		internal SubtitlesTrack(int trackNo, MKVDemuxer demuxer)
			: base(trackNo, demuxer)
		{
		}
	}

	[Implements(new string[] { "org.jcodec.common.SeekableDemuxerTrack" })]
	public class VideoTrack : java.lang.Object, SeekableDemuxerTrack, DemuxerTrack
	{
		private ByteBuffer state;

		internal int ___003C_003EtrackNo;

		private int frameIdx;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/boxes/MkvBlock;>;")]
		internal List blocks;

		private MKVDemuxer demuxer;

		private Codec codec;

		private AvcCBox avcC;

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public int trackNo
		{
			[HideFromJava]
			get
			{
				return ___003C_003EtrackNo;
			}
			[HideFromJava]
			private set
			{
				___003C_003EtrackNo = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 92, 162, 233, 58, 232, 71, 108, 104, 104,
			105, 106, 109, 148, 136
		})]
		public VideoTrack(MKVDemuxer demuxer, int trackNo, ByteBuffer state, Codec codec)
		{
			frameIdx = 0;
			blocks = new ArrayList();
			this.demuxer = demuxer;
			___003C_003EtrackNo = trackNo;
			this.codec = codec;
			if (codec == Codec.___003C_003EH264)
			{
				avcC = H264Utils.parseAVCCFromBuffer(state);
				this.state = H264Utils.avcCToAnnexB(avcC);
			}
			else
			{
				this.state = state;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 88, 130, 116, 131, 120, 100, 113, 239, 70,
			120, 109, 115, 104, 109, 100, 116, 127, 5, 111,
			110, 142
		})]
		public virtual Packet nextFrame()
		{
			if (frameIdx >= blocks.size())
			{
				return null;
			}
			MkvBlock b = (MkvBlock)blocks.get(frameIdx);
			if (b == null)
			{
				throw new RuntimeException("Something somewhere went wrong.");
			}
			frameIdx++;
			access_0024000(demuxer).setPosition(b.dataOffset);
			ByteBuffer data = ByteBuffer.allocate(b.dataLen);
			access_0024000(demuxer).read(data);
			data.flip();
			b.readFrames(data.duplicate());
			long duration = 1L;
			if (frameIdx < blocks.size())
			{
				duration = ((MkvBlock)blocks.get(frameIdx)).absoluteTimecode - b.absoluteTimecode;
			}
			ByteBuffer result = b.frames[0].duplicate();
			if (codec == Codec.___003C_003EH264)
			{
				result = H264Utils.decodeMOVPacket(result, avcC);
			}
			Packet result2 = Packet.createPacket(result, b.absoluteTimecode, demuxer.timescale, duration, frameIdx - 1, (!b._keyFrame) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY, TapeTimecode.___003C_003EZERO_TAPE_TIMECODE);
			return result2;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 80, 66, 106, 99, 112, 131, 105 })]
		public virtual bool gotoFrame(long i)
		{
			if (i > 2147483647u)
			{
				return false;
			}
			if (i > blocks.size())
			{
				return false;
			}
			frameIdx = (int)i;
			return true;
		}

		[LineNumberTable(259)]
		public virtual long getCurFrame()
		{
			return frameIdx;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(264)]
		public virtual void seek(double second)
		{
			throw new RuntimeException("Not implemented yet");
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(269)]
		public virtual int getFrameCount()
		{
			int result = blocks.size();
			return result;
		}

		[LineNumberTable(273)]
		public virtual ByteBuffer getCodecState()
		{
			return state;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 73, 130, 127, 26, 46 })]
		public virtual DemuxerTrackMeta getMeta()
		{
			DemuxerTrackMeta result = new DemuxerTrackMeta(TrackType.___003C_003EVIDEO, codec, 0.0, null, 0, state, VideoCodecMeta.createSimpleVideoCodecMeta(new Size(demuxer.pictureWidth, demuxer.pictureHeight), ColorSpace.___003C_003EYUV420), null);			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(284)]
		public virtual bool gotoSyncFrame(long i)
		{
			throw new RuntimeException("Unsupported");
		}
	}

	private VideoTrack vTrack;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/demuxer/MKVDemuxer$AudioTrack;>;")]
	private List aTracks;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/demuxer/MKVDemuxer$SubtitlesTrack;>;")]
	private List subsTracks;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/boxes/EbmlMaster;>;")]
	private List t;

	private SeekableByteChannel channel;

	internal int timescale;

	internal int pictureWidth;

	internal int pictureHeight;

	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/common/Codec;>;")]
	private static Map codecMapping;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 123, 98, 233, 48, 232, 69, 232, 76, 104,
		108, 108, 109, 109, 105
	})]
	public MKVDemuxer(SeekableByteChannel fileChannelWrapper)
	{
		vTrack = null;
		timescale = 1;
		channel = fileChannelWrapper;
		aTracks = new ArrayList();
		subsTracks = new ArrayList();
		MKVParser parser = new MKVParser(channel);
		t = parser.parse();
		demux();
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(60)]
	internal static SeekableByteChannel access_0024000(MKVDemuxer x0)
	{
		return x0.channel;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 162, 127, 1, 115, 100, 110, 159, 1,
		127, 17, 121, 118, 121, 118, 138, 105, 113, 121,
		121, 113, 153, 113, 100, 101, 138, 159, 2, 113,
		127, 2, 113, 127, 2, 113, 127, 2, 113, 127,
		2, 113, 105, 111, 113, 105, 112, 111, 145, 209,
		148, 111, 108, 127, 2, 113, 101, 144, 111, 106,
		108, 143, 102, 121, 127, 21, 121, 118, 127, 10,
		116, 106, 115, 105, 124, 106, 127, 7, 111, 106,
		115, 137, 227, 52, 230, 78, 102
	})]
	private void demux()
	{
		MKVType[] path = new MKVType[3]
		{
			MKVType.___003C_003ESegment,
			MKVType.___003C_003EInfo,
			MKVType.___003C_003ETimecodeScale
		};
		EbmlUint ts = (EbmlUint)MKVType.findFirstTree(t, path);
		if (ts != null)
		{
			timescale = (int)ts.getUint();
		}
		MKVType[] path14 = new MKVType[3]
		{
			MKVType.___003C_003ESegment,
			MKVType.___003C_003ETracks,
			MKVType.___003C_003ETrackEntry
		};
		Iterator iterator = MKVType.findList(t, ClassLiteral<EbmlMaster>.Value, path14).iterator();
		while (iterator.hasNext())
		{
			EbmlMaster aTrack = (EbmlMaster)iterator.next();
			MKVType[] path3 = new MKVType[2]
			{
				MKVType.___003C_003ETrackEntry,
				MKVType.___003C_003ETrackType
			};
			long type = ((EbmlUint)MKVType.findFirst(aTrack, path3)).getUint();
			MKVType[] path6 = new MKVType[2]
			{
				MKVType.___003C_003ETrackEntry,
				MKVType.___003C_003ETrackNumber
			};
			long id = ((EbmlUint)MKVType.findFirst(aTrack, path6)).getUint();
			switch (type)
			{
			case 1L:
			{
				if (vTrack != null)
				{
					throw new RuntimeException("More then 1 video track, can not compute...");
				}
				MKVType[] path8 = new MKVType[2]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003ECodecPrivate
				};
				MKVType[] path4 = new MKVType[2]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003ECodecID
				};
				EbmlString codecId = (EbmlString)MKVType.findFirst(aTrack, path4);
				Codec codec = (Codec)codecMapping.get(codecId.getString());
				EbmlBin videoCodecState = (EbmlBin)MKVType.findFirst(aTrack, path8);
				ByteBuffer state = null;
				if (videoCodecState != null)
				{
					state = videoCodecState.data;
				}
				MKVType[] path9 = new MKVType[3]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003EVideo,
					MKVType.___003C_003EPixelWidth
				};
				EbmlUint width = (EbmlUint)MKVType.findFirst(aTrack, path9);
				MKVType[] path10 = new MKVType[3]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003EVideo,
					MKVType.___003C_003EPixelHeight
				};
				EbmlUint height = (EbmlUint)MKVType.findFirst(aTrack, path10);
				MKVType[] path11 = new MKVType[3]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003EVideo,
					MKVType.___003C_003EDisplayWidth
				};
				EbmlUint dwidth = (EbmlUint)MKVType.findFirst(aTrack, path11);
				MKVType[] path12 = new MKVType[3]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003EVideo,
					MKVType.___003C_003EDisplayHeight
				};
				EbmlUint dheight = (EbmlUint)MKVType.findFirst(aTrack, path12);
				MKVType[] path13 = new MKVType[3]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003EVideo,
					MKVType.___003C_003EDisplayUnit
				};
				EbmlUint unit = (EbmlUint)MKVType.findFirst(aTrack, path13);
				if (width != null && height != null)
				{
					pictureWidth = (int)width.getUint();
					pictureHeight = (int)height.getUint();
				}
				else if (dwidth != null && dheight != null)
				{
					if (unit != null && unit.getUint() != 0u)
					{
						throw new RuntimeException("DisplayUnits other then 0 are not implemented yet");
					}
					pictureHeight = (int)dheight.getUint();
					pictureWidth = (int)dwidth.getUint();
				}
				vTrack = new VideoTrack(this, (int)id, state, codec);
				break;
			}
			case 2L:
			{
				AudioTrack audioTrack = new AudioTrack((int)id, this);
				MKVType[] path7 = new MKVType[3]
				{
					MKVType.___003C_003ETrackEntry,
					MKVType.___003C_003EAudio,
					MKVType.___003C_003ESamplingFrequency
				};
				EbmlFloat sf = (EbmlFloat)MKVType.findFirst(aTrack, path7);
				if (sf != null)
				{
					audioTrack.samplingFrequency = sf.getDouble();
				}
				aTracks.add(audioTrack);
				break;
			}
			case 17L:
			{
				SubtitlesTrack subsTrack = new SubtitlesTrack((int)id, this);
				subsTracks.add(subsTrack);
				break;
			}
			}
		}
		MKVType[] path5 = new MKVType[2]
		{
			MKVType.___003C_003ESegment,
			MKVType.___003C_003ECluster
		};
		Iterator iterator2 = MKVType.findList(t, ClassLiteral<EbmlMaster>.Value, path5).iterator();
		while (iterator2.hasNext())
		{
			EbmlMaster aCluster = (EbmlMaster)iterator2.next();
			MKVType[] path2 = new MKVType[2]
			{
				MKVType.___003C_003ECluster,
				MKVType.___003C_003ETimecode
			};
			long baseTimecode = ((EbmlUint)MKVType.findFirst(aCluster, path2)).getUint();
			Iterator iterator3 = aCluster.___003C_003Echildren.iterator();
			while (iterator3.hasNext())
			{
				EbmlBase child = (EbmlBase)iterator3.next();
				if (java.lang.Object.instancehelper_equals(MKVType.___003C_003ESimpleBlock, child.type))
				{
					MkvBlock b2 = (MkvBlock)child;
					b2.absoluteTimecode = b2.timecode + baseTimecode;
					putIntoRightBasket(b2);
				}
				else
				{
					if (!java.lang.Object.instancehelper_equals(MKVType.___003C_003EBlockGroup, child.type))
					{
						continue;
					}
					EbmlMaster group = (EbmlMaster)child;
					Iterator iterator4 = group.___003C_003Echildren.iterator();
					while (iterator4.hasNext())
					{
						EbmlBase grandChild = (EbmlBase)iterator4.next();
						if (grandChild.type == MKVType.___003C_003EBlock)
						{
							MkvBlock b = (MkvBlock)grandChild;
							b.absoluteTimecode = b.timecode + baseTimecode;
							putIntoRightBasket(b);
						}
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 98, 117, 184, 113, 115, 112, 121, 245,
		60, 234, 71, 113, 115, 112, 121, 245, 60, 234,
		72
	})]
	private void putIntoRightBasket(MkvBlock b)
	{
		if (b.trackNumber == vTrack.___003C_003EtrackNo)
		{
			vTrack.blocks.add(b);
			return;
		}
		for (int j = 0; j < aTracks.size(); j++)
		{
			AudioTrack audio = (AudioTrack)aTracks.get(j);
			if (b.trackNumber == audio.___003C_003EtrackNo)
			{
				audio.blocks.add(IndexedBlock.make(audio.framesCount, b));
				audio.framesCount = (int)(audio.framesCount + (nint)b.frameSizes.LongLength);
			}
		}
		for (int i = 0; i < subsTracks.size(); i++)
		{
			SubtitlesTrack subs = (SubtitlesTrack)subsTracks.get(i);
			if (b.trackNumber == subs.___003C_003EtrackNo)
			{
				subs.blocks.add(IndexedBlock.make(subs.framesCount, b));
				subs.framesCount = (int)(subs.framesCount + (nint)b.frameSizes.LongLength);
			}
		}
	}

	[LineNumberTable(526)]
	public virtual int getPictureWidth()
	{
		return pictureWidth;
	}

	[LineNumberTable(530)]
	public virtual int getPictureHeight()
	{
		return pictureHeight;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(535)]
	public virtual List getAudioTracks()
	{
		return aTracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 7, 66, 114, 110, 110 })]
	public virtual List getTracks()
	{
		ArrayList tracks = new ArrayList(aTracks);
		tracks.add(vTrack);
		tracks.addAll(subsTracks);
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 5, 66, 103, 110 })]
	public virtual List getVideoTracks()
	{
		ArrayList tracks = new ArrayList();
		tracks.add(vTrack);
		return tracks;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(554)]
	public virtual List getSubtitleTracks()
	{
		return subsTracks;
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;")]
	[LineNumberTable(558)]
	public virtual List getTree()
	{
		return t;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 2, 162, 110 })]
	public virtual void close()
	{
		channel.close();
	}

	[LineNumberTable(new byte[] { 159, 125, 130, 139, 118, 118, 118 })]
	static MKVDemuxer()
	{
		codecMapping = new HashMap();
		codecMapping.put("V_VP8", Codec.___003C_003EVP8);
		codecMapping.put("V_VP9", Codec.___003C_003EVP9);
		codecMapping.put("V_MPEG4/ISO/AVC", Codec.___003C_003EH264);
	}

	public void Dispose()
	{
		close();
	}
}
