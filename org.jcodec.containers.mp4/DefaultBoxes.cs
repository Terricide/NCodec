using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class DefaultBoxes : Boxes
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 105, 119, 119, 119, 119, 119, 119,
		119, 119, 119, 119, 119, 119, 119, 119, 119, 119,
		119, 119, 119, 119, 119, 119, 119, 119, 119, 119,
		119, 119, 119, 119, 119, 119, 119, 119, 119, 119,
		119, 119, 119, 119, 119, 119, 119, 119, 119, 119,
		119, 119, 119, 119, 119, 119, 119, 119, 151, 119,
		119, 119, 119, 119, 119
	})]
	public DefaultBoxes()
	{
		___003C_003Emappings.put(MovieExtendsBox.fourcc(), ClassLiteral<MovieExtendsBox>.Value);
		___003C_003Emappings.put(MovieExtendsHeaderBox.fourcc(), ClassLiteral<MovieExtendsHeaderBox>.Value);
		___003C_003Emappings.put(SegmentIndexBox.fourcc(), ClassLiteral<SegmentIndexBox>.Value);
		___003C_003Emappings.put(SegmentTypeBox.fourcc(), ClassLiteral<SegmentTypeBox>.Value);
		___003C_003Emappings.put(TrackExtendsBox.fourcc(), ClassLiteral<TrackExtendsBox>.Value);
		___003C_003Emappings.put(VideoMediaHeaderBox.fourcc(), ClassLiteral<VideoMediaHeaderBox>.Value);
		___003C_003Emappings.put(FileTypeBox.fourcc(), ClassLiteral<FileTypeBox>.Value);
		___003C_003Emappings.put(MovieBox.fourcc(), ClassLiteral<MovieBox>.Value);
		___003C_003Emappings.put(MovieHeaderBox.fourcc(), ClassLiteral<MovieHeaderBox>.Value);
		___003C_003Emappings.put(TrakBox.fourcc(), ClassLiteral<TrakBox>.Value);
		___003C_003Emappings.put(TrackHeaderBox.fourcc(), ClassLiteral<TrackHeaderBox>.Value);
		___003C_003Emappings.put("edts", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put(EditListBox.fourcc(), ClassLiteral<EditListBox>.Value);
		___003C_003Emappings.put(MediaBox.fourcc(), ClassLiteral<MediaBox>.Value);
		___003C_003Emappings.put(MediaHeaderBox.fourcc(), ClassLiteral<MediaHeaderBox>.Value);
		___003C_003Emappings.put(MediaInfoBox.fourcc(), ClassLiteral<MediaInfoBox>.Value);
		___003C_003Emappings.put(HandlerBox.fourcc(), ClassLiteral<HandlerBox>.Value);
		___003C_003Emappings.put(DataInfoBox.fourcc(), ClassLiteral<DataInfoBox>.Value);
		___003C_003Emappings.put("stbl", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put(SampleDescriptionBox.fourcc(), ClassLiteral<SampleDescriptionBox>.Value);
		___003C_003Emappings.put(TimeToSampleBox.fourcc(), ClassLiteral<TimeToSampleBox>.Value);
		___003C_003Emappings.put("stss", ClassLiteral<SyncSamplesBox>.Value);
		___003C_003Emappings.put("stps", ClassLiteral<PartialSyncSamplesBox>.Value);
		___003C_003Emappings.put(SampleToChunkBox.fourcc(), ClassLiteral<SampleToChunkBox>.Value);
		___003C_003Emappings.put(SampleSizesBox.fourcc(), ClassLiteral<SampleSizesBox>.Value);
		___003C_003Emappings.put(ChunkOffsetsBox.fourcc(), ClassLiteral<ChunkOffsetsBox>.Value);
		___003C_003Emappings.put("keys", ClassLiteral<KeysBox>.Value);
		___003C_003Emappings.put(IListBox.fourcc(), ClassLiteral<IListBox>.Value);
		___003C_003Emappings.put("mvex", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("moof", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("traf", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("mfra", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("skip", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put(MetaBox.fourcc(), ClassLiteral<MetaBox>.Value);
		___003C_003Emappings.put(DataRefBox.fourcc(), ClassLiteral<DataRefBox>.Value);
		___003C_003Emappings.put("ipro", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("sinf", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put(ChunkOffsets64Box.fourcc(), ClassLiteral<ChunkOffsets64Box>.Value);
		___003C_003Emappings.put(SoundMediaHeaderBox.fourcc(), ClassLiteral<SoundMediaHeaderBox>.Value);
		___003C_003Emappings.put("clip", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put(ClipRegionBox.fourcc(), ClassLiteral<ClipRegionBox>.Value);
		___003C_003Emappings.put(LoadSettingsBox.fourcc(), ClassLiteral<LoadSettingsBox>.Value);
		___003C_003Emappings.put("tapt", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("gmhd", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("tmcd", ClassLiteral<Box.LeafBox>.Value);
		___003C_003Emappings.put("tref", ClassLiteral<NodeBox>.Value);
		___003C_003Emappings.put("clef", ClassLiteral<ClearApertureBox>.Value);
		___003C_003Emappings.put("prof", ClassLiteral<ProductionApertureBox>.Value);
		___003C_003Emappings.put("enof", ClassLiteral<EncodedPixelBox>.Value);
		___003C_003Emappings.put(GenericMediaInfoBox.fourcc(), ClassLiteral<GenericMediaInfoBox>.Value);
		___003C_003Emappings.put(TimecodeMediaInfoBox.fourcc(), ClassLiteral<TimecodeMediaInfoBox>.Value);
		___003C_003Emappings.put(UdtaBox.fourcc(), ClassLiteral<UdtaBox>.Value);
		___003C_003Emappings.put(CompositionOffsetsBox.fourcc(), ClassLiteral<CompositionOffsetsBox>.Value);
		___003C_003Emappings.put(NameBox.fourcc(), ClassLiteral<NameBox>.Value);
		___003C_003Emappings.put("mdta", ClassLiteral<Box.LeafBox>.Value);
		___003C_003Emappings.put(MovieFragmentHeaderBox.fourcc(), ClassLiteral<MovieFragmentHeaderBox>.Value);
		___003C_003Emappings.put(TrackFragmentHeaderBox.fourcc(), ClassLiteral<TrackFragmentHeaderBox>.Value);
		___003C_003Emappings.put(MovieFragmentBox.fourcc(), ClassLiteral<MovieFragmentBox>.Value);
		___003C_003Emappings.put(TrackFragmentBox.fourcc(), ClassLiteral<TrackFragmentBox>.Value);
		___003C_003Emappings.put(TrackFragmentBaseMediaDecodeTimeBox.fourcc(), ClassLiteral<TrackFragmentBaseMediaDecodeTimeBox>.Value);
		___003C_003Emappings.put(TrunBox.fourcc(), ClassLiteral<TrunBox>.Value);
	}
}
