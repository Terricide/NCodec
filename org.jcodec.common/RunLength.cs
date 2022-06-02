using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.common;

public abstract class RunLength : Object
{
	public class Integer : RunLength
	{
		private const int MIN_VALUE = int.MinValue;

		private int lastValue;

		private int count;

		private IntArrayList values;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 130, 103 })]
		public virtual int[] getValues()
		{
			finish();
			int[] result = values.toArray();
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 129, 66, 233, 59, 232, 70, 108, 108 })]
		public Integer()
		{
			count = 0;
			lastValue = int.MinValue;
			values = IntArrayList.createIntArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 128, 130, 119, 110, 114, 114, 136, 136, 111 })]
		public virtual void add(int value)
		{
			if (lastValue == int.MinValue || lastValue != value)
			{
				if (lastValue != int.MinValue)
				{
					values.add(lastValue);
					counts.add(count);
					count = 0;
				}
				lastValue = value;
			}
			count++;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 124, 162, 110, 114, 114, 108, 136 })]
		protected internal override void finish()
		{
			if (lastValue != int.MinValue)
			{
				values.add(lastValue);
				counts.add(count);
				lastValue = int.MinValue;
				count = 0;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 121, 66, 104, 104, 104, 105, 99, 109, 103,
			106, 105, 108, 101, 141, 109, 236, 55, 240, 75,
			105
		})]
		public virtual void serialize(ByteBuffer bb)
		{
			ByteBuffer dup = bb.duplicate();
			int[] counts = getCounts();
			int[] values = getValues();
			NIOUtils.skip(bb, 4);
			int recCount = 0;
			int i = 0;
			while (i < (nint)counts.LongLength)
			{
				int count;
				for (count = counts[i]; count >= 256; count += -256)
				{
					bb.put(byte.MaxValue);
					bb.putInt(values[i]);
					recCount++;
				}
				bb.put((byte)(sbyte)(count - 1));
				bb.putInt(values[i]);
				i++;
				recCount++;
			}
			dup.putInt(recCount);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 116, 66, 103, 104, 103, 113, 105, 109, 238,
			60, 231, 70
		})]
		public static Integer parse(ByteBuffer bb)
		{
			Integer rl = new Integer();
			int recCount = bb.getInt();
			for (int i = 0; i < recCount; i++)
			{
				int count = ((sbyte)bb.get() & 0xFF) + 1;
				int value = bb.getInt();
				rl.counts.add(count);
				rl.values.add(value);
			}
			return rl;
		}

		[LineNumberTable(116)]
		protected internal override int recSize()
		{
			return 5;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 112, 66, 104, 99, 104, 39, 167, 104, 105,
			109, 108, 42, 47, 201
		})]
		public virtual int[] flattern()
		{
			int[] counts = getCounts();
			int total = 0;
			for (int j = 0; j < (nint)counts.LongLength; j++)
			{
				total += counts[j];
			}
			int[] values = getValues();
			int[] result = new int[total];
			int i = 0;
			int ind = 0;
			for (; i < (nint)counts.LongLength; i++)
			{
				int k = 0;
				while (k < counts[i])
				{
					result[ind] = values[i];
					k++;
					ind++;
				}
			}
			return result;
		}
	}

	public class Long : RunLength
	{
		private const long MIN_VALUE = long.MinValue;

		private long lastValue;

		private int count;

		private LongArrayList values;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 102, 130, 103 })]
		public override int[] getCounts()
		{
			finish();
			int[] result = counts.toArray();
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 101, 162, 103 })]
		public virtual long[] getValues()
		{
			finish();
			long[] result = values.toArray();
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 106, 66, 233, 59, 232, 70, 112, 108 })]
		public Long()
		{
			count = 0;
			lastValue = long.MinValue;
			values = LongArrayList.createLongArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 105, 130, 123, 114, 114, 114, 136, 136, 111 })]
		public virtual void add(long value)
		{
			if (lastValue == long.MinValue || lastValue != value)
			{
				if (lastValue != long.MinValue)
				{
					values.add(lastValue);
					counts.add(count);
					count = 0;
				}
				lastValue = value;
			}
			count++;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 99, 66, 114, 114, 114, 112, 136 })]
		protected internal override void finish()
		{
			if (lastValue != long.MinValue)
			{
				values.add(lastValue);
				counts.add(count);
				lastValue = long.MinValue;
				count = 0;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 97, 98, 104, 104, 104, 105, 99, 109, 103,
			106, 105, 108, 101, 141, 109, 236, 55, 240, 75,
			105
		})]
		public virtual void serialize(ByteBuffer bb)
		{
			ByteBuffer dup = bb.duplicate();
			int[] counts = getCounts();
			long[] values = getValues();
			NIOUtils.skip(bb, 4);
			int recCount = 0;
			int i = 0;
			while (i < (nint)counts.LongLength)
			{
				int count;
				for (count = counts[i]; count >= 256; count += -256)
				{
					bb.put(byte.MaxValue);
					bb.putLong(values[i]);
					recCount++;
				}
				bb.put((byte)(sbyte)(count - 1));
				bb.putLong(values[i]);
				i++;
				recCount++;
			}
			dup.putInt(recCount);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 92, 98, 103, 104, 103, 113, 105, 109, 238,
			60, 231, 70
		})]
		public static Long parse(ByteBuffer bb)
		{
			Long rl = new Long();
			int recCount = bb.getInt();
			for (int i = 0; i < recCount; i++)
			{
				int count = ((sbyte)bb.get() & 0xFF) + 1;
				long value = bb.getLong();
				rl.counts.add(count);
				rl.values.add(value);
			}
			return rl;
		}

		[LineNumberTable(214)]
		protected internal override int recSize()
		{
			return 9;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 88, 130, 104, 99, 104, 39, 167, 104, 105,
			109, 108, 42, 47, 201
		})]
		public virtual long[] flattern()
		{
			int[] counts = getCounts();
			int total = 0;
			for (int j = 0; j < (nint)counts.LongLength; j++)
			{
				total += counts[j];
			}
			long[] values = getValues();
			long[] result = new long[total];
			int i = 0;
			int ind = 0;
			for (; i < (nint)counts.LongLength; i++)
			{
				int k = 0;
				while (k < counts[i])
				{
					result[ind] = values[i];
					k++;
					ind++;
				}
			}
			return result;
		}
	}

	protected internal IntArrayList counts;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 103 })]
	public virtual int[] getCounts()
	{
		finish();
		int[] result = counts.toArray();
		
		return result;
	}

	protected internal abstract int recSize();

	protected internal abstract void finish();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 105, 108 })]
	public RunLength()
	{
		counts = IntArrayList.createIntArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 104, 99, 104, 101, 105, 101, 235,
		60, 235, 71
	})]
	public virtual int estimateSize()
	{
		int[] counts = getCounts();
		int recCount = 0;
		int i = 0;
		while (i < (nint)counts.LongLength)
		{
			for (int count = counts[i]; count >= 256; count += -256)
			{
				recCount++;
			}
			i++;
			recCount++;
		}
		return recCount * recSize() + 4;
	}
}
