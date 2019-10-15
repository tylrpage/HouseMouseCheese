using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseMouseCheese
{
    class Pattern
    {
        readonly float[] FRAMES_PER_BEAT_OPTIONS = { 0.25f, 0.5f, 1f, 2f, 4f };
        const int MAX_UNIQUE_CYCLES = 4;

        private float _framesPerBeat;
        public float FramesPerBeat
        {
            get { return _framesPerBeat; }
            set
            {
                if (FRAMES_PER_BEAT_OPTIONS.Contains(value)) { _framesPerBeat = value; }
                else
                {
                    throw new ArgumentException(
                        $"Frames per beat value must be one of the following: {string.Join(", ", FRAMES_PER_BEAT_OPTIONS)}"
                    );
                }
            }
        }
        private int _uniqueCycles;
        public int UniqueCycles
        {
            get { return _uniqueCycles; }
            set
            {
                if (value > 0 && value <= MAX_UNIQUE_CYCLES) { _uniqueCycles = value; }
                else { throw new ArgumentOutOfRangeException($"Cycles must be > 0 and <= {MAX_UNIQUE_CYCLES}"); }
            }
        }
        public Frame[] Frames { get; set; }

        public Pattern()
        {
            Frames = new Frame[4 * MAX_UNIQUE_CYCLES];
        }
    }
}
