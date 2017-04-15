//Grammer File syntax:

<role>;                                     //remove tokens with given role
<role>, <pattern>;							//add tokens with a given role matching a given pattern
<role>, <role>, <pattern>;	                //change tokens with a given role matching a given pattern
		
<role>                                      //a valid C# identifier
<pattern>                                   //a valid C# verbatim string representing a valid C# regex with a SingleLine option