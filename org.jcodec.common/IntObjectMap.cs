using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.lang.reflect;
using org.jcodec.platform;

namespace org.jcodec.common;

[Signature("<T:Ljava/lang/Object;>Ljava/lang/Object;")]
public class IntObjectMap : Object
{
	private const int GROW_BY = 128;

	private object[] storage;

	private int _size;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 113 })]
	public IntObjectMap()
	{
		storage = new object[128];
	}

	[Signature("(ITT;)V")]
	[LineNumberTable(new byte[] { 159, 136, 98, 107, 110, 118, 136, 107, 111, 106 })]
	public virtual void put(int key, object val)
	{
		if ((nint)storage.LongLength <= key)
		{
			object[] ns = new object[key + 128];
			ByteCodeHelper.arraycopy(storage, 0, ns, 0, storage.Length);
			storage = ns;
		}
		if (storage[key] == null)
		{
			_size++;
		}
		storage[key] = val;
	}

	[Signature("(I)TT;")]
	[LineNumberTable(37)]
	public virtual object get(int key)
	{
		return (key < (nint)storage.LongLength) ? storage[key] : null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("([TT;)[TT;")]
	[LineNumberTable(new byte[] { 159, 126, 162, 125, 111, 107, 16, 199 })]
	public virtual object[] values(object[] runtime)
	{
		object[] result = (object[])Array.newInstance(Platform.arrayComponentType(runtime), _size);
		int i = 0;
		int r = 0;
		for (; i < (nint)storage.LongLength; i++)
		{
			if (storage[i] != null)
			{
				int num = r;
				r++;
				result[num] = storage[i];
			}
		}
		return result;
	}

	[LineNumberTable(56)]
	public virtual int size()
	{
		return _size;
	}

	[LineNumberTable(new byte[] { 159, 132, 98, 109, 111, 107, 9, 199 })]
	public virtual int[] keys()
	{
		int[] result = new int[_size];
		int i = 0;
		int r = 0;
		for (; i < (nint)storage.LongLength; i++)
		{
			if (storage[i] != null)
			{
				int num = r;
				r++;
				result[num] = i;
			}
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 130, 130, 109, 42, 135, 104 })]
	public virtual void clear()
	{
		for (int i = 0; i < (nint)storage.LongLength; i++)
		{
			storage[i] = null;
		}
		_size = 0;
	}

	[LineNumberTable(new byte[] { 159, 127, 66, 107, 111, 106 })]
	public virtual void remove(int key)
	{
		if (storage[key] != null)
		{
			_size--;
		}
		storage[key] = null;
	}
}
