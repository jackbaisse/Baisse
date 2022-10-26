using System;
using System.Collections.Generic;
using System.Text;

namespace PressureTest
{
    public class ServiceConfig
    {
        public string Url { get; set; }
        public string Interface { get; set; }
        public int Frequency { get; set; }
        public bool Thread { get; set; }
        public int ThreadFrequency { get; set; }

    }
}
