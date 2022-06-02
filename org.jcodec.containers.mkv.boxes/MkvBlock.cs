using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mkv.util;
using org.jcodec.platform;

namespace org.jcodec.containers.mkv.boxes;

public class MkvBlock : EbmlBin
{
	private const string XIPH = "Xiph";

	private const string EBML = "EBML";

	private const string FIXED = "Fixed";

	private const int MAX_BLOCK_HEADER_SIZE = 512;

	public int[] frameOffsets;

	public int[] frameSizes;

	public long trackNumber;

	public int timecode;

	public long absoluteTimecode;

	public bool _keyFrame;

	public int headerSize;

	public string lacing;

	public bool discardable;

	public bool lacingPresent;

	public ByteBuffer[] frames;

	internal static byte[] ___003C_003EBLOCK_ID;

	internal static byte[] ___003C_003ESIMPLEBLOCK_ID;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] BLOCK_ID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBLOCK_ID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] SIMPLEBLOCK_ID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESIMPLEBLOCK_ID;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 66, 104, 123, 127, 12 })]
	public MkvBlock(byte[] type)
		: base(type)
	{
		if (!Platform.arrayEqualsByte(___003C_003ESIMPLEBLOCK_ID, type) && !Platform.arrayEqualsByte(___003C_003EBLOCK_ID, type))
		{
			string s = new StringBuilder().append("Block initiated with invalid id: ").append(EbmlUtil.toHexString(type)).toString();
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 66, 136, 109, 111, 111, 143, 111, 113,
		109, 134, 111, 108, 112, 112, 134, 108, 159, 5,
		134, 108, 159, 2, 134, 108, 109, 125, 142, 99,
		145, 109, 131, 108, 105, 109, 139, 141, 109, 150
	})]
	public override void read(ByteBuffer source)
	{
		ByteBuffer bb = source.slice();
		trackNumber = ebmlDecode(bb);
		int tcPart1 = (sbyte)bb.get() & 0xFF;
		int tcPart2 = (sbyte)bb.get() & 0xFF;
		timecode = (short)(((short)tcPart1 << 8) | (short)tcPart2);
		int flags = (sbyte)bb.get() & 0xFF;
		_keyFrame = (flags & 0x80) > 0;
		discardable = (flags & 1) > 0;
		int laceFlags = flags & 6;
		lacingPresent = ((laceFlags != 0) ? true : false);
		if (lacingPresent)
		{
			int lacesCount = (sbyte)bb.get() & 0xFF;
			frameSizes = new int[lacesCount + 1];
			switch (laceFlags)
			{
			case 2:
				lacing = "Xiph";
				headerSize = readXiphLaceSizes(bb, frameSizes, dataLen, bb.position());
				break;
			case 6:
				lacing = "EBML";
				headerSize = readEBMLLaceSizes(bb, frameSizes, dataLen, bb.position());
				break;
			case 4:
			{
				lacing = "Fixed";
				headerSize = bb.position();
				int num = dataLen - headerSize;
				int num2 = lacesCount + 1;
				int aLaceSize = ((num2 != -1) ? (num / num2) : (-num));
				Arrays.fill(frameSizes, aLaceSize);
				break;
			}
			default:
				
				throw new RuntimeException("Unsupported lacing type flag.");
			}
			turnSizesToFrameOffsets(frameSizes);
		}
		else
		{
			lacing = "";
			int frameOffset = bb.position();
			frameOffsets = new int[1];
			frameOffsets[0] = frameOffset;
			headerSize = bb.position();
			frameSizes = new int[1];
			frameSizes[0] = dataLen - headerSize;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 62, 130, 105, 104, 100, 145, 143, 101, 101,
		116, 167
	})]
	public static long ebmlDecode(ByteBuffer bb)
	{
		int firstByte = (sbyte)bb.get();
		int length = EbmlUtil.computeLength((byte)firstByte);
		if (length == 0)
		{
			
			throw new RuntimeException("Invalid ebml integer size.");
		}
		long value = firstByte & (int)(255u >> length);
		for (length += -1; length > 0; length += -1)
		{
			value = (value << 8) | ((sbyte)bb.get() & 0xFF);
		}
		return value;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 104, 66, 104, 102, 133, 106, 103, 105, 111,
		181, 245, 57, 234, 74, 109, 148
	})]
	public static int readXiphLaceSizes(ByteBuffer bb, int[] sizes, int size, int preLacingHeaderSize)
	{
		int startPos = bb.position();
		int lastIndex = (int)((nint)sizes.LongLength - 1);
		sizes[lastIndex] = size;
		int num;
		int[] array;
		for (int i = 0; i < lastIndex; i++)
		{
			int laceSize = 255;
			while (laceSize == 255)
			{
				laceSize = (sbyte)bb.get() & 0xFF;
				num = i;
				array = sizes;
				array[num] += laceSize;
			}
			num = lastIndex;
			array = sizes;
			array[num] -= sizes[i];
		}
		int headerSize = bb.position() - startPos + preLacingHeaderSize;
		num = lastIndex;
		array = sizes;
		array[num] -= headerSize;
		return headerSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 130, 102, 133, 104, 139, 143, 102, 101,
		105, 137, 106, 167, 240, 57, 233, 74, 109, 110
	})]
	public static int readEBMLLaceSizes(ByteBuffer source, int[] sizes, int size, int preLacingHeaderSize)
	{
		int lastIndex = (int)((nint)sizes.LongLength - 1);
		sizes[lastIndex] = size;
		int startPos = source.position();
		sizes[0] = (int)ebmlDecode(source);
		int num = lastIndex;
		int[] array = sizes;
		array[num] -= sizes[0];
		int laceSize = sizes[0];
		long laceSizeDiff = 0L;
		for (int i = 1; i < lastIndex; i++)
		{
			laceSizeDiff = ebmlDecodeSigned(source);
			laceSize = (sizes[i] = (int)(laceSize + laceSizeDiff));
			num = lastIndex;
			array = sizes;
			array[num] -= sizes[i];
		}
		int headerSize = source.position() - startPos + preLacingHeaderSize;
		num = lastIndex;
		array = sizes;
		array[num] -= headerSize;
		return headerSize;
	}

	[LineNumberTable(new byte[] { 159, 106, 66, 110, 111, 104, 57, 167 })]
	private void turnSizesToFrameOffsets(int[] sizes)
	{
		frameOffsets = new int[(nint)sizes.LongLength];
		frameOffsets[0] = headerSize;
		for (int i = 1; i < (nint)sizes.LongLength; i++)
		{
			frameOffsets[i] = frameOffsets[i - 1] + sizes[i - 1];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 58, 162, 105, 136, 100, 145, 111, 101, 101,
		116, 167
	})]
	public static long ebmlDecodeSigned(ByteBuffer source)
	{
		int firstByte = (sbyte)source.get();
		int size = EbmlUtil.computeLength((byte)firstByte);
		if (size == 0)
		{
			
			throw new RuntimeException("Invalid ebml integer size.");
		}
		long value = firstByte & (int)(255u >> size);
		for (int remaining = size - 1; remaining > 0; remaining += -1)
		{
			value = (value << 8) | ((sbyte)source.get() & 0xFF);
		}
		return value - EbmlSint.___003C_003EsignedComplement[size];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 110, 112, 113, 127, 34, 112, 104,
		112, 229, 58, 234, 72
	})]
	public virtual ByteBuffer[] getFrames(ByteBuffer source)
	{
		ByteBuffer[] frames = new ByteBuffer[(nint)frameSizes.LongLength];
		for (int i = 0; i < (nint)frameSizes.LongLength; i++)
		{
			if (frameOffsets[i] > source.limit())
			{
				java.lang.System.err.println(new StringBuilder().append("frame offset: ").append(frameOffsets[i]).append(" limit: ")
					.append(source.limit())
					.toString());
			}
			source.position(frameOffsets[i]);
			ByteBuffer bb = source.slice();
			bb.limit(frameSizes[i]);
			frames[i] = bb;
		}
		return frames;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 69, 98, 131, 119, 40, 167, 105, 107, 165,
		101, 111
	})]
	public virtual int getDataSize()
	{
		int size = 0;
		int[] array = frameSizes;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			long fsize = array[i];
			size = (int)(size + fsize);
		}
		if (lacingPresent)
		{
			size = (int)(size + (nint)muxLacingInfo().LongLength);
			size++;
		}
		size += 3;
		return size + EbmlUtil.ebmlLength(trackNumber);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 65, 98, 115, 143, 115, 143, 115, 136 })]
	private byte[] muxLacingInfo()
	{
		if (String.instancehelper_equals("EBML", lacing))
		{
			byte[] result = muxEbmlLacing(frameSizes);
			
			return result;
		}
		if (String.instancehelper_equals("Xiph", lacing))
		{
			byte[] result2 = muxXiphLacing(frameSizes);
			
			return result2;
		}
		if (String.instancehelper_equals("Fixed", lacing))
		{
			return new byte[0];
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 51, 130, 135, 104, 143, 104, 47, 167 })]
	public static byte[] muxEbmlLacing(int[] laceSizes)
	{
		ByteArrayList bytes = ByteArrayList.createByteArrayList();
		long[] laceSizeDiffs = calcEbmlLacingDiffs(laceSizes);
		bytes.addAll(EbmlUtil.ebmlEncode(laceSizeDiffs[0]));
		for (int i = 1; i < (nint)laceSizeDiffs.LongLength; i++)
		{
			bytes.addAll(EbmlSint.convertToBytes(laceSizeDiffs[i]));
		}
		byte[] result = bytes.toArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 48, 130, 103, 106, 102, 106, 104, 140, 234,
		58, 231, 72
	})]
	public static byte[] muxXiphLacing(int[] laceSizes)
	{
		ByteArrayList bytes = ByteArrayList.createByteArrayList();
		for (int i = 0; i < (nint)laceSizes.LongLength - 1; i++)
		{
			long laceSize;
			for (laceSize = laceSizes[i]; laceSize >= 255u; laceSize -= 255u)
			{
				bytes.add(byte.MaxValue);
			}
			bytes.add((byte)(sbyte)laceSize);
		}
		byte[] result = bytes.toArray();
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 53, 66, 102, 104, 104, 103, 46, 167 })]
	public static long[] calcEbmlLacingDiffs(int[] laceSizes)
	{
		int lacesCount = (int)((nint)laceSizes.LongLength - 1);
		long[] @out = new long[lacesCount];
		@out[0] = laceSizes[0];
		for (int i = 1; i < lacesCount; i++)
		{
			@out[i] = laceSizes[i] - laceSizes[i - 1];
		}
		return @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 130, 109, 109, 109, 109, 109, 109, 109,
		109, 109, 115, 115, 109, 109, 109, 123, 123
	})]
	public static MkvBlock copy(MkvBlock old)
	{
		MkvBlock be = new MkvBlock(old.id);
		be.trackNumber = old.trackNumber;
		be.timecode = old.timecode;
		be.absoluteTimecode = old.absoluteTimecode;
		be._keyFrame = old._keyFrame;
		be.headerSize = old.headerSize;
		be.lacing = old.lacing;
		be.discardable = old.discardable;
		be.lacingPresent = old.lacingPresent;
		be.frameOffsets = new int[(nint)old.frameOffsets.LongLength];
		be.frameSizes = new int[(nint)old.frameSizes.LongLength];
		be.dataOffset = old.dataOffset;
		be.offset = old.offset;
		be.type = old.type;
		ByteCodeHelper.arraycopy_primitive_4(old.frameOffsets, 0, be.frameOffsets, 0, be.frameOffsets.Length);
		ByteCodeHelper.arraycopy_primitive_4(old.frameSizes, 0, be.frameSizes, 0, be.frameSizes.Length);
		return be;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 130, 108, 113, 118, 104, 104, 104 })]
	public static MkvBlock keyFrame(long trackNumber, int timecode, ByteBuffer frame)
	{
		MkvBlock be = new MkvBlock(___003C_003ESIMPLEBLOCK_ID);
		be.frames = new ByteBuffer[1] { frame };
		be.frameSizes = new int[1] { frame.limit() };
		be._keyFrame = true;
		be.trackNumber = trackNumber;
		be.timecode = timecode;
		return be;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 122, 162, 105, 105, 104, 104, 118 })]
	public override void readChannel(org.jcodec.common.io.SeekableByteChannel @is)
	{
		ByteBuffer bb = ByteBuffer.allocate(100);
		@is.read(bb);
		bb.flip();
		read(bb);
		@is.setPosition(dataOffset + dataLen);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 92, 98, 103, 120, 120, 120, 120, 120, 120,
		109, 63, 34, 167, 141
	})]
	public override string toString()
	{
		StringBuilder sb = new StringBuilder();
		sb.append("{dataOffset: ").append(dataOffset);
		sb.append(", trackNumber: ").append(trackNumber);
		sb.append(", timecode: ").append(timecode);
		sb.append(", keyFrame: ").append(_keyFrame);
		sb.append(", headerSize: ").append(headerSize);
		sb.append(", lacing: ").append(lacing);
		for (int i = 0; i < (nint)frameSizes.LongLength; i++)
		{
			sb.append(", frame[").append(i).append("]  offset ")
				.append(frameOffsets[i])
				.append(" size ")
				.append(frameSizes[i]);
		}
		sb.append(" }");
		string result = sb.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 85, 130, 110 })]
	public virtual void readFrames(ByteBuffer source)
	{
		frames = getFrames(source);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 162, 104, 120, 110, 143, 115, 119, 149,
		99, 115, 101, 115, 101, 115, 163, 105, 102, 105,
		138, 137, 102, 120, 174, 109, 107, 10, 231, 69,
		104
	})]
	public override ByteBuffer getData()
	{
		int dataSize = getDataSize();
		ByteBuffer bb = ByteBuffer.allocate((int)(dataSize + EbmlUtil.ebmlLength(dataSize) + (nint)id.LongLength));
		bb.put(id);
		bb.put(EbmlUtil.ebmlEncode(dataSize));
		bb.put(EbmlUtil.ebmlEncode(trackNumber));
		bb.put((byte)(sbyte)(((uint)timecode >> 8) & 0xFFu));
		bb.put((byte)(sbyte)((uint)timecode & 0xFFu));
		int flags = 0;
		if (String.instancehelper_equals("Xiph", lacing))
		{
			flags = 2;
		}
		else if (String.instancehelper_equals("EBML", lacing))
		{
			flags = 6;
		}
		else if (String.instancehelper_equals("Fixed", lacing))
		{
			flags = 4;
		}
		if (discardable)
		{
			flags = (sbyte)(flags | 1);
		}
		if (_keyFrame)
		{
			flags = (sbyte)(flags | 0x80);
		}
		bb.put((byte)flags);
		if (((uint)flags & 6u) != 0)
		{
			bb.put((byte)(sbyte)(((nint)frames.LongLength - 1) & 0xFF));
			bb.put(muxLacingInfo());
		}
		for (int i = 0; i < (nint)frames.LongLength; i++)
		{
			ByteBuffer frame = frames[i];
			bb.put(frame);
		}
		bb.flip();
		return bb;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 74, 162, 114, 110, 110, 109 })]
	public virtual void seekAndReadContent(FileChannel source)
	{
		data = ByteBuffer.allocate(dataLen);
		source.position(dataOffset);
		source.read(data);
		data.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 71, 130, 105, 107, 108 })]
	public override long size()
	{
		long size = getDataSize();
		size += EbmlUtil.ebmlLength(size);
		return size + id.LongLength;
	}

	[LineNumberTable(new byte[] { 159, 132, 130, 113 })]
	static MkvBlock()
	{
		___003C_003EBLOCK_ID = new byte[1] { 161 };
		___003C_003ESIMPLEBLOCK_ID = new byte[1] { 163 };
	}
}
