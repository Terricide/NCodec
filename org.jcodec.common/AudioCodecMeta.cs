using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.common;

public class AudioCodecMeta : CodecMeta
{
	private int sampleSize;

	private int channelCount;

	private int sampleRate;

	private ByteOrder endian;

	private int samplesPerPacket;

	private int bytesPerPacket;

	private int bytesPerFrame;

	private bool pcm;

	private Label[] labels;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 66, 105, 111, 109, 109, 123, 104 })]
	public static AudioCodecMeta fromAudioFormat(AudioFormat format)
	{
		AudioCodecMeta self = new AudioCodecMeta(null, null);
		self.sampleSize = format.getSampleSizeInBits() >> 3;
		self.channelCount = format.getChannels();
		self.sampleRate = format.getSampleRate();
		self.endian = ((!format.isBigEndian()) ? ByteOrder.LITTLE_ENDIAN : ByteOrder.BIG_ENDIAN);
		self.pcm = false;
		return self;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	public virtual AudioFormat getFormat()
	{
		AudioFormat.___003Cclinit_003E();
		AudioFormat result = new AudioFormat(sampleRate, sampleSize << 3, channelCount, signed: true, endian == ByteOrder.BIG_ENDIAN);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 130, 107 })]
	public AudioCodecMeta(string fourcc, ByteBuffer codecPrivate)
		: base(fourcc, codecPrivate)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 161, 68, 106, 104, 104, 104, 105, 104,
		105
	})]
	public static AudioCodecMeta createAudioCodecMeta(string fourcc, int sampleSize, int channelCount, int sampleRate, ByteOrder endian, bool pcm, Label[] labels, ByteBuffer codecPrivate)
	{
		AudioCodecMeta self = new AudioCodecMeta(fourcc, codecPrivate);
		self.sampleSize = sampleSize;
		self.channelCount = channelCount;
		self.sampleRate = sampleRate;
		self.endian = endian;
		self.pcm = pcm;
		self.labels = labels;
		return self;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 65, 68, 106, 104, 104, 104, 105, 105,
		105, 105, 104, 105
	})]
	public static AudioCodecMeta createAudioCodecMeta2(string fourcc, int sampleSize, int channelCount, int sampleRate, ByteOrder endian, bool pcm, Label[] labels, int samplesPerPacket, int bytesPerPacket, int bytesPerFrame, ByteBuffer codecPrivate)
	{
		AudioCodecMeta self = new AudioCodecMeta(fourcc, codecPrivate);
		self.sampleSize = sampleSize;
		self.channelCount = channelCount;
		self.sampleRate = sampleRate;
		self.endian = endian;
		self.samplesPerPacket = samplesPerPacket;
		self.bytesPerPacket = bytesPerPacket;
		self.bytesPerFrame = bytesPerFrame;
		self.pcm = pcm;
		self.labels = labels;
		return self;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 161, 67, 105, 111, 109, 109, 123, 104,
		105
	})]
	public static AudioCodecMeta createAudioCodecMeta3(string fourcc, ByteBuffer codecPrivate, AudioFormat format, bool pcm, Label[] labels)
	{
		AudioCodecMeta self = new AudioCodecMeta(fourcc, codecPrivate);
		self.sampleSize = format.getSampleSizeInBits() >> 3;
		self.channelCount = format.getChannels();
		self.sampleRate = format.getSampleRate();
		self.endian = ((!format.isBigEndian()) ? ByteOrder.LITTLE_ENDIAN : ByteOrder.BIG_ENDIAN);
		self.pcm = pcm;
		self.labels = labels;
		return self;
	}

	[LineNumberTable(72)]
	public virtual int getFrameSize()
	{
		return sampleSize * channelCount;
	}

	[LineNumberTable(76)]
	public virtual int getSampleRate()
	{
		return sampleRate;
	}

	[LineNumberTable(80)]
	public virtual int getSampleSize()
	{
		return sampleSize;
	}

	[LineNumberTable(84)]
	public virtual int getChannelCount()
	{
		return channelCount;
	}

	[LineNumberTable(88)]
	public virtual int getSamplesPerPacket()
	{
		return samplesPerPacket;
	}

	[LineNumberTable(92)]
	public virtual int getBytesPerPacket()
	{
		return bytesPerPacket;
	}

	[LineNumberTable(96)]
	public virtual int getBytesPerFrame()
	{
		return bytesPerFrame;
	}

	[LineNumberTable(100)]
	public virtual ByteOrder getEndian()
	{
		return endian;
	}

	[LineNumberTable(104)]
	public virtual bool isPCM()
	{
		return pcm;
	}

	[LineNumberTable(112)]
	public virtual Label[] getChannelLabels()
	{
		return labels;
	}
}
