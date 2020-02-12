using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxConsecutiveCharacterCount
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareAlphanumericStrings();
            //FindMaximumFromAString();
        }

        /// <summary>
        /// Compare two strings considering only alphanumeric characters.
        /// E.g.: 
        /// Equal:
        ///   a.Girish-Chawrai = GirishChawrai
        ///   b.Girish@@#.Chawrai = GirishChawrai
        ///   
        ///   a. 123#@PQR = 123PQR
        ///   b. 123.....#PQR = 123PQR
        ///   
        /// Not equal:
        ///   a. 123aab
        ///   b. aab123
        /// </summary>

        private static void CompareAlphanumericStrings()
        {
            string str1 = "Girish-Chawrai";
            var str2 = "Girish@@#.Chawrai";

            bool result = CompareStringsAlphanumeric(str1, str2, out bool isNull);

            if (!isNull)
            {
                var resultString = result ? "equal" : "not equal";
                Console.WriteLine($"The strings {str1} and {str2} are {resultString}.");
            }

            Console.ReadLine();
        }

        private static bool CompareStringsAlphanumeric(string str1, string str2, out bool isNull)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                Console.WriteLine("Strings cannot be null or empty.");
                
                isNull = true;
                return false;
            }
            var charset1 = str1.ToCharArray().ToList();
            var charset2 = str2.ToCharArray().ToList();

            //remove all non-alphanumeric characters
            var alphanumericCharset1 = charset1.FindAll(c => (c >= 'a' && c <= 'z')
                                                          || (c >= 'A' && c <= 'Z')
                                                          || (c >= '0' && c <= '9'));

            var alphanumericCharset2 = charset2.FindAll(c => (c >= 'a' && c <= 'z')
                                                          || (c >= 'A' && c <= 'Z')
                                                          || (c >= '0' && c <= '9'));

            //form the string back
            var string1 = string.Join(string.Empty, alphanumericCharset1);
            var string2 = string.Join(string.Empty, alphanumericCharset2);

            //compare
            isNull = false;
            return string1 == string2;
        }

        /// <summary>
        /// 1. String operation:
        ///     a.Maximum consecutive repeating characters in a given string.
	    ///     b.Do not need to consider the overall count.
        ///     E.g.: 
		///        i.Giiirrrrriiiish
        ///             r - 5
        /// </summary>
        private static void FindMaximumFromAString()
        {
            var inputString = Console.ReadLine();

            if (string.IsNullOrEmpty(inputString))
            {
                Console.WriteLine("String is either null or empty.");
                Console.ReadLine();
                return;
            }

            var chars = inputString.ToCharArray();

            if (chars.Length == 1)
            {
                Console.WriteLine($"{chars[0]} - 1");

                Console.ReadLine();
                return;
            }

            var outputs = new List<KeyValue>();
            var counter = 1;
            for (int charIndex = 0; charIndex < chars.Length; charIndex++)
            {
                var currentCharacter = chars[charIndex];
                char? nextCharacter = null;
                if (charIndex == chars.Length - 1)
                {

                    nextCharacter = currentCharacter;
                }
                else
                {
                    nextCharacter = chars[charIndex + 1];
                }

                if (currentCharacter == nextCharacter)
                {
                    counter++;

                    if (charIndex == chars.Length - 2)
                    {
                        outputs.Add(new KeyValue
                        {
                            Character = currentCharacter,
                            Count = counter
                        });

                        counter = 1;
                    }
                }
                else
                {
                    outputs.Add(new KeyValue
                    {
                        Character = currentCharacter,
                        Count = counter
                    });

                    counter = 1;
                }
            }
            outputs = outputs.OrderByDescending(o => o.Count).ToList();

            if (outputs.Count > 1 && outputs[0].Count == outputs[1].Count)
            {
                Console.WriteLine("All distinct characters. No maximum found.");
                Console.ReadLine();
                return;
            }

            var highestOutput = outputs.First();
            Console.WriteLine($"{highestOutput.Character} - {highestOutput.Count}");

            Console.ReadLine();
        }
    }
}
