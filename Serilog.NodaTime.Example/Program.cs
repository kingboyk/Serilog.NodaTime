// Copyright 2019 Stephen Kennedy. All rights reserved.
// Use of this source code is governed by the Apache License 2.0,
// as found in the LICENSE file.

using System;
using System.Reflection;
using Oakton;

namespace Serilog.NodaTime.Example
{
    static class Program
    {
        private static int Main(string[] args)
        {
            while (true)
            {
                if (args.Length == 1 && args[0].Trim() == "--help")
                {
                    args = new[] { "help" };
                    continue;
                }

                return CommandExecutor.For(_ =>
                {
                    _.RegisterCommands(typeof(Program).GetTypeInfo().Assembly);
                    _.DefaultCommand = typeof(InteractiveCommand);
                }).Execute(args);
            }
        }
    }

    public class Options { }

    [Description("Serilog.NodaTime example", Name = "e")]
    public class LibraryExampleCommand : OaktonCommand<Options>
    {
        public override bool Execute(Options input)
        {
            Examples.LoggingExamples(withNodaTimeDeconstruction: true);
            return true;
        }
    }

    [Description("Interactive comparison (the default)", Name = "i")]
    public class InteractiveCommand : OaktonCommand<Options>
    {
        public override bool Execute(Options input)
        {
            Console.WriteLine("With Serilog.NodaTime:");
            Examples.LoggingExamples(withNodaTimeDeconstruction: true);
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.WriteLine("Without Serilog.NodaTime:");
            Examples.LoggingExamples(withNodaTimeDeconstruction: false);
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
            return true;
        }
    }

    [Description("NodaTime objects logged _without_ the Serilog.NodaTime library", Name = "w")]
    public class WithoutCommand : OaktonCommand<Options>
    {
        public override bool Execute(Options input)
        {
            Examples.LoggingExamples(withNodaTimeDeconstruction: false);
            return true;
        }
    }
}