using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class Cut : Object
{
	public class Slice : Object
	{
		private double inSec;

		private double outSec;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 112, 98, 105, 106, 106 })]
		public Slice(double _in, double @out)
		{
			inSec = _in;
			outSec = @out;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(116)]
		internal static double access_0024000(Slice x0)
		{
			return x0.inSec;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(116)]
		internal static double access_0024100(Slice x0)
		{
			return x0.outSec;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(40)]
	public Cut()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/containers/mp4/MP4Util$Movie;Ljava/util/List<Lorg/jcodec/movtool/Cut$Slice;>;)Ljava/util/List<Lorg/jcodec/containers/mp4/MP4Util$Movie;>;")]
	[LineNumberTable(new byte[]
	{
		159, 110, 66, 136, 104, 114, 141, 104, 107, 102,
		105, 106, 127, 1, 115, 115, 227, 57, 234, 73,
		104, 127, 4, 120, 127, 0, 51, 169, 118, 134,
		101, 126, 114, 111, 241, 61, 233, 69, 137
	})]
	public virtual List cut(MP4Util.Movie movie, List commands)
	{
		MovieBox moov = movie.getMoov();
		TrakBox videoTrack = moov.getVideoTrack();
		if (videoTrack != null && videoTrack.getTimescale() != moov.getTimescale())
		{
			moov.fixTimescale(videoTrack.getTimescale());
		}
		TrakBox[] tracks = moov.getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox3 = tracks[i];
			Util.forceEditList(moov, trakBox3);
			List edits = trakBox3.getEdits();
			Iterator iterator = commands.iterator();
			while (iterator.hasNext())
			{
				Slice cut2 = (Slice)iterator.next();
				split(edits, Slice.access_0024000(cut2), moov, trakBox3);
				split(edits, Slice.access_0024100(cut2), moov, trakBox3);
			}
		}
		ArrayList result = new ArrayList();
		Iterator iterator2 = commands.iterator();
		while (iterator2.hasNext())
		{
			Slice cut = (Slice)iterator2.next();
			MovieBox clone = (MovieBox)NodeBox.cloneBox(moov, 16777216, BoxFactory.getDefault());
			TrakBox[] tracks2 = clone.getTracks();
			int num = tracks2.Length;
			for (int j = 0; j < num; j++)
			{
				TrakBox trakBox2 = tracks2[j];
				selectInner(trakBox2.getEdits(), cut, moov, trakBox2);
			}
			result.add(new MP4Util.Movie(movie.getFtyp(), clone));
		}
		long movDuration = 0L;
		TrakBox[] tracks3 = moov.getTracks();
		int num2 = tracks3.Length;
		for (int k = 0; k < num2; k++)
		{
			TrakBox trakBox = tracks3[k];
			selectOuter(trakBox.getEdits(), commands, moov, trakBox);
			trakBox.setEdits(trakBox.getEdits());
			movDuration = Math.max(movDuration, trakBox.getDuration());
		}
		moov.setDuration(movDuration);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/MP4Util$Movie;>;Ljava/util/List<Ljava/lang/String;>;Ljava/io/File;)V")]
	[LineNumberTable(new byte[]
	{
		159, 117, 162, 111, 106, 99, 131, 126, 151, 74,
		99, 227, 55, 234, 75
	})]
	private static void saveSlices(List slices, List names, File parentFile)
	{
		for (int i = 0; i < slices.size(); i++)
		{
			if (names.get(i) != null)
			{
				FileChannelWrapper @out = null;
				try
				{
					
					@out = NIOUtils.writableChannel(new File(parentFile, (string)names.get(i)));
					MP4Util.writeFullMovie(@out, (MP4Util.Movie)slices.get(i));
				}
				finally
				{
					NIOUtils.closeQuietly(@out);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;DLorg/jcodec/containers/mp4/boxes/MovieBox;Lorg/jcodec/containers/mp4/boxes/TrakBox;)V")]
	[LineNumberTable(new byte[] { 159, 93, 130, 121 })]
	private void split(List edits, double sec, MovieBox movie, TrakBox trakBox)
	{
		Util.split(movie, trakBox, ByteCodeHelper.d2l(sec * (double)movie.getTimescale()));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;Lorg/jcodec/movtool/Cut$Slice;Lorg/jcodec/containers/mp4/boxes/MovieBox;Lorg/jcodec/containers/mp4/boxes/TrakBox;)V")]
	[LineNumberTable(new byte[]
	{
		159, 96, 66, 117, 149, 100, 104, 105, 110, 113,
		103, 107, 99
	})]
	private void selectInner(List edits, Slice cut, MovieBox movie, TrakBox trakBox)
	{
		long inMv = ByteCodeHelper.d2l((double)movie.getTimescale() * Slice.access_0024000(cut));
		long outMv = ByteCodeHelper.d2l((double)movie.getTimescale() * Slice.access_0024100(cut));
		long editStart = 0L;
		ListIterator lit = edits.listIterator();
		while (lit.hasNext())
		{
			Edit edit = (Edit)lit.next();
			if (editStart + edit.getDuration() <= inMv || editStart >= outMv)
			{
				lit.remove();
			}
			editStart += edit.getDuration();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;Ljava/util/List<Lorg/jcodec/movtool/Cut$Slice;>;Lorg/jcodec/containers/mp4/boxes/MovieBox;Lorg/jcodec/containers/mp4/boxes/TrakBox;)V")]
	[LineNumberTable(new byte[]
	{
		159, 101, 98, 109, 109, 108, 127, 3, 31, 3,
		199, 100, 105, 109, 111, 106, 119, 8, 201, 107,
		102
	})]
	private void selectOuter(List edits, List commands, MovieBox movie, TrakBox trakBox)
	{
		long[] inMv = new long[commands.size()];
		long[] outMv = new long[commands.size()];
		for (int j = 0; j < commands.size(); j++)
		{
			inMv[j] = ByteCodeHelper.d2l(Slice.access_0024000((Slice)commands.get(j)) * (double)movie.getTimescale());
			outMv[j] = ByteCodeHelper.d2l(Slice.access_0024100((Slice)commands.get(j)) * (double)movie.getTimescale());
		}
		long editStartMv = 0L;
		ListIterator lit = edits.listIterator();
		while (lit.hasNext())
		{
			Edit edit = (Edit)lit.next();
			for (int i = 0; i < (nint)inMv.LongLength; i++)
			{
				if (editStartMv + edit.getDuration() > inMv[i] && editStartMv < outMv[i])
				{
					lit.remove();
				}
			}
			editStartMv += edit.getDuration();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 102, 107, 134, 167, 103, 135, 99,
		131, 115, 114, 127, 2, 103, 142, 105, 101, 117,
		101, 200, 144, 100, 100, 136, 106, 159, 11, 100,
		159, 31, 112, 143, 159, 31, 112, 114, 143, 148,
		101, 104, 101, 104, 127, 2, 104, 230, 58, 101,
		104, 101, 104, 127, 2, 104, 99, 99
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 1)
		{
			java.lang.System.@out.println("Syntax: cut [-command arg]...[-command arg] [-self] <movie file>\n\tCreates a reference movie out of the file and applies a set of changes specified by the commands to it.");
			java.lang.System.exit(-1);
		}
		ArrayList slices = new ArrayList();
		ArrayList sliceNames = new ArrayList();
		int selfContained = 0;
		int shift = 0;
		while (true)
		{
			if (String.instancehelper_equals("-cut", args[shift]))
			{
				string[] pt = StringUtils.splitS(args[shift + 1], ":");
				((List)slices).add((object)new Slice(Integer.parseInt(pt[0]), Integer.parseInt(pt[1])));
				if ((nint)pt.LongLength > 2)
				{
					((List)sliceNames).add((object)pt[2]);
				}
				else
				{
					((List)sliceNames).add((object)null);
				}
				shift += 2;
			}
			else
			{
				if (!String.instancehelper_equals("-self", args[shift]))
				{
					break;
				}
				shift++;
				selfContained = 1;
			}
		}
		
		File source = new File(args[shift]);
		FileChannelWrapper input = null;
		FileChannelWrapper @out = null;
		ArrayList outs = new ArrayList();
		try
		{
			input = NIOUtils.readableChannel(source);
			MP4Util.Movie movie = MP4Util.createRefFullMovie(input, new StringBuilder().append("file://").append(source.getCanonicalPath()).toString());
			List slicesMovs;
			if (selfContained == 0)
			{
				
				@out = NIOUtils.writableChannel(new File(source.getParentFile(), new StringBuilder().append(JCodecUtil2.removeExtension(source.getName())).append(".ref.mov").toString()));
				slicesMovs = new Cut().cut(movie, slices);
				MP4Util.writeFullMovie(@out, movie);
			}
			else
			{
				
				@out = NIOUtils.writableChannel(new File(source.getParentFile(), new StringBuilder().append(JCodecUtil2.removeExtension(source.getName())).append(".self.mov").toString()));
				slicesMovs = new Cut().cut(movie, slices);
				new Strip().strip(movie.getMoov());
				new Flatten().flattenChannel(movie, @out);
			}
			saveSlices(slicesMovs, sliceNames, source.getParentFile());
		}
		catch
		{
			//try-fault
			((Channel)input)?.close();
			((Channel)@out)?.close();
			Iterator iterator = ((List)outs).iterator();
			while (iterator.hasNext())
			{
				org.jcodec.common.io.SeekableByteChannel o2 = (org.jcodec.common.io.SeekableByteChannel)iterator.next();
				o2.close();
			}
			throw;
		}
		((Channel)input)?.close();
		((Channel)@out)?.close();
		Iterator iterator2 = ((List)outs).iterator();
		while (iterator2.hasNext())
		{
			org.jcodec.common.io.SeekableByteChannel o = (org.jcodec.common.io.SeekableByteChannel)iterator2.next();
			o.close();
		}
	}
}
