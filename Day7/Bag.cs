using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Bag
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public HashSet<(int, Bag)> CanContainBags { get; set; } = new HashSet<(int, Bag)>();
        public HashSet<(int, Bag)> CanBeContainedBy { get; set; } = new HashSet<(int, Bag)>();
        public int GetNumberOfChildBags()
        {
            int childBags = 0;
            foreach (var childBagMap in CanContainBags)
            {
                childBags += childBagMap.Item1;
                childBags += childBagMap.Item1 * childBagMap.Item2.GetNumberOfChildBags();
            }
            return childBags;
        }
    }
}
