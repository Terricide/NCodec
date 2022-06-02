using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.model;

public class TapeTimecode : Object
{
	internal static TapeTimecode ___003C_003EZERO_TAPE_TIMECODE;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private short hour;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private byte minute;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private byte second;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private byte frame;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool dropFrame;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int tapeFps;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static TapeTimecode ZERO_TAPE_TIMECODE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EZERO_TAPE_TIMECODE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 97, 81, 106, 104, 104, 104, 104, 105,
		105
	})]
	public TapeTimecode(short hour, byte minute, byte second, byte frame, bool dropFrame, int tapeFps)
	{
		int minute2 = (sbyte)minute;
		int second2 = (sbyte)second;
		int frame2 = (sbyte)frame;
		
		this.hour = hour;
		this.minute = (byte)minute2;
		this.second = (byte)second2;
		this.frame = (byte)frame2;
		this.dropFrame = dropFrame;
		this.tapeFps = tapeFps;
	}

	[LineNumberTable(51)]
	public virtual bool isDropFrame()
	{
		return dropFrame;
	}

	[LineNumberTable(35)]
	public virtual short getHour()
	{
		return hour;
	}

	[LineNumberTable(39)]
	public virtual byte getMinute()
	{
		return (byte)(sbyte)minute;
	}

	[LineNumberTable(43)]
	public virtual byte getSecond()
	{
		return (byte)(sbyte)second;
	}

	[LineNumberTable(47)]
	public virtual byte getFrame()
	{
		return (byte)(sbyte)frame;
	}

	[LineNumberTable(55)]
	public virtual int getTapeFps()
	{
		return tapeFps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 66, 127, 8, 124, 127, 12, 242, 61 })]
	public override string toString()
	{
		string result = new StringBuilder().append(StringUtils.zeroPad2(hour)).append(":").append(StringUtils.zeroPad2((sbyte)minute))
			.append(":")
			.append(StringUtils.zeroPad2((sbyte)second))
			.append((!dropFrame) ? ":" : ";")
			.append(StringUtils.zeroPad2((sbyte)frame))
			.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 161, 67, 100, 106, 117, 153, 111 })]
	public static TapeTimecode tapeTimecode(long frame, bool dropFrame, int tapeFps)
	{
		if (dropFrame)
		{
			long D = frame / 17982L;
			long num = frame;
			long num2 = 17982L;
			long M = ((num2 != -1) ? (num % num2) : 0);
			frame += 18u * D + 2u * ((M - 2u) / 1798L);
		}
		long num3 = frame;
		long num4 = tapeFps;
		long sec = ((num4 != -1) ? (num3 / num4) : (-num3));
		short num5 = (short)(sec / 3600L);
		long num6 = sec / 60L;
		long num7 = 60L;
		sbyte num8 = (sbyte)((num7 != -1) ? (num6 % num7) : 0);
		long num9 = 60L;
		sbyte num10 = (sbyte)((num9 != -1) ? (sec % num9) : 0);
		long num11 = frame;
		long num12 = tapeFps;
		TapeTimecode result = new TapeTimecode(num5, (byte)num8, (byte)num10, (byte)(sbyte)((num12 != -1) ? (num11 % num12) : 0), dropFrame, tapeFps);
		
		return result;
	}

	[LineNumberTable(16)]
	static TapeTimecode()
	{
		___003C_003EZERO_TAPE_TIMECODE = new TapeTimecode(0, 0, 0, 0, dropFrame: false, 0);
	}
}
