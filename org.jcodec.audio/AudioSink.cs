using IKVM.Attributes;
using java.nio;

namespace org.jcodec.audio;

public interface AudioSink
{
	[Throws(new string[] { "java.io.IOException" })]
	void writeFloat(FloatBuffer fb);
}
