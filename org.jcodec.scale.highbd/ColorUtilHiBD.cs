using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.common.model;

namespace org.jcodec.scale.highbd;

public class ColorUtilHiBD : Object
{
	public class Idential : Object, TransformHiBD
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(90)]
		public Idential()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 98, 103, 115, 61, 43, 199 })]
		public virtual void transform(PictureHiBD src, PictureHiBD dst)
		{
			for (int i = 0; i < 3; i++)
			{
				ByteCodeHelper.arraycopy_primitive_4(src.getPlaneData(i), 0, dst.getPlaneData(i), 0, Math.min(src.getPlaneWidth(i) * src.getPlaneHeight(i), dst.getPlaneWidth(i) * dst.getPlaneHeight(i)));
			}
		}
	}

	[Signature("Ljava/util/Map<Lorg/jcodec/common/model/ColorSpace;Ljava/util/Map<Lorg/jcodec/common/model/ColorSpace;Lorg/jcodec/scale/highbd/TransformHiBD;>;>;")]
	private static Map map;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(17)]
	public ColorUtilHiBD()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 98, 146 })]
	public static TransformHiBD getTransform(ColorSpace from, ColorSpace to)
	{
		Map map2 = (Map)map.get(from);
		return (map2 != null) ? ((TransformHiBD)map2.get(to)) : null;
	}

	[LineNumberTable(new byte[]
	{
		159, 138, 162, 171, 103, 114, 116, 114, 116, 116,
		146, 103, 114, 116, 116, 116, 146, 103, 114, 116,
		116, 116, 146, 103, 114, 116, 116, 116, 146, 104,
		115, 117, 117, 147, 104, 115, 117, 117, 147, 104,
		115, 115, 115, 147, 104, 115, 115, 115, 117, 147,
		104, 115, 115, 115, 117, 115
	})]
	static ColorUtilHiBD()
	{
		map = new HashMap();
		HashMap rgb = new HashMap();
		((Map)rgb).put((object)ColorSpace.___003C_003ERGB, (object)new Idential());
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV420, (object)new RgbToYuv420pHiBD(0, 0));
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV420J, (object)new RgbToYuv420jHiBD());
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV422, (object)new RgbToYuv422pHiBD(0, 0));
		((Map)rgb).put((object)ColorSpace.___003C_003EYUV422_10, (object)new RgbToYuv422pHiBD(2, 0));
		map.put(ColorSpace.___003C_003ERGB, rgb);
		HashMap yuv420 = new HashMap();
		((Map)yuv420).put((object)ColorSpace.___003C_003EYUV420, (object)new Idential());
		((Map)yuv420).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv420pToRgbHiBD(0, 0));
		((Map)yuv420).put((object)ColorSpace.___003C_003EYUV422, (object)new Yuv420pToYuv422pHiBD(0, 0));
		((Map)yuv420).put((object)ColorSpace.___003C_003EYUV422_10, (object)new Yuv420pToYuv422pHiBD(0, 2));
		map.put(ColorSpace.___003C_003EYUV420, yuv420);
		HashMap yuv421 = new HashMap();
		((Map)yuv421).put((object)ColorSpace.___003C_003EYUV422, (object)new Idential());
		((Map)yuv421).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv422pToRgbHiBD(0, 0));
		((Map)yuv421).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv422pToYuv420pHiBD(0, 0));
		((Map)yuv421).put((object)ColorSpace.___003C_003EYUV420J, (object)new Yuv422pToYuv420jHiBD(0, 0));
		map.put(ColorSpace.___003C_003EYUV422, yuv421);
		HashMap yuv422_10 = new HashMap();
		((Map)yuv422_10).put((object)ColorSpace.___003C_003EYUV422_10, (object)new Idential());
		((Map)yuv422_10).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv422pToRgbHiBD(2, 0));
		((Map)yuv422_10).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv422pToYuv420pHiBD(0, 2));
		((Map)yuv422_10).put((object)ColorSpace.___003C_003EYUV420J, (object)new Yuv422pToYuv420jHiBD(0, 2));
		map.put(ColorSpace.___003C_003EYUV422_10, yuv422_10);
		HashMap yuv422 = new HashMap();
		((Map)yuv422).put((object)ColorSpace.___003C_003EYUV444, (object)new Idential());
		((Map)yuv422).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv444pToRgb(0, 0));
		((Map)yuv422).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv444pToYuv420pHiBD(0, 0));
		map.put(ColorSpace.___003C_003EYUV444, yuv422);
		HashMap yuv444_10 = new HashMap();
		((Map)yuv444_10).put((object)ColorSpace.___003C_003EYUV444_10, (object)new Idential());
		((Map)yuv444_10).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv444pToRgb(2, 0));
		((Map)yuv444_10).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv444pToYuv420pHiBD(0, 2));
		map.put(ColorSpace.___003C_003EYUV444_10, yuv444_10);
		HashMap yuv420j = new HashMap();
		((Map)yuv420j).put((object)ColorSpace.___003C_003EYUV420J, (object)new Idential());
		((Map)yuv420j).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv420jToRgbHiBD());
		((Map)yuv420j).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv420jToYuv420HiBD());
		map.put(ColorSpace.___003C_003EYUV420J, yuv420j);
		HashMap yuv422j = new HashMap();
		((Map)yuv422j).put((object)ColorSpace.___003C_003EYUV422J, (object)new Idential());
		((Map)yuv422j).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv422jToRgbHiBD());
		((Map)yuv422j).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv422jToYuv420pHiBD());
		((Map)yuv422j).put((object)ColorSpace.___003C_003EYUV420J, (object)new Yuv422pToYuv420pHiBD(0, 0));
		map.put(ColorSpace.___003C_003EYUV422J, yuv422j);
		HashMap yuv444j = new HashMap();
		((Map)yuv444j).put((object)ColorSpace.___003C_003EYUV444J, (object)new Idential());
		((Map)yuv444j).put((object)ColorSpace.___003C_003ERGB, (object)new Yuv444jToRgbHiBD());
		((Map)yuv444j).put((object)ColorSpace.___003C_003EYUV420, (object)new Yuv444jToYuv420pHiBD());
		((Map)yuv444j).put((object)ColorSpace.___003C_003EYUV420J, (object)new Yuv444pToYuv420pHiBD(0, 0));
		map.put(ColorSpace.___003C_003EYUV444J, yuv444j);
	}
}
