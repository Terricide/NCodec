using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util.zip;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.png;

public class PNGEncoder : VideoEncoder
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public PNGEncoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 142, 103, 109 })]
	private static int crc32(ByteBuffer from, ByteBuffer to)
	{
		from.limit(to.position());
		CRC32 crc32 = new CRC32();
		crc32.update(NIOUtils.toArray(from));
		return (int)crc32.getValue();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 104, 109, 109, 103, 109, 109, 104,
		104, 138, 104, 109, 104, 143, 103, 114, 106, 109,
		169, 114, 150, 118, 104, 136, 101, 106, 104, 109,
		109, 111, 100, 203, 107, 134, 102, 112, 114, 118,
		246, 61, 239, 69, 104, 105, 109, 231, 35, 236,
		95, 102, 106, 104, 109, 109, 143, 105, 109, 109,
		136
	})]
	public override EncodedFrame encodeFrame(Picture pic, ByteBuffer @out)
	{
		ByteBuffer _out = @out.duplicate();
		_out.putInt(-1991225785);
		_out.putInt(218765834);
		IHDR ihdr = new IHDR();
		ihdr.width = pic.getCroppedWidth();
		ihdr.height = pic.getCroppedHeight();
		ihdr.bitDepth = 8;
		ihdr.colorType = 2;
		_out.putInt(13);
		ByteBuffer crcFrom = _out.duplicate();
		_out.putInt(1229472850);
		ihdr.write(_out);
		_out.putInt(crc32(crcFrom, _out));
		Deflater deflater = new Deflater();
		byte[] rowData = new byte[pic.getCroppedWidth() * 3 + 1];
		byte[] pix = pic.getPlaneData(0);
		byte[] buffer = new byte[32768];
		int ptr = 0;
		int len = buffer.Length;
		int lineStep = (pic.getWidth() - pic.getCroppedWidth()) * 3;
		int row = 0;
		int bptr = 0;
		for (; row < pic.getCroppedHeight() + 1; row++)
		{
			int count;
			while ((count = deflater.deflate(buffer, ptr, len)) > 0)
			{
				ptr += count;
				len -= count;
				if (len == 0)
				{
					_out.putInt(ptr);
					crcFrom = _out.duplicate();
					_out.putInt(1229209940);
					_out.put(buffer, 0, ptr);
					_out.putInt(crc32(crcFrom, _out));
					ptr = 0;
					len = buffer.Length;
				}
			}
			if (row >= pic.getCroppedHeight())
			{
				break;
			}
			rowData[0] = 0;
			int i = 1;
			while (i <= pic.getCroppedWidth() * 3)
			{
				rowData[i] = (byte)(sbyte)(pix[bptr] + 128);
				rowData[i + 1] = (byte)(sbyte)(pix[bptr + 1] + 128);
				rowData[i + 2] = (byte)(sbyte)(pix[bptr + 2] + 128);
				i += 3;
				bptr += 3;
			}
			bptr += lineStep;
			deflater.setInput(rowData);
			if (row >= pic.getCroppedHeight() - 1)
			{
				deflater.finish();
			}
		}
		if (ptr > 0)
		{
			_out.putInt(ptr);
			crcFrom = _out.duplicate();
			_out.putInt(1229209940);
			_out.put(buffer, 0, ptr);
			_out.putInt(crc32(crcFrom, _out));
		}
		_out.putInt(0);
		_out.putInt(1229278788);
		_out.putInt(-1371381630);
		_out.flip();
		EncodedFrame result = new EncodedFrame(_out, keyFrame: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	public override ColorSpace[] getSupportedColorSpaces()
	{
		return new ColorSpace[1] { ColorSpace.___003C_003ERGB };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(113)]
	public override int estimateBufferSize(Picture frame)
	{
		return frame.getCroppedWidth() * frame.getCroppedHeight() * 4;
	}
}
