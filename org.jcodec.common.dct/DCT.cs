using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.api;

namespace org.jcodec.common.dct;

public abstract class DCT : Object
{
	public abstract int[] decode(int[] iarr);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public DCT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public virtual short[] encode(byte[] orig)
	{
		
		throw new NotSupportedException("");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 104, 45, 167 })]
	public virtual void decodeAll(int[][] src)
	{
		for (int i = 0; i < (nint)src.LongLength; i++)
		{
			src[i] = decode(src[i]);
		}
	}
}
