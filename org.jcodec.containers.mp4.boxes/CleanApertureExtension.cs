using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class CleanApertureExtension : Box
{
	private int vertOffsetDenominator;

	private int vertOffsetNumerator;

	private int horizOffsetDenominator;

	private int horizOffsetNumerator;

	private int apertureHeightDenominator;

	private int apertureHeightNumerator;

	private int apertureWidthDenominator;

	private int apertureWidthNumerator;

	[LineNumberTable(55)]
	public static string fourcc()
	{
		return "clap";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public CleanApertureExtension(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 118, 104, 104, 104, 104, 105, 105,
		105, 105
	})]
	public static CleanApertureExtension createCleanApertureExtension(int apertureWidthN, int apertureWidthD, int apertureHeightN, int apertureHeightD, int horizOffN, int horizOffD, int vertOffN, int vertOffD)
	{
		
		CleanApertureExtension clap = new CleanApertureExtension(new Header(fourcc()));
		clap.apertureWidthNumerator = apertureWidthN;
		clap.apertureWidthDenominator = apertureWidthD;
		clap.apertureHeightNumerator = apertureHeightN;
		clap.apertureHeightDenominator = apertureHeightD;
		clap.horizOffsetNumerator = horizOffN;
		clap.horizOffsetDenominator = horizOffD;
		clap.vertOffsetNumerator = vertOffN;
		clap.vertOffsetDenominator = vertOffD;
		return clap;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 98, 109, 141, 109, 141, 109, 141, 109,
		109
	})]
	public override void parse(ByteBuffer @is)
	{
		apertureWidthNumerator = @is.getInt();
		apertureWidthDenominator = @is.getInt();
		apertureHeightNumerator = @is.getInt();
		apertureHeightDenominator = @is.getInt();
		horizOffsetNumerator = @is.getInt();
		horizOffsetDenominator = @is.getInt();
		vertOffsetNumerator = @is.getInt();
		vertOffsetDenominator = @is.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 110, 142, 110, 142, 110, 142, 110,
		110
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(apertureWidthNumerator);
		@out.putInt(apertureWidthDenominator);
		@out.putInt(apertureHeightNumerator);
		@out.putInt(apertureHeightDenominator);
		@out.putInt(horizOffsetNumerator);
		@out.putInt(horizOffsetDenominator);
		@out.putInt(vertOffsetNumerator);
		@out.putInt(vertOffsetDenominator);
	}

	[LineNumberTable(76)]
	public override int estimateSize()
	{
		return 40;
	}
}
