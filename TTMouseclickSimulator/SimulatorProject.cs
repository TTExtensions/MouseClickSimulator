﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMouseclickSimulator.Core;

namespace TTMouseclickSimulator
{
    /// <summary>
    /// A simulator project that can be persisted.
    /// </summary>
    [Serializable]
    public class SimulatorProject
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public SimulatorConfiguration Configuration { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
