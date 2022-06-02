using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mps.index;

public class MTSRandomAccessDemuxer : Object
{
	[SpecialName]
	[EnclosingMethod(null, "getProgramDemuxer", "(I)Lorg.jcodec.containers.mps.index.MPSRandomAccessDemuxer;")]
	internal class _1 : MPSRandomAccessDemuxer
	{
		[SpecialName]
		[InnerClass(null, (Modifiers)0)]
		[EnclosingMethod(null, "newStream", "(Lorg.jcodec.common.io.SeekableByteChannel;Lorg.jcodec.containers.mps.index.MPSIndex$MPSStreamIndex;)Lorg.jcodec.containers.mps.index.MPSRandomAccessDemuxer$Stream;")]
		internal class _2 : Stream
		{
			[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
			internal MTSRandomAccessDemuxer._1 this_00241;

			[MethodImpl(MethodImplOptions.NoInlining)]
			[LineNumberTable(41)]
			internal _2(MTSRandomAccessDemuxer._1 this_00241, MPSRandomAccessDemuxer demuxer, MPSIndex.MPSStreamIndex streamIndex, SeekableByteChannel source) : base(demuxer, streamIndex, source)
			{
				this.this_00241 = this_00241;
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[Throws(new string[] { "java.io.IOException" })]
			[LineNumberTable(new byte[]
			{
				159, 131, 66, 142, 106, 114, 119, 127, 0, 106,
				112, 99, 105, 112, 104, 104, 149, 233, 51, 234,
				79, 104
			})]
			protected internal override ByteBuffer fetch(int pesLen)
			{
				ByteBuffer bb = ByteBuffer.allocate(pesLen * 188);
				for (int i = 0; i < pesLen; i++)
				{
					ByteBuffer tsBuf = NIOUtils.fetchFromChannel(source, 188);
					Preconditions.checkState(71 == ((sbyte)tsBuf.get() & 0xFF));
					int guidFlags = (((sbyte)tsBuf.get() & 0xFF) << 8) | ((sbyte)tsBuf.get() & 0xFF);
					int guid = guidFlags & 0x1FFF;
					if (guid == this_00241.val_0024tgtGuid)
					{
						int payloadStart = (guidFlags >> 14) & 1;
						int b0 = (sbyte)tsBuf.get() & 0xFF;
						int counter = b0 & 0xF;
						if (((uint)b0 & 0x20u) != 0)
						{
							NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
						}
						bb.put(tsBuf);
					}
				}
				bb.flip();
				return bb;
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[Throws(new string[] { "java.io.IOException" })]
			[LineNumberTable(new byte[] { 159, 126, 162, 127, 2 })]
			protected internal override void skip(long leadingSize)
			{
				source.setPosition(source.position() + leadingSize * 188u);
			}

			[MethodImpl(MethodImplOptions.NoInlining)]
			[Throws(new string[] { "java.io.IOException" })]
			[LineNumberTable(new byte[] { 159, 124, 66, 111 })]
			protected internal override void reset()
			{
				source.setPosition(0L);
			}
		}

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal int val_0024tgtGuid;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MTSRandomAccessDemuxer this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(38)]
		internal _1(MTSRandomAccessDemuxer this_00240, SeekableByteChannel ch, MPSIndex mpsIndex, int i) : base(ch, mpsIndex)
		{
			this.this_00240 = this_00240;
			val_0024tgtGuid = i;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(41)]
		protected internal override Stream newStream(SeekableByteChannel ch, MPSIndex.MPSStreamIndex streamIndex)
		{
			_2 result = new _2(this, this, streamIndex, ch);
			return result;
		}
	}

	private MTSIndex.MTSProgram[] programs;

	private SeekableByteChannel ch;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 98, 117, 106, 3, 199 })]
	private MPSIndex getProgram(int guid)
	{
		MTSIndex.MTSProgram[] array = programs;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MTSIndex.MTSProgram mtsProgram = array[i];
			if (mtsProgram.getTargetGuid() == guid)
			{
				return mtsProgram;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 105, 109, 104 })]
	public MTSRandomAccessDemuxer(SeekableByteChannel ch, MTSIndex index)
	{
		programs = index.getPrograms();
		this.ch = ch;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 110, 109, 49, 135 })]
	public virtual int[] getGuids()
	{
		int[] guids = new int[(nint)programs.LongLength];
		for (int i = 0; i < (nint)programs.LongLength; i++)
		{
			guids[i] = programs[i].getTargetGuid();
		}
		return guids;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 98, 105 })]
	public virtual MPSRandomAccessDemuxer getProgramDemuxer(int tgtGuid)
	{
		MPSIndex index = getProgram(tgtGuid);
		_1 result = new _1(this, ch, index, tgtGuid);
		return result;
	}
}
