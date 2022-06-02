using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.codecs.ppm;

public class PPMEncoder : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public PPMEncoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 110, 113, 124, 159, 44, 104, 117,
		118, 118, 244, 61, 231, 70, 136
	})]
	public virtual ByteBuffer encodeFrame(Picture picture)
	{
		if (picture.getColor() != ColorSpace.___003C_003ERGB)
		{
			
			throw new IllegalArgumentException("Only RGB image can be stored in PPM");
		}
		ByteBuffer buffer = ByteBuffer.allocate(picture.getWidth() * picture.getHeight() * 3 + 200);
		buffer.put(JCodecUtil2.asciiString(new StringBuilder().append("P6 ").append(picture.getWidth()).append(" ")
			.append(picture.getHeight())
			.append(" 255\n")
			.toString()));
		byte[][] data = picture.getData();
		for (int i = 0; i < picture.getWidth() * picture.getHeight() * 3; i += 3)
		{
			buffer.put((byte)(sbyte)(data[0][i + 2] + 128));
			buffer.put((byte)(sbyte)(data[0][i + 1] + 128));
			buffer.put((byte)(sbyte)(data[0][i] + 128));
		}
		buffer.flip();
		return buffer;
	}
}
