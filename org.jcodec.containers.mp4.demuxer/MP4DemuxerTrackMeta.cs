using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.h264.mp4;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.demuxer;

public class MP4DemuxerTrackMeta : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 66, 104, 124, 174, 132, 110, 106, 39,
		203, 106, 106, 52, 201, 105, 127, 9, 100, 100,
		106, 116, 127, 0, 101, 111, 108, 112, 143, 105,
		117, 110, 116, 145, 109, 189, 106, 106, 106, 106,
		106, 138, 136, 170
	})]
	public static DemuxerTrackMeta fromTrack(AbstractMP4DemuxerTrack track)
	{
		TrakBox trak = track.getBox();
		int[] syncSamples = ((SyncSamplesBox)NodeBox.findFirstPath(trak, ClassLiteral<SyncSamplesBox>.Value, Box.path("mdia.minf.stbl.stss")))?.getSyncSamples();
		int[] seekFrames;
		if (syncSamples == null)
		{
			seekFrames = new int[(int)track.getFrameCount()];
			for (int j = 0; j < (nint)seekFrames.LongLength; j++)
			{
				seekFrames[j] = j;
			}
		}
		else
		{
			seekFrames = Platform.copyOfInt(syncSamples, syncSamples.Length);
			for (int i = 0; i < (nint)seekFrames.LongLength; i++)
			{
				int[] array = seekFrames;
				int num = i;
				int[] array2 = array;
				array2[num]--;
			}
		}
		MP4TrackType type = track.getType();
		TrackType t = ((type == MP4TrackType.___003C_003EVIDEO) ? TrackType.___003C_003EVIDEO : ((type != MP4TrackType.___003C_003ESOUND) ? TrackType.___003C_003EOTHER : TrackType.___003C_003EAUDIO));
		VideoCodecMeta videoCodecMeta = null;
		AudioCodecMeta audioCodecMeta = null;
		if (type == MP4TrackType.___003C_003EVIDEO)
		{
			videoCodecMeta = VideoCodecMeta.createSimpleVideoCodecMeta(trak.getCodedSize(), getColorInfo(track));
			PixelAspectExt pasp = (PixelAspectExt)NodeBox.findFirst(track.getSampleEntries()[0], ClassLiteral<PixelAspectExt>.Value, "pasp");
			if (pasp != null)
			{
				videoCodecMeta.setPixelAspectRatio(pasp.getRational());
			}
		}
		else if (type == MP4TrackType.___003C_003ESOUND)
		{
			AudioSampleEntry ase = (AudioSampleEntry)track.getSampleEntries()[0];
			audioCodecMeta = AudioCodecMeta.fromAudioFormat(ase.getFormat());
		}
		RationalLarge duration = track.getDuration();
		double sec = (double)duration.getNum() / (double)duration.getDen();
		int frameCount = Ints.checkedCast(track.getFrameCount());
		DemuxerTrackMeta meta = new DemuxerTrackMeta(t, Codec.codecByFourcc(track.getFourcc()), sec, seekFrames, frameCount, getCodecPrivate(track), videoCodecMeta, audioCodecMeta);
		if (type == MP4TrackType.___003C_003EVIDEO)
		{
			TrackHeaderBox tkhd = (TrackHeaderBox)NodeBox.findFirstPath(trak, ClassLiteral<TrackHeaderBox>.Value, Box.path("tkhd"));
			DemuxerTrackMeta.Orientation orientation = (tkhd.isOrientation90() ? DemuxerTrackMeta.Orientation.___003C_003ED_90 : (tkhd.isOrientation180() ? DemuxerTrackMeta.Orientation.___003C_003ED_180 : ((!tkhd.isOrientation270()) ? DemuxerTrackMeta.Orientation.___003C_003ED_0 : DemuxerTrackMeta.Orientation.___003C_003ED_270)));
			meta.setOrientation(orientation);
		}
		return meta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 98, 109, 105, 116, 138, 105, 177 })]
	public static ByteBuffer getCodecPrivate(AbstractMP4DemuxerTrack track)
	{
		Codec codec = Codec.codecByFourcc(track.getFourcc());
		if (codec == Codec.___003C_003EH264)
		{
			AvcCBox avcC = H264Utils.parseAVCC((VideoSampleEntry)track.getSampleEntries()[0]);
			ByteBuffer result = H264Utils.avcCToAnnexB(avcC);
			
			return result;
		}
		if (codec == Codec.___003C_003EAAC)
		{
			ByteBuffer codecPrivate = AACUtils.getCodecPrivate(track.getSampleEntries()[0]);
			
			return codecPrivate;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 66, 109, 105, 116, 104, 106, 120, 170 })]
	protected internal static ColorSpace getColorInfo(AbstractMP4DemuxerTrack track)
	{
		Codec codec = Codec.codecByFourcc(track.getFourcc());
		if (codec == Codec.___003C_003EH264)
		{
			AvcCBox avcC = H264Utils.parseAVCC((VideoSampleEntry)track.getSampleEntries()[0]);
			List spsList = avcC.getSpsList();
			if (spsList.size() > 0)
			{
				SeqParameterSet sps = SeqParameterSet.read(((ByteBuffer)spsList.get(0)).duplicate());
				ColorSpace chromaFormatIdc = sps.getChromaFormatIdc();
				
				return chromaFormatIdc;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(34)]
	public MP4DemuxerTrackMeta()
	{
	}
}
