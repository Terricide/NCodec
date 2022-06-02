using IKVM.Attributes;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public interface PacketSink
{
	[Throws(new string[] { "java.io.IOException" })]
	void outputVideoPacket(Packet p, VideoCodecMeta vcm);

	[Throws(new string[] { "java.io.IOException" })]
	void outputAudioPacket(Packet p, AudioCodecMeta acm);
}
