using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common;

public class StringUtils : Object
{
	internal static string[] ___003C_003EzeroPad00;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static string[] zeroPad00
	{
		[HideFromJava]
		get
		{
			return ___003C_003EzeroPad00;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 161, 67 })]
	public static string[] splitC(string str, char separatorChar)
	{
		string[] result = splitWorker(str, separatorChar, preserveAllTokens: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(143)]
	public static string[] splitS(string str, string separatorChars)
	{
		string[] result = splitWorker4(str, separatorChars, -1, preserveAllTokens: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 162, 106, 137 })]
	public static string zeroPad2(int x)
	{
		if (x >= 0 && x < 10)
		{
			return ___003C_003EzeroPad00[x];
		}
		string result = new StringBuilder().append("").append(x).toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(147)]
	public static string[] splitS3(string str, string separatorChars, int max)
	{
		string[] result = splitWorker4(str, separatorChars, max, preserveAllTokens: false);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 65, 67, 100, 131, 104, 100, 136, 103,
		99, 103, 100, 100, 135, 105, 112, 104, 100, 105,
		100, 132, 114, 132, 107, 134, 100, 100, 140, 141,
		106, 105, 109, 104, 100, 105, 100, 132, 114, 132,
		107, 134, 100, 100, 140, 134, 105, 114, 104, 100,
		105, 100, 132, 114, 132, 107, 134, 100, 100, 172,
		108, 146
	})]
	private static string[] splitWorker4(string str, string separatorChars, int max, bool preserveAllTokens)
	{
		if (str == null)
		{
			return null;
		}
		int len = String.instancehelper_length(str);
		if (len == 0)
		{
			return new string[0];
		}
		ArrayList list = new ArrayList();
		int sizePlus1 = 1;
		int i = 0;
		int start = 0;
		int match = 0;
		int lastMatch = 0;
		if (separatorChars == null)
		{
			while (i < len)
			{
				if (Character.isWhitespace(String.instancehelper_charAt(str, i)))
				{
					if (match != 0 || preserveAllTokens)
					{
						lastMatch = 1;
						int num = sizePlus1;
						sizePlus1++;
						if (num == max)
						{
							i = len;
							lastMatch = 0;
						}
						((List)list).add((object)String.instancehelper_substring(str, start, i));
						match = 0;
					}
					i++;
					start = i;
				}
				else
				{
					lastMatch = 0;
					match = 1;
					i++;
				}
			}
		}
		else if (String.instancehelper_length(separatorChars) == 1)
		{
			int sep = String.instancehelper_charAt(separatorChars, 0);
			while (i < len)
			{
				if (String.instancehelper_charAt(str, i) == sep)
				{
					if (match != 0 || preserveAllTokens)
					{
						lastMatch = 1;
						int num2 = sizePlus1;
						sizePlus1++;
						if (num2 == max)
						{
							i = len;
							lastMatch = 0;
						}
						((List)list).add((object)String.instancehelper_substring(str, start, i));
						match = 0;
					}
					i++;
					start = i;
				}
				else
				{
					lastMatch = 0;
					match = 1;
					i++;
				}
			}
		}
		else
		{
			while (i < len)
			{
				if (String.instancehelper_indexOf(separatorChars, String.instancehelper_charAt(str, i)) >= 0)
				{
					if (match != 0 || preserveAllTokens)
					{
						lastMatch = 1;
						int num3 = sizePlus1;
						sizePlus1++;
						if (num3 == max)
						{
							i = len;
							lastMatch = 0;
						}
						((List)list).add((object)String.instancehelper_substring(str, start, i));
						match = 0;
					}
					i++;
					start = i;
				}
				else
				{
					lastMatch = 0;
					match = 1;
					i++;
				}
			}
		}
		if (match != 0 || (preserveAllTokens && lastMatch != 0))
		{
			((List)list).add((object)String.instancehelper_substring(str, start, i));
		}
		return (string[])((List)list).toArray((object[])new string[((List)list).size()]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 161, 69, 100, 131, 104, 100, 136, 103,
		103, 100, 100, 105, 108, 104, 114, 100, 132, 107,
		131, 100, 100, 140, 108, 146
	})]
	private static string[] splitWorker(string str, char separatorChar, bool preserveAllTokens)
	{
		if (str == null)
		{
			return null;
		}
		int len = String.instancehelper_length(str);
		if (len == 0)
		{
			return new string[0];
		}
		ArrayList list = new ArrayList();
		int i = 0;
		int start = 0;
		int match = 0;
		int lastMatch = 0;
		while (i < len)
		{
			if (String.instancehelper_charAt(str, i) == separatorChar)
			{
				if (match != 0 || preserveAllTokens)
				{
					((List)list).add((object)String.instancehelper_substring(str, start, i));
					match = 0;
					lastMatch = 1;
				}
				i++;
				start = i;
			}
			else
			{
				lastMatch = 0;
				match = 1;
				i++;
			}
		}
		if (match != 0 || (preserveAllTokens && lastMatch != 0))
		{
			((List)list).add((object)String.instancehelper_substring(str, start, i));
		}
		return (string[])((List)list).toArray((object[])new string[((List)list).size()]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(175)]
	public static string capitalize(string str)
	{
		string result = capitalizeD(str, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 162, 106, 111, 131, 104, 104, 99, 108,
		139, 107, 106, 101, 100, 111, 133, 234, 54, 236,
		77
	})]
	public static string capitalizeD(string str, char[] delimiters)
	{
		int delimLen = (int)((delimiters != null) ? delimiters.LongLength : (-1));
		if (str == null || String.instancehelper_length(str) == 0 || delimLen == 0)
		{
			return str;
		}
		int strLen = String.instancehelper_length(str);
		StringBuilder buffer = new StringBuilder(strLen);
		int capitalizeNext = 1;
		for (int i = 0; i < strLen; i++)
		{
			int ch = String.instancehelper_charAt(str, i);
			if (isDelimiter((char)ch, delimiters))
			{
				buffer.append((char)ch);
				capitalizeNext = 1;
			}
			else if (capitalizeNext != 0)
			{
				buffer.append(Character.toTitleCase((char)ch));
				capitalizeNext = 0;
			}
			else
			{
				buffer.append((char)ch);
			}
		}
		string result = buffer.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 161, 67, 100, 138, 106, 103, 3, 231,
		69
	})]
	private static bool isDelimiter(char ch, char[] delimiters)
	{
		if (delimiters == null)
		{
			bool result = Character.isWhitespace(ch);
			
			return result;
		}
		int i = 0;
		for (int isize = delimiters.Length; i < isize; i++)
		{
			if (ch == delimiters[i])
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 81, 66, 100, 131 })]
	public static string joinS(object[] array, string separator)
	{
		if (array == null)
		{
			return null;
		}
		string result = joinS4(array, separator, 0, array.Length);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 161, 67, 100, 131, 101, 101, 167, 124,
		136, 103, 101, 137, 102, 235, 59, 231, 72
	})]
	public static string join4(object[] array, char separator, int startIndex, int endIndex)
	{
		if (array == null)
		{
			return null;
		}
		int bufSize = endIndex - startIndex;
		if (bufSize <= 0)
		{
			return "";
		}
		bufSize *= ((array[startIndex] != null) ? String.instancehelper_length(Object.instancehelper_toString(array[startIndex])) : 16) + 1;
		StringBuilder buf = new StringBuilder(bufSize);
		for (int i = startIndex; i < endIndex; i++)
		{
			if (i > startIndex)
			{
				buf.append(separator);
			}
			if (array[i] != null)
			{
				buf.append(array[i]);
			}
		}
		string result = buf.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 79, 66, 100, 131, 100, 232, 69, 101, 101,
		167, 121, 137, 136, 103, 101, 137, 102, 235, 59,
		231, 72
	})]
	public static string joinS4(object[] array, string separator, int startIndex, int endIndex)
	{
		if (array == null)
		{
			return null;
		}
		if (separator == null)
		{
			separator = "";
		}
		int bufSize = endIndex - startIndex;
		if (bufSize <= 0)
		{
			return "";
		}
		bufSize *= ((array[startIndex] != null) ? String.instancehelper_length(Object.instancehelper_toString(array[startIndex])) : 16) + String.instancehelper_length(separator);
		StringBuilder buf = new StringBuilder(bufSize);
		for (int i = startIndex; i < endIndex; i++)
		{
			if (i > startIndex)
			{
				buf.append(separator);
			}
			if (array[i] != null)
			{
				buf.append(array[i]);
			}
		}
		string result = buf.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(15)]
	public StringUtils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(139)]
	public static string[] split(string str)
	{
		string[] result = splitS3(str, null, -1);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(155)]
	public static bool isEmpty(string str)
	{
		return (str == null || String.instancehelper_length(str) == 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(171)]
	public static string capitaliseAllWords(string str)
	{
		string result = capitalize(str);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(205)]
	public static string join(object[] array)
	{
		string result = joinS(array, null);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 90, 129, 67, 100, 163 })]
	public static string join2(object[] array, char separator)
	{
		if (array == null)
		{
			return null;
		}
		string result = join4(array, separator, 0, array.Length);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 70, 130, 104 })]
	public static string zeroPad3(int n)
	{
		string s1 = zeroPad2(n);
		return (String.instancehelper_length(s1) != 2) ? s1 : new StringBuilder().append("0").append(s1).toString();
	}

	[LineNumberTable(17)]
	static StringUtils()
	{
		___003C_003EzeroPad00 = new string[10] { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09" };
	}
}
