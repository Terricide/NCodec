using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.syntax;

internal interface ScaleFactorTable
{
	static readonly float[] SCALEFACTOR_TABLE;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(13)]
	static ScaleFactorTable()
	{
		SCALEFACTOR_TABLE = new float[428]
		{
			8.881784E-16f,
			1.05622806E-15f,
			1.25607395E-15f,
			1.49373212E-15f,
			1.77635684E-15f,
			2.11245612E-15f,
			2.51214789E-15f,
			2.98746424E-15f,
			3.55271368E-15f,
			4.22491225E-15f,
			5.02429578E-15f,
			5.97492848E-15f,
			7.10542736E-15f,
			8.4498245E-15f,
			1.00485916E-14f,
			1.1949857E-14f,
			1.42108547E-14f,
			1.6899649E-14f,
			2.00971831E-14f,
			2.38997139E-14f,
			2.842171E-14f,
			3.3799298E-14f,
			4.01943663E-14f,
			4.77994279E-14f,
			5.684342E-14f,
			6.7598596E-14f,
			8.038873E-14f,
			9.55988557E-14f,
			1.13686838E-13f,
			1.35197192E-13f,
			1.60777465E-13f,
			1.91197711E-13f,
			2.27373675E-13f,
			2.70394384E-13f,
			3.2155493E-13f,
			3.82395423E-13f,
			4.54747351E-13f,
			5.40788768E-13f,
			6.4310986E-13f,
			7.64790846E-13f,
			9.094947E-13f,
			1.08157754E-12f,
			1.28621972E-12f,
			1.52958169E-12f,
			1.8189894E-12f,
			2.163155E-12f,
			2.57243944E-12f,
			3.05916338E-12f,
			3.637979E-12f,
			4.32631E-12f,
			5.144879E-12f,
			6.11832677E-12f,
			7.275958E-12f,
			8.65262E-12f,
			1.02897578E-11f,
			1.22366535E-11f,
			1.45519152E-11f,
			1.730524E-11f,
			2.05795155E-11f,
			2.44733071E-11f,
			2.910383E-11f,
			3.461048E-11f,
			4.115903E-11f,
			4.89466141E-11f,
			5.820766E-11f,
			6.922096E-11f,
			8.231806E-11f,
			9.789323E-11f,
			1.16415322E-10f,
			1.38441925E-10f,
			1.64636124E-10f,
			1.95786456E-10f,
			2.32830644E-10f,
			2.76883849E-10f,
			3.29272248E-10f,
			3.915729E-10f,
			4.656613E-10f,
			5.537677E-10f,
			6.585445E-10f,
			7.831458E-10f,
			9.313226E-10f,
			1.1075354E-09f,
			1.317089E-09f,
			1.56629165E-09f,
			1.86264515E-09f,
			2.21507079E-09f,
			2.634178E-09f,
			3.1325833E-09f,
			3.7252903E-09f,
			4.43014159E-09f,
			5.268356E-09f,
			6.26516661E-09f,
			7.450581E-09f,
			8.860283E-09f,
			1.05367119E-08f,
			1.25303332E-08f,
			1.49011612E-08f,
			1.77205663E-08f,
			2.10734239E-08f,
			2.50606664E-08f,
			2.98023224E-08f,
			3.54411327E-08f,
			4.21468478E-08f,
			5.01213329E-08f,
			5.96046448E-08f,
			7.08822654E-08f,
			8.42936956E-08f,
			1.00242666E-07f,
			1.1920929E-07f,
			1.41764531E-07f,
			1.68587391E-07f,
			2.00485331E-07f,
			2.38418579E-07f,
			2.83529062E-07f,
			3.37174782E-07f,
			4.00970663E-07f,
			4.76837158E-07f,
			5.670581E-07f,
			6.74349565E-07f,
			8.019413E-07f,
			9.536743E-07f,
			1.13411625E-06f,
			1.34869913E-06f,
			1.60388265E-06f,
			1.90734863E-06f,
			2.26823249E-06f,
			2.69739826E-06f,
			3.2077653E-06f,
			3.81469727E-06f,
			4.536465E-06f,
			5.39479652E-06f,
			6.41553061E-06f,
			7.62939453E-06f,
			9.07293E-06f,
			1.0789593E-05f,
			1.28310612E-05f,
			1.52587891E-05f,
			1.814586E-05f,
			2.15791861E-05f,
			2.56621224E-05f,
			3.05175781E-05f,
			3.629172E-05f,
			4.31583721E-05f,
			5.13242449E-05f,
			6.10351563E-05f,
			7.258344E-05f,
			8.63167443E-05f,
			0.00010264849f,
			0.000122070313f,
			0.00014516688f,
			0.000172633489f,
			0.000205296979f,
			0.000244140625f,
			0.000290333759f,
			0.000345266977f,
			0.000410593959f,
			0.00048828125f,
			0.0005806675f,
			0.000690533954f,
			0.0008211879f,
			0.0009765625f,
			0.001161335f,
			0.00138106791f,
			0.00164237584f,
			0.001953125f,
			0.00232267f,
			0.00276213582f,
			0.00328475167f,
			0.00390625f,
			0.00464534f,
			0.00552427163f,
			0.00656950334f,
			1f / 128f,
			0.00929068f,
			0.0110485433f,
			0.0131390067f,
			1f / 64f,
			0.01858136f,
			0.0220970865f,
			0.0262780134f,
			1f / 32f,
			0.03716272f,
			0.0441941731f,
			0.0525560267f,
			0.0625f,
			0.07432544f,
			0.0883883461f,
			0.105112053f,
			0.125f,
			0.148650885f,
			0.176776692f,
			0.2102241f,
			0.25f,
			0.297301769f,
			0.353553385f,
			0.4204482f,
			0.5f,
			0.594603539f,
			0.707106769f,
			0.8408964f,
			1f,
			1.18920708f,
			1.41421354f,
			1.68179286f,
			2f,
			2.37841415f,
			2.828427f,
			3.36358571f,
			4f,
			4.75682831f,
			5.656854f,
			6.72717142f,
			8f,
			9.513657f,
			11.3137083f,
			13.4543428f,
			16f,
			19.0273132f,
			22.6274166f,
			26.9086857f,
			32f,
			38.0546265f,
			45.2548332f,
			53.81737f,
			64f,
			76.10925f,
			90.50967f,
			107.634743f,
			128f,
			152.2185f,
			181.019333f,
			215.269485f,
			256f,
			304.437f,
			362.038666f,
			430.538971f,
			512f,
			608.874f,
			724.077332f,
			861.077942f,
			1024f,
			1217.748f,
			1448.15466f,
			1722.15588f,
			2048f,
			2435.496f,
			2896.30933f,
			3444.31177f,
			4096f,
			4870.992f,
			5792.61865f,
			6888.62354f,
			8192f,
			9741.984f,
			11585.2373f,
			13777.2471f,
			16384f,
			19483.9688f,
			23170.4746f,
			86565f / (float)Math.PI,
			32768f,
			38967.9375f,
			46340.95f,
			173130f / (float)Math.PI,
			65536f,
			77935.875f,
			92681.9f,
			346260f / (float)Math.PI,
			131072f,
			155871.75f,
			185363.8f,
			692520f / (float)Math.PI,
			262144f,
			311743.5f,
			370727.6f,
			1385040f / (float)Math.PI,
			524288f,
			623487f,
			741455.2f,
			2770080f / (float)Math.PI,
			1048576f,
			1246974f,
			1482910.38f,
			5540160f / (float)Math.PI,
			2097152f,
			2493948f,
			9317401f / (float)Math.PI,
			1.108032E+07f / (float)Math.PI,
			4194304f,
			4987896f,
			18634802f / (float)Math.PI,
			2.216064E+07f / (float)Math.PI,
			8388608f,
			9975792f,
			11863283f,
			14107901f,
			16777216f,
			19951584f,
			23726566f,
			28215802f,
			33554432f,
			3.990317E+07f,
			47453132f,
			56431604f,
			67108864f,
			7.980634E+07f,
			94906264f,
			112863208f,
			134217728f,
			159612672f,
			189812528f,
			225726416f,
			268435456f,
			319225344f,
			379625056f,
			451452832f,
			5.368709E+08f,
			6.384507E+08f,
			7.592501E+08f,
			902905664f,
			1.07374182E+09f,
			1.27690138E+09f,
			1.51850022E+09f,
			1.80581133E+09f,
			2.14748365E+09f,
			2.55380275E+09f,
			3.03700045E+09f,
			3.61162266E+09f,
			4.2949673E+09f,
			5.1076055E+09f,
			6.074001E+09f,
			7.22324531E+09f,
			8.589935E+09f,
			1.0215211E+10f,
			1.21480018E+10f,
			1.44464906E+10f,
			1.717987E+10f,
			2.0430422E+10f,
			2.42960036E+10f,
			2.88929812E+10f,
			3.435974E+10f,
			4.0860844E+10f,
			4.85920072E+10f,
			5.77859625E+10f,
			6.871948E+10f,
			8.172169E+10f,
			9.71840143E+10f,
			1.15571925E+11f,
			1.37438953E+11f,
			1.63443376E+11f,
			1.94368029E+11f,
			2.3114385E+11f,
			2.748779E+11f,
			3.26886752E+11f,
			3.88736057E+11f,
			4.622877E+11f,
			5.497558E+11f,
			6.537735E+11f,
			7.774721E+11f,
			9.245754E+11f,
			1.09951163E+12f,
			1.307547E+12f,
			1.55494423E+12f,
			1.8491508E+12f,
			2.19902326E+12f,
			2.615094E+12f,
			3.10988846E+12f,
			3.6983016E+12f,
			4.39804651E+12f,
			5.230188E+12f,
			6.219777E+12f,
			7.396603E+12f,
			8.796093E+12f,
			1.04603761E+13f,
			1.24395538E+13f,
			1.47932064E+13f,
			1.7592186E+13f,
			2.09207521E+13f,
			2.48791077E+13f,
			2.95864128E+13f,
			3.51843721E+13f,
			4.18415043E+13f,
			4.97582153E+13f,
			5.91728256E+13f,
			7.03687442E+13f,
			8.368301E+13f,
			9.951643E+13f,
			1.18345651E+14f,
			1.40737488E+14f,
			1.67366017E+14f,
			1.99032861E+14f,
			2.366913E+14f,
			2.81474977E+14f,
			3.34732034E+14f,
			3.98065723E+14f,
			4.733826E+14f,
			5.62949953E+14f,
			6.694641E+14f,
			7.96131445E+14f,
			9.467652E+14f,
			1.12589991E+15f,
			1.33892814E+15f,
			1.59226289E+15f,
			1.89353042E+15f,
			2.25179981E+15f,
			2.67785627E+15f,
			3.18452578E+15f,
			3.78706084E+15f,
			4.50359963E+15f,
			5.35571255E+15f,
			6.36905156E+15f,
			7.57412168E+15f,
			9.007199E+15f,
			1.07114251E+16f,
			1.27381031E+16f,
			1.51482434E+16f,
			1.80143985E+16f,
			2.142285E+16f,
			2.54762063E+16f,
			3.02964867E+16f,
			3.6028797E+16f,
			4.28457E+16f,
			5.09524125E+16f,
			6.05929734E+16f,
			7.2057594E+16f,
			8.56914E+16f,
			1.01904825E+17f,
			1.21185947E+17f
		};
	}
}