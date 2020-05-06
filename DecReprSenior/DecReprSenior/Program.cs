using System;
using System.Collections.Generic;
using System.Linq;

namespace DecReprSenior
{
    class Program
    {
        static void Main(string[] args)
        {
            string stringInput, stringOutput;
            do
            {
                Console.WriteLine("Input N = ");
                stringInput = Console.ReadLine();

                var stringValidation = Validation(stringInput);
                if (!string.IsNullOrEmpty(stringValidation))
                {
                    Console.WriteLine(stringValidation + "\n");
                }
                else
                {
                    stringOutput = Rearring(stringInput);
                    Console.WriteLine("Output = \n{0}\n", stringOutput);
                }

            } while (stringInput != "stop");    
        }

        static string Validation(string stringInput)
        {
            try
            {
                int integer = Convert.ToInt32(stringInput);
                return integer > 1000000000 ? "-1" : null;
            }
            catch (OverflowException o)
            {
                return o.Message + "\nPlease enter a number within the range of [0..2,147,483,647]";
            }
            catch (Exception e)
            {
                return e.Message;
            }          
        }

        static string Rearring(string stringInput)
        {
            var listValues = new List<KeyValuePair<char, int>>();

            foreach (var character in stringInput)
            {
                var item = new KeyValuePair<char, int>(character, Convert.ToInt32(character));

                listValues.Add(item);
            }

            return string.Join("", listValues.OrderByDescending(x => x.Value).Select(e => e.Key));
        }


    }
}
