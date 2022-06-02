using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class Tuple : Object
{
	[Signature("<T0:Ljava/lang/Object;>Ljava/lang/Object;")]
	public class _1 : Object
	{
		[Signature("TT0;")]
		internal object ___003C_003Ev0;

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v0
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev0;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev0 = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 137, 130, 105, 104 })]
		public _1(object v0)
		{
			___003C_003Ev0 = v0;
		}
	}

	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>Ljava/lang/Object;")]
	public class _2 : Object
	{
		[Signature("TT0;")]
		internal object ___003C_003Ev0;

		[Signature("TT1;")]
		internal object ___003C_003Ev1;

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v0
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev0;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev0 = value;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v1
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev1;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev1 = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 162, 105, 104, 104 })]
		public _2(object v0, object v1)
		{
			___003C_003Ev0 = v0;
			___003C_003Ev1 = v1;
		}
	}

	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;>Ljava/lang/Object;")]
	public class _3 : Object
	{
		[Signature("TT0;")]
		internal object ___003C_003Ev0;

		[Signature("TT1;")]
		internal object ___003C_003Ev1;

		[Signature("TT2;")]
		internal object ___003C_003Ev2;

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v0
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev0;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev0 = value;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v1
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev1;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev1 = value;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v2
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev2;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev2 = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 132, 130, 105, 104, 104, 104 })]
		public _3(object v0, object v1, object v2)
		{
			___003C_003Ev0 = v0;
			___003C_003Ev1 = v1;
			___003C_003Ev2 = v2;
		}
	}

	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>Ljava/lang/Object;")]
	public class _4 : Object
	{
		[Signature("TT0;")]
		internal object ___003C_003Ev0;

		[Signature("TT1;")]
		internal object ___003C_003Ev1;

		[Signature("TT2;")]
		internal object ___003C_003Ev2;

		[Signature("TT3;")]
		internal object ___003C_003Ev3;

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v0
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev0;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev0 = value;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v1
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev1;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev1 = value;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v2
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev2;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev2 = value;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public object v3
		{
			[HideFromJava]
			get
			{
				return ___003C_003Ev3;
			}
			[HideFromJava]
			private set
			{
				___003C_003Ev3 = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 129, 162, 105, 104, 104, 104, 105 })]
		public _4(object v0, object v1, object v2, object v3)
		{
			___003C_003Ev0 = v0;
			___003C_003Ev1 = v1;
			___003C_003Ev2 = v2;
			___003C_003Ev3 = v3;
		}
	}

	[Signature("<T:Ljava/lang/Object;U:Ljava/lang/Object;>Ljava/lang/Object;")]
	public interface Mapper
	{
		[Signature("(TT;)TU;")]
		object map(object obj);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;>(TT0;TT1;TT2;)Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;")]
	[LineNumberTable(72)]
	public static _3 triple(object v0, object v1, object v2)
	{
		_3 result = new _3(v0, v1, v2);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>(TT0;TT1;)Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;")]
	[LineNumberTable(68)]
	public static _2 pair(object v0, object v1)
	{
		_2 result = new _2(v0, v1);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;>(TT0;)Lorg/jcodec/common/Tuple$_1<TT0;>;")]
	[LineNumberTable(64)]
	public static _1 single(object v0)
	{
		_1 result = new _1(v0);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(TT0;TT1;TT2;TT3;)Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;")]
	[LineNumberTable(76)]
	public static _4 quad(object v0, object v1, object v2, object v3)
	{
		_4 result = new _4(v0, v1, v2, v3);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public Tuple()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>(Ljava/lang/Iterable<Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;>;)Ljava/util/Map<TT0;TT1;>;")]
	[LineNumberTable(new byte[] { 159, 122, 66, 103, 124, 116, 99 })]
	public static Map asMap(Iterable it)
	{
		HashMap result = new HashMap();
		Iterator iterator = it.iterator();
		while (iterator.hasNext())
		{
			_2 el = (_2)iterator.next();
			result.put(el.___003C_003Ev0, el.___003C_003Ev1);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>([Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;)Ljava/util/Map<TT0;TT1;>;")]
	[LineNumberTable(new byte[] { 159, 120, 66, 103, 104, 101, 20, 199 })]
	public static Map arrayAsMap(_2[] arr)
	{
		HashMap result = new HashMap();
		for (int i = 0; i < (nint)arr.LongLength; i++)
		{
			_2 el = arr[i];
			result.put(el.___003C_003Ev0, el.___003C_003Ev1);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>(Ljava/util/Map<TT0;TT1;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;>;")]
	[LineNumberTable(new byte[] { 159, 118, 98, 103, 104, 124, 121, 99 })]
	public static List asList(Map m)
	{
		LinkedList result = new LinkedList();
		Set entrySet = m.entrySet();
		Iterator iterator = entrySet.iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			result.add(pair(entry.getKey(), entry.getValue()));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_1<TT0;>;>;)Ljava/util/List<TT0;>;")]
	[LineNumberTable(new byte[] { 159, 116, 130, 103, 124, 110, 99 })]
	public static List _1_project0(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_1 _1 = (_1)iterator.next();
			((List)result).add(_1.___003C_003Ev0);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;>;)Ljava/util/List<TT0;>;")]
	[LineNumberTable(new byte[] { 159, 114, 130, 103, 124, 110, 99 })]
	public static List _2_project0(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_2 _2 = (_2)iterator.next();
			((List)result).add(_2.___003C_003Ev0);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;>;)Ljava/util/List<TT1;>;")]
	[LineNumberTable(new byte[] { 159, 112, 130, 103, 124, 110, 99 })]
	public static List _2_project1(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_2 _2 = (_2)iterator.next();
			((List)result).add(_2.___003C_003Ev1);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;>;)Ljava/util/List<TT0;>;")]
	[LineNumberTable(new byte[] { 159, 110, 130, 103, 124, 110, 99 })]
	public static List _3_project0(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_3 _3 = (_3)iterator.next();
			((List)result).add(_3.___003C_003Ev0);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;>;)Ljava/util/List<TT1;>;")]
	[LineNumberTable(new byte[] { 159, 108, 130, 103, 124, 110, 99 })]
	public static List _3_project1(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_3 _3 = (_3)iterator.next();
			((List)result).add(_3.___003C_003Ev1);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;>;)Ljava/util/List<TT2;>;")]
	[LineNumberTable(new byte[] { 159, 106, 130, 103, 124, 110, 99 })]
	public static List _3_project2(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_3 _3 = (_3)iterator.next();
			((List)result).add(_3.___003C_003Ev2);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;)Ljava/util/List<TT0;>;")]
	[LineNumberTable(new byte[] { 159, 104, 130, 103, 124, 110, 99 })]
	public static List _4_project0(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			((List)result).add(_4.___003C_003Ev0);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;)Ljava/util/List<TT1;>;")]
	[LineNumberTable(new byte[] { 159, 102, 130, 103, 124, 110, 99 })]
	public static List _4_project1(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			((List)result).add(_4.___003C_003Ev1);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;)Ljava/util/List<TT2;>;")]
	[LineNumberTable(new byte[] { 159, 100, 130, 103, 124, 110, 99 })]
	public static List _4_project2(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			((List)result).add(_4.___003C_003Ev2);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;)Ljava/util/List<TT3;>;")]
	[LineNumberTable(new byte[] { 159, 98, 130, 103, 124, 110, 99 })]
	public static List _4_project3(List temp)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = temp.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			((List)result).add(_4.___003C_003Ev3);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_1<TT0;>;>;Lorg/jcodec/common/Tuple$Mapper<TT0;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_1<TU;>;>;")]
	[LineNumberTable(new byte[] { 159, 95, 130, 103, 124, 121, 99 })]
	public static List _1map0(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_1 _1 = (_1)iterator.next();
			result.add(single(mapper.map(_1.___003C_003Ev0)));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;>;Lorg/jcodec/common/Tuple$Mapper<TT0;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TU;TT1;>;>;")]
	[LineNumberTable(new byte[] { 159, 93, 130, 103, 124, 127, 0, 99 })]
	public static List _2map0(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_2 _2 = (_2)iterator.next();
			result.add(pair(mapper.map(_2.___003C_003Ev0), _2.___003C_003Ev1));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TT0;TT1;>;>;Lorg/jcodec/common/Tuple$Mapper<TT1;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_2<TT0;TU;>;>;")]
	[LineNumberTable(new byte[] { 159, 91, 130, 103, 124, 127, 0, 99 })]
	public static List _2map1(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_2 _2 = (_2)iterator.next();
			result.add(pair(_2.___003C_003Ev0, mapper.map(_2.___003C_003Ev1)));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;>;Lorg/jcodec/common/Tuple$Mapper<TT0;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TU;TT1;TT2;>;>;")]
	[LineNumberTable(new byte[] { 159, 89, 130, 103, 124, 127, 6, 99 })]
	public static List _3map0(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_3 _3 = (_3)iterator.next();
			result.add(triple(mapper.map(_3.___003C_003Ev0), _3.___003C_003Ev1, _3.___003C_003Ev2));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;>;Lorg/jcodec/common/Tuple$Mapper<TT1;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TU;TT2;>;>;")]
	[LineNumberTable(new byte[] { 159, 87, 130, 103, 124, 127, 6, 99 })]
	public static List _3map1(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_3 _3 = (_3)iterator.next();
			result.add(triple(_3.___003C_003Ev0, mapper.map(_3.___003C_003Ev1), _3.___003C_003Ev2));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TT2;>;>;Lorg/jcodec/common/Tuple$Mapper<TT2;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_3<TT0;TT1;TU;>;>;")]
	[LineNumberTable(new byte[] { 159, 85, 130, 103, 124, 127, 6, 99 })]
	public static List _3map3(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_3 _3 = (_3)iterator.next();
			result.add(triple(_3.___003C_003Ev0, _3.___003C_003Ev1, mapper.map(_3.___003C_003Ev2)));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;Lorg/jcodec/common/Tuple$Mapper<TT0;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TU;TT1;TT2;TT3;>;>;")]
	[LineNumberTable(new byte[] { 159, 83, 130, 103, 124, 127, 12, 99 })]
	public static List _4map0(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			result.add(quad(mapper.map(_4.___003C_003Ev0), _4.___003C_003Ev1, _4.___003C_003Ev2, _4.___003C_003Ev3));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;Lorg/jcodec/common/Tuple$Mapper<TT1;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TU;TT2;TT3;>;>;")]
	[LineNumberTable(new byte[] { 159, 81, 130, 103, 124, 127, 12, 99 })]
	public static List _4map1(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			result.add(quad(_4.___003C_003Ev0, mapper.map(_4.___003C_003Ev1), _4.___003C_003Ev2, _4.___003C_003Ev3));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;Lorg/jcodec/common/Tuple$Mapper<TT2;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TU;TT3;>;>;")]
	[LineNumberTable(new byte[] { 159, 79, 130, 103, 124, 127, 12, 99 })]
	public static List _4map3(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			result.add(quad(_4.___003C_003Ev0, _4.___003C_003Ev1, mapper.map(_4.___003C_003Ev2), _4.___003C_003Ev3));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T0:Ljava/lang/Object;T1:Ljava/lang/Object;T2:Ljava/lang/Object;T3:Ljava/lang/Object;U:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TT3;>;>;Lorg/jcodec/common/Tuple$Mapper<TT3;TU;>;)Ljava/util/List<Lorg/jcodec/common/Tuple$_4<TT0;TT1;TT2;TU;>;>;")]
	[LineNumberTable(new byte[] { 159, 77, 130, 103, 124, 127, 12, 99 })]
	public static List _4map4(List l, Mapper mapper)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = l.iterator();
		while (iterator.hasNext())
		{
			_4 _4 = (_4)iterator.next();
			result.add(quad(_4.___003C_003Ev0, _4.___003C_003Ev1, _4.___003C_003Ev2, mapper.map(_4.___003C_003Ev3)));
		}
		return result;
	}
}
