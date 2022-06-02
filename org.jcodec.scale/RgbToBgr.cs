using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class RgbToBgr : Object, Transform
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public RgbToBgr()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 138, 130, 124, 122, 113, 191, 17, 105, 105,
		104, 103, 105, 101, 235, 60, 231, 70
	})]
	public virtual void transform(Picture src, Picture dst)
	{
		if ((src.getColor() != ColorSpace.___003C_003ERGB && src.getColor() != ColorSpace.___003C_003EBGR) || (dst.getColor() != ColorSpace.___003C_003ERGB && dst.getColor() != ColorSpace.___003C_003EBGR))
		{
			string s = new StringBuilder().append("Expected RGB or BGR inputs, was: ").append(src.getColor()).append(", ")
				.append(dst.getColor())
				.toString();
			
			throw new IllegalArgumentException(s);
		}
		byte[] dataSrc = src.getPlaneData(0);
		byte[] dataDst = dst.getPlaneData(0);
		for (int i = 0; i < (nint)dataSrc.LongLength; i += 3)
		{
			int tmp = dataSrc[i + 2];
			dataDst[i + 2] = dataSrc[i];
			dataDst[i] = (byte)tmp;
			dataDst[i + 1] = dataSrc[i + 1];
		}
	}
}
