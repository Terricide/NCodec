using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.api;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.containers.mp4.muxer;

public abstract class AbstractMP4MuxerTrack : Object, MuxerTrack
{
	protected internal const int NO_TIMESCALE_SET = -1;

	protected internal int trackId;

	protected internal MP4TrackType type;

	protected internal int _timescale;

	protected internal Rational tgtChunkDuration;

	protected internal Unit tgtChunkDurationUnit;

	protected internal long chunkDuration;

	[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	protected internal List curChunk;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/SampleToChunkBox$SampleToChunkEntry;>;")]
	protected internal List samplesInChunks;

	protected internal int samplesInLastChunk;

	protected internal int chunkNo;

	protected internal bool finished;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/SampleEntry;>;")]
	protected internal List sampleEntries;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	protected internal List edits;

	private string name;

	protected internal SeekableByteChannel @out;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 66, 101, 127, 19, 115, 119, 117, 127,
		1, 136
	})]
	public virtual Size getDisplayDimensions()
	{
		int width = 0;
		int height = 0;
		if (sampleEntries != null && !sampleEntries.isEmpty() && sampleEntries.get(0) is VideoSampleEntry)
		{
			VideoSampleEntry vse = (VideoSampleEntry)sampleEntries.get(0);
			PixelAspectExt paspBox = (PixelAspectExt)NodeBox.findFirst(vse, ClassLiteral<PixelAspectExt>.Value, PixelAspectExt.fourcc());
			Rational pasp = ((paspBox == null) ? new Rational(1, 1) : paspBox.getRational());
			int num = pasp.getNum() * vse.getWidth();
			int den = pasp.getDen();
			width = ((den != -1) ? (num / den) : (-num));
			height = vse.getHeight();
		}
		Size result = new Size(width, height);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 162, 233, 53, 104, 232, 75, 108, 108,
		140, 104, 104, 104
	})]
	public AbstractMP4MuxerTrack(int trackId, MP4TrackType type)
	{
		samplesInLastChunk = -1;
		chunkNo = 0;
		curChunk = new ArrayList();
		samplesInChunks = new ArrayList();
		sampleEntries = new ArrayList();
		this.trackId = trackId;
		this.type = type;
		_timescale = -1;
	}

	[LineNumberTable(new byte[] { 159, 121, 130, 104 })]
	internal virtual AbstractMP4MuxerTrack setOut(SeekableByteChannel @out)
	{
		this.@out = @out;
		return this;
	}

	[LineNumberTable(new byte[] { 159, 120, 162, 104, 104 })]
	public virtual void setTgtChunkDuration(Rational duration, Unit unit)
	{
		tgtChunkDuration = duration;
		tgtChunkDurationUnit = unit;
	}

	public abstract long getTrackTotalDuration();

	[Throws(new string[] { "java.io.IOException" })]
	protected internal abstract Box finish(MovieHeaderBox mhb);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(100)]
	public virtual bool isVideo()
	{
		return type == MP4TrackType.___003C_003EVIDEO;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(104)]
	public virtual bool isTimecode()
	{
		return type == MP4TrackType.___003C_003ETIMECODE;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(108)]
	public virtual bool isAudio()
	{
		return type == MP4TrackType.___003C_003ESOUND;
	}

	[LineNumberTable(112)]
	public virtual MP4TrackType getType()
	{
		return type;
	}

	[LineNumberTable(116)]
	public virtual int getTrackId()
	{
		return trackId;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 66, 104, 113, 113, 120, 120, 120, 138 })]
	public virtual void tapt(TrakBox trak)
	{
		Size dd = getDisplayDimensions();
		if (type == MP4TrackType.___003C_003EVIDEO)
		{
			NodeBox tapt = new NodeBox(new Header("tapt"));
			tapt.add(ClearApertureBox.createClearApertureBox(dd.getWidth(), dd.getHeight()));
			tapt.add(ProductionApertureBox.createProductionApertureBox(dd.getWidth(), dd.getHeight()));
			tapt.add(EncodedPixelBox.createEncodedPixelBox(dd.getWidth(), dd.getHeight()));
			trak.add(tapt);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 162, 119, 110 })]
	public virtual AbstractMP4MuxerTrack addSampleEntry(SampleEntry se)
	{
		Preconditions.checkState((!finished) ? true : false, (object)"The muxer track has finished muxing");
		sampleEntries.add(se);
		return this;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/SampleEntry;>;")]
	[LineNumberTable(149)]
	public virtual List getEntries()
	{
		return sampleEntries;
	}

	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;)V")]
	[LineNumberTable(new byte[] { 159, 104, 98, 104 })]
	public virtual void setEdits(List edits)
	{
		this.edits = edits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 98, 105, 113, 114, 138 })]
	protected internal virtual void putEdits(TrakBox trak)
	{
		if (edits != null)
		{
			NodeBox edts = new NodeBox(new Header("edts"));
			edts.add(EditListBox.createEditListBox(edits));
			trak.add(edts);
		}
	}

	[LineNumberTable(new byte[] { 159, 101, 98, 104 })]
	public virtual void setName(string name)
	{
		this.name = name;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 100, 98, 105, 113, 114, 138 })]
	protected internal virtual void putName(TrakBox trak)
	{
		if (name != null)
		{
			NodeBox udta = new NodeBox(new Header("udta"));
			udta.add(NameBox.createNameBox(name));
			trak.add(udta);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 98, 105, 107, 104, 104, 110, 103, 104,
		104, 113, 113, 108, 113, 104, 127, 28, 38, 166,
		104, 173, 159, 22
	})]
	protected internal virtual void mediaHeader(MediaInfoBox minf, MP4TrackType type)
	{
		if (MP4TrackType.___003C_003EVIDEO == type)
		{
			VideoMediaHeaderBox vmhd = VideoMediaHeaderBox.createVideoMediaHeaderBox(0, 0, 0, 0);
			vmhd.setFlags(1);
			minf.add(vmhd);
		}
		else if (MP4TrackType.___003C_003ESOUND == type)
		{
			SoundMediaHeaderBox smhd = SoundMediaHeaderBox.createSoundMediaHeaderBox();
			smhd.setFlags(1);
			minf.add(smhd);
		}
		else if (MP4TrackType.___003C_003ETIMECODE == type)
		{
			NodeBox gmhd = new NodeBox(new Header("gmhd"));
			gmhd.add(GenericMediaInfoBox.createGenericMediaInfoBox());
			NodeBox tmcd = new NodeBox(new Header("tmcd"));
			gmhd.add(tmcd);
			tmcd.add(TimecodeMediaInfoBox.createTimecodeMediaInfoBox(0, 0, 12, new short[3] { 0, 0, 0 }, new short[3] { 255, 255, 255 }, "Lucida Grande"));
			minf.add(gmhd);
		}
		else if (MP4TrackType.___003C_003EDATA != type)
		{
			string @string = new StringBuilder().append("Handler ").append(type.getHandler()).append(" not supported")
				.toString();
			
			throw new UnhandledStateException(@string);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 130, 103, 104, 103, 104, 127, 22 })]
	protected internal virtual void addDref(NodeBox minf)
	{
		DataInfoBox dinf = DataInfoBox.createDataInfoBox();
		minf.add(dinf);
		DataRefBox dref = DataRefBox.createDataRefBox();
		dinf.add(dref);
		dref.add(Box.createLeafBox(Header.createHeader("alis", 0L), ByteBuffer.wrap(new byte[4] { 0, 0, 0, 1 })));
	}

	[LineNumberTable(210)]
	protected internal virtual int getTimescale()
	{
		return _timescale;
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public abstract void addFrame(Packet P_0);
}
