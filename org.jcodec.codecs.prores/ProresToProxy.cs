using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.prores;

public class ProresToProxy : java.lang.Object
{
	private int[] qMatLumaTo;

	private int[] qMatChromaTo;

	private int frameSize;

	private const int START_QP = 6;

	private int bitsPer1024;

	private int bitsPer1024High;

	private int bitsPer1024Low;

	private int nCoeffs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 162, 104, 116, 104, 152, 101, 110, 110,
		103, 146, 105, 137, 137, 159, 11, 109, 138, 112,
		144, 112, 103, 115, 137, 113, 135, 139, 102, 102,
		110, 99, 229, 35, 236, 96
	})]
	private void transcodePicture(ByteBuffer inBuf, ByteBuffer outBuf, ProresConsts.FrameHeader fh)
	{
		ProresConsts.PictureHeader ph = ProresDecoder.readPictureHeader(inBuf);
		ProresEncoder.writePictureHeader(ph.log2SliceMbWidth, ph.sliceSizes.Length, outBuf);
		ByteBuffer sliceSizes = outBuf.duplicate();
		outBuf.position((int)(outBuf.position() + ((nint)ph.sliceSizes.LongLength << 1)));
		int mbX = 0;
		int mbY = 0;
		int mbWidth = fh.width + 15 >> 4;
		int sliceMbCount = 1 << ph.log2SliceMbWidth;
		int balance = 0;
		int qp = 6;
		for (int i = 0; i < (nint)ph.sliceSizes.LongLength; i++)
		{
			while (mbWidth - mbX < sliceMbCount)
			{
				sliceMbCount >>= 1;
			}
			int savedPoint = outBuf.position();
			transcodeSlice(inBuf, outBuf, fh.qMatLuma, fh.qMatChroma, fh.scan, sliceMbCount, mbX, mbY, ph.sliceSizes[i], qp);
			int encodedSize = (short)(outBuf.position() - savedPoint);
			sliceSizes.putShort((short)encodedSize);
			int max = (sliceMbCount * bitsPer1024High >> 5) + 6;
			int low = (sliceMbCount * bitsPer1024Low >> 5) + 6;
			if (encodedSize > max && qp < 128)
			{
				qp++;
				if (encodedSize > max + balance && qp < 128)
				{
					qp++;
				}
			}
			else if (encodedSize < low && qp > 2 && balance > 0)
			{
				qp += -1;
			}
			balance += max - encodedSize;
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
		159, 107, 97, 68, 113, 122, 115, 105, 105, 140,
		106, 107, 105, 137, 137, 127, 4, 45, 134, 105,
		127, 5, 45, 134, 105, 127, 5, 45, 166, 111,
		111
	})]
	private void transcodeSlice(ByteBuffer inBuf, ByteBuffer outBuf, int[] qMatLuma, int[] qMatChroma, int[] scan, int sliceMbCount, int mbX, int mbY, short sliceSize, int qp)
	{
		int hdrSize = ((sbyte)inBuf.get() & 0xFF) >> 3;
		int qScaleOrig = ProresDecoder.clip((sbyte)inBuf.get() & 0xFF, 1, 224);
		int qScale = ((qScaleOrig <= 128) ? qScaleOrig : (qScaleOrig - 96 << 2));
		int yDataSize = inBuf.getShort();
		int uDataSize = inBuf.getShort();
		int vDataSize = sliceSize - uDataSize - yDataSize - hdrSize;
		outBuf.put(48);
		outBuf.put((byte)(sbyte)qp);
		ByteBuffer beforeSizes = outBuf.duplicate();
		outBuf.putInt(0);
		int beforeY = outBuf.position();
		requant(ProresDecoder.bitstream(inBuf, yDataSize), new BitWriter(outBuf), sliceMbCount << 2, ProresDecoder.scaleMat(qMatLuma, qScale), ProresDecoder.scaleMat(qMatLumaTo, qp), scan, mbX, mbY, 0);
		int beforeCb = outBuf.position();
		requant(ProresDecoder.bitstream(inBuf, uDataSize), new BitWriter(outBuf), sliceMbCount << 1, ProresDecoder.scaleMat(qMatChroma, qScale), ProresDecoder.scaleMat(qMatChromaTo, qp), scan, mbX, mbY, 1);
		int beforeCr = outBuf.position();
		requant(ProresDecoder.bitstream(inBuf, vDataSize), new BitWriter(outBuf), sliceMbCount << 1, ProresDecoder.scaleMat(qMatChroma, qScale), ProresDecoder.scaleMat(qMatChromaTo, qp), scan, mbX, mbY, 2);
		beforeSizes.putShort((short)(beforeCb - beforeY));
		beforeSizes.putShort((short)(beforeCr - beforeCb));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 138, 109, 159, 12, 35, 130, 104,
		51, 135, 107, 115, 103
	})]
	internal virtual void requant(BitReader ib, BitWriter ob, int blocksPerSlice, int[] qMatFrom, int[] qMatTo, int[] scan, int mbX, int mbY, int plane)
	{
		int[] @out = new int[blocksPerSlice << 6];
		RuntimeException ex2;
		try
		{
			ProresDecoder.readDCCoeffs(ib, qMatFrom, @out, blocksPerSlice, 64);
			ProresDecoder.readACCoeffs(ib, qMatFrom, @out, blocksPerSlice, scan, nCoeffs, 6);
		}
		catch (System.Exception x)
		{
			RuntimeException ex = ByteCodeHelper.MapException<RuntimeException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
			goto IL_0040;
		}
		goto IL_0048;
		IL_0040:
		RuntimeException ex3 = ex2;
		goto IL_0048;
		IL_0048:
		for (int i = 0; i < (nint)@out.LongLength; i++)
		{
			int num = i;
			int[] array = @out;
			array[num] <<= 2;
		}
		ProresEncoder.writeDCCoeffs(ob, qMatTo, @out, blocksPerSlice);
		ProresEncoder.writeACCoeffs(ob, qMatTo, @out, blocksPerSlice, scan, nCoeffs);
		ob.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 105, 108, 108, 136, 117, 103, 151,
		119, 151, 127, 8
	})]
	public ProresToProxy(int width, int height, int frameSize)
	{
		qMatLumaTo = ProresConsts.___003C_003EQMAT_LUMA_APCO;
		qMatChromaTo = ProresConsts.___003C_003EQMAT_CHROMA_APCO;
		this.frameSize = frameSize;
		int headerBytes = (height >> 4) * ((width >> 4) + 7 >> 3) * 8 + 148;
		int dataBits = frameSize - headerBytes << 3;
		int num = dataBits << 10;
		int num2 = width * height;
		bitsPer1024 = ((num2 != -1) ? (num / num2) : (-num));
		bitsPer1024High = bitsPer1024 - bitsPer1024 / 10;
		bitsPer1024Low = bitsPer1024 - bitsPer1024 / 20;
		int num3 = width * height >> 8;
		nCoeffs = java.lang.Math.max(java.lang.Math.min((num3 != -1) ? (33000 / num3) : (-33000), 64), 4);
	}

	[LineNumberTable(56)]
	public virtual int getFrameSize()
	{
		return frameSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 162, 136, 104, 136, 104, 105, 140, 106,
		138, 109, 109, 111, 106
	})]
	public virtual void transcode(ByteBuffer inBuf, ByteBuffer outBuf)
	{
		ByteBuffer fork = outBuf.duplicate();
		ProresConsts.FrameHeader fh = ProresDecoder.readFrameHeader(inBuf);
		ProresEncoder.writeFrameHeader(outBuf, fh);
		int beforePicture = outBuf.position();
		if (fh.frameType == 0)
		{
			transcodePicture(inBuf, outBuf, fh);
		}
		else
		{
			transcodePicture(inBuf, outBuf, fh);
			transcodePicture(inBuf, outBuf, fh);
		}
		fh.qMatLuma = qMatLumaTo;
		fh.qMatChroma = qMatChromaTo;
		fh.payloadSize = outBuf.position() - beforePicture;
		ProresEncoder.writeFrameHeader(fork, fh);
	}
}
