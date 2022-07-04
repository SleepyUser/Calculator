// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Calculator
{
    static class Program
    {
        static void Main(string[] args)
        {
            CalculatorUi calc = new CalculatorUi();
        }
    }

    class CalculatorUi
    {
        private InputChecks _iChecks;
        private CalculatorFunctions _calcFunc;
        public CalculatorUi()
        {
            _iChecks = new InputChecks();
            _calcFunc = new CalculatorFunctions();
            CalculatorMenu();
        }
        
        /// <summary>
        /// Main Menu of the Calculator. Displays the menu and asks for an int to be entered to select a menu option.
        /// </summary>
        /// <exception cref="NotImplementedException">Menu Options that aren't implemented will return this Exception</exception>
        public void CalculatorMenu()
        {
            Console.WriteLine("Welcome to the Calculator!");
            do
            {
                Console.WriteLine("Main Menu\n" +
                                  "====================================================================");
                int input = 0;
                Console.WriteLine("Please choose a menu option:\n" +
                                  "1) Area Calculation\n" +
                                  "2) Simple Calculation\n" +
                                  "3) Cumulative Sum up to Integer\n" +
                                  "4) Reverse Words\n" +
                                  "5) Is this a Square Number?\n" +
                                  "6) Times Tables\n" +
                                  "7) Word Joiner");
                switch (_iChecks.getValidInt())
                {
                    case 1:
                        Console.WriteLine("Area Calculation Selected...");
                        RadiusToAreaUI();
                        break;
                    case 2:
                        Console.WriteLine("Simple Calculation Selected...");
                        CalculationUI();
                        break;
                    case 3:
                        Console.WriteLine("Cumulative Sum Selected...");
                        CumulativeSumUpToInt();
                        break;
                    case 4:
                        Console.WriteLine("Reverse Words Selected...");
                        ReverseWordsUI();
                        break;
                    case 5:
                        Console.WriteLine("Is this a square number? selected...");
                        RootFinderUI();
                        break;
                    case 6:
                        Console.WriteLine("Times Tables Selected...");
                        TimesTablesUI();
                        break;
                    case 7:
                        Console.WriteLine("Word Joiner Selected...");
                        WordJoinerUI();
                        break;
                    default:
                        throw new Exception("Invalid Menu Option Selected");
                }
            } while (ContinueYN());
            Console.WriteLine("Goodbye!");
        }

        private bool ContinueYN()
        {
            Console.WriteLine("Would you like to continue?");
            return _iChecks.getYN();
        }

        /// <summary>
        /// Asks the user for a number, and returns the sum of all numbers up to and including that number.
        /// </summary>
        public void CumulativeSumUpToInt()
        {
            Console.WriteLine("Please enter a valid positive integer:");
            int input = _iChecks.getValidInt(posOnly: true);
            Console.WriteLine("The cumulative sum from 0 to " + input + " is: " + _calcFunc.CumulativeSum(input));
        }

        /// <summary>
        /// Asks for a number and tells you if it is a square number, and what its root is (if it is)
        /// </summary>
        public void RootFinderUI()
        {
            Console.WriteLine("Please enter your potentially square number: ");
            int input = _iChecks.getValidInt();
            int root = _calcFunc.RootFinder(input);
            if (root == -1)
            {
                Console.WriteLine(input + " is not a square number, sorry! :(");
            }
            else
            {
                Console.WriteLine(input + " is a square number and its root is " + root + "!");
            }
        }
        
        /// <summary>
        /// Asks the user for an integer and outputs times tables up to that number.
        /// </summary>
        public void TimesTablesUI()
        {
            Console.WriteLine("Please enter a positive number to do times tables up to: ");
            int input = _iChecks.getValidInt(posOnly: true);
            for (int i = 1; i <= input; i++)
            {
                Console.WriteLine(i + " x " + input + " = " + Convert.ToInt32(_calcFunc.Calculation(i, input, '*')));
            }
        }

        /// <summary>
        /// Asks the user for two numbers, and an operator, performs the operation and returns the result to the console.
        /// </summary>
        public void CalculationUI()
        {
            Console.WriteLine("Please enter the operation you would like to perform");
            char op = _iChecks.getValidOperator();
            Console.WriteLine("How many numbers would you like to "+ op+"?");
            int numCount = _iChecks.getValidInt();
            double total = 0;
            for (int i = 0; i < numCount; i++)
            {
                Console.WriteLine("Enter number " + (i+1) + ":");
                total = _calcFunc.Calculation(total, _iChecks.getValidDouble(), op);
            }
            Console.WriteLine("The answer is: " + total);
        }

        /// <summary>
        /// Asks for an integer, and then that number of strings and returns each string to the user but the chars within are reversed
        /// </summary>
        public void ReverseWordsUI()
        {
            Console.WriteLine("Please enter a number of words:");
            int wordCount = _iChecks.getValidInt();
            string[] wordList = new string[wordCount];

            for (int i = 0; i < wordCount; i++)
            { 
                Console.WriteLine("Please enter a word.");
                wordList[i] = Console.ReadLine() ?? string.Empty;
            }
            Console.WriteLine("Your sentence is: " + string.Join(" ", wordList));
            Console.WriteLine("Your reversed words are:");
            foreach (string s in wordList)
            {
                Console.WriteLine(Strings.StrReverse(s));
            }
        }

        /// <summary>
        /// Asks the user for words, adding each to a sentence which is displayed.
        /// When a blank is entered, all words entered so far are repeated back in a sentence and the function ends.
        /// </summary>
        public void WordJoinerUI()
        {
            string sentence = "";
            string word = "";
            while(true)
            { 
                Console.WriteLine("Please enter a word.");
                word = Console.ReadLine() ?? string.Empty;
                if (word == String.Empty)
                {
                    Console.WriteLine("Final Sentence: " + sentence);
                    break;
                }

                sentence = string.Concat(sentence, " ", word);
                Console.WriteLine("Current Sentence: " + sentence);
            }

        }
        
        /// <summary>
        /// Asks the user to enter a number for the radius of a circle and returns the area of that circle.
        /// </summary>
        public void RadiusToAreaUI()
        {
            Console.WriteLine("Please enter the radius of the circle.");
            double radius = _iChecks.getValidDouble();
            Console.WriteLine("The area of the circle is: " + _calcFunc.AreaCalc(radius));
        }
    }

    class CalculatorFunctions
    {
        private InputChecks iChecks;

        //Constructor
        public CalculatorFunctions()
        {
            iChecks = new InputChecks();
        }

        /// <summary>
        /// Returns the square root of the number, returns -1 if no square root exists.
        /// </summary>
        /// <param name="number">an integer</param>
        /// <returns>-1 if no root found, otherwise returns the root</returns>
        public int RootFinder(int number)
        {
            int total = 0;
            for (int i = 0; i <= number; i++)
            {
                total = i * i;
                if (total == number)
                {
                    return i;
                }
                else if (total > number)
                {
                    return -1;
                }
            }

            return -1;
        }
        /// <summary>
        /// Cumulatively sums each integer from 0 to the parameter (inclusive), returning the total sum.
        /// </summary>
        /// <param name="number">The number to sum up to</param>
        /// <returns>The cumulative sum up to the parameter number</returns>
        public int CumulativeSum(int number)
        {
            int total = 0;
            
            for (int i = 0; i <= number; i++)
            {
                total += i;
            }
            
            return total;
        }
        
        /// <summary>
        /// Takes a single double as the radius of a circle, and returns the area of the circle
        /// </summary>
        /// <param name="radius">Double, The radius of the circle</param>
        /// <returns>Double, the area of the circle</returns>
        public double AreaCalc(double radius)
        {
            return (radius * radius) * Math.PI;
        }

        /// <summary>
        /// Takes two numbers and an operator and performs the operator, returning the result
        /// Ignores division by zero and returns only the first operator
        /// </summary>
        /// <param name="numOne">Double, first number to the left of the operator</param>
        /// <param name="numTwo">Double, second number to the right of the operator</param>
        /// <param name="op">Char, the operator</param>
        /// <returns>The answer to the calculation (first number only if division by zero)</returns>
        public double Calculation(double numOne, double numTwo, char op)
        {
            switch (op)
            {
                case '*':
                    return numOne * numTwo;
                case '-':
                    return numOne - numTwo;
                case '+':
                    return numOne + numTwo;
                case '/':
                    if (numTwo == 0)
                    {
                        Console.WriteLine("Error: Division by zero, ignoring the calculation!");
                        return numOne;
                    }

                    return numOne / numTwo;
                default:
                    throw new Exception("Invalid Operator in calculation");
                    break;

            }
        }
    }

    class InputChecks
        {
            /// <summary>
            /// Asks the user to enter an operator, and repeats until they enter a valid input
            /// </summary>
            /// <returns>char: either +, -, /, *</returns>
            public char getValidOperator()
            {
                char[] validOps = { '+', '-', '/', '*' };
                while (true)
                {
                    Console.WriteLine("Please type an operator (*, /, +, -)");
                    string input = Console.ReadLine() ?? string.Empty;
                    if (validOps != null && validOps.Contains(Convert.ToChar(input)))
                    {
                        return Convert.ToChar(input);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input, please try again.");
                    }
                }
            }
            
            /// <summary>
            /// Asks the user to enter a double, and repeatedly asks until a valid answer is entered
            /// </summary>
            /// <returns>the successfully entered double</returns>
            public double getValidDouble()
            {
                while (true)
                {
                    try
                    {
                        return Convert.ToDouble(Console.ReadLine());

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input, please try again.");
                    }
                }
            }

            /// <summary>
            /// Asks the user to enter Y or N (not case sensitive) and returns the corresponding bool
            /// </summary>
            /// <returns>True if user entered Y, False if N</returns>
            public bool getYN()
            {
                string input = "";
                while (true)
                {
                    Console.WriteLine("Please enter Y or N.");
                    input = Console.ReadLine() ?? String.Empty;
                    if (input == "Y" || input == "y")
                    {
                        return true;
                    }
                    else if (input == "N" || input == "n")
                    {
                        return false;
                    }
                    Console.WriteLine("Invalid input.");
                }
            }

            /// <summary>
            /// Asks the user to enter an int, and repeatedly asks until a valid answer is entered
            /// </summary>
            /// <returns>the successfully entered int</returns>
            public int getValidInt(bool posOnly = false, bool nonZero = false)
            {
                int input = 0;
                while (true)
                {
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input, please try again.");
                        continue;
                    }

                    if (nonZero && input == 0)
                    {
                        Console.WriteLine("Zero is not an acceptable input here.");
                        continue;
                    }
                    else if (posOnly && input < 0)
                    {
                        Console.WriteLine("Negative numbers are not accepted here.");
                        continue;
                    }
                    else
                    {
                        return input;
                    }
                }
            }
        }
}
