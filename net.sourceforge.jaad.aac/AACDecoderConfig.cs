using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac;

public class AACDecoderConfig : Object, SyntaxConstants
{
	private Profile profile;

	private Profile extProfile;

	private SampleFrequency sampleFrequency;

	private ChannelConfiguration channelConfiguration;

	private bool frameLengthFlag;

	private bool dependsOnCoreCoder;

	private int coreCoderDelay;

	private bool extensionFlag;

	private bool sbrPresent;

	private bool downSampledSBR;

	private bool sbrEnabled;

	private bool sectionDataResilience;

	private bool scalefactorResilience;

	private bool spectralDataResilience;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 105, 108, 108, 108, 108, 104, 104,
		104, 104, 104, 104, 104
	})]
	private AACDecoderConfig()
	{
		profile = Profile.___003C_003EAAC_MAIN;
		extProfile = Profile.___003C_003EUNKNOWN;
		sampleFrequency = SampleFrequency.___003C_003ESAMPLE_FREQUENCY_NONE;
		channelConfiguration = ChannelConfiguration.___003C_003ECHANNEL_CONFIG_UNSUPPORTED;
		frameLengthFlag = false;
		sbrPresent = false;
		downSampledSBR = false;
		sbrEnabled = true;
		sectionDataResilience = false;
		scalefactorResilience = false;
		spectralDataResilience = false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 90, 162, 105, 113 })]
	private static Profile readProfile(IBitStream _in)
	{
		int i = _in.readBits(5);
		if (i == 31)
		{
			i = 32 + _in.readBits(6);
		}
		Profile result = Profile.forInt(i);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 106, 147, 142, 110, 109, 105, 136,
		137, 118, 102, 241, 71
	})]
	private static void readSyncExtension(IBitStream _in, AACDecoderConfig config)
	{
		int type = _in.readBits(11);
		if (type != 695)
		{
			return;
		}
		Profile profile = Profile.forInt(_in.readBits(5));
		if (!Object.instancehelper_equals(profile, Profile.___003C_003EAAC_SBR))
		{
			return;
		}
		config.sbrPresent = _in.readBool();
		if (config.sbrPresent)
		{
			config.profile = profile;
			int tmp = _in.readBits(4);
			if (tmp == config.sampleFrequency.getIndex())
			{
				config.downSampledSBR = true;
			}
			if (tmp == 15)
			{
				
				throw new AACException("sample rate specified explicitly, not supported yet!");
			}
		}
	}

	[LineNumberTable(51)]
	public virtual ChannelConfiguration getChannelConfiguration()
	{
		return channelConfiguration;
	}

	[LineNumberTable(new byte[] { 159, 129, 162, 104 })]
	public virtual void setChannelConfiguration(ChannelConfiguration channelConfiguration)
	{
		this.channelConfiguration = channelConfiguration;
	}

	[LineNumberTable(59)]
	public virtual int getCoreCoderDelay()
	{
		return coreCoderDelay;
	}

	[LineNumberTable(new byte[] { 159, 127, 162, 104 })]
	public virtual void setCoreCoderDelay(int coreCoderDelay)
	{
		this.coreCoderDelay = coreCoderDelay;
	}

	[LineNumberTable(67)]
	public virtual bool isDependsOnCoreCoder()
	{
		return dependsOnCoreCoder;
	}

	[LineNumberTable(new byte[] { 159, 125, 161, 67, 104 })]
	public virtual void setDependsOnCoreCoder(bool dependsOnCoreCoder)
	{
		int dependsOnCoreCoder2 = ((this.dependsOnCoreCoder = dependsOnCoreCoder) ? 1 : 0);
	}

	[LineNumberTable(75)]
	public virtual Profile getExtObjectType()
	{
		return extProfile;
	}

	[LineNumberTable(new byte[] { 159, 123, 162, 104 })]
	public virtual void setExtObjectType(Profile extObjectType)
	{
		extProfile = extObjectType;
	}

	[LineNumberTable(83)]
	public virtual int getFrameLength()
	{
		return (!frameLengthFlag) ? 1024 : 960;
	}

	[LineNumberTable(87)]
	public virtual bool isSmallFrameUsed()
	{
		return frameLengthFlag;
	}

	[LineNumberTable(new byte[] { 159, 120, 161, 67, 104 })]
	public virtual void setSmallFrameUsed(bool shortFrame)
	{
		int shortFrame2 = ((frameLengthFlag = shortFrame) ? 1 : 0);
	}

	[LineNumberTable(95)]
	public virtual Profile getProfile()
	{
		return profile;
	}

	[LineNumberTable(new byte[] { 159, 118, 162, 104 })]
	public virtual void setProfile(Profile profile)
	{
		this.profile = profile;
	}

	[LineNumberTable(103)]
	public virtual SampleFrequency getSampleFrequency()
	{
		return sampleFrequency;
	}

	[LineNumberTable(new byte[] { 159, 116, 162, 104 })]
	public virtual void setSampleFrequency(SampleFrequency sampleFrequency)
	{
		this.sampleFrequency = sampleFrequency;
	}

	[LineNumberTable(112)]
	public virtual bool isSBRPresent()
	{
		return sbrPresent;
	}

	[LineNumberTable(116)]
	public virtual bool isSBRDownSampled()
	{
		return downSampledSBR;
	}

	[LineNumberTable(120)]
	public virtual bool isSBREnabled()
	{
		return sbrEnabled;
	}

	[LineNumberTable(new byte[] { 159, 111, 65, 67, 104 })]
	public virtual void setSBREnabled(bool enabled)
	{
		int enabled2 = ((sbrEnabled = enabled) ? 1 : 0);
	}

	[LineNumberTable(129)]
	public virtual bool isScalefactorResilienceUsed()
	{
		return scalefactorResilience;
	}

	[LineNumberTable(133)]
	public virtual bool isSectionDataResilienceUsed()
	{
		return sectionDataResilience;
	}

	[LineNumberTable(137)]
	public virtual bool isSpectralDataResilienceUsed()
	{
		return spectralDataResilience;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 104, 167, 141, 105, 123, 109, 147,
		104, 105, 104, 104, 201, 117, 109, 114, 159, 29,
		109, 121, 109, 121, 104, 141, 105, 105, 109, 109,
		173, 167, 142, 104, 104, 105, 110, 110, 179, 148,
		159, 12, 168, 74, 227, 61
	})]
	public static AACDecoderConfig parseMP4DecoderSpecificInfo(byte[] data)
	{
		BitStream _in = BitStream.createBitStream(data);
		AACDecoderConfig config = new AACDecoderConfig();
		try
		{
			config.profile = readProfile(_in);
			int sf = ((IBitStream)_in).readBits(4);
			if (sf == 15)
			{
				config.sampleFrequency = SampleFrequency.forFrequency(((IBitStream)_in).readBits(24));
			}
			else
			{
				config.sampleFrequency = SampleFrequency.forInt(sf);
			}
			config.channelConfiguration = ChannelConfiguration.forInt(((IBitStream)_in).readBits(4));
			Profile cp = config.profile;
			if (Profile.___003C_003EAAC_SBR == cp)
			{
				config.extProfile = cp;
				config.sbrPresent = true;
				sf = ((IBitStream)_in).readBits(4);
				config.downSampledSBR = config.sampleFrequency.getIndex() == sf;
				config.sampleFrequency = SampleFrequency.forInt(sf);
				config.profile = readProfile(_in);
			}
			else
			{
				if (Profile.___003C_003EAAC_MAIN != cp && Profile.___003C_003EAAC_LC != cp && Profile.___003C_003EAAC_SSR != cp && Profile.___003C_003EAAC_LTP != cp && Profile.___003C_003EER_AAC_LC != cp && Profile.___003C_003EER_AAC_LTP != cp && Profile.___003C_003EER_AAC_LD != cp)
				{
					string message = new StringBuilder().append("profile not supported: ").append(cp.getIndex()).toString();
					
					throw new AACException(message);
				}
				config.frameLengthFlag = ((IBitStream)_in).readBool();
				if (config.frameLengthFlag)
				{
					
					throw new AACException("config uses 960-sample frames, not yet supported");
				}
				config.dependsOnCoreCoder = ((IBitStream)_in).readBool();
				if (config.dependsOnCoreCoder)
				{
					config.coreCoderDelay = ((IBitStream)_in).readBits(14);
				}
				else
				{
					config.coreCoderDelay = 0;
				}
				config.extensionFlag = ((IBitStream)_in).readBool();
				if (config.extensionFlag)
				{
					if (cp.isErrorResilientProfile())
					{
						config.sectionDataResilience = ((IBitStream)_in).readBool();
						config.scalefactorResilience = ((IBitStream)_in).readBool();
						config.spectralDataResilience = ((IBitStream)_in).readBool();
					}
					((IBitStream)_in).skipBit();
				}
				if (config.channelConfiguration == ChannelConfiguration.___003C_003ECHANNEL_CONFIG_NONE)
				{
					((IBitStream)_in).skipBits(3);
					PCE pce = new PCE();
					pce.decode(_in);
					config.profile = pce.getProfile();
					config.sampleFrequency = pce.getSampleFrequency();
					config.channelConfiguration = ChannelConfiguration.forInt(pce.getChannelCount());
				}
				if (((IBitStream)_in).getBitsLeft() > 10)
				{
					readSyncExtension(_in, config);
				}
			}
			return config;
		}
		finally
		{
			((IBitStream)_in).destroy();
		}
	}
}
