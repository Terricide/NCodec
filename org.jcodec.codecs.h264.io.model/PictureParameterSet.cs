using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.codecs.h264.io.model;

public class PictureParameterSet : Object
{
	public class PPSExt : Object
	{
		public bool transform8x8ModeFlag;

		public int[][] scalingMatrix;

		public int secondChromaQpIndexOffset;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(35)]
		public PPSExt()
		{
		}

		[LineNumberTable(42)]
		public virtual bool isTransform8x8ModeFlag()
		{
			return transform8x8ModeFlag;
		}

		[LineNumberTable(46)]
		public virtual int[][] getScalingMatrix()
		{
			return scalingMatrix;
		}

		[LineNumberTable(51)]
		public virtual int getSecondChromaQpIndexOffset()
		{
			return secondChromaQpIndexOffset;
		}
	}

	public bool entropyCodingModeFlag;

	public int[] numRefIdxActiveMinus1;

	public int sliceGroupChangeRateMinus1;

	public int picParameterSetId;

	public int seqParameterSetId;

	public bool picOrderPresentFlag;

	public int numSliceGroupsMinus1;

	public int sliceGroupMapType;

	public bool weightedPredFlag;

	public int weightedBipredIdc;

	public int picInitQpMinus26;

	public int picInitQsMinus26;

	public int chromaQpIndexOffset;

	public bool deblockingFilterControlPresentFlag;

	public bool constrainedIntraPredFlag;

	public bool redundantPicCntPresentFlag;

	public int[] topLeft;

	public int[] bottomRight;

	public int[] runLengthMinus1;

	public bool sliceGroupChangeDirectionFlag;

	public int[] sliceGroupId;

	public PPSExt extended;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 66, 104, 135, 114, 114, 114, 114, 114,
		109, 114, 116, 116, 116, 105, 108, 52, 140, 106,
		108, 116, 20, 204, 124, 114, 119, 141, 108, 102,
		108, 134, 100, 110, 112, 106, 63, 24, 233, 69,
		127, 10, 114, 115, 114, 114, 114, 114, 114, 114,
		108, 108, 119, 110, 104, 114, 125, 110, 110, 247,
		61, 236, 71, 183
	})]
	public static PictureParameterSet read(ByteBuffer @is)
	{
		BitReader _in = BitReader.createBitReader(@is);
		PictureParameterSet pps = new PictureParameterSet();
		pps.picParameterSetId = CAVLCReader.readUEtrace(_in, "PPS: pic_parameter_set_id");
		pps.seqParameterSetId = CAVLCReader.readUEtrace(_in, "PPS: seq_parameter_set_id");
		pps.entropyCodingModeFlag = CAVLCReader.readBool(_in, "PPS: entropy_coding_mode_flag");
		pps.picOrderPresentFlag = CAVLCReader.readBool(_in, "PPS: pic_order_present_flag");
		pps.numSliceGroupsMinus1 = CAVLCReader.readUEtrace(_in, "PPS: num_slice_groups_minus1");
		if (pps.numSliceGroupsMinus1 > 0)
		{
			pps.sliceGroupMapType = CAVLCReader.readUEtrace(_in, "PPS: slice_group_map_type");
			pps.topLeft = new int[pps.numSliceGroupsMinus1 + 1];
			pps.bottomRight = new int[pps.numSliceGroupsMinus1 + 1];
			pps.runLengthMinus1 = new int[pps.numSliceGroupsMinus1 + 1];
			if (pps.sliceGroupMapType == 0)
			{
				for (int iGroup2 = 0; iGroup2 <= pps.numSliceGroupsMinus1; iGroup2++)
				{
					pps.runLengthMinus1[iGroup2] = CAVLCReader.readUEtrace(_in, "PPS: run_length_minus1");
				}
			}
			else if (pps.sliceGroupMapType == 2)
			{
				for (int iGroup = 0; iGroup < pps.numSliceGroupsMinus1; iGroup++)
				{
					pps.topLeft[iGroup] = CAVLCReader.readUEtrace(_in, "PPS: top_left");
					pps.bottomRight[iGroup] = CAVLCReader.readUEtrace(_in, "PPS: bottom_right");
				}
			}
			else if (pps.sliceGroupMapType == 3 || pps.sliceGroupMapType == 4 || pps.sliceGroupMapType == 5)
			{
				pps.sliceGroupChangeDirectionFlag = CAVLCReader.readBool(_in, "PPS: slice_group_change_direction_flag");
				pps.sliceGroupChangeRateMinus1 = CAVLCReader.readUEtrace(_in, "PPS: slice_group_change_rate_minus1");
			}
			else if (pps.sliceGroupMapType == 6)
			{
				int NumberBitsPerSliceGroupId = ((pps.numSliceGroupsMinus1 + 1 > 4) ? 3 : ((pps.numSliceGroupsMinus1 + 1 <= 2) ? 1 : 2));
				int pic_size_in_map_units_minus1 = CAVLCReader.readUEtrace(_in, "PPS: pic_size_in_map_units_minus1");
				pps.sliceGroupId = new int[pic_size_in_map_units_minus1 + 1];
				for (int j = 0; j <= pic_size_in_map_units_minus1; j++)
				{
					pps.sliceGroupId[j] = CAVLCReader.readU(_in, NumberBitsPerSliceGroupId, new StringBuilder().append("PPS: slice_group_id [").append(j).append("]f")
						.toString());
				}
			}
		}
		pps.numRefIdxActiveMinus1 = new int[2]
		{
			CAVLCReader.readUEtrace(_in, "PPS: num_ref_idx_l0_active_minus1"),
			CAVLCReader.readUEtrace(_in, "PPS: num_ref_idx_l1_active_minus1")
		};
		pps.weightedPredFlag = CAVLCReader.readBool(_in, "PPS: weighted_pred_flag");
		pps.weightedBipredIdc = CAVLCReader.readNBit(_in, 2, "PPS: weighted_bipred_idc");
		pps.picInitQpMinus26 = CAVLCReader.readSE(_in, "PPS: pic_init_qp_minus26");
		pps.picInitQsMinus26 = CAVLCReader.readSE(_in, "PPS: pic_init_qs_minus26");
		pps.chromaQpIndexOffset = CAVLCReader.readSE(_in, "PPS: chroma_qp_index_offset");
		pps.deblockingFilterControlPresentFlag = CAVLCReader.readBool(_in, "PPS: deblocking_filter_control_present_flag");
		pps.constrainedIntraPredFlag = CAVLCReader.readBool(_in, "PPS: constrained_intra_pred_flag");
		pps.redundantPicCntPresentFlag = CAVLCReader.readBool(_in, "PPS: redundant_pic_cnt_present_flag");
		if (CAVLCReader.moreRBSPData(_in))
		{
			pps.extended = new PPSExt();
			pps.extended.transform8x8ModeFlag = CAVLCReader.readBool(_in, "PPS: transform_8x8_mode_flag");
			if (CAVLCReader.readBool(_in, "PPS: pic_scaling_matrix_present_flag"))
			{
				pps.extended.scalingMatrix = new int[8][];
				for (int i = 0; i < 6 + 2 * (pps.extended.transform8x8ModeFlag ? 1 : 0); i++)
				{
					int scalingListSize = ((i >= 6) ? 64 : 16);
					if (CAVLCReader.readBool(_in, "PPS: pic_scaling_list_present_flag"))
					{
						pps.extended.scalingMatrix[i] = SeqParameterSet.readScalingList(_in, scalingListSize);
					}
				}
			}
			pps.extended.secondChromaQpIndexOffset = CAVLCReader.readSE(_in, "PPS: second_chroma_qp_index_offset");
		}
		return pps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 66, 136, 114, 114, 114, 114, 114, 109,
		114, 105, 108, 52, 172, 106, 108, 116, 20, 204,
		124, 114, 119, 141, 108, 101, 108, 133, 99, 115,
		111, 49, 233, 69, 116, 116, 114, 116, 114, 114,
		114, 114, 114, 114, 108, 119, 125, 113, 125, 127,
		1, 113, 244, 61, 236, 71, 183, 105
	})]
	public virtual void write(ByteBuffer @out)
	{
		BitWriter writer = new BitWriter(@out);
		CAVLCWriter.writeUEtrace(writer, picParameterSetId, "PPS: pic_parameter_set_id");
		CAVLCWriter.writeUEtrace(writer, seqParameterSetId, "PPS: seq_parameter_set_id");
		CAVLCWriter.writeBool(writer, entropyCodingModeFlag, "PPS: entropy_coding_mode_flag");
		CAVLCWriter.writeBool(writer, picOrderPresentFlag, "PPS: pic_order_present_flag");
		CAVLCWriter.writeUEtrace(writer, numSliceGroupsMinus1, "PPS: num_slice_groups_minus1");
		if (numSliceGroupsMinus1 > 0)
		{
			CAVLCWriter.writeUEtrace(writer, sliceGroupMapType, "PPS: slice_group_map_type");
			if (sliceGroupMapType == 0)
			{
				for (int iGroup2 = 0; iGroup2 <= numSliceGroupsMinus1; iGroup2++)
				{
					CAVLCWriter.writeUEtrace(writer, runLengthMinus1[iGroup2], "PPS: ");
				}
			}
			else if (sliceGroupMapType == 2)
			{
				for (int iGroup = 0; iGroup < numSliceGroupsMinus1; iGroup++)
				{
					CAVLCWriter.writeUEtrace(writer, topLeft[iGroup], "PPS: ");
					CAVLCWriter.writeUEtrace(writer, bottomRight[iGroup], "PPS: ");
				}
			}
			else if (sliceGroupMapType == 3 || sliceGroupMapType == 4 || sliceGroupMapType == 5)
			{
				CAVLCWriter.writeBool(writer, sliceGroupChangeDirectionFlag, "PPS: slice_group_change_direction_flag");
				CAVLCWriter.writeUEtrace(writer, sliceGroupChangeRateMinus1, "PPS: slice_group_change_rate_minus1");
			}
			else if (sliceGroupMapType == 6)
			{
				int NumberBitsPerSliceGroupId = ((numSliceGroupsMinus1 + 1 > 4) ? 3 : ((numSliceGroupsMinus1 + 1 <= 2) ? 1 : 2));
				CAVLCWriter.writeUEtrace(writer, sliceGroupId.Length, "PPS: ");
				for (int j = 0; j <= (nint)sliceGroupId.LongLength; j++)
				{
					CAVLCWriter.writeU(writer, sliceGroupId[j], NumberBitsPerSliceGroupId);
				}
			}
		}
		CAVLCWriter.writeUEtrace(writer, numRefIdxActiveMinus1[0], "PPS: num_ref_idx_l0_active_minus1");
		CAVLCWriter.writeUEtrace(writer, numRefIdxActiveMinus1[1], "PPS: num_ref_idx_l1_active_minus1");
		CAVLCWriter.writeBool(writer, weightedPredFlag, "PPS: weighted_pred_flag");
		CAVLCWriter.writeNBit(writer, weightedBipredIdc, 2, "PPS: weighted_bipred_idc");
		CAVLCWriter.writeSEtrace(writer, picInitQpMinus26, "PPS: pic_init_qp_minus26");
		CAVLCWriter.writeSEtrace(writer, picInitQsMinus26, "PPS: pic_init_qs_minus26");
		CAVLCWriter.writeSEtrace(writer, chromaQpIndexOffset, "PPS: chroma_qp_index_offset");
		CAVLCWriter.writeBool(writer, deblockingFilterControlPresentFlag, "PPS: deblocking_filter_control_present_flag");
		CAVLCWriter.writeBool(writer, constrainedIntraPredFlag, "PPS: constrained_intra_pred_flag");
		CAVLCWriter.writeBool(writer, redundantPicCntPresentFlag, "PPS: redundant_pic_cnt_present_flag");
		if (extended != null)
		{
			CAVLCWriter.writeBool(writer, extended.transform8x8ModeFlag, "PPS: transform_8x8_mode_flag");
			CAVLCWriter.writeBool(writer, extended.scalingMatrix != null, "PPS: scalindMatrix");
			if (extended.scalingMatrix != null)
			{
				for (int i = 0; i < 6 + 2 * (extended.transform8x8ModeFlag ? 1 : 0); i++)
				{
					CAVLCWriter.writeBool(writer, extended.scalingMatrix[i] != null, "PPS: ");
					if (extended.scalingMatrix[i] != null)
					{
						SeqParameterSet.writeScalingList(writer, extended.scalingMatrix, i);
					}
				}
			}
			CAVLCWriter.writeSEtrace(writer, extended.secondChromaQpIndexOffset, "PPS: ");
		}
		CAVLCWriter.writeTrailingBits(writer);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 162, 105, 109 })]
	public PictureParameterSet()
	{
		numRefIdxActiveMinus1 = new int[2];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 66, 100, 99, 114, 109, 123, 123, 123,
		125, 111, 111, 109, 109, 109, 123, 109, 123, 114,
		109, 123, 109, 114, 109, 114, 109, 123
	})]
	public override int hashCode()
	{
		int prime = 31;
		int result = 1;
		result = 31 * result + Arrays.hashCode(bottomRight);
		result = 31 * result + chromaQpIndexOffset;
		result = 31 * result + ((!constrainedIntraPredFlag) ? 1237 : 1231);
		result = 31 * result + ((!deblockingFilterControlPresentFlag) ? 1237 : 1231);
		result = 31 * result + ((!entropyCodingModeFlag) ? 1237 : 1231);
		result = 31 * result + ((extended != null) ? Object.instancehelper_hashCode(extended) : 0);
		result = 31 * result + numRefIdxActiveMinus1[0];
		result = 31 * result + numRefIdxActiveMinus1[1];
		result = 31 * result + numSliceGroupsMinus1;
		result = 31 * result + picInitQpMinus26;
		result = 31 * result + picInitQsMinus26;
		result = 31 * result + ((!picOrderPresentFlag) ? 1237 : 1231);
		result = 31 * result + picParameterSetId;
		result = 31 * result + ((!redundantPicCntPresentFlag) ? 1237 : 1231);
		result = 31 * result + Arrays.hashCode(runLengthMinus1);
		result = 31 * result + seqParameterSetId;
		result = 31 * result + ((!sliceGroupChangeDirectionFlag) ? 1237 : 1231);
		result = 31 * result + sliceGroupChangeRateMinus1;
		result = 31 * result + Arrays.hashCode(sliceGroupId);
		result = 31 * result + sliceGroupMapType;
		result = 31 * result + Arrays.hashCode(topLeft);
		result = 31 * result + weightedBipredIdc;
		return 31 * result + ((!weightedPredFlag) ? 1237 : 1231);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 76, 130, 101, 99, 100, 99, 111, 99, 104,
		116, 99, 111, 99, 111, 99, 111, 99, 111, 99,
		105, 105, 99, 116, 99, 115, 99, 115, 99, 111,
		99, 111, 99, 111, 99, 111, 99, 111, 99, 111,
		99, 116, 99, 111, 99, 111, 99, 111, 99, 116,
		99, 111, 99, 116, 99, 111, 99, 111, 99
	})]
	public override bool equals(object obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (obj == null)
		{
			return false;
		}
		if ((object)((object)this).GetType() != obj.GetType())
		{
			return false;
		}
		PictureParameterSet other = (PictureParameterSet)obj;
		if (!Platform.arrayEqualsInt(bottomRight, other.bottomRight))
		{
			return false;
		}
		if (chromaQpIndexOffset != other.chromaQpIndexOffset)
		{
			return false;
		}
		if (constrainedIntraPredFlag != other.constrainedIntraPredFlag)
		{
			return false;
		}
		if (deblockingFilterControlPresentFlag != other.deblockingFilterControlPresentFlag)
		{
			return false;
		}
		if (entropyCodingModeFlag != other.entropyCodingModeFlag)
		{
			return false;
		}
		if (extended == null)
		{
			if (other.extended != null)
			{
				return false;
			}
		}
		else if (!Object.instancehelper_equals(extended, other.extended))
		{
			return false;
		}
		if (numRefIdxActiveMinus1[0] != other.numRefIdxActiveMinus1[0])
		{
			return false;
		}
		if (numRefIdxActiveMinus1[1] != other.numRefIdxActiveMinus1[1])
		{
			return false;
		}
		if (numSliceGroupsMinus1 != other.numSliceGroupsMinus1)
		{
			return false;
		}
		if (picInitQpMinus26 != other.picInitQpMinus26)
		{
			return false;
		}
		if (picInitQsMinus26 != other.picInitQsMinus26)
		{
			return false;
		}
		if (picOrderPresentFlag != other.picOrderPresentFlag)
		{
			return false;
		}
		if (picParameterSetId != other.picParameterSetId)
		{
			return false;
		}
		if (redundantPicCntPresentFlag != other.redundantPicCntPresentFlag)
		{
			return false;
		}
		if (!Platform.arrayEqualsInt(runLengthMinus1, other.runLengthMinus1))
		{
			return false;
		}
		if (seqParameterSetId != other.seqParameterSetId)
		{
			return false;
		}
		if (sliceGroupChangeDirectionFlag != other.sliceGroupChangeDirectionFlag)
		{
			return false;
		}
		if (sliceGroupChangeRateMinus1 != other.sliceGroupChangeRateMinus1)
		{
			return false;
		}
		if (!Platform.arrayEqualsInt(sliceGroupId, other.sliceGroupId))
		{
			return false;
		}
		if (sliceGroupMapType != other.sliceGroupMapType)
		{
			return false;
		}
		if (!Platform.arrayEqualsInt(topLeft, other.topLeft))
		{
			return false;
		}
		if (weightedBipredIdc != other.weightedBipredIdc)
		{
			return false;
		}
		if (weightedPredFlag != other.weightedPredFlag)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 61, 130, 108, 104, 104 })]
	public virtual PictureParameterSet copy()
	{
		ByteBuffer buf = ByteBuffer.allocate(2048);
		write(buf);
		buf.flip();
		PictureParameterSet result = read(buf);
		
		return result;
	}

	[LineNumberTable(333)]
	public virtual bool isEntropyCodingModeFlag()
	{
		return entropyCodingModeFlag;
	}

	[LineNumberTable(337)]
	public virtual int[] getNumRefIdxActiveMinus1()
	{
		return numRefIdxActiveMinus1;
	}

	[LineNumberTable(341)]
	public virtual int getSliceGroupChangeRateMinus1()
	{
		return sliceGroupChangeRateMinus1;
	}

	[LineNumberTable(345)]
	public virtual int getPicParameterSetId()
	{
		return picParameterSetId;
	}

	[LineNumberTable(349)]
	public virtual int getSeqParameterSetId()
	{
		return seqParameterSetId;
	}

	[LineNumberTable(353)]
	public virtual bool isPicOrderPresentFlag()
	{
		return picOrderPresentFlag;
	}

	[LineNumberTable(357)]
	public virtual int getNumSliceGroupsMinus1()
	{
		return numSliceGroupsMinus1;
	}

	[LineNumberTable(361)]
	public virtual int getSliceGroupMapType()
	{
		return sliceGroupMapType;
	}

	[LineNumberTable(365)]
	public virtual bool isWeightedPredFlag()
	{
		return weightedPredFlag;
	}

	[LineNumberTable(369)]
	public virtual int getWeightedBipredIdc()
	{
		return weightedBipredIdc;
	}

	[LineNumberTable(373)]
	public virtual int getPicInitQpMinus26()
	{
		return picInitQpMinus26;
	}

	[LineNumberTable(377)]
	public virtual int getPicInitQsMinus26()
	{
		return picInitQsMinus26;
	}

	[LineNumberTable(381)]
	public virtual int getChromaQpIndexOffset()
	{
		return chromaQpIndexOffset;
	}

	[LineNumberTable(385)]
	public virtual bool isDeblockingFilterControlPresentFlag()
	{
		return deblockingFilterControlPresentFlag;
	}

	[LineNumberTable(389)]
	public virtual bool isConstrainedIntraPredFlag()
	{
		return constrainedIntraPredFlag;
	}

	[LineNumberTable(393)]
	public virtual bool isRedundantPicCntPresentFlag()
	{
		return redundantPicCntPresentFlag;
	}

	[LineNumberTable(397)]
	public virtual int[] getTopLeft()
	{
		return topLeft;
	}

	[LineNumberTable(401)]
	public virtual int[] getBottomRight()
	{
		return bottomRight;
	}

	[LineNumberTable(405)]
	public virtual int[] getRunLengthMinus1()
	{
		return runLengthMinus1;
	}

	[LineNumberTable(409)]
	public virtual bool isSliceGroupChangeDirectionFlag()
	{
		return sliceGroupChangeDirectionFlag;
	}

	[LineNumberTable(413)]
	public virtual int[] getSliceGroupId()
	{
		return sliceGroupId;
	}

	[LineNumberTable(417)]
	public virtual PPSExt getExtended()
	{
		return extended;
	}
}
