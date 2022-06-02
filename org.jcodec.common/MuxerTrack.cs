using IKVM.Attributes;
using org.jcodec.common.model;

namespace org.jcodec.common;

public interface MuxerTrack
{
	[Throws(new string[] { "java.io.IOException" })]
	void addFrame(Packet p);
}
