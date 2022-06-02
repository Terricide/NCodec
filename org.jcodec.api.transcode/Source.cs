using IKVM.Attributes;
using org.jcodec.common;

namespace org.jcodec.api.transcode;

public interface Source
{
	[Throws(new string[] { "java.io.IOException" })]
	void init(PixelStore ps);

	[Throws(new string[] { "java.io.IOException" })]
	void seekFrames(int i);

	[Throws(new string[] { "java.io.IOException" })]
	VideoFrameWithPacket getNextVideoFrame();

	[Throws(new string[] { "java.io.IOException" })]
	AudioFrameWithPacket getNextAudioFrame();

	void finish();

	bool haveAudio();

	void setOption(Options o, object obj);

	VideoCodecMeta getVideoCodecMeta();

	AudioCodecMeta getAudioCodecMeta();

	bool isVideo();

	bool isAudio();
}
