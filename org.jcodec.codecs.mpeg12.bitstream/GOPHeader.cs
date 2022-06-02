using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class GOPHeader : Object, MPEGHeader
{
	private TapeTimecode timeCode;

	private bool closedGop;

	private bool brokenLink;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 97, 69, 105, 104, 104, 104 })]
	public GOPHeader(TapeTimecode timeCode, bool closedGop, bool brokenLink)
	{
		this.timeCode = timeCode;
		this.closedGop = closedGop;
		this.brokenLink = brokenLink;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 104, 111, 106, 106, 137, 107, 139,
		112, 144
	})]
	public static GOPHeader read(ByteBuffer bb)
	{
		BitReader _in = BitReader.createBitReader(bb);
		int dropFrame = ((_in.read1Bit() == 1) ? 1 : 0);
		int hours = (short)_in.readNBit(5);
		int minutes = (sbyte)_in.readNBit(6);
		_in.skip(1);
		int seconds = (sbyte)_in.readNBit(6);
		int frames = (sbyte)_in.readNBit(6);
		int closedGop = ((_in.read1Bit() == 1) ? 1 : 0);
		int brokenLink = ((_in.read1Bit() == 1) ? 1 : 0);
		GOPHeader result = new GOPHeader(new TapeTimecode((short)hours, (byte)minutes, (byte)seconds, (byte)frames, (byte)dropFrame != 0, 0), (byte)closedGop != 0, (byte)brokenLink != 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 104, 105, 143, 120, 115, 116, 104,
		116, 148, 115, 115, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		if (timeCode == null)
		{
			bw.writeNBit(0, 25);
		}
		else
		{
			bw.write1Bit(timeCode.isDropFrame() ? 1 : 0);
			bw.writeNBit(timeCode.getHour(), 5);
			bw.writeNBit((sbyte)timeCode.getMinute(), 6);
			bw.write1Bit(1);
			bw.writeNBit((sbyte)timeCode.getSecond(), 6);
			bw.writeNBit((sbyte)timeCode.getFrame(), 6);
		}
		bw.write1Bit(closedGop ? 1 : 0);
		bw.write1Bit(brokenLink ? 1 : 0);
		bw.flush();
	}

	[LineNumberTable(62)]
	public virtual TapeTimecode getTimeCode()
	{
		return timeCode;
	}

	[LineNumberTable(66)]
	public virtual bool isClosedGop()
	{
		return closedGop;
	}

	[LineNumberTable(70)]
	public virtual bool isBrokenLink()
	{
		return brokenLink;
	}
}
