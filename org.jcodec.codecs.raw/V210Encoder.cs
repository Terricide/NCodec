using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.raw;

public class V210Encoder : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 123, 65, 68 })]
	internal static int clip(byte val)
	{
		int val2 = (sbyte)val;
		int result = MathUtil.clip(val2 + 128 << 2, 8, 1019);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public V210Encoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 104, 109, 114, 136, 104, 107, 139,
		106, 113, 116, 117, 149, 114, 100, 121, 120, 118,
		138, 100, 117, 120, 121, 138, 100, 121, 120, 118,
		138, 100, 117, 120, 121, 106, 102, 109, 109, 237,
		32, 236, 98, 136
	})]
	public virtual ByteBuffer encodeFrame(ByteBuffer _out, Picture frame)
	{
		ByteBuffer @out = _out.duplicate();
		@out.order(ByteOrder.LITTLE_ENDIAN);
		int tgtStride = (frame.getPlaneWidth(0) + 47) / 48 * 48;
		byte[][] data = frame.getData();
		byte[] tmpY = new byte[tgtStride];
		byte[] tmpCb = new byte[tgtStride >> 1];
		byte[] tmpCr = new byte[tgtStride >> 1];
		int yOff = 0;
		int cbOff = 0;
		int crOff = 0;
		for (int yy = 0; yy < frame.getHeight(); yy++)
		{
			ByteCodeHelper.arraycopy_primitive_1(data[0], yOff, tmpY, 0, frame.getPlaneWidth(0));
			ByteCodeHelper.arraycopy_primitive_1(data[1], cbOff, tmpCb, 0, frame.getPlaneWidth(1));
			ByteCodeHelper.arraycopy_primitive_1(data[2], crOff, tmpCr, 0, frame.getPlaneWidth(2));
			int yi = 0;
			int cbi = 0;
			int cri = 0;
			while (yi < tgtStride)
			{
				int i = 0;
				int num = i;
				int num2 = cri;
				cri++;
				i = num | (clip(tmpCr[num2]) << 20);
				int num3 = i;
				int num4 = yi;
				yi++;
				i = num3 | (clip(tmpY[num4]) << 10);
				int num5 = i;
				int num6 = cbi;
				cbi++;
				i = num5 | clip(tmpCb[num6]);
				@out.putInt(i);
				i = 0;
				int num7 = i;
				int num8 = yi;
				yi++;
				i = num7 | clip(tmpY[num8]);
				int num9 = i;
				int num10 = yi;
				yi++;
				i = num9 | (clip(tmpY[num10]) << 20);
				int num11 = i;
				int num12 = cbi;
				cbi++;
				i = num11 | (clip(tmpCb[num12]) << 10);
				@out.putInt(i);
				i = 0;
				int num13 = i;
				int num14 = cbi;
				cbi++;
				i = num13 | (clip(tmpCb[num14]) << 20);
				int num15 = i;
				int num16 = yi;
				yi++;
				i = num15 | (clip(tmpY[num16]) << 10);
				int num17 = i;
				int num18 = cri;
				cri++;
				i = num17 | clip(tmpCr[num18]);
				@out.putInt(i);
				i = 0;
				int num19 = i;
				int num20 = yi;
				yi++;
				i = num19 | clip(tmpY[num20]);
				int num21 = i;
				int num22 = yi;
				yi++;
				i = num21 | (clip(tmpY[num22]) << 20);
				int num23 = i;
				int num24 = cri;
				cri++;
				i = num23 | (clip(tmpCr[num24]) << 10);
				@out.putInt(i);
			}
			yOff += frame.getPlaneWidth(0);
			cbOff += frame.getPlaneWidth(1);
			crOff += frame.getPlaneWidth(2);
		}
		@out.flip();
		return @out;
	}
}
