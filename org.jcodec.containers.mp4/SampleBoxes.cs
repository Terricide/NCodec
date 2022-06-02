using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4;

public class SampleBoxes : Boxes
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 139, 98, 105, 135, 113, 113, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 113, 113, 113,
		113, 145, 113, 113, 113, 113, 113, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 113, 113, 113,
		113, 113, 113, 127, 11, 127, 11, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 113, 113, 113,
		113, 113, 113, 113, 113, 113, 113, 145, 113, 145,
		113, 113, 177, 115
	})]
	public SampleBoxes()
	{
		clear();
		@override("ap4h", ClassLiteral<VideoSampleEntry>.Value);
		@override("apch", ClassLiteral<VideoSampleEntry>.Value);
		@override("apcn", ClassLiteral<VideoSampleEntry>.Value);
		@override("apcs", ClassLiteral<VideoSampleEntry>.Value);
		@override("apco", ClassLiteral<VideoSampleEntry>.Value);
		@override("avc1", ClassLiteral<VideoSampleEntry>.Value);
		@override("cvid", ClassLiteral<VideoSampleEntry>.Value);
		@override("jpeg", ClassLiteral<VideoSampleEntry>.Value);
		@override("smc ", ClassLiteral<VideoSampleEntry>.Value);
		@override("rle ", ClassLiteral<VideoSampleEntry>.Value);
		@override("rpza", ClassLiteral<VideoSampleEntry>.Value);
		@override("kpcd", ClassLiteral<VideoSampleEntry>.Value);
		@override("png ", ClassLiteral<VideoSampleEntry>.Value);
		@override("mjpa", ClassLiteral<VideoSampleEntry>.Value);
		@override("mjpb", ClassLiteral<VideoSampleEntry>.Value);
		@override("SVQ1", ClassLiteral<VideoSampleEntry>.Value);
		@override("SVQ3", ClassLiteral<VideoSampleEntry>.Value);
		@override("mp4v", ClassLiteral<VideoSampleEntry>.Value);
		@override("dvc ", ClassLiteral<VideoSampleEntry>.Value);
		@override("dvcp", ClassLiteral<VideoSampleEntry>.Value);
		@override("gif ", ClassLiteral<VideoSampleEntry>.Value);
		@override("h263", ClassLiteral<VideoSampleEntry>.Value);
		@override("tiff", ClassLiteral<VideoSampleEntry>.Value);
		@override("raw ", ClassLiteral<VideoSampleEntry>.Value);
		@override("2vuY", ClassLiteral<VideoSampleEntry>.Value);
		@override("yuv2", ClassLiteral<VideoSampleEntry>.Value);
		@override("v308", ClassLiteral<VideoSampleEntry>.Value);
		@override("v408", ClassLiteral<VideoSampleEntry>.Value);
		@override("v216", ClassLiteral<VideoSampleEntry>.Value);
		@override("v410", ClassLiteral<VideoSampleEntry>.Value);
		@override("v210", ClassLiteral<VideoSampleEntry>.Value);
		@override("m2v1", ClassLiteral<VideoSampleEntry>.Value);
		@override("m1v1", ClassLiteral<VideoSampleEntry>.Value);
		@override("xd5b", ClassLiteral<VideoSampleEntry>.Value);
		@override("dv5n", ClassLiteral<VideoSampleEntry>.Value);
		@override("jp2h", ClassLiteral<VideoSampleEntry>.Value);
		@override("mjp2", ClassLiteral<VideoSampleEntry>.Value);
		@override("ac-3", ClassLiteral<AudioSampleEntry>.Value);
		@override("cac3", ClassLiteral<AudioSampleEntry>.Value);
		@override("ima4", ClassLiteral<AudioSampleEntry>.Value);
		@override("aac ", ClassLiteral<AudioSampleEntry>.Value);
		@override("celp", ClassLiteral<AudioSampleEntry>.Value);
		@override("hvxc", ClassLiteral<AudioSampleEntry>.Value);
		@override("twvq", ClassLiteral<AudioSampleEntry>.Value);
		@override(".mp1", ClassLiteral<AudioSampleEntry>.Value);
		@override(".mp2", ClassLiteral<AudioSampleEntry>.Value);
		@override("midi", ClassLiteral<AudioSampleEntry>.Value);
		@override("apvs", ClassLiteral<AudioSampleEntry>.Value);
		@override("alac", ClassLiteral<AudioSampleEntry>.Value);
		@override("aach", ClassLiteral<AudioSampleEntry>.Value);
		@override("aacl", ClassLiteral<AudioSampleEntry>.Value);
		@override("aace", ClassLiteral<AudioSampleEntry>.Value);
		@override("aacf", ClassLiteral<AudioSampleEntry>.Value);
		@override("aacp", ClassLiteral<AudioSampleEntry>.Value);
		@override("aacs", ClassLiteral<AudioSampleEntry>.Value);
		@override("samr", ClassLiteral<AudioSampleEntry>.Value);
		@override("AUDB", ClassLiteral<AudioSampleEntry>.Value);
		@override("ilbc", ClassLiteral<AudioSampleEntry>.Value);
		@override(Platform.stringFromBytes(new byte[4] { 109, 115, 0, 17 }), ClassLiteral<AudioSampleEntry>.Value);
		@override(Platform.stringFromBytes(new byte[4] { 109, 115, 0, 49 }), ClassLiteral<AudioSampleEntry>.Value);
		@override("aes3", ClassLiteral<AudioSampleEntry>.Value);
		@override("NONE", ClassLiteral<AudioSampleEntry>.Value);
		@override("raw ", ClassLiteral<AudioSampleEntry>.Value);
		@override("twos", ClassLiteral<AudioSampleEntry>.Value);
		@override("sowt", ClassLiteral<AudioSampleEntry>.Value);
		@override("MAC3 ", ClassLiteral<AudioSampleEntry>.Value);
		@override("MAC6 ", ClassLiteral<AudioSampleEntry>.Value);
		@override("ima4", ClassLiteral<AudioSampleEntry>.Value);
		@override("fl32", ClassLiteral<AudioSampleEntry>.Value);
		@override("fl64", ClassLiteral<AudioSampleEntry>.Value);
		@override("in24", ClassLiteral<AudioSampleEntry>.Value);
		@override("in32", ClassLiteral<AudioSampleEntry>.Value);
		@override("ulaw", ClassLiteral<AudioSampleEntry>.Value);
		@override("alaw", ClassLiteral<AudioSampleEntry>.Value);
		@override("dvca", ClassLiteral<AudioSampleEntry>.Value);
		@override("QDMC", ClassLiteral<AudioSampleEntry>.Value);
		@override("QDM2", ClassLiteral<AudioSampleEntry>.Value);
		@override("Qclp", ClassLiteral<AudioSampleEntry>.Value);
		@override(".mp3", ClassLiteral<AudioSampleEntry>.Value);
		@override("mp4a", ClassLiteral<AudioSampleEntry>.Value);
		@override("lpcm", ClassLiteral<AudioSampleEntry>.Value);
		@override("tmcd", ClassLiteral<TimecodeSampleEntry>.Value);
		@override("time", ClassLiteral<TimecodeSampleEntry>.Value);
		@override("c608", ClassLiteral<SampleEntry>.Value);
		@override("c708", ClassLiteral<SampleEntry>.Value);
		@override("text", ClassLiteral<SampleEntry>.Value);
		@override("fdsc", ClassLiteral<SampleEntry>.Value);
	}
}
