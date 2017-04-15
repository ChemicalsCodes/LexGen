using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChemicalWarfare.RUnits
{
    internal abstract class RItem
    {
        protected const int INDENT_STEP = 4;
        protected static string getIndent(int indent)
        {
            string s = "";
            for (int i = 1; i <= indent; i++)
            {
                s += ' ';
            }
            return s;
        }
        protected static string getIndented(int indent, string s)
        {
            string[] sep = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < sep.Count(); i++)
            {
                sep[i] = getIndent(indent) + sep[i];
            }
            return string.Join(Environment.NewLine, sep);
        }

        string _name;

        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        public RItem(string name)
        {
            _name = name;
        }

        public abstract string GetString();
        public abstract string GetStringSpaced(int indent = 0);
    }
    internal abstract class RContainableItem : RItem
    {
        string _fullname;
        public RContainableItem RootMember
        {
            get;
            set;
        }

        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        public RContainableItem(string name)
            : base(name)
        { }
    }
    internal abstract class RModifiableItem : RItem
    {
        protected List<RModifier> _modifiers;

        public RModifiableItem(string name)
            : base(name)
        {
            _modifiers = new List<RModifier>();
        }
        public RModifiableItem(string name, RModifier m1)
            : base(name)
        {
            _modifiers = new List<RModifier>();
            _modifiers.Add(m1);
        }
        public RModifiableItem(string name, RModifier m1, RModifier m2)
            : base(name)
        {
            _modifiers = new List<RModifier>();
            _modifiers.Add(m1);
            _modifiers.Add(m2);
        }

        protected string getModifiers()
        {
            string s = "";

            foreach (RModifier mod in _modifiers)
            {
                switch (mod)
                {
                    case RModifier.rpublic:
                        if (s == "") s = "public";
                        else s = "public" + " " + s;
                        break;
                    case RModifier.rinternal:
                        if (s == "") s = "internal";
                        else s = "internal" + " " + s;
                        break;
                    case RModifier.rprivate:
                        if (s == "") s = "private";
                        else s = "private" + " " + s;
                        break;
                    case RModifier.rprotected:
                        if (s == "") s = "protected";
                        else s = "protected" + " " + s;
                        break;
                    case RModifier.rsealed:
                        if (s == "") s = "sealed";
                        else s += " sealed";
                        break;
                    case RModifier.rstatic:
                        if (s == "") s = "static";
                        else s += " static";
                        break;
                    case RModifier.rconst:
                        if (s == "") s = "const";
                        else s += " const";
                        break;
                    case RModifier.rreadonly:
                        if (s == "") s = "readonly";
                        else s += " readonly";
                        break;
                    case RModifier.rvirtual:
                        if (s == "") s = "virtual";
                        else s += " virtual";
                        break;
                    case RModifier.rabstract:
                        if (s == "") s = "abstract";
                        else s += " abstract";
                        break;
                    case RModifier.rref:
                        s = "ref";
                        break;
                    case RModifier.rout:
                        s = "out";
                        break;
                    case RModifier.roverride:
                        if (s == "") s = "override";
                        else s += " override";
                        break;
                }
            }

            return s;
        }
    }
    internal abstract class RModifiableCodeableItem : RModifiableItem
    {
        object _code;
        protected bool hasCode
        {
            get { return _code != null; }
        }

        public RModifiableCodeableItem(string name)
            : base(name)
        { }
        public RModifiableCodeableItem(string name, RModifier m1)
            : base(name, m1)
        { }
        public RModifiableCodeableItem(string name, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { }
        public RModifiableCodeableItem(string name, object code)
            : base(name)
        { _code = code; }
        public RModifiableCodeableItem(string name, object code, RModifier m1)
            : base(name, m1)
        { _code = code; }
        public RModifiableCodeableItem(string name, object code, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { _code = code; }


        protected void addCode(object code)
        {
            _code = code;
        }
        protected string getCode()
        {
            string s = "";

            if(_code != null) s = _code.ToString();

            return s;
        }
    }
    internal abstract class RModifiableTypeableItem : RModifiableItem
    {
        protected object Typed
        {
            get;
            private set;
        }

        public RModifiableTypeableItem(string name)
            : base(name)
        { }
        public RModifiableTypeableItem(string name, RModifier m1)
            : base(name, m1)
        { }
        public RModifiableTypeableItem(string name, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { }
        public RModifiableTypeableItem(string name, object type)
            : base(name)
        {
            Typed = type;
        }
        public RModifiableTypeableItem(string name, object type, RModifier m1)
            : base(name, m1)
        {
            Typed = type;
        }
        public RModifiableTypeableItem(string name, object type, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        {
            Typed = type;
        }

        protected string getType()
        {
            string s = "";

            s = Typed.ToString();

            return s;
        }
    }
    internal abstract class RContainableModifiableItem : RContainableItem
    {
        protected List<RModifier> _modifiers;

        public RContainableModifiableItem(string name)
            : base(name)
        {
            _modifiers = new List<RModifier>();
        }
        public RContainableModifiableItem(string name, RModifier m1)
            : base(name)
        {
            _modifiers = new List<RModifier>();
            _modifiers.Add(m1);
        }
        public RContainableModifiableItem(string name, RModifier m1, RModifier m2)
            : base(name)
        {
            _modifiers = new List<RModifier>();
            _modifiers.Add(m1);
            _modifiers.Add(m2);
        }
        public RContainableModifiableItem(string name, RModifier m1, RModifier m2, RModifier m3)
            : base(name)
        {
            _modifiers = new List<RModifier>();
            _modifiers.Add(m1);
            _modifiers.Add(m2);
            _modifiers.Add(m3);
        }

        protected string getModifiers()
        {
            string s = "";

            foreach (RModifier mod in _modifiers)
            {
                switch (mod)
                {
                    case RModifier.rpublic:
                        if (s == "") s = "public";
                        else s = "public" + " " + s;
                        break;
                    case RModifier.rinternal:
                        if (s == "") s = "internal";
                        else s = "internal" + " " + s;
                        break;
                    case RModifier.rprivate:
                        if (s == "") s = "private";
                        else s = "private" + " " + s;
                        break;
                    case RModifier.rprotected:
                        if (s == "") s = "protected";
                        else s = "protected" + " " + s;
                        break;
                    case RModifier.rsealed:
                        if (s == "") s = "sealed";
                        else s += " sealed";
                        break;
                    case RModifier.rstatic:
                        if (s == "") s = "static";
                        else s += " static";
                        break;
                    case RModifier.rconst:
                        if (s == "") s = "const";
                        else s += " const";
                        break;
                    case RModifier.rreadonly:
                        if (s == "") s = "readonly";
                        else s += " readonly";
                        break;
                    case RModifier.rvirtual:
                        if (s == "") s = "virtual";
                        else s += " virtual";
                        break;
                    case RModifier.rabstract:
                        if (s == "") s = "abstract";
                        else s += " abstract";
                        break;
                    case RModifier.roverride:
                        if (s == "") s = "override";
                        s = " override";
                        break;
                }
            }

            return s;
        }
    }
    internal abstract class RContainableModifiableTypeableItem : RContainableModifiableItem
    {
        protected object Typed
        {
            get;
            private set;
        }

        public RContainableModifiableTypeableItem(string name)
            : base(name)
        { }
        public RContainableModifiableTypeableItem(string name, RModifier m1)
            : base(name, m1)
        { }
        public RContainableModifiableTypeableItem(string name, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { }
        public RContainableModifiableTypeableItem(string name, object type)
            : base(name)
        {
            Typed = type;
        }
        public RContainableModifiableTypeableItem(string name, object type, RModifier m1)
            : base(name, m1)
        {
            Typed = type;
        }
        public RContainableModifiableTypeableItem(string name, object type, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        {
            Typed = type;
        }

        public string GetType()
        {
            string s = "";

            s = Typed.ToString();

            return s;
        }
    }
    internal abstract class RContainableModifiableTypeableCodeableItem : RContainableModifiableTypeableItem
    {
        object _code;
        protected bool hasCode
        {
            get { return _code != null; }
        }

        public RContainableModifiableTypeableCodeableItem(string name)
            : base(name)
        { }
        public RContainableModifiableTypeableCodeableItem(string name, RModifier m1)
            : base(name, m1)
        { }
        public RContainableModifiableTypeableCodeableItem(string name, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { }
        public RContainableModifiableTypeableCodeableItem(string name, object type)
            : base(name, type)
        { }
        public RContainableModifiableTypeableCodeableItem(string name, object type, RModifier m1)
            : base(name, type, m1)
        { }
        public RContainableModifiableTypeableCodeableItem(string name, object type, RModifier m1, RModifier m2)
            : base(name, type, m1, m2)
        { }
        public RContainableModifiableTypeableCodeableItem(string name, object type, object code)
            : base(name, type)
        {
            _code = code;
        }
        public RContainableModifiableTypeableCodeableItem(string name, object type, object code, RModifier m1)
            : base(name, type, m1)
        {
            _code = code;
        }
        public RContainableModifiableTypeableCodeableItem(string name, object type, object code, RModifier m1, RModifier m2)
            : base(name, type, m1, m2)
        {
            _code = code;
        }


        public void AddCode(object code)
        {
            _code = code;
        }
        protected string getCode()
        {
            string s = null;

            if (_code != null) s = _code.ToString();

            return s;
        }
    }

    internal abstract class RContainer : RContainableItem
    {
        protected List<RContainableItem> _children;

        public RContainer(string name)
            : base(name)
        {
            _children = new List<RContainableItem>();
        }

        public void AddChild(RContainableItem item)
        {
            if (_children.Contains(item)) return;

            if (this.FullName != null)
            {
                item.FullName = this.FullName + "." + item.Name;
            }
            else
            {
                item.FullName = this.Name + "." + item.Name;
            }
            if (this.RootMember != null)
            {
                item.RootMember = this.RootMember;
            }
            else
            {
                item.RootMember = this;
            }
            _children.Add(item);
        }
    }
    internal abstract class RModifiableContainer : RContainableModifiableItem
    {
        protected List<RContainableItem> _children;

        public RModifiableContainer(string name)
            : base(name)
        {
            _children = new List<RContainableItem>();
        }
        public RModifiableContainer(string name, RModifier m1)
            : base(name, m1)
        {
            _children = new List<RContainableItem>();
        }
        public RModifiableContainer(string name, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        {
            _children = new List<RContainableItem>();
        }
        public RModifiableContainer(string name, RModifier m1, RModifier m2, RModifier m3)
            : base(name, m1, m2, m3)
        {
            _children = new List<RContainableItem>();
        }

        public void AddChild(RContainableItem item)
        {
            if (_children.Contains(item)) return;

            if (this.FullName != null)
            {
                item.FullName = this.FullName + "." + item.Name;
            }
            else
            {
                item.FullName = this.Name + "." + item.Name;
            }
            if (this.RootMember != null)
            {
                item.RootMember = this.RootMember;
            }
            else
            {
                item.RootMember = this;
            }
            _children.Add(item);
        }
    }
    internal abstract class RInheritableContainer : RModifiableContainer
    {
        protected object Base
        {
            get;
            private set;
        }

        public RInheritableContainer(string name)
            : base(name)
        { }
        public RInheritableContainer(string name, RModifier m1)
            : base(name, m1)
        { }
        public RInheritableContainer(string name, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { }
        public RInheritableContainer(string name, RModifier m1, RModifier m2, RModifier m3)
            : base(name, m1, m2, m3)
        { }
        public RInheritableContainer(string name, object baseType)
            : base(name)
        { Base = baseType; }
        public RInheritableContainer(string name, object baseType, RModifier m1)
            : base(name, m1)
        { Base = baseType; }
        public RInheritableContainer(string name, object baseType, RModifier m1, RModifier m2)
            : base(name, m1, m2)
        { Base = baseType; }
        public RInheritableContainer(string name, object baseType, RModifier m1, RModifier m2, RModifier m3)
            : base(name, m1, m2, m3)
        { Base = baseType; }

        protected string getBase()
        {
            string s = "";

            s = Base.ToString();

            return s;
        }
    }
    internal abstract class RUseableContainer : RContainer
    {
        public List<RUsing> _usings;
        bool hasUsing(List<RUsing> list, string u)
        {
            List<string> us = new List<string>();
            foreach (RUsing use in list) us.Add(use.Name);

            if (us.Contains(u)) return true;
            else return false;
        }

        public RUseableContainer(string name)
            : base(name)
        {
            setDefaultUsings();
        }

        new public void AddChild(RContainableItem item)
        {
            base.AddChild(item);
            if (item is RUseableContainer)
            {
                RUseableContainer ruc = (RUseableContainer)item;
                foreach (RUsing ru in ruc._usings)
                {
                    this.addUsing(ru);
                }
                ruc._usings = new List<RUsing>();
            }
        }

        public void setDefaultUsings()
        {
            _usings = new List<RUsing>();
            _usings.Add(new RUsing("System"));
            _usings.Add(new RUsing("System.Collections.Generic"));
            _usings.Add(new RUsing("System.Linq"));
            _usings.Add(new RUsing("System.Text"));
        }

        public void addUsing(string nspace)
        {
            List<RUsing> toadd = _usings;
            if (RootMember != null && RootMember is RUseableContainer)
            {
                toadd = ((RUseableContainer)RootMember)._usings;
            }

            RUsing ru = new RUsing(nspace);
            if (!hasUsing(toadd, ru.Name))
            {
                toadd.Add(ru);
            }
        }
        public void addUsing(RUsing u)
        {
            List<RUsing> toadd = _usings;
            if (RootMember != null && RootMember is RUseableContainer)
            {
                toadd = ((RUseableContainer)RootMember)._usings;
            }

            if (!hasUsing(toadd, u.Name))
            {
                toadd.Add(u);
            }
        }
    }

    internal abstract class RItemlessContainer : RItem
    {
        protected List<RContainableItem> _children;

        public RItemlessContainer(string name = "RItemlessContainer")
            : base(name)
        {
            _children = new List<RContainableItem>();
        }

        public void AddChild(RContainableItem item)
        {
            if (_children.Contains(item)) return;

            _children.Add(item);
        }
    }
    internal abstract class RItemlessUseableContainer : RItemlessContainer
    {
        public List<RUsing> _usings;
        bool hasUsing(string u)
        {
            List<string> us = new List<string>();
            foreach (RUsing use in _usings) us.Add(use.Name);

            if (us.Contains(u)) return true;
            else return false;
        }

        public RItemlessUseableContainer(string name = "RItemlessUseableContainer") 
            : base(name)
        {
            setDefaultUsings();
        }

        new public void AddChild(RContainableItem item)
        {
            base.AddChild(item);
            if (item is RUseableContainer)
            {
                RUseableContainer ruc = (RUseableContainer)item;
                foreach (RUsing ru in ruc._usings)
                {
                    this.addUsing(ru);
                }
                ruc._usings = new List<RUsing>();
            }
        }

        public void setDefaultUsings()
        {
            _usings = new List<RUsing>();
            _usings.Add(new RUsing("System"));
            _usings.Add(new RUsing("System.Collections.Generic"));
            _usings.Add(new RUsing("System.Linq"));
            _usings.Add(new RUsing("System.Text"));
        }

        public void addUsing(string nspace)
        {
            RUsing ru = new RUsing(nspace);
            if (!hasUsing(ru.Name))
            {
                _usings.Add(ru);
            }
        }
        public void addUsing(RUsing u)
        {
            if (!hasUsing(u.Name))
            {
                _usings.Add(u);
            }
        }
    }
    internal abstract class RItemlessUseableAttributeableContainer : RItemlessUseableContainer
    {
        List<object> _attributes;
        protected bool hasAttributes
        {
            get { return _attributes != null; }
        }

        public RItemlessUseableAttributeableContainer(string name)
            : base(name)
        { }

        public void addAttribute(object attribute)
        {
            if (_attributes == null) _attributes = new List<object>();
            _attributes.Add(attribute);
        }
        public void addAttributes(IEnumerable<object> attributes)
        {
            if (_attributes == null) _attributes = new List<object>();
            foreach(object o in attributes) _attributes.Add(o);
        }
        protected string getAttributes()
        {
            string s = null;

            if (hasAttributes && _attributes.Count > 0)
            {
                s = _attributes[0].ToString();
                for (int i = 1; i < _attributes.Count; i++ )
                {
                    s += " " + _attributes[i].ToString();
                }
            }

            return s;
        }
        protected string getAttributesSpaced()
        {
            string s = null;

            if (hasAttributes && _attributes.Count > 0)
            {
                s = _attributes[0].ToString();
                for (int i = 1; i < _attributes.Count; i++)
                {
                    s += Environment.NewLine + _attributes[i].ToString();
                }
            }

            return s;
        }
    }
}

//delegate
//event
//generics
//interfaces
//operator overloading