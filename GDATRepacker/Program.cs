using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoroHeartFormats;
using Kaitai;

namespace CoroHeartToolkit
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Drag and drop the directory you want to transform to GDAT!");
                Console.ReadLine();
                return;
            }

            else

            if (File.Exists(args[0]) || Directory.Exists(args[0]))
            {
                FileAttributes attr = File.GetAttributes(args[0]);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    if (args.Length < 2)
                    {
                        using (FileStream stream = File.Create(Path.GetFileName(args[0] + ".dat"))) {
                            Gdat gdat = Gdat.Create(stream);
                            string[] files = Directory.EnumerateFiles(args[0]).OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(f))).ToArray();
                            foreach (var file in files)
                            {
                                Console.WriteLine("Adding file " + file);
                                Kaitai.Gdat.File f = new Kaitai.Gdat.File(gdat.M_Io);
                                f.SetParent(gdat);
                                gdat.Files.Add(f);
                                f.Body = File.ReadAllBytes(file);
                            }
                            gdat.RecalculateAllOffsets();
                            gdat.Write(stream);
                        }
                            
                    }
                    else
                    {
                      //  GDAT.Create(args[0], Path.GetFileName(args[1]));
                    }
                }
                else
                {
                    ZLIB.Create(args[0], args[0] + ".repacked");
                }
            }
            Console.ReadLine();
        }
    }
}
