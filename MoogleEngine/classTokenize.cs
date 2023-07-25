using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System;

namespace MoogleEngine
{
    public class Tokenize
    {
        public static string[] StringToArray(string content)//llevar el contenido de un txt a un array quitando caracteres extranhos
        {
            content = content.ToLower();
            content = Regex.Replace(content.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            content = StrangeCharacterRemover(content);

            string[] wordsInArray;

            char[] charSeparators = new char[] { ' ' };

            wordsInArray = content.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

            return wordsInArray;
        }

        private static string StrangeCharacterRemover(string content)// metodo auxiliar para eliminar caracteres extranhos
        {
            char[] charToRemove = new char[] { '\r', '^', '[', ']', '_', '\n' };
            foreach (char c in charToRemove)
            {
                content = content.Replace(c.ToString(), string.Empty);
            }
            return content;
        }
    }
}