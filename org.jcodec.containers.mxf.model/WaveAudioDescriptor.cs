using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;

namespace org.jcodec.containers.mxf.model;

public class WaveAudioDescriptor : GenericSoundEssenceDescriptor
{
	private short blockAlign;

	private byte sequenceOffset;

	private int avgBps;

	private UL channelAssignment;

	private int peakEnvelopeVersion;

	private int peakEnvelopeFormat;

	private int pointsPerPeakValue;

	private int peakEnvelopeBlockSize;

	private int peakChannels;

	private int peakFrames;

	private ByteBuffer peakOfPeaksPosition;

	private ByteBuffer peakEnvelopeTimestamp;

	private ByteBuffer peakEnvelopeData;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 106 })]
	public WaveAudioDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 136, 120, 141, 141, 159, 160, 106,
		109, 134, 110, 134, 109, 134, 109, 134, 109, 134,
		109, 134, 109, 134, 109, 134, 109, 134, 109, 134,
		104, 131, 104, 131, 104, 163, 127, 41, 134, 103,
		102
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
			case 15626:
				blockAlign = _bb.getShort();
				break;
			case 15627:
				sequenceOffset = (byte)(sbyte)_bb.get();
				break;
			case 15625:
				avgBps = _bb.getInt();
				break;
			case 15666:
				channelAssignment = UL.read(_bb);
				break;
			case 15657:
				peakEnvelopeVersion = _bb.getInt();
				break;
			case 15658:
				peakEnvelopeFormat = _bb.getInt();
				break;
			case 15659:
				pointsPerPeakValue = _bb.getInt();
				break;
			case 15660:
				peakEnvelopeBlockSize = _bb.getInt();
				break;
			case 15661:
				peakChannels = _bb.getInt();
				break;
			case 15662:
				peakFrames = _bb.getInt();
				break;
			case 15663:
				peakOfPeaksPosition = _bb;
				break;
			case 15664:
				peakEnvelopeTimestamp = _bb;
				break;
			case 15665:
				peakEnvelopeData = _bb;
				break;
			default:
				java.lang.System.@out.println(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(95)]
	public virtual short getBlockAlign()
	{
		return blockAlign;
	}

	[LineNumberTable(99)]
	public virtual byte getSequenceOffset()
	{
		return (byte)(sbyte)sequenceOffset;
	}

	[LineNumberTable(103)]
	public virtual int getAvgBps()
	{
		return avgBps;
	}

	[LineNumberTable(107)]
	public virtual UL getChannelAssignment()
	{
		return channelAssignment;
	}

	[LineNumberTable(111)]
	public virtual int getPeakEnvelopeVersion()
	{
		return peakEnvelopeVersion;
	}

	[LineNumberTable(115)]
	public virtual int getPeakEnvelopeFormat()
	{
		return peakEnvelopeFormat;
	}

	[LineNumberTable(119)]
	public virtual int getPointsPerPeakValue()
	{
		return pointsPerPeakValue;
	}

	[LineNumberTable(123)]
	public virtual int getPeakEnvelopeBlockSize()
	{
		return peakEnvelopeBlockSize;
	}

	[LineNumberTable(127)]
	public virtual int getPeakChannels()
	{
		return peakChannels;
	}

	[LineNumberTable(131)]
	public virtual int getPeakFrames()
	{
		return peakFrames;
	}

	[LineNumberTable(135)]
	public virtual ByteBuffer getPeakOfPeaksPosition()
	{
		return peakOfPeaksPosition;
	}

	[LineNumberTable(139)]
	public virtual ByteBuffer getPeakEnvelopeTimestamp()
	{
		return peakEnvelopeTimestamp;
	}

	[LineNumberTable(143)]
	public virtual ByteBuffer getPeakEnvelopeData()
	{
		return peakEnvelopeData;
	}
}
