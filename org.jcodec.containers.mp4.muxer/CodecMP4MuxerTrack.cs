using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.mpeg4.mp4;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.muxer;

public class CodecMP4MuxerTrack : MP4MuxerTrack
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class ByteArrayWrapper : Object
	{
		private byte[] bytes;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 97, 130, 105, 109 })]
		public ByteArrayWrapper(ByteBuffer bytes)
		{
			this.bytes = NIOUtils.toArray(bytes);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(187)]
		public virtual ByteBuffer get()
		{
			ByteBuffer result = ByteBuffer.wrap(bytes);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 94, 66, 105, 99 })]
		public override bool equals(object obj)
		{
			if (!(obj is ByteArrayWrapper))
			{
				return false;
			}
			bool result = Platform.arrayEqualsByte(bytes, ((ByteArrayWrapper)obj).bytes);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(199)]
		public override int hashCode()
		{
			int result = Arrays.hashCode(bytes);
			
			return result;
		}
	}

	[Signature("Ljava/util/Map<Lorg/jcodec/common/Codec;Ljava/lang/String;>;")]
	private static Map codec2fourcc;

	private Codec codec;

	[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	private List spsList;

	[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	private List ppsList;

	private ADTSParser.Header adtsHeader;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 162, 127, 8, 105, 114, 105 })]
	internal virtual void addVideoSampleEntry(VideoCodecMeta meta)
	{
		VideoSampleEntry se = VideoSampleEntry.videoSampleEntry((string)codec2fourcc.get(codec), meta.getSize(), "JCodec");
		if (meta.getPixelAspectRatio() != null)
		{
			se.add(PixelAspectExt.createPixelAspectExt(meta.getPixelAspectRatio()));
		}
		addSampleEntry(se);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 110, 109, 109, 113, 159, 2, 139,
		112, 105, 159, 5, 173
	})]
	public virtual void setCodecPrivateIfNeeded()
	{
		if (codec == Codec.___003C_003EH264)
		{
			List sps = selectUnique(spsList);
			List pps = selectUnique(ppsList);
			if (!sps.isEmpty() && !pps.isEmpty())
			{
				((SampleEntry)getEntries().get(0)).add(H264Utils.createAvcCFromPS(sps, pps, 4));
			}
			else
			{
				Logger.warn("CodecMP4MuxerTrack: Not adding a sample entry for h.264 track, missing any SPS/PPS NAL units");
			}
		}
		else if (codec == Codec.___003C_003EAAC)
		{
			if (adtsHeader != null)
			{
				((SampleEntry)getEntries().get(0)).add(EsdsBox.fromADTS(adtsHeader));
			}
			else
			{
				Logger.warn("CodecMP4MuxerTrack: Not adding a sample entry for AAC track, missing any ADTS headers.");
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[]
	{
		159, 105, 130, 103, 124, 110, 99, 103, 127, 1,
		111, 99
	})]
	private static List selectUnique(List bblist)
	{
		HashSet all = new HashSet();
		Iterator iterator = bblist.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			((Set)all).add((object)new ByteArrayWrapper(byteBuffer));
		}
		ArrayList result = new ArrayList();
		Iterator iterator2 = ((Set)all).iterator();
		while (iterator2.hasNext())
		{
			ByteArrayWrapper bs = (ByteArrayWrapper)iterator2.next();
			((List)result).add((object)bs.get());
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 162, 107, 104, 108, 108 })]
	public CodecMP4MuxerTrack(int trackId, MP4TrackType type, Codec codec)
		: base(trackId, type)
	{
		this.codec = codec;
		spsList = new ArrayList();
		ppsList = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 110, 136, 110, 187, 115, 104, 106,
		112, 104, 205, 138, 106
	})]
	public override void addFrame(Packet pkt)
	{
		if (codec == Codec.___003C_003EH264)
		{
			ByteBuffer result2 = pkt.getData();
			if (pkt.frameType == Packet.FrameType.___003C_003EUNKNOWN)
			{
				pkt.setFrameType((!H264Utils.isByteBufferIDRSlice(result2)) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY);
			}
			H264Utils.wipePSinplace(result2, spsList, ppsList);
			result2 = H264Utils.encodeMOVPacket(result2);
			pkt = Packet.createPacketWithData(pkt, result2);
		}
		else if (codec == Codec.___003C_003EAAC)
		{
			ByteBuffer result = pkt.getData();
			adtsHeader = ADTSParser.read(result);
			pkt = Packet.createPacketWithData(pkt, result);
		}
		base.addFrame(pkt);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 130, 151, 106, 105, 148, 205, 111, 127,
		7, 191, 6, 105, 173, 107
	})]
	public override void addFrameInternal(Packet pkt, int entryNo)
	{
		Preconditions.checkState((!finished) ? true : false, (object)"The muxer track has finished muxing");
		if (_timescale == -1)
		{
			if (adtsHeader != null)
			{
				_timescale = adtsHeader.getSampleRate();
			}
			else
			{
				_timescale = pkt.getTimescale();
			}
		}
		if (_timescale != pkt.getTimescale())
		{
			long num = pkt.getPts() * _timescale;
			long num2 = pkt.getTimescale();
			pkt.setPts((num2 != -1) ? (num / num2) : (-num));
			long num3 = pkt.getPts() * _timescale;
			long duration = pkt.getDuration();
			pkt.setDuration((duration != -1) ? (num3 / duration) : (-num3));
		}
		if (adtsHeader != null)
		{
			pkt.setDuration(1024L);
		}
		base.addFrameInternal(pkt, entryNo);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 111, 130, 119, 113, 123, 125, 104, 109, 104,
		99, 171, 135
	})]
	protected internal override Box finish(MovieHeaderBox mvhd)
	{
		Preconditions.checkState((!finished) ? true : false, (object)"The muxer track has finished muxing");
		if (getEntries().isEmpty())
		{
			if (codec == Codec.___003C_003EH264 && !spsList.isEmpty())
			{
				SeqParameterSet sps = SeqParameterSet.read(((ByteBuffer)spsList.get(0)).duplicate());
				Size size = H264Utils.getPicSize(sps);
				VideoCodecMeta meta = VideoCodecMeta.createSimpleVideoCodecMeta(size, ColorSpace.___003C_003EYUV420);
				addVideoSampleEntry(meta);
			}
			else
			{
				Logger.warn("CodecMP4MuxerTrack: Creating a track without sample entry");
			}
		}
		setCodecPrivateIfNeeded();
		Box result = base.finish(mvhd);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 91, 66, 122, 47, 167, 105 })]
	internal virtual void addAudioSampleEntry(AudioFormat format)
	{
		AudioSampleEntry ase = AudioSampleEntry.compressedAudioSampleEntry((string)codec2fourcc.get(codec), 1, 16, format.getChannels(), format.getSampleRate(), 0, 0, 0);
		addSampleEntry(ase);
	}

	[LineNumberTable(new byte[]
	{
		159, 131, 162, 171, 118, 118, 118, 118, 118, 118,
		118, 118, 118
	})]
	static CodecMP4MuxerTrack()
	{
		codec2fourcc = new HashMap();
		codec2fourcc.put(Codec.___003C_003EMP1, ".mp1");
		codec2fourcc.put(Codec.___003C_003EMP2, ".mp2");
		codec2fourcc.put(Codec.___003C_003EMP3, ".mp3");
		codec2fourcc.put(Codec.___003C_003EH264, "avc1");
		codec2fourcc.put(Codec.___003C_003EAAC, "mp4a");
		codec2fourcc.put(Codec.___003C_003EPRORES, "apch");
		codec2fourcc.put(Codec.___003C_003EJPEG, "mjpg");
		codec2fourcc.put(Codec.___003C_003EPNG, "png ");
		codec2fourcc.put(Codec.___003C_003EV210, "v210");
	}
}
