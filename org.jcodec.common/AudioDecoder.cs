using IKVM.Attributes;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.common;

public interface AudioDecoder
{
	[Throws(new string[] { "java.io.IOException" })]
	AudioBuffer decodeFrame(ByteBuffer bb1, ByteBuffer bb2);

	[Throws(new string[] { "java.io.IOException" })]
	AudioCodecMeta getCodecMeta(ByteBuffer bb);
}
