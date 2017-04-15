using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace LexerGenerator
{
    public class Generator
    {
        public static string GenerateCS4()
        {
            string logic = GetEmbeddedResourceS(@"LexerGenerator.Grammers.cs4.cs");
            return GenerateLexer(logic, @"Lex_CS4");
        }

        public static string GenerateLexer(string logic, string name)
        {
            Lexer lex = new Lexer(logic);
            lex.Tokenize();
            var reader = lex.GetReader();
                //string s = lex.ToString();
                //Console.WriteLine(s);

            List<string> enums = new List<string>();
            List<string> rdefinitions = new List<string>();
            List<string> mdefinitions = new List<string>();

            while (!reader.isEnded)
            {
                Token t = reader.Read();
                if (t.Role != eTokenRole.role)
                    throw new InvalidDataException(
                        @"Source logic is not syntactically correct. 
                        Please check the documentation for more information.");
                string role = t.Code;
                if (reader.Peek().Role == eTokenRole.lineend)
                {
                    mdefinitions.Add(@"Manipulations.Add(new TokenRemover(eTokenRole." + role + "))");
                    if (!enums.Contains(role)) enums.Add(role);
                    reader.Next();
                    continue;
                }
                else if(reader.Peek().Role != eTokenRole.comma)
                    throw new InvalidDataException(
                        @"Source logic is not syntactically correct. 
                        Please check the documentation for more information.");

                reader.Next();
                t = reader.Read();
                string role2 = "";
                if (t.Role == eTokenRole.role)
                {
                    role2 = t.Code;
                    if (reader.Peek().Role != eTokenRole.comma)
                        throw new InvalidDataException(
                        @"Source logic is not syntactically correct. 
                        Please check the documentation for more information.");
                    reader.Next();
                    t = reader.Read();
                }
                if (t.Role != eTokenRole.pattern)
                    throw new InvalidDataException(
                        @"Source logic is not syntactically correct. 
                        Please check the documentation for more information.");
                if (reader.Peek().Role != eTokenRole.lineend)
                    throw new InvalidDataException(
                        @"Source logic is not syntactically correct. 
                        Please check the documentation for more information.");
                reader.Next();

                string pattern = t.Code;

                if (role2 == "")
                {
                    rdefinitions.Add(@"Regexes.Add(new TokenRegex(eTokenRole."+ role +", " + pattern + "))");
                    if (!enums.Contains(role)) enums.Add(role);
                }
                else
                {
                    mdefinitions.Add(@"Manipulations.Add(new TokenRegexReplacer(eTokenRole."+role+", eTokenRole."+role2+", " + pattern + "))");
                    if (!enums.Contains(role)) enums.Add(role);
                    if (!enums.Contains(role2)) enums.Add(role2);
                }
            }

            string scr = GetEmbeddedResourceS(@"LexerGenerator.scr.cs");
            scr = scr.Replace(@"//<placeholder_namespace_name>", name);
            scr = scr.Replace(@"//<placeholder_enum>", string.Join("," + Environment.NewLine + "        ", enums) + ",");
            scr = scr.Replace(@"//<placeholder_regexes>", string.Join(";" + Environment.NewLine + "            ", rdefinitions) + ";");
            scr = scr.Replace(@"//<placeholder_manipulations>", string.Join(";" + Environment.NewLine + "            ", mdefinitions) + ";");
            return scr;
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
}
