using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.platform;

namespace org.jcodec.common.io;

public class IOUtils : Object
{
	public const int DEFAULT_BUFFER_SIZE = 4096;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 100, 130, 153, 35, 130 })]
	public static void closeQuietly(Closeable c)
	{
		if (c != null)
		{
			IOException ex;
			try
			{
				c.close();
				return;
			}
			catch (IOException x)
			{
				ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
			}
			IOException ex2 = ex;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 162, 99, 131, 104, 104, 149, 103, 42,
		131, 99
	})]
	public static void copyFile(File src, File dst)
	{
		FileChannelWrapper _in = null;
		FileChannelWrapper @out = null;
		try
		{
			_in = NIOUtils.readableChannel(src);
			@out = NIOUtils.writableChannel(dst);
			NIOUtils.copy(_in, @out, long.MaxValue);
		}
		finally
		{
			NIOUtils.closeQuietly(_in);
			NIOUtils.closeQuietly(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 162, 108, 99, 99, 109, 106, 135 })]
	public static int copy(InputStream input, OutputStream output)
	{
		byte[] buffer = new byte[4096];
		int count = 0;
		int i = 0;
		while (-1 != (i = input.read(buffer)))
		{
			output.write(buffer, 0, i);
			count += i;
		}
		return count;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 134, 98, 103, 105 })]
	public static byte[] toByteArray(InputStream input)
	{
		ByteArrayOutputStream output = new ByteArrayOutputStream();
		copy(input, output);
		byte[] result = output.toByteArray();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public IOUtils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 130, 130, 99, 99, 108, 104, 135 })]
	public static int copyDumb(InputStream input, OutputStream output)
	{
		int count = 0;
		int i = 0;
		while (-1 != (i = input.read()))
		{
			output.write(i);
			count++;
		}
		return count;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(60)]
	public static byte[] readFileToByteArray(File file)
	{
		byte[] result = NIOUtils.toArray(NIOUtils.fetchFromFile(file));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(64)]
	public static string readToString(InputStream @is)
	{
		string result = Platform.stringFromBytes(toByteArray(@is));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 66, 116 })]
	public static void writeStringToFile(File file, string str)
	{
		NIOUtils.writeTo(ByteBuffer.wrap(String.instancehelper_getBytes(str)), file);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 124, 66, 105, 108, 159, 7, 173, 169, 105,
		124, 205
	})]
	public static void forceMkdir(File directory)
	{
		if (directory.exists())
		{
			if (!directory.isDirectory())
			{
				string message2 = new StringBuilder().append("File ").append(directory).append(" exists and is not a directory. Unable to create directory.")
					.toString();
				
				throw new IOException(message2);
			}
		}
		else if (!directory.mkdirs() && !directory.isDirectory())
		{
			string message = new StringBuilder().append("Unable to create directory ").append(directory).toString();
			
			throw new IOException(message);
		}
	}
}
