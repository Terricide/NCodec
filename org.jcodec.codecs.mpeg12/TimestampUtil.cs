using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg12;

public class TimestampUtil : Object
{
	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal sealed class _1 : BaseCommand
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal long val_0024shift;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(62)]
		internal _1(string stream, long l)
		{
			val_0024shift = l;
			base._002Ector(stream);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(64)]
		protected internal override long withTimestamp(long pts, bool isPts)
		{
			long result = Math.max(pts + val_0024shift, 0L);
			
			return result;
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal sealed class _2 : BaseCommand
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal RationalLarge val_0024scale;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(69)]
		internal _2(string stream, RationalLarge rl)
		{
			val_0024scale = rl;
			base._002Ector(stream);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(71)]
		protected internal override long withTimestamp(long pts, bool isPts)
		{
			long result = val_0024scale.multiplyS(pts);
			
			return result;
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal sealed class _3 : BaseCommand
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal int val_0024precision;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(76)]
		internal _3(string stream, int i)
		{
			val_0024precision = i;
			base._002Ector(stream);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(78)]
		protected internal override long withTimestamp(long pts, bool isPts)
		{
			return Math.round((double)pts / (double)val_0024precision) * val_0024precision;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static | Modifiers.Abstract)]
	internal abstract class BaseCommand : FixTimestamp
	{
		private string streamSelector;

		protected internal abstract long withTimestamp(long l, bool b);

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 162, 105, 104 })]
		public BaseCommand(string stream)
		{
			streamSelector = stream;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 65, 67, 127, 26, 113, 140 })]
		protected internal override long doWithTimestamp(int streamId, long pts, bool isPts)
		{
			if (String.instancehelper_equals("all", streamSelector) || (String.instancehelper_equals("video", streamSelector) && isVideo(streamId)) || (String.instancehelper_equals("audio", streamSelector) && isAudio(streamId)))
			{
				long result = withTimestamp(pts, isPts);
				
				return result;
			}
			return pts;
		}
	}

	private const string STREAM_ALL = "all";

	private const string STREAM_AUDIO = "audio";

	private const string STRAM_VIDEO = "video";

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_STREAM;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag[] ALL_FLAGS;

	private const string COMMAND_SHIFT = "shift";

	private const string COMMAND_SCALE = "scale";

	private const string COMMAND_ROUND = "round";

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(21)]
	public TimestampUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 66, 109, 107, 112, 127, 23, 242, 71,
		162, 115, 106, 115, 104, 163, 137, 115, 110, 111,
		203, 102, 115, 111, 203, 102, 112, 111, 203, 136
	})]
	public static void main1(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, ALL_FLAGS);
		if ((nint)cmd.args.LongLength < 3)
		{
			java.lang.System.@out.println("A utility to tweak MPEG TS timestamps.");
			MainUtils.printHelp(ALL_FLAGS, Arrays.asList("command", "arg", "in name", "?out file"));
			java.lang.System.@out.println("Where command is:\n\tshift\tShift timestamps of selected stream by arg.\n\tscale\tScale timestams of selected stream by arg [num:den].\n\tround\tRound timestamps of selected stream to multiples of arg.");
			return;
		}
		
		File src = new File(cmd.getArg(2));
		if (cmd.argsLength() > 3)
		{
			
			File dst = new File(cmd.getArg(3));
			IOUtils.copyFile(src, dst);
			src = dst;
		}
		string command = cmd.getArg(0);
		string stream = cmd.getStringFlagD(FLAG_STREAM, "all");
		if (String.instancehelper_equalsIgnoreCase("shift", command))
		{
			long shift = Long.parseLong(cmd.getArg(1));
			new _1(stream, shift).fix(src);
		}
		else if (String.instancehelper_equalsIgnoreCase("scale", command))
		{
			RationalLarge scale = RationalLarge.parse(cmd.getArg(1));
			new _2(stream, scale).fix(src);
		}
		else if (String.instancehelper_equalsIgnoreCase("round", command))
		{
			int precision = Integer.parseInt(cmd.getArg(1));
			new _3(stream, precision).fix(src);
		}
	}

	[LineNumberTable(new byte[] { 159, 136, 130, 186 })]
	static TimestampUtil()
	{
		FLAG_STREAM = MainUtils.Flag.flag("stream", "s", "A stream to shift, i.e. 'video' or 'audio' or 'all' [default]");
		ALL_FLAGS = new MainUtils.Flag[1] { FLAG_STREAM };
	}
}
