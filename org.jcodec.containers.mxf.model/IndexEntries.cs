using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mxf.model;

public class IndexEntries : Object
{
	private byte[] displayOff;

	private byte[] flags;

	private long[] fileOff;

	private byte[] keyFrameOff;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104, 104, 104, 105 })]
	public IndexEntries(byte[] displayOff, byte[] keyFrameOff, byte[] flags, long[] fileOff)
	{
		this.displayOff = displayOff;
		this.keyFrameOff = keyFrameOff;
		this.flags = flags;
		this.fileOff = fileOff;
	}

	[LineNumberTable(28)]
	public virtual byte[] getDisplayOff()
	{
		return displayOff;
	}

	[LineNumberTable(32)]
	public virtual byte[] getFlags()
	{
		return flags;
	}

	[LineNumberTable(36)]
	public virtual long[] getFileOff()
	{
		return fileOff;
	}

	[LineNumberTable(40)]
	public virtual byte[] getKeyFrameOff()
	{
		return keyFrameOff;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 109, 104, 136, 104, 104, 105, 137,
		105, 111, 109, 108, 108, 236, 59, 233, 71, 105,
		105, 105, 105, 108, 227, 61, 41, 233, 73
	})]
	public static IndexEntries read(ByteBuffer bb)
	{
		bb.order(ByteOrder.BIG_ENDIAN);
		int l = bb.getInt();
		int len = bb.getInt();
		int[] temporalOff = new int[l];
		byte[] flags = new byte[l];
		long[] fileOff = new long[l];
		byte[] keyFrameOff = new byte[l];
		for (int j = 0; j < l; j++)
		{
			temporalOff[j] = j + (sbyte)bb.get();
			keyFrameOff[j] = (byte)(sbyte)bb.get();
			flags[j] = (byte)(sbyte)bb.get();
			fileOff[j] = bb.getLong();
			NIOUtils.skip(bb, len - 11);
		}
		byte[] displayOff = new byte[l];
		for (int i = 0; i < l; i++)
		{
			for (int k = 0; k < l; k++)
			{
				if (temporalOff[k] == i)
				{
					displayOff[i] = (byte)(sbyte)(k - i);
					break;
				}
			}
		}
		IndexEntries result = new IndexEntries(displayOff, keyFrameOff, flags, fileOff);
		
		return result;
	}
}
