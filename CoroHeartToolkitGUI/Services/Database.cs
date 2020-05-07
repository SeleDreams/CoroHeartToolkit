using System;
using System.Collections.Generic;
using System.Text;

namespace CoroHeartToolkitGUI.Services
{
    public class Database
    {
        public IEnumerable<string> GetItems() => new string[]
        {
            "Test1",
            "Test2",
            "Test3",
            "Test4"
        };
    }
}
