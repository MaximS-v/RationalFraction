using System;
using System.Linq;

namespace RationalFraction
{
    // Input rational fraction, 0 < numerator < denominator < 100 000
    // Output decimal fraction as string with period
    internal class Program
    {
        /// <summary>
        /// Add num2add to end of array and return result array
        /// </summary>
        /// <param name="array">source array</param>
        /// <param name="num2add">element to add</param>
        /// <returns>result array</returns>
        static int[] Add2Array(int[] array, int num2add)
        {
            var newArray = new int[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[newArray.Length - 1] = num2add;
            return newArray;
        }

        /// <summary>
        /// Returns position of element in array from end of array or 0
        /// Last element has position 1
        /// </summary>
        /// <param name="array">array for search</param>
        /// <param name="element">element for search</param>
        /// <returns>position element in array from end</returns>
        static int GetPosFromEnd(int[] array, int element)
        {
            if (array.Contains(element))
                return array.Length - Array.IndexOf(array, element);
            return 0;
        }

        /// <summary>
        /// Input decimal fractional ends periodical part
        /// and length of periodical part
        /// </summary>
        /// <param name="input"></param>
        /// <param name="periodLength"></param>
        /// <returns>result string like 0.(3)</returns>
        static string FormPeriodicFract(string input, int periodLength)
        {
            if (periodLength > 0)
                return input.Substring(0, input.Length - periodLength)
                    + "("
                    + input.Substring(input.Length - periodLength, periodLength)
                    + ")";
            else
                return input;
        }

        /// <summary>
        /// Convert rational fractional to decimal fractional
        /// </summary>
        /// <param name="numerator">numerator</param>
        /// <param name="denominator">denominator</param>
        /// <returns>result as string with period if exist</returns>
        static string ConvertFromRat2Dec(int numerator, int denominator)
        {
            string result = "0.";
            var modsStore = new int[0];
            int periodLength = 0;
            while (true)
            {
                numerator *= 10;
                int quiotent = numerator / denominator;
                int mod = numerator % denominator;
                if (mod == 0)
                {
                    result = result + quiotent;
                    break;
                }
                periodLength = GetPosFromEnd(modsStore, mod);
                if (periodLength > 0)
                {
                    if (result[result.Length - periodLength].ToString() != quiotent.ToString())
                        result = result + quiotent;
                    break;
                }
                else
                    result = result + quiotent;
                modsStore = Add2Array(modsStore, mod);
                numerator = mod;
            }
            return FormPeriodicFract(result, periodLength);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input ratioanl fractional as numerator/denominator:");
            string[] input = Console.ReadLine().Split('/');
            int numerator = Int32.Parse(input[0]);
            int denominator = Int32.Parse(input[1]);
            Console.WriteLine("Result: {0}", ConvertFromRat2Dec(numerator, denominator));
        }
    }
}
