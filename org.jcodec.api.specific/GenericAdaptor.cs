using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.api.specific;

public class GenericAdaptor : Object, ContainerAdaptor
{
	private VideoDecoder decoder;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 105, 104 })]
	public GenericAdaptor(VideoDecoder detect)
	{
		decoder = detect;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(29)]
	public virtual Picture decodeFrame(Packet packet, byte[][] data)
	{
		Picture result = decoder.decodeFrame(packet.getData(), data);
		
		return result;
	}

	[LineNumberTable(34)]
	public virtual bool canSeek(Packet data)
	{
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(39)]
	public virtual MediaInfo getMediaInfo()
	{
		MediaInfo result = new MediaInfo(new Size(0, 0));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(44)]
	public virtual byte[][] allocatePicture()
	{
		byte[][] data = Picture.create(1920, 1088, ColorSpace.___003C_003EYUV444).getData();
		
		return data;
	}
}
