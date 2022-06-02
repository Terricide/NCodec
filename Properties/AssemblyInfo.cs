using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using IKVM.Attributes;

[assembly: InternalsVisibleTo("jcodec-0.2.5-ikvm-runtime-injected, PublicKey=00240000048000009400000006020000002400005253413100040000010001009D674F3D63B8D7A4C428BD7388341B025C71AA61C6224CD53A12C21330A3159D300051FE2EED154FE30D70673A079E4529D0FD78113DCA771DA8B0C1EF2F77B73651D55645B0A4294F0AF9BF7078432E13D0F46F951D712C2FCF02EB15552C0FE7817FC0AED58E0984F86661BF64D882F29B619899DD264041E7D4992548EB9E")]
[assembly: AssemblyVersion("0.0.0.0")]
[module: SourceFile(null)]
[module: JavaModule(Jars = new string[] { "jcodec-0.2.5.jar" })]
[module: PackageList("jcodec-0.2.5.jar", new string[]
{
	"net.sourceforge.jaad.aac.syntax", "net.sourceforge.jaad.aac", "net.sourceforge.jaad.aac.error", "net.sourceforge.jaad.aac.filterbank", "net.sourceforge.jaad.aac.gain", "net.sourceforge.jaad.aac.huffman", "net.sourceforge.jaad.aac.ps", "net.sourceforge.jaad.aac.sbr", "net.sourceforge.jaad.aac.tools", "net.sourceforge.jaad.aac.transport",
	"org.jcodec.algo", "org.jcodec.api", "org.jcodec.api.specific", "org.jcodec.api.transcode", "org.jcodec.api.transcode.filters", "org.jcodec.common", "org.jcodec.audio", "org.jcodec.codecs.aac", "org.jcodec.codecs.aac.blocks", "org.jcodec.platform",
	"org.jcodec.codecs.common.biari", "org.jcodec.codecs.h264", "org.jcodec.codecs.h264.decode.aso", "org.jcodec.codecs.h264.decode", "org.jcodec.codecs.h264.decode.deblock", "org.jcodec.codecs.h264.encode", "org.jcodec.codecs.h264.io", "org.jcodec.codecs.h264.io.model", "org.jcodec.common.model", "org.jcodec.codecs.h264.io.write",
	"org.jcodec.containers.mp4.boxes", "org.jcodec.codecs.h264.mp4", "org.jcodec.codecs.mjpeg", "org.jcodec.codecs.mjpeg.tools", "org.jcodec.codecs.mpa", "org.jcodec.codecs.mpeg12.bitstream", "org.jcodec.codecs.mpeg12", "org.jcodec.containers.mps", "org.jcodec.common.io", "org.jcodec.codecs.mpeg4.es",
	"org.jcodec.codecs.mpeg4", "org.jcodec.codecs.mpeg4.mp4", "org.jcodec.codecs.pcmdvd", "org.jcodec.codecs.png", "org.jcodec.codecs.ppm", "org.jcodec.codecs.prores", "org.jcodec.codecs.raw", "org.jcodec.codecs.s302", "org.jcodec.codecs.vpx", "org.jcodec.codecs.vpx.vp9",
	"org.jcodec.codecs.wav", "org.jcodec.codecs.y4m", "org.jcodec.common.dct", "org.jcodec.common.logging", "org.jcodec.common.tools", "org.jcodec.containers.avi", "org.jcodec.containers.dpx", "org.jcodec.containers.flv", "org.jcodec.containers.imgseq", "org.jcodec.containers.mkv.boxes",
	"org.jcodec.containers.mkv", "org.jcodec.containers.mkv.demuxer", "org.jcodec.containers.mkv.muxer", "org.jcodec.containers.mkv.util", "org.jcodec.containers.mp3", "org.jcodec.containers.mp4", "org.jcodec.containers.mp4.boxes.channel", "org.jcodec.containers.mp4.demuxer", "org.jcodec.containers.mp4.muxer", "org.jcodec.containers.mps.index",
	"org.jcodec.containers.mps.psi", "org.jcodec.containers.mxf.model", "org.jcodec.containers.mxf", "org.jcodec.containers.raw", "org.jcodec.containers.webp", "org.jcodec.containers.y4m", "org.jcodec.filters.color", "org.jcodec.movtool", "org.jcodec.scale", "org.jcodec.scale.highbd",
	"org.jcodec.testing"
})]
