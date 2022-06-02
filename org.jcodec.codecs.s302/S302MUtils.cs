using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.codecs.s302;

public class S302MUtils : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public S302MUtils()
	{
	}

	[LineNumberTable(new byte[] { 159, 139, 130, 159, 12, 135, 135, 135, 135 })]
	public static string name(int channels)
	{
		return channels switch
		{
			1 => "Mono", 
			2 => "Stereo 2.0", 
			4 => "Surround 4.0", 
			8 => "Stereo 2.0 + Surround 5.1", 
			_ => null, 
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 159, 15, 144, 152, 191, 9, 223,
		41
	})]
	public static ChannelLabel[] labels(int channels)
	{
		return channels switch
		{
			1 => new ChannelLabel[1] { ChannelLabel.___003C_003EMONO }, 
			2 => new ChannelLabel[2]
			{
				ChannelLabel.___003C_003ESTEREO_LEFT,
				ChannelLabel.___003C_003ESTEREO_RIGHT
			}, 
			4 => new ChannelLabel[4]
			{
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT
			}, 
			8 => new ChannelLabel[8]
			{
				ChannelLabel.___003C_003ESTEREO_LEFT,
				ChannelLabel.___003C_003ESTEREO_RIGHT,
				ChannelLabel.___003C_003EFRONT_LEFT,
				ChannelLabel.___003C_003EFRONT_RIGHT,
				ChannelLabel.___003C_003EREAR_LEFT,
				ChannelLabel.___003C_003EREAR_RIGHT,
				ChannelLabel.___003C_003ECENTER,
				ChannelLabel.___003C_003ELFE
			}, 
			_ => null, 
		};
	}
}
