﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMouseclickSimulator.Core.Environment;

namespace TTMouseclickSimulator.Core.Environment
{
    /// <summary>
    /// Allows actions to interact with the destination window, e.g. press keys and
    /// simulate mouse clicks and to wait asynchronously.
    /// </summary>
    public interface IInteractionProvider
    {

        /// <summary>
        /// Checks that the Simulator has not been canceled.
        /// </summary>
        void EnsureNotCanceled();

        /// <summary>
        /// Asynchronously waits until the specified interval is elapsed or the Simulator
        /// has been canceled.
        /// </summary>
        /// <param name="millisecondsTimeout">The interval to wait.</param>
        /// <param name="useAccurateTimer">If an accurate timer should be used. If true, measuring
        /// of the time is more accurate but requires a bit CPU usage shortly before the method
        /// returns.</param>
        /// <returns></returns>
        /// <exception cref="SimulatorCanceledException">If the wait has been cancelled.
        /// IActions don't need to catch this exception.</exception>
        Task WaitAsync(int millisecondsTimeout, bool useAccurateTimer = false);

        /// <summary>
        /// Gets the current position of the destination window.
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SimulatorCanceledException">If the simulator has been cancelled.
        /// IActions don't need to catch this exception.</exception>
        WindowPosition GetCurrentWindowPosition();

        IScreenshotContent CreateCurrentWindowScreenshot();

        void MoveMouse(int x, int y);

        void MoveMouse(Coordinates c);

        void PressMouseButton();

        void ReleaseMouseButton();

        void PressKey(AbstractWindowsEnvironment.VirtualKeyShort key);
        void ReleaseKey(AbstractWindowsEnvironment.VirtualKeyShort key);

        /// <summary>
        /// Writes the given string into the window.
        /// </summary>
        /// <param name="text"></param>
        void WriteText(string text);

    }


    /// <summary>
    /// Thrown when an action has been canceled because the simulator has been stopped.
    /// </summary>
    public class SimulatorCanceledException : Exception
    {
        public SimulatorCanceledException() : 
            base("The Simulator has been canceled.")
        {

        }
    }
}
