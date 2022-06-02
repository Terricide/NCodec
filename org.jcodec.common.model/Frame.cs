using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common.model;

public class Frame : Object
{
	private Picture pic;

	private RationalLarge pts;

	private RationalLarge duration;

	private Rational pixelAspect;

	private TapeTimecode tapeTimecode;

	private int frameNo;

	[Signature("Ljava/util/List<Ljava/lang/String;>;")]
	private List messages;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 105, 104, 104, 104, 105, 105, 105,
		105
	})]
	public Frame(Picture pic, RationalLarge pts, RationalLarge duration, Rational pixelAspect, int frameNo, TapeTimecode tapeTimecode, List messages)
	{
		this.pic = pic;
		this.pts = pts;
		this.duration = duration;
		this.pixelAspect = pixelAspect;
		this.tapeTimecode = tapeTimecode;
		this.frameNo = frameNo;
		this.messages = messages;
	}

	[LineNumberTable(32)]
	public virtual Picture getPic()
	{
		return pic;
	}

	[LineNumberTable(36)]
	public virtual RationalLarge getPts()
	{
		return pts;
	}

	[LineNumberTable(40)]
	public virtual RationalLarge getDuration()
	{
		return duration;
	}

	[LineNumberTable(44)]
	public virtual Rational getPixelAspect()
	{
		return pixelAspect;
	}

	[LineNumberTable(48)]
	public virtual TapeTimecode getTapeTimecode()
	{
		return tapeTimecode;
	}

	[LineNumberTable(52)]
	public virtual int getFrameNo()
	{
		return frameNo;
	}

	[Signature("()Ljava/util/List<Ljava/lang/String;>;")]
	[LineNumberTable(56)]
	public virtual List getMessages()
	{
		return messages;
	}

	[LineNumberTable(60)]
	public virtual bool isAvailable()
	{
		return true;
	}
}
