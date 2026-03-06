using System;
using System.Globalization;

public static class CalculatorEngine
{
    public static double Sum(double a, double b) => a + b;

    public static double Sub(double a, double b) => a - b;

    public static double Mul(double a, double b) => a * b;

    public static double Div(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }

        return a / b;
    }

    public static double Calculate(double left, double right, char op)
    {
        return op switch
        {
            '+' => Sum(left, right),
            '-' => Sub(left, right),
            '*' => Mul(left, right),
            '/' => Div(left, right),
            _ => throw new InvalidOperationException("Operator is not supported.")
        };
    }
}

public sealed class ExpressionParser
{
    public bool TryParse(string input, out CalculationRequest request, out string error)
    {
        request = default;
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            error = "Input is empty. Use format: <number> <operator> <number>.";
            return false;
        }

        string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3)
        {
            error = "Invalid format. Example: 12.5 * 3";
            return false;
        }

        if (!double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double left))
        {
            error = "First number is invalid.";
            return false;
        }

        if (!double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double right))
        {
            error = "Second number is invalid.";
            return false;
        }

        char op = parts[1][0];
        if (parts[1].Length != 1 || !IsSupportedOperator(op))
        {
            error = "Unsupported operator. Allowed: +, -, *, /.";
            return false;
        }

        request = new CalculationRequest(left, right, op);
        return true;
    }

    private static bool IsSupportedOperator(char op)
    {
        return op == '+' || op == '-' || op == '*' || op == '/';
    }
}

public readonly struct CalculationRequest
{
    public CalculationRequest(double left, double right, char op)
    {
        Left = left;
        Right = right;
        Operator = op;
    }

    public double Left { get; }
    public double Right { get; }
    public char Operator { get; }
}

internal static class Program
{
    private const string StopSymbol = "q";

    private static void Main()
    {
        var parser = new ExpressionParser();

        Console.WriteLine("Simple Calculator");
        Console.WriteLine("Format: <number> <operator> <number>");
        Console.WriteLine($"Operators: + - * /");
        Console.WriteLine($"Type '{StopSymbol}' to quit.");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (input is null)
            {
                break;
            }

            if (string.Equals(input.Trim(), StopSymbol, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Bye.");
                break;
            }

            if (!parser.TryParse(input, out CalculationRequest request, out string error))
            {
                Console.WriteLine($"Parse error: {error}");
                continue;
            }

            try
            {
                double result = CalculatorEngine.Calculate(request.Left, request.Right, request.Operator);

                Console.WriteLine($"Result: {result.ToString(CultureInfo.InvariantCulture)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Calculation error: {ex.Message}");
            }
        }
    }
}
