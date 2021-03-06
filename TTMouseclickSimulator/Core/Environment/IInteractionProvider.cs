﻿using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace TTMouseclickSimulator.Core.Environment
{
    /// <summary>
    /// Allows actions to interact with the destination window, e.g. press keys and
    /// simulate mouse clicks and to wait asynchronously.
    /// </summary>
    public interface IInteractionProvider
    {
        /// <summary>
        /// If this method returns, this means the action should run again. Otherwise, this method will
        /// re-throw the exception or throw an <see cref="SimulatorCanceledException"/>.
        /// </summary>
        /// <param name="ex"></param>
        Task CheckRetryForExceptionAsync(Exception ex);

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

        /// <summary>
        /// Gets a current screenshot of the window. Note that because the IInteractionProvider
        /// caches the current screenshot for performance reason, this method may return
        /// the same IScreenshotContent instance as previous calls but with refreshed content.
        /// </summary>
        /// <returns></returns>
        IScreenshotContent GetCurrentWindowScreenshot();

        /// <summary>
        /// Moves the mouse to the specified window-relative coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void MoveMouse(int x, int y);

        /// <summary>
        /// Moves the mouse to the specified window-relative coordinates.
        /// </summary>
        /// <param name="c"></param>
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
        public SimulatorCanceledException()
            : base("The Simulator has been canceled.")
        {
        }
    }    
}
