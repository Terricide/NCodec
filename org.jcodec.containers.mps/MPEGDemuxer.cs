using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.containers.mps;

[Implements(new string[] { "org.jcodec.common.Demuxer" })]
public interface MPEGDemuxer : Demuxer, Closeable, AutoCloseable
{
	[Implements(new string[] { "org.jcodec.common.DemuxerTrack" })]
	public interface MPEGDemuxerTrack : DemuxerTrack
	{
		void ignore();

		[Throws(new string[] { "java.io.IOException" })]
		Packet nextFrameWithBuffer(ByteBuffer bb);

		DemuxerTrackMeta getMeta();

		int getSid();

		[Signature("()Ljava/util/List<Lorg/jcodec/containers/mps/PESPacket;>;")]
		List getPending();
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/containers/mps/MPEGDemuxer$MPEGDemuxerTrack;>;")]
	List getTracks();

	[Signature("()Ljava/util/List<+Lorg/jcodec/containers/mps/MPEGDemuxer$MPEGDemuxerTrack;>;")]
	List getVideoTracks();

	[Signature("()Ljava/util/List<+Lorg/jcodec/containers/mps/MPEGDemuxer$MPEGDemuxerTrack;>;")]
	List getAudioTracks();
}
