using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio.channels;

namespace org.jcodec.common.io;

[Implements(new string[] { "java.nio.channels.ByteChannel", "java.nio.channels.Channel", "java.io.Closeable", "java.nio.channels.ReadableByteChannel", "java.nio.channels.WritableByteChannel" })]
public interface SeekableByteChannel : ByteChannel, ReadableByteChannel, Channel, Closeable, AutoCloseable, WritableByteChannel
{
	[Throws(new string[] { "java.io.IOException" })]
	SeekableByteChannel setPosition(long l);

	[Throws(new string[] { "java.io.IOException" })]
	long position();

	[Throws(new string[] { "java.io.IOException" })]
	long size();

	[Throws(new string[] { "java.io.IOException" })]
	SeekableByteChannel truncate(long l);
}
