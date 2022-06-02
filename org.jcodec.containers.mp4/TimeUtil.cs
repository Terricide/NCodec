using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.containers.mp4;

public class TimeUtil : Object
{
	internal static long ___003C_003EMOV_TIME_OFFSET;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static long MOV_TIME_OFFSET
	{
		[HideFromJava]
		get
		{
			return ___003C_003EMOV_TIME_OFFSET;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(29)]
	public static long fromMovTime(int movSec)
	{
		return movSec * 1000u + ___003C_003EMOV_TIME_OFFSET;
	}

	[LineNumberTable(33)]
	public static int toMovTime(long millis)
	{
		return (int)((millis - ___003C_003EMOV_TIME_OFFSET) / 1000L);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public TimeUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(25)]
	public static Date macTimeToDate(int movSec)
	{
		
		Date result = new Date(fromMovTime(movSec));
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 138, 130, 113, 113, 106, 108 })]
	static TimeUtil()
	{
		Calendar calendar = Calendar.getInstance(TimeZone.getTimeZone("GMT"));
		calendar.set(1904, 0, 1, 0, 0, 0);
		calendar.set(14, 0);
		___003C_003EMOV_TIME_OFFSET = calendar.getTimeInMillis();
	}
}
