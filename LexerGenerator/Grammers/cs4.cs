skipped;

skipped, @"\s|\u000d\u000a|\u000d|\u000a|\u0085|\u2028|\p{Zs}|\u0009|\u000b|\u000c";
comment, @"(//(.*?)(\r?\n|$))|(/\*([^*]*\*)*?/)";
directive, @"(#region(.*?)(\r?\n|$))|(#endregion)";
character_literal, @"'([^'\\\n]|\\'|\\""|\\\\|\\0|\\a|\\b|\\f|\\n|\\r|\\t|\\v|\\x[0-9A-Fa-f]|\\x[0-9A-Fa-f]{2}|\\x[0-9A-Fa-f]{3}|\\x[0-9A-Fa-f]{4}|\\u[0-9A-Fa-f]{4}|\\U[0-9A-Fa-f]{8})'";
string_literal, @"(@""((""""|[^""])+)?"")|(""(\\'|\\""|\\\\|\\0|\\a|\\b|\\f|\\n|\\r|\\t|\\v|\\x[0-9A-Fa-f]|\\x[0-9A-Fa-f]{2}|\\x[0-9A-Fa-f]{3}|\\x[0-9A-Fa-f]{4}|\\u[0-9A-Fa-f]{4}|\\U[0-9A-Fa-f]{8}|[^""\n])*"")";
integer_literal, @"(0x[0-9A-Fa-f]+(UL|Ul|uL|ul|U|u|LU|Lu|lU|lu|L|l)?)|(([0-9])+(UL|Ul|uL|ul|U|u|LU|Lu|lU|lu|L|l)?)";
real_literal, @"([0-9]+\.[0-9]+([eE](\+|-)?[0-9]+)?[FfDdMm]?)|(\.[0-9]+([eE](\+|-)?[0-9]+)?[FfDdMm]?)|([0-9]+[eE](\+|-)?[0-9]+[FfDdMm]?)|(([0-9])+[FfDdMm])";
identifier, @"(\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}|_)((\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}|\p{Nd}|\p{Pc}|\p{Mn}|\p{Mc}|\p{Cf})+)*";
operator_or_punctuator, @"\{|}|\[|]|\(|\)|\.|,|::|:|;|\+\+|\+=|\+|--|->|-=|-|\*=|\*|/=|/|%=|%|&=|&&|&|\|\||\|=|\||\^=|\^|!=|!|~|==|=>|=|<=|<<|<|>=|>|\?\?|\?|<<=";

identifier, keyword, @"abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|double|do|else|enum|event|explicit|extern|false|finally|fixed|float|foreach|for|goto|if|implicit|internal|interface|int|in|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while";
keyword, primitive, @"bool|byte|char|decimal|double|float|int|long|object|sbyte|short|string|uint|ulong|ushort";