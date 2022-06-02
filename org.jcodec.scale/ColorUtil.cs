using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.common.model;

namespace org.jcodec.scale;

public class ColorUtil : Object
{
	public class Idential : Object, Transform
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(66)]
		public Idential()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 125, 98, 121, 115, 52, 38, 167, 104, 104,
			103, 121, 107, 52, 38, 199
		})]
		public virtual void transform(Picture src, Picture dst)
		{
			for (int j = 0; j < Math.min(src.getData().Length, dst.getData().Length); j++)
			{
				ByteCodeHelper.arraycopy_primitive_1(src.getPlaneData(j), 0, dst.getPlaneData(j), 0, Math.min(src.getPlaneData(j).Length, dst.getPlaneData(j).Length));
			}
			byte[][] srcLowBits = src.getLowBits();
			byte[][] dstLowBits = dst.getLowBits();
			if (srcLowBits != null && dstLowBits != null)
			{
				for (int i = 0; i < Math.min(src.getData().Length, dst.getData().Length); i++)
				{
					ByteCodeHelper.arraycopy_primitive_1(srcLowBits[i], 0, dstLowBits[i], 0, Math.min(src.getPlaneData(i).Length, dst.getPlaneData(i).Length));
				}
			}
		}
	}

	[Signature("Ljava/util/Map<Lorg/jcodec/common/model/ColorSpace;Ljava/util/Map<Lorg/jcodec/common/model/ColorSpace;Lorg/jcodec/scale/Transform;>;>;")]
	private static Map map;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 146 })]
	public static Transform getTransform(ColorSpace from, ColorSpace to)
	{
		Map map2 = (Map)map.get(from);
		return (map2 != null) ? ((Transform)map2.get(to)) : null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public ColorUtil()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 162, 171, 103, 114, 114, 114, 114, 146,
		103, 114, 114, 114, 114, 146, 103, 114, 114, 114,
		114, 146, 103, 114, 146, 104, 115, 115, 147, 104,
		115, 115, 115, 115, 115
	})]
	static ColorUtil()
	{
		map = new HashMap();
		HashMap rgb = new HashMap();
		((Map)rgb).put((object)ColorSpace.___003C_003ERGB, (object)new Idential());
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV420J, (object)new RgbToYuv420j());
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV420, (object)new RgbToYuv420p());
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV422, (object)new RgbToYuv422p());
		map.put(ColorSpace.___003C_003ERGB, rgb);
		HashMap yuv420 = new HashMap();
		((Map)yuv420).put((object)ColorSpace.___003C_003EYUV420, (object)new Idential());
		((Map)yuv420).put((object)ColorSpace.___003C_003EYUV422, (object)new Yuv420pToYuv422p());
		((Map)yuv420).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv420pToRgb());
		((Map)yuv420).put((object)ColorSpace.___003C_003EYUV420J, (object)new Idential());
		map.put(ColorSpace.___003C_003EYUV420, yuv420);
		HashMap yuv421 = new HashMap();
		((Map)yuv421).put((object)ColorSpace.___003C_003EYUV422, (object)new Idential());
		((Map)yuv421).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv422pToYuv420p());
		((Map)yuv421).put((object)ColorSpace.___003C_003EYUV420J, (object)new Yuv422pToYuv420p());
		((Map)yuv421).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv422pToRgb());
		map.put(ColorSpace.___003C_003EYUV422, yuv421);
		HashMap yuv422 = new HashMap();
		((Map)yuv422).put((object)ColorSpace.___003C_003EYUV444, (object)new Idential());
		map.put(ColorSpace.___003C_003EYUV444, yuv422);
		HashMap yuv444j = new HashMap();
		((Map)yuv444j).put((object)ColorSpace.___003C_003EYUV444J, (object)new Idential());
		((Map)yuv444j).put((object)ColorSpace.___003C_003EYUV420J, (object)new Yuv444jToYuv420j());
		map.put(ColorSpace.___003C_003EYUV444J, yuv444j);
		HashMap yuv420j = new HashMap();
		((Map)yuv420j).put((object)ColorSpace.___003C_003EYUV420J, (object)new Idential());
		((Map)yuv420j).put((object)ColorSpace.___003C_003EYUV422, (object)new Yuv420pToYuv422p());
		((Map)yuv420j).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv420jToRgb());
		((Map)yuv420j).put((object)ColorSpace.___003C_003EYUV420, (object)new Idential());
		map.put(ColorSpace.___003C_003EYUV420J, yuv420j);
	}
}
