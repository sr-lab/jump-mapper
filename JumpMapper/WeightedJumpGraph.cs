using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    class WeightedJumpGraph
    {
        public List<WeightedJumpGraph> Children { get; private set; }

        public Jump AssociatedJump { get; private set; }

        public int Weight { get; set; }

        public WeightedJumpGraph()
        {
            Children = new List<WeightedJumpGraph>();
            AssociatedJump = null;
            Weight = 0;
        }

        public WeightedJumpGraph(Jump associatedJump) : this()
        {
            AssociatedJump = associatedJump;
        }

        public void Insert(List<Jump> jumps, int weight)
        {
            Weight += weight;

            if (jumps.Count == 0)
            {
                return;
            }

            if (!Children.Any(c => c.AssociatedJump.Same(jumps[0])))
            {
                Children.Add(new WeightedJumpGraph(jumps[0]));
            }

            var target = Children.First(c => c.AssociatedJump.Same(jumps[0]));
            target.Insert(jumps.GetRange(1, jumps.Count - 1), weight);
        }

        public int Lookup(List<Jump> jumps)
        {
            if (jumps.Count == 0)
            {
                return Weight;
            }

            return Weight + Children.First(c => c.AssociatedJump.Same(jumps[0]))
                .Lookup(jumps.GetRange(1, jumps.Count - 1));
        }
    }
}
