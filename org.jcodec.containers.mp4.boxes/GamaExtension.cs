using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class GamaExtension : Box
{
	private float gamma;

	[LineNumberTable(41)]
	public static string fourcc()
	{
		return "gama";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 106 })]
	public GamaExtension(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 118, 105 })]
	public static GamaExtension createGamaExtension(float gamma)
	{
		
		GamaExtension gamaExtension = new GamaExtension(new Header(fourcc()));
		gamaExtension.gamma = gamma;
		return gamaExtension;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 105, 111 })]
	public override void parse(ByteBuffer input)
	{
		float g = input.getInt();
		gamma = g / 65536f;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 122 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(ByteCodeHelper.f2i(gamma * 65536f));
	}

	[LineNumberTable(37)]
	public virtual float getGamma()
	{
		return gamma;
	}

	[LineNumberTable(46)]
	public override int estimateSize()
	{
		return 12;
	}
}
