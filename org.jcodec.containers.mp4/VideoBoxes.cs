using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using org.jcodec.codecs.h264.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class VideoBoxes : Boxes
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 140, 162, 105, 119, 119, 119, 119, 119, 119 })]
	public VideoBoxes()
	{
		___003C_003Emappings.put(PixelAspectExt.fourcc(), ClassLiteral<PixelAspectExt>.Value);
		___003C_003Emappings.put(AvcCBox.fourcc(), ClassLiteral<AvcCBox>.Value);
		___003C_003Emappings.put(ColorExtension.fourcc(), ClassLiteral<ColorExtension>.Value);
		___003C_003Emappings.put(GamaExtension.fourcc(), ClassLiteral<GamaExtension>.Value);
		___003C_003Emappings.put(CleanApertureExtension.fourcc(), ClassLiteral<CleanApertureExtension>.Value);
		___003C_003Emappings.put(FielExtension.fourcc(), ClassLiteral<FielExtension>.Value);
	}
}
