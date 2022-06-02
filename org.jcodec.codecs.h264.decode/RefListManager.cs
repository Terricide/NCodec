using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.codecs.h264.decode;

public class RefListManager : Object
{
	private SliceHeader sh;

	private int[] numRef;

	private org.jcodec.codecs.h264.io.model.Frame[] sRefs;

	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	private IntObjectMap lRefs;

	private org.jcodec.codecs.h264.io.model.Frame frameOut;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 66, 109, 153, 143, 99, 126, 112, 108,
		127, 1, 229, 60, 236, 71, 110, 104, 118, 61,
		201, 137
	})]
	private org.jcodec.codecs.h264.io.model.Frame[] buildRefListP()
	{
		int frame_num = sh.frameNum;
		int maxFrames = 1 << sh.sps.log2MaxFrameNumMinus4 + 4;
		org.jcodec.codecs.h264.io.model.Frame[] result = new org.jcodec.codecs.h264.io.model.Frame[numRef[0]];
		int refs = 0;
		for (int j = frame_num - 1; j >= frame_num - maxFrames; j += -1)
		{
			if (refs >= numRef[0])
			{
				break;
			}
			int fn = ((j >= 0) ? j : (j + maxFrames));
			if (sRefs[fn] != null)
			{
				result[refs] = ((sRefs[fn] != H264Const.___003C_003ENO_PIC) ? sRefs[fn] : null);
				refs++;
			}
		}
		int[] keys = lRefs.keys();
		Arrays.sort(keys);
		for (int i = 0; i < (nint)keys.LongLength; i++)
		{
			if (refs >= numRef[0])
			{
				break;
			}
			int num = refs;
			refs++;
			result[num] = (org.jcodec.codecs.h264.io.model.Frame)lRefs.get(keys[i]);
		}
		reorder(result, 0);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 114, 146, 116, 101, 103, 165, 159,
		21, 107, 139
	})]
	private org.jcodec.codecs.h264.io.model.Frame[][] buildRefListB()
	{
		org.jcodec.codecs.h264.io.model.Frame[] l0 = buildList(org.jcodec.codecs.h264.io.model.Frame.POCDesc, org.jcodec.codecs.h264.io.model.Frame.POCAsc);
		org.jcodec.codecs.h264.io.model.Frame[] l1 = buildList(org.jcodec.codecs.h264.io.model.Frame.POCAsc, org.jcodec.codecs.h264.io.model.Frame.POCDesc);
		if (Platform.arrayEqualsObj(l0, l1) && count(l1) > 1)
		{
			org.jcodec.codecs.h264.io.model.Frame frame = l1[1];
			l1[1] = l1[0];
			l1[0] = frame;
		}
		org.jcodec.codecs.h264.io.model.Frame[][] result = new org.jcodec.codecs.h264.io.model.Frame[2][]
		{
			(org.jcodec.codecs.h264.io.model.Frame[])Platform.copyOfObj(l0, numRef[0]),
			(org.jcodec.codecs.h264.io.model.Frame[])Platform.copyOfObj(l1, numRef[1])
		};
		reorder(result[0], 0);
		reorder(result[1], 1);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 98, 112, 130, 109, 153, 121, 159, 6,
		125, 131, 125, 131, 145, 112, 41, 135, 108, 124,
		111, 15, 233, 50, 234, 83
	})]
	private void reorder(Picture[] result, int list)
	{
		if (sh.refPicReordering[list] == null)
		{
			return;
		}
		int predict = sh.frameNum;
		int maxFrames = 1 << sh.sps.log2MaxFrameNumMinus4 + 4;
		for (int ind = 0; ind < (nint)sh.refPicReordering[list][0].LongLength; ind++)
		{
			switch (sh.refPicReordering[list][0][ind])
			{
			case 0:
				predict = MathUtil.wrap(predict - sh.refPicReordering[list][1][ind] - 1, maxFrames);
				break;
			case 1:
				predict = MathUtil.wrap(predict + sh.refPicReordering[list][1][ind] + 1, maxFrames);
				break;
			case 2:
				throw new RuntimeException("long term");
			}
			for (int j = numRef[list] - 1; j > ind; j += -1)
			{
				result[j] = result[j - 1];
			}
			result[ind] = sRefs[predict];
			int i = ind + 1;
			int k = i;
			for (; i < numRef[list] && result[i] != null; i++)
			{
				if (result[i] != sRefs[predict])
				{
					int num = k;
					k++;
					result[num] = result[i];
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;)[Lorg/jcodec/codecs/h264/io/model/Frame;")]
	[LineNumberTable(new byte[]
	{
		159, 116, 66, 122, 111, 111, 105, 138, 100, 105,
		41, 143, 106, 41, 175, 110, 104, 107, 58, 175
	})]
	private org.jcodec.codecs.h264.io.model.Frame[] buildList(Comparator cmpFwd, Comparator cmpInv)
	{
		org.jcodec.codecs.h264.io.model.Frame[] refs = new org.jcodec.codecs.h264.io.model.Frame[(nint)sRefs.LongLength + lRefs.size()];
		org.jcodec.codecs.h264.io.model.Frame[] fwd = copySort(cmpFwd, frameOut);
		org.jcodec.codecs.h264.io.model.Frame[] inv = copySort(cmpInv, frameOut);
		int nFwd = count(fwd);
		int nInv = count(inv);
		int @ref = 0;
		int k = 0;
		while (k < nFwd)
		{
			refs[@ref] = fwd[k];
			k++;
			@ref++;
		}
		int j = 0;
		while (j < nInv)
		{
			refs[@ref] = inv[j];
			j++;
			@ref++;
		}
		int[] keys = lRefs.keys();
		Arrays.sort(keys);
		int i = 0;
		while (i < (nint)keys.LongLength)
		{
			refs[@ref] = (org.jcodec.codecs.h264.io.model.Frame)lRefs.get(keys[i]);
			i++;
			@ref++;
		}
		return refs;
	}

	[LineNumberTable(new byte[] { 159, 111, 98, 104, 102, 3, 167 })]
	private int count(org.jcodec.codecs.h264.io.model.Frame[] arr)
	{
		for (int nn = 0; nn < (nint)arr.LongLength; nn++)
		{
			if (arr[nn] == null)
			{
				return nn;
			}
		}
		return arr.Length;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;Lorg/jcodec/codecs/h264/io/model/Frame;)[Lorg/jcodec/codecs/h264/io/model/Frame;")]
	[LineNumberTable(new byte[] { 159, 109, 66, 121, 104, 110, 5, 167, 104 })]
	private org.jcodec.codecs.h264.io.model.Frame[] copySort(Comparator fwd, org.jcodec.codecs.h264.io.model.Frame dummy)
	{
		org.jcodec.codecs.h264.io.model.Frame[] copyOf = (org.jcodec.codecs.h264.io.model.Frame[])Platform.copyOfObj(sRefs, sRefs.Length);
		for (int i = 0; i < (nint)copyOf.LongLength; i++)
		{
			if (fwd.compare(dummy, copyOf[i]) > 0)
			{
				copyOf[i] = null;
			}
		}
		Arrays.sort(copyOf, fwd);
		return copyOf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 105, 104, 104, 104, 105, 159, 10,
		127, 18, 105
	})]
	public RefListManager(SliceHeader sh, org.jcodec.codecs.h264.io.model.Frame[] sRefs, IntObjectMap lRefs, org.jcodec.codecs.h264.io.model.Frame frameOut)
	{
		this.sh = sh;
		this.sRefs = sRefs;
		this.lRefs = lRefs;
		if (sh.numRefIdxActiveOverrideFlag)
		{
			numRef = new int[2]
			{
				sh.numRefIdxActiveMinus1[0] + 1,
				sh.numRefIdxActiveMinus1[1] + 1
			};
		}
		else
		{
			numRef = new int[2]
			{
				sh.pps.numRefIdxActiveMinus1[0] + 1,
				sh.pps.numRefIdxActiveMinus1[1] + 1
			};
		}
		this.frameOut = frameOut;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 104, 115, 119, 115, 168, 116, 103,
		106, 105, 106, 104, 31, 25, 7, 234, 71
	})]
	public virtual org.jcodec.codecs.h264.io.model.Frame[][] getRefList()
	{
		org.jcodec.codecs.h264.io.model.Frame[][] refList = null;
		if (sh.sliceType == SliceType.___003C_003EP)
		{
			refList = new org.jcodec.codecs.h264.io.model.Frame[2][]
			{
				buildRefListP(),
				null
			};
		}
		else if (sh.sliceType == SliceType.___003C_003EB)
		{
			refList = buildRefListB();
		}
		MBlockDecoderUtils.debugPrint("------");
		if (refList != null)
		{
			for (int j = 0; j < 2; j++)
			{
				if (refList[j] == null)
				{
					continue;
				}
				for (int i = 0; i < (nint)refList[j].LongLength; i++)
				{
					if (refList[j][i] != null)
					{
						MBlockDecoderUtils.debugPrint("REF[%d][%d]: ", Integer.valueOf(j), Integer.valueOf(i), Integer.valueOf(refList[j][i].getPOC()));
					}
				}
			}
		}
		return refList;
	}
}
