using System;
using System.Collections.Generic;
using System.Text;

namespace Day23
{
    public class Cup
    {
        public int Value { get; set; }
        public Cup Next { get; set; }
        public Cup Previous { get; set; }
    }
}
