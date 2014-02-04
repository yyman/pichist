using System;

namespace RGBHSL
{
	/// <summary>
	/// RGBHSL ‚Í@RGB‹óŠÔ‚ÆHSL‹óŠÔ‚ğ‘ŠŒİ‚É•ÏŠ·‚·‚é
	/// </summary>
	public class RGBHSL
	{
		// hue 0..360  saturation 0..255  luminance 0..255 for r,g,b 0..255

		public static void HSLToRGB(int h, int s, int l, out int rr, out int gg, out int bb)
		{
			int hh = h;
			double ss = s / 255d;
			double ll = l / 255d;

			double r, g, b, maxc, minc;

			if (s == 0)
			{
				r = g = b = ll;
			}
			else
			{
				if (ll <= 0.5) maxc = ll * (1 + ss); else maxc = ll * (1 - ss) + ss;
				minc = 2 * ll - maxc;

				int hhh = hh + 120;
				if (hhh >= 360) hhh = hhh - 360;
				if (hhh < 60) r = minc + (maxc - minc) * hhh / 60;
				else if (hhh < 180) r = maxc;
				else if (hhh < 240) r = minc + (maxc - minc) * (240 - hhh) / 60;
				else r = minc;

				hhh = hh;
				if (hhh < 60) g = minc + (maxc - minc) * hhh / 60;
				else if (hhh < 180) g = maxc;
				else if (hhh < 240) g = minc + (maxc - minc) * (240 - hhh) / 60;
				else g = minc;

				hhh = hh - 120;
				if (hhh < 0) hhh = hhh + 360;
				if (hhh < 60) b = minc + (maxc - minc) * hhh / 60;
				else if (hhh < 180) b = maxc;
				else if (hhh < 240) b = minc + (maxc - minc) * (240 - hhh) / 60;
				else b = minc;
			}
			rr = (byte)(r * 255);
			gg = (byte)(g * 255);
			bb = (byte)(b * 255);
		}

		public static void RGBToHSL(int rr, int gg, int bb, out int h, out int s, out int l)
		{
			double r = rr / 255d;
			double g = gg / 255d;
			double b = bb / 255d;

			double maxc = Math.Max(Math.Max(r, g), b);
			double minc = Math.Min(Math.Min(r, g), b);

			double ll = (maxc + minc) / 2;
			double ss, hh;

			if ((maxc - minc) < 0.000001)
				ss = hh = 0;
			else
			{
				if (ll <= 0.5)
					ss = (maxc - minc) / (maxc + minc);
				else
					ss = (maxc - minc) / (2 - maxc - minc);

				double cr = (maxc - r) / (maxc - minc);
				double cg = (maxc - g) / (maxc - minc);
				double cb = (maxc - b) / (maxc - minc);

				if (maxc == r)
					hh = cb - cg;
				else if (maxc == g) hh = 2 + cr - cb;
				else hh = 4 + cg - cr;

				hh = 60 * hh;
				if (hh < 0) hh = hh + 360;
			}

			h = (int)hh;
			s = (int)(ss * 255);
			l = (int)(ll * 255);
		}

	}
}
