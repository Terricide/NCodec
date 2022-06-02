using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.syntax;

public interface IBitStream
{
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	int readBits(int i);

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	bool readBool();

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	void skipBit();

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	void skipBits(int i);

	int getBitsLeft();

	void destroy();

	void setData(byte[] barr);

	int getPosition();

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	int readBit();

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	void byteAlign();

	void reset();

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	int peekBits(int i);

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	int peekBit();

	int maskBits(int i);
}
