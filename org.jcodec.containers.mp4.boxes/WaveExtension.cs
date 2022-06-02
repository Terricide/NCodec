using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.containers.mp4.boxes;

public class WaveExtension : NodeBox
{
	[LineNumberTable(14)]
	public static string fourcc()
	{
		return "wave";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 138 })]
	public WaveExtension(Header atom)
		: base(atom)
	{
	}
}
