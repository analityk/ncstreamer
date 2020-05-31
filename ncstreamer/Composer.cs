using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ncstreamer
{
    class Composer
    {
        public LineParser lineParser;
        public Int32[] command = new Int32[4];

        public Vector old_pos;
        public Vector new_pos;
        public Vector move;

        List<Int32[]> ComSet = new List<int[]>();

        public List<Int32[]> Compose(string path)
        {
            lineParser = new LineParser();

            old_pos = new Vector(0.0, 0.0);
            new_pos = new Vector(0.0, 0.0);
            move = new Vector(0.0, 0.0);

            Console.WriteLine(path);

            string[] gcodeLines = File.ReadAllLines(path);

            foreach (string s in gcodeLines)
            {
                command = lineParser.Parse(s);

                if(command[0] == 0 || command[0] == 1)
                {
                    new_pos.x = command[1];
                    new_pos.y = command[2];

                    move.x = new_pos.x - old_pos.x;
                    move.y = new_pos.y - old_pos.y;

                    old_pos.x = new_pos.x;
                    old_pos.y = new_pos.y;

                    if( command[0] == 0)
                    {
                        command[0] = (Int32)(0);
                        command[1] = (Int32)move.x;
                        command[2] = (Int32)move.y;
                        command[3] = (Int32)command[3]; // xD
                    }

                    if (command[0] == 1)
                    {
                        command[0] = (Int32)(1);
                        command[1] = (Int32)move.x;
                        command[2] = (Int32)move.y;
                        command[3] = (Int32)command[3]; // xD
                    }
                    ComSet.Add(new Int32[4] { command[0], command[1], command[2], command[3] });
                }
                else
                {
                    if(command[0] == 258 || command[0] == 260)
                    {
                        ComSet.Add(new Int32[4] { command[0], command[1], command[2], command[3] });
                    }
                }
            }
            return ComSet;
        }


    }
}
