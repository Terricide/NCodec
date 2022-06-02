using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.lang.reflect;
using java.util;
using org.jcodec.platform;

namespace org.jcodec.containers.flv;

public class FLVMetadata : java.lang.Object
{
	private sealed class ___003CCallerID_003E : CallerID
	{
		internal ___003CCallerID_003E()
		{
		}
	}

	private double duration;

	private double width;

	private double height;

	private double framerate;

	private string audiocodecid;

	private double videokeyframe_frequency;

	private string videodevice;

	private double avclevel;

	private double audiosamplerate;

	private double audiochannels;

	private string presetname;

	private double videodatarate;

	private double audioinputvolume;

	private Date creationdate;

	private string videocodecid;

	private double avcprofile;

	private string audiodevice;

	private double audiodatarate;

	[SpecialName]
	private static CallerID ___003CcallerID_003E;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 98, 105, 109, 107, 101, 143, 106, 123,
		106, 155, 191, 9, 35, 227, 53, 234, 78
	})]
	public FLVMetadata(Map md)
	{
		Field[] declaredFields = Platform.getDeclaredFields(java.lang.Object.instancehelper_getClass(this));
		for (int i = 0; i < (nint)declaredFields.LongLength; i++)
		{
			Field field = declaredFields[i];
			object @object = md.get(field.getName());
			java.lang.Exception ex2;
			try
			{
				if (@object is java.lang.Double)
				{
					field.setDouble(this, ((java.lang.Double)@object).doubleValue(), FLVMetadata.___003CGetCallerID_003E());
				}
				else if (@object is java.lang.Boolean)
				{
					field.setBoolean(this, ((java.lang.Boolean)@object).booleanValue(), FLVMetadata.___003CGetCallerID_003E());
				}
				else
				{
					field.set(this, @object, FLVMetadata.___003CGetCallerID_003E());
				}
			}
			catch (System.Exception x)
			{
				java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
				if (ex == null)
				{
					throw;
				}
				ex2 = ex;
				goto IL_00a4;
			}
			continue;
			IL_00a4:
			java.lang.Exception ex3 = ex2;
		}
	}

	[LineNumberTable(60)]
	public virtual double getDuration()
	{
		return duration;
	}

	[LineNumberTable(64)]
	public virtual double getWidth()
	{
		return width;
	}

	[LineNumberTable(68)]
	public virtual double getHeight()
	{
		return height;
	}

	[LineNumberTable(72)]
	public virtual double getFramerate()
	{
		return framerate;
	}

	[LineNumberTable(76)]
	public virtual string getAudiocodecid()
	{
		return audiocodecid;
	}

	[LineNumberTable(80)]
	public virtual double getVideokeyframe_frequency()
	{
		return videokeyframe_frequency;
	}

	[LineNumberTable(84)]
	public virtual string getVideodevice()
	{
		return videodevice;
	}

	[LineNumberTable(88)]
	public virtual double getAvclevel()
	{
		return avclevel;
	}

	[LineNumberTable(92)]
	public virtual double getAudiosamplerate()
	{
		return audiosamplerate;
	}

	[LineNumberTable(96)]
	public virtual double getAudiochannels()
	{
		return audiochannels;
	}

	[LineNumberTable(100)]
	public virtual string getPresetname()
	{
		return presetname;
	}

	[LineNumberTable(104)]
	public virtual double getVideodatarate()
	{
		return videodatarate;
	}

	[LineNumberTable(108)]
	public virtual double getAudioinputvolume()
	{
		return audioinputvolume;
	}

	[LineNumberTable(112)]
	public virtual Date getCreationdate()
	{
		return creationdate;
	}

	[LineNumberTable(116)]
	public virtual string getVideocodecid()
	{
		return videocodecid;
	}

	[LineNumberTable(120)]
	public virtual double getAvcprofile()
	{
		return avcprofile;
	}

	[LineNumberTable(124)]
	public virtual string getAudiodevice()
	{
		return audiodevice;
	}

	[LineNumberTable(128)]
	public virtual double getAudiodatarate()
	{
		return audiodatarate;
	}

	static CallerID ___003CGetCallerID_003E()
	{
		if (___003CcallerID_003E == null)
		{
			___003CcallerID_003E = new ___003CCallerID_003E();
		}
		return ___003CcallerID_003E;
	}
}
