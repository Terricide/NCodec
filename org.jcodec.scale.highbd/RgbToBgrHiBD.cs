using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.api;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

[Obsolete]
[Deprecated(new object[]
{
	(byte)64,
	"Ljava/lang/Deprecated;"
})]
public class RgbToBgrHiBD : java.lang.Object, TransformHiBD
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public RgbToBgrHiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 66, 124, 122, 113, 159, 17, 113, 145,
		105, 105, 136, 103, 105, 229, 60, 231, 70
	})]
	public virtual void transform(PictureHiBD src, PictureHiBD dst)
	{
		if ((src.getColor() != ColorSpace.___003C_003ERGB && src.getColor() != ColorSpace.___003C_003EBGR) || (dst.getColor() != ColorSpace.___003C_003ERGB && dst.getColor() != ColorSpace.___003C_003EBGR))
		{
			string s = new StringBuilder().append("Expected RGB or BGR inputs, was: ").append(src.getColor()).append(", ")
				.append(dst.getColor())
				.toString();
			
			throw new IllegalArgumentException(s);
		}
		if (src.getCrop() != null || dst.getCrop() != null)
		{
			
			throw new org.jcodec.api.NotSupportedException("Cropped images not supported");
		}
		int[] dataSrc = src.getPlaneData(0);
		int[] dataDst = dst.getPlaneData(0);
		for (int i = 0; i < (nint)dataSrc.LongLength; i += 3)
		{
			int tmp = dataSrc[i + 2];
			dataDst[i + 2] = dataSrc[i];
			dataDst[i] = tmp;
		}
	}
}
