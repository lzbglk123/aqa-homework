namespace Battleship;

class ComputerPlayer : IPlayer
{
    public Position LastShot { get; private set; }


    public ShootResult Shoot(Board targetBoard)
    {
        var random = new Random();

        LastShot = targetBoard.GeneratePosition(random);

        return targetBoard.HasShip(LastShot)
            ? ShootResult.Hit
            : ShootResult.Miss;
    }


    public void WriteName()
    {
        Console.WriteLine("Robot");
    }
}