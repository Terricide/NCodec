using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.common;

public class CodecMeta : Object
{
	private string fourcc;

	private ByteBuffer codecPrivate;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105, 104, 104 })]
	public CodecMeta(string fourcc, ByteBuffer codecPrivate)
	{
		this.fourcc = fourcc;
		this.codecPrivate = codecPrivate;
	}

	[LineNumberTable(25)]
	public virtual string getFourcc()
	{
		return fourcc;
	}

	[LineNumberTable(29)]
	public virtual ByteBuffer getCodecPrivate()
	{
		return codecPrivate;
	}
}
