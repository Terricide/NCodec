using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.logging;

public class Message : Object
{
	private LogLevel level;

	private string fileName;

	private string className;

	private int lineNumber;

	private string message;

	private string methodName;

	private object[] args;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 139, 66, 105, 104, 104, 104, 105, 105, 105,
		105, 105
	})]
	public Message(LogLevel level, string fileName, string className, string methodName, int lineNumber, string message, object[] args)
	{
		this.level = level;
		this.fileName = fileName;
		this.className = className;
		this.methodName = methodName;
		this.message = methodName;
		this.lineNumber = lineNumber;
		this.message = message;
		this.args = args;
	}

	[LineNumberTable(24)]
	public virtual LogLevel getLevel()
	{
		return level;
	}

	[LineNumberTable(28)]
	public virtual string getFileName()
	{
		return fileName;
	}

	[LineNumberTable(32)]
	public virtual string getClassName()
	{
		return className;
	}

	[LineNumberTable(36)]
	public virtual string getMethodName()
	{
		return methodName;
	}

	[LineNumberTable(40)]
	public virtual int getLineNumber()
	{
		return lineNumber;
	}

	[LineNumberTable(44)]
	public virtual string getMessage()
	{
		return message;
	}

	[LineNumberTable(48)]
	public virtual object[] getArgs()
	{
		return args;
	}
}
