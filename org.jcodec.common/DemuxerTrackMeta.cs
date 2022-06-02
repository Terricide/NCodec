using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;

namespace org.jcodec.common;

public class DemuxerTrackMeta : java.lang.Object
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/common/DemuxerTrackMeta$Orientation;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class Orientation : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			D_0,
			D_90,
			D_180,
			D_270
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Orientation ___003C_003ED_0;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Orientation ___003C_003ED_90;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Orientation ___003C_003ED_180;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Orientation ___003C_003ED_270;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static Orientation[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Orientation D_0
		{
			[HideFromJava]
			get
			{
				return ___003C_003ED_0;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Orientation D_90
		{
			[HideFromJava]
			get
			{
				return ___003C_003ED_90;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Orientation D_180
		{
			[HideFromJava]
			get
			{
				return ___003C_003ED_180;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Orientation D_270
		{
			[HideFromJava]
			get
			{
				return ___003C_003ED_270;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(26)]
		private Orientation(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(26)]
		public static Orientation[] values()
		{
			return (Orientation[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(26)]
		public static Orientation valueOf(string name)
		{
			return (Orientation)java.lang.Enum.valueOf(ClassLiteral<Orientation>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 136, 162, 63, 34 })]
		static Orientation()
		{
			___003C_003ED_0 = new Orientation("D_0", 0);
			___003C_003ED_90 = new Orientation("D_90", 1);
			___003C_003ED_180 = new Orientation("D_180", 2);
			___003C_003ED_270 = new Orientation("D_270", 3);
			_0024VALUES = new Orientation[4] { ___003C_003ED_0, ___003C_003ED_90, ___003C_003ED_180, ___003C_003ED_270 };
		}
	}

	private TrackType type;

	private Codec codec;

	private double totalDuration;

	private int[] seekFrames;

	private int totalFrames;

	private ByteBuffer codecPrivate;

	private VideoCodecMeta videoCodecMeta;

	private AudioCodecMeta audioCodecMeta;

	private int index;

	private Orientation orientation;

	[LineNumberTable(63)]
	public virtual int[] getSeekFrames()
	{
		return seekFrames;
	}

	[LineNumberTable(47)]
	public virtual Codec getCodec()
	{
		return codec;
	}

	[LineNumberTable(94)]
	public virtual Orientation getOrientation()
	{
		return orientation;
	}

	[LineNumberTable(78)]
	public virtual ByteBuffer getCodecPrivate()
	{
		return codecPrivate;
	}

	[LineNumberTable(82)]
	public virtual VideoCodecMeta getVideoCodecMeta()
	{
		return videoCodecMeta;
	}

	[LineNumberTable(86)]
	public virtual AudioCodecMeta getAudioCodecMeta()
	{
		return audioCodecMeta;
	}

	[LineNumberTable(70)]
	public virtual int getTotalFrames()
	{
		return totalFrames;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 105, 104, 104, 106, 105, 105, 105,
		105, 105, 108
	})]
	public DemuxerTrackMeta(TrackType type, Codec codec, double totalDuration, int[] seekFrames, int totalFrames, ByteBuffer codecPrivate, VideoCodecMeta videoCodecMeta, AudioCodecMeta audioCodecMeta)
	{
		this.type = type;
		this.codec = codec;
		this.totalDuration = totalDuration;
		this.seekFrames = seekFrames;
		this.totalFrames = totalFrames;
		this.codecPrivate = codecPrivate;
		this.videoCodecMeta = videoCodecMeta;
		this.audioCodecMeta = audioCodecMeta;
		orientation = Orientation.___003C_003ED_0;
	}

	[LineNumberTable(43)]
	public virtual TrackType getType()
	{
		return type;
	}

	[LineNumberTable(54)]
	public virtual double getTotalDuration()
	{
		return totalDuration;
	}

	[LineNumberTable(74)]
	public virtual int getIndex()
	{
		return index;
	}

	[LineNumberTable(new byte[] { 159, 120, 130, 104 })]
	public virtual void setOrientation(Orientation orientation)
	{
		this.orientation = orientation;
	}
}
