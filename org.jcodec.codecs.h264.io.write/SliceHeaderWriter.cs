using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.io.write;

public class SliceHeaderWriter : java.lang.Object
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : java.lang.Object
	{
		[Modifiers(Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		internal static int[] _0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(141)]
		static _1()
		{
			_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType = new int[(nint)RefPicMarking.InstrType.values().LongLength];
			NoSuchFieldError noSuchFieldError2;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003EREMOVE_SHORT.ordinal()] = 1;
			}
			catch (System.Exception x)
			{
				NoSuchFieldError noSuchFieldError = ByteCodeHelper.MapException<NoSuchFieldError>(x, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError == null)
				{
					throw;
				}
				noSuchFieldError2 = noSuchFieldError;
				goto IL_0037;
			}
			goto IL_003d;
			IL_0037:
			NoSuchFieldError noSuchFieldError3 = noSuchFieldError2;
			goto IL_003d;
			IL_003d:
			NoSuchFieldError noSuchFieldError5;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003EREMOVE_LONG.ordinal()] = 2;
			}
			catch (System.Exception x2)
			{
				NoSuchFieldError noSuchFieldError4 = ByteCodeHelper.MapException<NoSuchFieldError>(x2, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError4 == null)
				{
					throw;
				}
				noSuchFieldError5 = noSuchFieldError4;
				goto IL_0062;
			}
			goto IL_0068;
			IL_0062:
			NoSuchFieldError noSuchFieldError6 = noSuchFieldError5;
			goto IL_0068;
			IL_0068:
			NoSuchFieldError noSuchFieldError8;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003ECONVERT_INTO_LONG.ordinal()] = 3;
			}
			catch (System.Exception x3)
			{
				NoSuchFieldError noSuchFieldError7 = ByteCodeHelper.MapException<NoSuchFieldError>(x3, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError7 == null)
				{
					throw;
				}
				noSuchFieldError8 = noSuchFieldError7;
				goto IL_008e;
			}
			goto IL_0096;
			IL_008e:
			NoSuchFieldError noSuchFieldError9 = noSuchFieldError8;
			goto IL_0096;
			IL_0096:
			NoSuchFieldError noSuchFieldError11;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003ETRUNK_LONG.ordinal()] = 4;
			}
			catch (System.Exception x4)
			{
				NoSuchFieldError noSuchFieldError10 = ByteCodeHelper.MapException<NoSuchFieldError>(x4, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError10 == null)
				{
					throw;
				}
				noSuchFieldError11 = noSuchFieldError10;
				goto IL_00bc;
			}
			goto IL_00c4;
			IL_00bc:
			NoSuchFieldError noSuchFieldError12 = noSuchFieldError11;
			goto IL_00c4;
			IL_00c4:
			NoSuchFieldError noSuchFieldError14;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003ECLEAR.ordinal()] = 5;
			}
			catch (System.Exception x5)
			{
				NoSuchFieldError noSuchFieldError13 = ByteCodeHelper.MapException<NoSuchFieldError>(x5, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError13 == null)
				{
					throw;
				}
				noSuchFieldError14 = noSuchFieldError13;
				goto IL_00ea;
			}
			goto IL_00f2;
			IL_00ea:
			NoSuchFieldError noSuchFieldError15 = noSuchFieldError14;
			goto IL_00f2;
			IL_00f2:
			NoSuchFieldError noSuchFieldError17;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003EMARK_LONG.ordinal()] = 6;
				return;
			}
			catch (System.Exception x6)
			{
				NoSuchFieldError noSuchFieldError16 = ByteCodeHelper.MapException<NoSuchFieldError>(x6, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError16 == null)
				{
					throw;
				}
				noSuchFieldError17 = noSuchFieldError16;
			}
			NoSuchFieldError noSuchFieldError18 = noSuchFieldError17;
		}

		_1()
		{
			throw null;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 129, 67, 104, 104, 114, 127, 5, 114,
		118, 159, 19, 122, 105, 114, 105, 178, 100, 146,
		108, 118, 159, 19, 117, 113, 178, 114, 116, 113,
		148, 105, 146, 110, 146, 159, 9, 114, 105, 116,
		110, 212, 104, 159, 26, 104, 100, 105, 118, 146,
		114, 123, 110, 146, 146, 105, 114, 106, 114, 178,
		127, 6, 159, 5, 159, 8, 133, 106, 176
	})]
	public static void write(SliceHeader sliceHeader, bool idrSlice, int nalRefIdc, BitWriter writer)
	{
		SeqParameterSet sps = sliceHeader.sps;
		PictureParameterSet pps = sliceHeader.pps;
		CAVLCWriter.writeUEtrace(writer, sliceHeader.firstMbInSlice, "SH: first_mb_in_slice");
		CAVLCWriter.writeUEtrace(writer, sliceHeader.sliceType.ordinal() + (sliceHeader.sliceTypeRestr ? 5 : 0), "SH: slice_type");
		CAVLCWriter.writeUEtrace(writer, sliceHeader.picParameterSetId, "SH: pic_parameter_set_id");
		if (sliceHeader.frameNum > 1 << sps.log2MaxFrameNumMinus4 + 4)
		{
			string s = new StringBuilder().append("frame_num > ").append(1 << sps.log2MaxFrameNumMinus4 + 4).toString();
			
			throw new IllegalArgumentException(s);
		}
		CAVLCWriter.writeUtrace(writer, sliceHeader.frameNum, sps.log2MaxFrameNumMinus4 + 4, "SH: frame_num");
		if (!sps.frameMbsOnlyFlag)
		{
			CAVLCWriter.writeBool(writer, sliceHeader.fieldPicFlag, "SH: field_pic_flag");
			if (sliceHeader.fieldPicFlag)
			{
				CAVLCWriter.writeBool(writer, sliceHeader.bottomFieldFlag, "SH: bottom_field_flag");
			}
		}
		if (idrSlice)
		{
			CAVLCWriter.writeUEtrace(writer, sliceHeader.idrPicId, "SH: idr_pic_id");
		}
		if (sps.picOrderCntType == 0)
		{
			if (sliceHeader.picOrderCntLsb > 1 << sps.log2MaxPicOrderCntLsbMinus4 + 4)
			{
				string s2 = new StringBuilder().append("pic_order_cnt_lsb > ").append(1 << sps.log2MaxPicOrderCntLsbMinus4 + 4).toString();
				
				throw new IllegalArgumentException(s2);
			}
			CAVLCWriter.writeU(writer, sliceHeader.picOrderCntLsb, sps.log2MaxPicOrderCntLsbMinus4 + 4);
			if (pps.picOrderPresentFlag && !sps.fieldPicFlag)
			{
				CAVLCWriter.writeSEtrace(writer, sliceHeader.deltaPicOrderCntBottom, "SH: delta_pic_order_cnt_bottom");
			}
		}
		if (sps.picOrderCntType == 1 && !sps.deltaPicOrderAlwaysZeroFlag)
		{
			CAVLCWriter.writeSEtrace(writer, sliceHeader.deltaPicOrderCnt[0], "SH: delta_pic_order_cnt");
			if (pps.picOrderPresentFlag && !sps.fieldPicFlag)
			{
				CAVLCWriter.writeSEtrace(writer, sliceHeader.deltaPicOrderCnt[1], "SH: delta_pic_order_cnt");
			}
		}
		if (pps.redundantPicCntPresentFlag)
		{
			CAVLCWriter.writeUEtrace(writer, sliceHeader.redundantPicCnt, "SH: redundant_pic_cnt");
		}
		if (sliceHeader.sliceType == SliceType.___003C_003EB)
		{
			CAVLCWriter.writeBool(writer, sliceHeader.directSpatialMvPredFlag, "SH: direct_spatial_mv_pred_flag");
		}
		if (sliceHeader.sliceType == SliceType.___003C_003EP || sliceHeader.sliceType == SliceType.___003C_003ESP || sliceHeader.sliceType == SliceType.___003C_003EB)
		{
			CAVLCWriter.writeBool(writer, sliceHeader.numRefIdxActiveOverrideFlag, "SH: num_ref_idx_active_override_flag");
			if (sliceHeader.numRefIdxActiveOverrideFlag)
			{
				CAVLCWriter.writeUEtrace(writer, sliceHeader.numRefIdxActiveMinus1[0], "SH: num_ref_idx_l0_active_minus1");
				if (sliceHeader.sliceType == SliceType.___003C_003EB)
				{
					CAVLCWriter.writeUEtrace(writer, sliceHeader.numRefIdxActiveMinus1[1], "SH: num_ref_idx_l1_active_minus1");
				}
			}
		}
		writeRefPicListReordering(sliceHeader, writer);
		if ((pps.weightedPredFlag && (sliceHeader.sliceType == SliceType.___003C_003EP || sliceHeader.sliceType == SliceType.___003C_003ESP)) || (pps.weightedBipredIdc == 1 && sliceHeader.sliceType == SliceType.___003C_003EB))
		{
			writePredWeightTable(sliceHeader, writer);
		}
		if (nalRefIdc != 0)
		{
			writeDecRefPicMarking(sliceHeader, idrSlice, writer);
		}
		if (pps.entropyCodingModeFlag && sliceHeader.sliceType.isInter())
		{
			CAVLCWriter.writeUEtrace(writer, sliceHeader.cabacInitIdc, "SH: cabac_init_idc");
		}
		CAVLCWriter.writeSEtrace(writer, sliceHeader.sliceQpDelta, "SH: slice_qp_delta");
		if (sliceHeader.sliceType == SliceType.___003C_003ESP || sliceHeader.sliceType == SliceType.___003C_003ESI)
		{
			if (sliceHeader.sliceType == SliceType.___003C_003ESP)
			{
				CAVLCWriter.writeBool(writer, sliceHeader.spForSwitchFlag, "SH: sp_for_switch_flag");
			}
			CAVLCWriter.writeSEtrace(writer, sliceHeader.sliceQsDelta, "SH: slice_qs_delta");
		}
		if (pps.deblockingFilterControlPresentFlag)
		{
			CAVLCWriter.writeUEtrace(writer, sliceHeader.disableDeblockingFilterIdc, "SH: disable_deblocking_filter_idc");
			if (sliceHeader.disableDeblockingFilterIdc != 1)
			{
				CAVLCWriter.writeSEtrace(writer, sliceHeader.sliceAlphaC0OffsetDiv2, "SH: slice_alpha_c0_offset_div2");
				CAVLCWriter.writeSEtrace(writer, sliceHeader.sliceBetaOffsetDiv2, "SH: slice_beta_offset_div2");
			}
		}
		if (pps.numSliceGroupsMinus1 > 0 && pps.sliceGroupMapType >= 3 && pps.sliceGroupMapType <= 5)
		{
			int num = (sps.picHeightInMapUnitsMinus1 + 1) * (sps.picWidthInMbsMinus1 + 1);
			int num2 = pps.sliceGroupChangeRateMinus1 + 1;
			int len = ((num2 != -1) ? (num / num2) : (-num));
			int num3 = (sps.picHeightInMapUnitsMinus1 + 1) * (sps.picWidthInMbsMinus1 + 1);
			int num4 = pps.sliceGroupChangeRateMinus1 + 1;
			if (((num4 != -1 && num3 % num4 != 0) ? 1 : 0) > (false ? 1 : 0))
			{
				len++;
			}
			len = CeilLog2(len + 1);
			CAVLCWriter.writeU(writer, sliceHeader.sliceGroupChangeCycle, len);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 89, 162, 110, 152, 109, 100, 143, 110, 152,
		109, 100, 145
	})]
	private static void writeRefPicListReordering(SliceHeader sliceHeader, BitWriter writer)
	{
		if (sliceHeader.sliceType.isInter())
		{
			int l0ReorderingPresent = ((sliceHeader.refPicReordering != null && sliceHeader.refPicReordering[0] != null) ? 1 : 0);
			CAVLCWriter.writeBool(writer, (byte)l0ReorderingPresent != 0, "SH: ref_pic_list_reordering_flag_l0");
			if (l0ReorderingPresent != 0)
			{
				writeReorderingList(sliceHeader.refPicReordering[0], writer);
			}
		}
		if (sliceHeader.sliceType == SliceType.___003C_003EB)
		{
			int l1ReorderingPresent = ((sliceHeader.refPicReordering != null && sliceHeader.refPicReordering[1] != null) ? 1 : 0);
			CAVLCWriter.writeBool(writer, (byte)l1ReorderingPresent != 0, "SH: ref_pic_list_reordering_flag_l1");
			if (l1ReorderingPresent != 0)
			{
				writeReorderingList(sliceHeader.refPicReordering[1], writer);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 130, 104, 119, 110, 183, 105, 110, 139 })]
	private static void writePredWeightTable(SliceHeader sliceHeader, BitWriter writer)
	{
		SeqParameterSet sps = sliceHeader.sps;
		CAVLCWriter.writeUEtrace(writer, sliceHeader.predWeightTable.lumaLog2WeightDenom, "SH: luma_log2_weight_denom");
		if (sps.chromaFormatIdc != ColorSpace.___003C_003EMONO)
		{
			CAVLCWriter.writeUEtrace(writer, sliceHeader.predWeightTable.chromaLog2WeightDenom, "SH: chroma_log2_weight_denom");
		}
		writeOffsetWeight(sliceHeader, writer, 0);
		if (sliceHeader.sliceType == SliceType.___003C_003EB)
		{
			writeOffsetWeight(sliceHeader, writer, 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 129, 67, 100, 104, 114, 114, 102, 120,
		108, 104, 104, 109, 103, 159, 24, 109, 117, 134,
		109, 115, 134, 109, 117, 115, 131, 109, 117, 131,
		109, 131, 109, 243, 39, 236, 93, 175
	})]
	private static void writeDecRefPicMarking(SliceHeader sliceHeader, bool idrSlice, BitWriter writer)
	{
		if (idrSlice)
		{
			RefPicMarkingIDR drpmidr2 = sliceHeader.refPicMarkingIDR;
			CAVLCWriter.writeBool(writer, drpmidr2.isDiscardDecodedPics(), "SH: no_output_of_prior_pics_flag");
			CAVLCWriter.writeBool(writer, drpmidr2.isUseForlongTerm(), "SH: long_term_reference_flag");
			return;
		}
		CAVLCWriter.writeBool(writer, sliceHeader.refPicMarkingNonIDR != null, "SH: adaptive_ref_pic_marking_mode_flag");
		if (sliceHeader.refPicMarkingNonIDR == null)
		{
			return;
		}
		RefPicMarking drpmidr = sliceHeader.refPicMarkingNonIDR;
		RefPicMarking.Instruction[] instructions = drpmidr.getInstructions();
		for (int i = 0; i < (nint)instructions.LongLength; i++)
		{
			RefPicMarking.Instruction mmop = instructions[i];
			switch (_1._0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[mmop.getType().ordinal()])
			{
			case 1:
				CAVLCWriter.writeUEtrace(writer, 1, "SH: memory_management_control_operation");
				CAVLCWriter.writeUEtrace(writer, mmop.getArg1() - 1, "SH: difference_of_pic_nums_minus1");
				break;
			case 2:
				CAVLCWriter.writeUEtrace(writer, 2, "SH: memory_management_control_operation");
				CAVLCWriter.writeUEtrace(writer, mmop.getArg1(), "SH: long_term_pic_num");
				break;
			case 3:
				CAVLCWriter.writeUEtrace(writer, 3, "SH: memory_management_control_operation");
				CAVLCWriter.writeUEtrace(writer, mmop.getArg1() - 1, "SH: difference_of_pic_nums_minus1");
				CAVLCWriter.writeUEtrace(writer, mmop.getArg2(), "SH: long_term_frame_idx");
				break;
			case 4:
				CAVLCWriter.writeUEtrace(writer, 4, "SH: memory_management_control_operation");
				CAVLCWriter.writeUEtrace(writer, mmop.getArg1() + 1, "SH: max_long_term_frame_idx_plus1");
				break;
			case 5:
				CAVLCWriter.writeUEtrace(writer, 5, "SH: memory_management_control_operation");
				break;
			case 6:
				CAVLCWriter.writeUEtrace(writer, 6, "SH: memory_management_control_operation");
				CAVLCWriter.writeUEtrace(writer, mmop.getArg1(), "SH: long_term_frame_idx");
				break;
			}
		}
		CAVLCWriter.writeUEtrace(writer, 0, "SH: memory_management_control_operation");
	}

	[LineNumberTable(new byte[] { 159, 113, 162, 101, 131, 100, 101, 135 })]
	private static int CeilLog2(int uiVal)
	{
		int uiTmp = uiVal - 1;
		int uiRet = 0;
		while (uiTmp != 0)
		{
			uiTmp >>= 1;
			uiRet++;
		}
		return uiRet;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 162, 104, 114, 146, 119, 159, 11, 110,
		101, 127, 17, 159, 17, 113, 223, 54, 110, 104,
		108, 127, 20, 31, 20, 236, 49, 234, 85
	})]
	private static void writeOffsetWeight(SliceHeader sliceHeader, BitWriter writer, int list)
	{
		SeqParameterSet sps = sliceHeader.sps;
		int defaultLW = 1 << sliceHeader.predWeightTable.lumaLog2WeightDenom;
		int defaultCW = 1 << sliceHeader.predWeightTable.chromaLog2WeightDenom;
		for (int i = 0; i < (nint)sliceHeader.predWeightTable.lumaWeight[list].LongLength; i++)
		{
			int flagLuma = ((sliceHeader.predWeightTable.lumaWeight[list][i] != defaultLW || sliceHeader.predWeightTable.lumaOffset[list][i] != 0) ? 1 : 0);
			CAVLCWriter.writeBool(writer, (byte)flagLuma != 0, "SH: luma_weight_l0_flag");
			if (flagLuma != 0)
			{
				CAVLCWriter.writeSEtrace(writer, sliceHeader.predWeightTable.lumaWeight[list][i], new StringBuilder().append("SH: luma_weight_l").append(list).toString());
				CAVLCWriter.writeSEtrace(writer, sliceHeader.predWeightTable.lumaOffset[list][i], new StringBuilder().append("SH: luma_offset_l").append(list).toString());
			}
			if (sps.chromaFormatIdc == ColorSpace.___003C_003EMONO)
			{
				continue;
			}
			int flagChroma = ((sliceHeader.predWeightTable.chromaWeight[list][0][i] != defaultCW || sliceHeader.predWeightTable.chromaOffset[list][0][i] != 0 || sliceHeader.predWeightTable.chromaWeight[list][1][i] != defaultCW || sliceHeader.predWeightTable.chromaOffset[list][1][i] != 0) ? 1 : 0);
			CAVLCWriter.writeBool(writer, (byte)flagChroma != 0, "SH: chroma_weight_l0_flag");
			if (flagChroma != 0)
			{
				for (int j = 0; j < 2; j++)
				{
					CAVLCWriter.writeSEtrace(writer, sliceHeader.predWeightTable.chromaWeight[list][j][i], new StringBuilder().append("SH: chroma_weight_l").append(list).toString());
					CAVLCWriter.writeSEtrace(writer, sliceHeader.predWeightTable.chromaOffset[list][j][i], new StringBuilder().append("SH: chroma_offset_l").append(list).toString());
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 66, 100, 130, 106, 113, 17, 199, 111 })]
	private static void writeReorderingList(int[][] reordering, BitWriter writer)
	{
		if (reordering != null)
		{
			for (int i = 0; i < (nint)reordering[0].LongLength; i++)
			{
				CAVLCWriter.writeUEtrace(writer, reordering[0][i], "SH: reordering_of_pic_nums_idc");
				CAVLCWriter.writeUEtrace(writer, reordering[1][i], "SH: abs_diff_pic_num_minus1");
			}
			CAVLCWriter.writeUEtrace(writer, 3, "SH: reordering_of_pic_nums_idc");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105 })]
	private SliceHeaderWriter()
	{
	}
}
