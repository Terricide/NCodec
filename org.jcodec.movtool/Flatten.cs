using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.movtool;

public class Flatten : Object
{
	public interface ProgressListener
	{
		void trigger(int i);
	}

	[Signature("Ljava/util/List<Lorg/jcodec/movtool/Flatten$ProgressListener;>;")]
	public List listeners;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 66, 105, 108 })]
	public Flatten()
	{
		listeners = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 124, 98, 104, 136, 105, 113, 106, 136, 105,
		104, 137, 105, 155, 138, 105, 107, 107, 107, 106,
		107, 110, 112, 144, 118, 112, 109, 239, 57, 236,
		75, 100, 110, 104, 134, 102, 138, 127, 5, 127,
		5, 103, 229, 54, 236, 77, 102, 99, 112, 112,
		135, 111, 134, 107, 43, 169, 140, 106, 136, 108,
		103, 113, 148, 106, 118
	})]
	public virtual void flattenChannel(MP4Util.Movie movie, org.jcodec.common.io.SeekableByteChannel @out)
	{
		FileTypeBox ftyp = movie.getFtyp();
		MovieBox moov = movie.getMoov();
		if (!moov.isPureRefMovie())
		{
			
			throw new IllegalArgumentException("movie should be reference");
		}
		@out.setPosition(0L);
		MP4Util.writeFullMovie(@out, movie);
		int extraSpace = calcSpaceReq(moov);
		ByteBuffer buf = ByteBuffer.allocate(extraSpace);
		@out.write(buf);
		long mdatOff = @out.position();
		writeHeader(Header.createHeader("mdat", 4294967297L), @out);
		org.jcodec.common.io.SeekableByteChannel[][] inputs = getInputs(moov);
		TrakBox[] tracks = moov.getTracks();
		ChunkReader[] readers = new ChunkReader[(nint)tracks.LongLength];
		ChunkWriter[] writers = new ChunkWriter[(nint)tracks.LongLength];
		Chunk[] head = new Chunk[(nint)tracks.LongLength];
		int totalChunks = 0;
		int writtenChunks = 0;
		int lastProgress = 0;
		long[] off = new long[(nint)tracks.LongLength];
		for (int k = 0; k < (nint)tracks.LongLength; k++)
		{
			readers[k] = new ChunkReader(tracks[k]);
			totalChunks += readers[k].size();
			writers[k] = new ChunkWriter(tracks[k], inputs[k], @out);
			head[k] = readers[k].next();
			if (tracks[k].isVideo())
			{
				off[k] = 2 * moov.getTimescale();
			}
		}
		while (true)
		{
			int min = -1;
			for (int j = 0; j < (nint)readers.LongLength; j++)
			{
				if (head[j] == null)
				{
					continue;
				}
				if (min == -1)
				{
					min = j;
					continue;
				}
				long iTv = moov.rescale(head[j].getStartTv(), tracks[j].getTimescale()) + off[j];
				long minTv = moov.rescale(head[min].getStartTv(), tracks[min].getTimescale()) + off[min];
				if (iTv < minTv)
				{
					min = j;
				}
			}
			if (min == -1)
			{
				break;
			}
			writers[min].write(head[min]);
			head[min] = readers[min].next();
			writtenChunks++;
			lastProgress = calcProgress(totalChunks, writtenChunks, lastProgress);
		}
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			writers[i].apply();
		}
		long mdatSize = @out.position() - mdatOff;
		@out.setPosition(0L);
		MP4Util.writeFullMovie(@out, movie);
		long extra = mdatOff - @out.position();
		if (extra < 0u)
		{
			
			throw new RuntimeException("Not enough space to write the header");
		}
		writeHeader(Header.createHeader("free", extra), @out);
		@out.setPosition(mdatOff);
		writeHeader(Header.createHeader("mdat", mdatSize), @out);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 89, 66, 104, 131, 104, 141, 100, 42, 100,
		137
	})]
	public virtual void flatten(MP4Util.Movie movie, File video)
	{
		Platform.deleteFile(video);
		FileChannelWrapper @out = null;
		try
		{
			@out = NIOUtils.writableChannel(video);
			flattenChannel(movie, @out);
		}
		catch
		{
			//try-fault
			((Channel)@out)?.close();
			throw;
		}
		((Channel)@out)?.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 66, 99, 104, 104, 101, 105, 101, 238,
		60, 231, 70
	})]
	private int calcSpaceReq(MovieBox movie)
	{
		int sum = 0;
		TrakBox[] tracks = movie.getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			ChunkOffsetsBox stco = trakBox.getStco();
			if (stco != null)
			{
				sum = (int)(sum + (nint)stco.getChunkOffsets().LongLength * 4);
			}
		}
		return sum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 105, 66, 105, 104, 104, 105 })]
	private void writeHeader(Header header, org.jcodec.common.io.SeekableByteChannel @out)
	{
		ByteBuffer bb = ByteBuffer.allocate(16);
		header.write(bb);
		bb.flip();
		@out.write(bb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 101, 98, 104, 105, 107, 126, 100, 145, 105,
		111, 111, 107, 58, 169, 230, 53, 234, 77
	})]
	protected internal virtual org.jcodec.common.io.SeekableByteChannel[][] getInputs(MovieBox movie)
	{
		TrakBox[] tracks = movie.getTracks();
		org.jcodec.common.io.SeekableByteChannel[][] result = new org.jcodec.common.io.SeekableByteChannel[(nint)tracks.LongLength][];
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			DataRefBox drefs = (DataRefBox)NodeBox.findFirstPath(tracks[i], ClassLiteral<DataRefBox>.Value, Box.path("mdia.minf.dinf.dref"));
			if (drefs == null)
			{
				
				throw new RuntimeException("No data references");
			}
			List entries = drefs.getBoxes();
			org.jcodec.common.io.SeekableByteChannel[] e = new org.jcodec.common.io.SeekableByteChannel[entries.size()];
			org.jcodec.common.io.SeekableByteChannel[] inputs = new org.jcodec.common.io.SeekableByteChannel[entries.size()];
			for (int j = 0; j < (nint)e.LongLength; j++)
			{
				inputs[j] = resolveDataRef((Box)entries.get(j));
			}
			result[i] = inputs;
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 162, 112, 101, 100, 127, 2, 138 })]
	private int calcProgress(int totalChunks, int writtenChunks, int lastProgress)
	{
		int num = 100 * writtenChunks;
		int curProgress = ((totalChunks != -1) ? (num / totalChunks) : (-num));
		if (lastProgress < curProgress)
		{
			lastProgress = curProgress;
			Iterator iterator = listeners.iterator();
			while (iterator.hasNext())
			{
				ProgressListener pl = (ProgressListener)iterator.next();
				pl.trigger(lastProgress);
			}
		}
		return lastProgress;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 93, 66, 105, 109, 110, 113, 122, 105, 109,
		100, 113, 143
	})]
	public virtual org.jcodec.common.io.SeekableByteChannel resolveDataRef(Box box)
	{
		if (box is UrlBox)
		{
			string url = ((UrlBox)box).getUrl();
			if (!String.instancehelper_startsWith(url, "file://"))
			{
				
				throw new RuntimeException("Only file:// urls are supported in data reference");
			}
			
			FileChannelWrapper result = NIOUtils.readableChannel(new File(String.instancehelper_substring(url, 7)));
			
			return result;
		}
		if (box is AliasBox)
		{
			string uxPath = ((AliasBox)box).getUnixPath();
			if (uxPath == null)
			{
				
				throw new RuntimeException("Could not resolve alias");
			}
			FileChannelWrapper result2 = NIOUtils.readableChannel(new File(uxPath));
			
			return result2;
		}
		string message = new StringBuilder().append(box.getHeader().getFourcc()).append(" dataref type is not supported").toString();
		
		throw new RuntimeException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 132, 98, 102, 112, 135, 111, 104, 131, 116,
		104, 145, 100, 42, 100, 137
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 2)
		{
			java.lang.System.@out.println("Syntax: self <ref movie> <out movie>");
			java.lang.System.exit(-1);
		}
		
		File outFile = new File(args[1]);
		Platform.deleteFile(outFile);
		FileChannelWrapper input = null;
		try
		{
			
			input = NIOUtils.readableChannel(new File(args[0]));
			MP4Util.Movie movie = MP4Util.parseFullMovieChannel(input);
			new Flatten().flatten(movie, outFile);
		}
		catch
		{
			//try-fault
			((Channel)input)?.close();
			throw;
		}
		((Channel)input)?.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 98, 110 })]
	public virtual void addProgressListener(ProgressListener listener)
	{
		listeners.add(listener);
	}
}
