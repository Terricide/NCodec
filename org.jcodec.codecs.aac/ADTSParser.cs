using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac;

public class ADTSParser : Object
{
	public class Header : Object
	{
		private int objectType;

		private int chanConfig;

		private int crcAbsent;

		private int numAACFrames;

		private int samplingIndex;

		private int samples;

		private int size;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 133, 66, 105, 104, 104, 104, 105, 105, 105 })]
		public Header(int object_type, int chanConfig, int crcAbsent, int numAACFrames, int samplingIndex, int size)
		{
			objectType = object_type;
			this.chanConfig = chanConfig;
			this.crcAbsent = crcAbsent;
			this.numAACFrames = numAACFrames;
			this.samplingIndex = samplingIndex;
			this.size = size;
		}

		[LineNumberTable(46)]
		public virtual int getObjectType()
		{
			return objectType;
		}

		[LineNumberTable(50)]
		public virtual int getChanConfig()
		{
			return chanConfig;
		}

		[LineNumberTable(54)]
		public virtual int getCrcAbsent()
		{
			return crcAbsent;
		}

		[LineNumberTable(58)]
		public virtual int getNumAACFrames()
		{
			return numAACFrames;
		}

		[LineNumberTable(62)]
		public virtual int getSamplingIndex()
		{
			return samplingIndex;
		}

		[LineNumberTable(66)]
		public virtual int getSamples()
		{
			return samples;
		}

		[LineNumberTable(70)]
		public virtual int getSize()
		{
			return size;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(74)]
		public virtual int getSampleRate()
		{
			return AACConts.AAC_SAMPLE_RATES[samplingIndex];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 104, 200, 112, 163, 104, 105, 105,
		106, 106, 105, 138, 105, 169, 105, 105, 107, 102,
		131, 107, 106, 135, 142
	})]
	public static Header read(ByteBuffer data)
	{
		ByteBuffer dup = data.duplicate();
		BitReader br = BitReader.createBitReader(dup);
		if (br.readNBit(12) != 4095)
		{
			return null;
		}
		int id = br.read1Bit();
		int layer = br.readNBit(2);
		int crc_abs = br.read1Bit();
		int aot = br.readNBit(2);
		int sr = br.readNBit(4);
		int pb = br.read1Bit();
		int ch = br.readNBit(3);
		int origCopy = br.read1Bit();
		int home = br.read1Bit();
		int copy = br.read1Bit();
		int copyStart = br.read1Bit();
		int size = br.readNBit(13);
		if (size < 7)
		{
			return null;
		}
		int buffer = br.readNBit(11);
		int rdb = br.readNBit(2);
		br.stop();
		data.position(dup.position());
		Header result = new Header(aot + 1, ch, crc_abs, rdb + 1, sr, size);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 104, 104, 110, 110, 110, 103, 104 })]
	public static ByteBuffer adtsToStreamInfo(Header hdr)
	{
		ByteBuffer si = ByteBuffer.allocate(2);
		BitWriter wr = new BitWriter(si);
		wr.writeNBit(hdr.getObjectType(), 5);
		wr.writeNBit(hdr.getSamplingIndex(), 4);
		wr.writeNBit(hdr.getChanConfig(), 4);
		wr.flush();
		si.clear();
		return si;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public ADTSParser()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 113, 66, 104, 200, 142, 104, 105, 109, 112,
		110, 104, 142, 104, 168, 104, 104, 143, 106, 112,
		135, 104
	})]
	public static ByteBuffer write(Header header, ByteBuffer buf)
	{
		ByteBuffer data = buf.duplicate();
		BitWriter br = new BitWriter(data);
		br.writeNBit(4095, 12);
		br.write1Bit(1);
		br.writeNBit(0, 2);
		br.write1Bit(header.getCrcAbsent());
		br.writeNBit(header.getObjectType() - 1, 2);
		br.writeNBit(header.getSamplingIndex(), 4);
		br.write1Bit(0);
		br.writeNBit(header.getChanConfig(), 3);
		br.write1Bit(0);
		br.write1Bit(0);
		br.write1Bit(0);
		br.write1Bit(0);
		br.writeNBit(header.getSize(), 13);
		br.writeNBit(0, 11);
		br.writeNBit(header.getNumAACFrames() - 1, 2);
		br.flush();
		data.flip();
		return data;
	}
}
