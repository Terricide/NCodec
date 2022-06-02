namespace org.jcodec.codecs.h264.decode.aso;

public interface Mapper
{
	bool leftAvailable(int i);

	bool topAvailable(int i);

	int getAddress(int i);

	int getMbX(int i);

	int getMbY(int i);

	bool topRightAvailable(int i);

	bool topLeftAvailable(int i);
}
