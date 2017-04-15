﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LexerGenerator
{
    public enum eTokenRole
    {
        role,
        pattern,

        comma,
        lineend,

        skipped,
        unknown_sign,
    }

    public class TokenRegexList
    {
        public List<TokenRegex> Regexes;

        public TokenRegexList()
        {
            Regexes = new List<TokenRegex>();

            Regexes.Add(new TokenRegex(eTokenRole.skipped, @"\s"));
            Regexes.Add(new TokenRegex(eTokenRole.pattern, @"@(?:""[^""]*"")+"));
            Regexes.Add(new TokenRegex(eTokenRole.role, @"(\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}|_)((\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}|\p{Nd}|\p{Pc}|\p{Mn}|\p{Mc}|\p{Cf})+)*"));
            Regexes.Add(new TokenRegex(eTokenRole.comma, @","));
            Regexes.Add(new TokenRegex(eTokenRole.lineend, @";"));
        }

        public List<TokenRegex> ToList()
        {
            return Regexes;
        }
    }
    public class TokenRegex
    {
        public eTokenRole Role;
        Regex _regex;

        public TokenRegex(eTokenRole role, string regex)
        {
            Role = role;
            _regex = new Regex("^(" + regex + ")", RegexOptions.Singleline);
        }

        public string Match(string s)
        {
            Match match = _regex.Match(s);
            if (match.Success) return match.Value;
            else return null;
        }
    }

    public class TokenManipulatorList
    {
        public List<TokenManipulator> Manipulations;

        public TokenManipulatorList()
        {
            Manipulations = new List<TokenManipulator>();

            Manipulations.Add(new TokenRemover(eTokenRole.skipped));
        }

        public List<TokenManipulator> ToList()
        {
            return Manipulations;
        }
    }
    public class TokenManipulator
    {

    }
    public class TokenRemover : TokenManipulator
    {
        public eTokenRole Role;

        public TokenRemover(eTokenRole role)
        {
            Role = role;
        }
    }
    public class TokenRegexReplacer : TokenManipulator
    {
        public eTokenRole Role;
        public eTokenRole NewRole;
        Regex _regex;

        public TokenRegexReplacer(eTokenRole role, eTokenRole newRole, string regex)
        {
            Role = role;
            NewRole = newRole;
            _regex = new Regex("^(" + regex + ")", RegexOptions.Singleline);
        }

        public bool isMatch(string s)
        {
            Match match = _regex.Match(s);
            if (match.Success && match.Value.Length == s.Length) return true;
            else return false;
        }
    }

    public class TokenReader
    {
        List<Token> _tokens;
        int _pos;

        public List<Token> Tokens
        {
            get { return _tokens; }
        }
        public bool isEnded
        {
            get
            {
                return _pos >= _tokens.Count;
            }
        }
        public int Pos
        {
            get { return _pos; }
        }

        public TokenReader(List<Token> tokens)
        {
            _tokens = tokens;
            _pos = 0;
        }

        public void Restart()
        {
            _pos = 0;
        }
        public Token Read()
        {
            if (_pos < _tokens.Count)
            {
                Token t = _tokens[_pos];
                _pos++;
                return t;
            }
            return null;
        }
        public Token Peek()
        {
            if (_pos < _tokens.Count)
            {
                Token t = _tokens[_pos];
                return t;
            }
            return null;
        }
        public Token Peek(int pos)
        {
            if (pos < _tokens.Count && pos > -1)
            {
                Token t = _tokens[pos];
                return t;
            }
            return null;
        }
        public Token PeekNext()
        {
            if (_pos + 1 < _tokens.Count)
            {
                Token t = _tokens[_pos + 1];
                return t;
            }
            return null;
        }
        public Token PeekNext(int next)
        {
            if (_pos + next < _tokens.Count)
            {
                Token t = _tokens[_pos + next];
                return t;
            }
            return null;
        }
        public Token PeekBack()
        {
            if (_pos > 1)
            {
                Token t = _tokens[_pos - 1];
                return t;
            }
            return null;
        }
        public Token PeekBack(int back)
        {
            if (_pos > back)
            {
                Token t = _tokens[_pos - back];
                return t;
            }
            return null;
        }
        public void Back()
        {
            if (_pos > 0) _pos--;
        }
        public void Next()
        {
            if (_pos < _tokens.Count) _pos++;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < _tokens.Count; i++)
            {
                if (string.IsNullOrEmpty(s)) s += Environment.NewLine;
                s += i.ToString().PadRight(20) + _tokens[i].Role + "   :   " + _tokens[i].Code;
            }
            return s;
        }
    }
    public class Token
    {
        eTokenRole _role;
        string _code;

        public eTokenRole Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        public string Code
        {
            get
            {
                return _code;
            }
        }

        public Token(eTokenRole role)
        {
            _role = role;
            _code = role.ToString();
        }
        public Token(eTokenRole role, string code)
        {
            _role = role;
            _code = code;
        }
    }
    public class Lexer
    {
        TokenRegexList REGEXES = new TokenRegexList();
        TokenManipulatorList MANIPULATIONS = new TokenManipulatorList();

        string _source;
        public List<Token> _tokens;
        public bool Tokenized;

        public Lexer(string source)
        {
            Tokenized = false;
            _tokens = new List<Token>();
            _source = source;
        }

        public TokenReader Tokenize()
        {
            if (Tokenized) return null;
            try
            {
                while (_source != "")
                {
                    _tokens.Add(scan());
                }
                Tokenized = true;
                foreach (TokenManipulator manip in MANIPULATIONS.ToList())
                {
                    if (manip is TokenRemover)
                    {
                        TokenRemover m = (TokenRemover)manip;
                        List<Token> newList = new List<Token>();
                        foreach (Token t in _tokens)
                            if (t.Role != m.Role) newList.Add(t);
                        _tokens = newList;
                    }
                    if (manip is TokenRegexReplacer)
                    {
                        TokenRegexReplacer m = (TokenRegexReplacer)manip;
                        List<Token> newList = new List<Token>();
                        foreach (Token t in _tokens)
                            if (t.Role == m.Role && m.isMatch(t.Code)) newList.Add(new Token(m.NewRole, t.Code));
                        _tokens = newList;
                    }
                }
                return new TokenReader(_tokens);
            }
            catch (Exception ex)
            {
                //string toks = GetTokens();
                //string msg = ex.Message + Environment.NewLine + ex.StackTrace;
                return null;
            }
        }
        Token scan()
        {
            foreach (TokenRegex regex in REGEXES.ToList())
            {
                string s = regex.Match(_source);
                if (s != null && s != "")
                {
                    _source = _source.Substring(s.Length, _source.Length - s.Length);
                    return new Token(regex.Role, s);
                }
            }

            string z = _source[0].ToString();
            _source = _source.Substring(1, _source.Length - 1);
            return new Token(eTokenRole.unknown_sign, z);
        }

        public List<Token> Tokens
        {
            get
            {
                return _tokens;
            }
        }
        public string GetTokens()
        {
            string s = "";
            foreach (Token t in _tokens)
            {
                if (t.Code != null)
                    s += t.Role.ToString().PadRight(40) + t.Code + Environment.NewLine;
                else
                    s += t.Role.ToString().PadRight(40) + t.Role + Environment.NewLine;
            }
            return s;
        }
        public TokenReader GetReader()
        {
            return new TokenReader(_tokens);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < _tokens.Count; i++)
            {
                if (!string.IsNullOrEmpty(s)) s += Environment.NewLine;
                s += _tokens[i].Code.Replace(Environment.NewLine, "").PadRight(50) + "//" + _tokens[i].Role.ToString().PadRight(25) + i.ToString();
            }
            return s;
        }
    }
}