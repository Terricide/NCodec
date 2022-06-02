using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.containers.mp4.boxes;

public class MovieFragmentBox : NodeBox
{
	private MovieBox moov;

	[LineNumberTable(23)]
	public static string fourcc()
	{
		return "moof";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public MovieFragmentBox(Header atom)
		: base(atom)
	{
	}

	[LineNumberTable(27)]
	public virtual MovieBox getMovie()
	{
		return moov;
	}

	[LineNumberTable(new byte[] { 159, 135, 162, 104 })]
	public virtual void setMovie(MovieBox moov)
	{
		this.moov = moov;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public virtual TrackFragmentBox[] getTracks()
	{
		return (TrackFragmentBox[])NodeBox.findAll(this, ClassLiteral<TrackFragmentBox>.Value, TrackFragmentBox.fourcc());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 103, 113, 100, 113 })]
	public virtual int getSequenceNumber()
	{
		MovieFragmentHeaderBox mfhd = (MovieFragmentHeaderBox)NodeBox.findFirst(this, ClassLiteral<MovieFragmentHeaderBox>.Value, MovieFragmentHeaderBox.fourcc());
		if (mfhd == null)
		{
			
			throw new RuntimeException("Corrupt movie fragment, no header atom found");
		}
		int sequenceNumber = mfhd.getSequenceNumber();
		
		return sequenceNumber;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(47)]
	public static MovieFragmentBox createMovieFragmentBox()
	{
		
		MovieFragmentBox result = new MovieFragmentBox(new Header(fourcc()));
		
		return result;
	}
}
