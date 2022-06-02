using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.tools;

internal interface TNSTables
{
	static readonly float[] TNS_COEF_1_3;

	static readonly float[] TNS_COEF_0_3;

	static readonly float[] TNS_COEF_1_4;

	static readonly float[] TNS_COEF_0_4;

	static readonly float[][] TNS_TABLES;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		130,
		159,
		13,
		191,
		45,
		191,
		45,
		byte.MaxValue,
		117,
		69
	})]
	static TNSTables()
	{
		TNS_COEF_1_3 = new float[4] { 0f, -0.433883727f, 0.6427876f, 0.342020154f };
		TNS_COEF_0_3 = new float[8] { 0f, -0.433883727f, -0.7818315f, -0.9749279f, 0.9848077f, 0.8660254f, 0.6427876f, 0.342020154f };
		TNS_COEF_1_4 = new float[8] { 0f, -0.2079117f, -0.406736642f, -0.587785244f, 0.6736956f, 0.526432157f, 0.361241668f, 0.183749512f };
		TNS_COEF_0_4 = new float[16]
		{
			0f, -0.2079117f, -0.406736642f, -0.587785244f, -0.7431448f, -0.8660254f, -0.95105654f, -0.9945219f, 0.995734155f, 0.9618256f,
			0.8951633f, 0.7980172f, 0.6736956f, 0.526432157f, 0.361241668f, 0.183749512f
		};
		TNS_TABLES = new float[4][] { TNS_COEF_0_3, TNS_COEF_0_4, TNS_COEF_1_3, TNS_COEF_1_4 };
	}
}
