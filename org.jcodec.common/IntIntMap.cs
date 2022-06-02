using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class IntIntMap : Object
{
	private const int GROW_BY = 128;

	private const int MIN_VALUE = int.MinValue;

	private int[] storage;

	private int _size;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 162, 109, 111, 112, 16, 199 })]
	public virtual int[] values()
	{
		int[] result = createArray(_size);
		int i = 0;
		int r = 0;
		for (; i < (nint)storage.LongLength; i++)
		{
			if (storage[i] != int.MinValue)
			{
				int num = r;
				r++;
				result[num] = storage[i];
			}
		}
		return result;
	}

	[LineNumberTable(84)]
	private static int[] createArray(int size)
	{
		return new int[size];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 113, 115 })]
	public IntIntMap()
	{
		storage = createArray(128);
		Arrays.fill(storage, int.MinValue);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 105, 145, 107, 110, 118, 117, 136,
		112, 111, 106
	})]
	public virtual void put(int key, int val)
	{
		if (val == int.MinValue)
		{
			
			throw new IllegalArgumentException("This implementation can not store -2147483648");
		}
		if ((nint)storage.LongLength <= key)
		{
			int[] ns = createArray(key + 128);
			ByteCodeHelper.arraycopy_primitive_4(storage, 0, ns, 0, storage.Length);
			Arrays.fill(ns, storage.Length, ns.Length, int.MinValue);
			storage = ns;
		}
		if (storage[key] == int.MinValue)
		{
			_size++;
		}
		storage[key] = val;
	}

	[LineNumberTable(42)]
	public virtual int get(int key)
	{
		return (key < (nint)storage.LongLength) ? storage[key] : int.MinValue;
	}

	[LineNumberTable(46)]
	public virtual bool contains(int key)
	{
		return (key >= 0 && key < (nint)storage.LongLength) ? true : false;
	}

	[LineNumberTable(new byte[] { 159, 130, 130, 109, 111, 112, 9, 199 })]
	public virtual int[] keys()
	{
		int[] result = new int[_size];
		int i = 0;
		int r = 0;
		for (; i < (nint)storage.LongLength; i++)
		{
			if (storage[i] != int.MinValue)
			{
				int num = r;
				r++;
				result[num] = i;
			}
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 128, 162, 109, 46, 135, 104 })]
	public virtual void clear()
	{
		for (int i = 0; i < (nint)storage.LongLength; i++)
		{
			storage[i] = int.MinValue;
		}
		_size = 0;
	}

	[LineNumberTable(65)]
	public virtual int size()
	{
		return _size;
	}

	[LineNumberTable(new byte[] { 159, 125, 98, 112, 111, 110 })]
	public virtual void remove(int key)
	{
		if (storage[key] != int.MinValue)
		{
			_size--;
		}
		storage[key] = int.MinValue;
	}
}
