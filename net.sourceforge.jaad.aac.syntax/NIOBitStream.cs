using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace net.sourceforge.jaad.aac.syntax;

public class NIOBitStream : Object, IBitStream
{
	private BitReader br;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(40)]
	public virtual void reset()
	{
		
		throw new RuntimeException("todo");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 127, 162, 111, 143 })]
	public virtual int readBit()
	{
		if (br.remaining() >= 1)
		{
			int result = br.read1Bit();
			
			return result;
		}
		throw AACException.endOfStream();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 120, 98, 110 })]
	public virtual void skipBits(int n)
	{
		br.skip(n);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104 })]
	public NIOBitStream(BitReader br)
	{
		this.br = br;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 103, 104 })]
	public virtual void destroy()
	{
		reset();
		br = null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 114 })]
	public virtual void setData(byte[] data)
	{
		br = BitReader.createBitReader(ByteBuffer.wrap(data));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 134, 162, 109 })]
	public virtual void byteAlign()
	{
		br.align();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(45)]
	public virtual int getPosition()
	{
		int result = br.position();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(50)]
	public virtual int getBitsLeft()
	{
		int result = br.remaining();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 129, 162, 111, 144 })]
	public virtual int readBits(int n)
	{
		if (br.remaining() >= n)
		{
			int result = br.readNBit(n);
			
			return result;
		}
		throw AACException.endOfStream();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 104 })]
	public virtual bool readBool()
	{
		return (readBit() != 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 123, 98, 110 })]
	public virtual int peekBits(int n)
	{
		return br.checkNBit(n);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 122, 162, 109 })]
	public virtual int peekBit()
	{
		return br.curBit();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 119, 130, 106 })]
	public virtual void skipBit()
	{
		skipBits(1);
	}

	[LineNumberTable(new byte[] { 159, 117, 66, 102, 133, 106 })]
	public virtual int maskBits(int n)
	{
		if (n == 32)
		{
			return -1;
		}
		return (1 << n) - 1;
	}
}
