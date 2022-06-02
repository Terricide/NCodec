using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class MetaValue : Object
{
	public const int TYPE_STRING_UTF16 = 2;

	public const int TYPE_STRING_UTF8 = 1;

	public const int TYPE_FLOAT_64 = 24;

	public const int TYPE_FLOAT_32 = 23;

	public const int TYPE_INT_32 = 67;

	public const int TYPE_INT_16 = 66;

	public const int TYPE_INT_8 = 65;

	public const int TYPE_INT_V = 22;

	public const int TYPE_UINT_V = 21;

	public const int TYPE_JPEG = 13;

	public const int TYPE_PNG = 13;

	public const int TYPE_BMP = 27;

	private int type;

	private int locale;

	private byte[] data;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public static MetaValue createOtherWithLocale(int type, int locale, byte[] data)
	{
		MetaValue result = new MetaValue(type, locale, data);
		
		return result;
	}

	[LineNumberTable(123)]
	public virtual int getType()
	{
		return type;
	}

	[LineNumberTable(127)]
	public virtual int getLocale()
	{
		return locale;
	}

	[LineNumberTable(131)]
	public virtual byte[] getData()
	{
		return data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 162, 104, 104, 109, 105 })]
	private static byte[] fromInt(int value)
	{
		byte[] bytes = new byte[4];
		ByteBuffer bb = ByteBuffer.wrap(bytes);
		bb.order(ByteOrder.BIG_ENDIAN);
		bb.putInt(value);
		return bytes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 105, 104, 104, 104 })]
	private MetaValue(int type, int locale, byte[] data)
	{
		this.type = type;
		this.locale = locale;
		this.data = data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 162, 104, 104, 109, 106 })]
	private static byte[] fromFloat(float floatValue)
	{
		byte[] bytes = new byte[4];
		ByteBuffer bb = ByteBuffer.wrap(bytes);
		bb.order(ByteOrder.BIG_ENDIAN);
		bb.putFloat(floatValue);
		return bytes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 162, 104, 109 })]
	private int toInt16(byte[] data)
	{
		ByteBuffer bb = ByteBuffer.wrap(data);
		bb.order(ByteOrder.BIG_ENDIAN);
		short @short = bb.getShort();
		
		return @short;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 98, 104, 109 })]
	private int toInt24(byte[] data)
	{
		ByteBuffer bb = ByteBuffer.wrap(data);
		bb.order(ByteOrder.BIG_ENDIAN);
		return ((bb.getShort() & 0xFFFF) << 8) | ((sbyte)bb.get() & 0xFF);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 162, 104, 109 })]
	private int toInt32(byte[] data)
	{
		ByteBuffer bb = ByteBuffer.wrap(data);
		bb.order(ByteOrder.BIG_ENDIAN);
		int @int = bb.getInt();
		
		return @int;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 100, 98, 104, 109 })]
	private float toFloat(byte[] data)
	{
		ByteBuffer bb = ByteBuffer.wrap(data);
		bb.order(ByteOrder.BIG_ENDIAN);
		float @float = bb.getFloat();
		
		return @float;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 162, 104, 109 })]
	private double toDouble(byte[] data)
	{
		ByteBuffer bb = ByteBuffer.wrap(data);
		bb.order(ByteOrder.BIG_ENDIAN);
		double @double = bb.getDouble();
		
		return @double;
	}

	[LineNumberTable(100)]
	public virtual bool isInt()
	{
		return (type == 21 || type == 22 || type == 65 || type == 66 || type == 67) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 117, 159, 2, 138, 144, 144, 176,
		107, 106, 107, 112, 107, 112
	})]
	public virtual int getInt()
	{
		if (type == 21 || type == 22)
		{
			switch ((nint)data.LongLength - 1)
			{
			case 0:
				return data[0];
			case 1:
			{
				int result3 = toInt16(data);
				
				return result3;
			}
			case 2:
			{
				int result2 = toInt24(data);
				
				return result2;
			}
			case 3:
			{
				int result = toInt32(data);
				
				return result;
			}
			}
		}
		if (type == 65)
		{
			return data[0];
		}
		if (type == 66)
		{
			int result4 = toInt16(data);
			
			return result4;
		}
		if (type == 67)
		{
			int result5 = toInt32(data);
			
			return result5;
		}
		return 0;
	}

	[LineNumberTable(108)]
	public virtual bool isFloat()
	{
		return (type == 23 || type == 24) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 162, 107, 111, 107, 112 })]
	public virtual double getFloat()
	{
		if (type == 23)
		{
			return toFloat(data);
		}
		if (type == 24)
		{
			double result = toDouble(data);
			
			return result;
		}
		return 0.0;
	}

	[LineNumberTable(104)]
	public virtual bool isString()
	{
		return (type == 1 || type == 2) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 162, 106, 116, 106, 148 })]
	public virtual string getString()
	{
		if (type == 1)
		{
			string result = Platform.stringFromCharset(data, "UTF-8");
			
			return result;
		}
		if (type == 2)
		{
			string result2 = Platform.stringFromCharset(data, "UTF-16BE");
			
			return result2;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	public static MetaValue createInt(int value)
	{
		MetaValue result = new MetaValue(21, 0, fromInt(value));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(45)]
	public static MetaValue createFloat(float value)
	{
		MetaValue result = new MetaValue(23, 0, fromFloat(value));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(49)]
	public static MetaValue createString(string value)
	{
		MetaValue result = new MetaValue(1, 0, Platform.getBytesForCharset(value, "UTF-8"));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(53)]
	public static MetaValue createOther(int type, byte[] data)
	{
		MetaValue result = new MetaValue(type, 0, data);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 66, 105, 111, 105, 111, 105, 143 })]
	public override string toString()
	{
		if (isInt())
		{
			string result = String.valueOf(getInt());
			
			return result;
		}
		if (isFloat())
		{
			string result2 = String.valueOf(getFloat());
			
			return result2;
		}
		if (isString())
		{
			string result3 = String.valueOf(getString());
			
			return result3;
		}
		return "BLOB";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(181)]
	public virtual bool isBlob()
	{
		return (!isFloat() && !isInt() && !isString()) ? true : false;
	}
}
