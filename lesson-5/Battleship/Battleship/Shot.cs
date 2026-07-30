namespace Battleship;

class Shot
{
    public Board Board { get; }
    public Position Position { get; }
    public Ship? Ship { get; }


    public Shot(Board board, Position position, Ship? ship)
    {
        Board = board;
        Position = position;
        Ship = ship;
    }
}