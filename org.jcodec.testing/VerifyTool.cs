using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.codecs.h264;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.platform;

namespace org.jcodec.testing;

public class VerifyTool : java.lang.Object
{
	[SpecialName]
	[EnclosingMethod(null, "doIt", "(Ljava.lang.String;)V")]
	internal class _1 : java.lang.Object, FilenameFilter
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal VerifyTool this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(36)]
		internal _1(VerifyTool this_00240)
		{
			this.this_00240 = this_00240;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(39)]
		public virtual bool accept(File dir, string name)
		{
			bool result = java.lang.String.instancehelper_endsWith(name, ".264");
			
			return result;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(25)]
	public VerifyTool()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		133,
		66,
		243,
		70,
		116,
		127,
		11,
		153,
		109,
		127,
		12,
		105,
		139,
		223,
		32,
		3,
		99,
		byte.MaxValue,
		24,
		52,
		234,
		80
	})]
	private void doIt(string location)
	{
		File[] h264 = new File(location).listFiles(new _1(this));
		File[] array = h264;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			File coded = array[i];
			
			File @ref = new File(coded.getParentFile(), java.lang.String.instancehelper_replaceAll(coded.getName(), ".264$", "_dec.yuv"));
			if (!coded.exists() || !@ref.exists())
			{
				continue;
			}
			System.Exception ex;
			try
			{
				if (test(coded, @ref))
				{
					java.lang.System.@out.println(new StringBuilder().append(coded.getAbsolutePath()).append(" -- FIXED").toString());
					Platform.deleteFile(coded);
					Platform.deleteFile(@ref);
				}
				else
				{
					java.lang.System.@out.println(new StringBuilder().append(coded.getAbsolutePath()).append(" -- NOT FIXED!!!!").toString());
				}
			}
			catch (System.Exception x)
			{
				ex = ByteCodeHelper.MapException<System.Exception>(x, ByteCodeHelper.MapFlags.None);
				goto IL_00f2;
			}
			continue;
			IL_00f2:
			System.Exception t = ex;
			java.lang.System.@out.println(new StringBuilder().append(coded.getAbsolutePath()).append(" -- ERROR: ").append(Throwable.instancehelper_getMessage(t))
				.toString());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 109, 118, 135, 104, 111, 123, 106,
		106, 114, 103, 135, 145, 126, 99, 126, 99, 126,
		99, 102
	})]
	private bool test(File coded, File @ref)
	{
		BufferH264ES es = new BufferH264ES(NIOUtils.fetchFromFile(coded));
		Picture buf = Picture.create(1920, 1088, ColorSpace.___003C_003EYUV420);
		H264Decoder dec = new H264Decoder();
		ByteBuffer _yuv = NIOUtils.fetchFromFile(@ref);
		Packet nextFrame;
		while ((nextFrame = es.nextFrame()) != null)
		{
			var @out = dec.decodeFrame(nextFrame.getData(), buf.getData()).cropped();
			Picture pic = @out.createCompatible();
			pic.copyFrom(@out);
			int lumaSize = pic.getWidth() * pic.getHeight();
			int crSize = lumaSize >> 2;
			int cbSize = lumaSize >> 2;
			ByteBuffer yuv = NIOUtils.read(_yuv, lumaSize + crSize + cbSize);
			if (!Platform.arrayEqualsByte(ArrayUtil.toByteArrayShifted(JCodecUtil2.getAsIntArray(yuv, lumaSize)), pic.getPlaneData(0)))
			{
				return false;
			}
			if (!Platform.arrayEqualsByte(ArrayUtil.toByteArrayShifted(JCodecUtil2.getAsIntArray(yuv, crSize)), pic.getPlaneData(1)))
			{
				return false;
			}
			if (!Platform.arrayEqualsByte(ArrayUtil.toByteArrayShifted(JCodecUtil2.getAsIntArray(yuv, cbSize)), pic.getPlaneData(2)))
			{
				return false;
			}
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 66, 102, 114, 130, 112 })]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength != 1)
		{
			java.lang.System.@out.println("Syntax: <error folder location>");
		}
		else
		{
			new VerifyTool().doIt(args[0]);
		}
	}
}
