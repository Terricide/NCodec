using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public class AudioFrameWithPacket : Object
{
	private AudioBuffer audio;

	private Packet packet;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 105, 104, 104 })]
	public AudioFrameWithPacket(AudioBuffer audio, Packet packet)
	{
		this.audio = audio;
		this.packet = packet;
	}

	[LineNumberTable(24)]
	public virtual AudioBuffer getAudio()
	{
		return audio;
	}

	[LineNumberTable(29)]
	public virtual Packet getPacket()
	{
		return packet;
	}
}
