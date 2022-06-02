using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.containers.dpx;

public class ImageSourceHeader : Object
{
	public int xOffset;

	public int yOffset;

	public float xCenter;

	public float yCenter;

	public int xOriginal;

	public int yOriginal;

	public string sourceImageFilename;

	public Date sourceImageDate;

	public string deviceName;

	public string deviceSerial;

	public short[] borderValidity;

	public int[] aspectRatio;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(5)]
	public ImageSourceHeader()
	{
	}
}
