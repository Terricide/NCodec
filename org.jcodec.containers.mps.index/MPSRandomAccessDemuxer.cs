using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.api;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.platform;

namespace org.jcodec.containers.mps.index;

public class MPSRandomAccessDemuxer : Object
{
	[Implements(new string[] { "org.jcodec.common.SeekableDemuxerTrack" })]
	public class Stream : MPSIndex.MPSStreamIndex, SeekableDemuxerTrack, DemuxerTrack
	{
		private const int MPEG_TIMESCALE = 90000;

		private int curPesIdx;

		private int curFrame;

		private ByteBuffer pesBuf;

		private int _seekToFrame;

		protected internal SeekableByteChannel source;

		private long[] foffs;

		private MPSRandomAccessDemuxer demuxer;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159,
			100,
			66,
			106,
			98,
			141,
			111,
			132,
			135,
			104,
			123,
			121,
			102,
			99,
			134,
			byte.MaxValue,
			21,
			57,
			244,
			74,
			127,
			2,
			127,
			5,
			111,
			143,
			104
		})]
		private void seekToFrame()
		{
			if (_seekToFrame == -1)
			{
				return;
			}
			curFrame = _seekToFrame;
			long payloadOff = foffs[curFrame];
			long posShift = 0L;
			reset();
			curPesIdx = 0;
			while (true)
			{
				if (access_0024000(demuxer)[curPesIdx] == streamId)
				{
					int payloadSize = MPSIndex.payLoadSize(access_0024100(demuxer)[curPesIdx]);
					if (payloadOff < payloadSize)
					{
						break;
					}
					payloadOff -= payloadSize;
				}
				posShift += MPSIndex.pesLen(access_0024100(demuxer)[curPesIdx]) + MPSIndex.leadingSize(access_0024100(demuxer)[curPesIdx]);
				curPesIdx++;
			}
			skip(posShift + MPSIndex.leadingSize(access_0024100(demuxer)[curPesIdx]));
			pesBuf = fetch(MPSIndex.pesLen(access_0024100(demuxer)[curPesIdx]));
			MPSUtils.readPESHeader(pesBuf, 0L);
			NIOUtils.skip(pesBuf, (int)payloadOff);
			_seekToFrame = -1;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 119, 162, 135, 112, 131, 143, 104, 144, 108,
			110, 159, 12, 111, 100, 123, 127, 21, 145, 127,
			2, 121, 110, 111, 134, 136, 127, 32, 54, 168,
			143
		})]
		private Packet _nextFrame(ByteBuffer buf)
		{
			seekToFrame();
			if (curFrame >= (nint)fsizes.LongLength)
			{
				return null;
			}
			int fs = fsizes[curFrame];
			ByteBuffer result = buf.duplicate();
			result.limit(result.position() + fs);
			while (result.hasRemaining())
			{
				if (pesBuf.hasRemaining())
				{
					result.put(NIOUtils.read(pesBuf, Math.min(pesBuf.remaining(), result.remaining())));
					continue;
				}
				curPesIdx++;
				long posShift = 0L;
				while (access_0024000(demuxer)[curPesIdx] != streamId)
				{
					posShift += MPSIndex.pesLen(access_0024100(demuxer)[curPesIdx]) + MPSIndex.leadingSize(access_0024100(demuxer)[curPesIdx]);
					curPesIdx++;
				}
				skip(posShift + MPSIndex.leadingSize(access_0024100(demuxer)[curPesIdx]));
				int pesLen = MPSIndex.pesLen(access_0024100(demuxer)[curPesIdx]);
				pesBuf = fetch(pesLen);
				MPSUtils.readPESHeader(pesBuf, 0L);
			}
			result.flip();
			Packet pkt = Packet.createPacket(result, fpts[curFrame], 90000, fdur[curFrame], curFrame, (sync.Length != 0 && Arrays.binarySearch(sync, curFrame) < 0) ? Packet.FrameType.___003C_003EINTER : Packet.FrameType.___003C_003EKEY, null);
			curFrame++;
			return pkt;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 108, 66, 122 })]
		protected internal virtual void skip(long leadingSize)
		{
			source.setPosition(source.position() + leadingSize);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(132)]
		protected internal virtual ByteBuffer fetch(int pesLen)
		{
			ByteBuffer result = NIOUtils.fetchFromChannel(source, pesLen);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 107, 66, 111 })]
		protected internal virtual void reset()
		{
			source.setPosition(0L);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159,
			126,
			66,
			byte.MaxValue,
			8,
			58,
			232,
			71,
			104,
			136,
			115,
			100,
			109,
			106,
			13,
			231,
			69,
			111,
			135,
			104,
			105
		})]
		public Stream(MPSRandomAccessDemuxer demuxer, MPSIndex.MPSStreamIndex streamIndex, SeekableByteChannel source)
			: base(streamIndex.streamId, streamIndex.fsizes, streamIndex.fpts, streamIndex.fdur, streamIndex.sync)
		{
			_seekToFrame = -1;
			this.demuxer = demuxer;
			this.source = source;
			foffs = new long[(nint)fsizes.LongLength];
			long curOff = 0L;
			for (int i = 0; i < (nint)fsizes.LongLength; i++)
			{
				foffs[i] = curOff;
				curOff += fsizes[i];
			}
			int[] seg = Platform.copyOfInt(streamIndex.getFpts(), 100);
			Arrays.sort(seg);
			_seekToFrame = 0;
			seekToFrame();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 121, 66, 135, 112, 131, 111, 104 })]
		public virtual Packet nextFrame()
		{
			seekToFrame();
			if (curFrame >= (nint)fsizes.LongLength)
			{
				return null;
			}
			int fs = fsizes[curFrame];
			ByteBuffer result = ByteBuffer.allocate(fs);
			Packet result2 = _nextFrame(result);
			return result2;
		}

		[LineNumberTable(145)]
		public virtual DemuxerTrackMeta getMeta()
		{
			return null;
		}

		[LineNumberTable(new byte[] { 159, 105, 130, 137 })]
		public virtual bool gotoFrame(long frameNo)
		{
			_seekToFrame = (int)frameNo;
			return true;
		}

		[LineNumberTable(new byte[]
		{
			159, 103, 98, 109, 109, 113, 227, 61, 231, 70,
			119
		})]
		public virtual bool gotoSyncFrame(long frameNo)
		{
			for (int i = 0; i < (nint)sync.LongLength; i++)
			{
				if (sync[i] > frameNo)
				{
					_seekToFrame = sync[i - 1];
					return true;
				}
			}
			_seekToFrame = sync[(nint)sync.LongLength - 1];
			return true;
		}

		[LineNumberTable(197)]
		public virtual long getCurFrame()
		{
			return curFrame;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(202)]
		public virtual void seek(double second)
		{
			throw new NotSupportedException("");
		}
	}

	private Stream[] streams;

	private long[] pesTokens;

	private int[] pesStreamIds;

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(28)]
	internal static int[] access_0024000(MPSRandomAccessDemuxer x0)
	{
		return x0.pesStreamIds;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(28)]
	internal static long[] access_0024100(MPSRandomAccessDemuxer x0)
	{
		return x0.pesTokens;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(45)]
	protected internal virtual Stream newStream(SeekableByteChannel ch, MPSIndex.MPSStreamIndex streamIndex)
	{
		Stream result = new Stream(this, streamIndex, ch);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 105, 109, 114, 104, 110, 104, 51,
		167
	})]
	public MPSRandomAccessDemuxer(SeekableByteChannel ch, MPSIndex mpsIndex)
	{
		pesTokens = mpsIndex.getPesTokens();
		pesStreamIds = mpsIndex.getPesStreamIds().flattern();
		MPSIndex.MPSStreamIndex[] streamIndices = mpsIndex.getStreams();
		streams = new Stream[(nint)streamIndices.LongLength];
		for (int i = 0; i < (nint)streamIndices.LongLength; i++)
		{
			streams[i] = newStream(ch, streamIndices[i]);
		}
	}

	[LineNumberTable(49)]
	public virtual Stream[] getStreams()
	{
		return streams;
	}
}
