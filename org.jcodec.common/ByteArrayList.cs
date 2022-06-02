using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class ByteArrayList : Object
{
	private const int DEFAULT_GROW_AMOUNT = 2048;

	private byte[] storage;

	private int _size;

	private int growAmount;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 104, 109 })]
	public ByteArrayList(int growAmount)
	{
		this.growAmount = growAmount;
		storage = new byte[growAmount];
	}

	[LineNumberTable(new byte[] { 159, 133, 97, 68, 112, 117, 118, 136, 124 })]
	public virtual void add(byte val)
	{
		int val2 = (sbyte)val;
		if (_size >= (nint)storage.LongLength)
		{
			byte[] ns = new byte[(nint)storage.LongLength + growAmount];
			ByteCodeHelper.arraycopy_primitive_1(storage, 0, ns, 0, storage.Length);
			storage = ns;
		}
		byte[] array = storage;
		int num = _size;
		_size = num + 1;
		array[num] = (byte)val2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public static ByteArrayList createByteArrayList()
	{
		ByteArrayList result = new ByteArrayList(2048);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 135, 162, 109, 117 })]
	public virtual byte[] toArray()
	{
		byte[] result = new byte[_size];
		ByteCodeHelper.arraycopy_primitive_1(storage, 0, result, 0, _size);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 129, 68, 106 })]
	public virtual void push(byte id)
	{
		int id2 = (sbyte)id;
		add((byte)id2);
	}

	[LineNumberTable(new byte[] { 159, 130, 130, 105, 98, 111 })]
	public virtual void pop()
	{
		if (_size != 0)
		{
			_size--;
		}
	}

	[LineNumberTable(new byte[] { 159, 128, 65, 68, 106 })]
	public virtual void set(int index, byte value)
	{
		int value2 = (sbyte)value;
		storage[index] = (byte)value2;
	}

	[LineNumberTable(60)]
	public virtual byte get(int index)
	{
		return storage[index];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 65, 68, 107, 111, 118, 136, 111, 115 })]
	public virtual void fill(int start, int end, byte val)
	{
		int val2 = (sbyte)val;
		if (end > (nint)storage.LongLength)
		{
			byte[] ns = new byte[end + growAmount];
			ByteCodeHelper.arraycopy_primitive_1(storage, 0, ns, 0, storage.Length);
			storage = ns;
		}
		Arrays.fill(storage, start, end, (byte)val2);
		_size = Math.max(_size, end);
	}

	[LineNumberTable(74)]
	public virtual int size()
	{
		return _size;
	}

	[LineNumberTable(new byte[] { 159, 123, 130, 115, 119, 117, 136, 118, 112 })]
	public virtual void addAll(byte[] other)
	{
		if (_size + (nint)other.LongLength >= (nint)storage.LongLength)
		{
			byte[] ns = new byte[_size + growAmount + (nint)other.LongLength];
			ByteCodeHelper.arraycopy_primitive_1(storage, 0, ns, 0, _size);
			storage = ns;
		}
		ByteCodeHelper.arraycopy_primitive_1(other, 0, storage, _size, other.Length);
		_size = (int)(_size + (nint)other.LongLength);
	}

	[LineNumberTable(new byte[] { 159, 120, 65, 68, 108, 108, 3, 167 })]
	public virtual bool contains(byte needle)
	{
		int needle2 = (sbyte)needle;
		for (int i = 0; i < _size; i++)
		{
			if (storage[i] == needle2)
			{
				return true;
			}
		}
		return false;
	}
}
