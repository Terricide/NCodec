using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.huffman;

public interface HCB
{
	[HideFromJava]
	public static class __Fields
	{
		public const int ZERO_HCB = 0;

		public const int ESCAPE_HCB = 11;

		public const int NOISE_HCB = 13;

		public const int INTENSITY_HCB2 = 14;

		public const int INTENSITY_HCB = 15;

		public const int FIRST_PAIR_HCB = 5;
	}

	const int ZERO_HCB = 0;

	const int ESCAPE_HCB = 11;

	const int NOISE_HCB = 13;

	const int INTENSITY_HCB2 = 14;

	const int INTENSITY_HCB = 15;

	const int FIRST_PAIR_HCB = 5;
}
