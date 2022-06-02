using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.api;
using org.jcodec.common;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mjpeg;

public class JpegDecoder : VideoDecoder
{
	private bool interlace;

	private bool topFieldFirst;

	internal int[] buf;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 105, 110 })]
	public JpegDecoder()
	{
		buf = new int[64];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 130, 108, 140, 104, 104, 63, 5, 39,
		231, 70, 127, 3, 127, 5
	})]
	private void decodeMCU(BitReader bits, int[] dcPredictor, int[][] quant, VLC[] huff, Picture result, int bx, int by, int blockH, int blockV, int field, int step)
	{
		int sx = bx << blockH - 1;
		int sy = by << blockV - 1;
		for (int i = 0; i < blockV; i++)
		{
			for (int j = 0; j < blockH; j++)
			{
				decodeBlock(bits, dcPredictor, quant, huff, result, buf, sx + j << 3, sy + i << 3, 0, 0, field, step);
			}
		}
		decodeBlock(bits, dcPredictor, quant, huff, result, buf, bx << 3, by << 3, 1, 1, field, step);
		decodeBlock(bits, dcPredictor, quant, huff, result, buf, bx << 3, by << 3, 2, 1, field, step);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 98, 105, 127, 8, 117, 137, 127, 5 })]
	internal virtual void decodeBlock(BitReader bits, int[] dcPredictor, int[][] quant, VLC[] huff, Picture result, int[] buf, int blkX, int blkY, int plane, int chroma, int field, int step)
	{
		Arrays.fill(buf, 0);
		int num = readDCValue(bits, huff[chroma]) * quant[chroma][0] + dcPredictor[plane];
		int num2 = 0;
		buf[num2] = num;
		dcPredictor[plane] = num;
		readACValues(bits, buf, huff[chroma + 2], quant[chroma]);
		SimpleIDCT10Bit.idct10(buf, 0);
		putBlock(result.getPlaneData(plane), result.getPlaneWidth(plane), buf, blkX, blkY, field, step);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 130, 105 })]
	internal static int readDCValue(BitReader _in, VLC table)
	{
		int code = table.readVLC16(_in);
		return (code != 0) ? toValue(_in.readNBit(code), code) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 66, 131, 105, 105, 104, 101, 101, 101,
		102, 124, 133, 108
	})]
	internal virtual void readACValues(BitReader _in, int[] target, VLC table, int[] quantTable)
	{
		int curOff = 1;
		int code;
		do
		{
			code = table.readVLC16(_in);
			if (code == 240)
			{
				curOff += 16;
			}
			else if (code > 0)
			{
				int rle = code >> 4;
				curOff += rle;
				int len = code & 0xF;
				target[JpegConst.___003C_003EnaturalOrder[curOff]] = toValue(_in.readNBit(len), len) * quantTable[curOff];
				curOff++;
			}
		}
		while (code != 0 && curOff < 64);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 162, 102, 117, 105, 63, 0, 137, 101,
		229, 60, 231, 70
	})]
	private static void putBlock(byte[] plane, int stride, int[] patch, int x, int y, int field, int step)
	{
		int dstride = step * stride;
		int i = 0;
		int off = field * stride + y * dstride + x;
		int poff = 0;
		for (; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				plane[j + off] = (byte)(sbyte)(MathUtil.clip(patch[j + poff], 0, 255) - 128);
			}
			off += dstride;
			poff += 8;
		}
	}

	[LineNumberTable(128)]
	internal static int toValue(int raw, int length)
	{
		return (length < 1 || raw >= 1 << length - 1) ? raw : (-(1 << length) + 1 + raw);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 162, 131, 99, 159, 9, 120, 100, 100,
		140, 101, 146, 120, 131, 100, 101, 99, 106, 113,
		191, 24, 120, 131, 106, 141, 106, 111, 109, 106,
		113, 117, 99, 111, 111, 109, 106, 113, 108, 99,
		143, 101, 145, 137, 124, 124, 107, 105, 106, 102,
		115, 111, 108, 108, 107, 134, 101, 127, 7, 132,
		134
	})]
	public virtual Picture decodeField(ByteBuffer data, byte[][] data2, int field, int step)
	{
		Picture result = null;
		FrameHeader header = null;
		VLC[] huffTables = new VLC[4]
		{
			JpegConst.___003C_003EYDC_DEFAULT,
			JpegConst.___003C_003ECDC_DEFAULT,
			JpegConst.___003C_003EYAC_DEFAULT,
			JpegConst.___003C_003ECAC_DEFAULT
		};
		int[][] quant = new int[2][]
		{
			JpegConst.DEFAULT_QUANT_LUMA,
			JpegConst.DEFAULT_QUANT_CHROMA
		};
		ScanHeader scan = null;
		int skipToNext = 0;
		while (data.hasRemaining())
		{
			int marker;
			if (skipToNext == 0)
			{
				marker = (sbyte)data.get() & 0xFF;
			}
			else
			{
				while ((marker = (sbyte)data.get() & 0xFF) != 255)
				{
				}
			}
			skipToNext = 0;
			switch (marker)
			{
			case 0:
				continue;
			default:
			{
				string message = new StringBuilder().append("@").append(Long.toHexString(data.position())).append(" Marker expected: 0x")
					.append(Integer.toHexString(marker))
					.toString();
				
				throw new RuntimeException(message);
			}
			case 255:
				break;
			}
			int b;
			while ((b = (sbyte)data.get() & 0xFF) == 255)
			{
			}
			switch (b)
			{
			case 192:
				header = FrameHeader.read(data);
				continue;
			case 196:
			{
				int len1 = data.getShort() & 0xFFFF;
				ByteBuffer buf2 = NIOUtils.read(data, len1 - 2);
				while (buf2.hasRemaining())
				{
					int tableNo = (sbyte)buf2.get() & 0xFF;
					huffTables[(tableNo & 1) | ((tableNo >> 3) & 2)] = readHuffmanTable(buf2);
				}
				continue;
			}
			case 219:
			{
				int len3 = data.getShort() & 0xFFFF;
				ByteBuffer buf = NIOUtils.read(data, len3 - 2);
				while (buf.hasRemaining())
				{
					int ind = (sbyte)buf.get() & 0xFF;
					quant[ind] = readQuantTable(buf);
				}
				continue;
			}
			case 218:
				if (scan != null)
				{
					
					throw new UnhandledStateException("unhandled - more than one scan header");
				}
				scan = ScanHeader.read(data);
				result = decodeScan(readToMarker(data), header, scan, huffTables, quant, data2, field, step);
				continue;
			case 208:
			case 209:
			case 210:
			case 211:
			case 212:
			case 213:
			case 214:
			case 215:
			case 216:
				Logger.warn("SOI not supported.");
				skipToNext = 1;
				continue;
			}
			switch (b)
			{
			case 224:
			case 225:
			case 226:
			case 227:
			case 228:
			case 229:
			case 230:
			case 231:
			case 232:
			case 233:
			case 234:
			case 235:
			case 236:
			case 237:
			case 238:
			case 239:
			case 240:
			case 241:
			case 242:
			case 243:
			case 244:
			case 245:
			case 246:
			case 247:
			case 248:
			case 249:
			case 250:
			case 251:
			case 252:
			case 253:
			case 254:
			{
				int len2 = data.getShort() & 0xFFFF;
				NIOUtils.read(data, len2 - 2);
				continue;
			}
			default:
				switch (b)
				{
				case 221:
					Logger.warn("DRI not supported.");
					skipToNext = 1;
					continue;
				default:
					Logger.warn(new StringBuilder().append("unhandled marker ").append(JpegConst.markerToString(b)).toString());
					break;
				case 0:
					break;
				}
				skipToNext = 1;
				continue;
			case 217:
				break;
			}
			break;
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 66, 135, 143, 99, 107, 102, 106, 112,
		104, 239, 61, 233, 69, 229, 57, 234, 74
	})]
	private static VLC readHuffmanTable(ByteBuffer data)
	{
		VLCBuilder builder = new VLCBuilder();
		byte[] levelSizes = NIOUtils.toArray(NIOUtils.read(data, 16));
		int levelStart = 0;
		for (int i = 0; i < 16; i++)
		{
			int length = levelSizes[i];
			for (int c = 0; c < length; c++)
			{
				int val = (sbyte)data.get() & 0xFF;
				int num = levelStart;
				levelStart++;
				int code = num;
				builder.setInt(code, i + 1, val);
			}
			levelStart <<= 1;
		}
		VLC vLC = builder.getVLC();
		
		return vLC;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 162, 105, 104, 49, 167 })]
	private static int[] readQuantTable(ByteBuffer data)
	{
		int[] result = new int[64];
		for (int i = 0; i < 64; i++)
		{
			result[i] = (sbyte)data.get() & 0xFF;
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 109, 105, 105, 101, 105, 100, 139,
		112, 131, 99, 105, 99, 104
	})]
	private static ByteBuffer readToMarker(ByteBuffer data)
	{
		ByteBuffer @out = ByteBuffer.allocate(data.remaining());
		while (data.hasRemaining())
		{
			int b0 = (sbyte)data.get();
			if (b0 == -1)
			{
				if ((sbyte)data.get() != 0)
				{
					data.position(data.position() - 2);
					break;
				}
				@out.put(byte.MaxValue);
			}
			else
			{
				@out.put((byte)b0);
			}
		}
		@out.flip();
		return @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 104, 104, 101, 133, 105, 137, 112,
		144, 102, 223, 44, 105, 127, 2, 106, 115, 59,
		41, 201
	})]
	private Picture decodeScan(ByteBuffer data, FrameHeader header, ScanHeader scan, VLC[] huffTables, int[][] quant, byte[][] data2, int field, int step)
	{
		int blockW = header.getHmax();
		int blockH = header.getVmax();
		int mcuW = blockW << 3;
		int mcuH = blockH << 3;
		int width = header.width;
		int height = header.height;
		int xBlocks = width + mcuW - 1 >> blockW + 2;
		int yBlocks = height + mcuH - 1 >> blockH + 2;
		Picture result = new Picture(xBlocks << blockW + 2, yBlocks << blockH + 2, data2, null, (blockW + blockH) switch
		{
			4 => ColorSpace.___003C_003EYUV420J, 
			3 => ColorSpace.___003C_003EYUV422J, 
			_ => ColorSpace.___003C_003EYUV444J, 
		}, 0, new Rect(0, 0, width, height));
		BitReader bits = BitReader.createBitReader(data);
		int[] dcPredictor = new int[3] { 1024, 1024, 1024 };
		for (int by = 0; by < yBlocks; by++)
		{
			for (int bx = 0; bx < xBlocks; bx++)
			{
				if (!bits.moreData())
				{
					break;
				}
				decodeMCU(bits, dcPredictor, quant, huffTables, result, bx, by, blockW, blockH, field, step);
			}
		}
		return result;
	}

	[LineNumberTable(new byte[] { 159, 133, 161, 69, 104, 104 })]
	public virtual void setInterlace(bool interlace, bool topFieldFirst)
	{
		this.interlace = interlace;
		this.topFieldFirst = topFieldFirst;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 98, 105, 119, 119, 158 })]
	public override Picture decodeFrame(ByteBuffer data, byte[][] data2)
	{
		if (interlace)
		{
			Picture r1 = decodeField(data, data2, (!topFieldFirst) ? 1 : 0, 2);
			Picture r2 = decodeField(data, data2, topFieldFirst ? 1 : 0, 2);
			Picture result = Picture.createPicture(r1.getWidth(), r1.getHeight() << 1, data2, r1.getColor());
			
			return result;
		}
		Picture result2 = decodeField(data, data2, 0, 1);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 76, 66, 99, 105, 125, 163, 119, 99, 105,
		104, 131, 99, 103, 104, 104, 102, 127, 1, 156
	})]
	public override VideoCodecMeta getCodecMeta(ByteBuffer data)
	{
		FrameHeader header = null;
		while (data.hasRemaining())
		{
			while (data.hasRemaining() && ((sbyte)data.get() & 0xFF) != 255)
			{
			}
			int type;
			while ((type = (sbyte)data.get() & 0xFF) == 255)
			{
			}
			if (type == 192)
			{
				header = FrameHeader.read(data);
				break;
			}
		}
		if (header != null)
		{
			int blockW = header.getHmax();
			int blockH = header.getVmax();
			ColorSpace color = (blockW + blockH) switch
			{
				4 => ColorSpace.___003C_003EYUV420J, 
				3 => ColorSpace.___003C_003EYUV422J, 
				_ => ColorSpace.___003C_003EYUV444J, 
			};
			VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(header.width, header.height), color);
			
			return result;
		}
		return null;
	}
}
