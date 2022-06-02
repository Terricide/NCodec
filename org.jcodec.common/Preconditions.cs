using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common;

public sealed class Preconditions : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 37, 161, 67, 100, 140 })]
	public static void checkState(bool expression)
	{
		if (!expression)
		{
			
			throw new IllegalStateException();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 33, 129, 67, 100, 146 })]
	public static void checkState(bool expression, object errorMessage)
	{
		if (!expression)
		{
			string s = String.valueOf(errorMessage);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 80, 130, 100, 140, 169, 115, 99, 99, 105,
		110, 101, 131, 127, 6, 111, 101, 102, 191, 11,
		102, 109, 111, 102, 109, 145, 170
	})]
	internal static string format(string template, params object[] args)
	{
		if (args == null)
		{
			
			throw new NullPointerException();
		}
		template = String.valueOf(template);
		StringBuilder builder = new StringBuilder((int)(String.instancehelper_length(template) + 16 * (nint)args.LongLength));
		int templateStart = 0;
		int i = 0;
		CharSequence s = default(CharSequence);
		int end;
		int start;
		while (i < (nint)args.LongLength)
		{
			int placeholderStart = String.instancehelper_indexOf(template, "%s", templateStart);
			if (placeholderStart == -1)
			{
				break;
			}
			string __003Cref_003E = template;
			int num = templateStart;
			end = placeholderStart;
			start = num;
			builder.append(s, start, end);
			int num2 = i;
			i++;
			builder.append(args[num2]);
			templateStart = placeholderStart + 2;
		}
		string __003Cref_003E2 = template;
		int num3 = templateStart;
		end = String.instancehelper_length(template);
		start = num3;
		builder.append(s, start, end);
		if (i < (nint)args.LongLength)
		{
			builder.append(" [");
			int num4 = i;
			i++;
			builder.append(args[num4]);
			while (i < (nint)args.LongLength)
			{
				builder.append(", ");
				int num5 = i;
				i++;
				builder.append(args[num5]);
			}
			builder.append(']');
		}
		string result = builder.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 106, 162, 105, 148 })]
	public static int checkElementIndex(int index, int size, string desc)
	{
		if (index < 0 || index >= size)
		{
			string s = badElementIndex(index, size, desc);
			
			throw new IndexOutOfBoundsException(s);
		}
		return index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 104, 130, 101, 127, 2, 101, 159, 7 })]
	private static string badElementIndex(int index, int size, string desc)
	{
		if (index < 0)
		{
			string result = format("%s (%s) must not be negative", desc, Integer.valueOf(index));
			
			return result;
		}
		if (size < 0)
		{
			string s = new StringBuilder().append("negative size: ").append(size).toString();
			
			throw new IllegalArgumentException(s);
		}
		string result2 = format("%s (%s) must be less than size (%s)", desc, Integer.valueOf(index), Integer.valueOf(size));
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 94, 66, 105, 148 })]
	public static int checkPositionIndex(int index, int size, string desc)
	{
		if (index < 0 || index > size)
		{
			string s = badPositionIndex(index, size, desc);
			
			throw new IndexOutOfBoundsException(s);
		}
		return index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 93, 162, 101, 127, 2, 101, 159, 7 })]
	private static string badPositionIndex(int index, int size, string desc)
	{
		if (index < 0)
		{
			string result = format("%s (%s) must not be negative", desc, Integer.valueOf(index));
			
			return result;
		}
		if (size < 0)
		{
			string s = new StringBuilder().append("negative size: ").append(size).toString();
			
			throw new IllegalArgumentException(s);
		}
		string result2 = format("%s (%s) must not be greater than size (%s)", desc, Integer.valueOf(index), Integer.valueOf(size));
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 85, 66, 105, 144, 105, 176 })]
	private static string badPositionIndexes(int start, int end, int size)
	{
		if (start < 0 || start > size)
		{
			string result = badPositionIndex(start, size, "start index");
			
			return result;
		}
		if (end < 0 || end > size)
		{
			string result2 = badPositionIndex(end, size, "end index");
			
			return result2;
		}
		string result3 = format("end index (%s) must not be less than start index (%s)", Integer.valueOf(end), Integer.valueOf(start));
		
		return result3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(93)]
	private Preconditions()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 129, 67, 100, 140 })]
	public static void checkArgument(bool expression)
	{
		if (!expression)
		{
			
			throw new IllegalArgumentException();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 65, 67, 100, 146 })]
	public static void checkArgument(bool expression, object errorMessage)
	{
		if (!expression)
		{
			string s = String.valueOf(errorMessage);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 65, 67, 100, 147 })]
	public static void checkArgument(bool expression, string errorMessageTemplate, params object[] errorMessageArgs)
	{
		if (!expression)
		{
			string s = format(errorMessageTemplate, errorMessageArgs);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 161, 69, 100, 159, 2 })]
	public static void checkArgument(bool b, string errorMessageTemplate, char p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 129, 67, 100, 159, 2 })]
	public static void checkArgument(bool b, string errorMessageTemplate, int p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 97, 67, 100, 159, 2 })]
	public static void checkArgument(bool b, string errorMessageTemplate, long p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 97, 67, 100, 156 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 93, 97, 71, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, char p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Character.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 90, 97, 69, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, char p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Integer.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 97, 69, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, char p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Long.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 97, 69, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, char p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), p2);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 81, 97, 69, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, int p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Character.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 78, 97, 67, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, int p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Integer.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 75, 97, 67, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, int p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Long.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 97, 67, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, int p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), p2);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 69, 97, 69, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, long p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Character.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 66, 97, 67, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, long p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Integer.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 63, 97, 67, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, long p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Long.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 60, 97, 67, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, long p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), p2);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 57, 97, 69, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, Character.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 97, 67, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, Integer.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 51, 97, 67, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, Long.valueOf(p2));
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 48, 97, 67, 100, 159, 1 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, p2);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 44, 97, 67, 100, 159, 6 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1, object p2, object p3)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, p2, p3);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 40, 129, 67, 100, 159, 11 })]
	public static void checkArgument(bool b, string errorMessageTemplate, object p1, object p2, object p3, object p4)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, p2, p3, p4);
			
			throw new IllegalArgumentException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 27, 161, 67, 100, 147 })]
	public static void checkState(bool expression, string errorMessageTemplate, params object[] errorMessageArgs)
	{
		if (!expression)
		{
			string s = format(errorMessageTemplate, errorMessageArgs);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 24, 161, 69, 100, 159, 2 })]
	public static void checkState(bool b, string errorMessageTemplate, char p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 21, 161, 67, 100, 159, 2 })]
	public static void checkState(bool b, string errorMessageTemplate, int p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 18, 161, 67, 100, 159, 2 })]
	public static void checkState(bool b, string errorMessageTemplate, long p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 14, 65, 67, 100, 156 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 11, 97, 71, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, char p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Character.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 8, 97, 69, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, char p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Integer.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 5, 129, 69, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, char p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Long.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 2, 161, 69, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, char p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), p2);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		byte.MaxValue,
		161,
		69,
		100,
		159,
		11
	})]
	public static void checkState(bool b, string errorMessageTemplate, int p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Character.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 252, 161, 67, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, int p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Integer.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 249, 161, 67, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, int p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Long.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 245, 65, 67, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, int p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), p2);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 242, 97, 69, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, long p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Character.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 239, 97, 67, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, long p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Integer.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 236, 129, 67, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, long p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Long.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 233, 161, 67, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, long p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), p2);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 229, 65, 69, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1, char p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, Character.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 226, 97, 67, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1, int p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, Integer.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 223, 129, 67, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1, long p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, Long.valueOf(p2));
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 220, 161, 67, 100, 159, 1 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1, object p2)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, p2);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 215, 65, 67, 100, 159, 6 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1, object p2, object p3)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, p2, p3);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 211, 129, 67, 100, 159, 11 })]
	public static void checkState(bool b, string errorMessageTemplate, object p1, object p2, object p3, object p4)
	{
		if (!b)
		{
			string s = format(errorMessageTemplate, p1, p2, p3, p4);
			
			throw new IllegalStateException(s);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;)TT;")]
	[LineNumberTable(new byte[] { 158, 207, 66, 100, 140 })]
	public static object checkNotNull(object reference)
	{
		if (reference == null)
		{
			
			throw new NullPointerException();
		}
		return reference;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 203, 98, 100, 146 })]
	public static object checkNotNull(object reference, object errorMessage)
	{
		if (reference == null)
		{
			string s = String.valueOf(errorMessage);
			
			throw new NullPointerException(s);
		}
		return reference;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;[Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 197, 66, 132, 147 })]
	public static object checkNotNull(object reference, string errorMessageTemplate, params object[] errorMessageArgs)
	{
		if (reference == null)
		{
			string s = format(errorMessageTemplate, errorMessageArgs);
			
			throw new NullPointerException(s);
		}
		return reference;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;C)TT;")]
	[LineNumberTable(new byte[] { 158, 194, 129, 67, 100, 159, 2 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, char p1)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;I)TT;")]
	[LineNumberTable(new byte[] { 158, 191, 162, 100, 159, 2 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, int p1)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;J)TT;")]
	[LineNumberTable(new byte[] { 158, 187, 66, 100, 159, 2 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, long p1)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 184, 130, 100, 156 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;CC)TT;")]
	[LineNumberTable(new byte[] { 158, 181, 161, 69, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, char p1, char p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Character.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;CI)TT;")]
	[LineNumberTable(new byte[] { 158, 177, 65, 67, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, char p1, int p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Integer.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;CJ)TT;")]
	[LineNumberTable(new byte[] { 158, 174, 97, 67, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, char p1, long p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), Long.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;CLjava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 171, 161, 67, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, char p1, object p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Character.valueOf(p1), p2);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;IC)TT;")]
	[LineNumberTable(new byte[] { 158, 167, 65, 67, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, int p1, char p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Character.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;II)TT;")]
	[LineNumberTable(new byte[] { 158, 164, 98, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, int p1, int p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Integer.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;IJ)TT;")]
	[LineNumberTable(new byte[] { 158, 161, 130, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, int p1, long p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), Long.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;ILjava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 157, 66, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, int p1, object p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Integer.valueOf(p1), p2);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;JC)TT;")]
	[LineNumberTable(new byte[] { 158, 154, 97, 67, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, long p1, char p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Character.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;JI)TT;")]
	[LineNumberTable(new byte[] { 158, 151, 130, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, long p1, int p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Integer.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;JJ)TT;")]
	[LineNumberTable(new byte[] { 158, 148, 162, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, long p1, long p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), Long.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;JLjava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 144, 98, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, long p1, object p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, Long.valueOf(p1), p2);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;C)TT;")]
	[LineNumberTable(new byte[] { 158, 141, 161, 67, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1, char p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1, Character.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;I)TT;")]
	[LineNumberTable(new byte[] { 158, 137, 98, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1, int p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1, Integer.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;J)TT;")]
	[LineNumberTable(new byte[] { 158, 134, 162, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1, long p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1, Long.valueOf(p2));
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 130, 98, 100, 159, 1 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1, object p2)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1, p2);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 126, 162, 100, 159, 6 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1, object p2, object p3)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1, p2, p3);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(TT;Ljava/lang/String;Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)TT;")]
	[LineNumberTable(new byte[] { 158, 121, 130, 100, 159, 11 })]
	public static object checkNotNull(object obj, string errorMessageTemplate, object p1, object p2, object p3, object p4)
	{
		if (obj == null)
		{
			string s = format(errorMessageTemplate, p1, p2, p3, p4);
			
			throw new NullPointerException(s);
		}
		return obj;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(1154)]
	public static int checkElementIndex(int index, int size)
	{
		int result = checkElementIndex(index, size, "index");
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(1199)]
	public static int checkPositionIndex(int index, int size)
	{
		int result = checkPositionIndex(index, size, "index");
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 87, 130, 109, 148 })]
	public static void checkPositionIndexes(int start, int end, int size)
	{
		if (start < 0 || end < start || end > size)
		{
			string s = badPositionIndexes(start, end, size);
			
			throw new IndexOutOfBoundsException(s);
		}
	}
}
