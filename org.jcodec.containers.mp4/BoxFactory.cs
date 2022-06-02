using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4;

public class BoxFactory : Object, IBoxFactory
{
	private static IBoxFactory instance;

	private static IBoxFactory audio;

	private static IBoxFactory data;

	private static IBoxFactory sample;

	private static IBoxFactory timecode;

	private static IBoxFactory video;

	private static IBoxFactory waveext;

	private Boxes boxes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 105, 104 })]
	public BoxFactory(Boxes boxes)
	{
		this.boxes = boxes;
	}

	[LineNumberTable(36)]
	public static IBoxFactory getDefault()
	{
		return instance;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 115, 100, 106, 119, 108, 104, 105,
		113, 105, 113, 105, 110, 105, 110, 105, 110, 105,
		142, 168
	})]
	public virtual Box newBox(Header header)
	{
		Class claz = boxes.toClass(header.getFourcc());
		if (claz == null)
		{
			Box.LeafBox result = new Box.LeafBox(header);
			
			return result;
		}
		Box box = (Box)Platform.newInstance(claz, new object[1] { header });
		if (box is NodeBox)
		{
			NodeBox nodebox = (NodeBox)box;
			if (nodebox is SampleDescriptionBox)
			{
				nodebox.setFactory(sample);
			}
			else if (nodebox is VideoSampleEntry)
			{
				nodebox.setFactory(video);
			}
			else if (nodebox is AudioSampleEntry)
			{
				nodebox.setFactory(audio);
			}
			else if (nodebox is TimecodeSampleEntry)
			{
				nodebox.setFactory(timecode);
			}
			else if (nodebox is DataRefBox)
			{
				nodebox.setFactory(data);
			}
			else if (nodebox is WaveExtension)
			{
				nodebox.setFactory(waveext);
			}
			else
			{
				nodebox.setFactory(this);
			}
		}
		return box;
	}

	[LineNumberTable(new byte[] { 159, 136, 98, 112, 112, 112, 112, 112, 112 })]
	static BoxFactory()
	{
		instance = new BoxFactory(new DefaultBoxes());
		audio = new BoxFactory(new AudioBoxes());
		data = new BoxFactory(new DataBoxes());
		sample = new BoxFactory(new SampleBoxes());
		timecode = new BoxFactory(new TimecodeBoxes());
		video = new BoxFactory(new VideoBoxes());
		waveext = new BoxFactory(new WaveExtBoxes());
	}
}
