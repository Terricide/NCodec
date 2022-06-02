using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio.channels;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.movtool;

public class Strip : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(46)]
	public Strip()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 104, 104, 101, 9, 199 })]
	public virtual void strip(MovieBox movie)
	{
		TrakBox[] tracks = movie.getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox track = tracks[i];
			stripTrack(movie, track);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 104, 104, 105, 167, 111, 100, 127,
		4, 108, 99, 106, 127, 0, 106, 147, 113, 101,
		99, 102, 104, 110, 127, 6, 28, 235, 69, 106,
		102, 125, 116, 116, 116, 126, 111, 127, 10
	})]
	public virtual void stripTrack(MovieBox movie, TrakBox track)
	{
		ChunkReader chunks = new ChunkReader(track);
		List edits = track.getEdits();
		List oldEdits = deepCopy(edits);
		ArrayList result = new ArrayList();
		Chunk chunk;
		while ((chunk = chunks.next()) != null)
		{
			int intersects = 0;
			Iterator iterator = oldEdits.iterator();
			while (iterator.hasNext())
			{
				Edit edit = (Edit)iterator.next();
				if (edit.getMediaTime() != -1)
				{
					long editS = edit.getMediaTime();
					long editE = edit.getMediaTime() + track.rescale(edit.getDuration(), movie.getTimescale());
					long chunkS = chunk.getStartTv();
					long chunkE = chunk.getStartTv() + chunk.getDuration();
					intersects = (this.intersects(editS, editE, chunkS, chunkE) ? 1 : 0);
					if (intersects != 0)
					{
						break;
					}
				}
			}
			if (intersects == 0)
			{
				for (int i = 0; i < oldEdits.size(); i++)
				{
					if (((Edit)oldEdits.get(i)).getMediaTime() >= chunk.getStartTv() + chunk.getDuration())
					{
						((Edit)edits.get(i)).shift(-chunk.getDuration());
					}
				}
			}
			else
			{
				((List)result).add((object)chunk);
			}
		}
		NodeBox stbl = (NodeBox)NodeBox.findFirstPath(track, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"));
		stbl.replace("stts", getTimeToSamples(result));
		stbl.replace("stsz", getSampleSizes(result));
		stbl.replace("stsc", getSamplesToChunk(result));
		stbl.removeChildren(new string[2] { "stco", "co64" });
		stbl.add(getChunkOffsets(result));
		((MediaHeaderBox)NodeBox.findFirstPath(track, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"))).setDuration(totalDuration(result));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;)Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	[LineNumberTable(new byte[] { 159, 111, 98, 103, 124, 110, 99 })]
	private List deepCopy(List edits)
	{
		ArrayList newList = new ArrayList();
		Iterator iterator = edits.iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			newList.add(Edit.createEdit(edit));
		}
		return newList;
	}

	[LineNumberTable(219)]
	private bool intersects(long a, long b, long c, long d)
	{
		return ((a >= c && a < d) || (b >= c && b < d) || (c >= a && c < b) || (d >= a && d < b)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/Chunk;>;)Lorg/jcodec/containers/mp4/boxes/TimeToSampleBox;")]
	[LineNumberTable(new byte[]
	{
		159, 106, 98, 103, 101, 127, 1, 107, 111, 101,
		111, 99, 137, 144, 127, 0, 106, 101, 111, 99,
		132, 229, 57, 233, 74, 102, 101, 111
	})]
	public virtual TimeToSampleBox getTimeToSamples(List chunks)
	{
		ArrayList tts = new ArrayList();
		int curTts = -1;
		int cnt = 0;
		Iterator iterator = chunks.iterator();
		while (iterator.hasNext())
		{
			Chunk chunk = (Chunk)iterator.next();
			if (chunk.getSampleDur() > 0)
			{
				if (curTts == -1 || curTts != chunk.getSampleDur())
				{
					if (curTts != -1)
					{
						tts.add(new TimeToSampleBox.TimeToSampleEntry(cnt, curTts));
					}
					cnt = 0;
					curTts = chunk.getSampleDur();
				}
				cnt += chunk.getSampleCount();
				continue;
			}
			int[] sampleDurs = chunk.getSampleDurs();
			int num = sampleDurs.Length;
			for (int i = 0; i < num; i++)
			{
				int dur = sampleDurs[i];
				if (curTts == -1 || curTts != dur)
				{
					if (curTts != -1)
					{
						tts.add(new TimeToSampleBox.TimeToSampleEntry(cnt, curTts));
					}
					cnt = 0;
					curTts = dur;
				}
				cnt++;
			}
		}
		if (cnt > 0)
		{
			tts.add(new TimeToSampleBox.TimeToSampleEntry(cnt, curTts));
		}
		TimeToSampleBox result = TimeToSampleBox.createTimeToSampleBox((TimeToSampleBox.TimeToSampleEntry[])tts.toArray(new TimeToSampleBox.TimeToSampleEntry[0]));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/Chunk;>;)Lorg/jcodec/containers/mp4/boxes/SampleSizesBox;")]
	[LineNumberTable(new byte[]
	{
		159, 99, 130, 117, 124, 106, 108, 113, 131, 101,
		139, 105, 100, 127, 1, 121, 109, 99
	})]
	public virtual SampleSizesBox getSampleSizes(List chunks)
	{
		int nSamples = 0;
		int prevSize = ((Chunk)chunks.get(0)).getSampleSize();
		Iterator iterator = chunks.iterator();
		while (iterator.hasNext())
		{
			Chunk chunk2 = (Chunk)iterator.next();
			nSamples += chunk2.getSampleCount();
			if (prevSize == 0 && chunk2.getSampleSize() != 0)
			{
				
				throw new RuntimeException("Mixed sample sizes not supported");
			}
		}
		if (prevSize > 0)
		{
			SampleSizesBox result = SampleSizesBox.createSampleSizesBox(prevSize, nSamples);
			
			return result;
		}
		int[] sizes = new int[nSamples];
		int startSample = 0;
		Iterator iterator2 = chunks.iterator();
		while (iterator2.hasNext())
		{
			Chunk chunk = (Chunk)iterator2.next();
			ByteCodeHelper.arraycopy_primitive_4(chunk.getSampleSizes(), 0, sizes, startSample, chunk.getSampleCount());
			startSample += chunk.getSampleCount();
		}
		SampleSizesBox result2 = SampleSizesBox.createSampleSizesBox2(sizes);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/Chunk;>;)Lorg/jcodec/containers/mp4/boxes/SampleToChunkBox;")]
	[LineNumberTable(new byte[]
	{
		159, 94, 130, 103, 104, 109, 104, 105, 103, 108,
		109, 105, 105, 108, 115, 100, 101, 104, 132, 103,
		102, 102, 115
	})]
	public virtual SampleToChunkBox getSamplesToChunk(List chunks)
	{
		ArrayList result = new ArrayList();
		Iterator it = chunks.iterator();
		Chunk chunk = (Chunk)it.next();
		int curSz = chunk.getSampleCount();
		int curEntry = chunk.getEntry();
		int first = 1;
		int cnt = 1;
		while (it.hasNext())
		{
			chunk = (Chunk)it.next();
			int newSz = chunk.getSampleCount();
			int newEntry = chunk.getEntry();
			if (curSz != newSz || curEntry != newEntry)
			{
				result.add(new SampleToChunkBox.SampleToChunkEntry(first, curSz, curEntry));
				curSz = newSz;
				curEntry = newEntry;
				first += cnt;
				cnt = 0;
			}
			cnt++;
		}
		if (cnt > 0)
		{
			result.add(new SampleToChunkBox.SampleToChunkEntry(first, curSz, curEntry));
		}
		SampleToChunkBox result2 = SampleToChunkBox.createSampleToChunkBox((SampleToChunkBox.SampleToChunkEntry[])result.toArray(new SampleToChunkBox.SampleToChunkEntry[0]));
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/Chunk;>;)Lorg/jcodec/containers/mp4/boxes/Box;")]
	[LineNumberTable(new byte[]
	{
		159, 109, 98, 109, 99, 99, 125, 115, 99, 111,
		99
	})]
	public virtual Box getChunkOffsets(List chunks)
	{
		long[] result = new long[chunks.size()];
		int longBox = 0;
		int i = 0;
		Iterator iterator = chunks.iterator();
		while (iterator.hasNext())
		{
			Chunk chunk = (Chunk)iterator.next();
			if (chunk.getOffset() >= 4294967296L)
			{
				longBox = 1;
			}
			int num = i;
			i++;
			result[num] = chunk.getOffset();
		}
		FullBox result2 = ((longBox == 0) ? ((FullBox)ChunkOffsetsBox.createChunkOffsetsBox(result)) : ((FullBox)ChunkOffsets64Box.createChunkOffsets64Box(result)));
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/Chunk;>;)J")]
	[LineNumberTable(new byte[] { 159, 113, 98, 100, 124, 107, 99 })]
	private long totalDuration(List result)
	{
		long duration = 0L;
		Iterator iterator = result.iterator();
		while (iterator.hasNext())
		{
			Chunk chunk = (Chunk)iterator.next();
			duration += chunk.getDuration();
		}
		return duration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 130, 66, 102, 112, 135, 99, 131, 116, 111,
		104, 104, 127, 20, 113, 140, 100, 103, 100, 234,
		61, 100, 103, 100, 137
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 2)
		{
			java.lang.System.@out.println("Syntax: strip <ref movie> <out movie>");
			java.lang.System.exit(-1);
		}
		FileChannelWrapper input = null;
		FileChannelWrapper @out = null;
		try
		{
			
			input = NIOUtils.readableChannel(new File(args[0]));
			
			File file = new File(args[1]);
			Platform.deleteFile(file);
			@out = NIOUtils.writableChannel(file);
			FileChannelWrapper input2 = input;
			StringBuilder stringBuilder = new StringBuilder().append("file://");
			
			MP4Util.Movie movie = MP4Util.createRefFullMovie(input2, stringBuilder.append(new File(args[0]).getAbsolutePath()).toString());
			new Strip().strip(movie.getMoov());
			MP4Util.writeFullMovie(@out, movie);
		}
		catch
		{
			//try-fault
			((Channel)input)?.close();
			((Channel)@out)?.close();
			throw;
		}
		((Channel)input)?.close();
		((Channel)@out)?.close();
	}
}
