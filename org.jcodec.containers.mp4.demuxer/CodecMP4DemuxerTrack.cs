using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.mp4;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.demuxer;

public class CodecMP4DemuxerTrack : MP4DemuxerTrack
{
	private ByteBuffer codecPrivate;

	private AvcCBox avcC;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 108, 115, 153, 109 })]
	public CodecMP4DemuxerTrack(MovieBox mov, TrakBox trak, SeekableByteChannel input)
		: base(mov, trak, input)
	{
		if (Codec.codecByFourcc(getFourcc()) == Codec.___003C_003EH264)
		{
			avcC = H264Utils.parseAVCC((VideoSampleEntry)getSampleEntries()[0]);
		}
		codecPrivate = MP4DemuxerTrackMeta.getCodecPrivate(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 108, 115, 110, 105, 159, 2, 99,
		147, 117, 104, 105, 188
	})]
	public override ByteBuffer convertPacket(ByteBuffer result)
	{
		if (codecPrivate != null)
		{
			if (Codec.codecByFourcc(getFourcc()) == Codec.___003C_003EH264)
			{
				ByteBuffer annexbCoded = H264Utils.decodeMOVPacket(result, avcC);
				if (H264Utils.isByteBufferIDRSlice(annexbCoded))
				{
					ByteBuffer result2 = NIOUtils.combineBuffers(Arrays.asList(codecPrivate, annexbCoded));
					
					return result2;
				}
				return annexbCoded;
			}
			if (Codec.codecByFourcc(getFourcc()) == Codec.___003C_003EAAC)
			{
				ADTSParser.Header adts = AACUtils.streamInfoToADTS(codecPrivate, crcAbsent: true, 1, result.remaining());
				ByteBuffer adtsRaw = ByteBuffer.allocate(7);
				ADTSParser.write(adts, adtsRaw);
				ByteBuffer result3 = NIOUtils.combineBuffers(Arrays.asList(adtsRaw, result));
				
				return result3;
			}
		}
		return result;
	}
}
