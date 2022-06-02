using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.api;

namespace org.jcodec.codecs.vpx;

public class FilterUtil : Object
{
	public class Segment : Object
	{
		internal int p0;

		internal int p1;

		internal int p2;

		internal int p3;

		internal int q0;

		internal int q1;

		internal int q2;

		internal int q3;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(25)]
		public Segment()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 124, 98, 103, 114, 114, 114, 114, 114, 114,
			114, 114
		})]
		public virtual Segment getSigned()
		{
			Segment seg = new Segment();
			seg.p3 = access_0024000(p3);
			seg.p2 = access_0024000(p2);
			seg.p1 = access_0024000(p1);
			seg.p0 = access_0024000(p0);
			seg.q0 = access_0024000(q0);
			seg.q1 = access_0024000(q1);
			seg.q2 = access_0024000(q2);
			seg.q3 = access_0024000(q3);
			return seg;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 130, 66, 127, 30, 121, 118, 118, 118, 118,
			237, 58
		})]
		public virtual bool isFilterRequired(int interior, int edge)
		{
			return ((Math.abs(p0 - q0) << 2) + (Math.abs(p1 - q1) >> 2) <= edge && Math.abs(p3 - p2) <= interior && Math.abs(p2 - p1) <= interior && Math.abs(p1 - p0) <= interior && Math.abs(q3 - q2) <= interior && Math.abs(q2 - q1) <= interior && Math.abs(q1 - q0) <= interior) ? true : false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(69)]
		public virtual bool isHighVariance(int threshold)
		{
			return (Math.abs(p1 - p0) > threshold || Math.abs(q1 - q0) > threshold) ? true : false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 92, 65, 67, 109, 109, 109, 238, 74, 253,
			70, 238, 70, 238, 69, 240, 71, 144
		})]
		private int adjust(bool use_outer_taps)
		{
			int p1 = access_0024000(this.p1);
			int p0 = access_0024000(this.p0);
			int q0 = access_0024000(this.q0);
			int q1 = access_0024000(this.q1);
			int a = access_0024100((use_outer_taps ? access_0024100(p1 - q1) : 0) + 3 * (q0 - p0));
			int b = access_0024100(a + 3) >> 3;
			a = access_0024100(a + 4) >> 3;
			this.q0 = access_0024200(q0 - a);
			this.p0 = access_0024200(p0 + b);
			return a;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 121, 130, 103, 114, 113, 113, 113, 113, 113,
			113, 114
		})]
		public static Segment horizontal(VPXMacroblock.Subblock right, VPXMacroblock.Subblock left, int a)
		{
			Segment seg = new Segment();
			seg.p0 = left.val[12 + a];
			seg.p1 = left.val[8 + a];
			seg.p2 = left.val[4 + a];
			seg.p3 = left.val[0 + a];
			seg.q0 = right.val[0 + a];
			seg.q1 = right.val[4 + a];
			seg.q2 = right.val[8 + a];
			seg.q3 = right.val[12 + a];
			return seg;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 118, 162, 103, 115, 115, 115, 115, 115, 115,
			115, 115
		})]
		public static Segment vertical(VPXMacroblock.Subblock lower, VPXMacroblock.Subblock upper, int a)
		{
			Segment seg = new Segment();
			seg.p0 = upper.val[a * 4 + 3];
			seg.p1 = upper.val[a * 4 + 2];
			seg.p2 = upper.val[a * 4 + 1];
			seg.p3 = upper.val[a * 4 + 0];
			seg.q0 = lower.val[a * 4 + 0];
			seg.q1 = lower.val[a * 4 + 1];
			seg.q2 = lower.val[a * 4 + 2];
			seg.q3 = lower.val[a * 4 + 3];
			return seg;
		}

		[LineNumberTable(new byte[]
		{
			159, 114, 66, 114, 113, 113, 113, 113, 113, 113,
			146
		})]
		public virtual void applyHorizontally(VPXMacroblock.Subblock right, VPXMacroblock.Subblock left, int a)
		{
			left.val[12 + a] = p0;
			left.val[8 + a] = p1;
			left.val[4 + a] = p2;
			left.val[0 + a] = p3;
			right.val[0 + a] = q0;
			right.val[4 + a] = q1;
			right.val[8 + a] = q2;
			right.val[12 + a] = q3;
		}

		[LineNumberTable(new byte[]
		{
			159, 111, 66, 115, 115, 115, 115, 115, 115, 115,
			147
		})]
		public virtual void applyVertically(VPXMacroblock.Subblock lower, VPXMacroblock.Subblock upper, int a)
		{
			upper.val[a * 4 + 3] = p0;
			upper.val[a * 4 + 2] = p1;
			upper.val[a * 4 + 1] = p2;
			upper.val[a * 4 + 0] = p3;
			lower.val[a * 4 + 0] = q0;
			lower.val[a * 4 + 1] = q1;
			lower.val[a * 4 + 2] = q2;
			lower.val[a * 4 + 3] = q3;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159,
			107,
			162,
			104,
			110,
			173,
			byte.MaxValue,
			10,
			69,
			139,
			116,
			148,
			139,
			116,
			148,
			107,
			116,
			116,
			131,
			137
		})]
		internal virtual void filterMb(int hevThreshold, int interiorLimit, int edgeLimit)
		{
			Segment signedSeg = getSigned();
			if (signedSeg.isFilterRequired(interiorLimit, edgeLimit))
			{
				if (!signedSeg.isHighVariance(hevThreshold))
				{
					int w = access_0024100(access_0024100(signedSeg.p1 - signedSeg.q1) + 3 * (signedSeg.q0 - signedSeg.p0));
					int a = 27 * w + 63 >> 7;
					q0 = access_0024200(signedSeg.q0 - a);
					p0 = access_0024200(signedSeg.p0 + a);
					a = 18 * w + 63 >> 7;
					q1 = access_0024200(signedSeg.q1 - a);
					p1 = access_0024200(signedSeg.p1 + a);
					a = 9 * w + 63 >> 7;
					q2 = access_0024200(signedSeg.q2 - a);
					p2 = access_0024200(signedSeg.p2 + a);
				}
				else
				{
					adjust(use_outer_taps: true);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 97, 130, 104, 107, 105, 109, 100, 116, 180 })]
		public virtual void filterSb(int hev_threshold, int interior_limit, int edge_limit)
		{
			Segment signedSeg = getSigned();
			if (signedSeg.isFilterRequired(interior_limit, edge_limit))
			{
				int hv = (signedSeg.isHighVariance(hev_threshold) ? 1 : 0);
				int a = adjust((byte)hv != 0) + 1 >> 1;
				if (hv == 0)
				{
					q1 = access_0024200(signedSeg.q1 - a);
					p1 = access_0024200(signedSeg.p1 + a);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(16)]
	internal static int access_0024000(int x0)
	{
		int result = minus128(x0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(16)]
	internal static int access_0024100(int x0)
	{
		int result = clipSigned(x0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(16)]
	internal static int access_0024200(int x0)
	{
		int result = clipPlus128(x0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	private static int clipPlus128(int v)
	{
		return clipSigned(v) + 128;
	}

	[LineNumberTable(245)]
	private static int clipSigned(int v)
	{
		return (v < -128) ? (-128) : ((v <= 127) ? v : 127);
	}

	[LineNumberTable(250)]
	private static int minus128(int v)
	{
		return v - 128;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public FilterUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 79, 129, 67, 109, 111, 107, 108, 105, 104,
		105, 101, 113, 105, 135, 101, 132, 100, 100, 103,
		102, 103, 134, 241, 74, 140, 138, 104, 110, 108,
		110, 111, 110, 111, 108, 110, 110, 110, 110, 110,
		238, 58, 236, 59, 236, 82, 108, 108, 108, 113,
		111, 113, 111, 108, 110, 110, 110, 110, 110, 238,
		58, 236, 59, 44, 236, 82, 104, 110, 108, 111,
		111, 111, 111, 140, 110, 110, 110, 110, 110, 238,
		57, 236, 59, 236, 82, 108, 108, 108, 114, 112,
		114, 112, 108, 110, 110, 110, 110, 110, 238, 58,
		236, 59, 44, 236, 159, 161, 42, 234, 160, 116
	})]
	public static void loopFilterUV(VPXMacroblock[][] mbs, int sharpnessLevel, bool keyFrame)
	{
		for (int y = 0; y < (nint)mbs.LongLength - 2; y++)
		{
			for (int x = 0; x < (nint)mbs[0].LongLength - 2; x++)
			{
				VPXMacroblock rmb = mbs[y + 1][x + 1];
				VPXMacroblock bmb = mbs[y + 1][x + 1];
				int loop_filter_level = rmb.filterLevel;
				if (loop_filter_level == 0)
				{
					continue;
				}
				int interior_limit = rmb.filterLevel;
				if (sharpnessLevel > 0)
				{
					interior_limit >>= ((sharpnessLevel <= 4) ? 1 : 2);
					if (interior_limit > 9 - sharpnessLevel)
					{
						interior_limit = 9 - sharpnessLevel;
					}
				}
				if (interior_limit == 0)
				{
					interior_limit = 1;
				}
				int hev_threshold = 0;
				if (keyFrame)
				{
					if (loop_filter_level >= 40)
					{
						hev_threshold = 2;
					}
					else if (loop_filter_level >= 15)
					{
						hev_threshold = 1;
					}
					int mbedge_limit = (loop_filter_level + 2) * 2 + interior_limit;
					int sub_bedge_limit = loop_filter_level * 2 + interior_limit;
					if (x > 0)
					{
						VPXMacroblock lmb = mbs[y + 1][x + 1 - 1];
						for (int b4 = 0; b4 < 2; b4++)
						{
							VPXMacroblock.Subblock rsbU2 = rmb.___003C_003EuSubblocks[b4][0];
							VPXMacroblock.Subblock lsbU2 = lmb.___003C_003EuSubblocks[b4][1];
							VPXMacroblock.Subblock rsbV2 = rmb.___003C_003EvSubblocks[b4][0];
							VPXMacroblock.Subblock lsbV2 = lmb.___003C_003EvSubblocks[b4][1];
							for (int a4 = 0; a4 < 4; a4++)
							{
								Segment seg4 = Segment.horizontal(rsbU2, lsbU2, a4);
								seg4.filterMb(hev_threshold, interior_limit, mbedge_limit);
								seg4.applyHorizontally(rsbU2, lsbU2, a4);
								seg4 = Segment.horizontal(rsbV2, lsbV2, a4);
								seg4.filterMb(hev_threshold, interior_limit, mbedge_limit);
								seg4.applyHorizontally(rsbV2, lsbV2, a4);
							}
						}
					}
					if (!rmb.skipFilter)
					{
						for (int a3 = 1; a3 < 2; a3++)
						{
							for (int b3 = 0; b3 < 2; b3++)
							{
								VPXMacroblock.Subblock lsbU = rmb.___003C_003EuSubblocks[b3][a3 - 1];
								VPXMacroblock.Subblock rsbU = rmb.___003C_003EuSubblocks[b3][a3];
								VPXMacroblock.Subblock lsbV = rmb.___003C_003EvSubblocks[b3][a3 - 1];
								VPXMacroblock.Subblock rsbV = rmb.___003C_003EvSubblocks[b3][a3];
								for (int c2 = 0; c2 < 4; c2++)
								{
									Segment seg3 = Segment.horizontal(rsbU, lsbU, c2);
									seg3.filterSb(hev_threshold, interior_limit, sub_bedge_limit);
									seg3.applyHorizontally(rsbU, lsbU, c2);
									seg3 = Segment.horizontal(rsbV, lsbV, c2);
									seg3.filterSb(hev_threshold, interior_limit, sub_bedge_limit);
									seg3.applyHorizontally(rsbV, lsbV, c2);
								}
							}
						}
					}
					if (y > 0)
					{
						VPXMacroblock tmb = mbs[y + 1 - 1][x + 1];
						for (int b2 = 0; b2 < 2; b2++)
						{
							VPXMacroblock.Subblock tsbU2 = tmb.___003C_003EuSubblocks[1][b2];
							VPXMacroblock.Subblock bsbU2 = bmb.___003C_003EuSubblocks[0][b2];
							VPXMacroblock.Subblock tsbV2 = tmb.___003C_003EvSubblocks[1][b2];
							VPXMacroblock.Subblock bsbV2 = bmb.___003C_003EvSubblocks[0][b2];
							for (int a2 = 0; a2 < 4; a2++)
							{
								Segment seg2 = Segment.vertical(bsbU2, tsbU2, a2);
								seg2.filterMb(hev_threshold, interior_limit, mbedge_limit);
								seg2.applyVertically(bsbU2, tsbU2, a2);
								seg2 = Segment.vertical(bsbV2, tsbV2, a2);
								seg2.filterMb(hev_threshold, interior_limit, mbedge_limit);
								seg2.applyVertically(bsbV2, tsbV2, a2);
							}
						}
					}
					if (rmb.skipFilter)
					{
						continue;
					}
					for (int a = 1; a < 2; a++)
					{
						for (int b = 0; b < 2; b++)
						{
							VPXMacroblock.Subblock tsbU = bmb.___003C_003EuSubblocks[a - 1][b];
							VPXMacroblock.Subblock bsbU = bmb.___003C_003EuSubblocks[a][b];
							VPXMacroblock.Subblock tsbV = bmb.___003C_003EvSubblocks[a - 1][b];
							VPXMacroblock.Subblock bsbV = bmb.___003C_003EvSubblocks[a][b];
							for (int c = 0; c < 4; c++)
							{
								Segment seg = Segment.vertical(bsbU, tsbU, c);
								seg.filterSb(hev_threshold, interior_limit, sub_bedge_limit);
								seg.applyVertically(bsbU, tsbU, c);
								seg = Segment.vertical(bsbV, tsbV, c);
								seg.filterSb(hev_threshold, interior_limit, sub_bedge_limit);
								seg.applyVertically(bsbV, tsbV, c);
							}
						}
					}
					continue;
				}
				
				throw new NotImplementedException("TODO: non-key frames are not supported yet.");
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 49, 97, 67, 109, 111, 107, 108, 137, 104,
		137, 101, 113, 105, 135, 101, 132, 100, 100, 103,
		102, 103, 134, 241, 76, 204, 170, 104, 110, 108,
		110, 111, 105, 110, 110, 238, 61, 233, 61, 236,
		75, 108, 108, 108, 113, 111, 105, 110, 110, 238,
		61, 233, 61, 44, 236, 77, 104, 110, 108, 111,
		111, 105, 110, 110, 238, 61, 233, 61, 236, 75,
		108, 108, 108, 114, 112, 105, 110, 110, 238, 61,
		233, 61, 44, 236, 159, 173, 42, 234, 160, 99
	})]
	public static void loopFilterY(VPXMacroblock[][] mbs, int sharpnessLevel, bool keyFrame)
	{
		for (int y = 0; y < (nint)mbs.LongLength - 2; y++)
		{
			for (int x = 0; x < (nint)mbs[0].LongLength - 2; x++)
			{
				VPXMacroblock rmb = mbs[y + 1][x + 1];
				VPXMacroblock bmb = mbs[y + 1][x + 1];
				int loopFilterLevel = rmb.filterLevel;
				if (loopFilterLevel == 0)
				{
					continue;
				}
				int interiorLimit = rmb.filterLevel;
				if (sharpnessLevel > 0)
				{
					interiorLimit >>= ((sharpnessLevel <= 4) ? 1 : 2);
					if (interiorLimit > 9 - sharpnessLevel)
					{
						interiorLimit = 9 - sharpnessLevel;
					}
				}
				if (interiorLimit == 0)
				{
					interiorLimit = 1;
				}
				int varianceThreshold = 0;
				if (keyFrame)
				{
					if (loopFilterLevel >= 40)
					{
						varianceThreshold = 2;
					}
					else if (loopFilterLevel >= 15)
					{
						varianceThreshold = 1;
					}
					int edgeLimitMb = (loopFilterLevel + 2) * 2 + interiorLimit;
					int edgeLimitSb = loopFilterLevel * 2 + interiorLimit;
					if (x > 0)
					{
						VPXMacroblock lmb = mbs[y + 1][x - 1 + 1];
						for (int b4 = 0; b4 < 4; b4++)
						{
							VPXMacroblock.Subblock rsb2 = rmb.___003C_003EySubblocks[b4][0];
							VPXMacroblock.Subblock lsb2 = lmb.___003C_003EySubblocks[b4][3];
							for (int a4 = 0; a4 < 4; a4++)
							{
								Segment seg4 = Segment.horizontal(rsb2, lsb2, a4);
								seg4.filterMb(varianceThreshold, interiorLimit, edgeLimitMb);
								seg4.applyHorizontally(rsb2, lsb2, a4);
							}
						}
					}
					if (!rmb.skipFilter)
					{
						for (int a3 = 1; a3 < 4; a3++)
						{
							for (int b3 = 0; b3 < 4; b3++)
							{
								VPXMacroblock.Subblock lsb = rmb.___003C_003EySubblocks[b3][a3 - 1];
								VPXMacroblock.Subblock rsb = rmb.___003C_003EySubblocks[b3][a3];
								for (int c2 = 0; c2 < 4; c2++)
								{
									Segment seg3 = Segment.horizontal(rsb, lsb, c2);
									seg3.filterSb(varianceThreshold, interiorLimit, edgeLimitSb);
									seg3.applyHorizontally(rsb, lsb, c2);
								}
							}
						}
					}
					if (y > 0)
					{
						VPXMacroblock tmb = mbs[y - 1 + 1][x + 1];
						for (int b2 = 0; b2 < 4; b2++)
						{
							VPXMacroblock.Subblock tsb2 = tmb.___003C_003EySubblocks[3][b2];
							VPXMacroblock.Subblock bsb2 = bmb.___003C_003EySubblocks[0][b2];
							for (int a2 = 0; a2 < 4; a2++)
							{
								Segment seg2 = Segment.vertical(bsb2, tsb2, a2);
								seg2.filterMb(varianceThreshold, interiorLimit, edgeLimitMb);
								seg2.applyVertically(bsb2, tsb2, a2);
							}
						}
					}
					if (rmb.skipFilter)
					{
						continue;
					}
					for (int a = 1; a < 4; a++)
					{
						for (int b = 0; b < 4; b++)
						{
							VPXMacroblock.Subblock tsb = bmb.___003C_003EySubblocks[a - 1][b];
							VPXMacroblock.Subblock bsb = bmb.___003C_003EySubblocks[a][b];
							for (int c = 0; c < 4; c++)
							{
								Segment seg = Segment.vertical(bsb, tsb, c);
								seg.filterSb(varianceThreshold, interiorLimit, edgeLimitSb);
								seg.applyVertically(bsb, tsb, c);
							}
						}
					}
					continue;
				}
				
				throw new NotImplementedException("TODO: non-key frames are not supported yet");
			}
		}
	}
}
