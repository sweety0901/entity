using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace WpfSewApp
{
    class Files
    {
        public static string NewFile(string fileName)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                FileName = fileName
            };

            if (saveFile.ShowDialog() == true)
            {
                return saveFile.FileName;
            }
            else
            {
                return null;
            }
        }

        public static bool WriteToFile()
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\soft\Desktop\1.csv"))
            {
                file.WriteLine()
            }

            return true;
        }
    }
}