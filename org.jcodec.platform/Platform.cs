using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.lang.reflect;
using java.nio.charset;
using java.util;
using org.jcodec.common.tools;

namespace org.jcodec.platform;

public class Platform : java.lang.Object
{
	private sealed class ___003CCallerID_003E : CallerID
	{
		internal ___003CCallerID_003E()
		{
		}
	}

	public const string UTF_8 = "UTF-8";

	public const string UTF_16 = "UTF-16";

	public const string UTF_16BE = "UTF-16BE";

	public const string ISO8859_1 = "iso8859-1";

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/Map<Ljava/lang/Class;Ljava/lang/Class;>;")]
	private static Map boxed2primitive;

	[SpecialName]
	private static CallerID ___003CcallerID_003E;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(112)]
	public static int[] copyOfInt(int[] original, int newLength)
	{
		int[] result = Arrays.copyOf(original, newLength);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(116)]
	public static bool[] copyOfBool(bool[] original, int newLength)
	{
		bool[] result = Arrays.copyOf(original, newLength);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/lang/Class<TT;>;[Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 159, 133, 66, 127, 25, 98 })]
	public static object newInstance(Class clazz, object[] @params)
	{
		//Discarded unreachable code: IL_0020
		java.lang.Exception ex2;
		try
		{
			return clazz.getConstructor(classes(@params), Platform.___003CGetCallerID_003E()).newInstance(@params, Platform.___003CGetCallerID_003E());
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
		java.lang.Exception e = ex2;
		
		throw new RuntimeException(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>([TT;II)[TT;")]
	[LineNumberTable(88)]
	public static object[] copyOfRangeO(object[] original, int from, int to)
	{
		object[] result = Arrays.copyOfRange(original, from, to);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(92)]
	public static long[] copyOfRangeL(long[] original, int from, int to)
	{
		long[] result = Arrays.copyOfRange(original, from, to);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(84)]
	public static bool arrayEqualsObj(object[] a, object[] a2)
	{
		bool result = Arrays.equals(a, a2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>([TT;I)[TT;")]
	[LineNumberTable(104)]
	public static object[] copyOfObj(object[] original, int newLength)
	{
		object[] result = Arrays.copyOf(original, newLength);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(76)]
	public static bool arrayEqualsInt(int[] a, int[] a2)
	{
		bool result = Arrays.equals(a, a2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(159)]
	public static string toJSON(object o)
	{
		string result = ToJSON.toJSON(o);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 98, 127, 3, 98 })]
	public static string stringFromBytes(byte[] bytes)
	{
		//Discarded unreachable code: IL_000f
		UnsupportedEncodingException ex;
		try
		{
			return java.lang.String.newhelper(bytes, "iso8859-1");
		}
		catch (UnsupportedEncodingException x)
		{
			ex = ByteCodeHelper.MapException<UnsupportedEncodingException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		UnsupportedEncodingException e = ex;
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	public static long[] copyOfLong(long[] original, int newLength)
	{
		long[] result = Arrays.copyOf(original, newLength);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 98, 127, 3, 98 })]
	public static byte[] getBytes(string fourcc)
	{
		//Discarded unreachable code: IL_000f
		UnsupportedEncodingException ex;
		try
		{
			return java.lang.String.instancehelper_getBytes(fourcc, "iso8859-1");
		}
		catch (UnsupportedEncodingException x)
		{
			ex = ByteCodeHelper.MapException<UnsupportedEncodingException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		UnsupportedEncodingException e = ex;
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("([Ljava/lang/Object;)Ljava/lang/Class<*>;")]
	[LineNumberTable(184)]
	public static Class arrayComponentType(object[] array)
	{
		Class componentType = java.lang.Object.instancehelper_getClass(array).getComponentType();
		
		return componentType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(64)]
	public static string stringFromCharset(byte[] data, string charset)
	{
		string result = java.lang.String.newhelper(data, Charset.forName(charset));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(80)]
	public static bool arrayEqualsByte(byte[] a, byte[] a2)
	{
		bool result = Arrays.equals(a, a2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(180)]
	public static string stringFromChars(char[] symb)
	{
		string result = java.lang.String.newhelper(symb);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/lang/Class<*>;Ljava/lang/String;[Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[]
	{
		159, 101, 66, 122, 111, 18, 231, 69, 127, 35,
		99
	})]
	public static object invokeStaticMethod(Class cls, string methodName, object[] @params)
	{
		java.lang.Exception ex2;
		try
		{
			Method[] declaredMethods = cls.getDeclaredMethods(Platform.___003CGetCallerID_003E());
			int num = declaredMethods.Length;
			for (int i = 0; i < num; i++)
			{
				Method method = declaredMethods[i];
				if (java.lang.String.instancehelper_equals(method.getName(), methodName))
				{
					return method.invoke(null, @params, Platform.___003CGetCallerID_003E());
				}
			}
			string s = new StringBuilder().append(cls).append(".").append(methodName)
				.toString();
			
			throw new NoSuchMethodException(s);
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
		java.lang.Exception e = ex2;
		
		throw new RuntimeException(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/Class<*>;)[Ljava/lang/reflect/Field;")]
	[LineNumberTable(56)]
	public static Field[] getDeclaredFields(Class class1)
	{
		Field[] declaredFields = class1.getDeclaredFields(Platform.___003CGetCallerID_003E());
		
		return declaredFields;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(72)]
	public static string stringFromCharset4(byte[] data, int offset, int len, string charset)
	{
		string result = java.lang.String.newhelper(data, offset, len, Charset.forName(charset));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 66, 110, 131 })]
	public static bool isAssignableFrom(Class class1, Class class2)
	{
		if (class1 == class2 || java.lang.Object.instancehelper_equals(class1, class2))
		{
			return true;
		}
		bool result = class1.isAssignableFrom(class2);
		
		return result;
	}

	[LineNumberTable(176)]
	public static long unsignedInt(int signed)
	{
		return signed & 0xFFFFFFFFu;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(68)]
	public static byte[] getBytesForCharset(string url, string charset)
	{
		byte[] result = java.lang.String.instancehelper_getBytes(url, Charset.forName(charset));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(124)]
	public static string arrayToString(object[] a)
	{
		string result = Arrays.toString(a);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(96)]
	public static int[] copyOfRangeI(int[] original, int from, int to)
	{
		int[] result = Arrays.copyOfRange(original, from, to);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/Class<*>;)[Ljava/lang/reflect/Field;")]
	[LineNumberTable(60)]
	public static Field[] getFields(Class class1)
	{
		Field[] fields = class1.getFields(Platform.___003CGetCallerID_003E());
		
		return fields;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(128)]
	public static bool deleteFile(File file)
	{
		bool result = file.delete();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(155)]
	public static InputStream stdin()
	{
		return java.lang.System.@in;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 105, 104, 106, 110, 150, 229, 59,
		231, 72
	})]
	private static Class[] classes(object[] @params)
	{
		Class[] classes = new Class[(nint)@params.LongLength];
		for (int i = 0; i < (nint)@params.LongLength; i++)
		{
			Class cls = java.lang.Object.instancehelper_getClass(@params[i]);
			if (boxed2primitive.containsKey(cls))
			{
				classes[i] = (Class)boxed2primitive.get(cls);
			}
			else
			{
				classes[i] = cls;
			}
		}
		return classes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public Platform()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(100)]
	public static byte[] copyOfRangeB(byte[] original, int from, int to)
	{
		byte[] result = Arrays.copyOfRange(original, from, to);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(120)]
	public static byte[] copyOfByte(byte[] original, int newLength)
	{
		byte[] result = Arrays.copyOf(original, newLength);
		
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 137, 130, 139, 118, 118, 118, 118, 118, 118,
		118, 118
	})]
	static Platform()
	{
		boxed2primitive = new HashMap();
		boxed2primitive.put(ClassLiteral<java.lang.Void>.Value, java.lang.Void.TYPE);
		boxed2primitive.put(ClassLiteral<java.lang.Byte>.Value, java.lang.Byte.TYPE);
		boxed2primitive.put(ClassLiteral<Short>.Value, Short.TYPE);
		boxed2primitive.put(ClassLiteral<Character>.Value, Character.TYPE);
		boxed2primitive.put(ClassLiteral<Integer>.Value, Integer.TYPE);
		boxed2primitive.put(ClassLiteral<Long>.Value, Long.TYPE);
		boxed2primitive.put(ClassLiteral<Float>.Value, Float.TYPE);
		boxed2primitive.put(ClassLiteral<java.lang.Double>.Value, java.lang.Double.TYPE);
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
