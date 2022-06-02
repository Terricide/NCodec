using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.containers.mp4.boxes;

public class AVC1Box : VideoSampleEntry
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 115 })]
	public AVC1Box()
		: base(new Header("avc1"))
	{
	}
}
