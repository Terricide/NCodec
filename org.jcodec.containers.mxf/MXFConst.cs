using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.containers.mxf.model;

namespace org.jcodec.containers.mxf;

public class MXFConst : Object
{
	public class KLVFill : MXFMetadata
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 117, 98, 106 })]
		public KLVFill(UL ul)
			: base(ul)
		{
		}

		[LineNumberTable(105)]
		public override void readBuf(ByteBuffer bb)
		{
		}
	}

	internal static UL ___003C_003EHEADER_PARTITION_KLV;

	internal static UL ___003C_003EINDEX_KLV;

	internal static UL ___003C_003EGENERIC_DESCRIPTOR_KLV;

	[Signature("Ljava/util/Map<Lorg/jcodec/containers/mxf/model/UL;Ljava/lang/Class<+Lorg/jcodec/containers/mxf/model/MXFMetadata;>;>;")]
	public static Map klMetadata;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static UL HEADER_PARTITION_KLV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHEADER_PARTITION_KLV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static UL INDEX_KLV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EINDEX_KLV;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static UL GENERIC_DESCRIPTOR_KLV
	{
		[HideFromJava]
		get
		{
			return ___003C_003EGENERIC_DESCRIPTOR_KLV;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(42)]
	public MXFConst()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 131, 66, 144, 144, 144, 171, 123, 123, 123,
		123, 123, 123, 123, 123, 123, 123, 155, 123, 123,
		123, 123, 123, 123, 123, 123, 123, 123, 123, 123,
		155, 123, 123, 123, 123, 123, 123, 123, 123, 123,
		123, 155, 155, 155, 155
	})]
	static MXFConst()
	{
		___003C_003EHEADER_PARTITION_KLV = UL.newUL("06.0e.2b.34.02.05.01.01.0d.01.02.01.01.02");
		___003C_003EINDEX_KLV = UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.02.01.01.10.01.00");
		___003C_003EGENERIC_DESCRIPTOR_KLV = UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01");
		klMetadata = new HashMap();
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.18.00"), ClassLiteral<ContentStorage>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.37.00"), ClassLiteral<SourcePackage>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.0F.00"), ClassLiteral<Sequence>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0D.01.01.01.01.01.2F.00"), ClassLiteral<Preface>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.30.00"), ClassLiteral<Identification>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.11.00"), ClassLiteral<SourceClip>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.23.00"), ClassLiteral<EssenceContainerData>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.3A.00"), ClassLiteral<TimelineTrack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.3B.00"), ClassLiteral<TimelineTrack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.36.00"), ClassLiteral<MaterialPackage>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.02.01.01.10.01.00"), ClassLiteral<IndexSegment>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.44.00"), ClassLiteral<GenericDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.5b.00"), ClassLiteral<GenericDataEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.5b.00"), ClassLiteral<GenericDataEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.5c.00"), ClassLiteral<GenericDataEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.43.00"), ClassLiteral<GenericDataEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.42.00"), ClassLiteral<GenericSoundEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.28.00"), ClassLiteral<CDCIEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.29.00"), ClassLiteral<RGBAEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.51.00"), ClassLiteral<MPEG2VideoDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.48.00"), ClassLiteral<WaveAudioDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.25.00"), ClassLiteral<FileDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.27.00"), ClassLiteral<GenericPictureEssenceDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0d.01.01.01.01.01.47.00"), ClassLiteral<AES3PCMDescriptor>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.05.01.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.02.01.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.02.02.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.02.03.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.02.04.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.03.01.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.03.02.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.03.03.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.03.04.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.04.02.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.05.01.01.0d.01.02.01.01.04.04.00"), ClassLiteral<MXFPartitionPack>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.02.53.01.01.0D.01.01.01.01.01.14.00"), ClassLiteral<TimecodeComponent>.Value);
		klMetadata.put(UL.newUL("06.0E.2B.34.01.01.01.02.03.01.02.10.01.00.00.00"), ClassLiteral<KLVFill>.Value);
		klMetadata.put(UL.newUL("06.0e.2b.34.02.53.01.01.0d.01.01.01.01.01.5a.00"), ClassLiteral<J2KPictureDescriptor>.Value);
	}
}
