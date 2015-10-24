﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMouseclickSimulator.Core.Actions;
using TTMouseclickSimulator.Core.Environment;

namespace TTMouseclickSimulator.Core.ToontownRewritten.Actions.Fishing
{
    [Serializable]
    public class QuitFishingAction : IAction
    {

        public async Task RunAsync(IInteractionProvider provider)
        {
            Coordinates c = new Coordinates(1503, 1086);
            await MouseHelpers.DoSimpleMouseClickAsync(provider, c, 200);
        }

    }
}
