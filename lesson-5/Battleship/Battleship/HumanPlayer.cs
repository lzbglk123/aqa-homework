namespace Battleship;

class HumanPlayer : IPlayer
{
    public string Name { get; set; }

    public Position LastShot { get; private set; }


    public ShootResult Shoot(Board targetBoard)
    {
        if (!TryReadFromConsole("X", 0, out var xPosition))
            return ShootResult.InvalidShot;

        Console.WriteLine();

        if (!TryReadFromConsole("Y", 0, out var yPosition))
            return ShootResult.InvalidShot;


        if (xPosition == null || yPosition == null)
            return ShootResult.InvalidShot;


        LastShot = new Position(
            xPosition.Value,
            yPosition.Value);


        var shotPosition = LastShot;


        if (!targetBoard.IsInside(shotPosition))
        {
            Console.WriteLine("Invalid shot position!");
            return ShootResult.InvalidShot;
        }


        return targetBoard.HasShip(shotPosition)
            ? ShootResult.Hit
            : ShootResult.Miss;
    }


    private bool TryReadFromConsole(string coordinateName, int roundCount, out int? coordinate)
    {
        Console.WriteLine($"Input your {coordinateName} coordinate for round {roundCount}:");

        var input = Console.ReadLine();

        coordinate = null;


        try
        {
            if (!int.TryParse(input, out var result))
            {
                Console.WriteLine("Invalid input!");
                return false;
            }

            coordinate = result;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Invalid input!");
        }


        return true;
    }


    public void WriteName()
    {
        Console.WriteLine($"Player: {Name}");
    }
}