using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.containers.flv;

public class FLVReader : java.lang.Object
{
	private const int REPOSITION_BUFFER_READS = 10;

	private const int TAG_HEADER_SIZE = 15;

	private const int READ_BUFFER_SIZE = 1024;

	private int frameNo;

	private ByteBuffer readBuf;

	private org.jcodec.common.io.SeekableByteChannel ch;

	private bool eof;

	private static bool platformBigEndian;

	public static Codec[] audioCodecMapping;

	public static Codec[] videoCodecMapping;

	public static int[] sampleRates;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 123, 66, 109, 112, 104, 109 })]
	private void initialRead(ReadableByteChannel ch)
	{
		readBuf.clear();
		if (ch.read(readBuf) == -1)
		{
			eof = true;
		}
		readBuf.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 162, 127, 14, 127, 0, 131 })]
	public static bool readHeader(ByteBuffer readBuf)
	{
		if (readBuf.remaining() < 9 || (sbyte)readBuf.get() != 70 || (sbyte)readBuf.get() != 76 || (sbyte)readBuf.get() != 86 || (sbyte)readBuf.get() != 1 || ((sbyte)readBuf.get() & 5) == 0 || readBuf.getInt() != 9)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 45, 98, 99, 107, 113, 126, 113, 127, 7,
		108, 122, 131, 102, 109, 110, 227, 52, 234, 78
	})]
	public virtual bool repositionFile()
	{
		int payloadSize = 0;
		for (int i = 0; i < 10; i++)
		{
			while (readBuf.hasRemaining())
			{
				payloadSize = ((payloadSize & 0xFFFF) << 8) | ((sbyte)readBuf.get() & 0xFF);
				int pointerPos = readBuf.position() + 7 + payloadSize;
				if (readBuf.position() >= 8 && pointerPos < readBuf.limit() - 4 && readBuf.getInt(pointerPos) - payloadSize == 11)
				{
					readBuf.position(readBuf.position() - 8);
					return true;
				}
			}
			initialRead(ch);
			if (!readBuf.hasRemaining())
			{
				break;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 104, 98, 107, 163, 104, 117, 104, 111, 127,
		0, 126, 115, 191, 0, 112, 114, 109, 113, 105,
		127, 0, 120, 104, 131, 127, 4, 40, 145, 198,
		107, 105, 163, 111, 106, 166, 208, 101, 104, 113,
		102, 104, 113, 102, 104, 134, 112, 134, 100, 110,
		106, 144, 147
	})]
	public virtual FLVTag parsePacket(ByteBuffer readBuf)
	{
		long packetPos;
		int prevPacketSize;
		int packetType;
		int timestamp;
		int streamId;
		ByteBuffer payload;
		FLVTag.Type type;
		FLVTag.TagHeader tagHeader;
		while (true)
		{
			if (readBuf.remaining() < 15)
			{
				return null;
			}
			int pos = readBuf.position();
			packetPos = ch.position() - readBuf.remaining();
			prevPacketSize = readBuf.getInt();
			packetType = (sbyte)readBuf.get() & 0xFF;
			int payloadSize = ((readBuf.getShort() & 0xFFFF) << 8) | ((sbyte)readBuf.get() & 0xFF);
			timestamp = ((readBuf.getShort() & 0xFFFF) << 8) | ((sbyte)readBuf.get() & 0xFF) | (((sbyte)readBuf.get() & 0xFF) << 24);
			streamId = ((readBuf.getShort() & 0xFFFF) << 8) | ((sbyte)readBuf.get() & 0xFF);
			if (readBuf.remaining() >= payloadSize + 4)
			{
				int thisPacketSize = readBuf.getInt(readBuf.position() + payloadSize);
				if (thisPacketSize != payloadSize + 11)
				{
					readBuf.position(readBuf.position() - 15);
					if (!repositionFile())
					{
						Logger.error(java.lang.String.format("Corrupt FLV stream at %d, failed to reposition!", Long.valueOf(packetPos)));
						ch.setPosition(ch.size());
						eof = true;
						return null;
					}
					Logger.warn(java.lang.String.format("Corrupt FLV stream at %d, repositioned to %d.", Long.valueOf(packetPos), Long.valueOf(ch.position() - readBuf.remaining())));
					continue;
				}
			}
			if (readBuf.remaining() < payloadSize)
			{
				readBuf.position(pos);
				return null;
			}
			if (packetType != 8 && packetType != 9 && packetType != 18)
			{
				NIOUtils.skip(readBuf, payloadSize);
				continue;
			}
			payload = NIOUtils.clone(NIOUtils.read(readBuf, payloadSize));
			switch (packetType)
			{
			case 8:
				type = FLVTag.Type.___003C_003EAUDIO;
				tagHeader = parseAudioTagHeader(payload.duplicate());
				break;
			case 9:
				type = FLVTag.Type.___003C_003EVIDEO;
				tagHeader = parseVideoTagHeader(payload.duplicate());
				break;
			case 18:
				type = FLVTag.Type.___003C_003ESCRIPT;
				tagHeader = null;
				break;
			default:
				java.lang.System.@out.println("NON AV packet");
				continue;
			}
			break;
		}
		int keyFrame = 0;
		if (tagHeader != null && tagHeader is FLVTag.VideoTagHeader)
		{
			FLVTag.VideoTagHeader vth = (FLVTag.VideoTagHeader)tagHeader;
			keyFrame &= ((vth.getFrameType() == 1) ? 1 : 0);
		}
		keyFrame &= ((packetType == 8 || packetType == 9) ? 1 : 0);
		FLVTag.Type type2 = type;
		FLVTag.TagHeader tagHeader2 = tagHeader;
		int keyFrame2 = keyFrame;
		int num = frameNo;
		frameNo = num + 1;
		FLVTag result = new FLVTag(type2, packetPos, tagHeader2, timestamp, payload, (byte)keyFrame2 != 0, num, streamId, prevPacketSize);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 162, 104, 103, 48, 167, 104, 105 })]
	private static void moveRemainderToTheStart(ByteBuffer readBuf)
	{
		int rem = readBuf.remaining();
		for (int i = 0; i < rem; i++)
		{
			readBuf.put(i, (byte)(sbyte)readBuf.get());
		}
		readBuf.clear();
		readBuf.position(rem);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 114, 162, 109, 122, 143, 122, 240, 70, 122,
		135, 131, 153, 110, 109, 115, 109, 113
	})]
	public virtual FLVTag readPrevPacket()
	{
		int startOfLastPacket = readBuf.getInt();
		readBuf.position(readBuf.position() - 4);
		if (readBuf.position() > startOfLastPacket)
		{
			readBuf.position(readBuf.position() - startOfLastPacket);
			FLVTag result = parsePacket(readBuf);
			
			return result;
		}
		long oldPos = ch.position() - readBuf.remaining();
		if (oldPos <= 9u)
		{
			return null;
		}
		long newPos = java.lang.Math.max(0L, oldPos - readBuf.capacity() / 2);
		ch.setPosition(newPos);
		readBuf.clear();
		ch.read(readBuf);
		readBuf.flip();
		readBuf.position((int)(oldPos - newPos));
		FLVTag result2 = readPrevPacket();
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 63, 130, 137, 107, 109, 106, 103, 106, 135,
		108, 115, 104, 102, 132, 159, 0, 106, 102, 106,
		175
	})]
	public static FLVTag.TagHeader parseAudioTagHeader(ByteBuffer dup)
	{
		int b = (sbyte)dup.get();
		int codecId = (b & 0xFF) >> 4;
		int sampleRate = sampleRates[(b >> 2) & 3];
		if (codecId == 4 || codecId == 11)
		{
			sampleRate = 16000;
		}
		if (codecId == 5 || codecId == 14)
		{
			sampleRate = 8000;
		}
		int sampleSizeInBits = ((((uint)b & 2u) != 0) ? 16 : 8);
		int signed = (((codecId != 3 && codecId != 0) || sampleSizeInBits == 16) ? 1 : 0);
		int channelCount = 1 + (b & 1);
		if (codecId == 11)
		{
			channelCount = 1;
		}
		AudioFormat.___003Cclinit_003E();
		AudioFormat audioFormat = new AudioFormat(sampleRate, sampleSizeInBits, channelCount, (byte)signed != 0, codecId != 3 && platformBigEndian);
		Codec codec = audioCodecMapping[codecId];
		if (codecId == 10)
		{
			int packetType = (sbyte)dup.get();
			FLVTag.AacAudioTagHeader result = new FLVTag.AacAudioTagHeader(codec, audioFormat, packetType);
			
			return result;
		}
		FLVTag.AudioTagHeader result2 = new FLVTag.AudioTagHeader(codec, audioFormat);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 67, 162, 105, 107, 102, 137, 101, 106, 121,
		175
	})]
	public static FLVTag.VideoTagHeader parseVideoTagHeader(ByteBuffer dup)
	{
		int b0 = (sbyte)dup.get();
		int frameType = (b0 & 0xFF) >> 4;
		int codecId = b0 & 0xF;
		Codec codec = videoCodecMapping[codecId];
		if (codecId == 7)
		{
			int avcPacketType = (sbyte)dup.get();
			int compOffset = (dup.getShort() << 8) | ((sbyte)dup.get() & 0xFF);
			FLVTag.AvcVideoTagHeader result = new FLVTag.AvcVideoTagHeader(codec, frameType, (byte)avcPacketType, compOffset);
			
			return result;
		}
		FLVTag.VideoTagHeader result2 = new FLVTag.VideoTagHeader(codec, frameType);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 98, 101, 144, 159, 34, 143, 147, 138,
		138, 138, 138, 119, 104, 131, 135
	})]
	private static object readAMFData(ByteBuffer input, int type)
	{
		if (type == -1)
		{
			type = (sbyte)input.get() & 0xFF;
		}
		switch (type)
		{
		case 0:
		{
			java.lang.Double result6 = java.lang.Double.valueOf(input.getDouble());
			
			return result6;
		}
		case 1:
		{
			java.lang.Boolean result5 = java.lang.Boolean.valueOf((sbyte)input.get() == 1);
			
			return result5;
		}
		case 2:
		{
			string result4 = readAMFString(input);
			
			return result4;
		}
		case 3:
		{
			object result3 = readAMFObject(input);
			
			return result3;
		}
		case 8:
		{
			object result2 = readAMFEcmaArray(input);
			
			return result2;
		}
		case 10:
		{
			object result = readAMFStrictArray(input);
			
			return result;
		}
		case 11:
		{
			Date.___003Cclinit_003E();
			Date date = new Date(ByteCodeHelper.d2l(input.getDouble()));
			input.getShort();
			return date;
		}
		case 13:
			return "UNDEFINED";
		default:
			return null;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 74, 130, 110 })]
	private static string readAMFString(ByteBuffer input)
	{
		int size = input.getShort() & 0xFFFF;
		string result = Platform.stringFromCharset(NIOUtils.toArray(NIOUtils.read(input, size)), "UTF-8");
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 73, 162, 135, 104, 111, 102, 131, 112, 99 })]
	private static object readAMFObject(ByteBuffer input)
	{
		HashMap array = new HashMap();
		while (true)
		{
			string key = readAMFString(input);
			int dataType = (sbyte)input.get() & 0xFF;
			if (dataType == 9)
			{
				break;
			}
			((Map)array).put((object)key, readAMFData(input, dataType));
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 69, 66, 105, 103, 104, 104, 112, 241, 61,
		231, 69
	})]
	private static object readAMFEcmaArray(ByteBuffer input)
	{
		long size = input.getInt();
		HashMap array = new HashMap();
		for (int i = 0; i < size; i++)
		{
			string key = readAMFString(input);
			int dataType = (sbyte)input.get() & 0xFF;
			((Map)array).put((object)key, readAMFData(input, dataType));
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 98, 104, 104, 103, 43, 167 })]
	private static object readAMFStrictArray(ByteBuffer input)
	{
		int count = input.getInt();
		object[] result = new object[count];
		for (int i = 0; i < count; i++)
		{
			result[i] = readAMFData(input, -1);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 54, 130, 111 })]
	public virtual void reset()
	{
		initialRead(ch);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 50, 66, 104, 99, 105, 108, 121, 108, 127,
		3, 112, 131, 102
	})]
	public static bool positionAtPacket(ByteBuffer readBuf)
	{
		ByteBuffer dup = readBuf.duplicate();
		int payloadSize = 0;
		NIOUtils.skip(dup, 5);
		while (dup.hasRemaining())
		{
			payloadSize = ((payloadSize & 0xFFFF) << 8) | ((sbyte)dup.get() & 0xFF);
			int pointerPos = dup.position() + 7 + payloadSize;
			if (dup.position() >= 8 && pointerPos < dup.limit() - 4 && dup.getInt(pointerPos) - payloadSize == 11)
			{
				readBuf.position(dup.position() - 8);
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 105, 104, 113, 146, 136, 174, 110,
		105, 145, 127, 8, 53, 205
	})]
	public FLVReader(org.jcodec.common.io.SeekableByteChannel ch)
	{
		this.ch = ch;
		readBuf = ByteBuffer.allocate(1024);
		readBuf.order(ByteOrder.BIG_ENDIAN);
		initialRead(ch);
		if (!readHeader(readBuf))
		{
			readBuf.position(0);
			if (!repositionFile())
			{
				
				throw new RuntimeException("Invalid FLV file");
			}
			Logger.warn(java.lang.String.format("Parsing a corrupt FLV file, first tag found at %d. %s", Integer.valueOf(readBuf.position()), (readBuf.position() != 0) ? "" : "Did you forget the FLV 9-byte header?"));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 122, 162, 105, 131, 142, 114, 108, 117, 104,
		163, 120, 109, 110, 114, 131, 116, 110, 104, 117,
		104, 131, 166
	})]
	public virtual FLVTag readNextPacket()
	{
		if (eof)
		{
			return null;
		}
		FLVTag pkt = parsePacket(readBuf);
		if (pkt == null && !eof)
		{
			moveRemainderToTheStart(readBuf);
			if (ch.read(readBuf) == -1)
			{
				eof = true;
				return null;
			}
			while (MathUtil.log2(readBuf.capacity()) <= 22)
			{
				readBuf.flip();
				pkt = parsePacket(readBuf);
				if (pkt != null || readBuf.position() > 0)
				{
					break;
				}
				ByteBuffer newBuf = ByteBuffer.allocate(readBuf.capacity() << 2);
				newBuf.put(readBuf);
				readBuf = newBuf;
				if (ch.read(readBuf) == -1)
				{
					eof = true;
					return null;
				}
			}
		}
		return pkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 85, 162, 116, 117 })]
	public static FLVMetadata parseMetadata(ByteBuffer bb)
	{
		if (java.lang.String.instancehelper_equals("onMetaData", readAMFData(bb, -1)))
		{
			FLVMetadata result = new FLVMetadata((Map)readAMFData(bb, -1));
			
			return result;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 56, 130, 104, 126, 98 })]
	public static int probe(ByteBuffer buf)
	{
		//Discarded unreachable code: IL_000e
		RuntimeException ex2;
		try
		{
			readHeader(buf);
			return 100;
		}
		catch (System.Exception x)
		{
			RuntimeException ex = ByteCodeHelper.MapException<RuntimeException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		RuntimeException e = ex2;
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 53, 130, 135, 110, 145 })]
	public virtual void reposition()
	{
		reset();
		if (!positionAtPacket(readBuf))
		{
			
			throw new RuntimeException("Could not find at FLV tag start");
		}
	}

	[LineNumberTable(new byte[] { 159, 131, 98, 152, 191, 91, 191, 37 })]
	static FLVReader()
	{
		platformBigEndian = ByteBuffer.allocate(0).order() == ByteOrder.BIG_ENDIAN;
		audioCodecMapping = new Codec[14]
		{
			Codec.___003C_003EPCM,
			Codec.___003C_003EADPCM,
			Codec.___003C_003EMP3,
			Codec.___003C_003EPCM,
			Codec.___003C_003ENELLYMOSER,
			Codec.___003C_003ENELLYMOSER,
			Codec.___003C_003ENELLYMOSER,
			Codec.___003C_003EG711,
			Codec.___003C_003EG711,
			null,
			Codec.___003C_003EAAC,
			Codec.___003C_003ESPEEX,
			Codec.___003C_003EMP3,
			null
		};
		videoCodecMapping = new Codec[8]
		{
			null,
			null,
			Codec.___003C_003ESORENSON,
			Codec.___003C_003EFLASH_SCREEN_VIDEO,
			Codec.___003C_003EVP6,
			Codec.___003C_003EVP6,
			Codec.___003C_003EFLASH_SCREEN_V2,
			Codec.___003C_003EH264
		};
		sampleRates = new int[4] { 5500, 11000, 22000, 44100 };
	}
}
