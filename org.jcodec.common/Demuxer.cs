using IKVM.Attributes;
using java.io;
using java.lang;
using java.util;

namespace org.jcodec.common;

[Implements(new string[] { "java.io.Closeable" })]
public interface Demuxer : Closeable, AutoCloseable
{
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	List getVideoTracks();

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	List getAudioTracks();

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	List getTracks();
}
