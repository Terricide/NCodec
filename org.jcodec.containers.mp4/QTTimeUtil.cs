using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.containers.mp4.demuxer;

namespace org.jcodec.containers.mp4;

public class QTTimeUtil : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 162, 105, 99, 100, 127, 5, 106, 99,
		112, 109, 115, 108, 131, 101, 134
	})]
	public static long mediaToEdited(TrakBox trak, long mediaTv, int movieTimescale)
	{
		if (trak.getEdits() == null)
		{
			return mediaTv;
		}
		long accum = 0L;
		Iterator iterator = trak.getEdits().iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			if (mediaTv < edit.getMediaTime())
			{
				return accum;
			}
			long duration = trak.rescale(edit.getDuration(), movieTimescale);
			if (edit.getMediaTime() != -1 && mediaTv >= edit.getMediaTime() && mediaTv < edit.getMediaTime() + duration)
			{
				accum += mediaTv - edit.getMediaTime();
				break;
			}
			accum += duration;
		}
		return accum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 130, 105, 99, 100, 127, 2, 144, 103,
		172, 101, 131
	})]
	public static long editedToMedia(TrakBox trak, long editedTv, int movieTimescale)
	{
		if (trak.getEdits() == null)
		{
			return editedTv;
		}
		long accum = 0L;
		Iterator iterator = trak.getEdits().iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			long duration = trak.rescale(edit.getDuration(), movieTimescale);
			if (accum + duration > editedTv)
			{
				return edit.getMediaTime() + editedTv - accum;
			}
			accum += duration;
		}
		return accum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 124, 104, 100, 102, 109, 110, 118,
		167
	})]
	public static long frameToTimevalue(TrakBox trak, int frameNumber)
	{
		TimeToSampleBox stts = (TimeToSampleBox)NodeBox.findFirstPath(trak, ClassLiteral<TimeToSampleBox>.Value, Box.path("mdia.minf.stbl.stts"));
		TimeToSampleBox.TimeToSampleEntry[] timeToSamples = stts.getEntries();
		long pts = 0L;
		int sttsInd = 0;
		int sttsSubInd;
		for (sttsSubInd = frameNumber; sttsSubInd >= timeToSamples[sttsInd].getSampleCount(); sttsInd++)
		{
			sttsSubInd -= timeToSamples[sttsInd].getSampleCount();
			pts += timeToSamples[sttsInd].getSampleCount() * timeToSamples[sttsInd].getSampleDuration();
		}
		return pts + timeToSamples[sttsInd].getSampleDuration() * sttsSubInd;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 130, 104, 136, 118, 117, 38, 169 })]
	public static int tv2QTFrameNo(MovieBox movie, long tv)
	{
		TrakBox videoTrack = movie.getVideoTrack();
		TrakBox timecodeTrack = movie.getTimecodeTrack();
		if (timecodeTrack != null && BoxUtil.containsBox2(videoTrack, "tref", "tmcd"))
		{
			RationalLarge.___003Cclinit_003E();
			int result = timevalueToTimecodeFrame(timecodeTrack, new RationalLarge(tv, videoTrack.getTimescale()), movie.getTimescale());
			
			return result;
		}
		int result2 = timevalueToFrame(videoTrack, tv);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 83, 66, 111 })]
	public static int timevalueToTimecodeFrame(TrakBox timecodeTrack, RationalLarge tv, int movieTimescale)
	{
		TimecodeSampleEntry se = (TimecodeSampleEntry)timecodeTrack.getSampleEntries()[0];
		long num = 2u * tv.multiplyS(se.getTimescale());
		long num2 = se.getFrameDuration();
		return (int)(((num2 != -1) ? (num / num2) : (-num)) + 1u) / 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 162, 127, 2, 99, 115, 118, 119, 247,
		61, 234, 70
	})]
	public static int timevalueToFrame(TrakBox trak, long tv)
	{
		TimeToSampleBox.TimeToSampleEntry[] tts = ((TimeToSampleBox)NodeBox.findFirstPath(trak, ClassLiteral<TimeToSampleBox>.Value, Box.path("mdia.minf.stbl.stts"))).getEntries();
		int frame = 0;
		int i = 0;
		while (tv > 0u && i < (nint)tts.LongLength)
		{
			long num = tv;
			long num2 = tts[i].getSampleDuration();
			long rem = ((num2 != -1) ? (num / num2) : (-num));
			tv -= tts[i].getSampleCount() * tts[i].getSampleDuration();
			frame = (int)(frame + ((tv <= 0u) ? rem : tts[i].getSampleCount()));
			i++;
		}
		return frame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 80, 66, 124, 137, 127, 7, 110, 127, 39,
		103, 127, 39, 103, 159, 27
	})]
	public static string formatTimecode(TrakBox timecodeTrack, int counter)
	{
		TimecodeSampleEntry tmcd = (TimecodeSampleEntry)NodeBox.findFirstPath(timecodeTrack, ClassLiteral<TimecodeSampleEntry>.Value, Box.path("mdia.minf.stbl.stsd.tmcd"));
		int nf = (sbyte)tmcd.getNumFrames();
		object[] array = new object[1];
		int num = counter;
		array[0] = Integer.valueOf((nf != -1) ? (num % nf) : 0);
		string tc = String.format("%02d", array);
		int num2 = counter;
		counter = ((nf != -1) ? (num2 / nf) : (-num2));
		StringBuilder stringBuilder = new StringBuilder();
		object[] array2 = new object[1];
		int num3 = counter;
		array2[0] = Integer.valueOf((60 != -1) ? (num3 % 60) : 0);
		tc = stringBuilder.append(String.format("%02d", array2)).append(":").append(tc)
			.toString();
		counter /= 60;
		StringBuilder stringBuilder2 = new StringBuilder();
		object[] array3 = new object[1];
		int num4 = counter;
		array3[0] = Integer.valueOf((60 != -1) ? (num4 % 60) : 0);
		tc = stringBuilder2.append(String.format("%02d", array3)).append(":").append(tc)
			.toString();
		counter /= 60;
		return new StringBuilder().append(String.format("%02d", Integer.valueOf(counter))).append(":").append(tc)
			.toString();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(25)]
	public QTTimeUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 104, 100, 138, 100, 124, 106, 99 })]
	public static long getEditedDuration(TrakBox track)
	{
		List edits = track.getEdits();
		if (edits == null)
		{
			long duration2 = track.getDuration();
			
			return duration2;
		}
		long duration = 0L;
		Iterator iterator = edits.iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			duration += edit.getDuration();
		}
		return duration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 130, 136, 149 })]
	public static int qtPlayerFrameNo(MovieBox movie, int mediaFrameNo)
	{
		TrakBox videoTrack = movie.getVideoTrack();
		long editedTv = mediaToEdited(videoTrack, frameToTimevalue(videoTrack, mediaFrameNo), movie.getTimescale());
		int result = tv2QTFrameNo(movie, editedTv);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 98, 104, 149, 117, 127, 105, 56 })]
	public static string qtPlayerTime(MovieBox movie, int mediaFrameNo)
	{
		TrakBox videoTrack = movie.getVideoTrack();
		long editedTv = mediaToEdited(videoTrack, frameToTimevalue(videoTrack, mediaFrameNo), movie.getTimescale());
		long num = videoTrack.getTimescale();
		int sec = (int)((num != -1) ? (editedTv / num) : (-editedTv));
		StringBuilder stringBuilder = new StringBuilder().append(String.format("%02d", Integer.valueOf(sec / 3600))).append("_");
		object[] array = new object[1];
		array[0] = Integer.valueOf(((3600 != -1) ? (sec % 3600) : 0) / 60);
		StringBuilder stringBuilder2 = stringBuilder.append(String.format("%02d", array)).append("_");
		object[] array2 = new object[1];
		array2[0] = Integer.valueOf((60 != -1) ? (sec % 60) : 0);
		string result = stringBuilder2.append(String.format("%02d", array2)).toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 94, 66, 104, 149, 104, 104, 159, 5, 98,
		103, 103, 112, 38, 231, 61
	})]
	public static string qtPlayerTimecodeFromMovie(MovieBox movie, TimecodeMP4DemuxerTrack timecodeTrack, int mediaFrameNo)
	{
		TrakBox videoTrack = movie.getVideoTrack();
		long editedTv = mediaToEdited(videoTrack, frameToTimevalue(videoTrack, mediaFrameNo), movie.getTimescale());
		TrakBox tt = timecodeTrack.getBox();
		int ttTimescale = tt.getTimescale();
		long num = editedTv * ttTimescale;
		long num2 = videoTrack.getTimescale();
		long ttTv = editedToMedia(tt, (num2 != -1) ? (num / num2) : (-num), movie.getTimescale());
		string result = formatTimecode(timecodeTrack.getBox(), timecodeTrack.getStartTimecode() + timevalueToTimecodeFrame(timecodeTrack.getBox(), new RationalLarge(ttTv, ttTimescale), movie.getTimescale()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 104, 104, 145, 98, 103, 103, 245,
		61
	})]
	public static string qtPlayerTimecode(TimecodeMP4DemuxerTrack timecodeTrack, RationalLarge tv, int movieTimescale)
	{
		TrakBox tt = timecodeTrack.getBox();
		int ttTimescale = tt.getTimescale();
		long ttTv = editedToMedia(tt, tv.multiplyS(ttTimescale), movieTimescale);
		string result = formatTimecode(timecodeTrack.getBox(), timecodeTrack.getStartTimecode() + timevalueToTimecodeFrame(timecodeTrack.getBox(), new RationalLarge(ttTv, ttTimescale), movieTimescale));
		
		return result;
	}
}
