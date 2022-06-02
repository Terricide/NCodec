using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public interface Filter
{
	PixelStore.LoanerPicture filter(Picture p, PixelStore ps);

	ColorSpace getInputColor();

	ColorSpace getOutputColor();
}
