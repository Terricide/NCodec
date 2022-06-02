using IKVM.Attributes;

namespace org.jcodec.common;

[Implements(new string[] { "org.jcodec.common.DemuxerTrack" })]
public interface SeekableDemuxerTrack : DemuxerTrack
{
	[Throws(new string[] { "java.io.IOException" })]
	void seek(double d);

	[Throws(new string[] { "java.io.IOException" })]
	bool gotoFrame(long l);

	long getCurFrame();

	[Throws(new string[] { "java.io.IOException" })]
	bool gotoSyncFrame(long l);
}
