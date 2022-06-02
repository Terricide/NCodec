using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.filterbank;
using net.sourceforge.jaad.aac.syntax;
using net.sourceforge.jaad.aac.transport;
using org.jcodec.common.logging;

namespace net.sourceforge.jaad.aac;

public class Decoder : java.lang.Object, SyntaxConstants
{
	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private AACDecoderConfig config;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private SyntacticElements syntacticElements;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private FilterBank filterBank;

	private IBitStream _in;

	private ADIFHeader adifHeader;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(37)]
	public static bool canDecode(Profile profile)
	{
		bool result = profile.isDecodingSupported();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 118, 162, 110, 114, 109, 114, 114, 183, 115,
		159, 22, 204, 146, 146, 223, 5, 227, 61, 98,
		113, 141
	})]
	private void decode(SampleBuffer buffer)
	{
		if (ADIFHeader.isPresent(_in))
		{
			adifHeader = ADIFHeader.readHeader(_in);
			PCE pce = adifHeader.getFirstPCE();
			config.setProfile(pce.getProfile());
			config.setSampleFrequency(pce.getSampleFrequency());
			config.setChannelConfiguration(ChannelConfiguration.forInt(pce.getChannelCount()));
		}
		if (!canDecode(config.getProfile()))
		{
			string message = new StringBuilder().append("unsupported profile: ").append(config.getProfile().getDescription()).toString();
			
			throw new AACException(message);
		}
		syntacticElements.startNewFrame();
		java.lang.Exception ex2;
		try
		{
			syntacticElements.decode(_in);
			syntacticElements.process(filterBank);
			syntacticElements.sendToOutput(buffer);
			return;
		}
		catch (System.Exception x)
		{
			java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		java.lang.Exception e = ex2;
		buffer.setData(new byte[0], 0, 0, 0, 0);
		throw AACException.wrap(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 105, 109, 105, 145, 115, 159, 22,
		114, 159, 8, 140, 127, 0, 127, 10, 127, 7
	})]
	public Decoder(byte[] decoderSpecificInfo)
	{
		config = AACDecoderConfig.parseMP4DecoderSpecificInfo(decoderSpecificInfo);
		if (config == null)
		{
			
			throw new IllegalArgumentException("illegal MP4 decoder specific info");
		}
		if (!canDecode(config.getProfile()))
		{
			string message = new StringBuilder().append("unsupported profile: ").append(config.getProfile().getDescription()).toString();
			
			throw new AACException(message);
		}
		syntacticElements = new SyntacticElements(config);
		filterBank = new FilterBank(config.isSmallFrameUsed(), config.getChannelConfiguration().getChannelCount());
		_in = new BitStream();
		Logger.debug("profile: {0}", config.getProfile());
		Logger.debug("sf: {0}", Integer.valueOf(config.getSampleFrequency().getFrequency()));
		Logger.debug("channels: {0}", config.getChannelConfiguration().getDescription());
	}

	[LineNumberTable(71)]
	public virtual AACDecoderConfig getConfig()
	{
		return config;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 100, 109, 159, 11, 250, 70, 227,
		59, 98, 105, 136, 139
	})]
	public virtual void decodeFrame(byte[] frame, SampleBuffer buffer)
	{
		if (frame != null)
		{
			_in.setData(frame);
		}
		Logger.debug(new StringBuilder().append("bits left ").append(_in.getBitsLeft()).toString());
		AACException ex;
		try
		{
			decode(buffer);
			return;
		}
		catch (AACException x)
		{
			ex = ByteCodeHelper.MapException<AACException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		AACException e = ex;
		if (!e.isEndOfStream())
		{
			throw e;
		}
		Logger.warn("unexpected end of frame");
	}
}
