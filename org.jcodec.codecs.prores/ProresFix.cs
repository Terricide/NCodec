using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.prores;

public class ProresFix : java.lang.Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 111, 103, 177, 102, 103, 117, 101,
		177, 229, 58, 236, 72
	})]
	internal static void readDCCoeffs(BitReader bits, int[] @out, int blocksPerSlice)
	{
		@out[0] = ProresDecoder.readCodeword(bits, ProresConsts.firstDCCodebook);
		if (@out[0] < 0)
		{
			
			throw new RuntimeException("First DC coeff damaged");
		}
		int code = 5;
		int idx = 64;
		int i = 1;
		while (i < blocksPerSlice)
		{
			code = ProresDecoder.readCodeword(bits, ProresConsts.___003C_003EdcCodebooks[java.lang.Math.min(code, 6)]);
			if (code < 0)
			{
				
				throw new RuntimeException("DC coeff damaged");
			}
			@out[idx] = code;
			i++;
			idx += 64;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 99, 131, 101, 104, 138, 100, 120,
		118, 111, 145, 137, 120, 109, 145, 106, 106, 118,
		102
	})]
	internal static void readACCoeffs(BitReader bits, int[] @out, int blocksPerSlice, int[] scan)
	{
		int run = 4;
		int level = 2;
		int blockMask = blocksPerSlice - 1;
		int log2BlocksPerSlice = MathUtil.log2(blocksPerSlice);
		int maxCoeffs = 64 << log2BlocksPerSlice;
		int pos = blockMask;
		while (bits.remaining() > 32 || bits.checkNBit(24) != 0)
		{
			run = ProresDecoder.readCodeword(bits, ProresConsts.___003C_003ErunCodebooks[java.lang.Math.min(run, 15)]);
			if (run < 0 || run >= maxCoeffs - pos - 1)
			{
				
				throw new RuntimeException("Run codeword damaged");
			}
			pos += run + 1;
			level = ProresDecoder.readCodeword(bits, ProresConsts.___003C_003ElevCodebooks[java.lang.Math.min(level, 9)]) + 1;
			if (level < 0 || level > 65535)
			{
				
				throw new RuntimeException("Level codeword damaged");
			}
			int sign = -bits.read1Bit();
			int ind = pos >> log2BlocksPerSlice;
			@out[((pos & blockMask) << 6) + scan[ind]] = MathUtil.toSigned(level, sign);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 122, 66, 143, 102, 103, 119, 5, 204 })]
	internal static void writeDCCoeffs(BitWriter bits, int[] _in, int blocksPerSlice)
	{
		ProresEncoder.writeCodeword(bits, ProresConsts.firstDCCodebook, _in[0]);
		int code = 5;
		int idx = 64;
		int i = 1;
		while (i < blocksPerSlice)
		{
			ProresEncoder.writeCodeword(bits, ProresConsts.___003C_003EdcCodebooks[java.lang.Math.min(code, 6)], _in[idx]);
			code = _in[idx];
			i++;
			idx += 64;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 120, 130, 99, 131, 99, 107, 102, 108, 108,
		101, 135, 118, 99, 99, 106, 121, 100, 238, 53,
		12, 234, 81
	})]
	internal static void writeACCoeffs(BitWriter bits, int[] _in, int blocksPerSlice, int[] scan)
	{
		int prevRun = 4;
		int prevLevel = 2;
		int run = 0;
		for (int i = 1; i < 64; i++)
		{
			int indp = scan[i];
			for (int j = 0; j < blocksPerSlice; j++)
			{
				int val = _in[(j << 6) + indp];
				if (val == 0)
				{
					run++;
					continue;
				}
				ProresEncoder.writeCodeword(bits, ProresConsts.___003C_003ErunCodebooks[java.lang.Math.min(prevRun, 15)], run);
				prevRun = run;
				run = 0;
				int level = ProresEncoder.getLevel(val);
				ProresEncoder.writeCodeword(bits, ProresConsts.___003C_003ElevCodebooks[java.lang.Math.min(prevLevel, 9)], level - 1);
				prevLevel = level;
				bits.write1Bit(MathUtil.sign(val));
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 98, 104, 116, 104, 152, 141, 109, 100,
		146, 104, 135, 137, 115, 146, 103, 102, 109, 228,
		51, 236, 80
	})]
	private static void transcodePicture(ByteBuffer inBuf, ByteBuffer outBuf, ProresConsts.FrameHeader fh)
	{
		ProresConsts.PictureHeader ph = ProresDecoder.readPictureHeader(inBuf);
		ProresEncoder.writePictureHeader(ph.log2SliceMbWidth, ph.sliceSizes.Length, outBuf);
		ByteBuffer fork = outBuf.duplicate();
		outBuf.position((int)(outBuf.position() + ((nint)ph.sliceSizes.LongLength << 1)));
		int mbWidth = fh.width + 15 >> 4;
		int sliceMbCount = 1 << ph.log2SliceMbWidth;
		int mbX = 0;
		for (int i = 0; i < (nint)ph.sliceSizes.LongLength; i++)
		{
			while (mbWidth - mbX < sliceMbCount)
			{
				sliceMbCount >>= 1;
			}
			int savedPoint = outBuf.position();
			transcodeSlice(inBuf, outBuf, sliceMbCount, ph.sliceSizes[i], fh);
			fork.putShort((short)(outBuf.position() - savedPoint));
			mbX += sliceMbCount;
			if (mbX == mbWidth)
			{
				sliceMbCount = 1 << ph.log2SliceMbWidth;
				mbX = 0;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 161, 67, 113, 111, 104, 105, 139, 106,
		106, 105, 137, 105, 125, 105, 126, 105, 158, 111,
		111
	})]
	private static void transcodeSlice(ByteBuffer inBuf, ByteBuffer outBuf, int sliceMbCount, short sliceSize, ProresConsts.FrameHeader fh)
	{
		int hdrSize = ((sbyte)inBuf.get() & 0xFF) >> 3;
		int qScaleOrig = (sbyte)inBuf.get() & 0xFF;
		int yDataSize = inBuf.getShort();
		int uDataSize = inBuf.getShort();
		int vDataSize = sliceSize - uDataSize - yDataSize - hdrSize;
		outBuf.put(48);
		outBuf.put((byte)(sbyte)qScaleOrig);
		ByteBuffer beforeSizes = outBuf.duplicate();
		outBuf.putInt(0);
		int beforeY = outBuf.position();
		copyCoeff(ProresDecoder.bitstream(inBuf, yDataSize), new BitWriter(outBuf), sliceMbCount << 2, fh.scan);
		int beforeCb = outBuf.position();
		copyCoeff(ProresDecoder.bitstream(inBuf, uDataSize), new BitWriter(outBuf), sliceMbCount << 1, fh.scan);
		int beforeCr = outBuf.position();
		copyCoeff(ProresDecoder.bitstream(inBuf, vDataSize), new BitWriter(outBuf), sliceMbCount << 1, fh.scan);
		beforeSizes.putShort((short)(beforeCb - beforeY));
		beforeSizes.putShort((short)(beforeCr - beforeCb));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 130, 138, 105, 159, 2, 35, 130, 105,
		106, 103
	})]
	internal static void copyCoeff(BitReader ib, BitWriter ob, int blocksPerSlice, int[] scan)
	{
		int[] @out = new int[blocksPerSlice << 6];
		RuntimeException ex2;
		try
		{
			readDCCoeffs(ib, @out, blocksPerSlice);
			readACCoeffs(ib, @out, blocksPerSlice, scan);
		}
		catch (System.Exception x)
		{
			RuntimeException ex = ByteCodeHelper.MapException<RuntimeException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
			goto IL_0032;
		}
		goto IL_003a;
		IL_0032:
		RuntimeException ex3 = ex2;
		goto IL_003a;
		IL_003a:
		writeDCCoeffs(ob, @out, blocksPerSlice);
		writeACCoeffs(ob, @out, blocksPerSlice, scan);
		ob.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;IILjava/util/List<Ljava/lang/String;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 82, 130, 136, 104, 136, 109, 103, 146, 104,
		167, 191, 16, 3, 99, 191, 43, 103, 102, 109,
		100, 231, 49, 236, 82
	})]
	private static void checkPicture(ByteBuffer data, int width, int height, List messages)
	{
		ProresConsts.PictureHeader ph = ProresDecoder.readPictureHeader(data);
		int mbWidth = width + 15 >> 4;
		int mbHeight = height + 15 >> 4;
		int sliceMbCount = 1 << ph.log2SliceMbWidth;
		int mbX = 0;
		int mbY = 0;
		for (int i = 0; i < (nint)ph.sliceSizes.LongLength; i++)
		{
			while (mbWidth - mbX < sliceMbCount)
			{
				sliceMbCount >>= 1;
			}
			java.lang.Exception ex2;
			try
			{
				checkSlice(NIOUtils.read(data, ph.sliceSizes[i]), sliceMbCount);
			}
			catch (System.Exception x)
			{
				java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
				if (ex == null)
				{
					throw;
				}
				ex2 = ex;
				goto IL_0079;
			}
			goto IL_00cd;
			IL_0079:
			java.lang.Exception e = ex2;
			messages.add(new StringBuilder().append("[ERROR] Slice data corrupt: mbX = ").append(mbX).append(", mbY = ")
				.append(mbY)
				.append(". ")
				.append(Throwable.instancehelper_getMessage(e))
				.toString());
			goto IL_00cd;
			IL_00cd:
			mbX += sliceMbCount;
			if (mbX == mbWidth)
			{
				sliceMbCount = 1 << ph.log2SliceMbWidth;
				mbX = 0;
				mbY++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 75, 130, 136, 113, 111, 104, 105, 139, 112,
		113, 115
	})]
	private static void checkSlice(ByteBuffer sliceData, int sliceMbCount)
	{
		int sliceSize = sliceData.remaining();
		int hdrSize = ((sbyte)sliceData.get() & 0xFF) >> 3;
		int qScaleOrig = (sbyte)sliceData.get() & 0xFF;
		int yDataSize = sliceData.getShort();
		int uDataSize = sliceData.getShort();
		int vDataSize = sliceSize - uDataSize - yDataSize - hdrSize;
		checkCoeff(ProresDecoder.bitstream(sliceData, yDataSize), sliceMbCount << 2);
		checkCoeff(ProresDecoder.bitstream(sliceData, uDataSize), sliceMbCount << 1);
		checkCoeff(ProresDecoder.bitstream(sliceData, vDataSize), sliceMbCount << 1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 71, 66, 105, 106, 105, 108 })]
	private static void checkCoeff(BitReader ib, int blocksPerSlice)
	{
		int[] scan = new int[64];
		int[] @out = new int[blocksPerSlice << 6];
		readDCCoeffs(ib, @out, blocksPerSlice);
		readACCoeffs(ib, @out, blocksPerSlice, scan);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(34)]
	public ProresFix()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 130, 104, 136, 104, 136, 105, 139, 137,
		169, 136, 136
	})]
	public static ByteBuffer transcode(ByteBuffer inBuf, ByteBuffer _outBuf)
	{
		ByteBuffer outBuf = _outBuf.slice();
		ByteBuffer fork = outBuf.duplicate();
		ProresConsts.FrameHeader fh = ProresDecoder.readFrameHeader(inBuf);
		ProresEncoder.writeFrameHeader(outBuf, fh);
		if (fh.frameType == 0)
		{
			transcodePicture(inBuf, outBuf, fh);
		}
		else
		{
			transcodePicture(inBuf, outBuf, fh);
			transcodePicture(inBuf, outBuf, fh);
		}
		ProresEncoder.writeFrameHeader(fork, fh);
		outBuf.flip();
		return outBuf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;)Ljava/util/List<Ljava/lang/String;>;")]
	[LineNumberTable(new byte[]
	{
		159, 92, 130, 103, 136, 115, 109, 163, 104, 105,
		109, 131, 136, 137, 105, 105, 125, 127, 31, 163,
		138, 147, 105, 142, 110, 174
	})]
	public static List check(ByteBuffer data)
	{
		ArrayList messages = new ArrayList();
		int frameSize = data.getInt();
		if (!java.lang.String.instancehelper_equals("icpf", ProresDecoder.readSig(data)))
		{
			((List)messages).add((object)"[ERROR] Missing ProRes signature (icpf).");
			return messages;
		}
		int headerSize = data.getShort();
		if (headerSize > 148)
		{
			((List)messages).add((object)"[ERROR] Wrong ProRes frame header.");
			return messages;
		}
		int version = data.getShort();
		int res1 = data.getInt();
		int width = data.getShort();
		int height = data.getShort();
		if (width < 0 || width > 10000 || height < 0 || height > 10000)
		{
			((List)messages).add((object)new StringBuilder().append("[ERROR] Wrong ProRes frame header, invalid image size [").append(width).append("x")
				.append(height)
				.append("].")
				.toString());
			return messages;
		}
		int flags1 = (sbyte)data.get();
		data.position(data.position() + headerSize - 13);
		if (((flags1 >> 2) & 3) == 0)
		{
			checkPicture(data, width, height, messages);
		}
		else
		{
			checkPicture(data, width, height / 2, messages);
			checkPicture(data, width, height / 2, messages);
		}
		return messages;
	}
}
