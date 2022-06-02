using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mxf.model;

public class BER : Object
{
	public const byte ASN_LONG_LEN = 128;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public BER()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 138, 162, 100, 143, 107, 102, 100, 113, 101,
		113, 137, 103, 42, 167, 102, 113, 99, 138
	})]
	public static long decodeLength(SeekableByteChannel @is)
	{
		long length = 0L;
		int lengthbyte = (sbyte)NIOUtils.readByte(@is) & 0xFF;
		if ((lengthbyte & -128) > 0)
		{
			lengthbyte &= 0x7F;
			if (lengthbyte == 0)
			{
				
				throw new IOException("Indefinite lengths are not supported");
			}
			if (lengthbyte > 8)
			{
				
				throw new IOException("Data length > 4 bytes are not supported!");
			}
			byte[] bb = NIOUtils.readNByte(@is, lengthbyte);
			for (int i = 0; i < lengthbyte; i++)
			{
				length = (length << 8) | (int)bb[i];
			}
			if (length < 0u)
			{
				
				throw new IOException("mxflib does not support data lengths > 2^63");
			}
		}
		else
		{
			length = lengthbyte & 0xFF;
		}
		return length;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 100, 143, 107, 102, 100, 145, 101,
		145, 103, 52, 167, 102, 145, 138
	})]
	public static long decodeLengthBuf(ByteBuffer buffer)
	{
		long length = 0L;
		int lengthbyte = (sbyte)buffer.get() & 0xFF;
		if ((lengthbyte & -128) > 0)
		{
			lengthbyte &= 0x7F;
			if (lengthbyte == 0)
			{
				
				throw new RuntimeException("Indefinite lengths are not supported");
			}
			if (lengthbyte > 8)
			{
				
				throw new RuntimeException("Data length > 8 bytes are not supported!");
			}
			for (int i = 0; i < lengthbyte; i++)
			{
				length = (length << 8) | ((sbyte)buffer.get() & 0xFF);
			}
			if (length < 0u)
			{
				
				throw new RuntimeException("mxflib does not support data lengths > 2^63");
			}
		}
		else
		{
			length = lengthbyte & 0xFF;
		}
		return length;
	}
}
