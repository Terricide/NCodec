using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.nio;
using java.util;

namespace org.jcodec.containers.mp4.boxes;

public class EditListBox : FullBox
{
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	private List edits;

	[LineNumberTable(18)]
	public static string fourcc()
	{
		return "elst";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public EditListBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;)Lorg/jcodec/containers/mp4/boxes/EditListBox;")]
	[LineNumberTable(new byte[] { 159, 137, 130, 118, 104 })]
	public static EditListBox createEditListBox(List edits)
	{
		
		EditListBox elst = new EditListBox(new Header(fourcc()));
		elst.edits = edits;
		return elst;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 136, 108, 105, 104, 104, 104, 113,
		248, 60, 231, 70
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		edits = new ArrayList();
		long num = input.getInt();
		for (int i = 0; i < num; i++)
		{
			int duration = input.getInt();
			int mediaTime = input.getInt();
			float rate = (float)input.getInt() / 65536f;
			edits.add(new Edit(duration, mediaTime, rate));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 136, 115, 127, 2, 111, 111, 122,
		99
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(edits.size());
		Iterator iterator = edits.iterator();
		while (iterator.hasNext())
		{
			Edit edit = (Edit)iterator.next();
			@out.putInt((int)edit.getDuration());
			@out.putInt((int)edit.getMediaTime());
			@out.putInt(ByteCodeHelper.f2i(edit.getRate() * 65536f));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	public override int estimateSize()
	{
		return 16 + edits.size() * 12;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Edit;>;")]
	[LineNumberTable(61)]
	public virtual List getEdits()
	{
		return edits;
	}
}
