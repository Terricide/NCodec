using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.common.io;

public class VLC : Object
{
	private int[] codes;

	private int[] codeSizes;

	private int[] values;

	private int[] valueSizes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		116,
		130,
		105,
		106,
		106,
		103,
		107,
		139,
		106,
		102,
		116,
		138,
		101,
		byte.MaxValue,
		19,
		52,
		236,
		81
	})]
	public virtual int readVLC(BitReader _in)
	{
		int code = 0;
		int len = 0;
		int overall = 0;
		int total = 0;
		int i = 0;
		while (len == 0)
		{
			int @string = _in.checkNBit(8);
			int ind = @string + code;
			code = values[ind];
			len = valueSizes[ind];
			int bits = ((len == 0) ? 8 : len);
			total += bits;
			overall = (overall << bits) | (@string >> 8 - bits);
			_in.skip(bits);
			if (code == -1)
			{
				string message = new StringBuilder().append("Invalid code prefix ").append(binary(overall, (i << 3) + bits)).toString();
				
				throw new RuntimeException(message);
			}
			i++;
		}
		return code;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 130, 105, 104, 136, 105 })]
	public VLC(int[] codes, int[] codeSizes)
	{
		this.codes = codes;
		this.codeSizes = codeSizes;
		_invert();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 66, 127, 9 })]
	public virtual void writeVLC(BitWriter @out, int code)
	{
		@out.writeNBit((int)((uint)codes[code] >> 32 - codeSizes[code]), codeSizes[code]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 98, 104, 101, 106, 138, 100, 107, 106,
		148, 137
	})]
	public virtual int readVLC16(BitReader _in)
	{
		int @string = _in.check16Bits();
		int b = (int)((uint)@string >> 8);
		int code = values[b];
		int len = valueSizes[b];
		if (len == 0)
		{
			b = (@string & 0xFF) + code;
			code = values[b];
			_in.skipFast(8 + valueSizes[b]);
		}
		else
		{
			_in.skipFast(len);
		}
		return code;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 98, 103, 103, 104, 101, 123, 237, 61,
		231, 69, 116
	})]
	public static VLC createVLC(string[] codes)
	{
		IntArrayList _codes = IntArrayList.createIntArrayList();
		IntArrayList _codeSizes = IntArrayList.createIntArrayList();
		for (int i = 0; i < (nint)codes.LongLength; i++)
		{
			string @string = codes[i];
			_codes.add(Integer.parseInt(@string, 2) << 32 - String.instancehelper_length(@string));
			_codeSizes.add(String.instancehelper_length(@string));
		}
		return new VLC(_codes.toArray(), _codeSizes.toArray());
	}

	[LineNumberTable(157)]
	public virtual int[] getCodes()
	{
		return codes;
	}

	[LineNumberTable(161)]
	public virtual int[] getCodeSizes()
	{
		return codeSizes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 103, 103, 109, 109, 109 })]
	private void _invert()
	{
		IntArrayList values = IntArrayList.createIntArrayList();
		IntArrayList valueSizes = IntArrayList.createIntArrayList();
		invert(0, 0, 0, values, valueSizes);
		this.values = values.toArray();
		this.valueSizes = valueSizes.toArray();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 105, 107, 139, 101, 112, 127, 7,
		134, 116, 106, 109, 102, 113, 112, 17, 235, 69,
		111, 109, 241, 49, 234, 84
	})]
	private int invert(int startOff, int level, int prefix, IntArrayList values, IntArrayList valueSizes)
	{
		int tableEnd = startOff + 256;
		values.fill(startOff, tableEnd, -1);
		valueSizes.fill(startOff, tableEnd, 0);
		int prefLen = level << 3;
		for (int i = 0; i < (nint)codeSizes.LongLength; i++)
		{
			if (codeSizes[i] <= prefLen || (level > 0 && (uint)codes[i] >> 32 - prefLen != (uint)prefix))
			{
				continue;
			}
			int pref = (int)((uint)codes[i] >> 32 - prefLen - 8);
			int code = pref & 0xFF;
			int len = codeSizes[i] - prefLen;
			if (len <= 8)
			{
				for (int j = 0; j < 1 << 8 - len; j++)
				{
					values.set(startOff + code + j, i);
					valueSizes.set(startOff + code + j, len);
				}
			}
			else if (values.get(startOff + code) == -1)
			{
				values.set(startOff + code, tableEnd);
				tableEnd = invert(tableEnd, level + 1, pref, values, valueSizes);
			}
		}
		return tableEnd;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 66, 104, 103, 56, 167 })]
	private static string binary(int @string, int len)
	{
		char[] symb = new char[len];
		for (int i = 0; i < len; i++)
		{
			symb[i] = (((@string & (1 << len - i - 1)) == 0) ? '0' : '1');
		}
		string result = Platform.stringFromChars(symb);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 66, 111, 103, 110, 45, 135, 105 })]
	private static string extracted(int num)
	{
		string str = Integer.toString(num & 0xFF, 2);
		StringBuilder builder = new StringBuilder();
		for (int i = 0; i < 8 - String.instancehelper_length(str); i++)
		{
			builder.append("0");
		}
		builder.append(str);
		string result = builder.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 66, 109, 63, 59, 202 })]
	public virtual void printTable(PrintStream ps)
	{
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			ps.println(new StringBuilder().append(i).append(": ").append(extracted(i))
				.append(" (")
				.append(valueSizes[i])
				.append(") -> ")
				.append(values[i])
				.toString());
		}
	}
}
