using System;

namespace Engine
{
    public class GenerationResolvedEventArgs : EventArgs
    {
        public int GenerationCount { get; set; }
        public Generation Generation { get; set; }
    }
}
