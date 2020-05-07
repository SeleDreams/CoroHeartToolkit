using System;
using System.Collections.Generic;
using System.Text;

namespace CoroHeartToolkitGUI.Services
{
    public class FileProperty
    {
        public FileProperty(string name, string content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
