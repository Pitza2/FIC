using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIC
{
    internal class Flags
    {
        public short flags;
        static Flags f;
        public const short SET_OVERFLOW = 1;
        public const short SET_ZERO = 2;
        public const short SET_NEGATIVE = 4;
        private Flags() { flags = 0; }
        public static Flags GetFlags()
        {
            if (f != null)
            {
                return f;
            }
            f = new Flags();
            return f;
        }
    }
}
