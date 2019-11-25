using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicSquares
{
    class MagicSquare
    {
        public MagicSquare()
        {
            mass = stringToArrayInt(System.IO.Directory.GetCurrentDirectory() + @"\text.txt");
            size = mass.Length;
        }

        private static int[] mass;
        private static int size;

        private static int[] stringToArrayInt(string text)
        {
            List<int> arr = new List<int>();
            int n;
            string[] mass = File.ReadAllLines(text);
            n = Convert.ToInt32(mass[0]);

            for (int p = 0; p < n; p++)
            {
                int[] m = mass[p + 1].Split(new char[] { ' ' },
              StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
                for (int i = 0; i < m.Length; i++)
                {
                    arr.Add(m[i]);
                }
            }
            return arr.ToArray();
        }

        public string Encryption(string EncryptedText)
        {
            string result = "";
            List<string> text = new List<string>();
            text = BreakingUp(EncryptedText);
            foreach (string Text in text)
            {
                char[] newMass = new char[size];
                char[] charArr = Text.ToCharArray();
                if (Text.Length <= mass.Length)
                    for (int i = 0; i < mass.Length; i++)
                        for (int j = 0; j < Text.Length; j++)
                            if (j + 1 == mass[i])
                                newMass[i] = charArr[j];
                result += new string(newMass);
            }
            return result;
        }

        public string Decryption(string HiddenText)
        {
            string result = "";
            List<string> text = new List<string>();
            text = BreakingUp(HiddenText);
            foreach (string Text in text)
            {
                char[] newMass = new char[size];
                char[] charArr = Text.ToCharArray();
                for (int i = 0; i < size + 1; i++)
                    for (int j = 0; j < mass.Length; j++)
                        if (i + 1 == mass[j])
                            newMass[i] = charArr[j];
                string str = new string(newMass).Trim();
                result += str;
            }
            return result;
        }

        private static List<string> BreakingUp(string Text)
        {
            List<string> newMass = new List<string>();
            int ostatoc = 1;
            if (Text.Length > mass.Length)
            {
                if (Text.Length % mass.Length == 0)
                    ostatoc = Text.Length / mass.Length;
                else
                    ostatoc = Text.Length / mass.Length + 1;
            }

            char[] newMass1 = new char[size];
            char[] charArr = Text.ToCharArray();
            int end = Text.Length;
            for (int i = 0; i < ostatoc; i++)
            {
                if (end > size)
                {
                    newMass.Add(Text.Substring(0, size));
                    Text = Text.Remove(0, size);
                    end = end - size;
                }
                else
                {
                    newMass.Add(Text.Substring(0, end));
                }
            }
            return newMass;
        }
    }
}
