using java.nio;

namespace org.jcodec.common;

public interface AudioEncoder
{
	ByteBuffer encode(ByteBuffer bb1, ByteBuffer bb2);
}
