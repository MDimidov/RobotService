using RobotService.IO.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.IO
{
    public class TextWriter : IWriter
    {
        private const string Path = "../../../output.txt";
        public void Write(string message)
        {
            using (StreamWriter writer = new(Path, true))
            {
                writer.Write(message);
            }
        }

        public void WriteLine(string message)
        {
            using (StreamWriter writer = new(Path, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
