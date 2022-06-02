using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.common.biari;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.codecs.h264.io;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;

namespace org.jcodec.codecs.h264.decode;

public class FrameReader : Object
{
	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;")]
	private IntObjectMap sps;

	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;")]
	private IntObjectMap pps;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 66, 104, 104, 125, 127, 3, 150, 153,
		191, 58, 112, 138, 100, 113, 103, 127, 19, 120,
		120, 171
	})]
	private SliceReader createSliceReader(ByteBuffer segment, NALUnit nalUnit)
	{
		BitReader _in = BitReader.createBitReader(segment);
		SliceHeader sh = SliceHeaderReader.readPart1(_in);
		sh.pps = (PictureParameterSet)pps.get(sh.picParameterSetId);
		sh.sps = (SeqParameterSet)sps.get(sh.pps.seqParameterSetId);
		SliceHeaderReader.readPart2(sh, nalUnit, sh.sps, sh.pps, _in);
		Mapper mapper = new MapManager(sh.sps, sh.pps).getMapper(sh);
		CAVLC[] array = new CAVLC[3];
		CAVLC.___003Cclinit_003E();
		array[0] = new CAVLC(sh.sps, sh.pps, 2, 2);
		CAVLC.___003Cclinit_003E();
		array[1] = new CAVLC(sh.sps, sh.pps, 1, 1);
		CAVLC.___003Cclinit_003E();
		array[2] = new CAVLC(sh.sps, sh.pps, 1, 1);
		CAVLC[] cavlc = array;
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		CABAC cabac = new CABAC(mbWidth);
		MDecoder mDecoder = null;
		if (sh.pps.entropyCodingModeFlag)
		{
			_in.terminate();
			int[] array2 = new int[2];
			int num = (array2[1] = 1024);
			num = (array2[0] = 2);
			int[][] cm = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array2);
			int qp = sh.pps.picInitQpMinus26 + 26 + sh.sliceQpDelta;
			cabac.initModels(cm, sh.sliceType, sh.cabacInitIdc, qp);
			mDecoder = new MDecoder(segment, cm);
		}
		SliceReader result = new SliceReader(sh.pps, cabac, cavlc, mDecoder, _in, mapper, sh, nalUnit);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 98, 104, 103, 104, 117 })]
	public virtual void addSps(ByteBuffer byteBuffer)
	{
		ByteBuffer clone = NIOUtils.clone(byteBuffer);
		H264Utils.unescapeNAL(clone);
		SeqParameterSet s = SeqParameterSet.read(clone);
		sps.put(s.seqParameterSetId, s);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 130, 104, 103, 104, 117 })]
	public virtual void addPps(ByteBuffer byteBuffer)
	{
		ByteBuffer clone = NIOUtils.clone(byteBuffer);
		H264Utils.unescapeNAL(clone);
		PictureParameterSet p = PictureParameterSet.read(clone);
		pps.put(p.picParameterSetId, p);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 105, 108, 108 })]
	public FrameReader()
	{
		sps = new IntObjectMap();
		pps = new IntObjectMap();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)Ljava/util/List<Lorg/jcodec/codecs/h264/decode/SliceReader;>;")]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 135, 127, 0, 136, 103, 110, 105,
		117, 115, 105, 117, 125, 123, 107, 131, 144, 134
	})]
	public virtual List readFrame(List nalUnits)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = nalUnits.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer nalData = (ByteBuffer)iterator.next();
			NALUnit nalUnit = NALUnit.read(nalData);
			H264Utils.unescapeNAL(nalData);
			if (NALUnitType.___003C_003ESPS == nalUnit.type)
			{
				SeqParameterSet _sps = SeqParameterSet.read(nalData);
				sps.put(_sps.seqParameterSetId, _sps);
			}
			else if (NALUnitType.___003C_003EPPS == nalUnit.type)
			{
				PictureParameterSet _pps = PictureParameterSet.read(nalData);
				pps.put(_pps.picParameterSetId, _pps);
			}
			else if (NALUnitType.___003C_003EIDR_SLICE == nalUnit.type || NALUnitType.___003C_003ENON_IDR_SLICE == nalUnit.type)
			{
				if (sps.size() == 0 || pps.size() == 0)
				{
					Logger.warn("Skipping frame as no SPS/PPS have been seen so far...");
					return null;
				}
				((List)result).add((object)createSliceReader(nalData, nalUnit));
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[] { 159, 118, 162, 124, 104, 99 })]
	public virtual void addSpsList(List spsList)
	{
		Iterator iterator = spsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			addSps(byteBuffer);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[] { 159, 114, 66, 124, 104, 99 })]
	public virtual void addPpsList(List ppsList)
	{
		Iterator iterator = ppsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			addPps(byteBuffer);
		}
	}
}
