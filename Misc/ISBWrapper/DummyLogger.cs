using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISBWrapper
{
    internal sealed class DummyLogger : Logger
    {
        // Methods
        internal DummyLogger(string name)
            : base(name)
        {
        }
    }
}
