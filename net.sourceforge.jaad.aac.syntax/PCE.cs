using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.logging;

namespace net.sourceforge.jaad.aac.syntax;

public class PCE : Element
{
	public class CCE : Object
	{
		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private bool isIndSW;

		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private int tag;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 130, 97, 67, 105, 104, 104 })]
		public CCE(bool isIndSW, int tag)
		{
			this.isIndSW = isIndSW;
			this.tag = tag;
		}

		[LineNumberTable(55)]
		public virtual bool isIsIndSW()
		{
			return isIndSW;
		}

		[LineNumberTable(59)]
		public virtual int getTag()
		{
			return tag;
		}
	}

	public class TaggedElement : Object
	{
		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private bool isCPE;

		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private int tag;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 129, 67, 105, 104, 104 })]
		public TaggedElement(bool isCPE, int tag)
		{
			this.isCPE = isCPE;
			this.tag = tag;
		}

		[LineNumberTable(36)]
		public virtual bool isIsCPE()
		{
			return isCPE;
		}

		[LineNumberTable(40)]
		public virtual int getTag()
		{
			return tag;
		}
	}

	private const int MAX_FRONT_CHANNEL_ELEMENTS = 16;

	private const int MAX_SIDE_CHANNEL_ELEMENTS = 16;

	private const int MAX_BACK_CHANNEL_ELEMENTS = 16;

	private const int MAX_LFE_CHANNEL_ELEMENTS = 4;

	private const int MAX_ASSOC_DATA_ELEMENTS = 8;

	private const int MAX_VALID_CC_ELEMENTS = 16;

	private Profile profile;

	private SampleFrequency sampleFrequency;

	private int frontChannelElementsCount;

	private int sideChannelElementsCount;

	private int backChannelElementsCount;

	private int lfeChannelElementsCount;

	private int assocDataElementsCount;

	private int validCCElementsCount;

	private bool monoMixdown;

	private bool stereoMixdown;

	private bool matrixMixdownIDXPresent;

	private int monoMixdownElementNumber;

	private int stereoMixdownElementNumber;

	private int matrixMixdownIDX;

	private bool pseudoSurround;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private TaggedElement[] frontElements;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private TaggedElement[] sideElements;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private TaggedElement[] backElements;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] lfeElementTags;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] assocDataElementTags;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private CCE[] ccElements;

	private byte[] commentFieldData;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 98, 105, 110, 110, 110, 109, 109, 110,
		108
	})]
	public PCE()
	{
		frontElements = new TaggedElement[16];
		sideElements = new TaggedElement[16];
		backElements = new TaggedElement[16];
		lfeElementTags = new int[4];
		assocDataElementTags = new int[8];
		ccElements = new CCE[16];
		sampleFrequency = SampleFrequency.___003C_003ESAMPLE_FREQUENCY_NONE;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 136, 147, 147, 110, 110, 110, 110,
		110, 142, 116, 107, 142, 116, 107, 142, 116, 107,
		110, 173, 148, 148, 180, 108, 48, 199, 108, 48,
		199, 108, 59, 199, 135, 105, 109, 103, 49, 167
	})]
	public virtual void decode(IBitStream _in)
	{
		readElementInstanceTag(_in);
		profile = Profile.forInt(_in.readBits(2));
		sampleFrequency = SampleFrequency.forInt(_in.readBits(4));
		frontChannelElementsCount = _in.readBits(4);
		sideChannelElementsCount = _in.readBits(4);
		backChannelElementsCount = _in.readBits(4);
		lfeChannelElementsCount = _in.readBits(2);
		assocDataElementsCount = _in.readBits(3);
		validCCElementsCount = _in.readBits(4);
		int num = (_in.readBool() ? 1 : 0);
		int num2 = num;
		monoMixdown = (byte)num != 0;
		if (num2 != 0)
		{
			Logger.warn("mono mixdown present, but not yet supported");
			monoMixdownElementNumber = _in.readBits(4);
		}
		num = (_in.readBool() ? 1 : 0);
		int num3 = num;
		stereoMixdown = (byte)num != 0;
		if (num3 != 0)
		{
			Logger.warn("stereo mixdown present, but not yet supported");
			stereoMixdownElementNumber = _in.readBits(4);
		}
		num = (_in.readBool() ? 1 : 0);
		int num4 = num;
		matrixMixdownIDXPresent = (byte)num != 0;
		if (num4 != 0)
		{
			Logger.warn("matrix mixdown present, but not yet supported");
			matrixMixdownIDX = _in.readBits(2);
			pseudoSurround = _in.readBool();
		}
		readTaggedElementArray(frontElements, _in, frontChannelElementsCount);
		readTaggedElementArray(sideElements, _in, sideChannelElementsCount);
		readTaggedElementArray(backElements, _in, backChannelElementsCount);
		for (int i = 0; i < lfeChannelElementsCount; i++)
		{
			lfeElementTags[i] = _in.readBits(4);
		}
		for (int i = 0; i < assocDataElementsCount; i++)
		{
			assocDataElementTags[i] = _in.readBits(4);
		}
		for (int i = 0; i < validCCElementsCount; i++)
		{
			ccElements[i] = new CCE(_in.readBool(), _in.readBits(4));
		}
		_in.byteAlign();
		int commentFieldBytes = _in.readBits(8);
		commentFieldData = new byte[commentFieldBytes];
		for (int i = 0; i < commentFieldBytes; i++)
		{
			commentFieldData[i] = (byte)(sbyte)_in.readBits(8);
		}
	}

	[LineNumberTable(150)]
	public virtual Profile getProfile()
	{
		return profile;
	}

	[LineNumberTable(154)]
	public virtual SampleFrequency getSampleFrequency()
	{
		return sampleFrequency;
	}

	[LineNumberTable(158)]
	public virtual int getChannelCount()
	{
		return frontChannelElementsCount + sideChannelElementsCount + backChannelElementsCount + lfeChannelElementsCount + assocDataElementsCount;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 106, 66, 103, 54, 167 })]
	private void readTaggedElementArray(TaggedElement[] te, IBitStream _in, int len)
	{
		for (int i = 0; i < len; i++)
		{
			te[i] = new TaggedElement(_in.readBool(), _in.readBits(4));
		}
	}
}
