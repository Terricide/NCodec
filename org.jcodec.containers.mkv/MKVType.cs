using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.lang.reflect;
using java.util;
using org.jcodec.containers.mkv.boxes;
using org.jcodec.containers.mkv.util;
using org.jcodec.platform;

namespace org.jcodec.containers.mkv;

public class MKVType : java.lang.Object
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/MKVType;>;")]
	private static List _values;

	internal static MKVType ___003C_003EVoid;

	internal static MKVType ___003C_003ECRC32;

	internal static MKVType ___003C_003EEBML;

	internal static MKVType ___003C_003EEBMLVersion;

	internal static MKVType ___003C_003EEBMLReadVersion;

	internal static MKVType ___003C_003EEBMLMaxIDLength;

	internal static MKVType ___003C_003EEBMLMaxSizeLength;

	internal static MKVType ___003C_003EDocType;

	internal static MKVType ___003C_003EDocTypeVersion;

	internal static MKVType ___003C_003EDocTypeReadVersion;

	internal static MKVType ___003C_003ESegment;

	internal static MKVType ___003C_003ESeekHead;

	internal static MKVType ___003C_003ESeek;

	internal static MKVType ___003C_003ESeekID;

	internal static MKVType ___003C_003ESeekPosition;

	internal static MKVType ___003C_003EInfo;

	internal static MKVType ___003C_003ESegmentUID;

	internal static MKVType ___003C_003ESegmentFilename;

	internal static MKVType ___003C_003EPrevUID;

	internal static MKVType ___003C_003EPrevFilename;

	internal static MKVType ___003C_003ENextUID;

	internal static MKVType ___003C_003ENextFilenam;

	internal static MKVType ___003C_003ESegmentFamily;

	internal static MKVType ___003C_003EChapterTranslate;

	internal static MKVType ___003C_003EChapterTranslateEditionUID;

	internal static MKVType ___003C_003EChapterTranslateCodec;

	internal static MKVType ___003C_003EChapterTranslateID;

	internal static MKVType ___003C_003ETimecodeScale;

	internal static MKVType ___003C_003EDuration;

	internal static MKVType ___003C_003EDateUTC;

	internal static MKVType ___003C_003ETitle;

	internal static MKVType ___003C_003EMuxingApp;

	internal static MKVType ___003C_003EWritingApp;

	internal static MKVType ___003C_003ECluster;

	internal static MKVType ___003C_003ETimecode;

	internal static MKVType ___003C_003ESilentTracks;

	internal static MKVType ___003C_003ESilentTrackNumber;

	internal static MKVType ___003C_003EPosition;

	internal static MKVType ___003C_003EPrevSize;

	internal static MKVType ___003C_003ESimpleBlock;

	internal static MKVType ___003C_003EBlockGroup;

	internal static MKVType ___003C_003EBlock;

	internal static MKVType ___003C_003EBlockAdditions;

	internal static MKVType ___003C_003EBlockMore;

	internal static MKVType ___003C_003EBlockAddID;

	internal static MKVType ___003C_003EBlockAdditional;

	internal static MKVType ___003C_003EBlockDuration;

	internal static MKVType ___003C_003EReferencePriority;

	internal static MKVType ___003C_003EReferenceBlock;

	internal static MKVType ___003C_003ECodecState;

	internal static MKVType ___003C_003ESlices;

	internal static MKVType ___003C_003ETimeSlice;

	internal static MKVType ___003C_003ELaceNumber;

	internal static MKVType ___003C_003ETracks;

	internal static MKVType ___003C_003ETrackEntry;

	internal static MKVType ___003C_003ETrackNumber;

	internal static MKVType ___003C_003ETrackUID;

	internal static MKVType ___003C_003ETrackType;

	internal static MKVType ___003C_003EFlagEnabled;

	internal static MKVType ___003C_003EFlagDefault;

	internal static MKVType ___003C_003EFlagForced;

	internal static MKVType ___003C_003EFlagLacing;

	internal static MKVType ___003C_003EMinCache;

	internal static MKVType ___003C_003EMaxCache;

	internal static MKVType ___003C_003EDefaultDuration;

	internal static MKVType ___003C_003EMaxBlockAdditionID;

	internal static MKVType ___003C_003EName;

	internal static MKVType ___003C_003ELanguage;

	internal static MKVType ___003C_003ECodecID;

	internal static MKVType ___003C_003ECodecPrivate;

	internal static MKVType ___003C_003ECodecName;

	internal static MKVType ___003C_003EAttachmentLink;

	internal static MKVType ___003C_003ECodecDecodeAll;

	internal static MKVType ___003C_003ETrackOverlay;

	internal static MKVType ___003C_003ETrackTranslate;

	internal static MKVType ___003C_003ETrackTranslateEditionUID;

	internal static MKVType ___003C_003ETrackTranslateCodec;

	internal static MKVType ___003C_003ETrackTranslateTrackID;

	internal static MKVType ___003C_003EVideo;

	internal static MKVType ___003C_003EFlagInterlaced;

	internal static MKVType ___003C_003EStereoMode;

	internal static MKVType ___003C_003EAlphaMode;

	internal static MKVType ___003C_003EPixelWidth;

	internal static MKVType ___003C_003EPixelHeight;

	internal static MKVType ___003C_003EPixelCropBottom;

	internal static MKVType ___003C_003EPixelCropTop;

	internal static MKVType ___003C_003EPixelCropLeft;

	internal static MKVType ___003C_003EPixelCropRight;

	internal static MKVType ___003C_003EDisplayWidth;

	internal static MKVType ___003C_003EDisplayHeight;

	internal static MKVType ___003C_003EDisplayUnit;

	internal static MKVType ___003C_003EAspectRatioType;

	internal static MKVType ___003C_003EColourSpace;

	internal static MKVType ___003C_003EAudio;

	internal static MKVType ___003C_003ESamplingFrequency;

	internal static MKVType ___003C_003EOutputSamplingFrequency;

	internal static MKVType ___003C_003EChannels;

	internal static MKVType ___003C_003EBitDepth;

	internal static MKVType ___003C_003ETrackOperation;

	internal static MKVType ___003C_003ETrackCombinePlanes;

	internal static MKVType ___003C_003ETrackPlane;

	internal static MKVType ___003C_003ETrackPlaneUID;

	internal static MKVType ___003C_003ETrackPlaneType;

	internal static MKVType ___003C_003ETrackJoinBlocks;

	internal static MKVType ___003C_003ETrackJoinUID;

	internal static MKVType ___003C_003EContentEncodings;

	internal static MKVType ___003C_003EContentEncoding;

	internal static MKVType ___003C_003EContentEncodingOrder;

	internal static MKVType ___003C_003EContentEncodingScope;

	internal static MKVType ___003C_003EContentEncodingType;

	internal static MKVType ___003C_003EContentCompression;

	internal static MKVType ___003C_003EContentCompAlgo;

	internal static MKVType ___003C_003EContentCompSettings;

	internal static MKVType ___003C_003EContentEncryption;

	internal static MKVType ___003C_003EContentEncAlgo;

	internal static MKVType ___003C_003EContentEncKeyID;

	internal static MKVType ___003C_003EContentSignature;

	internal static MKVType ___003C_003EContentSigKeyID;

	internal static MKVType ___003C_003EContentSigAlgo;

	internal static MKVType ___003C_003EContentSigHashAlgo;

	internal static MKVType ___003C_003ECues;

	internal static MKVType ___003C_003ECuePoint;

	internal static MKVType ___003C_003ECueTime;

	internal static MKVType ___003C_003ECueTrackPositions;

	internal static MKVType ___003C_003ECueTrack;

	internal static MKVType ___003C_003ECueClusterPosition;

	internal static MKVType ___003C_003ECueRelativePosition;

	internal static MKVType ___003C_003ECueDuration;

	internal static MKVType ___003C_003ECueBlockNumber;

	internal static MKVType ___003C_003ECueCodecState;

	internal static MKVType ___003C_003ECueReference;

	internal static MKVType ___003C_003ECueRefTime;

	internal static MKVType ___003C_003EAttachments;

	internal static MKVType ___003C_003EAttachedFile;

	internal static MKVType ___003C_003EFileDescription;

	internal static MKVType ___003C_003EFileName;

	internal static MKVType ___003C_003EFileMimeType;

	internal static MKVType ___003C_003EFileData;

	internal static MKVType ___003C_003EFileUID;

	internal static MKVType ___003C_003EChapters;

	internal static MKVType ___003C_003EEditionEntry;

	internal static MKVType ___003C_003EEditionUID;

	internal static MKVType ___003C_003EEditionFlagHidden;

	internal static MKVType ___003C_003EEditionFlagDefault;

	internal static MKVType ___003C_003EEditionFlagOrdered;

	internal static MKVType ___003C_003EChapterAtom;

	internal static MKVType ___003C_003EChapterUID;

	internal static MKVType ___003C_003EChapterStringUID;

	internal static MKVType ___003C_003EChapterTimeStart;

	internal static MKVType ___003C_003EChapterTimeEnd;

	internal static MKVType ___003C_003EChapterFlagHidden;

	internal static MKVType ___003C_003EChapterFlagEnabled;

	internal static MKVType ___003C_003EChapterSegmentUID;

	internal static MKVType ___003C_003EChapterSegmentEditionUID;

	internal static MKVType ___003C_003EChapterPhysicalEquiv;

	internal static MKVType ___003C_003EChapterTrack;

	internal static MKVType ___003C_003EChapterTrackNumber;

	internal static MKVType ___003C_003EChapterDisplay;

	internal static MKVType ___003C_003EChapString;

	internal static MKVType ___003C_003EChapLanguage;

	internal static MKVType ___003C_003EChapCountry;

	internal static MKVType ___003C_003EChapProcess;

	internal static MKVType ___003C_003EChapProcessCodecID;

	internal static MKVType ___003C_003EChapProcessPrivate;

	internal static MKVType ___003C_003EChapProcessCommand;

	internal static MKVType ___003C_003EChapProcessTime;

	internal static MKVType ___003C_003EChapProcessData;

	internal static MKVType ___003C_003ETags;

	internal static MKVType ___003C_003ETag;

	internal static MKVType ___003C_003ETargets;

	internal static MKVType ___003C_003ETargetTypeValue;

	internal static MKVType ___003C_003ETargetType;

	internal static MKVType ___003C_003ETagTrackUID;

	internal static MKVType ___003C_003ETagEditionUID;

	internal static MKVType ___003C_003ETagChapterUID;

	internal static MKVType ___003C_003ETagAttachmentUID;

	internal static MKVType ___003C_003ESimpleTag;

	internal static MKVType ___003C_003ETagName;

	internal static MKVType ___003C_003ETagLanguage;

	internal static MKVType ___003C_003ETagDefault;

	internal static MKVType ___003C_003ETagString;

	internal static MKVType ___003C_003ETagBinary;

	public static MKVType[] firstLevelHeaders;

	internal byte[] ___003C_003Eid;

	[Signature("Ljava/lang/Class<+Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;")]
	internal Class ___003C_003Eclazz;

	private string _name;

	[Signature("Ljava/util/Map<Lorg/jcodec/containers/mkv/MKVType;Ljava/util/Set<Lorg/jcodec/containers/mkv/MKVType;>;>;")]
	internal static Map ___003C_003Echildren;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Void
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVoid;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CRC32
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECRC32;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EBML
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEBML;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EBMLVersion
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEBMLVersion;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EBMLReadVersion
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEBMLReadVersion;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EBMLMaxIDLength
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEBMLMaxIDLength;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EBMLMaxSizeLength
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEBMLMaxSizeLength;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DocType
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDocType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DocTypeVersion
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDocTypeVersion;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DocTypeReadVersion
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDocTypeReadVersion;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Segment
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESegment;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SeekHead
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESeekHead;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Seek
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESeek;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SeekID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESeekID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SeekPosition
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESeekPosition;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Info
	{
		[HideFromJava]
		get
		{
			return ___003C_003EInfo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SegmentUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESegmentUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SegmentFilename
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESegmentFilename;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PrevUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPrevUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PrevFilename
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPrevFilename;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType NextUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENextUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType NextFilenam
	{
		[HideFromJava]
		get
		{
			return ___003C_003ENextFilenam;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SegmentFamily
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESegmentFamily;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTranslate
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTranslate;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTranslateEditionUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTranslateEditionUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTranslateCodec
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTranslateCodec;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTranslateID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTranslateID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TimecodeScale
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETimecodeScale;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Duration
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDuration;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DateUTC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDateUTC;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Title
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETitle;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType MuxingApp
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMuxingApp;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType WritingApp
	{
		[HideFromJava]
		get
		{
			return ___003C_003EWritingApp;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Cluster
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECluster;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Timecode
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETimecode;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SilentTracks
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESilentTracks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SilentTrackNumber
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESilentTrackNumber;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Position
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPosition;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PrevSize
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPrevSize;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SimpleBlock
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESimpleBlock;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BlockGroup
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlockGroup;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Block
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlock;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BlockAdditions
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlockAdditions;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BlockMore
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlockMore;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BlockAddID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlockAddID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BlockAdditional
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlockAdditional;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BlockDuration
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBlockDuration;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ReferencePriority
	{
		[HideFromJava]
		get
		{
			return ___003C_003EReferencePriority;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ReferenceBlock
	{
		[HideFromJava]
		get
		{
			return ___003C_003EReferenceBlock;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CodecState
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECodecState;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Slices
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESlices;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TimeSlice
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETimeSlice;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType LaceNumber
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELaceNumber;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Tracks
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETracks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackEntry
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackEntry;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackNumber
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackNumber;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackType
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FlagEnabled
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFlagEnabled;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FlagDefault
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFlagDefault;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FlagForced
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFlagForced;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FlagLacing
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFlagLacing;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType MinCache
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMinCache;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType MaxCache
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMaxCache;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DefaultDuration
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDefaultDuration;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType MaxBlockAdditionID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMaxBlockAdditionID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Name
	{
		[HideFromJava]
		get
		{
			return ___003C_003EName;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Language
	{
		[HideFromJava]
		get
		{
			return ___003C_003ELanguage;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CodecID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECodecID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CodecPrivate
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECodecPrivate;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CodecName
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECodecName;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType AttachmentLink
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAttachmentLink;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CodecDecodeAll
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECodecDecodeAll;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackOverlay
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackOverlay;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackTranslate
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackTranslate;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackTranslateEditionUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackTranslateEditionUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackTranslateCodec
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackTranslateCodec;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackTranslateTrackID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackTranslateTrackID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Video
	{
		[HideFromJava]
		get
		{
			return ___003C_003EVideo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FlagInterlaced
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFlagInterlaced;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType StereoMode
	{
		[HideFromJava]
		get
		{
			return ___003C_003EStereoMode;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType AlphaMode
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAlphaMode;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PixelWidth
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPixelWidth;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PixelHeight
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPixelHeight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PixelCropBottom
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPixelCropBottom;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PixelCropTop
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPixelCropTop;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PixelCropLeft
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPixelCropLeft;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType PixelCropRight
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPixelCropRight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DisplayWidth
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDisplayWidth;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DisplayHeight
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDisplayHeight;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType DisplayUnit
	{
		[HideFromJava]
		get
		{
			return ___003C_003EDisplayUnit;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType AspectRatioType
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAspectRatioType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ColourSpace
	{
		[HideFromJava]
		get
		{
			return ___003C_003EColourSpace;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Audio
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAudio;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SamplingFrequency
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESamplingFrequency;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType OutputSamplingFrequency
	{
		[HideFromJava]
		get
		{
			return ___003C_003EOutputSamplingFrequency;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Channels
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChannels;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType BitDepth
	{
		[HideFromJava]
		get
		{
			return ___003C_003EBitDepth;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackOperation
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackOperation;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackCombinePlanes
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackCombinePlanes;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackPlane
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackPlane;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackPlaneUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackPlaneUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackPlaneType
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackPlaneType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackJoinBlocks
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackJoinBlocks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TrackJoinUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETrackJoinUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncodings
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncodings;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncoding
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncoding;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncodingOrder
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncodingOrder;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncodingScope
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncodingScope;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncodingType
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncodingType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentCompression
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentCompression;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentCompAlgo
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentCompAlgo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentCompSettings
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentCompSettings;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncryption
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncryption;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncAlgo
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncAlgo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentEncKeyID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentEncKeyID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentSignature
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentSignature;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentSigKeyID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentSigKeyID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentSigAlgo
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentSigAlgo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ContentSigHashAlgo
	{
		[HideFromJava]
		get
		{
			return ___003C_003EContentSigHashAlgo;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Cues
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECues;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CuePoint
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECuePoint;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueTime
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueTime;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueTrackPositions
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueTrackPositions;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueTrack
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueTrack;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueClusterPosition
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueClusterPosition;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueRelativePosition
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueRelativePosition;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueDuration
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueDuration;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueBlockNumber
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueBlockNumber;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueCodecState
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueCodecState;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueReference
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueReference;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType CueRefTime
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECueRefTime;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Attachments
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAttachments;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType AttachedFile
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAttachedFile;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FileDescription
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFileDescription;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FileName
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFileName;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FileMimeType
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFileMimeType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FileData
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFileData;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType FileUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFileUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Chapters
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapters;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EditionEntry
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEditionEntry;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EditionUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEditionUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EditionFlagHidden
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEditionFlagHidden;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EditionFlagDefault
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEditionFlagDefault;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType EditionFlagOrdered
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEditionFlagOrdered;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterAtom
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterAtom;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterStringUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterStringUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTimeStart
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTimeStart;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTimeEnd
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTimeEnd;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterFlagHidden
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterFlagHidden;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterFlagEnabled
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterFlagEnabled;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterSegmentUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterSegmentUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterSegmentEditionUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterSegmentEditionUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterPhysicalEquiv
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterPhysicalEquiv;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTrack
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTrack;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterTrackNumber
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterTrackNumber;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapterDisplay
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapterDisplay;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapString
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapString;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapLanguage
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapLanguage;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapCountry
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapCountry;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapProcess
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapProcess;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapProcessCodecID
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapProcessCodecID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapProcessPrivate
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapProcessPrivate;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapProcessCommand
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapProcessCommand;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapProcessTime
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapProcessTime;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType ChapProcessData
	{
		[HideFromJava]
		get
		{
			return ___003C_003EChapProcessData;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Tags
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETags;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Tag
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETag;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType Targets
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETargets;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TargetTypeValue
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETargetTypeValue;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TargetType
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETargetType;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagTrackUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagTrackUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagEditionUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagEditionUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagChapterUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagChapterUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagAttachmentUID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagAttachmentUID;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType SimpleTag
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESimpleTag;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagName
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagName;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagLanguage
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagLanguage;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagDefault
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagDefault;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagString
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagString;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MKVType TagBinary
	{
		[HideFromJava]
		get
		{
			return ___003C_003ETagBinary;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public byte[] id
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eid;
		}
		[HideFromJava]
		private set
		{
			___003C_003Eid = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public Class clazz
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eclazz;
		}
		[HideFromJava]
		private set
		{
			___003C_003Eclazz = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Map children
	{
		[HideFromJava]
		get
		{
			return ___003C_003Echildren;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 10, 162, 109 })]
	public static EbmlBase findFirst(EbmlBase master, MKVType[] path)
	{
		LinkedList tlist = new LinkedList(Arrays.asList(path));
		EbmlBase result = findFirstSub(master, tlist);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mkv/boxes/EbmlBase;>(Lorg/jcodec/containers/mkv/MKVType;)TT;")]
	[LineNumberTable(new byte[] { 159, 50, 130, 127, 2, 104, 125, 98, 103 })]
	public static EbmlBase createByType(MKVType g)
	{
		//Discarded unreachable code: IL_002e
		java.lang.Exception ex2;
		try
		{
			EbmlBase elem = (EbmlBase)Platform.newInstance(g.___003C_003Eclazz, new object[1] { g.___003C_003Eid });
			elem.type = g;
			return elem;
		}
		catch (System.Exception x)
		{
			java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		java.lang.Exception e = ex2;
		Throwable.instancehelper_printStackTrace(e);
		return new EbmlBin(g.___003C_003Eid);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/List<+Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;[Lorg/jcodec/containers/mkv/MKVType;)TT;")]
	[LineNumberTable(new byte[] { 159, 8, 130, 109, 124, 105, 100, 99, 131 })]
	public static object findFirstTree(List tree, MKVType[] path)
	{
		LinkedList tlist = new LinkedList(Arrays.asList(path));
		Iterator iterator = tree.iterator();
		while (iterator.hasNext())
		{
			EbmlBase e = (EbmlBase)iterator.next();
			EbmlBase z = findFirstSub(e, tlist);
			if (z != null)
			{
				return z;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/List<+Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;Ljava/lang/Class<TT;>;[Lorg/jcodec/containers/mkv/MKVType;)Ljava/util/List<TT;>;")]
	[LineNumberTable(new byte[]
	{
		158,
		byte.MaxValue,
		130,
		103,
		109,
		109,
		124,
		111,
		116,
		137,
		106,
		131
	})]
	public static List findList(List tree, Class class1, MKVType[] path)
	{
		LinkedList result = new LinkedList();
		LinkedList tlist = new LinkedList(Arrays.asList(path));
		if (((List)tlist).size() > 0)
		{
			Iterator iterator = tree.iterator();
			while (iterator.hasNext())
			{
				EbmlBase node = (EbmlBase)iterator.next();
				MKVType head = (MKVType)((List)tlist).remove(0);
				if (head == null || java.lang.Object.instancehelper_equals(head, node.type))
				{
					findSubList(node, tlist, result);
				}
				((List)tlist).add(0, (object)head);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 21, 130, 100, 123, 131, 227, 71, 127, 16,
		219, 123, 131, 119
	})]
	public static bool possibleChild(EbmlMaster parent, EbmlBase child)
	{
		if (parent == null)
		{
			if (child.type == ___003C_003EEBML || child.type == ___003C_003ESegment)
			{
				return true;
			}
			return false;
		}
		if (Platform.arrayEqualsByte(child.id, ___003C_003EVoid.___003C_003Eid) || Platform.arrayEqualsByte(child.id, ___003C_003ECRC32.___003C_003Eid))
		{
			return child.offset != parent.dataOffset + parent.dataLen;
		}
		if (child.type == ___003C_003EVoid || child.type == ___003C_003ECRC32)
		{
			return true;
		}
		Set candidates = (Set)___003C_003Echildren.get(parent.type);
		return (candidates != null && candidates.contains(child.type)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mkv/boxes/EbmlBase;>([BJ)TT;")]
	[LineNumberTable(new byte[]
	{
		159, 47, 98, 103, 104, 101, 111, 234, 61, 231,
		69, 127, 12, 53, 134, 104, 108
	})]
	public static EbmlBase createById(byte[] id, long offset)
	{
		MKVType[] values = MKVType.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			MKVType t2 = values[i];
			if (Platform.arrayEqualsByte(t2.___003C_003Eid, id))
			{
				EbmlBase result = createByType(t2);
				return result;
			}
		}
		java.lang.System.err.println(new StringBuilder().append("WARNING: unspecified ebml ID (").append(EbmlUtil.toHexString(id)).append(") encountered at position 0x")
			.append(java.lang.String.instancehelper_toUpperCase(Long.toHexString(offset)))
			.toString());
		EbmlVoid t = new EbmlVoid(id);
		t.type = ___003C_003EVoid;
		return t;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 41, 130, 103, 104, 101, 111, 227, 61, 231,
		69
	})]
	public static bool isSpecifiedHeader(byte[] b)
	{
		MKVType[] values = MKVType.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			MKVType firstLevelHeader = values[i];
			if (Platform.arrayEqualsByte(firstLevelHeader.___003C_003Eid, b))
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(364)]
	public static MKVType[] values()
	{
		return (MKVType[])_values.toArray(new MKVType[0]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/containers/mkv/boxes/EbmlBase;Ljava/util/List<Lorg/jcodec/containers/mkv/MKVType;>;)Lorg/jcodec/containers/mkv/boxes/EbmlBase;")]
	[LineNumberTable(new byte[]
	{
		159, 5, 130, 105, 131, 117, 131, 106, 131, 110,
		99, 105, 114, 108, 213, 105
	})]
	private static EbmlBase findFirstSub(EbmlBase elem, List path)
	{
		if (path.size() == 0)
		{
			return null;
		}
		if (!java.lang.Object.instancehelper_equals(elem.type, path.get(0)))
		{
			return null;
		}
		if (path.size() == 1)
		{
			return elem;
		}
		MKVType head = (MKVType)path.remove(0);
		EbmlBase result = null;
		if (elem is EbmlMaster)
		{
			Iterator iter = ((EbmlMaster)elem).___003C_003Echildren.iterator();
			while (iter.hasNext() && result == null)
			{
				result = findFirstSub((EbmlBase)iter.next(), path);
			}
		}
		path.add(0, head);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Lorg/jcodec/containers/mkv/boxes/EbmlBase;Ljava/util/List<Lorg/jcodec/containers/mkv/MKVType;>;Ljava/util/Collection<TT;>;)V")]
	[LineNumberTable(new byte[]
	{
		158, 251, 130, 109, 110, 105, 104, 127, 2, 114,
		137, 131, 105, 99, 137
	})]
	private static void findSubList(EbmlBase element, List path, Collection result)
	{
		if (path.size() > 0)
		{
			MKVType head = (MKVType)path.remove(0);
			if (element is EbmlMaster)
			{
				EbmlMaster nb = (EbmlMaster)element;
				Iterator iterator = nb.___003C_003Echildren.iterator();
				while (iterator.hasNext())
				{
					EbmlBase candidate = (EbmlBase)iterator.next();
					if (head == null || java.lang.Object.instancehelper_equals(head, candidate.type))
					{
						findSubList(candidate, path, result);
					}
				}
			}
			path.add(0, head);
		}
		else
		{
			result.add(element);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/containers/mkv/boxes/EbmlBase;Ljava/util/List<Lorg/jcodec/containers/mkv/MKVType;>;Ljava/util/Collection<Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;)V")]
	[LineNumberTable(new byte[]
	{
		158, 239, 66, 109, 110, 105, 104, 127, 2, 114,
		137, 131, 105, 99, 137
	})]
	private static void findSub(EbmlBase master, List path, Collection result)
	{
		if (path.size() > 0)
		{
			MKVType head = (MKVType)path.remove(0);
			if (master is EbmlMaster)
			{
				EbmlMaster nb = (EbmlMaster)master;
				Iterator iterator = nb.___003C_003Echildren.iterator();
				while (iterator.hasNext())
				{
					EbmlBase candidate = (EbmlBase)iterator.next();
					if (head == null || java.lang.Object.instancehelper_equals(head, candidate.type))
					{
						findSub(candidate, path, result);
					}
				}
			}
			path.add(0, head);
		}
		else
		{
			result.add(master);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 55, 130, 105, 104, 104, 104, 109 })]
	private MKVType(string name, byte[] id, Class clazz)
	{
		_name = name;
		___003C_003Eid = id;
		___003C_003Eclazz = clazz;
		_values.add(this);
	}

	[LineNumberTable(357)]
	public virtual string name()
	{
		return _name;
	}

	[LineNumberTable(360)]
	public override string toString()
	{
		return _name;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 44, 161, 68, 103, 104, 101, 108, 227, 61,
		231, 70
	})]
	public static bool isHeaderFirstByte(byte b)
	{
		int b2 = (sbyte)b;
		MKVType[] values = MKVType.values();
		for (int i = 0; i < (nint)values.LongLength; i++)
		{
			MKVType t = values[i];
			if (t.___003C_003Eid[0] == b2)
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 38, 66, 116, 111, 3, 167 })]
	public static bool isFirstLevelHeader(byte[] b)
	{
		MKVType[] array = firstLevelHeaders;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MKVType firstLevelHeader = array[i];
			if (Platform.arrayEqualsByte(firstLevelHeader.___003C_003Eid, b))
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 23, 130, 127, 6, 116, 109, 99 })]
	public static MKVType getParent(MKVType t)
	{
		Iterator iterator = ___003C_003Echildren.entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry ent = (Map.Entry)iterator.next();
			if (((Set)ent.getValue()).contains(t))
			{
				return (MKVType)ent.getKey();
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 15, 162, 127, 9, 163, 100, 163, 127, 6,
		163, 127, 17, 111, 35, 163
	})]
	public static bool possibleChildById(EbmlMaster parent, byte[] typeId)
	{
		if (parent == null && (Platform.arrayEqualsByte(___003C_003EEBML.___003C_003Eid, typeId) || Platform.arrayEqualsByte(___003C_003ESegment.___003C_003Eid, typeId)))
		{
			return true;
		}
		if (parent == null)
		{
			return false;
		}
		if (Platform.arrayEqualsByte(___003C_003EVoid.___003C_003Eid, typeId) || Platform.arrayEqualsByte(___003C_003ECRC32.___003C_003Eid, typeId))
		{
			return true;
		}
		Iterator iterator = ((Set)___003C_003Echildren.get(parent.type)).iterator();
		while (iterator.hasNext())
		{
			MKVType aCandidate = (MKVType)iterator.next();
			if (Platform.arrayEqualsByte(aCandidate.___003C_003Eid, typeId))
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/List<+Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;Ljava/lang/Class<TT;>;[Lorg/jcodec/containers/mkv/MKVType;)[TT;")]
	[LineNumberTable(new byte[]
	{
		158, 246, 66, 103, 109, 109, 124, 111, 116, 137,
		106, 131
	})]
	public static object[] findAllTree(List tree, Class class1, MKVType[] path)
	{
		LinkedList result = new LinkedList();
		LinkedList tlist = new LinkedList(Arrays.asList(path));
		if (((List)tlist).size() > 0)
		{
			Iterator iterator = tree.iterator();
			while (iterator.hasNext())
			{
				EbmlBase node = (EbmlBase)iterator.next();
				MKVType head = (MKVType)((List)tlist).remove(0);
				if (head == null || java.lang.Object.instancehelper_equals(head, node.type))
				{
					findSub(node, tlist, result);
				}
				((List)tlist).add(0, (object)head);
			}
		}
		object[] result2 = ((List)result).toArray((object[])java.lang.reflect.Array.newInstance(class1, 0));
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Lorg/jcodec/containers/mkv/boxes/EbmlBase;Ljava/lang/Class<TT;>;Z[Lorg/jcodec/containers/mkv/MKVType;)[TT;")]
	[LineNumberTable(new byte[] { 158, 242, 66, 103, 109, 117, 155, 105, 105 })]
	public static object[] findAll(EbmlBase master, Class class1, bool ga, MKVType[] path)
	{
		LinkedList result = new LinkedList();
		LinkedList tlist = new LinkedList(Arrays.asList(path));
		if (!java.lang.Object.instancehelper_equals(master.type, ((List)tlist).get(0)))
		{
			object[] result2 = ((List)result).toArray((object[])java.lang.reflect.Array.newInstance(class1, 0));
			return result2;
		}
		((List)tlist).remove(0);
		findSub(master, tlist, result);
		object[] result3 = ((List)result).toArray((object[])java.lang.reflect.Array.newInstance(class1, 0));
		return result3;
	}

	[LineNumberTable(new byte[]
	{
		159,
		132,
		98,
		139,
		127,
		1,
		127,
		1,
		127,
		16,
		127,
		6,
		127,
		6,
		127,
		6,
		159,
		6,
		127,
		6,
		127,
		6,
		159,
		6,
		122,
		127,
		16,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		16,
		159,
		6,
		127,
		6,
		127,
		11,
		127,
		11,
		159,
		11,
		159,
		11,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		191,
		6,
		159,
		11,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		191,
		6,
		154,
		159,
		1,
		159,
		6,
		159,
		6,
		159,
		1,
		159,
		1,
		154,
		159,
		1,
		154,
		159,
		6,
		159,
		1,
		159,
		1,
		byte.MaxValue,
		1,
		74,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		191,
		1,
		159,
		16,
		159,
		1,
		159,
		1,
		159,
		6,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		6,
		159,
		1,
		159,
		6,
		159,
		6,
		159,
		11,
		159,
		6,
		159,
		6,
		159,
		11,
		159,
		1,
		159,
		6,
		159,
		11,
		159,
		6,
		159,
		1,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		127,
		6,
		159,
		1,
		159,
		1,
		159,
		6,
		127,
		6,
		127,
		1,
		159,
		1,
		159,
		6,
		159,
		6,
		159,
		6,
		127,
		6,
		127,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		127,
		11,
		127,
		1,
		127,
		1,
		127,
		6,
		127,
		1,
		159,
		6,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		127,
		16,
		127,
		1,
		127,
		1,
		127,
		1,
		127,
		1,
		159,
		1,
		159,
		1,
		159,
		1,
		159,
		6,
		159,
		1,
		159,
		1,
		159,
		1,
		127,
		16,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		159,
		6,
		127,
		16,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		1,
		159,
		6,
		127,
		6,
		127,
		1,
		127,
		1,
		127,
		1,
		159,
		6,
		159,
		6,
		127,
		6,
		127,
		6,
		127,
		1,
		127,
		1,
		127,
		1,
		127,
		1,
		127,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		159,
		6,
		127,
		16,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		159,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		127,
		6,
		159,
		6,
		byte.MaxValue,
		108,
		160,
		77,
		139,
		127,
		63,
		159,
		71,
		127,
		15,
		127,
		23,
		127,
		125,
		159,
		31,
		127,
		55,
		127,
		15,
		127,
		63,
		127,
		15,
		127,
		23,
		127,
		15,
		159,
		15,
		127,
		15,
		127,
		160,
		151,
		127,
		31,
		127,
		125,
		127,
		39,
		127,
		23,
		127,
		15,
		127,
		23,
		127,
		15,
		127,
		15,
		127,
		47,
		127,
		23,
		159,
		55,
		127,
		15,
		127,
		23,
		127,
		63,
		159,
		15,
		127,
		15,
		159,
		47,
		127,
		15,
		127,
		47,
		127,
		107,
		127,
		15,
		127,
		31,
		127,
		31,
		159,
		23,
		127,
		15,
		127,
		23,
		127,
		55,
		127,
		47
	})]
	static MKVType()
	{
		_values = new ArrayList();
		___003C_003EVoid = new MKVType("Void", new byte[1] { 236 }, ClassLiteral<EbmlVoid>.Value);
		___003C_003ECRC32 = new MKVType("CRC32", new byte[1] { 191 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EEBML = new MKVType("EBML", new byte[4] { 26, 69, 223, 163 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EEBMLVersion = new MKVType("EBMLVersion", new byte[2] { 66, 134 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EEBMLReadVersion = new MKVType("EBMLReadVersion", new byte[2] { 66, 247 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EEBMLMaxIDLength = new MKVType("EBMLMaxIDLength", new byte[2] { 66, 242 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EEBMLMaxSizeLength = new MKVType("EBMLMaxSizeLength", new byte[2] { 66, 243 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDocType = new MKVType("DocType", new byte[2] { 66, 130 }, ClassLiteral<EbmlString>.Value);
		___003C_003EDocTypeVersion = new MKVType("DocTypeVersion", new byte[2] { 66, 135 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDocTypeReadVersion = new MKVType("DocTypeReadVersion", new byte[2] { 66, 133 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ESegment = new MKVType("Segment", MkvSegment.___003C_003ESEGMENT_ID, ClassLiteral<MkvSegment>.Value);
		___003C_003ESeekHead = new MKVType("SeekHead", new byte[4] { 17, 77, 155, 116 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ESeek = new MKVType("Seek", new byte[2] { 77, 187 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ESeekID = new MKVType("SeekID", new byte[2] { 83, 171 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ESeekPosition = new MKVType("SeekPosition", new byte[2] { 83, 172 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EInfo = new MKVType("Info", new byte[4] { 21, 73, 169, 102 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ESegmentUID = new MKVType("SegmentUID", new byte[2] { 115, 164 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ESegmentFilename = new MKVType("SegmentFilename", new byte[2] { 115, 132 }, ClassLiteral<EbmlString>.Value);
		___003C_003EPrevUID = new MKVType("PrevUID", new byte[3] { 60, 185, 35 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EPrevFilename = new MKVType("PrevFilename", new byte[3] { 60, 131, 171 }, ClassLiteral<EbmlString>.Value);
		___003C_003ENextUID = new MKVType("NextUID", new byte[3] { 62, 185, 35 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ENextFilenam = new MKVType("NextFilenam", new byte[3] { 62, 131, 187 }, ClassLiteral<EbmlString>.Value);
		___003C_003ESegmentFamily = new MKVType("SegmentFamily", new byte[2] { 68, 68 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EChapterTranslate = new MKVType("ChapterTranslate", new byte[2] { 105, 36 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EChapterTranslateEditionUID = new MKVType("ChapterTranslateEditionUID", new byte[2] { 105, 252 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterTranslateCodec = new MKVType("ChapterTranslateCodec", new byte[2] { 105, 191 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterTranslateID = new MKVType("ChapterTranslateID", new byte[2] { 105, 165 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ETimecodeScale = new MKVType("TimecodeScale", new byte[3] { 42, 215, 177 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDuration = new MKVType("Duration", new byte[2] { 68, 137 }, ClassLiteral<EbmlFloat>.Value);
		___003C_003EDateUTC = new MKVType("DateUTC", new byte[2] { 68, 97 }, ClassLiteral<EbmlDate>.Value);
		___003C_003ETitle = new MKVType("Title", new byte[2] { 123, 169 }, ClassLiteral<EbmlString>.Value);
		___003C_003EMuxingApp = new MKVType("MuxingApp", new byte[2] { 77, 128 }, ClassLiteral<EbmlString>.Value);
		___003C_003EWritingApp = new MKVType("WritingApp", new byte[2] { 87, 65 }, ClassLiteral<EbmlString>.Value);
		___003C_003ECluster = new MKVType("Cluster", EbmlMaster.___003C_003ECLUSTER_ID, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETimecode = new MKVType("Timecode", new byte[1] { 231 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ESilentTracks = new MKVType("SilentTracks", new byte[2] { 88, 84 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ESilentTrackNumber = new MKVType("SilentTrackNumber", new byte[2] { 88, 215 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPosition = new MKVType("Position", new byte[1] { 167 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPrevSize = new MKVType("PrevSize", new byte[1] { 171 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ESimpleBlock = new MKVType("SimpleBlock", MkvBlock.___003C_003ESIMPLEBLOCK_ID, ClassLiteral<MkvBlock>.Value);
		___003C_003EBlockGroup = new MKVType("BlockGroup", new byte[1] { 160 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EBlock = new MKVType("Block", MkvBlock.___003C_003EBLOCK_ID, ClassLiteral<MkvBlock>.Value);
		___003C_003EBlockAdditions = new MKVType("BlockAdditions", new byte[2] { 117, 161 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EBlockMore = new MKVType("BlockMore", new byte[1] { 166 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EBlockAddID = new MKVType("BlockAddID", new byte[1] { 238 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EBlockAdditional = new MKVType("BlockAdditional", new byte[1] { 165 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EBlockDuration = new MKVType("BlockDuration", new byte[1] { 155 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EReferencePriority = new MKVType("ReferencePriority", new byte[1] { 250 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EReferenceBlock = new MKVType("ReferenceBlock", new byte[1] { 251 }, ClassLiteral<EbmlSint>.Value);
		___003C_003ECodecState = new MKVType("CodecState", new byte[1] { 164 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ESlices = new MKVType("Slices", new byte[1] { 142 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETimeSlice = new MKVType("TimeSlice", new byte[1] { 232 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ELaceNumber = new MKVType("LaceNumber", new byte[1] { 204 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETracks = new MKVType("Tracks", new byte[4] { 22, 84, 174, 107 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackEntry = new MKVType("TrackEntry", new byte[1] { 174 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackNumber = new MKVType("TrackNumber", new byte[1] { 215 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackUID = new MKVType("TrackUID", new byte[2] { 115, 197 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackType = new MKVType("TrackType", new byte[1] { 131 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EFlagEnabled = new MKVType("FlagEnabled", new byte[1] { 185 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EFlagDefault = new MKVType("FlagDefault", new byte[1] { 136 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EFlagForced = new MKVType("FlagForced", new byte[2] { 85, 170 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EFlagLacing = new MKVType("FlagLacing", new byte[1] { 156 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EMinCache = new MKVType("MinCache", new byte[2] { 109, 231 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EMaxCache = new MKVType("MaxCache", new byte[2] { 109, 248 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDefaultDuration = new MKVType("DefaultDuration", new byte[3] { 35, 227, 131 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EMaxBlockAdditionID = new MKVType("MaxBlockAdditionID", new byte[2] { 85, 238 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EName = new MKVType("Name", new byte[2] { 83, 110 }, ClassLiteral<EbmlString>.Value);
		___003C_003ELanguage = new MKVType("Language", new byte[3] { 34, 181, 156 }, ClassLiteral<EbmlString>.Value);
		___003C_003ECodecID = new MKVType("CodecID", new byte[1] { 134 }, ClassLiteral<EbmlString>.Value);
		___003C_003ECodecPrivate = new MKVType("CodecPrivate", new byte[2] { 99, 162 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ECodecName = new MKVType("CodecName", new byte[3] { 37, 134, 136 }, ClassLiteral<EbmlString>.Value);
		___003C_003EAttachmentLink = new MKVType("AttachmentLink", new byte[2] { 116, 70 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECodecDecodeAll = new MKVType("CodecDecodeAll", new byte[1] { 170 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackOverlay = new MKVType("TrackOverlay", new byte[2] { 111, 171 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackTranslate = new MKVType("TrackTranslate", new byte[2] { 102, 36 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackTranslateEditionUID = new MKVType("TrackTranslateEditionUID", new byte[2] { 102, 252 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackTranslateCodec = new MKVType("TrackTranslateCodec", new byte[2] { 102, 191 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackTranslateTrackID = new MKVType("TrackTranslateTrackID", new byte[2] { 102, 165 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EVideo = new MKVType("Video", new byte[1] { 224 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EFlagInterlaced = new MKVType("FlagInterlaced", new byte[1] { 154 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EStereoMode = new MKVType("StereoMode", new byte[2] { 83, 184 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EAlphaMode = new MKVType("AlphaMode", new byte[2] { 83, 192 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPixelWidth = new MKVType("PixelWidth", new byte[1] { 176 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPixelHeight = new MKVType("PixelHeight", new byte[1] { 186 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPixelCropBottom = new MKVType("PixelCropBottom", new byte[2] { 84, 170 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPixelCropTop = new MKVType("PixelCropTop", new byte[2] { 84, 187 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPixelCropLeft = new MKVType("PixelCropLeft", new byte[2] { 84, 204 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EPixelCropRight = new MKVType("PixelCropRight", new byte[2] { 84, 221 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDisplayWidth = new MKVType("DisplayWidth", new byte[2] { 84, 176 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDisplayHeight = new MKVType("DisplayHeight", new byte[2] { 84, 186 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EDisplayUnit = new MKVType("DisplayUnit", new byte[2] { 84, 178 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EAspectRatioType = new MKVType("AspectRatioType", new byte[2] { 84, 179 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EColourSpace = new MKVType("ColourSpace", new byte[3] { 46, 181, 36 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EAudio = new MKVType("Audio", new byte[1] { 225 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ESamplingFrequency = new MKVType("SamplingFrequency", new byte[1] { 181 }, ClassLiteral<EbmlFloat>.Value);
		___003C_003EOutputSamplingFrequency = new MKVType("OutputSamplingFrequency", new byte[2] { 120, 181 }, ClassLiteral<EbmlFloat>.Value);
		___003C_003EChannels = new MKVType("Channels", new byte[1] { 159 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EBitDepth = new MKVType("BitDepth", new byte[2] { 98, 100 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackOperation = new MKVType("TrackOperation", new byte[1] { 226 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackCombinePlanes = new MKVType("TrackCombinePlanes", new byte[1] { 227 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackPlane = new MKVType("TrackPlane", new byte[1] { 228 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackPlaneUID = new MKVType("TrackPlaneUID", new byte[1] { 229 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackPlaneType = new MKVType("TrackPlaneType", new byte[1] { 230 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETrackJoinBlocks = new MKVType("TrackJoinBlocks", new byte[1] { 233 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETrackJoinUID = new MKVType("TrackJoinUID", new byte[1] { 237 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentEncodings = new MKVType("ContentEncodings", new byte[2] { 109, 128 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EContentEncoding = new MKVType("ContentEncoding", new byte[2] { 98, 64 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EContentEncodingOrder = new MKVType("ContentEncodingOrder", new byte[2] { 80, 49 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentEncodingScope = new MKVType("ContentEncodingScope", new byte[2] { 80, 50 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentEncodingType = new MKVType("ContentEncodingType", new byte[2] { 80, 51 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentCompression = new MKVType("ContentCompression", new byte[2] { 80, 52 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EContentCompAlgo = new MKVType("ContentCompAlgo", new byte[2] { 66, 84 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentCompSettings = new MKVType("ContentCompSettings", new byte[2] { 66, 85 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EContentEncryption = new MKVType("ContentEncryption", new byte[2] { 80, 53 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EContentEncAlgo = new MKVType("ContentEncAlgo", new byte[2] { 71, 225 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentEncKeyID = new MKVType("ContentEncKeyID", new byte[2] { 71, 226 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EContentSignature = new MKVType("ContentSignature", new byte[2] { 71, 227 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EContentSigKeyID = new MKVType("ContentSigKeyID", new byte[2] { 71, 228 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EContentSigAlgo = new MKVType("ContentSigAlgo", new byte[2] { 71, 229 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EContentSigHashAlgo = new MKVType("ContentSigHashAlgo", new byte[2] { 71, 230 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECues = new MKVType("Cues", new byte[4] { 28, 83, 187, 107 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ECuePoint = new MKVType("CuePoint", new byte[1] { 187 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ECueTime = new MKVType("CueTime", new byte[1] { 179 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueTrackPositions = new MKVType("CueTrackPositions", new byte[1] { 183 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ECueTrack = new MKVType("CueTrack", new byte[1] { 247 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueClusterPosition = new MKVType("CueClusterPosition", new byte[1] { 241 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueRelativePosition = new MKVType("CueRelativePosition", new byte[1] { 240 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueDuration = new MKVType("CueDuration", new byte[1] { 178 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueBlockNumber = new MKVType("CueBlockNumber", new byte[2] { 83, 120 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueCodecState = new MKVType("CueCodecState", new byte[1] { 234 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ECueReference = new MKVType("CueReference", new byte[1] { 219 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ECueRefTime = new MKVType("CueRefTime", new byte[1] { 150 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EAttachments = new MKVType("Attachments", new byte[4] { 25, 65, 164, 105 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EAttachedFile = new MKVType("AttachedFile", new byte[2] { 97, 167 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EFileDescription = new MKVType("FileDescription", new byte[2] { 70, 126 }, ClassLiteral<EbmlString>.Value);
		___003C_003EFileName = new MKVType("FileName", new byte[2] { 70, 110 }, ClassLiteral<EbmlString>.Value);
		___003C_003EFileMimeType = new MKVType("FileMimeType", new byte[2] { 70, 96 }, ClassLiteral<EbmlString>.Value);
		___003C_003EFileData = new MKVType("FileData", new byte[2] { 70, 92 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EFileUID = new MKVType("FileUID", new byte[2] { 70, 174 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapters = new MKVType("Chapters", new byte[4] { 16, 67, 167, 112 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EEditionEntry = new MKVType("EditionEntry", new byte[2] { 69, 185 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EEditionUID = new MKVType("EditionUID", new byte[2] { 69, 188 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EEditionFlagHidden = new MKVType("EditionFlagHidden", new byte[2] { 69, 189 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EEditionFlagDefault = new MKVType("EditionFlagDefault", new byte[2] { 69, 219 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EEditionFlagOrdered = new MKVType("EditionFlagOrdered", new byte[2] { 69, 221 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterAtom = new MKVType("ChapterAtom", new byte[1] { 182 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EChapterUID = new MKVType("ChapterUID", new byte[2] { 115, 196 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterStringUID = new MKVType("ChapterStringUID", new byte[2] { 86, 84 }, ClassLiteral<EbmlString>.Value);
		___003C_003EChapterTimeStart = new MKVType("ChapterTimeStart", new byte[1] { 145 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterTimeEnd = new MKVType("ChapterTimeEnd", new byte[1] { 146 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterFlagHidden = new MKVType("ChapterFlagHidden", new byte[1] { 152 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterFlagEnabled = new MKVType("ChapterFlagEnabled", new byte[2] { 69, 152 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterSegmentUID = new MKVType("ChapterSegmentUID", new byte[2] { 110, 103 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EChapterSegmentEditionUID = new MKVType("ChapterSegmentEditionUID", new byte[2] { 110, 188 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterPhysicalEquiv = new MKVType("ChapterPhysicalEquiv", new byte[2] { 99, 195 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterTrack = new MKVType("ChapterTrack", new byte[1] { 143 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EChapterTrackNumber = new MKVType("ChapterTrackNumber", new byte[1] { 137 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapterDisplay = new MKVType("ChapterDisplay", new byte[1] { 128 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EChapString = new MKVType("ChapString", new byte[1] { 133 }, ClassLiteral<EbmlString>.Value);
		___003C_003EChapLanguage = new MKVType("ChapLanguage", new byte[2] { 67, 124 }, ClassLiteral<EbmlString>.Value);
		___003C_003EChapCountry = new MKVType("ChapCountry", new byte[2] { 67, 126 }, ClassLiteral<EbmlString>.Value);
		___003C_003EChapProcess = new MKVType("ChapProcess", new byte[2] { 105, 68 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EChapProcessCodecID = new MKVType("ChapProcessCodecID", new byte[2] { 105, 85 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapProcessPrivate = new MKVType("ChapProcessPrivate", new byte[2] { 69, 13 }, ClassLiteral<EbmlBin>.Value);
		___003C_003EChapProcessCommand = new MKVType("ChapProcessCommand", new byte[2] { 105, 17 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003EChapProcessTime = new MKVType("ChapProcessTime", new byte[2] { 105, 34 }, ClassLiteral<EbmlUint>.Value);
		___003C_003EChapProcessData = new MKVType("ChapProcessData", new byte[2] { 105, 51 }, ClassLiteral<EbmlBin>.Value);
		___003C_003ETags = new MKVType("Tags", new byte[4] { 18, 84, 195, 103 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETag = new MKVType("Tag", new byte[2] { 115, 115 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETargets = new MKVType("Targets", new byte[2] { 99, 192 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETargetTypeValue = new MKVType("TargetTypeValue", new byte[2] { 104, 202 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETargetType = new MKVType("TargetType", new byte[2] { 99, 202 }, ClassLiteral<EbmlString>.Value);
		___003C_003ETagTrackUID = new MKVType("TagTrackUID", new byte[2] { 99, 197 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETagEditionUID = new MKVType("TagEditionUID", new byte[2] { 99, 201 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETagChapterUID = new MKVType("TagChapterUID", new byte[2] { 99, 196 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETagAttachmentUID = new MKVType("TagAttachmentUID", new byte[2] { 99, 198 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ESimpleTag = new MKVType("SimpleTag", new byte[2] { 103, 200 }, ClassLiteral<EbmlMaster>.Value);
		___003C_003ETagName = new MKVType("TagName", new byte[2] { 69, 163 }, ClassLiteral<EbmlString>.Value);
		___003C_003ETagLanguage = new MKVType("TagLanguage", new byte[2] { 68, 122 }, ClassLiteral<EbmlString>.Value);
		___003C_003ETagDefault = new MKVType("TagDefault", new byte[2] { 68, 132 }, ClassLiteral<EbmlUint>.Value);
		___003C_003ETagString = new MKVType("TagString", new byte[2] { 68, 135 }, ClassLiteral<EbmlString>.Value);
		___003C_003ETagBinary = new MKVType("TagBinary", new byte[2] { 68, 133 }, ClassLiteral<EbmlBin>.Value);
		firstLevelHeaders = new MKVType[15]
		{
			___003C_003ESeekHead, ___003C_003EInfo, ___003C_003ECluster, ___003C_003ETracks, ___003C_003ECues, ___003C_003EAttachments, ___003C_003EChapters, ___003C_003ETags, ___003C_003EEBMLVersion, ___003C_003EEBMLReadVersion,
			___003C_003EEBMLMaxIDLength, ___003C_003EEBMLMaxSizeLength, ___003C_003EDocType, ___003C_003EDocTypeVersion, ___003C_003EDocTypeReadVersion
		};
		___003C_003Echildren = new HashMap();
		Map __003C_003Echildren = ___003C_003Echildren;
		MKVType __003C_003EEBML = ___003C_003EEBML;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren.put(__003C_003EEBML, new HashSet(Arrays.asList(___003C_003EEBMLVersion, ___003C_003EEBMLReadVersion, ___003C_003EEBMLMaxIDLength, ___003C_003EEBMLMaxSizeLength, ___003C_003EDocType, ___003C_003EDocTypeVersion, ___003C_003EDocTypeReadVersion)));
		Map __003C_003Echildren2 = ___003C_003Echildren;
		MKVType __003C_003ESegment = ___003C_003ESegment;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren2.put(__003C_003ESegment, new HashSet(Arrays.asList(___003C_003ESeekHead, ___003C_003EInfo, ___003C_003ECluster, ___003C_003ETracks, ___003C_003ECues, ___003C_003EAttachments, ___003C_003EChapters, ___003C_003ETags)));
		Map __003C_003Echildren3 = ___003C_003Echildren;
		MKVType __003C_003ESeekHead = ___003C_003ESeekHead;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren3.put(__003C_003ESeekHead, new HashSet(Arrays.asList(___003C_003ESeek)));
		Map __003C_003Echildren4 = ___003C_003Echildren;
		MKVType __003C_003ESeek = ___003C_003ESeek;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren4.put(__003C_003ESeek, new HashSet(Arrays.asList(___003C_003ESeekID, ___003C_003ESeekPosition)));
		Map __003C_003Echildren5 = ___003C_003Echildren;
		MKVType __003C_003EInfo = ___003C_003EInfo;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren5.put(__003C_003EInfo, new HashSet(Arrays.asList(___003C_003ESegmentUID, ___003C_003ESegmentFilename, ___003C_003EPrevUID, ___003C_003EPrevFilename, ___003C_003ENextUID, ___003C_003ENextFilenam, ___003C_003ESegmentFamily, ___003C_003EChapterTranslate, ___003C_003ETimecodeScale, ___003C_003EDuration, ___003C_003EDateUTC, ___003C_003ETitle, ___003C_003EMuxingApp, ___003C_003EWritingApp)));
		Map __003C_003Echildren6 = ___003C_003Echildren;
		MKVType __003C_003EChapterTranslate = ___003C_003EChapterTranslate;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren6.put(__003C_003EChapterTranslate, new HashSet(Arrays.asList(___003C_003EChapterTranslateEditionUID, ___003C_003EChapterTranslateCodec, ___003C_003EChapterTranslateID)));
		Map __003C_003Echildren7 = ___003C_003Echildren;
		MKVType __003C_003ECluster = ___003C_003ECluster;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren7.put(__003C_003ECluster, new HashSet(Arrays.asList(___003C_003ETimecode, ___003C_003ESilentTracks, ___003C_003EPosition, ___003C_003EPrevSize, ___003C_003ESimpleBlock, ___003C_003EBlockGroup)));
		Map __003C_003Echildren8 = ___003C_003Echildren;
		MKVType __003C_003ESilentTracks = ___003C_003ESilentTracks;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren8.put(__003C_003ESilentTracks, new HashSet(Arrays.asList(___003C_003ESilentTrackNumber)));
		Map __003C_003Echildren9 = ___003C_003Echildren;
		MKVType __003C_003EBlockGroup = ___003C_003EBlockGroup;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren9.put(__003C_003EBlockGroup, new HashSet(Arrays.asList(___003C_003EBlock, ___003C_003EBlockAdditions, ___003C_003EBlockDuration, ___003C_003EReferencePriority, ___003C_003EReferenceBlock, ___003C_003ECodecState, ___003C_003ESlices)));
		Map __003C_003Echildren10 = ___003C_003Echildren;
		MKVType __003C_003EBlockAdditions = ___003C_003EBlockAdditions;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren10.put(__003C_003EBlockAdditions, new HashSet(Arrays.asList(___003C_003EBlockMore)));
		Map __003C_003Echildren11 = ___003C_003Echildren;
		MKVType __003C_003EBlockMore = ___003C_003EBlockMore;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren11.put(__003C_003EBlockMore, new HashSet(Arrays.asList(___003C_003EBlockAddID, ___003C_003EBlockAdditional)));
		Map __003C_003Echildren12 = ___003C_003Echildren;
		MKVType __003C_003ESlices = ___003C_003ESlices;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren12.put(__003C_003ESlices, new HashSet(Arrays.asList(___003C_003ETimeSlice)));
		Map __003C_003Echildren13 = ___003C_003Echildren;
		MKVType __003C_003ETimeSlice = ___003C_003ETimeSlice;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren13.put(__003C_003ETimeSlice, new HashSet(Arrays.asList(___003C_003ELaceNumber)));
		Map __003C_003Echildren14 = ___003C_003Echildren;
		MKVType __003C_003ETracks = ___003C_003ETracks;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren14.put(__003C_003ETracks, new HashSet(Arrays.asList(___003C_003ETrackEntry)));
		Map __003C_003Echildren15 = ___003C_003Echildren;
		MKVType __003C_003ETrackEntry = ___003C_003ETrackEntry;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren15.put(__003C_003ETrackEntry, new HashSet(Arrays.asList(___003C_003ETrackNumber, ___003C_003ETrackUID, ___003C_003ETrackType, ___003C_003ETrackType, ___003C_003EFlagDefault, ___003C_003EFlagForced, ___003C_003EFlagLacing, ___003C_003EMinCache, ___003C_003EMaxCache, ___003C_003EDefaultDuration, ___003C_003EMaxBlockAdditionID, ___003C_003EName, ___003C_003ELanguage, ___003C_003ECodecID, ___003C_003ECodecPrivate, ___003C_003ECodecName, ___003C_003EAttachmentLink, ___003C_003ECodecDecodeAll, ___003C_003ETrackOverlay, ___003C_003ETrackTranslate, ___003C_003EVideo, ___003C_003EAudio, ___003C_003ETrackOperation, ___003C_003EContentEncodings)));
		Map __003C_003Echildren16 = ___003C_003Echildren;
		MKVType __003C_003ETrackTranslate = ___003C_003ETrackTranslate;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren16.put(__003C_003ETrackTranslate, new HashSet(Arrays.asList(___003C_003ETrackTranslateEditionUID, ___003C_003ETrackTranslateCodec, ___003C_003ETrackTranslateTrackID)));
		Map __003C_003Echildren17 = ___003C_003Echildren;
		MKVType __003C_003EVideo = ___003C_003EVideo;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren17.put(__003C_003EVideo, new HashSet(Arrays.asList(___003C_003EFlagInterlaced, ___003C_003EStereoMode, ___003C_003EAlphaMode, ___003C_003EPixelWidth, ___003C_003EPixelHeight, ___003C_003EPixelCropBottom, ___003C_003EPixelCropTop, ___003C_003EPixelCropLeft, ___003C_003EPixelCropRight, ___003C_003EDisplayWidth, ___003C_003EDisplayHeight, ___003C_003EDisplayUnit, ___003C_003EAspectRatioType, ___003C_003EColourSpace)));
		Map __003C_003Echildren18 = ___003C_003Echildren;
		MKVType __003C_003EAudio = ___003C_003EAudio;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren18.put(__003C_003EAudio, new HashSet(Arrays.asList(___003C_003ESamplingFrequency, ___003C_003EOutputSamplingFrequency, ___003C_003EChannels, ___003C_003EBitDepth)));
		Map __003C_003Echildren19 = ___003C_003Echildren;
		MKVType __003C_003ETrackOperation = ___003C_003ETrackOperation;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren19.put(__003C_003ETrackOperation, new HashSet(Arrays.asList(___003C_003ETrackCombinePlanes, ___003C_003ETrackJoinBlocks)));
		Map __003C_003Echildren20 = ___003C_003Echildren;
		MKVType __003C_003ETrackCombinePlanes = ___003C_003ETrackCombinePlanes;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren20.put(__003C_003ETrackCombinePlanes, new HashSet(Arrays.asList(___003C_003ETrackPlane)));
		Map __003C_003Echildren21 = ___003C_003Echildren;
		MKVType __003C_003ETrackPlane = ___003C_003ETrackPlane;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren21.put(__003C_003ETrackPlane, new HashSet(Arrays.asList(___003C_003ETrackPlaneUID, ___003C_003ETrackPlaneType)));
		Map __003C_003Echildren22 = ___003C_003Echildren;
		MKVType __003C_003ETrackJoinBlocks = ___003C_003ETrackJoinBlocks;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren22.put(__003C_003ETrackJoinBlocks, new HashSet(Arrays.asList(___003C_003ETrackJoinUID)));
		Map __003C_003Echildren23 = ___003C_003Echildren;
		MKVType __003C_003EContentEncodings = ___003C_003EContentEncodings;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren23.put(__003C_003EContentEncodings, new HashSet(Arrays.asList(___003C_003EContentEncoding)));
		Map __003C_003Echildren24 = ___003C_003Echildren;
		MKVType __003C_003EContentEncoding = ___003C_003EContentEncoding;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren24.put(__003C_003EContentEncoding, new HashSet(Arrays.asList(___003C_003EContentEncodingOrder, ___003C_003EContentEncodingScope, ___003C_003EContentEncodingType, ___003C_003EContentCompression, ___003C_003EContentEncryption)));
		Map __003C_003Echildren25 = ___003C_003Echildren;
		MKVType __003C_003EContentCompression = ___003C_003EContentCompression;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren25.put(__003C_003EContentCompression, new HashSet(Arrays.asList(___003C_003EContentCompAlgo, ___003C_003EContentCompSettings)));
		Map __003C_003Echildren26 = ___003C_003Echildren;
		MKVType __003C_003EContentEncryption = ___003C_003EContentEncryption;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren26.put(__003C_003EContentEncryption, new HashSet(Arrays.asList(___003C_003EContentEncAlgo, ___003C_003EContentEncKeyID, ___003C_003EContentSignature, ___003C_003EContentSigKeyID, ___003C_003EContentSigAlgo, ___003C_003EContentSigHashAlgo)));
		Map __003C_003Echildren27 = ___003C_003Echildren;
		MKVType __003C_003ECues = ___003C_003ECues;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren27.put(__003C_003ECues, new HashSet(Arrays.asList(___003C_003ECuePoint)));
		Map __003C_003Echildren28 = ___003C_003Echildren;
		MKVType __003C_003ECuePoint = ___003C_003ECuePoint;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren28.put(__003C_003ECuePoint, new HashSet(Arrays.asList(___003C_003ECueTime, ___003C_003ECueTrackPositions)));
		Map __003C_003Echildren29 = ___003C_003Echildren;
		MKVType __003C_003ECueTrackPositions = ___003C_003ECueTrackPositions;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren29.put(__003C_003ECueTrackPositions, new HashSet(Arrays.asList(___003C_003ECueTrack, ___003C_003ECueClusterPosition, ___003C_003ECueRelativePosition, ___003C_003ECueDuration, ___003C_003ECueBlockNumber, ___003C_003ECueCodecState, ___003C_003ECueReference)));
		Map __003C_003Echildren30 = ___003C_003Echildren;
		MKVType __003C_003ECueReference = ___003C_003ECueReference;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren30.put(__003C_003ECueReference, new HashSet(Arrays.asList(___003C_003ECueRefTime)));
		Map __003C_003Echildren31 = ___003C_003Echildren;
		MKVType __003C_003EAttachments = ___003C_003EAttachments;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren31.put(__003C_003EAttachments, new HashSet(Arrays.asList(___003C_003EAttachedFile)));
		Map __003C_003Echildren32 = ___003C_003Echildren;
		MKVType __003C_003EAttachedFile = ___003C_003EAttachedFile;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren32.put(__003C_003EAttachedFile, new HashSet(Arrays.asList(___003C_003EFileDescription, ___003C_003EFileName, ___003C_003EFileMimeType, ___003C_003EFileData, ___003C_003EFileUID)));
		Map __003C_003Echildren33 = ___003C_003Echildren;
		MKVType __003C_003EChapters = ___003C_003EChapters;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren33.put(__003C_003EChapters, new HashSet(Arrays.asList(___003C_003EEditionEntry)));
		Map __003C_003Echildren34 = ___003C_003Echildren;
		MKVType __003C_003EEditionEntry = ___003C_003EEditionEntry;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren34.put(__003C_003EEditionEntry, new HashSet(Arrays.asList(___003C_003EEditionUID, ___003C_003EEditionFlagHidden, ___003C_003EEditionFlagDefault, ___003C_003EEditionFlagOrdered, ___003C_003EChapterAtom)));
		Map __003C_003Echildren35 = ___003C_003Echildren;
		MKVType __003C_003EChapterAtom = ___003C_003EChapterAtom;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren35.put(__003C_003EChapterAtom, new HashSet(Arrays.asList(___003C_003EChapterUID, ___003C_003EChapterStringUID, ___003C_003EChapterTimeStart, ___003C_003EChapterTimeEnd, ___003C_003EChapterFlagHidden, ___003C_003EChapterFlagEnabled, ___003C_003EChapterSegmentUID, ___003C_003EChapterSegmentEditionUID, ___003C_003EChapterPhysicalEquiv, ___003C_003EChapterTrack, ___003C_003EChapterDisplay, ___003C_003EChapProcess)));
		Map __003C_003Echildren36 = ___003C_003Echildren;
		MKVType __003C_003EChapterTrack = ___003C_003EChapterTrack;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren36.put(__003C_003EChapterTrack, new HashSet(Arrays.asList(___003C_003EChapterTrackNumber)));
		Map __003C_003Echildren37 = ___003C_003Echildren;
		MKVType __003C_003EChapterDisplay = ___003C_003EChapterDisplay;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren37.put(__003C_003EChapterDisplay, new HashSet(Arrays.asList(___003C_003EChapString, ___003C_003EChapLanguage, ___003C_003EChapCountry)));
		Map __003C_003Echildren38 = ___003C_003Echildren;
		MKVType __003C_003EChapProcess = ___003C_003EChapProcess;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren38.put(__003C_003EChapProcess, new HashSet(Arrays.asList(___003C_003EChapProcessCodecID, ___003C_003EChapProcessPrivate, ___003C_003EChapProcessCommand)));
		Map __003C_003Echildren39 = ___003C_003Echildren;
		MKVType __003C_003EChapProcessCommand = ___003C_003EChapProcessCommand;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren39.put(__003C_003EChapProcessCommand, new HashSet(Arrays.asList(___003C_003EChapProcessTime, ___003C_003EChapProcessData)));
		Map __003C_003Echildren40 = ___003C_003Echildren;
		MKVType __003C_003ETags = ___003C_003ETags;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren40.put(__003C_003ETags, new HashSet(Arrays.asList(___003C_003ETag)));
		Map __003C_003Echildren41 = ___003C_003Echildren;
		MKVType __003C_003ETag = ___003C_003ETag;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren41.put(__003C_003ETag, new HashSet(Arrays.asList(___003C_003ETargets, ___003C_003ESimpleTag)));
		Map __003C_003Echildren42 = ___003C_003Echildren;
		MKVType __003C_003ETargets = ___003C_003ETargets;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren42.put(__003C_003ETargets, new HashSet(Arrays.asList(___003C_003ETargetTypeValue, ___003C_003ETargetType, ___003C_003ETagTrackUID, ___003C_003ETagEditionUID, ___003C_003ETagChapterUID, ___003C_003ETagAttachmentUID)));
		Map __003C_003Echildren43 = ___003C_003Echildren;
		MKVType __003C_003ESimpleTag = ___003C_003ESimpleTag;
		HashSet.___003Cclinit_003E();
		__003C_003Echildren43.put(__003C_003ESimpleTag, new HashSet(Arrays.asList(___003C_003ETagName, ___003C_003ETagLanguage, ___003C_003ETagDefault, ___003C_003ETagString, ___003C_003ETagBinary)));
	}
}
