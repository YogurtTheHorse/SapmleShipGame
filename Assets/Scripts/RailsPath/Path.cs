using System;

namespace RailsPath
{
    [Serializable]
    public class Path
    {
        public bool loop = true;

        public PathSection[] sections = { };
    }
}