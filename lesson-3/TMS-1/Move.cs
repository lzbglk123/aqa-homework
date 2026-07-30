namespace TMS_1;

public class Move
{
    public int Number { get; }

    public string Name { get; }


    public Move(int number)
    {
        Number = number;
        Name = GetMoveName(number);
    }


    public static Move ReadFromConsole()
    {
        Console.WriteLine("1 - Rock");
        Console.WriteLine("2 - Paper");
        Console.WriteLine("3 - Scissors");
        Console.WriteLine("4 - Well");
        Console.WriteLine("0 - Exit");

        int number;

        while (!int.TryParse(Console.ReadLine(), out number) || 
               (number != 0 && !IsValid(number)))
        {
            Console.WriteLine("Invalid input");
        }

        return new Move(number);
    }


    public static Move GenerateRandom()
    {
        Random random = new Random();

        int number = random.Next(1, 5);

        return new Move(number);
    }


    public static bool IsValid(int number)
    {
        return number >= 1 && number <= 4;
    }


    private static string GetMoveName(int number)
    {
        switch (number)
        {
            case 1:
                return "Rock";

            case 2:
                return "Paper";

            case 3:
                return "Scissors";

            case 4:
                return "Well";

            default:
                return "";
        }
    }
}