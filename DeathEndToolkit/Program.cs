using System;
using System.IO;
using DeathEndFormats.Formats.GDAT;
using DeathEndFormats.Formats.CSH;
namespace DeathEndToolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Drag and drop your .csh or your .dat on DeathEndToolkit.exe then try again !");
                Console.ReadLine();
                return;
            }
            string FileName = args[0];
            FileStream file = File.OpenRead(FileName);
            using (file)
            {
                if (FileName.EndsWith(".csh"))
                {
                    FileStream outputFile = File.Create(FileName + ".extracted");
                    using (outputFile)
                    {
                        CSH.Load(file, outputFile);
                    }
                }
                else if (FileName.EndsWith(".dat"))
                {
                    using (GDAT gdat = GDAT.Load(file))
                    {
                        DirectoryInfo info = Directory.CreateDirectory( Environment.CurrentDirectory + "\\EXPORT\\");
                        for (int i = 0; i < gdat.FileCount; i++)
                        {
                            gdat.Export(i, info.FullName + "\\" + gdat.Files[i].Offset);
                        }

                    }
                }

            }
        }
    }
}
