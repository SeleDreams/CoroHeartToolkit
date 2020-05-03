using System;
using System.IO;
using CoroHeart.Formats.GDAT;
using CoroHeart.Formats.CSH;

namespace CoroHeart.Toolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Drag and drop your .csh or your .dat on CoroHeartToolkit.exe to extract them !");
                Console.ReadLine();
                return;
            }
            string[] files = null;
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
                    if (CSH.isCSH(file))
                    {
                        DirectoryInfo info = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + Path.GetFileNameWithoutExtension(FileName));
                        FileStream outputFile = File.Create(info.FullName + "\\" + Path.GetFileName(FileName) + ".extracted");
                        using (outputFile)
                        {
                            try
                            {
                                CSH.Load(file, outputFile);
                                Console.WriteLine(FileName + " Exported successfully !");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("An exception occurred while loading " + FileName + " contact SeleDreams to determine what happened.");
                                Console.WriteLine(ex.GetType().Name + " : " + ex.Message);
                            }

                        }
                    }
                    else if (GDAT.isGDAT(file))
                    {
                        using (GDAT gdat = GDAT.Load(file))
                        {

                            DirectoryInfo info = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + Path.GetFileNameWithoutExtension(FileName));
                            int SuccessfulExports = 0;
                            for (int i = 0; i < gdat.FileCount; i++)
                            {
                                try
                                {
                                    gdat.Export(i, info.FullName + "\\");
                                    SuccessfulExports++;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("An exception occurred while loading " + FileName + " contact SeleDreams to determine what happened.");
                                    Console.WriteLine(ex.GetType().Name + " : " + ex.Message);
                                }
                            }
                            if (SuccessfulExports == gdat.FileCount)
                            {
                                Console.WriteLine("All files of " + FileName + " got succesfully exported !");
                            }
                            else
                            {
                                Console.WriteLine(SuccessfulExports + " out of " + gdat.FileCount + " got successfully exported from " + FileName);
                            }

                        }

                    }
                    else
                    {
                        Console.WriteLine("Incorrect file, the valid file types are .dat and .csh");
                    }
                }
            }
            Console.WriteLine("All files finished extracting !");
            Console.ReadLine();
        }
    }
}
