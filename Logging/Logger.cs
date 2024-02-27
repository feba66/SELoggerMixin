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
    partial class Program {
        public enum LogLevel : byte { DEBUG, INFO, WARN, ERROR };
        public class Logger {
            private const string VERSION = "v1.0.0";
            private readonly IMyTextSurface textSurface;
            private readonly Action<string> echo;
            private readonly LogLevel? textSurfaceLevel, echoLevel, cDLevel;
            private readonly IMyTerminalBlock cD;
            bool doEcho, doCustomData, doTextSurface;
            

            public Logger(IMyTextSurface textSurface, LogLevel level = LogLevel.INFO) : this(textSurface, null, null, level, null, null) { }

            public Logger(Action<string> echo, LogLevel level = LogLevel.INFO) : this(null, echo, null, null, level, null) { }

            public Logger(IMyTerminalBlock cd, LogLevel level = LogLevel.INFO) : this(null, null, cd, null, null, level) { }

            public Logger(IMyTextSurface textSurface, Action<string> echo) : this(textSurface, echo, null, LogLevel.INFO, LogLevel.INFO, null) { }

            public Logger(IMyTerminalBlock cd, Action<string> echo) : this(null, echo, cd, null, LogLevel.INFO, LogLevel.INFO) { }

            public Logger(IMyTextSurface textSurface, Action<string> echo, IMyTerminalBlock cD, LogLevel? textSurfaceLevel, LogLevel? echoLevel, LogLevel? cDLevel) {
                doTextSurface = textSurface != null;
                if (doTextSurface) {
                    this.textSurface = textSurface;
                    textSurface.ContentType = ContentType.TEXT_AND_IMAGE;
                    textSurface.Font = "Monospace";
                    textSurface.FontSize = 0.45f;
                }
                doEcho = echo != null;
                doCustomData = cD != null;
                if (doCustomData)
                    this.cD = cD;

                this.echo = echo;
                this.textSurfaceLevel = textSurfaceLevel;
                this.echoLevel = echoLevel;
                this.cDLevel = cDLevel;

                Debug($"Logger {VERSION} initialized");
            }

            public void Clear() {
                if (doTextSurface)
                    textSurface.WriteText("");
                if (doCustomData)
                    cD.CustomData = "";
            }

            public void Log(string msg, LogLevel logLevel = LogLevel.INFO) {
                string tmp = $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} | {logLevel,-5} | {msg}\n";
                if (doEcho && echoLevel <= logLevel)
                    echo(tmp);
                if (doTextSurface && textSurfaceLevel <= logLevel)
                    textSurface.WriteText($"{tmp}{textSurface.GetText()}");
                if (doCustomData && cDLevel <= logLevel)
                    cD.CustomData = $"{tmp}{cD.CustomData}";
            }

            public void Info(string msg) {
                Log(msg, LogLevel.INFO);
            }

            public void Debug(string msg) {
                Log(msg, LogLevel.DEBUG);
            }

            public void Warn(string msg) {
                Log(msg, LogLevel.WARN);
            }

            public void Error(string msg) {
                Log(msg, LogLevel.ERROR);
            }
        }
    }
}
