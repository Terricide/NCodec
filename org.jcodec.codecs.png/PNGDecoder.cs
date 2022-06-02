using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.util;
using java.util.zip;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.png;

public class PNGDecoder : VideoDecoder
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : Object
	{
		_1()
		{
			throw null;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class PLTE : Object
	{
		internal int[] palette;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Synthetic)]
		[LineNumberTable(400)]
		internal PLTE(_1 x0)
			: this()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 41, 98, 119, 113, 101, 141, 103, 127, 16,
			15, 199, 105, 46, 135, 104
		})]
		public virtual void parse(ByteBuffer data, int length)
		{
			if ((3 != -1 && length % 3 != 0) || length > 768)
			{
				
				throw new RuntimeException("Invalid data");
			}
			int j = length / 3;
			palette = new int[j];
			int i;
			for (i = 0; i < j; i++)
			{
				palette[i] = -16777216 | (((sbyte)data.get() & 0xFF) << 16) | (((sbyte)data.get() & 0xFF) << 8) | ((sbyte)data.get() & 0xFF);
			}
			for (; i < 256; i++)
			{
				palette[i] = -16777216;
			}
			data.getInt();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(400)]
		private PLTE()
		{
		}
	}

	public class TRNS : Object
	{
		private int colorType;

		internal byte[] alphaPal;

		internal byte alphaGrey;

		internal byte alphaR;

		internal byte alphaG;

		internal byte alphaB;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 35, 161, 68, 105, 104 })]
		internal TRNS(byte colorType)
		{
			int colorType2 = (sbyte)colorType;
			base._002Ector();
			this.colorType = colorType2;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 33, 66, 106, 113, 112, 107, 42, 169, 105,
			112, 106, 110, 110, 142, 104
		})]
		public virtual void parse(ByteBuffer data, int length)
		{
			if (colorType == 3)
			{
				alphaPal = new byte[256];
				data.get(alphaPal, 0, length);
				for (int i = length; i < 256; i++)
				{
					alphaPal[i] = byte.MaxValue;
				}
			}
			else if (colorType == 0)
			{
				alphaGrey = (byte)(sbyte)data.get();
			}
			else if (colorType == 2)
			{
				alphaR = (byte)(sbyte)data.get();
				alphaG = (byte)(sbyte)data.get();
				alphaG = (byte)(sbyte)data.get();
			}
			data.getInt();
		}
	}

	private const int FILTER_TYPE_LOCO = 64;

	private const int FILTER_VALUE_NONE = 0;

	private const int FILTER_VALUE_SUB = 1;

	private const int FILTER_VALUE_UP = 2;

	private const int FILTER_VALUE_AVG = 3;

	private const int FILTER_VALUE_PAETH = 4;

	private const int PNG_COLOR_TYPE_GRAY = 0;

	private const int PNG_COLOR_TYPE_PALETTE = 3;

	private const int PNG_COLOR_TYPE_RGB = 2;

	private const int alphaR = 127;

	private const int alphaG = 127;

	private const int alphaB = 127;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] logPassStep;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] logPassRowStep;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] passOff;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] passRowOff;

	private byte[] ca;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 66, 105, 109 })]
	public PNGDecoder()
	{
		ca = new byte[4];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 23, 162, 104, 104, 127, 7 })]
	private static bool ispng(ByteBuffer data)
	{
		int sighi = data.getInt();
		int siglo = data.getInt();
		return ((sighi == -1991225785 || sighi == -1974645177) && (siglo == 218765834 || siglo == 218765834)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.util.zip.DataFormatException" })]
	[Signature("(Lorg/jcodec/codecs/png/IHDR;Lorg/jcodec/codecs/png/PNGDecoder$PLTE;Lorg/jcodec/codecs/png/PNGDecoder$TRNS;Ljava/util/List<Ljava/nio/ByteBuffer;>;[[B)V")]
	[LineNumberTable(new byte[]
	{
		159,
		111,
		66,
		108,
		111,
		103,
		137,
		140,
		106,
		107,
		103,
		137,
		114,
		122,
		107,
		112,
		107,
		144,
		108,
		106,
		113,
		114,
		107,
		118,
		105,
		127,
		1,
		134,
		110,
		110,
		105,
		111,
		103,
		127,
		1,
		198,
		103,
		159,
		2,
		112,
		131,
		111,
		131,
		110,
		131,
		111,
		131,
		208,
		101,
		111,
		111,
		111,
		122,
		123,
		249,
		60,
		250,
		70,
		111,
		108,
		116,
		120,
		248,
		61,
		250,
		70,
		111,
		63,
		35,
		245,
		69,
		111,
		117,
		114,
		123,
		127,
		0,
		byte.MaxValue,
		0,
		60,
		250,
		70,
		103,
		110,
		115,
		121,
		123,
		127,
		0,
		byte.MaxValue,
		0,
		60,
		250,
		70,
		110,
		118,
		118,
		118,
		118,
		115,
		127,
		7,
		106,
		108,
		236,
		60,
		245,
		72,
		111,
		112,
		111,
		106,
		108,
		236,
		60,
		242,
		73,
		241,
		159,
		162,
		237,
		47,
		236,
		160,
		114
	})]
	private void decodeData(IHDR ihdr, PLTE plte, TRNS trns, List list, byte[][] buffer)
	{
		int bpp = ihdr.getBitsPerPixel() + 7 >> 3;
		int passes = (((sbyte)ihdr.interlaceType == 0) ? 1 : 7);
		Inflater inflater = new Inflater();
		Iterator it = list.iterator();
		for (int pass = 0; pass < passes; pass++)
		{
			int rowSize;
			int colStart;
			int rowStart;
			int colStep;
			int rowStep;
			if ((sbyte)ihdr.interlaceType == 0)
			{
				rowSize = ihdr.rowSize() + 1;
				colStart = (rowStart = 0);
				colStep = (rowStep = 1);
			}
			else
			{
				int round = (1 << logPassStep[pass]) - 1;
				rowSize = (ihdr.width + round >> logPassStep[pass]) + 1;
				rowStart = passRowOff[pass];
				rowStep = 1 << logPassRowStep[pass];
				colStart = passOff[pass];
				colStep = 1 << logPassStep[pass];
			}
			byte[] lastRow = new byte[rowSize - 1];
			byte[] uncompressed = new byte[rowSize];
			int bptr = 3 * (ihdr.width * rowStart + colStart);
			for (int row = rowStart; row < ihdr.height; row += rowStep)
			{
				int count = inflater.inflate(uncompressed);
				if (count < (nint)uncompressed.LongLength && inflater.needsInput())
				{
					if (!it.hasNext())
					{
						Logger.warn(String.format("Data truncation at row %d", Integer.valueOf(row)));
						break;
					}
					ByteBuffer next = (ByteBuffer)it.next();
					inflater.setInput(NIOUtils.toArray(next));
					int toRead = (int)((nint)uncompressed.LongLength - count);
					count = inflater.inflate(uncompressed, count, toRead);
					if (count != toRead)
					{
						Logger.warn(String.format("Data truncation at row %d", Integer.valueOf(row)));
						break;
					}
				}
				switch (uncompressed[0])
				{
				case 0:
					ByteCodeHelper.arraycopy_primitive_1(uncompressed, 1, lastRow, 0, rowSize - 1);
					break;
				case 1:
					filterSub(uncompressed, rowSize - 1, lastRow, bpp);
					break;
				case 2:
					filterUp(uncompressed, rowSize - 1, lastRow);
					break;
				case 3:
					filterAvg(uncompressed, rowSize - 1, lastRow, bpp);
					break;
				case 4:
					filterPaeth(uncompressed, rowSize - 1, lastRow, bpp);
					break;
				}
				int bptrWas = bptr;
				if (((uint)(sbyte)ihdr.colorType & (true ? 1u : 0u)) != 0)
				{
					int i2 = 0;
					while (i2 < rowSize - 1)
					{
						int plt = plte.palette[lastRow[i2]];
						buffer[0][bptr] = (byte)(sbyte)(((plt >> 16) & 0xFF) - 128);
						buffer[0][bptr + 1] = (byte)(sbyte)(((plt >> 8) & 0xFF) - 128);
						buffer[0][bptr + 2] = (byte)(sbyte)((plt & 0xFF) - 128);
						i2 += bpp;
						bptr += 3 * colStep;
					}
				}
				else if (((uint)(sbyte)ihdr.colorType & 2u) != 0)
				{
					int n = 0;
					while (n < rowSize - 1)
					{
						buffer[0][bptr] = (byte)(sbyte)(lastRow[n] - 128);
						buffer[0][bptr + 1] = (byte)(sbyte)(lastRow[n + 1] - 128);
						buffer[0][bptr + 2] = (byte)(sbyte)(lastRow[n + 2] - 128);
						n += bpp;
						bptr += 3 * colStep;
					}
				}
				else
				{
					int m = 0;
					while (m < rowSize - 1)
					{
						byte[] obj = buffer[0];
						int num = bptr;
						byte[] obj2 = buffer[0];
						int num2 = bptr + 1;
						byte[] obj3 = buffer[0];
						int num3 = bptr + 2;
						int num4 = (sbyte)(lastRow[m] - 128);
						int num5 = num3;
						byte[] array = obj3;
						int num6 = num4;
						array[num5] = (byte)num4;
						num4 = num6;
						num5 = num2;
						array = obj2;
						int num7 = num4;
						array[num5] = (byte)num4;
						obj[num] = (byte)num7;
						m += bpp;
						bptr += 3 * colStep;
					}
				}
				if (((uint)(sbyte)ihdr.colorType & 4u) != 0)
				{
					int l = bpp - 1;
					int j5 = bptrWas;
					while (l < rowSize - 1)
					{
						int alpha2 = lastRow[l];
						int nalpha2 = 256 - alpha2;
						buffer[0][j5] = (byte)(sbyte)(127 * nalpha2 + buffer[0][j5] * alpha2 >> 8);
						buffer[0][j5 + 1] = (byte)(sbyte)(127 * nalpha2 + buffer[0][j5 + 1] * alpha2 >> 8);
						buffer[0][j5 + 2] = (byte)(sbyte)(127 * nalpha2 + buffer[0][j5 + 2] * alpha2 >> 8);
						l += bpp;
						j5 += 3 * colStep;
					}
				}
				else if (trns != null)
				{
					if ((sbyte)ihdr.colorType == 3)
					{
						int k = 0;
						int j4 = bptrWas;
						while (k < rowSize - 1)
						{
							int alpha = trns.alphaPal[lastRow[k]];
							int nalpha = 256 - alpha;
							buffer[0][j4] = (byte)(sbyte)(127 * nalpha + buffer[0][j4] * alpha >> 8);
							buffer[0][j4 + 1] = (byte)(sbyte)(127 * nalpha + buffer[0][j4 + 1] * alpha >> 8);
							buffer[0][j4 + 2] = (byte)(sbyte)(127 * nalpha + buffer[0][j4 + 2] * alpha >> 8);
							k++;
							j4 += 3 * colStep;
						}
					}
					else if ((sbyte)ihdr.colorType == 2)
					{
						int ar = ((sbyte)trns.alphaR & 0xFF) - 128;
						int ag = ((sbyte)trns.alphaG & 0xFF) - 128;
						int ab = ((sbyte)trns.alphaB & 0xFF) - 128;
						if (ab != 127 || ag != 127 || ar != 127)
						{
							int j = 0;
							int j3 = bptrWas;
							while (j < rowSize - 1)
							{
								if (buffer[0][j3] == ar && buffer[0][j3 + 1] == ag && buffer[0][j3 + 2] == ab)
								{
									buffer[0][j3] = 127;
									buffer[0][j3 + 1] = 127;
									buffer[0][j3 + 2] = 127;
								}
								j += bpp;
								j3 += 3 * colStep;
							}
						}
					}
					else if ((sbyte)ihdr.colorType == 0)
					{
						int i = 0;
						int j2 = bptrWas;
						while (i < rowSize - 1)
						{
							if (lastRow[i] == (sbyte)trns.alphaGrey)
							{
								buffer[0][j2] = 127;
								buffer[0][j2 + 1] = 127;
								buffer[0][j2 + 2] = 127;
							}
							i++;
							j2 += 3 * colStep;
						}
					}
				}
				bptr = bptrWas + 3 * ihdr.width * rowStep;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 74, 98, 151, 105, 131, 105, 131, 105, 131,
		139
	})]
	private static void filterSub(byte[] uncompressed, int rowSize, byte[] lastRow, int bpp)
	{
		switch (bpp)
		{
		case 1:
			filterSub1(uncompressed, lastRow, rowSize);
			break;
		case 2:
			filterSub2(uncompressed, lastRow, rowSize);
			break;
		case 3:
			filterSub3(uncompressed, lastRow, rowSize);
			break;
		default:
			filterSub4(uncompressed, lastRow, rowSize);
			break;
		}
	}

	[LineNumberTable(new byte[] { 159, 64, 98, 103, 46, 167 })]
	private static void filterUp(byte[] uncompressed, int rowSize, byte[] lastRow)
	{
		for (int i = 0; i < rowSize; i++)
		{
			lastRow[i] = (byte)(sbyte)(lastRow[i] + uncompressed[i + 1]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 70, 98, 151, 105, 131, 105, 131, 105, 131,
		171
	})]
	private static void filterAvg(byte[] uncompressed, int rowSize, byte[] lastRow, int bpp)
	{
		switch (bpp)
		{
		case 1:
			filterAvg1(uncompressed, lastRow, rowSize);
			break;
		case 2:
			filterAvg2(uncompressed, lastRow, rowSize);
			break;
		case 3:
			filterAvg3(uncompressed, lastRow, rowSize);
			break;
		default:
			filterAvg4(uncompressed, lastRow, rowSize);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 130, 104, 108, 14, 199, 107, 104, 101,
		119, 103, 135, 106, 106, 141, 109, 102, 103, 134,
		101, 120, 237, 46, 234, 84
	})]
	private void filterPaeth(byte[] uncompressed, int rowSize, byte[] lastRow, int bpp)
	{
		for (int j = 0; j < bpp; j++)
		{
			ca[j] = lastRow[j];
			lastRow[j] = (byte)(sbyte)(uncompressed[j + 1] + lastRow[j]);
		}
		for (int i = bpp; i < rowSize; i++)
		{
			int a = lastRow[i - bpp];
			int b = lastRow[i];
			byte[] array = ca;
			int num = i;
			int c = array[(bpp != -1) ? (num % bpp) : 0];
			int p = b - c;
			int pc = a - c;
			int pa = MathUtil.abs(p);
			int pb = MathUtil.abs(pc);
			pc = MathUtil.abs(p + pc);
			p = ((pa <= pb && pa <= pc) ? a : ((pb > pc) ? c : b));
			byte[] array2 = ca;
			int num2 = i;
			array2[(bpp != -1) ? (num2 % bpp) : 0] = lastRow[i];
			lastRow[i] = (byte)(sbyte)(p + uncompressed[i + 1]);
		}
	}

	[LineNumberTable(new byte[] { 159, 66, 130, 111, 105, 60, 169 })]
	private static void filterSub1(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = uncompressed[1];
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p = num3;
		for (int i = 1; i < rowSize; i++)
		{
			int num4 = i;
			num = (sbyte)((p & 0xFF) + uncompressed[i + 1]);
			num2 = num4;
			array = lastRow;
			int num5 = num;
			array[num2] = (byte)num;
			p = num5;
		}
	}

	[LineNumberTable(new byte[] { 159, 61, 130, 111, 112, 108, 124, 31, 1, 204 })]
	private static void filterSub2(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = uncompressed[1];
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p0 = num3;
		num = uncompressed[2];
		num2 = 1;
		array = lastRow;
		int num4 = num;
		array[num2] = (byte)num;
		int p1 = num4;
		for (int i = 2; i < rowSize; i += 2)
		{
			int num5 = i;
			num = (sbyte)((p0 & 0xFF) + uncompressed[1 + i]);
			num2 = num5;
			array = lastRow;
			int num6 = num;
			array[num2] = (byte)num;
			p0 = num6;
			int num7 = i + 1;
			num = (sbyte)((p1 & 0xFF) + uncompressed[2 + i]);
			num2 = num7;
			array = lastRow;
			int num8 = num;
			array[num2] = (byte)num;
			p1 = num8;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		56,
		98,
		111,
		112,
		112,
		108,
		124,
		127,
		1,
		byte.MaxValue,
		1,
		61,
		236,
		69
	})]
	private static void filterSub3(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = uncompressed[1];
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p0 = num3;
		num = uncompressed[2];
		num2 = 1;
		array = lastRow;
		int num4 = num;
		array[num2] = (byte)num;
		int p1 = num4;
		num = uncompressed[3];
		num2 = 2;
		array = lastRow;
		int num5 = num;
		array[num2] = (byte)num;
		int p2 = num5;
		for (int i = 3; i < rowSize; i += 3)
		{
			int num6 = i;
			num = (sbyte)((p0 & 0xFF) + uncompressed[i + 1]);
			num2 = num6;
			array = lastRow;
			int num7 = num;
			array[num2] = (byte)num;
			p0 = num7;
			int num8 = i + 1;
			num = (sbyte)((p1 & 0xFF) + uncompressed[i + 2]);
			num2 = num8;
			array = lastRow;
			int num9 = num;
			array[num2] = (byte)num;
			p1 = num9;
			int num10 = i + 2;
			num = (sbyte)((p2 & 0xFF) + uncompressed[i + 3]);
			num2 = num10;
			array = lastRow;
			int num11 = num;
			array[num2] = (byte)num;
			p2 = num11;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		50,
		98,
		111,
		112,
		112,
		112,
		108,
		124,
		127,
		1,
		127,
		1,
		byte.MaxValue,
		1,
		60,
		236,
		70
	})]
	private static void filterSub4(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = uncompressed[1];
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p0 = num3;
		num = uncompressed[2];
		num2 = 1;
		array = lastRow;
		int num4 = num;
		array[num2] = (byte)num;
		int p1 = num4;
		num = uncompressed[3];
		num2 = 2;
		array = lastRow;
		int num5 = num;
		array[num2] = (byte)num;
		int p2 = num5;
		num = uncompressed[4];
		num2 = 3;
		array = lastRow;
		int num6 = num;
		array[num2] = (byte)num;
		int p3 = num6;
		for (int i = 4; i < rowSize; i += 4)
		{
			int num7 = i;
			num = (sbyte)((p0 & 0xFF) + uncompressed[i + 1]);
			num2 = num7;
			array = lastRow;
			int num8 = num;
			array[num2] = (byte)num;
			p0 = num8;
			int num9 = i + 1;
			num = (sbyte)((p1 & 0xFF) + uncompressed[i + 2]);
			num2 = num9;
			array = lastRow;
			int num10 = num;
			array[num2] = (byte)num;
			p1 = num10;
			int num11 = i + 2;
			num = (sbyte)((p2 & 0xFF) + uncompressed[i + 3]);
			num2 = num11;
			array = lastRow;
			int num12 = num;
			array[num2] = (byte)num;
			p2 = num12;
			int num13 = i + 3;
			num = (sbyte)((p3 & 0xFF) + uncompressed[i + 4]);
			num2 = num13;
			array = lastRow;
			int num14 = num;
			array[num2] = (byte)num;
			p3 = num14;
		}
	}

	[LineNumberTable(new byte[] { 159, 63, 162, 118, 105, 63, 4, 169 })]
	private static void filterAvg1(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = (sbyte)(uncompressed[1] + (lastRow[0] >> 1));
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p = num3;
		for (int i = 1; i < rowSize; i++)
		{
			int num4 = i;
			num = (sbyte)((lastRow[i] + (p & 0xFF) >> 1) + uncompressed[i + 1]);
			num2 = num4;
			array = lastRow;
			int num5 = num;
			array[num2] = (byte)num;
			p = num5;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 59, 162, 118, 119, 108, 127, 4, 31, 10,
		236, 69
	})]
	private static void filterAvg2(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = (sbyte)(uncompressed[1] + (lastRow[0] >> 1));
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p0 = num3;
		num = (sbyte)(uncompressed[2] + (lastRow[1] >> 1));
		num2 = 1;
		array = lastRow;
		int num4 = num;
		array[num2] = (byte)num;
		int p1 = num4;
		for (int i = 2; i < rowSize; i += 2)
		{
			int num5 = i;
			num = (sbyte)((lastRow[i] + (p0 & 0xFF) >> 1) + uncompressed[1 + i]);
			num2 = num5;
			array = lastRow;
			int num6 = num;
			array[num2] = (byte)num;
			p0 = num6;
			int num7 = i + 1;
			num = (sbyte)((lastRow[i + 1] + (p1 & 0xFF) >> 1) + uncompressed[i + 2]);
			num2 = num7;
			array = lastRow;
			int num8 = num;
			array[num2] = (byte)num;
			p1 = num8;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		53,
		66,
		118,
		119,
		119,
		108,
		127,
		4,
		159,
		10,
		byte.MaxValue,
		10,
		60,
		236,
		71
	})]
	private static void filterAvg3(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = (sbyte)(uncompressed[1] + (lastRow[0] >> 1));
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p0 = num3;
		num = (sbyte)(uncompressed[2] + (lastRow[1] >> 1));
		num2 = 1;
		array = lastRow;
		int num4 = num;
		array[num2] = (byte)num;
		int p1 = num4;
		num = (sbyte)(uncompressed[3] + (lastRow[2] >> 1));
		num2 = 2;
		array = lastRow;
		int num5 = num;
		array[num2] = (byte)num;
		int p2 = num5;
		for (int i = 3; i < rowSize; i += 3)
		{
			int num6 = i;
			num = (sbyte)((lastRow[i] + (p0 & 0xFF) >> 1) + uncompressed[i + 1]);
			num2 = num6;
			array = lastRow;
			int num7 = num;
			array[num2] = (byte)num;
			p0 = num7;
			int num8 = i + 1;
			num = (sbyte)((lastRow[i + 1] + (p1 & 0xFF) >> 1) + uncompressed[i + 2]);
			num2 = num8;
			array = lastRow;
			int num9 = num;
			array[num2] = (byte)num;
			p1 = num9;
			int num10 = i + 2;
			num = (sbyte)((lastRow[i + 2] + (p2 & 0xFF) >> 1) + uncompressed[i + 3]);
			num2 = num10;
			array = lastRow;
			int num11 = num;
			array[num2] = (byte)num;
			p2 = num11;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		47,
		130,
		118,
		119,
		119,
		119,
		108,
		127,
		4,
		159,
		10,
		159,
		10,
		byte.MaxValue,
		10,
		58,
		236,
		73
	})]
	private static void filterAvg4(byte[] uncompressed, byte[] lastRow, int rowSize)
	{
		int num = (sbyte)(uncompressed[1] + (lastRow[0] >> 1));
		int num2 = 0;
		byte[] array = lastRow;
		int num3 = num;
		array[num2] = (byte)num;
		int p0 = num3;
		num = (sbyte)(uncompressed[2] + (lastRow[1] >> 1));
		num2 = 1;
		array = lastRow;
		int num4 = num;
		array[num2] = (byte)num;
		int p1 = num4;
		num = (sbyte)(uncompressed[3] + (lastRow[2] >> 1));
		num2 = 2;
		array = lastRow;
		int num5 = num;
		array[num2] = (byte)num;
		int p2 = num5;
		num = (sbyte)(uncompressed[4] + (lastRow[3] >> 1));
		num2 = 3;
		array = lastRow;
		int num6 = num;
		array[num2] = (byte)num;
		int p3 = num6;
		for (int i = 4; i < rowSize; i += 4)
		{
			int num7 = i;
			num = (sbyte)((lastRow[i] + (p0 & 0xFF) >> 1) + uncompressed[i + 1]);
			num2 = num7;
			array = lastRow;
			int num8 = num;
			array[num2] = (byte)num;
			p0 = num8;
			int num9 = i + 1;
			num = (sbyte)((lastRow[i + 1] + (p1 & 0xFF) >> 1) + uncompressed[i + 2]);
			num2 = num9;
			array = lastRow;
			int num10 = num;
			array[num2] = (byte)num;
			p1 = num10;
			int num11 = i + 2;
			num = (sbyte)((lastRow[i + 2] + (p2 & 0xFF) >> 1) + uncompressed[i + 3]);
			num2 = num11;
			array = lastRow;
			int num12 = num;
			array[num2] = (byte)num;
			p2 = num12;
			int num13 = i + 3;
			num = (sbyte)((lastRow[i + 3] + (p3 & 0xFF) >> 1) + uncompressed[i + 4]);
			num2 = num13;
			array = lastRow;
			int num14 = num;
			array[num2] = (byte)num;
			p3 = num14;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 130, 105, 145, 99, 99, 99, 103, 109,
		105, 137, 107, 134, 159, 39, 103, 104, 134, 104,
		106, 134, 100, 145, 110, 106, 131, 112, 105, 131,
		105, 131, 147, 102, 132, 191, 1, 3, 99, 131,
		154
	})]
	public override Picture decodeFrame(ByteBuffer data, byte[][] buffer)
	{
		if (!ispng(data))
		{
			
			throw new RuntimeException("Not a PNG file.");
		}
		IHDR ihdr = null;
		PLTE plte = null;
		TRNS trns = null;
		ArrayList list = new ArrayList();
		while (data.remaining() >= 8)
		{
			int length = data.getInt();
			int tag = data.getInt();
			if (data.remaining() < length)
			{
				break;
			}
			switch (tag)
			{
			case 1229472850:
				ihdr = new IHDR();
				ihdr.parse(data);
				break;
			case 1347179589:
				plte = new PLTE(null);
				plte.parse(data, length);
				break;
			case 1951551059:
				if (ihdr == null)
				{
					
					throw new IllegalStateException("tRNS tag before IHDR");
				}
				trns = new TRNS((byte)(sbyte)ihdr.colorType);
				trns.parse(data, length);
				break;
			case 1229209940:
				((List)list).add((object)NIOUtils.read(data, length));
				NIOUtils.skip(data, 4);
				break;
			case 1229278788:
				NIOUtils.skip(data, 4);
				break;
			default:
				data.position(data.position() + length + 4);
				break;
			}
		}
		DataFormatException ex;
		if (ihdr != null)
		{
			try
			{
				decodeData(ihdr, plte, trns, list, buffer);
			}
			catch (DataFormatException x)
			{
				ex = ByteCodeHelper.MapException<DataFormatException>(x, ByteCodeHelper.MapFlags.NoRemapping);
				goto IL_015f;
			}
			return Picture.createPicture(ihdr.width, ihdr.height, buffer, ihdr.colorSpace());
		}
		
		throw new IllegalStateException("no IHDR tag");
		IL_015f:
		DataFormatException e = ex;
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 29, 162, 104, 105, 145, 109, 104, 136, 106,
		131, 144, 103, 104, 159, 0, 146, 102
	})]
	public override VideoCodecMeta getCodecMeta(ByteBuffer _data)
	{
		ByteBuffer data = _data.duplicate();
		if (!ispng(data))
		{
			
			throw new RuntimeException("Not a PNG file.");
		}
		while (data.remaining() >= 8)
		{
			int length = data.getInt();
			int tag = data.getInt();
			if (data.remaining() < length)
			{
				break;
			}
			if (tag == 1229472850)
			{
				IHDR ihdr = new IHDR();
				ihdr.parse(data);
				VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(ihdr.width, ihdr.height), ColorSpace.___003C_003ERGB);
				
				return result;
			}
			data.position(data.position() + length + 4);
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 21, 130, 105, 100 })]
	public static int probe(ByteBuffer data)
	{
		if (!ispng(data))
		{
			return 100;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.util.zip.DataFormatException" })]
	[LineNumberTable(new byte[]
	{
		159, 19, 66, 104, 105, 108, 105, 105, 106, 113,
		99
	})]
	public static byte[] deflate(byte[] data, Inflater inflater)
	{
		inflater.setInput(data);
		ByteArrayOutputStream baos = new ByteArrayOutputStream(data.Length);
		byte[] buffer = new byte[16384];
		while (!inflater.needsInput())
		{
			int count = inflater.inflate(buffer);
			baos.write(buffer, 0, count);
			java.lang.System.@out.println(baos.size());
		}
		byte[] result = baos.toByteArray();
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 128, 98, 127, 9, 127, 9, 127, 9 })]
	static PNGDecoder()
	{
		logPassStep = new int[7] { 3, 3, 2, 2, 1, 1, 0 };
		logPassRowStep = new int[7] { 3, 3, 3, 2, 2, 1, 1 };
		passOff = new int[7] { 0, 4, 0, 2, 0, 1, 0 };
		passRowOff = new int[7] { 0, 0, 4, 0, 2, 0, 1 };
	}
}
