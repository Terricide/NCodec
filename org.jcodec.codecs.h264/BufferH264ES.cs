using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264;

[Implements(new string[] { "org.jcodec.common.DemuxerTrack", "org.jcodec.common.Demuxer" })]
public class BufferH264ES : java.lang.Object, DemuxerTrack, Demuxer, Closeable, AutoCloseable
{
	private ByteBuffer bb;

	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;")]
	private IntObjectMap pps;

	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;")]
	private IntObjectMap sps;

	private int prevFrameNumOffset;

	private int prevFrameNum;

	private int prevPicOrderCntMsb;

	private int prevPicOrderCntLsb;

	private int frameNo;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 105, 108, 140, 104, 104 })]
	public BufferH264ES(ByteBuffer bb)
	{
		pps = new IntObjectMap();
		sps = new IntObjectMap();
		this.bb = bb;
		frameNo = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 162, 104, 104, 120, 127, 2 })]
	private SliceHeader readSliceHeader(ByteBuffer buf, NALUnit nu)
	{
		BitReader br = BitReader.createBitReader(buf);
		SliceHeader sh = SliceHeaderReader.readPart1(br);
		PictureParameterSet pp = (PictureParameterSet)pps.get(sh.picParameterSetId);
		SliceHeaderReader.readPart2(sh, nu, (SeqParameterSet)sps.get(pp.seqParameterSetId), pp, br);
		return sh;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 162, 112, 131, 112, 131, 136, 120, 131,
		127, 17, 131, 127, 0, 131, 125, 131, 112, 131
	})]
	private bool sameFrame(NALUnit nu1, NALUnit nu2, SliceHeader sh1, SliceHeader sh2)
	{
		if (sh1.picParameterSetId != sh2.picParameterSetId)
		{
			return false;
		}
		if (sh1.frameNum != sh2.frameNum)
		{
			return false;
		}
		SeqParameterSet sps = sh1.sps;
		if (sps.picOrderCntType == 0 && sh1.picOrderCntLsb != sh2.picOrderCntLsb)
		{
			return false;
		}
		if (sps.picOrderCntType == 1 && (sh1.deltaPicOrderCnt[0] != sh2.deltaPicOrderCnt[0] || sh1.deltaPicOrderCnt[1] != sh2.deltaPicOrderCnt[1]))
		{
			return false;
		}
		if ((nu1.nal_ref_idc == 0 || nu2.nal_ref_idc == 0) && nu1.nal_ref_idc != nu2.nal_ref_idc)
		{
			return false;
		}
		if (nu1.type == NALUnitType.___003C_003EIDR_SLICE != (nu2.type == NALUnitType.___003C_003EIDR_SLICE))
		{
			return false;
		}
		if (sh1.idrPicId != sh2.idrPicId)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 130, 116, 107, 137, 155, 99, 110, 139 })]
	private Packet detectPoc(ByteBuffer result, NALUnit nu, SliceHeader sh)
	{
		int maxFrameNum = 1 << sh.sps.log2MaxFrameNumMinus4 + 4;
		if (detectGap(sh, maxFrameNum))
		{
			issueNonExistingPic(sh, maxFrameNum);
		}
		int absFrameNum = updateFrameNumber(sh.frameNum, maxFrameNum, detectMMCO5(sh.refPicMarkingNonIDR));
		int poc = 0;
		if (nu.type == NALUnitType.___003C_003ENON_IDR_SLICE)
		{
			poc = calcPoc(absFrameNum, nu, sh);
		}
		Packet.___003Cclinit_003E();
		long pts = absFrameNum;
		long duration = 1L;
		int num = frameNo;
		frameNo = num + 1;
		Packet result2 = new Packet(result, pts, 1, duration, num, (nu.type != NALUnitType.___003C_003EIDR_SLICE) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY, null, poc);
		return result2;
	}

	[LineNumberTable(160)]
	private bool detectGap(SliceHeader sh, int maxFrameNum)
	{
		int result;
		if (sh.frameNum != prevFrameNum)
		{
			int frameNum = sh.frameNum;
			int num = prevFrameNum + 1;
			if (frameNum != ((maxFrameNum != -1) ? (num % maxFrameNum) : 0))
			{
				result = 1;
				goto IL_002f;
			}
		}
		result = 0;
		goto IL_002f;
		IL_002f:
		return (byte)result != 0;
	}

	[LineNumberTable(new byte[] { 159, 104, 130, 149, 104 })]
	private void issueNonExistingPic(SliceHeader sh, int maxFrameNum)
	{
		int num = prevFrameNum + 1;
		int nextFrameNum = (prevFrameNum = ((maxFrameNum != -1) ? (num % maxFrameNum) : 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 66, 100, 131, 104, 104, 101, 110, 227,
		61, 231, 70
	})]
	private bool detectMMCO5(RefPicMarking refPicMarkingNonIDR)
	{
		if (refPicMarkingNonIDR == null)
		{
			return false;
		}
		RefPicMarking.Instruction[] instructions = refPicMarkingNonIDR.getInstructions();
		for (int i = 0; i < (nint)instructions.LongLength; i++)
		{
			RefPicMarking.Instruction instr = instructions[i];
			if (instr.getType() == RefPicMarking.InstrType.___003C_003ECLEAR)
			{
				return true;
			}
		}
		return false;
	}

	[LineNumberTable(new byte[] { 159, 107, 97, 67, 106, 140, 136, 133, 110, 104 })]
	private int updateFrameNumber(int frameNo, int maxFrameNum, bool mmco5)
	{
		int frameNumOffset = ((prevFrameNum <= frameNo) ? prevFrameNumOffset : (prevFrameNumOffset + maxFrameNum));
		int absFrameNum = frameNumOffset + frameNo;
		prevFrameNum = ((!mmco5) ? frameNo : 0);
		prevFrameNumOffset = frameNumOffset;
		return absFrameNum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 66, 110, 108, 111, 141 })]
	private int calcPoc(int absFrameNum, NALUnit nu, SliceHeader sh)
	{
		if (sh.sps.picOrderCntType == 0)
		{
			int result = calcPOC0(nu, sh);
			return result;
		}
		if (sh.sps.picOrderCntType == 1)
		{
			int result2 = calcPOC1(absFrameNum, nu, sh);
			return result2;
		}
		int result3 = calcPOC2(absFrameNum, nu, sh);
		return result3;
	}

	[LineNumberTable(new byte[]
	{
		159, 90, 162, 104, 212, 119, 108, 119, 140, 136,
		105, 104, 168
	})]
	private int calcPOC0(NALUnit nu, SliceHeader sh)
	{
		int pocCntLsb = sh.picOrderCntLsb;
		int maxPicOrderCntLsb = 1 << sh.sps.log2MaxPicOrderCntLsbMinus4 + 4;
		int picOrderCntMsb = ((pocCntLsb < prevPicOrderCntLsb && prevPicOrderCntLsb - pocCntLsb >= maxPicOrderCntLsb / 2) ? (prevPicOrderCntMsb + maxPicOrderCntLsb) : ((pocCntLsb <= prevPicOrderCntLsb || pocCntLsb - prevPicOrderCntLsb <= maxPicOrderCntLsb / 2) ? prevPicOrderCntMsb : (prevPicOrderCntMsb - maxPicOrderCntLsb)));
		if (nu.nal_ref_idc != 0)
		{
			prevPicOrderCntMsb = picOrderCntMsb;
			prevPicOrderCntLsb = pocCntLsb;
		}
		return picOrderCntMsb + pocCntLsb;
	}

	[LineNumberTable(new byte[]
	{
		159, 97, 162, 110, 100, 109, 134, 99, 113, 49,
		199, 104, 121, 154, 102, 105, 52, 137, 99, 132,
		105, 145
	})]
	private int calcPOC1(int absFrameNum, NALUnit nu, SliceHeader sh)
	{
		if (sh.sps.numRefFramesInPicOrderCntCycle == 0)
		{
			absFrameNum = 0;
		}
		if (nu.nal_ref_idc == 0 && absFrameNum > 0)
		{
			absFrameNum--;
		}
		int expectedDeltaPerPicOrderCntCycle = 0;
		for (int j = 0; j < sh.sps.numRefFramesInPicOrderCntCycle; j++)
		{
			expectedDeltaPerPicOrderCntCycle += sh.sps.offsetForRefFrame[j];
		}
		int expectedPicOrderCnt;
		if (absFrameNum > 0)
		{
			int num = absFrameNum - 1;
			int numRefFramesInPicOrderCntCycle = sh.sps.numRefFramesInPicOrderCntCycle;
			int picOrderCntCycleCnt = ((numRefFramesInPicOrderCntCycle != -1) ? (num / numRefFramesInPicOrderCntCycle) : (-num));
			int num2 = absFrameNum - 1;
			int numRefFramesInPicOrderCntCycle2 = sh.sps.numRefFramesInPicOrderCntCycle;
			int frameNumInPicOrderCntCycle = ((numRefFramesInPicOrderCntCycle2 != -1) ? (num2 % numRefFramesInPicOrderCntCycle2) : 0);
			expectedPicOrderCnt = picOrderCntCycleCnt * expectedDeltaPerPicOrderCntCycle;
			for (int i = 0; i <= frameNumInPicOrderCntCycle; i++)
			{
				expectedPicOrderCnt += sh.sps.offsetForRefFrame[i];
			}
		}
		else
		{
			expectedPicOrderCnt = 0;
		}
		if (nu.nal_ref_idc == 0)
		{
			expectedPicOrderCnt += sh.sps.offsetForNonRefPic;
		}
		return expectedPicOrderCnt + sh.deltaPicOrderCnt[0];
	}

	[LineNumberTable(new byte[] { 159, 99, 162, 105, 135 })]
	private int calcPOC2(int absFrameNum, NALUnit nu, SliceHeader sh)
	{
		if (nu.nal_ref_idc == 0)
		{
			return 2 * absFrameNum - 1;
		}
		return 2 * absFrameNum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 75, 162, 103, 105 })]
	public virtual List getVideoTracks()
	{
		ArrayList tracks = new ArrayList();
		((List)tracks).add((object)this);
		return tracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 141, 99, 131, 109, 109, 100, 134,
		137, 125, 140, 117, 109, 134, 100, 100, 113, 105,
		117, 113, 105, 149, 134, 147
	})]
	public virtual Packet nextFrame()
	{
		ByteBuffer result = bb.duplicate();
		NALUnit prevNu = null;
		SliceHeader prevSh = null;
		while (true)
		{
			bb.mark();
			ByteBuffer buf = H264Utils.nextNALUnit(bb);
			if (buf == null)
			{
				break;
			}
			NALUnit nu = NALUnit.read(buf);
			if (nu.type == NALUnitType.___003C_003EIDR_SLICE || nu.type == NALUnitType.___003C_003ENON_IDR_SLICE)
			{
				SliceHeader sh = readSliceHeader(buf, nu);
				if (prevNu != null && prevSh != null && !sameFrame(prevNu, nu, prevSh, sh))
				{
					bb.reset();
					break;
				}
				prevSh = sh;
				prevNu = nu;
			}
			else if (nu.type == NALUnitType.___003C_003EPPS)
			{
				PictureParameterSet read2 = PictureParameterSet.read(buf);
				pps.put(read2.picParameterSetId, read2);
			}
			else if (nu.type == NALUnitType.___003C_003ESPS)
			{
				SeqParameterSet read = SeqParameterSet.read(buf);
				sps.put(read.seqParameterSetId, read);
			}
		}
		result.limit(bb.position());
		Packet result2 = ((prevSh != null) ? detectPoc(result, prevNu, prevSh) : null);
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(246)]
	public virtual SeqParameterSet[] getSps()
	{
		return (SeqParameterSet[])sps.values(new SeqParameterSet[0]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(250)]
	public virtual PictureParameterSet[] getPps()
	{
		return (PictureParameterSet[])pps.values(new PictureParameterSet[0]);
	}

	[LineNumberTable(255)]
	public virtual DemuxerTrackMeta getMeta()
	{
		return null;
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(262)]
	public virtual void close()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(266)]
	public virtual List getTracks()
	{
		List videoTracks = getVideoTracks();
		return videoTracks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 73, 130, 103 })]
	public virtual List getAudioTracks()
	{
		return new ArrayList();
	}

    public void Dispose()
    {
		close();
	}
}
