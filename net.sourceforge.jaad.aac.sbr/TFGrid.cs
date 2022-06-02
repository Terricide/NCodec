using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac.sbr;

internal class TFGrid : Object, SBRConstants
{
	[LineNumberTable(new byte[]
	{
		159, 138, 98, 136, 115, 154, 159, 4, 154, 106,
		110, 110, 108, 134, 115, 134, 230, 69, 111, 106,
		138, 114, 111, 131, 111, 240, 59, 233, 71, 198,
		111, 100, 139, 114, 145, 127, 1, 131, 244, 58,
		233, 72, 198, 110, 100, 139, 112, 145, 127, 1,
		131, 244, 58, 233, 74, 110, 107, 139, 112, 112,
		131, 113, 244, 59, 233, 76, 105, 48, 201
	})]
	public static int envelope_time_border_vector(SBR sbr, int ch)
	{
		int[] t_E_temp = new int[6];
		t_E_temp[0] = sbr.rate * sbr.abs_bord_lead[ch];
		t_E_temp[sbr.L_E[ch]] = sbr.rate * sbr.abs_bord_trail[ch];
		switch (sbr.bs_frame_class[ch])
		{
		case 0:
			switch (sbr.L_E[ch])
			{
			case 4:
			{
				int temp = sbr.numTimeSlots / 4;
				t_E_temp[3] = sbr.rate * 3 * temp;
				t_E_temp[2] = sbr.rate * 2 * temp;
				t_E_temp[1] = sbr.rate * temp;
				break;
			}
			case 2:
				t_E_temp[1] = sbr.rate * (sbr.numTimeSlots / 2);
				break;
			}
			break;
		case 1:
		{
			if (sbr.L_E[ch] <= 1)
			{
				break;
			}
			int i = sbr.L_E[ch];
			int border = sbr.abs_bord_trail[ch];
			for (int n = 0; n < sbr.L_E[ch] - 1; n++)
			{
				if (border < sbr.bs_rel_bord[ch][n])
				{
					return 1;
				}
				border -= sbr.bs_rel_bord[ch][n];
				i += -1;
				t_E_temp[i] = sbr.rate * border;
			}
			break;
		}
		case 2:
		{
			if (sbr.L_E[ch] <= 1)
			{
				break;
			}
			int j = 1;
			int border2 = sbr.abs_bord_lead[ch];
			for (int l2 = 0; l2 < sbr.L_E[ch] - 1; l2++)
			{
				border2 += sbr.bs_rel_bord[ch][l2];
				if (sbr.rate * border2 + sbr.tHFAdj > sbr.numTimeSlotsRate + sbr.tHFGen)
				{
					return 1;
				}
				int num2 = j;
				j++;
				t_E_temp[num2] = sbr.rate * border2;
			}
			break;
		}
		case 3:
		{
			if (sbr.bs_num_rel_0[ch] != 0)
			{
				int l = 1;
				int border4 = sbr.abs_bord_lead[ch];
				for (int l4 = 0; l4 < sbr.bs_num_rel_0[ch]; l4++)
				{
					border4 += sbr.bs_rel_bord_0[ch][l4];
					if (sbr.rate * border4 + sbr.tHFAdj > sbr.numTimeSlotsRate + sbr.tHFGen)
					{
						return 1;
					}
					int num = l;
					l++;
					t_E_temp[num] = sbr.rate * border4;
				}
			}
			if (sbr.bs_num_rel_1[ch] == 0)
			{
				break;
			}
			int k = sbr.L_E[ch];
			int border3 = sbr.abs_bord_trail[ch];
			for (int l3 = 0; l3 < sbr.bs_num_rel_1[ch]; l3++)
			{
				if (border3 < sbr.bs_rel_bord_1[ch][l3])
				{
					return 1;
				}
				border3 -= sbr.bs_rel_bord_1[ch][l3];
				k += -1;
				t_E_temp[k] = sbr.rate * border3;
			}
			break;
		}
		}
		for (int m = 0; m < 6; m++)
		{
			sbr.t_E[ch][m] = t_E_temp[m];
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 98, 149, 108, 117, 174, 105, 117, 156 })]
	public static void noise_floor_time_border_vector(SBR sbr, int ch)
	{
		sbr.t_Q[ch][0] = sbr.t_E[ch][0];
		if (sbr.L_E[ch] == 1)
		{
			sbr.t_Q[ch][1] = sbr.t_E[ch][1];
			sbr.t_Q[ch][2] = 0;
		}
		else
		{
			int index = middleBorder(sbr, ch);
			sbr.t_Q[ch][1] = sbr.t_E[ch][index];
			sbr.t_Q[ch][2] = sbr.t_E[ch][sbr.L_E[ch]];
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 112, 162, 131, 159, 4, 108, 134, 107, 101,
		108, 142, 108, 163, 108, 151, 204
	})]
	private static int middleBorder(SBR sbr, int ch)
	{
		int retval = 0;
		switch (sbr.bs_frame_class[ch])
		{
		case 0:
			retval = sbr.L_E[ch] / 2;
			break;
		case 2:
			retval = ((sbr.bs_pointer[ch] == 0) ? 1 : ((sbr.bs_pointer[ch] != 1) ? (sbr.bs_pointer[ch] - 1) : (sbr.L_E[ch] - 1)));
			break;
		case 1:
		case 3:
			retval = ((sbr.bs_pointer[ch] <= 1) ? (sbr.L_E[ch] - 1) : (sbr.L_E[ch] + 1 - sbr.bs_pointer[ch]));
			break;
		}
		return (retval > 0) ? retval : 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	internal TFGrid()
	{
	}
}
