using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.common.biari;

public class MDecoder : Object
{
	private ByteBuffer _in;

	private int range;

	private int code;

	private int nBitsPending;

	private int[][] cm;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 103, 106, 113, 111, 103, 111, 112 })]
	protected internal virtual void initCodeRegister()
	{
		readOneByte();
		if (nBitsPending != 8)
		{
			
			throw new RuntimeException("Empty stream");
		}
		code <<= 8;
		readOneByte();
		code <<= 1;
		nBitsPending -= 9;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 110, 98, 116, 111, 111 })]
	protected internal virtual void readOneByte()
	{
		if (_in.hasRemaining())
		{
			int b = (sbyte)_in.get() & 0xFF;
			code |= b;
			nBitsPending += 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 66, 110, 111, 111, 147, 111, 106, 137 })]
	private void renormalize()
	{
		while (range < 256)
		{
			range <<= 1;
			code <<= 1;
			code &= 131071;
			nBitsPending--;
			if (nBitsPending <= 0)
			{
				readOneByte();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 108, 136, 105 })]
	public MDecoder(ByteBuffer _in, int[][] cm)
	{
		this._in = _in;
		range = 510;
		this.cm = cm;
		initCodeRegister();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 108, 116, 111, 138, 138, 111, 151,
		135, 175, 104, 143, 135, 143, 109, 151, 219
	})]
	public virtual int decodeBin(int m)
	{
		int qIdx = (range >> 6) & 3;
		int rLPS = MConst.___003C_003ErangeLPS[qIdx][cm[0][m]];
		range -= rLPS;
		int rs8 = range << 8;
		int bin;
		if (code < rs8)
		{
			if (cm[0][m] < 62)
			{
				int[] array = cm[0];
				array[m]++;
			}
			renormalize();
			bin = cm[1][m];
		}
		else
		{
			range = rLPS;
			code -= rs8;
			renormalize();
			bin = 1 - cm[1][m];
			if (cm[0][m] == 0)
			{
				cm[1][m] = 1 - cm[1][m];
			}
			cm[0][m] = MConst.___003C_003EtransitLPS[cm[0][m]];
		}
		return bin;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 130, 143, 113, 135, 163 })]
	public virtual int decodeFinalBin()
	{
		range -= 2;
		if (code < range << 8)
		{
			renormalize();
			return 0;
		}
		return 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 143, 111, 106, 135, 113, 133, 163,
		104
	})]
	public virtual int decodeBinBypass()
	{
		code <<= 1;
		nBitsPending--;
		if (nBitsPending <= 0)
		{
			readOneByte();
		}
		int tmp = code - (range << 8);
		if (tmp < 0)
		{
			return 0;
		}
		code = tmp;
		return 1;
	}
}
