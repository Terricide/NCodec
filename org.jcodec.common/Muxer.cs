using IKVM.Attributes;

namespace org.jcodec.common;

public interface Muxer
{
	MuxerTrack addVideoTrack(Codec c, VideoCodecMeta vcm);

	MuxerTrack addAudioTrack(Codec c, AudioCodecMeta acm);

	[Throws(new string[] { "java.io.IOException" })]
	void finish();
}
