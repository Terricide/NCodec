using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class DecoderState : Object
{
	internal int[] chromaQpOffset;

	internal int qp;

	internal byte[][] leftRow;

	internal byte[][] topLine;

	internal byte[][] topLeft;

	internal ColorSpace chromaFormat;

	internal H264Utils.MvList mvTop;

	internal H264Utils.MvList mvLeft;

	internal H264Utils.MvList mvTopLeft;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 105, 111, 191, 41, 146, 113, 109,
		141, 127, 12, 127, 11, 159, 13, 124
	})]
	public DecoderState(SliceHeader sh)
	{
		int mbWidth = sh.sps.picWidthInMbsMinus1 + 1;
		chromaQpOffset = new int[2]
		{
			sh.pps.chromaQpIndexOffset,
			(sh.pps.extended == null) ? sh.pps.chromaQpIndexOffset : sh.pps.extended.secondChromaQpIndexOffset
		};
		chromaFormat = sh.sps.chromaFormatIdc;
		mvTop = new H264Utils.MvList((mbWidth << 2) + 1);
		mvLeft = new H264Utils.MvList(4);
		mvTopLeft = new H264Utils.MvList(1);
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 3);
		leftRow = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 3);
		topLeft = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		int num2 = mbWidth << 4;
		array = new int[2];
		num = (array[1] = num2);
		num = (array[0] = 3);
		topLine = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		qp = sh.pps.picInitQpMinus26 + 26 + sh.sliceQpDelta;
	}
}
