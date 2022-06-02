using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12;

public class SegmentReader : java.lang.Object
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/mpeg12/SegmentReader$State;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class State : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			MORE_DATA,
			DONE,
			STOP
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static State ___003C_003EMORE_DATA;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static State ___003C_003EDONE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static State ___003C_003ESTOP;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static State[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static State MORE_DATA
		{
			[HideFromJava]
			get
			{
				return ___003C_003EMORE_DATA;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static State DONE
		{
			[HideFromJava]
			get
			{
				return ___003C_003EDONE;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static State STOP
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESTOP;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(54)]
		private State(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(54)]
		public static State[] values()
		{
			return (State[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(54)]
		public static State valueOf(string name)
		{
			return (State)java.lang.Enum.valueOf(ClassLiteral<State>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 129, 162, 63, 18 })]
		static State()
		{
			___003C_003EMORE_DATA = new State("MORE_DATA", 0);
			___003C_003EDONE = new State("DONE", 1);
			___003C_003ESTOP = new State("STOP", 2);
			_0024VALUES = new State[3] { ___003C_003EMORE_DATA, ___003C_003EDONE, ___003C_003ESTOP };
		}
	}

	private ReadableByteChannel channel;

	private ByteBuffer buf;

	protected internal int curMarker;

	private int fetchSize;

	protected internal bool done;

	private long pos;

	private int bytesInMarker;

	private int bufferIncrement;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[] { 159, 110, 162, 109, 105, 104, 105, 105 })]
	public virtual void readToNextMarkerBuffers(List buffers)
	{
		State state;
		do
		{
			ByteBuffer curBuffer = ByteBuffer.allocate(bufferIncrement);
			state = readToNextMarkerPartial(curBuffer);
			curBuffer.flip();
			buffers.add(curBuffer);
		}
		while (state == State.___003C_003EMORE_DATA);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 105, 103, 127, 1, 136, 113, 123,
		100, 135, 133, 105, 103, 114, 159, 8, 120, 122,
		145, 106, 127, 0, 135, 114, 114, 111, 111, 123,
		167, 105, 104, 135
	})]
	public State readToNextMarkerPartial(ByteBuffer @out)
	{
		if (done)
		{
			return State.___003C_003ESTOP;
		}
		int skipOneMarker = ((curMarker >= 256 && curMarker <= 511) ? 1 : 0);
		int written = @out.position();
		while (true)
		{
			if (buf.hasRemaining())
			{
				if (curMarker >= 256 && curMarker <= 511)
				{
					if (skipOneMarker == 0)
					{
						return State.___003C_003EDONE;
					}
					skipOneMarker += -1;
				}
				if (!@out.hasRemaining())
				{
					return State.___003C_003EMORE_DATA;
				}
				@out.put((byte)(sbyte)((uint)curMarker >> 24));
				curMarker = (curMarker << 8) | ((sbyte)buf.get() & 0xFF);
			}
			else
			{
				buf = NIOUtils.fetchFromChannel(channel, fetchSize);
				pos += buf.remaining();
				if (!buf.hasRemaining())
				{
					break;
				}
			}
		}
		written = @out.position() - written;
		if (written > 0 && curMarker >= 256 && curMarker <= 511)
		{
			return State.___003C_003EDONE;
		}
		while (bytesInMarker > 0 && @out.hasRemaining())
		{
			@out.put((byte)(sbyte)((uint)curMarker >> 24));
			curMarker <<= 8;
			bytesInMarker--;
			if (curMarker >= 256 && curMarker <= 511)
			{
				return State.___003C_003EDONE;
			}
		}
		if (bytesInMarker == 0)
		{
			done = true;
			return State.___003C_003ESTOP;
		}
		return State.___003C_003EMORE_DATA;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 9, 172, 104, 104, 110, 115, 114,
		104
	})]
	public SegmentReader(ReadableByteChannel channel, int fetchSize)
	{
		bufferIncrement = 32768;
		this.channel = channel;
		this.fetchSize = fetchSize;
		buf = NIOUtils.fetchFromChannel(channel, 4);
		pos = buf.remaining();
		curMarker = buf.getInt();
		bytesInMarker = 4;
	}

	[LineNumberTable(44)]
	public virtual int getBufferIncrement()
	{
		return bufferIncrement;
	}

	[LineNumberTable(new byte[] { 159, 130, 98, 104 })]
	public virtual void setBufferIncrement(int bufferIncrement)
	{
		this.bufferIncrement = bufferIncrement;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 113, 162, 105, 99, 103, 136 })]
	public virtual ByteBuffer readToNextMarkerNewBuffer()
	{
		if (done)
		{
			return null;
		}
		ArrayList buffers = new ArrayList();
		readToNextMarkerBuffers(buffers);
		ByteBuffer result = NIOUtils.combineBuffers(buffers);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 108, 162, 105, 105, 108 })]
	public bool readToNextMarker(ByteBuffer @out)
	{
		State state = readToNextMarkerPartial(@out);
		if (state == State.___003C_003EMORE_DATA)
		{
			throw new BufferOverflowException();
		}
		return state == State.___003C_003EDONE;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 106, 130, 105, 131, 110, 127, 3, 123, 163,
		120, 122, 113, 136
	})]
	public bool skipToMarker()
	{
		if (done)
		{
			return false;
		}
		while (true)
		{
			if (buf.hasRemaining())
			{
				curMarker = (curMarker << 8) | ((sbyte)buf.get() & 0xFF);
				if (curMarker >= 256 && curMarker <= 511)
				{
					return true;
				}
				continue;
			}
			buf = NIOUtils.fetchFromChannel(channel, fetchSize);
			pos += buf.remaining();
			if (!buf.hasRemaining())
			{
				break;
			}
		}
		done = true;
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 101, 66, 105, 131, 110, 105, 99, 114, 159,
		5, 120, 122, 113, 110, 136
	})]
	public bool read(ByteBuffer @out, int length)
	{
		if (done)
		{
			return false;
		}
		while (true)
		{
			if (buf.hasRemaining())
			{
				int num = length;
				length += -1;
				if (num == 0)
				{
					return true;
				}
				@out.put((byte)(sbyte)((uint)curMarker >> 24));
				curMarker = (curMarker << 8) | ((sbyte)buf.get() & 0xFF);
			}
			else
			{
				buf = NIOUtils.fetchFromChannel(channel, fetchSize);
				pos += buf.remaining();
				if (!buf.hasRemaining())
				{
					break;
				}
			}
		}
		@out.putInt(curMarker);
		done = true;
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(183)]
	public long curPos()
	{
		return pos - buf.remaining() - 4u;
	}
}
