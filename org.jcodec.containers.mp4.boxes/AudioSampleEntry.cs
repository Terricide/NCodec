using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.api;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes.channel;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class AudioSampleEntry : SampleEntry
{
	public static int kAudioFormatFlagIsFloat;

	public static int kAudioFormatFlagIsBigEndian;

	public static int kAudioFormatFlagIsSignedInteger;

	public static int kAudioFormatFlagIsPacked;

	public static int kAudioFormatFlagIsAlignedHigh;

	public static int kAudioFormatFlagIsNonInterleaved;

	public static int kAudioFormatFlagIsNonMixable;

	private short channelCount;

	private short sampleSize;

	private float sampleRate;

	private short revision;

	private int vendor;

	private int compressionId;

	private int pktSize;

	private int samplesPerPkt;

	private int bytesPerPkt;

	private int bytesPerFrame;

	private int bytesPerSample;

	private short version;

	private int lpcmFlags;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Label;>;")]
	private static List MONO;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Label;>;")]
	private static List STEREO;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Label;>;")]
	private static List MATRIX_STEREO;

	internal static Label[] ___003C_003EEMPTY;

	[Signature("Ljava/util/Set<Ljava/lang/String;>;")]
	public static Set pcms;

	[Signature("Ljava/util/Map<Lorg/jcodec/common/model/Label;Lorg/jcodec/common/model/ChannelLabel;>;")]
	private static Map translationStereo;

	[Signature("Ljava/util/Map<Lorg/jcodec/common/model/Label;Lorg/jcodec/common/model/ChannelLabel;>;")]
	private static Map translationSurround;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Label[] EMPTY
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEMPTY;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 130, 106 })]
	public AudioSampleEntry(Header atom)
		: base(atom)
	{
	}

	[LineNumberTable(new byte[] { 159, 99, 66, 113, 145 })]
	public virtual int calcFrameSize()
	{
		if (version == 0 || bytesPerFrame == 0)
		{
			return (sampleSize >> 3) * channelCount;
		}
		return bytesPerFrame;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(179)]
	public virtual int calcSampleSize()
	{
		int num = calcFrameSize();
		short num2 = channelCount;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 92, 162, 127, 9, 103, 120, 103, 120, 124,
		120, 135, 135
	})]
	public virtual ByteOrder getEndian()
	{
		EndianBox endianBox = (EndianBox)NodeBox.findFirstPath(this, ClassLiteral<EndianBox>.Value, new string[2]
		{
			WaveExtension.fourcc(),
			EndianBox.fourcc()
		});
		if (endianBox == null)
		{
			if (String.instancehelper_equals("twos", header.getFourcc()))
			{
				return ByteOrder.BIG_ENDIAN;
			}
			if (String.instancehelper_equals("lpcm", header.getFourcc()))
			{
				return ((lpcmFlags & kAudioFormatFlagIsBigEndian) == 0) ? ByteOrder.LITTLE_ENDIAN : ByteOrder.BIG_ENDIAN;
			}
			if (String.instancehelper_equals("sowt", header.getFourcc()))
			{
				return ByteOrder.LITTLE_ENDIAN;
			}
			return ByteOrder.BIG_ENDIAN;
		}
		ByteOrder endian = endianBox.getEndian();
		
		return endian;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 29, 162, 105, 109, 106, 104, 103, 48, 135,
		131, 104, 107, 104, 108, 106, 111, 106, 144, 235,
		56, 236, 76
	})]
	public static Label[] getLabelsFromChan(ChannelBox box)
	{
		long tag = box.getChannelLayout();
		if (tag >> 16 == 147u)
		{
			int k = (int)tag & 0xFFFF;
			Label[] res = new Label[k];
			for (int j = 0; j < k; j++)
			{
				res[j] = Label.getByVal(0x10000 | j);
			}
			return res;
		}
		ChannelLayout[] values = ChannelLayout.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			ChannelLayout layout = values[i];
			if (layout.getCode() != tag)
			{
				continue;
			}
			if (layout == ChannelLayout.___003C_003EkCAFChannelLayoutTag_UseChannelDescriptions)
			{
				Label[] result = extractLabels(box.getDescriptions());
				
				return result;
			}
			if (layout == ChannelLayout.___003C_003EkCAFChannelLayoutTag_UseChannelBitmap)
			{
				Label[] labelsByBitmap = getLabelsByBitmap(box.getChannelBitmap());
				
				return labelsByBitmap;
			}
			Label[] labels = layout.getLabels();
			
			return labels;
		}
		return ___003C_003EEMPTY;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Lorg/jcodec/common/model/Label;Lorg/jcodec/common/model/ChannelLabel;>;[Lorg/jcodec/common/model/Label;)[Lorg/jcodec/common/model/ChannelLabel;")]
	[LineNumberTable(new byte[] { 159, 75, 130, 105, 99, 104, 101, 20, 199 })]
	private ChannelLabel[] translate(Map translation, Label[] labels)
	{
		ChannelLabel[] result = new ChannelLabel[(nint)labels.LongLength];
		int i = 0;
		for (int j = 0; j < (nint)labels.LongLength; j++)
		{
			Label label = labels[j];
			int num = i;
			i++;
			result[num] = (ChannelLabel)translation.get(label);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 97, 78, 105, 105, 105, 105, 107, 105,
		106, 106, 106, 106, 106, 106, 106, 106
	})]
	public static AudioSampleEntry createAudioSampleEntry(Header header, short drefInd, short channelCount, short sampleSize, int sampleRate, short revision, int vendor, int compressionId, int pktSize, int samplesPerPkt, int bytesPerPkt, int bytesPerFrame, int bytesPerSample, short version)
	{
		AudioSampleEntry audio = new AudioSampleEntry(header);
		audio.drefInd = drefInd;
		audio.channelCount = channelCount;
		audio.sampleSize = sampleSize;
		audio.sampleRate = sampleRate;
		audio.revision = revision;
		audio.vendor = vendor;
		audio.compressionId = compressionId;
		audio.pktSize = pktSize;
		audio.samplesPerPkt = samplesPerPkt;
		audio.bytesPerPkt = bytesPerPkt;
		audio.bytesPerFrame = bytesPerFrame;
		audio.bytesPerSample = bytesPerSample;
		audio.version = version;
		return audio;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 66, 98, 115, 103, 107, 135 })]
	public static string lookupFourcc(AudioFormat format)
	{
		if (format.getSampleSizeInBits() == 16 && !format.isBigEndian())
		{
			return "sowt";
		}
		if (format.getSampleSizeInBits() == 24)
		{
			return "in24";
		}
		string msg = new StringBuilder().append("Audio format ").append(format).append(" is not supported.")
			.toString();
		
		throw new NotSupportedException(msg);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 70, 98, 223, 7, 113, 136, 109, 110, 172 })]
	public static AudioSampleEntry audioSampleEntry(string fourcc, int drefId, int sampleSize, int channels, int sampleRate, ByteOrder endian)
	{
		AudioSampleEntry ase = createAudioSampleEntry(Header.createHeader(fourcc, 0L), (short)drefId, (short)channels, 16, sampleRate, 0, 0, 65535, 0, 1, sampleSize, channels * sampleSize, sampleSize, 1);
		NodeBox wave = new NodeBox(new Header("wave"));
		ase.add(wave);
		wave.add(FormatBox.createFormatBox(fourcc));
		wave.add(EndianBox.createEndianBox(endian));
		wave.add(Box.terminatorAtom());
		return ase;
	}

	[LineNumberTable(168)]
	public virtual short getChannelCount()
	{
		return channelCount;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 54, 130, 119, 100, 138, 104, 159, 7, 144,
		152, 159, 1, 159, 9, 159, 17, 191, 25, 104,
		108
	})]
	public static Label[] getLabelsFromSampleEntry(AudioSampleEntry se)
	{
		ChannelBox channel = (ChannelBox)NodeBox.findFirst(se, ClassLiteral<ChannelBox>.Value, "chan");
		if (channel != null)
		{
			Label[] labelsFromChan = getLabelsFromChan(channel);
			
			return labelsFromChan;
		}
		int channelCount = se.getChannelCount();
		switch (channelCount)
		{
		case 1:
			return new Label[1] { Label.___003C_003EMono };
		case 2:
			return new Label[2]
			{
				Label.___003C_003ELeft,
				Label.___003C_003ERight
			};
		case 3:
			return new Label[3]
			{
				Label.___003C_003ELeft,
				Label.___003C_003ERight,
				Label.___003C_003ECenter
			};
		case 4:
			return new Label[4]
			{
				Label.___003C_003ELeft,
				Label.___003C_003ERight,
				Label.___003C_003ELeftSurround,
				Label.___003C_003ERightSurround
			};
		case 5:
			return new Label[5]
			{
				Label.___003C_003ELeft,
				Label.___003C_003ERight,
				Label.___003C_003ECenter,
				Label.___003C_003ELeftSurround,
				Label.___003C_003ERightSurround
			};
		case 6:
			return new Label[6]
			{
				Label.___003C_003ELeft,
				Label.___003C_003ERight,
				Label.___003C_003ECenter,
				Label.___003C_003ELFEScreen,
				Label.___003C_003ELeftSurround,
				Label.___003C_003ERightSurround
			};
		default:
		{
			Label[] res = new Label[channelCount];
			Arrays.fill(res, Label.___003C_003EMono);
			return res;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(382)]
	public static Label[] getLabelsFromTrack(TrakBox trakBox)
	{
		Label[] labelsFromSampleEntry = getLabelsFromSampleEntry((AudioSampleEntry)trakBox.getSampleEntries()[0]);
		
		return labelsFromSampleEntry;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 44, 66, 127, 37, 100, 103, 159, 34, 106 })]
	public static void _setLabels(TrakBox trakBox, Label[] labels)
	{
		ChannelBox channel = (ChannelBox)NodeBox.findFirstPath(trakBox, ClassLiteral<ChannelBox>.Value, new string[6] { "mdia", "minf", "stbl", "stsd", null, "chan" });
		if (channel == null)
		{
			channel = ChannelBox.createChannelBox();
			((SampleEntry)NodeBox.findFirstPath(trakBox, ClassLiteral<SampleEntry>.Value, new string[5] { "mdia", "minf", "stbl", "stsd", null })).add(channel);
		}
		setLabels(labels, channel);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 42, 98, 113, 105, 104, 63, 17, 135, 106 })]
	public static void setLabels(Label[] labels, ChannelBox channel)
	{
		channel.setChannelLayout(ChannelLayout.___003C_003EkCAFChannelLayoutTag_UseChannelDescriptions.getCode());
		ChannelBox.ChannelDescription[] list = new ChannelBox.ChannelDescription[(nint)labels.LongLength];
		for (int i = 0; i < (nint)labels.LongLength; i++)
		{
			list[i] = new ChannelBox.ChannelDescription(labels[i].getVal(), 0, new float[3] { 0f, 0f, 0f });
		}
		channel.setDescriptions(list);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 30, 66, 105, 104, 44, 135 })]
	public static Label[] extractLabels(ChannelBox.ChannelDescription[] descriptions)
	{
		Label[] result = new Label[(nint)descriptions.LongLength];
		for (int i = 0; i < (nint)descriptions.LongLength; i++)
		{
			result[i] = descriptions[i].getLabel();
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 33, 98, 103, 103, 104, 101, 109, 233, 61,
		231, 69
	})]
	public static Label[] getLabelsByBitmap(long channelBitmap)
	{
		ArrayList result = new ArrayList();
		Label[] values = Label.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			Label label = values[i];
			if ((label.___003C_003EbitmapVal & channelBitmap) != 0u)
			{
				((List)result).add((object)label);
			}
		}
		return (Label[])((List)result).toArray((object[])new Label[0]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 130, 136, 109, 109, 141, 109, 141, 109,
		141, 109, 144, 106, 109, 109, 109, 111, 106, 104,
		117, 110, 104, 110, 109, 109, 141, 106
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		version = input.getShort();
		revision = input.getShort();
		vendor = input.getInt();
		channelCount = input.getShort();
		sampleSize = input.getShort();
		compressionId = input.getShort();
		pktSize = input.getShort();
		long sr = Platform.unsignedInt(input.getInt());
		sampleRate = (float)sr / 65536f;
		if (version == 1)
		{
			samplesPerPkt = input.getInt();
			bytesPerPkt = input.getInt();
			bytesPerFrame = input.getInt();
			bytesPerSample = input.getInt();
		}
		else if (version == 2)
		{
			input.getInt();
			DoubleConverter converter = default(DoubleConverter);
			sampleRate = (float)DoubleConverter.ToDouble(input.getLong(), ref converter);
			channelCount = (short)input.getInt();
			input.getInt();
			sampleSize = (short)input.getInt();
			lpcmFlags = input.getInt();
			bytesPerFrame = input.getInt();
			samplesPerPkt = input.getInt();
		}
		parseExtensions(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 66, 136, 110, 110, 142, 109, 110, 105,
		144, 138, 111, 143, 159, 0, 109, 110, 110, 110,
		147, 109, 105, 106, 106, 105, 109, 106, 116, 110,
		109, 110, 110, 110, 174, 106
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putShort(version);
		@out.putShort(revision);
		@out.putInt(vendor);
		if (version < 2)
		{
			@out.putShort(channelCount);
			if (version == 0)
			{
				@out.putShort(sampleSize);
			}
			else
			{
				@out.putShort(16);
			}
			@out.putShort((short)compressionId);
			@out.putShort((short)pktSize);
			@out.putInt((int)Math.round((double)sampleRate * 65536.0));
			if (version == 1)
			{
				@out.putInt(samplesPerPkt);
				@out.putInt(bytesPerPkt);
				@out.putInt(bytesPerFrame);
				@out.putInt(bytesPerSample);
			}
		}
		else if (version == 2)
		{
			@out.putShort(3);
			@out.putShort(16);
			@out.putShort(-2);
			@out.putShort(0);
			@out.putInt(65536);
			@out.putInt(72);
			@out.putLong(Double.doubleToLongBits(sampleRate));
			@out.putInt(channelCount);
			@out.putInt(2130706432);
			@out.putInt(sampleSize);
			@out.putInt(lpcmFlags);
			@out.putInt(bytesPerFrame);
			@out.putInt(samplesPerPkt);
		}
		writeExtensions(@out);
	}

	[LineNumberTable(183)]
	public virtual short getSampleSize()
	{
		return sampleSize;
	}

	[LineNumberTable(187)]
	public virtual float getSampleRate()
	{
		return sampleRate;
	}

	[LineNumberTable(191)]
	public virtual int getBytesPerFrame()
	{
		return bytesPerFrame;
	}

	[LineNumberTable(195)]
	public virtual int getBytesPerSample()
	{
		return bytesPerSample;
	}

	[LineNumberTable(199)]
	public virtual short getVersion()
	{
		return version;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 88, 130, 127, 27, 63, 0 })]
	public virtual bool isFloat()
	{
		return (String.instancehelper_equals("fl32", header.getFourcc()) || String.instancehelper_equals("fl64", header.getFourcc()) || (String.instancehelper_equals("lpcm", header.getFourcc()) && (lpcmFlags & kAudioFormatFlagIsFloat) != 0)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(236)]
	public virtual bool isPCM()
	{
		bool result = pcms.contains(header.getFourcc());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 66, 127, 2, 52 })]
	public virtual AudioFormat getFormat()
	{
		AudioFormat.___003Cclinit_003E();
		AudioFormat result = new AudioFormat(ByteCodeHelper.f2i(sampleRate), calcSampleSize() << 3, channelCount, signed: true, getEndian() == ByteOrder.BIG_ENDIAN);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 98, 119, 100, 104, 106, 144, 144, 159,
		0, 144, 152, 191, 25, 109, 108
	})]
	public virtual ChannelLabel[] getLabels()
	{
		ChannelBox channelBox = (ChannelBox)NodeBox.findFirst(this, ClassLiteral<ChannelBox>.Value, "chan");
		if (channelBox != null)
		{
			Label[] labels = getLabelsFromChan(channelBox);
			if (channelCount == 2)
			{
				ChannelLabel[] result = translate(translationStereo, labels);
				
				return result;
			}
			ChannelLabel[] result2 = translate(translationSurround, labels);
			
			return result2;
		}
		switch (channelCount)
		{
		case 1:
			return new ChannelLabel[1] { ChannelLabel.___003C_003EMONO };
		case 2:
			return new ChannelLabel[2]
			{
				ChannelLabel.___003C_003ESTEREO_LEFT,
				ChannelLabel.___003C_003ESTEREO_RIGHT
			};
		case 6:
			return new ChannelLabel[6]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003ELFE,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT
			};
		default:
		{
			ChannelLabel[] lbl = new ChannelLabel[channelCount];
			Arrays.fill(lbl, ChannelLabel.___003C_003EMONO);
			return lbl;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 98, 191, 8 })]
	public static AudioSampleEntry compressedAudioSampleEntry(string fourcc, int drefId, int sampleSize, int channels, int sampleRate, int samplesPerPacket, int bytesPerPacket, int bytesPerFrame)
	{
		return createAudioSampleEntry(Header.createHeader(fourcc, 0L), (short)drefId, (short)channels, 16, sampleRate, 0, 0, 65534, 0, samplesPerPacket, bytesPerPacket, bytesPerFrame, 2, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 64, 130, 113, 109, 20 })]
	public static AudioSampleEntry audioSampleEntryPCM(AudioFormat format)
	{
		AudioSampleEntry result = audioSampleEntry(lookupFourcc(format), 1, format.getSampleSizeInBits() >> 3, format.getChannels(), format.getSampleRate(), (!format.isBigEndian()) ? ByteOrder.LITTLE_ENDIAN : ByteOrder.BIG_ENDIAN);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 46, 130, 104, 101, 106 })]
	public static void setLabel(TrakBox trakBox, int channel, Label label)
	{
		Label[] labels = getLabelsFromTrack(trakBox);
		labels[channel] = label;
		_setLabels(trakBox, labels);
	}

	[LineNumberTable(new byte[]
	{
		159, 133, 98, 103, 103, 103, 103, 104, 104, 232,
		101, 121, 127, 2, 127, 2, 236, 160, 139, 171,
		113, 113, 113, 113, 113, 113, 113, 241, 160, 87,
		107, 171, 118, 118, 118, 118, 118, 118, 118, 150,
		118, 118, 118, 118, 118, 118, 118, 118, 118, 118,
		118, 118, 118, 118, 118, 118, 118, 118, 118
	})]
	static AudioSampleEntry()
	{
		kAudioFormatFlagIsFloat = 1;
		kAudioFormatFlagIsBigEndian = 2;
		kAudioFormatFlagIsSignedInteger = 4;
		kAudioFormatFlagIsPacked = 8;
		kAudioFormatFlagIsAlignedHigh = 16;
		kAudioFormatFlagIsNonInterleaved = 32;
		kAudioFormatFlagIsNonMixable = 64;
		MONO = Arrays.asList(Label.___003C_003EMono);
		STEREO = Arrays.asList(Label.___003C_003ELeft, Label.___003C_003ERight);
		MATRIX_STEREO = Arrays.asList(Label.___003C_003ELeftTotal, Label.___003C_003ERightTotal);
		___003C_003EEMPTY = new Label[0];
		pcms = new HashSet();
		pcms.add("raw ");
		pcms.add("twos");
		pcms.add("sowt");
		pcms.add("fl32");
		pcms.add("fl64");
		pcms.add("in24");
		pcms.add("in32");
		pcms.add("lpcm");
		translationStereo = new HashMap();
		translationSurround = new HashMap();
		translationStereo.put(Label.___003C_003ELeft, ChannelLabel.___003C_003ESTEREO_LEFT);
		translationStereo.put(Label.___003C_003ERight, ChannelLabel.___003C_003ESTEREO_RIGHT);
		translationStereo.put(Label.___003C_003EHeadphonesLeft, ChannelLabel.___003C_003ESTEREO_LEFT);
		translationStereo.put(Label.___003C_003EHeadphonesRight, ChannelLabel.___003C_003ESTEREO_RIGHT);
		translationStereo.put(Label.___003C_003ELeftTotal, ChannelLabel.___003C_003ESTEREO_LEFT);
		translationStereo.put(Label.___003C_003ERightTotal, ChannelLabel.___003C_003ESTEREO_RIGHT);
		translationStereo.put(Label.___003C_003ELeftWide, ChannelLabel.___003C_003ESTEREO_LEFT);
		translationStereo.put(Label.___003C_003ERightWide, ChannelLabel.___003C_003ESTEREO_RIGHT);
		translationSurround.put(Label.___003C_003ELeft, ChannelLabel.___003C_003EFRONT_LEFT);
		translationSurround.put(Label.___003C_003ERight, ChannelLabel.___003C_003EFRONT_RIGHT);
		translationSurround.put(Label.___003C_003ELeftCenter, ChannelLabel.___003C_003EFRONT_CENTER_LEFT);
		translationSurround.put(Label.___003C_003ERightCenter, ChannelLabel.___003C_003EFRONT_CENTER_RIGHT);
		translationSurround.put(Label.___003C_003ECenter, ChannelLabel.___003C_003ECENTER);
		translationSurround.put(Label.___003C_003ECenterSurround, ChannelLabel.___003C_003EREAR_CENTER);
		translationSurround.put(Label.___003C_003ECenterSurroundDirect, ChannelLabel.___003C_003EREAR_CENTER);
		translationSurround.put(Label.___003C_003ELeftSurround, ChannelLabel.___003C_003EREAR_LEFT);
		translationSurround.put(Label.___003C_003ELeftSurroundDirect, ChannelLabel.___003C_003EREAR_LEFT);
		translationSurround.put(Label.___003C_003ERightSurround, ChannelLabel.___003C_003EREAR_RIGHT);
		translationSurround.put(Label.___003C_003ERightSurroundDirect, ChannelLabel.___003C_003EREAR_RIGHT);
		translationSurround.put(Label.___003C_003ERearSurroundLeft, ChannelLabel.___003C_003ESIDE_LEFT);
		translationSurround.put(Label.___003C_003ERearSurroundRight, ChannelLabel.___003C_003ESIDE_RIGHT);
		translationSurround.put(Label.___003C_003ELFE2, ChannelLabel.___003C_003ELFE);
		translationSurround.put(Label.___003C_003ELFEScreen, ChannelLabel.___003C_003ELFE);
		translationSurround.put(Label.___003C_003ELeftTotal, ChannelLabel.___003C_003ESTEREO_LEFT);
		translationSurround.put(Label.___003C_003ERightTotal, ChannelLabel.___003C_003ESTEREO_RIGHT);
		translationSurround.put(Label.___003C_003ELeftWide, ChannelLabel.___003C_003ESTEREO_LEFT);
		translationSurround.put(Label.___003C_003ERightWide, ChannelLabel.___003C_003ESTEREO_RIGHT);
	}
}
