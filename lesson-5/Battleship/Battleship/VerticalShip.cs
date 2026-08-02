namespace Battleship;

class VerticalShip : Ship
{
    public VerticalShip(Position position, int length)
        : base(position, length)
    {
        Console.WriteLine("Vertical ship created!");
    }


    public override bool IsOnPosition(Position position)
    {
        return position.X == Position.X &&
               position.Y >= Position.Y &&
               position.Y < Position.Y + Length;
    }


    public override bool IsIntersecting(Ship other)
    {
        for (int y = Position.Y; y < Position.Y + Length; y++)
        {
            var currentPosition = new Position(Position.X, y);

            if (other.IsOnPosition(currentPosition))
            {
                return true;
            }
        }

        return false;
    }
}