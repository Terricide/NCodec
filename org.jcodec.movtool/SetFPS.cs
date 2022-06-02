using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class SetFPS : Object
{
	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal class _1 : Object, MP4Edit
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal RationalLarge val_0024newFPS;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(34)]
		internal _1(RationalLarge rl) : base()
		{
			val_0024newFPS = rl;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 133, 98, 104, 104, 104, 100, 101, 121, 108,
			22, 233, 69, 124, 135, 174, 127, 2, 105, 127,
			2, 121, 63, 3, 169, 176, 107, 127, 6, 126,
			105, 105, 99, 159, 23
		})]
		public virtual void apply(MovieBox mov)
		{
			TrakBox vt = mov.getVideoTrack();
			TimeToSampleBox stts = vt.getStts();
			TimeToSampleBox.TimeToSampleEntry[] entries = stts.getEntries();
			long nSamples = 0L;
			long totalDuration = 0L;
			TimeToSampleBox.TimeToSampleEntry[] array = entries;
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				TimeToSampleBox.TimeToSampleEntry e2 = array[i];
				nSamples += e2.getSampleCount();
				totalDuration += e2.getSampleCount() * e2.getSampleDuration();
			}
			int newTimescale = (int)val_0024newFPS.multiply(new RationalLarge(totalDuration, nSamples)).scalarClip();
			if (newTimescale >= 25)
			{
				vt.setTimescale(newTimescale);
			}
			else
			{
				RationalLarge.___003Cclinit_003E();
				double mul = new RationalLarge(vt.getTimescale() * totalDuration, nSamples).divideBy(val_0024newFPS).scalar();
				Logger.info(new StringBuilder().append("Applying multiplier to sample durations: ").append(mul).toString());
				TimeToSampleBox.TimeToSampleEntry[] array2 = entries;
				int num2 = array2.Length;
				for (int j = 0; j < num2; j++)
				{
					TimeToSampleBox.TimeToSampleEntry e = array2[j];
					e.setSampleDuration(ByteCodeHelper.d2i((double)e.getSampleDuration() * mul * 100.0));
				}
				vt.setTimescale(vt.getTimescale() * 100);
			}
			if (newTimescale != vt.getTimescale())
			{
				Logger.info(new StringBuilder().append("Changing timescale to: ").append(vt.getTimescale()).toString());
				long num3 = totalDuration * mov.getTimescale();
				long num4 = vt.getTimescale();
				long newDuration = ((num4 != -1) ? (num3 / num4) : (-num3));
				mov.setDuration(newDuration);
				vt.setDuration(newDuration);
			}
			else
			{
				Logger.info(new StringBuilder().append("Already at ").append(val_0024newFPS.toString()).append("fps, not changing.")
					.toString());
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(74)]
		public virtual void applyToFragment(MovieBox mov, MovieFragmentBox[] fragmentBox)
		{
			
			throw new RuntimeException("Unsupported");
		}
	}

	private const int MIN_TIMESCALE_ALLOWED = 25;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(23)]
	public SetFPS()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159,
		136,
		162,
		110,
		106,
		124,
		135,
		142,
		byte.MaxValue,
		4,
		107
	})]
	public static void main1(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, new MainUtils.Flag[0]);
		if (cmd.argsLength() < 2)
		{
			MainUtils.printHelpNoFlags("movie", "num:den");
			java.lang.System.exit(-1);
		}
		RationalLarge newFPS = RationalLarge.parse(cmd.getArg(1));
		InplaceMP4Editor inplaceMP4Editor = new InplaceMP4Editor();
		
		inplaceMP4Editor.modify(new File(cmd.getArg(0)), new _1(newFPS));
	}
}
