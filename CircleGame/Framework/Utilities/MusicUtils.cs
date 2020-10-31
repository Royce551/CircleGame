using System;
using System.Collections.Generic;
using System.Text;

namespace CircleGame.Framework.Utilities
{
    public static class MusicUtils
    {
        public static float ToMsOffset(float bpm) => 60_000f / bpm;

        public static float ToBpm(float ms) => 60_000f / ms;
    }
}
