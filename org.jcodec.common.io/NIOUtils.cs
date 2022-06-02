using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.platform;

namespace org.jcodec.common.io;

public class NIOUtils : Object
{
	public abstract class FileReader : Object
	{
		private int oldPd;

		protected internal abstract void data(ByteBuffer bb, long l);

		protected internal abstract void done();

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 43, 162, 104, 104, 117, 104, 105, 104, 100,
			115, 106, 104, 232, 56, 237, 75, 105
		})]
		public virtual void readChannel(SeekableByteChannel ch, int bufferSize, FileReaderListener listener)
		{
			ByteBuffer buf = ByteBuffer.allocate(bufferSize);
			long size = ch.size();
			long pos = ch.position();
			while (ch.read(buf) != -1)
			{
				buf.flip();
				data(buf, pos);
				buf.flip();
				if (listener != null)
				{
					long num = 100u * pos;
					int newPd = (int)((size != -1) ? (num / size) : (-num));
					if (newPd != oldPd)
					{
						listener.progress(newPd);
					}
					oldPd = newPd;
				}
				pos = ch.position();
			}
			done();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(391)]
		public FileReader()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 38, 66, 131, 104, 142, 74, 99, 99 })]
		public virtual void readFile(File source, int bufferSize, FileReaderListener listener)
		{
			FileChannelWrapper ch = null;
			try
			{
				ch = readableChannel(source);
				readChannel(ch, bufferSize, listener);
			}
			finally
			{
				closeQuietly(ch);
			}
		}
	}

	public interface FileReaderListener
	{
		void progress(int i);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(344)]
	public static FileChannelWrapper readableChannel(File file)
	{
		FileChannelWrapper result = new FileChannelWrapper(new FileInputStream(file).getChannel());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 71, 98, 100, 130, 153, 35, 130 })]
	public static void closeQuietly(Closeable channel)
	{
		if (channel != null)
		{
			IOException ex;
			try
			{
				channel.close();
				return;
			}
			catch (IOException x)
			{
				ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
			}
			IOException ex2 = ex;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(348)]
	public static FileChannelWrapper writableChannel(File file)
	{
		FileChannelWrapper result = new FileChannelWrapper(new FileOutputStream(file).getChannel());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(360)]
	public static FileChannelWrapper writableFileChannel(string file)
	{
		FileChannelWrapper result = new FileChannelWrapper(new FileOutputStream(file).getChannel());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 33, 130, 109, 110, 104 })]
	public static ByteBuffer clone(ByteBuffer byteBuffer)
	{
		ByteBuffer result = ByteBuffer.allocate(byteBuffer.remaining());
		result.put(byteBuffer.duplicate());
		result.flip();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(356)]
	public static FileChannelWrapper readableFileChannel(string file)
	{
		FileChannelWrapper result = new FileChannelWrapper(new FileInputStream(file).getChannel());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 121, 98, 167, 109, 105, 137 })]
	public static ByteBuffer fetchAllFromChannel(SeekableByteChannel ch)
	{
		ArrayList buffers = new ArrayList();
		ByteBuffer buf;
		do
		{
			buf = fetchFromChannel(ch, 1048576);
			((List)buffers).add((object)buf);
		}
		while (buf.hasRemaining());
		ByteBuffer result = combineBuffers(buffers);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 130, 109, 110 })]
	public static byte[] toArray(ByteBuffer buffer)
	{
		byte[] result = new byte[buffer.remaining()];
		buffer.duplicate().get(result);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 126, 130, 104, 106, 105, 105 })]
	public static ByteBuffer read(ByteBuffer buffer, int count)
	{
		ByteBuffer slice = buffer.duplicate();
		int limit = buffer.position() + count;
		slice.limit(limit);
		buffer.position(limit);
		return slice;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 49, 66, 109, 110, 104 })]
	public static ByteBuffer duplicate(ByteBuffer bb)
	{
		ByteBuffer @out = ByteBuffer.allocate(bb.remaining());
		@out.put(bb.duplicate());
		@out.flip();
		return @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 58, 66, 126 })]
	public static void writeInt(WritableByteChannel channel, int value)
	{
		channel.write((ByteBuffer)ByteBuffer.allocate(4).putInt(value).flip());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 62, 97, 68, 126 })]
	public static void writeByte(WritableByteChannel channel, byte value)
	{
		int value2 = (sbyte)value;
		channel.write((ByteBuffer)ByteBuffer.allocate(1).put((byte)value2).flip());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 130, 105, 159, 15, 148 })]
	public static void write(ByteBuffer to, ByteBuffer from)
	{
		if (from.hasArray())
		{
			to.put(from.array(), from.arrayOffset() + from.position(), Math.min(to.remaining(), from.remaining()));
		}
		else
		{
			to.put(toArrayL(from, to.remaining()));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 130, 110, 112 })]
	public static int skip(ByteBuffer buffer, int count)
	{
		int toSkip = Math.min(buffer.remaining(), count);
		buffer.position(buffer.position() + toSkip);
		return toSkip;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 29, 130, 107, 48, 167, 105, 110 })]
	public static void relocateLeftover(ByteBuffer bb)
	{
		int pos = 0;
		while (bb.hasRemaining())
		{
			bb.put(pos, (byte)(sbyte)bb.get());
			pos++;
		}
		bb.position(pos);
		bb.limit(bb.capacity());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(427)]
	public static byte getRel(ByteBuffer bb, int rel)
	{
		sbyte result = (sbyte)bb.get(bb.position() + rel);
		
		return (byte)result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 123, 130, 104, 105, 104 })]
	public static ByteBuffer fetchFromChannel(ReadableByteChannel ch, int size)
	{
		ByteBuffer buf = ByteBuffer.allocate(size);
		readFromChannel(ch, buf);
		buf.flip();
		return buf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/Iterable<Ljava/nio/ByteBuffer;>;)Ljava/nio/ByteBuffer;")]
	[LineNumberTable(new byte[]
	{
		159, 91, 130, 99, 124, 106, 99, 104, 127, 1,
		105, 99, 104
	})]
	public static ByteBuffer combineBuffers(Iterable picture)
	{
		int size = 0;
		Iterator iterator = picture.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer2 = (ByteBuffer)iterator.next();
			size += byteBuffer2.remaining();
		}
		ByteBuffer result = ByteBuffer.allocate(size);
		Iterator iterator2 = picture.iterator();
		while (iterator2.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator2.next();
			write(result, byteBuffer);
		}
		result.flip();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 35, 162, 109, 110, 104 })]
	public static ByteBuffer cloneBuffer(ByteBuffer pesBuffer)
	{
		ByteBuffer res = ByteBuffer.allocate(pesBuffer.remaining());
		res.put(pesBuffer.duplicate());
		res.clear();
		return res;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 100, 130, 105, 159, 10, 143 })]
	public static void writeL(ByteBuffer to, ByteBuffer from, int count)
	{
		if (from.hasArray())
		{
			to.put(from.array(), from.arrayOffset() + from.position(), Math.min(from.remaining(), count));
		}
		else
		{
			to.put(toArrayL(from, count));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 66, 104, 110 })]
	public static ByteBuffer readBuf(ByteBuffer buffer)
	{
		ByteBuffer result = buffer.duplicate();
		buffer.position(buffer.limit());
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 60, 130, 104, 109, 121 })]
	public static void writeIntLE(WritableByteChannel channel, int value)
	{
		ByteBuffer allocate = ByteBuffer.allocate(4);
		allocate.order(ByteOrder.LITTLE_ENDIAN);
		channel.write((ByteBuffer)allocate.putInt(value).flip());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(223)]
	public static string readString(ByteBuffer buffer, int len)
	{
		string result = Platform.stringFromBytes(toArray(read(buffer, len)));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 106, 130, 104, 123, 115, 99, 110 })]
	public static int readL(ReadableByteChannel channel, ByteBuffer buffer, int length)
	{
		ByteBuffer fork = buffer.duplicate();
		fork.limit(Math.min(fork.position() + length, fork.limit()));
		while (channel.read(fork) != -1 && fork.hasRemaining())
		{
		}
		buffer.position(fork.position());
		int result = ((buffer.position() != 0) ? buffer.position() : (-1));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(74)]
	public static ByteBuffer fetchFromFile(File file)
	{
		ByteBuffer result = fetchFromFileL(file, (int)file.length());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 111, 66, 131, 109, 141, 74, 99, 99 })]
	public static void writeTo(ByteBuffer buffer, File file)
	{
		FileChannel @out = null;
		try
		{
			@out = new FileOutputStream(file).getChannel();
			@out.write(buffer);
		}
		finally
		{
			closeQuietly(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 75, 130, 172, 105, 118, 105, 101, 104, 105,
		135, 106
	})]
	public static void copy(ReadableByteChannel _in, WritableByteChannel @out, long amount)
	{
		ByteBuffer buf = ByteBuffer.allocate(65536);
		int read;
		do
		{
			buf.position(0);
			buf.limit((int)Math.min(amount, buf.capacity()));
			read = _in.read(buf);
			if (read != -1)
			{
				buf.flip();
				@out.write(buf);
				amount -= read;
			}
		}
		while (read != -1 && amount > 0u);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 114, 130, 131, 109, 141, 74, 3 })]
	public static ByteBuffer fetchFromFileL(File file, int length)
	{
		FileChannel @is = null;
		try
		{
			@is = new FileInputStream(file).getChannel();
			return fetchFromChannel(@is, length);
		}
		finally
		{
			closeQuietly(@is);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 104, 162, 104, 115, 99 })]
	public static int readFromChannel(ReadableByteChannel channel, ByteBuffer buffer)
	{
		int rem = buffer.position();
		while (channel.read(buffer) != -1 && buffer.hasRemaining())
		{
		}
		return buffer.position() - rem;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 66, 115, 110 })]
	public static byte[] toArrayL(ByteBuffer buffer, int count)
	{
		byte[] result = new byte[Math.min(buffer.remaining(), count)];
		buffer.duplicate().get(result);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 96, 162, 104, 122, 103 })]
	public static MappedByteBuffer mapFile(File file)
	{
		FileInputStream @is = new FileInputStream(file);
		MappedByteBuffer map = @is.getChannel().map(FileChannel.MapMode.READ_ONLY, 0L, file.length());
		@is.close();
		return map;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(238)]
	public static byte[] asciiString(string fourcc)
	{
		byte[] bytes = Platform.getBytes(fourcc);
		
		return bytes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 162, 104, 114, 99, 105, 112 })]
	public static string readNullTermStringCharset(ByteBuffer buffer, string charset)
	{
		ByteBuffer fork = buffer.duplicate();
		while (buffer.hasRemaining() && (sbyte)buffer.get() != 0)
		{
		}
		if (buffer.hasRemaining())
		{
			fork.limit(buffer.position() - 1);
		}
		string result = Platform.stringFromCharset(toArray(fork), charset);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(445)]
	public static ByteBuffer asByteBuffer(byte[] bytes)
	{
		ByteBuffer result = ByteBuffer.wrap(bytes);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public NIOUtils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 104, 106, 108, 105, 103, 101, 102,
		100, 105, 110, 131, 102, 165, 100, 99, 101, 139,
		136, 102
	})]
	public static ByteBuffer search(ByteBuffer buffer, int n, byte[] param)
	{
		ByteBuffer result = buffer.duplicate();
		int step = 0;
		int rem = buffer.position();
		while (buffer.hasRemaining())
		{
			int b = (sbyte)buffer.get();
			if (b == param[step])
			{
				step++;
				if ((nint)step == (nint)param.LongLength)
				{
					if (n == 0)
					{
						buffer.position(rem);
						result.limit(buffer.position());
						break;
					}
					n += -1;
					step = 0;
				}
			}
			else if (step != 0)
			{
				step = 0;
				rem++;
				buffer.position(rem);
			}
			else
			{
				rem = buffer.position();
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 116, 130, 104, 105, 105, 104 })]
	public static ByteBuffer fetchFrom(ByteBuffer buf, ReadableByteChannel ch, int size)
	{
		ByteBuffer result = buf.duplicate();
		result.limit(size);
		readFromChannel(ch, result);
		result.flip();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 98, 129, 68, 105, 107 })]
	public static void fill(ByteBuffer buffer, byte val)
	{
		int val2 = (sbyte)val;
		while (buffer.hasRemaining())
		{
			buffer.put((byte)val2);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(183)]
	public static MappedByteBuffer map(string fileName)
	{
		MappedByteBuffer result = mapFile(new File(fileName));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 66, 104, 112 })]
	public static ByteBuffer from(ByteBuffer buffer, int offset)
	{
		ByteBuffer dup = buffer.duplicate();
		dup.position(dup.position() + offset);
		return dup;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Ljava/util/List<Ljava/nio/ByteBuffer;>;)Z")]
	[LineNumberTable(219)]
	public static bool combineBuffersInto(ByteBuffer dup, List buffers)
	{
		
		throw new RuntimeException("Stan");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 86, 162, 107 })]
	public static string readPascalStringL(ByteBuffer buffer, int maxLen)
	{
		ByteBuffer sub = read(buffer, maxLen + 1);
		string result = Platform.stringFromBytes(toArray(read(sub, Math.min((sbyte)sub.get() & 0xFF, maxLen))));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 66, 111, 110, 112 })]
	public static void writePascalStringL(ByteBuffer buffer, string @string, int maxLen)
	{
		buffer.put((byte)(sbyte)String.instancehelper_length(@string));
		buffer.put(asciiString(@string));
		skip(buffer, maxLen - String.instancehelper_length(@string));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 130, 111, 110 })]
	public static void writePascalString(ByteBuffer buffer, string name)
	{
		buffer.put((byte)(sbyte)String.instancehelper_length(name));
		buffer.put(asciiString(name));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(247)]
	public static string readPascalString(ByteBuffer buffer)
	{
		string result = readString(buffer, (sbyte)buffer.get() & 0xFF);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(251)]
	public static string readNullTermString(ByteBuffer buffer)
	{
		string result = readNullTermStringCharset(buffer, "UTF-8");
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 69, 130, 104, 105, 104 })]
	public static byte readByte(ReadableByteChannel channel)
	{
		ByteBuffer buf = ByteBuffer.allocate(1);
		channel.read(buf);
		buf.flip();
		sbyte result = (sbyte)buf.get();
		
		return (byte)result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 67, 98, 104, 110 })]
	public static byte[] readNByte(ReadableByteChannel channel, int n)
	{
		byte[] result = new byte[n];
		channel.read(ByteBuffer.wrap(result));
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 66, 162, 104, 105, 104 })]
	public static int readInt(ReadableByteChannel channel)
	{
		ByteBuffer buf = ByteBuffer.allocate(4);
		channel.read(buf);
		buf.flip();
		int @int = buf.getInt();
		
		return @int;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 64, 130, 110, 105, 104 })]
	public static int readIntOrder(ReadableByteChannel channel, ByteOrder order)
	{
		ByteBuffer buf = ByteBuffer.allocate(4).order(order);
		channel.read(buf);
		buf.flip();
		int @int = buf.getInt();
		
		return @int;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 61, 98, 110, 121 })]
	public static void writeIntOrder(WritableByteChannel channel, int value, ByteOrder order)
	{
		ByteBuffer order2 = ByteBuffer.allocate(4).order(order);
		channel.write((ByteBuffer)order2.putInt(value).flip());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 57, 66, 126 })]
	public static void writeLong(WritableByteChannel channel, long value)
	{
		channel.write((ByteBuffer)ByteBuffer.allocate(8).putLong(value).flip());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(352)]
	public static FileChannelWrapper rwChannel(File file)
	{
		FileChannelWrapper result = new FileChannelWrapper(new RandomAccessFile(file, "rw").getChannel());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.FileNotFoundException" })]
	[LineNumberTable(364)]
	public static FileChannelWrapper rwFileChannel(string file)
	{
		FileChannelWrapper result = new FileChannelWrapper(new RandomAccessFile(file, "rw").getChannel());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(368)]
	public static AutoFileChannelWrapper autoChannel(File file)
	{
		AutoFileChannelWrapper result = new AutoFileChannelWrapper(file);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/nio/ByteBuffer;)I")]
	[LineNumberTable(new byte[] { 159, 48, 162, 104, 108, 122, 3, 199 })]
	public static int find(List catalog, ByteBuffer key)
	{
		byte[] keyA = toArray(key);
		for (int i = 0; i < catalog.size(); i++)
		{
			if (Platform.arrayEqualsByte(toArray((ByteBuffer)catalog.get(i)), keyA))
			{
				return i;
			}
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(449)]
	public static ByteBuffer asByteBufferInt(int[] ints)
	{
		ByteBuffer result = asByteBuffer(ArrayUtil.toByteArray(ints));
		
		return result;
	}
}
