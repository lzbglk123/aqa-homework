namespace Battleship;

class HorizontalShip : Ship
{
    public HorizontalShip(Position position, int length)
        : base(position, length)
    {
        Console.WriteLine("Horizontal ship created!");
    }


    public override bool IsOnPosition(Position position)
    {
        return position.Y == Position.Y &&
               position.X >= Position.X &&
               position.X < Position.X + Length;
    }


    public override bool IsIntersecting(Ship other)
    {
        for (int x = Position.X; x < Position.X + Length; x++)
        {
            var currentPosition = new Position(x, Position.Y);

            if (other.IsOnPosition(currentPosition))
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