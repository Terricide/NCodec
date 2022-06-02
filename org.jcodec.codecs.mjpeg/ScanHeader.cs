using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.mjpeg;

public class ScanHeader : Object
{
	public class Component : Object
	{
		internal int cs;

		internal int td;

		internal int ta;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(35)]
		public Component()
		{
		}
	}

	internal int ls;

	internal int ns;

	internal Component[] components;

	internal int ss;

	internal int se;

	internal int ah;

	internal int al;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 130, 103, 115, 116, 114, 112, 121, 117,
		112, 114, 237, 59, 234, 71, 116, 116, 112, 113,
		108
	})]
	public static ScanHeader read(ByteBuffer bb)
	{
		ScanHeader scan = new ScanHeader();
		scan.ls = bb.getShort() & 0xFFFF;
		scan.ns = (sbyte)bb.get() & 0xFF;
		scan.components = new Component[scan.ns];
		for (int i = 0; i < (nint)scan.components.LongLength; i++)
		{
			Component[] array = scan.components;
			int num = i;
			Component component = new Component();
			int num2 = num;
			Component[] array2 = array;
			array2[num2] = component;
			Component c = component;
			c.cs = (sbyte)bb.get() & 0xFF;
			int tdta = (sbyte)bb.get() & 0xFF;
			c.td = (int)((uint)(tdta & 0xF0) >> 4);
			c.ta = tdta & 0xF;
		}
		scan.ss = (sbyte)bb.get() & 0xFF;
		scan.se = (sbyte)bb.get() & 0xFF;
		int ahal = (sbyte)bb.get() & 0xFF;
		scan.ah = (int)((uint)(ahal & 0xF0) >> 4);
		scan.al = ahal & 0xF;
		return scan;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public ScanHeader()
	{
	}

	[LineNumberTable(32)]
	public virtual bool isInterleaved()
	{
		return ns > 1;
	}
}
