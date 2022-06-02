using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.platform;

namespace org.jcodec.codecs.h264.io.model;

public class SeqParameterSet : Object
{
	public int picOrderCntType;

	public bool fieldPicFlag;

	public bool deltaPicOrderAlwaysZeroFlag;

	public bool mbAdaptiveFrameFieldFlag;

	public bool direct8x8InferenceFlag;

	public ColorSpace chromaFormatIdc;

	public int log2MaxFrameNumMinus4;

	public int log2MaxPicOrderCntLsbMinus4;

	public int picHeightInMapUnitsMinus1;

	public int picWidthInMbsMinus1;

	public int bitDepthLumaMinus8;

	public int bitDepthChromaMinus8;

	public bool qpprimeYZeroTransformBypassFlag;

	public int profileIdc;

	public bool constraintSet0Flag;

	public bool constraintSet1Flag;

	public bool constraintSet2Flag;

	public bool constraintSet3Flag;

	public bool constraintSet4Flag;

	public bool constraintSet5Flag;

	public int levelIdc;

	public int seqParameterSetId;

	public bool separateColourPlaneFlag;

	public int offsetForNonRefPic;

	public int offsetForTopToBottomField;

	public int numRefFrames;

	public bool gapsInFrameNumValueAllowedFlag;

	public bool frameMbsOnlyFlag;

	public bool frameCroppingFlag;

	public int frameCropLeftOffset;

	public int frameCropRightOffset;

	public int frameCropTopOffset;

	public int frameCropBottomOffset;

	public int[] offsetForRefFrame;

	public VUIParameters vuiParams;

	public int[][] scalingMatrix;

	public int numRefFramesInPicOrderCntCycle;

