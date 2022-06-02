using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using java.util.function;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.movtool;

namespace org.jcodec.containers.mp4.muxer;

public class TimecodeMP4MuxerTrack : CodecMP4MuxerTrack
{
	[SpecialName]
	[Signature("Ljava/lang/Object;Ljava/util/Comparator<Lorg/jcodec/common/model/Packet;>;")]
	[EnclosingMethod(null, "sortByDisplay", "(Ljava.util.List;)Ljava.util.List;")]
	internal class _1 : Object, Comparator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal TimecodeMP4MuxerTrack this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 124, 130, 103, 99, 100, 99, 100, 131, 121,
			44
		})]
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
			return (o1.getDisplayOrder() > o2.getDisplayOrder()) ? 1 : ((o1.getDisplayOrder() != o2.getDisplayOrder()) ? (-1) : 0);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(71)]
		internal _1(TimecodeMP4MuxerTrack this_00240)
		{
			this.this_00240 = this_00240;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
		[LineNumberTable(71)]
		public virtual int compare(object obj1, object obj2)
		{
			int result = compare((Packet)obj1, (Packet)obj2);
			
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

	private TapeTimecode prevTimecode;

	private TapeTimecode firstTimecode;

	private int fpsEstimate;

	private long sampleDuration;

	private long samplePts;

	private int tcFrames;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	private List lower;

	[Signature("Ljava/util/List<Lorg/jcodec/common/model/Packet;>;")]
	private List gop;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 130, 162, 106, 109, 120, 113, 105, 103, 121 })]
	public virtual void addTimecode(Packet packet)
	{
		if (_timescale == -1)
		{
			_timescale = packet.getTimescale();
		}
		if (_timescale != -1 && _timescale != packet.getTimescale())
		{
			
			throw new RuntimeException("MP4 timecode track doesn't support timescale switching.");
		}
		if (packet.isKeyFrame())
		{
			processGop();
		}
		gop.add(Packet.createPacketWithData(packet, null));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 116, 108, 108 })]
	public TimecodeMP4MuxerTrack(int trackId)
		: base(trackId, MP4TrackType.___003C_003ETIMECODE, Codec.___003C_003ETIMECODE)
	{
		lower = new ArrayList();
		gop = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 127, 98, 111, 127, 8, 104, 99, 142 })]
	private void processGop()
	{
		if (gop.size() > 0)
		{
			Iterator iterator = sortByDisplay(gop).iterator();
			while (iterator.hasNext())
			{
				Packet pkt = (Packet)iterator.next();
				addTimecodeInt(pkt);
			}
			gop.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/common/model/Packet;>;)Ljava/util/List<Lorg/jcodec/common/model/Packet;>;")]
	[LineNumberTable(new byte[] { 159, 125, 130, 104, 237, 78 })]
	private List sortByDisplay(List gop)
	{
		ArrayList result = new ArrayList(gop);
		Collections.sort(result, new _1(this));
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 116, 98, 104, 111, 136, 100, 103, 104, 116,
		116, 105, 136, 116, 111
	})]
	private void addTimecodeInt(Packet packet)
	{
		TapeTimecode tapeTimecode = packet.getTapeTimecode();
		int gap = (isGap(prevTimecode, tapeTimecode) ? 1 : 0);
		prevTimecode = tapeTimecode;
		if (gap != 0)
		{
			outTimecodeSample();
			firstTimecode = tapeTimecode;
			fpsEstimate = ((!tapeTimecode.isDropFrame()) ? (-1) : 30);
			samplePts += sampleDuration;
			sampleDuration = 0L;
			tcFrames = 0;
		}
		sampleDuration += packet.getDuration();
		tcFrames++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 100, 66, 110, 108, 106, 117, 127, 29, 110,
		104, 122, 104, 159, 28, 127, 4, 99, 191, 0
	})]
	private void outTimecodeSample()
	{
		if (sampleDuration <= 0u)
		{
			return;
		}
		if (firstTimecode != null)
		{
			if (fpsEstimate == -1)
			{
				fpsEstimate = (sbyte)prevTimecode.getFrame() + 1;
			}
			int flags = (firstTimecode.isDropFrame() ? 1 : 0);
			int timescale = _timescale;
			long num = sampleDuration;
			long num2 = tcFrames;
			TimecodeSampleEntry tmcd = TimecodeSampleEntry.createTimecodeSampleEntry(flags, timescale, (int)((num2 != -1) ? (num / num2) : (-num)), fpsEstimate);
			sampleEntries.add(tmcd);
			ByteBuffer sample = ByteBuffer.allocate(4);
			sample.putInt(toCounter(firstTimecode, fpsEstimate));
			sample.flip();
			addFrame(MP4Packet.createMP4Packet(sample, samplePts, _timescale, sampleDuration, 0L, Packet.FrameType.___003C_003EKEY, null, 0, samplePts, sampleEntries.size() - 1));
			lower.add(new Edit(sampleDuration, samplePts, 1f));
		}
		else
		{
			lower.add(new Edit(sampleDuration, -1L, 1f));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 130, 131, 103, 101, 100, 100, 133, 111,
		133, 202
	})]
	private bool isGap(TapeTimecode prevTimecode, TapeTimecode tapeTimecode)
	{
		int gap = 0;
		if (prevTimecode == null && tapeTimecode != null)
		{
			gap = 1;
		}
		else if (prevTimecode != null)
		{
			gap = ((tapeTimecode == null || prevTimecode.isDropFrame() != tapeTimecode.isDropFrame() || isTimeGap(prevTimecode, tapeTimecode)) ? 1 : 0);
		}
		return (byte)gap != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 98, 99, 104, 106, 100, 113, 106, 122,
		105, 109, 106, 106, 149, 136, 159, 17, 125, 99,
		131, 131
	})]
	private bool isTimeGap(TapeTimecode prevTimecode, TapeTimecode tapeTimecode)
	{
		int gap = 0;
		int sec = toSec(tapeTimecode);
		int num;
		int firstFrame;
		switch (sec - toSec(prevTimecode))
		{
		case 0:
		{
			int frameDiff = (sbyte)tapeTimecode.getFrame() - (sbyte)prevTimecode.getFrame();
			if (fpsEstimate != -1)
			{
				int num2 = frameDiff + fpsEstimate;
				int num3 = fpsEstimate;
				frameDiff = ((num3 != -1) ? (num2 % num3) : 0);
			}
			gap = ((frameDiff != 1) ? 1 : 0);
			break;
		}
		case 1:
			if (fpsEstimate == -1)
			{
				if ((sbyte)tapeTimecode.getFrame() == 0)
				{
					fpsEstimate = (sbyte)prevTimecode.getFrame() + 1;
				}
				else
				{
					gap = 1;
				}
				break;
			}
			if (tapeTimecode.isDropFrame())
			{
				if (60 == -1 || sec % 60 == 0)
				{
					if (600 != -1 && sec % 600 != 0)
					{
						num = 2;
						goto IL_00c4;
					}
				}
			}
			num = 0;
			goto IL_00c4;
		default:
			{
				gap = 1;
				break;
			}
			IL_00c4:
			firstFrame = num;
			if ((sbyte)tapeTimecode.getFrame() != firstFrame || (sbyte)prevTimecode.getFrame() != fpsEstimate - 1)
			{
				gap = 1;
			}
			break;
		}
		return (byte)gap != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(198)]
	private static int toSec(TapeTimecode tc)
	{
		return tc.getHour() * 3600 + (sbyte)tc.getMinute() * 60 + (sbyte)tc.getSecond();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 162, 114, 105, 106, 115, 186 })]
	private int toCounter(TapeTimecode tc, int fps)
	{
		int frames = toSec(tc) * fps + (sbyte)tc.getFrame();
		if (tc.isDropFrame())
		{
			long D = frames / 18000;
			int num = frames;
			long M = ((18000 != -1) ? (num % 18000) : 0);
			frames = (int)(frames - (18u * D + 2u * ((M - 2u) / 1800L)));
		}
		return frames;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 130, 103, 135, 110, 131, 105, 159, 2,
		141
	})]
	protected internal override Box finish(MovieHeaderBox mvhd)
	{
		processGop();
		outTimecodeSample();
		if (sampleEntries.size() == 0)
		{
			return null;
		}
		if (edits != null)
		{
			edits = Util.editsOnEdits(new Rational(1, 1), lower, edits);
		}
		else
		{
			edits = lower;
		}
		Box result = base.finish(mvhd);
		
		return result;
	}

	[HideFromJava]
	static TimecodeMP4MuxerTrack()
	{
		CodecMP4MuxerTrack.___003Cclinit_003E();
	}
}
