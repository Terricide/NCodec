using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.lang.reflect;
using java.nio;
using java.util;
using org.jcodec.common.io;

namespace org.jcodec.common.tools;

public class ToJSON : java.lang.Object
{
	private sealed class ___003CCallerID_003E : CallerID
	{
		internal ___003CCallerID_003E()
		{
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Set<Ljava/lang/Class;>;")]
	private static Set primitive;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Set<Ljava/lang/String;>;")]
	private static Set omitMethods;

	[SpecialName]
	private static CallerID ___003CcallerID_003E;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 100, 109, 162, 109, 123, 109, 162,
		104, 106, 109, 130, 136, 105, 142, 100, 114, 105,
		109, 109, 114, 108, 114, 109, 105, 109, 109, 110,
		109, 110, 105, 109, 99, 109, 110, 110, 109, 106,
		111, 106, 143, 109, 113, 109, 105, 106, 112, 105,
		237, 61, 233, 69, 109, 113, 110, 109, 107, 127,
		6, 106, 237, 61, 233, 69, 109, 113, 110, 109,
		107, 127, 6, 106, 237, 61, 233, 69, 109, 113,
		110, 109, 107, 127, 6, 106, 237, 61, 233, 69,
		109, 113, 110, 109, 107, 127, 6, 106, 237, 61,
		233, 69, 109, 113, 110, 109, 107, 127, 6, 106,
		237, 61, 233, 69, 109, 113, 110, 109, 107, 127,
		6, 106, 237, 61, 233, 69, 109, 113, 110, 109,
		107, 109, 106, 237, 61, 233, 69, 109, 115, 147,
		109, 115, 104, 122, 125, 99, 235, 61, 233, 69,
		106, 106, 111, 106, 109, 106, 109, 99, 173, 105
	})]
	private static void toJSONSub(object obj, IntArrayList stack, StringBuilder builder)
	{
		if (obj == null)
		{
			builder.append("null");
			return;
		}
		string className = java.lang.Object.instancehelper_getClass(obj).getName();
		if (java.lang.String.instancehelper_startsWith(className, "java.lang") && !java.lang.String.instancehelper_equals(className, "java.lang.String"))
		{
			builder.append("null");
			return;
		}
		int id = java.lang.System.identityHashCode(obj);
		if (stack.contains(id))
		{
			builder.append("null");
			return;
		}
		stack.push(id);
		if (obj is ByteBuffer)
		{
			obj = NIOUtils.toArray((ByteBuffer)obj);
		}
		if (obj == null)
		{
			builder.append("null");
		}
		else if (obj is string)
		{
			builder.append("\"");
			escape((string)obj, builder);
			builder.append("\"");
		}
		else if (obj is Map)
		{
			Iterator it2 = ((Map)obj).entrySet().iterator();
			builder.append("{");
			while (it2.hasNext())
			{
				Map.Entry e = (Map.Entry)it2.next();
				builder.append("\"");
				builder.append(e.getKey());
				builder.append("\":");
				toJSONSub(e.getValue(), stack, builder);
				if (it2.hasNext())
				{
					builder.append(",");
				}
			}
			builder.append("}");
		}
		else if (obj is Iterable)
		{
			Iterator it = ((Iterable)obj).iterator();
			builder.append("[");
			while (it.hasNext())
			{
				toJSONSub(it.next(), stack, builder);
				if (it.hasNext())
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is object[])
		{
			builder.append("[");
			int len = java.lang.reflect.Array.getLength(obj);
			for (int i3 = 0; i3 < len; i3++)
			{
				toJSONSub(java.lang.reflect.Array.get(obj, i3), stack, builder);
				if (i3 < len - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is long[])
		{
			long[] a7 = (long[])obj;
			builder.append("[");
			for (int i2 = 0; i2 < (nint)a7.LongLength; i2++)
			{
				builder.append(java.lang.String.format("0x%016x", Long.valueOf(a7[i2])));
				if (i2 < (nint)a7.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is int[])
		{
			int[] a6 = (int[])obj;
			builder.append("[");
			for (int n = 0; n < (nint)a6.LongLength; n++)
			{
				builder.append(java.lang.String.format("0x%08x", Integer.valueOf(a6[n])));
				if (n < (nint)a6.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is float[])
		{
			float[] a5 = (float[])obj;
			builder.append("[");
			for (int m = 0; m < (nint)a5.LongLength; m++)
			{
				builder.append(java.lang.String.format("%.3f", Float.valueOf(a5[m])));
				if (m < (nint)a5.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is double[])
		{
			double[] a4 = (double[])obj;
			builder.append("[");
			for (int l = 0; l < (nint)a4.LongLength; l++)
			{
				builder.append(java.lang.String.format("%.6f", java.lang.Double.valueOf(a4[l])));
				if (l < (nint)a4.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is short[])
		{
			short[] a3 = (short[])obj;
			builder.append("[");
			for (int k = 0; k < (nint)a3.LongLength; k++)
			{
				builder.append(java.lang.String.format("0x%04x", Short.valueOf(a3[k])));
				if (k < (nint)a3.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is byte[])
		{
			byte[] a2 = (byte[])obj;
			builder.append("[");
			for (int j = 0; j < (nint)a2.LongLength; j++)
			{
				builder.append(java.lang.String.format("0x%02x", java.lang.Byte.valueOf(a2[j])));
				if (j < (nint)a2.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (obj is bool[])
		{
			bool[] a = (bool[])obj;
			builder.append("[");
			for (int i = 0; i < (nint)a.LongLength; i++)
			{
				builder.append(a[i]);
				if (i < (nint)a.LongLength - 1)
				{
					builder.append(",");
				}
			}
			builder.append("]");
		}
		else if (java.lang.Object.instancehelper_getClass(obj).isEnum())
		{
			builder.append(java.lang.String.valueOf(obj));
		}
		else
		{
			builder.append("{");
			Method[] methods = java.lang.Object.instancehelper_getClass(obj).getMethods(ToJSON.___003CGetCallerID_003E());
			ArrayList filteredMethods = new ArrayList();
			Method[] array = methods;
			int num = array.Length;
			for (int num2 = 0; num2 < num; num2++)
			{
				Method method2 = array[num2];
				if (!omitMethods.contains(method2.getName()) && isGetter(method2))
				{
					((List)filteredMethods).add((object)method2);
				}
			}
			Iterator iterator = ((List)filteredMethods).iterator();
			while (iterator.hasNext())
			{
				Method method = (Method)iterator.next();
				string name = toName(method);
				invoke(obj, stack, builder, method, name);
				if (iterator.hasNext())
				{
					builder.append(",");
				}
			}
			builder.append("}");
		}
		stack.pop();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 86, 162, 104, 113, 103, 159, 5, 234, 60,
		231, 70
	})]
	private static void escape(string invoke, StringBuilder sb)
	{
		char[] ch = java.lang.String.instancehelper_toCharArray(invoke);
		char[] array = ch;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			int c = array[i];
			if (c < 32)
			{
				sb.append(java.lang.String.format("\\%02x", Integer.valueOf(c)));
			}
			else
			{
				sb.append((char)c);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 162, 110, 99, 116, 127, 0, 99, 106,
		163
	})]
	private static bool isGetter(Method method)
	{
		if (!Modifier.isPublic(method.getModifiers()))
		{
			return false;
		}
		if (!java.lang.String.instancehelper_startsWith(method.getName(), "get") && (!java.lang.String.instancehelper_startsWith(method.getName(), "is") || method.getReturnType() != java.lang.Boolean.TYPE))
		{
			return false;
		}
		if (method.getParameterTypes().Length != 0)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 83, 98, 105, 145, 109, 109, 108 })]
	private static string toName(Method method)
	{
		if (!isGetter(method))
		{
			
			throw new IllegalArgumentException("Not a getter");
		}
		char[] name = java.lang.String.instancehelper_toCharArray(method.getName());
		int ind = ((name[0] != 'g') ? 2 : 3);
		name[ind] = Character.toLowerCase(name[ind]);
		string result = java.lang.String.newhelper(name, ind, (int)((nint)name.LongLength - ind));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 89, 130, 116, 106, 106, 109, 118, 139, 159,
		1, 35, 130
	})]
	private static void invoke(object obj, IntArrayList stack, StringBuilder builder, Method method, string name)
	{
		java.lang.Exception ex2;
		try
		{
			object invoke = method.invoke(obj, new object[0], ToJSON.___003CGetCallerID_003E());
			builder.append('"');
			builder.append(name);
			builder.append("\":");
			if (invoke != null && primitive.contains(java.lang.Object.instancehelper_getClass(invoke)))
			{
				builder.append(invoke);
			}
			else
			{
				toJSONSub(invoke, stack, builder);
			}
			return;
		}
		catch (System.Exception x)
		{
			java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		java.lang.Exception ex3 = ex2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public ToJSON()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 103, 103, 105 })]
	public static string toJSON(object obj)
	{
		StringBuilder builder = new StringBuilder();
		IntArrayList stack = IntArrayList.createIntArrayList();
		toJSONSub(obj, stack, builder);
		string result = builder.toString();
		
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 134, 98, 107, 171, 113, 113, 113, 113, 113,
		113, 113, 209, 113, 113
	})]
	static ToJSON()
	{
		primitive = new HashSet();
		omitMethods = new HashSet();
		primitive.add(ClassLiteral<java.lang.Boolean>.Value);
		primitive.add(ClassLiteral<java.lang.Byte>.Value);
		primitive.add(ClassLiteral<Short>.Value);
		primitive.add(ClassLiteral<Integer>.Value);
		primitive.add(ClassLiteral<Long>.Value);
		primitive.add(ClassLiteral<Float>.Value);
		primitive.add(ClassLiteral<java.lang.Double>.Value);
		primitive.add(ClassLiteral<Character>.Value);
		omitMethods.add("getClass");
		omitMethods.add("get");
	}

	static CallerID ___003CGetCallerID_003E()
	{
		if (___003CcallerID_003E == null)
		{
			___003CcallerID_003E = new ___003CCallerID_003E();
		}
		return ___003CcallerID_003E;
	}
}
