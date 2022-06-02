using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.syntax;

public interface SyntaxConstants
{
	[HideFromJava]
	public static class __Fields
	{
		public const int MAX_ELEMENTS = 16;

		public const int BYTE_MASK = 255;

		public const int MIN_INPUT_SIZE = 768;

		public const int WINDOW_LEN_LONG = 1024;

		public const int WINDOW_LEN_SHORT = 128;

		public const int WINDOW_SMALL_LEN_LONG = 960;

		public const int WINDOW_SMALL_LEN_SHORT = 120;

		public const int ELEMENT_SCE = 0;

		public const int ELEMENT_CPE = 1;

		public const int ELEMENT_CCE = 2;

		public const int ELEMENT_LFE = 3;

		public const int ELEMENT_DSE = 4;

		public const int ELEMENT_PCE = 5;

		public const int ELEMENT_FIL = 6;

		public const int ELEMENT_END = 7;

		public const int MAX_WINDOW_COUNT = 8;

		public const int MAX_WINDOW_GROUP_COUNT = 8;

		public const int MAX_LTP_SFB = 40;

		public const int MAX_SECTIONS = 120;

		public const int MAX_MS_MASK = 128;

		public const float SQRT2 = 1.41421354f;
	}

	const int MAX_ELEMENTS = 16;

	const int BYTE_MASK = 255;

	const int MIN_INPUT_SIZE = 768;

	const int WINDOW_LEN_LONG = 1024;

	const int WINDOW_LEN_SHORT = 128;

	const int WINDOW_SMALL_LEN_LONG = 960;

	const int WINDOW_SMALL_LEN_SHORT = 120;

	const int ELEMENT_SCE = 0;

	const int ELEMENT_CPE = 1;

	const int ELEMENT_CCE = 2;

	const int ELEMENT_LFE = 3;

	const int ELEMENT_DSE = 4;

	const int ELEMENT_PCE = 5;

	const int ELEMENT_FIL = 6;

	const int ELEMENT_END = 7;

	const int MAX_WINDOW_COUNT = 8;

	const int MAX_WINDOW_GROUP_COUNT = 8;

	const int MAX_LTP_SFB = 40;

	const int MAX_SECTIONS = 120;

	const int MAX_MS_MASK = 128;

	const float SQRT2 = 1.41421354f;
}
