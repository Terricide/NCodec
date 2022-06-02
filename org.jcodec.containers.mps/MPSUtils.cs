using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.containers.mps;

public class MPSUtils : java.lang.Object
{
	public class AACAudioDescriptor : MPEGMediaDescriptor
	{
		private int profile;

		private int channel;

		private int flags;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(425)]
		public AACAudioDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 34, 66, 104, 116, 116, 116 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			profile = (sbyte)buf.get() & 0xFF;
			channel = (sbyte)buf.get() & 0xFF;
			flags = (sbyte)buf.get() & 0xFF;
		}

		[LineNumberTable(439)]
		public virtual int getProfile()
		{
			return profile;
		}

		[LineNumberTable(443)]
		public virtual int getChannel()
		{
			return channel;
		}

		[LineNumberTable(447)]
		public virtual int getFlags()
		{
			return flags;
		}
	}

	public class AudioStreamDescriptor : MPEGMediaDescriptor
	{
		private int variableRateAudioIndicator;

		private int freeFormatFlag;

		private int id;

		private int layer;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(319)]
		public AudioStreamDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 61, 162, 104, 111, 108, 108, 108, 108 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			int b0 = (sbyte)buf.get() & 0xFF;
			freeFormatFlag = (b0 >> 7) & 1;
			id = (b0 >> 6) & 1;
			layer = (b0 >> 5) & 3;
			variableRateAudioIndicator = (b0 >> 3) & 1;
		}

		[LineNumberTable(336)]
		public virtual int getVariableRateAudioIndicator()
		{
			return variableRateAudioIndicator;
		}

		[LineNumberTable(340)]
		public virtual int getFreeFormatFlag()
		{
			return freeFormatFlag;
		}

		[LineNumberTable(344)]
		public virtual int getId()
		{
			return id;
		}

		[LineNumberTable(348)]
		public virtual int getLayer()
		{
			return layer;
		}
	}

	public class AVCVideoDescriptor : MPEGMediaDescriptor
	{
		private int profileIdc;

		private int flags;

		private int level;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(398)]
		public AVCVideoDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 41, 130, 104, 116, 116, 116 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			profileIdc = (sbyte)buf.get() & 0xFF;
			flags = (sbyte)buf.get() & 0xFF;
			level = (sbyte)buf.get() & 0xFF;
		}

		[LineNumberTable(413)]
		public virtual int getProfileIdc()
		{
			return profileIdc;
		}

		[LineNumberTable(417)]
		public virtual int getFlags()
		{
			return flags;
		}

		[LineNumberTable(421)]
		public virtual int getLevel()
		{
			return level;
		}
	}

	public class DataStreamAlignmentDescriptor : MPEGMediaDescriptor
	{
		private int alignmentType;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(451)]
		public DataStreamAlignmentDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 28, 66, 104, 116 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			alignmentType = (sbyte)buf.get() & 0xFF;
		}

		[LineNumberTable(461)]
		public virtual int getAlignmentType()
		{
			return alignmentType;
		}
	}

	public class ISO639LanguageDescriptor : MPEGMediaDescriptor
	{
		private IntArrayList languageCodes;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 53, 66, 105, 108 })]
		public ISO639LanguageDescriptor()
		{
			languageCodes = IntArrayList.createIntArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 52, 130, 104, 106, 148 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			while (buf.remaining() >= 4)
			{
				languageCodes.add(buf.getInt());
			}
		}

		[LineNumberTable(369)]
		public virtual IntArrayList getLanguageCodes()
		{
			return languageCodes;
		}
	}

	public class Mpeg4AudioDescriptor : MPEGMediaDescriptor
	{
		private int profileLevel;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(383)]
		public Mpeg4AudioDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 45, 98, 104, 116 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			profileLevel = (sbyte)buf.get() & 0xFF;
		}

		[LineNumberTable(394)]
		public virtual int getProfileLevel()
		{
			return profileLevel;
		}
	}

	public class Mpeg4VideoDescriptor : MPEGMediaDescriptor
	{
		private int profileLevel;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(373)]
		public Mpeg4VideoDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 48, 130, 104, 116 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			profileLevel = (sbyte)buf.get() & 0xFF;
		}
	}

	public class MPEGMediaDescriptor : java.lang.Object
	{
		private int tag;

		private int len;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(229)]
		public MPEGMediaDescriptor()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 84, 130, 116, 116 })]
		public virtual void parse(ByteBuffer buf)
		{
			tag = (sbyte)buf.get() & 0xFF;
			len = (sbyte)buf.get() & 0xFF;
		}

		[LineNumberTable(239)]
		public virtual int getTag()
		{
			return tag;
		}

		[LineNumberTable(243)]
		public virtual int getLen()
		{
			return len;
		}
	}

	public abstract class PESReader : java.lang.Object
	{
		private int marker;

		private int lenFieldLeft;

		private int pesLen;

		private long pesFileStart;

		private int stream;

		private bool _pes;

		private int pesLeft;

		private ByteBuffer pesBuffer;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 122, 130, 104, 108, 109, 115, 116, 143, 105,
			110, 127, 3, 105, 104, 104, 166, 111, 105, 119,
			113, 127, 2, 114, 105, 127, 4, 137, 104, 115,
			104, 104, 127, 1, 105, 114, 159, 4, 105, 104,
			106, 106, 113, 111, 105, 109, 105, 103, 200, 102
		})]
		public virtual void analyseBuffer(ByteBuffer buf, long pos)
		{
			int init = buf.position();
			while (buf.hasRemaining())
			{
				if (pesLeft > 0)
				{
					int toRead = java.lang.Math.min(buf.remaining(), pesLeft);
					pesBuffer.put(NIOUtils.read(buf, toRead));
					pesLeft -= toRead;
					if (pesLeft == 0)
					{
						long filePos3 = pos + buf.position() - init;
						pes1(pesBuffer, pesFileStart, (int)(filePos3 - pesFileStart), stream);
						pesFileStart = -1L;
						_pes = false;
						stream = -1;
					}
					continue;
				}
				int bt = (sbyte)buf.get() & 0xFF;
				if (_pes)
				{
					pesBuffer.put((byte)(sbyte)((uint)marker >> 24));
				}
				marker = (marker << 8) | bt;
				if (marker >= 443 && marker <= 495)
				{
					long filePos2 = pos + buf.position() - init - 4u;
					if (_pes)
					{
						pes1(pesBuffer, pesFileStart, (int)(filePos2 - pesFileStart), stream);
					}
					pesFileStart = filePos2;
					_pes = true;
					stream = marker & 0xFF;
					lenFieldLeft = 2;
					pesLen = 0;
				}
				else if (marker >= 441 && marker <= 511)
				{
					if (_pes)
					{
						long filePos = pos + buf.position() - init - 4u;
						pes1(pesBuffer, pesFileStart, (int)(filePos - pesFileStart), stream);
					}
					pesFileStart = -1L;
					_pes = false;
					stream = -1;
				}
				else
				{
					if (lenFieldLeft <= 0)
					{
						continue;
					}
					pesLen = (pesLen << 8) | bt;
					lenFieldLeft--;
					if (lenFieldLeft == 0)
					{
						pesLeft = pesLen;
						if (pesLen != 0)
						{
							flushMarker();
							marker = -1;
						}
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 107, 130, 104, 108, 104 })]
		private void pes1(ByteBuffer pesBuffer, long start, int pesLen, int stream)
		{
			pesBuffer.flip();
			pes(pesBuffer, start, pesLen, stream);
			pesBuffer.clear();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 109, 162, 119, 125, 124, 122 })]
		private void flushMarker()
		{
			pesBuffer.put((byte)(sbyte)((uint)marker >> 24));
			pesBuffer.put((byte)(sbyte)(((uint)marker >> 16) & 0xFFu));
			pesBuffer.put((byte)(sbyte)(((uint)marker >> 8) & 0xFFu));
			pesBuffer.put((byte)(sbyte)((uint)marker & 0xFFu));
		}

		protected internal abstract void pes(ByteBuffer bb, long l, int i1, int i2);

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 124, 162, 233, 54, 168, 233, 72, 113 })]
		public PESReader()
		{
			marker = -1;
			pesFileStart = -1L;
			pesBuffer = ByteBuffer.allocate(2097152);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 105, 66, 106, 103, 159, 7 })]
		public virtual void finishRead()
		{
			if (pesLeft <= 4)
			{
				flushMarker();
				pes1(pesBuffer, pesFileStart, pesBuffer.position(), stream);
			}
		}
	}

	public class RegistrationDescriptor : MPEGMediaDescriptor
	{
		private int formatIdentifier;

		private IntArrayList additionalFormatIdentifiers;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 25, 130, 105, 108 })]
		public RegistrationDescriptor()
		{
			additionalFormatIdentifiers = IntArrayList.createIntArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 23, 66, 104, 109, 105, 155 })]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			formatIdentifier = buf.getInt();
			while (buf.hasRemaining())
			{
				additionalFormatIdentifiers.add((sbyte)buf.get() & 0xFF);
			}
		}

		[LineNumberTable(484)]
		public virtual int getFormatIdentifier()
		{
			return formatIdentifier;
		}

		[LineNumberTable(488)]
		public virtual IntArrayList getAdditionalFormatIdentifiers()
		{
			return additionalFormatIdentifiers;
		}
	}

	public class VideoStreamDescriptor : MPEGMediaDescriptor
	{
		private int multipleFrameRate;

		private int frameRateCode;

		private bool mpeg1Only;

		private int constrainedParameter;

		private int stillPicture;

		private int profileAndLevel;

		private int chromaFormat;

		private int frameRateExtension;

		internal Rational[] frameRates;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 78, 162, 105, 191, 160, 67 })]
		public VideoStreamDescriptor()
		{
			frameRates = new Rational[16]
			{
				null,
				new Rational(24000, 1001),
				new Rational(24, 1),
				new Rational(25, 1),
				new Rational(30000, 1001),
				new Rational(30, 1),
				new Rational(50, 1),
				new Rational(60000, 1001),
				new Rational(60, 1),
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 76, 162, 104, 111, 108, 109, 114, 108, 106,
			105, 116, 111, 106, 140
		})]
		public override void parse(ByteBuffer buf)
		{
			base.parse(buf);
			int b0 = (sbyte)buf.get() & 0xFF;
			multipleFrameRate = (b0 >> 7) & 1;
			frameRateCode = (b0 >> 3) & 0xF;
			mpeg1Only = ((((b0 >> 2) & 1) == 0) ? true : false);
			constrainedParameter = (b0 >> 1) & 1;
			stillPicture = b0 & 1;
			if (!mpeg1Only)
			{
				profileAndLevel = (sbyte)buf.get() & 0xFF;
				int b1 = (sbyte)buf.get() & 0xFF;
				chromaFormat = b1 >> 6;
				frameRateExtension = (b1 >> 5) & 1;
			}
		}

		[LineNumberTable(283)]
		public virtual Rational getFrameRate()
		{
			return frameRates[frameRateCode];
		}

		[LineNumberTable(287)]
		public virtual int getMultipleFrameRate()
		{
			return multipleFrameRate;
		}

		[LineNumberTable(291)]
		public virtual int getFrameRateCode()
		{
			return frameRateCode;
		}

		[LineNumberTable(295)]
		public virtual bool isMpeg1Only()
		{
			return mpeg1Only;
		}

		[LineNumberTable(299)]
		public virtual int getConstrainedParameter()
		{
			return constrainedParameter;
		}

		[LineNumberTable(303)]
		public virtual int getStillPicture()
		{
			return stillPicture;
		}

		[LineNumberTable(307)]
		public virtual int getProfileAndLevel()
		{
			return profileAndLevel;
		}

		[LineNumberTable(311)]
		public virtual int getChromaFormat()
		{
			return chromaFormat;
		}

		[LineNumberTable(315)]
		public virtual int getFrameRateExtension()
		{
			return frameRateExtension;
		}
	}

	private sealed class ___003CCallerID_003E : CallerID
	{
		internal ___003CCallerID_003E()
		{
		}
	}

	public const int VIDEO_MIN = 480;

	public const int VIDEO_MAX = 495;

	public const int AUDIO_MIN = 448;

	public const int AUDIO_MAX = 479;

	public const int PACK = 442;

	public const int SYSTEM = 443;

	public const int PSM = 444;

	public const int PRIVATE_1 = 445;

	public const int PRIVATE_2 = 447;

	[Signature("[Ljava/lang/Class<+Lorg/jcodec/containers/mps/MPSUtils$MPEGMediaDescriptor;>;")]
	public static Class[] dMapping;

	[SpecialName]
	private static CallerID ___003CcallerID_003E;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 134, 130, 127, 15, 44 })]
	public static bool mediaStream(int streamId)
	{
		return ((streamId >= _0024(448) && streamId <= _0024(495)) || streamId == _0024(445) || streamId == _0024(447)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 66, 110, 110, 105, 111, 111, 142, 142 })]
	public static PESPacket readPESHeader(ByteBuffer iss, long pos)
	{
		int streamId = iss.getInt() & 0xFF;
		int len = iss.getShort() & 0xFFFF;
		if (streamId != 191)
		{
			int b0 = (sbyte)iss.get() & 0xFF;
			if ((b0 & 0xC0) == 128)
			{
				PESPacket result = mpeg2Pes(b0, len, streamId, iss, pos);
				
				return result;
			}
			PESPacket result2 = mpeg1Pes(b0, len, streamId, iss, pos);
			
			return result2;
		}
		PESPacket result3 = new PESPacket(null, -1L, streamId, len, pos, -1L);
		
		return result3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(51)]
	public static bool videoStream(int streamId)
	{
		return (streamId >= _0024(480) && streamId <= _0024(495)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 127, 15, 44 })]
	public static bool audioStream(int streamId)
	{
		return ((streamId >= _0024(448) && streamId <= _0024(479)) || streamId == _0024(445) || streamId == _0024(447)) ? true : false;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(43)]
	public static bool psMarker(int marker)
	{
		return (marker >= 445 && marker <= 495) ? true : false;
	}

	[LineNumberTable(47)]
	public static bool videoMarker(int marker)
	{
		return (marker >= 480 && marker <= 495) ? true : false;
	}

	[LineNumberTable(60)]
	internal static int _0024(int marker)
	{
		return marker & 0xFF;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 130, 99, 111, 143, 104, 111, 104, 109,
		111, 104, 105, 142, 137
	})]
	public static PESPacket mpeg2Pes(int b0, int len, int streamId, ByteBuffer @is, long pos)
	{
		int flags2 = (sbyte)@is.get() & 0xFF;
		int header_len = (sbyte)@is.get() & 0xFF;
		long pts = -1L;
		long dts = -1L;
		if ((flags2 & 0xC0) == 128)
		{
			pts = readTs(@is);
			NIOUtils.skip(@is, header_len - 5);
		}
		else if ((flags2 & 0xC0) == 192)
		{
			pts = readTs(@is);
			dts = readTs(@is);
			NIOUtils.skip(@is, header_len - 10);
		}
		else
		{
			NIOUtils.skip(@is, header_len);
		}
		PESPacket result = new PESPacket(null, pts, streamId, len, pos, dts);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 98, 99, 105, 177, 108, 105, 143, 103,
		108, 107, 108, 105, 138, 102, 177
	})]
	public static PESPacket mpeg1Pes(int b0, int len, int streamId, ByteBuffer @is, long pos)
	{
		int c;
		for (c = b0; c == 255; c = (sbyte)@is.get() & 0xFF)
		{
		}
		if ((c & 0xC0) == 64)
		{
			_ = (sbyte)@is.get();
			c = (sbyte)@is.get() & 0xFF;
		}
		long pts = -1L;
		long dts = -1L;
		if ((c & 0xF0) == 32)
		{
			pts = _readTs(@is, c);
		}
		else if ((c & 0xF0) == 48)
		{
			pts = _readTs(@is, c);
			dts = readTs(@is);
		}
		else if (c != 15)
		{
			
			throw new RuntimeException("Invalid data");
		}
		PESPacket result = new PESPacket(null, pts, streamId, len, pos, dts);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 98, 127, 18, 63, 3 })]
	public static long _readTs(ByteBuffer @is, int c)
	{
		return ((c & 0xEu) << 29) | (((sbyte)@is.get() & 0xFF) << 22) | (((sbyte)@is.get() & 0xFF) >> 1 << 15) | (((sbyte)@is.get() & 0xFF) << 7) | (((sbyte)@is.get() & 0xFF) >> 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 88, 98, 127, 24, 63, 3 })]
	public static long readTs(ByteBuffer @is)
	{
		return (((sbyte)@is.get() & 0xEu) << 29) | (((sbyte)@is.get() & 0xFF) << 22) | (((sbyte)@is.get() & 0xFF) >> 1 << 15) | (((sbyte)@is.get() & 0xFF) << 7) | (((sbyte)@is.get() & 0xFF) >> 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public MPSUtils()
	{
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(39)]
	public static bool mediaMarker(int marker)
	{
		return ((marker >= 448 && marker <= 495) || marker == 445 || marker == 447) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 130, 112, 110, 112, 109, 109 })]
	public static void writeTs(ByteBuffer @is, long ts)
	{
		@is.put((byte)(sbyte)(ts >> 29 << 1));
		@is.put((byte)(sbyte)(ts >> 22));
		@is.put((byte)(sbyte)(ts >> 15 << 1));
		@is.put((byte)(sbyte)(ts >> 7));
		@is.put((byte)(sbyte)(ts >> 1));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;)Ljava/util/List<Lorg/jcodec/containers/mps/MPSUtils$MPEGMediaDescriptor;>;")]
	[LineNumberTable(new byte[]
	{
		159, 16, 162, 103, 109, 104, 111, 111, 108, 141,
		121, 106, 191, 4, 3, 99, 142, 102
	})]
	public static List parseDescriptors(ByteBuffer bb)
	{
		ArrayList result = new ArrayList();
		while (bb.remaining() >= 2)
		{
			ByteBuffer dup = bb.duplicate();
			int tag = (sbyte)dup.get() & 0xFF;
			int len = (sbyte)dup.get() & 0xFF;
			ByteBuffer descriptorBuffer = NIOUtils.read(bb, len + 2);
			if (dMapping[tag] == null)
			{
				continue;
			}
			java.lang.Exception ex2;
			try
			{
				MPEGMediaDescriptor descriptor = (MPEGMediaDescriptor)dMapping[tag].newInstance(MPSUtils.___003CGetCallerID_003E());
				descriptor.parse(descriptorBuffer);
				((List)result).add((object)descriptor);
			}
			catch (System.Exception x)
			{
				java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
				if (ex == null)
				{
					throw;
				}
				ex2 = ex;
				goto IL_0096;
			}
			continue;
			IL_0096:
			java.lang.Exception e = ex2;
			
			throw new RuntimeException(e);
		}
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 19, 66, 176, 109, 109, 109, 109, 110, 110,
		110, 110, 110
	})]
	static MPSUtils()
	{
		dMapping = new Class[256];
		dMapping[2] = ClassLiteral<VideoStreamDescriptor>.Value;
		dMapping[3] = ClassLiteral<AudioStreamDescriptor>.Value;
		dMapping[6] = ClassLiteral<DataStreamAlignmentDescriptor>.Value;
		dMapping[5] = ClassLiteral<RegistrationDescriptor>.Value;
		dMapping[10] = ClassLiteral<ISO639LanguageDescriptor>.Value;
		dMapping[27] = ClassLiteral<Mpeg4VideoDescriptor>.Value;
		dMapping[28] = ClassLiteral<Mpeg4AudioDescriptor>.Value;
		dMapping[40] = ClassLiteral<AVCVideoDescriptor>.Value;
		dMapping[43] = ClassLiteral<AACAudioDescriptor>.Value;
	}

	static CallerID ___003CGetCallerID_003E()
	{
		if (___003CcallerID_003E == null)
		{
			___003CcallerID_003E = new ___003CCallerID_003E();
		}
		return ___003CcallerID_003E;
	}
}
