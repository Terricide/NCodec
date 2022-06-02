using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4.boxes;

public class TrakBox : NodeBox
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(189)]
	public virtual SampleEntry[] getSampleEntries()
	{
		return (SampleEntry[])NodeBox.findAllPath(this, ClassLiteral<SampleEntry>.Value, new string[5] { "mdia", "minf", "stbl", "stsd", null });
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(85)]
	public virtual bool isVideo()
	{
		bool result = String.instancehelper_equals("vide", getHandlerType());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(89)]
	public virtual bool isTimecode()
	{
		bool result = String.instancehelper_equals("tmcd", getHandlerType());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(134)]
	public virtual long getDuration()
	{
		long duration = getTrackHeader().getDuration();
		
		return duration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 130, 111 })]
	public virtual void setDuration(long duration)
	{
		getTrackHeader().setDuration(duration);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	[LineNumberTable(new byte[] { 159, 126, 130, 124, 100, 99 })]
	public virtual List getEdits()
	{
		EditListBox elst = (EditListBox)NodeBox.findFirstPath(this, ClassLiteral<EditListBox>.Value, Box.path("edts.elst"));
		if (elst == null)
		{
			return null;
		}
		List edits = elst.getEdits();
		
		return edits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(101)]
	public virtual bool isAudio()
	{
		bool result = String.instancehelper_equals("soun", getHandlerType());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;)V")]
	[LineNumberTable(new byte[] { 159, 124, 98, 119, 100, 113, 136, 149, 109, 116 })]
	public virtual void setEdits(List edits)
	{
		NodeBox edts = (NodeBox)NodeBox.findFirst(this, ClassLiteral<NodeBox>.Value, "edts");
		if (edts == null)
		{
			edts = new NodeBox(new Header("edts"));
			add(edts);
		}
		edts.removeChildren(new string[1] { "elst" });
		edts.add(EditListBox.createEditListBox(edits));
		getTrackHeader().setDuration(getEditedDuration(this));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(62)]
	public virtual TrackHeaderBox getTrackHeader()
	{
		return (TrackHeaderBox)NodeBox.findFirst(this, ClassLiteral<TrackHeaderBox>.Value, "tkhd");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 130, 109, 104, 100, 131, 104, 100, 131,
		127, 3, 113, 99, 99
	})]
	public virtual bool isPureRef()
	{
		MediaInfoBox minf = getMdia().getMinf();
		DataInfoBox dinf = minf.getDinf();
		if (dinf == null)
		{
			return false;
		}
		DataRefBox dref = dinf.getDref();
		if (dref == null)
		{
			return false;
		}
		Iterator iterator = dref.boxes.iterator();
		while (iterator.hasNext())
		{
			Box box = (Box)iterator.next();
			if (((uint)((FullBox)box).getFlags() & (true ? 1u : 0u)) != 0)
			{
				return false;
			}
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 162, 127, 37 })]
	public virtual Rational getPAR()
	{
		PixelAspectExt pasp = (PixelAspectExt)NodeBox.findFirstPath(this, ClassLiteral<PixelAspectExt>.Value, new string[6] { "mdia", "minf", "stbl", "stsd", null, "pasp" });
		Rational result = ((pasp != null) ? pasp.getRational() : new Rational(1, 1));
		
		return result;
	}

	[LineNumberTable(23)]
	public static string fourcc()
	{
		return "trak";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 106 })]
	public TrakBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(58)]
	public virtual MediaBox getMdia()
	{
		return (MediaBox)NodeBox.findFirst(this, ClassLiteral<MediaBox>.Value, "mdia");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 63, 162, 104, 100, 138, 100, 124, 106, 99 })]
	public static long getEditedDuration(TrakBox track)
	{
		List edits = track.getEdits();
		if (edits == null)
		{
			long duration2 = track.getDuration();
			
			return duration2;
		}
		long duration = 0L;
		Iterator iterator = edits.iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			duration += edit.getDuration();
		}
		return duration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 98, 124, 100, 99, 104 })]
	public virtual string getHandlerType()
	{
		return ((HandlerBox)NodeBox.findFirstPath(this, ClassLiteral<HandlerBox>.Value, Box.path("mdia.hdlr")))?.getComponentSubType();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(111)]
	public virtual int getTimescale()
	{
		int timescale = ((MediaHeaderBox)NodeBox.findFirstPath(this, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"))).getTimescale();
		
		return timescale;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(27)]
	public static TrakBox createTrakBox()
	{
		
		TrakBox result = new TrakBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 109, 104, 100, 103, 136, 104, 104,
		100, 103, 104, 138, 110, 106, 111, 108, 105, 131
	})]
	public virtual void setDataRef(string url)
	{
		MediaInfoBox minf = getMdia().getMinf();
		DataInfoBox dinf = minf.getDinf();
		if (dinf == null)
		{
			dinf = DataInfoBox.createDataInfoBox();
			minf.add(dinf);
		}
		DataRefBox dref = dinf.getDref();
		UrlBox urlBox = UrlBox.createUrlBox(url);
		if (dref == null)
		{
			dref = DataRefBox.createDataRefBox();
			dinf.add(dref);
			dref.add(urlBox);
			return;
		}
		ListIterator lit = dref.boxes.listIterator();
		while (lit.hasNext())
		{
			FullBox box = (FullBox)lit.next();
			if (((uint)box.getFlags() & (true ? 1u : 0u)) != 0)
			{
				lit.set(urlBox);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 130, 127, 4 })]
	public virtual void setTimescale(int timescale)
	{
		((MediaHeaderBox)NodeBox.findFirstPath(this, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"))).setTimescale(timescale);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(126)]
	public virtual long rescale(long tv, long ts)
	{
		long num = tv * getTimescale();
		return (ts != -1) ? (num / ts) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(138)]
	public virtual long getMediaDuration()
	{
		long duration = ((MediaHeaderBox)NodeBox.findFirstPath(this, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"))).getDuration();
		
		return duration;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 162, 114, 100, 131, 104, 100, 131, 99,
		127, 3, 120, 99
	})]
	public virtual bool hasDataRef()
	{
		DataInfoBox dinf = getMdia().getMinf().getDinf();
		if (dinf == null)
		{
			return false;
		}
		DataRefBox dref = dinf.getDref();
		if (dref == null)
		{
			return false;
		}
		int result = 0;
		Iterator iterator = dref.boxes.iterator();
		while (iterator.hasNext())
		{
			Box box = (Box)iterator.next();
			result |= (((((FullBox)box).getFlags() & 1) != 1) ? 1 : 0);
		}
		return (byte)result != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 97, 66, 104, 104, 101, 117, 237, 61, 231,
		69
	})]
	public virtual void setPAR(Rational par)
	{
		SampleEntry[] sampleEntries = getSampleEntries();
		for (int i = 0; i < (nint)sampleEntries.LongLength; i++)
		{
			SampleEntry sampleEntry = sampleEntries[i];
			sampleEntry.removeChildren(new string[1] { "pasp" });
			sampleEntry.add(PixelAspectExt.createPixelAspectExt(par));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 97, 74, 120, 101, 114, 137, 120 })]
	public virtual void setClipRect(short x, short y, short width, short height)
	{
		NodeBox clip = (NodeBox)NodeBox.findFirst(this, ClassLiteral<NodeBox>.Value, "clip");
		if (clip == null)
		{
			clip = new NodeBox(new Header("clip"));
			add(clip);
		}
		clip.replace("crgn", ClipRegionBox.createClipRegionBox(x, y, width, height));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(202)]
	public virtual long getSampleCount()
	{
		return ((SampleSizesBox)NodeBox.findFirstPath(this, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"))).getCount();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 91, 130, 117, 113, 120, 120, 120, 106 })]
	public virtual void setAperture(Size sar, Size dar)
	{
		removeChildren(new string[1] { "tapt" });
		NodeBox tapt = new NodeBox(new Header("tapt"));
		tapt.add(ClearApertureBox.createClearApertureBox(dar.getWidth(), dar.getHeight()));
		tapt.add(ProductionApertureBox.createProductionApertureBox(dar.getWidth(), dar.getHeight()));
		tapt.add(EncodedPixelBox.createEncodedPixelBox(sar.getWidth(), sar.getHeight()));
		add(tapt);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 162, 115, 117 })]
	public virtual void setDimensions(Size dd)
	{
		getTrackHeader().setWidth(dd.getWidth());
		getTrackHeader().setHeight(dd.getHeight());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 66, 124 })]
	public virtual int getFrameCount()
	{
		SampleSizesBox stsz = (SampleSizesBox)NodeBox.findFirstPath(this, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
		return (int)((stsz.getDefaultSize() == 0) ? stsz.getSizes().LongLength : stsz.getCount());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 86, 98, 124 })]
	public virtual string getName()
	{
		string result = ((NameBox)NodeBox.findFirstPath(this, ClassLiteral<NameBox>.Value, Box.path("udta.name")))?.getName();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 85, 130, 124, 136, 104, 124, 104, 100, 125,
		126, 131, 125, 106, 107, 104, 27, 201
	})]
	public virtual void fixMediaTimescale(int ts)
	{
		MediaHeaderBox mdhd = (MediaHeaderBox)NodeBox.findFirstPath(this, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"));
		int oldTs = mdhd.getTimescale();
		mdhd.setTimescale(ts);
		long num = ts * mdhd.getDuration();
		long num2 = oldTs;
		mdhd.setDuration((num2 != -1) ? (num / num2) : (-num));
		List edits = getEdits();
		if (edits != null)
		{
			Iterator iterator = edits.iterator();
			while (iterator.hasNext())
			{
				Edit edit = (Edit)iterator.next();
				long num3 = ts * edit.getMediaTime();
				long num4 = oldTs;
				edit.setMediaTime((num4 != -1) ? (num3 / num4) : (-num3));
			}
		}
		TimeToSampleBox tts = (TimeToSampleBox)NodeBox.findFirstPath(this, ClassLiteral<TimeToSampleBox>.Value, Box.path("mdia.minf.stbl.stts"));
		TimeToSampleBox.TimeToSampleEntry[] entries = tts.getEntries();
		for (int i = 0; i < (nint)entries.LongLength; i++)
		{
			TimeToSampleBox.TimeToSampleEntry tte = entries[i];
			int num5 = ts * tte.getSampleDuration();
			tte.setSampleDuration((oldTs != -1) ? (num5 / oldTs) : (-num5));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 80, 130, 119, 100, 113, 136, 117, 111 })]
	public virtual void setName(string @string)
	{
		NodeBox udta = (NodeBox)NodeBox.findFirst(this, ClassLiteral<NodeBox>.Value, "udta");
		if (udta == null)
		{
			udta = new NodeBox(new Header("udta"));
			add(udta);
		}
		udta.removeChildren(new string[1] { "name" });
		udta.add(NameBox.createNameBox(@string));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 76, 162, 106, 105, 113, 136 })]
	public virtual Size getCodedSize()
	{
		SampleEntry se = getSampleEntries()[0];
		if (!(se is VideoSampleEntry))
		{
			
			throw new IllegalArgumentException("Not a video track");
		}
		VideoSampleEntry vse = (VideoSampleEntry)se;
		Size result = new Size(vse.getWidth(), vse.getHeight());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(276)]
	public virtual TimeToSampleBox getStts()
	{
		return (TimeToSampleBox)NodeBox.findFirstPath(this, ClassLiteral<TimeToSampleBox>.Value, Box.path("mdia.minf.stbl.stts"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(280)]
	public virtual ChunkOffsetsBox getStco()
	{
		return (ChunkOffsetsBox)NodeBox.findFirstPath(this, ClassLiteral<ChunkOffsetsBox>.Value, Box.path("mdia.minf.stbl.stco"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(284)]
	public virtual ChunkOffsets64Box getCo64()
	{
		return (ChunkOffsets64Box)NodeBox.findFirstPath(this, ClassLiteral<ChunkOffsets64Box>.Value, Box.path("mdia.minf.stbl.co64"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(288)]
	public virtual SampleSizesBox getStsz()
	{
		return (SampleSizesBox)NodeBox.findFirstPath(this, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(292)]
	public virtual SampleToChunkBox getStsc()
	{
		return (SampleToChunkBox)NodeBox.findFirstPath(this, ClassLiteral<SampleToChunkBox>.Value, Box.path("mdia.minf.stbl.stsc"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(296)]
	public virtual SampleDescriptionBox getStsd()
	{
		return (SampleDescriptionBox)NodeBox.findFirstPath(this, ClassLiteral<SampleDescriptionBox>.Value, Box.path("mdia.minf.stbl.stsd"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(300)]
	public virtual SyncSamplesBox getStss()
	{
		return (SyncSamplesBox)NodeBox.findFirstPath(this, ClassLiteral<SyncSamplesBox>.Value, Box.path("mdia.minf.stbl.stss"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(304)]
	public virtual CompositionOffsetsBox getCtts()
	{
		return (CompositionOffsetsBox)NodeBox.findFirstPath(this, ClassLiteral<CompositionOffsetsBox>.Value, Box.path("mdia.minf.stbl.ctts"));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 65, 66, 124 })]
	public static MP4TrackType getTrackType(TrakBox trak)
	{
		HandlerBox handler = (HandlerBox)NodeBox.findFirstPath(trak, ClassLiteral<HandlerBox>.Value, Box.path("mdia.hdlr"));
		MP4TrackType result = ((handler != null) ? MP4TrackType.fromHandler(handler.getComponentSubType()) : null);
		
		return result;
	}
}
