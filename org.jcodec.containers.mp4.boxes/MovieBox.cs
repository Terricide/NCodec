using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.util;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4.boxes;

public class MovieBox : NodeBox
{
	[LineNumberTable(26)]
	public static string fourcc()
	{
		return "moov";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public MovieBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(34)]
	public virtual TrakBox[] getTracks()
	{
		return (TrakBox[])NodeBox.findAll(this, ClassLiteral<TrakBox>.Value, "trak");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(96)]
	private MovieHeaderBox getMovieHeader()
	{
		return (MovieHeaderBox)NodeBox.findFirst(this, ClassLiteral<MovieHeaderBox>.Value, "mvhd");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(58)]
	public virtual int getTimescale()
	{
		int timescale = getMovieHeader().getTimescale();
		
		return timescale;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 66, 126 })]
	private void setTimescale(int newTs)
	{
		((MovieHeaderBox)NodeBox.findFirst(this, ClassLiteral<MovieHeaderBox>.Value, "mvhd")).setTimescale(newTs);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(62)]
	public virtual long rescale(long tv, long ts)
	{
		long num = tv * getTimescale();
		return (ts != -1) ? (num / ts) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(111)]
	public virtual long getDuration()
	{
		long duration = getMovieHeader().getDuration();
		
		return duration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 66, 111 })]
	public virtual void setDuration(long movDuration)
	{
		getMovieHeader().setDuration(movDuration);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 104, 104, 101, 105, 227, 61, 231,
		69
	})]
	public virtual TrakBox getVideoTrack()
	{
		TrakBox[] tracks = getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			if (trakBox.isVideo())
			{
				return trakBox;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 66, 109 })]
	private Size applyMatrix(TrakBox videoTrack, Size size)
	{
		int[] matrix = videoTrack.getTrackHeader().getMatrix();
		Size result = new Size(ByteCodeHelper.d2i((double)size.getWidth() * (double)matrix[0] / 65536.0), ByteCodeHelper.d2i((double)size.getHeight() * (double)matrix[4] / 65536.0));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(30)]
	public static MovieBox createMovieBox()
	{
		
		MovieBox result = new MovieBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 66, 104, 104, 101, 105, 227, 61, 231,
		69
	})]
	public virtual TrakBox getTimecodeTrack()
	{
		TrakBox[] tracks = getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			if (trakBox.isTimecode())
			{
				return trakBox;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 104, 136, 104, 107, 101, 149, 105,
		101, 99, 106, 106, 111, 127, 11, 227, 53, 234,
		78, 119
	})]
	public virtual void fixTimescale(int newTs)
	{
		int oldTs = getTimescale();
		setTimescale(newTs);
		TrakBox[] tracks = getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			trakBox.setDuration(rescale(trakBox.getDuration(), oldTs));
			List edits = trakBox.getEdits();
			if (edits != null)
			{
				ListIterator lit = edits.listIterator();
				while (lit.hasNext())
				{
					Edit edit = (Edit)lit.next();
					lit.set(new Edit(rescale(edit.getDuration(), oldTs), edit.getMediaTime(), edit.getRate()));
				}
			}
		}
		setDuration(rescale(getDuration(), oldTs));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/TrakBox;>;")]
	[LineNumberTable(new byte[]
	{
		159, 117, 66, 103, 104, 104, 101, 105, 233, 61,
		231, 69
	})]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		TrakBox[] tracks = getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			if (trakBox.isAudio())
			{
				result.add(trakBox);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 162, 152, 136, 103, 100, 125, 127, 0,
		43, 135, 131, 136
	})]
	public virtual TrakBox importTrack(MovieBox movie, TrakBox track)
	{
		TrakBox newTrack = (TrakBox)NodeBox.cloneBox(track, 1048576, factory);
		List edits = newTrack.getEdits();
		ArrayList result = new ArrayList();
		if (edits != null)
		{
			Iterator iterator = edits.iterator();
			while (iterator.hasNext())
			{
				Edit edit = (Edit)iterator.next();
				result.add(new Edit(rescale(edit.getDuration(), movie.getTimescale()), edit.getMediaTime(), edit.getRate()));
			}
		}
		newTrack.setEdits(result);
		return newTrack;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 66, 119, 121, 110 })]
	public virtual void appendTrack(TrakBox newTrack)
	{
		newTrack.getTrackHeader().setNo(getMovieHeader().getNextTrackId());
		getMovieHeader().setNextTrackId(getMovieHeader().getNextTrackId() + 1);
		boxes.add(newTrack);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 130, 99, 104, 104, 101, 10, 199 })]
	public virtual bool isPureRefMovie()
	{
		int pureRef = 1;
		TrakBox[] tracks = getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			pureRef &= (trakBox.isPureRef() ? 1 : 0);
		}
		return (byte)pureRef != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 104, 104, 104, 101, 106, 232, 61,
		231, 69, 111
	})]
	public virtual void updateDuration()
	{
		TrakBox[] tracks = getTracks();
		long min = 2147483647L;
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			if (trakBox.getDuration() < min)
			{
				min = trakBox.getDuration();
			}
		}
		getMovieHeader().setDuration(min);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 162, 104, 100, 99, 156, 100, 159, 7,
		127, 2, 108, 108, 131, 104, 137, 100, 63, 10
	})]
	public virtual Size getDisplaySize()
	{
		TrakBox videoTrack = getVideoTrack();
		if (videoTrack == null)
		{
			return null;
		}
		ClearApertureBox clef = (ClearApertureBox)NodeBox.findFirstPath(videoTrack, ClassLiteral<ClearApertureBox>.Value, Box.path("tapt.clef"));
		if (clef != null)
		{
			Size result = applyMatrix(videoTrack, new Size(ByteCodeHelper.f2i(clef.getWidth()), ByteCodeHelper.f2i(clef.getHeight())));
			
			return result;
		}
		Box box = (Box)((SampleDescriptionBox)NodeBox.findFirstPath(videoTrack, ClassLiteral<SampleDescriptionBox>.Value, Box.path("mdia.minf.stbl.stsd"))).getBoxes().get(0);
		if (box == null || !(box is VideoSampleEntry))
		{
			return null;
		}
		VideoSampleEntry vs = (VideoSampleEntry)box;
		Rational par = videoTrack.getPAR();
		int num = vs.getWidth() * par.getNum();
		int den = par.getDen();
		Size result2 = applyMatrix(videoTrack, new Size((den != -1) ? (num / den) : (-num), vs.getHeight()));
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 130, 104, 100, 99, 156, 100, 159, 0,
		127, 2, 108, 108, 131, 136
	})]
	public virtual Size getStoredSize()
	{
		TrakBox videoTrack = getVideoTrack();
		if (videoTrack == null)
		{
			return null;
		}
		EncodedPixelBox enof = (EncodedPixelBox)NodeBox.findFirstPath(videoTrack, ClassLiteral<EncodedPixelBox>.Value, Box.path("tapt.enof"));
		if (enof != null)
		{
			Size result = new Size(ByteCodeHelper.f2i(enof.getWidth()), ByteCodeHelper.f2i(enof.getHeight()));
			
			return result;
		}
		Box box = (Box)((SampleDescriptionBox)NodeBox.findFirstPath(videoTrack, ClassLiteral<SampleDescriptionBox>.Value, Box.path("mdia.minf.stbl.stsd"))).getBoxes().get(0);
		if (box == null || !(box is VideoSampleEntry))
		{
			return null;
		}
		VideoSampleEntry vs = (VideoSampleEntry)box;
		Size result2 = new Size(vs.getWidth(), vs.getHeight());
		
		return result2;
	}
}
