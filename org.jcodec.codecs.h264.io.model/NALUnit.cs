using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.h264.io.model;

public class NALUnit : Object
{
	public NALUnitType type;

	public int nal_ref_idc;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 111, 103, 134, 104 })]
	public static NALUnit read(ByteBuffer _in)
	{
		int nalu = (sbyte)_in.get() & 0xFF;
		int nal_ref_idc = (nalu >> 5) & 3;
		int nb = nalu & 0x1F;
		NALUnitType type = NALUnitType.fromValue(nb);
		NALUnit result = new NALUnit(type, nal_ref_idc);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104 })]
	public NALUnit(NALUnitType type, int nal_ref_idc)
	{
		this.type = type;
		this.nal_ref_idc = nal_ref_idc;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 118, 106 })]
	public virtual void write(ByteBuffer @out)
	{
		int nalu = type.getValue() | (nal_ref_idc << 5);
		@out.put((byte)(sbyte)nalu);
	}
}
