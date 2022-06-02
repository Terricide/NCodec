using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.vpx;

public class VPXBooleanDecoder : Object
{
	internal int bit_count;

	internal ByteBuffer input;

	internal int offset;

	internal int range;

	internal int value;

	internal long callCounter;

	private string debugName;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 233, 61, 201, 104, 104, 105 })]
	public VPXBooleanDecoder(ByteBuffer input, int offset)
	{
		callCounter = 0L;
		this.input = input;
		this.offset = offset;
		initBoolDecoder();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(46)]
	public virtual int readBitEq()
	{
		int result = readBit(128);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 98, 99, 106, 115 })]
	public virtual int decodeInt(int sizeInBits)
	{
		int v = 0;
		while (true)
		{
			int num = sizeInBits;
			sizeInBits += -1;
			if (num <= 0)
			{
				break;
			}
			v = (v << 1) | readBit(128);
		}
		return v;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 99, 104, 104, 107, 134, 240, 70,
		131, 102, 106, 102, 163, 105, 106, 105, 105, 136,
		102, 157, 111, 167, 105, 104, 104
	})]
	public virtual int readBit(int probability)
	{
		int bit = 0;
		int range = this.range;
		int value = this.value;
		int split = 1 + ((range - 1) * probability >> 8);
		int bigsplit = split << 8;
		callCounter++;
		range = split;
		if (value >= bigsplit)
		{
			range = this.range - range;
			value -= bigsplit;
			bit = 1;
		}
		int count = bit_count;
		int shift = leadingZeroCountInByte((byte)(sbyte)range);
		range <<= shift;
		value <<= shift;
		count -= shift;
		if (count <= 0)
		{
			value |= ((sbyte)input.get() & 0xFF) << -count;
			offset++;
			count += 8;
		}
		bit_count = count;
		this.value = value;
		this.range = range;
		return bit;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 66, 227, 72, 151 })]
	public virtual int readTree(int[] tree, int[] probability)
	{
		int i = 0;
		while ((i = tree[i + readBit(probability[i >> 1])]) > 0)
		{
		}
		return -i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 168, 155, 143, 108, 104 })]
	internal virtual void initBoolDecoder()
	{
		value = 0;
		value = ((sbyte)input.get() & 0xFF) << 8;
		offset++;
		range = 255;
		bit_count = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 91, 129, 68, 105, 108, 131 })]
	public static int leadingZeroCountInByte(byte b)
	{
		int b2 = (sbyte)b;
		int i = b2 & 0xFF;
		if (i >= 128 || i == 0)
		{
			return 0;
		}
		return Integer.numberOfLeadingZeros(b2) - 24;
	}

	[LineNumberTable(new byte[] { 159, 94, 66, 101, 101 })]
	public static int getBitInBytes(byte[] bs, int i)
	{
		int byteIndex = i >> 3;
		int bitIndex = i & 7;
		return (bs[byteIndex] >> 7 - bitIndex) & 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 233, 52, 233, 77 })]
	protected internal VPXBooleanDecoder()
	{
		callCounter = 0L;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 66, 99, 113, 147 })]
	public virtual int readTree3(int[] tree, int prob0, int prob1)
	{
		int i = 0;
		if ((i = tree[i + readBit(prob0)]) > 0)
		{
			while ((i = tree[i + readBit(prob1)]) > 0)
			{
			}
		}
		return -i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 130, 165, 151 })]
	public virtual int readTreeSkip(int[] t, int[] p, int skip_branches)
	{
		int i = skip_branches * 2;
		while ((i = t[i + readBit(p[i >> 1])]) > 0)
		{
		}
		return -i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 66, 115 })]
	public virtual void seek()
	{
		input.position(offset);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(188)]
	public override string toString()
	{
		string result = new StringBuilder().append("bc: ").append(value).toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 93, 130, 99, 103, 47, 167 })]
	public static int getBitsInBytes(byte[] bytes, int idx, int len)
	{
		int val = 0;
		for (int i = 0; i < len; i++)
		{
			val = (val << 1) | getBitInBytes(bytes, idx + i);
		}
		return val;
	}
}
