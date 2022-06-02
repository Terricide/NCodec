using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio.channels;
using java.util;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class QTEdit : java.lang.Object
{
	public abstract class BaseCommand : java.lang.Object, MP4Edit
	{
		public abstract void apply(MovieBox mb);

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(36)]
		public BaseCommand()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 133, 130, 106 })]
		public virtual void applyRefs(MovieBox movie, FileChannel[][] refs)
		{
			apply(movie);
		}

		[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
		public abstract void applyToFragment(MovieBox P_0, MovieFragmentBox[] P_1);
	}

	public interface EditFactory
	{
		string getName();

		[Signature("(Ljava/util/List<Ljava/lang/String;>;)Lorg/jcodec/movtool/MP4Edit;")]
		MP4Edit parseArgs(List l);

		string getHelp();
	}

	internal EditFactory[] ___003C_003Efactories;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/movtool/Flatten$ProgressListener;>;")]
	private List listeners;

	[Modifiers(Modifiers.Protected | Modifiers.Final)]
	protected internal EditFactory[] factories
	{
		[HideFromJava]
		get
		{
			return ___003C_003Efactories;
		}
		[HideFromJava]
		private set
		{
			___003C_003Efactories = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 98, 112, 112, 112, 117, 63, 11, 199,
		105
	})]
	protected internal virtual void help()
	{
		java.lang.System.@out.println("Quicktime movie editor");
		java.lang.System.@out.println("Syntax: qtedit <command1> <options> ... <commandN> <options> <movie>");
		java.lang.System.@out.println("Where options:");
		EditFactory[] __003C_003Efactories = ___003C_003Efactories;
		int num = __003C_003Efactories.Length;
		for (int i = 0; i < num; i++)
		{
			EditFactory commandFactory = __003C_003Efactories[i];
			java.lang.System.@out.println(new StringBuilder().append("\t").append(commandFactory.getHelp()).toString());
		}
		java.lang.System.exit(-1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 105, 108, 104 })]
	public QTEdit(EditFactory[] editFactories)
	{
		listeners = new ArrayList();
		___003C_003Efactories = editFactories;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 110 })]
	public virtual void addProgressListener(Flatten.ProgressListener listener)
	{
		listeners.add(listener);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 141, 103, 141, 112, 127, 5, 137,
		223, 14, 227, 61, 99, 127, 12, 226, 57, 234,
		76, 107, 99, 102, 105, 112, 135, 105, 112, 135,
		153, 106, 127, 22, 167, 115
	})]
	public virtual void execute(string[] args)
	{
		LinkedList aa = new LinkedList(Arrays.asList(args));
		LinkedList commands = new LinkedList();
		while (aa.size() > 0)
		{
			int i;
			for (i = 0; i < (nint)___003C_003Efactories.LongLength; i++)
			{
				if (!java.lang.String.instancehelper_equals((string)aa.get(0), ___003C_003Efactories[i].getName()))
				{
					continue;
				}
				aa.remove(0);
				java.lang.Exception ex2;
				try
				{
					((List)commands).add((object)___003C_003Efactories[i].parseArgs(aa));
				}
				catch (System.Exception x)
				{
					java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
					if (ex == null)
					{
						throw;
					}
					ex2 = ex;
					goto IL_0089;
				}
				break;
				IL_0089:
				java.lang.Exception e = ex2;
				java.lang.System.err.println(new StringBuilder().append("ERROR: ").append(Throwable.instancehelper_getMessage(e)).toString());
				return;
			}
			if ((nint)i == (nint)___003C_003Efactories.LongLength)
			{
				break;
			}
		}
		if (aa.size() == 0)
		{
			java.lang.System.err.println("ERROR: A movie file should be specified");
			help();
		}
		if (((List)commands).size() == 0)
		{
			java.lang.System.err.println("ERROR: At least one command should be specified");
			help();
		}
		
		File input = new File((string)aa.remove(0));
		if (!input.exists())
		{
			java.lang.System.err.println(new StringBuilder().append("ERROR: Input file '").append(input.getAbsolutePath()).append("' doesn't exist")
				.toString());
			help();
		}
		new ReplaceMP4Editor().replace(input, new CompoundMP4Edit(commands));
	}
}
