using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using net.sourceforge.jaad.aac.tools;

namespace net.sourceforge.jaad.aac.syntax;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants" })]
public class CPE : Element, SyntaxConstants
{
	private MSMask msMask;

	private bool[] msUsed;

	private bool commonWindow;

	internal ICStream icsL;

	internal ICStream icsR;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 119, 162, 103, 102, 145 })]
	public static MSMask msMaskFromInt(int i)
	{
		MSMask[] values = MSMask.values();
		if (i >= (nint)values.LongLength)
		{
			
			throw new AACException("unknown MS mask type");
		}
		return values[i];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 105, 113, 109, 109 })]
	internal CPE(int frameLength)
	{
		msUsed = new bool[128];
		icsL = new ICStream(frameLength);
		icsR = new ICStream(frameLength);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 104, 104, 158, 136, 109, 109, 108,
		111, 146, 115, 115, 104, 137, 108, 48, 169, 99,
		127, 2, 127, 2, 177, 108, 173, 113, 191, 8,
		116, 118
	})]
	internal virtual void decode(IBitStream _in, AACDecoderConfig conf)
	{
		Profile profile = conf.getProfile();
		SampleFrequency sf = conf.getSampleFrequency();
		if (Object.instancehelper_equals(sf, SampleFrequency.___003C_003ESAMPLE_FREQUENCY_NONE))
		{
			
			throw new AACException("invalid sample frequency");
		}
		readElementInstanceTag(_in);
		commonWindow = _in.readBool();
		ICSInfo info = icsL.getInfo();
		if (commonWindow)
		{
			info.decode(_in, conf, commonWindow);
			icsR.getInfo().setData(info);
			msMask = msMaskFromInt(_in.readBits(2));
			if (msMask.equals(MSMask.___003C_003ETYPE_USED))
			{
				int maxSFB = info.getMaxSFB();
				int windowGroupCount = info.getWindowGroupCount();
				for (int idx = 0; idx < windowGroupCount * maxSFB; idx++)
				{
					msUsed[idx] = _in.readBool();
				}
			}
			else if (msMask.equals(MSMask.___003C_003ETYPE_ALL_1))
			{
				Arrays.fill(msUsed, val: true);
			}
			else
			{
				if (!msMask.equals(MSMask.___003C_003ETYPE_ALL_0))
				{
					
					throw new AACException("reserved MS mask type used");
				}
				Arrays.fill(msUsed, val: false);
			}
		}
		else
		{
			msMask = MSMask.___003C_003ETYPE_ALL_0;
			Arrays.fill(msUsed, val: false);
		}
		if (profile.isErrorResilientProfile() && info.isLTPrediction1Present())
		{
			int num = (_in.readBool() ? 1 : 0);
			ICSInfo iCSInfo = info;
			iCSInfo.ltpData2Present = (byte)num != 0;
			if (num != 0)
			{
				info.getLTPrediction2().decode(_in, info, profile);
			}
		}
		icsL.decode(_in, commonWindow, conf);
		icsR.decode(_in, commonWindow, conf);
	}

	[LineNumberTable(71)]
	public virtual ICStream getLeftChannel()
	{
		return icsL;
	}

	[LineNumberTable(75)]
	public virtual ICStream getRightChannel()
	{
		return icsR;
	}

	[LineNumberTable(79)]
	public virtual MSMask getMSMask()
	{
		return msMask;
	}

	[LineNumberTable(83)]
	public virtual bool isMSUsed(int off)
	{
		return msUsed[off];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(87)]
	public virtual bool isMSMaskPresent()
	{
		return (!msMask.equals(MSMask.___003C_003ETYPE_ALL_0)) ? true : false;
	}

	[LineNumberTable(91)]
	public virtual bool isCommonWindow()
	{
		return commonWindow;
	}
}
