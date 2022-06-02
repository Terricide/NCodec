using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.wav;
using org.jcodec.common.io;

namespace org.jcodec.common.tools;

public class WavSplit : Object
{
	internal static MainUtils.Flag ___003C_003EFLAG_PATTERN;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag[] ALL_FLAGS;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static MainUtils.Flag FLAG_PATTERN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EFLAG_PATTERN;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 105, 104, 52, 167, 149, 110, 104,
		105, 104, 104, 106, 109, 234, 61, 236, 70
	})]
	private static void copy(AudioFormat format, ReadableByteChannel @is, org.jcodec.common.io.SeekableByteChannel[] @out)
	{
		ByteBuffer[] outs = new ByteBuffer[(nint)@out.LongLength];
		for (int j = 0; j < (nint)@out.LongLength; j++)
		{
			outs[j] = ByteBuffer.allocate(format.framesToBytes(4096));
		}
		ByteBuffer inb = ByteBuffer.allocate((int)(format.framesToBytes(4096) * (nint)@out.LongLength));
		while (@is.read(inb) != -1)
		{
			inb.flip();
			AudioUtil.deinterleave(format, inb, outs);
			inb.clear();
			for (int i = 0; i < (nint)@out.LongLength; i++)
			{
				outs[i].flip();
				@out[i].write(outs[i]);
				outs[i].clear();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(28)]
	public WavSplit()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 109, 106, 126, 167, 111, 146, 136,
		159, 11, 116, 105, 105, 140, 110, 106, 106, 127,
		18, 18, 233, 69, 144, 106, 43, 169
	})]
	public static void main1(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, ALL_FLAGS);
		if (cmd.argsLength() < 1)
		{
			MainUtils.printHelp(ALL_FLAGS, Arrays.asList("filename.wav"));
			java.lang.System.exit(-1);
		}
		
		File s = new File(args[0]);
		string pattern = cmd.getStringFlagD(___003C_003EFLAG_PATTERN, "c%02d.wav");
		WavHeader wavHeader = WavHeader.read(s);
		java.lang.System.@out.println(new StringBuilder().append("WAV: ").append(wavHeader.getFormat()).toString());
		Preconditions.checkState(2 == wavHeader.fmt.numChannels);
		int dataOffset = wavHeader.dataOffset;
		FileChannelWrapper @is = NIOUtils.readableChannel(s);
		@is.setPosition(dataOffset);
		int channels = wavHeader.getFormat().getChannels();
		org.jcodec.common.io.SeekableByteChannel[] @out = new org.jcodec.common.io.SeekableByteChannel[channels];
		for (int j = 0; j < channels; j++)
		{
			int num = j;
			
			@out[num] = NIOUtils.writableChannel(new File(s.getParentFile(), String.format(pattern, Integer.valueOf(j))));
			WavHeader.copyWithChannels(wavHeader, 1).write(@out[j]);
		}
		copy(wavHeader.getFormat(), @is, @out);
		for (int i = 0; i < channels; i++)
		{
			@out[i].close();
		}
	}

	[LineNumberTable(new byte[] { 159, 135, 98, 122 })]
	static WavSplit()
	{
		___003C_003EFLAG_PATTERN = MainUtils.Flag.flag("pattern", "p", "Output file name pattern, i.e. out%02d.wav");
		ALL_FLAGS = new MainUtils.Flag[1] { ___003C_003EFLAG_PATTERN };
	}
}
