using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.mkv.util;

public class EbmlUtil : Object
{
	internal static byte[] ___003C_003ElengthOptions;

	public const long one = 127L;

	public const long two = 16256L;

	public const long three = 2080768L;

	public const long four = 266338304L;

	public const long five = 34091302912L;

	public const long six = 4363686772736L;

	public const long seven = 558551906910208L;

	public const long eight = 71494644084506624L;

	internal static long[] ___003C_003EebmlLengthMasks;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] lengthOptions
	{
		[HideFromJava]
		get
		{
			return ___003C_003ElengthOptions;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static long[] ebmlLengthMasks
	{
		[HideFromJava]
		get
		{
			return ___003C_003EebmlLengthMasks;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 120, 66, 106, 131, 131, 114, 135 })]
	public static int ebmlLength(long v)
	{
		if (v == 0u)
		{
			return 1;
		}
		int length;
		for (length = 8; length > 0 && (v & ___003C_003EebmlLengthMasks[length]) == 0u; length += -1)
		{
		}
		return length;
	}

	[LineNumberTable(new byte[] { 159, 137, 162, 104, 135, 25, 231, 69, 121 })]
	public static byte[] ebmlEncodeLen(long value, int length)
	{
		byte[] b = new byte[length];
		for (int idx = 0; idx < length; idx++)
		{
			b[length - idx - 1] = (byte)(sbyte)(((ulong)value >> 8 * idx) & 0xFFu);
		}
		int num = 0;
		byte[] array = b;
		array[num] = (byte)(sbyte)(array[num] | (128u >> length - 1));
		return b;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	public static byte[] ebmlEncode(long value)
	{
		byte[] result = ebmlEncodeLen(value, ebmlLength(value));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 66, 103, 113, 63, 9, 135 })]
	public static string toHexString(byte[] a)
	{
		StringBuilder sb = new StringBuilder();
		int num = a.Length;
		for (int i = 0; i < num; i++)
		{
			int b = a[i];
			sb.append(String.format("0x%02x ", Integer.valueOf(b & 0xFF)));
		}
		string result = sb.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 97, 68, 100, 145, 99, 108, 135 })]
	public static int computeLength(byte b)
	{
		int b2 = (sbyte)b;
		if (b2 == 0)
		{
			
			throw new RuntimeException("Invalid head element for ebml sequence");
		}
		int i;
		for (i = 1; (b2 & ___003C_003ElengthOptions[i]) == 0; i++)
		{
		}
		return i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public EbmlUtil()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		131,
		66,
		byte.MaxValue,
		22,
		98
	})]
	static EbmlUtil()
	{
		___003C_003ElengthOptions = new byte[9] { 0, 128, 64, 32, 16, 8, 4, 2, 1 };
		___003C_003EebmlLengthMasks = new long[9] { 0L, 127L, 16256L, 2080768L, 266338304L, 34091302912L, 4363686772736L, 558551906910208L, 71494644084506624L };
	}
}
