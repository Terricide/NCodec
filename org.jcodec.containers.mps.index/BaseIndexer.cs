using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using java.util.function;
using org.jcodec.common;
using org.jcodec.common.logging;
using org.jcodec.common.tools;

namespace org.jcodec.containers.mps.index;

public abstract class BaseIndexer : MPSUtils.PESReader
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : Object
	{
		_1()
		{
			throw null;
		}
	}

	[InnerClass(null, Modifiers.Protected | Modifiers.Static | Modifiers.Abstract)]
	public abstract class BaseAnalyser : Object
	{
		protected internal IntArrayList pts;

		protected internal IntArrayList dur;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(66)]
		public virtual int estimateSize()
		{
			return (pts.size() << 2) + 4;
		}

		public abstract MPSIndex.MPSStreamIndex serialize(int i);

		public abstract void finishAnalyse();

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 128, 66, 105, 113, 113 })]
		public BaseAnalyser()
		{
			pts = new IntArrayList(250000);
			dur = new IntArrayList(250000);
		}

		public abstract void pkt(ByteBuffer bb, PESPacket pesp);
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class GenericAnalyser : BaseAnalyser
	{
		private IntArrayList sizes;

		private int knownDuration;

		private long lastPts;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 122, 66, 105, 113 })]
		public GenericAnalyser()
		{
			sizes = new IntArrayList(250000);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 98, 146, 107, 151, 117, 141, 115, 116 })]
		public override void pkt(ByteBuffer pkt, PESPacket pesHeader)
		{
			sizes.add(pkt.remaining());
			if (pesHeader.pts == -1)
			{
				pesHeader.pts = lastPts + knownDuration;
			}
			else
			{
				knownDuration = (int)(pesHeader.pts - lastPts);
				lastPts = pesHeader.pts;
			}
			pts.add((int)pesHeader.pts);
			dur.add(knownDuration);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(98)]
		public override MPSIndex.MPSStreamIndex serialize(int streamId)
		{
			MPSIndex.MPSStreamIndex result = new MPSIndex.MPSStreamIndex(streamId, sizes.toArray(), pts.toArray(), dur.toArray(), new int[0]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(103)]
		public override int estimateSize()
		{
			return base.estimateSize() + (sizes.size() << 2) + 32;
		}

		[LineNumberTable(108)]
		public override void finishAnalyse()
		{
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class MPEGVideoAnalyser : BaseAnalyser
	{
		[SpecialName]
		[Signature("Ljava/lang/Object;Ljava/util/Comparator<Lorg/jcodec/containers/mps/index/BaseIndexer$MPEGVideoAnalyser$Frame;>;")]
		[EnclosingMethod(null, "fixPts", "(Ljava.util.List;)V")]
		internal class _1 : Object, Comparator
		{
			[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
			internal MPEGVideoAnalyser this_00240;

			[LineNumberTable(203)]
			public virtual int compare(Frame o1, Frame o2)
			{
				return (o1.tempRef > o2.tempRef) ? 1 : ((o1.tempRef != o2.tempRef) ? (-1) : 0);
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(201)]
			internal _1(MPEGVideoAnalyser this_00240)
			{
				this.this_00240 = this_00240;
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
			[LineNumberTable(201)]
			public virtual int compare(object obj1, object obj2)
			{
				int result = compare((Frame)obj1, (Frame)obj2);
				
				return result;
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator reversed()
			{
				return Comparator._003Cdefault_003Ereversed(this);
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator thenComparing(Comparator P_0)
			{
				return Comparator._003Cdefault_003EthenComparing(this, P_0);
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator thenComparing(Function P_0, Comparator P_1)
			{
				return Comparator._003Cdefault_003EthenComparing(this, P_0, P_1);
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator thenComparing(Function P_0)
			{
				return Comparator._003Cdefault_003EthenComparing(this, P_0);
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator thenComparingInt(ToIntFunction P_0)
			{
				return Comparator._003Cdefault_003EthenComparingInt(this, P_0);
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator thenComparingLong(ToLongFunction P_0)
			{
				return Comparator._003Cdefault_003EthenComparingLong(this, P_0);
			}

			[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
			public virtual Comparator thenComparingDouble(ToDoubleFunction P_0)
			{
				return Comparator._003Cdefault_003EthenComparingDouble(this, P_0);
			}

			bool Comparator.Comparator_003A_003Aequals(object P_0)
			{
				return Object.instancehelper_equals(this, P_0);
			}
		}

		[InnerClass(null, Modifiers.Private | Modifiers.Static)]
		internal class Frame : Object
		{
			internal long offset;

			internal int size;

			internal int pts;

			internal int tempRef;

			[MethodImpl(MethodImplOptions.NoInlining)]
			[Modifiers(Modifiers.Synthetic)]
			[LineNumberTable(131)]
			internal Frame(BaseIndexer._1 x0)
				: this()
			{
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(131)]
			private Frame()
			{
			}
		}

		private int marker;

		private long position;

		private IntArrayList sizes;

		private IntArrayList keyFrames;

		private int frameNo;

		private bool inFrameData;

		private Frame lastFrame;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/mps/index/BaseIndexer$MPEGVideoAnalyser$Frame;>;")]
		private List curGop;

		private long phPos;

		private Frame lastFrameOfLastGop;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 111, 98, 233, 51, 232, 73, 233, 69, 113,
			113, 108
		})]
		public MPEGVideoAnalyser()
		{
			marker = -1;
			phPos = -1L;
			sizes = new IntArrayList(250000);
			keyFrames = new IntArrayList(20000);
			curGop = new ArrayList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 95, 162, 109, 127, 2, 114, 114, 99, 110 })]
		private void outGop()
		{
			fixPts(curGop);
			Iterator iterator = curGop.iterator();
			while (iterator.hasNext())
			{
				Frame frame = (Frame)iterator.next();
				sizes.add(frame.size);
				pts.add(frame.pts);
			}
			curGop.clear();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/List<Lorg/jcodec/containers/mps/index/BaseIndexer$MPEGVideoAnalyser$Frame;>;)V")]
		[LineNumberTable(new byte[]
		{
			159, 92, 66, 115, 237, 69, 106, 118, 117, 127,
			3, 108, 100, 101, 106, 235, 57, 234, 74, 231,
			53, 234, 77, 105, 159, 1, 106, 63, 2, 169,
			109
		})]
		private void fixPts(List curGop)
		{
			Frame[] frames = (Frame[])curGop.toArray(new Frame[0]);
			Arrays.sort(frames, new _1(this));
			for (int dir = 0; dir < 3; dir++)
			{
				int j = 0;
				int lastPts = -1;
				int secondLastPts = -1;
				int lastTref = -1;
				int secondLastTref = -1;
				for (; j < (nint)frames.LongLength; j++)
				{
					if (frames[j].pts == -1 && lastPts != -1 && secondLastPts != -1)
					{
						Frame obj = frames[j];
						int num = lastPts;
						int num2 = lastPts - secondLastPts;
						int num3 = MathUtil.abs(lastTref - secondLastTref);
						obj.pts = num + ((num3 != -1) ? (num2 / num3) : (-num2));
					}
					if (frames[j].pts != -1)
					{
						secondLastPts = lastPts;
						secondLastTref = lastTref;
						lastPts = frames[j].pts;
						lastTref = frames[j].tempRef;
					}
				}
				ArrayUtil.reverse(frames);
			}
			if (lastFrameOfLastGop != null)
			{
				dur.add(frames[0].pts - lastFrameOfLastGop.pts);
			}
			for (int i = 1; i < (nint)frames.LongLength; i++)
			{
				dur.add(frames[i].pts - frames[i - 1].pts);
			}
			lastFrameOfLastGop = frames[(nint)frames.LongLength - 1];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 107, 66, 108, 111, 112, 145, 110, 111, 102,
			113, 102, 103, 118, 101, 116, 111, 231, 69, 116,
			134, 159, 4, 127, 3, 115, 104, 106, 127, 4,
			168, 127, 23, 104, 110, 112, 127, 28, 39, 139,
			111, 136, 127, 5, 179, 127, 2, 102
		})]
		public override void pkt(ByteBuffer pkt, PESPacket pesHeader)
		{
			while (pkt.hasRemaining())
			{
				int b = (sbyte)pkt.get() & 0xFF;
				position++;
				marker = (marker << 8) | b;
				if (phPos != -1)
				{
					switch (position - phPos)
					{
					case 5L:
						lastFrame.tempRef = b << 2;
						break;
					case 6L:
					{
						int picCodingType = (b >> 3) & 7;
						lastFrame.tempRef |= b >> 6;
						if (picCodingType == 1)
						{
							keyFrames.add(frameNo - 1);
							if (curGop.size() > 0)
							{
								outGop();
							}
						}
						break;
					}
					}
				}
				if ((marker & -256) == 256)
				{
					if (inFrameData && (marker == 256 || marker > 431))
					{
						lastFrame.size = (int)(position - 4u - lastFrame.offset);
						curGop.add(lastFrame);
						lastFrame = null;
						inFrameData = false;
					}
					else if (!inFrameData && marker > 256 && marker <= 431)
					{
						inFrameData = true;
					}
					if (lastFrame == null && (marker == 435 || marker == 440 || marker == 256))
					{
						Frame frame = new Frame(null);
						frame.pts = (int)pesHeader.pts;
						frame.offset = position - 4u;
						Logger.info(String.format("FRAME[%d]: %012x, %d", Integer.valueOf(frameNo), Long.valueOf(pesHeader.pos + pkt.position() - 4u), Long.valueOf(pesHeader.pts)));
						frameNo++;
						lastFrame = frame;
					}
					if (lastFrame != null && lastFrame.pts == -1 && marker == 256)
					{
						lastFrame.pts = (int)pesHeader.pts;
					}
					phPos = ((marker != 256) ? (-1) : (position - 4u));
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 85, 130, 105, 98, 127, 0, 115, 105 })]
		public override void finishAnalyse()
		{
			if (lastFrame != null)
			{
				lastFrame.size = (int)(position - lastFrame.offset);
				curGop.add(lastFrame);
				outGop();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(238)]
		public override MPSIndex.MPSStreamIndex serialize(int streamId)
		{
			MPSIndex.MPSStreamIndex result = new MPSIndex.MPSStreamIndex(streamId, sizes.toArray(), pts.toArray(), dur.toArray(), keyFrames.toArray());
			
			return result;
		}
	}

	[Signature("Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mps/index/BaseIndexer$BaseAnalyser;>;")]
	private Map analyzers;

	private LongArrayList tokens;

	private RunLength.Integer streams;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 105, 108, 108, 108 })]
	public BaseIndexer()
	{
		analyzers = new HashMap();
		tokens = LongArrayList.createLongArrayList();
		streams = new RunLength.Integer();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 127, 2, 127, 7, 122, 99 })]
	public virtual int estimateSize()
	{
		int sizeEstimate = (tokens.size() << 3) + streams.estimateSize() + 128;
		Iterator iterator = analyzers.keySet().iterator();
		while (iterator.hasNext())
		{
			Integer stream = (Integer)iterator.next();
			sizeEstimate += ((BaseAnalyser)analyzers.get(stream)).estimateSize();
		}
		return sizeEstimate;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 162, 120, 100, 126, 148 })]
	protected internal virtual BaseAnalyser getAnalyser(int stream)
	{
		BaseAnalyser analizer = (BaseAnalyser)analyzers.get(Integer.valueOf(stream));
		if (analizer == null)
		{
			analizer = ((stream < 224 || stream > 239) ? ((BaseAnalyser)new GenericAnalyser()) : ((BaseAnalyser)new MPEGVideoAnalyser()));
			analyzers.put(Integer.valueOf(stream), analizer);
		}
		return (BaseAnalyser)analyzers.get(Integer.valueOf(stream));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 66, 103, 109, 124, 127, 9, 99 })]
	public virtual MPSIndex serialize()
	{
		ArrayList streamsIndices = new ArrayList();
		Set entrySet = analyzers.entrySet();
		Iterator iterator = entrySet.iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			((List)streamsIndices).add((object)((BaseAnalyser)entry.getValue()).serialize(((Integer)entry.getKey()).intValue()));
		}
		MPSIndex result = new MPSIndex(tokens.toArray(), streams, (MPSIndex.MPSStreamIndex[])((List)streamsIndices).toArray((object[])new MPSIndex.MPSStreamIndex[0]));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 77, 98, 109, 111 })]
	protected internal virtual void savePESMeta(int stream, long token)
	{
		tokens.add(token);
		streams.add(stream);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 130, 103, 127, 7, 103, 99 })]
	internal virtual void finishAnalyse()
	{
		base.finishRead();
		Iterator iterator = analyzers.values().iterator();
		while (iterator.hasNext())
		{
			BaseAnalyser baseAnalyser = (BaseAnalyser)iterator.next();
			baseAnalyser.finishAnalyse();
		}
	}
}
