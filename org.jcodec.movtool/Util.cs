using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class Util : Object
{
	[Signature("<T:Ljava/lang/Object;>Ljava/lang/Object;")]
	public class Pair : Object
	{
		[Signature("TT;")]
		private object a;

		[Signature("TT;")]
		private object b;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 131, 98, 105, 104, 104 })]
		public Pair(object a, object b)
		{
			this.a = a;
			this.b = b;
		}

		[Signature("()TT;")]
		[LineNumberTable(51)]
		public virtual object getA()
		{
			return a;
		}

		[Signature("()TT;")]
		[LineNumberTable(55)]
		public virtual object getB()
		{
			return b;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/common/model/Rational;Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;)Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	[LineNumberTable(new byte[]
	{
		159, 77, 130, 103, 104, 127, 0, 111, 127, 13,
		127, 4, 116, 110, 102
	})]
	public static List editsOnEdits(Rational mvByTrack, List lower, List higher)
	{
		ArrayList result = new ArrayList();
		object next = new ArrayList(lower);
		Iterator iterator = higher.iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			long startMv = mvByTrack.multiplyLong(edit.getMediaTime());
			object obj = next;
			Rational rational = mvByTrack.flip();
			Pair split = splitEdits(tvMv: startMv, trackByMv: rational, edits: (obj == null) ? null : ((obj as List) ?? throw new java.lang.IncompatibleClassChangeError()));
			Pair split2 = splitEdits((List)split.getB(), mvByTrack.flip(), startMv + edit.getDuration());
			((List)result).addAll((Collection)split2.getA());
			next = (List)split2.getB();
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 82, 162, 104, 118, 119, 103, 109, 127, 2,
		143
	})]
	public static void forceEditList(MovieBox movie, TrakBox trakBox)
	{
		object edits = trakBox.getEdits();
		if ((List)edits == null || ((List)edits).size() == 0)
		{
			MovieHeaderBox mvhd = (MovieHeaderBox)NodeBox.findFirst(movie, ClassLiteral<MovieHeaderBox>.Value, "mvhd");
			edits = new ArrayList();
			trakBox.setEdits((ArrayList)edits);
			((List)(ArrayList)edits).add((object)new Edit((int)mvhd.getDuration(), 0L, 1f));
			trakBox.setEdits((ArrayList)edits);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/containers/mp4/boxes/MovieBox;Lorg/jcodec/containers/mp4/boxes/TrakBox;J)Lorg/jcodec/movtool/Util$Pair<Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;>;")]
	[LineNumberTable(103)]
	public static Pair split(MovieBox movie, TrakBox track, long tvMv)
	{
		List edits = track.getEdits();
		Rational.___003Cclinit_003E();
		Pair result = splitEdits(edits, new Rational(track.getTimescale(), movie.getTimescale()), tvMv);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 79, 130, 104, 104, 101, 8, 199 })]
	public static void forceEditListMov(MovieBox movie)
	{
		TrakBox[] tracks = movie.getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox trakBox = tracks[i];
			forceEditList(movie, trakBox);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 98, 105, 106, 106 })]
	public static void insertTo(MovieBox movie, TrakBox dest, TrakBox src, long tvMv)
	{
		appendToInternal(movie, dest, src);
		insertEdits(movie, dest, src, tvMv);
		updateDuration(dest, src);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 66, 124 })]
	public static void shift(MovieBox movie, TrakBox track, long tvMv)
	{
		track.getEdits().add(0, new Edit(tvMv, -1L, 1f));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 162, 106, 127, 12 })]
	public static void spread(MovieBox movie, TrakBox track, long tvMv, long durationMv)
	{
		Pair split = Util.split(movie, track, tvMv);
		track.getEdits().add(((List)split.getA()).size(), new Edit(durationMv, -1L, 1f));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 113, 66, 104, 99, 104, 104, 44, 135, 107,
		100, 106, 113, 56, 47, 233, 69
	})]
	public static long[] getTimevalues(TrakBox track)
	{
		TimeToSampleBox stts = track.getStts();
		int count = 0;
		TimeToSampleBox.TimeToSampleEntry[] tts = stts.getEntries();
		for (int j = 0; j < (nint)tts.LongLength; j++)
		{
			count += tts[j].getSampleCount();
		}
		long[] tv = new long[count + 1];
		int l = 0;
		for (int i = 0; i < (nint)tts.LongLength; i++)
		{
			int k = 0;
			while (k < tts[i].getSampleCount())
			{
				tv[l + 1] = tv[l] + tts[i].getSampleDuration();
				k++;
				l++;
			}
		}
		return tv;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;Lorg/jcodec/common/model/Rational;J)Lorg/jcodec/movtool/Util$Pair<Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;>;")]
	[LineNumberTable(new byte[]
	{
		159, 127, 66, 100, 103, 103, 104, 108, 110, 112,
		103, 139, 119, 159, 4, 103, 108, 105, 138, 108,
		105, 204, 138, 107, 102, 105, 144
	})]
	public static Pair splitEdits(List edits, Rational trackByMv, long tvMv)
	{
		long total = 0L;
		ArrayList i = new ArrayList();
		ArrayList r = new ArrayList();
		ListIterator lit = edits.listIterator();
		while (lit.hasNext())
		{
			Edit edit = (Edit)lit.next();
			if (total + edit.getDuration() > tvMv)
			{
				int leftDurMV = (int)(tvMv - total);
				int leftDurMedia = trackByMv.multiplyS(leftDurMV);
				Edit left = new Edit(leftDurMV, edit.getMediaTime(), 1f);
				Edit right = new Edit(edit.getDuration() - leftDurMV, leftDurMedia + edit.getMediaTime(), 1f);
				lit.remove();
				if (left.getDuration() > 0u)
				{
					lit.add(left);
					((List)i).add((object)left);
				}
				if (right.getDuration() > 0u)
				{
					lit.add(right);
					((List)r).add((object)right);
				}
				break;
			}
			((List)i).add((object)edit);
			total += edit.getDuration();
		}
		while (lit.hasNext())
		{
			((List)r).add(lit.next());
		}
		Pair result = new Pair(i, r);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 92, 66, 136, 104, 136, 104, 104, 102, 115,
		233, 61, 231, 69, 127, 7
	})]
	private static int appendEntries(TrakBox trakBox1, TrakBox trakBox2)
	{
		appendDrefs(trakBox1, trakBox2);
		SampleEntry[] ent1 = trakBox1.getSampleEntries();
		SampleEntry[] ent2 = trakBox2.getSampleEntries();
		SampleDescriptionBox stsd = SampleDescriptionBox.createSampleDescriptionBox(ent1);
		for (int i = 0; i < (nint)ent2.LongLength; i++)
		{
			SampleEntry se = ent2[i];
			se.setDrefInd((short)(se.getDrefInd() + (nint)ent1.LongLength));
			stsd.add(se);
		}
		((NodeBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"))).replace("stsd", stsd);
		return ent1.Length;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 85, 130, 104, 104, 104, 136, 116, 116, 125,
		126, 127, 15
	})]
	private static void appendChunkOffsets(TrakBox trakBox1, TrakBox trakBox2)
	{
		ChunkOffsetsBox stco1 = trakBox1.getStco();
		ChunkOffsets64Box co641 = trakBox1.getCo64();
		ChunkOffsetsBox stco2 = trakBox2.getStco();
		ChunkOffsets64Box co642 = trakBox2.getCo64();
		long[] off1 = ((stco1 != null) ? stco1.getChunkOffsets() : co641.getChunkOffsets());
		long[] off2 = ((stco2 != null) ? stco2.getChunkOffsets() : co642.getChunkOffsets());
		NodeBox stbl1 = (NodeBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"));
		stbl1.removeChildren(new string[2] { "stco", "co64" });
		stbl1.add((co641 != null || co642 != null) ? ((FullBox)ChunkOffsets64Box.createChunkOffsets64Box(ArrayUtil.addAllLong(off1, off2))) : ((FullBox)ChunkOffsetsBox.createChunkOffsetsBox(ArrayUtil.addAllLong(off1, off2))));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 130, 104, 104, 104, 38, 150, 127, 9 })]
	private static void appendTimeToSamples(TrakBox trakBox1, TrakBox trakBox2)
	{
		TimeToSampleBox stts1 = trakBox1.getStts();
		TimeToSampleBox stts2 = trakBox2.getStts();
		TimeToSampleBox sttsNew = TimeToSampleBox.createTimeToSampleBox((TimeToSampleBox.TimeToSampleEntry[])ArrayUtil.addAllObj(stts1.getEntries(), stts2.getEntries()));
		((NodeBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"))).replace("stts", sttsNew);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 130, 104, 136, 104, 105, 106, 122, 23,
		201, 127, 2, 59, 136
	})]
	private static void appendSampleToChunk(TrakBox trakBox1, TrakBox trakBox2, int off)
	{
		SampleToChunkBox stsc1 = trakBox1.getStsc();
		SampleToChunkBox stsc2 = trakBox2.getStsc();
		SampleToChunkBox.SampleToChunkEntry[] orig = stsc2.getSampleToChunk();
		SampleToChunkBox.SampleToChunkEntry[] shifted = new SampleToChunkBox.SampleToChunkEntry[(nint)orig.LongLength];
		for (int i = 0; i < (nint)orig.LongLength; i++)
		{
			shifted[i] = new SampleToChunkBox.SampleToChunkEntry(orig[i].getFirst() + stsc1.getSampleToChunk().LongLength, orig[i].getCount(), orig[i].getEntry() + off);
		}
		((NodeBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"))).replace("stsc", SampleToChunkBox.createSampleToChunkBox((SampleToChunkBox.SampleToChunkEntry[])ArrayUtil.addAllObj(stsc1.getSampleToChunk(), shifted)));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 99, 66, 104, 104, 111, 145, 106, 156, 152,
		127, 9
	})]
	private static void appendSampleSizes(TrakBox trakBox1, TrakBox trakBox2)
	{
		SampleSizesBox stsz1 = trakBox1.getStsz();
		SampleSizesBox stsz2 = trakBox2.getStsz();
		if (stsz1.getDefaultSize() != stsz2.getDefaultSize())
		{
			throw new IllegalArgumentException("Can't append to track that has different default sample size");
		}
		SampleSizesBox stszr = ((stsz1.getDefaultSize() <= 0) ? SampleSizesBox.createSampleSizesBox2(ArrayUtil.addAllInt(stsz1.getSizes(), stsz2.getSizes())) : SampleSizesBox.createSampleSizesBox(stsz1.getDefaultSize(), stsz1.getCount() + stsz2.getCount()));
		((NodeBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<NodeBox>.Value, Box.path("mdia.minf.stbl"))).replace("stsz", stszr);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 66, 137, 104, 104, 105, 106 })]
	private static void appendToInternal(MovieBox movie, TrakBox dest, TrakBox src)
	{
		int off = appendEntries(dest, src);
		appendChunkOffsets(dest, src);
		appendTimeToSamples(dest, src);
		appendSampleToChunk(dest, src, off);
		appendSampleSizes(dest, src);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 66, 127, 2, 109, 99, 116, 111 })]
	private static void appendEdits(TrakBox dest, TrakBox src, int ind)
	{
		Iterator iterator = src.getEdits().iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			edit.shift(dest.getMediaDuration());
		}
		dest.getEdits().addAll(ind, src.getEdits());
		dest.setEdits(dest.getEdits());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 98, 124, 124, 118 })]
	private static void updateDuration(TrakBox dest, TrakBox src)
	{
		MediaHeaderBox mdhd1 = (MediaHeaderBox)NodeBox.findFirstPath(dest, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"));
		MediaHeaderBox mdhd2 = (MediaHeaderBox)NodeBox.findFirstPath(src, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"));
		mdhd1.setDuration(mdhd1.getDuration() + mdhd2.getDuration());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 162, 106, 122 })]
	private static void insertEdits(MovieBox movie, TrakBox dest, TrakBox src, long tvMv)
	{
		Pair split = Util.split(movie, dest, tvMv);
		appendEdits(dest, src, ((List)split.getA()).size());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 88, 66, 124, 124, 115 })]
	private static void appendDrefs(TrakBox trakBox1, TrakBox trakBox2)
	{
		DataRefBox dref1 = (DataRefBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<DataRefBox>.Value, Box.path("mdia.minf.dinf.dref"));
		DataRefBox dref2 = (DataRefBox)NodeBox.findFirstPath(trakBox2, ClassLiteral<DataRefBox>.Value, Box.path("mdia.minf.dinf.dref"));
		dref1.getBoxes().addAll(dref2.getBoxes());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(39)]
	public Util()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 162, 105, 115, 106 })]
	public static void appendTo(MovieBox movie, TrakBox dest, TrakBox src)
	{
		appendToInternal(movie, dest, src);
		appendEdits(dest, src, dest.getEdits().size());
		updateDuration(dest, src);
	}
}
