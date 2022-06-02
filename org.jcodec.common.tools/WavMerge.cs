using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using org.jcodec.codecs.wav;
using org.jcodec.common.io;

namespace org.jcodec.common.tools;

public class WavMerge : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 162, 99, 105, 105, 137, 100, 109, 110,
		108, 118, 113, 111, 103, 251, 57, 236, 73, 157,
		105, 104, 137, 100, 100, 109, 103, 107, 113, 106,
		136, 100, 235, 56, 236, 75, 101, 99, 105, 113,
		105, 177, 103, 121, 40, 44, 103, 121, 40, 169,
		99
	})]
	public static void merge(File result, File[] src)
	{
		FileChannelWrapper @out = null;
		ReadableByteChannel[] inputs = new ReadableByteChannel[(nint)src.LongLength];
		WavHeader[] headers = new WavHeader[(nint)src.LongLength];
		ByteBuffer[] ins = new ByteBuffer[(nint)src.LongLength];
		try
		{
			int sampleSize = -1;
			for (int j = 0; j < (nint)src.LongLength; j++)
			{
				inputs[j] = NIOUtils.readableChannel(src[j]);
				WavHeader hdr = WavHeader.readChannel(inputs[j]);
				if (sampleSize != -1 && sampleSize != hdr.fmt.bitsPerSample)
				{
					
					throw new RuntimeException("Input files have different sample sizes");
				}
				sampleSize = hdr.fmt.bitsPerSample;
				headers[j] = hdr;
				ins[j] = ByteBuffer.allocate(hdr.getFormat().framesToBytes(4096));
			}
			ByteBuffer outb = ByteBuffer.allocate((int)(headers[0].getFormat().framesToBytes(4096) * (nint)src.LongLength));
			WavHeader newHeader = WavHeader.multiChannelWav(headers);
			@out = NIOUtils.writableChannel(result);
			newHeader.write(@out);
			int readOnce = 1;
			while (true)
			{
				readOnce = 0;
				for (int i = 0; i < (nint)ins.LongLength; i++)
				{
					if (inputs[i] != null)
					{
						ins[i].clear();
						if (inputs[i].read(ins[i]) == -1)
						{
							NIOUtils.closeQuietly(inputs[i]);
							inputs[i] = null;
						}
						else
						{
							readOnce = 1;
						}
						ins[i].flip();
					}
				}
				if (readOnce == 0)
				{
					break;
				}
				outb.clear();
				AudioUtil.interleave(headers[0].getFormat(), ins, outb);
				outb.flip();
				((WritableByteChannel)@out).write(outb);
			}
		}
		catch
		{
			//try-fault
			IOUtils.closeQuietly(@out);
			ReadableByteChannel[] array = inputs;
			int num = array.Length;
			for (int k = 0; k < num; k++)
			{
				ReadableByteChannel inputStream2 = array[k];
				IOUtils.closeQuietly(inputStream2);
			}
			throw;
		}
		IOUtils.closeQuietly(@out);
		ReadableByteChannel[] array2 = inputs;
		int num2 = array2.Length;
		for (int l = 0; l < num2; l++)
		{
			ReadableByteChannel inputStream = array2[l];
			IOUtils.closeQuietly(inputStream);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(23)]
	public WavMerge()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 102, 112, 135, 111, 107, 104, 51,
		135, 106
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 3)
		{
			java.lang.System.@out.println("wavmerge <output wav> <input wav> .... <input wav>");
			java.lang.System.exit(-1);
		}
		
		File @out = new File(args[0]);
		File[] ins = new File[(nint)args.LongLength - 1];
		for (int i = 1; i < (nint)args.LongLength; i++)
		{
			int num = i - 1;
			
			ins[num] = new File(args[i]);
		}
		merge(@out, ins);
	}
}
