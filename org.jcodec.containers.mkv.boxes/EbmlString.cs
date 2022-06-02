using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlString : EbmlBin
{
	public string charset;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 234, 61, 204 })]
	public EbmlString(byte[] id)
		: base(id)
	{
		charset = "UTF-8";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 191, 11, 3, 98, 135 })]
	public virtual void setString(string value)
	{
		UnsupportedEncodingException ex2;
		try
		{
			data = ByteBuffer.wrap(String.instancehelper_getBytes(value, charset));
			return;
		}
		catch (UnsupportedEncodingException x)
		{
			ex2 = ByteCodeHelper.MapException<UnsupportedEncodingException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		UnsupportedEncodingException ex = ex2;
		Throwable.instancehelper_printStackTrace(ex);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 104, 104 })]
	public static EbmlString createEbmlString(byte[] id, string value)
	{
		EbmlString e = new EbmlString(id);
		e.setString(value);
		return e;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 127, 14, 98, 103 })]
	public virtual string getString()
	{
		//Discarded unreachable code: IL_001a
		UnsupportedEncodingException ex2;
		try
		{
			return String.newhelper(data.array(), charset);
		}
		catch (UnsupportedEncodingException x)
		{
			ex2 = ByteCodeHelper.MapException<UnsupportedEncodingException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		UnsupportedEncodingException ex = ex2;
		Throwable.instancehelper_printStackTrace(ex);
		return "";
	}
}
