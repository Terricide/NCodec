using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class VideoMediaHeaderBox : FullBox
{
	internal int graphicsMode;

	internal int rOpColor;

	internal int gOpColor;

	internal int bOpColor;

	[LineNumberTable(19)]
	public static string fourcc()
	{
		return "vmhd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 106 })]
	public VideoMediaHeaderBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 118, 104, 104, 104, 104 })]
	public static VideoMediaHeaderBox createVideoMediaHeaderBox(int graphicsMode, int rOpColor, int gOpColor, int bOpColor)
	{
		
		VideoMediaHeaderBox vmhd = new VideoMediaHeaderBox(new Header(fourcc()));
		vmhd.graphicsMode = graphicsMode;
		vmhd.rOpColor = rOpColor;
		vmhd.gOpColor = gOpColor;
		vmhd.bOpColor = bOpColor;
		return vmhd;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 104, 109, 109, 109, 109 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		graphicsMode = input.getShort();
		rOpColor = input.getShort();
		gOpColor = input.getShort();
		bOpColor = input.getShort();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 104, 111, 111, 111, 111 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putShort((short)graphicsMode);
		@out.putShort((short)rOpColor);
		@out.putShort((short)gOpColor);
		@out.putShort((short)bOpColor);
	}

	[LineNumberTable(56)]
	public override int estimateSize()
	{
		return 20;
	}

	[LineNumberTable(60)]
	public virtual int getGraphicsMode()
	{
		return graphicsMode;
	}

	[LineNumberTable(64)]
	public virtual int getrOpColor()
	{
		return rOpColor;
	}

	[LineNumberTable(68)]
	public virtual int getgOpColor()
	{
		return gOpColor;
	}

	[LineNumberTable(72)]
	public virtual int getbOpColor()
	{
		return bOpColor;
	}
}
