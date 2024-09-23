using System;
//System.Globalization used to use both . and , between dollars and cents
using System.Globalization;
using System.Text;

namespace TestTask2GrandMAStudio
{
    class Program
    {

        //Arrays for words (first is empty for zero)
        static private string[] wordList = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        static private string[] tensWordList = new string[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Infinite menu (To exit user shuld print -1)
            while (true)
            {
                Console.WriteLine("Введіть число для переводу в пропис (-1 для виходу):");
                string input = Console.ReadLine();
                decimal number;
                //Try parse to number, condition used to apply both , and . between dollars and cents without any errors
                if(input.Contains(",")? decimal.TryParse(input, out number) : decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out number))
                {
                    //Print -1 to exit
                    if (number == -1)
                    {
                        return;
                    }
                    //Define boundaries
                    else if (number >= -1000000000 && number <= 1000000000)
                    {
                        //Invoke function to convert and write result to console
                        Console.WriteLine(ConvertToWords(number));
                    }
                    //Out of boundaries message
                    else
                    {
                        Console.WriteLine("Значення поза допустимими межами, введіть інше");
                        continue;
                    }
                }
                //Error of parsing message (invoke when input isnt a number)
                else
                {
                    Console.WriteLine("Невірно введене значення, спробуйте ще раз");
                    continue;
                }
            }
        }

        //Function that operates convert process
        private static string ConvertToWords(decimal number)
        {
            string numberResult;
            //Split dollars and cents parts
            int dollars = (int)number;
            int cents = (int)Math.Abs(Math.Round((number - dollars), 2) * 100);

            //Combine result (if dollar count is one, use "dollar", for others "dollars", same for cents. If cents count is zero, dont print it)
            numberResult = (dollars == 0? "" : NumberToWords(dollars) 
                            + (dollars == 1 ? " dollar " : " dollars "))
                            
                         + (cents == 0 ? "" : NumberToWords(cents)
                            + (cents == 1 ? " cent" : " cents"));
            return numberResult;
        }

        //Recursive function to сonvert Number
        /*
         *How it work:
         *123456789
         *Remove 123 millions and convert it to one hundred twenty-three millions,(Invoke itself and convert it like just 123) after that
         *remove 456 thousend and convert it to four hundreds fifty-six thousands, after that
         *convert 789 to seven hundred eighty-nine
         *and return combinations of this
         */
        private static string NumberToWords(int number)
        {
            //main variable to save result
            string words = "";

            //Add "-", if number less than zero
            if (number < 0)
            {
                return "minus " + NumberToWords(Math.Abs(number));
            }

            //Count and remove billions
            if ((number / 1000000000) > 0)
            {
                int temp = number / 1000000000;
                words += NumberToWords(temp) + (temp == 1 ? " billion " : " billions ");
                number %= 1000000000;
            }

            //Count and remove millions
            if ((number / 1000000) > 0)
            {
                int temp = number / 1000000;
                words += NumberToWords(temp) + (temp == 1 ? " million " : " millions ");
                number %= 1000000;
            }

            //Count and remove thousands
            if ((number / 1000) > 0)
            {
                int temp = number / 1000;
                words += NumberToWords(temp) + (temp == 1 ? " thousand " : " thousands ");
                number %= 1000;
            }

            //Count and remove hundreds
            if ((number / 100) > 0)
            {
                int temp = number / 100;
                words += NumberToWords(number / 100) + (temp == 1 ? " hundred " : " hundreds ");
                number %= 100;
            }

            //Count tens
            if (number > 0)
            {
                //use words for 1 to 19
                if (number < 20)
                    words += wordList[number];
                //else use words for tens
                else
                {
                    words += tensWordList[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + wordList[number % 10];
                }
            }

            return words;
        }
    }
}
