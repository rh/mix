using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Shows all selected nodes.")]
    public class Show : Task, IReadOnly
    {
        protected const ConsoleColor BracketColor = ConsoleColor.DarkMagenta;
        protected const ConsoleColor ElementColor = ConsoleColor.DarkMagenta;
        protected const ConsoleColor AttributeColor = ConsoleColor.DarkYellow;
        protected const ConsoleColor StringColor = ConsoleColor.DarkGray;
        protected const ConsoleColor TextColor = ConsoleColor.DarkGray;
        protected const ConsoleColor CDataColor = ConsoleColor.DarkGray;
        protected const ConsoleColor CommentColor = ConsoleColor.DarkGray;
        protected const ConsoleColor ProcessingInstructionColor = ConsoleColor.DarkGray;

        protected ConsoleColor ForegroundColor;

        [Argument, Description("If set, attributes are not shown in the output.")]
        public bool SkipAttributes { get; set; }

        [Argument, Description("If set, comments are not shown in the output.")]
        public bool SkipComments { get; set; }

        [Argument, Description("If set, processing instructions are not shown in the output.")]
        public bool SkipProcessingInstructions { get; set; }

        public Show()
        {
            ForegroundColor = Console.ForegroundColor;
        }

        protected override void ExecuteCore(XmlDocument document)
        {
            Print(document.DocumentElement, 0);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            Print(element, 0);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            Print(attribute);
            Context.Output.WriteLine();
        }

        protected override void ExecuteCore(XmlText text)
        {
            Print(text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            Print(section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            Print(comment);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            Print(instruction);
        }

        private void Print(XmlElement element, int level)
        {
            Indent(level);
            Console.ForegroundColor = BracketColor;
            Context.Output.Write("<");
            Console.ForegroundColor = ElementColor;
            Context.Output.Write(element.Name);

            if (element.HasAttributes)
            {
                PrintAttributes(element);
            }

            if (element.HasChildNodes)
            {
                Console.ForegroundColor = BracketColor;
                Context.Output.Write(">");

                PrintChildNodes(element, level);

                Console.ForegroundColor = BracketColor;
                Context.Output.Write("</");
                Console.ForegroundColor = ElementColor;
                Context.Output.Write(element.LocalName);
                Console.ForegroundColor = BracketColor;
                Context.Output.WriteLine(">");
            }
            else
            {
                Console.ForegroundColor = BracketColor;
                Context.Output.WriteLine(" />");
            }
        }

        private void PrintAttributes(XmlElement element)
        {
            foreach (XmlAttribute attribute in element.Attributes)
            {
                Print(attribute);
            }
        }

        private void PrintChildNodes(XmlElement element, int level)
        {
            if (element.ChildNodes.Count == 1)
            {
                PrintSingleChildNode(element, level);
            }
            else
            {
                PrintAllChildNodes(element, level);
            }
        }

        private void PrintSingleChildNode(XmlElement element, int level)
        {
            var node = element.FirstChild;
            if (node is XmlElement)
            {
                Context.Output.WriteLine();
                Print(node as XmlElement, level + 1);
                Indent(level);
            }
            else if (node is XmlText)
            {
                Print(node as XmlText);
            }
            else if (node is XmlCDataSection)
            {
                Print(node as XmlCDataSection);
            }
            else if (node is XmlComment)
            {
                if (SkipComments == false)
                {
                    Context.Output.WriteLine();
                    Indent(level + 1);
                    Print(node as XmlComment);
                    Context.Output.WriteLine();
                    Indent(level);
                }
            }
            else if (node is XmlProcessingInstruction)
            {
                Print(node as XmlProcessingInstruction);
            }
        }

        private void PrintAllChildNodes(XmlElement element, int level)
        {
            Context.Output.WriteLine();
            foreach (var node in element.ChildNodes)
            {
                if (node is XmlElement)
                {
                    Print(node as XmlElement, level + 1);
                }
                else if (node is XmlText)
                {
                    Indent(level + 1);
                    Print(node as XmlText);
                    Context.Output.WriteLine();
                }
                else if (node is XmlCDataSection)
                {
                    Indent(level + 1);
                    Print(node as XmlCDataSection);
                    Context.Output.WriteLine();
                }
                else if (node is XmlComment)
                {
                    if (SkipComments == false)
                    {
                        Indent(level + 1);
                        Print(node as XmlComment);
                        Context.Output.WriteLine();
                    }
                }
                else if (node is XmlProcessingInstruction)
                {
                    if (SkipProcessingInstructions == false)
                    {
                        Indent(level + 1);
                        Print(node as XmlProcessingInstruction);
                        Context.Output.WriteLine();
                    }
                }
            }
            Indent(level);
        }

        private void Print(XmlAttribute attribute)
        {
            if (SkipAttributes)
            {
                return;
            }

            Console.ForegroundColor = AttributeColor;
            Context.Output.Write(" " + attribute.Name);
            Console.ForegroundColor = StringColor;
            Context.Output.Write("=\"" + attribute.Value + "\"");
        }

        public void Print(XmlText text)
        {
            Console.ForegroundColor = TextColor;
            Context.Output.Write(text.Value.Trim());
        }

        public void Print(XmlCDataSection section)
        {
            Console.ForegroundColor = BracketColor;
            Context.Output.Write("<![CDATA[");
            Console.ForegroundColor = CDataColor;
            Context.Output.Write(section.Value);
            Console.ForegroundColor = BracketColor;
            Context.Output.Write("]]>");
        }

        private void Print(XmlComment comment)
        {
            if (SkipComments)
            {
                return;
            }

            Console.ForegroundColor = CommentColor;
            Context.Output.Write("<!--" + comment.Value + "-->");
        }

        public void Print(XmlProcessingInstruction instruction)
        {
            if (SkipProcessingInstructions)
            {
                return;
            }

            Console.ForegroundColor = BracketColor;
            Context.Output.Write("<?" + instruction.Name);
            Console.ForegroundColor = ProcessingInstructionColor;
            Context.Output.Write(" " + instruction.Value);
            Console.ForegroundColor = BracketColor;
            Context.Output.Write("?>");
        }

        private void Indent(int level)
        {
            Context.Output.Write(new string(' ', 2 * level));
        }

        protected override void OnBeforeExecute(int count)
        {
            Context.Output.WriteLine("{0}: {1}", Context.FileName, count);
            ForegroundColor = Console.ForegroundColor;
        }

        protected override void OnAfterExecute()
        {
            Console.ForegroundColor = ForegroundColor;
        }
    }
}