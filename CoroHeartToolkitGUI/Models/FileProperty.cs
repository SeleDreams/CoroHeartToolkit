using System;
using System.Collections.Generic;
using System.Text;

namespace CoroHeartToolkitGUI.Models
{
    public class FileProperties
    {
        public FileProperties(string property = "", string value = "")
        {
            this.Property = property;
            Value = value;
        }
        public static string GetExtension(byte[] originalMagic){
            byte[] magic =  new byte[originalMagic.Length];
            Array.Copy(originalMagic,magic,originalMagic.Length);
            Array.Reverse(magic);
            string strMagic = Encoding.UTF8.GetString(magic);
            strMagic = string.Concat(strMagic.Split('\\','\0'));
            string formattedMagic = strMagic.ToUpperInvariant();
            return formattedMagic;
        }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
