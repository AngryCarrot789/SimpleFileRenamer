using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileRenamer.Utilities
{
    public static class StringHelper
    {
        /// <summary>
        /// Replaces the last occourance of a word with another word
        /// </summary>
        /// <param name="original">The original text (HelloThereHello)</param>
        /// <param name="oldText"></param>
        /// <param name="newText"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string original, string oldText, string newText)
        {
            int place = original.LastIndexOf(oldText);

            if (place == -1)
                return original;

            string result = original.Remove(place, oldText.Length).Insert(place, newText);
            return result;
        }
    }
}
