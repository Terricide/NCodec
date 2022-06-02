using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.containers.mxf.model;

public class GenericSoundEssenceDescriptor : FileDescriptor
{
	private Rational audioSamplingRate;

	private byte locked;

	private byte audioRefLevel;

	private byte electroSpatialFormulation;

	private int channelCount;

	private int quantizationBits;

	private byte dialNorm;

	private UL soundEssenceCompression;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 106 })]
	public GenericSoundEssenceDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 136, 120, 141, 141, 159, 50, 125,
		134, 110, 134, 110, 134, 110, 134, 109, 134, 109,
		131, 110, 131, 109, 163, 127, 36, 134, 103, 102
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
			case 15619:
				Rational.___003Cclinit_003E();
				audioSamplingRate = new Rational(_bb.getInt(), _bb.getInt());
				break;
			case 15618:
				locked = (byte)(sbyte)_bb.get();
				break;
			case 15620:
				audioRefLevel = (byte)(sbyte)_bb.get();
				break;
			case 15621:
				electroSpatialFormulation = (byte)(sbyte)_bb.get();
				break;
			case 15623:
				channelCount = _bb.getInt();
				break;
			case 15617:
				quantizationBits = _bb.getInt();
				break;
			case 15628:
				dialNorm = (byte)(sbyte)_bb.get();
				break;
			case 15622:
				soundEssenceCompression = UL.read(_bb);
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(76)]
	public virtual Rational getAudioSamplingRate()
	{
		return audioSamplingRate;
	}

	[LineNumberTable(80)]
	public virtual byte getLocked()
	{
		return (byte)(sbyte)locked;
	}

	[LineNumberTable(84)]
	public virtual byte getAudioRefLevel()
	{
		return (byte)(sbyte)audioRefLevel;
	}

	[LineNumberTable(88)]
	public virtual byte getElectroSpatialFormulation()
	{
		return (byte)(sbyte)electroSpatialFormulation;
	}

	[LineNumberTable(92)]
	public virtual int getChannelCount()
	{
		return channelCount;
	}

	[LineNumberTable(96)]
	public virtual int getQuantizationBits()
	{
		return quantizationBits;
	}

	[LineNumberTable(100)]
	public virtual byte getDialNorm()
	{
		return (byte)(sbyte)dialNorm;
	}

	[LineNumberTable(104)]
	public virtual UL getSoundEssenceCompression()
	{
		return soundEssenceCompression;
	}
}
