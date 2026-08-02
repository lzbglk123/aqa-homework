namespace Battleship;

abstract class Ship
{
    public Position Position { get; }
    public int Length { get; }

    public List<Shot> Shots { get; } = new();

    public bool IsSunk => Shots.Count == Length;

    public Ship(Position position, int length)
    {
        Position = position;
        Length = length;
    }

    public abstract bool IsOnPosition(Position position);

    public abstract bool IsIntersecting(Ship otherShip);
}