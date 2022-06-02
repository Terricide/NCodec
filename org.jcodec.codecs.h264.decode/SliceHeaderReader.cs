using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class SliceHeaderReader : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 103, 114, 109, 120, 141, 146 })]
	public static SliceHeader readPart1(BitReader _in)
	{
		SliceHeader sh = new SliceHeader();
		sh.firstMbInSlice = CAVLCReader.readUEtrace(_in, "SH: first_mb_in_slice");
		int shType = CAVLCReader.readUEtrace(_in, "SH: slice_type");
		sh.sliceType = SliceType.fromValue((5 != -1) ? (shType % 5) : 0);
		sh.sliceTypeRestr = shType / 5 > 0;
		sh.picParameterSetId = CAVLCReader.readUEtrace(_in, "SH: pic_parameter_set_id");
		return sh;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 98, 104, 136, 123, 105, 115, 105, 179,
		110, 147, 105, 123, 113, 179, 109, 114, 117, 113,
		149, 105, 147, 110, 147, 127, 9, 115, 105, 117,
		110, 213, 105, 159, 26, 107, 105, 106, 118, 147,
		115, 123, 110, 147, 147, 105, 115, 106, 115, 179,
		127, 6, 159, 3, 159, 6, 133, 106, 180
	})]
	public static SliceHeader readPart2(SliceHeader sh, NALUnit nalUnit, SeqParameterSet sps, PictureParameterSet pps, BitReader _in)
	{
		sh.pps = pps;
		sh.sps = sps;
		sh.frameNum = CAVLCReader.readU(_in, sps.log2MaxFrameNumMinus4 + 4, "SH: frame_num");
		if (!sps.frameMbsOnlyFlag)
		{
			sh.fieldPicFlag = CAVLCReader.readBool(_in, "SH: field_pic_flag");
			if (sh.fieldPicFlag)
			{
				sh.bottomFieldFlag = CAVLCReader.readBool(_in, "SH: bottom_field_flag");
			}
		}
		if (nalUnit.type == NALUnitType.___003C_003EIDR_SLICE)
		{
			sh.idrPicId = CAVLCReader.readUEtrace(_in, "SH: idr_pic_id");
		}
		if (sps.picOrderCntType == 0)
		{
			sh.picOrderCntLsb = CAVLCReader.readU(_in, sps.log2MaxPicOrderCntLsbMinus4 + 4, "SH: pic_order_cnt_lsb");
			if (pps.picOrderPresentFlag && !sps.fieldPicFlag)
			{
				sh.deltaPicOrderCntBottom = CAVLCReader.readSE(_in, "SH: delta_pic_order_cnt_bottom");
			}
		}
		sh.deltaPicOrderCnt = new int[2];
		if (sps.picOrderCntType == 1 && !sps.deltaPicOrderAlwaysZeroFlag)
		{
			sh.deltaPicOrderCnt[0] = CAVLCReader.readSE(_in, "SH: delta_pic_order_cnt[0]");
			if (pps.picOrderPresentFlag && !sps.fieldPicFlag)
			{
				sh.deltaPicOrderCnt[1] = CAVLCReader.readSE(_in, "SH: delta_pic_order_cnt[1]");
			}
		}
		if (pps.redundantPicCntPresentFlag)
		{
			sh.redundantPicCnt = CAVLCReader.readUEtrace(_in, "SH: redundant_pic_cnt");
		}
		if (sh.sliceType == SliceType.___003C_003EB)
		{
			sh.directSpatialMvPredFlag = CAVLCReader.readBool(_in, "SH: direct_spatial_mv_pred_flag");
		}
		if (sh.sliceType == SliceType.___003C_003EP || sh.sliceType == SliceType.___003C_003ESP || sh.sliceType == SliceType.___003C_003EB)
		{
			sh.numRefIdxActiveOverrideFlag = CAVLCReader.readBool(_in, "SH: num_ref_idx_active_override_flag");
			if (sh.numRefIdxActiveOverrideFlag)
			{
				sh.numRefIdxActiveMinus1[0] = CAVLCReader.readUEtrace(_in, "SH: num_ref_idx_l0_active_minus1");
				if (sh.sliceType == SliceType.___003C_003EB)
				{
					sh.numRefIdxActiveMinus1[1] = CAVLCReader.readUEtrace(_in, "SH: num_ref_idx_l1_active_minus1");
				}
			}
		}
		readRefPicListReordering(sh, _in);
		if ((pps.weightedPredFlag && (sh.sliceType == SliceType.___003C_003EP || sh.sliceType == SliceType.___003C_003ESP)) || (pps.weightedBipredIdc == 1 && sh.sliceType == SliceType.___003C_003EB))
		{
			readPredWeightTable(sps, pps, sh, _in);
		}
		if (nalUnit.nal_ref_idc != 0)
		{
			readDecoderPicMarking(nalUnit, sh, _in);
		}
		if (pps.entropyCodingModeFlag && sh.sliceType.isInter())
		{
			sh.cabacInitIdc = CAVLCReader.readUEtrace(_in, "SH: cabac_init_idc");
		}
		sh.sliceQpDelta = CAVLCReader.readSE(_in, "SH: slice_qp_delta");
		if (sh.sliceType == SliceType.___003C_003ESP || sh.sliceType == SliceType.___003C_003ESI)
		{
			if (sh.sliceType == SliceType.___003C_003ESP)
			{
				sh.spForSwitchFlag = CAVLCReader.readBool(_in, "SH: sp_for_switch_flag");
			}
			sh.sliceQsDelta = CAVLCReader.readSE(_in, "SH: slice_qs_delta");
		}
		if (pps.deblockingFilterControlPresentFlag)
		{
			sh.disableDeblockingFilterIdc = CAVLCReader.readUEtrace(_in, "SH: disable_deblocking_filter_idc");
			if (sh.disableDeblockingFilterIdc != 1)
			{
				sh.sliceAlphaC0OffsetDiv2 = CAVLCReader.readSE(_in, "SH: slice_alpha_c0_offset_div2");
				sh.sliceBetaOffsetDiv2 = CAVLCReader.readSE(_in, "SH: slice_beta_offset_div2");
			}
		}
		if (pps.numSliceGroupsMinus1 > 0 && pps.sliceGroupMapType >= 3 && pps.sliceGroupMapType <= 5)
		{
			int num = SeqParameterSet.getPicHeightInMbs(sps) * (sps.picWidthInMbsMinus1 + 1);
			int num2 = pps.sliceGroupChangeRateMinus1 + 1;
			int len = ((num2 != -1) ? (num / num2) : (-num));
			int num3 = SeqParameterSet.getPicHeightInMbs(sps) * (sps.picWidthInMbsMinus1 + 1);
			int num4 = pps.sliceGroupChangeRateMinus1 + 1;
			if (((num4 != -1 && num3 % num4 != 0) ? 1 : 0) > (false ? 1 : 0))
			{
				len++;
			}
			len = CeilLog2(len + 1);
			sh.sliceGroupChangeCycle = CAVLCReader.readU(_in, len, "SH: slice_group_change_cycle");
		}
		return sh;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 81, 162, 141, 110, 109, 100, 175, 110, 109,
		100, 175
	})]
	private static void readRefPicListReordering(SliceHeader sh, BitReader _in)
	{
		sh.refPicReordering = new int[2][][];
		if (sh.sliceType.isInter() && CAVLCReader.readBool(_in, "SH: ref_pic_list_reordering_flag_l0"))
		{
			sh.refPicReordering[0] = readReorderingEntries(_in);
		}
		if (sh.sliceType == SliceType.___003C_003EB && CAVLCReader.readBool(_in, "SH: ref_pic_list_reordering_flag_l1"))
		{
			sh.refPicReordering[1] = readReorderingEntries(_in);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 66, 108, 152, 152, 119, 110, 151, 114,
		146, 108, 120, 120, 127, 30, 127, 30, 111, 115,
		115, 117, 117, 117, 245, 58, 236, 59, 236, 79,
		108, 110, 142
	})]
	private static void readPredWeightTable(SeqParameterSet sps, PictureParameterSet pps, SliceHeader sh, BitReader _in)
	{
		sh.predWeightTable = new PredictionWeightTable();
		int[] numRefsMinus1 = ((!sh.numRefIdxActiveOverrideFlag) ? pps.numRefIdxActiveMinus1 : sh.numRefIdxActiveMinus1);
		int[] nr = new int[2]
		{
			numRefsMinus1[0] + 1,
			numRefsMinus1[1] + 1
		};
		sh.predWeightTable.lumaLog2WeightDenom = CAVLCReader.readUEtrace(_in, "SH: luma_log2_weight_denom");
		if (sps.chromaFormatIdc != ColorSpace.___003C_003EMONO)
		{
			sh.predWeightTable.chromaLog2WeightDenom = CAVLCReader.readUEtrace(_in, "SH: chroma_log2_weight_denom");
		}
		int defaultLW = 1 << sh.predWeightTable.lumaLog2WeightDenom;
		int defaultCW = 1 << sh.predWeightTable.chromaLog2WeightDenom;
		for (int list = 0; list < 2; list++)
		{
			sh.predWeightTable.lumaWeight[list] = new int[nr[list]];
			sh.predWeightTable.lumaOffset[list] = new int[nr[list]];
			int[][][] chromaWeight = sh.predWeightTable.chromaWeight;
			int num = list;
			int num2 = nr[list];
			int[] array = new int[2];
			int num3 = (array[1] = num2);
			num3 = (array[0] = 2);
			chromaWeight[num] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			int[][][] chromaOffset = sh.predWeightTable.chromaOffset;
			int num4 = list;
			int num5 = nr[list];
			array = new int[2];
			num3 = (array[1] = num5);
			num3 = (array[0] = 2);
			chromaOffset[num4] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			for (int i = 0; i < nr[list]; i++)
			{
				sh.predWeightTable.lumaWeight[list][i] = defaultLW;
				sh.predWeightTable.lumaOffset[list][i] = 0;
				sh.predWeightTable.chromaWeight[list][0][i] = defaultCW;
				sh.predWeightTable.chromaOffset[list][0][i] = 0;
				sh.predWeightTable.chromaWeight[list][1][i] = defaultCW;
				sh.predWeightTable.chromaOffset[list][1][i] = 0;
			}
		}
		readWeightOffset(sps, pps, sh, _in, nr, 0);
		if (sh.sliceType == SliceType.___003C_003EB)
		{
			readWeightOffset(sps, pps, sh, _in, nr, 1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 66, 110, 109, 109, 110, 102, 109, 103,
		167, 142, 132, 159, 8, 155, 134, 108, 110, 134,
		121, 109, 131, 155, 131, 111, 131, 108, 174, 101,
		106, 104, 189
	})]
	private static void readDecoderPicMarking(NALUnit nalUnit, SliceHeader sh, BitReader _in)
	{
		if (nalUnit.type == NALUnitType.___003C_003EIDR_SLICE)
		{
			int noOutputOfPriorPicsFlag = (CAVLCReader.readBool(_in, "SH: no_output_of_prior_pics_flag") ? 1 : 0);
			int longTermReferenceFlag = (CAVLCReader.readBool(_in, "SH: long_term_reference_flag") ? 1 : 0);
			sh.refPicMarkingIDR = new RefPicMarkingIDR((byte)noOutputOfPriorPicsFlag != 0, (byte)longTermReferenceFlag != 0);
		}
		else
		{
			if (!CAVLCReader.readBool(_in, "SH: adaptive_ref_pic_marking_mode_flag"))
			{
				return;
			}
			ArrayList mmops = new ArrayList();
			int memoryManagementControlOperation;
			do
			{
				memoryManagementControlOperation = CAVLCReader.readUEtrace(_in, "SH: memory_management_control_operation");
				RefPicMarking.Instruction instr = null;
				switch (memoryManagementControlOperation)
				{
				case 1:
					instr = new RefPicMarking.Instruction(RefPicMarking.InstrType.___003C_003EREMOVE_SHORT, CAVLCReader.readUEtrace(_in, "SH: difference_of_pic_nums_minus1") + 1, 0);
					break;
				case 2:
					instr = new RefPicMarking.Instruction(RefPicMarking.InstrType.___003C_003EREMOVE_LONG, CAVLCReader.readUEtrace(_in, "SH: long_term_pic_num"), 0);
					break;
				case 3:
					instr = new RefPicMarking.Instruction(RefPicMarking.InstrType.___003C_003ECONVERT_INTO_LONG, CAVLCReader.readUEtrace(_in, "SH: difference_of_pic_nums_minus1") + 1, CAVLCReader.readUEtrace(_in, "SH: long_term_frame_idx"));
					break;
				case 4:
					instr = new RefPicMarking.Instruction(RefPicMarking.InstrType.___003C_003ETRUNK_LONG, CAVLCReader.readUEtrace(_in, "SH: max_long_term_frame_idx_plus1") - 1, 0);
					break;
				case 5:
					instr = new RefPicMarking.Instruction(RefPicMarking.InstrType.___003C_003ECLEAR, 0, 0);
					break;
				case 6:
					instr = new RefPicMarking.Instruction(RefPicMarking.InstrType.___003C_003EMARK_LONG, CAVLCReader.readUEtrace(_in, "SH: long_term_frame_idx"), 0);
					break;
				}
				if (instr != null)
				{
					mmops.add(instr);
				}
			}
			while (memoryManagementControlOperation != 0);
			sh.refPicMarkingNonIDR = new RefPicMarking((RefPicMarking.Instruction[])mmops.toArray(new RefPicMarking.Instruction[0]));
		}
	}

	[LineNumberTable(new byte[] { 159, 110, 162, 101, 131, 100, 101, 135 })]
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
		159, 85, 66, 110, 109, 100, 124, 156, 113, 109,
		103, 126, 126, 126, 254, 52, 234, 80
	})]
	private static void readWeightOffset(SeqParameterSet sps, PictureParameterSet pps, SliceHeader sh, BitReader _in, int[] numRefs, int list)
	{
		for (int i = 0; i < numRefs[list]; i++)
		{
			if (CAVLCReader.readBool(_in, "SH: luma_weight_l0_flag"))
			{
				sh.predWeightTable.lumaWeight[list][i] = CAVLCReader.readSE(_in, "SH: weight");
				sh.predWeightTable.lumaOffset[list][i] = CAVLCReader.readSE(_in, "SH: offset");
			}
			if (sps.chromaFormatIdc != ColorSpace.___003C_003EMONO && CAVLCReader.readBool(_in, "SH: chroma_weight_l0_flag"))
			{
				sh.predWeightTable.chromaWeight[list][0][i] = CAVLCReader.readSE(_in, "SH: weight");
				sh.predWeightTable.chromaOffset[list][0][i] = CAVLCReader.readSE(_in, "SH: offset");
				sh.predWeightTable.chromaWeight[list][1][i] = CAVLCReader.readSE(_in, "SH: weight");
				sh.predWeightTable.chromaOffset[list][1][i] = CAVLCReader.readSE(_in, "SH: offset");
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 76, 66, 103, 135, 109, 101, 99, 104, 114,
		99
	})]
	private static int[][] readReorderingEntries(BitReader _in)
	{
		IntArrayList ops = IntArrayList.createIntArrayList();
		IntArrayList args = IntArrayList.createIntArrayList();
		while (true)
		{
			int idc = CAVLCReader.readUEtrace(_in, "SH: reordering_of_pic_nums_idc");
			if (idc == 3)
			{
				break;
			}
			ops.add(idc);
			args.add(CAVLCReader.readUEtrace(_in, "SH: abs_diff_pic_num_minus1"));
		}
		return new int[2][]
		{
			ops.toArray(),
			args.toArray()
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 105 })]
	private SliceHeaderReader()
	{
	}
}
