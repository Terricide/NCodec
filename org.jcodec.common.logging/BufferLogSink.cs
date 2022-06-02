using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.common.logging;

public class BufferLogSink : Object, LogSink
{
	[Signature("Ljava/util/List<Lorg/jcodec/common/logging/Message;>;")]
	private List messages;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 108 })]
	public BufferLogSink()
	{
		messages = new LinkedList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 110 })]
	public virtual void postMessage(Message msg)
	{
		messages.add(msg);
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/common/logging/Message;>;")]
	[LineNumberTable(28)]
	public virtual List getMessages()
	{
		return messages;
	}
}
