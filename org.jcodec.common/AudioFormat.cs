using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common;

public class AudioFormat : Object
{
	private int sampleRate;

	private int sampleSizeInBits;

	private int channelCount;

	private bool signed;

	private bool bigEndian;

	public static AudioFormat STEREO_48K_S16_BE;

	public static AudioFormat STEREO_48K_S16_LE;

	public static AudioFormat STEREO_48K_S24_BE;

	public static AudioFormat STEREO_48K_S24_LE;

	public static AudioFormat MONO_48K_S16_BE;

	public static AudioFormat MONO_48K_S16_LE;

	public static AudioFormat MONO_48K_S24_BE;

	public static AudioFormat MONO_48K_S24_LE;

	public static AudioFormat STEREO_44K_S16_BE;

	public static AudioFormat STEREO_44K_S16_LE;

	public static AudioFormat STEREO_44K_S24_BE;

	public static AudioFormat STEREO_44K_S24_LE;

	public static AudioFormat MONO_44K_S16_BE;

	public static AudioFormat MONO_44K_S16_LE;

	public static AudioFormat MONO_44K_S24_BE;

	public static AudioFormat MONO_44K_S24_LE;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(126)]
	public virtual int getChannels()
	{
		return channelCount;
	}

	[LineNumberTable(138)]
	public virtual short getFrameSize()
	{
		return (short)((sampleSizeInBits >> 3) * channelCount);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 97, 71, 105, 104, 104, 104, 104, 104 })]
	public AudioFormat(int sampleRate, int sampleSizeInBits, int channelCount, bool signed, bool bigEndian)
	{
		this.sampleRate = sampleRate;
		this.sampleSizeInBits = sampleSizeInBits;
		this.channelCount = channelCount;
		this.signed = signed;
		this.bigEndian = bigEndian;
	}

	[LineNumberTable(142)]
	public virtual int getFrameRate()
	{
		return sampleRate;
	}

	[LineNumberTable(134)]
	public virtual int getSampleRate()
	{
		return sampleRate;
	}

	[LineNumberTable(130)]
	public virtual int getSampleSizeInBits()
	{
		return sampleSizeInBits;
	}

	[LineNumberTable(166)]
	public virtual int samplesToBytes(int samples)
	{
		return samples * (sampleSizeInBits >> 3);
	}

	[LineNumberTable(154)]
	public virtual int bytesToFrames(int bytes)
	{
		int num = channelCount * sampleSizeInBits >> 3;
		return (num != -1) ? (bytes / num) : (-bytes);
	}

	[LineNumberTable(158)]
	public virtual int framesToBytes(int samples)
	{
		return samples * (channelCount * sampleSizeInBits >> 3);
	}

	[LineNumberTable(146)]
	public virtual bool isBigEndian()
	{
		return bigEndian;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(44)]
	public static AudioFormat STEREO_S16_BE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 16, 2, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(48)]
	public static AudioFormat STEREO_S16_LE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 16, 2, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(52)]
	public static AudioFormat STEREO_S24_BE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 24, 2, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(56)]
	public static AudioFormat STEREO_S24_LE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 24, 2, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(60)]
	public static AudioFormat MONO_S16_BE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 16, 1, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(64)]
	public static AudioFormat MONO_S16_LE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 16, 1, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(68)]
	public static AudioFormat MONO_S24_BE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 24, 1, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(72)]
	public static AudioFormat MONO_S24_LE(int rate)
	{
		AudioFormat result = new AudioFormat(rate, 24, 1, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(76)]
	public static AudioFormat NCH_48K_S16_BE(int n)
	{
		AudioFormat result = new AudioFormat(48000, 16, n, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(80)]
	public static AudioFormat NCH_48K_S16_LE(int n)
	{
		AudioFormat result = new AudioFormat(48000, 16, n, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(84)]
	public static AudioFormat NCH_48K_S24_BE(int n)
	{
		AudioFormat result = new AudioFormat(48000, 24, n, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(88)]
	public static AudioFormat NCH_48K_S24_LE(int n)
	{
		AudioFormat result = new AudioFormat(48000, 24, n, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(92)]
	public static AudioFormat NCH_44K_S16_BE(int n)
	{
		AudioFormat result = new AudioFormat(44100, 16, n, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(96)]
	public static AudioFormat NCH_44K_S16_LE(int n)
	{
		AudioFormat result = new AudioFormat(44100, 16, n, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(100)]
	public static AudioFormat NCH_44K_S24_BE(int n)
	{
		AudioFormat result = new AudioFormat(44100, 24, n, signed: true, bigEndian: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(104)]
	public static AudioFormat NCH_44K_S24_LE(int n)
	{
		AudioFormat result = new AudioFormat(44100, 24, n, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	public static AudioFormat createAudioFormat(AudioFormat format)
	{
		AudioFormat result = new AudioFormat(format.sampleRate, format.sampleSizeInBits, format.channelCount, format.signed, format.bigEndian);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 66, 127, 6, 104 })]
	public static AudioFormat createAudioFormat2(AudioFormat format, int newSampleRate)
	{
		AudioFormat af = new AudioFormat(format.sampleRate, format.sampleSizeInBits, format.channelCount, format.signed, format.bigEndian);
		af.sampleRate = newSampleRate;
		return af;
	}

	[LineNumberTable(150)]
	public virtual bool isSigned()
	{
		return signed;
	}

	[LineNumberTable(162)]
	public virtual int bytesToSamples(int bytes)
	{
		int num = sampleSizeInBits >> 3;
		return (num != -1) ? (bytes / num) : (-bytes);
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 162, 117, 149, 117, 149, 117, 149, 117,
		149, 117, 149, 117, 149, 117, 149, 117
	})]
	static AudioFormat()
	{
		STEREO_48K_S16_BE = new AudioFormat(48000, 16, 2, signed: true, bigEndian: true);
		STEREO_48K_S16_LE = new AudioFormat(48000, 16, 2, signed: true, bigEndian: false);
		STEREO_48K_S24_BE = new AudioFormat(48000, 24, 2, signed: true, bigEndian: true);
		STEREO_48K_S24_LE = new AudioFormat(48000, 24, 2, signed: true, bigEndian: false);
		MONO_48K_S16_BE = new AudioFormat(48000, 16, 1, signed: true, bigEndian: true);
		MONO_48K_S16_LE = new AudioFormat(48000, 16, 1, signed: true, bigEndian: false);
		MONO_48K_S24_BE = new AudioFormat(48000, 24, 1, signed: true, bigEndian: true);
		MONO_48K_S24_LE = new AudioFormat(48000, 24, 1, signed: true, bigEndian: false);
		STEREO_44K_S16_BE = new AudioFormat(44100, 16, 2, signed: true, bigEndian: true);
		STEREO_44K_S16_LE = new AudioFormat(44100, 16, 2, signed: true, bigEndian: false);
		STEREO_44K_S24_BE = new AudioFormat(44100, 24, 2, signed: true, bigEndian: true);
		STEREO_44K_S24_LE = new AudioFormat(44100, 24, 2, signed: true, bigEndian: false);
		MONO_44K_S16_BE = new AudioFormat(44100, 16, 1, signed: true, bigEndian: true);
		MONO_44K_S16_LE = new AudioFormat(44100, 16, 1, signed: true, bigEndian: false);
		MONO_44K_S24_BE = new AudioFormat(44100, 24, 1, signed: true, bigEndian: true);
		MONO_44K_S24_LE = new AudioFormat(44100, 24, 1, signed: true, bigEndian: false);
	}
}
