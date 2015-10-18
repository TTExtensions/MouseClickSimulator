﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMouseclickSimulator.Core.Actions;
using TTMouseclickSimulator.Core.Environment;

namespace TTMouseclickSimulator.Core
{
    public class Simulator
    {
        public const int WaitIntervalMinimum = 100;
        public const int WaitIntervalMaximum = 60000;


        private readonly SimulatorConfiguration config;
        private readonly AbstractEnvironmentInterface environmentInterface;

        private readonly StandardInteractionProvider provider;
        private readonly Random rng = new Random();

        private bool canceled = false;

        public Simulator(SimulatorConfiguration config, AbstractEnvironmentInterface environmentInterface)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (environmentInterface == null)
                throw new ArgumentNullException(nameof(environmentInterface));
            if (config.Actions == null || config.Actions.Count == 0)
                throw new ArgumentException("There must be at least one IAction to start the simulator.");
            if (config.MinimumWaitInterval < WaitIntervalMinimum
                    || config.MinimumWaitInterval > WaitIntervalMaximum
                    || config.MaximumWaitInterval < WaitIntervalMinimum
                    || config.MaximumWaitInterval > WaitIntervalMaximum)
                throw new ArgumentOutOfRangeException("The wait interval values must be between " +
                    $"{WaitIntervalMinimum} and {WaitIntervalMaximum} milliseconds.");
            if (config.MinimumWaitInterval > config.MaximumWaitInterval)
                throw new ArgumentException("The minimum wait interval must not be greater " 
                    + "than the maximum wait interval."); 
            

            this.config = config;
            this.environmentInterface = environmentInterface;

            provider = new StandardInteractionProvider(environmentInterface);

        }

        public async Task RunAsync()
        {
            if (canceled)
                throw new InvalidOperationException("The simulator has already been canceled.");

            provider.Initialize();

            // Wait a bit so that the window can go into foreground.
            await provider.WaitAsync(1000);

            // Run the actions.
            int nextActionIdx = 0;

            using (provider)
            {
                while (true)
                {
                    if (config.RunInOrder)
                    {
                        nextActionIdx = (nextActionIdx + 1) % config.Actions.Count;
                    }
                    else
                    {
                        nextActionIdx = rng.Next(config.Actions.Count);
                    }

                    IAction action = config.Actions[nextActionIdx];
                    await action.RunAsync(provider);

                    // After running an action, wait.
                    int waitInterval = rng.Next(config.MinimumWaitInterval, config.MaximumWaitInterval);
                    await provider.WaitAsync(waitInterval);
                }
            }
        }

        public void Cancel()
        {
            provider.Dispose();
            canceled = true;
        }

    }
}