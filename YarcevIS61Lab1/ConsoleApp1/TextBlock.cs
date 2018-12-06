using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Textblock - Продукт:
    /// Предоставляет интерфейс для взаимодействия с текстовыми блоками.
    /// </summary>
    abstract class TextBlock
    {
        public abstract void InputContent(string input);
        public abstract void ShowContent();
        public abstract string GetContent();
    }
    /// <summary>
    /// ConcreteTextblock - Конкретный продукт:
    /// Реализует интерфейс предоставляемый базовым классом Textblock.
    /// </summary>
    class XTextBlock : TextBlock
    {
        public string content = "";
        public XTextBlock()
        { }
        public override void InputContent(string input)
        {
            this.content = input;
        }
        public override void ShowContent()
        {
            Console.Write(this.content);
        }
        public override string GetContent()
        {
            return this.content;
        }
    }
    /// <summary>
    /// ConcreteTextblock - Конкретный продукт:
    /// Реализует интерфейс предоставляемый базовым классом Textblock.
    /// </summary>
    class HtmlTextBlock : TextBlock
    {
        public string content = "";
        public HtmlTextBlock()
        { }
        public override void InputContent(string input)
        {
            this.content = input;
        }
        public override void ShowContent()
        {
            Console.Write(this.content);
        }
        public override string GetContent()
        {
            return this.content;
        }
    }
    /// <summary>
    /// ConcreteTextblock - Конкретный продукт:
    /// Реализует интерфейс предоставляемый базовым классом Textblock.
    /// </summary>
    class MarkdownTextBlock : TextBlock
    {
        public string content = "";
        public MarkdownTextBlock()
        { }
        public override void InputContent(string input)
        {
            this.content = input;
        }
        public override void ShowContent()
        {
            Console.Write(this.content);
        }
        public override string GetContent()
        {
            return this.content;
        }
    }
}
