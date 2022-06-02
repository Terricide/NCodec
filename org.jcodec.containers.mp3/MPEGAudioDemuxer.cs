using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp3;

[Implements(new string[] { "org.jcodec.common.Demuxer", "org.jcodec.common.DemuxerTrack" })]
public class MPEGAudioDemuxer : java.lang.Object, Demuxer, Closeable, AutoCloseable, DemuxerTrack
{
	private const int MAX_FRAME_SIZE = 1728;

	private const int MIN_FRAME_SIZE = 52;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int CHANNELS;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int PADDING;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int SAMPLE_RATE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int BITRATE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int VERSION;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int LAYER;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int SYNC;

	private const int MPEG1 = 3;

	private const int MPEG2 = 2;

	private const int MPEG25 = 0;

	private static int[][][] bitrateTable;

	private static int[] freqTab;

	private static int[] rateReductTab;

	private SeekableByteChannel ch;

	[Signature("Ljava/util/List<Lorg/jcodec/common/DemuxerTrack;>;")]
	private List tracks;

	private int frameNo;

	private ByteBuffer readBuffer;

	private int runningFour;

	private bool eof;

	private DemuxerTrackMeta meta;

	private int sampleRate;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 124, 98, 105, 104, 113, 103, 111, 138, 114,
		110, 141, 167, 108, 110
	})]
	public MPEGAudioDemuxer(SeekableByteChannel ch)
	{
		this.ch = ch;
		readBuffer = ByteBuffer.allocate(262144);
		readMoreData();
		if (readBuffer.remaining() < 4)
		{
			eof = true;
		}
		else
		{
			runningFour = readBuffer.getInt();
			if (!validHeader(runningFour))
			{
				eof = skipJunk();
			}
			extractMeta();
		}
		tracks = new ArrayList();
		tracks.add(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 103, 98, 109, 115, 109 })]
	private void readMoreData()
	{
		readBuffer.clear();
		ch.read(readBuffer);
		readBuffer.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 98, 115, 99, 110, 99, 111, 99, 112,
		99
	})]
	private static bool validHeader(int four)
	{
		if (getField(four, SYNC) != 2047)
		{
			return false;
		}
		if (getField(four, LAYER) == 0)
		{
			return false;
		}
		if (getField(four, SAMPLE_RATE) == 3)
		{
			return false;
		}
		if (getField(four, BITRATE) == 15)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 93, 130, 99, 99, 110, 110, 103, 110, 99,
		131, 111, 127, 1, 138, 127, 0
	})]
	private bool skipJunk()
	{
		int eof = 0;
		int total = 0;
		while (!validHeader(runningFour))
		{
			if (!readBuffer.hasRemaining())
			{
				readMoreData();
			}
			if (!readBuffer.hasRemaining())
			{
				eof = 1;
				break;
			}
			runningFour <<= 8;
			runningFour |= (sbyte)readBuffer.get() & 0xFF;
			total++;
		}
		Logger.warn(java.lang.String.format("[mp3demuxer] Skipped %d bytes of junk", Integer.valueOf(total)));
		return (byte)eof != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 110, 98, 116, 153, 114, 127, 9,
		157, 126, 125
	})]
	private void extractMeta()
	{
		if (validHeader(runningFour))
		{
			int layer = 3 - getField(runningFour, LAYER);
			int channelCount = ((getField(runningFour, CHANNELS) == 3) ? 1 : 2);
			int version = getField(runningFour, VERSION);
			sampleRate = freqTab[getField(runningFour, SAMPLE_RATE)] >> rateReductTab[version];
			AudioCodecMeta codecMeta = AudioCodecMeta.createAudioCodecMeta(".mp3", 16, channelCount, sampleRate, ByteOrder.LITTLE_ENDIAN, pcm: false, null, null);
			Codec codec = layer switch
			{
				2 => Codec.___003C_003EMP3, 
				1 => Codec.___003C_003EMP2, 
				_ => Codec.___003C_003EMP1, 
			};
			meta = new DemuxerTrackMeta(TrackType.___003C_003EAUDIO, codec, 0.0, null, 0, null, null, codecMeta);
		}
	}

	[LineNumberTable(61)]
	private static int getField(int header, int field)
	{
		return (header >> (field & 0xFFFF)) & (field >> 16);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 162, 109, 111, 109, 106, 116, 127, 0,
		110, 110, 149, 151, 184
	})]
	private static int calcFrameSize(int header)
	{
		int bitrateIdx = getField(header, BITRATE);
		int layer = 3 - getField(header, LAYER);
		int version = getField(header, VERSION);
		int mpeg2 = ((version != 3) ? 1 : 0);
		int bitRate = bitrateTable[mpeg2][layer][bitrateIdx] * 1000;
		int sampleRate = freqTab[getField(header, SAMPLE_RATE)] >> rateReductTab[version];
		int padding = getField(header, PADDING);
		int lsf = ((version == 0 || version == 2) ? 1 : 0);
		switch (layer)
		{
		case 0:
		{
			int num4 = bitRate * 12;
			return (((sampleRate != -1) ? (num4 / sampleRate) : (-num4)) + padding) * 4;
		}
		case 1:
		{
			int num3 = bitRate * 144;
			return ((sampleRate != -1) ? (num3 / sampleRate) : (-num3)) + padding;
		}
		default:
		{
			int num = bitRate * 144;
			int num2 = sampleRate << lsf;
			return ((num2 != -1) ? (num / num2) : (-num)) + padding;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 97, 162, 99, 105, 114, 111, 110, 103, 110,
		159, 3, 136
	})]
	private bool readFrame(ByteBuffer frame)
	{
		int eof = 0;
		while (frame.hasRemaining())
		{
			frame.put((byte)(sbyte)(runningFour >> 24));
			runningFour <<= 8;
			if (!readBuffer.hasRemaining())
			{
				readMoreData();
			}
			if (readBuffer.hasRemaining())
			{
				runningFour |= (sbyte)readBuffer.get() & 0xFF;
			}
			else
			{
				eof = 1;
			}
		}
		return (byte)eof != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 162, 113, 102, 148 })]
	private static int skipJunkBB(int header, ByteBuffer fork)
	{
		while (!validHeader(header) && fork.hasRemaining())
		{
			header <<= 8;
			header |= (sbyte)fork.get() & 0xFF;
		}
		return header;
	}

	[LineNumberTable(33)]
	private static int field(int off, int size)
	{
		return ((1 << size) - 1 << 16) | off;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 116, 162, 110 })]
	public virtual void close()
	{
		ch.close();
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(112)]
	public virtual List getTracks()
	{
		return tracks;
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(117)]
	public virtual List getVideoTracks()
	{
		return null;
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(122)]
	public virtual List getAudioTracks()
	{
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 111, 162, 105, 99, 110, 141, 109, 104, 110,
		136, 159, 21, 143
	})]
	public virtual Packet nextFrame()
	{
		if (eof)
		{
			return null;
		}
		if (!validHeader(runningFour))
		{
			eof = skipJunk();
		}
		int frameSize = calcFrameSize(runningFour);
		ByteBuffer frame = ByteBuffer.allocate(frameSize);
		eof = readFrame(frame);
		frame.flip();
		Packet.___003Cclinit_003E();
		Packet pkt = new Packet(frame, frameNo * 1152, sampleRate, 1152L, frameNo, Packet.FrameType.___003C_003EKEY, null, 0);
		frameNo++;
		return pkt;
	}

	[LineNumberTable(217)]
	public virtual DemuxerTrackMeta getMeta()
	{
		return meta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 85, 98, 136, 101, 136, 105, 105, 105, 107,
		102, 101, 102, 142, 105, 106, 104, 120, 133, 141
	})]
	public static int probe(ByteBuffer b)
	{
		ByteBuffer fork = b.duplicate();
		int valid = 0;
		int total = 0;
		int header = fork.getInt();
		do
		{
			if (!validHeader(header))
			{
				header = skipJunkBB(header, fork);
			}
			int size = calcFrameSize(header);
			if (fork.remaining() < size)
			{
				break;
			}
			total++;
			if (size > 0)
			{
				NIOUtils.skip(fork, size - 4);
			}
			else
			{
				header = skipJunkBB(header, fork);
			}
			if (fork.remaining() >= 4)
			{
				header = fork.getInt();
				if (size >= 52 && size <= 1728 && validHeader(header))
				{
					valid++;
				}
			}
		}
		while (fork.remaining() >= 4);
		int num = 100 * valid;
		int num2 = total;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[LineNumberTable(new byte[]
	{
		159,
		133,
		130,
		109,
		110,
		110,
		110,
		110,
		110,
		239,
		70,
		byte.MaxValue,
		162,
		72,
		71,
		127,
		5
	})]
	static MPEGAudioDemuxer()
	{
		CHANNELS = field(6, 2);
		PADDING = field(9, 1);
		SAMPLE_RATE = field(10, 2);
		BITRATE = field(12, 4);
		VERSION = field(19, 2);
		LAYER = field(17, 2);
		SYNC = field(21, 11);
		bitrateTable = new int[2][][]
		{
			new int[3][]
			{
				new int[15]
				{
					0, 32, 64, 96, 128, 160, 192, 224, 256, 288,
					320, 352, 384, 416, 448
				},
				new int[15]
				{
					0, 32, 48, 56, 64, 80, 96, 112, 128, 160,
					192, 224, 256, 320, 384
				},
				new int[15]
				{
					0, 32, 40, 48, 56, 64, 80, 96, 112, 128,
					160, 192, 224, 256, 320
				}
			},
			new int[3][]
			{
				new int[15]
				{
					0, 32, 48, 56, 64, 80, 96, 112, 128, 144,
					160, 176, 192, 224, 256
				},
				new int[15]
				{
					0, 8, 16, 24, 32, 40, 48, 56, 64, 80,
					96, 112, 128, 144, 160
				},
				new int[15]
				{
					0, 8, 16, 24, 32, 40, 48, 56, 64, 80,
					96, 112, 128, 144, 160
				}
			}
		};
		freqTab = new int[3] { 44100, 48000, 32000 };
		rateReductTab = new int[4] { 2, 0, 1, 0 };
	}

	public void Dispose()
	{
		close();
	}
}
