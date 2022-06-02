using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class WaveExtBoxes : Boxes
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 141, 162, 105, 119, 151 })]
	public WaveExtBoxes()
	{
		___003C_003Emappings.put(FormatBox.fourcc(), ClassLiteral<FormatBox>.Value);
		___003C_003Emappings.put(EndianBox.fourcc(), ClassLiteral<EndianBox>.Value);
	}
}
