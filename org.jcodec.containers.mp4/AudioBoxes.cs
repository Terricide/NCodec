using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class AudioBoxes : Boxes
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 140, 130, 105, 119, 119, 119 })]
	public AudioBoxes()
	{
		___003C_003Emappings.put(WaveExtension.fourcc(), ClassLiteral<WaveExtension>.Value);
		___003C_003Emappings.put(ChannelBox.fourcc(), ClassLiteral<ChannelBox>.Value);
		___003C_003Emappings.put("esds", ClassLiteral<Box.LeafBox>.Value);
	}
}
