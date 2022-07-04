// See https://aka.ms/new-console-template for more information

namespace Calculator
{
    static class Program
    {
        static void Main(string[] args)
        {
            CalculatorFunctions calc = new CalculatorFunctions();
            calc.RadiusToAreaUI();
            calc.CalculationUI();
        }
    }

    class CalculatorFunctions
    {
        /// <summary>
        /// Takes a single double as the radius of a circle, and returns the area of the circle
        /// </summary>
        /// <param name="radius">Double, The radius of the circle</param>
        /// <returns>Double, the area of the circle</returns>
        private double AreaCalc(double radius)
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
        private double Calculation(double numOne, double numTwo, char op)
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
        
/// <summary>
/// Asks the user for two numbers, and an operator, performs the operation and returns the result to the console.
/// </summary>
        public void CalculationUI()
        {
            Console.WriteLine("Please enter the first number.");
            double first = getValidDouble();
            Console.WriteLine("Please enter the second number");
            double second = getValidDouble();
            Console.WriteLine("Please enter the operation you would like to perform");
            char op = getValidOperator();

            Console.WriteLine("The answer is: " + Calculation(first, second, op));

        }

/// <summary>
/// Asks the user to enter an operator, and repeats until they enter a valid input
/// </summary>
/// <returns>char: either +, -, /, *</returns>
        private char getValidOperator()
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
/// Asks the user to enter a number for the radius of a circle and returns the area of that circle.
/// </summary>
        public void RadiusToAreaUI()
        {
            Console.WriteLine("Please enter the radius of the circle.");
            double radius = getValidDouble();
            Console.WriteLine("The area of the circle is: " + AreaCalc(radius));
        }

/// <summary>
/// Asks the user to enter a number, and repeatedly asks until a valid answer is entered
/// </summary>
/// <returns></returns>
        private double getValidDouble()
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
    }
}
