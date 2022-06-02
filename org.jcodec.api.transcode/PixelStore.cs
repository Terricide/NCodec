using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public interface PixelStore
{
	public class LoanerPicture : Object
	{
		private Picture picture;

		private int refCnt;

		[LineNumberTable(17)]
		public virtual Picture getPicture()
		{
			return picture;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 140, 162, 105, 104, 104 })]
		public LoanerPicture(Picture picture, int refCnt)
		{
			this.picture = picture;
			this.refCnt = refCnt;
		}

		[LineNumberTable(21)]
		public virtual int getRefCnt()
		{
			return refCnt;
		}

		[LineNumberTable(new byte[] { 159, 136, 98, 111 })]
		public virtual void decRefCnt()
		{
			refCnt--;
		}

		[LineNumberTable(29)]
		public virtual bool unused()
		{
			return refCnt <= 0;
		}

		[LineNumberTable(new byte[] { 159, 134, 98, 111 })]
		public virtual void incRefCnt()
		{
			refCnt++;
		}
	}

	LoanerPicture getPicture(int i1, int i2, ColorSpace cs);

	void putBack(LoanerPicture pslp);

	void retake(LoanerPicture pslp);
}
