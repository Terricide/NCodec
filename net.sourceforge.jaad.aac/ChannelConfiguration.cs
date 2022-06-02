using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac;

public class ChannelConfiguration : Object
{
	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_UNSUPPORTED;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_NONE;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_MONO;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_STEREO;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_FIVE;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_FIVE_PLUS_ONE;

	internal static ChannelConfiguration ___003C_003ECHANNEL_CONFIG_SEVEN_PLUS_ONE;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int chCount;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private string descr;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_UNSUPPORTED
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_UNSUPPORTED;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_NONE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_NONE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_MONO
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_MONO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_STEREO
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_STEREO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_STEREO_PLUS_CENTER
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_FIVE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_FIVE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_FIVE_PLUS_ONE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_FIVE_PLUS_ONE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static ChannelConfiguration CHANNEL_CONFIG_SEVEN_PLUS_ONE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECHANNEL_CONFIG_SEVEN_PLUS_ONE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 135, 130, 159, 17, 103, 131, 103, 131, 103,
		131, 103, 131, 103, 131, 103, 131, 103, 163, 103,
		131, 167
	})]
	public static ChannelConfiguration forInt(int i)
	{
		switch (i)
		{
		case 0:
			return ___003C_003ECHANNEL_CONFIG_NONE;
		case 1:
			return ___003C_003ECHANNEL_CONFIG_MONO;
		case 2:
			return ___003C_003ECHANNEL_CONFIG_STEREO;
		case 3:
			return ___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER;
		case 4:
			return ___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO;
		case 5:
			return ___003C_003ECHANNEL_CONFIG_FIVE;
		case 6:
			return ___003C_003ECHANNEL_CONFIG_FIVE_PLUS_ONE;
		case 7:
		case 8:
			return ___003C_003ECHANNEL_CONFIG_SEVEN_PLUS_ONE;
		default:
			return ___003C_003ECHANNEL_CONFIG_UNSUPPORTED;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 130, 105, 104, 104 })]
	private ChannelConfiguration(int chCount, string descr)
	{
		this.chCount = chCount;
		this.descr = descr;
	}

	[LineNumberTable(75)]
	public virtual int getChannelCount()
	{
		return chCount;
	}

	[LineNumberTable(84)]
	public virtual string getDescription()
	{
		return descr;
	}

	[LineNumberTable(95)]
	public override string toString()
	{
		return descr;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 130, 113, 113, 113, 113, 145, 145, 113,
		145
	})]
	static ChannelConfiguration()
	{
		___003C_003ECHANNEL_CONFIG_UNSUPPORTED = new ChannelConfiguration(-1, "invalid");
		___003C_003ECHANNEL_CONFIG_NONE = new ChannelConfiguration(0, "No channel");
		___003C_003ECHANNEL_CONFIG_MONO = new ChannelConfiguration(1, "Mono");
		___003C_003ECHANNEL_CONFIG_STEREO = new ChannelConfiguration(2, "Stereo");
		___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER = new ChannelConfiguration(3, "Stereo+Center");
		___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO = new ChannelConfiguration(4, "Stereo+Center+Rear");
		___003C_003ECHANNEL_CONFIG_FIVE = new ChannelConfiguration(5, "Five channels");
		___003C_003ECHANNEL_CONFIG_FIVE_PLUS_ONE = new ChannelConfiguration(6, "Five channels+LF");
		___003C_003ECHANNEL_CONFIG_SEVEN_PLUS_ONE = new ChannelConfiguration(8, "Seven channels+LF");
	}
}
