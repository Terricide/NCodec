using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac.syntax;

[NonNestedInnerClass("net.sourceforge.jaad.aac.syntax.FIL$DynamicRangeInfo")]
[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants" })]
internal class FIL : Element, SyntaxConstants
{
	private const int TYPE_FILL = 0;

	private const int TYPE_FILL_DATA = 1;

	private const int TYPE_EXT_DATA_ELEMENT = 2;

	private const int TYPE_DYNAMIC_RANGE = 11;

	private const int TYPE_SBR_DATA = 13;

	private const int TYPE_SBR_DATA_CRC = 14;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool downSampledSBR;

	private FIL_0024DynamicRangeInfo dri;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 125, 129, 71, 105, 101, 159, 41, 106, 166,
		103, 121, 127, 1, 99, 131, 191, 7, 104, 227,
		70, 104, 163
	})]
	private int decodeExtensionPayload(IBitStream _in, int count, Element prev, SampleFrequency sf, bool sbrEnabled, bool smallFrames)
	{
		int type = _in.readBits(4);
		int ret = count - 4;
		switch (type)
		{
		case 11:
			return decodeDynamicRangeInfo(_in, ret);
		case 13:
		case 14:
			if (sbrEnabled)
			{
				if (prev is SCE_LFE || prev is CPE || prev is CCE)
				{
					prev.decodeSBR(_in, sf, ret, prev is CPE, type == 14, downSampledSBR, smallFrames);
					return 0;
				}
				string message = new StringBuilder().append("SBR applied on unexpected element: ").append(prev).toString();
				
				throw new AACException(message);
			}
			_in.skipBits(ret);
			ret = 0;
			break;
		}
		_in.skipBits(ret);
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 130, 116, 131, 163, 116, 116, 212, 116,
		203, 119, 116, 116, 102, 111, 115, 103, 117, 6,
		231, 71, 116, 116, 116, 166, 115, 115, 103, 116,
		117, 230, 61, 231, 69
	})]
	private int decodeDynamicRangeInfo(IBitStream _in, int count)
	{
		if (dri == null)
		{
			dri = new FIL_0024DynamicRangeInfo();
		}
		int ret = count;
		int bandCount = 1;
		if (FIL_0024DynamicRangeInfo.access_0024002(dri, _in.readBool()))
		{
			FIL_0024DynamicRangeInfo.access_0024102(dri, _in.readBits(4));
			FIL_0024DynamicRangeInfo.access_0024202(dri, _in.readBits(4));
		}
		if (FIL_0024DynamicRangeInfo.access_0024302(dri, _in.readBool()))
		{
			ret -= decodeExcludedChannels(_in);
		}
		if (FIL_0024DynamicRangeInfo.access_0024402(dri, _in.readBool()))
		{
			FIL_0024DynamicRangeInfo.access_0024502(dri, _in.readBits(4));
			FIL_0024DynamicRangeInfo.access_0024602(dri, _in.readBits(4));
			ret += -8;
			bandCount += FIL_0024DynamicRangeInfo.access_0024500(dri);
			FIL_0024DynamicRangeInfo.access_0024702(dri, new int[bandCount]);
			for (int j = 0; j < bandCount; j++)
			{
				FIL_0024DynamicRangeInfo.access_0024700(dri)[j] = _in.readBits(8);
				ret += -8;
			}
		}
		if (FIL_0024DynamicRangeInfo.access_0024802(dri, _in.readBool()))
		{
			FIL_0024DynamicRangeInfo.access_0024902(dri, _in.readBits(7));
			FIL_0024DynamicRangeInfo.access_00241002(dri, _in.readBits(1));
			ret += -8;
		}
		FIL_0024DynamicRangeInfo.access_00241102(dri, new bool[bandCount]);
		FIL_0024DynamicRangeInfo.access_00241202(dri, new int[bandCount]);
		for (int i = 0; i < bandCount; i++)
		{
			FIL_0024DynamicRangeInfo.access_00241100(dri)[i] = _in.readBool();
			FIL_0024DynamicRangeInfo.access_00241200(dri)[i] = _in.readBits(7);
			ret += -8;
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 105, 130, 163, 103, 116, 5, 231, 69, 142 })]
	private int decodeExcludedChannels(IBitStream _in)
	{
		int exclChs = 0;
		do
		{
			for (int i = 0; i < 7; i++)
			{
				FIL_0024DynamicRangeInfo.access_00241300(dri)[exclChs] = _in.readBool();
				exclChs++;
			}
		}
		while (exclChs < 57 && _in.readBool());
		return exclChs / 7 * 8;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 161, 67, 105, 104 })]
	internal FIL(bool downSampledSBR)
	{
		this.downSampledSBR = downSampledSBR;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 65, 71, 105, 114, 133, 99, 137, 101,
		176, 108, 103, 112, 127, 13
	})]
	internal virtual void decode(IBitStream _in, Element prev, SampleFrequency sf, bool sbrEnabled, bool smallFrames)
	{
		int count = _in.readBits(4);
		if (count == 15)
		{
			count += _in.readBits(8) - 1;
		}
		count *= 8;
		int cpy = count;
		int pos = _in.getPosition();
		while (count > 0)
		{
			count = decodeExtensionPayload(_in, count, prev, sf, sbrEnabled, smallFrames);
		}
		int pos2 = _in.getPosition() - pos;
		int bitsLeft = cpy - pos2;
		if (bitsLeft > 0)
		{
			_in.skipBits(pos2);
		}
		else if (bitsLeft < 0)
		{
			string message = new StringBuilder().append("FIL element overread: ").append(bitsLeft).toString();
			
			throw new AACException(message);
		}
	}
}
