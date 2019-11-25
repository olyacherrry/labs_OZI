using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicSquares
{
    class Program
    {

        //static public int[] mass =
        //    {
        //     6, 32,  3, 34, 35,  1,
        //     7, 11, 27, 28,  8, 30,
        //    19, 14, 16, 15, 23, 24,
        //    18, 20, 22, 21, 17, 13,
        //    25, 29, 10,  9, 26, 12,
        //    36,  2, 33,  4,  5, 31
        //};

        static void Main(string[] args)
        {
            string text;

            Console.WriteLine("Введите текст: ");
            text = Console.ReadLine();

            MagicSquare magicSquare = new MagicSquare();

            string w = magicSquare.Decryption(text);
            string e = magicSquare.Encryption(w);


            Console.WriteLine("Зашифрованное сообщение: " + w);
            Console.WriteLine("Расшифрованное сообщение: " + e);
        } 
    }
}
