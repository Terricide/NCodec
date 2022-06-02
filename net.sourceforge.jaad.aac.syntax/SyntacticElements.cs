using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.filterbank;
using net.sourceforge.jaad.aac.sbr;
using net.sourceforge.jaad.aac.tools;
using org.jcodec.common.logging;

namespace net.sourceforge.jaad.aac.syntax;

public class SyntacticElements : Object, SyntaxConstants
{
	private AACDecoderConfig config;

	private bool sbrPresent;

	private bool psPresent;

	private int bitsRead;

	private PCE pce;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private Element[] elements;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private CCE[] cces;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private DSE[] dses;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private FIL[] fils;

	private int curElem;

	private int curCCE;

	private int curDSE;

	private int curFIL;

	private float[][] data;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 130, 105, 136, 108, 110, 110, 110, 142,
		105
	})]
	public SyntacticElements(AACDecoderConfig config)
	{
		this.config = config;
		pce = new PCE();
		elements = new Element[64];
		cces = new CCE[16];
		dses = new DSE[16];
		fils = new FIL[16];
		startNewFrame();
	}

	[LineNumberTable(new byte[] { 159, 128, 162, 104, 104, 104, 104, 104, 104, 104 })]
	public void startNewFrame()
	{
		curElem = 0;
		curCCE = 0;
		curDSE = 0;
		curFIL = 0;
		sbrPresent = false;
		psPresent = false;
		bitsRead = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 125, 98, 168, 99, 99, 118, 118, 191, 9,
		107, 105, 134, 107, 105, 131, 107, 104, 99, 131,
		107, 104, 99, 131, 107, 104, 99, 131, 107, 105,
		99, 166, 107, 99, 200, 110, 106, 110, 106, 110,
		106, 105, 110, 106, 105, 105, 110, 106, 105, 105,
		110, 106, 105, 105, 105, 107, 106, 105, 105, 105,
		105, 139, 191, 8, 135, 111
	})]
	public virtual void decode(IBitStream _in)
	{
		int start = _in.getPosition();
		Element prev = null;
		int content = 1;
		if (!config.getProfile().isErrorResilientProfile())
		{
			int type;
			while (content != 0 && (type = _in.readBits(3)) != 7)
			{
				switch (type)
				{
				case 0:
				case 3:
					Logger.debug("SCE");
					prev = decodeSCE_LFE(_in);
					break;
				case 1:
					Logger.debug("CPE");
					prev = decodeCPE(_in);
					break;
				case 2:
					Logger.debug("CCE");
					decodeCCE(_in);
					prev = null;
					break;
				case 4:
					Logger.debug("DSE");
					decodeDSE(_in);
					prev = null;
					break;
				case 5:
					Logger.debug("PCE");
					decodePCE(_in);
					prev = null;
					break;
				case 6:
					Logger.debug("FIL");
					decodeFIL(_in, prev);
					prev = null;
					break;
				}
			}
			Logger.debug("END");
			content = 0;
			prev = null;
		}
		else
		{
			ChannelConfiguration cc = config.getChannelConfiguration();
			if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_MONO == cc)
			{
				decodeSCE_LFE(_in);
			}
			else if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_STEREO == cc)
			{
				decodeCPE(_in);
			}
			else if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER == cc)
			{
				decodeSCE_LFE(_in);
				decodeCPE(_in);
			}
			else if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_STEREO_PLUS_CENTER_PLUS_REAR_MONO == cc)
			{
				decodeSCE_LFE(_in);
				decodeCPE(_in);
				decodeSCE_LFE(_in);
			}
			else if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_FIVE == cc)
			{
				decodeSCE_LFE(_in);
				decodeCPE(_in);
				decodeCPE(_in);
			}
			else if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_FIVE_PLUS_ONE == cc)
			{
				decodeSCE_LFE(_in);
				decodeCPE(_in);
				decodeCPE(_in);
				decodeSCE_LFE(_in);
			}
			else
			{
				if (ChannelConfiguration.___003C_003ECHANNEL_CONFIG_SEVEN_PLUS_ONE != cc)
				{
					string message = new StringBuilder().append("unsupported channel configuration for error resilience: ").append(cc).toString();
					
					throw new AACException(message);
				}
				decodeSCE_LFE(_in);
				decodeCPE(_in);
				decodeCPE(_in);
				decodeCPE(_in);
				decodeSCE_LFE(_in);
			}
		}
		_in.byteAlign();
		bitsRead = _in.getPosition() - start;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 93, 130, 109, 173, 114, 113, 142, 159, 73,
		196, 122, 108, 106, 106, 106, 149, 106, 106, 110,
		137, 138, 109, 231, 49, 236, 82
	})]
	public virtual void process(FilterBank filterBank)
	{
		Profile profile = config.getProfile();
		SampleFrequency sf = config.getSampleFrequency();
		int chs = config.getChannelConfiguration().getChannelCount();
		if (chs == 1 && psPresent)
		{
			chs++;
		}
		int mult = ((!sbrPresent) ? 1 : 2);
		if (data == null || (nint)chs != (nint)data.LongLength || (nint)(mult * config.getFrameLength()) != (nint)data[0].LongLength)
		{
			int num = chs;
			int num2 = mult * config.getFrameLength();
			int[] array = new int[2];
			int num3 = (array[1] = num2);
			num3 = (array[0] = num);
			data = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		}
		int channel = 0;
		for (int i = 0; i < (nint)elements.LongLength; i++)
		{
			if (channel >= chs)
			{
				break;
			}
			Element e = elements[i];
			if (e != null)
			{
				if (e is SCE_LFE)
				{
					SCE_LFE scelfe = (SCE_LFE)e;
					channel += processSingle(scelfe, filterBank, channel, profile, sf);
				}
				else if (e is CPE)
				{
					CPE cpe = (CPE)e;
					processPair(cpe, filterBank, channel, profile, sf);
					channel += 2;
				}
				else if (e is CCE)
				{
					((CCE)e).process();
					channel++;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 44, 162, 136, 105, 123, 111, 149, 105, 247,
		69, 108, 108, 108, 127, 3, 108, 100, 113, 179,
		115, 239, 55, 12, 236, 80, 118
	})]
	public virtual void sendToOutput(SampleBuffer buffer)
	{
		int be = (buffer.isBigEndian() ? 1 : 0);
		int chs = data.Length;
		int mult = ((!sbrPresent || !config.isSBREnabled()) ? 1 : 2);
		int length = mult * config.getFrameLength();
		int freq = mult * config.getSampleFrequency().getFrequency();
		byte[] b = buffer.getData();
		if ((nint)b.LongLength != chs * length * 2)
		{
			b = new byte[chs * length * 2];
		}
		for (int i = 0; i < chs; i++)
		{
			float[] cur = data[i];
			for (int j = 0; j < length; j++)
			{
				int s = (short)Math.max(Math.min(Math.round(cur[j]), 32767), -32768);
				int off = (j * chs + i) * 2;
				if (be != 0)
				{
					b[off] = (byte)(sbyte)((uint)(s >> 8) & 0xFFu);
					b[off + 1] = (byte)(sbyte)((uint)s & 0xFFu);
				}
				else
				{
					b[off + 1] = (byte)(sbyte)((uint)(s >> 8) & 0xFFu);
					b[off] = (byte)(sbyte)((uint)s & 0xFFu);
				}
			}
		}
		buffer.setData(b, freq, chs, 16, bitsRead);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 105, 162, 127, 14, 127, 0, 111 })]
	private Element decodeSCE_LFE(IBitStream _in)
	{
		if (elements[curElem] == null)
		{
			elements[curElem] = new SCE_LFE(config.getFrameLength());
		}
		((SCE_LFE)elements[curElem]).decode(_in, config);
		curElem++;
		return elements[curElem - 1];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 103, 130, 127, 14, 127, 0, 111 })]
	private Element decodeCPE(IBitStream _in)
	{
		if (elements[curElem] == null)
		{
			elements[curElem] = new CPE(config.getFrameLength());
		}
		((CPE)elements[curElem]).decode(_in, config);
		curElem++;
		return elements[curElem - 1];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 101, 98, 123, 127, 19, 122, 111 })]
	private void decodeCCE(IBitStream _in)
	{
		if (curCCE == 16)
		{
			
			throw new AACException("too much CCE elements");
		}
		if (cces[curCCE] == null)
		{
			CCE[] array = cces;
			int num = curCCE;
			CCE.___003Cclinit_003E();
			array[num] = new CCE(config.getFrameLength());
		}
		cces[curCCE].decode(_in, config);
		curCCE++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 99, 66, 123, 127, 3, 116, 111 })]
	private void decodeDSE(IBitStream _in)
	{
		if (curDSE == 16)
		{
			
			throw new AACException("too much CCE elements");
		}
		if (dses[curDSE] == null)
		{
			dses[curDSE] = new DSE();
		}
		dses[curDSE].decode(_in);
		curDSE++;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 98, 162, 109, 119, 119, 126 })]
	private void decodePCE(IBitStream _in)
	{
		pce.decode(_in);
		config.setProfile(pce.getProfile());
		config.setSampleFrequency(pce.getSampleFrequency());
		config.setChannelConfiguration(ChannelConfiguration.forInt(pce.getChannelCount()));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 96, 130, 123, 127, 14, 127, 23, 143, 108,
		104, 157
	})]
	private void decodeFIL(IBitStream _in, Element prev)
	{
		if (curFIL == 16)
		{
			
			throw new AACException("too much FIL elements");
		}
		if (fils[curFIL] == null)
		{
			fils[curFIL] = new FIL(config.isSBRDownSampled());
		}
		fils[curFIL].decode(_in, prev, config.getSampleFrequency(), config.isSBREnabled(), config.isSmallFrameUsed());
		curFIL++;
		if (prev != null && prev.isSBRPresent())
		{
			sbrPresent = true;
			if (!psPresent && prev.getSBR().isPSUsed())
			{
				psPresent = true;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 84, 98, 104, 104, 104, 168, 169, 127, 8,
		190, 173, 186, 173, 159, 7, 191, 2, 178, 191,
		11, 100, 124, 119, 107, 105, 106, 100, 159, 3,
		150
	})]
	private int processSingle(SCE_LFE scelfe, FilterBank filterBank, int channel, Profile profile, SampleFrequency sf)
	{
		ICStream ics = scelfe.getICStream();
		ICSInfo info = ics.getInfo();
		ICSInfo.LTPrediction ltp = info.getLTPrediction1();
		int elementID = scelfe.getElementInstanceTag();
		float[] iqData = ics.getInvQuantData();
		if (Object.instancehelper_equals(profile, Profile.___003C_003EAAC_MAIN) && info.isICPredictionPresent())
		{
			info.getICPrediction().process(ics, iqData, sf);
		}
		if (ICSInfo.LTPrediction.isLTPProfile(profile) && info.isLTPrediction1Present())
		{
			ltp.process(ics, iqData, filterBank, sf);
		}
		processDependentCoupling(channelPair: false, elementID, 0, iqData, null);
		if (ics.isTNSDataPresent())
		{
			ics.getTNS().process(ics, iqData, sf, decode: false);
		}
		processDependentCoupling(channelPair: false, elementID, 1, iqData, null);
		filterBank.process(info.getWindowSequence(), info.getWindowShape(1), info.getWindowShape(0), iqData, data[channel], channel);
		if (ICSInfo.LTPrediction.isLTPProfile(profile))
		{
			ltp.updateState(data[channel], filterBank.getOverlap(channel), profile);
		}
		processIndependentCoupling(channelPair: false, elementID, data[channel], null);
		if (ics.isGainControlPresent())
		{
			ics.getGainControl().process(iqData, info.getWindowShape(1), info.getWindowShape(0), info.getWindowSequence());
		}
		int chs = 1;
		if (sbrPresent && config.isSBREnabled())
		{
			if ((nint)data[channel].LongLength == config.getFrameLength())
			{
				Logger.warn("SBR data present, but buffer has normal size!");
			}
			SBR sbr = scelfe.getSBR();
			if (sbr.isPSUsed())
			{
				chs = 2;
				scelfe.getSBR()._process(data[channel], data[channel + 1], just_seeked: false);
			}
			else
			{
				scelfe.getSBR().process(data[channel], just_seeked: false);
			}
		}
		return chs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 72, 98, 104, 104, 104, 104, 105, 121, 169,
		105, 169, 155, 111, 121, 185, 171, 106, 118, 127,
		1, 214, 175, 122, 186, 175, 127, 7, 159, 11,
		106, 121, 221, 188, 127, 11, 191, 11, 118, 119,
		107, 159, 1
	})]
	private void processPair(CPE cpe, FilterBank filterBank, int channel, Profile profile, SampleFrequency sf)
	{
		ICStream ics1 = cpe.getLeftChannel();
		ICStream ics2 = cpe.getRightChannel();
		ICSInfo info1 = ics1.getInfo();
		ICSInfo info2 = ics2.getInfo();
		ICSInfo.LTPrediction ltp1 = info1.getLTPrediction1();
		ICSInfo.LTPrediction ltp2 = ((!cpe.isCommonWindow()) ? info2.getLTPrediction1() : info1.getLTPrediction2());
		int elementID = cpe.getElementInstanceTag();
		float[] iqData1 = ics1.getInvQuantData();
		float[] iqData2 = ics2.getInvQuantData();
		if (cpe.isCommonWindow() && cpe.isMSMaskPresent())
		{
			MS.process(cpe, iqData1, iqData2);
		}
		if (Object.instancehelper_equals(profile, Profile.___003C_003EAAC_MAIN))
		{
			if (info1.isICPredictionPresent())
			{
				info1.getICPrediction().process(ics1, iqData1, sf);
			}
			if (info2.isICPredictionPresent())
			{
				info2.getICPrediction().process(ics2, iqData2, sf);
			}
		}
		IS.process(cpe, iqData1, iqData2);
		if (ICSInfo.LTPrediction.isLTPProfile(profile))
		{
			if (info1.isLTPrediction1Present())
			{
				ltp1.process(ics1, iqData1, filterBank, sf);
			}
			if (cpe.isCommonWindow() && info1.isLTPrediction2Present())
			{
				ltp2.process(ics2, iqData2, filterBank, sf);
			}
			else if (info2.isLTPrediction1Present())
			{
				ltp2.process(ics2, iqData2, filterBank, sf);
			}
		}
		processDependentCoupling(channelPair: true, elementID, 0, iqData1, iqData2);
		if (ics1.isTNSDataPresent())
		{
			ics1.getTNS().process(ics1, iqData1, sf, decode: false);
		}
		if (ics2.isTNSDataPresent())
		{
			ics2.getTNS().process(ics2, iqData2, sf, decode: false);
		}
		processDependentCoupling(channelPair: true, elementID, 1, iqData1, iqData2);
		filterBank.process(info1.getWindowSequence(), info1.getWindowShape(1), info1.getWindowShape(0), iqData1, data[channel], channel);
		filterBank.process(info2.getWindowSequence(), info2.getWindowShape(1), info2.getWindowShape(0), iqData2, data[channel + 1], channel + 1);
		if (ICSInfo.LTPrediction.isLTPProfile(profile))
		{
			ltp1.updateState(data[channel], filterBank.getOverlap(channel), profile);
			ltp2.updateState(data[channel + 1], filterBank.getOverlap(channel + 1), profile);
		}
		processIndependentCoupling(channelPair: true, elementID, data[channel], data[channel + 1]);
		if (ics1.isGainControlPresent())
		{
			ics1.getGainControl().process(iqData1, info1.getWindowShape(1), info1.getWindowShape(0), info1.getWindowSequence());
		}
		if (ics2.isGainControlPresent())
		{
			ics2.getGainControl().process(iqData2, info2.getWindowShape(1), info2.getWindowShape(0), info2.getWindowSequence());
		}
		if (sbrPresent && config.isSBREnabled())
		{
			if ((nint)data[channel].LongLength == config.getFrameLength())
			{
				Logger.warn("SBR data present, but buffer has normal size!");
			}
			cpe.getSBR()._process(data[channel], data[channel + 1], just_seeked: false);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 49, 65, 67, 112, 106, 99, 115, 113, 107,
		119, 102, 106, 137, 102, 106, 167, 235, 52, 236,
		60, 234, 84
	})]
	private void processDependentCoupling(bool channelPair, int elementID, int couplingPoint, float[] data1, float[] data2)
	{
		for (int i = 0; i < (nint)cces.LongLength; i++)
		{
			CCE cce = cces[i];
			int index = 0;
			if (cce == null || cce.getCouplingPoint() != couplingPoint)
			{
				continue;
			}
			for (int c = 0; c <= cce.getCoupledCount(); c++)
			{
				int chSelect = cce.getCHSelect(c);
				if (cce.isChannelPair(c) == channelPair && cce.getIDSelect(c) == elementID)
				{
					if (chSelect != 1)
					{
						cce.applyDependentCoupling(index, data1);
						if (chSelect != 0)
						{
							index++;
						}
					}
					if (chSelect != 2)
					{
						cce.applyDependentCoupling(index, data2);
						index++;
					}
				}
				else
				{
					index += 1 + ((chSelect == 3) ? 1 : 0);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 56, 161, 67, 112, 106, 99, 115, 113, 107,
		119, 102, 105, 137, 102, 106, 167, 235, 52, 236,
		60, 234, 84
	})]
	private void processIndependentCoupling(bool channelPair, int elementID, float[] data1, float[] data2)
	{
		for (int i = 0; i < (nint)cces.LongLength; i++)
		{
			CCE cce = cces[i];
			int index = 0;
			if (cce == null || cce.getCouplingPoint() != 2)
			{
				continue;
			}
			for (int c = 0; c <= cce.getCoupledCount(); c++)
			{
				int chSelect = cce.getCHSelect(c);
				if (cce.isChannelPair(c) == channelPair && cce.getIDSelect(c) == elementID)
				{
					if (chSelect != 1)
					{
						cce.applyIndependentCoupling(index, data1);
						if (chSelect != 0)
						{
							index++;
						}
					}
					if (chSelect != 2)
					{
						cce.applyIndependentCoupling(index, data2);
						index++;
					}
				}
				else
				{
					index += 1 + ((chSelect == 3) ? 1 : 0);
				}
			}
		}
	}
}
