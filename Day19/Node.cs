using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Node
    {
        public string RawValue { get; set; }
        public List<string> Strings { get; set; } = new List<string>();
        public List<Node> Parents { get; set; } = new List<Node>();
        public List<List<int>> SubNodeIds { get; set; } = new List<List<int>>();
        public bool IsRoot { get; set; } = false;
        public bool IsComplete
        {
            get
            {
                return IsRoot || Strings.Count() == SubNodeIds.Count();
            }
        }
    }
}