	[LineNumberTable(new byte[] { 158, 222, 98, 122 })]
	public static int getPicHeightInMbs(SeqParameterSet sps)
	{
		return sps.picHeightInMapUnitsMinus1 + 1 << ((!sps.frameMbsOnlyFlag) ? 1 : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 162, 104, 135, 115, 114, 114, 114, 114,
		114, 114, 110, 115, 146, 127, 16, 119, 110, 146,
		114, 114, 114, 109, 100, 136, 99, 140, 114, 114,
		105, 119, 109, 114, 114, 114, 114, 114, 108, 63,
		20, 199, 114, 114, 114, 114, 114, 105, 146, 114,
		114, 105, 114, 114, 114, 146, 110, 101, 141
	})]
	public static SeqParameterSet read(ByteBuffer @is)
	{
		BitReader _in = BitReader.createBitReader(@is);
		SeqParameterSet sps = new SeqParameterSet();
		sps.profileIdc = CAVLCReader.readNBit(_in, 8, "SPS: profile_idc");
		sps.constraintSet0Flag = CAVLCReader.readBool(_in, "SPS: constraint_set_0_flag");
		sps.constraintSet1Flag = CAVLCReader.readBool(_in, "SPS: constraint_set_1_flag");
		sps.constraintSet2Flag = CAVLCReader.readBool(_in, "SPS: constraint_set_2_flag");
		sps.constraintSet3Flag = CAVLCReader.readBool(_in, "SPS: constraint_set_3_flag");
		sps.constraintSet4Flag = CAVLCReader.readBool(_in, "SPS: constraint_set_4_flag");
		sps.constraintSet5Flag = CAVLCReader.readBool(_in, "SPS: constraint_set_5_flag");
		CAVLCReader.readNBit(_in, 2, "SPS: reserved_zero_2bits");
		sps.levelIdc = CAVLCReader.readNBit(_in, 8, "SPS: level_idc");
		sps.seqParameterSetId = CAVLCReader.readUEtrace(_in, "SPS: seq_parameter_set_id");
		if (sps.profileIdc == 100 || sps.profileIdc == 110 || sps.profileIdc == 122 || sps.profileIdc == 144)
		{
			sps.chromaFormatIdc = getColor(CAVLCReader.readUEtrace(_in, "SPS: chroma_format_idc"));
			if (sps.chromaFormatIdc == ColorSpace.___003C_003EYUV444)
			{
				sps.separateColourPlaneFlag = CAVLCReader.readBool(_in, "SPS: separate_colour_plane_flag");
			}
			sps.bitDepthLumaMinus8 = CAVLCReader.readUEtrace(_in, "SPS: bit_depth_luma_minus8");
			sps.bitDepthChromaMinus8 = CAVLCReader.readUEtrace(_in, "SPS: bit_depth_chroma_minus8");
			sps.qpprimeYZeroTransformBypassFlag = CAVLCReader.readBool(_in, "SPS: qpprime_y_zero_transform_bypass_flag");
			if (CAVLCReader.readBool(_in, "SPS: seq_scaling_matrix_present_lag"))
			{
				readScalingListMatrix(_in, sps);
			}
		}
		else
		{
			sps.chromaFormatIdc = ColorSpace.___003C_003EYUV420J;
		}
		sps.log2MaxFrameNumMinus4 = CAVLCReader.readUEtrace(_in, "SPS: log2_max_frame_num_minus4");
		sps.picOrderCntType = CAVLCReader.readUEtrace(_in, "SPS: pic_order_cnt_type");
		if (sps.picOrderCntType == 0)
		{
			sps.log2MaxPicOrderCntLsbMinus4 = CAVLCReader.readUEtrace(_in, "SPS: log2_max_pic_order_cnt_lsb_minus4");
		}
		else if (sps.picOrderCntType == 1)
		{
			sps.deltaPicOrderAlwaysZeroFlag = CAVLCReader.readBool(_in, "SPS: delta_pic_order_always_zero_flag");
			sps.offsetForNonRefPic = CAVLCReader.readSE(_in, "SPS: offset_for_non_ref_pic");
			sps.offsetForTopToBottomField = CAVLCReader.readSE(_in, "SPS: offset_for_top_to_bottom_field");
			sps.numRefFramesInPicOrderCntCycle = CAVLCReader.readUEtrace(_in, "SPS: num_ref_frames_in_pic_order_cnt_cycle");
			sps.offsetForRefFrame = new int[sps.numRefFramesInPicOrderCntCycle];
			for (int i = 0; i < sps.numRefFramesInPicOrderCntCycle; i++)
			{
				sps.offsetForRefFrame[i] = CAVLCReader.readSE(_in, new StringBuilder().append("SPS: offsetForRefFrame [").append(i).append("]")
					.toString());
			}
		}
		sps.numRefFrames = CAVLCReader.readUEtrace(_in, "SPS: num_ref_frames");
		sps.gapsInFrameNumValueAllowedFlag = CAVLCReader.readBool(_in, "SPS: gaps_in_frame_num_value_allowed_flag");
		sps.picWidthInMbsMinus1 = CAVLCReader.readUEtrace(_in, "SPS: pic_width_in_mbs_minus1");
		sps.picHeightInMapUnitsMinus1 = CAVLCReader.readUEtrace(_in, "SPS: pic_height_in_map_units_minus1");
		sps.frameMbsOnlyFlag = CAVLCReader.readBool(_in, "SPS: frame_mbs_only_flag");
		if (!sps.frameMbsOnlyFlag)
		{
			sps.mbAdaptiveFrameFieldFlag = CAVLCReader.readBool(_in, "SPS: mb_adaptive_frame_field_flag");
		}
		sps.direct8x8InferenceFlag = CAVLCReader.readBool(_in, "SPS: direct_8x8_inference_flag");
		sps.frameCroppingFlag = CAVLCReader.readBool(_in, "SPS: frame_cropping_flag");
		if (sps.frameCroppingFlag)
		{
			sps.frameCropLeftOffset = CAVLCReader.readUEtrace(_in, "SPS: frame_crop_left_offset");
			sps.frameCropRightOffset = CAVLCReader.readUEtrace(_in, "SPS: frame_crop_right_offset");
			sps.frameCropTopOffset = CAVLCReader.readUEtrace(_in, "SPS: frame_crop_top_offset");
			sps.frameCropBottomOffset = CAVLCReader.readUEtrace(_in, "SPS: frame_crop_bottom_offset");
		}
		if (CAVLCReader.readBool(_in, "SPS: vui_parameters_present_flag"))
		{
			sps.vuiParams = readVUIParameters(_in);
		}
		return sps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 41, 66, 136, 116, 114, 114, 114, 114, 114,
		114, 111, 116, 146, 127, 16, 119, 110, 146, 114,
		114, 114, 120, 105, 103, 122, 107, 238, 61, 231,
		71, 114, 114, 105, 119, 109, 114, 114, 114, 115,
		109, 52, 167, 114, 114, 114, 114, 114, 105, 146,
		114, 114, 105, 114, 114, 114, 146, 120, 105, 142,
		105
	})]
	public virtual void write(ByteBuffer @out)
	{
		BitWriter writer = new BitWriter(@out);
		CAVLCWriter.writeNBit(writer, profileIdc, 8, "SPS: profile_idc");
		CAVLCWriter.writeBool(writer, constraintSet0Flag, "SPS: constraint_set_0_flag");
		CAVLCWriter.writeBool(writer, constraintSet1Flag, "SPS: constraint_set_1_flag");
		CAVLCWriter.writeBool(writer, constraintSet2Flag, "SPS: constraint_set_2_flag");
		CAVLCWriter.writeBool(writer, constraintSet3Flag, "SPS: constraint_set_3_flag");
		CAVLCWriter.writeBool(writer, constraintSet4Flag, "SPS: constraint_set_4_flag");
		CAVLCWriter.writeBool(writer, constraintSet5Flag, "SPS: constraint_set_5_flag");
		CAVLCWriter.writeNBit(writer, 0L, 2, "SPS: reserved");
		CAVLCWriter.writeNBit(writer, levelIdc, 8, "SPS: level_idc");
		CAVLCWriter.writeUEtrace(writer, seqParameterSetId, "SPS: seq_parameter_set_id");
		if (profileIdc == 100 || profileIdc == 110 || profileIdc == 122 || profileIdc == 144)
		{
			CAVLCWriter.writeUEtrace(writer, fromColor(chromaFormatIdc), "SPS: chroma_format_idc");
			if (chromaFormatIdc == ColorSpace.___003C_003EYUV444)
			{
				CAVLCWriter.writeBool(writer, separateColourPlaneFlag, "SPS: residual_color_transform_flag");
			}
			CAVLCWriter.writeUEtrace(writer, bitDepthLumaMinus8, "SPS: ");
			CAVLCWriter.writeUEtrace(writer, bitDepthChromaMinus8, "SPS: ");
			CAVLCWriter.writeBool(writer, qpprimeYZeroTransformBypassFlag, "SPS: qpprime_y_zero_transform_bypass_flag");
			CAVLCWriter.writeBool(writer, scalingMatrix != null, "SPS: ");
			if (scalingMatrix != null)
			{
				for (int j = 0; j < 8; j++)
				{
					CAVLCWriter.writeBool(writer, scalingMatrix[j] != null, "SPS: ");
					if (scalingMatrix[j] != null)
					{
						writeScalingList(writer, scalingMatrix, j);
					}
				}
			}
		}
		CAVLCWriter.writeUEtrace(writer, log2MaxFrameNumMinus4, "SPS: log2_max_frame_num_minus4");
		CAVLCWriter.writeUEtrace(writer, picOrderCntType, "SPS: pic_order_cnt_type");
		if (picOrderCntType == 0)
		{
			CAVLCWriter.writeUEtrace(writer, log2MaxPicOrderCntLsbMinus4, "SPS: log2_max_pic_order_cnt_lsb_minus4");
		}
		else if (picOrderCntType == 1)
		{
			CAVLCWriter.writeBool(writer, deltaPicOrderAlwaysZeroFlag, "SPS: delta_pic_order_always_zero_flag");
			CAVLCWriter.writeSEtrace(writer, offsetForNonRefPic, "SPS: offset_for_non_ref_pic");
			CAVLCWriter.writeSEtrace(writer, offsetForTopToBottomField, "SPS: offset_for_top_to_bottom_field");
			CAVLCWriter.writeUEtrace(writer, offsetForRefFrame.Length, "SPS: ");
			for (int i = 0; i < (nint)offsetForRefFrame.LongLength; i++)
			{
				CAVLCWriter.writeSEtrace(writer, offsetForRefFrame[i], "SPS: ");
			}
		}
		CAVLCWriter.writeUEtrace(writer, numRefFrames, "SPS: num_ref_frames");
		CAVLCWriter.writeBool(writer, gapsInFrameNumValueAllowedFlag, "SPS: gaps_in_frame_num_value_allowed_flag");
		CAVLCWriter.writeUEtrace(writer, picWidthInMbsMinus1, "SPS: pic_width_in_mbs_minus1");
		CAVLCWriter.writeUEtrace(writer, picHeightInMapUnitsMinus1, "SPS: pic_height_in_map_units_minus1");
		CAVLCWriter.writeBool(writer, frameMbsOnlyFlag, "SPS: frame_mbs_only_flag");
		if (!frameMbsOnlyFlag)
		{
			CAVLCWriter.writeBool(writer, mbAdaptiveFrameFieldFlag, "SPS: mb_adaptive_frame_field_flag");
		}
		CAVLCWriter.writeBool(writer, direct8x8InferenceFlag, "SPS: direct_8x8_inference_flag");
		CAVLCWriter.writeBool(writer, frameCroppingFlag, "SPS: frame_cropping_flag");
		if (frameCroppingFlag)
		{
			CAVLCWriter.writeUEtrace(writer, frameCropLeftOffset, "SPS: frame_crop_left_offset");
			CAVLCWriter.writeUEtrace(writer, frameCropRightOffset, "SPS: frame_crop_right_offset");
			CAVLCWriter.writeUEtrace(writer, frameCropTopOffset, "SPS: frame_crop_top_offset");
			CAVLCWriter.writeUEtrace(writer, frameCropBottomOffset, "SPS: frame_crop_bottom_offset");
		}
		CAVLCWriter.writeBool(writer, vuiParams != null, "SPS: ");
		if (vuiParams != null)
		{
			writeVUIParameters(vuiParams, writer);
		}
		CAVLCWriter.writeTrailingBits(writer);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public SeqParameterSet()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 70, 162, 104, 99, 99, 106, 100, 110, 123,
		103, 131, 107, 229, 56, 234, 74
	})]
	public static int[] readScalingList(BitReader src, int sizeOfScalingList)
	{
		int[] scalingList = new int[sizeOfScalingList];
		int lastScale = 8;
		int nextScale = 8;
		for (int i = 0; i < sizeOfScalingList; i++)
		{
			if (nextScale != 0)
			{
				int deltaScale = CAVLCReader.readSE(src, "deltaScale");
				int num = lastScale + deltaScale + 256;
				nextScale = ((256 != -1) ? (num % 256) : 0);
				if (i == 0 && nextScale == 0)
				{
					return null;
				}
			}
			scalingList[i] = ((nextScale != 0) ? nextScale : lastScale);
			lastScale = scalingList[i];
		}
		return scalingList;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 162, 99, 159, 13, 111, 163, 109, 131,
		111, 163, 109, 131, 111, 131, 175, 133, 100, 112,
		162, 99, 99, 106, 100, 111, 142, 230, 59, 233,
		71
	})]
	public static void writeScalingList(BitWriter @out, int[][] scalingMatrix, int which)
	{
		int useDefaultScalingMatrixFlag = 0;
		switch (which)
		{
		case 0:
			useDefaultScalingMatrixFlag = (Platform.arrayEqualsInt(scalingMatrix[which], H264Const.___003C_003EdefaultScalingList4x4Intra) ? 1 : 0);
			break;
		case 1:
		case 2:
			useDefaultScalingMatrixFlag = (Platform.arrayEqualsInt(scalingMatrix[which], scalingMatrix[0]) ? 1 : 0);
			break;
		case 3:
			useDefaultScalingMatrixFlag = (Platform.arrayEqualsInt(scalingMatrix[which], H264Const.___003C_003EdefaultScalingList4x4Inter) ? 1 : 0);
			break;
		case 4:
		case 5:
			useDefaultScalingMatrixFlag = (Platform.arrayEqualsInt(scalingMatrix[which], scalingMatrix[3]) ? 1 : 0);
			break;
		case 6:
			useDefaultScalingMatrixFlag = (Platform.arrayEqualsInt(scalingMatrix[which], H264Const.___003C_003EdefaultScalingList8x8Intra) ? 1 : 0);
			break;
		case 7:
			useDefaultScalingMatrixFlag = (Platform.arrayEqualsInt(scalingMatrix[which], H264Const.___003C_003EdefaultScalingList8x8Inter) ? 1 : 0);
			break;
		}
		int[] scalingList = scalingMatrix[which];
		if (useDefaultScalingMatrixFlag != 0)
		{
			CAVLCWriter.writeSEtrace(@out, -8, "SPS: ");
			return;
		}
		int lastScale = 8;
		int nextScale = 8;
		for (int i = 0; i < (nint)scalingList.LongLength; i++)
		{
			if (nextScale != 0)
			{
				int deltaScale = scalingList[i] - lastScale - 256;
				CAVLCWriter.writeSEtrace(@out, deltaScale, "SPS: ");
			}
			lastScale = scalingList[i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 66, 153, 135, 135, 135, 135 })]
	public static ColorSpace getColor(int id)
	{
		switch (id)
		{
		case 0:
			return ColorSpace.___003C_003EMONO;
		case 1:
			return ColorSpace.___003C_003EYUV420J;
		case 2:
			return ColorSpace.___003C_003EYUV422;
		case 3:
			return ColorSpace.___003C_003EYUV444;
		default:
			
			throw new RuntimeException("Colorspace not supported");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 65, 98, 109, 103, 109, 100, 108, 240, 60,
		231, 71
	})]
	private static void readScalingListMatrix(BitReader src, SeqParameterSet sps)
	{
		sps.scalingMatrix = new int[8][];
		for (int i = 0; i < 8; i++)
		{
			if (CAVLCReader.readBool(src, "SPS: seqScalingListPresentFlag"))
			{
				int scalingListSize = ((i >= 6) ? 64 : 16);
				sps.scalingMatrix[i] = readScalingList(src, scalingListSize);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 62, 66, 103, 114, 105, 120, 110, 116, 180,
		114, 105, 146, 114, 108, 115, 114, 114, 105, 115,
		115, 179, 114, 105, 114, 146, 114, 105, 116, 116,
		146, 109, 100, 109, 109, 100, 109, 103, 146, 114,
		109, 103, 108, 151, 119, 119, 119, 119, 119, 183
	})]
	private static VUIParameters readVUIParameters(BitReader _in)
	{
		VUIParameters vuip = new VUIParameters();
		vuip.aspectRatioInfoPresentFlag = CAVLCReader.readBool(_in, "VUI: aspect_ratio_info_present_flag");
		if (vuip.aspectRatioInfoPresentFlag)
		{
			vuip.aspectRatio = AspectRatio.fromValue(CAVLCReader.readNBit(_in, 8, "VUI: aspect_ratio"));
			if (vuip.aspectRatio == AspectRatio.___003C_003EExtended_SAR)
			{
				vuip.sarWidth = CAVLCReader.readNBit(_in, 16, "VUI: sar_width");
				vuip.sarHeight = CAVLCReader.readNBit(_in, 16, "VUI: sar_height");
			}
		}
		vuip.overscanInfoPresentFlag = CAVLCReader.readBool(_in, "VUI: overscan_info_present_flag");
		if (vuip.overscanInfoPresentFlag)
		{
			vuip.overscanAppropriateFlag = CAVLCReader.readBool(_in, "VUI: overscan_appropriate_flag");
		}
		vuip.videoSignalTypePresentFlag = CAVLCReader.readBool(_in, "VUI: video_signal_type_present_flag");
		if (vuip.videoSignalTypePresentFlag)
		{
			vuip.videoFormat = CAVLCReader.readNBit(_in, 3, "VUI: video_format");
			vuip.videoFullRangeFlag = CAVLCReader.readBool(_in, "VUI: video_full_range_flag");
			vuip.colourDescriptionPresentFlag = CAVLCReader.readBool(_in, "VUI: colour_description_present_flag");
			if (vuip.colourDescriptionPresentFlag)
			{
				vuip.colourPrimaries = CAVLCReader.readNBit(_in, 8, "VUI: colour_primaries");
				vuip.transferCharacteristics = CAVLCReader.readNBit(_in, 8, "VUI: transfer_characteristics");
				vuip.matrixCoefficients = CAVLCReader.readNBit(_in, 8, "VUI: matrix_coefficients");
			}
		}
		vuip.chromaLocInfoPresentFlag = CAVLCReader.readBool(_in, "VUI: chroma_loc_info_present_flag");
		if (vuip.chromaLocInfoPresentFlag)
		{
			vuip.chromaSampleLocTypeTopField = CAVLCReader.readUEtrace(_in, "VUI chroma_sample_loc_type_top_field");
			vuip.chromaSampleLocTypeBottomField = CAVLCReader.readUEtrace(_in, "VUI chroma_sample_loc_type_bottom_field");
		}
		vuip.timingInfoPresentFlag = CAVLCReader.readBool(_in, "VUI: timing_info_present_flag");
		if (vuip.timingInfoPresentFlag)
		{
			vuip.numUnitsInTick = CAVLCReader.readNBit(_in, 32, "VUI: num_units_in_tick");
			vuip.timeScale = CAVLCReader.readNBit(_in, 32, "VUI: time_scale");
			vuip.fixedFrameRateFlag = CAVLCReader.readBool(_in, "VUI: fixed_frame_rate_flag");
		}
		int nalHRDParametersPresentFlag = (CAVLCReader.readBool(_in, "VUI: nal_hrd_parameters_present_flag") ? 1 : 0);
		if (nalHRDParametersPresentFlag != 0)
		{
			vuip.nalHRDParams = readHRDParameters(_in);
		}
		int vclHRDParametersPresentFlag = (CAVLCReader.readBool(_in, "VUI: vcl_hrd_parameters_present_flag") ? 1 : 0);
		if (vclHRDParametersPresentFlag != 0)
		{
			vuip.vclHRDParams = readHRDParameters(_in);
		}
		if (nalHRDParametersPresentFlag != 0 || vclHRDParametersPresentFlag != 0)
		{
			vuip.lowDelayHrdFlag = CAVLCReader.readBool(_in, "VUI: low_delay_hrd_flag");
		}
		vuip.picStructPresentFlag = CAVLCReader.readBool(_in, "VUI: pic_struct_present_flag");
		if (CAVLCReader.readBool(_in, "VUI: bitstream_restriction_flag"))
		{
			vuip.bitstreamRestriction = new VUIParameters.BitstreamRestriction();
			vuip.bitstreamRestriction.motionVectorsOverPicBoundariesFlag = CAVLCReader.readBool(_in, "VUI: motion_vectors_over_pic_boundaries_flag");
			vuip.bitstreamRestriction.maxBytesPerPicDenom = CAVLCReader.readUEtrace(_in, "VUI max_bytes_per_pic_denom");
			vuip.bitstreamRestriction.maxBitsPerMbDenom = CAVLCReader.readUEtrace(_in, "VUI max_bits_per_mb_denom");
			vuip.bitstreamRestriction.log2MaxMvLengthHorizontal = CAVLCReader.readUEtrace(_in, "VUI log2_max_mv_length_horizontal");
			vuip.bitstreamRestriction.log2MaxMvLengthVertical = CAVLCReader.readUEtrace(_in, "VUI log2_max_mv_length_vertical");
			vuip.bitstreamRestriction.numReorderFrames = CAVLCReader.readUEtrace(_in, "VUI num_reorder_frames");
			vuip.bitstreamRestriction.maxDecFrameBuffering = CAVLCReader.readUEtrace(_in, "VUI max_dec_frame_buffering");
		}
		return vuip;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 47, 130, 103, 114, 115, 115, 116, 116, 148,
		108, 116, 116, 244, 61, 231, 69, 147, 115, 115,
		115
	})]
	private static HRDParameters readHRDParameters(BitReader _in)
	{
		HRDParameters hrd = new HRDParameters();
		hrd.cpbCntMinus1 = CAVLCReader.readUEtrace(_in, "SPS: cpb_cnt_minus1");
		hrd.bitRateScale = CAVLCReader.readNBit(_in, 4, "HRD: bit_rate_scale");
		hrd.cpbSizeScale = CAVLCReader.readNBit(_in, 4, "HRD: cpb_size_scale");
		hrd.bitRateValueMinus1 = new int[hrd.cpbCntMinus1 + 1];
		hrd.cpbSizeValueMinus1 = new int[hrd.cpbCntMinus1 + 1];
		hrd.cbrFlag = new bool[hrd.cpbCntMinus1 + 1];
		for (int SchedSelIdx = 0; SchedSelIdx <= hrd.cpbCntMinus1; SchedSelIdx++)
		{
			hrd.bitRateValueMinus1[SchedSelIdx] = CAVLCReader.readUEtrace(_in, "HRD: bit_rate_value_minus1");
			hrd.cpbSizeValueMinus1[SchedSelIdx] = CAVLCReader.readUEtrace(_in, "HRD: cpb_size_value_minus1");
			hrd.cbrFlag[SchedSelIdx] = CAVLCReader.readBool(_in, "HRD: cbr_flag");
		}
		hrd.initialCpbRemovalDelayLengthMinus1 = CAVLCReader.readNBit(_in, 5, "HRD: initial_cpb_removal_delay_length_minus1");
		hrd.cpbRemovalDelayLengthMinus1 = CAVLCReader.readNBit(_in, 5, "HRD: cpb_removal_delay_length_minus1");
		hrd.dpbOutputDelayLengthMinus1 = CAVLCReader.readNBit(_in, 5, "HRD: dpb_output_delay_length_minus1");
		hrd.timeOffsetLength = CAVLCReader.readNBit(_in, 5, "HRD: time_offset_length");
		return hrd;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 130, 105, 99, 105, 99, 105, 99, 105,
		131
	})]
	public static int fromColor(ColorSpace color)
	{
		if (color == ColorSpace.___003C_003EMONO)
		{
			return 0;
		}
		if (color == ColorSpace.___003C_003EYUV420J)
		{
			return 1;
		}
		if (color == ColorSpace.___003C_003EYUV422)
		{
			return 2;
		}
		if (color == ColorSpace.___003C_003EYUV444)
		{
			return 3;
		}
		
		throw new RuntimeException("Colorspace not supported");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 25, 130, 114, 105, 121, 110, 117, 181, 114,
		105, 146, 114, 108, 116, 114, 114, 105, 116, 116,
		180, 114, 105, 114, 146, 114, 105, 117, 117, 146,
		120, 105, 142, 120, 105, 174, 113, 146, 114, 120,
		108, 151, 119, 119, 151, 119, 119, 185
	})]
	private void writeVUIParameters(VUIParameters vuip, BitWriter writer)
	{
		CAVLCWriter.writeBool(writer, vuip.aspectRatioInfoPresentFlag, "VUI: aspect_ratio_info_present_flag");
		if (vuip.aspectRatioInfoPresentFlag)
		{
			CAVLCWriter.writeNBit(writer, vuip.aspectRatio.getValue(), 8, "VUI: aspect_ratio");
			if (vuip.aspectRatio == AspectRatio.___003C_003EExtended_SAR)
			{
				CAVLCWriter.writeNBit(writer, vuip.sarWidth, 16, "VUI: sar_width");
				CAVLCWriter.writeNBit(writer, vuip.sarHeight, 16, "VUI: sar_height");
			}
		}
		CAVLCWriter.writeBool(writer, vuip.overscanInfoPresentFlag, "VUI: overscan_info_present_flag");
		if (vuip.overscanInfoPresentFlag)
		{
			CAVLCWriter.writeBool(writer, vuip.overscanAppropriateFlag, "VUI: overscan_appropriate_flag");
		}
		CAVLCWriter.writeBool(writer, vuip.videoSignalTypePresentFlag, "VUI: video_signal_type_present_flag");
		if (vuip.videoSignalTypePresentFlag)
		{
			CAVLCWriter.writeNBit(writer, vuip.videoFormat, 3, "VUI: video_format");
			CAVLCWriter.writeBool(writer, vuip.videoFullRangeFlag, "VUI: video_full_range_flag");
			CAVLCWriter.writeBool(writer, vuip.colourDescriptionPresentFlag, "VUI: colour_description_present_flag");
			if (vuip.colourDescriptionPresentFlag)
			{
				CAVLCWriter.writeNBit(writer, vuip.colourPrimaries, 8, "VUI: colour_primaries");
				CAVLCWriter.writeNBit(writer, vuip.transferCharacteristics, 8, "VUI: transfer_characteristics");
				CAVLCWriter.writeNBit(writer, vuip.matrixCoefficients, 8, "VUI: matrix_coefficients");
			}
		}
		CAVLCWriter.writeBool(writer, vuip.chromaLocInfoPresentFlag, "VUI: chroma_loc_info_present_flag");
		if (vuip.chromaLocInfoPresentFlag)
		{
			CAVLCWriter.writeUEtrace(writer, vuip.chromaSampleLocTypeTopField, "VUI: chroma_sample_loc_type_top_field");
			CAVLCWriter.writeUEtrace(writer, vuip.chromaSampleLocTypeBottomField, "VUI: chroma_sample_loc_type_bottom_field");
		}
		CAVLCWriter.writeBool(writer, vuip.timingInfoPresentFlag, "VUI: timing_info_present_flag");
		if (vuip.timingInfoPresentFlag)
		{
			CAVLCWriter.writeNBit(writer, vuip.numUnitsInTick, 32, "VUI: num_units_in_tick");
			CAVLCWriter.writeNBit(writer, vuip.timeScale, 32, "VUI: time_scale");
			CAVLCWriter.writeBool(writer, vuip.fixedFrameRateFlag, "VUI: fixed_frame_rate_flag");
		}
		CAVLCWriter.writeBool(writer, vuip.nalHRDParams != null, "VUI: ");
		if (vuip.nalHRDParams != null)
		{
			writeHRDParameters(vuip.nalHRDParams, writer);
		}
		CAVLCWriter.writeBool(writer, vuip.vclHRDParams != null, "VUI: ");
		if (vuip.vclHRDParams != null)
		{
			writeHRDParameters(vuip.vclHRDParams, writer);
		}
		if (vuip.nalHRDParams != null || vuip.vclHRDParams != null)
		{
			CAVLCWriter.writeBool(writer, vuip.lowDelayHrdFlag, "VUI: low_delay_hrd_flag");
		}
		CAVLCWriter.writeBool(writer, vuip.picStructPresentFlag, "VUI: pic_struct_present_flag");
		CAVLCWriter.writeBool(writer, vuip.bitstreamRestriction != null, "VUI: ");
		if (vuip.bitstreamRestriction != null)
		{
			CAVLCWriter.writeBool(writer, vuip.bitstreamRestriction.motionVectorsOverPicBoundariesFlag, "VUI: motion_vectors_over_pic_boundaries_flag");
			CAVLCWriter.writeUEtrace(writer, vuip.bitstreamRestriction.maxBytesPerPicDenom, "VUI: max_bytes_per_pic_denom");
			CAVLCWriter.writeUEtrace(writer, vuip.bitstreamRestriction.maxBitsPerMbDenom, "VUI: max_bits_per_mb_denom");
			CAVLCWriter.writeUEtrace(writer, vuip.bitstreamRestriction.log2MaxMvLengthHorizontal, "VUI: log2_max_mv_length_horizontal");
			CAVLCWriter.writeUEtrace(writer, vuip.bitstreamRestriction.log2MaxMvLengthVertical, "VUI: log2_max_mv_length_vertical");
			CAVLCWriter.writeUEtrace(writer, vuip.bitstreamRestriction.numReorderFrames, "VUI: num_reorder_frames");
			CAVLCWriter.writeUEtrace(writer, vuip.bitstreamRestriction.maxDecFrameBuffering, "VUI: max_dec_frame_buffering");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 9, 98, 114, 116, 148, 108, 116, 116, 244,
		61, 231, 69, 148, 116, 116, 118
	})]
	private void writeHRDParameters(HRDParameters hrd, BitWriter writer)
	{
		CAVLCWriter.writeUEtrace(writer, hrd.cpbCntMinus1, "HRD: cpb_cnt_minus1");
		CAVLCWriter.writeNBit(writer, hrd.bitRateScale, 4, "HRD: bit_rate_scale");
		CAVLCWriter.writeNBit(writer, hrd.cpbSizeScale, 4, "HRD: cpb_size_scale");
		for (int SchedSelIdx = 0; SchedSelIdx <= hrd.cpbCntMinus1; SchedSelIdx++)
		{
			CAVLCWriter.writeUEtrace(writer, hrd.bitRateValueMinus1[SchedSelIdx], "HRD: ");
			CAVLCWriter.writeUEtrace(writer, hrd.cpbSizeValueMinus1[SchedSelIdx], "HRD: ");
			CAVLCWriter.writeBool(writer, hrd.cbrFlag[SchedSelIdx], "HRD: ");
		}
		CAVLCWriter.writeNBit(writer, hrd.initialCpbRemovalDelayLengthMinus1, 5, "HRD: initial_cpb_removal_delay_length_minus1");
		CAVLCWriter.writeNBit(writer, hrd.cpbRemovalDelayLengthMinus1, 5, "HRD: cpb_removal_delay_length_minus1");
		CAVLCWriter.writeNBit(writer, hrd.dpbOutputDelayLengthMinus1, 5, "HRD: dpb_output_delay_length_minus1");
		CAVLCWriter.writeNBit(writer, hrd.timeOffsetLength, 5, "HRD: time_offset_length");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 5, 130, 108, 104, 104 })]
	public virtual SeqParameterSet copy()
	{
		ByteBuffer buf = ByteBuffer.allocate(2048);
		write(buf);
		buf.flip();
		SeqParameterSet result = read(buf);
		
		return result;
	}

	[LineNumberTable(557)]
	public virtual int getPicOrderCntType()
	{
		return picOrderCntType;
	}

	[LineNumberTable(561)]
	public virtual bool isFieldPicFlag()
	{
		return fieldPicFlag;
	}

	[LineNumberTable(565)]
	public virtual bool isDeltaPicOrderAlwaysZeroFlag()
	{
		return deltaPicOrderAlwaysZeroFlag;
	}

	[LineNumberTable(569)]
	public virtual bool isMbAdaptiveFrameFieldFlag()
	{
		return mbAdaptiveFrameFieldFlag;
	}

	[LineNumberTable(573)]
	public virtual bool isDirect8x8InferenceFlag()
	{
		return direct8x8InferenceFlag;
	}

	[LineNumberTable(577)]
	public virtual ColorSpace getChromaFormatIdc()
	{
		return chromaFormatIdc;
	}

	[LineNumberTable(581)]
	public virtual int getLog2MaxFrameNumMinus4()
	{
		return log2MaxFrameNumMinus4;
	}

	[LineNumberTable(585)]
	public virtual int getLog2MaxPicOrderCntLsbMinus4()
	{
		return log2MaxPicOrderCntLsbMinus4;
	}

	[LineNumberTable(589)]
	public virtual int getPicHeightInMapUnitsMinus1()
	{
		return picHeightInMapUnitsMinus1;
	}

	[LineNumberTable(593)]
	public virtual int getPicWidthInMbsMinus1()
	{
		return picWidthInMbsMinus1;
	}

	[LineNumberTable(597)]
	public virtual int getBitDepthLumaMinus8()
	{
		return bitDepthLumaMinus8;
	}

	[LineNumberTable(601)]
	public virtual int getBitDepthChromaMinus8()
	{
		return bitDepthChromaMinus8;
	}

	[LineNumberTable(605)]
	public virtual bool isQpprimeYZeroTransformBypassFlag()
	{
		return qpprimeYZeroTransformBypassFlag;
	}

	[LineNumberTable(609)]
	public virtual int getProfileIdc()
	{
		return profileIdc;
	}

	[LineNumberTable(613)]
	public virtual bool isConstraintSet0Flag()
	{
		return constraintSet0Flag;
	}

	[LineNumberTable(617)]
	public virtual bool isConstraintSet1Flag()
	{
		return constraintSet1Flag;
	}

	[LineNumberTable(621)]
	public virtual bool isConstraintSet2Flag()
	{
		return constraintSet2Flag;
	}

	[LineNumberTable(625)]
	public virtual bool isConstraintSet3Flag()
	{
		return constraintSet3Flag;
	}

	[LineNumberTable(629)]
	public virtual bool isConstraintSet4Flag()
	{
		return constraintSet4Flag;
	}

	[LineNumberTable(633)]
	public virtual bool isConstraintSet5Flag()
	{
		return constraintSet5Flag;
	}

	[LineNumberTable(637)]
	public virtual int getLevelIdc()
	{
		return levelIdc;
	}

	[LineNumberTable(641)]
	public virtual int getSeqParameterSetId()
	{
		return seqParameterSetId;
	}

	[LineNumberTable(645)]
	public virtual bool isResidualColorTransformFlag()
	{
		return separateColourPlaneFlag;
	}

	[LineNumberTable(649)]
	public virtual int getOffsetForNonRefPic()
	{
		return offsetForNonRefPic;
	}

	[LineNumberTable(653)]
	public virtual int getOffsetForTopToBottomField()
	{
		return offsetForTopToBottomField;
	}

	[LineNumberTable(657)]
	public virtual int getNumRefFrames()
	{
		return numRefFrames;
	}

	[LineNumberTable(661)]
	public virtual bool isGapsInFrameNumValueAllowedFlag()
	{
		return gapsInFrameNumValueAllowedFlag;
	}

	[LineNumberTable(665)]
	public virtual bool isFrameMbsOnlyFlag()
	{
		return frameMbsOnlyFlag;
	}

	[LineNumberTable(669)]
	public virtual bool isFrameCroppingFlag()
	{
		return frameCroppingFlag;
	}

	[LineNumberTable(673)]
	public virtual int getFrameCropLeftOffset()
	{
		return frameCropLeftOffset;
	}

	[LineNumberTable(677)]
	public virtual int getFrameCropRightOffset()
	{
		return frameCropRightOffset;
	}

	[LineNumberTable(681)]
	public virtual int getFrameCropTopOffset()
	{
		return frameCropTopOffset;
	}

	[LineNumberTable(685)]
	public virtual int getFrameCropBottomOffset()
	{
		return frameCropBottomOffset;
	}

	[LineNumberTable(689)]
	public virtual int[] getOffsetForRefFrame()
	{
		return offsetForRefFrame;
	}

	[LineNumberTable(693)]
	public virtual VUIParameters getVuiParams()
	{
		return vuiParams;
	}

	[LineNumberTable(697)]
	public virtual int[][] getScalingMatrix()
	{
		return scalingMatrix;
	}

	[LineNumberTable(701)]
	public virtual int getNumRefFramesInPicOrderCntCycle()
	{
		return numRefFramesInPicOrderCntCycle;
	}
}
