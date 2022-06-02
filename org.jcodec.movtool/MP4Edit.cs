using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public interface MP4Edit
{
	void applyToFragment(MovieBox mb, MovieFragmentBox[] mfbarr);

	void apply(MovieBox mb);
}
