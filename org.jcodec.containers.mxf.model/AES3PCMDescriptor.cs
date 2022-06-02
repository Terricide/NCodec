using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class AES3PCMDescriptor : WaveAudioDescriptor
{
	private byte emphasis;

	private short blockStartOffset;

	private byte auxBitsMode;

	private ByteBuffer channelStatusMode;

	private ByteBuffer fixedChannelStatusData;

	private ByteBuffer userDataMode;

	private ByteBuffer fixedUserData;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 106 })]
	public AES3PCMDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 136, 120, 141, 141, 159, 50, 110,
		134, 109, 134, 110, 134, 104, 134, 104, 131, 104,
		131, 104, 163, 127, 36, 134, 103, 102
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
			case 15629:
				emphasis = (byte)(sbyte)_bb.get();
				break;
			case 15631:
				blockStartOffset = _bb.getShort();
				break;
			case 15624:
				auxBitsMode = (byte)(sbyte)_bb.get();
				break;
			case 15632:
				channelStatusMode = _bb;
				break;
			case 15633:
				fixedChannelStatusData = _bb;
				break;
			case 15634:
				userDataMode = _bb;
				break;
			case 15635:
				fixedUserData = _bb;
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(70)]
	public virtual byte getEmphasis()
	{
		return (byte)(sbyte)emphasis;
	}

	[LineNumberTable(74)]
	public virtual short getBlockStartOffset()
	{
		return blockStartOffset;
	}

	[LineNumberTable(78)]
	public virtual byte getAuxBitsMode()
	{
		return (byte)(sbyte)auxBitsMode;
	}

	[LineNumberTable(82)]
	public virtual ByteBuffer getChannelStatusMode()
	{
		return channelStatusMode;
	}

	[LineNumberTable(86)]
	public virtual ByteBuffer getFixedChannelStatusData()
	{
		return fixedChannelStatusData;
	}

	[LineNumberTable(90)]
	public virtual ByteBuffer getUserDataMode()
	{
		return userDataMode;
	}

	[LineNumberTable(94)]
	public virtual ByteBuffer getFixedUserData()
	{
		return fixedUserData;
	}
}
