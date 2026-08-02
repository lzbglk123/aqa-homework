namespace Battleship;

record Shot(Board Board, Position Position, Ship? Ship)
{
    public ShootResult Result =>
        Ship == null ? ShootResult.Miss : ShootResult.Hit;
}