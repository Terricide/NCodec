using System.ComponentModel;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using java.util.function;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.io.model;

public class Frame : Picture
{
	[SpecialName]
	[Signature("Ljava/lang/Object;Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	[EnclosingMethod(null, null, null)]
	internal class _1 : Object, Comparator
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 162, 103, 99, 100, 99, 100, 131 })]
		public virtual int compare(Frame o1, Frame o2)
		{
			if (o1 == null && o2 == null)
			{
				return 0;
			}
			if (o1 == null)
			{
				return 1;
			}
			if (o2 == null)
			{
				return -1;
			}
			return (access_0024000(o1) > access_0024000(o2)) ? 1 : ((access_0024000(o1) != access_0024000(o2)) ? (-1) : 0);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(93)]
		internal _1()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
		[LineNumberTable(93)]
		public virtual int compare(object obj1, object obj2)
		{
			int result = compare((Frame)obj1, (Frame)obj2);
			return result;
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator reversed()
		{
			return Comparator.__DefaultMethods.reversed(this);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparing(Comparator P_0)
		{
			return Comparator.__DefaultMethods.thenComparing(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparing(Function P_0, Comparator P_1)
		{
			return Comparator.__DefaultMethods.thenComparing(this, P_0, P_1);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparing(Function P_0)
		{
			return Comparator.__DefaultMethods.thenComparing(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparingInt(ToIntFunction P_0)
		{
			return Comparator.__DefaultMethods.thenComparingInt(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparingLong(ToLongFunction P_0)
		{
			return Comparator.__DefaultMethods.thenComparingLong(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparingDouble(ToDoubleFunction P_0)
		{
			return Comparator.__DefaultMethods.thenComparingDouble(this, P_0);
		}

		bool Comparator.equals(object P_0)
		{
			return Object.instancehelper_equals(this, P_0);
		}
	}

	[SpecialName]
	[Signature("Ljava/lang/Object;Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	[EnclosingMethod(null, null, null)]
	internal class _2 : Object, Comparator
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(106)]
		internal _2()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 115, 66, 103, 99, 100, 99, 100, 131 })]
		public virtual int compare(Frame o1, Frame o2)
		{
			if (o1 == null && o2 == null)
			{
				return 0;
			}
			if (o1 == null)
			{
				return 1;
			}
			if (o2 == null)
			{
				return -1;
			}
			return (access_0024000(o1) < access_0024000(o2)) ? 1 : ((access_0024000(o1) != access_0024000(o2)) ? (-1) : 0);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
		[LineNumberTable(106)]
		public virtual int compare(object obj1, object obj2)
		{
			int result = compare((Frame)obj1, (Frame)obj2);
			return result;
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator reversed()
		{
			return Comparator.__DefaultMethods.reversed(this);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparing(Comparator P_0)
		{
			return Comparator.__DefaultMethods.thenComparing(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparing(Function P_0, Comparator P_1)
		{
			return Comparator.__DefaultMethods.thenComparing(this, P_0, P_1);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparing(Function P_0)
		{
			return Comparator.__DefaultMethods.thenComparing(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparingInt(ToIntFunction P_0)
		{
			return Comparator.__DefaultMethods.thenComparingInt(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparingLong(ToLongFunction P_0)
		{
			return Comparator.__DefaultMethods.thenComparingLong(this, P_0);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public virtual Comparator thenComparingDouble(ToDoubleFunction P_0)
		{
			return Comparator.__DefaultMethods.thenComparingDouble(this, P_0);
		}

		bool Comparator.equals(object P_0)
		{
			return Object.instancehelper_equals(this, P_0);
		}
	}

	private int frameNo;

	private SliceType frameType;

	private H264Utils.MvList2D mvs;

	private Frame[][][] refsUsed;

	private bool shortTerm;

	private int poc;

	[Signature("Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	public static Comparator POCAsc;

	[Signature("Ljava/util/Comparator<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	public static Comparator POCDesc;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(124)]
	public virtual SliceType getFrameType()
	{
		return frameType;
	}

	[LineNumberTable(78)]
	public virtual H264Utils.MvList2D getMvs()
	{
		return mvs;
	}

	[LineNumberTable(120)]
	public virtual Frame[][][] getRefsUsed()
	{
		return refsUsed;
	}

	[LineNumberTable(90)]
	public virtual int getPOC()
	{
		return poc;
	}

	[LineNumberTable(82)]
	public virtual bool isShortTerm()
	{
		return shortTerm;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 119, 105, 105, 105, 105, 104 })]
	public Frame(int width, int height, byte[][] data, ColorSpace color, Rect crop, int frameNo, SliceType frameType, H264Utils.MvList2D mvs, Frame[][][] refsUsed, int poc)
		: base(width, height, data, null, color, 0, crop)
	{
		this.frameNo = frameNo;
		this.mvs = mvs;
		this.refsUsed = refsUsed;
		this.poc = poc;
		shortTerm = true;
	}

	[LineNumberTable(new byte[] { 159, 121, 129, 67, 104 })]
	public virtual void setShortTerm(bool shortTerm)
	{
		int shortTerm2 = ((this.shortTerm = shortTerm) ? 1 : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 104 })]
	public static Frame createFrame(Frame pic)
	{
		Picture comp = pic.createCompatible();
		Frame result = new Frame(comp.getWidth(), comp.getHeight(), comp.getData(), comp.getColor(), pic.getCrop(), pic.frameNo, pic.frameType, pic.mvs, pic.refsUsed, pic.poc);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 104, 109, 109, 109, 109, 109 })]
	public virtual void copyFromFrame(Frame src)
	{
		base.copyFrom(src);
		frameNo = src.frameNo;
		mvs = src.mvs;
		shortTerm = src.shortTerm;
		refsUsed = src.refsUsed;
		poc = src.poc;
	}

	[LineNumberTable(74)]
	public virtual int getFrameNo()
	{
		return frameNo;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(19)]
	internal static int access_0024000(Frame x0)
	{
		return x0.poc;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 104 })]
	public new virtual Frame cropped()
	{
		Picture cropped = base.cropped();
		Frame result = new Frame(cropped.getWidth(), cropped.getHeight(), cropped.getData(), cropped.getColor(), null, frameNo, frameType, mvs, refsUsed, poc);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 66, 105, 138, 104, 104 })]
	public new virtual Frame cloneCropped()
	{
		if (cropNeeded())
		{
			Frame result = cropped();
			return result;
		}
		Frame clone = createFrame(this);
		clone.copyFrom(this);
		return clone;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LineNumberTable(19)]
	public virtual Picture _003Cbridge_003Ecropped()
	{
		Frame result = cropped();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LineNumberTable(19)]
	public virtual Picture _003Cbridge_003EcloneCropped()
	{
		Frame result = cloneCropped();
		return result;
	}

	[LineNumberTable(new byte[] { 159, 119, 98, 235, 77 })]
	static Frame()
	{
		POCAsc = new _1();
		POCDesc = new _2();
	}
}
