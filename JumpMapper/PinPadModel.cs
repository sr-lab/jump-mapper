﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMapper
{
    /// <summary>
    /// Represents a model of a PIN pad.
    /// </summary>
    class PinPadModel
    {
        private Dictionary<int, Jump> baseOffsets;

        public WeightedJumpGraph graph;

        /// <summary>
        /// Initializes a new instance of a model of a PIN pad.
        /// </summary>
        public PinPadModel(IPinPadLayout layout)
        {
            // Place digits at correct PIN pad positions (offsets)
            baseOffsets = layout.GetBaseOffsets();

            // Initialize root of weighted graph.
            graph = new WeightedJumpGraph();
        }

        /// <summary>
        /// Gets a jump representing a movement between two keys on the pad.
        /// </summary>
        /// <param name="a">The starting key.</param>
        /// <param name="b">The destination key.</param>
        /// <returns></returns>
        private Jump GetJump(int a, int b)
        {
            var origin = baseOffsets[a];
            var destination = baseOffsets[b];
            return new Jump(destination.X - origin.X, destination.Y - origin.Y);
        }

        /// <summary>
        /// Converts a PIN to a series of jumps.
        /// </summary>
        /// <param name="pin">The PIN to convert.</param>
        /// <returns></returns>
        private List<Jump> ToJumps(string pin)
        {
            var output = new List<Jump>();
            for (int i = 0; i < pin.Length - 1; i++)
            {
                output.Add(GetJump(int.Parse(pin[i].ToString()), int.Parse(pin[i + 1].ToString())));
            }
            return output;
        }

        /// <summary>
        /// Looks up the value of a PIN (higher value means weaker PIN).
        /// </summary>
        /// <param name="pin">The pin to look up the value of.</param>
        /// <returns></returns>
        public int Lookup(string pin)
        {
            return graph.Lookup(ToJumps(pin)) - graph.Weight; // Subtract weight of root.
        }

        /// <summary>
        /// Trains the model with the specified PIN and weight.
        /// </summary>
        /// <param name="pin">The PIN to train with.</param>
        /// <param name="weight">The weight (frequency) of the PIN.</param>
        public void Process(string pin, int weight)
        {
            graph.Insert(ToJumps(pin), weight);
        }

        /// <summary>
        /// Orders a number of PINs by probability (best guess first).
        /// </summary>
        /// <param name="pins">The guesses to order.</param>
        /// <returns></returns>
        public List<string> Rank(List<string> pins)
        {
            return pins.OrderBy(x => Lookup(x)).ToList();
        }
    }
}
