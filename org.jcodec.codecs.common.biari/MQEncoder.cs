using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace org.jcodec.codecs.common.biari;

public class MQEncoder : Object
{
	public const int CARRY_MASK = 134217728;

	private int range;

	private int offset;

	private int bitsToCode;

	private long bytesOutput;

	private int byteToGo;

	private OutputStream @out;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 119, 162, 111, 111, 150, 143, 105, 105 })]
	private void renormalize()
	{
		offset <<= 1;
		range <<= 1;
		range = (int)(range & 0xFFFFu);
		bitsToCode--;
		if (bitsToCode == 0)
		{
			outputByte();
		}
	}

	[LineNumberTable(new byte[] { 159, 121, 66, 110, 115, 100, 149, 147 })]
	private void finalizeValue()
	{
		int halfBit = offset & 0x8000;
		offset &= -65536;
		if (halfBit == 0)
		{
			offset |= 32768;
		}
		else
		{
			offset += 65536;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 116, 130, 107, 140, 110, 137, 111, 111, 115,
		110, 137, 137, 169
	})]
	private void outputByte()
	{
		if (bytesOutput == 0u)
		{
			outputByteNoStuffing();
		}
		else if (byteToGo == 255)
		{
			outputByteWithStuffing();
		}
		else if (((uint)offset & 0x8000000u) != 0)
		{
			byteToGo++;
			offset &= 134217727;
			if (byteToGo == 255)
			{
				outputByteWithStuffing();
			}
			else
			{
				outputByteNoStuffing();
			}
		}
		else
		{
			outputByteNoStuffing();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 108, 98, 104, 107, 146, 150, 147, 112 })]
	private void outputByteNoStuffing()
	{
		bitsToCode = 8;
		if (bytesOutput > 0u)
		{
			@out.write(byteToGo);
		}
		byteToGo = (offset >> 19) & 0xFF;
		offset &= 524287;
		bytesOutput++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 111, 130, 104, 107, 146, 118, 147, 112 })]
	private void outputByteWithStuffing()
	{
		bitsToCode = 7;
		if (bytesOutput > 0u)
		{
			@out.write(byteToGo);
		}
		byteToGo = (offset >> 20) & 0xFF;
		offset &= 1048575;
		bytesOutput++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 105, 108, 104, 105, 104 })]
	public MQEncoder(OutputStream @out)
	{
		range = 32768;
		offset = 0;
		bitsToCode = 12;
		this.@out = @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 130, 66, 110, 106, 111, 111, 113, 110, 105,
		181, 104, 110, 137, 111, 111, 149
	})]
	public virtual void encode(int symbol, Context cm)
	{
		int rangeLps = MQConst.___003C_003EpLps[cm.getState()];
		if (symbol == cm.getMps())
		{
			range -= rangeLps;
			offset += rangeLps;
			if (range < 32768)
			{
				while (range < 32768)
				{
					renormalize();
				}
				cm.setState(MQConst.___003C_003EtransitMPS[cm.getState()]);
			}
		}
		else
		{
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
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 125, 98, 103, 151, 107, 103, 106, 101, 119,
		167, 116
	})]
	public virtual void finish()
	{
		finalizeValue();
		offset <<= bitsToCode;
		int bitsToOutput = 12 - bitsToCode;
		outputByte();
		bitsToOutput -= bitsToCode;
		if (bitsToOutput > 0)
		{
			offset <<= bitsToCode;
			outputByte();
		}
		@out.write(byteToGo);
	}
}
