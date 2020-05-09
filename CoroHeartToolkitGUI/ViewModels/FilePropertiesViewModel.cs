using CoroHeartToolkitGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CoroHeartFormats;
using System.IO;
namespace CoroHeartToolkitGUI.ViewModels
{
    public class FilePropertiesViewModel : ViewModelBase
    {
        public FilePropertiesViewModel()
        {
            Properties = new ObservableCollection<FileProperties>();
        }

        public void UpdateProperties(GDATFile file)
        {
            Properties.Clear();
            AddIDProperty(file);
            AddFormatProperties(file);
            AddOffsetProperty(file);
            AddSizeProperty(file);
            Console.WriteLine("Updated Properties");
        }



        void AddIDProperty(GDATFile file)
        {
            FileProperties props = new FileProperties("ID", file.ID.ToString());
            Properties.Add(props);
        }

        void AddFormatProperties(GDATFile file)
        {
            string formattedMagic = FileProperties.GetExtension(file.Magic);
            FileProperties props = new FileProperties("Format", formattedMagic);
            Properties.Add(props);
            AddFormatDefinition(formattedMagic);
        }

        void AddFormatDefinition(string format)
        {
            string formatType = string.Empty;
            switch (format.Trim())
            {
                case "MDL":
                case "MDLX":
                    formatType = "3D Model";
                    break;
                case "TEX":
                    formatType = "Texture";
                    break;
                case "PSB":
                    formatType = "HLSL Shader";
                    break;
                case "CL3":
                    formatType = "VN Lines, Format editable with NepTools";
                    break;
                case "ALC":
                case "1000":
                case "1010":
                case "2000":
                case "2010":
                case "3000":
                case "3010":
                case "4000":
                case "4010":
                case "5000":
                case "5010":
                case "6000":
                case "6010":
                case "7000":
                case "7010":
                case "8000":
                case "8010":
                case "9000":
                case "9010":
                    formatType = "S-JIS Config files (I still ignore what they do)";
                    break;
                default:
                    formatType = "Unknown";
                    break;
            }
            FileProperties props = new FileProperties("Type", formatType);
            Properties.Add(props);
        }
        void AddOffsetProperty(GDATFile file)
        {
            FileProperties props = new FileProperties("Offset", "0x" + file.Offset.ToString("X8"));
            Properties.Add(props);
        }
        void AddSizeProperty(GDATFile file)
        {
            FileProperties props = new FileProperties();
            props.Property = "Size";
            props.Value = GetSize(file);
            Properties.Add(props);
        }

        public string GetSize(GDATFile file)
        {
            ulong size = file.Size;
            if (size > 100)
            {
                float kbSize = (float)file.Size * 0.001f;
                if (kbSize > 10)
                {
                    float mbs = kbSize * 0.001f;
                    return mbs.ToString("0.00") + " MB";

                }
                else
                {
                    return kbSize.ToString("0.00") + " KB";
                }
            }
            else
            {
                return size.ToString() + " B";
            }
        }
        public ObservableCollection<FileProperties> Properties { get; set; }
    }
}
