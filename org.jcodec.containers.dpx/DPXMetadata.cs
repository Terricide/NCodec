using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common;

namespace org.jcodec.containers.dpx;

public class DPXMetadata : Object
{
	public const string V2 = "V2.0";

	public const string V1 = "V1.0";

	public FileHeader file;

	public ImageHeader image;

	public ImageSourceHeader imageSource;

	public FilmHeader film;

	public TelevisionHeader television;

	public string userId;

	[LineNumberTable(new byte[] { 159, 134, 66, 102, 101, 107, 99 })]
	private static int bcd2uint(int bcd)
	{
		int low = bcd & 0xF;
		int high = bcd >> 4;
		if (low > 9 || high > 9)
		{
			return 0;
		}
		return low + 10 * high;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 65, 67, 107, 109, 110, 111, 118, 125,
		118, 127, 2, 242, 61
	})]
	private static string smpteTC(int tcsmpte, bool prevent_dropframe)
	{
		int ff = bcd2uint(tcsmpte & 0x3F);
		int ss = bcd2uint((tcsmpte >> 8) & 0x7F);
		int mm = bcd2uint((tcsmpte >> 16) & 0x7F);
		int hh = bcd2uint((tcsmpte >> 24) & 0x3F);
		int drop = (((tcsmpte & 0x40000000) > 0u && !prevent_dropframe) ? 1 : 0);
		string result = new StringBuilder().append(StringUtils.zeroPad2(hh)).append(":").append(StringUtils.zeroPad2(mm))
			.append(":")
			.append(StringUtils.zeroPad2(ss))
			.append((drop == 0) ? ":" : ";")
			.append(StringUtils.zeroPad2(ff))
			.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(8)]
	public DPXMetadata()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(40)]
	public virtual string getTimecodeString()
	{
		string result = smpteTC(television.timecode, prevent_dropframe: false);
		
		return result;
	}
}
