using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Repository<Product> productRepository = new Repository<Product>();

        productRepository.Add(new Product(1, "Laptop", 1200));
        productRepository.Add(new Product(2, "Mouse", 50));
        productRepository.Add(new Product(3, "Phone", 900));
        productRepository.Add(new Product(4, "Monitor", 1500));

        Console.WriteLine("Product by id:");
        Console.WriteLine(productRepository.GetById(1));

        Console.WriteLine("Products more expensive than 1000:");
        IReadOnlyList<Product> expensiveProducts = productRepository.Find(product => product.Price > 1000);

        foreach (Product product in expensiveProducts)
        {
            Console.WriteLine(product);
        }

        try
        {
            productRepository.Add(new Product(1, "Duplicate Laptop", 2000));
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }

        Console.WriteLine("Remove product with id 2:");
        Console.WriteLine(productRepository.Remove(2));

        Console.WriteLine("Products count:");
        Console.WriteLine(productRepository.Count);

        Repository<User> userRepository = new Repository<User>();

        userRepository.Add(new User(1, "Ivan"));
        userRepository.Add(new User(2, "Anna"));

        Console.WriteLine("Users:");
        foreach (User user in userRepository.GetAll())
        {
            Console.WriteLine(user);
        }

        List<int> numbers = new List<int> { 1, 2, 2, 3, 1, 4 };
        List<int> uniqueNumbers = CollectionUtils.Distinct(numbers);

        Console.WriteLine("Distinct numbers:");
        foreach (int number in uniqueNumbers)
        {
            Console.WriteLine(number);
        }

        List<string> strings = new List<string> { "cat", "dog", "cat", "bird", "dog" };
        List<string> uniqueStrings = CollectionUtils.Distinct(strings);

        Console.WriteLine("Distinct strings:");
        foreach (string text in uniqueStrings)
        {
            Console.WriteLine(text);
        }

        List<string> words = new List<string> { "one", "two", "three", "four", "five", "six" };
        Dictionary<int, List<string>> groupedWords = CollectionUtils.GroupBy(words, word => word.Length);

        Console.WriteLine("Grouped words:");
        foreach (KeyValuePair<int, List<string>> group in groupedWords)
        {
            Console.Write(group.Key + ": ");

            foreach (string word in group.Value)
            {
                Console.Write(word + " ");
            }

            Console.WriteLine();
        }

        Dictionary<string, int> firstCounters = new Dictionary<string, int>
        {
            { "apple", 2 },
            { "banana", 1 }
        };

        Dictionary<string, int> secondCounters = new Dictionary<string, int>
        {
            { "apple", 3 },
            { "orange", 5 }
        };

        Dictionary<string, int> mergedCounters = CollectionUtils.Merge(
            firstCounters,
            secondCounters,
            (first, second) => first + second);

        Console.WriteLine("Merged counters:");
        foreach (KeyValuePair<string, int> pair in mergedCounters)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        List<Product> products = new List<Product>
        {
            new Product(1, "Laptop", 1200),
            new Product(2, "Mouse", 50),
            new Product(3, "Monitor", 1500)
        };

        Product mostExpensiveProduct = CollectionUtils.MaxBy(products, product => product.Price);

        Console.WriteLine("Most expensive product:");
        Console.WriteLine(mostExpensiveProduct);
    }
}
