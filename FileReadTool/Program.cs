using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReadTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "path/to/your/textfile.txt"; // 替换为你的文件路径
            List<string> lines = ReadFile(filePath);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        static List<string> ReadFile(string filePath)
        {
            List<string> lines = new List<string>();

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                using (StringReader reader = new StringReader(fileContent))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
            }

            return lines;
        }
    }
}
