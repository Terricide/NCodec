using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.api;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.wav;

public class WavHeader : Object
{
	public class FmtChunk : Object
	{
		public short audioFormat;

		public short numChannels;

		public int sampleRate;

		public int byteRate;

		public short blockAlign;

		public short bitsPerSample;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 119, 161, 75, 106, 104, 104, 104, 105, 104,
			104
		})]
		public FmtChunk(short audioFormat, short numChannels, int sampleRate, int byteRate, short blockAlign, short bitsPerSample)
		{
			this.audioFormat = audioFormat;
			this.numChannels = numChannels;
			this.sampleRate = sampleRate;
			this.byteRate = byteRate;
			this.blockAlign = blockAlign;
			this.bitsPerSample = bitsPerSample;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 116, 98, 136, 109, 127, 1, 144, 76, 227,
			61
		})]
		public static FmtChunk get(ByteBuffer bb)
		{
			ByteOrder old = bb.order();
			try
			{
				bb.order(ByteOrder.LITTLE_ENDIAN);
				return new FmtChunk(bb.getShort(), bb.getShort(), bb.getInt(), bb.getInt(), bb.getShort(), bb.getShort());
			}
			finally
			{
				bb.order(old);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 113, 66, 104, 109, 110, 110, 110, 110, 110,
			110, 105
		})]
		public virtual void put(ByteBuffer bb)
		{
			ByteOrder old = bb.order();
			bb.order(ByteOrder.LITTLE_ENDIAN);
			bb.putShort(audioFormat);
			bb.putShort(numChannels);
			bb.putInt(sampleRate);
			bb.putInt(byteRate);
			bb.putShort(blockAlign);
			bb.putShort(bitsPerSample);
			bb.order(old);
		}

		[LineNumberTable(128)]
		public virtual int size()
		{
			return 16;
		}
	}

	public class FmtChunkExtended : FmtChunk
	{
		internal short cbSize;

		internal short bitsPerCodedSample;

		internal int channelLayout;

		internal int guid;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 134, 161, 69, 159, 14, 104, 104, 105, 105 })]
		public FmtChunkExtended(FmtChunk other, short cbSize, short bitsPerCodedSample, int channelLayout, int guid)
			: base(other.audioFormat, other.numChannels, other.sampleRate, other.byteRate, other.blockAlign, other.bitsPerSample)
		{
			this.cbSize = cbSize;
			this.bitsPerCodedSample = bitsPerCodedSample;
			this.channelLayout = channelLayout;
			this.guid = guid;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 130, 103, 108, 112, 15, 199 })]
		public virtual ChannelLabel[] getLabels()
		{
			ArrayList labels = new ArrayList();
			for (int i = 0; i < (nint)mapping.LongLength; i++)
			{
				if ((channelLayout & (1 << i)) != 0)
				{
					((List)labels).add((object)mapping[i]);
				}
			}
			return (ChannelLabel[])((List)labels).toArray((object[])new ChannelLabel[0]);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 131, 66, 104, 136, 109, 159, 5, 76, 3 })]
		public static FmtChunk read(ByteBuffer bb)
		{
			FmtChunk fmtChunk = FmtChunk.get(bb);
			ByteOrder old = bb.order();
			try
			{
				bb.order(ByteOrder.LITTLE_ENDIAN);
				return new FmtChunkExtended(fmtChunk, bb.getShort(), bb.getShort(), bb.getInt(), bb.getInt());
			}
			finally
			{
				bb.order(old);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 129, 162, 104, 104, 109, 110, 110, 110, 110,
			105
		})]
		public override void put(ByteBuffer bb)
		{
			base.put(bb);
			ByteOrder old = bb.order();
			bb.order(ByteOrder.LITTLE_ENDIAN);
			bb.putShort(cbSize);
			bb.putShort(bitsPerCodedSample);
			bb.putInt(channelLayout);
			bb.putInt(guid);
			bb.order(old);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(66)]
		public override int size()
		{
			return base.size() + 12;
		}
	}

	internal static ChannelLabel[] mapping;

	public string chunkId;

	public int chunkSize;

	public string format;

	public FmtChunk fmt;

	public int dataOffset;

	public long dataSize;

	public const int WAV_HEADER_SIZE = 44;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 87, 130, 108, 109, 105, 106, 113, 136, 105,
		104, 105, 132, 123, 163, 132, 106, 105, 127, 8,
		159, 7, 105, 134, 105, 105, 134, 105, 106, 131,
		105, 131, 159, 8, 111, 138, 146
	})]
	public static WavHeader readChannel(ReadableByteChannel _in)
	{
		ByteBuffer buf = ByteBuffer.allocate(128);
		buf.order(ByteOrder.LITTLE_ENDIAN);
		_in.read(buf);
		if (buf.remaining() > 0)
		{
			
			throw new IOException("Incomplete wav header found");
		}
		buf.flip();
		string chunkId = NIOUtils.readString(buf, 4);
		int chunkSize = buf.getInt();
		string format = NIOUtils.readString(buf, 4);
		FmtChunk fmt = null;
		if (!String.instancehelper_equals("RIFF", chunkId) || !String.instancehelper_equals("WAVE", format))
		{
			return null;
		}
		int size = 0;
		string fourcc;
		do
		{
			fourcc = NIOUtils.readString(buf, 4);
			size = buf.getInt();
			if (String.instancehelper_equals("fmt ", fourcc) && size >= 14 && size <= 1048576)
			{
				switch (size)
				{
				case 16:
					fmt = FmtChunk.get(buf);
					break;
				case 18:
					fmt = FmtChunk.get(buf);
					NIOUtils.skip(buf, 2);
					break;
				case 40:
					fmt = FmtChunk.get(buf);
					NIOUtils.skip(buf, 12);
					break;
				case 28:
					fmt = FmtChunk.get(buf);
					break;
				default:
				{
					string @string = new StringBuilder().append("Don't know how to handle fmt size: ").append(size).toString();
					
					throw new UnhandledStateException(@string);
				}
				}
			}
			else if (!String.instancehelper_equals("data", fourcc))
			{
				NIOUtils.skip(buf, size);
			}
		}
		while (!String.instancehelper_equals("data", fourcc));
		WavHeader result = new WavHeader(chunkId, chunkSize, format, fmt, buf.position(), size);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(381)]
	public virtual AudioFormat getFormat()
	{
		AudioFormat.___003Cclinit_003E();
		AudioFormat result = new AudioFormat(fmt.sampleRate, fmt.bitsPerSample, fmt.numChannels, signed: true, bigEndian: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 66, 105, 104, 127, 2, 99, 191, 13 })]
	private static FmtChunk copyFmt(FmtChunk fmt)
	{
		if (fmt is FmtChunkExtended)
		{
			FmtChunkExtended fmtext = (FmtChunkExtended)fmt;
			fmt = new FmtChunkExtended(fmtext, fmtext.cbSize, fmtext.bitsPerCodedSample, fmtext.channelLayout, fmtext.guid);
		}
		else
		{
			fmt = new FmtChunk(fmt.audioFormat, fmt.numChannels, fmt.sampleRate, fmt.byteRate, fmt.blockAlign, fmt.bitsPerSample);
		}
		return fmt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 66, 105, 104, 104, 104, 105, 105, 105 })]
	public WavHeader(string chunkId, int chunkSize, string format, FmtChunk fmt, int dataOffset, long dataSize)
	{
		this.chunkId = chunkId;
		this.chunkSize = chunkSize;
		this.format = format;
		this.fmt = fmt;
		this.dataOffset = dataOffset;
		this.dataSize = dataSize;
	}

	[LineNumberTable(327)]
	public static long calcDataSize(int numChannels, int bytesPerSample, long samples)
	{
		return samples * numChannels * bytesPerSample;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 130, 127, 7, 45 })]
	public static WavHeader stereo48kWithSamples(long samples)
	{
		WavHeader result = new WavHeader("RIFF", 40, "WAVE", new FmtChunk(1, 2, 48000, 192000, 4, 16), 44, calcDataSize(2, 2, samples));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(208)]
	private static FmtChunk newFmtChunk()
	{
		FmtChunk result = new FmtChunk(1, 0, 0, 0, 0, 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 89, 66, 131, 104, 140, 74, 3 })]
	public static WavHeader read(File file)
	{
		FileChannelWrapper @is = null;
		try
		{
			@is = NIOUtils.readableChannel(file);
			return readChannel(@is);
		}
		finally
		{
			IOUtils.closeQuietly(@is);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 73, 162, 103, 99, 104, 101, 12, 199, 105,
		107, 106, 103, 106, 111, 114, 116, 111, 110
	})]
	public static WavHeader multiChannelWav(WavHeader[] headers)
	{
		WavHeader w = emptyWavHeader();
		int totalSize = 0;
		for (int i = 0; i < (nint)headers.LongLength; i++)
		{
			WavHeader wavHeader = headers[i];
			totalSize = (int)(totalSize + wavHeader.dataSize);
		}
		w.dataSize = totalSize;
		FmtChunk fmt = headers[0].fmt;
		int bitsPerSample = fmt.bitsPerSample;
		int bytesPerSample = bitsPerSample / 8;
		int sampleRate = fmt.sampleRate;
		w.fmt.bitsPerSample = (short)bitsPerSample;
		w.fmt.blockAlign = (short)((nint)headers.LongLength * bytesPerSample);
		w.fmt.byteRate = (int)((nint)headers.LongLength * bytesPerSample * sampleRate);
		w.fmt.numChannels = (short)headers.Length;
		w.fmt.sampleRate = sampleRate;
		return w;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(204)]
	public static WavHeader emptyWavHeader()
	{
		WavHeader result = new WavHeader("RIFF", 40, "WAVE", newFmtChunk(), 44, 0L);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 130, 121, 120, 109 })]
	public static WavHeader copyWithRate(WavHeader header, int rate)
	{
		WavHeader result = new WavHeader(header.chunkId, header.chunkSize, header.format, copyFmt(header.fmt), header.dataOffset, header.dataSize);
		result.fmt.sampleRate = rate;
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 98, 121, 120, 110 })]
	public static WavHeader copyWithChannels(WavHeader header, int channels)
	{
		WavHeader result = new WavHeader(header.chunkId, header.chunkSize, header.format, copyFmt(header.fmt), header.dataOffset, header.dataSize);
		result.fmt.numChannels = (short)channels;
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 98, 124, 119, 113, 117, 42, 140 })]
	public static WavHeader createWavHeader(AudioFormat format, int samples)
	{
		return new WavHeader("RIFF", 40, "WAVE", new FmtChunk(1, (short)format.getChannels(), format.getSampleRate(), format.getSampleRate() * format.getChannels() * (format.getSampleSizeInBits() >> 3), (short)(format.getChannels() * (format.getSampleSizeInBits() >> 3)), (short)format.getSampleSizeInBits()), 44, calcDataSize(format.getChannels(), format.getSampleSizeInBits() >> 3, samples));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(190)]
	public static WavHeader stereo48k()
	{
		WavHeader result = stereo48kWithSamples(0L);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 93, 162, 127, 7, 45 })]
	public static WavHeader mono48k(long samples)
	{
		WavHeader result = new WavHeader("RIFF", 40, "WAVE", new FmtChunk(1, 1, 48000, 96000, 2, 16), 44, calcDataSize(1, 2, samples));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 75, 130, 105, 104, 44, 167 })]
	public static WavHeader multiChannelWavFromFiles(File[] files)
	{
		WavHeader[] headers = new WavHeader[(nint)files.LongLength];
		for (int i = 0; i < (nint)files.LongLength; i++)
		{
			headers[i] = read(files[i]);
		}
		WavHeader result = multiChannelWav(headers);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 68, 162, 105, 173, 107, 142, 165, 114, 106,
		146, 114, 115, 109, 114, 107, 145, 137, 104, 105
	})]
	public virtual void write(WritableByteChannel @out)
	{
		ByteBuffer bb = ByteBuffer.allocate(44);
		bb.order(ByteOrder.LITTLE_ENDIAN);
		long chunkSize = ((dataSize > uint.MaxValue) ? 40u : (dataSize + 36u));
		bb.put(JCodecUtil2.asciiString("RIFF"));
		bb.putInt((int)chunkSize);
		bb.put(JCodecUtil2.asciiString("WAVE"));
		bb.put(JCodecUtil2.asciiString("fmt "));
		bb.putInt(fmt.size());
		fmt.put(bb);
		bb.put(JCodecUtil2.asciiString("data"));
		if (dataSize <= uint.MaxValue)
		{
			bb.putInt((int)dataSize);
		}
		else
		{
			bb.putInt(0);
		}
		bb.flip();
		@out.write(bb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 60, 162, 103, 105, 103, 104, 101, 105, 110,
		114, 121, 115, 114
	})]
	public static WavHeader create(AudioFormat af, int size)
	{
		WavHeader w = emptyWavHeader();
		w.dataSize = size;
		FmtChunk fmt = newFmtChunk();
		int bitsPerSample = af.getSampleSizeInBits();
		int bytesPerSample = bitsPerSample / 8;
		int sampleRate = af.getSampleRate();
		w.fmt.bitsPerSample = (short)bitsPerSample;
		w.fmt.blockAlign = af.getFrameSize();
		w.fmt.byteRate = af.getFrameRate() * af.getFrameSize();
		w.fmt.numChannels = (short)af.getChannels();
		w.fmt.sampleRate = af.getSampleRate();
		return w;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 56, 130, 110, 148, 159, 25, 144, 152, 159,
		1, 191, 9, 191, 17, 191, 25, 191, 33, 223,
		41, 114, 108
	})]
	public virtual ChannelLabel[] getChannelLabels()
	{
		if (fmt is FmtChunkExtended)
		{
			ChannelLabel[] labels2 = ((FmtChunkExtended)fmt).getLabels();
			
			return labels2;
		}
		switch (fmt.numChannels)
		{
		case 1:
			return new ChannelLabel[1] { ChannelLabel.___003C_003EMONO };
		case 2:
			return new ChannelLabel[2]
			{
				ChannelLabel.___003C_003ESTEREO_LEFT,
				ChannelLabel.___003C_003ESTEREO_RIGHT
			};
		case 3:
			return new ChannelLabel[3]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_CENTER
			};
		case 4:
			return new ChannelLabel[4]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT
			};
		case 5:
			return new ChannelLabel[5]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT
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
		case 7:
			return new ChannelLabel[7]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003ELFE,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT,
				ChannelLabel.___003C_003EREAR_CENTER
			};
		case 8:
			return new ChannelLabel[8]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003ELFE,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT
			};
		default:
		{
			ChannelLabel[] labels = new ChannelLabel[fmt.numChannels];
			Arrays.fill(labels, ChannelLabel.___003C_003EMONO);
			return labels;
		}
		}
	}

	[LineNumberTable(79)]
	static WavHeader()
	{
		mapping = new ChannelLabel[20]
		{
			ChannelLabel.___003C_003EFRONT_LEFT,
			ChannelLabel.___003C_003EFRONT_RIGHT,
			ChannelLabel.___003C_003ECENTER,
			ChannelLabel.___003C_003ELFE,
			ChannelLabel.___003C_003EREAR_LEFT,
			ChannelLabel.___003C_003EREAR_RIGHT,
			ChannelLabel.___003C_003EFRONT_CENTER_LEFT,
			ChannelLabel.___003C_003EFRONT_CENTER_RIGHT,
			ChannelLabel.___003C_003EREAR_CENTER,
			ChannelLabel.___003C_003ESIDE_LEFT,
			ChannelLabel.___003C_003ESIDE_RIGHT,
			ChannelLabel.___003C_003ECENTER,
			ChannelLabel.___003C_003EFRONT_LEFT,
			ChannelLabel.___003C_003ECENTER,
			ChannelLabel.___003C_003EFRONT_RIGHT,
			ChannelLabel.___003C_003EREAR_LEFT,
			ChannelLabel.___003C_003EREAR_CENTER,
			ChannelLabel.___003C_003EREAR_RIGHT,
			ChannelLabel.___003C_003ESTEREO_LEFT,
			ChannelLabel.___003C_003ESTEREO_RIGHT
		};
	}
}
