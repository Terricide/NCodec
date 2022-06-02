using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4;

public class DataBoxes : Boxes
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 141, 162, 105, 119, 119, 119 })]
	public DataBoxes()
	{
		___003C_003Emappings.put(UrlBox.fourcc(), ClassLiteral<UrlBox>.Value);
		___003C_003Emappings.put(AliasBox.fourcc(), ClassLiteral<AliasBox>.Value);
		___003C_003Emappings.put("cios", ClassLiteral<AliasBox>.Value);
	}
}
