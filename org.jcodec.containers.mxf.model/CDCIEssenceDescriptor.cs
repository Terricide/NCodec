using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class CDCIEssenceDescriptor : GenericPictureEssenceDescriptor
{
	private int componentDepth;

	private int horizontalSubsampling;

	private int verticalSubsampling;

	private byte colorSiting;

	private byte reversedByteOrder;

	private short paddingBits;

	private int alphaSampleDepth;

	private int blackRefLevel;

	private int whiteReflevel;

	private int colorRange;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 106 })]
	public CDCIEssenceDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 136, 120, 141, 141, 159, 46, 109,
		134, 109, 134, 109, 134, 110, 134, 110, 134, 109,
		134, 109, 134, 109, 131, 109, 131, 109, 131, 127,
		36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		base.read(tags);
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 13057:
				componentDepth = _bb.getInt();
				break;
			case 13058:
				horizontalSubsampling = _bb.getInt();
				break;
			case 13064:
				verticalSubsampling = _bb.getInt();
				break;
			case 13059:
				colorSiting = (byte)(sbyte)_bb.get();
				break;
			case 13067:
				reversedByteOrder = (byte)(sbyte)_bb.get();
				break;
			case 13063:
				paddingBits = _bb.getShort();
				break;
			case 13065:
				alphaSampleDepth = _bb.getInt();
				break;
			case 13060:
				blackRefLevel = _bb.getInt();
				break;
			case 13061:
				whiteReflevel = _bb.getInt();
				break;
			case 13062:
				colorRange = _bb.getInt();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(82)]
	public virtual int getComponentDepth()
	{
		return componentDepth;
	}

	[LineNumberTable(86)]
	public virtual int getHorizontalSubsampling()
	{
		return horizontalSubsampling;
	}

	[LineNumberTable(90)]
	public virtual int getVerticalSubsampling()
	{
		return verticalSubsampling;
	}

	[LineNumberTable(94)]
	public virtual byte getColorSiting()
	{
		return (byte)(sbyte)colorSiting;
	}

	[LineNumberTable(98)]
	public virtual byte getReversedByteOrder()
	{
		return (byte)(sbyte)reversedByteOrder;
	}

	[LineNumberTable(102)]
	public virtual short getPaddingBits()
	{
		return paddingBits;
	}

	[LineNumberTable(106)]
	public virtual int getAlphaSampleDepth()
	{
		return alphaSampleDepth;
	}

	[LineNumberTable(110)]
	public virtual int getBlackRefLevel()
	{
		return blackRefLevel;
	}

	[LineNumberTable(114)]
	public virtual int getWhiteReflevel()
	{
		return whiteReflevel;
	}

	[LineNumberTable(118)]
	public virtual int getColorRange()
	{
		return colorRange;
	}
}
