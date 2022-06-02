using IKVM.Attributes;
using java.nio;
using org.jcodec.common;

namespace org.jcodec.audio;

public interface AudioSource
{
	[Throws(new string[] { "java.io.IOException" })]
	int readFloat(FloatBuffer fb);

	AudioFormat getFormat();
}
