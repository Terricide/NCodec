using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using org.jcodec.common;

namespace org.jcodec.containers.flv;

public class FLVTag : java.lang.Object
{
	public class AacAudioTagHeader : AudioTagHeader
	{
		private int packetType;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 102, 130, 107, 104 })]
		public AacAudioTagHeader(Codec codec, AudioFormat audioFormat, int packetType)
			: base(codec, audioFormat)
		{
			this.packetType = packetType;
		}

		[LineNumberTable(167)]
		public virtual int getPacketType()
		{
			return packetType;
		}
	}

	public class AudioTagHeader : TagHeader
	{
		private AudioFormat audioFormat;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 105, 66, 106, 104 })]
		public AudioTagHeader(Codec codec, AudioFormat audioFormat)
			: base(codec)
		{
			this.audioFormat = audioFormat;
		}

		[LineNumberTable(153)]
		public virtual AudioFormat getAudioFormat()
		{
			return audioFormat;
		}
	}

	public class AvcVideoTagHeader : VideoTagHeader
	{
		private int compOffset;

		private byte avcPacketType;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 110, 97, 68, 107, 104, 105 })]
		public AvcVideoTagHeader(Codec codec, int frameType, byte avcPacketType, int compOffset) : base(codec, frameType)
		{
			int avcPacketType2 = (sbyte)avcPacketType;
			this.avcPacketType = (byte)avcPacketType2;
			this.compOffset = compOffset;
		}

		[LineNumberTable(135)]
		public virtual int getCompOffset()
		{
			return compOffset;
		}

		[LineNumberTable(139)]
		public virtual byte getAvcPacketType()
		{
			return (byte)(sbyte)avcPacketType;
		}
	}

	public class TagHeader : java.lang.Object
	{
		private Codec codec;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 117, 66, 105, 104 })]
		public TagHeader(Codec codec)
		{
			this.codec = codec;
		}

		[LineNumberTable(105)]
		public virtual Codec getCodec()
		{
			return codec;
		}
	}

	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/containers/flv/FLVTag$Type;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class Type : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			VIDEO,
			AUDIO,
			SCRIPT
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Type ___003C_003EVIDEO;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Type ___003C_003EAUDIO;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static Type ___003C_003ESCRIPT;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static Type[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Type VIDEO
		{
			[HideFromJava]
			get
			{
				return ___003C_003EVIDEO;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Type AUDIO
		{
			[HideFromJava]
			get
			{
				return ___003C_003EAUDIO;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static Type SCRIPT
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESCRIPT;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(40)]
		private Type(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(40)]
		public static Type[] values()
		{
			return (Type[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(40)]
		public static Type valueOf(string name)
		{
			return (Type)java.lang.Enum.valueOf(ClassLiteral<Type>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 132, 98, 63, 18 })]
		static Type()
		{
			___003C_003EVIDEO = new Type("VIDEO", 0);
			___003C_003EAUDIO = new Type("AUDIO", 1);
			___003C_003ESCRIPT = new Type("SCRIPT", 2);
			_0024VALUES = new Type[3] { ___003C_003EVIDEO, ___003C_003EAUDIO, ___003C_003ESCRIPT };
		}
	}

	public class VideoTagHeader : TagHeader
	{
		private int frameType;

		[LineNumberTable(119)]
		public virtual int getFrameType()
		{
			return frameType;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 114, 130, 106, 104 })]
		public VideoTagHeader(Codec codec, int frameType)
			: base(codec)
		{
			this.frameType = frameType;
		}
	}

	private Type type;

	private long position;

	private TagHeader tagHeader;

	private int pts;

	private ByteBuffer data;

	private bool keyFrame;

	private long frameNo;

	private int streamId;

	private int prevPacketSize;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 65, 68, 105, 104, 104, 104, 105, 105,
		104, 105, 105, 105
	})]
	public FLVTag(Type type, long position, TagHeader tagHeader, int pts, ByteBuffer data, bool keyFrame, long frameNo, int streamId, int prevPacketSize)
	{
		this.type = type;
		this.position = position;
		this.tagHeader = tagHeader;
		this.pts = pts;
		this.data = data;
		this.keyFrame = keyFrame;
		this.frameNo = frameNo;
		this.streamId = streamId;
		this.prevPacketSize = prevPacketSize;
	}

	[LineNumberTable(45)]
	public virtual Type getType()
	{
		return type;
	}

	[LineNumberTable(49)]
	public virtual long getPosition()
	{
		return position;
	}

	[LineNumberTable(53)]
	public virtual TagHeader getTagHeader()
	{
		return tagHeader;
	}

	[LineNumberTable(57)]
	public virtual int getPts()
	{
		return pts;
	}

	[LineNumberTable(new byte[] { 159, 127, 98, 104 })]
	public virtual void setPts(int pts)
	{
		this.pts = pts;
	}

	[LineNumberTable(65)]
	public virtual int getStreamId()
	{
		return streamId;
	}

	[LineNumberTable(new byte[] { 159, 125, 98, 104 })]
	public virtual void setStreamId(int streamId)
	{
		this.streamId = streamId;
	}

	[LineNumberTable(73)]
	public virtual int getPrevPacketSize()
	{
		return prevPacketSize;
	}

	[LineNumberTable(new byte[] { 159, 123, 98, 104 })]
	public virtual void setPrevPacketSize(int prevPacketSize)
	{
		this.prevPacketSize = prevPacketSize;
	}

	[LineNumberTable(81)]
	public virtual ByteBuffer getData()
	{
		return data;
	}

	[LineNumberTable(85)]
	public virtual double getPtsD()
	{
		return (double)pts / 1000.0;
	}

	[LineNumberTable(89)]
	public virtual bool isKeyFrame()
	{
		return keyFrame;
	}

	[LineNumberTable(93)]
	public virtual long getFrameNo()
	{
		return frameNo;
	}
}
