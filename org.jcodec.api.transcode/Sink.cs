using IKVM.Attributes;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public interface Sink
{
	[Throws(new string[] { "java.io.IOException" })]
	void init();

	ColorSpace getInputColor();

	[Throws(new string[] { "java.io.IOException" })]
	void outputVideoFrame(VideoFrameWithPacket vfwp);

	[Throws(new string[] { "java.io.IOException" })]
	void finish();

	[Throws(new string[] { "java.io.IOException" })]
	void outputAudioFrame(AudioFrameWithPacket afwp);

	void setOption(Options o, object obj);

	bool isVideo();

	bool isAudio();
}
