using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac;

public class SampleBuffer : Object
{
	private int sampleRate;

	private int channels;

	private int bitsPerSample;

	private double length;

	private double bitrate;

	private double encodedBitrate;

	private byte[] data;

	private bool bigEndian;

	[LineNumberTable(new byte[]
	{
		159, 114, 130, 104, 104, 104, 137, 100, 109, 109,
		175, 102, 112, 109, 118, 146
	})]
	public virtual void setData(byte[] data, int sampleRate, int channels, int bitsPerSample, int bitsRead)
	{
		this.data = data;
		this.sampleRate = sampleRate;
		this.channels = channels;
		this.bitsPerSample = bitsPerSample;
		if (sampleRate == 0)
		{
			length = 0.0;
			bitrate = 0.0;
			encodedBitrate = 0.0;
			return;
		}
		int bytesPerSample = bitsPerSample / 8;
		nint num = (nint)data.LongLength;
		int num2 = bytesPerSample * channels;
		int samplesPerChannel = (int)((num2 != -1) ? (num / num2) : (-num));
		length = (double)samplesPerChannel / (double)sampleRate;
		bitrate = (double)(samplesPerChannel * bitsPerSample * channels) / length;
		encodedBitrate = (double)bitsRead / length;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 109, 104, 104, 104, 104 })]
	public SampleBuffer()
	{
		data = new byte[0];
		sampleRate = 0;
		channels = 0;
		bitsPerSample = 0;
		bigEndian = true;
	}

	[LineNumberTable(32)]
	public virtual byte[] getData()
	{
		return data;
	}

	[LineNumberTable(40)]
	public virtual int getSampleRate()
	{
		return sampleRate;
	}

	[LineNumberTable(48)]
	public virtual int getChannels()
	{
		return channels;
	}

	[LineNumberTable(57)]
	public virtual int getBitsPerSample()
	{
		return bitsPerSample;
	}

	[LineNumberTable(66)]
	public virtual double getLength()
	{
		return length;
	}

	[LineNumberTable(75)]
	public virtual double getBitrate()
	{
		return bitrate;
	}

	[LineNumberTable(83)]
	public virtual double getEncodedBitrate()
	{
		return encodedBitrate;
	}

	[LineNumberTable(92)]
	public virtual bool isBigEndian()
	{
		return bigEndian;
	}

	[LineNumberTable(new byte[]
	{
		159, 117, 129, 67, 138, 109, 106, 115, 236, 61,
		231, 69, 136
	})]
	public virtual void setBigEndian(bool bigEndian)
	{
		if (bigEndian != this.bigEndian)
		{
			for (int i = 0; i < (nint)data.LongLength; i += 2)
			{
				int tmp = data[i];
				data[i] = data[i + 1];
				data[i + 1] = (byte)tmp;
			}
			this.bigEndian = bigEndian;
		}
	}
}
