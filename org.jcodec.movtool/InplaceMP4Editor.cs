using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class InplaceMP4Editor : Object
{
	[SpecialName]
	[Signature("Ljava/lang/Object;Lorg/jcodec/common/Tuple$Mapper<Lorg/jcodec/containers/mp4/MP4Util$Atom;Ljava/lang/Long;>;")]
	[EnclosingMethod(null, "copy", "(Ljava.io.File;Ljava.io.File;Lorg.jcodec.movtool.MP4Edit;)Z")]
	internal class _1 : Object, Tuple.Mapper
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal InplaceMP4Editor this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(115)]
		public virtual Long map(MP4Util.Atom t)
		{
			Long result = Long.valueOf(t.getOffset());
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(113)]
		internal _1(InplaceMP4Editor this_00240)
		{
			this.this_00240 = this_00240;
			base._002Ector();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
		[LineNumberTable(113)]
		public virtual object map(object obj)
		{
			Long result = map((MP4Util.Atom)obj);
			
			return result;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(43)]
	public InplaceMP4Editor()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 127, 66, 131, 136, 106, 100, 233, 74, 76,
		231, 54, 195, 125, 127, 1, 131, 136, 74, 3
	})]
	public virtual bool modify(File file, MP4Edit edit)
	{
		FileChannelWrapper fi = null;
		List fragments;
		int result;
		try
		{
			fi = NIOUtils.rwChannel(file);
			fragments = doTheFix(fi, edit);
			if (fragments == null)
			{
				result = 0;
				goto IL_002f;
			}
		}
		catch
		{
			//try-fault
			NIOUtils.closeQuietly(fi);
			throw;
		}
		try
		{
			Iterator iterator = fragments.iterator();
			while (iterator.hasNext())
			{
				Tuple._2 fragment = (Tuple._2)iterator.next();
				replaceBox(fi, (MP4Util.Atom)fragment.___003C_003Ev0, (ByteBuffer)fragment.___003C_003Ev1);
			}
			return true;
		}
		finally
		{
			NIOUtils.closeQuietly(fi);
		}
		IL_002f:
		NIOUtils.closeQuietly(fi);
		return (byte)result != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/SeekableByteChannel;Lorg/jcodec/movtool/MP4Edit;)Ljava/util/List<Lorg/jcodec/common/Tuple$_2<Lorg/jcodec/containers/mp4/MP4Util$Atom;Ljava/nio/ByteBuffer;>;>;")]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 105, 136, 106, 142, 103, 113, 104,
		127, 7, 108, 113, 112, 105, 114, 134, 159, 0,
		127, 2, 127, 2, 99, 99, 99, 136, 107, 99,
		111
	})]
	private List doTheFix(org.jcodec.common.io.SeekableByteChannel fi, MP4Edit edit)
	{
		MP4Util.Atom moovAtom = getMoov(fi);
		Preconditions.checkNotNull(moovAtom);
		ByteBuffer moovBuffer = fetchBox(fi, moovAtom);
		MovieBox moovBox = (MovieBox)parseBox(moovBuffer);
		LinkedList fragments = new LinkedList();
		if (BoxUtil.containsBox(moovBox, "mvex"))
		{
			LinkedList temp = new LinkedList();
			Iterator iterator = getFragments(fi).iterator();
			while (iterator.hasNext())
			{
				MP4Util.Atom fragAtom = (MP4Util.Atom)iterator.next();
				ByteBuffer fragBuffer = fetchBox(fi, fragAtom);
				((List)fragments).add((object)Tuple.pair(fragAtom, fragBuffer));
				MovieFragmentBox fragBox = (MovieFragmentBox)parseBox(fragBuffer);
				fragBox.setMovie(moovBox);
				((List)temp).add((object)Tuple.pair(fragBuffer, fragBox));
			}
			edit.applyToFragment(moovBox, (MovieFragmentBox[])Tuple._2_project1(temp).toArray(new MovieFragmentBox[0]));
			Iterator iterator2 = ((List)temp).iterator();
			while (iterator2.hasNext())
			{
				Tuple._2 frag = (Tuple._2)iterator2.next();
				if (!rewriteBox((ByteBuffer)frag.___003C_003Ev0, (Box)frag.___003C_003Ev1))
				{
					return null;
				}
			}
		}
		else
		{
			edit.apply(moovBox);
		}
		if (!rewriteBox(moovBuffer, moovBox))
		{
			return null;
		}
		((List)fragments).add((object)Tuple.pair(moovAtom, moovBuffer));
		return fragments;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 94, 162, 110, 105 })]
	private void replaceBox(org.jcodec.common.io.SeekableByteChannel fi, MP4Util.Atom atom, ByteBuffer buffer)
	{
		fi.setPosition(atom.getOffset());
		fi.write(buffer);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 162, 99, 131, 104, 136, 106, 100, 233,
		84, 103, 44, 103, 231, 43, 131, 239, 71, 106,
		127, 6, 123, 101, 140, 106, 131, 136, 103, 42,
		3
	})]
	public virtual bool copy(File src, File dst, MP4Edit edit)
	{
		FileChannelWrapper fi = null;
		FileChannelWrapper fo = null;
		List fragments;
		int result;
		try
		{
			fi = NIOUtils.readableChannel(src);
			fo = NIOUtils.writableChannel(dst);
			fragments = doTheFix(fi, edit);
			if (fragments == null)
			{
				result = 0;
				goto IL_0041;
			}
		}
		catch
		{
			//try-fault
			NIOUtils.closeQuietly(fi);
			NIOUtils.closeQuietly(fo);
			throw;
		}
		try
		{
			List fragOffsets = Tuple._2map0(fragments, new _1(this));
			Map rewrite = Tuple.asMap(fragOffsets);
			Iterator iterator = MP4Util.getRootAtoms(fi).iterator();
			while (iterator.hasNext())
			{
				MP4Util.Atom atom = (MP4Util.Atom)iterator.next();
				ByteBuffer byteBuffer = (ByteBuffer)rewrite.get(Long.valueOf(atom.getOffset()));
				if (byteBuffer != null)
				{
					((WritableByteChannel)fo).write(byteBuffer);
				}
				else
				{
					atom.copy(fi, fo);
				}
			}
			return true;
		}
		finally
		{
			NIOUtils.closeQuietly(fi);
			NIOUtils.closeQuietly(fo);
		}
		IL_0041:
		NIOUtils.closeQuietly(fi);
		NIOUtils.closeQuietly(fo);
		return (byte)result != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 85, 98, 127, 2, 120, 131, 99 })]
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
	[LineNumberTable(new byte[] { 159, 88, 98, 110, 116 })]
	private ByteBuffer fetchBox(org.jcodec.common.io.SeekableByteChannel fi, MP4Util.Atom moov)
	{
		fi.setPosition(moov.getOffset());
		return NIOUtils.fetchFromChannel(fi, (int)moov.getHeader().getSize());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 162, 104, 110 })]
	private Box parseBox(ByteBuffer oldMov)
	{
		Header header = Header.read(oldMov);
		return BoxUtil.parseBox(oldMov, header, BoxFactory.getDefault());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/SeekableByteChannel;)Ljava/util/List<Lorg/jcodec/containers/mp4/MP4Util$Atom;>;")]
	[LineNumberTable(new byte[] { 159, 83, 130, 103, 127, 2, 120, 137, 99 })]
	private List getFragments(org.jcodec.common.io.SeekableByteChannel f)
	{
		LinkedList result = new LinkedList();
		Iterator iterator = MP4Util.getRootAtoms(f).iterator();
		while (iterator.hasNext())
		{
			MP4Util.Atom atom = (MP4Util.Atom)iterator.next();
			if (String.instancehelper_equals("moof", atom.getHeader().getFourcc()))
			{
				((List)result).add((object)atom);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 92, 98, 104, 104, 105, 106, 101, 110, 159,
		3, 104, 122, 98
	})]
	private bool rewriteBox(ByteBuffer buffer, Box box)
	{
		//Discarded unreachable code: IL_0065
		BufferOverflowException ex;
		try
		{
			buffer.clear();
			box.write(buffer);
			if (buffer.hasRemaining())
			{
				if (buffer.remaining() < 8)
				{
					return false;
				}
				buffer.putInt(buffer.remaining());
				buffer.put(new byte[4] { 102, 114, 101, 101 });
			}
			buffer.flip();
			return true;
		}
		catch (BufferOverflowException x)
		{
			ex = ByteCodeHelper.MapException<BufferOverflowException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		BufferOverflowException e = ex;
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 104, 98, 127, 18, 108, 105, 131 })]
	public virtual bool replace(File src, MP4Edit edit)
	{
		
		File tmp = new File(src.getParentFile(), new StringBuilder().append(".").append(src.getName()).toString());
		if (copy(src, tmp, edit))
		{
			tmp.renameTo(src);
			return true;
		}
		return false;
	}
}
