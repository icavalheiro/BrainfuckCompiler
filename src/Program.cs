using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BFC
{
    internal static class Program
    {
        private static void PrintManual()
        {
            Console.WriteLine("Usage: bfc <arguments[]> [program_path]");
        }

        private static void Main(string[] args)
        {
            var file = args.FirstOrDefault(x => !x.StartsWith('-'));
            // var file = "./hello.bf";

            if (string.IsNullOrEmpty(file))
            {
                Console.WriteLine("No file path provided for compilation.");
                PrintManual();
                return;
            }

            if (Path.GetExtension(file.ToLower()) != ".bf")
            {
                Console.WriteLine("Please provide a valid brainfuck file path (*.bf)");
                PrintManual();
                return;
            }
            
            var flags = args
                .Where(x => x.StartsWith('-'))
                .Select(x => x.ToLower().Trim().TrimStart('-'));

            //read program from file
            var program = File.ReadAllText(file);
            
            if (flags.Contains("c"))
            {
                //TODO: Compile application
                Console.WriteLine("Compiling to C++ is not yet supported");
            }
            else
            {
                InterpretProgram(program);
                Console.WriteLine("");
            }
        }

        private static void InterpretProgram(string program)
        {
            var interpreter = new Interpreter(program, Console.Write, Console.Read);
            interpreter.Run();
        }
    }
}
