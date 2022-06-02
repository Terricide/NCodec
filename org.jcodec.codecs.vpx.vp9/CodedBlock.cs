using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class CodedBlock : Object
{
	internal static CodedBlock[] ___003C_003EEMPTY_ARR;

	private ModeInfo mode;

	private Residual residual;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static CodedBlock[] EMPTY_ARR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EEMPTY_ARR;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104 })]
	public CodedBlock(ModeInfo mode, Residual r)
	{
		this.mode = mode;
		residual = r;
	}

	[LineNumberTable(24)]
	public virtual ModeInfo getMode()
	{
		return mode;
	}

	[LineNumberTable(28)]
	public virtual Residual getResidual()
	{
		return residual;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 106, 148, 114, 110 })]
	public static CodedBlock read(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		ModeInfo mode = ((!c.isKeyIntraFrame()) ? new InterModeInfo().read(miCol, miRow, blSz, decoder, c) : new ModeInfo().read(miCol, miRow, blSz, decoder, c));
		Residual r = Residual.readResidual(miCol, miRow, blSz, decoder, c, mode);
		CodedBlock result = new CodedBlock(mode, r);
		
		return result;
	}

	[LineNumberTable(13)]
	static CodedBlock()
	{
		___003C_003EEMPTY_ARR = new CodedBlock[0];
	}
}
