using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.containers.mp4;

namespace org.jcodec.api.specific;

public class AVCMP4Adaptor : Object, ContainerAdaptor
{
	private H264Decoder decoder;

	private int curENo;

	private Size size;

	private DemuxerTrackMeta meta;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 105, 104, 136, 105 })]
	public AVCMP4Adaptor(DemuxerTrackMeta meta)
	{
		this.meta = meta;
		curENo = -1;
		calcBufferSize();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 141, 146, 110, 105, 111, 99, 137,
		108, 102, 100, 106, 102, 100, 134, 114
	})]
	private void calcBufferSize()
	{
		int w = int.MinValue;
		int h = int.MinValue;
		ByteBuffer bb = meta.getCodecPrivate().duplicate();
		ByteBuffer b;
		while ((b = H264Utils.nextNALUnit(bb)) != null)
		{
			NALUnit nu = NALUnit.read(b);
			if (nu.type == NALUnitType.___003C_003ESPS)
			{
				SeqParameterSet sps = H264Utils.readSPS(b);
				int ww = sps.picWidthInMbsMinus1 + 1;
				if (ww > w)
				{
					w = ww;
				}
				int hh = SeqParameterSet.getPicHeightInMbs(sps);
				if (hh > h)
				{
					h = hh;
				}
			}
		}
		size = new Size(w << 4, h << 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 162, 109, 106, 232, 70, 105, 151 })]
	private void updateState(Packet packet)
	{
		int eNo = ((MP4Packet)packet).getEntryNo();
		if (eNo != curENo)
		{
			curENo = eNo;
		}
		if (decoder == null)
		{
			decoder = H264Decoder.createH264DecoderFromCodecPrivate(meta.getCodecPrivate());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 130, 136, 116, 146, 196 })]
	public virtual Picture decodeFrame(Packet packet, byte[][] data)
	{
		updateState(packet);
		org.jcodec.codecs.h264.io.model.Frame pic = decoder.decodeFrame(packet.getData(), data);
		Rational pasp = meta.getVideoCodecMeta().getPixelAspectRatio();
		if (pasp != null)
		{
		}
		return pic;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 130, 104 })]
	public virtual bool canSeek(Packet pkt)
	{
		updateState(pkt);
		bool result = H264Utils.idrSlice(H264Utils.splitFrame(pkt.getData()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(100)]
	public virtual byte[][] allocatePicture()
	{
		byte[][] data = Picture.create(size.getWidth(), size.getHeight(), ColorSpace.___003C_003EYUV444).getData();
		
		return data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(105)]
	public virtual MediaInfo getMediaInfo()
	{
		MediaInfo result = new MediaInfo(size);
		
		return result;
	}
}
