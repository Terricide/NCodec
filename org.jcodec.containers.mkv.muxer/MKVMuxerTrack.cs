using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mkv.boxes;

namespace org.jcodec.containers.mkv.muxer;

public class MKVMuxerTrack : java.lang.Object, MuxerTrack
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/containers/mkv/muxer/MKVMuxerTrack$MKVMuxerTrackType;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class MKVMuxerTrackType : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			VIDEO
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static MKVMuxerTrackType ___003C_003EVIDEO;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static MKVMuxerTrackType[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static MKVMuxerTrackType VIDEO
		{
			[HideFromJava]
			get
			{
				return ___003C_003EVIDEO;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(21)]
		private MKVMuxerTrackType(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(21)]
		public static MKVMuxerTrackType[] values()
		{
			return (MKVMuxerTrackType[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(21)]
		public static MKVMuxerTrackType valueOf(string name)
		{
			return (MKVMuxerTrackType)java.lang.Enum.valueOf(ClassLiteral<MKVMuxerTrackType>.Value, name);
		}

		[LineNumberTable(21)]
		static MKVMuxerTrackType()
		{
			___003C_003EVIDEO = new MKVMuxerTrackType("VIDEO", 0);
			_0024VALUES = new MKVMuxerTrackType[1] { ___003C_003EVIDEO };
		}
	}

	public MKVMuxerTrackType type;

	public VideoCodecMeta videoMeta;

	public string codecId;

	public int trackNo;

	private int frameDuration;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/boxes/MkvBlock;>;")]
	internal List trackBlocks;

	internal const int DEFAULT_TIMESCALE = 1000000000;

	internal const int NANOSECONDS_IN_A_MILISECOND = 1000000;

	internal const int MULTIPLIER = 1000;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 108, 108 })]
	public MKVMuxerTrack()
	{
		trackBlocks = new ArrayList();
		type = MKVMuxerTrackType.___003C_003EVIDEO;
	}

	[LineNumberTable(41)]
	public virtual int getTimescale()
	{
		return 1000000;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 117, 112, 110 })]
	public virtual void addFrame(Packet outPacket)
	{
		MkvBlock frame = MkvBlock.keyFrame(trackNo, 0, outPacket.getData());
		frame.absoluteTimecode = outPacket.getPts() - 1u;
		trackBlocks.add(frame);
	}

	[LineNumberTable(52)]
	public virtual long getTrackNo()
	{
		return trackNo;
	}
}
