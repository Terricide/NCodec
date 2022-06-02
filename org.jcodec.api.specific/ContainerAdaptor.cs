using org.jcodec.common.model;

namespace org.jcodec.api.specific;

public interface ContainerAdaptor
{
	Picture decodeFrame(Packet p, byte[][] barr);

	byte[][] allocatePicture();

	MediaInfo getMediaInfo();

	bool canSeek(Packet p);
}
