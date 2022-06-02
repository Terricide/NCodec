using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class RelocateMP4Editor : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public RelocateMP4Editor()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 98, 110, 100, 107 })]
	public virtual void modifyOrRelocate(File src, MP4Edit edit)
	{
		if (!new InplaceMP4Editor().modify(src, edit))
		{
			relocate(src, edit);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 131, 104, 105, 106, 142, 136, 123,
		107, 113, 114, 144, 142, 140, 74, 99, 99
	})]
	public virtual void relocate(File src, MP4Edit edit)
	{
		FileChannelWrapper f = null;
		try
		{
			f = NIOUtils.rwChannel(src);
			MP4Util.Atom moovAtom = getMoov(f);
			ByteBuffer moovBuffer = fetchBox(f, moovAtom);
			MovieBox moovBox = (MovieBox)parseBox(moovBuffer);
			edit.apply(moovBox);
			if (moovAtom.getOffset() + moovAtom.getHeader().getSize() < ((org.jcodec.common.io.SeekableByteChannel)f).size())
			{
				Logger.info("Relocating movie header to the end of the file.");
				((org.jcodec.common.io.SeekableByteChannel)f).setPosition(moovAtom.getOffset() + 4u);
				((WritableByteChannel)f).write(ByteBuffer.wrap(Header.___003C_003EFOURCC_FREE));
				((org.jcodec.common.io.SeekableByteChannel)f).setPosition(((org.jcodec.common.io.SeekableByteChannel)f).size());
			}
			else
			{
				((org.jcodec.common.io.SeekableByteChannel)f).setPosition(moovAtom.getOffset());
			}
			MP4Util.writeMovie(f, moovBox);
		}
		finally
		{
			NIOUtils.closeQuietly(f);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 162, 127, 2, 120, 131, 99 })]
	private MP4Util.Atom getMoov(org.jcodec.common.io.SeekableByteChannel f)
	{
		Iterator iterator = MP4Util.getRootAtoms(f).iterator();
		while (iterator.hasNext())
		{
			MP4Util.Atom atom = (MP4Util.Atom)iterator.next();
			if (String.instancehelper_equals("moov", atom.getHeader().getFourcc()))
			{
				return atom;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 128, 162, 110, 116 })]
	private ByteBuffer fetchBox(org.jcodec.common.io.SeekableByteChannel fi, MP4Util.Atom moov)
	{
		fi.setPosition(moov.getOffset());
		return NIOUtils.fetchFromChannel(fi, (int)moov.getHeader().getSize());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 98, 104, 110 })]
	private Box parseBox(ByteBuffer oldMov)
	{
		Header header = Header.read(oldMov);
		return BoxUtil.parseBox(oldMov, header, BoxFactory.getDefault());
	}
}
