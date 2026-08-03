using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}


interface IDiscountable
{
    void ApplyDiscount(decimal percent);
}


class Book : Product, IDiscountable
{
    public Book()
    {
    }

    public void ApplyDiscount(decimal percent)
    {
        Price = Price - Price * percent / 100;
    }
}


class Phone : Product, IDiscountable
{
    public Phone()
    {
    }

    public void ApplyDiscount(decimal percent)
    {
        Price = Price - Price * percent / 100;
    }
}



class Program
{

    static void PrintValue<T>(T value)
    {
        Console.WriteLine(value);
    }


    static void PrintList<T>(List<T> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine(items[i]);
        }
    }


    static T GetFirst<T>(List<T> items)
    {
        if (items.Count == 0)
        {
            throw new Exception("Список пуст");
        }

        return items[0];
    }


    static T GetLast<T>(List<T> items)
    {
        if (items.Count == 0)
        {
            throw new Exception("Список пуст");
        }

        return items[items.Count - 1];
    }


    static T GetByIndex<T>(List<T> items, int index)
    {
        return items[index];
    }


    static List<T> Repeat<T>(T value, int count)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < count; i++)
        {
            result.Add(value);
        }

        return result;
    }


    static List<T> Copy<T>(List<T> items)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < items.Count; i++)
        {
            result.Add(items[i]);
        }

        return result;
    }


    static List<T> Merge<T>(
        List<T> first,
        List<T> second)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < first.Count; i++)
        {
            result.Add(first[i]);
        }

        for (int i = 0; i < second.Count; i++)
        {
            result.Add(second[i]);
        }

        return result;
    }


    static List<T> Reverse<T>(List<T> items)
    {
        List<T> result = new List<T>();

        for (int i = items.Count - 1; i >= 0; i--)
        {
            result.Add(items[i]);
        }

        return result;
    }


    static List<T> Take<T>(
        List<T> items,
        int count)
    {
        List<T> result = new List<T>();

        if (count > items.Count)
        {
            count = items.Count;
        }

        for (int i = 0; i < count; i++)
        {
            result.Add(items[i]);
        }

        return result;
    }



    static void PrintProducts<T>(
        List<T> products)
        where T : Product
    {
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine(
                products[i].Name +
                " - " +
                products[i].Price);
        }
    }



    static T GetMostExpensive<T>(
        List<T> products)
        where T : Product
    {
        if (products.Count == 0)
        {
            throw new Exception("Нет товаров");
        }


        T max = products[0];

        for (int i = 1; i < products.Count; i++)
        {
            if (products[i].Price > max.Price)
            {
                max = products[i];
            }
        }

        return max;
    }



    static List<T> GetProductsCheaperThan<T>(
        List<T> products,
        decimal maximumPrice)
        where T : Product
    {
        List<T> result = new List<T>();

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Price < maximumPrice)
            {
                result.Add(products[i]);
            }
        }

        return result;
    }



    static T CreateProduct<T>(
        string name,
        decimal price)
        where T : Product, new()
    {
        T product = new T();

        product.Name = name;
        product.Price = price;

        return product;
    }



    static void ApplyDiscountToAll<T>(
        List<T> products,
        decimal percent)
        where T : Product, IDiscountable
    {
        for (int i = 0; i < products.Count; i++)
        {
            products[i].ApplyDiscount(percent);

            Console.WriteLine(
                products[i].Name +
                " - " +
                products[i].Price);
        }
    }



    static List<int> GetEvenNumbers(
        List<int> numbers)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] % 2 == 0)
            {
                result.Add(numbers[i]);
            }
        }

        return result;
    }



    static List<int> GetNumbersGreaterThan(
        List<int> numbers,
        int minimum)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] > minimum)
            {
                result.Add(numbers[i]);
            }
        }

        return result;
    }



    static List<string> GetLongWords(
        List<string> words,
        int minimumLength)
    {
        List<string> result = new List<string>();

        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].Length >= minimumLength)
            {
                result.Add(words[i]);
            }
        }

        return result;
    }



    static List<T> Filter<T>(
        List<T> items,
        Predicate<T> condition)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < items.Count; i++)
        {
            if (condition(items[i]))
            {
                result.Add(items[i]);
            }
        }

        return result;
    }



    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }


    static bool IsGreaterThanTen(int number)
    {
        return number > 10;
    }


    static bool IsLongWord(string word)
    {
        return word.Length >= 5;
    }
        static void ExecuteTwice(Action action)
    {
        action();
        action();
    }


    static void PrintHello()
    {
        Console.WriteLine("Hello");
    }


    static void PrintSeparator()
    {
        Console.WriteLine("----------------");
    }



    static void ProcessItems<T>(
        List<T> items,
        Action<T> action)
    {
        for (int i = 0; i < items.Count; i++)
        {
            action(items[i]);
        }
    }



    static void PrintNumber(int number)
    {
        Console.WriteLine(number);
    }



    static void PrintUpperCase(string text)
    {
        Console.WriteLine(text.ToUpper());
    }



    static int Calculate(
        int first,
        int second,
        Func<int, int, int> operation)
    {
        return operation(first, second);
    }



    static int Add(int first, int second)
    {
        return first + second;
    }



    static int Subtract(int first, int second)
    {
        return first - second;
    }



    static int Multiply(int first, int second)
    {
        return first * second;
    }




    static void Main(string[] args)
    {
        Console.WriteLine("=== Generic методы ===");


        PrintValue(123);
        PrintValue("Hello");
        PrintValue(true);



        List<int> numbers = new List<int>();

        numbers.Add(2);
        numbers.Add(5);
        numbers.Add(10);
        numbers.Add(15);



        List<string> words = new List<string>();

        words.Add("cat");
        words.Add("apple");
        words.Add("computer");



        PrintList(numbers);
        PrintList(words);



        Console.WriteLine(GetFirst(numbers));
        Console.WriteLine(GetLast(numbers));
        Console.WriteLine(GetByIndex(numbers, 1));



        PrintList(Repeat(7, 5));



        List<int> copiedNumbers = Copy(numbers);

        PrintList(copiedNumbers);



        List<int> secondNumbers = new List<int>();

        secondNumbers.Add(20);
        secondNumbers.Add(30);



        PrintList(Merge(numbers, secondNumbers));

        PrintList(Reverse(numbers));

        PrintList(Take(numbers, 2));




        Console.WriteLine("=== Проверка исключений ===");


        try
        {
            List<int> empty = new List<int>();

            GetFirst(empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



        try
        {
            List<int> empty = new List<int>();

            GetLast(empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }





        Console.WriteLine("=== Product ===");



        List<Book> books = new List<Book>();

        books.Add(new Book
        {
            Name = "C#",
            Price = 1000
        });

        books.Add(new Book
        {
            Name = "Algorithms",
            Price = 1500
        });



        List<Phone> phones = new List<Phone>();

        phones.Add(new Phone
        {
            Name = "iPhone",
            Price = 90000
        });

        phones.Add(new Phone
        {
            Name = "Samsung",
            Price = 70000
        });



        PrintProducts(books);

        PrintProducts(phones);



        Console.WriteLine(
            GetMostExpensive(books).Name);



        Console.WriteLine(
            GetMostExpensive(phones).Name);



        PrintProducts(
            GetProductsCheaperThan(books, 1200));



        Book createdBook =
            CreateProduct<Book>("New Book", 500);


        Phone createdPhone =
            CreateProduct<Phone>("New Phone", 50000);



        Console.WriteLine(createdBook.Name);
        Console.WriteLine(createdPhone.Name);





        Console.WriteLine("=== Проверка пустых товаров ===");


        try
        {
            List<Book> emptyBooks =
                new List<Book>();

            GetMostExpensive(emptyBooks);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }





        Console.WriteLine("=== Discount ===");



        ApplyDiscountToAll(
            books,
            10);



        ApplyDiscountToAll(
            phones,
            15);






        Console.WriteLine("=== Фильтрация без делегатов ===");


        PrintList(
            GetEvenNumbers(numbers));



        PrintList(
            GetNumbersGreaterThan(numbers, 10));



        PrintList(
            GetLongWords(words, 5));






        Console.WriteLine("=== Predicate ===");



        PrintList(
            Filter(numbers, IsEven));



        PrintList(
            Filter(numbers, IsGreaterThanTen));



        PrintList(
            Filter(words, IsLongWord));






        Console.WriteLine("=== Action ===");



        ExecuteTwice(PrintHello);

        ExecuteTwice(PrintSeparator);



        ProcessItems(numbers, PrintNumber);

        ProcessItems(words, PrintUpperCase);






        Console.WriteLine("=== Func ===");



        Console.WriteLine(
            Calculate(10, 5, Add));

        Console.WriteLine(
            Calculate(10, 5, Subtract));

        Console.WriteLine(
            Calculate(10, 5, Multiply));






        Console.WriteLine("=== Проверка Copy ===");


        numbers.Add(100);


        Console.WriteLine("Исходный:");

        PrintList(numbers);



        Console.WriteLine("Копия:");

        PrintList(copiedNumbers);



        Console.WriteLine("Работа программы завершена");
    }
}