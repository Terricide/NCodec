using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlFloat : EbmlBin
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 106 })]
	public EbmlFloat(byte[] id)
		: base(id)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 110, 104, 107, 104, 136, 112, 104,
		106, 104, 168
	})]
	public virtual void setDouble(double value)
	{
		if (value < 3.4028234663852886E+38)
		{
			ByteBuffer bb2 = ByteBuffer.allocate(4);
			bb2.putFloat((float)value);
			bb2.flip();
			data = bb2;
		}
		else if (value < double.MaxValue)
		{
			ByteBuffer bb = ByteBuffer.allocate(8);
			bb.putDouble(value);
			bb.flip();
			data = bb;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 111, 179 })]
	public virtual double getDouble()
	{
		if (data.limit() == 4)
		{
			return data.duplicate().getFloat();
		}
		double @double = data.duplicate().getDouble();
		
		return @double;
	}
}
