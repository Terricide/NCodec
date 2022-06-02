using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.common;

public class DictionaryCompressor : Object
{
	public class Int : DictionaryCompressor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 113, 66, 106, 103, 103, 104, 101, 8, 199 })]
		private RunLength.Integer getValueStats(int[] values)
		{
			int[] copy = Platform.copyOfInt(values, values.Length);
			Arrays.sort(copy);
			RunLength.Integer rl = new RunLength.Integer();
			for (int i = 0; i < (nint)copy.LongLength; i++)
			{
				int j = copy[i];
				rl.add(j);
			}
			return rl;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(86)]
		public Int()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 120, 66, 105, 104, 104, 120, 107, 106, 106,
			107, 107, 110, 113, 236, 61, 233, 70, 105, 109,
			103, 106, 105, 108, 105, 236, 60, 9, 236, 73,
			106
		})]
		public virtual void compress(int[] values, ByteBuffer bb)
		{
			RunLength.Integer rl = getValueStats(values);
			int[] counts = rl.getCounts();
			int[] keys = rl.getValues();
			int esc = Math.max(1, (1 << MathUtil.log2(counts.Length) - 2) - 1);
			VLC vlc = buildCodes(counts, esc);
			int[] codes = vlc.getCodes();
			int[] codeSizes = vlc.getCodeSizes();
			bb.putInt(codes.Length);
			for (int j = 0; j < (nint)codes.LongLength; j++)
			{
				bb.put((byte)(sbyte)codeSizes[j]);
				bb.putShort((short)((uint)codes[j] >> 16));
				bb.putInt(keys[j]);
			}
			BitWriter br = new BitWriter(bb);
			for (int k = 0; k < (nint)values.LongLength; k++)
			{
				int l = values[k];
				for (int i = 0; i < (nint)keys.LongLength; i++)
				{
					if (keys[i] == l)
					{
						vlc.writeVLC(br, i);
						if (codes[i] == esc)
						{
							br.writeNBit(i, 16);
						}
					}
				}
			}
			br.flush();
		}
	}

	public class Long : DictionaryCompressor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 124, 162, 106, 103, 103, 104, 101, 8, 199 })]
		private RunLength.Long getValueStats(long[] values)
		{
			long[] copy = Platform.copyOfLong(values, values.Length);
			Arrays.sort(copy);
			RunLength.Long rl = new RunLength.Long();
			for (int i = 0; i < (nint)copy.LongLength; i++)
			{
				long j = copy[i];
				rl.add(j);
			}
			return rl;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(46)]
		public Long()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 130, 66, 105, 104, 104, 110, 105, 105, 107,
			107, 110, 113, 236, 61, 233, 70, 105, 109, 103,
			106, 105, 107, 106, 236, 60, 9, 236, 73, 106
		})]
		public virtual void compress(long[] values, ByteBuffer bb)
		{
			RunLength.Long rl = getValueStats(values);
			int[] counts = rl.getCounts();
			long[] keys = rl.getValues();
			VLC vlc = buildCodes(counts, (int)((nint)values.LongLength / 10));
			int[] codes = vlc.getCodes();
			int[] codeSizes = vlc.getCodeSizes();
			bb.putInt(codes.Length);
			for (int j = 0; j < (nint)codes.LongLength; j++)
			{
				bb.put((byte)(sbyte)codeSizes[j]);
				bb.putShort((short)((uint)codes[j] >> 16));
				bb.putLong(keys[j]);
			}
			BitWriter br = new BitWriter(bb);
			for (int k = 0; k < (nint)values.LongLength; k++)
			{
				long l = values[k];
				for (int i = 0; i < (nint)keys.LongLength; i++)
				{
					if (keys[i] == l)
					{
						vlc.writeVLC(br, i);
						if (codes[i] == 15)
						{
							br.writeNBit(16, i);
						}
					}
				}
			}
			br.flush();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(20)]
	public DictionaryCompressor()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 105, 105, 99, 111, 99, 106, 106,
		4, 233, 69, 101, 112, 233, 55, 234, 75, 105,
		106, 104, 102, 231, 61, 233, 70
	})]
	protected internal virtual VLC buildCodes(int[] counts, int esc)
	{
		int[] codes = new int[(nint)counts.LongLength];
		int[] codeSizes = new int[(nint)counts.LongLength];
		for (int code = 0; code < Math.min(codes.Length, esc); code++)
		{
			int max = 0;
			for (int j = 0; j < (nint)counts.LongLength; j++)
			{
				if (counts[j] > counts[max])
				{
					max = j;
				}
			}
			codes[max] = code;
			codeSizes[max] = Math.max(1, MathUtil.log2(code));
			counts[max] = int.MinValue;
		}
		int escSize = MathUtil.log2(esc);
		for (int i = 0; i < (nint)counts.LongLength; i++)
		{
			if (counts[i] >= 0)
			{
				codes[i] = esc;
				codeSizes[i] = escSize;
			}
		}
		VLC result = new VLC(codes, codeSizes);
		
		return result;
	}
}
