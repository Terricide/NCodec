using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mps.index;

public class MTSIndex : Object
{
	public class MTSProgram : MPSIndex
	{
		private int targetGuid;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 162, 108, 105 })]
		public MTSProgram(long[] pesTokens, RunLength.Integer pesStreamIds, MPSStreamIndex[] streams, int targetGuid)
			: base(pesTokens, pesStreamIds, streams)
		{
			this.targetGuid = targetGuid;
		}

		[LineNumberTable(36)]
		public virtual int getTargetGuid()
		{
			return targetGuid;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 132, 98, 110, 106 })]
		public override void serializeTo(ByteBuffer index)
		{
			index.putInt(targetGuid);
			base.serializeTo(index);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 131, 130, 104 })]
		public static MTSProgram parse(ByteBuffer read)
		{
			int targetGuid = read.getInt();
			MTSProgram result = createMTSProgram(MPSIndex.parseIndex(read), targetGuid);
			
			return result;
		}
	}

	private MTSProgram[] programs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 122 })]
	public static MTSProgram createMTSProgram(MPSIndex mpsIndex, int target)
	{
		return new MTSProgram(mpsIndex.pesTokens, mpsIndex.pesStreamIds, mpsIndex.streams, target);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 105, 104 })]
	public MTSIndex(MTSProgram[] programs)
	{
		this.programs = programs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 130, 100, 118, 45, 167 })]
	public virtual int estimateSize()
	{
		int totalSize = 64;
		MTSProgram[] array = programs;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MTSProgram mtsProgram = array[i];
			totalSize += 4 + mtsProgram.estimateSize();
		}
		return totalSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 111, 117, 105, 105, 104, 249, 60,
		231, 70
	})]
	public virtual void serializeTo(ByteBuffer buf)
	{
		buf.putInt(programs.Length);
		MTSProgram[] array = programs;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MTSProgram mtsAnalyser = array[i];
			ByteBuffer dup = buf.duplicate();
			NIOUtils.skip(buf, 4);
			mtsAnalyser.serializeTo(buf);
			dup.putInt(buf.position() - dup.position() - 4);
		}
	}

	[LineNumberTable(56)]
	public virtual MTSProgram[] getPrograms()
	{
		return programs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 66, 104, 104, 103, 104, 16, 199 })]
	public static MTSIndex parse(ByteBuffer buf)
	{
		int numPrograms = buf.getInt();
		MTSProgram[] programs = new MTSProgram[numPrograms];
		for (int i = 0; i < numPrograms; i++)
		{
			int programDataSize = buf.getInt();
			programs[i] = MTSProgram.parse(NIOUtils.read(buf, programDataSize));
		}
		MTSIndex result = new MTSIndex(programs);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 66, 109, 104, 104 })]
	public virtual ByteBuffer serialize()
	{
		ByteBuffer bb = ByteBuffer.allocate(estimateSize());
		serializeTo(bb);
		bb.flip();
		return bb;
	}
}
