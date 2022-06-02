using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public interface IBoxFactory
{
	Box newBox(Header h);
}
