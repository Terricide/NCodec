using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mxf.model;

public class KLV : Object
{
	internal long ___003C_003Eoffset;

	internal long ___003C_003EdataOffset;

	internal UL ___003C_003Ekey;

	internal long ___003C_003Elen;

	internal ByteBuffer value;

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public long offset
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eoffset;
		}
		[HideFromJava]
		private set
		{
			___003C_003Eoffset = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public long dataOffset
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdataOffset;
		}
		[HideFromJava]
		private set
		{
			___003C_003EdataOffset = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public UL key
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ekey;
		}
		[HideFromJava]
		private set
		{
			___003C_003Ekey = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public long len
	{
		[HideFromJava]
		get
		{
			return ___003C_003Elen;
		}
		[HideFromJava]
		private set
		{
			___003C_003Elen = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 105, 104, 104, 104, 105 })]
	public KLV(UL k, long len, long offset, long dataOffset)
	{
		___003C_003Ekey = k;
		___003C_003Elen = len;
		___003C_003Eoffset = offset;
		___003C_003EdataOffset = dataOffset;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public override string toString()
	{
		string result = new StringBuilder().append("KLV [offset=").append(___003C_003Eoffset).append(", dataOffset=")
			.append(___003C_003EdataOffset)
			.append(", key=")
			.append(___003C_003Ekey)
			.append(", len=")
			.append(___003C_003Elen)
			.append(", value=")
			.append(value)
			.append("]")
			.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 98, 104, 109, 131, 105, 142, 104, 104 })]
	public static KLV readKL(SeekableByteChannel ch)
	{
		long offset = ch.position();
		if (offset >= ch.size() - 1u)
		{
			return null;
		}
		byte[] key = new byte[16];
		ch.read(ByteBuffer.wrap(key));
		long len = BER.decodeLength(ch);
		long dataOffset = ch.position();
		KLV result = new KLV(new UL(key), len, offset, dataOffset);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 129, 98, 116 })]
	public virtual int getLenByteCount()
	{
		int berlen = (int)(___003C_003EdataOffset - ___003C_003Eoffset - 16u);
		return (berlen > 0) ? berlen : 4;
	}

	[LineNumberTable(new byte[] { 159, 128, 130, 103, 105, 3, 167 })]
	public static bool matches(byte[] key1, byte[] key2, int len)
	{
		for (int i = 0; i < len; i++)
		{
			if (key1[i] != key2[i])
			{
				return false;
			}
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 98, 107, 131, 139, 136, 104 })]
	public static KLV readKLFromBuffer(ByteBuffer buffer, long baseOffset)
	{
		if (buffer.remaining() < 17)
		{
			return null;
		}
		long offset = baseOffset + buffer.position();
		UL ul = UL.read(buffer);
		long len = BER.decodeLengthBuf(buffer);
		KLV result = new KLV(ul, len, offset, baseOffset + buffer.position());
		
		return result;
	}
}
