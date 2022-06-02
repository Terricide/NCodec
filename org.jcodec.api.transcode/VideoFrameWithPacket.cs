using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

[Signature("Ljava/lang/Object;Ljava/lang/Comparable<Lorg/jcodec/api/transcode/VideoFrameWithPacket;>;")]
[Implements(new string[] { "java.lang.Comparable" })]
public class VideoFrameWithPacket : java.lang.Object, Comparable
{
	private Packet packet;

	private PixelStore.LoanerPicture frame;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 104, 104 })]
	public VideoFrameWithPacket(Packet inFrame, PixelStore.LoanerPicture dec2)
	{
		packet = inFrame;
		frame = dec2;
	}

	[LineNumberTable(37)]
	public virtual PixelStore.LoanerPicture getFrame()
	{
		return frame;
	}

	[LineNumberTable(33)]
	public virtual Packet getPacket()
	{
		return packet;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 100, 131, 109, 109 })]
	public virtual int compareTo(VideoFrameWithPacket arg)
	{
		if (arg == null)
		{
			return -1;
		}
		long pts1 = packet.getPts();
		long pts2 = arg.packet.getPts();
		return (pts1 > pts2) ? 1 : ((pts1 != pts2) ? (-1) : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LineNumberTable(12)]
	public virtual int compareTo(object obj)
	{
		int result = compareTo((VideoFrameWithPacket)obj);
		return result;
	}

	int IComparable.CompareTo(object P_0)
	{
		return compareTo(P_0);
	}
}
