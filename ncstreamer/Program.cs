using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;


/// <summary>
/// Aplikacja pobiera Gkod wygenerowany przez flat cam dla Laser GRBL
/// Przekształca zawartą ścieżkę narzędzia w pojedyncze wektory x y (z) f
/// Wysyła każdy jednostkowy ruch maszyny jako zbiór 4 wartości 
/// jeżeli G0 lub G1:
/// [g0/g1][x][y][f]
/// jeżeli M
/// [M][on/off][power %][inne]
/// </summary>




namespace ncstreamer
{
    

    class Program
    {
        public static ClientUDP sender;

        static void Main(string[] args)
        {

            string path = "";
            string x_offset = "";
            string y_offset = "";
            string laser_pwm = "";
            string g1_feed = "";
            string rapid_feed = "";
            string homeing = "";

            for(int i=0; i<args.Count(); i++)
            {
                if (args[i].Contains("-file"))
                {
                    path = args[i + 1];
                }

                if (args[i].Contains("-X"))
                {
                    x_offset = args[i + 1];
                }

                if (args[i].Contains("-Y"))
                {
                    y_offset = args[i + 1];
                }

                if (args[i].Contains("-P"))
                {
                    laser_pwm = args[i + 1];
                }

                if (args[i].Contains("-F"))
                {
                    g1_feed = args[i + 1];
                }

                if (args[i].Contains("-R"))
                {
                    rapid_feed = args[i + 1];
                }

                if(args[i].Contains("-H"))
                {
                    homeing = "true";
                }
            }

            Int32 iLaserPwm = 0;
            Int32.TryParse(laser_pwm, out iLaserPwm);

            Int32 iXoffset = 0;
            Int32.TryParse(x_offset, out iXoffset);

            Int32 iYoffset = 0;
            Int32.TryParse(y_offset, out iYoffset);

            Int32 ig1feed = 0;
            Int32.TryParse(g1_feed, out ig1feed);

            Int32 irapidfeed = 0;
            Int32.TryParse(rapid_feed, out irapidfeed);
            

            Composer composer = new Composer();
            
            List<Int32[]> r = new List<Int32[]>();

            r = composer.Compose(path);
            
            foreach(Int32[] cm in r)
            {
                if(cm[0] == 258)
                {
                    if(iLaserPwm > 0)
                    {
                        cm[1] = iLaserPwm;
                    }
                    else
                    {
                        cm[1] = 50;
                    }
                }

                if (cm[0] == 0)
                {
                    if (irapidfeed > 0)
                    {
                        cm[3] = irapidfeed * 10000;
                    }
                    else
                    {
                        cm[1] = 100000;
                    }
                }


                if (cm[0] == 1)
                {
                    if (ig1feed > 0)
                    {
                        cm[3] = ig1feed * 10000;
                    }
                }

            }
            

            sender = new ClientUDP("192.168.1.20", 52001);



            //foreach (Int32[] cm in r)
            //{
            //    Console.WriteLine(cm[0] + " : " + cm[1] + " : " + cm[2] + " : " + cm[3]);
            //}

            
            if (args.Count() == 0)
            {
                // natychmiast zatrzymaj maszyne
            }           
        }
    }
}
