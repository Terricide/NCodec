using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.containers.mp4.boxes;

public class ProductionApertureBox : ClearApertureBox
{
	public const string PROF = "prof";

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public ProductionApertureBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 113, 105, 105 })]
	public static ProductionApertureBox createProductionApertureBox(int width, int height)
	{
		ProductionApertureBox prof = new ProductionApertureBox(new Header("prof"));
		prof.width = width;
		prof.height = height;
		return prof;
	}
}
