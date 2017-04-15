# LexGen
C# Lexer Generator

This is a lexical analyzer generator, that is - a tool that takes some 
grammer as an input and emits a lexical analizer source code.
Very much like "lex"/"flex".

Unlike "lex"/"flex" however, this one emits C# lexer instead of C++.
It is also somehow more intuitive to use, and have the option to reassign token roles.
It is a quite simple implementation, too.

Unfortunately, the parser generator that goes with it is still under active development,
so that is a kind of a drawback for now.

Also, if you have your own set of regexes based on some spec I would be more then happy
to test them and add to the library if working fine..


Usage :
___________________________________________________________________________________________________________________________
Create an input using following rules : 

//Grammer File syntax:
\<role>;                         //remove tokens with given role
\<role>, \<pattern>;		//add tokens with a given role matching a given pattern
\<role>, \<role>, \<pattern>;	//change tokens with a given role matching a given pattern
		
\<role>                          //a valid C# identifier
\<pattern>                       //a valid C# verbatim string representing a valid C# regex with a SingleLine option
                                            
                                //whitespaces are ignored.
                                //"," is delimiter
                                //";" is lineend
___________________________________________________________________________________________________________________________
An example of such a file 
(it can be just a string realy, but is kind of inconvenient) :

skipped;

skipped, @"\s|\u000d\u000a|\u000d|\u000a|\u0085|\u2028|\p{Zs}|\u0009|\u000b|\u000c";

comment, @"(//(.*?)(\r?\n|$))|(/\*([^*]*\*)*?/)";

directive, @"(#region(.*?)(\r?\n|$))|(#endregion)";

identifier, @"[a-z_]*";


identifier, keyword, @"abstract|as|base|bool|break";
___________________________________________________________________________________________________________________________

using LexerGenerator;

//generate using existing logic

string CSLexer = Generator.GenerateCS4();

//or using your own logic, that's the whole point after all

string MyLogic = File.ReadAllText(@"MyLogic.txt");
string MyNamespaceName = "MyLexer.Basic";                       //e.g.
string MyLexer = Generator.GenerateCS4(MyLogic, MyNamespaceName);

