using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.common.model;

public class AudioBuffer : Object
{
	protected internal ByteBuffer data;

	protected internal AudioFormat format;

	protected internal int nFrames;

	[LineNumberTable(31)]
	public virtual AudioFormat getFormat()
	{
		return format;
	}

	[LineNumberTable(27)]
	public virtual ByteBuffer getData()
	{
		return data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104, 104, 104 })]
	public AudioBuffer(ByteBuffer data, AudioFormat format, int nFrames)
	{
		this.data = data;
		this.format = format;
		this.nFrames = nFrames;
	}

	[LineNumberTable(35)]
	public virtual int getNFrames()
	{
		return nFrames;
	}
}
