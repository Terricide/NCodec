using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class J2KPictureDescriptor : MXFInterchangeObject
{
	private short rsiz;

	private int xsiz;

	private int ysiz;

	private int xOsiz;

	private int yOsiz;

	private int xTsiz;

	private int yTsiz;

	private int xTOsiz;

	private int yTOsiz;

	private short csiz;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 106 })]
	public J2KPictureDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 133, 66, 120, 141, 109, 159, 42, 109, 134,
		109, 134, 109, 134, 109, 134, 109, 134, 109, 134,
		109, 134, 109, 131, 109, 131, 109, 163, 127, 36,
		134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 24836:
				rsiz = _bb.getShort();
				break;
			case 24837:
				xsiz = _bb.getInt();
				break;
			case 24838:
				ysiz = _bb.getInt();
				break;
			case 24839:
				xOsiz = _bb.getInt();
				break;
			case 24840:
				yOsiz = _bb.getInt();
				break;
			case 24841:
				xTsiz = _bb.getInt();
				break;
			case 24842:
				yTsiz = _bb.getInt();
				break;
			case 24843:
				xTOsiz = _bb.getInt();
				break;
			case 24844:
				yTOsiz = _bb.getInt();
				break;
			case 24845:
				csiz = _bb.getShort();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(81)]
	public virtual short getRsiz()
	{
		return rsiz;
	}

	[LineNumberTable(85)]
	public virtual int getXsiz()
	{
		return xsiz;
	}

	[LineNumberTable(89)]
	public virtual int getYsiz()
	{
		return ysiz;
	}

	[LineNumberTable(93)]
	public virtual int getxOsiz()
	{
		return xOsiz;
	}

	[LineNumberTable(97)]
	public virtual int getyOsiz()
	{
		return yOsiz;
	}

	[LineNumberTable(101)]
	public virtual int getxTsiz()
	{
		return xTsiz;
	}

	[LineNumberTable(105)]
	public virtual int getyTsiz()
	{
		return yTsiz;
	}

	[LineNumberTable(109)]
	public virtual int getxTOsiz()
	{
		return xTOsiz;
	}

	[LineNumberTable(113)]
	public virtual int getyTOsiz()
	{
		return yTOsiz;
	}

	[LineNumberTable(117)]
	public virtual short getCsiz()
	{
		return csiz;
	}
}
