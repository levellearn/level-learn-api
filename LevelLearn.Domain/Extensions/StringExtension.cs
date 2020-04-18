using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LevelLearn.Domain.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Generates a slug for passed string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>clean slug string (ex. TeSte Teste => testeteste)</returns>
        public static string GenerateSlug(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            string result = text.RemoveAccent().ToLower(); // remove invalid chars and lowercase          
            result = Regex.Replace(result, @"[^a-z0-9\s]", string.Empty); // remove special chars  
            result = Regex.Replace(result, @"\s+", string.Empty).Trim();  // convert multiple spaces into empty 
            //result = Regex.Replace(result, @"\s", "-");  
            
            return result;
        }

        public static string RemoveAccent(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var sbResult = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray(); // ex.: água -> a´gua
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbResult.Append(letter);
            }

            return sbResult.ToString();
        }

        public static string GetNumbers(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            text = text.Trim();
            string numbers = new String(text.Where(Char.IsDigit).ToArray());

            return numbers;
        }

        public static string GetLetters(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            text = text.Trim();
            string letters = new String(text.Where(Char.IsLetter).ToArray());

            return letters;
        }

        /// <summary>
        /// Convert multiple spaces into one space
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveExtraSpaces(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            text = Regex.Replace(text, @"\s+", " ").Trim();  // convert multiple spaces into one space 

            return text;
        }

        public static string EncodeTextToBase64(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            string textEncoded = WebEncoders.Base64UrlEncode(textBytes);

            return textEncoded;
        }

        public static string DecodeBase64ToText(this string textEncoded)
        {
            if (string.IsNullOrEmpty(textEncoded)) return string.Empty;

            byte[] textDecodedBytes = WebEncoders.Base64UrlDecode(textEncoded);
            string decodedText = Encoding.UTF8.GetString(textDecodedBytes);

            return decodedText;
        }

    }
}
