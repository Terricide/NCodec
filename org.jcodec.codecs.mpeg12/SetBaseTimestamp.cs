using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace org.jcodec.codecs.mpeg12;

public class SetBaseTimestamp : FixTimestamp
{
	private int baseTs;

	private long firstPts;

	private bool video;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 97, 67, 233, 61, 201, 104, 104 })]
	public SetBaseTimestamp(bool video, int baseTs)
	{
		firstPts = -1L;
		this.video = video;
		this.baseTs = baseTs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 137, 162, 111, 127, 4 })]
	public static void main1(string[] args)
	{
		
		File file = new File(args[0]);
		new SetBaseTimestamp(String.instancehelper_equalsIgnoreCase("video", args[1]), Integer.parseInt(args[2])).fix(file);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 127, 4, 107, 136, 146 })]
	protected internal override long doWithTimestamp(int streamId, long pts, bool isPts)
	{
		if ((video && isVideo(streamId)) || (!video && isAudio(streamId)))
		{
			if (firstPts == -1)
			{
				firstPts = pts;
			}
			return pts - firstPts + baseTs;
		}
		return pts;
	}
}
