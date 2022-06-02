using java.nio;

namespace org.jcodec.audio;

public interface AudioFilter
{
	void filter(FloatBuffer[] fbarr1, long[] larr, FloatBuffer[] fbarr2);

	int getDelay();

	int getNInputs();

	int getNOutputs();
}
