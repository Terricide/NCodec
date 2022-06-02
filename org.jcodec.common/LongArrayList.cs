using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class LongArrayList : Object
{
	private const int DEFAULT_GROW_AMOUNT = 128;

	private long[] storage;

	private int limit;

	private int start;

	private int growAmount;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 105, 104, 109 })]
	public LongArrayList(int growAmount)
	{
		this.growAmount = growAmount;
		storage = new long[growAmount];
	}

	[LineNumberTable(new byte[]
	{
		159, 133, 162, 114, 124, 127, 3, 104, 116, 136,
		124
	})]
	public virtual void add(long val)
	{
		if (limit > (nint)storage.LongLength - 1)
		{
			long[] ns = new long[(nint)storage.LongLength + growAmount - start];
			ByteCodeHelper.arraycopy_primitive_8(storage, start, ns, 0, (int)((nint)storage.LongLength - start));
			storage = ns;
			limit -= start;
			start = 0;
		}
		long[] array = storage;
		int num = limit;
		limit = num + 1;
		array[num] = val;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public static LongArrayList createLongArrayList()
	{
		LongArrayList result = new LongArrayList(128);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 134, 98, 116, 127, 2 })]
	public virtual long[] toArray()
	{
		long[] result = new long[limit - start];
		ByteCodeHelper.arraycopy_primitive_8(storage, start, result, 0, limit - start);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 106 })]
	public virtual void push(long id)
	{
		add(id);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 130, 111, 108 })]
	public virtual long pop()
	{
		if (limit <= start)
		{
			
			throw new IllegalStateException();
		}
		long[] array = storage;
		int num = limit;
		limit = num - 1;
		return array[num];
	}

	[LineNumberTable(new byte[] { 159, 127, 66, 114 })]
	public virtual void set(int index, int value)
	{
		storage[index + start] = value;
	}

	[LineNumberTable(64)]
	public virtual long get(int index)
	{
		return storage[index + start];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 66, 111, 108 })]
	public virtual long shift()
	{
		if (start >= limit)
		{
			
			throw new IllegalStateException();
		}
		long[] array = storage;
		int num = start;
		start = num + 1;
		return array[num];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 130, 107, 118, 127, 3, 136, 112, 115 })]
	public virtual void fill(int from, int to, int val)
	{
		if (to > (nint)storage.LongLength)
		{
			long[] ns = new long[to + growAmount - start];
			ByteCodeHelper.arraycopy_primitive_8(storage, start, ns, 0, (int)((nint)storage.LongLength - start));
			storage = ns;
		}
		Arrays.fill(storage, from, to, val);
		limit = Math.max(limit, to);
	}

	[LineNumberTable(84)]
	public virtual int size()
	{
		return limit - start;
	}

	[LineNumberTable(new byte[] { 159, 120, 66, 115, 126, 122, 136, 118, 112 })]
	public virtual void addAll(long[] other)
	{
		if (limit + (nint)other.LongLength >= (nint)storage.LongLength)
		{
			long[] ns = new long[limit + growAmount + (nint)other.LongLength - start];
			ByteCodeHelper.arraycopy_primitive_8(storage, start, ns, 0, limit);
			storage = ns;
		}
		ByteCodeHelper.arraycopy_primitive_8(other, 0, storage, limit, other.Length);
		limit = (int)(limit + (nint)other.LongLength);
	}

	[LineNumberTable(new byte[] { 159, 118, 130, 104, 104 })]
	public virtual void clear()
	{
		limit = 0;
		start = 0;
	}

	[LineNumberTable(new byte[] { 159, 117, 162, 113, 108, 3, 167 })]
	public virtual bool contains(long needle)
	{
		for (int i = start; i < limit; i++)
		{
			if (storage[i] == needle)
			{
				return true;
			}
		}
		return false;
	}
}
