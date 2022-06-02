using java.nio;

namespace org.jcodec.codecs.mpeg12.bitstream;

public interface MPEGHeader
{
	void write(ByteBuffer bb);
}
