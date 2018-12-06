using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        //данная функция выводит строку состоящую из указанного символа
        static void lineCh(char ch)
        {
            for (int i = 0; i < 80; i++)
                Console.Write(ch);
            Console.WriteLine();
        }
        //данная функция выводит подпись автора 
        static void authorLine()
        {
            lineCh('=');
            Console.WriteLine(" Author: Yarcev Dmitry");
            Console.WriteLine(" group: IS-61");
            Console.WriteLine(" number laboratory work: 1");
            lineCh('=');
        }
        //основная функция программы
        static void Main(string[] args)
        {
            //подпись автора 
            authorLine();
            //основной цыкл программы
            bool loop = true;
            while (loop)
            {
                Console.WriteLine(" Please enter some text in a specific format:");
                lineCh('*');
                //вводим набор строк
                string input = "";
                bool loopInput = true;
                while (loopInput)
                {
                    //добавляем новую строку
                    input += Console.ReadLine().ToString() + "\n";
                    //продолжаем до тех пор пока не появится строка с таким окончанием "h1 end!.
                    if (input.EndsWith("End!\n"))
                        loopInput = false;
                }
                lineCh('*');
                Console.WriteLine(" Сonverting received text into a specific format...");
                //создаем текст странного формата
                lineCh('-');
                Creator creator = new XCreator();
                TextBlock block = creator.CreateMethod(input);
                block.ShowContent();
                input = block.GetContent();
                lineCh('-');
                //создаем текст html формата
                Console.WriteLine(" Сonverting received text into a HTML format...");
                lineCh('-');
                creator = new HtmlCreator();
                block = creator.CreateMethod(input);
                block.ShowContent();
                lineCh('-');
                //создаем текст markdown формата
                Console.WriteLine(" Сonverting received text into a Markdown format...");
                lineCh('-');
                creator = new MarkdownCreator();
                block = creator.CreateMethod(input);
                block.ShowContent();
                lineCh('-');
                //проверяем, желает ли пользователь завершить программу
                bool loopAsk = true;
                while(loopAsk)
                {
                    Console.Write(" Are you want repeat this program? (y/n): ");
                    char[] boolKey = Console.ReadLine().ToCharArray();
                    if (boolKey.Length == 0)
                        boolKey = new char[] { 'n'};
                    if (boolKey[0] == 'y')
                    {
                        loopAsk = false;
                    }
                    else if (boolKey[0] == 'n')
                    {
                        Console.WriteLine(" Now program is ending.");
                        loop = false;
                        loopAsk = false;
                    }
                    else
                    {
                        Console.WriteLine(" You don't use correct symbol.");
                    }
                }
                lineCh('=');
                Console.WriteLine();
            }
            //пауза перед окончанием программы
            Console.ReadLine();
        }
    }
}
