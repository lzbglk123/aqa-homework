namespace TMS_1;

public class Move
{
    public int Number { get; }

    public string Name => Number switch
    {
        1 => "Rock",
        2 => "Paper",
        3 => "Scissors",
        4 => "Well",
        _ => ""
    };

    public Move(int number)
    {
        Number = number;
    }

    public static Move ReadFromConsole()
    {
        Console.WriteLine("1 - Rock");
        Console.WriteLine("2 - Paper");
        Console.WriteLine("3 - Scissors");
        Console.WriteLine("4 - Well");

        int number;

        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Invalid input");
        }

        return new Move(number);
    }

    public static Move GenerateRandom()
    {
        Random random = new Random();

        return new Move(random.Next(1, 5));
    }

    public static bool IsValid(int number, int min = 1, int max = 4)
    {
        return number >= min && number <= max;
    }
}