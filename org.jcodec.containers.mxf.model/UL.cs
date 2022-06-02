using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.platform;

namespace org.jcodec.containers.mxf.model;

public class UL : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private byte[] bytes;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static char[] hex;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 162, 105, 105 })]
	public static UL read(ByteBuffer _bb)
	{
		byte[] umid = new byte[16];
		_bb.get(umid);
		UL result = new UL(umid);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 105, 104, 104 })]
	public UL(byte[] bytes)
	{
		Preconditions.checkNotNull(bytes);
		this.bytes = bytes;
	}

	[LineNumberTable(103)]
	public virtual int get(int i)
	{
		return bytes[i];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 66, 105, 131, 141, 116, 110, 3, 199 })]
	public override bool equals(object obj)
	{
		if (!(obj is UL))
		{
			return false;
		}
		byte[] other = ((UL)obj).bytes;
		for (int i = 4; i < Math.min(bytes.Length, other.Length); i++)
		{
			if (bytes[i] != other[i])
			{
				return false;
			}
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 105, 104, 40, 199 })]
	public static UL newULFromInts(int[] args)
	{
		byte[] bytes = new byte[(nint)args.LongLength];
		for (int i = 0; i < (nint)args.LongLength; i++)
		{
			bytes[i] = (byte)(sbyte)args[i];
		}
		UL result = new UL(bytes);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 104, 109, 105, 104, 108, 6, 199 })]
	public static UL newUL(string ul)
	{
		Preconditions.checkNotNull(ul);
		string[] split = StringUtils.splitS(ul, ".");
		byte[] b = new byte[(nint)split.LongLength];
		for (int i = 0; i < (nint)split.LongLength; i++)
		{
			int parseInt = Integer.parseInt(split[i], 16);
			b[i] = (byte)(sbyte)parseInt;
		}
		UL result = new UL(b);
		
		return result;
	}

	[LineNumberTable(55)]
	public override int hashCode()
	{
		return (bytes[4] << 24) | (bytes[5] << 16) | (bytes[6] << 8) | bytes[7];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 98, 100, 99, 104, 102, 116, 116, 3,
		204
	})]
	public virtual bool maskEquals(UL o, int mask)
	{
		if (o == null)
		{
			return false;
		}
		byte[] other = o.bytes;
		mask >>= 4;
		int i = 4;
		while (i < Math.min(bytes.Length, other.Length))
		{
			if ((mask & 1) == 1 && bytes[i] != other[i])
			{
				return false;
			}
			i++;
			mask >>= 1;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 112, 114, 99, 99, 111, 123, 121,
		234, 61, 234, 69, 123, 121
	})]
	public override string toString()
	{
		if (bytes.Length == 0)
		{
			return "";
		}
		char[] str = new char[(nint)bytes.LongLength * 3 - 1];
		int i = 0;
		int j = 0;
		for (i = 0; i < (nint)bytes.LongLength - 1; i++)
		{
			int num = j;
			j++;
			str[num] = hex[(bytes[i] >> 4) & 0xF];
			int num2 = j;
			j++;
			str[num2] = hex[bytes[i] & 0xF];
			int num3 = j;
			j++;
			str[num3] = '.';
		}
		int num4 = j;
		j++;
		str[num4] = hex[(bytes[i] >> 4) & 0xF];
		int num5 = j;
		j++;
		str[num5] = hex[bytes[i] & 0xF];
		string result = Platform.stringFromChars(str);
		
		return result;
	}

	[LineNumberTable(84)]
	static UL()
	{
		hex = new char[16]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
