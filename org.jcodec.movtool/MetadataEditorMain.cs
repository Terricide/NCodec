using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.tools;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.movtool;

public class MetadataEditorMain : Object
{
	private const string TYPENAME_FLOAT = "float";

	private const string TYPENAME_INT2 = "integer";

	private const string TYPENAME_INT = "int";

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_SET_KEYED;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_SET_ITUNES;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_SET_ITUNES_BLOB;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_QUERY;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_FAST;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_DROP_KEYED;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag FLAG_DROP_ITUNES;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag[] flags;

	[Signature("Ljava/util/Map<Ljava/lang/String;Ljava/lang/Integer;>;")]
	private static Map strToType;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/String;)Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	[LineNumberTable(new byte[]
	{
		159,
		94,
		66,
		103,
		126,
		111,
		113,
		byte.MaxValue,
		12,
		61,
		234,
		69
	})]
	private static Map parseMetaSpec(string flagSetKeyed)
	{
		HashMap map = new HashMap();
		string[] array = String.instancehelper_split(flagSetKeyed, ":");
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			string value = array[i];
			string[] lr = String.instancehelper_split(value, "=");
			string[] kt = String.instancehelper_split(lr[0], ",");
			((Map)map).put((object)kt[0], (object)typedValue(((nint)lr.LongLength <= 1) ? null : lr[1], ((nint)kt.LongLength <= 1) ? null : kt[1]));
		}
		return map;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 90, 66, 106, 99, 109 })]
	private static int stringToFourcc(string fourcc)
	{
		if (String.instancehelper_length(fourcc) != 4)
		{
			return 0;
		}
		byte[] bytes = Platform.getBytesForCharset(fourcc, "iso8859-1");
		int @int = ByteBuffer.wrap(bytes).order(ByteOrder.BIG_ENDIAN).getInt();
		
		return @int;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;)Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	[LineNumberTable(new byte[] { 159, 96, 66, 103, 127, 2, 127, 4, 99 })]
	private static Map toFourccMeta(Map keyed)
	{
		HashMap ret = new HashMap();
		Iterator iterator = keyed.entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			ret.put(Integer.valueOf(stringToFourcc((string)entry.getKey())), entry.getValue());
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 102, 98, 131, 100, 114, 238, 69, 76, 231,
		59, 131, 176, 74, 227, 61
	})]
	private static byte[] readStdin(string fileName)
	{
		FileInputStream fis = null;
		byte[] result;
		try
		{
			if (fileName != null)
			{
				
				fis = new FileInputStream(new File(fileName));
				result = IOUtils.toByteArray(fis);
				goto IL_0034;
			}
		}
		catch
		{
			//try-fault
			IOUtils.closeQuietly(fis);
			throw;
		}
		try
		{
			return IOUtils.toByteArray(Platform.stdin());
		}
		finally
		{
			IOUtils.closeQuietly(fis);
		}
		IL_0034:
		IOUtils.closeQuietly(fis);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 99, 162, 100, 98, 105, 147, 110 })]
	private static void printValue(MetaValue value)
	{
		if (value != null)
		{
			if (value.isBlob())
			{
				java.lang.System.@out.write(value.getData());
			}
			else
			{
				java.lang.System.@out.println(value);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 130, 104, 120 })]
	private static string fourccToString(int key)
	{
		byte[] bytes = new byte[4];
		ByteBuffer.wrap(bytes).order(ByteOrder.BIG_ENDIAN).putInt(key);
		string result = Platform.stringFromCharset(bytes, "iso8859-1");
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 162, 123, 111, 110, 111 })]
	private static MetaValue typedValue(string value, string type)
	{
		if (String.instancehelper_equalsIgnoreCase("int", type) || String.instancehelper_equalsIgnoreCase("integer", type))
		{
			MetaValue result = MetaValue.createInt(Integer.parseInt(value));
			
			return result;
		}
		if (String.instancehelper_equalsIgnoreCase("float", type))
		{
			MetaValue result2 = MetaValue.createFloat(Float.parseFloat(value));
			
			return result2;
		}
		MetaValue result3 = MetaValue.createString(value);
		
		return result3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(29)]
	public MetadataEditorMain()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 109, 106, 117, 105, 162, 120, 99,
		109, 100, 105, 110, 174, 110, 104, 111, 137, 122,
		51, 233, 69, 110, 104, 111, 137, 122, 106, 24,
		233, 70, 110, 101, 111, 110, 174, 110, 104, 111,
		113, 135, 105, 103, 150, 101, 117, 127, 8, 99,
		99, 223, 9, 100, 119, 184, 105, 104, 110, 104,
		112, 127, 7, 127, 29, 133, 212, 105, 104, 110,
		104, 112, 127, 7, 127, 39, 133, 191, 1
	})]
	public static void main(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, flags);
		if (cmd.argsLength() < 1)
		{
			MainUtils.printHelpCmdVa("metaedit", flags, "file name");
			java.lang.System.exit(-1);
			return;
		}
		
		MetadataEditor mediaMeta = MetadataEditor.createFrom(new File(cmd.getArg(0)));
		int save = 0;
		string flagSetKeyed = cmd.getStringFlag(FLAG_SET_KEYED);
		if (flagSetKeyed != null)
		{
			Map map2 = parseMetaSpec(flagSetKeyed);
			save |= ((map2.size() > 0) ? 1 : 0);
			mediaMeta.getKeyedMeta().putAll(map2);
		}
		string flagDropKeyed = cmd.getStringFlag(FLAG_DROP_KEYED);
		if (flagDropKeyed != null)
		{
			string[] keys2 = String.instancehelper_split(flagDropKeyed, ",");
			Map keyedMeta2 = mediaMeta.getKeyedMeta();
			string[] array = keys2;
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string key3 = array[i];
				save |= ((keyedMeta2.remove(key3) != null) ? 1 : 0);
			}
		}
		string flagDropItunes = cmd.getStringFlag(FLAG_DROP_ITUNES);
		if (flagDropItunes != null)
		{
			string[] keys = String.instancehelper_split(flagDropItunes, ",");
			Map itunesMeta2 = mediaMeta.getItunesMeta();
			string[] array2 = keys;
			int num2 = array2.Length;
			for (int j = 0; j < num2; j++)
			{
				string key2 = array2[j];
				int fourcc = stringToFourcc(key2);
				save |= ((itunesMeta2.remove(Integer.valueOf(fourcc)) != null) ? 1 : 0);
			}
		}
		string flagSetItunes = cmd.getStringFlag(FLAG_SET_ITUNES);
		if (flagSetItunes != null)
		{
			Map map = toFourccMeta(parseMetaSpec(flagSetItunes));
			save |= ((map.size() > 0) ? 1 : 0);
			mediaMeta.getItunesMeta().putAll(map);
		}
		string flagSetItunesBlob = cmd.getStringFlag(FLAG_SET_ITUNES_BLOB);
		if (flagSetItunesBlob != null)
		{
			string[] lr = String.instancehelper_split(flagSetItunesBlob, "=");
			string[] kt = String.instancehelper_split(lr[0], ",");
			string key = kt[0];
			Integer type = Integer.valueOf(1);
			if ((nint)kt.LongLength > 1)
			{
				type = (Integer)strToType.get(kt[1]);
			}
			if (type != null)
			{
				byte[] data = readStdin(((nint)lr.LongLength <= 1) ? null : lr[1]);
				mediaMeta.getItunesMeta().put(Integer.valueOf(stringToFourcc(key)), MetaValue.createOther(type.intValue(), data));
				save = 1;
			}
			else
			{
				java.lang.System.err.println(new StringBuilder().append("Unsupported metadata type: ").append(kt[1]).toString());
			}
		}
		if (save != 0)
		{
			mediaMeta.save(cmd.getBooleanFlag(FLAG_FAST).booleanValue());
			
			mediaMeta = MetadataEditor.createFrom(new File(cmd.getArg(0)));
		}
		Map keyedMeta = mediaMeta.getKeyedMeta();
		if (keyedMeta != null)
		{
			string flagQuery2 = cmd.getStringFlag(FLAG_QUERY);
			if (flagQuery2 == null)
			{
				java.lang.System.@out.println("Keyed metadata:");
				Iterator iterator = keyedMeta.entrySet().iterator();
				while (iterator.hasNext())
				{
					Map.Entry entry2 = (Map.Entry)iterator.next();
					java.lang.System.@out.println(new StringBuilder().append((string)entry2.getKey()).append(": ").append(entry2.getValue())
						.toString());
				}
			}
			else
			{
				printValue((MetaValue)keyedMeta.get(flagQuery2));
			}
		}
		Map itunesMeta = mediaMeta.getItunesMeta();
		if (itunesMeta == null)
		{
			return;
		}
		string flagQuery = cmd.getStringFlag(FLAG_QUERY);
		if (flagQuery == null)
		{
			java.lang.System.@out.println("iTunes metadata:");
			Iterator iterator2 = itunesMeta.entrySet().iterator();
			while (iterator2.hasNext())
			{
				Map.Entry entry = (Map.Entry)iterator2.next();
				java.lang.System.@out.println(new StringBuilder().append(fourccToString(((Integer)entry.getKey()).intValue())).append(": ").append(entry.getValue())
					.toString());
			}
		}
		else
		{
			printValue((MetaValue)itunesMeta.get(Integer.valueOf(stringToFourcc(flagQuery))));
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 134, 98, 154, 154, 154, 122, 191, 0, 154,
		154, 191, 37, 171, 119, 119, 120, 120, 120, 120,
		120, 120, 120
	})]
	static MetadataEditorMain()
	{
		FLAG_SET_KEYED = MainUtils.Flag.flag("set-keyed", "sk", "key1[,type1]=value1:key2[,type2]=value2[,...] Sets the metadata piece into a file.");
		FLAG_SET_ITUNES = MainUtils.Flag.flag("set-itunes", "si", "key1[,type1]=value1:key2[,type2]=value2[,...] Sets the metadata piece into a file.");
		FLAG_SET_ITUNES_BLOB = MainUtils.Flag.flag("set-itunes-blob", "sib", "key[,type]=file Sets the data read from a file into the metadata field 'key'. If file is not present stdin is read.");
		FLAG_QUERY = MainUtils.Flag.flag("query", "q", "Query the value of one key from the metadata set.");
		FLAG_FAST = new MainUtils.Flag("fast", "f", "Fast edit, will move the header to the end of the file when ther's no room to fit it.", MainUtils.FlagType.___003C_003EVOID);
		FLAG_DROP_KEYED = MainUtils.Flag.flag("drop-keyed", "dk", "Drop the field(s) from keyed metadata, format: key1,key2,key3,...");
		FLAG_DROP_ITUNES = MainUtils.Flag.flag("drop-itunes", "di", "Drop the field(s) from iTunes metadata, format: key1,key2,key3,...");
		flags = new MainUtils.Flag[7] { FLAG_SET_KEYED, FLAG_SET_ITUNES, FLAG_QUERY, FLAG_FAST, FLAG_SET_ITUNES_BLOB, FLAG_DROP_KEYED, FLAG_DROP_ITUNES };
		strToType = new HashMap();
		strToType.put("utf8", Integer.valueOf(1));
		strToType.put("utf16", Integer.valueOf(2));
		strToType.put("float", Integer.valueOf(23));
		strToType.put("int", Integer.valueOf(21));
		strToType.put("integer", Integer.valueOf(21));
		strToType.put("jpeg", Integer.valueOf(13));
		strToType.put("jpg", Integer.valueOf(13));
		strToType.put("png", Integer.valueOf(14));
		strToType.put("bmp", Integer.valueOf(27));
	}
}
