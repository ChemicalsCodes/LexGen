using System;
using System.IO;
using System.Reflection;

using LexerGenerator;

namespace LexerGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewers.SetSize();

            //test Generation
            string lexer = Generator.GenerateCS4();
            Viewers.Write(lexer, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);

            //test C# 4.0
            string source = GetEmbeddedResourceS(@"LexerGeneratorTest.SampleSources.Logic.cs");
            Lex_CS4.Lexer lex = new Lex_CS4.Lexer(source);
            lex.Tokenize();
            string lexed = lex.ToString();
            Viewers.Write(lexed, ConsoleColor.Yellow, ConsoleColor.Magenta, true);
        }

        private static string GetEmbeddedResourceS(string fullName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            string result = "";
            using (Stream stream = assembly.GetManifestResourceStream(fullName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
    class Viewers
    {
        public static void SetSize()
        {
            int width = (Console.LargestWindowWidth / 6) * 5;
            int height = (Console.LargestWindowHeight / 6) * 5;
            Console.SetWindowSize(width, height);
        }
        public static void Write(string lexed, ConsoleColor main, ConsoleColor system, bool delimit = false)
        {
            Console.ForegroundColor = main;
            int lines = lexed.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Length;
            lines += Console.BufferHeight;
            Console.BufferHeight = lines <= (Int16.MaxValue - 1) ? lines : (Int16.MaxValue - 1);

            if (delimit)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine(lexed);
            Console.WriteLine();
            Console.ForegroundColor = system;
            Console.WriteLine("ChemicalLexer for C# 4.0");
            Console.Write("Press any key to continue ...");

            Console.ReadLine();
        }
    }
}
