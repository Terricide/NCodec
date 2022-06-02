using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.codecs.h264.io.model;

public class RefPicMarkingIDR : Object
{
	internal bool discardDecodedPics;

	internal bool useForlongTerm;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 161, 69, 105, 104, 104 })]
	public RefPicMarkingIDR(bool discardDecodedPics, bool useForlongTerm)
	{
		this.discardDecodedPics = discardDecodedPics;
		this.useForlongTerm = useForlongTerm;
	}

	[LineNumberTable(29)]
	public virtual bool isUseForlongTerm()
	{
		return useForlongTerm;
	}

	[LineNumberTable(25)]
	public virtual bool isDiscardDecodedPics()
	{
		return discardDecodedPics;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(34)]
	public override string toString()
	{
		string result = Platform.toJSON(this);
		
		return result;
	}
}
