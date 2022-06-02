using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using org.jcodec.codecs.mpeg12;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.muxer;

namespace org.jcodec.containers.mps.index;

public class MTSRandomAccessDemuxerMain : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 104, 113, 125, 4, 199 })]
	private static MPSRandomAccessDemuxer.Stream getVideoStream(MPSRandomAccessDemuxer demuxer)
	{
		MPSRandomAccessDemuxer.Stream[] streams = demuxer.getStreams();
		MPSRandomAccessDemuxer.Stream[] array = streams;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MPSRandomAccessDemuxer.Stream stream = array[i];
			if (stream.getStreamId() >= 224 && stream.getStreamId() <= 239)
			{
				return stream;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(18)]
	public MTSRandomAccessDemuxerMain()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 103, 175, 127, 18, 137, 105, 104,
		143, 127, 11, 173, 111, 138, 147, 117, 143, 111,
		106, 116, 113, 106, 119, 125, 63, 8, 139, 234,
		61, 236, 69, 104, 106
	})]
	public static void main1(string[] args)
	{
		MTSIndexer indexer = new MTSIndexer();
		
		File source = new File(args[0]);
		
		File indexFile = new File(source.getParentFile(), new StringBuilder().append(source.getName()).append(".idx").toString());
		MTSIndex index;
		if (!indexFile.exists())
		{
			indexer.index(source, null);
			index = indexer.serialize();
			NIOUtils.writeTo(index.serialize(), indexFile);
		}
		else
		{
			java.lang.System.@out.println(new StringBuilder().append("Reading index from: ").append(indexFile.getName()).toString());
			index = MTSIndex.parse(NIOUtils.fetchFromFile(indexFile));
		}
		MTSRandomAccessDemuxer demuxer = new MTSRandomAccessDemuxer(NIOUtils.readableChannel(source), index);
		int[] guids = demuxer.getGuids();
		MPSRandomAccessDemuxer.Stream video = getVideoStream(demuxer.getProgramDemuxer(guids[0]));
		
		FileChannelWrapper ch = NIOUtils.writableChannel(new File(args[1]));
		MP4Muxer mp4Muxer = MP4Muxer.createMP4Muxer(ch, Brand.___003C_003EMOV);
		video.gotoSyncFrame(175L);
		Packet pkt = video.nextFrame();
		VideoCodecMeta meta = new MPEGDecoder().getCodecMeta(pkt.getData());
		MuxerTrack videoTrack = mp4Muxer.addVideoTrack(Codec.___003C_003EMPEG2, meta);
		long firstPts = pkt.getPts();
		int i = 0;
		while (pkt != null && i < 150)
		{
			videoTrack.addFrame(MP4Packet.createMP4Packet(pkt.getData(), pkt.getPts() - firstPts, pkt.getTimescale(), pkt.getDuration(), pkt.getFrameNo(), pkt.getFrameType(), pkt.getTapeTimecode(), 0, pkt.getPts() - firstPts, 0));
			pkt = video.nextFrame();
			i++;
		}
		mp4Muxer.finish();
		NIOUtils.closeQuietly(ch);
	}
}
