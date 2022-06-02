using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace org.jcodec.codecs.common.biari;

public class MQDecoder : Object
{
	private int range;

	private int value;

	private int availableBits;

	private int lastByte;

	private int decodedBytes;

	private InputStream @is;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 122, 98, 104, 119, 136, 114, 106, 153, 111 })]
	private void fetchByte()
	{
		availableBits = 8;
		if (decodedBytes > 0 && lastByte == 255)
		{
			availableBits = 7;
		}
		lastByte = @is.read();
		int shiftCarry = 8 - availableBits;
		value += lastByte << shiftCarry;
		decodedBytes++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 119, 98, 111, 111, 147, 111, 105, 105 })]
	private void renormalize()
	{
		value <<= 1;
		range <<= 1;
		range &= 65535;
		availableBits--;
		if (availableBits == 0)
		{
			fetchByte();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 105, 136, 108, 136, 103, 111, 103,
		153, 104
	})]
	public MQDecoder(InputStream @is)
	{
		this.@is = @is;
		range = 32768;
		value = 0;
		fetchByte();
		value <<= 8;
		fetchByte();
		value <<= availableBits - 1;
		availableBits = 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 174, 138, 111, 111, 110, 110, 137,
		147, 170, 104, 110, 137, 111, 111, 147, 170
	})]
	public virtual int decode(Context cm)
	{
		int rangeLps = MQConst.___003C_003EpLps[cm.getState()];
		if (value > rangeLps)
		{
			range -= rangeLps;
			value -= rangeLps;
			if (range < 32768)
			{
				while (range < 32768)
				{
					renormalize();
				}
				cm.setState(MQConst.___003C_003EtransitMPS[cm.getState()]);
			}
			return cm.getMps();
		}
		range = rangeLps;
		while (range < 32768)
		{
			renormalize();
		}
		if (MQConst.___003C_003EmpsSwitch[cm.getState()] != 0)
		{
			cm.setMps(1 - cm.getMps());
		}
		cm.setState(MQConst.___003C_003EtransitLPS[cm.getState()]);
		return 1 - cm.getMps();
	}
}
