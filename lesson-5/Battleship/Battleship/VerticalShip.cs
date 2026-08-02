namespace Battleship;

class VerticalShip : Ship
{
    public VerticalShip(Position position, int length) : base(position, length)
    {
        Console.WriteLine("Vertical ship created!");
    }

    public override bool IsOnPosition(Position position)
    {
        return position.X == Position.X &&
               position.Y >= Position.Y &&
               position.Y < Position.Y + Length;
    }

    public override bool IsIntersecting(Ship otherShip)
    {
        for (int y = Position.Y; y < Position.Y + Length; y++)
        {
            var position = new Position(Position.X, y);

            if (otherShip.IsOnPosition(position))
            {
                return true;
            }
        }

        return false;
    }
}