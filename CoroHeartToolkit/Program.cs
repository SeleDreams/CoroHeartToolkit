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
                Console.WriteLine("Drag and drop your .csh or your .dat on DeathEndToolkit.exe then try again !");
                Console.ReadLine();
                return;
            }
            foreach (string FileName in args)
            {
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
                        Console.WriteLine("Incorrect argument, the valid file types are .dat and .csh");
                    }
                }
                Console.ReadLine();
            }
        }
    }
}
