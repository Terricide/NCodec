using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace org.jcodec.containers.mp4.boxes;

public class PartialSyncSamplesBox : SyncSamplesBox
{
	public const string STPS = "stps";

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 66, 106 })]
	public PartialSyncSamplesBox(Header header)
		: base(header)
	{
	}
}
