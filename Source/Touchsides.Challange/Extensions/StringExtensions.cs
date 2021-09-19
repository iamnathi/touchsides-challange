using System.Globalization;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove any punctuation from the word from start to the end string.
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string TrimPunctuation(this string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase)) return phrase;

            // Count start punctuation.
            int removeFromStart = 0;
            for (int i = 0; i < phrase.Length; i++)
            {
                if (char.IsPunctuation(phrase[i]))
                {
                    removeFromStart++;
                }
                else
                {
                    break;
                }
            }

            // Count end punctuation.
            int removeFromEnd = 0;
            for (int i = phrase.Length - 1; i >= 0; i--)
            {
                if (char.IsPunctuation(phrase[i]))
                {
                    removeFromEnd++;
                }
                else
                {
                    break;
                }
            }

            // No characters were punctuation.
            if (removeFromStart == 0 && removeFromEnd == 0)
            {
                return phrase;
            }

            // All characters were punctuation.
            if (removeFromStart == phrase.Length && removeFromEnd == phrase.Length)
            {
                return "";
            }

            // Substring.
            return phrase.Substring(removeFromStart, phrase.Length - removeFromEnd - removeFromStart);
        }

        /// <summary>
        /// Remove accents\diacroctics from word
        /// </summary>
        /// <param name="word">Word fom the book</param>
        /// <returns></returns>
        public static string NormalizeWord(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return word;
            }

            StringBuilder builder = new StringBuilder();
            var wordLetters = word.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in wordLetters)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(letter);
                }
            }

            return builder.ToString();
        }
    }
}