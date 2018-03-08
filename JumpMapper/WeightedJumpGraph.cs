using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents a weighted graph of PIN pad jumps.
    /// </summary>
    class WeightedJumpGraph
    {
        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        public List<WeightedJumpGraph> Children { get; private set; }

        /// <summary>
        /// Gets the jump associated with this node.
        /// </summary>
        public Jump AssociatedJump { get; private set; }

        /// <summary>
        /// Gets the weight of this node.
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Initializes a new instance of a weighted graph of PIN pad jumps.
        /// </summary>
        public WeightedJumpGraph()
        {
            Children = new List<WeightedJumpGraph>();
            AssociatedJump = null;
            Weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of a weighted graph of PIN pad jumps.
        /// </summary>
        /// <param name="associatedJump">The jump associated with this node.</param>
        public WeightedJumpGraph(Jump associatedJump) : this()
        {
            AssociatedJump = associatedJump;
        }

        /// <summary>
        /// Trains this graph with a series of jumps and a weight.
        /// </summary>
        /// <param name="jumps">The jumps to train with.</param>
        /// <param name="weight">The weight (frequency) of the training data.</param>
        public void Insert(List<Jump> jumps, int weight)
        {
            // Add weight to this node.
            Weight += weight;

            // If we're out of jumps, we're done.
            if (jumps.Count != 0)
            {
                // Add new child if one doesn't exist already.
                if (!Children.Any(c => c.AssociatedJump.Same(jumps[0])))
                {
                    Children.Add(new WeightedJumpGraph(jumps[0]));
                }

                // Recursively insert at appropriate child node.
                var target = Children.First(c => c.AssociatedJump.Same(jumps[0]));
                target.Insert(jumps.GetRange(1, jumps.Count - 1), weight);
            }
        }

        /// <summary>
        /// Looks up the value of a series of jumps in the graph.
        /// </summary>
        /// <param name="jumps">The series of jumps.</param>
        /// <returns></returns>
        public int Lookup(List<Jump> jumps)
        {
            // If we're out of jumps, return node value.
            if (jumps.Count == 0)
            {
                return Weight;
            }

            // Recursively look up value (add value of current node).
            var child = Children.FirstOrDefault(c => c.AssociatedJump.Same(jumps[0]));
            if (child == null)
            {
                return 0;
            }
            return Weight + child.Lookup(jumps.GetRange(1, jumps.Count - 1));
        }
    }
}
