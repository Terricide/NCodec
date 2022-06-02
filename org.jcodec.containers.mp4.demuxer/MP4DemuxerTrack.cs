using System.ComponentModel;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.demuxer;

public class MP4DemuxerTrack : AbstractMP4DemuxerTrack
{
	private int[] sizes;

	private long offInChunk;

	private int noInChunk;

	private int[] syncSamples;

	private int[] partialSync;

	private int ssOff;

	private int psOff;

	private CompositionOffsetsBox.Entry[] compOffsets;

	private int cttsInd;

	private int cttsSubInd;

	private SeekableByteChannel input;

	private MovieBox movie;

	[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 113, 99, 144, 109, 177, 159, 5,
		145, 109, 131, 148, 143, 127, 18, 100, 175, 100,
		127, 18, 100, 175, 105, 105, 125, 111, 127, 13,
		111, 200, 112, 123, 115, 119, 191, 12, 144, 112,
		111, 123, 104, 137, 135, 137
	})]
	public override MP4Packet getNextFrame(ByteBuffer storage)
	{
		if (curFrame >= sizes.LongLength)
		{
			return null;
		}
		int size = sizes[(int)curFrame];
		if (storage != null && storage.remaining() < size)
		{
			
			throw new IllegalArgumentException("Buffer size is not enough to fit a packet");
		}
		long pktPos = chunkOffsets[Math.min((int)((nint)chunkOffsets.LongLength - 1), stcoInd)] + offInChunk;
		ByteBuffer result = readPacketData(input, storage, pktPos, size);
		if (result != null && result.remaining() < size)
		{
			return null;
		}
		int duration = timeToSamples[sttsInd].getSampleDuration();
		int sync = ((syncSamples == null) ? 1 : 0);
		if (syncSamples != null && ssOff < (nint)syncSamples.LongLength && curFrame + 1u == syncSamples[ssOff])
		{
			sync = 1;
			ssOff++;
		}
		int psync = 0;
		if (partialSync != null && psOff < (nint)partialSync.LongLength && curFrame + 1u == partialSync[psOff])
		{
			psync = 1;
			psOff++;
		}
		long realPts = pts;
		if (compOffsets != null)
		{
			realPts = pts + compOffsets[cttsInd].getOffset();
			cttsSubInd++;
			if (cttsInd < (nint)compOffsets.LongLength - 1 && cttsSubInd == compOffsets[cttsInd].getCount())
			{
				cttsInd++;
				cttsSubInd = 0;
			}
		}
		ByteBuffer data = ((result != null) ? convertPacket(result) : null);
		long _pts = QTTimeUtil.mediaToEdited(box, realPts, movie.getTimescale());
		Packet.FrameType ftype = ((sync == 0) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY);
		int entryNo = sampleToChunks[stscInd].getEntry() - 1;
		MP4Packet.___003Cclinit_003E();
		MP4Packet pkt = new MP4Packet(data, _pts, timescale, duration, curFrame, ftype, null, 0, realPts, entryNo, pktPos, size, (byte)psync != 0);
		offInChunk += size;
		curFrame++;
		noInChunk++;
		if (noInChunk >= sampleToChunks[stscInd].getCount())
		{
			noInChunk = 0;
			offInChunk = 0L;
			nextChunk();
		}
		shiftPts(1L);
		return pkt;
	}

	[LineNumberTable(199)]
	public override long getFrameCount()
	{
		return sizes.LongLength;
	}

	[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 113, 99, 144 })]
	public override MP4Packet nextFrame()
	{
		if (curFrame >= sizes.LongLength)
		{
			return null;
		}
		int size = sizes[(int)curFrame];
		MP4Packet result = getNextFrame(ByteBuffer.allocate(size));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 162, 106, 104, 104, 124, 124, 124, 125,
		117, 100, 141, 100, 173, 109
	})]
	public MP4DemuxerTrack(MovieBox mov, TrakBox trak, SeekableByteChannel input)
		: base(trak)
	{
		this.input = input;
		movie = mov;
		SampleSizesBox stsz = (SampleSizesBox)NodeBox.findFirstPath(trak, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
		SyncSamplesBox stss = (SyncSamplesBox)NodeBox.findFirstPath(trak, ClassLiteral<SyncSamplesBox>.Value, Box.path("mdia.minf.stbl.stss"));
		SyncSamplesBox stps = (SyncSamplesBox)NodeBox.findFirstPath(trak, ClassLiteral<SyncSamplesBox>.Value, Box.path("mdia.minf.stbl.stps"));
		compOffsets = ((CompositionOffsetsBox)NodeBox.findFirstPath(trak, ClassLiteral<CompositionOffsetsBox>.Value, Box.path("mdia.minf.stbl.ctts")))?.getEntries();
		if (stss != null)
		{
			syncSamples = stss.getSyncSamples();
		}
		if (stps != null)
		{
			partialSync = stps.getSyncSamples();
		}
		sizes = stsz.getSizes();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 98, 105, 107, 102, 113, 106, 99, 106,
		99, 109, 111, 23, 199
	})]
	public override bool gotoSyncFrame(long frameNo)
	{
		if (syncSamples == null)
		{
			bool result = gotoFrame(frameNo);
			
			return result;
		}
		if (frameNo < 0u)
		{
			
			throw new IllegalArgumentException("negative frame number");
		}
		if (frameNo >= getFrameCount())
		{
			return false;
		}
		if (frameNo == curFrame)
		{
			return true;
		}
		for (int i = 0; i < (nint)syncSamples.LongLength; i++)
		{
			if (syncSamples[i] - 1 > frameNo)
			{
				bool result2 = gotoFrame(syncSamples[i - 1] - 1);
				
				return result2;
			}
		}
		bool result3 = gotoFrame(syncSamples[(nint)syncSamples.LongLength - 1] - 1);
		
		return result3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 105, 105, 104, 123, 127, 1, 209,
		106, 104, 104, 105, 137, 123, 159, 1, 169, 108,
		63, 2, 199, 105, 191, 33, 105, 191, 33
	})]
	protected internal override void seekPointer(long frameNo)
	{
		if (compOffsets != null)
		{
			cttsSubInd = (int)frameNo;
			cttsInd = 0;
			while (cttsSubInd >= compOffsets[cttsInd].getCount())
			{
				cttsSubInd -= compOffsets[cttsInd].getCount();
				cttsInd++;
			}
		}
		curFrame = (int)frameNo;
		stcoInd = 0;
		stscInd = 0;
		noInChunk = (int)frameNo;
		offInChunk = 0L;
		while (noInChunk >= sampleToChunks[stscInd].getCount())
		{
			noInChunk -= sampleToChunks[stscInd].getCount();
			nextChunk();
		}
		for (int i = 0; i < noInChunk; i++)
		{
			offInChunk += sizes[(int)frameNo - noInChunk + i];
		}
		if (syncSamples != null)
		{
			ssOff = 0;
			while (ssOff < (nint)syncSamples.LongLength && syncSamples[ssOff] < curFrame + 1u)
			{
				ssOff++;
			}
		}
		if (partialSync != null)
		{
			psOff = 0;
			while (psOff < (nint)partialSync.LongLength && partialSync[psOff] < curFrame + 1u)
			{
				psOff++;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LineNumberTable(29)]
	public virtual Packet _003Cbridge_003EnextFrame()
	{
		MP4Packet result = nextFrame();
		
		return result;
	}
}
