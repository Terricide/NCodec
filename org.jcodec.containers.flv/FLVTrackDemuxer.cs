using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.containers.flv;

public class FLVTrackDemuxer : Object
{
	[Implements(new string[] { "org.jcodec.common.SeekableDemuxerTrack" })]
	public class FLVDemuxerTrack : Object, SeekableDemuxerTrack, DemuxerTrack
	{
		private FLVTag.Type type;

		private int curFrame;

		private Codec codec;

		private LongArrayList framePositions;

		private byte[] codecPrivate;

		private FLVTrackDemuxer demuxer;

		[LineNumberTable(77)]
		private Packet toPacket(FLVTag frame)
		{
			return null;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 130, 98, 105, 108, 104, 104, 106, 114 })]
		public FLVDemuxerTrack(FLVTrackDemuxer demuxer, FLVTag.Type type)
		{
			framePositions = LongArrayList.createLongArrayList();
			this.demuxer = demuxer;
			this.type = type;
			FLVTag frame = access_0024000(demuxer, type, x2: false);
			codec = frame.getTagHeader().getCodec();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 128, 162, 116, 114 })]
		public virtual Packet nextFrame()
		{
			FLVTag frame = access_0024000(demuxer, type, x2: true);
			framePositions.add(frame.getPosition());
			Packet result = toPacket(frame);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 126, 98, 148 })]
		public virtual Packet prevFrame()
		{
			FLVTag frame = access_0024100(demuxer, type, x2: true);
			Packet result = toPacket(frame);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 125, 162, 148 })]
		public virtual Packet pickFrame()
		{
			FLVTag frame = access_0024000(demuxer, type, x2: false);
			Packet result = toPacket(frame);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 122, 130, 123 })]
		public virtual DemuxerTrackMeta getMeta()
		{
			TrackType t = ((type != FLVTag.Type.___003C_003EVIDEO) ? TrackType.___003C_003EAUDIO : TrackType.___003C_003EVIDEO);
			DemuxerTrackMeta result = new DemuxerTrackMeta(t, codec, 0.0, null, 0, ByteBuffer.wrap(codecPrivate), null, null);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 120, 66, 112, 99, 121 })]
		public virtual bool gotoFrame(long i)
		{
			if (i >= framePositions.size())
			{
				return false;
			}
			access_0024200(demuxer, framePositions.get((int)i));
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(96)]
		public virtual bool gotoSyncFrame(long i)
		{
			
			throw new RuntimeException();
		}

		[LineNumberTable(101)]
		public virtual long getCurFrame()
		{
			return curFrame;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 116, 130, 112 })]
		public virtual void seek(double second)
		{
			access_0024300(demuxer, second);
		}
	}

	private const int MAX_CRAWL_DISTANCE_SEC = 10;

	private FLVReader demuxer;

	private FLVDemuxerTrack video;

	private FLVDemuxerTrack audio;

	[Signature("Ljava/util/LinkedList<Lorg/jcodec/containers/flv/FLVTag;>;")]
	private LinkedList packets;

	private SeekableByteChannel _in;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 135, 65, 67 })]
	internal static FLVTag access_0024000(FLVTrackDemuxer x0, FLVTag.Type x1, bool x2)
	{
		FLVTag result = x0.nextFrameI(x1, x2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 135, 65, 67 })]
	internal static FLVTag access_0024100(FLVTrackDemuxer x0, FLVTag.Type x1, bool x2)
	{
		FLVTag result = x0.prevFrameI(x1, x2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(28)]
	internal static void access_0024200(FLVTrackDemuxer x0, long x1)
	{
		x0.resetToPosition(x1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(28)]
	internal static void access_0024300(FLVTrackDemuxer x0, double x1)
	{
		x0.seekI(x1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 111, 130, 140, 125, 131, 132, 162, 122, 108,
		141, 127, 4, 154, 110, 172, 108, 110, 110, 154,
		159, 7, 124, 99, 101, 111, 98, 151, 127, 8,
		127, 6, 236, 48, 236, 83
	})]
	private void seekI(double second)
	{
		packets.clear();
		FLVTag @base;
		while ((@base = demuxer.readNextPacket()) != null && @base.getPtsD() == 0.0)
		{
		}
		if (@base == null)
		{
			return;
		}
		_in.setPosition(@base.getPosition() + 1048576u);
		demuxer.reposition();
		FLVTag off = demuxer.readNextPacket();
		int byteRate = ByteCodeHelper.d2i((double)(off.getPosition() - @base.getPosition()) / (off.getPtsD() - @base.getPtsD()));
		long offset = @base.getPosition() + ByteCodeHelper.d2l((second - @base.getPtsD()) * (double)byteRate);
		_in.setPosition(offset);
		demuxer.reposition();
		for (int i = 0; i < 5; i++)
		{
			FLVTag pkt = demuxer.readNextPacket();
			double distance = second - pkt.getPtsD();
			if (distance > 0.0 && distance < 10.0)
			{
				java.lang.System.@out.println(new StringBuilder().append("Crawling forward: ").append(distance).toString());
				FLVTag testPkt;
				while ((testPkt = demuxer.readNextPacket()) != null && testPkt.getPtsD() < second)
				{
				}
				if (testPkt != null)
				{
					packets.add(pkt);
				}
				break;
			}
			if (distance < 0.0 && distance > -10.0)
			{
				java.lang.System.@out.println(new StringBuilder().append("Overshoot by: ").append(0.0 - distance).toString());
				_in.setPosition(pkt.getPosition() + ByteCodeHelper.d2l((distance - 1.0) * (double)byteRate));
				demuxer.reposition();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 112, 66, 110, 108, 110 })]
	private void resetToPosition(long position)
	{
		_in.setPosition(position);
		demuxer.reset();
		packets.clear();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 96, 161, 67, 117, 109, 106, 100, 103, 131,
		131, 121, 112, 100, 142
	})]
	private FLVTag prevFrameI(FLVTag.Type type, bool remove)
	{
		ListIterator it = packets.listIterator();
		while (it.hasPrevious())
		{
			FLVTag pkt2 = (FLVTag)it.previous();
			if (pkt2.getType() == type)
			{
				if (remove)
				{
					it.remove();
				}
				return pkt2;
			}
		}
		FLVTag pkt;
		while ((pkt = demuxer.readPrevPacket()) != null && pkt.getType() != type)
		{
			packets.add(0, pkt);
		}
		if (!remove)
		{
			packets.add(0, pkt);
		}
		return pkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 100, 97, 67, 117, 109, 106, 100, 103, 131,
		131, 121, 112, 100, 142
	})]
	private FLVTag nextFrameI(FLVTag.Type type, bool remove)
	{
		Iterator it = packets.iterator();
		while (it.hasNext())
		{
			FLVTag pkt2 = (FLVTag)it.next();
			if (pkt2.getType() == type)
			{
				if (remove)
				{
					it.remove();
				}
				return pkt2;
			}
		}
		FLVTag pkt;
		while ((pkt = demuxer.readNextPacket()) != null && pkt.getType() != type)
		{
			packets.add(pkt);
		}
		if (!remove)
		{
			packets.add(pkt);
		}
		return pkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 115, 130, 105, 108, 104, 106, 109, 114, 114 })]
	public FLVTrackDemuxer(SeekableByteChannel _in)
	{
		packets = new LinkedList();
		this._in = _in;
		_in.setPosition(0L);
		demuxer = new FLVReader(_in);
		video = new FLVDemuxerTrack(this, FLVTag.Type.___003C_003EVIDEO);
		audio = new FLVDemuxerTrack(this, FLVTag.Type.___003C_003EAUDIO);
	}

	[LineNumberTable(206)]
	public virtual DemuxerTrack[] getTracks()
	{
		return new DemuxerTrack[2] { video, audio };
	}

	[LineNumberTable(210)]
	public virtual DemuxerTrack getVideoTrack()
	{
		return video;
	}

	[LineNumberTable(214)]
	public virtual DemuxerTrack getAudioTrack()
	{
		return video;
	}
}
