namespace Battleship;

class HumanPlayer : IPlayer
{
    public string Name { get; set; }


    public Shot Shoot(Board targetBoard)
    {
        if (!TryReadFromConsole("X", 0, out var xPosition))
        {
            throw new Exception("Invalid input!");
        }

        Console.WriteLine();

        if (!TryReadFromConsole("Y", 0, out var yPosition))
        {
            throw new Exception("Invalid input!");
        }

        if (xPosition == null || yPosition == null)
        {
            throw new Exception("Invalid input!");
        }

        var shotPosition = new Position(
            xPosition.Value,
            yPosition.Value);

        if (!targetBoard.IsInside(shotPosition))
        {
            throw new Exception("Invalid shot position!");
        }

        var ship = targetBoard.FindShip(shotPosition);

        return new Shot(
            targetBoard,
            shotPosition,
            ship);
    }


    private bool TryReadFromConsole(string coordinateName, int roundCount, out int? coordinate)
    {
        Console.WriteLine($"Input your {coordinateName} coordinate for round {roundCount}:");

        var input = Console.ReadLine();

        coordinate = null;

        if (!int.TryParse(input, out var result))
        {
            return false;
        }

        coordinate = result;

        return true;
    }


    public void WriteName()
    {
        Console.WriteLine($"Player: {Name}");
    }
}