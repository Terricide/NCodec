using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.vpx;

public class VPXBooleanEncoder : Object
{
	private ByteBuffer @out;

	private int lowvalue;

	private int range;

	private int count;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 104, 108, 105 })]
	public VPXBooleanEncoder(ByteBuffer @out)
	{
		this.@out = @out;
		lowvalue = 0;
		range = 255;
		count = -24;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(74)]
	public virtual int position()
	{
		return @out.position() + (count + 24 >> 3);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 98, 104, 45, 135 })]
	public virtual void stop()
	{
		for (int i = 0; i < 32; i++)
		{
			writeBit(128, 0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 144, 100, 111, 145, 168, 110, 114,
		143, 109, 138, 121, 143, 117, 111, 167, 191, 5,
		124, 114, 104, 115, 175, 114
	})]
	public virtual void writeBit(int prob, int bb)
	{
		int split = 1 + ((range - 1) * prob >> 8);
		if (bb != 0)
		{
			lowvalue += split;
			range -= split;
		}
		else
		{
			range = split;
		}
		int shift = VPXConst.___003C_003Evp8Norm[range];
		range <<= shift;
		count += shift;
		if (count >= 0)
		{
			int offset = shift - count;
			if (((uint)(lowvalue << offset - 1) & 0x80000000u) != 0)
			{
				int x;
				for (x = @out.position() - 1; x >= 0 && (sbyte)@out.get(x) == -1; x += -1)
				{
					@out.put(x, 0);
				}
				@out.put(x, (byte)(sbyte)(((sbyte)@out.get(x) & 0xFF) + 1));
			}
			@out.put((byte)(sbyte)(lowvalue >> 24 - offset));
			lowvalue <<= offset;
			shift = count;
			lowvalue &= 16777215;
			count -= 8;
		}
		lowvalue <<= shift;
	}
}
