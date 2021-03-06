﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

using TTMouseclickSimulator.Core.Environment;

namespace TTMouseclickSimulator.Core.ToontownRewritten.Environment
{
    /// <summary>
    /// Environment interface for Toontown Rewritten.
    /// </summary>
    public class TTRWindowsEnvironment : AbstractWindowsEnvironment
    {
        private const string ProcessName = "TTREngine";

        private TTRWindowsEnvironment()
        {
        }

        public static TTRWindowsEnvironment Instance { get; } = new TTRWindowsEnvironment();


        public override sealed List<Process> FindProcesses()
        {
            try
            {
                return FindProcessesByName(ProcessName);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Could not find Toontown Rewritten. Please make sure " +
                        "TT Rewritten is running before starting the simulator.", ex);
            }
        }        

        protected override sealed void ValidateWindowPosition(WindowPosition pos)
        {
            // Check if the aspect ratio of the window is 4:3 or higher.
            if (!(((double)pos.Size.Width / pos.Size.Height) >= 4d / 3d))
                throw new ArgumentException("The TT Rewritten window must have an aspect ratio " +
                        "of 4:3 or higher (e.g. 16:9).");
        }
    }
}
