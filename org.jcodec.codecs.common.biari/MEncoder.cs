using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.common.biari;

public class MEncoder : Object
{
	private ByteBuffer @out;

	private int range;

	private int offset;

	private int onesOutstanding;

	private bool zeroBorrowed;

	private int outReg;

	private int bitsInOutReg;

	private int[][] models;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 130, 113, 110, 106, 110, 115, 145, 115,
		168, 111, 148
	})]
	private void renormalize()
	{
		while (range < 256)
		{
			if (offset < 256)
			{
				flushOutstanding(0);
			}
			else if (offset < 512)
			{
				offset &= 255;
				onesOutstanding++;
			}
			else
			{
				offset &= 511;
				flushOutstanding(1);
			}
			range <<= 1;
			offset <<= 1;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 162, 105, 136, 101, 106, 40, 177, 104 })]
	private void flushOutstanding(int hasCarry)
	{
		if (zeroBorrowed)
		{
			putBit(hasCarry);
		}
		int trailingBit = 1 - hasCarry;
		while (onesOutstanding > 0)
		{
			putBit(trailingBit);
			onesOutstanding--;
		}
		zeroBorrowed = true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 130, 113, 143, 106, 116, 104, 136 })]
	private void putBit(int bit)
	{
		outReg = (outReg << 1) | bit;
		bitsInOutReg++;
		if (bitsInOutReg == 8)
		{
			@out.put((byte)(sbyte)outReg);
			outReg = 0;
			bitsInOutReg = 0;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 98, 105, 145, 113, 123, 116, 104, 136 })]
	private void stuffBits()
	{
		if (bitsInOutReg == 0)
		{
			@out.put(128);
			return;
		}
		outReg = (outReg << 1) | 1;
		outReg <<= 8 - (bitsInOutReg + 1);
		@out.put((byte)(sbyte)outReg);
		outReg = 0;
		bitsInOutReg = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 105, 140, 104, 104 })]
	public MEncoder(ByteBuffer @out, int[][] models)
	{
		range = 510;
		this.models = models;
		this.@out = @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 108, 116, 143, 110, 116, 104, 109,
		151, 157, 111, 180, 105
	})]
	public virtual void encodeBin(int model, int bin)
	{
		int qs = (range >> 6) & 3;
		int rangeLPS = MConst.___003C_003ErangeLPS[qs][models[0][model]];
		range -= rangeLPS;
		if (bin != models[1][model])
		{
			offset += range;
			range = rangeLPS;
			if (models[0][model] == 0)
			{
				models[1][model] = 1 - models[1][model];
			}
			models[0][model] = MConst.___003C_003EtransitLPS[models[0][model]];
		}
		else if (models[0][model] < 62)
		{
			int[] array = models[0];
			array[model]++;
		}
		renormalize();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 130, 111, 101, 180, 111, 104, 117, 111,
		115, 145, 138
	})]
	public virtual void encodeBinBypass(int bin)
	{
		offset <<= 1;
		if (bin == 1)
		{
			offset += range;
		}
		if (((uint)offset & 0x400u) != 0)
		{
			flushOutstanding(1);
			offset &= 1023;
		}
		else if (((uint)offset & 0x200u) != 0)
		{
			offset &= 511;
			onesOutstanding++;
		}
		else
		{
			flushOutstanding(0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 98, 111, 100, 137, 116, 104, 137 })]
	public virtual void encodeBinFinal(int bin)
	{
		range -= 2;
		if (bin == 0)
		{
			renormalize();
			return;
		}
		offset += range;
		range = 2;
		renormalize();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 66, 114, 113, 105 })]
	public virtual void finishEncoding()
	{
		flushOutstanding((offset >> 9) & 1);
		putBit((offset >> 8) & 1);
		stuffBits();
	}
}
