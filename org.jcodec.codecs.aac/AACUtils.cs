using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using org.jcodec.codecs.mpeg4.mp4;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.codecs.aac;

public class AACUtils : Object
{
	public class AACMetadata : Object
	{
		private AudioFormat format;

		private ChannelLabel[] labels;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 130, 105, 104, 104 })]
		public AACMetadata(AudioFormat format, ChannelLabel[] labels)
		{
			this.format = format;
			this.labels = labels;
		}

		[LineNumberTable(36)]
		public virtual AudioFormat getFormat()
		{
			return format;
		}

		[LineNumberTable(40)]
		public virtual ChannelLabel[] getLabels()
		{
			return labels;
		}
	}

	private static ChannelLabel[][] AAC_DEFAULT_CONFIGS;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 105, 110, 108 })]
	private static int getObjectType(BitReader reader)
	{
		int objectType = reader.readNBit(5);
		if (objectType == ObjectType.___003C_003EAOT_ESCAPE.ordinal())
		{
			objectType = 32 + reader.readNBit(6);
		}
		return objectType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 162, 119, 100, 159, 5, 100, 99, 114 })]
	public static ByteBuffer getCodecPrivate(SampleEntry mp4a)
	{
		Box.LeafBox b = (Box.LeafBox)NodeBox.findFirst(mp4a, ClassLiteral<Box.LeafBox>.Value, "esds");
		if (b == null)
		{
			b = (Box.LeafBox)NodeBox.findFirstPath(mp4a, ClassLiteral<Box.LeafBox>.Value, new string[2] { null, "esds" });
		}
		if (b == null)
		{
			return null;
		}
		EsdsBox esds = (EsdsBox)BoxUtil.@as(ClassLiteral<EsdsBox>.Value, b);
		ByteBuffer streamInfo = esds.getStreamInfo();
		
		return streamInfo;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 136, 104, 105, 120, 138, 111, 131,
		107
	})]
	public static AACMetadata parseAudioInfo(ByteBuffer privData)
	{
		BitReader reader = BitReader.createBitReader(privData);
		int objectType = getObjectType(reader);
		int index = reader.readNBit(4);
		int sampleRate = ((index != 15) ? AACConts.AAC_SAMPLE_RATES[index] : reader.readNBit(24));
		int channelConfig = reader.readNBit(4);
		if (channelConfig == 0 || channelConfig >= (nint)AAC_DEFAULT_CONFIGS.LongLength)
		{
			return null;
		}
		ChannelLabel[] channels = AAC_DEFAULT_CONFIGS[channelConfig];
		AudioFormat.___003Cclinit_003E();
		AACMetadata result = new AACMetadata(new AudioFormat(sampleRate, 16, channels.Length, signed: true, bigEndian: false), channels);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public AACUtils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 98, 115, 113, 104, 100, 131 })]
	public static AACMetadata getMetadata(SampleEntry mp4a)
	{
		if (!String.instancehelper_equals("mp4a", mp4a.getFourcc()))
		{
			
			throw new IllegalArgumentException("Not mp4a sample entry");
		}
		ByteBuffer b = getCodecPrivate(mp4a);
		if (b == null)
		{
			return null;
		}
		AACMetadata result = parseAudioInfo(b);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 161, 67, 109, 105, 105, 106 })]
	public static ADTSParser.Header streamInfoToADTS(ByteBuffer si, bool crcAbsent, int numAACFrames, int frameSize)
	{
		BitReader rd = BitReader.createBitReader(si.duplicate());
		int objectType = rd.readNBit(5);
		int samplingIndex = rd.readNBit(4);
		int chanConfig = rd.readNBit(4);
		ADTSParser.Header result = new ADTSParser.Header(objectType, chanConfig, crcAbsent ? 1 : 0, numAACFrames, samplingIndex, 7 + frameSize);
		
		return result;
	}

	[LineNumberTable(51)]
	static AACUtils()
	{
		AAC_DEFAULT_CONFIGS = new ChannelLabel[8][]
		{
			null,
			new ChannelLabel[1] { ChannelLabel.___003C_003EMONO },
			new ChannelLabel[2]
			{
				ChannelLabel.___003C_003ESTEREO_LEFT,
				ChannelLabel.___003C_003ESTEREO_RIGHT
			},
			new ChannelLabel[3]
			{
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT
			},
			new ChannelLabel[4]
			{
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_CENTER
			},
			new ChannelLabel[5]
			{
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT
			},
			new ChannelLabel[6]
			{
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT,
				ChannelLabel.___003C_003ELFE
			},
			new ChannelLabel[8]
			{
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003ESIDE_LEFT,
				ChannelLabel.___003C_003ESIDE_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT,
				ChannelLabel.___003C_003ELFE
			}
		};
	}
}
