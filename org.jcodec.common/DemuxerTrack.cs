using IKVM.Attributes;
using org.jcodec.common.model;

namespace org.jcodec.common;

public interface DemuxerTrack
{
	[Throws(new string[] { "java.io.IOException" })]
	Packet nextFrame();

	DemuxerTrackMeta getMeta();
}
