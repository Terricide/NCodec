using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.io.model;

namespace org.jcodec.codecs.h264;

public class POCManager : Object
{
	private int prevPOCMsb;

	private int prevPOCLsb;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(28)]
	public POCManager()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 159, 0, 140, 140, 140 })]
	public virtual int calcPOC(SliceHeader firstSliceHeader, NALUnit firstNu)
	{
		switch (firstSliceHeader.sps.picOrderCntType)
		{
		case 0:
		{
			int result3 = calcPOC0(firstSliceHeader, firstNu);
			
			return result3;
		}
		case 1:
		{
			int result2 = calcPOC1(firstSliceHeader, firstNu);
			
			return result2;
		}
		case 2:
		{
			int result = calcPOC2(firstSliceHeader, firstNu);
			
			return result;
		}
		default:
			
			throw new RuntimeException("POC no!!!");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 110, 147, 120, 168, 117, 109, 117,
		141, 137, 135, 106, 107, 104, 139, 105, 200
	})]
	private int calcPOC0(SliceHeader firstSliceHeader, NALUnit firstNu)
	{
		if (firstNu.type == NALUnitType.___003C_003EIDR_SLICE)
		{
			int num = 0;
			prevPOCLsb = num;
			prevPOCMsb = num;
		}
		int maxPOCLsbDiv2 = 1 << firstSliceHeader.sps.log2MaxPicOrderCntLsbMinus4 + 3;
		int maxPOCLsb = maxPOCLsbDiv2 << 1;
		int POCLsb = firstSliceHeader.picOrderCntLsb;
		int POCMsb = ((POCLsb < prevPOCLsb && prevPOCLsb - POCLsb >= maxPOCLsbDiv2) ? (prevPOCMsb + maxPOCLsb) : ((POCLsb <= prevPOCLsb || POCLsb - prevPOCLsb <= maxPOCLsbDiv2) ? prevPOCMsb : (prevPOCMsb - maxPOCLsb)));
		int POC = POCMsb + POCLsb;
		if (firstNu.nal_ref_idc > 0)
		{
			if (hasMMCO5(firstSliceHeader, firstNu))
			{
				prevPOCMsb = 0;
				prevPOCLsb = POC;
			}
			else
			{
				prevPOCMsb = POCMsb;
				prevPOCLsb = POCLsb;
			}
		}
		return POC;
	}

	[LineNumberTable(52)]
	private int calcPOC1(SliceHeader firstSliceHeader, NALUnit firstNu)
	{
		return firstSliceHeader.frameNum << 1;
	}

	[LineNumberTable(48)]
	private int calcPOC2(SliceHeader firstSliceHeader, NALUnit firstNu)
	{
		return firstSliceHeader.frameNum << 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 118, 109, 104, 101, 110, 227, 61,
		231, 70
	})]
	private bool hasMMCO5(SliceHeader firstSliceHeader, NALUnit firstNu)
	{
		if (firstNu.type != NALUnitType.___003C_003EIDR_SLICE && firstSliceHeader.refPicMarkingNonIDR != null)
		{
			RefPicMarking.Instruction[] instructions = firstSliceHeader.refPicMarkingNonIDR.getInstructions();
			for (int i = 0; i < (nint)instructions.LongLength; i++)
			{
				RefPicMarking.Instruction instruction = instructions[i];
				if (instruction.getType() == RefPicMarking.InstrType.___003C_003ECLEAR)
				{
					return true;
				}
			}
		}
		return false;
	}
}
