using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using IKVM.Attributes;
using java.io;
using java.lang;

namespace net.sourceforge.jaad.aac;

[Serializable]
public class AACException : IOException
{
	private bool eos;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 106 })]
	public AACException(string message)
		: base(message)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 108, 104 })]
	public static AACException endOfStream()
	{
		AACException ex = new AACException("end of stream");
		ex.eos = true;
		return ex;
	}

	[LineNumberTable(29)]
	public virtual bool isEndOfStream()
	{
		return eos;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 108, 136, 108, 143 })]
	public static AACException wrap(java.lang.Exception e)
	{
		if (e != null && e is AACException)
		{
			return (AACException)e;
		}
		if (e != null && Throwable.instancehelper_getMessage(e) != null)
		{
			AACException result = new AACException(Throwable.instancehelper_getMessage(e));
			return result;
		}
		AACException result2 = new AACException(new StringBuilder().append("").append(e).toString());
		return result2;
	}

	[HideFromJava]
	[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
	protected AACException(SerializationInfo P_0, StreamingContext P_1)
		: base(P_0, P_1)
	{
	}
}
