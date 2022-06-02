using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.nio.channels;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.io;

namespace org.jcodec.codecs.h264.io.write;

public class NALUnitWriter : Object
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private WritableByteChannel to;

	private static ByteBuffer _MARKER;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 162, 104, 101, 105, 105, 109, 99, 99,
		169, 99, 99, 105, 99
	})]
	private void emprev(ByteBuffer emprev, ByteBuffer data)
	{
		ByteBuffer dd = data.duplicate();
		int prev1 = 1;
		int prev2 = 1;
		while (dd.hasRemaining())
		{
			int b = (sbyte)dd.get();
			if (prev1 == 0 && prev2 == 0 && (b & 3) == b)
			{
				prev2 = prev1;
				prev1 = 3;
				emprev.put(3);
			}
			prev2 = prev1;
			prev1 = b;
			emprev.put((byte)b);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 104 })]
	public NALUnitWriter(WritableByteChannel to)
	{
		this.to = to;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 130, 115, 108, 104, 105, 104, 110 })]
	public virtual void writeUnit(NALUnit nal, ByteBuffer data)
	{
		ByteBuffer emprev = ByteBuffer.allocate(data.remaining() + 1024);
		NIOUtils.write(emprev, _MARKER);
		nal.write(emprev);
		this.emprev(emprev, data);
		emprev.flip();
		to.write(emprev);
	}

	[LineNumberTable(new byte[] { 159, 138, 130, 172, 109, 108 })]
	static NALUnitWriter()
	{
		_MARKER = ByteBuffer.allocate(4);
		_MARKER.putInt(1);
		_MARKER.flip();
	}
}
