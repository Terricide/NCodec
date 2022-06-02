using System;
using System.Runtime.CompilerServices;
using System.Threading;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.mpeg12;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.containers.mps;

[Implements(new string[] { "org.jcodec.containers.mps.MPEGDemuxer" })]
public class MPSDemuxer : SegmentReader, MPEGDemuxer, Demuxer, Closeable, AutoCloseable
{
	public class AACTrack : PlainTrack
	{
		[Signature("Ljava/util/List<Lorg/jcodec/common/model/Packet;>;")]
		private List audioStash;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 71, 98, 108, 108 })]
		public AACTrack(MPSDemuxer demuxer, int streamId, PESPacket pkt)
			: base(demuxer, streamId, pkt)
		{
			audioStash = new ArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 70, 162, 113, 105, 103, 104, 109, 104, 108,
			111, 107, 118, 49, 134, 105, 107, 111, 105, 109,
			166, 110, 99
		})]
		public override Packet nextFrame()
		{
			if (audioStash.size() == 0)
			{
				Packet nextFrame = nextFrameWithBuffer(null);
				if (nextFrame != null)
				{
					ByteBuffer data = nextFrame.getData();
					ADTSParser.Header adts = ADTSParser.read(data.duplicate());
					long nextPts = nextFrame.getPts();
					while (data.hasRemaining())
					{
						ByteBuffer data2 = NIOUtils.read(data, adts.getSize());
						Packet pkt = Packet.createPacketWithData(nextFrame, data2);
						int num = pkt.getTimescale() * 1024;
						int num2 = AACConts.AAC_SAMPLE_RATES[adts.getSamplingIndex()];
						pkt.setDuration((num2 != -1) ? (num / num2) : (-num));
						pkt.setPts(nextPts);
						nextPts += pkt.getDuration();
						audioStash.add(pkt);
						if (data.hasRemaining())
						{
							adts = ADTSParser.read(data.duplicate());
						}
					}
				}
			}
			if (audioStash.size() == 0)
			{
				return null;
			}
			return (Packet)audioStash.remove(0);
		}
	}

	[Implements(new string[] { "org.jcodec.containers.mps.MPEGDemuxer$MPEGDemuxerTrack" })]
	public abstract class BaseTrack : java.lang.Object, MPEGDemuxer.MPEGDemuxerTrack, DemuxerTrack
	{
		protected internal int streamId;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/mps/PESPacket;>;")]
		protected internal List _pending;

		protected internal MPSDemuxer demuxer;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 119, 162, 105, 108, 104, 104, 110 })]
		public BaseTrack(MPSDemuxer demuxer, int streamId, PESPacket pkt)
		{
			_pending = new ArrayList();
			this.demuxer = demuxer;
			this.streamId = streamId;
			_pending.add(pkt);
		}

		[LineNumberTable(104)]
		public virtual int getSid()
		{
			return streamId;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 115, 66, 105, 144, 116 })]
		public virtual void pending(PESPacket pkt)
		{
			if (_pending != null)
			{
				_pending.add(pkt);
			}
			else
			{
				demuxer.putBack(pkt.data);
			}
		}

		[Signature("()Ljava/util/List<Lorg/jcodec/containers/mps/PESPacket;>;")]
		[LineNumberTable(116)]
		public virtual List getPending()
		{
			return _pending;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 112, 98, 105, 98, 127, 2, 114, 99, 104 })]
		public virtual void ignore()
		{
			if (_pending != null)
			{
				Iterator iterator = _pending.iterator();
				while (iterator.hasNext())
				{
					PESPacket pesPacket = (PESPacket)iterator.next();
					demuxer.putBack(pesPacket.data);
				}
				_pending = null;
			}
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public abstract Packet nextFrame();

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public abstract DemuxerTrackMeta getMeta();

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public abstract Packet nextFrameWithBuffer(ByteBuffer P_0);
	}

	[Implements(new string[] { "java.nio.channels.ReadableByteChannel" })]
	public class MPEGTrack : BaseTrack, ReadableByteChannel, Channel, Closeable, AutoCloseable
	{
		private MPEGES es;

		private LongArrayList ptsSeen;

		private long lastPts;

		private int lastSeq;

		private int lastSeqSeen;

		private int seqWrap;

		private IntIntHistogram durationHistogram;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 107, 130, 108, 114, 110, 108, 108, 108, 108 })]
		public MPEGTrack(MPSDemuxer demuxer, int streamId, PESPacket pkt)
			: base(demuxer, streamId, pkt)
		{
			es = new MPEGES(this, 4096);
			ptsSeen = new LongArrayList(32);
			lastSeq = int.MinValue;
			lastSeqSeen = 2147482647;
			seqWrap = 2147482647;
			durationHistogram = new IntIntHistogram();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 98, 130, 111, 147, 123, 111, 107, 146, 131,
			175
		})]
		private PESPacket getPacket()
		{
			if (_pending.size() > 0)
			{
				return (PESPacket)_pending.remove(0);
			}
			PESPacket pkt;
			while ((pkt = demuxer.nextPacket(demuxer.getBuffer())) != null)
			{
				if (pkt.streamId == streamId)
				{
					if (pkt.pts != -1)
					{
						ptsSeen.add(pkt.pts);
					}
					return pkt;
				}
				access_0024000(demuxer, pkt);
			}
			return null;
		}

		[LineNumberTable(152)]
		public virtual bool isOpen()
		{
			return true;
		}

		[LineNumberTable(156)]
		public virtual MPEGES getES()
		{
			return es;
		}

		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(160)]
		public virtual void close()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 102, 162, 127, 10, 113, 99, 120, 148, 110,
			144, 146
		})]
		public virtual int read(ByteBuffer arg0)
		{
			PESPacket pes = ((_pending.size() <= 0) ? getPacket() : ((PESPacket)_pending.remove(0)));
			if (pes == null || !pes.data.hasRemaining())
			{
				return -1;
			}
			int toRead = java.lang.Math.min(arg0.remaining(), pes.data.remaining());
			arg0.put(NIOUtils.read(pes.data, toRead));
			if (pes.data.hasRemaining())
			{
				_pending.add(0, pes);
			}
			else
			{
				demuxer.putBack(pes.data);
			}
			return toRead;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(196)]
		public override Packet nextFrameWithBuffer(ByteBuffer buf)
		{
			MPEGPacket result = es.frame(buf);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 92, 98, 109, 100, 99, 109, 100, 111, 104,
			111, 159, 29, 114, 115, 127, 13, 47, 166, 109,
			136, 115, 108
		})]
		public override Packet nextFrame()
		{
			MPEGPacket pkt = es.getFrame();
			if (pkt == null)
			{
				return null;
			}
			int seq = MPEGDecoder.getSequenceNumber(pkt.getData());
			if (seq == 0)
			{
				seqWrap = lastSeqSeen + 1;
			}
			lastSeqSeen = seq;
			if (ptsSeen.size() <= 0)
			{
				pkt.setPts(java.lang.Math.min(seq - lastSeq, seq - lastSeq + seqWrap) * durationHistogram.max() + lastPts);
			}
			else
			{
				pkt.setPts(ptsSeen.shift());
				if (lastSeq >= 0 && seq > lastSeq)
				{
					IntIntHistogram intIntHistogram = durationHistogram;
					int num = (int)(pkt.getPts() - lastPts);
					int num2 = java.lang.Math.min(seq - lastSeq, seq - lastSeq + seqWrap);
					intIntHistogram.increment((num2 != -1) ? (num / num2) : (-num));
				}
				lastPts = pkt.getPts();
				lastSeq = seq;
			}
			pkt.setDuration(durationHistogram.max());
			java.lang.System.@out.println(seq);
			return pkt;
		}

		[LineNumberTable(226)]
		public override DemuxerTrackMeta getMeta()
		{
			return null;
		}

		public void Dispose()
		{
			close();
		}
	}

	public class PlainTrack : BaseTrack
	{
		private int frameNo;

		private Packet lastFrame;

		private long lastKnownDuration;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 80, 98, 111, 149, 127, 10, 143 })]
		public override Packet nextFrameWithBuffer(ByteBuffer buf)
		{
			PESPacket pkt;
			if (_pending.size() > 0)
			{
				pkt = (PESPacket)_pending.remove(0);
			}
			else
			{
				while ((pkt = demuxer.nextPacket(demuxer.getBuffer())) != null && pkt.streamId != streamId)
				{
					access_0024000(demuxer, pkt);
				}
			}
			object result;
			if (pkt == null)
			{
				result = null;
			}
			else
			{
				ByteBuffer data = pkt.data;
				long pts = pkt.pts;
				long duration = 0L;
				int num = frameNo;
				frameNo = num + 1;
				result = Packet.createPacket(data, pts, 90000, duration, num, Packet.FrameType.___003C_003EUNKNOWN, null);
			}
			
			return (Packet)result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 83, 66, 236, 61, 205 })]
		public PlainTrack(MPSDemuxer demuxer, int streamId, PESPacket pkt)
			: base(demuxer, streamId, pkt)
		{
			lastKnownDuration = 3003L;
		}

		[LineNumberTable(240)]
		public virtual bool isOpen()
		{
			return true;
		}

		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(244)]
		public virtual void close()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 77, 98, 105, 110, 105, 99, 104, 110, 105,
			153, 141
		})]
		public override Packet nextFrame()
		{
			if (lastFrame == null)
			{
				lastFrame = nextFrameWithBuffer(null);
			}
			if (lastFrame == null)
			{
				return null;
			}
			Packet toReturn = lastFrame;
			lastFrame = nextFrameWithBuffer(null);
			if (lastFrame != null)
			{
				lastKnownDuration = lastFrame.getPts() - toReturn.getPts();
			}
			toReturn.setDuration(lastKnownDuration);
			return toReturn;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 73, 66, 127, 16 })]
		public override DemuxerTrackMeta getMeta()
		{
			TrackType t = (MPSUtils.videoStream(streamId) ? TrackType.___003C_003EVIDEO : ((!MPSUtils.audioStream(streamId)) ? TrackType.___003C_003EOTHER : TrackType.___003C_003EAUDIO));
			return null;
		}
	}

	private const int BUFFER_SIZE = 1048576;

	[Signature("Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mps/MPSDemuxer$BaseTrack;>;")]
	private Map streams;

	private ReadableByteChannel channel;

	[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	private List bufPool;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 128, 98, 111, 108, 104, 140, 105 })]
	public MPSDemuxer(ReadableByteChannel channel)
		: base(channel, 4096)
	{
		streams = new HashMap();
		this.channel = channel;
		bufPool = new ArrayList();
		findStreams();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mps/MPEGDemuxer$MPEGDemuxerTrack;>;")]
	[LineNumberTable(360)]
	public virtual List getTracks()
	{
		
		ArrayList result = new ArrayList(streams.values());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mps/MPEGDemuxer$MPEGDemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 49, 98, 103, 127, 7, 110, 105, 99 })]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		Iterator iterator = streams.values().iterator();
		while (iterator.hasNext())
		{
			BaseTrack p = (BaseTrack)iterator.next();
			if (MPSUtils.audioStream(p.streamId))
			{
				((List)result).add((object)p);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mps/MPEGDemuxer$MPEGDemuxerTrack;>;")]
	[LineNumberTable(new byte[] { 159, 51, 66, 103, 127, 7, 110, 105, 99 })]
	public virtual List getVideoTracks()
	{
		ArrayList result = new ArrayList();
		Iterator iterator = streams.values().iterator();
		while (iterator.hasNext())
		{
			BaseTrack p = (BaseTrack)iterator.next();
			if (MPSUtils.videoStream(p.streamId))
			{
				((List)result).add((object)p);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 66, 104, 110, 110, 112 })]
	public virtual void putBack(ByteBuffer buffer)
	{
		buffer.clear();
		lock (bufPool)
		{
			bufPool.add(buffer);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 162, 110, 111, 155, 120 })]
	public virtual ByteBuffer getBuffer()
	{
		List obj;
		Monitor.Enter(obj = bufPool);
		try
		{
			if (bufPool.size() > 0)
			{
				ByteBuffer result = (ByteBuffer)bufPool.remove(0);
				Monitor.Exit(obj);
				return result;
			}
			Monitor.Exit(obj);
		}
		catch
		{
			//try-fault
			Monitor.Exit(obj);
			throw;
		}
		return ByteBuffer.allocate(1048576);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 58, 130, 136, 110, 105, 163, 104, 105, 110,
		105, 119, 131, 152, 110, 104
	})]
	public virtual PESPacket nextPacket(ByteBuffer @out)
	{
		ByteBuffer dup = @out.duplicate();
		while (!MPSUtils.psMarker(curMarker))
		{
			if (!skipToMarker())
			{
				return null;
			}
		}
		ByteBuffer fork = dup.duplicate();
		readToNextMarker(dup);
		PESPacket pkt = MPSUtils.readPESHeader(fork, curPos());
		if (pkt.length == 0)
		{
			while (!MPSUtils.psMarker(curMarker) && readToNextMarker(dup))
			{
			}
		}
		else
		{
			read(dup, pkt.length - dup.position() + 6);
		}
		fork.limit(dup.position());
		pkt.data = fork;
		return pkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(49)]
	internal static void access_0024000(MPSDemuxer x0, PESPacket x1)
	{
		x0.addToStream(x1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 62, 162, 125, 103, 111, 113, 111, 145, 111,
		155, 138
	})]
	private void addToStream(PESPacket pkt)
	{
		BaseTrack pes = (BaseTrack)streams.get(Integer.valueOf(pkt.streamId));
		if (pes == null)
		{
			pes = (isMPEG(pkt.data) ? ((BaseTrack)new MPEGTrack(this, pkt.streamId, pkt)) : ((BaseTrack)((!isAAC(pkt.data)) ? new PlainTrack(this, pkt.streamId, pkt) : new AACTrack(this, pkt.streamId, pkt))));
			streams.put(Integer.valueOf(pkt.streamId), pes);
		}
		else
		{
			pes.pending(pkt);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 127, 5, 110, 100, 99, 232, 60,
		231, 70
	})]
	protected internal virtual void findStreams()
	{
		for (int i = 0; i == 0 || (i < 5 * streams.size() && streams.size() < 2); i++)
		{
			PESPacket nextPacket = this.nextPacket(getBuffer());
			if (nextPacket == null)
			{
				break;
			}
			addToStream(nextPacket);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 46, 162, 104, 131, 99, 102, 108, 112, 104,
		113, 131, 113, 123, 102, 106, 105, 101, 99, 101,
		113, 100, 99, 101, 102, 132, 133, 102
	})]
	private bool isMPEG(ByteBuffer _data)
	{
		ByteBuffer b = _data.duplicate();
		int marker = -1;
		int score = 0;
		int hasHeader = 0;
		int slicesStarted = 0;
		while (b.hasRemaining())
		{
			int code = (sbyte)b.get() & 0xFF;
			marker = (marker << 8) | code;
			if (marker < 256 || marker > 440)
			{
				continue;
			}
			if (marker >= 432 && marker <= 440)
			{
				if ((hasHeader != 0 && marker != 437 && marker != 434) || slicesStarted != 0)
				{
					break;
				}
				score += 5;
			}
			else if (marker == 256)
			{
				if (slicesStarted != 0)
				{
					break;
				}
				hasHeader = 1;
			}
			else if (marker > 256 && marker < 432)
			{
				if (hasHeader == 0)
				{
					break;
				}
				if (slicesStarted == 0)
				{
					score += 50;
					slicesStarted = 1;
				}
				score++;
			}
		}
		return score > 50;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 47, 130, 109 })]
	private bool isAAC(ByteBuffer _data)
	{
		ADTSParser.Header read = ADTSParser.read(_data.duplicate());
		return read != null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/NALUnit;>;)I")]
	[LineNumberTable(new byte[]
	{
		159, 17, 130, 99, 103, 127, 4, 111, 103, 136,
		102, 104, 111, 103, 102, 100, 102, 101, 125, 100,
		102, 131, 102
	})]
	private static int rateSeq(List nuSeq)
	{
		int score = 0;
		int hasSps = 0;
		int hasPps = 0;
		int hasSlice = 0;
		Iterator iterator = nuSeq.iterator();
		while (iterator.hasNext())
		{
			NALUnit nalUnit = (NALUnit)iterator.next();
			if (NALUnitType.___003C_003ESPS == nalUnit.type)
			{
				score = ((hasSps == 0 || hasSlice != 0) ? (score + 30) : (score + -30));
				hasSps = 1;
			}
			else if (NALUnitType.___003C_003EPPS == nalUnit.type)
			{
				if (hasPps != 0 && hasSlice == 0)
				{
					score += -30;
				}
				if (hasSps != 0)
				{
					score += 20;
				}
				hasPps = 1;
			}
			else if (NALUnitType.___003C_003EIDR_SLICE == nalUnit.type || NALUnitType.___003C_003ENON_IDR_SLICE == nalUnit.type)
			{
				if (hasSlice == 0)
				{
					score += 20;
				}
				hasSlice = 1;
			}
		}
		return score;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 63, 98, 127, 7, 108, 99 })]
	public virtual void reset()
	{
		Iterator iterator = streams.values().iterator();
		while (iterator.hasNext())
		{
			BaseTrack track = (BaseTrack)iterator.next();
			track._pending.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 37, 98, 104, 99, 131, 99, 100, 100, 100,
		104, 108, 112, 102, 133, 136, 101, 120, 106, 107,
		164, 104, 100, 102, 116, 166, 105, 104, 166, 100,
		134, 100, 153, 113, 105, 105, 137, 100, 134, 105,
		105, 113, 137, 103, 134, 105, 105, 113, 137, 103,
		131, 106, 135, 99, 110, 110, 113, 132, 101, 99,
		102
	})]
	public static int probe(ByteBuffer b_)
	{
		ByteBuffer b = b_.duplicate();
		int marker = -1;
		int sliceSize = 0;
		int videoPes = 0;
		int state = 0;
		int errors = 0;
		int inNALUnit = 0;
		ArrayList nuSeq = new ArrayList();
		while (b.hasRemaining())
		{
			int code = (sbyte)b.get() & 0xFF;
			if (state >= 3)
			{
				sliceSize++;
			}
			marker = (marker << 8) | code;
			if (inNALUnit != 0)
			{
				NALUnit nu = NALUnit.read(NIOUtils.asByteBufferInt(new int[1] { code }));
				if (nu.type != null)
				{
					((List)nuSeq).add((object)nu);
				}
				inNALUnit = 0;
			}
			if (videoPes != 0 && marker == 1)
			{
				inNALUnit = 1;
			}
			else
			{
				if (marker < 256 || marker > 511)
				{
					continue;
				}
				if (marker >= 442)
				{
					videoPes = (MPSUtils.videoMarker(marker) ? 1 : 0);
				}
				else
				{
					if (videoPes == 0)
					{
						continue;
					}
					int stop = 0;
					switch (state)
					{
					case 0:
						state = ((marker >= 432 && marker <= 440) ? 1 : ((marker == 256) ? 2 : 0));
						break;
					case 1:
						switch (marker)
						{
						case 256:
							state = 2;
							break;
						case 432:
						case 433:
						case 434:
						case 435:
						case 436:
						case 437:
						case 438:
						case 439:
						case 440:
							state = 1;
							break;
						default:
							errors++;
							break;
						}
						break;
					case 2:
						switch (marker)
						{
						case 257:
							state = 3;
							break;
						case 434:
						case 437:
							state = 2;
							break;
						default:
							errors++;
							break;
						}
						break;
					default:
						if (state > 3 && sliceSize < 1)
						{
							errors++;
						}
						sliceSize = 0;
						if (state - 1 == marker - 256)
						{
							state = marker - 256 + 2;
						}
						else if (marker == 256 || marker >= 432)
						{
							stop = 1;
						}
						break;
					}
					if (stop != 0)
					{
						break;
					}
				}
			}
		}
		int a = rateSeq(nuSeq);
		int b2;
		if (state >= 3)
		{
			int num = 1 + errors;
			b2 = ((num != -1) ? (100 / num) : (-100));
		}
		else
		{
			b2 = 0;
		}
		int result = java.lang.Math.max(a, b2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 10, 66, 110 })]
	public virtual void close()
	{
		channel.close();
	}

	public void Dispose()
	{
		close();
	}
}
