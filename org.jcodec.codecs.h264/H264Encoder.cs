using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.codecs.h264.encode;
using org.jcodec.codecs.h264.io;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264;

public class H264Encoder : VideoEncoder
{
	private const int KEY_INTERVAL_DEFAULT = 25;

	private const int MOTION_SEARCH_RANGE_DEFAULT = 16;

	private CAVLC[] cavlc;

	private byte[][] leftRow;

	private byte[][] topLine;

	private RateControl rc;

	private int frameNumber;

	private int keyInterval;

	private int motionSearchRange;

	private int maxPOC;

	private int maxFrameNumber;

	private SeqParameterSet sps;

	private PictureParameterSet pps;

	private MBEncoderI16x16 mbEncoderI16x16;

	private MBEncoderP16x16 mbEncoderP16x16;

	private Picture @ref;

	private Picture picOut;

	private EncodedMB[] topEncoded;

	private EncodedMB outMB;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(51)]
	public static H264Encoder createH264Encoder()
	{
		H264Encoder result = new H264Encoder(new DumbRateControl());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 66, 105, 104, 105, 105 })]
	public H264Encoder(RateControl rc)
	{
		this.rc = rc;
		keyInterval = 25;
		motionSearchRange = 16;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 97, 67, 104, 122, 103, 150, 100, 126,
		141, 121, 185, 100, 105, 114, 142, 105, 114, 174,
		112, 144, 127, 10, 127, 18, 153, 108, 110, 106,
		47, 169, 155, 135, 141, 104
	})]
	public virtual ByteBuffer doEncodeFrame(Picture pic, ByteBuffer _out, bool idr, int frameNumber, SliceType frameType)
	{
		ByteBuffer dup = _out.duplicate();
		int maxSize = Math.min(dup.remaining(), pic.getWidth() * pic.getHeight());
		maxSize -= (int)((uint)maxSize >> 6);
		int qp = rc.startPicture(pic.getSize(), maxSize, frameType);
		if (idr)
		{
			sps = initSPS(new Size(pic.getCroppedWidth(), pic.getCroppedHeight()));
			pps = initPPS();
			maxPOC = 1 << sps.log2MaxPicOrderCntLsbMinus4 + 4;
			maxFrameNumber = 1 << sps.log2MaxFrameNumMinus4 + 4;
		}
		if (idr)
		{
			dup.putInt(1);
			new NALUnit(NALUnitType.___003C_003ESPS, 3).write(dup);
			writeSPS(dup, sps);
			dup.putInt(1);
			new NALUnit(NALUnitType.___003C_003EPPS, 3).write(dup);
			writePPS(dup, pps);
		}
		int mbWidth = sps.picWidthInMbsMinus1 + 1;
		int mbHeight = sps.picHeightInMapUnitsMinus1 + 1;
		leftRow = new byte[3][]
		{
			new byte[16],
			new byte[8],
			new byte[8]
		};
		topLine = new byte[3][]
		{
			new byte[mbWidth << 4],
			new byte[mbWidth << 3],
			new byte[mbWidth << 3]
		};
		picOut = Picture.create(mbWidth << 4, mbHeight << 4, ColorSpace.___003C_003EYUV420J);
		outMB = new EncodedMB();
		topEncoded = new EncodedMB[mbWidth];
		for (int i = 0; i < mbWidth; i++)
		{
			topEncoded[i] = new EncodedMB();
		}
		encodeSlice(sps, pps, pic, dup, idr, frameNumber, frameType, qp);
		putLastMBLine();
		@ref = picOut;
		dup.flip();
		return dup;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 89, 162, 103, 116, 116, 108, 105, 105, 104,
		104, 154, 108, 108, 125, 115, 147
	})]
	public virtual SeqParameterSet initSPS(Size sz)
	{
		SeqParameterSet sps = new SeqParameterSet();
		sps.picWidthInMbsMinus1 = (sz.getWidth() + 15 >> 4) - 1;
		sps.picHeightInMapUnitsMinus1 = (sz.getHeight() + 15 >> 4) - 1;
		sps.chromaFormatIdc = ColorSpace.___003C_003EYUV420J;
		sps.profileIdc = 66;
		sps.levelIdc = 40;
		sps.numRefFrames = 1;
		sps.frameMbsOnlyFlag = true;
		sps.log2MaxFrameNumMinus4 = Math.max(0, MathUtil.log2(keyInterval) - 3);
		int codedWidth = sps.picWidthInMbsMinus1 + 1 << 4;
		int codedHeight = sps.picHeightInMapUnitsMinus1 + 1 << 4;
		sps.frameCroppingFlag = ((codedWidth != sz.getWidth() || codedHeight != sz.getHeight()) ? true : false);
		sps.frameCropRightOffset = codedWidth - sz.getWidth() + 1 >> 1;
		sps.frameCropBottomOffset = codedHeight - sz.getHeight() + 1 >> 1;
		return sps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 90, 98, 103, 104 })]
	public virtual PictureParameterSet initPPS()
	{
		PictureParameterSet pps = new PictureParameterSet();
		pps.picInitQpMinus26 = 0;
		return pps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 130, 108, 104, 104, 106 })]
	private void writeSPS(ByteBuffer dup, SeqParameterSet sps)
	{
		ByteBuffer tmp = ByteBuffer.allocate(1024);
		sps.write(tmp);
		tmp.flip();
		H264Utils.escapeNAL(tmp, dup);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 162, 108, 104, 104, 106 })]
	private void writePPS(ByteBuffer dup, PictureParameterSet pps)
	{
		ByteBuffer tmp = ByteBuffer.allocate(1024);
		pps.write(tmp);
		tmp.flip();
		H264Utils.escapeNAL(tmp, dup);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 65, 68, 109, 99, 139, 127, 18, 127,
		4, 159, 5, 106, 125, 103, 105, 100, 110, 104,
		104, 123, 121, 147, 116, 104, 138, 118, 115, 106,
		168, 139, 237, 70, 115, 115, 147, 115, 144, 116,
		99, 206, 100, 142, 105, 104, 116, 124, 101, 105,
		101, 100, 136, 116, 235, 24, 50, 236, 108, 104,
		103, 104, 136, 107
	})]
	private void encodeSlice(SeqParameterSet sps, PictureParameterSet pps, Picture pic, ByteBuffer dup, bool idr, int frameNum, SliceType sliceType, int qp)
	{
		int idr2 = (idr ? 1 : 0);
		if (idr2 != 0 && sliceType != SliceType.___003C_003EI)
		{
			idr2 = 0;
			Logger.warn("Illegal value of idr = true when sliceType != I");
		}
		cavlc = new CAVLC[3]
		{
			new CAVLC(sps, pps, 2, 2),
			new CAVLC(sps, pps, 1, 1),
			new CAVLC(sps, pps, 1, 1)
		};
		MBEncoderI16x16.___003Cclinit_003E();
		mbEncoderI16x16 = new MBEncoderI16x16(cavlc, leftRow, topLine);
		mbEncoderP16x16 = new MBEncoderP16x16(sps, @ref, cavlc, new MotionEstimator(motionSearchRange));
		dup.putInt(1);
		new NALUnit((idr2 == 0) ? NALUnitType.___003C_003ENON_IDR_SLICE : NALUnitType.___003C_003EIDR_SLICE, 3).write(dup);
		SliceHeader sh = new SliceHeader();
		sh.sliceType = sliceType;
		if (idr2 != 0)
		{
			sh.refPicMarkingIDR = new RefPicMarkingIDR(discardDecodedPics: false, useForlongTerm: false);
		}
		sh.pps = pps;
		sh.sps = sps;
		int num = frameNum << 1;
		int num2 = maxPOC;
		sh.picOrderCntLsb = ((num2 != -1) ? (num % num2) : 0);
		int num3 = maxFrameNumber;
		sh.frameNum = ((num3 != -1) ? (frameNum % num3) : 0);
		sh.sliceQpDelta = qp - (pps.picInitQpMinus26 + 26);
		ByteBuffer buf = ByteBuffer.allocate(pic.getWidth() * pic.getHeight());
		BitWriter sliceData = new BitWriter(buf);
		SliceHeaderWriter.write(sh, (byte)idr2 != 0, 2, sliceData);
		int mbY = 0;
		int mbAddr = 0;
		for (; mbY < sps.picHeightInMapUnitsMinus1 + 1; mbY++)
		{
			int mbX = 0;
			while (mbX < sps.picWidthInMbsMinus1 + 1)
			{
				if (sliceType == SliceType.___003C_003EP)
				{
					CAVLCWriter.writeUE(sliceData, 0);
				}
				MBType mbType = selectMBType(sliceType);
				if (mbType == MBType.___003C_003EI_16x16)
				{
					int predMode = mbEncoderI16x16.getPredMode(pic, mbX, mbY);
					int cbpChroma = mbEncoderI16x16.getCbpChroma(pic, mbX, mbY);
					int cbpLuma = mbEncoderI16x16.getCbpLuma(pic, mbX, mbY);
					int i16x16TypeOffset = cbpLuma / 15 * 12 + cbpChroma * 4 + predMode;
					int mbTypeOffset = ((sliceType == SliceType.___003C_003EP) ? 5 : 0);
					CAVLCWriter.writeUE(sliceData, mbTypeOffset + mbType.code() + i16x16TypeOffset);
				}
				else
				{
					CAVLCWriter.writeUE(sliceData, mbType.code());
				}
				int totalQpDelta = 0;
				int qpDelta = rc.initialQpDelta();
				BitWriter candidate;
				do
				{
					candidate = sliceData.fork();
					totalQpDelta += qpDelta;
					encodeMacroblock(mbType, pic, mbX, mbY, candidate, qp, totalQpDelta);
					qpDelta = rc.accept(candidate.position() - sliceData.position());
					if (qpDelta != 0)
					{
						restoreMacroblock(mbType);
					}
				}
				while (qpDelta != 0);
				sliceData = candidate;
				qp += totalQpDelta;
				collectPredictors(outMB.getPixels(), mbX);
				addToReference(mbX, mbY);
				mbX++;
				mbAddr++;
			}
		}
		sliceData.write1Bit(1);
		sliceData.flush();
		buf = sliceData.getBuffer();
		buf.flip();
		H264Utils.escapeNAL(buf, dup);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 66, 111, 111, 103, 63, 2, 135 })]
	private void putLastMBLine()
	{
		int mbWidth = sps.picWidthInMbsMinus1 + 1;
		int mbHeight = sps.picHeightInMapUnitsMinus1 + 1;
		for (int mbX = 0; mbX < mbWidth; mbX++)
		{
			MBEncoderHelper.putBlkPic(picOut, topEncoded[mbX].getPixels(), mbX << 4, mbHeight - 1 << 4);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 59, 162, 105, 103, 105, 135 })]
	private MBType selectMBType(SliceType sliceType)
	{
		if (sliceType == SliceType.___003C_003EI)
		{
			return MBType.___003C_003EI_16x16;
		}
		if (sliceType == SliceType.___003C_003EP)
		{
			return MBType.___003C_003EP_16x16;
		}
		
		throw new RuntimeException("Unsupported slice type");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 64, 98, 105, 108, 159, 38, 105, 108, 191,
		35, 127, 17
	})]
	private void encodeMacroblock(MBType mbType, Picture pic, int mbX, int mbY, BitWriter candidate, int qp, int qpDelta)
	{
		if (mbType == MBType.___003C_003EI_16x16)
		{
			mbEncoderI16x16.save();
			mbEncoderI16x16.encodeMacroblock(pic, mbX, mbY, candidate, outMB, (mbX <= 0) ? null : topEncoded[mbX - 1], (mbY <= 0) ? null : topEncoded[mbX], qp + qpDelta, qpDelta);
			return;
		}
		if (mbType == MBType.___003C_003EP_16x16)
		{
			mbEncoderP16x16.save();
			mbEncoderP16x16.encodeMacroblock(pic, mbX, mbY, candidate, outMB, (mbX <= 0) ? null : topEncoded[mbX - 1], (mbY <= 0) ? null : topEncoded[mbX], qp + qpDelta, qpDelta);
			return;
		}
		string message = new StringBuilder().append("Macroblock of type ").append(mbType).append(" is not supported.")
			.toString();
		
		throw new RuntimeException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 61, 130, 105, 110, 105, 142, 127, 17 })]
	private void restoreMacroblock(MBType mbType)
	{
		if (mbType == MBType.___003C_003EI_16x16)
		{
			mbEncoderI16x16.restore();
			return;
		}
		if (mbType == MBType.___003C_003EP_16x16)
		{
			mbEncoderP16x16.restore();
			return;
		}
		string message = new StringBuilder().append("Macroblock of type ").append(mbType).append(" is not supported.")
			.toString();
		
		throw new RuntimeException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 53, 162, 127, 0, 123, 155, 122, 120, 122 })]
	private void collectPredictors(Picture outMB, int mbX)
	{
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(0), 240, topLine[0], mbX << 4, 16);
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(1), 56, topLine[1], mbX << 3, 8);
		ByteCodeHelper.arraycopy_primitive_1(outMB.getPlaneData(2), 56, topLine[2], mbX << 3, 8);
		copyCol(outMB.getPlaneData(0), 15, 16, leftRow[0]);
		copyCol(outMB.getPlaneData(1), 7, 8, leftRow[1]);
		copyCol(outMB.getPlaneData(2), 7, 8, leftRow[2]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 56, 66, 101, 127, 2, 106, 111, 104 })]
	private void addToReference(int mbX, int mbY)
	{
		if (mbY > 0)
		{
			MBEncoderHelper.putBlkPic(picOut, topEncoded[mbX].getPixels(), mbX << 4, mbY - 1 << 4);
		}
		EncodedMB tmp = topEncoded[mbX];
		topEncoded[mbX] = outMB;
		outMB = tmp;
	}

	[LineNumberTable(new byte[] { 159, 50, 98, 105, 104, 6, 199 })]
	private void copyCol(byte[] planeData, int off, int stride, byte[] @out)
	{
		for (int i = 0; i < (nint)@out.LongLength; i++)
		{
			@out[i] = planeData[off];
			off += stride;
		}
	}

	[LineNumberTable(87)]
	public virtual int getKeyInterval()
	{
		return keyInterval;
	}

	[LineNumberTable(new byte[] { 159, 120, 162, 104 })]
	public virtual void setKeyInterval(int keyInterval)
	{
		this.keyInterval = keyInterval;
	}

	[LineNumberTable(95)]
	public virtual int getMotionSearchRange()
	{
		return motionSearchRange;
	}

	[LineNumberTable(new byte[] { 159, 118, 162, 104 })]
	public virtual void setMotionSearchRange(int motionSearchRange)
	{
		this.motionSearchRange = motionSearchRange;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 162, 110, 159, 12, 111, 168, 118, 142,
		127, 1
	})]
	public override EncodedFrame encodeFrame(Picture pic, ByteBuffer _out)
	{
		if (pic.getColor() != ColorSpace.___003C_003EYUV420J)
		{
			string s = new StringBuilder().append("Input picture color is not supported: ").append(pic.getColor()).toString();
			
			throw new IllegalArgumentException(s);
		}
		if (frameNumber >= keyInterval)
		{
			frameNumber = 0;
		}
		SliceType sliceType = ((frameNumber != 0) ? SliceType.___003C_003EP : SliceType.___003C_003EI);
		int idr = ((frameNumber == 0) ? 1 : 0);
		int num = frameNumber;
		frameNumber = num + 1;
		ByteBuffer data = doEncodeFrame(pic, _out, (byte)idr != 0, num, sliceType);
		EncodedFrame result = new EncodedFrame(data, (byte)idr != 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 130, 104 })]
	public virtual ByteBuffer encodeIDRFrame(Picture pic, ByteBuffer _out)
	{
		frameNumber = 0;
		ByteBuffer result = doEncodeFrame(pic, _out, idr: true, frameNumber, SliceType.___003C_003EI);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 66, 111 })]
	public virtual ByteBuffer encodePFrame(Picture pic, ByteBuffer _out)
	{
		frameNumber++;
		ByteBuffer result = doEncodeFrame(pic, _out, idr: true, frameNumber, SliceType.___003C_003EP);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(377)]
	public override ColorSpace[] getSupportedColorSpaces()
	{
		return new ColorSpace[1] { ColorSpace.___003C_003EYUV420J };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(382)]
	public override int estimateBufferSize(Picture frame)
	{
		int result = Math.max(65536, frame.getWidth() * frame.getHeight() / 2);
		
		return result;
	}
}
