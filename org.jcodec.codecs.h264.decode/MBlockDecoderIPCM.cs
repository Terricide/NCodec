using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.decode.aso;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlockDecoderIPCM : Object
{
	private Mapper mapper;

	private DecoderState s;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104 })]
	public MBlockDecoderIPCM(Mapper mapper, DecoderState decoderState)
	{
		this.mapper = mapper;
		s = decoderState;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 115, 110, 127, 0 })]
	public virtual void decode(MBlock mBlock, Picture mb)
	{
		int mbX = mapper.getMbX(mBlock.mbIdx);
		MBlockDecoderUtils.collectPredictors(s, mb, mbX);
		MBlockDecoderUtils.saveVectIntra(s, mapper.getMbX(mBlock.mbIdx));
	}
}
