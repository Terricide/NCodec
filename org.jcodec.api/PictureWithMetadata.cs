using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.api;

public class PictureWithMetadata : Object
{
	private Picture picture;

	private double timestamp;

	private double duration;

	private DemuxerTrackMeta.Orientation orientation;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 105, 104, 106, 106, 105 })]
	public PictureWithMetadata(Picture picture, double timestamp, double duration, DemuxerTrackMeta.Orientation orientation)
	{
		this.picture = picture;
		this.timestamp = timestamp;
		this.duration = duration;
		this.orientation = orientation;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(20)]
	public static PictureWithMetadata createPictureWithMetadata(Picture picture, double timestamp, double duration)
	{
		PictureWithMetadata result = new PictureWithMetadata(picture, timestamp, duration, DemuxerTrackMeta.Orientation.___003C_003ED_0);
		
		return result;
	}

	[LineNumberTable(31)]
	public virtual Picture getPicture()
	{
		return picture;
	}

	[LineNumberTable(35)]
	public virtual double getTimestamp()
	{
		return timestamp;
	}

	[LineNumberTable(39)]
	public virtual double getDuration()
	{
		return duration;
	}

	[LineNumberTable(43)]
	public virtual DemuxerTrackMeta.Orientation getOrientation()
	{
		return orientation;
	}
}
