using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.encode;

public interface RateControl
{
	int startPicture(Size s, int i, SliceType st);

	int initialQpDelta();

	int accept(int i);
}
