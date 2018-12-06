using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Creator - Создатель:
    /// Предоставляет интерфейс(абстрактные фабричные методы) 
    /// для порождения продуктов.
    /// В частных случаях класс Creator может предоставлять реализацию фабричных методов, 
    /// которые возвращают экземпляры продуктов(ConcreteTextBlock)
    /// </summary>
    abstract class Creator
    {
        public abstract TextBlock CreateMethod(string text);
    }
    /// <summary>
    /// ConcreteCreator - Конкретная фабрика:
    /// Реализует интерфейс(фабричные методы) предоставляемый базовым классом Creator.
    /// </summary>
    class XCreator : Creator
    {
        public override TextBlock CreateMethod(string text)
        {
            TextBlock block = new XTextBlock();
            //список допустимых строк
            List<string> list_starts= new List<string>()
            {
                "p ",//0
                "h1 ",//1
                "h2 ",//2
                "h3 ",//3
                "ordlist",//4
                "bullist"//5
            };
            //отсортируем лишнее
            string[] old_lines = text.Split('\n');
            text = "";
            bool thisTrueText = false;
            string labelTrueText = "";
            int i = 0;
            while (!old_lines[i].EndsWith("End!"))
            {
                //проверка на список
                if (old_lines[i] != "")
                {
                    foreach (string element in list_starts)
                        if (old_lines[i].StartsWith(element))
                        {
                            thisTrueText = true;
                            labelTrueText = element;
                        }
                }
                else
                {
                    thisTrueText = false;
                }

                //действие в зависимости от того 
                if (thisTrueText)
                {
                    if (old_lines[i].StartsWith(list_starts[4]))
                    {
                        text += list_starts[4] + '\n';
                    }
                    else if (old_lines[i].StartsWith(list_starts[5]))
                    {
                        text += list_starts[5] + '\n';
                    }
                    else
                    {
                        text += old_lines[i] + '\n';
                    }
                }
                else
                {
                    if(!text.EndsWith("\n\n") && text != "")
                        text += '\n';
                }
                i++;
            }
            //присвоение
            block.InputContent(text);
            return block;
        }
    }
    /// <summary>
    /// ConcreteCreator - Конкретная фабрика:
    /// Реализует интерфейс(фабричные методы) предоставляемый базовым классом Creator.
    /// </summary>
    class HtmlCreator : Creator
    {
        public override TextBlock CreateMethod(string text)
        {
            TextBlock block = new HtmlTextBlock();
            //список допустимых строк
            List<string> list_starts = new List<string>()
            {
                "p ",//0
                "h1 ",//1
                "h2 ",//2
                "h3 ",//3
                "ordlist",//4
                "bullist"//5
            };
            List<string> list_tegs = new List<string>()
            {
                "\n< p >|</ p >",//0
                "\n< h1 >|</ h1 >",//1
                "\n< h2 >|</ h2 >",//2
                "\n< h3 >|</ h3 >",//3
                "\n< ol >|\n</ ol >",//4
                "\n< ul >|\n</ ul >"//5
            };

            string new_text = @"< !DOCTYPE html >
 < html >
  < head >
   < meta charset = 'utf-8' />
    < title > HTML5 </ title >
    < !--[if IE]>
    < script src = 'http://html5shiv.googlecode.com/svn/trunk/html5.js' ></ script >
    < ![endif]-- >
    < style >
     article, aside, details, figcaption, figure, footer,header,
     hgroup, menu, nav, section { display: block; }
    </ style >
 </ head >
 < body >
";
            //обработка
            text += "\nEnd!";
            string[] old_lines = text.Split('\n');
            text = "";
            string[] currentTeg = new string[2] { "", ""}; 
            int i = 0;
            int current_number = 0;
            while (!old_lines[i].EndsWith("End!"))
            {
                //проверка на список
                if (old_lines[i] != "")
                {
                    bool check = false;
                    foreach (string element in list_starts)
                        if (old_lines[i].StartsWith(element))
                        {
                            check = true;
                            current_number = list_starts.IndexOf(element);
                            currentTeg = list_tegs[current_number].Split('|');
                            if (current_number <= 3)
                                new_text += old_lines[i].Replace(element, currentTeg[0] + "\n") + "\n";
                            else
                                new_text += currentTeg[0];
                            //Console.WriteLine("{=="+ element+"==}");
                        }
                    if (!check && current_number >= 4)
                        new_text += "\n< li >" + old_lines[i] + "</ li >";
                    else if (!check)
                    {
                        new_text += old_lines[i]+"\n";
                    }
                    check = false;
                }
                else
                {
                    new_text += currentTeg[1] + "\n";
                    currentTeg = new string[2] { "", ""}; 
                }
                

                i++;
            }
            //закрываем документ
            new_text += @"
 </ body >
</ html >";
            new_text += '\n';


               //присвоение
            block.InputContent(new_text);
            return block;
        }
    }
    /// <summary>
    /// ConcreteCreator - Конкретная фабрика:
    /// Реализует интерфейс(фабричные методы) предоставляемый базовым классом Creator.
    /// </summary>
    class MarkdownCreator : Creator
    {
        public override TextBlock CreateMethod(string text)
        {
            TextBlock block = new MarkdownTextBlock();
            //список допустимых строк
            List<string> list_starts = new List<string>()
            {
                "p ",//0
                "h1 ",//1
                "h2 ",//2
                "h3 ",//3
                "ordlist",//4
                "bullist"//5
            };
            List<string> list_tegs = new List<string>()
            {
                "\n|",//0
                "\n#|",//1
                "\n##|",//2
                "\n###|",//3
                "|",//4
                "|"//5
            };

            string new_text = "";
            //обработка
            text += "\nEnd!";
            string[] old_lines = text.Split('\n');
            text = "";
            string[] currentTeg = new string[2] { "", "" };
            int i = 0;
            int num = 1;
            int current_number = 0;
            while (!old_lines[i].EndsWith("End!"))
            {
                //проверка на список
                if (old_lines[i] != "")
                {
                    bool check = false;
                    foreach (string element in list_starts)
                        if (old_lines[i].StartsWith(element))
                        {
                            check = true;
                            current_number = list_starts.IndexOf(element);
                            currentTeg = list_tegs[current_number].Split('|');
                            if (current_number <= 3)
                                new_text += old_lines[i].Replace(element, currentTeg[0] + "") + "";
                            else
                                new_text += currentTeg[0];
                        }
                    if (!check && current_number >= 4)
                    {
                        if(current_number == 5)
                            new_text += "\n* " + old_lines[i];
                        else
                        {
                            new_text += "\n" + num + " " + old_lines[i];
                            num++;
                        }
                            
                    }
                    else if (!check)
                    {
                        new_text += old_lines[i];
                    }
                    check = false;
                }
                else
                {
                    new_text += currentTeg[1] + "\n";
                    num = 1;
                    currentTeg = new string[2] { "", "" };
                }


                i++;
            }
            //закрываем документ
            new_text += '\n';


            //присвоение
            block.InputContent(new_text);
            return block;
        }
    }
}
