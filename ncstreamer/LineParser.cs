using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Globalization;


namespace ncstreamer
{
    class LineParser
    {
        public LineParser() { }
        
        public Int32[] xyval = new Int32[4];

        public Int32[] Parse(string s)
        {
            if (s.Contains("M5"))
            {
                xyval[0] = 260;
                xyval[1] = 0;
                xyval[2] = 0;
                xyval[3] = 0;
                return xyval;
            }

            if (s.Contains("G00 X"))
            {
                xyval[0] = 0;

                var r = s.Replace('.', ',').Split(' ');

                foreach(string sr in r)
                {
                    if(sr[0] == 'X')
                    {
                        string q = sr.Remove(0, 1);
                        xyval[1] = (Int32)Math.Round(GetDouble(q, 0.0) * 10000.0);
                    }

                    if (sr[0] == 'Y')
                    {
                        string q = sr.Remove(0, 1);
                        xyval[2] = (Int32)Math.Round(GetDouble(q, 0.0) * 10000.0);
                    }
                }
            }

            if(s.Contains("G01 X"))
            {
                xyval[0] = 1;

                var r = s.Replace('.', ',').Split(' ');

                foreach (string sr in r)
                {
                    if (sr[0] == 'X')
                    {
                        string q = sr.Remove(0, 1);
                        xyval[1] = (Int32)Math.Round(GetDouble(q, 0.0) * 10000.0);
                    }

                    if (sr[0] == 'Y')
                    {
                        string q = sr.Remove(0, 1);
                        xyval[2] = (Int32)Math.Round(GetDouble(q, 0.0) * 10000.0);
                    }

                    if (sr[0] == 'F')
                    {
                        string q = sr.Remove(0, 1);
                        xyval[3] = (Int32)Math.Round(GetDouble(q, 0.0) * 10000.0);
                    }
                }
            }

            if(s.Contains("G01 F"))
            {
                xyval[0] = 255;
                xyval[1] = 0;
                xyval[2] = 0;
                xyval[3] = 0;
            }
            
            if (s.Contains("M03"))
            {
                xyval[0] = 258;
                xyval[1] = 0;
                xyval[2] = 0;
                xyval[3] = 0;
                return xyval;
            }
            return xyval;
        }

        public static double GetDouble(string value, double defaultValue)
        {
            double result;
            // Try parsing in the current culture
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }

            return result;
        }


    }
}
