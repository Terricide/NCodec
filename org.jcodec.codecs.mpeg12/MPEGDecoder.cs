using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.codecs.mpeg12.bitstream;
using org.jcodec.common;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.mpeg12;

public class MPEGDecoder : VideoDecoder
{
	public class Context : java.lang.Object
	{
		internal int[] intra_dc_predictor;

		public int mbWidth;

		internal int mbNo;

		public int codedWidth;

		public int codedHeight;

		public int mbHeight;

		public ColorSpace color;

		public MPEGConst.MBType lastPredB;

		public int[][] qMats;

		public int[] scan;

		public int picWidth;

		public int picHeight;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 120, 162, 105, 109 })]
		public Context()
		{
			intra_dc_predictor = new int[3];
		}
	}

	protected internal SequenceHeader sh;

	protected internal GOPHeader gh;

	private Picture[] refFrames;

	private Picture[] refFields;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 66, 105, 109, 109 })]
	public MPEGDecoder()
	{
		refFrames = new Picture[2];
		refFields = new Picture[2];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 130, 131, 136, 110, 110, 105, 105, 105,
		142, 105, 110, 114, 100, 109, 105, 109, 176, 143,
		110, 235, 69, 110, 102
	})]
	private PictureHeader readHeader(ByteBuffer buffer)
	{
		PictureHeader ph = null;
		ByteBuffer segment;
		for (ByteBuffer fork = buffer.duplicate(); (segment = MPEGUtil.nextSegment(fork)) != null; buffer.position(fork.position()))
		{
			switch (segment.getInt() & 0xFF)
			{
			case 179:
			{
				SequenceHeader newSh = SequenceHeader.read(segment);
				if (sh != null)
				{
					newSh.copyExtensions(sh);
				}
				sh = newSh;
				continue;
			}
			case 184:
				gh = GOPHeader.read(segment);
				continue;
			case 0:
				ph = PictureHeader.read(segment);
				continue;
			case 181:
			{
				int extType = (sbyte)segment.get(4) >> 4;
				if (extType == 1 || extType == 5 || extType == 2)
				{
					SequenceHeader.readExtension(segment, sh);
				}
				else
				{
					PictureHeader.readExtension(segment, ph, sh);
				}
				continue;
			}
			case 178:
				continue;
			}
			break;
		}
		return ph;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 98, 103, 115, 110, 114, 114, 109, 141,
		99, 105, 141, 142, 159, 4, 159, 4, 159, 4,
		157, 108, 110, 116, 110, 116, 110, 116, 110, 148
	})]
	protected internal virtual Context initContext(SequenceHeader sh, PictureHeader ph)
	{
		Context context = new Context();
		context.codedWidth = (sh.horizontal_size + 15) & -16;
		context.codedHeight = getCodedHeight(sh, ph);
		context.mbWidth = sh.horizontal_size + 15 >> 4;
		context.mbHeight = sh.vertical_size + 15 >> 4;
		context.picWidth = sh.horizontal_size;
		context.picHeight = sh.vertical_size;
		int chromaFormat = 1;
		if (sh.sequenceExtension != null)
		{
			chromaFormat = sh.sequenceExtension.chroma_format;
		}
		context.color = getColor(chromaFormat);
		context.scan = MPEGConst.___003C_003Escan[(ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.alternate_scan : 0];
		int[] inter = ((sh.non_intra_quantiser_matrix != null) ? sh.non_intra_quantiser_matrix : zigzag(MPEGConst.___003C_003EdefaultQMatInter, context.scan));
		int[] intra = ((sh.intra_quantiser_matrix != null) ? sh.intra_quantiser_matrix : zigzag(MPEGConst.___003C_003EdefaultQMatIntra, context.scan));
		context.qMats = new int[4][] { inter, inter, intra, intra };
		if (ph.quantMatrixExtension != null)
		{
			if (ph.quantMatrixExtension.non_intra_quantiser_matrix != null)
			{
				context.qMats[0] = ph.quantMatrixExtension.non_intra_quantiser_matrix;
			}
			if (ph.quantMatrixExtension.chroma_non_intra_quantiser_matrix != null)
			{
				context.qMats[1] = ph.quantMatrixExtension.chroma_non_intra_quantiser_matrix;
			}
			if (ph.quantMatrixExtension.intra_quantiser_matrix != null)
			{
				context.qMats[2] = ph.quantMatrixExtension.intra_quantiser_matrix;
			}
			if (ph.quantMatrixExtension.chroma_intra_quantiser_matrix != null)
			{
				context.qMats[3] = ph.quantMatrixExtension.chroma_intra_quantiser_matrix;
			}
		}
		return context;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		87,
		66,
		111,
		127,
		0,
		byte.MaxValue,
		43,
		70,
		110,
		112,
		109,
		114,
		121,
		127,
		7,
		100,
		104,
		131,
		134,
		155,
		159,
		10,
		223,
		17,
		125,
		99
	})]
	public virtual Picture decodePicture(Context context, PictureHeader ph, ByteBuffer buffer, byte[][] buf, int vertOff, int vertStep)
	{
		//Discarded unreachable code: IL_0182
		int planeSize = context.codedWidth * context.codedHeight;
		if ((nint)buf.LongLength < 3 || (nint)buf[0].LongLength < planeSize || (nint)buf[1].LongLength < planeSize || (nint)buf[2].LongLength < planeSize)
		{
			string message = new StringBuilder().append("ByteBuffer too small to hold output picture [").append(context.codedWidth).append("x")
				.append(context.codedHeight)
				.append("]")
				.toString();
			
			throw new RuntimeException(message);
		}
		IOException ex;
		try
		{
			ByteBuffer segment;
			while ((segment = MPEGUtil.nextSegment(buffer)) != null)
			{
				int startCode = (sbyte)segment.get(3) & 0xFF;
				if (startCode >= 1 && startCode <= 175)
				{
					doDecodeSlice(context, ph, buf, vertOff, vertStep, segment);
					continue;
				}
				if (startCode >= 179 && startCode != 182 && startCode != 183)
				{
					string message2 = new StringBuilder().append("Unexpected start code ").append(startCode).toString();
					
					throw new RuntimeException(message2);
				}
				if (startCode == 0)
				{
					buffer.reset();
					break;
				}
			}
			Picture pic = Picture.createPicture(context.codedWidth, context.codedHeight, buf, context.color);
			if ((ph.picture_coding_type == 1 || ph.picture_coding_type == 2) && ph.pictureCodingExtension != null && ph.pictureCodingExtension.picture_structure != 3)
			{
				refFields[ph.pictureCodingExtension.picture_structure - 1] = copyAndCreateIfNeeded(pic, refFields[ph.pictureCodingExtension.picture_structure - 1]);
			}
			return pic;
		}
		catch (IOException x)
		{
			ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		IOException e = ex;
		
		throw new RuntimeException(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 130, 109, 137, 104 })]
	private Picture copyAndCreateIfNeeded(Picture src, Picture dst)
	{
		if (dst == null || !dst.compatible(src))
		{
			dst = src.createCompatible();
		}
		dst.copyFrom(src);
		return dst;
	}

	[LineNumberTable(new byte[] { 159, 89, 66, 156 })]
	public static int getCodedHeight(SequenceHeader sh, PictureHeader ph)
	{
		int field = ((ph.pictureCodingExtension != null && ph.pictureCodingExtension.picture_structure != 3) ? 1 : 0);
		return (((sh.vertical_size >> field) + 15) & -16) << field;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 162, 151, 135, 135, 167 })]
	private ColorSpace getColor(int chromaFormat)
	{
		return chromaFormat switch
		{
			1 => ColorSpace.___003C_003EYUV420, 
			2 => ColorSpace.___003C_003EYUV422, 
			3 => ColorSpace.___003C_003EYUV444, 
			_ => null, 
		};
	}

	[LineNumberTable(new byte[] { 159, 91, 98, 105, 104, 41, 135 })]
	private int[] zigzag(int[] array, int[] scan)
	{
		int[] result = new int[64];
		for (int i = 0; i < (nint)scan.LongLength; i++)
		{
			result[i] = array[scan[i]];
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 78, 66, 113, 105, 137, 191, 13, 3, 98,
		135
	})]
	private void doDecodeSlice(Context context, PictureHeader ph, byte[][] buf, int vertOff, int vertStep, ByteBuffer segment)
	{
		int startCode = (sbyte)segment.get(3) & 0xFF;
		ByteBuffer dup = segment.duplicate();
		dup.position(4);
		RuntimeException ex2;
		try
		{
			decodeSlice(ph, startCode, context, buf, BitReader.createBitReader(dup), vertOff, vertStep);
			return;
		}
		catch (System.Exception x)
		{
			RuntimeException ex = ByteCodeHelper.MapException<RuntimeException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		RuntimeException e = ex2;
		Throwable.instancehelper_printStackTrace(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		72,
		130,
		136,
		137,
		101,
		115,
		142,
		159,
		1,
		138,
		106,
		107,
		106,
		106,
		107,
		172,
		byte.MaxValue,
		115,
		71,
		141,
		152,
		122,
		145
	})]
	public virtual void decodeSlice(PictureHeader ph, int verticalPos, Context context, byte[][] buf, BitReader _in, int vertOff, int vertStep)
	{
		int stride = context.codedWidth;
		resetDCPredictors(context, ph);
		int mbRow = verticalPos - 1;
		if (sh.vertical_size > 2800)
		{
			mbRow += _in.readNBit(3) << 7;
		}
		if (sh.sequenceScalableExtension != null && sh.sequenceScalableExtension.scalable_mode == 0)
		{
			int num = _in.readNBit(7);
		}
		int qScaleCode = _in.readNBit(5);
		if (_in.read1Bit() == 1)
		{
			int intraSlice = _in.read1Bit();
			_in.skip(7);
			while (_in.read1Bit() == 1)
			{
				_in.readNBit(8);
			}
		}
		MPEGPred pred = new MPEGPred((ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.f_code : new int[2][]
		{
			new int[2] { ph.forward_f_code, ph.forward_f_code },
			new int[2] { ph.backward_f_code, ph.backward_f_code }
		}, (sh.sequenceExtension == null) ? 1 : sh.sequenceExtension.chroma_format, (ph.pictureCodingExtension == null || ph.pictureCodingExtension.top_field_first != 0) ? true : false);
		int[] ctx = new int[1] { qScaleCode };
		int prevAddr = mbRow * context.mbWidth - 1;
		while (_in.checkNBit(23) != 0)
		{
			prevAddr = decodeMacroblock(ph, context, prevAddr, ctx, buf, stride, _in, vertOff, vertStep, pred);
			context.mbNo++;
		}
	}

	[LineNumberTable(new byte[] { 159, 63, 162, 103, 105, 116, 127, 9 })]
	private void resetDCPredictors(Context context, PictureHeader ph)
	{
		int rval = 128;
		if (ph.pictureCodingExtension != null)
		{
			rval = 1 << 7 + ph.pictureCodingExtension.intra_dc_precision;
		}
		int[] intra_dc_predictor = context.intra_dc_predictor;
		int[] intra_dc_predictor2 = context.intra_dc_predictor;
		int[] intra_dc_predictor3 = context.intra_dc_predictor;
		int num = rval;
		int num2 = 2;
		int[] array = intra_dc_predictor3;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 1;
		array = intra_dc_predictor2;
		int num4 = num;
		array[num2] = num;
		intra_dc_predictor[0] = num4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		61,
		162,
		99,
		109,
		107,
		136,
		146,
		99,
		110,
		146,
		108,
		159,
		22,
		116,
		115,
		106,
		104,
		112,
		byte.MaxValue,
		8,
		56,
		234,
		76,
		121,
		153,
		143,
		113,
		169,
		100,
		159,
		1,
		171,
		100,
		115,
		159,
		6,
		134,
		171,
		100,
		191,
		23,
		202,
		106,
		141,
		188,
		105,
		116,
		115,
		107,
		170,
		109,
		109,
		112,
		127,
		23,
		119,
		191,
		8,
		106,
		191,
		13,
		223,
		35,
		108,
		127,
		23,
		104,
		177,
		105,
		109,
		127,
		23,
		119,
		191,
		4,
		223,
		33,
		105,
		159,
		37,
		136,
		110,
		144,
		117,
		106,
		171,
		104,
		159,
		2,
		136,
		159,
		6,
		138,
		100,
		105,
		147,
		116,
		138,
		120,
		104,
		102,
		156,
		107,
		124,
		59,
		168,
		191,
		18,
		249,
		52,
		242,
		79,
		191,
		9
	})]
	public virtual int decodeMacroblock(PictureHeader ph, Context context, int prevAddr, int[] qScaleCode, byte[][] buf, int stride, BitReader bits, int vertOff, int vertStep, MPEGPred pred)
	{
		int mbAddr = prevAddr;
		while (bits.checkNBit(11) == 8)
		{
			bits.skip(11);
			mbAddr += 33;
		}
		mbAddr += MPEGConst.___003C_003EvlcAddressIncrement.readVLC(bits) + 1;
		int chromaFormat = 1;
		if (sh.sequenceExtension != null)
		{
			chromaFormat = sh.sequenceExtension.chroma_format;
		}
		for (int j = prevAddr + 1; j < mbAddr; j++)
		{
			int[][] predFwd2 = new int[3][]
			{
				new int[256],
				new int[1 << chromaFormat + 5],
				new int[1 << chromaFormat + 5]
			};
			int num = j;
			int mbWidth = context.mbWidth;
			int mbX2 = ((mbWidth != -1) ? (num % mbWidth) : 0);
			int num2 = j;
			int mbWidth2 = context.mbWidth;
			int mbY2 = ((mbWidth2 != -1) ? (num2 / mbWidth2) : (-num2));
			if (ph.picture_coding_type == 2)
			{
				pred.reset();
			}
			mvZero(context, ph, pred, mbX2, mbY2, predFwd2);
			put(predFwd2, buf, stride, chromaFormat, mbX2, mbY2, context.codedWidth, context.codedHeight >> vertStep, vertOff, vertStep);
		}
		VLC vlcMBType = SequenceScalableExtension.vlcMBType(ph.picture_coding_type, sh.sequenceScalableExtension);
		MPEGConst.MBType[] mbTypeVal = SequenceScalableExtension.mbTypeVal(ph.picture_coding_type, sh.sequenceScalableExtension);
		MPEGConst.MBType mbType = mbTypeVal[vlcMBType.readVLC(bits)];
		if (mbType.macroblock_intra != 1 || mbAddr - prevAddr > 1)
		{
			resetDCPredictors(context, ph);
		}
		int spatial_temporal_weight_code = 0;
		if (mbType.spatial_temporal_weight_code_flag == 1 && ph.pictureSpatialScalableExtension != null && ph.pictureSpatialScalableExtension.spatial_temporal_weight_code_table_index != 0)
		{
			spatial_temporal_weight_code = bits.readNBit(2);
		}
		int motion_type = -1;
		if (mbType.macroblock_motion_forward != 0 || mbType.macroblock_motion_backward != 0)
		{
			motion_type = ((ph.pictureCodingExtension != null && (ph.pictureCodingExtension.picture_structure != 3 || ph.pictureCodingExtension.frame_pred_frame_dct != 1)) ? bits.readNBit(2) : 2);
		}
		int dctType = 0;
		if (ph.pictureCodingExtension != null && ph.pictureCodingExtension.picture_structure == 3 && ph.pictureCodingExtension.frame_pred_frame_dct == 0 && (mbType.macroblock_intra != 0 || mbType.macroblock_pattern != 0))
		{
			dctType = bits.read1Bit();
		}
		if (mbType.macroblock_quant != 0)
		{
			qScaleCode[0] = bits.readNBit(5);
		}
		int concealmentMv = ((ph.pictureCodingExtension != null && ph.pictureCodingExtension.concealment_motion_vectors != 0) ? 1 : 0);
		int[][] predFwd = null;
		int num3 = mbAddr;
		int mbWidth3 = context.mbWidth;
		int mbX = ((mbWidth3 != -1) ? (num3 % mbWidth3) : 0);
		int num4 = mbAddr;
		int mbWidth4 = context.mbWidth;
		int mbY = ((mbWidth4 != -1) ? (num4 / mbWidth4) : (-num4));
		if (mbType.macroblock_intra == 1)
		{
			if (concealmentMv == 0)
			{
				pred.reset();
			}
		}
		else if (mbType.macroblock_motion_forward != 0)
		{
			int refIdx = ((ph.picture_coding_type != 2) ? 1 : 0);
			predFwd = new int[3][]
			{
				new int[256],
				new int[1 << chromaFormat + 5],
				new int[1 << chromaFormat + 5]
			};
			if (ph.pictureCodingExtension == null || ph.pictureCodingExtension.picture_structure == 3)
			{
				pred.predictInFrame(refFrames[refIdx], mbX << 4, mbY << 4, predFwd, bits, motion_type, 0, spatial_temporal_weight_code);
			}
			else if (ph.picture_coding_type == 2)
			{
				pred.predictInField(refFields, mbX << 4, mbY << 4, predFwd, bits, motion_type, 0, ph.pictureCodingExtension.picture_structure - 1);
			}
			else
			{
				pred.predictInField(new Picture[2]
				{
					refFrames[refIdx],
					refFrames[refIdx]
				}, mbX << 4, mbY << 4, predFwd, bits, motion_type, 0, ph.pictureCodingExtension.picture_structure - 1);
			}
		}
		else if (ph.picture_coding_type == 2)
		{
			predFwd = new int[3][]
			{
				new int[256],
				new int[1 << chromaFormat + 5],
				new int[1 << chromaFormat + 5]
			};
			pred.reset();
			mvZero(context, ph, pred, mbX, mbY, predFwd);
		}
		int[][] predBack = null;
		if (mbType.macroblock_motion_backward != 0)
		{
			predBack = new int[3][]
			{
				new int[256],
				new int[1 << chromaFormat + 5],
				new int[1 << chromaFormat + 5]
			};
			if (ph.pictureCodingExtension == null || ph.pictureCodingExtension.picture_structure == 3)
			{
				pred.predictInFrame(refFrames[0], mbX << 4, mbY << 4, predBack, bits, motion_type, 1, spatial_temporal_weight_code);
			}
			else
			{
				pred.predictInField(new Picture[2]
				{
					refFrames[0],
					refFrames[0]
				}, mbX << 4, mbY << 4, predBack, bits, motion_type, 1, ph.pictureCodingExtension.picture_structure - 1);
			}
		}
		context.lastPredB = mbType;
		int[][] pp = ((mbType.macroblock_intra != 1) ? buildPred(predFwd, predBack) : new int[3][]
		{
			new int[256],
			new int[1 << chromaFormat + 5],
			new int[1 << chromaFormat + 5]
		});
		if (mbType.macroblock_intra != 0 && concealmentMv != 0)
		{
			Preconditions.checkState(1 == bits.read1Bit());
		}
		int cbp = ((mbType.macroblock_intra == 1) ? 4095 : 0);
		if (mbType.macroblock_pattern != 0)
		{
			cbp = readCbPattern(bits);
		}
		VLC vlcCoeff = MPEGConst.___003C_003EvlcCoeff0;
		if (ph.pictureCodingExtension != null && mbType.macroblock_intra == 1 && ph.pictureCodingExtension.intra_vlc_format == 1)
		{
			vlcCoeff = MPEGConst.___003C_003EvlcCoeff1;
		}
		int[] qScaleTab = ((ph.pictureCodingExtension == null || ph.pictureCodingExtension.q_scale_type != 1) ? MPEGConst.___003C_003EqScaleTab1 : MPEGConst.___003C_003EqScaleTab2);
		int qScale = qScaleTab[qScaleCode[0]];
		int intra_dc_mult = 8;
		if (ph.pictureCodingExtension != null)
		{
			intra_dc_mult = 8 >> ph.pictureCodingExtension.intra_dc_precision;
		}
		int blkCount = 6 + chromaFormat switch
		{
			1 => 0, 
			2 => 2, 
			_ => 6, 
		};
		int[] block = new int[64];
		int i = 0;
		int cbpMask = 1 << blkCount - 1;
		while (i < blkCount)
		{
			if ((cbp & cbpMask) != 0)
			{
				int[] qmat = context.qMats[((i >= 4) ? 1 : 0) + (mbType.macroblock_intra << 1)];
				if (mbType.macroblock_intra == 1)
				{
					blockIntra(bits, vlcCoeff, block, context.intra_dc_predictor, i, context.scan, (!sh.hasExtensions() && !ph.hasExtensions()) ? 8 : 12, intra_dc_mult, qScale, qmat);
				}
				else
				{
					blockInter(bits, vlcCoeff, block, context.scan, (!sh.hasExtensions() && !ph.hasExtensions()) ? 8 : 12, qScale, qmat);
				}
				mapBlock(block, pp[MPEGConst.___003C_003EBLOCK_TO_CC[i]], i, dctType, chromaFormat);
			}
			i++;
			cbpMask >>= 1;
		}
		put(pp, buf, stride, chromaFormat, mbX, mbY, context.codedWidth, context.codedHeight >> vertStep, vertOff, vertStep);
		return mbAddr;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 11, 66, 106, 191, 22, 100, 111, 159, 16,
		159, 16, 111, 159, 16, 102, 171
	})]
	private void mvZero(Context context, PictureHeader ph, MPEGPred pred, int mbX, int mbY, int[][] mbPix)
	{
		if (ph.picture_coding_type == 2)
		{
			pred.predict16x16NoMV(refFrames[0], mbX << 4, mbY << 4, (ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.picture_structure : 3, 0, mbPix);
			return;
		}
		int[][] pp = mbPix;
		if (context.lastPredB.macroblock_motion_backward == 1)
		{
			pred.predict16x16NoMV(refFrames[0], mbX << 4, mbY << 4, (ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.picture_structure : 3, 1, pp);
			pp = new int[3][]
			{
				new int[(nint)mbPix[0].LongLength],
				new int[(nint)mbPix[1].LongLength],
				new int[(nint)mbPix[2].LongLength]
			};
		}
		if (context.lastPredB.macroblock_motion_forward == 1)
		{
			pred.predict16x16NoMV(refFrames[1], mbX << 4, mbY << 4, (ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.picture_structure : 3, 0, pp);
			if (mbPix != pp)
			{
				avgPred(mbPix, pp);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 6, 130, 127, 0, 108, 140, 159, 13, 159,
		19, 159, 21
	})]
	protected internal virtual void put(int[][] mbPix, byte[][] buf, int stride, int chromaFormat, int mbX, int mbY, int width, int height, int vertOff, int vertStep)
	{
		int chromaStride = stride + (1 << MPEGConst.___003C_003ESQUEEZE_X[chromaFormat]) - 1 >> MPEGConst.___003C_003ESQUEEZE_X[chromaFormat];
		int chromaMBW = 4 - MPEGConst.___003C_003ESQUEEZE_X[chromaFormat];
		int chromaMBH = 4 - MPEGConst.___003C_003ESQUEEZE_Y[chromaFormat];
		putSub(buf[0], (mbY << 4) * (stride << vertStep) + vertOff * stride + (mbX << 4), stride << vertStep, mbPix[0], 4, 4);
		putSub(buf[1], (mbY << chromaMBH) * (chromaStride << vertStep) + vertOff * chromaStride + (mbX << chromaMBW), chromaStride << vertStep, mbPix[1], chromaMBW, chromaMBH);
		putSub(buf[2], (mbY << chromaMBH) * (chromaStride << vertStep) + vertOff * chromaStride + (mbX << chromaMBW), chromaStride << vertStep, mbPix[2], chromaMBW, chromaMBH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 17, 98, 103, 104, 99, 100, 99, 100, 131 })]
	private static int[][] buildPred(int[][] predFwd, int[][] predBack)
	{
		if (predFwd != null && predBack != null)
		{
			avgPred(predFwd, predBack);
			return predFwd;
		}
		if (predFwd != null)
		{
			return predFwd;
		}
		if (predBack != null)
		{
			return predBack;
		}
		
		throw new RuntimeException("Omited pred _in B-frames --> invalid");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		158, 225, 130, 109, 127, 2, 99, 116, 109, 116,
		109
	})]
	private int readCbPattern(BitReader bits)
	{
		int cbp420 = MPEGConst.___003C_003EvlcCBP.readVLC(bits);
		if (sh.sequenceExtension == null || sh.sequenceExtension.chroma_format == 1)
		{
			return cbp420;
		}
		if (sh.sequenceExtension.chroma_format == 2)
		{
			return (cbp420 << 2) | bits.readNBit(2);
		}
		if (sh.sequenceExtension.chroma_format == 3)
		{
			return (cbp420 << 6) | bits.readNBit(6);
		}
		string message = new StringBuilder().append("Unsupported chroma format: ").append(sh.sequenceExtension.chroma_format).toString();
		
		throw new RuntimeException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 244, 130, 106, 119, 111, 107, 105, 136, 109,
		170, 106, 102, 106, 111, 116, 150, 108, 158, 110,
		102, 105
	})]
	protected internal virtual void blockIntra(BitReader bits, VLC vlcCoeff, int[] block, int[] intra_dc_predictor, int blkIdx, int[] scan, int escSize, int intra_dc_mult, int qScale, int[] qmat)
	{
		int cc = MPEGConst.___003C_003EBLOCK_TO_CC[blkIdx];
		int size = ((cc != 0) ? MPEGConst.___003C_003EvlcDCSizeChroma : MPEGConst.___003C_003EvlcDCSizeLuma).readVLC(bits);
		int delta = ((size != 0) ? mpegSigned(bits, size) : 0);
		intra_dc_predictor[cc] += delta;
		int dc = intra_dc_predictor[cc] * intra_dc_mult;
		SparseIDCT.start(block, dc);
		int level;
		for (int idx = 0; idx < 64; SparseIDCT.coeff(block, scan[idx], level))
		{
			int readVLC = vlcCoeff.readVLC(bits);
			switch (readVLC)
			{
			case 2049:
			{
				idx += bits.readNBit(6) + 1;
				int num = twosSigned(bits, escSize) * qScale * qmat[idx];
				level = ((num < 0) ? (-(-num >> 4)) : (num >> 4));
				continue;
			}
			default:
				idx += (readVLC >> 6) + 1;
				level = toSigned((readVLC & 0x3F) * qScale * qmat[idx] >> 4, bits.read1Bit());
				continue;
			case 2048:
				break;
			}
			break;
		}
		SparseIDCT.finish(block);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 236, 98, 99, 115, 104, 122, 104, 101, 99,
		168, 105, 137, 105, 102, 105, 109, 152, 105, 157,
		140, 102, 105
	})]
	protected internal virtual void blockInter(BitReader bits, VLC vlcCoeff, int[] block, int[] scan, int escSize, int qScale, int[] qmat)
	{
		int idx = -1;
		if (vlcCoeff == MPEGConst.___003C_003EvlcCoeff0 && bits.checkNBit(1) == 1)
		{
			bits.read1Bit();
			int dc = toSigned(quantInter(1, qScale * qmat[0]), bits.read1Bit());
			SparseIDCT.start(block, dc);
			idx++;
		}
		else
		{
			SparseIDCT.start(block, 0);
		}
		int ac;
		for (; idx < 64; SparseIDCT.coeff(block, scan[idx], ac))
		{
			int readVLC = vlcCoeff.readVLC(bits);
			switch (readVLC)
			{
			case 2049:
				idx += bits.readNBit(6) + 1;
				ac = quantInterSigned(twosSigned(bits, escSize), qScale * qmat[idx]);
				continue;
			default:
				idx += (readVLC >> 6) + 1;
				ac = toSigned(quantInter(readVLC & 0x3F, qScale * qmat[idx]), bits.read1Bit());
				continue;
			case 2048:
				break;
			}
			break;
		}
		SparseIDCT.finish(block);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 23, 162, 116, 147, 104, 105, 106, 118, 111,
		119, 123, 123, 123, 123, 123, 123, 155, 232, 54,
		242, 76
	})]
	protected internal virtual void mapBlock(int[] block, int[] @out, int blkIdx, int dctType, int chromaFormat)
	{
		int stepVert = ((chromaFormat != 1 || (blkIdx != 4 && blkIdx != 5)) ? dctType : 0);
		int log2stride = ((blkIdx >= 4) ? (4 - MPEGConst.___003C_003ESQUEEZE_X[chromaFormat]) : 4);
		int blkIdxExt = blkIdx + (dctType << 4);
		int x = MPEGConst.___003C_003EBLOCK_POS_X[blkIdxExt];
		int y = MPEGConst.___003C_003EBLOCK_POS_Y[blkIdxExt];
		int off = (y << log2stride) + x;
		int stride = 1 << log2stride + stepVert;
		int i = 0;
		int coeff = 0;
		while (i < 8)
		{
			int num = off;
			int[] array = @out;
			array[num] += block[coeff];
			num = off + 1;
			array = @out;
			array[num] += block[coeff + 1];
			num = off + 2;
			array = @out;
			array[num] += block[coeff + 2];
			num = off + 3;
			array = @out;
			array[num] += block[coeff + 3];
			num = off + 4;
			array = @out;
			array[num] += block[coeff + 4];
			num = off + 5;
			array = @out;
			array[num] += block[coeff + 5];
			num = off + 6;
			array = @out;
			array[num] += block[coeff + 6];
			num = off + 7;
			array = @out;
			array[num] += block[coeff + 7];
			off += stride;
			i++;
			coeff += 8;
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 14, 98, 107, 109, 117, 123, 123, 251, 60,
		42, 234, 72
	})]
	private static void avgPred(int[][] predFwd, int[][] predBack)
	{
		for (int i = 0; i < (nint)predFwd.LongLength; i++)
		{
			for (int j = 0; j < (nint)predFwd[i].LongLength; j += 4)
			{
				predFwd[i][j] = predFwd[i][j] + predBack[i][j] + 1 >> 1;
				predFwd[i][j + 1] = predFwd[i][j + 1] + predBack[i][j + 1] + 1 >> 1;
				predFwd[i][j + 2] = predFwd[i][j + 2] + predBack[i][j + 2] + 1 >> 1;
				predFwd[i][j + 3] = predFwd[i][j + 3] + predBack[i][j + 3] + 1 >> 1;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 3, 162, 131, 105, 112, 110, 114, 114, 114,
		114, 114, 114, 146, 101, 230, 53, 239, 78, 112,
		110, 114, 114, 114, 114, 114, 114, 114, 114, 116,
		116, 116, 116, 116, 116, 148, 102, 230, 45, 234,
		86
	})]
	protected internal virtual void putSub(byte[] big, int off, int stride, int[] block, int mbW, int mbH)
	{
		int blOff = 0;
		if (mbW == 3)
		{
			for (int j = 0; j < 1 << mbH; j++)
			{
				big[off] = (byte)(sbyte)clipTo8Bit(block[blOff]);
				big[off + 1] = (byte)(sbyte)clipTo8Bit(block[blOff + 1]);
				big[off + 2] = (byte)(sbyte)clipTo8Bit(block[blOff + 2]);
				big[off + 3] = (byte)(sbyte)clipTo8Bit(block[blOff + 3]);
				big[off + 4] = (byte)(sbyte)clipTo8Bit(block[blOff + 4]);
				big[off + 5] = (byte)(sbyte)clipTo8Bit(block[blOff + 5]);
				big[off + 6] = (byte)(sbyte)clipTo8Bit(block[blOff + 6]);
				big[off + 7] = (byte)(sbyte)clipTo8Bit(block[blOff + 7]);
				blOff += 8;
				off += stride;
			}
			return;
		}
		for (int i = 0; i < 1 << mbH; i++)
		{
			big[off] = (byte)(sbyte)clipTo8Bit(block[blOff]);
			big[off + 1] = (byte)(sbyte)clipTo8Bit(block[blOff + 1]);
			big[off + 2] = (byte)(sbyte)clipTo8Bit(block[blOff + 2]);
			big[off + 3] = (byte)(sbyte)clipTo8Bit(block[blOff + 3]);
			big[off + 4] = (byte)(sbyte)clipTo8Bit(block[blOff + 4]);
			big[off + 5] = (byte)(sbyte)clipTo8Bit(block[blOff + 5]);
			big[off + 6] = (byte)(sbyte)clipTo8Bit(block[blOff + 6]);
			big[off + 7] = (byte)(sbyte)clipTo8Bit(block[blOff + 7]);
			big[off + 8] = (byte)(sbyte)clipTo8Bit(block[blOff + 8]);
			big[off + 9] = (byte)(sbyte)clipTo8Bit(block[blOff + 9]);
			big[off + 10] = (byte)(sbyte)clipTo8Bit(block[blOff + 10]);
			big[off + 11] = (byte)(sbyte)clipTo8Bit(block[blOff + 11]);
			big[off + 12] = (byte)(sbyte)clipTo8Bit(block[blOff + 12]);
			big[off + 13] = (byte)(sbyte)clipTo8Bit(block[blOff + 13]);
			big[off + 14] = (byte)(sbyte)clipTo8Bit(block[blOff + 14]);
			big[off + 15] = (byte)(sbyte)clipTo8Bit(block[blOff + 15]);
			blOff += 16;
			off += stride;
		}
	}

	[Modifiers(Modifiers.Protected | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(601)]
	protected internal static byte clipTo8Bit(int val)
	{
		return (byte)(sbyte)(((val >= 0) ? ((val <= 255) ? val : 255) : 0) - 128);
	}

	[Modifiers(Modifiers.Protected | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(609)]
	protected internal static int quantInter(int level, int quant)
	{
		return ((level << 1) + 1) * quant >> 5;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 158, 228, 162, 105, 108 })]
	public static int mpegSigned(BitReader bits, int size)
	{
		int val = bits.readNBit(size);
		int sign = (int)(((uint)val >> size - 1) ^ 1);
		return val + sign - (sign << size);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 158, 229, 130, 102 })]
	public static int twosSigned(BitReader bits, int size)
	{
		int shift = 32 - size;
		return bits.readNBit(size) << shift >> shift;
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 158, 226, 98, 105 })]
	public static int toSigned(int val, int s)
	{
		int sign = s << 31 >> 31;
		return (val ^ sign) - sign;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Protected | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(613)]
	protected internal static int quantInterSigned(int level, int quant)
	{
		return (level < 0) ? (-quantInter(-level, quant)) : quantInter(level, quant);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 209, 66, 104, 100, 99, 104 })]
	public static PictureHeader getPictureHeader(ByteBuffer data)
	{
		ByteBuffer bb = getRawPictureHeader(data);
		if (bb == null)
		{
			return null;
		}
		return PictureHeader.read(bb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 214, 98, 104, 100, 104, 105, 131, 104, 99 })]
	private static ByteBuffer getRawPictureHeader(ByteBuffer data)
	{
		for (ByteBuffer segment = MPEGUtil.nextSegment(data); segment != null; segment = MPEGUtil.nextSegment(data))
		{
			int marker = segment.getInt();
			if (marker == 256)
			{
				return segment;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 217, 98, 104, 100, 104, 105, 131, 104, 99 })]
	private static ByteBuffer getSequenceHeader(ByteBuffer data)
	{
		for (ByteBuffer segment = MPEGUtil.nextSegment(data); segment != null; segment = MPEGUtil.nextSegment(data))
		{
			int marker = segment.getInt();
			if (marker == 435)
			{
				return segment;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 162, 105, 127, 8, 191, 37, 111, 159,
		21, 122, 122, 105, 111, 156, 174, 115, 106, 113,
		177
	})]
	public override Picture decodeFrame(ByteBuffer buffer, byte[][] buf)
	{
		PictureHeader ph = readHeader(buffer);
		if ((refFrames[0] == null && ph.picture_coding_type > 1) || (refFrames[1] == null && ph.picture_coding_type > 2))
		{
			string message = new StringBuilder().append("Not enough references to decode ").append((ph.picture_coding_type != 1) ? "B" : "P").append(" frame")
				.toString();
			
			throw new RuntimeException(message);
		}
		Context context = initContext(sh, ph);
		Picture pic = new Picture(context.codedWidth, context.codedHeight, buf, null, context.color, 0, new Rect(0, 0, context.picWidth, context.picHeight));
		if (ph.pictureCodingExtension != null && ph.pictureCodingExtension.picture_structure != 3)
		{
			decodePicture(context, ph, buffer, buf, ph.pictureCodingExtension.picture_structure - 1, 1);
			ph = readHeader(buffer);
			context = initContext(sh, ph);
			decodePicture(context, ph, buffer, buf, ph.pictureCodingExtension.picture_structure - 1, 1);
		}
		else
		{
			decodePicture(context, ph, buffer, buf, 0, 0);
		}
		if (ph.picture_coding_type == 1 || ph.picture_coding_type == 2)
		{
			Picture unused = refFrames[1];
			refFrames[1] = refFrames[0];
			refFrames[0] = copyAndCreateIfNeeded(pic, unused);
		}
		return pic;
	}

	[Modifiers(Modifiers.Protected | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(605)]
	protected internal static int clip(int val)
	{
		return (val >= 0) ? ((val <= 255) ? val : 255) : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 222, 130, 105, 141, 106, 105, 102, 105, 99,
		136, 121, 105, 113, 233, 54, 234, 76
	})]
	public static int probe(ByteBuffer data)
	{
		data = data.duplicate();
		data.order(ByteOrder.BIG_ENDIAN);
		int i = 0;
		while (i < 2 && MPEGUtil.gotoNextMarker(data) != null && data.hasRemaining())
		{
			int marker = data.getInt();
			switch (marker)
			{
			case 256:
			case 432:
			case 433:
			case 434:
			case 435:
			case 436:
			case 437:
			case 438:
			case 439:
			case 440:
				return 50 - i * 10;
			}
			if (marker > 256 && marker < 432)
			{
				return 20 - i * 10;
			}
			i++;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 211, 98, 104, 100, 99 })]
	public static int getSequenceNumber(ByteBuffer data)
	{
		return getPictureHeader(data)?.temporal_reference ?? (-1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 207, 98, 109, 109 })]
	public override VideoCodecMeta getCodecMeta(ByteBuffer data)
	{
		ByteBuffer codecPrivate = getSequenceHeader(data.duplicate());
		SequenceHeader sh = SequenceHeader.read(codecPrivate.duplicate());
		VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(sh.horizontal_size, sh.vertical_size), ColorSpace.___003C_003EYUV420);
		
		return result;
	}
}
