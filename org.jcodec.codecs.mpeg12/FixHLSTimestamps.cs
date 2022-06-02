using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.util;

namespace org.jcodec.codecs.mpeg12;

public class FixHLSTimestamps : FixTimestamp
{
	private long[] lastPts;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 105, 113 })]
	public FixHLSTimestamps()
	{
		lastPts = new long[256];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 110, 99, 127, 2, 113, 105, 99,
		232, 59, 231, 71
	})]
	private void doIt(string wildCard, int startIdx)
	{
		Arrays.fill(lastPts, -1L);
		int i = startIdx;
		while (true)
		{
			
			File file = new File(String.format(wildCard, Integer.valueOf(i)));
			java.lang.System.@out.println(file.getAbsolutePath());
			if (!file.exists())
			{
				break;
			}
			fix(file);
			i++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 137, 130, 101, 138, 111 })]
	public static void main1(string[] args)
	{
		string wildCard = args[0];
		int startIdx = Integer.parseInt(args[1]);
		new FixHLSTimestamps().doIt(wildCard, startIdx);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 65, 67, 100, 99, 109, 106, 131, 106,
		119, 106, 106, 119, 138
	})]
	protected internal override long doWithTimestamp(int streamId, long pts, bool isPts)
	{
		if (!isPts)
		{
			return pts;
		}
		if (lastPts[streamId] == -1)
		{
			lastPts[streamId] = pts;
			return pts;
		}
		if (isVideo(streamId))
		{
			long[] array = lastPts;
			int num = streamId;
			long[] array2 = array;
			array2[num] += 3003L;
			return lastPts[streamId];
		}
		if (isAudio(streamId))
		{
			long[] array3 = lastPts;
			int num = streamId;
			long[] array2 = array3;
			array2[num] += 1920L;
			return lastPts[streamId];
		}
		
		throw new RuntimeException("Unexpected!!!");
	}
}
