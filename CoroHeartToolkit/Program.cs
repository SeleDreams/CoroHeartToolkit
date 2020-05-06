using System;
using System.IO;
using CoroHeartFormats;
using Kaitai;
namespace CoroHeart.Toolkit
{
    class Program
    {

        static void OpenZLIB(FileStream file,string FileName)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string croppedFilename = Path.GetFileNameWithoutExtension(FileName);
            DirectoryInfo info = Directory.CreateDirectory(currentDir + "\\" + croppedFilename);
            string outputPath = info.FullName + "\\" + Path.GetFileName(FileName) + "." + ZLIB.GetFileType(file);
            FileStream outputFile = File.Create(outputPath);
            using (outputFile)
            {
                try
                {
                    ZLIB.Load(file, outputFile);
                    Console.WriteLine(FileName + " Exported successfully !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred while loading " + FileName);
                    Console.WriteLine(ex.GetType().Name + " : " + ex.Message);
                }
            }
        }

        static void OpenGdat(FileStream file, string FileName)
        {
            Gdat gdat = Gdat.FromStream(file);
            gdat.ReadData();
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string croppedFilename = Path.GetFileNameWithoutExtension(FileName);
            DirectoryInfo info = Directory.CreateDirectory(currentDir + "\\" + croppedFilename); 
            int SuccessfulExports = 0;
            for (int i = 0; i < gdat.FileCount; i++)
            {
                try
                {
                    Console.WriteLine("Writing file " + i);
                    byte[] data = gdat.Files[i].Body;
                    string magic = gdat.Files[i].Magic;
                    string fileFormat = "." + string.Concat(magic.Split(Path.GetInvalidFileNameChars()));
                    File.WriteAllBytes(info.FullName + "\\" + i + fileFormat, data) ;
                    SuccessfulExports++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred while loading " + FileName);
                    Console.WriteLine(ex.GetType().Name + " : " + ex.Message);
                }
            }
            if (gdat.FileCount == 0)
            {
                Console.WriteLine("The file count is empty");
            }
            else if (SuccessfulExports == gdat.FileCount)
            {
                Console.WriteLine("All files of " + FileName + " got succesfully exported !");
            }
            else
            {
                Console.WriteLine(SuccessfulExports + " out of " + gdat.FileCount + " got successfully exported from " + FileName);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Drag and drop your .csh or your .dat on CoroHeartToolkit.exe to extract them !");
                Console.ReadLine();
                return;
            }

            string[] files;
            if (Directory.Exists(args[0]))
            {
                files = Directory.GetFiles(args[0]);
            }
            else
            {
                files = args;
            }
            foreach (string FileName in files)
            {
                FileStream file = File.OpenRead(FileName);
                Console.WriteLine("Opening " + FileName);
                using (file)
                {
                    if (ZLIB.isZLIB(file))
                    {
                        OpenZLIB(file, FileName);
                    }
                    else
                    {
                        OpenGdat(file, FileName);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
