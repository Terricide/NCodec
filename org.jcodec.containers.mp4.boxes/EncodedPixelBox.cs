using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.containers.mp4.boxes;

public class EncodedPixelBox : ClearApertureBox
{
	public const string ENOF = "enof";

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106 })]
	public EncodedPixelBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 113, 105, 105 })]
	public static EncodedPixelBox createEncodedPixelBox(int width, int height)
	{
		EncodedPixelBox enof = new EncodedPixelBox(new Header("enof"));
		enof.width = width;
		enof.height = height;
		return enof;
	}
}
