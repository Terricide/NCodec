using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class MP4Util : Object
{
	public class Atom : Object
	{
		private long offset;

		private Header header;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 100, 66, 105, 104, 104 })]
		public Atom(Header header, long offset)
		{
			this.header = header;
			this.offset = offset;
		}

		[LineNumberTable(174)]
		public virtual long getOffset()
		{
			return offset;
		}

		[LineNumberTable(178)]
		public virtual Header getHeader()
		{
			return header;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 97, 130, 122 })]
		public virtual Box parseBox(org.jcodec.common.io.SeekableByteChannel input)
		{
			input.setPosition(offset + header.headerSize());
			Box result = BoxUtil.parseBox(NIOUtils.fetchFromChannel(input, (int)header.getBodySize()), header, BoxFactory.getDefault());
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 96, 162, 110, 117 })]
		public virtual void copy(org.jcodec.common.io.SeekableByteChannel input, WritableByteChannel @out)
		{
			input.setPosition(offset);
			NIOUtils.copy(input, @out, header.getSize());
		}
	}

	public class Movie : Object
	{
		private FileTypeBox ftyp;

		private MovieBox moov;

		[LineNumberTable(59)]
		public virtual MovieBox getMoov()
		{
			return moov;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(45)]
		internal static MovieBox access_0024000(Movie x0)
		{
			return x0.moov;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 130, 98, 105, 104, 104 })]
		public Movie(FileTypeBox ftyp, MovieBox moov)
		{
			this.ftyp = ftyp;
			this.moov = moov;
		}

		[LineNumberTable(55)]
		public virtual FileTypeBox getFtyp()
		{
			return ftyp;
		}
	}

	[Signature("Ljava/util/Map<Lorg/jcodec/common/Codec;Ljava/lang/String;>;")]
	private static Map codecMapping;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 119, 162, 99, 127, 5, 120, 112, 120, 150,
		102
	})]
	public static Movie parseFullMovieChannel(org.jcodec.common.io.SeekableByteChannel input)
	{
		FileTypeBox ftyp = null;
		Iterator iterator = getRootAtoms(input).iterator();
		while (iterator.hasNext())
		{
			Atom atom = (Atom)iterator.next();
			if (String.instancehelper_equals("ftyp", atom.getHeader().getFourcc()))
			{
				ftyp = (FileTypeBox)atom.parseBox(input);
			}
			else if (String.instancehelper_equals("moov", atom.getHeader().getFourcc()))
			{
				Movie result = new Movie(ftyp, (MovieBox)atom.parseBox(input));
				
				return result;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 124, 162, 127, 2, 120, 142, 99 })]
	public static MovieBox parseMovieChannel(org.jcodec.common.io.SeekableByteChannel input)
	{
		Iterator iterator = getRootAtoms(input).iterator();
		while (iterator.hasNext())
		{
			Atom atom = (Atom)iterator.next();
			if (String.instancehelper_equals("moov", atom.getHeader().getFourcc()))
			{
				return (MovieBox)atom.parseBox(input);
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/SeekableByteChannel;)Ljava/util/List<Lorg/jcodec/containers/mp4/MP4Util$Atom;>;")]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 106, 103, 132, 106, 105, 111, 100,
		99, 111, 172
	})]
	public static List getRootAtoms(org.jcodec.common.io.SeekableByteChannel input)
	{
		input.setPosition(0L);
		ArrayList result = new ArrayList();
		Header atom;
		for (long off = 0L; off < input.size(); off += atom.getSize())
		{
			input.setPosition(off);
			atom = Header.read(NIOUtils.fetchFromChannel(input, 16));
			if (atom == null)
			{
				break;
			}
			((List)result).add((object)new Atom(atom, off));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 105, 98, 104, 124, 116, 99, 99 })]
	public static Atom findFirstAtom(string fourcc, org.jcodec.common.io.SeekableByteChannel input)
	{
		List rootAtoms = getRootAtoms(input);
		Iterator iterator = rootAtoms.iterator();
		while (iterator.hasNext())
		{
			Atom atom = (Atom)iterator.next();
			if (String.instancehelper_equals(fourcc, atom.getHeader().getFourcc()))
			{
				return atom;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 126, 66, 136, 104, 104, 101, 8, 199 })]
	public static MovieBox createRefMovie(org.jcodec.common.io.SeekableByteChannel input, string url)
	{
		MovieBox movie = parseMovieChannel(input);
		TrakBox[] tracks = movie.getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			trakBox.setDataRef(url);
		}
		return movie;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 86, 98, 107 })]
	public static void writeMovie(org.jcodec.common.io.SeekableByteChannel @out, MovieBox movie)
	{
		doWriteMovieToChannel(@out, movie, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 85, 98, 106, 159, 11, 106, 104, 104, 105 })]
	public static void doWriteMovieToChannel(org.jcodec.common.io.SeekableByteChannel @out, MovieBox movie, int additionalSize)
	{
		int sizeHint = estimateMoovBoxSize(movie) + additionalSize;
		Logger.debug(new StringBuilder().append("Using ").append(sizeHint).append(" bytes for MOOV box")
			.toString());
		ByteBuffer buf = ByteBuffer.allocate(sizeHint * 4);
		movie.write(buf);
		buf.flip();
		@out.write(buf);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(293)]
	public static int estimateMoovBoxSize(MovieBox movie)
	{
		return movie.estimateSize() + 4096;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 121, 66, 136, 109, 104, 101, 8, 199 })]
	public static Movie createRefFullMovie(org.jcodec.common.io.SeekableByteChannel input, string url)
	{
		Movie movie = parseFullMovieChannel(input);
		TrakBox[] tracks = Movie.access_0024000(movie).getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			trakBox.setDataRef(url);
		}
		return movie;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 75, 162, 107 })]
	public static void writeFullMovie(org.jcodec.common.io.SeekableByteChannel @out, Movie movie)
	{
		doWriteFullMovieToChannel(@out, movie, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 74, 162, 111, 159, 11, 110, 109, 109, 104,
		105
	})]
	public static void doWriteFullMovieToChannel(org.jcodec.common.io.SeekableByteChannel @out, Movie movie, int additionalSize)
	{
		int sizeHint = estimateMoovBoxSize(movie.getMoov()) + additionalSize;
		Logger.debug(new StringBuilder().append("Using ").append(sizeHint).append(" bytes for MOOV box")
			.toString());
		ByteBuffer buf = ByteBuffer.allocate(sizeHint + 128);
		movie.getFtyp().write(buf);
		movie.getMoov().write(buf);
		buf.flip();
		@out.write(buf);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public MP4Util()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/SeekableByteChannel;)Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/MovieFragmentBox;>;")]
	[LineNumberTable(new byte[]
	{
		159, 116, 162, 99, 103, 127, 5, 120, 112, 120,
		148, 102, 127, 1, 105, 99
	})]
	public static List parseMovieFragments(org.jcodec.common.io.SeekableByteChannel input)
	{
		MovieBox moov = null;
		LinkedList fragments = new LinkedList();
		Iterator iterator = getRootAtoms(input).iterator();
		while (iterator.hasNext())
		{
			Atom atom = (Atom)iterator.next();
			if (String.instancehelper_equals("moov", atom.getHeader().getFourcc()))
			{
				moov = (MovieBox)atom.parseBox(input);
			}
			else if (String.instancehelper_equalsIgnoreCase("moof", atom.getHeader().getFourcc()))
			{
				fragments.add((MovieFragmentBox)atom.parseBox(input));
			}
		}
		Iterator iterator2 = fragments.iterator();
		while (iterator2.hasNext())
		{
			MovieFragmentBox fragment = (MovieFragmentBox)iterator2.next();
			fragment.setMovie(moov);
		}
		return fragments;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 107, 66, 136, 141, 74, 3 })]
	public static Atom findFirstAtomInFile(string fourcc, File input)
	{
		AutoFileChannelWrapper c = new AutoFileChannelWrapper(input);
		try
		{
			return findFirstAtom(fourcc, c);
		}
		finally
		{
			IOUtils.closeQuietly(c);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 103, 130, 104, 143 })]
	public static Atom atom(org.jcodec.common.io.SeekableByteChannel input)
	{
		long off = input.position();
		Header atom = Header.read(NIOUtils.fetchFromChannel(input, 16));
		Atom result = ((atom != null) ? new Atom(atom, off) : null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 94, 98, 131, 104, 140, 100, 42, 3 })]
	public static MovieBox parseMovie(File source)
	{
		FileChannelWrapper input = null;
		try
		{
			input = NIOUtils.readableChannel(source);
			return parseMovieChannel(input);
		}
		finally
		{
			((Channel)input)?.close();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 91, 66, 131, 104, 159, 12, 100, 42, 3 })]
	public static MovieBox createRefMovieFromFile(File source)
	{
		FileChannelWrapper input = null;
		try
		{
			input = NIOUtils.readableChannel(source);
			return createRefMovie(input, new StringBuilder().append("file://").append(source.getCanonicalPath()).toString());
		}
		finally
		{
			((Channel)input)?.close();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 89, 162, 131, 104, 140, 74, 99, 99 })]
	public static void writeMovieToFile(File f, MovieBox movie)
	{
		FileChannelWrapper @out = null;
		try
		{
			@out = NIOUtils.writableChannel(f);
			writeMovie(@out, movie);
		}
		finally
		{
			IOUtils.closeQuietly(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 83, 162, 131, 104, 140, 100, 42, 3 })]
	public static Movie parseFullMovie(File source)
	{
		FileChannelWrapper input = null;
		try
		{
			input = NIOUtils.readableChannel(source);
			return parseFullMovieChannel(input);
		}
		finally
		{
			((Channel)input)?.close();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 80, 130, 131, 104, 159, 12, 100, 42, 3 })]
	public static Movie createRefFullMovieFromFile(File source)
	{
		FileChannelWrapper input = null;
		try
		{
			input = NIOUtils.readableChannel(source);
			return createRefFullMovie(input, new StringBuilder().append("file://").append(source.getCanonicalPath()).toString());
		}
		finally
		{
			((Channel)input)?.close();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 77, 98, 131, 104, 140, 74, 99, 99 })]
	public static void writeFullMovieToFile(File f, Movie movie)
	{
		FileChannelWrapper @out = null;
		try
		{
			@out = NIOUtils.writableChannel(f);
			writeFullMovie(@out, movie);
		}
		finally
		{
			IOUtils.closeQuietly(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(297)]
	public static string getFourcc(Codec codec)
	{
		return (string)codecMapping.get(codec);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 67, 98, 104, 104, 136 })]
	public static ByteBuffer writeBox(Box box, int approxSize)
	{
		ByteBuffer buf = ByteBuffer.allocate(approxSize);
		box.write(buf);
		buf.flip();
		return buf;
	}

	[LineNumberTable(new byte[] { 159, 133, 98, 171, 118, 118, 118 })]
	static MP4Util()
	{
		codecMapping = new HashMap();
		codecMapping.put(Codec.___003C_003EMPEG2, "m2v1");
		codecMapping.put(Codec.___003C_003EH264, "avc1");
		codecMapping.put(Codec.___003C_003EJ2K, "mjp2");
	}
}
