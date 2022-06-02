using IKVM.Attributes;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public interface PacketSource
{
	[Throws(new string[] { "java.io.IOException" })]
	Packet inputVideoPacket();

	[Throws(new string[] { "java.io.IOException" })]
	Packet inputAudioPacket();
}
