using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.vpx;

public class VPXQuantizer : Object
{
	private int y1_dc_delta_q;

	private int uv_dc_delta_q;

	private int uv_ac_delta_q;

	private int y2_dc_delta_q;

	private int y2_ac_delta_q;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public VPXQuantizer()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 123, 116, 108 })]
	public void quantizeY(int[] coeffs, int qp)
	{
		int factDC = MathUtil.clip(VPXConst.___003C_003Edc_qlookup[qp + y1_dc_delta_q], 8, 132);
		int invFactAC = MathUtil.clip(VPXConst.___003C_003Eac_qlookup[qp], 8, 132);
		quantize(coeffs, factDC, invFactAC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 125, 127, 5, 108 })]
	public void quantizeY2(int[] coeffs, int qp)
	{
		int factDC = MathUtil.clip(VPXConst.___003C_003Edc_qlookup[qp + y2_dc_delta_q] * 2, 8, 132);
		int invFactAC = MathUtil.clip(VPXConst.___003C_003Eac_qlookup[qp + y2_ac_delta_q] * 155 / 100, 8, 132);
		quantize(coeffs, factDC, invFactAC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 123, 123, 108 })]
	public void quantizeUV(int[] coeffs, int qp)
	{
		int factDC = MathUtil.clip(VPXConst.___003C_003Edc_qlookup[qp + uv_dc_delta_q], 8, 132);
		int invFactAC = MathUtil.clip(VPXConst.___003C_003Eac_qlookup[qp + uv_ac_delta_q], 8, 132);
		quantize(coeffs, factDC, invFactAC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 123, 123, 108 })]
	public void dequantizeUV(int[] coeffs, int qp)
	{
		int factDC = MathUtil.clip(VPXConst.___003C_003Edc_qlookup[qp + uv_dc_delta_q], 8, 132);
		int factAC = MathUtil.clip(VPXConst.___003C_003Eac_qlookup[qp + uv_ac_delta_q], 8, 132);
		dequantize(coeffs, factDC, factAC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 66, 125, 127, 5, 108 })]
	public void dequantizeY2(int[] coeffs, int qp)
	{
		int factDC = MathUtil.clip(VPXConst.___003C_003Edc_qlookup[qp + y2_dc_delta_q] * 2, 8, 132);
		int factAC = MathUtil.clip(VPXConst.___003C_003Eac_qlookup[qp + y2_ac_delta_q] * 155 / 100, 8, 132);
		dequantize(coeffs, factDC, factAC);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 123, 116, 108 })]
	public void dequantizeY(int[] coeffs, int qp)
	{
		int factDC = MathUtil.clip(VPXConst.___003C_003Edc_qlookup[qp + y1_dc_delta_q], 8, 132);
		int factAC = MathUtil.clip(VPXConst.___003C_003Eac_qlookup[qp], 8, 132);
		dequantize(coeffs, factDC, factAC);
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 133, 130, 117, 104, 49, 135 })]
	private void quantize(int[] coeffs, int factDC, int factAC)
	{
		int num = 0;
		int num2 = coeffs[num];
		coeffs[num] = ((factDC != -1) ? (num2 / factDC) : (-num2));
		for (int i = 1; i < 16; i++)
		{
			int num3 = i;
			int num4 = coeffs[i];
			coeffs[num3] = ((factAC != -1) ? (num4 / factAC) : (-num4));
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 127, 130, 109, 104, 45, 135 })]
	private void dequantize(int[] coeffs, int factDC, int factAC)
	{
		int num = 0;
		int[] array = coeffs;
		array[num] *= factDC;
		for (int i = 1; i < 16; i++)
		{
			num = i;
			array = coeffs;
			array[num] *= factAC;
		}
	}
}
