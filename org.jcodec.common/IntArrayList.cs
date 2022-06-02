using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class IntArrayList : Object
{
	private const int DEFAULT_GROW_AMOUNT = 128;

	private int[] storage;

	private int _size;

	private int growAmount;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 104, 109 })]
	public IntArrayList(int growAmount)
	{
		this.growAmount = growAmount;
		storage = new int[growAmount];
	}

	[LineNumberTable(new byte[] { 159, 128, 66, 106 })]
	public virtual void set(int index, int value)
	{
		storage[index] = value;
	}

	[LineNumberTable(new byte[] { 159, 133, 98, 112, 117, 118, 136, 124 })]
	public virtual void add(int val)
	{
		if (_size >= (nint)storage.LongLength)
		{
			int[] ns = new int[(nint)storage.LongLength + growAmount];
			ByteCodeHelper.arraycopy_primitive_4(storage, 0, ns, 0, storage.Length);
			storage = ns;
		}
		int[] array = storage;
		int num = _size;
		_size = num + 1;
		array[num] = val;
	}

	[LineNumberTable(new byte[] { 159, 135, 162, 109, 117 })]
	public virtual int[] toArray()
	{
		int[] result = new int[_size];
		ByteCodeHelper.arraycopy_primitive_4(storage, 0, result, 0, _size);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	public static IntArrayList createIntArrayList()
	{
		IntArrayList result = new IntArrayList(128);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 106 })]
	public virtual void push(int id)
	{
		add(id);
	}

	[LineNumberTable(new byte[] { 159, 130, 130, 105, 98, 111 })]
	public virtual void pop()
	{
		if (_size != 0)
		{
			_size--;
		}
	}

	[LineNumberTable(60)]
	public virtual int get(int index)
	{
		return storage[index];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 66, 107, 111, 118, 136, 111, 115 })]
	public virtual void fill(int start, int end, int val)
	{
		if (end > (nint)storage.LongLength)
		{
			int[] ns = new int[end + growAmount];
			ByteCodeHelper.arraycopy_primitive_4(storage, 0, ns, 0, storage.Length);
			storage = ns;
		}
		Arrays.fill(storage, start, end, val);
		_size = Math.max(_size, end);
	}

	[LineNumberTable(74)]
	public virtual int size()
	{
		return _size;
	}

	[LineNumberTable(new byte[] { 159, 123, 130, 115, 119, 117, 136, 118, 112 })]
	public virtual void addAll(int[] other)
	{
		if (_size + (nint)other.LongLength >= (nint)storage.LongLength)
		{
			int[] ns = new int[_size + growAmount + (nint)other.LongLength];
			ByteCodeHelper.arraycopy_primitive_4(storage, 0, ns, 0, _size);
			storage = ns;
		}
		ByteCodeHelper.arraycopy_primitive_4(other, 0, storage, _size, other.Length);
		_size = (int)(_size + (nint)other.LongLength);
	}

	[LineNumberTable(new byte[] { 159, 120, 66, 104 })]
	public virtual void clear()
	{
		_size = 0;
	}

	[LineNumberTable(new byte[] { 159, 119, 66, 108, 108, 3, 167 })]
	public virtual bool contains(int needle)
	{
		for (int i = 0; i < _size; i++)
		{
			if (storage[i] == needle)
			{
				return true;
			}
		}
		return false;
	}
}
