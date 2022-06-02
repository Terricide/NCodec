using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class ColorExtension : Box
{
	private short primariesIndex;

	private short transferFunctionIndex;

	private short matrixIndex;

	private string type;

	internal const byte RANGE_UNSPECIFIED = 0;

	internal const byte AVCOL_RANGE_MPEG = 1;

	internal const byte AVCOL_RANGE_JPEG = 2;

	private Byte colorRange;

	[LineNumberTable(67)]
	public static string fourcc()
	{
		return "colr";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 234, 56, 236, 69, 200 })]
	public ColorExtension(Header header)
		: base(header)
	{
		type = "nclc";
		colorRange = null;
	}

	[LineNumberTable(new byte[] { 159, 134, 130, 104 })]
	public virtual void setColorRange(Byte colorRange)
	{
		this.colorRange = colorRange;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 162, 104, 105, 109, 109, 109, 109, 105,
		147
	})]
	public override void parse(ByteBuffer input)
	{
		byte[] dst = new byte[4];
		input.get(dst);
		type = Platform.stringFromBytes(dst);
		primariesIndex = input.getShort();
		transferFunctionIndex = input.getShort();
		matrixIndex = input.getShort();
		if (input.hasRemaining())
		{
			colorRange = Byte.valueOf((byte)(sbyte)input.get());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 115, 110, 110, 110, 105, 148 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(JCodecUtil2.asciiString(type));
		@out.putShort(primariesIndex);
		@out.putShort(transferFunctionIndex);
		@out.putShort(matrixIndex);
		if (colorRange != null)
		{
			@out.put((byte)(sbyte)colorRange.byteValue());
		}
	}

	[LineNumberTable(63)]
	public override int estimateSize()
	{
		return 16;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 65, 71, 118, 104, 104, 104 })]
	public static ColorExtension createColorExtension(short primariesIndex, short transferFunctionIndex, short matrixIndex)
	{
		
		ColorExtension c = new ColorExtension(new Header(fourcc()));
		c.primariesIndex = primariesIndex;
		c.transferFunctionIndex = transferFunctionIndex;
		c.matrixIndex = matrixIndex;
		return c;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(80)]
	public static ColorExtension createColr()
	{
		
		ColorExtension result = new ColorExtension(new Header(fourcc()));
		
		return result;
	}

	[LineNumberTable(84)]
	public virtual short getPrimariesIndex()
	{
		return primariesIndex;
	}

	[LineNumberTable(88)]
	public virtual short getTransferFunctionIndex()
	{
		return transferFunctionIndex;
	}

	[LineNumberTable(92)]
	public virtual short getMatrixIndex()
	{
		return matrixIndex;
	}
}
