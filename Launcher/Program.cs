﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Brodee");
            Console.WriteLine("Hit anything to stop looking(not your cat)");
            Console.Write("Watching for Hearthstone...");
            ManualResetEventSlim resetEventSlim = new ManualResetEventSlim();
            var task = Task.Factory.StartNew(() =>
            {
                while (!resetEventSlim.IsSet)
                {
                    Thread.Sleep(1000);
                    var processes = Process.GetProcessesByName("hearthstone");
                    if (processes.Length == 1)
                    {
                        Console.Write("Found Hearthstone! giving it a sec");
                        Thread.Sleep(1000);
                        var process = Process.Start("mono-injector\\mono-injector.exe",
                            "-dll Brodee.dll -target hearthstone.exe -namespace Brodee -class Loader -method Load");
                        if (process != null && process.WaitForExit(50000))
                        {
                            var exitCode = process.ExitCode;
                            if (exitCode == 0)
                            {
                                Console.WriteLine("Success!");
                                resetEventSlim.Set();
                            }
                        }

                    }
                    else if (processes.Length > 1)
                    {
                        Console.WriteLine("Found multiple hearthstones...wut, will attempt again.");
                        Console.Write("Watching for Hearthstone...");
                        Thread.Sleep(5000);
                    }
                }
            });
            Console.CancelKeyPress += (sender, eventArgs) => resetEventSlim.Set();
            while (!Console.KeyAvailable)
            {
                if (resetEventSlim.IsSet)
                    break;
                Thread.Sleep(100);
            }
        }
    }

}
