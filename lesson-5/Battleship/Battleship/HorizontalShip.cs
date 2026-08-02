namespace Battleship;

class HorizontalShip : Ship
{
    public HorizontalShip(Position position, int length) : base(position, length)
    {
        Console.WriteLine("Horizontal ship created!");
    }

    public override bool IsOnPosition(Position position)
    {
        return position.Y == Position.Y &&
               position.X >= Position.X &&
               position.X < Position.X + Length;
    }

    public override bool IsIntersecting(Ship otherShip)
    {
        for (int x = Position.X; x < Position.X + Length; x++)
        {
            var position = new Position(x, Position.Y);

            if (otherShip.IsOnPosition(position))
            {
                return true;
            }
        }

        return false;
    }

    public void MakeSound()
    {
        Console.WriteLine("Buuuuuuuuuu!");
    }
}