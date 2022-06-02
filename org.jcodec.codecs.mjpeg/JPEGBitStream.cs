using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mjpeg;

public class JPEGBitStream : Object
{
	private VLC[] huff;

	private BitReader _in;

	private int[] dcPredictor;

	private int lumaLen;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 110 })]
	public virtual int readDCValue(int prevDC, VLC table)
	{
		int code = table.readVLC(_in);
		return (code == 0) ? prevDC : (toValue(_in.readNBit(code), code) + prevDC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 131, 110, 105, 104, 101, 101, 101,
		102, 119, 133, 108
	})]
	public virtual void readACValues(int[] target, VLC table)
	{
		int curOff = 1;
		int code;
		do
		{
			code = table.readVLC(_in);
			if (code == 240)
			{
				curOff += 16;
			}
			else if (code > 0)
			{
				int rle = code >> 4;
				curOff += rle;
				int len = code & 0xF;
				target[curOff] = toValue(_in.readNBit(len), len);
				curOff++;
			}
		}
		while (code != 0 && curOff < 64);
	}

	[LineNumberTable(70)]
	public int toValue(int raw, int length)
	{
		return (length < 1 || raw >= 1 << length - 1) ? raw : (-(1 << length) + 1 + raw);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 105, 109, 109, 104, 104 })]
	public JPEGBitStream(ByteBuffer b, VLC[] huff, int lumaLen)
	{
		dcPredictor = new int[3];
		_in = BitReader.createBitReader(b);
		this.huff = huff;
		this.lumaLen = lumaLen;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 99, 108, 127, 14, 18, 235, 69,
		127, 14, 114, 133, 127, 14, 114, 101
	})]
	public virtual void readMCU(int[][] buf)
	{
		int blk = 0;
		int i = 0;
		int num;
		int num2;
		int[] array2;
		while (i < lumaLen)
		{
			int[] array = dcPredictor;
			int[] obj = buf[blk];
			num = readDCValue(dcPredictor[0], huff[0]);
			num2 = 0;
			array2 = obj;
			int num3 = num;
			array2[num2] = num;
			array[0] = num3;
			readACValues(buf[blk], huff[2]);
			i++;
			blk++;
		}
		int[] array3 = dcPredictor;
		int[] obj2 = buf[blk];
		num = readDCValue(dcPredictor[1], huff[1]);
		num2 = 0;
		array2 = obj2;
		int num4 = num;
		array2[num2] = num;
		array3[1] = num4;
		readACValues(buf[blk], huff[3]);
		blk++;
		int[] array4 = dcPredictor;
		int[] obj3 = buf[blk];
		num = readDCValue(dcPredictor[2], huff[1]);
		num2 = 0;
		array2 = obj3;
		int num5 = num;
		array2[num2] = num;
		array4[2] = num5;
		readACValues(buf[blk], huff[3]);
		blk++;
	}
}
