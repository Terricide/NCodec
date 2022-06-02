using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.lang.reflect;

namespace org.jcodec.common;

public class ArrayUtil : java.lang.Object
{
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 129, 162, 101, 98, 101, 103, 101 })]
	public static void swap(int[] arr, int ind1, int ind2)
	{
		if (ind1 != ind2)
		{
			int tmp = arr[ind1];
			arr[ind1] = arr[ind2];
			arr[ind2] = tmp;
		}
	}

	[Signature("<T:Ljava/lang/Object;>([TT;)V")]
	[LineNumberTable(new byte[] { 159, 136, 130, 106, 41, 167, 104 })]
	public static void shiftLeft1(object[] array)
	{
		for (int i = 0; i < (nint)array.LongLength - 1; i++)
		{
			array[i] = array[i + 1];
		}
		array[(nint)array.LongLength - 1] = null;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 125, 162, 99, 104, 39, 167 })]
	public static int sumByte(byte[] array)
	{
		int result = 0;
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			result += array[i];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 121, 162, 99, 105, 39, 167 })]
	public static int sumByte3(byte[] array, int from, int count)
	{
		int result = 0;
		for (int i = from; i < from + count; i++)
		{
			result += array[i];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 84, 66, 104, 103, 42, 135 })]
	public static byte[][] create2D(int width, int height)
	{
		byte[][] result = new byte[height][];
		for (int i = 0; i < height; i++)
		{
			result[i] = new byte[width];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 33, 98, 111, 39, 167 })]
	public static void copy1D(int[] to, int[] from)
	{
		for (int i = 0; i < java.lang.Math.min(to.Length, from.Length); i++)
		{
			to[i] = from[i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 35, 162, 111, 44, 167 })]
	public static void copy2D(int[][] to, int[][] from)
	{
		for (int i = 0; i < java.lang.Math.min(to.Length, from.Length); i++)
		{
			copy1D(to[i], from[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 41, 162, 111, 44, 167 })]
	public static void copy6D(int[][][][][][] to, int[][][][][][] from)
	{
		for (int i = 0; i < java.lang.Math.min(to.Length, from.Length); i++)
		{
			copy5D(to[i], from[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 36, 98, 111, 44, 167 })]
	public static void copy3D(int[][][] to, int[][][] from)
	{
		for (int i = 0; i < java.lang.Math.min(to.Length, from.Length); i++)
		{
			copy2D(to[i], from[i]);
		}
	}

	[Signature("<T:Ljava/lang/Object;>([TT;II)V")]
	[LineNumberTable(new byte[] { 159, 132, 66, 105, 41, 167, 103 })]
	public static void shiftLeft3(object[] array, int from, int to)
	{
		for (int i = from; i < to - 1; i++)
		{
			array[i] = array[i + 1];
		}
		array[to - 1] = null;
	}

	[Signature("<T:Ljava/lang/Object;>([TT;II)V")]
	[LineNumberTable(new byte[] { 159, 134, 98, 105, 41, 167, 101 })]
	public static void shiftRight3(object[] array, int from, int to)
	{
		for (int i = to - 1; i > from; i += -1)
		{
			array[i] = array[i - 1];
		}
		array[from] = null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 98, 100, 131 })]
	public static int[] cloneInt(int[] array)
	{
		if (array == null)
		{
			return null;
		}
		return (int[])array.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 66, 100, 131 })]
	public static long[] cloneLong(long[] array)
	{
		if (array == null)
		{
			return null;
		}
		return (long[])array.Clone();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 162, 100, 131 })]
	public static object[] cloneObj(object[] array)
	{
		if (array == null)
		{
			return null;
		}
		return (object[])array.Clone();
	}

	[LineNumberTable(new byte[] { 159, 103, 130, 105, 104, 46, 135 })]
	public static byte[] toByteArrayShifted(int[] array)
	{
		byte[] result = new byte[(nint)array.LongLength];
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			result[i] = (byte)(sbyte)(array[i] - 128);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 67, 98, 101, 101, 130, 101, 101, 105, 105,
		100, 139, 98, 104, 105, 105, 100, 137, 101, 105,
		105, 100, 137, 105, 105, 100, 201, 165, 102, 107,
		104, 107, 100, 107, 231, 59, 233, 72, 106, 100,
		138, 107, 111
	})]
	public static void quickSort(int[] a, int start, int end, int[] p)
	{
		int len = end - start;
		if (len < 2)
		{
			return;
		}
		int startPlus1 = start + 1;
		switch (len)
		{
		case 2:
			if (a[start] > a[startPlus1])
			{
				swap(a, start, startPlus1);
				if (p != null)
				{
					swap(p, start, startPlus1);
				}
			}
			return;
		case 3:
		{
			if (a[start] > a[startPlus1])
			{
				swap(a, start, startPlus1);
				if (p != null)
				{
					swap(p, start, startPlus1);
				}
			}
			int startPlus2 = start + 2;
			if (a[startPlus1] > a[startPlus2])
			{
				swap(a, startPlus1, startPlus2);
				if (p != null)
				{
					swap(p, startPlus1, startPlus2);
				}
			}
			if (a[start] > a[startPlus1])
			{
				swap(a, start, startPlus1);
				if (p != null)
				{
					swap(p, start, startPlus1);
				}
			}
			break;
		}
		}
		int pivot = a[0];
		int p_large = end - 1;
		for (int i = end - 1; i >= start; i += -1)
		{
			if (a[i] > pivot)
			{
				swap(a, i, p_large);
				if (p != null)
				{
					swap(p, i, p_large);
				}
				p_large += -1;
			}
		}
		swap(a, start, p_large);
		if (p != null)
		{
			swap(p, start, p_large);
		}
		quickSort(a, start, p_large, p);
		quickSort(a, p_large + 1, end, p);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 48, 98, 104, 42, 167 })]
	private static void flatten5DL(int[][][][][] @is, IntArrayList list)
	{
		for (int i = 0; i < (nint)@is.LongLength; i++)
		{
			flatten4DL(@is[i], list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 47, 162, 104, 42, 167 })]
	private static void flatten4DL(int[][][][] @is, IntArrayList list)
	{
		for (int i = 0; i < (nint)@is.LongLength; i++)
		{
			flatten3DL(@is[i], list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 45, 98, 104, 42, 167 })]
	private static void flatten3DL(int[][][] @is, IntArrayList list)
	{
		for (int i = 0; i < (nint)@is.LongLength; i++)
		{
			flatten2DL(@is[i], list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 44, 162, 104, 42, 167 })]
	private static void flatten2DL(int[][] @is, IntArrayList list)
	{
		for (int i = 0; i < (nint)@is.LongLength; i++)
		{
			flatten1DL(@is[i], list);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 42, 98, 104, 42, 167 })]
	private static void flatten1DL(int[] @is, IntArrayList list)
	{
		for (int i = 0; i < (nint)@is.LongLength; i++)
		{
			list.add(@is[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 39, 98, 111, 44, 167 })]
	public static void copy5D(int[][][][][] to, int[][][][][] from)
	{
		for (int i = 0; i < java.lang.Math.min(to.Length, from.Length); i++)
		{
			copy4D(to[i], from[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 38, 162, 111, 44, 167 })]
	public static void copy4D(int[][][][] to, int[][][][] from)
	{
		for (int i = 0; i < java.lang.Math.min(to.Length, from.Length); i++)
		{
			copy3D(to[i], from[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 30, 130, 104, 45, 167 })]
	public static int fill5D(int[][][][][] to, int[] from, int index)
	{
		for (int i = 0; i < (nint)to.LongLength; i++)
		{
			index = fill4D(to[i], from, index);
		}
		return index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 28, 98, 104, 45, 167 })]
	public static int fill4D(int[][][][] to, int[] from, int index)
	{
		for (int i = 0; i < (nint)to.LongLength; i++)
		{
			index = fill3D(to[i], from, index);
		}
		return index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 26, 66, 104, 45, 167 })]
	public static int fill3D(int[][][] to, int[] from, int index)
	{
		for (int i = 0; i < (nint)to.LongLength; i++)
		{
			index = fill2D(to[i], from, index);
		}
		return index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 25, 162, 104, 45, 167 })]
	public static int fill2D(int[][] to, int[] from, int index)
	{
		for (int i = 0; i < (nint)to.LongLength; i++)
		{
			index = fill1D(to[i], from, index);
		}
		return index;
	}

	[LineNumberTable(new byte[] { 159, 23, 130, 104, 44, 167 })]
	public static int fill1D(int[] to, int[] from, int index)
	{
		for (int i = 0; i < (nint)to.LongLength; i++)
		{
			int num = i;
			int num2 = index;
			index++;
			to[num] = from[num2];
		}
		return index;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public ArrayUtil()
	{
	}

	[Signature("<T:Ljava/lang/Object;>([TT;)V")]
	[LineNumberTable(new byte[] { 159, 138, 162, 104, 41, 167, 101 })]
	public static void shiftRight1(object[] array)
	{
		for (int i = 1; i < (nint)array.LongLength; i++)
		{
			array[i] = array[i - 1];
		}
		array[0] = null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>([TT;I)V")]
	[LineNumberTable(new byte[] { 159, 131, 162, 108 })]
	public static void shiftLeft2(object[] array, int from)
	{
		shiftLeft3(array, from, array.Length);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>([TT;I)V")]
	[LineNumberTable(new byte[] { 159, 130, 162, 107 })]
	public static void shiftRight2(object[] array, int to)
	{
		shiftRight3(array, 0, to);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 127, 162, 99, 104, 39, 167 })]
	public static int sumInt(int[] array)
	{
		int result = 0;
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			result += array[i];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 123, 162, 99, 105, 39, 167 })]
	public static int sumInt3(int[] array, int from, int count)
	{
		int result = 0;
		for (int i = from; i < from + count; i++)
		{
			result += array[i];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 119, 162, 104, 45, 135 })]
	public static void addInt(int[] array, int val)
	{
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			int num = i;
			array[num] += val;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 66, 100, 106, 100, 138, 108, 108, 109 })]
	public static int[] addAllInt(int[] array1, int[] array2)
	{
		if (array1 == null)
		{
			int[] result = cloneInt(array2);
			
			return result;
		}
		if (array2 == null)
		{
			int[] result2 = cloneInt(array1);
			
			return result2;
		}
		int[] joinedArray = new int[(nint)array1.LongLength + (nint)array2.LongLength];
		ByteCodeHelper.arraycopy_primitive_4(array1, 0, joinedArray, 0, array1.Length);
		ByteCodeHelper.arraycopy_primitive_4(array2, 0, joinedArray, array1.Length, array2.Length);
		return joinedArray;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 66, 100, 106, 100, 138, 108, 108, 109 })]
	public static long[] addAllLong(long[] array1, long[] array2)
	{
		if (array1 == null)
		{
			long[] result = cloneLong(array2);
			
			return result;
		}
		if (array2 == null)
		{
			long[] result2 = cloneLong(array1);
			
			return result2;
		}
		long[] joinedArray = new long[(nint)array1.LongLength + (nint)array2.LongLength];
		ByteCodeHelper.arraycopy_primitive_8(array1, 0, joinedArray, 0, array1.Length);
		ByteCodeHelper.arraycopy_primitive_8(array2, 0, joinedArray, array1.Length, array2.Length);
		return joinedArray;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 66, 100, 106, 100, 138, 159, 2, 108,
		109
	})]
	public static object[] addAllObj(object[] array1, object[] array2)
	{
		if (array1 == null)
		{
			object[] result = cloneObj(array2);
			
			return result;
		}
		if (array2 == null)
		{
			object[] result2 = cloneObj(array1);
			
			return result2;
		}
		object[] joinedArray = (object[])java.lang.reflect.Array.newInstance(java.lang.Object.instancehelper_getClass(array1).getComponentType(), (int)((nint)array1.LongLength + (nint)array2.LongLength));
		ByteCodeHelper.arraycopy(array1, 0, joinedArray, 0, array1.Length);
		ByteCodeHelper.arraycopy(array2, 0, joinedArray, array1.Length, array2.Length);
		return joinedArray;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 98, 105, 104, 44, 167 })]
	public static byte[][] toByteArrayShifted2(int[][] intArray)
	{
		byte[][] result = new byte[(nint)intArray.LongLength][];
		for (int i = 0; i < (nint)intArray.LongLength; i++)
		{
			result[i] = toByteArrayShifted(intArray[i]);
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 99, 98, 105, 104, 46, 167 })]
	public static int[] toIntArrayUnshifted(byte[] bytes)
	{
		int[] result = new int[(nint)bytes.LongLength];
		for (int i = 0; i < (nint)result.LongLength; i++)
		{
			result[i] = (sbyte)(bytes[i] + 128);
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 97, 98, 105, 104, 40, 135 })]
	public static byte[] toByteArray(int[] ints)
	{
		byte[] result = new byte[(nint)ints.LongLength];
		for (int i = 0; i < (nint)ints.LongLength; i++)
		{
			result[i] = (byte)(sbyte)ints[i];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 95, 66, 105, 104, 39, 167 })]
	public static int[] toIntArray(byte[] bytes)
	{
		int[] result = new int[(nint)bytes.LongLength];
		for (int i = 0; i < (nint)result.LongLength; i++)
		{
			result[i] = bytes[i];
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 93, 66, 105, 104, 39, 135 })]
	public static int[] toUnsignedIntArray(byte[] bytes)
	{
		int[] result = new int[(nint)bytes.LongLength];
		for (int i = 0; i < (nint)bytes.LongLength; i++)
		{
			result[i] = bytes[i];
		}
		return result;
	}

	[Signature("<T:Ljava/lang/Object;>([TT;)V")]
	[LineNumberTable(new byte[] { 159, 92, 162, 111, 101, 103, 229, 61, 235, 69 })]
	public static void reverse(object[] frames)
	{
		int i = 0;
		int j = (int)((nint)frames.LongLength - 1);
		while (i < (nint)frames.LongLength >> 1)
		{
			object tmp = frames[i];
			frames[i] = frames[j];
			frames[j] = tmp;
			i++;
			j += -1;
		}
	}

	[LineNumberTable(new byte[] { 159, 90, 162, 103, 104, 103, 5, 231, 70 })]
	public static int max(int[] array)
	{
		int max = int.MinValue;
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			if (array[i] > max)
			{
				max = array[i];
			}
		}
		return max;
	}

	[LineNumberTable(new byte[]
	{
		159, 87, 130, 127, 10, 104, 108, 45, 41, 231,
		69
	})]
	public static int[][] rotate(int[][] src)
	{
		IntPtr intPtr = (nint)src[0].LongLength;
		IntPtr intPtr2 = (nint)src.LongLength;
		int[] array = new int[2];
		int num = (array[1] = (int)(nint)intPtr2);
		num = (array[0] = (int)(nint)intPtr);
		int[][] dst = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int i = 0; i < (nint)src.LongLength; i++)
		{
			for (int j = 0; j < (nint)src[0].LongLength; j++)
			{
				dst[j][i] = src[i][j];
			}
		}
		return dst;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 162, 105, 99, 104, 123, 16, 199, 107,
		104, 113, 116, 46, 137, 236, 61, 240, 69, 109,
		102
	})]
	public static void printMatrixBytes(byte[] array, string format, int width)
	{
		string[] strings = new string[(nint)array.LongLength];
		int maxLen = 0;
		for (int j = 0; j < (nint)array.LongLength; j++)
		{
			strings[j] = java.lang.String.format(format, java.lang.Byte.valueOf(array[j]));
			maxLen = java.lang.Math.max(maxLen, java.lang.String.instancehelper_length(strings[j]));
		}
		int ind = 0;
		while (ind < (nint)strings.LongLength)
		{
			StringBuilder builder = new StringBuilder();
			int i = 0;
			while (i < width && ind < (nint)strings.LongLength)
			{
				for (int k = 0; k < maxLen - java.lang.String.instancehelper_length(strings[ind]) + 1; k++)
				{
					builder.append(" ");
				}
				builder.append(strings[ind]);
				i++;
				ind++;
			}
			java.lang.System.@out.println(builder);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 78, 98, 105, 99, 104, 123, 16, 199, 107,
		104, 113, 116, 46, 137, 236, 61, 240, 69, 109,
		102
	})]
	public static void printMatrix(int[] array, string format, int width)
	{
		string[] strings = new string[(nint)array.LongLength];
		int maxLen = 0;
		for (int j = 0; j < (nint)array.LongLength; j++)
		{
			strings[j] = java.lang.String.format(format, Integer.valueOf(array[j]));
			maxLen = java.lang.Math.max(maxLen, java.lang.String.instancehelper_length(strings[j]));
		}
		int ind = 0;
		while (ind < (nint)strings.LongLength)
		{
			StringBuilder builder = new StringBuilder();
			int i = 0;
			while (i < width && ind < (nint)strings.LongLength)
			{
				for (int k = 0; k < maxLen - java.lang.String.instancehelper_length(strings[ind]) + 1; k++)
				{
					builder.append(" ");
				}
				builder.append(strings[ind]);
				i++;
				ind++;
			}
			java.lang.System.@out.println(builder);
		}
	}

	[LineNumberTable(new byte[] { 159, 74, 162, 107, 104, 41, 135 })]
	public static byte[] padLeft(byte[] array, int padLength)
	{
		byte[] result = new byte[(nint)array.LongLength + padLength];
		for (int i = padLength; i < (nint)result.LongLength; i++)
		{
			result[i] = array[i - padLength];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 72, 130, 101, 104, 103, 54, 135 })]
	public static int[] randomIntArray(int size, int from, int to)
	{
		int width = to - from;
		int[] result = new int[size];
		for (int i = 0; i < size; i++)
		{
			result[i] = ByteCodeHelper.d2i(java.lang.Math.random() * (double)width % (double)width) + from;
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 70, 129, 71, 102, 104, 105, 57, 137 })]
	public static byte[] randomByteArray(int size, byte from, byte to)
	{
		int to2 = (sbyte)to;
		int from2 = (sbyte)from;
		int width = (sbyte)(to2 - from2);
		byte[] result = new byte[size];
		for (int i = 0; i < size; i++)
		{
			result[i] = (byte)(sbyte)ByteCodeHelper.d2i(java.lang.Math.random() * (double)width % (double)width + (double)from2);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 98, 108, 104 })]
	public static int[] flatten5D(int[][][][][] @is)
	{
		IntArrayList list = new IntArrayList(128);
		flatten5DL(@is, list);
		int[] result = list.toArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 53, 162, 108, 104 })]
	public static int[] flatten4D(int[][][][] @is)
	{
		IntArrayList list = new IntArrayList(128);
		flatten4DL(@is, list);
		int[] result = list.toArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 51, 98, 108, 104 })]
	public static int[] flatten3D(int[][][] @is)
	{
		IntArrayList list = new IntArrayList(128);
		flatten3DL(@is, list);
		int[] result = list.toArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 50, 162, 108, 104 })]
	public static int[] flatten2D(int[][] @is)
	{
		IntArrayList list = new IntArrayList(128);
		flatten2DL(@is, list);
		int[] result = list.toArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 32, 162, 104, 45, 167 })]
	public static int fill6D(int[][][][][][] to, int[] from, int index)
	{
		for (int i = 0; i < (nint)to.LongLength; i++)
		{
			index = fill5D(to[i], from, index);
		}
		return index;
	}
}
