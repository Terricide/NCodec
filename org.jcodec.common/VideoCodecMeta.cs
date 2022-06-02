using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.common;

public class VideoCodecMeta : CodecMeta
{
	private Size size;

	private Rational pasp;

	private bool interlaced;

	private bool topFieldFirst;

	private ColorSpace color;

	[LineNumberTable(57)]
	public virtual Rational getPixelAspectRatio()
	{
		return pasp;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 98, 105, 104, 104 })]
	public static VideoCodecMeta createSimpleVideoCodecMeta(Size size, ColorSpace color)
	{
		VideoCodecMeta self = new VideoCodecMeta(null, null);
		self.size = size;
		self.color = color;
		return self;
	}

	[LineNumberTable(49)]
	public virtual Size getSize()
	{
		return size;
	}

	[LineNumberTable(69)]
	public virtual ColorSpace getColor()
	{
		return color;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 107 })]
	public VideoCodecMeta(string fourcc, ByteBuffer codecPrivate)
		: base(fourcc, codecPrivate)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 104 })]
	public static VideoCodecMeta createVideoCodecMeta(string fourcc, ByteBuffer codecPrivate, Size size, Rational pasp)
	{
		VideoCodecMeta self = new VideoCodecMeta(fourcc, codecPrivate);
		self.size = size;
		self.pasp = pasp;
		return self;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 129, 71, 105, 104, 104, 104, 104 })]
	public static VideoCodecMeta createVideoCodecMeta2(string fourcc, ByteBuffer codecPrivate, Size size, Rational pasp, bool interlaced, bool topFieldFirst)
	{
		VideoCodecMeta self = new VideoCodecMeta(fourcc, codecPrivate);
		self.size = size;
		self.pasp = pasp;
		self.interlaced = interlaced;
		self.topFieldFirst = topFieldFirst;
		return self;
	}

	[LineNumberTable(53)]
	public virtual Rational getPasp()
	{
		return pasp;
	}

	[LineNumberTable(61)]
	public virtual bool isInterlaced()
	{
		return interlaced;
	}

	[LineNumberTable(65)]
	public virtual bool isTopFieldFirst()
	{
		return topFieldFirst;
	}

	[LineNumberTable(new byte[] { 159, 122, 66, 104 })]
	public virtual void setPixelAspectRatio(Rational pasp)
	{
		this.pasp = pasp;
	}
}
