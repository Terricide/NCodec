using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;

namespace org.jcodec.common.logging;

public class Logger : Object
{
	[Signature("Ljava/util/List<Lorg/jcodec/common/logging/LogSink;>;")]
	private static List stageSinks;

	[Signature("Ljava/util/List<Lorg/jcodec/common/logging/LogSink;>;")]
	private static List sinks;

	private static LogLevel globalLogLevel;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 111 })]
	public static void debug(string message, params object[] args)
	{
		Logger.message(LogLevel.___003C_003EDEBUG, message, args);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 111 })]
	public static void debug(string message)
	{
		Logger.message(LogLevel.___003C_003EDEBUG, message, null);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 111 })]
	public static void warn(string message)
	{
		Logger.message(LogLevel.___003C_003EWARN, message, null);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 111 })]
	public static void warn(string message, params object[] args)
	{
		Logger.message(LogLevel.___003C_003EWARN, message, args);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 111 })]
	public static void info(string message)
	{
		Logger.message(LogLevel.___003C_003EINFO, message, null);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 98, 104, 113, 109 })]
	public static void addSink(LogSink sink)
	{
		if (stageSinks == null)
		{
			
			throw new IllegalStateException("Logger already started");
		}
		stageSinks.add(sink);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 111 })]
	public static void error(string message)
	{
		Logger.message(LogLevel.___003C_003EERROR, message, null);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 115, 130, 104, 109, 104, 107, 103,
		109, 145, 176, 114, 110, 159, 3, 99, 154, 127,
		2, 105, 99
	})]
	private static void message(LogLevel level, string message, object[] args)
	{
		if (globalLogLevel.ordinal() >= level.ordinal())
		{
			return;
		}
		if (sinks == null)
		{
			lock (ClassLiteral<Logger>.Value)
			{
				if (sinks == null)
				{
					sinks = stageSinks;
					stageSinks = null;
					if (sinks.isEmpty())
					{
						sinks.add(OutLogSink.createOutLogSink());
					}
				}
			}
		}
		Message msg;
		if (LogLevel.___003C_003EDEBUG.equals(globalLogLevel))
		{
			StackTraceElement tr = Thread.currentThread().getStackTrace()[3];
			msg = new Message(level, tr.getFileName(), tr.getClassName(), tr.getMethodName(), tr.getLineNumber(), message, args);
		}
		else
		{
			msg = new Message(level, "", "", "", 0, message, args);
		}
		Iterator iterator = sinks.iterator();
		while (iterator.hasNext())
		{
			LogSink logSink = (LogSink)iterator.next();
			logSink.postMessage(msg);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(21)]
	public Logger()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 111 })]
	public static void info(string message, params object[] args)
	{
		Logger.message(LogLevel.___003C_003EINFO, message, args);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 111 })]
	public static void error(string message, params object[] args)
	{
		Logger.message(LogLevel.___003C_003EERROR, message, args);
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Super)]
	[LineNumberTable(new byte[] { 159, 120, 65, 77, 103 })]
	public static void setLevel(LogLevel level)
	{
		lock (ClassLiteral<Logger>.Value)
		{
			globalLogLevel = level;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Super)]
	[LineNumberTable(new byte[] { 159, 119, 65, 77 })]
	public static LogLevel getLevel()
	{
		lock (ClassLiteral<Logger>.Value)
		{
			return globalLogLevel;
		}
	}

	[LineNumberTable(new byte[] { 159, 137, 162, 235, 126 })]
	static Logger()
	{
		stageSinks = new LinkedList();
		globalLogLevel = LogLevel.___003C_003EINFO;
	}
}
