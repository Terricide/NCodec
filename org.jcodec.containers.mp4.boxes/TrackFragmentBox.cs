using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;

namespace org.jcodec.containers.mp4.boxes;

public class TrackFragmentBox : NodeBox
{
	[LineNumberTable(23)]
	public static string fourcc()
	{
		return "traf";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 106 })]
	public TrackFragmentBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 103, 113, 100, 113 })]
	public virtual int getTrackId()
	{
		TrackFragmentHeaderBox tfhd = (TrackFragmentHeaderBox)NodeBox.findFirst(this, ClassLiteral<TrackFragmentHeaderBox>.Value, TrackFragmentHeaderBox.fourcc());
		if (tfhd == null)
		{
			
			throw new RuntimeException("Corrupt track fragment, no header atom found");
		}
		int trackId = tfhd.getTrackId();
		
		return trackId;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public static TrackFragmentBox createTrackFragmentBox()
	{
		
		TrackFragmentBox result = new TrackFragmentBox(new Header(fourcc()));
		
		return result;
	}
}
