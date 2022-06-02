using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class Header : Object
{
	internal static byte[] ___003C_003EFOURCC_FREE;

	private const long MAX_UNSIGNED_INT = 4294967296L;

	private string fourcc;

	private long size;

	private bool lng;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] FOURCC_FREE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFOURCC_FREE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 162, 112 })]
	public virtual void setBodySize(int length)
	{
		size = length + headerSize();
	}

	[LineNumberTable(79)]
	public virtual long headerSize()
	{
		return (!lng && size <= 4294967296L) ? 8u : 16u;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 162, 114, 139, 111, 109, 105, 139, 109,
		114, 142
	})]
	public virtual void write(ByteBuffer @out)
	{
		if (size > 4294967296L)
		{
			@out.putInt(1);
		}
		else
		{
			@out.putInt((int)size);
		}
		byte[] bt = JCodecUtil2.asciiString(fourcc);
		if (bt != null && (nint)bt.LongLength == 4)
		{
			@out.put(bt);
		}
		else
		{
			@out.put(___003C_003EFOURCC_FREE);
		}
		if (size > 4294967296L)
		{
			@out.putLong(size);
		}
	}

	[LineNumberTable(95)]
	public virtual string getFourcc()
	{
		return fourcc;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 105, 104 })]
	public Header(string fourcc)
	{
		this.fourcc = fourcc;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(99)]
	public virtual long getBodySize()
	{
		return size - headerSize();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 104, 104 })]
	public static Header createHeader(string fourcc, long size)
	{
		Header header = new Header(fourcc);
		header.size = size;
		return header;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 100, 123, 99, 116, 127, 1, 163,
		105, 99, 102, 106, 99, 138, 127, 1, 195
	})]
	public static Header read(ByteBuffer input)
	{
		long size = 0L;
		while (input.remaining() >= 4 && (size = Platform.unsignedInt(input.getInt())) == 0u)
		{
		}
		if (input.remaining() < 4 || (size < 8u && size != 1u))
		{
			Logger.error(new StringBuilder().append("Broken atom of size ").append(size).toString());
			return null;
		}
		string fourcc = NIOUtils.readString(input, 4);
		int lng = 0;
		if (size == 1u)
		{
			if (input.remaining() < 8)
			{
				Logger.error(new StringBuilder().append("Broken atom of size ").append(size).toString());
				return null;
			}
			lng = 1;
			size = input.getLong();
		}
		Header result = newHeader(fourcc, size, (byte)lng != 0);
		
		return result;
	}

	[LineNumberTable(83)]
	public static int estimateHeaderSize(int size)
	{
		return (size + 8 <= 4294967296L) ? 8 : 16;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 161, 67, 104, 104, 104 })]
	public static Header newHeader(string fourcc, long size, bool lng)
	{
		Header header = new Header(fourcc);
		header.size = size;
		header.lng = lng;
		return header;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 124, 162, 118 })]
	public virtual void skip(InputStream di)
	{
		org.jcodec.common.io.StringReader.sureSkip(di, size - headerSize());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 121, 162, 103, 116, 45, 167 })]
	public virtual byte[] readContents(InputStream di)
	{
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		for (int i = 0; i < size - headerSize(); i++)
		{
			baos.write(di.read());
		}
		byte[] result = baos.toByteArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 112, 130, 105, 104, 104, 105 })]
	public virtual void writeChannel(SeekableByteChannel output)
	{
		ByteBuffer bb = ByteBuffer.allocate(16);
		write(bb);
		bb.flip();
		output.write(bb);
	}

	[LineNumberTable(129)]
	public virtual long getSize()
	{
		return size;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 130, 100, 99, 125 })]
	public override int hashCode()
	{
		int prime = 31;
		int result = 1;
		return 31 * result + ((fourcc != null) ? String.instancehelper_hashCode(fourcc) : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 130, 101, 99, 100, 99, 111, 99, 104,
		105, 105, 99, 116, 99
	})]
	public override bool equals(object obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (obj == null)
		{
			return false;
		}
		if ((object)((object)this).GetType() != obj.GetType())
		{
			return false;
		}
		Header other = (Header)obj;
		if (fourcc == null)
		{
			if (other.fourcc != null)
			{
				return false;
			}
		}
		else if (!String.instancehelper_equals(fourcc, other.fourcc))
		{
			return false;
		}
		return true;
	}

	[LineNumberTable(26)]
	static Header()
	{
		___003C_003EFOURCC_FREE = new byte[4] { 102, 114, 101, 101 };
	}
}
