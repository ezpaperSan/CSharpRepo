using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main()
        {
            string sourcePath = "C:\\source\\folder"; // 源路径
            string destinationPath = "C:\\destination\\folder"; // 目标路径

            MoveFiles(sourcePath, destinationPath);
        }

        static void MoveFiles(string sourcePath, string destinationPath)
        {
            try
            {
                // 获取源目录中的所有文件和子目录
                foreach (string file in Directory.GetFiles(sourcePath))
                {
                    // 获取文件名
                    string fileName = Path.GetFileName(file);

                    // 创建目标文件路径
                    string destinationFile = Path.Combine(destinationPath, fileName);

                    // 移动文件
                    File.Move(file, destinationFile);

                    // 记录日志
                    Debug.WriteLine($"Moved file: {file} to {destinationFile}");
                }

                // 获取源目录中的所有子目录
                foreach (string subdirectory in Directory.GetDirectories(sourcePath))
                {
                    // 创建目标子目录路径
                    string destinationSubdirectory = Path.Combine(destinationPath, Path.GetFileName(subdirectory));

                    // 移动子目录及其内容
                    MoveFiles(subdirectory, destinationSubdirectory);

                    // 记录日志
                    Debug.WriteLine($"Moved directory: {subdirectory} to {destinationSubdirectory}");
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
