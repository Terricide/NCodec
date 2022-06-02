using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class MPEG2VideoDescriptor : CDCIEssenceDescriptor
{
	private byte singleSequence;

	private byte constantBFrames;

	private byte codedContentType;

	private byte lowDelay;

	private byte closedGOP;

	private byte identicalGOP;

	private short maxGOP;

	private short bPictureCount;

	private int bitRate;

	private byte profileAndLevel;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 106 })]
	public MPEG2VideoDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 136, 120, 141, 141, 191, 42, 110,
		134, 110, 134, 110, 134, 110, 134, 110, 134, 110,
		134, 109, 134, 117, 134, 109, 131, 110, 131, 127,
		57, 134, 103, 102
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
			case 32768:
				singleSequence = (byte)(sbyte)_bb.get();
				break;
			case 32769:
				constantBFrames = (byte)(sbyte)_bb.get();
				break;
			case 32770:
				codedContentType = (byte)(sbyte)_bb.get();
				break;
			case 32771:
				lowDelay = (byte)(sbyte)_bb.get();
				break;
			case 32772:
				closedGOP = (byte)(sbyte)_bb.get();
				break;
			case 32773:
				identicalGOP = (byte)(sbyte)_bb.get();
				break;
			case 32774:
				maxGOP = _bb.getShort();
				break;
			case 32775:
				bPictureCount = (short)((sbyte)_bb.get() & 0xFF);
				break;
			case 32776:
				bitRate = _bb.getInt();
				break;
			case 32777:
				profileAndLevel = (byte)(sbyte)_bb.get();
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x + (")
					.append(_bb.remaining())
					.append(")")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(83)]
	public virtual byte getSingleSequence()
	{
		return (byte)(sbyte)singleSequence;
	}

	[LineNumberTable(87)]
	public virtual byte getConstantBFrames()
	{
		return (byte)(sbyte)constantBFrames;
	}

	[LineNumberTable(91)]
	public virtual byte getCodedContentType()
	{
		return (byte)(sbyte)codedContentType;
	}

	[LineNumberTable(95)]
	public virtual byte getLowDelay()
	{
		return (byte)(sbyte)lowDelay;
	}

	[LineNumberTable(99)]
	public virtual byte getClosedGOP()
	{
		return (byte)(sbyte)closedGOP;
	}

	[LineNumberTable(103)]
	public virtual byte getIdenticalGOP()
	{
		return (byte)(sbyte)identicalGOP;
	}

	[LineNumberTable(107)]
	public virtual short getMaxGOP()
	{
		return maxGOP;
	}

	[LineNumberTable(111)]
	public virtual short getbPictureCount()
	{
		return bPictureCount;
	}

	[LineNumberTable(115)]
	public virtual int getBitRate()
	{
		return bitRate;
	}

	[LineNumberTable(119)]
	public virtual byte getProfileAndLevel()
	{
		return (byte)(sbyte)profileAndLevel;
	}
}
