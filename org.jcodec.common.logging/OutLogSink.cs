using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.util;
using org.jcodec.common.tools;

namespace org.jcodec.common.logging;

public class OutLogSink : Object, LogSink
{
	public interface MessageFormat
	{
		string formatMessage(Message m);
	}

	public class SimpleFormat : Object, MessageFormat
	{
		private string fmt;

		[Signature("Ljava/util/Map<Lorg/jcodec/common/logging/LogLevel;Lorg/jcodec/common/tools/MainUtils$ANSIColor;>;")]
		private static Map colorMap;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 134, 162, 105, 104 })]
		public SimpleFormat(string fmt)
		{
			this.fmt = fmt;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 132, 98, 127, 44, 127, 41, 127, 70, 127,
			75, 127, 15
		})]
		public virtual string formatMessage(Message msg)
		{
			string @this = fmt;
			object __003Cref_003E = String.valueOf(msg.getLevel());
			CharSequence charSequence = default(CharSequence);
			object obj = (charSequence.___003Cref_003E = "#level");
			CharSequence target = charSequence;
			obj = (charSequence.___003Cref_003E = __003Cref_003E);
			string this2 = String.instancehelper_replace(@this, target, charSequence);
			obj = String.valueOf(30 + ((MainUtils.ANSIColor)colorMap.get(msg.getLevel())).ordinal());
			__003Cref_003E = (charSequence.___003Cref_003E = "#color_code");
			CharSequence target2 = charSequence;
			__003Cref_003E = (charSequence.___003Cref_003E = obj);
			string this3 = String.instancehelper_replace(this2, target2, charSequence);
			__003Cref_003E = msg.getClassName();
			obj = (charSequence.___003Cref_003E = "#class");
			CharSequence target3 = charSequence;
			obj = (charSequence.___003Cref_003E = __003Cref_003E);
			string this4 = String.instancehelper_replace(this3, target3, charSequence);
			obj = msg.getMethodName();
			__003Cref_003E = (charSequence.___003Cref_003E = "#method");
			CharSequence target4 = charSequence;
			__003Cref_003E = (charSequence.___003Cref_003E = obj);
			string this5 = String.instancehelper_replace(this4, target4, charSequence);
			__003Cref_003E = msg.getFileName();
			obj = (charSequence.___003Cref_003E = "#file");
			CharSequence target5 = charSequence;
			obj = (charSequence.___003Cref_003E = __003Cref_003E);
			string this6 = String.instancehelper_replace(this5, target5, charSequence);
			obj = String.valueOf(msg.getLineNumber());
			__003Cref_003E = (charSequence.___003Cref_003E = "#line");
			CharSequence target6 = charSequence;
			__003Cref_003E = (charSequence.___003Cref_003E = obj);
			string this7 = String.instancehelper_replace(this6, target6, charSequence);
			__003Cref_003E = msg.getMessage();
			obj = (charSequence.___003Cref_003E = "#message");
			CharSequence target7 = charSequence;
			obj = (charSequence.___003Cref_003E = __003Cref_003E);
			return String.instancehelper_replace(this7, target7, charSequence);
		}

		[LineNumberTable(new byte[] { 159, 136, 162, 139, 118, 118, 118, 118 })]
		static SimpleFormat()
		{
			colorMap = new HashMap();
			colorMap.put(LogLevel.___003C_003EDEBUG, MainUtils.ANSIColor.___003C_003EBROWN);
			colorMap.put(LogLevel.___003C_003EINFO, MainUtils.ANSIColor.___003C_003EGREEN);
			colorMap.put(LogLevel.___003C_003EWARN, MainUtils.ANSIColor.___003C_003EMAGENTA);
			colorMap.put(LogLevel.___003C_003EERROR, MainUtils.ANSIColor.___003C_003ERED);
		}
	}

	private static string empty;

	public static SimpleFormat DEFAULT_FORMAT;

	private PrintStream @out;

	private MessageFormat fmt;

	private LogLevel minLevel;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 105, 104, 104, 104 })]
	public OutLogSink(PrintStream @out, MessageFormat fmt, LogLevel minLevel)
	{
		this.@out = @out;
		this.fmt = fmt;
		this.minLevel = minLevel;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(54)]
	public static OutLogSink createOutLogSink()
	{
		OutLogSink result = new OutLogSink(java.lang.System.@out, DEFAULT_FORMAT, LogLevel.___003C_003EINFO);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 98, 121, 98, 110, 111 })]
	public virtual void postMessage(Message msg)
	{
		if (msg.getLevel().ordinal() >= minLevel.ordinal())
		{
			string str = fmt.formatMessage(msg);
			@out.println(str);
		}
	}

	[LineNumberTable(new byte[] { 159, 137, 162, 235, 91, 117, 63, 20 })]
	static OutLogSink()
	{
		empty = "                                                                                                                                                                                                                                                ";
		SimpleFormat.___003Cclinit_003E();
		DEFAULT_FORMAT = new SimpleFormat(new StringBuilder().append(MainUtils.colorString("[#level]", "#color_code")).append(MainUtils.bold("\t#class.#method (#file:#line):")).append("\t#message")
			.toString());
	}
}
