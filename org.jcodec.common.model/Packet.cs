using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;
using java.util.function;

namespace org.jcodec.common.model;

public class Packet : java.lang.Object
{
	[SpecialName]
	[Signature("Ljava/lang/Object;Ljava/util/Comparator<Lorg/jcodec/common/model/Packet;>;")]
	[EnclosingMethod(null, null, null)]
	internal class _1 : java.lang.Object, Comparator
	{
		[LineNumberTable(new byte[] { 159, 112, 130, 103, 99, 100, 99, 100, 99 })]
		public virtual int compare(Packet o1, Packet o2)
		{
			if (o1 == null && o2 == null)
			{
				return 0;
			}
			if (o1 == null)
			{
				return -1;
			}
			if (o2 == null)
			{
				return 1;
			}
			return (o1.frameNo < o2.frameNo) ? (-1) : ((o1.frameNo != o2.frameNo) ? 1 : 0);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(120)]
		internal _1()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
		[LineNumberTable(120)]
		public virtual int compare(object obj1, object obj2)
		{
			int result = compare((Packet)obj1, (Packet)obj2);
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
			return java.lang.Object.instancehelper_equals(this, P_0);
		}
	}

	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/common/model/Packet$FrameType;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class FrameType : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			KEY,
			INTER,
			UNKNOWN
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FrameType ___003C_003EKEY;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FrameType ___003C_003EINTER;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static FrameType ___003C_003EUNKNOWN;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static FrameType[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FrameType KEY
		{
			[HideFromJava]
			get
			{
				return ___003C_003EKEY;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FrameType INTER
		{
			[HideFromJava]
			get
			{
				return ___003C_003EINTER;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static FrameType UNKNOWN
		{
			[HideFromJava]
			get
			{
				return ___003C_003EUNKNOWN;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(17)]
		private FrameType(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(17)]
		public static FrameType[] values()
		{
			return (FrameType[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(17)]
		public static FrameType valueOf(string name)
		{
			return (FrameType)java.lang.Enum.valueOf(ClassLiteral<FrameType>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 138, 130, 63, 18 })]
		static FrameType()
		{
			___003C_003EKEY = new FrameType("KEY", 0);
			___003C_003EINTER = new FrameType("INTER", 1);
			___003C_003EUNKNOWN = new FrameType("UNKNOWN", 2);
			_0024VALUES = new FrameType[3] { ___003C_003EKEY, ___003C_003EINTER, ___003C_003EUNKNOWN };
		}
	}

	public ByteBuffer data;

	public long pts;

	public int timescale;

	public long duration;

	public long frameNo;

	public FrameType frameType;

	public TapeTimecode tapeTimecode;

	public int displayOrder;

	[Signature("Ljava/util/Comparator<Lorg/jcodec/common/model/Packet;>;")]
	internal static Comparator ___003C_003EFRAME_ASC;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Comparator FRAME_ASC
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFRAME_ASC;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(69)]
	public virtual long getFrameNo()
	{
		return frameNo;
	}

	[LineNumberTable(105)]
	public virtual double getPtsD()
	{
		return (double)pts / (double)timescale;
	}

	[LineNumberTable(109)]
	public virtual double getDurationD()
	{
		return (double)duration / (double)timescale;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public static Packet createPacket(ByteBuffer data, long pts, int timescale, long duration, long frameNo, FrameType frameType, TapeTimecode tapeTimecode)
	{
		Packet result = new Packet(data, pts, timescale, duration, frameNo, frameType, tapeTimecode, 0);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(53)]
	public virtual ByteBuffer getData()
	{
		ByteBuffer result = data.duplicate();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(36)]
	public static Packet createPacketWithData(Packet other, ByteBuffer data)
	{
		Packet result = new Packet(data, other.pts, other.timescale, other.duration, other.frameNo, other.frameType, other.tapeTimecode, other.displayOrder);
		return result;
	}

	[LineNumberTable(new byte[] { 159, 118, 98, 104 })]
	public virtual void setFrameType(FrameType frameType)
	{
		this.frameType = frameType;
	}

	[LineNumberTable(57)]
	public virtual long getPts()
	{
		return pts;
	}

	[LineNumberTable(new byte[] { 159, 109, 98, 104 })]
	public virtual void setDuration(long duration)
	{
		this.duration = duration;
	}

	[LineNumberTable(93)]
	public virtual FrameType getFrameType()
	{
		return frameType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 98, 105, 104, 104, 104, 105, 105, 105,
		105, 105
	})]
	public Packet(ByteBuffer data, long pts, int timescale, long duration, long frameNo, FrameType frameType, TapeTimecode tapeTimecode, int displayOrder)
	{
		this.data = data;
		this.pts = pts;
		this.timescale = timescale;
		this.duration = duration;
		this.frameNo = frameNo;
		this.frameType = frameType;
		this.tapeTimecode = tapeTimecode;
		this.displayOrder = displayOrder;
	}

	[LineNumberTable(61)]
	public virtual int getTimescale()
	{
		return timescale;
	}

	[LineNumberTable(65)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(new byte[] { 159, 124, 98, 104 })]
	public virtual void setTimescale(int timescale)
	{
		this.timescale = timescale;
	}

	[LineNumberTable(77)]
	public virtual TapeTimecode getTapeTimecode()
	{
		return tapeTimecode;
	}

	[LineNumberTable(new byte[] { 159, 122, 98, 104 })]
	public virtual void setTapeTimecode(TapeTimecode tapeTimecode)
	{
		this.tapeTimecode = tapeTimecode;
	}

	[LineNumberTable(85)]
	public virtual int getDisplayOrder()
	{
		return displayOrder;
	}

	[LineNumberTable(new byte[] { 159, 120, 98, 104 })]
	public virtual void setDisplayOrder(int displayOrder)
	{
		this.displayOrder = displayOrder;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(101)]
	public virtual RationalLarge getPtsR()
	{
		RationalLarge result = RationalLarge.R(pts, timescale);
		return result;
	}

	[LineNumberTable(new byte[] { 159, 114, 98, 104 })]
	public virtual void setData(ByteBuffer data)
	{
		this.data = data;
	}

	[LineNumberTable(new byte[] { 159, 113, 98, 104 })]
	public virtual void setPts(long pts)
	{
		this.pts = pts;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(137)]
	public virtual bool isKeyFrame()
	{
		return frameType == FrameType.___003C_003EKEY;
	}

	[LineNumberTable(120)]
	static Packet()
	{
		___003C_003EFRAME_ASC = new _1();
	}
}
