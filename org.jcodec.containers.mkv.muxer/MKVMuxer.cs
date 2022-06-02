using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mkv.boxes;

namespace org.jcodec.containers.mkv.muxer;

public class MKVMuxer : java.lang.Object, Muxer
{
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/muxer/MKVMuxerTrack;>;")]
	private List tracks;

	private MKVMuxerTrack audioTrack;

	private MKVMuxerTrack videoTrack;

	private EbmlMaster mkvInfo;

	private EbmlMaster mkvTracks;

	private EbmlMaster mkvCues;

	private EbmlMaster mkvSeekHead;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/boxes/EbmlMaster;>;")]
	private List clusterList;

	private SeekableByteChannel sink;

	[Signature("Ljava/util/Map<Lorg/jcodec/common/Codec;Ljava/lang/String;>;")]
	private static Map codec2mkv;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 162, 105, 104, 108, 108 })]
	public MKVMuxer(SeekableByteChannel s)
	{
		sink = s;
		tracks = new ArrayList();
		clusterList = new LinkedList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 145, 110, 110, 110, 142, 113, 110,
		142
	})]
	private EbmlMaster defaultEbmlHeader()
	{
		EbmlMaster master = (EbmlMaster)MKVType.createByType(MKVType.___003C_003EEBML);
		createLong(master, MKVType.___003C_003EEBMLVersion, 1L);
		createLong(master, MKVType.___003C_003EEBMLReadVersion, 1L);
		createLong(master, MKVType.___003C_003EEBMLMaxIDLength, 4L);
		createLong(master, MKVType.___003C_003EEBMLMaxSizeLength, 8L);
		createString(master, MKVType.___003C_003EDocType, "webm");
		createLong(master, MKVType.___003C_003EDocTypeVersion, 2L);
		createLong(master, MKVType.___003C_003EDocTypeReadVersion, 2L);
		return master;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 66, 113, 103, 110, 113, 145, 104, 100,
		127, 1, 127, 3, 107, 105, 99, 122, 113
	})]
	private EbmlMaster muxInfo()
	{
		EbmlMaster master = (EbmlMaster)MKVType.createByType(MKVType.___003C_003EInfo);
		int frameDurationInNanoseconds = 40000000;
		createLong(master, MKVType.___003C_003ETimecodeScale, frameDurationInNanoseconds);
		createString(master, MKVType.___003C_003EWritingApp, "JCodec");
		createString(master, MKVType.___003C_003EMuxingApp, "JCodec");
		List tracks2 = tracks;
		long max = 0L;
		Iterator iterator = tracks2.iterator();
		while (iterator.hasNext())
		{
			MKVMuxerTrack track = (MKVMuxerTrack)iterator.next();
			MkvBlock lastBlock = (MkvBlock)track.trackBlocks.get(track.trackBlocks.size() - 1);
			if (lastBlock.absoluteTimecode > max)
			{
				max = lastBlock.absoluteTimecode;
			}
		}
		createDouble(master, MKVType.___003C_003EDuration, (double)((max + 1u) * frameDurationInNanoseconds) * 1.0);
		createDate(master, MKVType.___003C_003EDateUTC, new Date());
		return master;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 162, 113, 116, 115, 145, 147, 115, 118,
		110, 127, 19, 210, 114, 126, 158, 137, 99, 110,
		127, 19, 210, 232, 37, 234, 93
	})]
	private EbmlMaster muxTracks()
	{
		EbmlMaster master = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ETracks);
		for (int i = 0; i < tracks.size(); i++)
		{
			MKVMuxerTrack track = (MKVMuxerTrack)tracks.get(i);
			EbmlMaster trackEntryElem = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ETrackEntry);
			createLong(trackEntryElem, MKVType.___003C_003ETrackNumber, track.trackNo);
			createLong(trackEntryElem, MKVType.___003C_003ETrackUID, track.trackNo);
			if (MKVMuxerTrack.MKVMuxerTrackType.___003C_003EVIDEO.equals(track.type))
			{
				createLong(trackEntryElem, MKVType.___003C_003ETrackType, 1L);
				createString(trackEntryElem, MKVType.___003C_003EName, new StringBuilder().append("Track ").append(i + 1).append(" Video")
					.toString());
				createString(trackEntryElem, MKVType.___003C_003ECodecID, track.codecId);
				EbmlMaster trackVideoElem = (EbmlMaster)MKVType.createByType(MKVType.___003C_003EVideo);
				createLong(trackVideoElem, MKVType.___003C_003EPixelWidth, track.videoMeta.getSize().getWidth());
				createLong(trackVideoElem, MKVType.___003C_003EPixelHeight, track.videoMeta.getSize().getHeight());
				trackEntryElem.add(trackVideoElem);
			}
			else
			{
				createLong(trackEntryElem, MKVType.___003C_003ETrackType, 2L);
				createString(trackEntryElem, MKVType.___003C_003EName, new StringBuilder().append("Track ").append(i + 1).append(" Audio")
					.toString());
				createString(trackEntryElem, MKVType.___003C_003ECodecID, track.codecId);
			}
			master.add(trackEntryElem);
		}
		return master;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 162, 103, 109, 109, 109 })]
	private EbmlMaster muxSeekHead()
	{
		SeekHeadFactory shi = new SeekHeadFactory();
		shi.add(mkvInfo);
		shi.add(mkvTracks);
		shi.add(mkvCues);
		EbmlMaster result = shi.indexSeekHead();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 98, 159, 23, 127, 7, 105, 110, 109,
		131, 137, 127, 7, 112
	})]
	private void muxCues()
	{
		CuesFactory cf = new CuesFactory(mkvSeekHead.size() + mkvInfo.size() + mkvTracks.size(), videoTrack.trackNo);
		Iterator iterator = videoTrack.trackBlocks.iterator();
		while (iterator.hasNext())
		{
			MkvBlock aBlock = (MkvBlock)iterator.next();
			EbmlMaster mkvCluster = singleBlockedCluster(aBlock);
			clusterList.add(mkvCluster);
			cf.add(CuesFactory.CuePointMock.make(mkvCluster));
		}
		EbmlMaster indexedCues = cf.createCues();
		Iterator iterator2 = indexedCues.___003C_003Echildren.iterator();
		while (iterator2.hasNext())
		{
			EbmlBase aCuePoint = (EbmlBase)iterator2.next();
			mkvCues.add(aCuePoint);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 162, 109, 104, 106 })]
	public static void createLong(EbmlMaster parent, MKVType type, long value)
	{
		EbmlUint se = (EbmlUint)MKVType.createByType(type);
		se.setUint(value);
		parent.add(se);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 85, 98, 109, 104, 106 })]
	public static void createString(EbmlMaster parent, MKVType type, string value)
	{
		EbmlString se = (EbmlString)MKVType.createByType(type);
		se.setString(value);
		parent.add(se);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 66, 109, 105, 191, 0, 3, 98, 159,
		18
	})]
	public static void createDouble(EbmlMaster parent, MKVType type, double value)
	{
		ClassCastException ex2;
		try
		{
			EbmlFloat se = (EbmlFloat)MKVType.createByType(type);
			se.setDouble(value);
			parent.add(se);
			return;
		}
		catch (System.Exception x)
		{
			ClassCastException ex = ByteCodeHelper.MapException<ClassCastException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		ClassCastException cce = ex2;
		string message = new StringBuilder().append("Element of type ").append(type).append(" can't be cast to EbmlFloat")
			.toString();
		
		throw new RuntimeException(message, cce);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 162, 109, 104, 106 })]
	public static void createDate(EbmlMaster parent, MKVType type, Date value)
	{
		EbmlDate se = (EbmlDate)MKVType.createByType(type);
		se.setDate(value);
		parent.add(se);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 90, 66, 113, 122, 104 })]
	private EbmlMaster singleBlockedCluster(MkvBlock aBlock)
	{
		EbmlMaster mkvCluster = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ECluster);
		createLong(mkvCluster, MKVType.___003C_003ETimecode, aBlock.absoluteTimecode - aBlock.timecode);
		mkvCluster.add(aBlock);
		return mkvCluster;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 130, 105, 108, 115, 109, 109, 151 })]
	public virtual MKVMuxerTrack createVideoTrack(VideoCodecMeta meta, string codecId)
	{
		if (videoTrack == null)
		{
			videoTrack = new MKVMuxerTrack();
			tracks.add(videoTrack);
			videoTrack.codecId = codecId;
			videoTrack.videoMeta = meta;
			videoTrack.trackNo = tracks.size();
		}
		return videoTrack;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 98, 103, 104, 137, 113, 109, 109, 118,
		109, 135, 109, 109, 109, 109, 127, 3, 107, 137,
		127, 1, 113
	})]
	public virtual void finish()
	{
		ArrayList mkvFile = new ArrayList();
		EbmlMaster ebmlHeader = defaultEbmlHeader();
		((List)mkvFile).add((object)ebmlHeader);
		EbmlMaster segmentElem = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ESegment);
		mkvInfo = muxInfo();
		mkvTracks = muxTracks();
		mkvCues = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ECues);
		mkvSeekHead = muxSeekHead();
		muxCues();
		segmentElem.add(mkvSeekHead);
		segmentElem.add(mkvInfo);
		segmentElem.add(mkvTracks);
		segmentElem.add(mkvCues);
		Iterator iterator = clusterList.iterator();
		while (iterator.hasNext())
		{
			EbmlMaster aCluster = (EbmlMaster)iterator.next();
			segmentElem.add(aCluster);
		}
		((List)mkvFile).add((object)segmentElem);
		Iterator iterator2 = ((List)mkvFile).iterator();
		while (iterator2.hasNext())
		{
			EbmlMaster el = (EbmlMaster)iterator2.next();
			el.mux(sink);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 98, 109, 104, 106 })]
	public static void createBuffer(EbmlMaster parent, MKVType type, ByteBuffer value)
	{
		EbmlBin se = (EbmlBin)MKVType.createByType(type);
		se.setBuf(value);
		parent.add(se);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(258)]
	public virtual MuxerTrack addVideoTrack(Codec codec, VideoCodecMeta meta)
	{
		MKVMuxerTrack result = createVideoTrack(meta, (string)codec2mkv.get(codec));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 77, 162, 108, 115, 124, 119 })]
	public virtual MuxerTrack addAudioTrack(Codec codec, AudioCodecMeta meta)
	{
		audioTrack = new MKVMuxerTrack();
		tracks.add(audioTrack);
		audioTrack.codecId = (string)codec2mkv.get(codec);
		audioTrack.trackNo = tracks.size();
		return audioTrack;
	}

	[LineNumberTable(new byte[] { 159, 123, 66, 139, 118, 118, 118 })]
	static MKVMuxer()
	{
		codec2mkv = new HashMap();
		codec2mkv.put(Codec.___003C_003EH264, "V_MPEG4/ISO/AVC");
		codec2mkv.put(Codec.___003C_003EVP8, "V_VP8");
		codec2mkv.put(Codec.___003C_003EVP9, "V_VP9");
	}
}
