using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript {
    partial class Program : MyGridProgram {
        Logger log;

        public Program() {
            log = new Logger(Me.GetSurface(0), Echo, Me, LogLevel.INFO, LogLevel.DEBUG, LogLevel.DEBUG);
            log.Log("Hello, World!");
        }

        public void Save() {

        }

        public void Main(string argument, UpdateType updateSource) {
            if (argument == "clear") {
                log.Clear();
            }
            else {
                log.Info($"Run: {argument}");
                log.Debug($"Run: {argument}");
                log.Warn($"Run: {argument}");
                log.Error($"Run: {argument}");
            }
        }
    }
}
