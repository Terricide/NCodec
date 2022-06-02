using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.codecs.common.biari;

public class BitIO : Object
{
	[SpecialName]
	[EnclosingMethod(null, "outputFromArray", "([B)Lorg.jcodec.codecs.common.biari.BitIO$OutputBits;")]
	internal sealed class _1 : BaseOutputStream
	{
		internal int ptr;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal byte[] val_0024bytes;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(44)]
		internal _1(byte[] barr)
		{
			val_0024bytes = barr;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 130, 66, 112, 113, 125 })]
		protected internal override void writeByte(int b)
		{
			if (ptr >= (nint)val_0024bytes.LongLength)
			{
				
				throw new IOException("Buffer is full");
			}
			byte[] array = val_0024bytes;
			int num = ptr;
			ptr = num + 1;
			array[num] = (byte)(sbyte)b;
		}
	}

	public interface InputBits
	{
		[Throws(new string[] { "java.io.IOException" })]
		int getBit();
	}

	public interface OutputBits
	{
		[Throws(new string[] { "java.io.IOException" })]
		void putBit(int i);

		[Throws(new string[] { "java.io.IOException" })]
		void flush();
	}

	public class StreamInputBits : Object, InputBits
	{
		private InputStream _in;

		private int cur;

		private int bit;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 162, 105, 104, 104 })]
		public StreamInputBits(InputStream _in)
		{
			this._in = _in;
			bit = 8;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 119, 98, 106, 114, 106, 99, 136 })]
		public virtual int getBit()
		{
			if (bit > 7)
			{
				cur = _in.read();
				if (cur == -1)
				{
					return -1;
				}
				bit = 0;
			}
			int num = cur;
			int num2 = bit;
			bit = num2 + 1;
			return (num >> 7 - num2) & 1;
		}
	}

	public class StreamOutputBits : Object, OutputBits
	{
		private OutputStream @out;

		private int cur;

		private int bit;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 115, 66, 105, 104 })]
		public StreamOutputBits(OutputStream @out)
		{
			this.@out = @out;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 114, 98, 106, 114, 104, 136, 127, 11 })]
		public virtual void putBit(int symbol)
		{
			if (bit > 7)
			{
				@out.write(cur);
				cur = 0;
				bit = 0;
			}
			int num = cur;
			int num2 = symbol & 1;
			int num3 = bit;
			bit = num3 + 1;
			cur = num | (num2 << 7 - num3);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 112, 130, 106, 116 })]
		public virtual void flush()
		{
			if (bit > 0)
			{
				@out.write(cur);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(44)]
	public static OutputBits outputFromArray(byte[] bytes)
	{
		StreamOutputBits result = new StreamOutputBits(new _1(bytes));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(40)]
	public static InputBits inputFromArray(byte[] bytes)
	{
		StreamInputBits result = new StreamInputBits(new ByteArrayInputStream(bytes));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(18)]
	public BitIO()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public static InputBits inputFromStream(InputStream @is)
	{
		StreamInputBits result = new StreamInputBits(@is);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(36)]
	public static OutputBits outputFromStream(OutputStream @out)
	{
		StreamOutputBits result = new StreamOutputBits(@out);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 109, 136, 104, 101, 8, 249, 69,
		35, 163
	})]
	public static byte[] compressBits(int[] decompressed)
	{
		byte[] compressed = new byte[((nint)decompressed.LongLength >> 3) + 1];
		OutputBits @out = outputFromArray(compressed);
		IOException ex;
		try
		{
			for (int i = 0; i < (nint)decompressed.LongLength; i++)
			{
				int bit = decompressed[i];
				@out.putBit(bit);
			}
			return compressed;
		}
		catch (IOException x)
		{
			ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		IOException ex2 = ex;
		return compressed;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 130, 107, 168, 110, 37, 217, 35, 131 })]
	public static int[] decompressBits(byte[] compressed)
	{
		int[] decompressed = new int[(nint)compressed.LongLength << 3];
		InputBits inputFromArray = BitIO.inputFromArray(compressed);
		IOException ex;
		try
		{
			int i = 0;
			int read;
			while ((read = inputFromArray.getBit()) != -1)
			{
				decompressed[i] = read;
				i++;
			}
			return decompressed;
		}
		catch (IOException x)
		{
			ex = ByteCodeHelper.MapException<IOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		IOException ex2 = ex;
		return decompressed;
	}
}
