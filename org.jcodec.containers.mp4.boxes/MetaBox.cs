using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;

namespace org.jcodec.containers.mp4.boxes;

public class MetaBox : NodeBox
{
	private const string FOURCC = "meta";

	[LineNumberTable(160)]
	public static string fourcc()
	{
		return "meta";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 106 })]
	public MetaBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;)Lorg/jcodec/containers/mp4/boxes/DataBox;")]
	[LineNumberTable(new byte[] { 159, 128, 66, 124, 105, 136, 99 })]
	private DataBox getDataBox(List value)
	{
		Iterator iterator = value.iterator();
		while (iterator.hasNext())
		{
			Box box = (Box)iterator.next();
			if (box is DataBox)
			{
				return (DataBox)box;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;Ljava/lang/String;)V")]
	[LineNumberTable(new byte[] { 159, 105, 130, 104, 105, 109, 111, 135, 99 })]
	private void dropChildBox(List children, string fourcc2)
	{
		ListIterator listIterator = children.listIterator();
		while (listIterator.hasNext())
		{
			Box next = (Box)listIterator.next();
			if (String.instancehelper_equals(fourcc2, next.getFourcc()))
			{
				listIterator.remove();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(28)]
	public static MetaBox createMetaBox()
	{
		MetaBox result = new MetaBox(Header.createHeader(fourcc(), 0L));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 135, 119, 159, 9, 104, 131, 127,
		11, 111, 101, 99, 117, 101, 99, 125, 118, 154,
		102
	})]
	public virtual Map getKeyedMeta()
	{
		LinkedHashMap result = new LinkedHashMap();
		IListBox ilst = (IListBox)NodeBox.findFirst(this, ClassLiteral<IListBox>.Value, IListBox.fourcc());
		MdtaBox[] keys = (MdtaBox[])NodeBox.findAllPath(this, ClassLiteral<MdtaBox>.Value, new string[2]
		{
			KeysBox.fourcc(),
			MdtaBox.fourcc()
		});
		if (ilst == null || keys.Length == 0)
		{
			return result;
		}
		Iterator iterator = ilst.getValues().entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			Integer index = (Integer)entry.getKey();
			if (index == null)
			{
				continue;
			}
			DataBox db = getDataBox((List)entry.getValue());
			if (db != null)
			{
				MetaValue value = MetaValue.createOtherWithLocale(db.getType(), db.getLocale(), db.getData());
				if (index.intValue() > 0 && index.intValue() <= (nint)keys.LongLength)
				{
					((Map)result).put((object)keys[index.intValue() - 1].getKey(), (object)value);
				}
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 135, 151, 100, 131, 127, 10, 110,
		101, 99, 116, 101, 99, 125, 108, 102
	})]
	public virtual Map getItunesMeta()
	{
		LinkedHashMap result = new LinkedHashMap();
		IListBox ilst = (IListBox)NodeBox.findFirst(this, ClassLiteral<IListBox>.Value, IListBox.fourcc());
		if (ilst == null)
		{
			return result;
		}
		Iterator iterator = ilst.getValues().entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			Integer index = (Integer)entry.getKey();
			if (index != null)
			{
				DataBox db = getDataBox((List)entry.getValue());
				if (db != null)
				{
					MetaValue value = MetaValue.createOtherWithLocale(db.getType(), db.getLocale(), db.getData());
					((Map)result).put((object)index, (object)value);
				}
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 105, 98, 103, 103, 99, 127, 6,
		120, 111, 104, 127, 4, 112, 101, 102, 105, 104,
		107
	})]
	public virtual void setKeyedMeta(Map map)
	{
		if (!map.isEmpty())
		{
			KeysBox keys = KeysBox.createKeysBox();
			LinkedHashMap data = new LinkedHashMap();
			int i = 1;
			Iterator iterator = map.entrySet().iterator();
			while (iterator.hasNext())
			{
				Map.Entry entry = (Map.Entry)iterator.next();
				keys.add(MdtaBox.createMdtaBox((string)entry.getKey()));
				MetaValue v = (MetaValue)entry.getValue();
				ArrayList children = new ArrayList();
				((List)children).add((object)DataBox.createDataBox(v.getType(), v.getLocale(), v.getData()));
				((Map)data).put((object)Integer.valueOf(i), (object)children);
				i++;
			}
			IListBox ilst = IListBox.createIListBox(data);
			replaceBox(keys);
			replaceBox(ilst);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 116, 98, 105, 98, 103, 104, 151, 100, 140,
		168, 127, 11, 116, 117, 101, 125, 120, 117, 143,
		198, 127, 9, 116, 111, 125, 104, 127, 13, 107,
		166, 127, 7, 111, 127, 2, 127, 2, 131, 127,
		3
	})]
	public virtual void setItunesMeta(Map map)
	{
		if (map.isEmpty())
		{
			return;
		}
		LinkedHashMap copy = new LinkedHashMap();
		((Map)copy).putAll(map);
		IListBox ilst = (IListBox)NodeBox.findFirst(this, ClassLiteral<IListBox>.Value, IListBox.fourcc());
		object data;
		if (ilst == null)
		{
			data = new LinkedHashMap();
		}
		else
		{
			data = ilst.getValues();
			Iterator iterator = ((Map)data).entrySet().iterator();
			while (iterator.hasNext())
			{
				Map.Entry entry2 = (Map.Entry)iterator.next();
				int index2 = ((Integer)entry2.getKey()).intValue();
				MetaValue v2 = (MetaValue)((Map)copy).get((object)Integer.valueOf(index2));
				if (v2 != null)
				{
					DataBox dataBox2 = DataBox.createDataBox(v2.getType(), v2.getLocale(), v2.getData());
					dropChildBox((List)entry2.getValue(), DataBox.fourcc());
					((List)entry2.getValue()).add(dataBox2);
					((Map)copy).remove((object)Integer.valueOf(index2));
				}
			}
		}
		Iterator iterator2 = ((Map)copy).entrySet().iterator();
		while (iterator2.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator2.next();
			int index = ((Integer)entry.getKey()).intValue();
			MetaValue v = (MetaValue)entry.getValue();
			DataBox dataBox = DataBox.createDataBox(v.getType(), v.getLocale(), v.getData());
			ArrayList children = new ArrayList();
			object obj = data;
			Integer integer = Integer.valueOf(index);
			object value = children;
			object key = integer;
			((obj == null) ? null : ((obj as Map) ?? throw new java.lang.IncompatibleClassChangeError())).put(key, value);
			((List)children).add((object)dataBox);
		}
		
		object obj2 = data;
		HashSet keySet = new HashSet(((obj2 == null) ? null : ((obj2 as Map) ?? throw new java.lang.IncompatibleClassChangeError())).keySet());
		((Set)keySet).removeAll((Collection)map.keySet());
		Iterator iterator3 = ((Set)keySet).iterator();
		while (iterator3.hasNext())
		{
			Integer dropped = (Integer)iterator3.next();
			object obj3 = data;
			object key = dropped;
			((obj3 == null) ? null : ((obj3 as Map) ?? throw new java.lang.IncompatibleClassChangeError())).remove(key);
		}
		object obj4 = data;
		replaceBox(IListBox.createIListBox((obj4 == null) ? null : ((obj4 as Map) ?? throw new java.lang.IncompatibleClassChangeError())));
	}
}
