using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4.boxes;

public class PixelAspectExt : Box
{
	private int hSpacing;

	private int vSpacing;

	[LineNumberTable(59)]
	public static string fourcc()
	{
		return "pasp";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 106 })]
	public PixelAspectExt(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 118, 109, 109 })]
	public static PixelAspectExt createPixelAspectExt(Rational par)
	{
		
		PixelAspectExt pasp = new PixelAspectExt(new Header(fourcc()));
		pasp.hSpacing = par.getNum();
		pasp.vSpacing = par.getDen();
		return pasp;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 109, 109 })]
	public override void parse(ByteBuffer input)
	{
		hSpacing = input.getInt();
		vSpacing = input.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 110, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(hSpacing);
		@out.putInt(vSpacing);
	}

	[LineNumberTable(43)]
	public override int estimateSize()
	{
		return 16;
	}

	[LineNumberTable(47)]
	public virtual int gethSpacing()
	{
		return hSpacing;
	}

	[LineNumberTable(51)]
	public virtual int getvSpacing()
	{
		return vSpacing;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(55)]
	public virtual Rational getRational()
	{
		Rational.___003Cclinit_003E();
		Rational result = new Rational(hSpacing, vSpacing);
		
		return result;
	}
}
