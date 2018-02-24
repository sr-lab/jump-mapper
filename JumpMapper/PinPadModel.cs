using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    class PinPadModel
    {
        private Dictionary<int, Jump> baseOffsets;

        private WeightedJumpGraph graph;

        public PinPadModel()
        {
            baseOffsets = new Dictionary<int, Jump>() {
                { 7, new Jump(-1, 1) },
                { 8, new Jump(0, 1) },
                { 9, new Jump(1, 1) },
                { 4, new Jump(-1, 0) },
                { 5, new Jump(0, 0) },
                { 6, new Jump(1, 0) },
                { 1, new Jump(-1, -1) },
                { 2, new Jump(0, -1) },
                { 3, new Jump(1, -1) },
            };

            graph = new WeightedJumpGraph();
        }

        private Jump GetJump(int a, int b)
        {
            var origin = baseOffsets[a];
            var destination = baseOffsets[b];
            return new Jump(destination.X - origin.X, destination.Y - origin.Y);
        }

        private List<Jump> ToJumps(string pin)
        {
            var output = new List<Jump>();
            for (int i = 0; i < pin.Length - 1; i++)
            {
                output.Add(GetJump(int.Parse(pin[i].ToString()), int.Parse(pin[i + 1].ToString())));
            }
            return output;
        }

        public void Process(string pin, int weight)
        {
            graph.Insert(ToJumps(pin), weight);
        }
    }
}
