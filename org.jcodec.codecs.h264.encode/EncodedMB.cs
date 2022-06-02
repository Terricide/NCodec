using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.encode;

public class EncodedMB : Object
{
	private Picture pixels;

	private MBType type;

	private int qp;

	private int[] nc;

	private int[] mx;

	private int[] my;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 117, 110, 110, 110 })]
	public EncodedMB()
	{
		pixels = Picture.create(16, 16, ColorSpace.___003C_003EYUV420J);
		nc = new int[16];
		mx = new int[16];
		my = new int[16];
	}

	[LineNumberTable(29)]
	public virtual Picture getPixels()
	{
		return pixels;
	}

	[LineNumberTable(33)]
	public virtual MBType getType()
	{
		return type;
	}

	[LineNumberTable(new byte[] { 159, 133, 98, 104 })]
	public virtual void setType(MBType type)
	{
		this.type = type;
	}

	[LineNumberTable(41)]
	public virtual int getQp()
	{
		return qp;
	}

	[LineNumberTable(new byte[] { 159, 131, 98, 104 })]
	public virtual void setQp(int qp)
	{
		this.qp = qp;
	}

	[LineNumberTable(49)]
	public virtual int[] getNc()
	{
		return nc;
	}

	[LineNumberTable(53)]
	public virtual int[] getMx()
	{
		return mx;
	}

	[LineNumberTable(57)]
	public virtual int[] getMy()
	{
		return my;
	}
}
